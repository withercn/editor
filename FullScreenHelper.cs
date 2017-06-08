using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Editor
{
    public class FullScreenHelper
    {
        public bool m_bFullScreen = false;
        IntPtr m_OldWndParent = IntPtr.Zero;
        WINDOWPLACEMENT m_OldWndPlacement = new WINDOWPLACEMENT();
        public Control m_control = null;
        public Control m_inputControl = null;
        public FullScreenHelper() { }
        public FullScreenHelper(Control c)
        {
            m_control = c;
            m_inputControl = c;
        }
        struct POINT
        {
            int x;
            int y;
        };
        struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        };
        [DllImport("User32.dll")]
        static extern bool LockWindowUpdate(IntPtr hWndLock);
        //锁定指定窗口，禁止它更新。同时只能有一个窗口处于锁定状态。锁定指定窗口，禁止它更新。同时只能有一个窗口处于锁定状态
        [DllImport("User32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        //函数来设置弹出式窗口，层叠窗口或子窗口的父窗口。新的窗口与窗口必须属于同一应用程序
        [DllImport("User32.dll")]
        static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
        //函数设置指定窗口的显示状态和恢复，最大化，最小化位置。函数功能： 函及原型      
        [DllImport("User32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        //函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号
        [DllImport("User32.dll")]
        static extern IntPtr GetDesktopWindow();
        //该函数返回桌面窗口的句柄。桌面窗口覆盖整个屏幕。桌面窗口是一个要在其上绘制所有的图标和其他窗口的区域
        [DllImport("User32.dll")]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
        //函数名。该函数返回指定窗口的显示状态以及被恢复的、最大化的和最小化的窗口位置
        [DllImport("User32.dll")]
        static extern int GetSystemMetrics(int nIndex);
        //是用于得到被定义的系统数据或者系统配置信息的一个专有名词  
        
        public void FullScreen()
        {
            if (m_bFullScreen)
            {
                LockWindowUpdate(m_control.Handle);
                SetParent(m_control.Handle, m_OldWndParent);
                SetWindowPlacement(m_control.Parent.Handle, ref m_OldWndPlacement);
                SetForegroundWindow(m_OldWndParent);
                LockWindowUpdate(IntPtr.Zero);
                Hook_Clear();
            }
            else
            {
                LockWindowUpdate(m_control.Handle);
                GetWindowPlacement(m_inputControl.Handle, ref m_OldWndPlacement);
                int nScreenWidth = GetSystemMetrics(0);
                int nScreenHeight = GetSystemMetrics(1);
                m_OldWndParent = m_control.Parent.Handle;
                SetParent(m_control.Handle, GetDesktopWindow());
                WINDOWPLACEMENT wp1 = new WINDOWPLACEMENT();
                wp1.length = (uint)Marshal.SizeOf(wp1);
                wp1.showCmd = 1;
                wp1.rcNormalPosition.left = 0;
                wp1.rcNormalPosition.top = 0;
                wp1.rcNormalPosition.right = nScreenWidth;
                wp1.rcNormalPosition.bottom = nScreenHeight;
                SetWindowPlacement(m_control.Handle, ref wp1);
                SetForegroundWindow(GetDesktopWindow());
                SetForegroundWindow(m_inputControl.Handle);
                LockWindowUpdate(IntPtr.Zero);
                Hook_Start();
            }
            m_bFullScreen = !m_bFullScreen;
        }
        struct WINDOWPLACEMENT
        {
            public uint length;
            public uint flags;
            public uint showCmd;
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
            public RECT rcNormalPosition;
        };

        #region 屏蔽键盘第一步:声明API  
        //设置钩子   
        [DllImport("user32.dll")]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //抽掉钩子   
        public static extern bool UnhookWindowsHookEx(int idHook);
        [DllImport("user32.dll")]
        //调用下一个钩子   
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);
        #endregion
        #region 屏蔽键盘第二步: 定义委托  
        public delegate int HookProc(int nCode, int wParam, IntPtr lParam);
        static int hHook = 0;
        public const int WH_KEYBOARD_LL = 13;
        //LowLevel键盘截获，如果是WH_KEYBOARD＝2，并不能对系统键盘截取，Acrobat Reader会在你截取之前获得键盘。   
        HookProc KeyBoardHookProcedure;
        //FileStream MyFs;//用流来屏蔽ctrl+alt+del  
        //键盘Hook结构函数   
        [StructLayout(LayoutKind.Sequential)]
        public class KeyBoardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        #endregion
        #region 屏蔽键盘第三步：编写钩子子程  
        //钩子要做的事，你要处理什么？  
        public static int KeyBoardHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KeyBoardHookStruct kbh = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));
                if (kbh.vkCode == 91) // 截获左win(开始菜单键)  
                {
                    return 1;
                }
                if (kbh.vkCode == 92)// 截获右win  
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Escape && (int)Control.ModifierKeys == (int)Keys.Control) //截获Ctrl+Esc  
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.F4 && (int)Control.ModifierKeys == (int)Keys.Alt) //截获alt+f4  
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.F4 && (int)Control.ModifierKeys == (int)Keys.Alt + (int)Keys.Shift) //截获Alt+Shift+f4  
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Tab && (int)Control.ModifierKeys == (int)Keys.Alt) //截获alt+tab  
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Tab && (int)Control.ModifierKeys == (int)Keys.Alt + (int)Keys.Shift) //截获Alt+Shift+tab  
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Escape && (int)Control.ModifierKeys == (int)Keys.Alt)//截获alt+esc  
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Escape && (int)Control.ModifierKeys == (int)Keys.Alt + (int)Keys.Shift) //截获Alt+Shift+esc  
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Escape && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Shift) //截获Ctrl+Shift+Esc  
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Space && (int)Control.ModifierKeys == (int)Keys.Alt) //截获alt+空格  
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Left && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt)
                    return 1;
                if (kbh.vkCode == (int)Keys.Up && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt)
                    return 1;
                if (kbh.vkCode == (int)Keys.Right && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt)
                    return 1;
                if (kbh.vkCode == (int)Keys.Down && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt)
                    return 1;
                if (kbh.vkCode == 241) //截获F1  
                {
                    return 1;
                }
                //if (kbh.vkCode == (int)Keys.Space && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt) //截获Ctrl+Alt+空格  
                //{  
                // return 1;  
                //}  
            }
            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }
        #endregion
        #region 屏蔽键盘第四步：调用的方法  
        //打开钩子 ,并用流屏蔽任务管理器  
        public void Hook_Start()
        {
            // 安装键盘钩子   
            if (hHook == 0)
            {
                KeyBoardHookProcedure = new HookProc(KeyBoardHookProc);
                hHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyBoardHookProcedure,
                GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
                //如果设置钩子失败.   
                if (hHook == 0)
                {
                    Hook_Clear();
                    //throw new Exception("设置Hook失败!");   
                }
                //MyFs = new FileStream(Environment.ExpandEnvironmentVariables("%windir%\\system32\\taskmgr.exe"), FileMode.Open);
                //byte[] MyByte = new byte[(int)MyFs.Length];
                //MyFs.Write(MyByte, 0, (int)MyFs.Length);
            }
        }
        //PS：也可以通过将[HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System]  
        //下的DisableTaskmgr项的值设为"1”来屏蔽任务管理器。  
        //取消钩子事件 ,并关闭流，取消对任务管理器的屏蔽  
        public void Hook_Clear()
        {
            bool retKeyboard = true;
            if (hHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hHook);
                hHook = 0;
            }
            //if (null != MyFs)
            //{
            //    MyFs.Close();
            //}
            //如果去掉钩子失败.   
            if (!retKeyboard) throw new Exception("UnhookWindowsHookEx failed.");
        }
        #endregion
    }
}

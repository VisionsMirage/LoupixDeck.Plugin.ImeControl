using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace LoupixDeck.Plugin.ImeControl;

[SupportedOSPlatform("windows")]
internal static partial class ImeControl
{
    private const uint WM_IME_CONTROL = 0x0283;
    private const nint IMC_GETOPENSTATUS = 0x0005;
    private const nint IMC_SETOPENSTATUS = 0x0006;

    [StructLayout(LayoutKind.Sequential)]
    private struct Rect
    {
        public int Left, Top, Right, Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct GuiThreadInfo
    {
        public uint cbSize;
        public uint flags;
        public nint hwndActive;
        public nint hwndFocus;
        public nint hwndCapture;
        public nint hwndMenuOwner;
        public nint hwndMoveSize;
        public nint hwndCaret;
        public Rect rcCaret;
    }

    [LibraryImport("user32.dll")]
    private static partial nint GetForegroundWindow();

    [LibraryImport("user32.dll")]
    private static partial uint GetWindowThreadProcessId(nint hWnd, nint lpdwProcessId);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool GetGUIThreadInfo(uint idThread, ref GuiThreadInfo pgui);

    [LibraryImport("user32.dll", EntryPoint = "SendMessageW")]
    private static partial nint SendMessage(nint hWnd, uint msg, nint wParam, nint lParam);

    [LibraryImport("imm32.dll")]
    private static partial nint ImmGetDefaultIMEWnd(nint hWnd);

    /// <summary>
    /// Sets the IME open status of the focused window in the foreground app.
    /// Returns true when the status was changed, false when it already matched
    /// or no IME window was found.
    /// </summary>
    public static bool SetOpenStatus(bool open)
    {
        var hwnd = GetForegroundWindow();
        if (hwnd == 0)
            return false;

        // Prefer the focused child window when there is one.
        var gti = new GuiThreadInfo { cbSize = (uint)Marshal.SizeOf<GuiThreadInfo>() };
        var tid = GetWindowThreadProcessId(hwnd, 0);
        if (GetGUIThreadInfo(tid, ref gti) && gti.hwndFocus != 0)
            hwnd = gti.hwndFocus;

        var imeWnd = ImmGetDefaultIMEWnd(hwnd);
        if (imeWnd == 0)
            return false;

        var isOpen = SendMessage(imeWnd, WM_IME_CONTROL, IMC_GETOPENSTATUS, 0) != 0;
        if (isOpen == open)
            return false;

        SendMessage(imeWnd, WM_IME_CONTROL, IMC_SETOPENSTATUS, open ? 1 : 0);
        return true;
    }
}

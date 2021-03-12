﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System
{
	// Token: 0x020000C2 RID: 194
	public static class Console
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00022E80 File Offset: 0x00021080
		private static object InternalSyncObject
		{
			get
			{
				if (Console.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref Console.s_InternalSyncObject, value, null);
				}
				return Console.s_InternalSyncObject;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00022EB0 File Offset: 0x000210B0
		private static object ReadKeySyncObject
		{
			get
			{
				if (Console.s_ReadKeySyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref Console.s_ReadKeySyncObject, value, null);
				}
				return Console.s_ReadKeySyncObject;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00022EE0 File Offset: 0x000210E0
		private static IntPtr ConsoleInputHandle
		{
			[SecurityCritical]
			get
			{
				if (Console._consoleInputHandle == IntPtr.Zero)
				{
					Console._consoleInputHandle = Win32Native.GetStdHandle(-10);
				}
				return Console._consoleInputHandle;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00022F0A File Offset: 0x0002110A
		private static IntPtr ConsoleOutputHandle
		{
			[SecurityCritical]
			get
			{
				if (Console._consoleOutputHandle == IntPtr.Zero)
				{
					Console._consoleOutputHandle = Win32Native.GetStdHandle(-11);
				}
				return Console._consoleOutputHandle;
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00022F34 File Offset: 0x00021134
		[SecuritySafeCritical]
		private static bool IsHandleRedirected(IntPtr ioHandle)
		{
			SafeFileHandle handle = new SafeFileHandle(ioHandle, false);
			int fileType = Win32Native.GetFileType(handle);
			if ((fileType & 2) != 2)
			{
				return true;
			}
			int num;
			bool consoleMode = Win32Native.GetConsoleMode(ioHandle, out num);
			return !consoleMode;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00022F68 File Offset: 0x00021168
		public static bool IsInputRedirected
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._stdInRedirectQueried)
				{
					return Console._isStdInRedirected;
				}
				object internalSyncObject = Console.InternalSyncObject;
				bool isStdInRedirected;
				lock (internalSyncObject)
				{
					if (Console._stdInRedirectQueried)
					{
						isStdInRedirected = Console._isStdInRedirected;
					}
					else
					{
						Console._isStdInRedirected = Console.IsHandleRedirected(Console.ConsoleInputHandle);
						Console._stdInRedirectQueried = true;
						isStdInRedirected = Console._isStdInRedirected;
					}
				}
				return isStdInRedirected;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00022FE0 File Offset: 0x000211E0
		public static bool IsOutputRedirected
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._stdOutRedirectQueried)
				{
					return Console._isStdOutRedirected;
				}
				object internalSyncObject = Console.InternalSyncObject;
				bool isStdOutRedirected;
				lock (internalSyncObject)
				{
					if (Console._stdOutRedirectQueried)
					{
						isStdOutRedirected = Console._isStdOutRedirected;
					}
					else
					{
						Console._isStdOutRedirected = Console.IsHandleRedirected(Console.ConsoleOutputHandle);
						Console._stdOutRedirectQueried = true;
						isStdOutRedirected = Console._isStdOutRedirected;
					}
				}
				return isStdOutRedirected;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00023058 File Offset: 0x00021258
		public static bool IsErrorRedirected
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._stdErrRedirectQueried)
				{
					return Console._isStdErrRedirected;
				}
				object internalSyncObject = Console.InternalSyncObject;
				bool isStdErrRedirected;
				lock (internalSyncObject)
				{
					if (Console._stdErrRedirectQueried)
					{
						isStdErrRedirected = Console._isStdErrRedirected;
					}
					else
					{
						IntPtr stdHandle = Win32Native.GetStdHandle(-12);
						Console._isStdErrRedirected = Console.IsHandleRedirected(stdHandle);
						Console._stdErrRedirectQueried = true;
						isStdErrRedirected = Console._isStdErrRedirected;
					}
				}
				return isStdErrRedirected;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x000230D4 File Offset: 0x000212D4
		public static TextReader In
		{
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, UI = true)]
			get
			{
				if (Console._in == null)
				{
					object internalSyncObject = Console.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (Console._in == null)
						{
							Stream stream = Console.OpenStandardInput(256);
							TextReader @in;
							if (stream == Stream.Null)
							{
								@in = StreamReader.Null;
							}
							else
							{
								Encoding inputEncoding = Console.InputEncoding;
								@in = TextReader.Synchronized(new StreamReader(stream, inputEncoding, false, 256, true));
							}
							Thread.MemoryBarrier();
							Console._in = @in;
						}
					}
				}
				return Console._in;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0002316C File Offset: 0x0002136C
		public static TextWriter Out
		{
			[HostProtection(SecurityAction.LinkDemand, UI = true)]
			get
			{
				if (Console._out == null)
				{
					Console.InitializeStdOutError(true);
				}
				return Console._out;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00023184 File Offset: 0x00021384
		public static TextWriter Error
		{
			[HostProtection(SecurityAction.LinkDemand, UI = true)]
			get
			{
				if (Console._error == null)
				{
					Console.InitializeStdOutError(false);
				}
				return Console._error;
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002319C File Offset: 0x0002139C
		[SecuritySafeCritical]
		private static void InitializeStdOutError(bool stdout)
		{
			object internalSyncObject = Console.InternalSyncObject;
			lock (internalSyncObject)
			{
				if (!stdout || Console._out == null)
				{
					if (stdout || Console._error == null)
					{
						Stream stream;
						if (stdout)
						{
							stream = Console.OpenStandardOutput(256);
						}
						else
						{
							stream = Console.OpenStandardError(256);
						}
						TextWriter textWriter;
						if (stream == Stream.Null)
						{
							textWriter = TextWriter.Synchronized(StreamWriter.Null);
						}
						else
						{
							Encoding outputEncoding = Console.OutputEncoding;
							textWriter = TextWriter.Synchronized(new StreamWriter(stream, outputEncoding, 256, true)
							{
								HaveWrittenPreamble = true,
								AutoFlush = true
							});
						}
						if (stdout)
						{
							Console._out = textWriter;
						}
						else
						{
							Console._error = textWriter;
						}
					}
				}
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00023270 File Offset: 0x00021470
		private static bool IsStandardConsoleUnicodeEncoding(Encoding encoding)
		{
			UnicodeEncoding unicodeEncoding = encoding as UnicodeEncoding;
			return unicodeEncoding != null && Console.StdConUnicodeEncoding.CodePage == unicodeEncoding.CodePage && Console.StdConUnicodeEncoding.bigEndian == unicodeEncoding.bigEndian;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000232B0 File Offset: 0x000214B0
		private static bool GetUseFileAPIs(int handleType)
		{
			switch (handleType)
			{
			case -12:
				return !Console.IsStandardConsoleUnicodeEncoding(Console.OutputEncoding) || Console.IsErrorRedirected;
			case -11:
				return !Console.IsStandardConsoleUnicodeEncoding(Console.OutputEncoding) || Console.IsOutputRedirected;
			case -10:
				return !Console.IsStandardConsoleUnicodeEncoding(Console.InputEncoding) || Console.IsInputRedirected;
			default:
				return true;
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00023314 File Offset: 0x00021514
		[SecuritySafeCritical]
		private static Stream GetStandardFile(int stdHandleName, FileAccess access, int bufferSize)
		{
			IntPtr stdHandle = Win32Native.GetStdHandle(stdHandleName);
			SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, false);
			if (safeFileHandle.IsInvalid)
			{
				safeFileHandle.SetHandleAsInvalid();
				return Stream.Null;
			}
			if (stdHandleName != -10 && !Console.ConsoleHandleIsWritable(safeFileHandle))
			{
				return Stream.Null;
			}
			bool useFileAPIs = Console.GetUseFileAPIs(stdHandleName);
			return new __ConsoleStream(safeFileHandle, access, useFileAPIs);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00023368 File Offset: 0x00021568
		[SecuritySafeCritical]
		private unsafe static bool ConsoleHandleIsWritable(SafeFileHandle outErrHandle)
		{
			byte b = 65;
			int num2;
			int num = Win32Native.WriteFile(outErrHandle, &b, 0, out num2, IntPtr.Zero);
			return num != 0;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00023390 File Offset: 0x00021590
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0002340C File Offset: 0x0002160C
		public static Encoding InputEncoding
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._inputEncoding != null)
				{
					return Console._inputEncoding;
				}
				object internalSyncObject = Console.InternalSyncObject;
				Encoding inputEncoding;
				lock (internalSyncObject)
				{
					if (Console._inputEncoding != null)
					{
						inputEncoding = Console._inputEncoding;
					}
					else
					{
						uint consoleCP = Win32Native.GetConsoleCP();
						Console._inputEncoding = Encoding.GetEncoding((int)consoleCP);
						inputEncoding = Console._inputEncoding;
					}
				}
				return inputEncoding;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				object internalSyncObject = Console.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (!Console.IsStandardConsoleUnicodeEncoding(value))
					{
						uint codePage = (uint)value.CodePage;
						if (!Win32Native.SetConsoleCP(codePage))
						{
							__Error.WinIOError();
						}
					}
					Console._inputEncoding = (Encoding)value.Clone();
					Console._in = null;
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00023498 File Offset: 0x00021698
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x00023514 File Offset: 0x00021714
		public static Encoding OutputEncoding
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._outputEncoding != null)
				{
					return Console._outputEncoding;
				}
				object internalSyncObject = Console.InternalSyncObject;
				Encoding outputEncoding;
				lock (internalSyncObject)
				{
					if (Console._outputEncoding != null)
					{
						outputEncoding = Console._outputEncoding;
					}
					else
					{
						uint consoleOutputCP = Win32Native.GetConsoleOutputCP();
						Console._outputEncoding = Encoding.GetEncoding((int)consoleOutputCP);
						outputEncoding = Console._outputEncoding;
					}
				}
				return outputEncoding;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				object internalSyncObject = Console.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (Console._out != null && !Console._isOutTextWriterRedirected)
					{
						Console._out.Flush();
						Console._out = null;
					}
					if (Console._error != null && !Console._isErrorTextWriterRedirected)
					{
						Console._error.Flush();
						Console._error = null;
					}
					if (!Console.IsStandardConsoleUnicodeEncoding(value))
					{
						uint codePage = (uint)value.CodePage;
						if (!Win32Native.SetConsoleOutputCP(codePage))
						{
							__Error.WinIOError();
						}
					}
					Console._outputEncoding = (Encoding)value.Clone();
				}
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000235E4 File Offset: 0x000217E4
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static void Beep()
		{
			Console.Beep(800, 200);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000235F8 File Offset: 0x000217F8
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static void Beep(int frequency, int duration)
		{
			if (frequency < 37 || frequency > 32767)
			{
				throw new ArgumentOutOfRangeException("frequency", frequency, Environment.GetResourceString("ArgumentOutOfRange_BeepFrequency", new object[]
				{
					37,
					32767
				}));
			}
			if (duration <= 0)
			{
				throw new ArgumentOutOfRangeException("duration", duration, Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			Win32Native.Beep(frequency, duration);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00023674 File Offset: 0x00021874
		[SecuritySafeCritical]
		public static void Clear()
		{
			Win32Native.COORD coord = default(Win32Native.COORD);
			IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
			if (consoleOutputHandle == Win32Native.INVALID_HANDLE_VALUE)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
			}
			Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
			int num = (int)(bufferInfo.dwSize.X * bufferInfo.dwSize.Y);
			int num2 = 0;
			if (!Win32Native.FillConsoleOutputCharacter(consoleOutputHandle, ' ', num, coord, out num2))
			{
				__Error.WinIOError();
			}
			num2 = 0;
			if (!Win32Native.FillConsoleOutputAttribute(consoleOutputHandle, bufferInfo.wAttributes, num, coord, out num2))
			{
				__Error.WinIOError();
			}
			if (!Win32Native.SetConsoleCursorPosition(consoleOutputHandle, coord))
			{
				__Error.WinIOError();
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00023714 File Offset: 0x00021914
		[SecurityCritical]
		private static Win32Native.Color ConsoleColorToColorAttribute(ConsoleColor color, bool isBackground)
		{
			if ((color & (ConsoleColor)(-16)) != ConsoleColor.Black)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"));
			}
			Win32Native.Color color2 = (Win32Native.Color)color;
			if (isBackground)
			{
				color2 <<= 4;
			}
			return color2;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00023743 File Offset: 0x00021943
		[SecurityCritical]
		private static ConsoleColor ColorAttributeToConsoleColor(Win32Native.Color c)
		{
			if ((c & Win32Native.Color.BackgroundMask) != Win32Native.Color.Black)
			{
				c >>= 4;
			}
			return (ConsoleColor)c;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00023758 File Offset: 0x00021958
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x00023788 File Offset: 0x00021988
		public static ConsoleColor BackgroundColor
		{
			[SecuritySafeCritical]
			get
			{
				bool flag;
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out flag);
				if (!flag)
				{
					return ConsoleColor.Black;
				}
				Win32Native.Color c = (Win32Native.Color)(bufferInfo.wAttributes & 240);
				return Console.ColorAttributeToConsoleColor(c);
			}
			[SecuritySafeCritical]
			set
			{
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				Win32Native.Color color = Console.ConsoleColorToColorAttribute(value, true);
				bool flag;
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out flag);
				if (!flag)
				{
					return;
				}
				short num = bufferInfo.wAttributes;
				num &= -241;
				num = (short)((ushort)num | (ushort)color);
				Win32Native.SetConsoleTextAttribute(Console.ConsoleOutputHandle, num);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x000237D8 File Offset: 0x000219D8
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x00023804 File Offset: 0x00021A04
		public static ConsoleColor ForegroundColor
		{
			[SecuritySafeCritical]
			get
			{
				bool flag;
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out flag);
				if (!flag)
				{
					return ConsoleColor.Gray;
				}
				Win32Native.Color c = (Win32Native.Color)(bufferInfo.wAttributes & 15);
				return Console.ColorAttributeToConsoleColor(c);
			}
			[SecuritySafeCritical]
			set
			{
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				Win32Native.Color color = Console.ConsoleColorToColorAttribute(value, false);
				bool flag;
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out flag);
				if (!flag)
				{
					return;
				}
				short num = bufferInfo.wAttributes;
				num &= -16;
				num = (short)((ushort)num | (ushort)color);
				Win32Native.SetConsoleTextAttribute(Console.ConsoleOutputHandle, num);
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00023854 File Offset: 0x00021A54
		[SecuritySafeCritical]
		public static void ResetColor()
		{
			new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
			bool flag;
			Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo(false, out flag);
			if (!flag)
			{
				return;
			}
			short attributes = (short)Console._defaultColors;
			Win32Native.SetConsoleTextAttribute(Console.ConsoleOutputHandle, attributes);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00023890 File Offset: 0x00021A90
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
		{
			Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, ' ', ConsoleColor.Black, Console.BackgroundColor);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000238B4 File Offset: 0x00021AB4
		[SecuritySafeCritical]
		public unsafe static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			if (sourceForeColor < ConsoleColor.Black || sourceForeColor > ConsoleColor.White)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"), "sourceForeColor");
			}
			if (sourceBackColor < ConsoleColor.Black || sourceBackColor > ConsoleColor.White)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"), "sourceBackColor");
			}
			Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
			Win32Native.COORD dwSize = bufferInfo.dwSize;
			if (sourceLeft < 0 || sourceLeft > (int)dwSize.X)
			{
				throw new ArgumentOutOfRangeException("sourceLeft", sourceLeft, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (sourceTop < 0 || sourceTop > (int)dwSize.Y)
			{
				throw new ArgumentOutOfRangeException("sourceTop", sourceTop, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (sourceWidth < 0 || sourceWidth > (int)dwSize.X - sourceLeft)
			{
				throw new ArgumentOutOfRangeException("sourceWidth", sourceWidth, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (sourceHeight < 0 || sourceTop > (int)dwSize.Y - sourceHeight)
			{
				throw new ArgumentOutOfRangeException("sourceHeight", sourceHeight, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (targetLeft < 0 || targetLeft > (int)dwSize.X)
			{
				throw new ArgumentOutOfRangeException("targetLeft", targetLeft, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (targetTop < 0 || targetTop > (int)dwSize.Y)
			{
				throw new ArgumentOutOfRangeException("targetTop", targetTop, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (sourceWidth == 0 || sourceHeight == 0)
			{
				return;
			}
			new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
			Win32Native.CHAR_INFO[] array = new Win32Native.CHAR_INFO[sourceWidth * sourceHeight];
			dwSize.X = (short)sourceWidth;
			dwSize.Y = (short)sourceHeight;
			Win32Native.COORD bufferCoord = default(Win32Native.COORD);
			Win32Native.SMALL_RECT small_RECT = default(Win32Native.SMALL_RECT);
			small_RECT.Left = (short)sourceLeft;
			small_RECT.Right = (short)(sourceLeft + sourceWidth - 1);
			small_RECT.Top = (short)sourceTop;
			small_RECT.Bottom = (short)(sourceTop + sourceHeight - 1);
			bool flag;
			fixed (Win32Native.CHAR_INFO* ptr = array)
			{
				flag = Win32Native.ReadConsoleOutput(Console.ConsoleOutputHandle, ptr, dwSize, bufferCoord, ref small_RECT);
			}
			if (!flag)
			{
				__Error.WinIOError();
			}
			Win32Native.COORD coord = default(Win32Native.COORD);
			coord.X = (short)sourceLeft;
			Win32Native.Color color = Console.ConsoleColorToColorAttribute(sourceBackColor, true);
			color |= Console.ConsoleColorToColorAttribute(sourceForeColor, false);
			short wColorAttribute = (short)color;
			for (int i = sourceTop; i < sourceTop + sourceHeight; i++)
			{
				coord.Y = (short)i;
				int num;
				if (!Win32Native.FillConsoleOutputCharacter(Console.ConsoleOutputHandle, sourceChar, sourceWidth, coord, out num))
				{
					__Error.WinIOError();
				}
				if (!Win32Native.FillConsoleOutputAttribute(Console.ConsoleOutputHandle, wColorAttribute, sourceWidth, coord, out num))
				{
					__Error.WinIOError();
				}
			}
			Win32Native.SMALL_RECT small_RECT2 = default(Win32Native.SMALL_RECT);
			small_RECT2.Left = (short)targetLeft;
			small_RECT2.Right = (short)(targetLeft + sourceWidth);
			small_RECT2.Top = (short)targetTop;
			small_RECT2.Bottom = (short)(targetTop + sourceHeight);
			fixed (Win32Native.CHAR_INFO* ptr2 = array)
			{
				flag = Win32Native.WriteConsoleOutput(Console.ConsoleOutputHandle, ptr2, dwSize, bufferCoord, ref small_RECT2);
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00023B94 File Offset: 0x00021D94
		[SecurityCritical]
		private static Win32Native.CONSOLE_SCREEN_BUFFER_INFO GetBufferInfo()
		{
			bool flag;
			return Console.GetBufferInfo(true, out flag);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00023BAC File Offset: 0x00021DAC
		[SecuritySafeCritical]
		private static Win32Native.CONSOLE_SCREEN_BUFFER_INFO GetBufferInfo(bool throwOnNoConsole, out bool succeeded)
		{
			succeeded = false;
			IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
			if (!(consoleOutputHandle == Win32Native.INVALID_HANDLE_VALUE))
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO console_SCREEN_BUFFER_INFO;
				if (!Win32Native.GetConsoleScreenBufferInfo(consoleOutputHandle, out console_SCREEN_BUFFER_INFO))
				{
					bool consoleScreenBufferInfo = Win32Native.GetConsoleScreenBufferInfo(Win32Native.GetStdHandle(-12), out console_SCREEN_BUFFER_INFO);
					if (!consoleScreenBufferInfo)
					{
						consoleScreenBufferInfo = Win32Native.GetConsoleScreenBufferInfo(Win32Native.GetStdHandle(-10), out console_SCREEN_BUFFER_INFO);
					}
					if (!consoleScreenBufferInfo)
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error == 6 && !throwOnNoConsole)
						{
							return default(Win32Native.CONSOLE_SCREEN_BUFFER_INFO);
						}
						__Error.WinIOError(lastWin32Error, null);
					}
				}
				if (!Console._haveReadDefaultColors)
				{
					Console._defaultColors = (byte)(console_SCREEN_BUFFER_INFO.wAttributes & 255);
					Console._haveReadDefaultColors = true;
				}
				succeeded = true;
				return console_SCREEN_BUFFER_INFO;
			}
			if (!throwOnNoConsole)
			{
				return default(Win32Native.CONSOLE_SCREEN_BUFFER_INFO);
			}
			throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00023C68 File Offset: 0x00021E68
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x00023C86 File Offset: 0x00021E86
		public static int BufferHeight
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)bufferInfo.dwSize.Y;
			}
			set
			{
				Console.SetBufferSize(Console.BufferWidth, value);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00023C94 File Offset: 0x00021E94
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x00023CB2 File Offset: 0x00021EB2
		public static int BufferWidth
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)bufferInfo.dwSize.X;
			}
			set
			{
				Console.SetBufferSize(value, Console.BufferHeight);
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00023CC0 File Offset: 0x00021EC0
		[SecuritySafeCritical]
		public static void SetBufferSize(int width, int height)
		{
			new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
			Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
			Win32Native.SMALL_RECT srWindow = bufferInfo.srWindow;
			if (width < (int)(srWindow.Right + 1) || width >= 32767)
			{
				throw new ArgumentOutOfRangeException("width", width, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferLessThanWindowSize"));
			}
			if (height < (int)(srWindow.Bottom + 1) || height >= 32767)
			{
				throw new ArgumentOutOfRangeException("height", height, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferLessThanWindowSize"));
			}
			Win32Native.COORD size = default(Win32Native.COORD);
			size.X = (short)width;
			size.Y = (short)height;
			if (!Win32Native.SetConsoleScreenBufferSize(Console.ConsoleOutputHandle, size))
			{
				__Error.WinIOError();
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00023D70 File Offset: 0x00021F70
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x00023D9C File Offset: 0x00021F9C
		public static int WindowHeight
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)(bufferInfo.srWindow.Bottom - bufferInfo.srWindow.Top + 1);
			}
			set
			{
				Console.SetWindowSize(Console.WindowWidth, value);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00023DAC File Offset: 0x00021FAC
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x00023DD8 File Offset: 0x00021FD8
		public static int WindowWidth
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)(bufferInfo.srWindow.Right - bufferInfo.srWindow.Left + 1);
			}
			set
			{
				Console.SetWindowSize(value, Console.WindowHeight);
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00023DE8 File Offset: 0x00021FE8
		[SecuritySafeCritical]
		public unsafe static void SetWindowSize(int width, int height)
		{
			if (width <= 0)
			{
				throw new ArgumentOutOfRangeException("width", width, Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (height <= 0)
			{
				throw new ArgumentOutOfRangeException("height", height, Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
			Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
			bool flag = false;
			Win32Native.COORD size = default(Win32Native.COORD);
			size.X = bufferInfo.dwSize.X;
			size.Y = bufferInfo.dwSize.Y;
			if ((int)bufferInfo.dwSize.X < (int)bufferInfo.srWindow.Left + width)
			{
				if ((int)bufferInfo.srWindow.Left >= 32767 - width)
				{
					throw new ArgumentOutOfRangeException("width", Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowBufferSize"));
				}
				size.X = (short)((int)bufferInfo.srWindow.Left + width);
				flag = true;
			}
			if ((int)bufferInfo.dwSize.Y < (int)bufferInfo.srWindow.Top + height)
			{
				if ((int)bufferInfo.srWindow.Top >= 32767 - height)
				{
					throw new ArgumentOutOfRangeException("height", Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowBufferSize"));
				}
				size.Y = (short)((int)bufferInfo.srWindow.Top + height);
				flag = true;
			}
			if (flag && !Win32Native.SetConsoleScreenBufferSize(Console.ConsoleOutputHandle, size))
			{
				__Error.WinIOError();
			}
			Win32Native.SMALL_RECT srWindow = bufferInfo.srWindow;
			srWindow.Bottom = (short)((int)srWindow.Top + height - 1);
			srWindow.Right = (short)((int)srWindow.Left + width - 1);
			if (!Win32Native.SetConsoleWindowInfo(Console.ConsoleOutputHandle, true, &srWindow))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (flag)
				{
					Win32Native.SetConsoleScreenBufferSize(Console.ConsoleOutputHandle, bufferInfo.dwSize);
				}
				Win32Native.COORD largestConsoleWindowSize = Win32Native.GetLargestConsoleWindowSize(Console.ConsoleOutputHandle);
				if (width > (int)largestConsoleWindowSize.X)
				{
					throw new ArgumentOutOfRangeException("width", width, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowSize_Size", new object[]
					{
						largestConsoleWindowSize.X
					}));
				}
				if (height > (int)largestConsoleWindowSize.Y)
				{
					throw new ArgumentOutOfRangeException("height", height, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowSize_Size", new object[]
					{
						largestConsoleWindowSize.Y
					}));
				}
				__Error.WinIOError(lastWin32Error, string.Empty);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00024028 File Offset: 0x00022228
		public static int LargestWindowWidth
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.COORD largestConsoleWindowSize = Win32Native.GetLargestConsoleWindowSize(Console.ConsoleOutputHandle);
				return (int)largestConsoleWindowSize.X;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00024048 File Offset: 0x00022248
		public static int LargestWindowHeight
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.COORD largestConsoleWindowSize = Win32Native.GetLargestConsoleWindowSize(Console.ConsoleOutputHandle);
				return (int)largestConsoleWindowSize.Y;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00024068 File Offset: 0x00022268
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x00024086 File Offset: 0x00022286
		public static int WindowLeft
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)bufferInfo.srWindow.Left;
			}
			set
			{
				Console.SetWindowPosition(value, Console.WindowTop);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00024094 File Offset: 0x00022294
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x000240B2 File Offset: 0x000222B2
		public static int WindowTop
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)bufferInfo.srWindow.Top;
			}
			set
			{
				Console.SetWindowPosition(Console.WindowLeft, value);
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x000240C0 File Offset: 0x000222C0
		[SecuritySafeCritical]
		public unsafe static void SetWindowPosition(int left, int top)
		{
			new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
			Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
			Win32Native.SMALL_RECT srWindow = bufferInfo.srWindow;
			int num = left + (int)srWindow.Right - (int)srWindow.Left + 1;
			if (left < 0 || num > (int)bufferInfo.dwSize.X || num < 0)
			{
				throw new ArgumentOutOfRangeException("left", left, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowPos"));
			}
			int num2 = top + (int)srWindow.Bottom - (int)srWindow.Top + 1;
			if (top < 0 || num2 > (int)bufferInfo.dwSize.Y || num2 < 0)
			{
				throw new ArgumentOutOfRangeException("top", top, Environment.GetResourceString("ArgumentOutOfRange_ConsoleWindowPos"));
			}
			srWindow.Bottom -= (short)((int)srWindow.Top - top);
			srWindow.Right -= (short)((int)srWindow.Left - left);
			srWindow.Left = (short)left;
			srWindow.Top = (short)top;
			if (!Win32Native.SetConsoleWindowInfo(Console.ConsoleOutputHandle, true, &srWindow))
			{
				__Error.WinIOError();
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x000241C0 File Offset: 0x000223C0
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x000241DE File Offset: 0x000223DE
		public static int CursorLeft
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)bufferInfo.dwCursorPosition.X;
			}
			set
			{
				Console.SetCursorPosition(value, Console.CursorTop);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x000241EC File Offset: 0x000223EC
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x0002420A File Offset: 0x0002240A
		public static int CursorTop
		{
			[SecuritySafeCritical]
			get
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				return (int)bufferInfo.dwCursorPosition.Y;
			}
			set
			{
				Console.SetCursorPosition(Console.CursorLeft, value);
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00024218 File Offset: 0x00022418
		[SecuritySafeCritical]
		public static void SetCursorPosition(int left, int top)
		{
			if (left < 0 || left >= 32767)
			{
				throw new ArgumentOutOfRangeException("left", left, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (top < 0 || top >= 32767)
			{
				throw new ArgumentOutOfRangeException("top", top, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
			IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
			if (!Win32Native.SetConsoleCursorPosition(consoleOutputHandle, new Win32Native.COORD
			{
				X = (short)left,
				Y = (short)top
			}))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = Console.GetBufferInfo();
				if (left < 0 || left >= (int)bufferInfo.dwSize.X)
				{
					throw new ArgumentOutOfRangeException("left", left, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
				}
				if (top < 0 || top >= (int)bufferInfo.dwSize.Y)
				{
					throw new ArgumentOutOfRangeException("top", top, Environment.GetResourceString("ArgumentOutOfRange_ConsoleBufferBoundaries"));
				}
				__Error.WinIOError(lastWin32Error, string.Empty);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00024320 File Offset: 0x00022520
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x0002434C File Offset: 0x0002254C
		public static int CursorSize
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
				Win32Native.CONSOLE_CURSOR_INFO console_CURSOR_INFO;
				if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, out console_CURSOR_INFO))
				{
					__Error.WinIOError();
				}
				return console_CURSOR_INFO.dwSize;
			}
			[SecuritySafeCritical]
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException("value", value, Environment.GetResourceString("ArgumentOutOfRange_CursorSize"));
				}
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
				Win32Native.CONSOLE_CURSOR_INFO console_CURSOR_INFO;
				if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, out console_CURSOR_INFO))
				{
					__Error.WinIOError();
				}
				console_CURSOR_INFO.dwSize = value;
				if (!Win32Native.SetConsoleCursorInfo(consoleOutputHandle, ref console_CURSOR_INFO))
				{
					__Error.WinIOError();
				}
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x000243B8 File Offset: 0x000225B8
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x000243E4 File Offset: 0x000225E4
		public static bool CursorVisible
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
				Win32Native.CONSOLE_CURSOR_INFO console_CURSOR_INFO;
				if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, out console_CURSOR_INFO))
				{
					__Error.WinIOError();
				}
				return console_CURSOR_INFO.bVisible;
			}
			[SecuritySafeCritical]
			set
			{
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
				Win32Native.CONSOLE_CURSOR_INFO console_CURSOR_INFO;
				if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, out console_CURSOR_INFO))
				{
					__Error.WinIOError();
				}
				console_CURSOR_INFO.bVisible = value;
				if (!Win32Native.SetConsoleCursorInfo(consoleOutputHandle, ref console_CURSOR_INFO))
				{
					__Error.WinIOError();
				}
			}
		}

		// Token: 0x06000B50 RID: 2896
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Ansi)]
		private static extern int GetTitleNative(StringHandleOnStack outTitle, out int outTitleLength);

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0002442C File Offset: 0x0002262C
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x00024474 File Offset: 0x00022674
		public static string Title
		{
			[SecuritySafeCritical]
			get
			{
				string result = null;
				int num = -1;
				int titleNative = Console.GetTitleNative(JitHelpers.GetStringHandleOnStack(ref result), out num);
				if (titleNative != 0)
				{
					__Error.WinIOError(titleNative, string.Empty);
				}
				if (num > 24500)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ArgumentOutOfRange_ConsoleTitleTooLong"));
				}
				return result;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 24500)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_ConsoleTitleTooLong"));
				}
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				if (!Win32Native.SetConsoleTitle(value))
				{
					__Error.WinIOError();
				}
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000244C9 File Offset: 0x000226C9
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static ConsoleKeyInfo ReadKey()
		{
			return Console.ReadKey(false);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000244D1 File Offset: 0x000226D1
		[SecurityCritical]
		private static bool IsAltKeyDown(Win32Native.InputRecord ir)
		{
			return (ir.keyEvent.controlKeyState & 3) != 0;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x000244E3 File Offset: 0x000226E3
		[SecurityCritical]
		private static bool IsKeyDownEvent(Win32Native.InputRecord ir)
		{
			return ir.eventType == 1 && ir.keyEvent.keyDown;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x000244FC File Offset: 0x000226FC
		[SecurityCritical]
		private static bool IsModKey(Win32Native.InputRecord ir)
		{
			short virtualKeyCode = ir.keyEvent.virtualKeyCode;
			return (virtualKeyCode >= 16 && virtualKeyCode <= 18) || virtualKeyCode == 20 || virtualKeyCode == 144 || virtualKeyCode == 145;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00024538 File Offset: 0x00022738
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static ConsoleKeyInfo ReadKey(bool intercept)
		{
			int num = -1;
			object readKeySyncObject = Console.ReadKeySyncObject;
			Win32Native.InputRecord cachedInputRecord;
			lock (readKeySyncObject)
			{
				if (Console._cachedInputRecord.eventType == 1)
				{
					cachedInputRecord = Console._cachedInputRecord;
					if (Console._cachedInputRecord.keyEvent.repeatCount == 0)
					{
						Console._cachedInputRecord.eventType = -1;
					}
					else
					{
						Console._cachedInputRecord.keyEvent.repeatCount = Console._cachedInputRecord.keyEvent.repeatCount - 1;
					}
				}
				else
				{
					for (;;)
					{
						bool flag2 = Win32Native.ReadConsoleInput(Console.ConsoleInputHandle, out cachedInputRecord, 1, out num);
						if (!flag2 || num == 0)
						{
							break;
						}
						short virtualKeyCode = cachedInputRecord.keyEvent.virtualKeyCode;
						if ((Console.IsKeyDownEvent(cachedInputRecord) || virtualKeyCode == 18) && (cachedInputRecord.keyEvent.uChar != '\0' || !Console.IsModKey(cachedInputRecord)))
						{
							ConsoleKey consoleKey = (ConsoleKey)virtualKeyCode;
							if (!Console.IsAltKeyDown(cachedInputRecord) || ((consoleKey < ConsoleKey.NumPad0 || consoleKey > ConsoleKey.NumPad9) && consoleKey != ConsoleKey.Clear && consoleKey != ConsoleKey.Insert && (consoleKey < ConsoleKey.PageUp || consoleKey > ConsoleKey.DownArrow)))
							{
								goto IL_F0;
							}
						}
					}
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConsoleReadKeyOnFile"));
					IL_F0:
					if (cachedInputRecord.keyEvent.repeatCount > 1)
					{
						cachedInputRecord.keyEvent.repeatCount = cachedInputRecord.keyEvent.repeatCount - 1;
						Console._cachedInputRecord = cachedInputRecord;
					}
				}
			}
			Console.ControlKeyState controlKeyState = (Console.ControlKeyState)cachedInputRecord.keyEvent.controlKeyState;
			bool shift = (controlKeyState & Console.ControlKeyState.ShiftPressed) > (Console.ControlKeyState)0;
			bool alt = (controlKeyState & (Console.ControlKeyState.RightAltPressed | Console.ControlKeyState.LeftAltPressed)) > (Console.ControlKeyState)0;
			bool control = (controlKeyState & (Console.ControlKeyState.RightCtrlPressed | Console.ControlKeyState.LeftCtrlPressed)) > (Console.ControlKeyState)0;
			ConsoleKeyInfo result = new ConsoleKeyInfo(cachedInputRecord.keyEvent.uChar, (ConsoleKey)cachedInputRecord.keyEvent.virtualKeyCode, shift, alt, control);
			if (!intercept)
			{
				Console.Write(cachedInputRecord.keyEvent.uChar);
			}
			return result;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x000246E4 File Offset: 0x000228E4
		public static bool KeyAvailable
		{
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, UI = true)]
			get
			{
				if (Console._cachedInputRecord.eventType == 1)
				{
					return true;
				}
				Win32Native.InputRecord ir = default(Win32Native.InputRecord);
				int num = 0;
				for (;;)
				{
					if (!Win32Native.PeekConsoleInput(Console.ConsoleInputHandle, out ir, 1, out num))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error == 6)
						{
							break;
						}
						__Error.WinIOError(lastWin32Error, "stdin");
					}
					if (num == 0)
					{
						return false;
					}
					if (Console.IsKeyDownEvent(ir) && !Console.IsModKey(ir))
					{
						return true;
					}
					if (!Win32Native.ReadConsoleInput(Console.ConsoleInputHandle, out ir, 1, out num))
					{
						__Error.WinIOError();
					}
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConsoleKeyAvailableOnFile"));
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00024774 File Offset: 0x00022974
		public static bool NumberLock
		{
			[SecuritySafeCritical]
			get
			{
				short keyState = Win32Native.GetKeyState(144);
				return (keyState & 1) == 1;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00024794 File Offset: 0x00022994
		public static bool CapsLock
		{
			[SecuritySafeCritical]
			get
			{
				short keyState = Win32Native.GetKeyState(20);
				return (keyState & 1) == 1;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x000247B0 File Offset: 0x000229B0
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x000247FC File Offset: 0x000229FC
		public static bool TreatControlCAsInput
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr consoleInputHandle = Console.ConsoleInputHandle;
				if (consoleInputHandle == Win32Native.INVALID_HANDLE_VALUE)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
				}
				int num = 0;
				if (!Win32Native.GetConsoleMode(consoleInputHandle, out num))
				{
					__Error.WinIOError();
				}
				return (num & 1) == 0;
			}
			[SecuritySafeCritical]
			set
			{
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				IntPtr consoleInputHandle = Console.ConsoleInputHandle;
				if (consoleInputHandle == Win32Native.INVALID_HANDLE_VALUE)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
				}
				int num = 0;
				bool consoleMode = Win32Native.GetConsoleMode(consoleInputHandle, out num);
				if (value)
				{
					num &= -2;
				}
				else
				{
					num |= 1;
				}
				if (!Win32Native.SetConsoleMode(consoleInputHandle, num))
				{
					__Error.WinIOError();
				}
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00024860 File Offset: 0x00022A60
		private static bool BreakEvent(int controlType)
		{
			if (controlType != 0 && controlType != 1)
			{
				return false;
			}
			ConsoleCancelEventHandler cancelCallbacks = Console._cancelCallbacks;
			if (cancelCallbacks == null)
			{
				return false;
			}
			ConsoleSpecialKey controlKey = (controlType == 0) ? ConsoleSpecialKey.ControlC : ConsoleSpecialKey.ControlBreak;
			Console.ControlCDelegateData controlCDelegateData = new Console.ControlCDelegateData(controlKey, cancelCallbacks);
			WaitCallback callBack = new WaitCallback(Console.ControlCDelegate);
			if (!ThreadPool.QueueUserWorkItem(callBack, controlCDelegateData))
			{
				return false;
			}
			TimeSpan timeout = new TimeSpan(0, 0, 30);
			controlCDelegateData.CompletionEvent.WaitOne(timeout, false);
			if (!controlCDelegateData.DelegateStarted)
			{
				return false;
			}
			controlCDelegateData.CompletionEvent.WaitOne();
			controlCDelegateData.CompletionEvent.Close();
			return controlCDelegateData.Cancel;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000248EC File Offset: 0x00022AEC
		private static void ControlCDelegate(object data)
		{
			Console.ControlCDelegateData controlCDelegateData = (Console.ControlCDelegateData)data;
			try
			{
				controlCDelegateData.DelegateStarted = true;
				ConsoleCancelEventArgs consoleCancelEventArgs = new ConsoleCancelEventArgs(controlCDelegateData.ControlKey);
				controlCDelegateData.CancelCallbacks(null, consoleCancelEventArgs);
				controlCDelegateData.Cancel = consoleCancelEventArgs.Cancel;
			}
			finally
			{
				controlCDelegateData.CompletionEvent.Set();
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000B5F RID: 2911 RVA: 0x0002494C File Offset: 0x00022B4C
		// (remove) Token: 0x06000B60 RID: 2912 RVA: 0x000249CC File Offset: 0x00022BCC
		public static event ConsoleCancelEventHandler CancelKeyPress
		{
			[SecuritySafeCritical]
			add
			{
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				object internalSyncObject = Console.InternalSyncObject;
				lock (internalSyncObject)
				{
					Console._cancelCallbacks = (ConsoleCancelEventHandler)Delegate.Combine(Console._cancelCallbacks, value);
					if (Console._hooker == null)
					{
						Console._hooker = new Console.ControlCHooker();
						Console._hooker.Hook();
					}
				}
			}
			[SecuritySafeCritical]
			remove
			{
				new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
				object internalSyncObject = Console.InternalSyncObject;
				lock (internalSyncObject)
				{
					Console._cancelCallbacks = (ConsoleCancelEventHandler)Delegate.Remove(Console._cancelCallbacks, value);
					if (Console._hooker != null && Console._cancelCallbacks == null)
					{
						Console._hooker.Unhook();
					}
				}
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00024A48 File Offset: 0x00022C48
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardError()
		{
			return Console.OpenStandardError(256);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00024A54 File Offset: 0x00022C54
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardError(int bufferSize)
		{
			if (bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return Console.GetStandardFile(-12, FileAccess.Write, bufferSize);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00024A78 File Offset: 0x00022C78
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardInput()
		{
			return Console.OpenStandardInput(256);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00024A84 File Offset: 0x00022C84
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardInput(int bufferSize)
		{
			if (bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return Console.GetStandardFile(-10, FileAccess.Read, bufferSize);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00024AA8 File Offset: 0x00022CA8
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardOutput()
		{
			return Console.OpenStandardOutput(256);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00024AB4 File Offset: 0x00022CB4
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardOutput(int bufferSize)
		{
			if (bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return Console.GetStandardFile(-11, FileAccess.Write, bufferSize);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00024AD8 File Offset: 0x00022CD8
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static void SetIn(TextReader newIn)
		{
			if (newIn == null)
			{
				throw new ArgumentNullException("newIn");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			newIn = TextReader.Synchronized(newIn);
			object internalSyncObject = Console.InternalSyncObject;
			lock (internalSyncObject)
			{
				Console._in = newIn;
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00024B3C File Offset: 0x00022D3C
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static void SetOut(TextWriter newOut)
		{
			if (newOut == null)
			{
				throw new ArgumentNullException("newOut");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			Console._isOutTextWriterRedirected = true;
			newOut = TextWriter.Synchronized(newOut);
			object internalSyncObject = Console.InternalSyncObject;
			lock (internalSyncObject)
			{
				Console._out = newOut;
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00024BA8 File Offset: 0x00022DA8
		[SecuritySafeCritical]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public static void SetError(TextWriter newError)
		{
			if (newError == null)
			{
				throw new ArgumentNullException("newError");
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			Console._isErrorTextWriterRedirected = true;
			newError = TextWriter.Synchronized(newError);
			object internalSyncObject = Console.InternalSyncObject;
			lock (internalSyncObject)
			{
				Console._error = newError;
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00024C14 File Offset: 0x00022E14
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static int Read()
		{
			return Console.In.Read();
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00024C20 File Offset: 0x00022E20
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string ReadLine()
		{
			return Console.In.ReadLine();
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00024C2C File Offset: 0x00022E2C
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine()
		{
			Console.Out.WriteLine();
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00024C38 File Offset: 0x00022E38
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(bool value)
		{
			Console.Out.WriteLine(value);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00024C45 File Offset: 0x00022E45
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(char value)
		{
			Console.Out.WriteLine(value);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00024C52 File Offset: 0x00022E52
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(char[] buffer)
		{
			Console.Out.WriteLine(buffer);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00024C5F File Offset: 0x00022E5F
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(char[] buffer, int index, int count)
		{
			Console.Out.WriteLine(buffer, index, count);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00024C6E File Offset: 0x00022E6E
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(decimal value)
		{
			Console.Out.WriteLine(value);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00024C7B File Offset: 0x00022E7B
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(double value)
		{
			Console.Out.WriteLine(value);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00024C88 File Offset: 0x00022E88
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(float value)
		{
			Console.Out.WriteLine(value);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00024C95 File Offset: 0x00022E95
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(int value)
		{
			Console.Out.WriteLine(value);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00024CA2 File Offset: 0x00022EA2
		[CLSCompliant(false)]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(uint value)
		{
			Console.Out.WriteLine(value);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00024CAF File Offset: 0x00022EAF
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(long value)
		{
			Console.Out.WriteLine(value);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00024CBC File Offset: 0x00022EBC
		[CLSCompliant(false)]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(ulong value)
		{
			Console.Out.WriteLine(value);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00024CC9 File Offset: 0x00022EC9
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(object value)
		{
			Console.Out.WriteLine(value);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00024CD6 File Offset: 0x00022ED6
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(string value)
		{
			Console.Out.WriteLine(value);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00024CE3 File Offset: 0x00022EE3
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(string format, object arg0)
		{
			Console.Out.WriteLine(format, arg0);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00024CF1 File Offset: 0x00022EF1
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(string format, object arg0, object arg1)
		{
			Console.Out.WriteLine(format, arg0, arg1);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00024D00 File Offset: 0x00022F00
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			Console.Out.WriteLine(format, arg0, arg1, arg2);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00024D10 File Offset: 0x00022F10
		[CLSCompliant(false)]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator(__arglist);
			int num = argIterator.GetRemainingCount() + 4;
			object[] array = new object[num];
			array[0] = arg0;
			array[1] = arg1;
			array[2] = arg2;
			array[3] = arg3;
			for (int i = 4; i < num; i++)
			{
				array[i] = TypedReference.ToObject(argIterator.GetNextArg());
			}
			Console.Out.WriteLine(format, array);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00024D6F File Offset: 0x00022F6F
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void WriteLine(string format, params object[] arg)
		{
			if (arg == null)
			{
				Console.Out.WriteLine(format, null, null);
				return;
			}
			Console.Out.WriteLine(format, arg);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00024D8E File Offset: 0x00022F8E
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(string format, object arg0)
		{
			Console.Out.Write(format, arg0);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00024D9C File Offset: 0x00022F9C
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(string format, object arg0, object arg1)
		{
			Console.Out.Write(format, arg0, arg1);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00024DAB File Offset: 0x00022FAB
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(string format, object arg0, object arg1, object arg2)
		{
			Console.Out.Write(format, arg0, arg1, arg2);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00024DBC File Offset: 0x00022FBC
		[CLSCompliant(false)]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator(__arglist);
			int num = argIterator.GetRemainingCount() + 4;
			object[] array = new object[num];
			array[0] = arg0;
			array[1] = arg1;
			array[2] = arg2;
			array[3] = arg3;
			for (int i = 4; i < num; i++)
			{
				array[i] = TypedReference.ToObject(argIterator.GetNextArg());
			}
			Console.Out.Write(format, array);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00024E1B File Offset: 0x0002301B
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(string format, params object[] arg)
		{
			if (arg == null)
			{
				Console.Out.Write(format, null, null);
				return;
			}
			Console.Out.Write(format, arg);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00024E3A File Offset: 0x0002303A
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(bool value)
		{
			Console.Out.Write(value);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00024E47 File Offset: 0x00023047
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(char value)
		{
			Console.Out.Write(value);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00024E54 File Offset: 0x00023054
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(char[] buffer)
		{
			Console.Out.Write(buffer);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00024E61 File Offset: 0x00023061
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(char[] buffer, int index, int count)
		{
			Console.Out.Write(buffer, index, count);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00024E70 File Offset: 0x00023070
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(double value)
		{
			Console.Out.Write(value);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00024E7D File Offset: 0x0002307D
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(decimal value)
		{
			Console.Out.Write(value);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00024E8A File Offset: 0x0002308A
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(float value)
		{
			Console.Out.Write(value);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00024E97 File Offset: 0x00023097
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(int value)
		{
			Console.Out.Write(value);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00024EA4 File Offset: 0x000230A4
		[CLSCompliant(false)]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(uint value)
		{
			Console.Out.Write(value);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00024EB1 File Offset: 0x000230B1
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(long value)
		{
			Console.Out.Write(value);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00024EBE File Offset: 0x000230BE
		[CLSCompliant(false)]
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(ulong value)
		{
			Console.Out.Write(value);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00024ECB File Offset: 0x000230CB
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(object value)
		{
			Console.Out.Write(value);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00024ED8 File Offset: 0x000230D8
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Write(string value)
		{
			Console.Out.Write(value);
		}

		// Token: 0x04000465 RID: 1125
		private const int DefaultConsoleBufferSize = 256;

		// Token: 0x04000466 RID: 1126
		private const short AltVKCode = 18;

		// Token: 0x04000467 RID: 1127
		private const int NumberLockVKCode = 144;

		// Token: 0x04000468 RID: 1128
		private const int CapsLockVKCode = 20;

		// Token: 0x04000469 RID: 1129
		private const int MinBeepFrequency = 37;

		// Token: 0x0400046A RID: 1130
		private const int MaxBeepFrequency = 32767;

		// Token: 0x0400046B RID: 1131
		private const int MaxConsoleTitleLength = 24500;

		// Token: 0x0400046C RID: 1132
		private static readonly UnicodeEncoding StdConUnicodeEncoding = new UnicodeEncoding(false, false);

		// Token: 0x0400046D RID: 1133
		private static volatile TextReader _in;

		// Token: 0x0400046E RID: 1134
		private static volatile TextWriter _out;

		// Token: 0x0400046F RID: 1135
		private static volatile TextWriter _error;

		// Token: 0x04000470 RID: 1136
		private static volatile ConsoleCancelEventHandler _cancelCallbacks;

		// Token: 0x04000471 RID: 1137
		private static volatile Console.ControlCHooker _hooker;

		// Token: 0x04000472 RID: 1138
		[SecurityCritical]
		private static Win32Native.InputRecord _cachedInputRecord;

		// Token: 0x04000473 RID: 1139
		private static volatile bool _haveReadDefaultColors;

		// Token: 0x04000474 RID: 1140
		private static volatile byte _defaultColors;

		// Token: 0x04000475 RID: 1141
		private static volatile bool _isOutTextWriterRedirected = false;

		// Token: 0x04000476 RID: 1142
		private static volatile bool _isErrorTextWriterRedirected = false;

		// Token: 0x04000477 RID: 1143
		private static volatile Encoding _inputEncoding = null;

		// Token: 0x04000478 RID: 1144
		private static volatile Encoding _outputEncoding = null;

		// Token: 0x04000479 RID: 1145
		private static volatile bool _stdInRedirectQueried = false;

		// Token: 0x0400047A RID: 1146
		private static volatile bool _stdOutRedirectQueried = false;

		// Token: 0x0400047B RID: 1147
		private static volatile bool _stdErrRedirectQueried = false;

		// Token: 0x0400047C RID: 1148
		private static bool _isStdInRedirected;

		// Token: 0x0400047D RID: 1149
		private static bool _isStdOutRedirected;

		// Token: 0x0400047E RID: 1150
		private static bool _isStdErrRedirected;

		// Token: 0x0400047F RID: 1151
		private static volatile object s_InternalSyncObject;

		// Token: 0x04000480 RID: 1152
		private static volatile object s_ReadKeySyncObject;

		// Token: 0x04000481 RID: 1153
		private static volatile IntPtr _consoleInputHandle;

		// Token: 0x04000482 RID: 1154
		private static volatile IntPtr _consoleOutputHandle;

		// Token: 0x02000AAE RID: 2734
		[Flags]
		internal enum ControlKeyState
		{
			// Token: 0x04003044 RID: 12356
			RightAltPressed = 1,
			// Token: 0x04003045 RID: 12357
			LeftAltPressed = 2,
			// Token: 0x04003046 RID: 12358
			RightCtrlPressed = 4,
			// Token: 0x04003047 RID: 12359
			LeftCtrlPressed = 8,
			// Token: 0x04003048 RID: 12360
			ShiftPressed = 16,
			// Token: 0x04003049 RID: 12361
			NumLockOn = 32,
			// Token: 0x0400304A RID: 12362
			ScrollLockOn = 64,
			// Token: 0x0400304B RID: 12363
			CapsLockOn = 128,
			// Token: 0x0400304C RID: 12364
			EnhancedKey = 256
		}

		// Token: 0x02000AAF RID: 2735
		internal sealed class ControlCHooker : CriticalFinalizerObject
		{
			// Token: 0x060068D1 RID: 26833 RVA: 0x001682AC File Offset: 0x001664AC
			[SecurityCritical]
			internal ControlCHooker()
			{
				this._handler = new Win32Native.ConsoleCtrlHandlerRoutine(Console.BreakEvent);
			}

			// Token: 0x060068D2 RID: 26834 RVA: 0x001682C8 File Offset: 0x001664C8
			~ControlCHooker()
			{
				this.Unhook();
			}

			// Token: 0x060068D3 RID: 26835 RVA: 0x001682F4 File Offset: 0x001664F4
			[SecuritySafeCritical]
			internal void Hook()
			{
				if (!this._hooked)
				{
					if (!Win32Native.SetConsoleCtrlHandler(this._handler, true))
					{
						__Error.WinIOError();
					}
					this._hooked = true;
				}
			}

			// Token: 0x060068D4 RID: 26836 RVA: 0x00168328 File Offset: 0x00166528
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			internal void Unhook()
			{
				if (this._hooked)
				{
					if (!Win32Native.SetConsoleCtrlHandler(this._handler, false))
					{
						__Error.WinIOError();
					}
					this._hooked = false;
				}
			}

			// Token: 0x0400304D RID: 12365
			private bool _hooked;

			// Token: 0x0400304E RID: 12366
			[SecurityCritical]
			private Win32Native.ConsoleCtrlHandlerRoutine _handler;
		}

		// Token: 0x02000AB0 RID: 2736
		private sealed class ControlCDelegateData
		{
			// Token: 0x060068D5 RID: 26837 RVA: 0x00168359 File Offset: 0x00166559
			internal ControlCDelegateData(ConsoleSpecialKey controlKey, ConsoleCancelEventHandler cancelCallbacks)
			{
				this.ControlKey = controlKey;
				this.CancelCallbacks = cancelCallbacks;
				this.CompletionEvent = new ManualResetEvent(false);
			}

			// Token: 0x0400304F RID: 12367
			internal ConsoleSpecialKey ControlKey;

			// Token: 0x04003050 RID: 12368
			internal bool Cancel;

			// Token: 0x04003051 RID: 12369
			internal bool DelegateStarted;

			// Token: 0x04003052 RID: 12370
			internal ManualResetEvent CompletionEvent;

			// Token: 0x04003053 RID: 12371
			internal ConsoleCancelEventHandler CancelCallbacks;
		}
	}
}

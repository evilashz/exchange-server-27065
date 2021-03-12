using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x020003C7 RID: 967
	internal static class Log
	{
		// Token: 0x06003218 RID: 12824 RVA: 0x000C0455 File Offset: 0x000BE655
		static Log()
		{
			Log.GlobalSwitch.MinimumLevel = LoggingLevels.ErrorLevel;
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000C0494 File Offset: 0x000BE694
		public static void AddOnLogMessage(LogMessageEventHandler handler)
		{
			object obj = Log.locker;
			lock (obj)
			{
				Log._LogMessageEventHandler = (LogMessageEventHandler)Delegate.Combine(Log._LogMessageEventHandler, handler);
			}
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x000C04E4 File Offset: 0x000BE6E4
		public static void RemoveOnLogMessage(LogMessageEventHandler handler)
		{
			object obj = Log.locker;
			lock (obj)
			{
				Log._LogMessageEventHandler = (LogMessageEventHandler)Delegate.Remove(Log._LogMessageEventHandler, handler);
			}
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000C0534 File Offset: 0x000BE734
		public static void AddOnLogSwitchLevel(LogSwitchLevelHandler handler)
		{
			object obj = Log.locker;
			lock (obj)
			{
				Log._LogSwitchLevelHandler = (LogSwitchLevelHandler)Delegate.Combine(Log._LogSwitchLevelHandler, handler);
			}
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000C0588 File Offset: 0x000BE788
		public static void RemoveOnLogSwitchLevel(LogSwitchLevelHandler handler)
		{
			object obj = Log.locker;
			lock (obj)
			{
				Log._LogSwitchLevelHandler = (LogSwitchLevelHandler)Delegate.Remove(Log._LogSwitchLevelHandler, handler);
			}
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000C05DC File Offset: 0x000BE7DC
		internal static void InvokeLogSwitchLevelHandlers(LogSwitch ls, LoggingLevels newLevel)
		{
			LogSwitchLevelHandler logSwitchLevelHandler = Log._LogSwitchLevelHandler;
			if (logSwitchLevelHandler != null)
			{
				logSwitchLevelHandler(ls, newLevel);
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x000C05FC File Offset: 0x000BE7FC
		// (set) Token: 0x0600321F RID: 12831 RVA: 0x000C0605 File Offset: 0x000BE805
		public static bool IsConsoleEnabled
		{
			get
			{
				return Log.m_fConsoleDeviceEnabled;
			}
			set
			{
				Log.m_fConsoleDeviceEnabled = value;
			}
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000C060F File Offset: 0x000BE80F
		public static void LogMessage(LoggingLevels level, string message)
		{
			Log.LogMessage(level, Log.GlobalSwitch, message);
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000C0620 File Offset: 0x000BE820
		public static void LogMessage(LoggingLevels level, LogSwitch logswitch, string message)
		{
			if (logswitch == null)
			{
				throw new ArgumentNullException("LogSwitch");
			}
			if (level < LoggingLevels.TraceLevel0)
			{
				throw new ArgumentOutOfRangeException("level", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (logswitch.CheckLevel(level))
			{
				Debugger.Log((int)level, logswitch.strName, message);
				if (Log.m_fConsoleDeviceEnabled)
				{
					Console.Write(message);
				}
			}
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000C0679 File Offset: 0x000BE879
		public static void Trace(LogSwitch logswitch, string message)
		{
			Log.LogMessage(LoggingLevels.TraceLevel0, logswitch, message);
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000C0684 File Offset: 0x000BE884
		public static void Trace(string switchname, string message)
		{
			LogSwitch @switch = LogSwitch.GetSwitch(switchname);
			Log.LogMessage(LoggingLevels.TraceLevel0, @switch, message);
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000C06A0 File Offset: 0x000BE8A0
		public static void Trace(string message)
		{
			Log.LogMessage(LoggingLevels.TraceLevel0, Log.GlobalSwitch, message);
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000C06AE File Offset: 0x000BE8AE
		public static void Status(LogSwitch logswitch, string message)
		{
			Log.LogMessage(LoggingLevels.StatusLevel0, logswitch, message);
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000C06BC File Offset: 0x000BE8BC
		public static void Status(string switchname, string message)
		{
			LogSwitch @switch = LogSwitch.GetSwitch(switchname);
			Log.LogMessage(LoggingLevels.StatusLevel0, @switch, message);
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000C06D9 File Offset: 0x000BE8D9
		public static void Status(string message)
		{
			Log.LogMessage(LoggingLevels.StatusLevel0, Log.GlobalSwitch, message);
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000C06E8 File Offset: 0x000BE8E8
		public static void Warning(LogSwitch logswitch, string message)
		{
			Log.LogMessage(LoggingLevels.WarningLevel, logswitch, message);
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000C06F4 File Offset: 0x000BE8F4
		public static void Warning(string switchname, string message)
		{
			LogSwitch @switch = LogSwitch.GetSwitch(switchname);
			Log.LogMessage(LoggingLevels.WarningLevel, @switch, message);
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000C0711 File Offset: 0x000BE911
		public static void Warning(string message)
		{
			Log.LogMessage(LoggingLevels.WarningLevel, Log.GlobalSwitch, message);
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000C0720 File Offset: 0x000BE920
		public static void Error(LogSwitch logswitch, string message)
		{
			Log.LogMessage(LoggingLevels.ErrorLevel, logswitch, message);
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000C072C File Offset: 0x000BE92C
		public static void Error(string switchname, string message)
		{
			LogSwitch @switch = LogSwitch.GetSwitch(switchname);
			Log.LogMessage(LoggingLevels.ErrorLevel, @switch, message);
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000C0749 File Offset: 0x000BE949
		public static void Error(string message)
		{
			Log.LogMessage(LoggingLevels.ErrorLevel, Log.GlobalSwitch, message);
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000C0758 File Offset: 0x000BE958
		public static void Panic(string message)
		{
			Log.LogMessage(LoggingLevels.PanicLevel, Log.GlobalSwitch, message);
		}

		// Token: 0x0600322F RID: 12847
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void AddLogSwitch(LogSwitch logSwitch);

		// Token: 0x06003230 RID: 12848
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ModifyLogSwitch(int iNewLevel, string strSwitchName, string strParentName);

		// Token: 0x040015FA RID: 5626
		internal static Hashtable m_Hashtable = new Hashtable();

		// Token: 0x040015FB RID: 5627
		private static volatile bool m_fConsoleDeviceEnabled = false;

		// Token: 0x040015FC RID: 5628
		private static LogMessageEventHandler _LogMessageEventHandler;

		// Token: 0x040015FD RID: 5629
		private static volatile LogSwitchLevelHandler _LogSwitchLevelHandler;

		// Token: 0x040015FE RID: 5630
		private static object locker = new object();

		// Token: 0x040015FF RID: 5631
		public static readonly LogSwitch GlobalSwitch = new LogSwitch("Global", "Global Switch for this log");
	}
}

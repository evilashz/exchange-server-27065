using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CmdletLogAdapter : DisposeTrackableBase, ILogger, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000172 RID: 370 RVA: 0x00006154 File Offset: 0x00004354
		public CmdletLogAdapter(ILogger secondaryLog, Action<LocalizedString> verboseLogDelegate, Action<LocalizedString> warningLogDelegate, Action<LocalizedString> debugLogDelegate)
		{
			this.verboseLogDelegate = (verboseLogDelegate ?? new Action<LocalizedString>(this.VoidLog));
			this.warningLogDelegate = (warningLogDelegate ?? new Action<LocalizedString>(this.VoidLog));
			this.debugLogDelegate = (debugLogDelegate ?? new Action<LocalizedString>(this.VoidLog));
			this.secondaryLog = (secondaryLog ?? NullAnchorLogger.Instance);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000061C0 File Offset: 0x000043C0
		public void Log(MigrationEventType eventType, Exception exception, string format, params object[] args)
		{
			Action<LocalizedString> logDelegateFromEventType = this.GetLogDelegateFromEventType(eventType);
			if (exception != null)
			{
				this.LogOnDelegate(logDelegateFromEventType, eventType, CmdletLogAdapter.GetMessageWithException(exception, format, args), new object[0]);
			}
			else
			{
				this.LogOnDelegate(logDelegateFromEventType, eventType, format, args);
			}
			this.secondaryLog.Log(eventType, exception, format, args);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000620C File Offset: 0x0000440C
		public void Log(MigrationEventType eventType, string format, params object[] args)
		{
			Action<LocalizedString> logDelegateFromEventType = this.GetLogDelegateFromEventType(eventType);
			this.LogOnDelegate(logDelegateFromEventType, eventType, format, args);
			this.secondaryLog.Log(eventType, format, args);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000623C File Offset: 0x0000443C
		public void Log(string source, MigrationEventType eventType, object context, string format, params object[] args)
		{
			Action<LocalizedString> logDelegateFromEventType = this.GetLogDelegateFromEventType(eventType);
			this.LogOnDelegate(logDelegateFromEventType, eventType, format, args);
			this.secondaryLog.Log(source, eventType, context, format, args);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000626F File Offset: 0x0000446F
		public void LogError(Exception exception, string formatString, params object[] formatArgs)
		{
			this.LogOnDelegate(this.warningLogDelegate, MigrationEventType.Error, CmdletLogAdapter.GetMessageWithException(exception, formatString, formatArgs), new object[0]);
			this.secondaryLog.LogError(exception, formatString, formatArgs);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000629A File Offset: 0x0000449A
		public void LogEvent(MigrationEventType eventType, params string[] args)
		{
			this.secondaryLog.LogEvent(eventType, args);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000062A9 File Offset: 0x000044A9
		public void LogEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, params string[] args)
		{
			this.secondaryLog.LogEvent(eventType, eventId, args);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000062B9 File Offset: 0x000044B9
		public void LogEvent(MigrationEventType eventType, Exception ex, params string[] args)
		{
			this.secondaryLog.LogEvent(eventType, ex, args);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000062C9 File Offset: 0x000044C9
		public void LogEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, Exception ex, params string[] args)
		{
			this.secondaryLog.LogEvent(eventType, eventId, ex, args);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000062DB File Offset: 0x000044DB
		public void LogInformation(string formatString, params object[] formatArgs)
		{
			this.LogOnDelegate(this.verboseLogDelegate, MigrationEventType.Information, formatString, formatArgs);
			this.secondaryLog.LogInformation(formatString, formatArgs);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000062F9 File Offset: 0x000044F9
		public void LogTerseEvent(MigrationEventType eventType, params string[] args)
		{
			this.secondaryLog.LogTerseEvent(eventType, args);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006308 File Offset: 0x00004508
		public void LogTerseEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, params string[] args)
		{
			this.secondaryLog.LogTerseEvent(eventType, eventId, args);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006318 File Offset: 0x00004518
		public void LogTerseEvent(MigrationEventType eventType, Exception ex, params string[] args)
		{
			this.secondaryLog.LogTerseEvent(eventType, ex, args);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006328 File Offset: 0x00004528
		public void LogTerseEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, Exception ex, params string[] args)
		{
			this.secondaryLog.LogTerseEvent(eventType, eventId, ex, args);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000633A File Offset: 0x0000453A
		public void LogVerbose(string formatString, params object[] formatArgs)
		{
			this.LogOnDelegate(this.verboseLogDelegate, MigrationEventType.Verbose, formatString, formatArgs);
			this.secondaryLog.LogVerbose(formatString, formatArgs);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006358 File Offset: 0x00004558
		public void LogWarning(string formatString, params object[] formatArgs)
		{
			this.secondaryLog.LogWarning(formatString, formatArgs);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006367 File Offset: 0x00004567
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006369 File Offset: 0x00004569
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CmdletLogAdapter>(this);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006371 File Offset: 0x00004571
		private static string GetMessageWithException(Exception exception, string format, object[] args)
		{
			return string.Format("{0}. Exception: {1}", string.Format(format, args), exception);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006388 File Offset: 0x00004588
		private Action<LocalizedString> GetLogDelegateFromEventType(MigrationEventType eventType)
		{
			switch (eventType)
			{
			case MigrationEventType.Error:
			case MigrationEventType.Warning:
				return this.warningLogDelegate;
			case MigrationEventType.Information:
			case MigrationEventType.Verbose:
				return this.verboseLogDelegate;
			default:
				return this.debugLogDelegate;
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000063C8 File Offset: 0x000045C8
		private void LogOnDelegate(Action<LocalizedString> logDelegate, MigrationEventType logType, string message, params object[] formatArgs)
		{
			string arg;
			if (formatArgs.Length == 0)
			{
				arg = message;
			}
			else
			{
				arg = string.Format(message, formatArgs);
			}
			logDelegate(new LocalizedString(string.Format("[{0}] {1}", logType, arg)));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006404 File Offset: 0x00004604
		private void VoidLog(LocalizedString obj)
		{
		}

		// Token: 0x04000071 RID: 113
		private readonly Action<LocalizedString> debugLogDelegate;

		// Token: 0x04000072 RID: 114
		private readonly ILogger secondaryLog;

		// Token: 0x04000073 RID: 115
		private readonly Action<LocalizedString> verboseLogDelegate;

		// Token: 0x04000074 RID: 116
		private readonly Action<LocalizedString> warningLogDelegate;
	}
}

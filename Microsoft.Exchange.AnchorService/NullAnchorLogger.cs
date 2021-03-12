using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NullAnchorLogger : DisposeTrackableBase, ILogger, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600019F RID: 415 RVA: 0x00006908 File Offset: 0x00004B08
		static NullAnchorLogger()
		{
			NullAnchorLogger.Instance.SuppressDisposeTracker();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000691E File Offset: 0x00004B1E
		private NullAnchorLogger()
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006926 File Offset: 0x00004B26
		void ILogger.Log(MigrationEventType eventType, Exception exception, string format, params object[] args)
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006928 File Offset: 0x00004B28
		void ILogger.Log(MigrationEventType eventType, string format, params object[] args)
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000692A File Offset: 0x00004B2A
		void ILogger.Log(string source, MigrationEventType eventType, object context, string format, params object[] args)
		{
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000692C File Offset: 0x00004B2C
		void ILogger.LogError(Exception exception, string formatString, params object[] formatArgs)
		{
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000692E File Offset: 0x00004B2E
		void ILogger.LogEvent(MigrationEventType eventType, params string[] args)
		{
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006930 File Offset: 0x00004B30
		void ILogger.LogEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, params string[] args)
		{
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006932 File Offset: 0x00004B32
		void ILogger.LogEvent(MigrationEventType eventType, Exception ex, params string[] args)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006934 File Offset: 0x00004B34
		void ILogger.LogEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, Exception ex, params string[] args)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006936 File Offset: 0x00004B36
		void ILogger.LogInformation(string formatString, params object[] formatArgs)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006938 File Offset: 0x00004B38
		void ILogger.LogTerseEvent(MigrationEventType eventType, params string[] args)
		{
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000693A File Offset: 0x00004B3A
		void ILogger.LogTerseEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, params string[] args)
		{
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000693C File Offset: 0x00004B3C
		void ILogger.LogTerseEvent(MigrationEventType eventType, Exception ex, params string[] args)
		{
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000693E File Offset: 0x00004B3E
		void ILogger.LogTerseEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, Exception ex, params string[] args)
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006940 File Offset: 0x00004B40
		void ILogger.LogVerbose(string formatString, params object[] formatArgs)
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006942 File Offset: 0x00004B42
		void ILogger.LogWarning(string formatString, params object[] formatArgs)
		{
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006944 File Offset: 0x00004B44
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006946 File Offset: 0x00004B46
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<NullAnchorLogger>(this);
		}

		// Token: 0x0400007B RID: 123
		public static readonly ILogger Instance = new NullAnchorLogger();
	}
}

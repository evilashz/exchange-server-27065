using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000AE RID: 174
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class TaskPerformanceData
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001A6C5 File Offset: 0x000188C5
		public static PerformanceDataProvider CmdletInvoked
		{
			get
			{
				if (TaskPerformanceData.cmdletInvoked == null)
				{
					TaskPerformanceData.cmdletInvoked = new PerformanceDataProvider("Cmdlet Invoked");
				}
				return TaskPerformanceData.cmdletInvoked;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001A6E2 File Offset: 0x000188E2
		public static PerformanceDataProvider BeginProcessingInvoked
		{
			get
			{
				if (TaskPerformanceData.beginProcessingInvoked == null)
				{
					TaskPerformanceData.beginProcessingInvoked = new PerformanceDataProvider("BeginProcessing Invoked");
				}
				return TaskPerformanceData.beginProcessingInvoked;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001A6FF File Offset: 0x000188FF
		public static PerformanceDataProvider ProcessRecordInvoked
		{
			get
			{
				if (TaskPerformanceData.processRecordInvoked == null)
				{
					TaskPerformanceData.processRecordInvoked = new PerformanceDataProvider("ProcessRecord Invoked");
				}
				return TaskPerformanceData.processRecordInvoked;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001A71C File Offset: 0x0001891C
		public static PerformanceDataProvider EndProcessingInvoked
		{
			get
			{
				if (TaskPerformanceData.endProcessingInvoked == null)
				{
					TaskPerformanceData.endProcessingInvoked = new PerformanceDataProvider("EndProcessing Invoked");
				}
				return TaskPerformanceData.endProcessingInvoked;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001A739 File Offset: 0x00018939
		public static PerformanceDataProvider InternalValidate
		{
			get
			{
				if (TaskPerformanceData.internalValidate == null)
				{
					TaskPerformanceData.internalValidate = new PerformanceDataProvider("InternalValidate");
				}
				return TaskPerformanceData.internalValidate;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001A756 File Offset: 0x00018956
		public static PerformanceDataProvider InternalStateReset
		{
			get
			{
				if (TaskPerformanceData.internalStateReset == null)
				{
					TaskPerformanceData.internalStateReset = new PerformanceDataProvider("InternalStateReset");
				}
				return TaskPerformanceData.internalStateReset;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001A773 File Offset: 0x00018973
		public static PerformanceDataProvider InternalProcessRecord
		{
			get
			{
				if (TaskPerformanceData.internalProcessRecord == null)
				{
					TaskPerformanceData.internalProcessRecord = new PerformanceDataProvider("InternalProcessRecord");
				}
				return TaskPerformanceData.internalProcessRecord;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001A790 File Offset: 0x00018990
		public static PerformanceDataProvider SaveInitial
		{
			get
			{
				if (TaskPerformanceData.saveInitial == null)
				{
					TaskPerformanceData.saveInitial = new PerformanceDataProvider("SaveInitial");
				}
				return TaskPerformanceData.saveInitial;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001A7AD File Offset: 0x000189AD
		public static PerformanceDataProvider ReadUpdated
		{
			get
			{
				if (TaskPerformanceData.readUpdated == null)
				{
					TaskPerformanceData.readUpdated = new PerformanceDataProvider("ReadUpdated");
				}
				return TaskPerformanceData.readUpdated;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001A7CA File Offset: 0x000189CA
		public static PerformanceDataProvider SaveResult
		{
			get
			{
				if (TaskPerformanceData.saveResult == null)
				{
					TaskPerformanceData.saveResult = new PerformanceDataProvider("SaveResult");
				}
				return TaskPerformanceData.saveResult;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001A7E7 File Offset: 0x000189E7
		public static PerformanceDataProvider ReadResult
		{
			get
			{
				if (TaskPerformanceData.readResult == null)
				{
					TaskPerformanceData.readResult = new PerformanceDataProvider("ReadResult");
				}
				return TaskPerformanceData.readResult;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001A804 File Offset: 0x00018A04
		public static PerformanceDataProvider WriteResult
		{
			get
			{
				if (TaskPerformanceData.writeResult == null)
				{
					TaskPerformanceData.writeResult = new PerformanceDataProvider("WriteResult");
				}
				return TaskPerformanceData.writeResult;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001A821 File Offset: 0x00018A21
		public static PerformanceDataProvider WindowsLiveIdProvisioningHandlerForNew
		{
			get
			{
				if (TaskPerformanceData.windowsLiveIdProvisioningHandlerForNew == null)
				{
					TaskPerformanceData.windowsLiveIdProvisioningHandlerForNew = new PerformanceDataProvider("WindowsLiveIdProvisioningHandlerForNew");
				}
				return TaskPerformanceData.windowsLiveIdProvisioningHandlerForNew;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001A83E File Offset: 0x00018A3E
		public static PerformanceDataProvider MailboxProvisioningHandler
		{
			get
			{
				if (TaskPerformanceData.mailboxProvisioningHandler == null)
				{
					TaskPerformanceData.mailboxProvisioningHandler = new PerformanceDataProvider("MailboxProvisioningHandler");
				}
				return TaskPerformanceData.mailboxProvisioningHandler;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001A85B File Offset: 0x00018A5B
		public static PerformanceDataProvider AdminLogProvisioningHandler
		{
			get
			{
				if (TaskPerformanceData.adminLogProvisioningHandler == null)
				{
					TaskPerformanceData.adminLogProvisioningHandler = new PerformanceDataProvider("AdminLogProvisioningHandler");
				}
				return TaskPerformanceData.adminLogProvisioningHandler;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001A878 File Offset: 0x00018A78
		public static PerformanceDataProvider OtherProvisioningHandlers
		{
			get
			{
				if (TaskPerformanceData.otherProvisioningHandlers == null)
				{
					TaskPerformanceData.otherProvisioningHandlers = new PerformanceDataProvider("OtherProvisioningHandlers");
				}
				return TaskPerformanceData.otherProvisioningHandlers;
			}
		}

		// Token: 0x04000187 RID: 391
		[ThreadStatic]
		private static PerformanceDataProvider cmdletInvoked;

		// Token: 0x04000188 RID: 392
		[ThreadStatic]
		private static PerformanceDataProvider beginProcessingInvoked;

		// Token: 0x04000189 RID: 393
		[ThreadStatic]
		private static PerformanceDataProvider processRecordInvoked;

		// Token: 0x0400018A RID: 394
		[ThreadStatic]
		private static PerformanceDataProvider endProcessingInvoked;

		// Token: 0x0400018B RID: 395
		[ThreadStatic]
		private static PerformanceDataProvider internalValidate;

		// Token: 0x0400018C RID: 396
		[ThreadStatic]
		private static PerformanceDataProvider internalStateReset;

		// Token: 0x0400018D RID: 397
		[ThreadStatic]
		private static PerformanceDataProvider internalProcessRecord;

		// Token: 0x0400018E RID: 398
		[ThreadStatic]
		private static PerformanceDataProvider saveInitial;

		// Token: 0x0400018F RID: 399
		[ThreadStatic]
		private static PerformanceDataProvider readUpdated;

		// Token: 0x04000190 RID: 400
		[ThreadStatic]
		private static PerformanceDataProvider saveResult;

		// Token: 0x04000191 RID: 401
		[ThreadStatic]
		private static PerformanceDataProvider readResult;

		// Token: 0x04000192 RID: 402
		[ThreadStatic]
		private static PerformanceDataProvider writeResult;

		// Token: 0x04000193 RID: 403
		[ThreadStatic]
		private static PerformanceDataProvider windowsLiveIdProvisioningHandlerForNew;

		// Token: 0x04000194 RID: 404
		[ThreadStatic]
		private static PerformanceDataProvider mailboxProvisioningHandler;

		// Token: 0x04000195 RID: 405
		[ThreadStatic]
		private static PerformanceDataProvider adminLogProvisioningHandler;

		// Token: 0x04000196 RID: 406
		[ThreadStatic]
		private static PerformanceDataProvider otherProvisioningHandlers;
	}
}

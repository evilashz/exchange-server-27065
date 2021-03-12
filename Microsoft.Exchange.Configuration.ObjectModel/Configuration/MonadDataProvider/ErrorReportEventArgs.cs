using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001A2 RID: 418
	internal class ErrorReportEventArgs : RunGuidEventArgs
	{
		// Token: 0x06000F1A RID: 3866 RVA: 0x0002BA2B File Offset: 0x00029C2B
		public ErrorReportEventArgs(Guid guid, ErrorRecord errorRecord, int objectIndex, MonadCommand command) : base(guid)
		{
			if (errorRecord == null)
			{
				throw new ArgumentNullException("errorRecord");
			}
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			this.errorRecord = errorRecord;
			this.objectIndex = objectIndex;
			this.command = command;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0002BA67 File Offset: 0x00029C67
		public ErrorRecord ErrorRecord
		{
			get
			{
				return this.errorRecord;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0002BA6F File Offset: 0x00029C6F
		public int ObjectIndex
		{
			get
			{
				return this.objectIndex;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0002BA77 File Offset: 0x00029C77
		public MonadCommand Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x04000323 RID: 803
		private ErrorRecord errorRecord;

		// Token: 0x04000324 RID: 804
		private int objectIndex;

		// Token: 0x04000325 RID: 805
		private MonadCommand command;
	}
}

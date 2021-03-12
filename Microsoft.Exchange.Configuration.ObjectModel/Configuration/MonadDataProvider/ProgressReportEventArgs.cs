using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001DE RID: 478
	internal class ProgressReportEventArgs : EventArgs
	{
		// Token: 0x06001133 RID: 4403 RVA: 0x00034B03 File Offset: 0x00032D03
		public ProgressReportEventArgs(ProgressRecord progressRecord, MonadCommand command)
		{
			if (progressRecord == null)
			{
				throw new ArgumentNullException("progressRecord");
			}
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			this.progressRecord = progressRecord;
			this.command = command;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00034B35 File Offset: 0x00032D35
		public ProgressRecord ProgressRecord
		{
			get
			{
				return this.progressRecord;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00034B3D File Offset: 0x00032D3D
		public int ObjectIndex
		{
			get
			{
				return this.ProgressRecord.ActivityId;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00034B4A File Offset: 0x00032D4A
		public MonadCommand Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x040003D0 RID: 976
		private ProgressRecord progressRecord;

		// Token: 0x040003D1 RID: 977
		private MonadCommand command;
	}
}

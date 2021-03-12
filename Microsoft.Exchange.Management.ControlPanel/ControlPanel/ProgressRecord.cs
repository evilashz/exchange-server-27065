using System;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006BA RID: 1722
	[DataContract]
	public class ProgressRecord
	{
		// Token: 0x06004953 RID: 18771 RVA: 0x000E0026 File Offset: 0x000DE226
		public ProgressRecord()
		{
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x000E0039 File Offset: 0x000DE239
		public ProgressRecord(ProgressRecord progressRecord)
		{
			this.Percent = ((progressRecord.PercentComplete >= 0) ? progressRecord.PercentComplete : 100);
			this.Status = progressRecord.StatusDescription;
		}

		// Token: 0x170027DD RID: 10205
		// (get) Token: 0x06004955 RID: 18773 RVA: 0x000E0071 File Offset: 0x000DE271
		public object SyncRoot
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x170027DE RID: 10206
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x000E0079 File Offset: 0x000DE279
		// (set) Token: 0x06004957 RID: 18775 RVA: 0x000E0081 File Offset: 0x000DE281
		[DataMember]
		public int Percent { get; set; }

		// Token: 0x170027DF RID: 10207
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x000E008A File Offset: 0x000DE28A
		// (set) Token: 0x06004959 RID: 18777 RVA: 0x000E0092 File Offset: 0x000DE292
		[DataMember]
		public string Status { get; set; }

		// Token: 0x170027E0 RID: 10208
		// (get) Token: 0x0600495A RID: 18778 RVA: 0x000E009B File Offset: 0x000DE29B
		// (set) Token: 0x0600495B RID: 18779 RVA: 0x000E00A3 File Offset: 0x000DE2A3
		[DataMember]
		public int MaxCount { get; set; }

		// Token: 0x170027E1 RID: 10209
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x000E00AC File Offset: 0x000DE2AC
		// (set) Token: 0x0600495D RID: 18781 RVA: 0x000E00B4 File Offset: 0x000DE2B4
		[DataMember]
		public int SucceededCount { get; set; }

		// Token: 0x170027E2 RID: 10210
		// (get) Token: 0x0600495E RID: 18782 RVA: 0x000E00BD File Offset: 0x000DE2BD
		// (set) Token: 0x0600495F RID: 18783 RVA: 0x000E00C5 File Offset: 0x000DE2C5
		[DataMember]
		public int FailedCount { get; set; }

		// Token: 0x170027E3 RID: 10211
		// (get) Token: 0x06004960 RID: 18784 RVA: 0x000E00CE File Offset: 0x000DE2CE
		// (set) Token: 0x06004961 RID: 18785 RVA: 0x000E00D6 File Offset: 0x000DE2D6
		[DataMember]
		public ErrorRecord[] Errors { get; set; }

		// Token: 0x170027E4 RID: 10212
		// (get) Token: 0x06004962 RID: 18786 RVA: 0x000E00DF File Offset: 0x000DE2DF
		// (set) Token: 0x06004963 RID: 18787 RVA: 0x000E00E7 File Offset: 0x000DE2E7
		[DataMember]
		public bool HasCompleted { get; set; }

		// Token: 0x170027E5 RID: 10213
		// (get) Token: 0x06004964 RID: 18788 RVA: 0x000E00F0 File Offset: 0x000DE2F0
		// (set) Token: 0x06004965 RID: 18789 RVA: 0x000E00F8 File Offset: 0x000DE2F8
		[DataMember]
		public bool IsCancelled { get; set; }

		// Token: 0x04003151 RID: 12625
		private object syncRoot = new object();
	}
}

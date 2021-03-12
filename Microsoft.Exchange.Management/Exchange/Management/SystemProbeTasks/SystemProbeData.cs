using System;

namespace Microsoft.Exchange.Management.SystemProbeTasks
{
	// Token: 0x02000DA9 RID: 3497
	[Serializable]
	public class SystemProbeData
	{
		// Token: 0x170029B8 RID: 10680
		// (get) Token: 0x0600860B RID: 34315 RVA: 0x0022452A File Offset: 0x0022272A
		// (set) Token: 0x0600860C RID: 34316 RVA: 0x00224532 File Offset: 0x00222732
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x170029B9 RID: 10681
		// (get) Token: 0x0600860D RID: 34317 RVA: 0x0022453B File Offset: 0x0022273B
		// (set) Token: 0x0600860E RID: 34318 RVA: 0x00224543 File Offset: 0x00222743
		public DateTime TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
			set
			{
				this.timeStamp = value;
			}
		}

		// Token: 0x170029BA RID: 10682
		// (get) Token: 0x0600860F RID: 34319 RVA: 0x0022454C File Offset: 0x0022274C
		// (set) Token: 0x06008610 RID: 34320 RVA: 0x00224554 File Offset: 0x00222754
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x040040FF RID: 16639
		private Guid guid;

		// Token: 0x04004100 RID: 16640
		private DateTime timeStamp;

		// Token: 0x04004101 RID: 16641
		private string text;
	}
}

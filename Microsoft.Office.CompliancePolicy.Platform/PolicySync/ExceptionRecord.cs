using System;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000EC RID: 236
	[Serializable]
	public sealed class ExceptionRecord
	{
		// Token: 0x06000672 RID: 1650 RVA: 0x00014687 File Offset: 0x00012887
		public ExceptionRecord()
		{
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00014690 File Offset: 0x00012890
		public ExceptionRecord(Exception exception)
		{
			this.Type = exception.GetType().ToString();
			this.Message = exception.Message;
			this.Tag = exception.GetHashCode().ToString();
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x000146D4 File Offset: 0x000128D4
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x000146DC File Offset: 0x000128DC
		public string Type { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x000146E5 File Offset: 0x000128E5
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x000146ED File Offset: 0x000128ED
		public string Message { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x000146F6 File Offset: 0x000128F6
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x000146FE File Offset: 0x000128FE
		public string Tag { get; set; }
	}
}

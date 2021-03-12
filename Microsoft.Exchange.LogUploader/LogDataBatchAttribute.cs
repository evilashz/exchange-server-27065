using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000019 RID: 25
	internal class LogDataBatchAttribute : Attribute
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005938 File Offset: 0x00003B38
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00005940 File Offset: 0x00003B40
		public bool IsRawBatch
		{
			get
			{
				return this.isRawBatch;
			}
			set
			{
				this.isRawBatch = value;
			}
		}

		// Token: 0x0400009A RID: 154
		private bool isRawBatch;
	}
}

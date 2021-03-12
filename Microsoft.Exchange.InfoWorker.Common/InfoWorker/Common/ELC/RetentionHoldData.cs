using System;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x020001B0 RID: 432
	internal struct RetentionHoldData
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x00030F56 File Offset: 0x0002F156
		internal RetentionHoldData(bool holdEnabled, string comment, string url)
		{
			this.HoldEnabled = holdEnabled;
			this.Comment = comment;
			this.Url = url;
		}

		// Token: 0x04000886 RID: 2182
		internal bool HoldEnabled;

		// Token: 0x04000887 RID: 2183
		internal string Comment;

		// Token: 0x04000888 RID: 2184
		internal string Url;
	}
}

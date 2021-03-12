using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x02000165 RID: 357
	[Serializable]
	public abstract class SubmitMessageRequest : UMRpcRequest
	{
		// Token: 0x06000B59 RID: 2905 RVA: 0x0002A216 File Offset: 0x00028416
		public SubmitMessageRequest()
		{
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002A21E File Offset: 0x0002841E
		internal SubmitMessageRequest(ADUser user) : base(user)
		{
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0002A227 File Offset: 0x00028427
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x0002A22F File Offset: 0x0002842F
		public string To
		{
			get
			{
				return this.to;
			}
			set
			{
				this.to = value;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0002A238 File Offset: 0x00028438
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x0002A240 File Offset: 0x00028440
		public string PIN
		{
			get
			{
				return this.pin;
			}
			set
			{
				this.pin = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0002A249 File Offset: 0x00028449
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x0002A251 File Offset: 0x00028451
		public string Extension
		{
			get
			{
				return this.extension;
			}
			set
			{
				this.extension = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0002A25A File Offset: 0x0002845A
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x0002A262 File Offset: 0x00028462
		public string[] AccessNumbers
		{
			get
			{
				return this.accessNumbers;
			}
			set
			{
				this.accessNumbers = value;
			}
		}

		// Token: 0x04000619 RID: 1561
		private string to;

		// Token: 0x0400061A RID: 1562
		private string pin;

		// Token: 0x0400061B RID: 1563
		private string extension;

		// Token: 0x0400061C RID: 1564
		private string[] accessNumbers;
	}
}

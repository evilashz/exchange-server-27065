using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200013D RID: 317
	internal struct BlobType
	{
		// Token: 0x06000C53 RID: 3155 RVA: 0x00026818 File Offset: 0x00024A18
		public BlobType(string value)
		{
			this.value = value;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00026821 File Offset: 0x00024A21
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000614 RID: 1556
		private readonly string value;
	}
}

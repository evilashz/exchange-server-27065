using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000284 RID: 644
	internal class TagNode
	{
		// Token: 0x060017AE RID: 6062 RVA: 0x0008C8AA File Offset: 0x0008AAAA
		public TagNode(ushort nameSpace, ushort tag)
		{
			this.nameSpace = nameSpace;
			this.tag = tag;
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x0008C8C0 File Offset: 0x0008AAC0
		// (set) Token: 0x060017B0 RID: 6064 RVA: 0x0008C8C8 File Offset: 0x0008AAC8
		public ushort NameSpace
		{
			get
			{
				return this.nameSpace;
			}
			set
			{
				this.nameSpace = value;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x0008C8D1 File Offset: 0x0008AAD1
		// (set) Token: 0x060017B2 RID: 6066 RVA: 0x0008C8D9 File Offset: 0x0008AAD9
		public ushort Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		// Token: 0x04000E8E RID: 3726
		private ushort nameSpace;

		// Token: 0x04000E8F RID: 3727
		private ushort tag;
	}
}

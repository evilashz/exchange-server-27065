using System;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x0200016F RID: 367
	[Serializable]
	internal abstract class MultiMailboxSearchBase
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00009B54 File Offset: 0x00008F54
		internal int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00009B68 File Offset: 0x00008F68
		// (set) Token: 0x060008EB RID: 2283 RVA: 0x00009B80 File Offset: 0x00008F80
		internal Guid CorrelationId
		{
			get
			{
				return this.queryCorrelationId;
			}
			set
			{
				this.queryCorrelationId = value;
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00009BB0 File Offset: 0x00008FB0
		protected MultiMailboxSearchBase()
		{
			this.version = MultiMailboxSearchBase.CurrentVersion;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00009B94 File Offset: 0x00008F94
		protected MultiMailboxSearchBase(int version)
		{
			this.version = version;
		}

		// Token: 0x04000B01 RID: 2817
		private readonly int version;

		// Token: 0x04000B02 RID: 2818
		private Guid queryCorrelationId;

		// Token: 0x04000B03 RID: 2819
		protected static int CurrentVersion = 1;
	}
}

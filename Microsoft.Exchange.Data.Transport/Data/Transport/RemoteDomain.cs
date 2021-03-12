using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200007B RID: 123
	public abstract class RemoteDomain
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002B4 RID: 692
		public abstract string NameSpecification { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002B5 RID: 693
		public abstract string NonMimeCharset { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002B6 RID: 694
		public abstract bool IsInternal { get; }
	}
}

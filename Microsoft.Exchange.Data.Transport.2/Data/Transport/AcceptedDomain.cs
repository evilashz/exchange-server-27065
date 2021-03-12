using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000049 RID: 73
	public abstract class AcceptedDomain
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AF RID: 431
		public abstract bool IsInCorporation { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001B0 RID: 432
		public abstract bool UseAddressBook { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001B1 RID: 433
		public abstract string NameSpecification { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B2 RID: 434
		public abstract Guid TenantId { get; }
	}
}

using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000618 RID: 1560
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RawImGroup
	{
		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x000B6C8A File Offset: 0x000B4E8A
		// (set) Token: 0x060030FD RID: 12541 RVA: 0x000B6C92 File Offset: 0x000B4E92
		public string DisplayName { get; set; }

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x000B6C9B File Offset: 0x000B4E9B
		// (set) Token: 0x060030FF RID: 12543 RVA: 0x000B6CA3 File Offset: 0x000B4EA3
		public string GroupType { get; set; }

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x000B6CAC File Offset: 0x000B4EAC
		// (set) Token: 0x06003101 RID: 12545 RVA: 0x000B6CB4 File Offset: 0x000B4EB4
		public StoreObjectId ExchangeStoreId { get; set; }

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x000B6CBD File Offset: 0x000B4EBD
		// (set) Token: 0x06003103 RID: 12547 RVA: 0x000B6CC5 File Offset: 0x000B4EC5
		public StoreObjectId[] MemberCorrelationKey { get; set; }

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x000B6CCE File Offset: 0x000B4ECE
		// (set) Token: 0x06003105 RID: 12549 RVA: 0x000B6CD6 File Offset: 0x000B4ED6
		public ExtendedPropertyType[] ExtendedProperties { get; set; }

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000B6CDF File Offset: 0x000B4EDF
		// (set) Token: 0x06003107 RID: 12551 RVA: 0x000B6CE7 File Offset: 0x000B4EE7
		public string SmtpAddress { get; set; }
	}
}

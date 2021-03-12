using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000529 RID: 1321
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PersonPropertyAggregationContext : PropertyAggregationContext
	{
		// Token: 0x060038D4 RID: 14548 RVA: 0x000E8F4F File Offset: 0x000E714F
		public PersonPropertyAggregationContext(IList<IStorePropertyBag> sources, ContactFolders contactFolders, string clientInfoString) : base(sources)
		{
			this.contactFolders = contactFolders;
			this.clientInfoString = clientInfoString;
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x060038D5 RID: 14549 RVA: 0x000E8F66 File Offset: 0x000E7166
		public ContactFolders ContactFolders
		{
			get
			{
				return this.contactFolders;
			}
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x000E8F6E File Offset: 0x000E716E
		public string ClientInfoString
		{
			get
			{
				return this.clientInfoString;
			}
		}

		// Token: 0x04001E2F RID: 7727
		private readonly ContactFolders contactFolders;

		// Token: 0x04001E30 RID: 7728
		private readonly string clientInfoString;
	}
}

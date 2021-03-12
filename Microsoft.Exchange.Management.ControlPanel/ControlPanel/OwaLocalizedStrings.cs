using System;
using Microsoft.Exchange.Clients;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006B5 RID: 1717
	internal static class OwaLocalizedStrings
	{
		// Token: 0x170027CA RID: 10186
		// (get) Token: 0x06004915 RID: 18709 RVA: 0x000DF70D File Offset: 0x000DD90D
		internal static LocalizedString ErrorWrongCASServerBecauseOfOutOfDateDNSCache
		{
			get
			{
				return new LocalizedString(-23402676.ToString(), OwaLocalizedStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x04003136 RID: 12598
		private static readonly ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager(typeof(Strings).FullName, typeof(Strings).Assembly);
	}
}

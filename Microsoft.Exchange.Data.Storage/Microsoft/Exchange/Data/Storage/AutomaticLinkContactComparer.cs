using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000492 RID: 1170
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class AutomaticLinkContactComparer
	{
		// Token: 0x060033E7 RID: 13287
		internal abstract ContactLinkingOperation Match(IContactLinkingMatchProperties contact1, IContactLinkingMatchProperties contact2);

		// Token: 0x060033E8 RID: 13288 RVA: 0x000D3110 File Offset: 0x000D1310
		protected bool MatchEmails(IContactLinkingMatchProperties contact1, IContactLinkingMatchProperties contact2)
		{
			return contact1.EmailAddresses.Overlaps(contact2.EmailAddresses);
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000D3123 File Offset: 0x000D1323
		protected bool EqualsIgnoreCaseAndWhiteSpace(string s1, string s2)
		{
			return !string.IsNullOrWhiteSpace(s1) && !string.IsNullOrWhiteSpace(s2) && string.Equals(s1.Trim(), s2.Trim(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04001BF0 RID: 7152
		private const CompareOptions IgnoreCaseWhiteSpaceAndDiacriticsCompareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;
	}
}

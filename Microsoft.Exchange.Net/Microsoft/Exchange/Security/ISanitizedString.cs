using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A3D RID: 2621
	public interface ISanitizedString<SanitizingPolicy> where SanitizingPolicy : ISanitizingPolicy, new()
	{
		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06003908 RID: 14600
		// (set) Token: 0x06003909 RID: 14601
		string UntrustedValue { get; set; }

		// Token: 0x0600390A RID: 14602
		void DecreeToBeTrusted();

		// Token: 0x0600390B RID: 14603
		void DecreeToBeUntrusted();

		// Token: 0x0600390C RID: 14604
		string ToString();
	}
}

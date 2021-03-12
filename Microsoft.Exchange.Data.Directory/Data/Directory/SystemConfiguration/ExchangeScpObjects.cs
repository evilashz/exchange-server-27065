using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200045D RID: 1117
	internal static class ExchangeScpObjects
	{
		// Token: 0x0200045E RID: 1118
		internal static class AutodiscoverContainer
		{
			// Token: 0x04002265 RID: 8805
			public const string Name = "Microsoft Exchange Autodiscover";

			// Token: 0x04002266 RID: 8806
			public const string CN = "CN=Microsoft Exchange Autodiscover";
		}

		// Token: 0x0200045F RID: 1119
		internal static class AutodiscoverUrlKeyword
		{
			// Token: 0x04002267 RID: 8807
			public const string Value = "77378F46-2C66-4aa9-A6A6-3E7A48B19596";

			// Token: 0x04002268 RID: 8808
			public static readonly QueryFilter Filter = new TextFilter(ADServiceConnectionPointSchema.Keywords, "77378F46-2C66-4aa9-A6A6-3E7A48B19596", MatchOptions.FullString, MatchFlags.IgnoreCase);
		}

		// Token: 0x02000460 RID: 1120
		internal static class AutodiscoverDomainPointerKeyword
		{
			// Token: 0x04002269 RID: 8809
			public const string Value = "67661d7F-8FC4-4fa7-BFAC-E1D7794C1F68";

			// Token: 0x0400226A RID: 8810
			public static readonly QueryFilter Filter = new TextFilter(ADServiceConnectionPointSchema.Keywords, "67661d7F-8FC4-4fa7-BFAC-E1D7794C1F68", MatchOptions.FullString, MatchFlags.IgnoreCase);
		}

		// Token: 0x02000461 RID: 1121
		internal static class AutodiscoverTrustedHosterKeyword
		{
			// Token: 0x0400226B RID: 8811
			public const string Value = "D3614C7C-D214-4F1F-BD4C-00D91C67F93F";

			// Token: 0x0400226C RID: 8812
			public static readonly QueryFilter Filter = new TextFilter(ADServiceConnectionPointSchema.Keywords, "D3614C7C-D214-4F1F-BD4C-00D91C67F93F", MatchOptions.FullString, MatchFlags.IgnoreCase);
		}

		// Token: 0x02000462 RID: 1122
		internal static class DomainToApplicationUriKeyword
		{
			// Token: 0x0400226D RID: 8813
			public const string Value = "E1AA5F5E-2341-4FCB-8560-E3AB6F081468";

			// Token: 0x0400226E RID: 8814
			public const string DomainPrefix = "Domain=";

			// Token: 0x0400226F RID: 8815
			public static readonly QueryFilter Filter = new TextFilter(ADServiceConnectionPointSchema.Keywords, "E1AA5F5E-2341-4FCB-8560-E3AB6F081468", MatchOptions.FullString, MatchFlags.IgnoreCase);
		}
	}
}

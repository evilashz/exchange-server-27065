using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000490 RID: 1168
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AutomaticLinkConfiguration
	{
		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x000D2FC7 File Offset: 0x000D11C7
		public static bool IsOWAEnabled
		{
			get
			{
				return AutomaticLinkConfiguration.IsComponentEnabled(AutomaticLinkConfiguration.Components.OWA);
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000D2FCF File Offset: 0x000D11CF
		public static bool IsMOMTEnabled
		{
			get
			{
				return AutomaticLinkConfiguration.IsComponentEnabled(AutomaticLinkConfiguration.Components.MOMT);
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x000D2FD7 File Offset: 0x000D11D7
		public static bool IsBulkEnabled
		{
			get
			{
				return AutomaticLinkConfiguration.IsComponentEnabled(AutomaticLinkConfiguration.Components.Bulk);
			}
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000D2FDF File Offset: 0x000D11DF
		internal static void EnableAll()
		{
			AutomaticLinkConfiguration.enableAutomaticLinking = new AutomaticLinkConfiguration.Components?(AutomaticLinkConfiguration.Components.All);
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000D2FEC File Offset: 0x000D11EC
		internal static void EnableDefault()
		{
			AutomaticLinkConfiguration.enableAutomaticLinking = new AutomaticLinkConfiguration.Components?(AutomaticLinkConfiguration.Components.Default);
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x000D2FFC File Offset: 0x000D11FC
		internal static void Enable(AutomaticLinkConfiguration.Components components)
		{
			AutomaticLinkConfiguration.enableAutomaticLinking |= components;
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000D3038 File Offset: 0x000D1238
		internal static void Disable(AutomaticLinkConfiguration.Components components)
		{
			AutomaticLinkConfiguration.enableAutomaticLinking &= ~components;
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000D3078 File Offset: 0x000D1278
		private static bool IsComponentEnabled(AutomaticLinkConfiguration.Components component)
		{
			if (AutomaticLinkConfiguration.enableAutomaticLinking == null)
			{
				AutomaticLinkConfiguration.enableAutomaticLinking = new AutomaticLinkConfiguration.Components?((AutomaticLinkConfiguration.Components)Util.GetRegistryValueOrDefault(AutomaticLinkConfiguration.RegistryKeysLocation, AutomaticLinkConfiguration.EnableAutomaticLinkingValueName, 5, AutomaticLinkConfiguration.Tracer));
			}
			return (AutomaticLinkConfiguration.enableAutomaticLinking & component) != (AutomaticLinkConfiguration.Components)0;
		}

		// Token: 0x04001BE6 RID: 7142
		private static readonly Trace Tracer = ExTraceGlobals.ContactLinkingTracer;

		// Token: 0x04001BE7 RID: 7143
		internal static readonly string RegistryKeysLocation = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\People";

		// Token: 0x04001BE8 RID: 7144
		internal static readonly string EnableAutomaticLinkingValueName = "EnableAutomaticLinking";

		// Token: 0x04001BE9 RID: 7145
		private static AutomaticLinkConfiguration.Components? enableAutomaticLinking;

		// Token: 0x02000491 RID: 1169
		internal enum Components
		{
			// Token: 0x04001BEB RID: 7147
			OWA = 1,
			// Token: 0x04001BEC RID: 7148
			MOMT,
			// Token: 0x04001BED RID: 7149
			Bulk = 4,
			// Token: 0x04001BEE RID: 7150
			Default,
			// Token: 0x04001BEF RID: 7151
			All = 7
		}
	}
}

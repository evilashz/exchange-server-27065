using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000163 RID: 355
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class OABVariantConfigurationSettings
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0003B00C File Offset: 0x0003920C
		public static bool IsMultitenancyEnabled
		{
			get
			{
				return OABVariantConfigurationSettings.isMultitenancyEnabled.Member;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0003B018 File Offset: 0x00039218
		public static bool IsSharedTemplateFilesEnabled
		{
			get
			{
				return OABVariantConfigurationSettings.isSharedTemplateFilesEnabled.Member;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0003B024 File Offset: 0x00039224
		public static bool IsGenerateRequestedOABsOnlyEnabled
		{
			get
			{
				return OABVariantConfigurationSettings.isGenerateRequestedOABsOnlyEnabled.Member;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0003B030 File Offset: 0x00039230
		public static bool IsLinkedOABGenMailboxesEnabled
		{
			get
			{
				return OABVariantConfigurationSettings.isLinkedOABGenMailboxesEnabled.Member;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0003B03C File Offset: 0x0003923C
		public static bool IsEnforceManifestVersionMatchEnabled
		{
			get
			{
				return OABVariantConfigurationSettings.isEnforceManifestVersionMatchEnabled.Member;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0003B048 File Offset: 0x00039248
		public static bool OabHttpClientAccessRulesEnabled
		{
			get
			{
				return OABVariantConfigurationSettings.oabHttpClientAccessRulesEnabled.Member;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0003B054 File Offset: 0x00039254
		public static bool IsSkipServiceTopologyDiscoveryEnabled
		{
			get
			{
				return OABVariantConfigurationSettings.skipServiceTopologyDiscovery.Member;
			}
		}

		// Token: 0x04000789 RID: 1929
		private static LazyMember<bool> isMultitenancyEnabled = new LazyMember<bool>(() => VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled);

		// Token: 0x0400078A RID: 1930
		private static LazyMember<bool> isSharedTemplateFilesEnabled = new LazyMember<bool>(() => VariantConfiguration.InvariantNoFlightingSnapshot.OAB.SharedTemplateFiles.Enabled);

		// Token: 0x0400078B RID: 1931
		private static LazyMember<bool> isGenerateRequestedOABsOnlyEnabled = new LazyMember<bool>(() => VariantConfiguration.InvariantNoFlightingSnapshot.OAB.GenerateRequestedOABsOnly.Enabled);

		// Token: 0x0400078C RID: 1932
		private static LazyMember<bool> isLinkedOABGenMailboxesEnabled = new LazyMember<bool>(() => VariantConfiguration.InvariantNoFlightingSnapshot.OAB.LinkedOABGenMailboxes.Enabled);

		// Token: 0x0400078D RID: 1933
		private static LazyMember<bool> isEnforceManifestVersionMatchEnabled = new LazyMember<bool>(() => VariantConfiguration.InvariantNoFlightingSnapshot.OAB.EnforceManifestVersionMatch.Enabled);

		// Token: 0x0400078E RID: 1934
		private static LazyMember<bool> oabHttpClientAccessRulesEnabled = new LazyMember<bool>(() => VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OAB.OabHttpClientAccessRulesEnabled.Enabled);

		// Token: 0x0400078F RID: 1935
		private static LazyMember<bool> skipServiceTopologyDiscovery = new LazyMember<bool>(() => VariantConfiguration.InvariantNoFlightingSnapshot.OAB.SkipServiceTopologyDiscovery.Enabled);
	}
}

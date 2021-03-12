using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200050F RID: 1295
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class OscProviderRegistry
	{
		// Token: 0x060037C6 RID: 14278 RVA: 0x000E15B4 File Offset: 0x000DF7B4
		internal static Guid GetGuidFromName(string name)
		{
			Guid result;
			if (!OscProviderRegistry.TryGetGuidFromName(name, out result))
			{
				throw new ArgumentException("Unknown provider", "name");
			}
			return result;
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000E15DC File Offset: 0x000DF7DC
		internal static bool TryGetGuidFromName(string name, out Guid providerGuid)
		{
			OscProviderRegistry.OscProviderInfo oscProviderInfo;
			if (OscProviderRegistry.NameToInfoMap.TryGetValue(name, out oscProviderInfo))
			{
				providerGuid = oscProviderInfo.ProviderId;
				return true;
			}
			if (Guid.TryParse(name, out providerGuid))
			{
				return true;
			}
			providerGuid = Guid.Empty;
			return false;
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000E1620 File Offset: 0x000DF820
		internal static bool TryGetNameFromGuid(Guid providerGuid, out string name)
		{
			OscProviderRegistry.OscProviderInfo oscProviderInfo;
			if (OscProviderRegistry.GuidToInfoMap.TryGetValue(providerGuid, out oscProviderInfo))
			{
				name = oscProviderInfo.Name;
				return true;
			}
			name = string.Empty;
			return false;
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000E1650 File Offset: 0x000DF850
		internal static bool TryGetNetworkId(string provider, out string networkId)
		{
			OscProviderRegistry.OscProviderInfo oscProviderInfo;
			if (OscProviderRegistry.NameToInfoMap.TryGetValue(provider, out oscProviderInfo))
			{
				if (string.IsNullOrWhiteSpace(oscProviderInfo.NetworkId))
				{
					networkId = string.Empty;
					return false;
				}
				networkId = oscProviderInfo.NetworkId;
				return true;
			}
			else
			{
				Guid key;
				if (!Guid.TryParse(provider, out key) || !OscProviderRegistry.GuidToInfoMap.TryGetValue(key, out oscProviderInfo))
				{
					networkId = string.Empty;
					return false;
				}
				if (string.IsNullOrWhiteSpace(oscProviderInfo.NetworkId))
				{
					networkId = string.Empty;
					return false;
				}
				networkId = oscProviderInfo.NetworkId;
				return true;
			}
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000E16CD File Offset: 0x000DF8CD
		internal static DefaultFolderType GetParentFolder(Guid provider)
		{
			return DefaultFolderType.Root;
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000E16D4 File Offset: 0x000DF8D4
		internal static string GetDefaultFolderDisplayName(Guid provider)
		{
			OscProviderRegistry.OscProviderInfo oscProviderInfo;
			if (!OscProviderRegistry.GuidToInfoMap.TryGetValue(provider, out oscProviderInfo))
			{
				throw new ArgumentException("Unknown provider", "provider");
			}
			if (string.IsNullOrWhiteSpace(oscProviderInfo.DefaultFolderDisplayName))
			{
				throw new ArgumentException("Unknown folder display name", "provider");
			}
			return oscProviderInfo.DefaultFolderDisplayName;
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000E1724 File Offset: 0x000DF924
		internal static bool TryGetDefaultFolderDisplayName(Guid provider, out string defaultDisplayName)
		{
			OscProviderRegistry.OscProviderInfo oscProviderInfo;
			if (!OscProviderRegistry.GuidToInfoMap.TryGetValue(provider, out oscProviderInfo))
			{
				defaultDisplayName = string.Empty;
				return false;
			}
			if (string.IsNullOrWhiteSpace(oscProviderInfo.DefaultFolderDisplayName))
			{
				defaultDisplayName = string.Empty;
				return false;
			}
			defaultDisplayName = oscProviderInfo.DefaultFolderDisplayName;
			return true;
		}

		// Token: 0x04001DB1 RID: 7601
		private static readonly OscProviderRegistry.OscProviderInfo FacebookInfo = new OscProviderRegistry.OscProviderInfo
		{
			Name = "Facebook",
			ProviderId = OscProviderGuids.Facebook,
			NetworkId = "",
			DefaultFolderDisplayName = "Facebook"
		};

		// Token: 0x04001DB2 RID: 7602
		private static readonly OscProviderRegistry.OscProviderInfo LinkedInInfo = new OscProviderRegistry.OscProviderInfo
		{
			Name = "LinkedIn",
			ProviderId = OscProviderGuids.LinkedIn,
			NetworkId = "linkedin",
			DefaultFolderDisplayName = "LinkedIn"
		};

		// Token: 0x04001DB3 RID: 7603
		private static readonly OscProviderRegistry.OscProviderInfo SharePointInfo = new OscProviderRegistry.OscProviderInfo
		{
			Name = "SharePoint",
			ProviderId = OscProviderGuids.SharePoint
		};

		// Token: 0x04001DB4 RID: 7604
		private static readonly OscProviderRegistry.OscProviderInfo WindowsLiveInfo = new OscProviderRegistry.OscProviderInfo
		{
			Name = "WindowsLive",
			ProviderId = OscProviderGuids.WindowsLive
		};

		// Token: 0x04001DB5 RID: 7605
		private static readonly OscProviderRegistry.OscProviderInfo XingInfo = new OscProviderRegistry.OscProviderInfo
		{
			Name = "XING",
			ProviderId = OscProviderGuids.Xing,
			NetworkId = "xing",
			DefaultFolderDisplayName = "XING"
		};

		// Token: 0x04001DB6 RID: 7606
		private static readonly Dictionary<string, OscProviderRegistry.OscProviderInfo> NameToInfoMap = new Dictionary<string, OscProviderRegistry.OscProviderInfo>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"Facebook",
				OscProviderRegistry.FacebookInfo
			},
			{
				"LinkedIn",
				OscProviderRegistry.LinkedInInfo
			},
			{
				"SharePoint",
				OscProviderRegistry.SharePointInfo
			},
			{
				"WindowsLive",
				OscProviderRegistry.WindowsLiveInfo
			},
			{
				"XING",
				OscProviderRegistry.XingInfo
			}
		};

		// Token: 0x04001DB7 RID: 7607
		private static readonly Dictionary<Guid, OscProviderRegistry.OscProviderInfo> GuidToInfoMap = new Dictionary<Guid, OscProviderRegistry.OscProviderInfo>
		{
			{
				OscProviderGuids.LinkedIn,
				OscProviderRegistry.LinkedInInfo
			},
			{
				OscProviderGuids.Facebook,
				OscProviderRegistry.FacebookInfo
			},
			{
				OscProviderGuids.SharePoint,
				OscProviderRegistry.SharePointInfo
			},
			{
				OscProviderGuids.WindowsLive,
				OscProviderRegistry.WindowsLiveInfo
			},
			{
				OscProviderGuids.Xing,
				OscProviderRegistry.XingInfo
			}
		};

		// Token: 0x02000510 RID: 1296
		private class OscProviderInfo
		{
			// Token: 0x1700114F RID: 4431
			// (get) Token: 0x060037CE RID: 14286 RVA: 0x000E1932 File Offset: 0x000DFB32
			// (set) Token: 0x060037CF RID: 14287 RVA: 0x000E193A File Offset: 0x000DFB3A
			internal string Name { get; set; }

			// Token: 0x17001150 RID: 4432
			// (get) Token: 0x060037D0 RID: 14288 RVA: 0x000E1943 File Offset: 0x000DFB43
			// (set) Token: 0x060037D1 RID: 14289 RVA: 0x000E194B File Offset: 0x000DFB4B
			internal Guid ProviderId { get; set; }

			// Token: 0x17001151 RID: 4433
			// (get) Token: 0x060037D2 RID: 14290 RVA: 0x000E1954 File Offset: 0x000DFB54
			// (set) Token: 0x060037D3 RID: 14291 RVA: 0x000E195C File Offset: 0x000DFB5C
			internal string NetworkId { get; set; }

			// Token: 0x17001152 RID: 4434
			// (get) Token: 0x060037D4 RID: 14292 RVA: 0x000E1965 File Offset: 0x000DFB65
			// (set) Token: 0x060037D5 RID: 14293 RVA: 0x000E196D File Offset: 0x000DFB6D
			internal string DefaultFolderDisplayName { get; set; }
		}
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EasExtensionCapabilities : ServerCapabilities
	{
		// Token: 0x06000052 RID: 82 RVA: 0x000026DD File Offset: 0x000008DD
		internal EasExtensionCapabilities()
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000026E8 File Offset: 0x000008E8
		internal EasExtensionCapabilities(IEnumerable<string> capabilities)
		{
			foreach (string text in capabilities)
			{
				if (text.StartsWith("1="))
				{
					string s = text.Substring("1=".Length, text.Length - "1=".Length);
					int num;
					if (int.TryParse(s, NumberStyles.HexNumber, null, out num))
					{
						foreach (KeyValuePair<EasExtensionsVersion1, string> keyValuePair in EasExtensionCapabilities.CapabilitiesMap)
						{
							EasExtensionsVersion1 key = keyValuePair.Key;
							if ((num & (int)key) != 0)
							{
								base.Add(keyValuePair.Value);
							}
						}
						base.Add("1=");
					}
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000027E0 File Offset: 0x000009E0
		internal bool SupportsVersion1
		{
			get
			{
				return base.Supports("1=");
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000027F0 File Offset: 0x000009F0
		internal bool SupportsExtensions(EasExtensionsVersion1 extension)
		{
			foreach (KeyValuePair<EasExtensionsVersion1, string> keyValuePair in EasExtensionCapabilities.CapabilitiesMap)
			{
				EasExtensionsVersion1 key = keyValuePair.Key;
				if (extension.HasFlag(key) && !base.Supports(keyValuePair.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000286C File Offset: 0x00000A6C
		internal string RequestExtensions(EasExtensionsVersion1 extension)
		{
			string str = "1=";
			int num = (int)extension;
			return str + num.ToString("X4");
		}

		// Token: 0x04000024 RID: 36
		internal const string HeaderExtensionString = "X-OLK-Extension";

		// Token: 0x04000025 RID: 37
		private const string FolderTypesCapability = "FolderTypes";

		// Token: 0x04000026 RID: 38
		private const string SystemCategoriesCapability = "SystemCategories";

		// Token: 0x04000027 RID: 39
		private const string DefaultFromAddressCapability = "DefaultFromAddress";

		// Token: 0x04000028 RID: 40
		private const string ArchiveCapability = "Archive";

		// Token: 0x04000029 RID: 41
		private const string UnsubscribeCapability = "Unsubscribe";

		// Token: 0x0400002A RID: 42
		private const string MessageUploadCapability = "MessageUpload";

		// Token: 0x0400002B RID: 43
		private const string AdvanedSearchCapability = "AdvanedSearch";

		// Token: 0x0400002C RID: 44
		private const string PicwDataCapability = "PicwData";

		// Token: 0x0400002D RID: 45
		private const string TrueMessageReadCapability = "TrueMessageRead";

		// Token: 0x0400002E RID: 46
		private const string RulesCapability = "Rules";

		// Token: 0x0400002F RID: 47
		private const string ExtendedDateFiltersCapability = "ExtendedDateFilters";

		// Token: 0x04000030 RID: 48
		private const string SmsExtensionsCapability = "SmsExtensions";

		// Token: 0x04000031 RID: 49
		private const string ActionableSearchCapability = "ActionableSearch";

		// Token: 0x04000032 RID: 50
		private const string FolderPermissionCapability = "FolderPermission";

		// Token: 0x04000033 RID: 51
		private const string FolderExtensionTypeCapability = "FolderExtensionType";

		// Token: 0x04000034 RID: 52
		private const string VoiceMailExtensionCapability = "VoiceMailExtension";

		// Token: 0x04000035 RID: 53
		private const string Version1Capability = "1=";

		// Token: 0x04000036 RID: 54
		private static readonly Dictionary<EasExtensionsVersion1, string> CapabilitiesMap = new Dictionary<EasExtensionsVersion1, string>
		{
			{
				EasExtensionsVersion1.FolderTypes,
				"FolderTypes"
			},
			{
				EasExtensionsVersion1.SystemCategories,
				"SystemCategories"
			},
			{
				EasExtensionsVersion1.DefaultFromAddress,
				"DefaultFromAddress"
			},
			{
				EasExtensionsVersion1.Archive,
				"Archive"
			},
			{
				EasExtensionsVersion1.Unsubscribe,
				"Unsubscribe"
			},
			{
				EasExtensionsVersion1.MessageUpload,
				"MessageUpload"
			},
			{
				EasExtensionsVersion1.AdvanedSearch,
				"AdvanedSearch"
			},
			{
				EasExtensionsVersion1.PicwData,
				"PicwData"
			},
			{
				EasExtensionsVersion1.TrueMessageRead,
				"TrueMessageRead"
			},
			{
				EasExtensionsVersion1.Rules,
				"Rules"
			},
			{
				EasExtensionsVersion1.ExtendedDateFilters,
				"ExtendedDateFilters"
			},
			{
				EasExtensionsVersion1.SmsExtensions,
				"SmsExtensions"
			},
			{
				EasExtensionsVersion1.ActionableSearch,
				"ActionableSearch"
			},
			{
				EasExtensionsVersion1.FolderPermission,
				"FolderPermission"
			},
			{
				EasExtensionsVersion1.FolderExtensionType,
				"FolderExtensionType"
			},
			{
				EasExtensionsVersion1.VoiceMailExtension,
				"VoiceMailExtension"
			}
		};
	}
}

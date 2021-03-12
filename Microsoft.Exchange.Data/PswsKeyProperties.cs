using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000269 RID: 617
	internal static class PswsKeyProperties
	{
		// Token: 0x060014A8 RID: 5288 RVA: 0x000423CC File Offset: 0x000405CC
		internal static List<string> GetPropertiesNeedUrlTokenInputDecode(string cmdletName)
		{
			List<string> result;
			if (!PswsKeyProperties.PropertiesNeedUrlTokenInputDecode.TryGetValue(cmdletName, out result))
			{
				return PswsKeyProperties.DefaultPropertiesNeedUrlTokenInputDecode;
			}
			return result;
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x000423F0 File Offset: 0x000405F0
		internal static bool IsKeyProperty(object obj, PropertyDefinition property, string propertyInStr)
		{
			if (obj == null)
			{
				return false;
			}
			if (!string.IsNullOrWhiteSpace(propertyInStr))
			{
				return "Identity".Equals(propertyInStr);
			}
			if (obj is ConfigurableObject && property == ((ConfigurableObject)obj).propertyBag.ObjectIdentityPropertyDefinition)
			{
				return true;
			}
			string key = obj.GetType().ToString();
			string name = property.Name;
			string[] source;
			return PswsKeyProperties.PropertiesNeedUrlTokenOutputEncode.TryGetValue(key, out source) && source.Contains(name, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x04000C10 RID: 3088
		internal const string CommonIdentityProperty = "Identity";

		// Token: 0x04000C11 RID: 3089
		private static readonly Dictionary<string, string[]> PropertiesNeedUrlTokenOutputEncode = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"Microsoft.Exchange.Data.Directory.Permission.RecipientPermission",
				new string[]
				{
					"Trustee"
				}
			},
			{
				"Microsoft.Exchange.Management.RecipientTasks.MailboxAcePresentationObject",
				new string[]
				{
					"User"
				}
			},
			{
				"Microsoft.Exchange.Management.Extension.App",
				new string[]
				{
					"Identity"
				}
			}
		};

		// Token: 0x04000C12 RID: 3090
		private static readonly List<string> DefaultPropertiesNeedUrlTokenInputDecode = new List<string>
		{
			"Identity"
		};

		// Token: 0x04000C13 RID: 3091
		private static readonly Dictionary<string, List<string>> PropertiesNeedUrlTokenInputDecode = new Dictionary<string, List<string>>
		{
			{
				"Add-DistributionGroupMember",
				new List<string>
				{
					"Identity",
					"Member"
				}
			},
			{
				"New-CompliancePolicySyncNotification",
				new List<string>()
			},
			{
				"Add-MailboxPermission",
				new List<string>()
			},
			{
				"Add-RecipientPermission",
				new List<string>()
			},
			{
				"Get-MailboxPermission",
				new List<string>
				{
					"Identity",
					"User"
				}
			},
			{
				"Get-RecipientPermission",
				new List<string>
				{
					"Identity",
					"Trustee"
				}
			},
			{
				"Remove-DistributionGroupMember",
				new List<string>
				{
					"Identity",
					"Member"
				}
			},
			{
				"Remove-MailboxPermission",
				new List<string>
				{
					"Identity"
				}
			},
			{
				"Remove-RecipientPermission",
				new List<string>
				{
					"Identity",
					"Trustee"
				}
			}
		};
	}
}

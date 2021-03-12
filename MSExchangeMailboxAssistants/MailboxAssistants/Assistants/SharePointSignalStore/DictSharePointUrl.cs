using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x02000222 RID: 546
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DictSharePointUrl : ISharePointUrl
	{
		// Token: 0x060014B3 RID: 5299 RVA: 0x000773F0 File Offset: 0x000755F0
		public string GetUrl(IExchangePrincipal userIdentity, IRecipientSession recipientSession)
		{
			string unmodifiedOrganization = DictSharePointUrl.GetUnmodifiedOrganization((ADUser)DirectoryHelper.ReadADRecipient(userIdentity.MailboxInfo.MailboxGuid, userIdentity.MailboxInfo.IsArchive, recipientSession));
			return DictSharePointUrl.GetUrl(unmodifiedOrganization);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0007742C File Offset: 0x0007562C
		internal static string GetUrl(string organization)
		{
			organization = organization.ToLowerInvariant();
			if (DictSharePointUrl.OrgNameToSharePointUrl.ContainsKey(organization))
			{
				return DictSharePointUrl.OrgNameToSharePointUrl[organization];
			}
			string text = Regex.Replace(organization, "\\.onmicrosoft.com$", string.Empty);
			if (text.Length != organization.Length)
			{
				DictSharePointUrl.InitTenantInfo();
				if (DictSharePointUrl.tenantInfo != null && DictSharePointUrl.tenantInfo.Contains(text))
				{
					return "https://" + text + ".sharepoint.com";
				}
			}
			return null;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x000774A4 File Offset: 0x000756A4
		internal static string GetUnmodifiedOrganization(ADUser user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (user.OrganizationId.OrganizationalUnit == null)
			{
				return string.Empty;
			}
			return user.OrganizationId.OrganizationalUnit.Name;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x000774D8 File Offset: 0x000756D8
		private static void InitTenantInfo()
		{
			lock (DictSharePointUrl.TenantInfoLock)
			{
				if (DictSharePointUrl.tenantInfo == null)
				{
					DictSharePointUrl.tenantInfo = new HashSet<string>();
					using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SharePointTenantInfo.txt.gz"))
					{
						using (StreamReader streamReader = new StreamReader(new GZipStream(manifestResourceStream, CompressionMode.Decompress), Encoding.UTF8))
						{
							string text;
							while ((text = streamReader.ReadLine()) != null)
							{
								DictSharePointUrl.tenantInfo.Add(text.ToLowerInvariant().Trim());
							}
						}
					}
				}
			}
		}

		// Token: 0x04000C7E RID: 3198
		private static readonly Dictionary<string, string> OrgNameToSharePointUrl = new Dictionary<string, string>
		{
			{
				"msft.ccsctp.net",
				"https://msft.spoppe.com"
			},
			{
				"sdflabs.com",
				"https://msft.spoppe.com"
			},
			{
				"microsoft.onmicrosoft.com",
				"https://microsoft.sharepoint.com"
			},
			{
				"elkem.onmicrosoft.com",
				"https://elkem.sharepoint.com"
			},
			{
				"grundfos.onmicrosoft.com",
				"https://grundfos.sharepoint.com"
			},
			{
				"bainsight.onmicrosoft.com",
				"https://bainsight.sharepoint.com"
			},
			{
				"bosecorp.onmicrosoft.com",
				"https://bosecorp.sharepoint.com"
			},
			{
				"steria.onmicrosoft.com",
				"https://steria.sharepoint.com"
			},
			{
				"walshgroup.onmicrosoft.com",
				"https://walshgroup.sharepoint.com"
			},
			{
				"spc14a.ccsctp.net",
				"https://spc14a.spoppe.com"
			},
			{
				"spc14b.ccsctp.net",
				"https://spc14b.spoppe.com"
			}
		};

		// Token: 0x04000C7F RID: 3199
		private static HashSet<string> tenantInfo;

		// Token: 0x04000C80 RID: 3200
		private static readonly object TenantInfoLock = new object();
	}
}

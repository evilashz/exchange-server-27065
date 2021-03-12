using System;
using System.Data;
using System.IO;
using System.Net;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SnapIn.Esm.Toolbox
{
	// Token: 0x02000008 RID: 8
	public class EcpAutodiscovery
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000022C0 File Offset: 0x000004C0
		internal string GetEcpUrl(EcpUrlKey key)
		{
			if (this.EcpRootUrl == null)
			{
				return null;
			}
			if (PSConnectionInfoSingleton.GetInstance().Type == OrganizationType.Cloud && !string.IsNullOrEmpty(EMCRunspaceConfigurationSingleton.GetInstance().TenantDomain))
			{
				return string.Concat(new string[]
				{
					this.EcpRootUrl,
					"/?Realm=",
					EMCRunspaceConfigurationSingleton.GetInstance().TenantDomain,
					"&ftr=",
					key.ToString()
				});
			}
			return this.EcpRootUrl + "/?ftr=" + key.ToString();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002354 File Offset: 0x00000554
		internal string EcpRootUrl
		{
			get
			{
				if (this.ecpRootUrl == null)
				{
					if (PSConnectionInfoSingleton.GetInstance().Type == OrganizationType.LocalOnPremise)
					{
						this.ecpRootUrl = this.GetEcpRoot(PSConnectionInfoSingleton.GetInstance().LogonWithDefaultCredential);
					}
					else if (PSConnectionInfoSingleton.GetInstance().Type == OrganizationType.RemoteOnPremise)
					{
						IPHostEntry hostEntry = Dns.GetHostEntry(PSConnectionInfoSingleton.GetInstance().EcpServer);
						if (hostEntry != null)
						{
							this.ecpRootUrl = this.GetEcpRoot(PSConnectionInfoSingleton.GetInstance().LogonWithDefaultCredential, hostEntry.HostName);
						}
						if (string.IsNullOrEmpty(this.ecpRootUrl))
						{
							this.ecpRootUrl = "https://" + PSConnectionInfoSingleton.GetInstance().EcpServer + "/ecp";
						}
					}
					else
					{
						this.ecpRootUrl = "https://" + PSConnectionInfoSingleton.GetInstance().EcpServer + "/ecp";
					}
				}
				return this.ecpRootUrl;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002432 File Offset: 0x00000632
		internal bool Discovered
		{
			get
			{
				return this.ecpRootUrl != null;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002440 File Offset: 0x00000640
		internal string GetEcpRoot(bool useWIA, string targetServerFqdn)
		{
			MonadConnection connection = new MonadConnection("pooled=false");
			using (new OpenConnection(connection))
			{
				string str = Path.Combine(ConfigurationContext.Setup.BinPath, "ConnectFunctions.ps1");
				using (MonadCommand monadCommand = new LoggableMonadCommand(". '" + str + "'", connection))
				{
					monadCommand.CommandType = CommandType.Text;
					monadCommand.ExecuteNonQuery();
				}
				string cmdText = "Discover-EcpVirtualDirectoryForEmc";
				using (MonadCommand monadCommand2 = new LoggableMonadCommand(cmdText, connection))
				{
					monadCommand2.CommandType = CommandType.StoredProcedure;
					monadCommand2.Parameters.Add(new MonadParameter("CurrentVersion", new ServerVersion(ConfigurationContext.Setup.InstalledVersion.Major, ConfigurationContext.Setup.InstalledVersion.Minor, ConfigurationContext.Setup.InstalledVersion.Build, ConfigurationContext.Setup.InstalledVersion.Revision)));
					monadCommand2.Parameters.Add(new MonadParameter("UseWIA", useWIA));
					if (!string.IsNullOrEmpty(targetServerFqdn))
					{
						monadCommand2.Parameters.Add(new MonadParameter("TargetServerFqdn", targetServerFqdn));
					}
					object[] array = monadCommand2.Execute();
					if (array.Length > 0)
					{
						return (string)array[0];
					}
				}
			}
			return null;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000025C4 File Offset: 0x000007C4
		internal string GetEcpRoot(bool useWIA)
		{
			return this.GetEcpRoot(useWIA, null);
		}

		// Token: 0x040001A9 RID: 425
		private string ecpRootUrl;

		// Token: 0x040001AA RID: 426
		internal static readonly EcpAutodiscovery Instance = new EcpAutodiscovery();
	}
}

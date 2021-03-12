using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009AC RID: 2476
	[Cmdlet("Get", "ClientAccessServer", DefaultParameterSetName = "Identity")]
	public sealed class GetClientAccessServer : GetSystemConfigurationObjectTask<ClientAccessServerIdParameter, Server>
	{
		// Token: 0x17001A5C RID: 6748
		// (get) Token: 0x06005862 RID: 22626 RVA: 0x00170953 File Offset: 0x0016EB53
		// (set) Token: 0x06005863 RID: 22627 RVA: 0x00170979 File Offset: 0x0016EB79
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeAlternateServiceAccountCredentialStatus
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeAlternateServiceAccountCredentialStatus"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeAlternateServiceAccountCredentialStatus"] = value;
			}
		}

		// Token: 0x17001A5D RID: 6749
		// (get) Token: 0x06005864 RID: 22628 RVA: 0x00170991 File Offset: 0x0016EB91
		// (set) Token: 0x06005865 RID: 22629 RVA: 0x001709B7 File Offset: 0x0016EBB7
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeAlternateServiceAccountCredentialPassword
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeAlternateServiceAccountCredentialPassword"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeAlternateServiceAccountCredentialPassword"] = value;
			}
		}

		// Token: 0x17001A5E RID: 6750
		// (get) Token: 0x06005866 RID: 22630 RVA: 0x001709D0 File Offset: 0x0016EBD0
		protected override QueryFilter InternalFilter
		{
			get
			{
				return new OrFilter(new QueryFilter[]
				{
					new BitMaskAndFilter(ServerSchema.CurrentServerRole, 1UL),
					new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.LessThan, ServerSchema.VersionNumber, Server.E15MinVersion),
						new BitMaskAndFilter(ServerSchema.CurrentServerRole, 4UL)
					})
				});
			}
		}

		// Token: 0x17001A5F RID: 6751
		// (get) Token: 0x06005867 RID: 22631 RVA: 0x00170A30 File Offset: 0x0016EC30
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005868 RID: 22632 RVA: 0x00170A34 File Offset: 0x0016EC34
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			ClientAccessServer clientAccessServer = new ClientAccessServer((Server)dataObject);
			if (this.IncludeAlternateServiceAccountCredentialPassword.ToBool())
			{
				SetClientAccessServer.EnsureRunningOnTargetServer(this, (Server)dataObject);
				clientAccessServer.AlternateServiceAccountConfiguration = AlternateServiceAccountConfiguration.LoadWithPasswordsFromRegistry();
			}
			else if (this.IncludeAlternateServiceAccountCredentialStatus.ToBool())
			{
				clientAccessServer.AlternateServiceAccountConfiguration = AlternateServiceAccountConfiguration.LoadFromRegistry(clientAccessServer.Fqdn);
			}
			IConfigurable[] array = this.ConfigurationSession.Find<ADRpcHttpVirtualDirectory>((ADObjectId)clientAccessServer.Identity, QueryScope.SubTree, null, null, 1);
			clientAccessServer.OutlookAnywhereEnabled = new bool?(array.Length > 0);
			QueryFilter filter = ExchangeScpObjects.AutodiscoverUrlKeyword.Filter;
			array = this.ConfigurationSession.Find<ADServiceConnectionPoint>((ADObjectId)clientAccessServer.Identity, QueryScope.SubTree, filter, null, 2);
			if (array.Length == 1)
			{
				ADServiceConnectionPoint adserviceConnectionPoint = array[0] as ADServiceConnectionPoint;
				if (adserviceConnectionPoint.ServiceBindingInformation.Count > 0)
				{
					clientAccessServer.AutoDiscoverServiceInternalUri = new Uri(adserviceConnectionPoint.ServiceBindingInformation[0]);
				}
				clientAccessServer.AutoDiscoverServiceGuid = new Guid?(GetClientAccessServer.ScpUrlGuid);
				clientAccessServer.AutoDiscoverServiceCN = Fqdn.Parse(adserviceConnectionPoint.ServiceDnsName);
				clientAccessServer.AutoDiscoverServiceClassName = adserviceConnectionPoint.ServiceClassName;
				if (adserviceConnectionPoint.Keywords != null && adserviceConnectionPoint.Keywords.Count > 1)
				{
					MultiValuedProperty<string> multiValuedProperty = null;
					foreach (string text in adserviceConnectionPoint.Keywords)
					{
						if (text.StartsWith("site=", StringComparison.OrdinalIgnoreCase))
						{
							if (multiValuedProperty == null)
							{
								multiValuedProperty = new MultiValuedProperty<string>();
							}
							multiValuedProperty.Add(text.Substring(5));
						}
					}
					if (multiValuedProperty != null && multiValuedProperty.Count > 0)
					{
						clientAccessServer.AutoDiscoverSiteScope = multiValuedProperty;
					}
				}
			}
			base.WriteResult(clientAccessServer);
			TaskLogger.LogExit();
		}

		// Token: 0x040032CE RID: 13006
		private const string IncludeAlternateServiceAccountCredentialStatusTag = "IncludeAlternateServiceAccountCredentialStatus";

		// Token: 0x040032CF RID: 13007
		private const string IncludeAlternateServiceAccountCredentialPasswordTag = "IncludeAlternateServiceAccountCredentialPassword";

		// Token: 0x040032D0 RID: 13008
		private static readonly Guid ScpUrlGuid = new Guid("77378F46-2C66-4aa9-A6A6-3E7A48B19596");
	}
}

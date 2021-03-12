using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009B9 RID: 2489
	[Cmdlet("Get", "Trust", DefaultParameterSetName = "Trust")]
	public sealed class GetTrust : GetTaskBase<ADDomainTrustInfo>
	{
		// Token: 0x17001A77 RID: 6775
		// (get) Token: 0x060058B2 RID: 22706 RVA: 0x001721EE File Offset: 0x001703EE
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001A78 RID: 6776
		// (get) Token: 0x060058B3 RID: 22707 RVA: 0x001721F1 File Offset: 0x001703F1
		// (set) Token: 0x060058B4 RID: 22708 RVA: 0x00172208 File Offset: 0x00170408
		[Parameter]
		public Fqdn DomainName
		{
			get
			{
				return (Fqdn)base.Fields["DomainName"];
			}
			set
			{
				base.Fields["DomainName"] = value;
			}
		}

		// Token: 0x060058B5 RID: 22709 RVA: 0x0017221B File Offset: 0x0017041B
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 58, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\ExchangeServer\\GetTrust.cs");
		}

		// Token: 0x060058B6 RID: 22710 RVA: 0x0017223C File Offset: 0x0017043C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADForest localForest = ADForest.GetLocalForest();
			if (this.DomainName != null)
			{
				this.WriteResult<ADDomainTrustInfo>(localForest.FindAllTrustedForests());
				this.WriteResult<ADDomainTrustInfo>(localForest.FindTrustedDomains(this.DomainName));
			}
			else
			{
				HashSet<string> hashSet = new HashSet<string>();
				foreach (ADDomainTrustInfo addomainTrustInfo in localForest.FindAllTrustedForests())
				{
					hashSet.Add(addomainTrustInfo.Name);
					this.WriteResult(addomainTrustInfo);
				}
				foreach (ADDomain addomain in localForest.FindDomains())
				{
					foreach (ADDomainTrustInfo addomainTrustInfo2 in localForest.FindTrustedDomains(addomain.Fqdn))
					{
						if (!hashSet.Contains(addomainTrustInfo2.Name))
						{
							hashSet.Add(addomainTrustInfo2.Name);
							this.WriteResult(addomainTrustInfo2);
						}
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x00172354 File Offset: 0x00170554
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			base.WriteResult(new ADTrust(dataObject as ADDomainTrustInfo));
		}
	}
}

using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.FederationProvisioning;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009FE RID: 2558
	[Cmdlet("Test", "FederationTrustCertificate", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class TestFederationTrustCertificate : DataAccessTask<FederationTrust>
	{
		// Token: 0x17001B76 RID: 7030
		// (get) Token: 0x06005BB3 RID: 23475 RVA: 0x00180B7C File Offset: 0x0017ED7C
		// (set) Token: 0x06005BB4 RID: 23476 RVA: 0x00180B84 File Offset: 0x0017ED84
		[Parameter(Mandatory = false)]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x17001B77 RID: 7031
		// (get) Token: 0x06005BB5 RID: 23477 RVA: 0x00180B8D File Offset: 0x0017ED8D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestFederationTrustCertificate;
			}
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x00180B94 File Offset: 0x0017ED94
		protected override IConfigDataProvider CreateSession()
		{
			return base.RootOrgGlobalConfigSession;
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x00180B9C File Offset: 0x0017ED9C
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (base.HasErrors)
			{
				return;
			}
			Dictionary<TopologySite, List<TopologyServer>> dictionary = null;
			TopologySite topologySite = null;
			FederationCertificate.DiscoverServers(base.RootOrgGlobalConfigSession, false, out dictionary, out topologySite);
			if (topologySite == null)
			{
				base.WriteError(new CannotGetLocalSiteException(), ErrorCategory.ReadError, null);
			}
			foreach (KeyValuePair<TopologySite, List<TopologyServer>> keyValuePair in dictionary)
			{
				foreach (TopologyServer topologyServer in keyValuePair.Value)
				{
					foreach (CertificateRecord certificateRecord in FederationCertificate.FederationCertificates(base.RootOrgGlobalConfigSession))
					{
						FederationTrustCertificateState state = FederationCertificate.TestForCertificate(topologyServer.Name, certificateRecord.Thumbprint);
						FederationTrustCertificateStatus sendToPipeline = new FederationTrustCertificateStatus(keyValuePair.Key, topologyServer, state, certificateRecord.Thumbprint);
						base.WriteObject(sendToPipeline);
					}
				}
			}
		}
	}
}

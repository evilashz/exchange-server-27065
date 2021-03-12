using System;
using System.Management.Automation;
using System.ServiceModel;
using System.ServiceModel.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Ehf;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;
using Microsoft.Exchange.Management.EdgeSync;

namespace Microsoft.Exchange.Management.PerimeterConfig
{
	// Token: 0x0200005A RID: 90
	public abstract class DCPerimeterConfigTask : Task
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000C7DB File Offset: 0x0000A9DB
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000C7F2 File Offset: 0x0000A9F2
		[Parameter]
		public Fqdn DomainController
		{
			get
			{
				return (Fqdn)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000C805 File Offset: 0x0000AA05
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000C80D File Offset: 0x0000AA0D
		[Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public EdgeSyncEhfConnectorIdParameter ConnectorId
		{
			get
			{
				return this.connectorId;
			}
			set
			{
				this.connectorId = value;
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000C818 File Offset: 0x0000AA18
		protected override void InternalProcessRecord()
		{
			ITopologyConfigurationSession session = this.CreateSession();
			EhfTargetServerConfig config = Utils.CreateEhfTargetConfig(session, this.ConnectorId, this);
			using (EhfProvisioningService ehfProvisioningService = new EhfProvisioningService(config))
			{
				Exception ex = null;
				try
				{
					this.InvokeWebService(session, config, ehfProvisioningService);
				}
				catch (FaultException<ServiceFault> faultException)
				{
					ServiceFault detail = faultException.Detail;
					if (detail.Id == FaultId.UnableToConnectToDatabase)
					{
						ex = new InvalidOperationException("ServiceFault: EHF is unable to connect to its database", faultException);
					}
					else
					{
						ex = faultException;
					}
				}
				catch (MessageSecurityException ex2)
				{
					switch (EhfProvisioningService.DecodeMessageSecurityException(ex2))
					{
					case EhfProvisioningService.MessageSecurityExceptionReason.DatabaseFailure:
						ex = new InvalidOperationException("MessageSecurityException: EHF is unable to connect to its database", ex2.InnerException);
						goto IL_A4;
					case EhfProvisioningService.MessageSecurityExceptionReason.InvalidCredentials:
						ex = new InvalidOperationException("MessageSecurityException: EHF connector contains invalid credentials", ex2.InnerException);
						goto IL_A4;
					}
					ex = ex2;
					IL_A4:;
				}
				catch (CommunicationException ex3)
				{
					ex = ex3;
				}
				catch (TimeoutException ex4)
				{
					ex = ex4;
				}
				catch (EhfProvisioningService.ContractViolationException ex5)
				{
					ex = ex5;
				}
				if (ex != null)
				{
					base.WriteError(ex, ErrorCategory.InvalidOperation, null);
				}
			}
		}

		// Token: 0x06000335 RID: 821
		internal abstract void InvokeWebService(IConfigurationSession session, EhfTargetServerConfig config, EhfProvisioningService provisioningService);

		// Token: 0x06000336 RID: 822 RVA: 0x0000C944 File Offset: 0x0000AB44
		private ITopologyConfigurationSession CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 155, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\PerimeterConfig\\DCPerimeterConfigTask.cs");
		}

		// Token: 0x04000132 RID: 306
		private EdgeSyncEhfConnectorIdParameter connectorId;
	}
}

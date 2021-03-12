using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeCertificate;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AB8 RID: 2744
	[Cmdlet("Enable", "ExchangeCertificate", SupportsShouldProcess = true, DefaultParameterSetName = "Thumbprint")]
	public class EnableExchangeCertificate : DataAccessTask<Server>, IIdentityExchangeCertificateCmdlet
	{
		// Token: 0x17001D60 RID: 7520
		// (get) Token: 0x06006104 RID: 24836 RVA: 0x00194EC2 File Offset: 0x001930C2
		// (set) Token: 0x06006105 RID: 24837 RVA: 0x00194ED9 File Offset: 0x001930D9
		[Parameter(Mandatory = false, ParameterSetName = "Identity", Position = 0)]
		public ExchangeCertificateIdParameter Identity
		{
			get
			{
				return (ExchangeCertificateIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17001D61 RID: 7521
		// (get) Token: 0x06006106 RID: 24838 RVA: 0x00194EEC File Offset: 0x001930EC
		// (set) Token: 0x06006107 RID: 24839 RVA: 0x00194F03 File Offset: 0x00193103
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Thumbprint", Position = 0)]
		public string Thumbprint
		{
			get
			{
				return (string)base.Fields["Thumbprint"];
			}
			set
			{
				base.Fields["Thumbprint"] = value;
			}
		}

		// Token: 0x17001D62 RID: 7522
		// (get) Token: 0x06006108 RID: 24840 RVA: 0x00194F16 File Offset: 0x00193116
		// (set) Token: 0x06006109 RID: 24841 RVA: 0x00194F2D File Offset: 0x0019312D
		[Parameter(Mandatory = true)]
		public AllowedServices Services
		{
			get
			{
				return (AllowedServices)base.Fields["Services"];
			}
			set
			{
				base.Fields["Services"] = value;
			}
		}

		// Token: 0x17001D63 RID: 7523
		// (get) Token: 0x0600610A RID: 24842 RVA: 0x00194F45 File Offset: 0x00193145
		// (set) Token: 0x0600610B RID: 24843 RVA: 0x00194F6B File Offset: 0x0019316B
		[Parameter(Mandatory = false)]
		public SwitchParameter NetworkServiceAllowed
		{
			get
			{
				return (SwitchParameter)(base.Fields["NetworkServiceAllowed"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["NetworkServiceAllowed"] = value;
			}
		}

		// Token: 0x17001D64 RID: 7524
		// (get) Token: 0x0600610C RID: 24844 RVA: 0x00194F83 File Offset: 0x00193183
		// (set) Token: 0x0600610D RID: 24845 RVA: 0x00194FA9 File Offset: 0x001931A9
		[Parameter(Mandatory = false)]
		public SwitchParameter DoNotRequireSsl
		{
			get
			{
				return (SwitchParameter)(base.Fields["DoNotRequireSsl"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DoNotRequireSsl"] = value;
			}
		}

		// Token: 0x17001D65 RID: 7525
		// (get) Token: 0x0600610E RID: 24846 RVA: 0x00194FC1 File Offset: 0x001931C1
		// (set) Token: 0x0600610F RID: 24847 RVA: 0x00194FD8 File Offset: 0x001931D8
		[Parameter(Mandatory = false, ParameterSetName = "Thumbprint")]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17001D66 RID: 7526
		// (get) Token: 0x06006110 RID: 24848 RVA: 0x00194FEB File Offset: 0x001931EB
		// (set) Token: 0x06006111 RID: 24849 RVA: 0x00195011 File Offset: 0x00193211
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17001D67 RID: 7527
		// (get) Token: 0x06006112 RID: 24850 RVA: 0x00195029 File Offset: 0x00193229
		// (set) Token: 0x06006113 RID: 24851 RVA: 0x00195031 File Offset: 0x00193231
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

		// Token: 0x06006114 RID: 24852 RVA: 0x0019503A File Offset: 0x0019323A
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 144, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\ExchangeCertificate\\EnableExchangeCertificate.cs");
		}

		// Token: 0x17001D68 RID: 7528
		// (get) Token: 0x06006115 RID: 24853 RVA: 0x00195067 File Offset: 0x00193267
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmEnableExchangeCertificate(this.Thumbprint);
			}
		}

		// Token: 0x06006116 RID: 24854 RVA: 0x00195074 File Offset: 0x00193274
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			GetExchangeCertificate.PrepareParameters(this);
			this.serverObject = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound((string)this.Server)), new LocalizedString?(Strings.ErrorServerNotUnique((string)this.Server)));
			if (!this.serverObject.IsE14OrLater)
			{
				base.WriteError(new ArgumentException(Strings.RemoteCertificateExchangeVersionNotSupported(this.serverObject.Name)), ErrorCategory.InvalidArgument, null);
			}
			base.VerifyIsWithinScopes(DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromCustomScopeSet(base.ScopeSet, ADSystemConfigurationSession.GetRootOrgContainerId(this.DomainController, null), base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true), 186, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\ExchangeCertificate\\EnableExchangeCertificate.cs"), this.serverObject, true, new DataAccessTask<Server>.ADObjectOutOfScopeString(Strings.ErrorServerOutOfScope));
			if (!string.IsNullOrEmpty(this.Thumbprint))
			{
				this.Thumbprint = ManageExchangeCertificate.UnifyThumbprintFormat(this.Thumbprint);
			}
		}

		// Token: 0x06006117 RID: 24855 RVA: 0x00195188 File Offset: 0x00193388
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (base.HasErrors)
			{
				return;
			}
			ExchangeCertificateRpc exchangeCertificateRpc = new ExchangeCertificateRpc();
			exchangeCertificateRpc.EnableByThumbprint = this.Thumbprint;
			exchangeCertificateRpc.EnableServices = this.Services;
			exchangeCertificateRpc.RequireSsl = !this.DoNotRequireSsl;
			exchangeCertificateRpc.EnableAllowConfirmation = !this.Force;
			exchangeCertificateRpc.EnableNetworkService = this.NetworkServiceAllowed;
			ExchangeCertificateRpcVersion exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version1;
			byte[] outputBlob = null;
			try
			{
				byte[] inBlob = exchangeCertificateRpc.SerializeInputParameters(ExchangeCertificateRpcVersion.Version2);
				ExchangeCertificateRpcClient2 exchangeCertificateRpcClient = new ExchangeCertificateRpcClient2(this.serverObject.Name);
				outputBlob = exchangeCertificateRpcClient.EnableCertificate2(0, inBlob);
				exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version2;
			}
			catch (RpcException)
			{
				exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version1;
			}
			if (exchangeCertificateRpcVersion == ExchangeCertificateRpcVersion.Version1)
			{
				try
				{
					byte[] inBlob2 = exchangeCertificateRpc.SerializeInputParameters(exchangeCertificateRpcVersion);
					ExchangeCertificateRpcClient exchangeCertificateRpcClient2 = new ExchangeCertificateRpcClient(this.serverObject.Name);
					outputBlob = exchangeCertificateRpcClient2.EnableCertificate(0, inBlob2);
				}
				catch (RpcException e)
				{
					ManageExchangeCertificate.WriteRpcError(e, this.serverObject.Name, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
			}
			ExchangeCertificateRpc exchangeCertificateRpc2 = new ExchangeCertificateRpc(exchangeCertificateRpcVersion, null, outputBlob);
			ExchangeCertificateRpc.OutputTaskMessages(this.serverObject, exchangeCertificateRpc2, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (exchangeCertificateRpc2.ReturnConfirmationList != null)
			{
				foreach (KeyValuePair<AllowedServices, LocalizedString> keyValuePair in exchangeCertificateRpc2.ReturnConfirmationList)
				{
					if (base.ShouldContinue(keyValuePair.Value))
					{
						ExchangeCertificateRpc exchangeCertificateRpc3 = new ExchangeCertificateRpc();
						exchangeCertificateRpc3.EnableAllowConfirmation = false;
						exchangeCertificateRpc3.EnableByThumbprint = this.Thumbprint;
						exchangeCertificateRpc3.RequireSsl = !this.DoNotRequireSsl;
						exchangeCertificateRpc3.EnableNetworkService = this.NetworkServiceAllowed;
						exchangeCertificateRpc3.EnableServices = keyValuePair.Key;
						AllowedServices key = keyValuePair.Key;
						if (key == AllowedServices.SMTP)
						{
							exchangeCertificateRpc3.EnableUpdateAD = true;
						}
						try
						{
							byte[] inBlob3 = exchangeCertificateRpc3.SerializeInputParameters(exchangeCertificateRpcVersion);
							if (exchangeCertificateRpcVersion == ExchangeCertificateRpcVersion.Version1)
							{
								ExchangeCertificateRpcClient exchangeCertificateRpcClient3 = new ExchangeCertificateRpcClient(this.serverObject.Name);
								outputBlob = exchangeCertificateRpcClient3.EnableCertificate(0, inBlob3);
							}
							else
							{
								ExchangeCertificateRpcClient2 exchangeCertificateRpcClient4 = new ExchangeCertificateRpcClient2(this.serverObject.Name);
								outputBlob = exchangeCertificateRpcClient4.EnableCertificate2(0, inBlob3);
							}
						}
						catch (RpcException e2)
						{
							ManageExchangeCertificate.WriteRpcError(e2, this.serverObject.Name, new Task.TaskErrorLoggingDelegate(base.WriteError));
						}
						exchangeCertificateRpc2 = new ExchangeCertificateRpc(exchangeCertificateRpcVersion, null, outputBlob);
						ExchangeCertificateRpc.OutputTaskMessages(this.serverObject, exchangeCertificateRpc2, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
					}
				}
			}
		}

		// Token: 0x0400355B RID: 13659
		private Server serverObject;
	}
}

using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeCertificate;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000ACF RID: 2767
	[Cmdlet("Remove", "ExchangeCertificate", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Thumbprint")]
	public class RemoveExchangeCertificate : DataAccessTask<Server>, IIdentityExchangeCertificateCmdlet
	{
		// Token: 0x17001DDA RID: 7642
		// (get) Token: 0x06006256 RID: 25174 RVA: 0x0019A81C File Offset: 0x00198A1C
		// (set) Token: 0x06006257 RID: 25175 RVA: 0x0019A833 File Offset: 0x00198A33
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

		// Token: 0x17001DDB RID: 7643
		// (get) Token: 0x06006258 RID: 25176 RVA: 0x0019A846 File Offset: 0x00198A46
		// (set) Token: 0x06006259 RID: 25177 RVA: 0x0019A85D File Offset: 0x00198A5D
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

		// Token: 0x17001DDC RID: 7644
		// (get) Token: 0x0600625A RID: 25178 RVA: 0x0019A870 File Offset: 0x00198A70
		// (set) Token: 0x0600625B RID: 25179 RVA: 0x0019A887 File Offset: 0x00198A87
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

		// Token: 0x17001DDD RID: 7645
		// (get) Token: 0x0600625C RID: 25180 RVA: 0x0019A89A File Offset: 0x00198A9A
		// (set) Token: 0x0600625D RID: 25181 RVA: 0x0019A8A2 File Offset: 0x00198AA2
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

		// Token: 0x17001DDE RID: 7646
		// (get) Token: 0x0600625E RID: 25182 RVA: 0x0019A8AB File Offset: 0x00198AAB
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmRemoveExchangeCertificate(this.Thumbprint);
			}
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x0019A8B8 File Offset: 0x00198AB8
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 102, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\ExchangeCertificate\\RemoveExchangeCertificate.cs");
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x0019A8E4 File Offset: 0x00198AE4
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
			base.VerifyIsWithinScopes(DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromCustomScopeSet(base.ScopeSet, ADSystemConfigurationSession.GetRootOrgContainerId(this.DomainController, null), base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true), 136, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\ExchangeCertificate\\RemoveExchangeCertificate.cs"), this.serverObject, true, new DataAccessTask<Server>.ADObjectOutOfScopeString(Strings.ErrorServerOutOfScope));
			if (!string.IsNullOrEmpty(this.Thumbprint))
			{
				this.Thumbprint = ManageExchangeCertificate.UnifyThumbprintFormat(this.Thumbprint);
			}
		}

		// Token: 0x06006261 RID: 25185 RVA: 0x0019A9F8 File Offset: 0x00198BF8
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (base.HasErrors)
			{
				return;
			}
			ExchangeCertificateRpc exchangeCertificateRpc = new ExchangeCertificateRpc();
			exchangeCertificateRpc.RemoveByThumbprint = this.Thumbprint;
			ExchangeCertificateRpcVersion exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version1;
			byte[] outputBlob = null;
			try
			{
				byte[] inBlob = exchangeCertificateRpc.SerializeInputParameters(ExchangeCertificateRpcVersion.Version2);
				ExchangeCertificateRpcClient2 exchangeCertificateRpcClient = new ExchangeCertificateRpcClient2(this.serverObject.Name);
				outputBlob = exchangeCertificateRpcClient.RemoveCertificate2(0, inBlob);
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
					outputBlob = exchangeCertificateRpcClient2.RemoveCertificate(0, inBlob2);
				}
				catch (RpcException e)
				{
					ManageExchangeCertificate.WriteRpcError(e, this.serverObject.Name, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
			}
			ExchangeCertificateRpc exchangeCertificateRpc2 = new ExchangeCertificateRpc(exchangeCertificateRpcVersion, null, outputBlob);
			ExchangeCertificateRpc.OutputTaskMessages(this.serverObject, exchangeCertificateRpc2, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (string.IsNullOrEmpty(exchangeCertificateRpc2.ReturnTaskErrorString))
			{
				AsyncOperationNotificationDataProvider.RemoveNotification(base.CurrentOrganizationId, this.serverObject.Fqdn + "\\" + this.Thumbprint, false);
			}
		}

		// Token: 0x040035D3 RID: 13779
		private Server serverObject;
	}
}

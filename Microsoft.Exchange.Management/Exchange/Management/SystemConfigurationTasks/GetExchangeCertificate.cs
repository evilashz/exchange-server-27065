using System;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;
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
	// Token: 0x02000AC5 RID: 2757
	[Cmdlet("Get", "ExchangeCertificate", DefaultParameterSetName = "Thumbprint")]
	public class GetExchangeCertificate : DataAccessTask<Server>, IIdentityExchangeCertificateCmdlet
	{
		// Token: 0x17001DAF RID: 7599
		// (get) Token: 0x060061BA RID: 25018 RVA: 0x00197708 File Offset: 0x00195908
		// (set) Token: 0x060061BB RID: 25019 RVA: 0x00197710 File Offset: 0x00195910
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

		// Token: 0x17001DB0 RID: 7600
		// (get) Token: 0x060061BC RID: 25020 RVA: 0x00197719 File Offset: 0x00195919
		// (set) Token: 0x060061BD RID: 25021 RVA: 0x00197730 File Offset: 0x00195930
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

		// Token: 0x17001DB1 RID: 7601
		// (get) Token: 0x060061BE RID: 25022 RVA: 0x00197743 File Offset: 0x00195943
		// (set) Token: 0x060061BF RID: 25023 RVA: 0x0019775A File Offset: 0x0019595A
		[Parameter(Mandatory = false, ParameterSetName = "Instance", ValueFromPipeline = true)]
		public X509Certificate2 Instance
		{
			get
			{
				return (X509Certificate2)base.Fields["Certificate"];
			}
			set
			{
				base.Fields["Certificate"] = value;
			}
		}

		// Token: 0x17001DB2 RID: 7602
		// (get) Token: 0x060061C0 RID: 25024 RVA: 0x0019776D File Offset: 0x0019596D
		// (set) Token: 0x060061C1 RID: 25025 RVA: 0x00197784 File Offset: 0x00195984
		[Parameter(Mandatory = false, ParameterSetName = "Instance")]
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

		// Token: 0x17001DB3 RID: 7603
		// (get) Token: 0x060061C2 RID: 25026 RVA: 0x00197797 File Offset: 0x00195997
		// (set) Token: 0x060061C3 RID: 25027 RVA: 0x001977AE File Offset: 0x001959AE
		[Parameter(Mandatory = false, ParameterSetName = "Thumbprint", ValueFromPipeline = true, Position = 0)]
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

		// Token: 0x17001DB4 RID: 7604
		// (get) Token: 0x060061C4 RID: 25028 RVA: 0x001977C1 File Offset: 0x001959C1
		// (set) Token: 0x060061C5 RID: 25029 RVA: 0x001977D8 File Offset: 0x001959D8
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpDomain> DomainName
		{
			internal get
			{
				return (MultiValuedProperty<SmtpDomain>)base.Fields["DomainName"];
			}
			set
			{
				base.Fields["DomainName"] = value;
			}
		}

		// Token: 0x060061C6 RID: 25030 RVA: 0x001977EB File Offset: 0x001959EB
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 121, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\ExchangeCertificate\\GetExchangeCertificate.cs");
		}

		// Token: 0x060061C7 RID: 25031 RVA: 0x00197818 File Offset: 0x00195A18
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
			if (!string.IsNullOrEmpty(this.Thumbprint))
			{
				this.Thumbprint = ManageExchangeCertificate.UnifyThumbprintFormat(this.Thumbprint);
			}
		}

		// Token: 0x060061C8 RID: 25032 RVA: 0x001978D0 File Offset: 0x00195AD0
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (base.HasErrors)
			{
				return;
			}
			ExchangeCertificateRpc exchangeCertificateRpc = new ExchangeCertificateRpc();
			if (this.Instance != null)
			{
				exchangeCertificateRpc.GetByCertificate = this.Instance.Export(X509ContentType.SerializedCert);
			}
			if (this.DomainName != null && this.DomainName.Count > 0)
			{
				exchangeCertificateRpc.GetByDomains = this.DomainName;
			}
			if (this.Thumbprint != null)
			{
				exchangeCertificateRpc.GetByThumbprint = this.Thumbprint;
			}
			ExchangeCertificateRpcVersion exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version1;
			byte[] outputBlob = null;
			try
			{
				byte[] inBlob = exchangeCertificateRpc.SerializeInputParameters(ExchangeCertificateRpcVersion.Version2);
				ExchangeCertificateRpcClient2 exchangeCertificateRpcClient = new ExchangeCertificateRpcClient2(this.serverObject.Name);
				outputBlob = exchangeCertificateRpcClient.GetCertificate2(0, inBlob);
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
					outputBlob = exchangeCertificateRpcClient2.GetCertificate(0, inBlob2);
				}
				catch (RpcException e)
				{
					ManageExchangeCertificate.WriteRpcError(e, this.serverObject.Name, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
			}
			ExchangeCertificateRpc exchangeCertificateRpc2 = new ExchangeCertificateRpc(exchangeCertificateRpcVersion, null, outputBlob);
			ExchangeCertificateRpc.OutputTaskMessages(this.serverObject, exchangeCertificateRpc2, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
			foreach (ExchangeCertificate exchangeCertificate in exchangeCertificateRpc2.ReturnCertList)
			{
				exchangeCertificate.Identity = this.serverObject.Fqdn + "\\" + exchangeCertificate.Thumbprint;
				if (string.IsNullOrEmpty(exchangeCertificate.FriendlyName))
				{
					exchangeCertificate.FriendlyName = exchangeCertificate.Issuer;
				}
				base.WriteObject(exchangeCertificate);
			}
		}

		// Token: 0x060061C9 RID: 25033 RVA: 0x00197A94 File Offset: 0x00195C94
		internal static void PrepareParameters(IIdentityExchangeCertificateCmdlet cmdlet)
		{
			if ((cmdlet.Server == null && cmdlet.Identity == null) || (cmdlet.Server == null && cmdlet.Identity.ServerIdParameter == null))
			{
				cmdlet.Server = new ServerIdParameter();
			}
			else if (cmdlet.Identity != null && cmdlet.Identity.ServerIdParameter != null)
			{
				cmdlet.Server = cmdlet.Identity.ServerIdParameter;
			}
			if (cmdlet.Identity != null && cmdlet.Identity.Thumbprint != null)
			{
				cmdlet.Thumbprint = cmdlet.Identity.Thumbprint;
			}
		}

		// Token: 0x040035A7 RID: 13735
		private Server serverObject;
	}
}

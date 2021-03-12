using System;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeCertificate;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AC6 RID: 2758
	[Cmdlet("Import", "ExchangeCertificate", DefaultParameterSetName = "FileData", SupportsShouldProcess = true)]
	public class ImportExchangeCertificate : DataAccessTask<Server>
	{
		// Token: 0x17001DB5 RID: 7605
		// (get) Token: 0x060061CB RID: 25035 RVA: 0x00197B27 File Offset: 0x00195D27
		// (set) Token: 0x060061CC RID: 25036 RVA: 0x00197B3E File Offset: 0x00195D3E
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
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

		// Token: 0x17001DB6 RID: 7606
		// (get) Token: 0x060061CD RID: 25037 RVA: 0x00197B51 File Offset: 0x00195D51
		// (set) Token: 0x060061CE RID: 25038 RVA: 0x00197B59 File Offset: 0x00195D59
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

		// Token: 0x17001DB7 RID: 7607
		// (get) Token: 0x060061CF RID: 25039 RVA: 0x00197B62 File Offset: 0x00195D62
		// (set) Token: 0x060061D0 RID: 25040 RVA: 0x00197B79 File Offset: 0x00195D79
		[Parameter(Mandatory = true, ParameterSetName = "Instance", ValueFromPipeline = true)]
		public string[] Instance
		{
			get
			{
				return (string[])base.Fields["Instance"];
			}
			set
			{
				base.Fields["Instance"] = value;
			}
		}

		// Token: 0x17001DB8 RID: 7608
		// (get) Token: 0x060061D1 RID: 25041 RVA: 0x00197B8C File Offset: 0x00195D8C
		// (set) Token: 0x060061D2 RID: 25042 RVA: 0x00197BA3 File Offset: 0x00195DA3
		[Parameter(Mandatory = true, ParameterSetName = "FileData")]
		public byte[] FileData
		{
			internal get
			{
				return (byte[])base.Fields["FileData"];
			}
			set
			{
				base.Fields["FileData"] = value;
			}
		}

		// Token: 0x17001DB9 RID: 7609
		// (get) Token: 0x060061D3 RID: 25043 RVA: 0x00197BB6 File Offset: 0x00195DB6
		// (set) Token: 0x060061D4 RID: 25044 RVA: 0x00197BCD File Offset: 0x00195DCD
		[Parameter(Mandatory = true, ParameterSetName = "FileName")]
		public string FileName
		{
			internal get
			{
				return (string)base.Fields["FileName"];
			}
			set
			{
				base.Fields["FileName"] = value;
			}
		}

		// Token: 0x17001DBA RID: 7610
		// (get) Token: 0x060061D5 RID: 25045 RVA: 0x00197BE0 File Offset: 0x00195DE0
		// (set) Token: 0x060061D6 RID: 25046 RVA: 0x00197BF7 File Offset: 0x00195DF7
		[Parameter(Mandatory = false)]
		public SecureString Password
		{
			internal get
			{
				return (SecureString)base.Fields["Password"];
			}
			set
			{
				base.Fields["Password"] = value;
			}
		}

		// Token: 0x17001DBB RID: 7611
		// (get) Token: 0x060061D7 RID: 25047 RVA: 0x00197C0A File Offset: 0x00195E0A
		// (set) Token: 0x060061D8 RID: 25048 RVA: 0x00197C2B File Offset: 0x00195E2B
		[Parameter(Mandatory = false)]
		public bool PrivateKeyExportable
		{
			get
			{
				return (bool)(base.Fields["PrivateKeyExportable"] ?? false);
			}
			set
			{
				base.Fields["PrivateKeyExportable"] = value;
			}
		}

		// Token: 0x17001DBC RID: 7612
		// (get) Token: 0x060061D9 RID: 25049 RVA: 0x00197C43 File Offset: 0x00195E43
		// (set) Token: 0x060061DA RID: 25050 RVA: 0x00197C5A File Offset: 0x00195E5A
		[Parameter(Mandatory = false)]
		public string FriendlyName
		{
			internal get
			{
				return (string)base.Fields["FriendlyName"];
			}
			set
			{
				base.Fields["FriendlyName"] = value;
			}
		}

		// Token: 0x060061DB RID: 25051 RVA: 0x00197C6D File Offset: 0x00195E6D
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 158, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\ExchangeCertificate\\ImportExchangeCertificate.cs");
		}

		// Token: 0x17001DBD RID: 7613
		// (get) Token: 0x060061DC RID: 25052 RVA: 0x00197C9A File Offset: 0x00195E9A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmImportExchangeCertificateDirect;
			}
		}

		// Token: 0x060061DD RID: 25053 RVA: 0x00197CA4 File Offset: 0x00195EA4
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.Server == null)
			{
				this.Server = new ServerIdParameter();
			}
			this.serverObject = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound((string)this.Server)), new LocalizedString?(Strings.ErrorServerNotUnique((string)this.Server)));
			if (!this.serverObject.IsE14OrLater)
			{
				base.WriteError(new ArgumentException(Strings.RemoteCertificateExchangeVersionNotSupported(this.serverObject.Name)), ErrorCategory.InvalidArgument, null);
			}
			base.VerifyIsWithinScopes(DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromCustomScopeSet(base.ScopeSet, ADSystemConfigurationSession.GetRootOrgContainerId(this.DomainController, null), base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true), 207, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\ExchangeCertificate\\ImportExchangeCertificate.cs"), this.serverObject, true, new DataAccessTask<Server>.ADObjectOutOfScopeString(Strings.ErrorServerOutOfScope));
			if (this.Instance == null && this.FileData == null && string.IsNullOrEmpty(this.FileName))
			{
				base.WriteError(new ArgumentException(Strings.ImportCertificateDataIsNull(this.serverObject.Name)), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x060061DE RID: 25054 RVA: 0x00197DE8 File Offset: 0x00195FE8
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (base.HasErrors)
			{
				return;
			}
			ExchangeCertificateRpc exchangeCertificateRpc = new ExchangeCertificateRpc();
			string text;
			if (this.FileData != null)
			{
				text = ImportExchangeCertificate.RemoveBase64HeaderFooter(CertificateEnroller.ToBase64String(this.FileData));
			}
			else if (this.Instance != null)
			{
				text = ImportExchangeCertificate.RemoveBase64HeaderFooter(string.Join(null, this.Instance));
			}
			else
			{
				text = ImportExchangeCertificate.RemoveBase64HeaderFooter(CertificateEnroller.ToBase64String(this.GetFileData(this.FileName)));
			}
			if (text.Length == 0)
			{
				base.WriteError(new ImportCertificateDataInvalidException(), ErrorCategory.ReadError, 0);
			}
			exchangeCertificateRpc.ImportCert = text;
			exchangeCertificateRpc.ImportDescription = this.FriendlyName;
			exchangeCertificateRpc.ImportExportable = this.PrivateKeyExportable;
			ExchangeCertificateRpcVersion exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version1;
			byte[] outputBlob = null;
			try
			{
				byte[] inBlob = exchangeCertificateRpc.SerializeInputParameters(ExchangeCertificateRpcVersion.Version2);
				ExchangeCertificateRpcClient2 exchangeCertificateRpcClient = new ExchangeCertificateRpcClient2(this.serverObject.Name);
				outputBlob = exchangeCertificateRpcClient.ImportCertificate2(0, inBlob, this.Password);
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
					outputBlob = exchangeCertificateRpcClient2.ImportCertificate(0, inBlob2, this.Password);
				}
				catch (RpcException e)
				{
					ManageExchangeCertificate.WriteRpcError(e, this.serverObject.Name, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
			}
			ExchangeCertificateRpc exchangeCertificateRpc2 = new ExchangeCertificateRpc(exchangeCertificateRpcVersion, null, outputBlob);
			ExchangeCertificateRpc.OutputTaskMessages(this.serverObject, exchangeCertificateRpc2, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (exchangeCertificateRpc2.ReturnCert != null)
			{
				exchangeCertificateRpc2.ReturnCert.Identity = this.serverObject.Fqdn + "\\" + exchangeCertificateRpc2.ReturnCert.Thumbprint;
			}
			base.WriteObject(exchangeCertificateRpc2.ReturnCert);
		}

		// Token: 0x060061DF RID: 25055 RVA: 0x00197FB0 File Offset: 0x001961B0
		private byte[] GetFileData(string fileName)
		{
			try
			{
				using (FileStream fileStream = File.OpenRead(fileName))
				{
					byte[] array = new byte[fileStream.Length];
					fileStream.Read(array, 0, (int)fileStream.Length);
					return array;
				}
			}
			catch (Exception ex)
			{
				if (!(ex is UnauthorizedAccessException) && !(ex is DirectoryNotFoundException) && !(ex is PathTooLongException) && !(ex is ArgumentNullException) && !(ex is NotSupportedException) && !(ex is ArgumentException) && !(ex is SecurityException) && !(ex is IOException))
				{
					throw;
				}
				base.WriteError(new InvalidOperationException(Strings.ImportCertificateFileInvalid(this.serverObject.Name, ex.Message)), ErrorCategory.InvalidOperation, null);
			}
			return null;
		}

		// Token: 0x060061E0 RID: 25056 RVA: 0x00198080 File Offset: 0x00196280
		private static string RemoveBase64HeaderFooter(string b64Data)
		{
			b64Data = b64Data.Trim();
			if (b64Data.StartsWith("-----BEGIN CERTIFICATE-----", StringComparison.OrdinalIgnoreCase))
			{
				b64Data = b64Data.Substring("-----BEGIN CERTIFICATE-----".Length);
			}
			else if (b64Data.StartsWith("-----BEGIN PKCS7-----", StringComparison.OrdinalIgnoreCase))
			{
				b64Data = b64Data.Substring("-----BEGIN PKCS7-----".Length);
			}
			if (b64Data.EndsWith("-----END CERTIFICATE-----", StringComparison.OrdinalIgnoreCase))
			{
				b64Data = b64Data.Substring(0, b64Data.Length - "-----END CERTIFICATE-----".Length);
			}
			else if (b64Data.EndsWith("-----END PKCS7-----", StringComparison.OrdinalIgnoreCase))
			{
				b64Data = b64Data.Substring(0, b64Data.Length - "-----END PKCS7-----".Length);
			}
			return b64Data;
		}

		// Token: 0x040035A8 RID: 13736
		private const string OptionalCertHeader = "-----BEGIN CERTIFICATE-----";

		// Token: 0x040035A9 RID: 13737
		private const string OptionalCertTrailer = "-----END CERTIFICATE-----";

		// Token: 0x040035AA RID: 13738
		private const string OptionalPkcs7Header = "-----BEGIN PKCS7-----";

		// Token: 0x040035AB RID: 13739
		private const string OptionalPkcs7Trailer = "-----END PKCS7-----";

		// Token: 0x040035AC RID: 13740
		private Server serverObject;
	}
}

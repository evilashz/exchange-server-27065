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

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AC4 RID: 2756
	[Cmdlet("Export", "ExchangeCertificate", SupportsShouldProcess = true, DefaultParameterSetName = "Thumbprint")]
	public class ExportExchangeCertificate : DataAccessTask<Server>, IIdentityExchangeCertificateCmdlet
	{
		// Token: 0x17001DA7 RID: 7591
		// (get) Token: 0x060061A5 RID: 24997 RVA: 0x00197157 File Offset: 0x00195357
		// (set) Token: 0x060061A6 RID: 24998 RVA: 0x0019716E File Offset: 0x0019536E
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

		// Token: 0x17001DA8 RID: 7592
		// (get) Token: 0x060061A7 RID: 24999 RVA: 0x00197181 File Offset: 0x00195381
		// (set) Token: 0x060061A8 RID: 25000 RVA: 0x00197189 File Offset: 0x00195389
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

		// Token: 0x17001DA9 RID: 7593
		// (get) Token: 0x060061A9 RID: 25001 RVA: 0x00197192 File Offset: 0x00195392
		// (set) Token: 0x060061AA RID: 25002 RVA: 0x001971A9 File Offset: 0x001953A9
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

		// Token: 0x17001DAA RID: 7594
		// (get) Token: 0x060061AB RID: 25003 RVA: 0x001971BC File Offset: 0x001953BC
		// (set) Token: 0x060061AC RID: 25004 RVA: 0x001971D3 File Offset: 0x001953D3
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

		// Token: 0x17001DAB RID: 7595
		// (get) Token: 0x060061AD RID: 25005 RVA: 0x001971E6 File Offset: 0x001953E6
		// (set) Token: 0x060061AE RID: 25006 RVA: 0x001971FD File Offset: 0x001953FD
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

		// Token: 0x17001DAC RID: 7596
		// (get) Token: 0x060061AF RID: 25007 RVA: 0x00197210 File Offset: 0x00195410
		// (set) Token: 0x060061B0 RID: 25008 RVA: 0x00197236 File Offset: 0x00195436
		[Parameter(Mandatory = false)]
		public SwitchParameter BinaryEncoded
		{
			internal get
			{
				return (bool)(base.Fields["BinaryEncoded"] ?? false);
			}
			set
			{
				base.Fields["BinaryEncoded"] = value;
			}
		}

		// Token: 0x17001DAD RID: 7597
		// (get) Token: 0x060061B1 RID: 25009 RVA: 0x00197253 File Offset: 0x00195453
		// (set) Token: 0x060061B2 RID: 25010 RVA: 0x0019726A File Offset: 0x0019546A
		[Parameter(Mandatory = false)]
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

		// Token: 0x060061B3 RID: 25011 RVA: 0x0019727D File Offset: 0x0019547D
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 147, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\ExchangeCertificate\\ExportExchangeCertificate.cs");
		}

		// Token: 0x17001DAE RID: 7598
		// (get) Token: 0x060061B4 RID: 25012 RVA: 0x001972AA File Offset: 0x001954AA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmExportExchangeCertificate(this.Thumbprint);
			}
		}

		// Token: 0x060061B5 RID: 25013 RVA: 0x001972B8 File Offset: 0x001954B8
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
			base.VerifyIsWithinScopes(DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromCustomScopeSet(base.ScopeSet, ADSystemConfigurationSession.GetRootOrgContainerId(this.DomainController, null), base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true), 189, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\ExchangeCertificate\\ExportExchangeCertificate.cs"), this.serverObject, false, new DataAccessTask<Server>.ADObjectOutOfScopeString(Strings.ErrorServerOutOfScope));
			if (string.IsNullOrEmpty(this.Thumbprint))
			{
				base.WriteError(new ArgumentException(Strings.ExceptionEmptyStringNotAllowed, "Thumbprint"), ErrorCategory.InvalidArgument, null);
			}
			this.Thumbprint = ManageExchangeCertificate.UnifyThumbprintFormat(this.Thumbprint);
			if (!string.IsNullOrEmpty(this.FileName) && (File.Exists(this.FileName) || File.Exists(this.FileName + ".pfx")))
			{
				base.WriteError(new ArgumentException(Strings.CertificateInvalidFileName(this.serverObject.Name), "FileName"), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x060061B6 RID: 25014 RVA: 0x00197440 File Offset: 0x00195640
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (base.HasErrors)
			{
				return;
			}
			ExchangeCertificateRpc exchangeCertificateRpc = new ExchangeCertificateRpc();
			exchangeCertificateRpc.ExportByThumbprint = this.Thumbprint;
			exchangeCertificateRpc.ExportBinary = this.BinaryEncoded;
			ExchangeCertificateRpcVersion exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version1;
			byte[] outputBlob = null;
			try
			{
				byte[] inBlob = exchangeCertificateRpc.SerializeInputParameters(ExchangeCertificateRpcVersion.Version2);
				ExchangeCertificateRpcClient2 exchangeCertificateRpcClient = new ExchangeCertificateRpcClient2(this.serverObject.Name);
				outputBlob = exchangeCertificateRpcClient.ExportCertificate2(0, inBlob, this.Password);
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
					outputBlob = exchangeCertificateRpcClient2.ExportCertificate(0, inBlob2, this.Password);
				}
				catch (RpcException e)
				{
					ManageExchangeCertificate.WriteRpcError(e, this.serverObject.Name, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
			}
			ExchangeCertificateRpc exchangeCertificateRpc2 = new ExchangeCertificateRpc(exchangeCertificateRpcVersion, null, outputBlob);
			ExchangeCertificateRpc.OutputTaskMessages(this.serverObject, exchangeCertificateRpc2, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (this.BinaryEncoded)
			{
				base.WriteObject(new BinaryFileDataObject
				{
					FileData = exchangeCertificateRpc2.ReturnExportFileData
				});
			}
			else
			{
				base.WriteObject(exchangeCertificateRpc2.ReturnExportBase64);
			}
			if (!string.IsNullOrEmpty(this.FileName))
			{
				this.WriteCertiricate(exchangeCertificateRpc2);
			}
		}

		// Token: 0x060061B7 RID: 25015 RVA: 0x001975A4 File Offset: 0x001957A4
		private bool HandleException(Exception e)
		{
			return e is UnauthorizedAccessException || e is DirectoryNotFoundException || e is PathTooLongException || e is ArgumentNullException || e is NotSupportedException || e is ArgumentException || e is SecurityException || e is IOException;
		}

		// Token: 0x060061B8 RID: 25016 RVA: 0x001975F4 File Offset: 0x001957F4
		private void WriteCertiricate(ExchangeCertificateRpc outputValues)
		{
			try
			{
				string text = this.FileName;
				string text2 = Path.GetExtension(text).Replace(".", "").ToUpper();
				text2 = text2.Replace("-", "_");
				if (!Enum.IsDefined(typeof(AllowedCertificateTypes), text2))
				{
					text += ".pfx";
				}
				if (this.BinaryEncoded)
				{
					using (FileStream fileStream = File.Create(text))
					{
						fileStream.Write(outputValues.ReturnExportFileData, 0, outputValues.ReturnExportFileData.Length);
						goto IL_A5;
					}
				}
				using (StreamWriter streamWriter = File.CreateText(text))
				{
					streamWriter.Write(outputValues.ReturnExportBase64);
				}
				IL_A5:;
			}
			catch (Exception ex)
			{
				if (!this.HandleException(ex))
				{
					throw;
				}
				base.WriteError(new InvalidOperationException(Strings.ExportCertificateFileInvalid(ex.Message)), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x040035A2 RID: 13730
		private const string ThumbprintParameterName = "Thumbprint";

		// Token: 0x040035A3 RID: 13731
		private const string BinaryEncodedSwitchName = "BinaryEncoded";

		// Token: 0x040035A4 RID: 13732
		private const string FileParamName = "FileName";

		// Token: 0x040035A5 RID: 13733
		private const string DefaultFileExt = ".pfx";

		// Token: 0x040035A6 RID: 13734
		private Server serverObject;
	}
}

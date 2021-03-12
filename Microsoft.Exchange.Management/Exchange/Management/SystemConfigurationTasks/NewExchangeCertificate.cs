using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Security;
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
	// Token: 0x02000ACD RID: 2765
	[Cmdlet("New", "ExchangeCertificate", SupportsShouldProcess = true, DefaultParameterSetName = "Certificate")]
	public class NewExchangeCertificate : DataAccessTask<Server>
	{
		// Token: 0x17001DC6 RID: 7622
		// (get) Token: 0x06006226 RID: 25126 RVA: 0x00199A74 File Offset: 0x00197C74
		// (set) Token: 0x06006227 RID: 25127 RVA: 0x00199A7C File Offset: 0x00197C7C
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

		// Token: 0x17001DC7 RID: 7623
		// (get) Token: 0x06006228 RID: 25128 RVA: 0x00199A85 File Offset: 0x00197C85
		// (set) Token: 0x06006229 RID: 25129 RVA: 0x00199AAB File Offset: 0x00197CAB
		[Parameter(Mandatory = false, ParameterSetName = "Request")]
		public SwitchParameter GenerateRequest
		{
			get
			{
				return (SwitchParameter)(base.Fields["GenerateRequest"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["GenerateRequest"] = value;
			}
		}

		// Token: 0x17001DC8 RID: 7624
		// (get) Token: 0x0600622A RID: 25130 RVA: 0x00199AC3 File Offset: 0x00197CC3
		// (set) Token: 0x0600622B RID: 25131 RVA: 0x00199ADA File Offset: 0x00197CDA
		[Parameter(Mandatory = false, ParameterSetName = "Request")]
		public string RequestFile
		{
			get
			{
				return (string)base.Fields["RequestFile"];
			}
			set
			{
				base.Fields["RequestFile"] = value;
			}
		}

		// Token: 0x17001DC9 RID: 7625
		// (get) Token: 0x0600622C RID: 25132 RVA: 0x00199AED File Offset: 0x00197CED
		// (set) Token: 0x0600622D RID: 25133 RVA: 0x00199B13 File Offset: 0x00197D13
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeAutoDiscover
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeAutoDiscover"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeAutoDiscover"] = value;
			}
		}

		// Token: 0x17001DCA RID: 7626
		// (get) Token: 0x0600622E RID: 25134 RVA: 0x00199B2B File Offset: 0x00197D2B
		// (set) Token: 0x0600622F RID: 25135 RVA: 0x00199B42 File Offset: 0x00197D42
		[Parameter(Mandatory = false)]
		public string FriendlyName
		{
			get
			{
				return (string)base.Fields["FriendlyName"];
			}
			set
			{
				base.Fields["FriendlyName"] = value;
			}
		}

		// Token: 0x17001DCB RID: 7627
		// (get) Token: 0x06006230 RID: 25136 RVA: 0x00199B55 File Offset: 0x00197D55
		// (set) Token: 0x06006231 RID: 25137 RVA: 0x00199B6C File Offset: 0x00197D6C
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public X509Certificate2 Instance
		{
			get
			{
				return (X509Certificate2)base.Fields["Instance"];
			}
			set
			{
				base.Fields["Instance"] = value;
			}
		}

		// Token: 0x17001DCC RID: 7628
		// (get) Token: 0x06006232 RID: 25138 RVA: 0x00199B7F File Offset: 0x00197D7F
		// (set) Token: 0x06006233 RID: 25139 RVA: 0x00199BA5 File Offset: 0x00197DA5
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeAcceptedDomains
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeAcceptedDomains"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeAcceptedDomains"] = value;
			}
		}

		// Token: 0x17001DCD RID: 7629
		// (get) Token: 0x06006234 RID: 25140 RVA: 0x00199BBD File Offset: 0x00197DBD
		// (set) Token: 0x06006235 RID: 25141 RVA: 0x00199BE3 File Offset: 0x00197DE3
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeServerFQDN
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeServerFQDN"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeServerFQDN"] = value;
			}
		}

		// Token: 0x17001DCE RID: 7630
		// (get) Token: 0x06006236 RID: 25142 RVA: 0x00199BFB File Offset: 0x00197DFB
		// (set) Token: 0x06006237 RID: 25143 RVA: 0x00199C21 File Offset: 0x00197E21
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeServerNetBIOSName
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeServerNetBIOSName"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeServerNetBIOSName"] = value;
			}
		}

		// Token: 0x17001DCF RID: 7631
		// (get) Token: 0x06006238 RID: 25144 RVA: 0x00199C39 File Offset: 0x00197E39
		// (set) Token: 0x06006239 RID: 25145 RVA: 0x00199C50 File Offset: 0x00197E50
		[Parameter(Mandatory = false)]
		public X500DistinguishedName SubjectName
		{
			get
			{
				return (X500DistinguishedName)base.Fields["SubjectName"];
			}
			set
			{
				base.Fields["SubjectName"] = value;
			}
		}

		// Token: 0x17001DD0 RID: 7632
		// (get) Token: 0x0600623A RID: 25146 RVA: 0x00199C63 File Offset: 0x00197E63
		// (set) Token: 0x0600623B RID: 25147 RVA: 0x00199C7A File Offset: 0x00197E7A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpDomainWithSubdomains> DomainName
		{
			get
			{
				return (MultiValuedProperty<SmtpDomainWithSubdomains>)base.Fields["DomainName"];
			}
			set
			{
				base.Fields["DomainName"] = value;
			}
		}

		// Token: 0x17001DD1 RID: 7633
		// (get) Token: 0x0600623C RID: 25148 RVA: 0x00199C8D File Offset: 0x00197E8D
		// (set) Token: 0x0600623D RID: 25149 RVA: 0x00199CAF File Offset: 0x00197EAF
		[Parameter(Mandatory = false, ParameterSetName = "Certificate")]
		public virtual AllowedServices Services
		{
			get
			{
				return (AllowedServices)(base.Fields["Services"] ?? AllowedServices.SMTP);
			}
			set
			{
				base.Fields["Services"] = value;
			}
		}

		// Token: 0x17001DD2 RID: 7634
		// (get) Token: 0x0600623E RID: 25150 RVA: 0x00199CC7 File Offset: 0x00197EC7
		// (set) Token: 0x0600623F RID: 25151 RVA: 0x00199CE8 File Offset: 0x00197EE8
		[Parameter(Mandatory = false)]
		public int KeySize
		{
			get
			{
				return (int)(base.Fields["KeySize"] ?? 0);
			}
			set
			{
				base.Fields["KeySize"] = value;
			}
		}

		// Token: 0x17001DD3 RID: 7635
		// (get) Token: 0x06006240 RID: 25152 RVA: 0x00199D00 File Offset: 0x00197F00
		// (set) Token: 0x06006241 RID: 25153 RVA: 0x00199D21 File Offset: 0x00197F21
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

		// Token: 0x17001DD4 RID: 7636
		// (get) Token: 0x06006242 RID: 25154 RVA: 0x00199D39 File Offset: 0x00197F39
		// (set) Token: 0x06006243 RID: 25155 RVA: 0x00199D50 File Offset: 0x00197F50
		[Parameter(Mandatory = false)]
		public string SubjectKeyIdentifier
		{
			get
			{
				return (string)base.Fields["SubjectKeyIdentifier"];
			}
			set
			{
				base.Fields["SubjectKeyIdentifier"] = value;
			}
		}

		// Token: 0x17001DD5 RID: 7637
		// (get) Token: 0x06006244 RID: 25156 RVA: 0x00199D63 File Offset: 0x00197F63
		// (set) Token: 0x06006245 RID: 25157 RVA: 0x00199D89 File Offset: 0x00197F89
		[Parameter(Mandatory = false, ParameterSetName = "Request")]
		public SwitchParameter BinaryEncoded
		{
			get
			{
				return (SwitchParameter)(base.Fields["BinaryEncoded"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["BinaryEncoded"] = value;
			}
		}

		// Token: 0x17001DD6 RID: 7638
		// (get) Token: 0x06006246 RID: 25158 RVA: 0x00199DA1 File Offset: 0x00197FA1
		// (set) Token: 0x06006247 RID: 25159 RVA: 0x00199DB8 File Offset: 0x00197FB8
		[Parameter(Mandatory = false)]
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

		// Token: 0x17001DD7 RID: 7639
		// (get) Token: 0x06006248 RID: 25160 RVA: 0x00199DCB File Offset: 0x00197FCB
		// (set) Token: 0x06006249 RID: 25161 RVA: 0x00199DF1 File Offset: 0x00197FF1
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

		// Token: 0x17001DD8 RID: 7640
		// (get) Token: 0x0600624A RID: 25162 RVA: 0x00199E09 File Offset: 0x00198009
		internal override IConfigurationSession ConfigurationSession
		{
			get
			{
				return (IConfigurationSession)base.DataSession;
			}
		}

		// Token: 0x17001DD9 RID: 7641
		// (get) Token: 0x0600624B RID: 25163 RVA: 0x00199E16 File Offset: 0x00198016
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return this.GetWhatIfMessage();
			}
		}

		// Token: 0x0600624C RID: 25164 RVA: 0x00199E20 File Offset: 0x00198020
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
			base.VerifyIsWithinScopes(DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromCustomScopeSet(base.ScopeSet, ADSystemConfigurationSession.GetRootOrgContainerId(this.DomainController, null), base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true), 325, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\ExchangeCertificate\\NewExchangeCertificate.cs"), this.serverObject, true, new DataAccessTask<Server>.ADObjectOutOfScopeString(Strings.ErrorServerOutOfScope));
			this.ValidateParameters();
			this.inputParams = new ExchangeCertificateRpc();
			this.inputParams.CreateExportable = this.PrivateKeyExportable;
			this.inputParams.CreateIncAccepted = this.IncludeAcceptedDomains;
			this.inputParams.CreateIncFqdn = this.IncludeServerFQDN;
			this.inputParams.CreateIncNetBios = this.IncludeServerNetBIOSName;
			this.inputParams.CreateIncAutoDisc = this.IncludeAutoDiscover;
			this.inputParams.CreateBinary = this.BinaryEncoded;
			this.inputParams.CreateRequest = this.GenerateRequest;
			this.inputParams.CreateKeySize = this.KeySize;
			this.inputParams.CreateServices = this.Services;
			this.inputParams.CreateAllowConfirmation = !this.Force;
			if (this.FriendlyName != null)
			{
				this.inputParams.CreateFriendlyName = this.FriendlyName;
			}
			if (this.SubjectName != null)
			{
				this.inputParams.CreateSubjectName = this.SubjectName.Name;
			}
			if (this.SubjectKeyIdentifier != null)
			{
				this.inputParams.CreateSubjectKeyIdentifier = this.SubjectKeyIdentifier;
			}
			if (this.DomainName != null)
			{
				this.inputParams.CreateDomains = this.DomainName;
			}
			if (this.Instance != null)
			{
				this.inputParams.CreateCloneCert = this.Instance.Export(X509ContentType.SerializedCert);
			}
		}

		// Token: 0x0600624D RID: 25165 RVA: 0x0019A08C File Offset: 0x0019828C
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (base.HasErrors)
			{
				return;
			}
			ExchangeCertificateRpcVersion exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version1;
			byte[] outputBlob = null;
			try
			{
				byte[] inBlob = this.inputParams.SerializeInputParameters(ExchangeCertificateRpcVersion.Version2);
				ExchangeCertificateRpcClient2 exchangeCertificateRpcClient = new ExchangeCertificateRpcClient2(this.serverObject.Name);
				outputBlob = exchangeCertificateRpcClient.CreateCertificate2(0, inBlob);
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
					byte[] inBlob2 = this.inputParams.SerializeInputParameters(ExchangeCertificateRpcVersion.Version1);
					ExchangeCertificateRpcClient exchangeCertificateRpcClient2 = new ExchangeCertificateRpcClient(this.serverObject.Name);
					outputBlob = exchangeCertificateRpcClient2.CreateCertificate(0, inBlob2);
				}
				catch (RpcException e)
				{
					ManageExchangeCertificate.WriteRpcError(e, this.serverObject.Name, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
			}
			ExchangeCertificateRpc exchangeCertificateRpc = new ExchangeCertificateRpc(exchangeCertificateRpcVersion, null, outputBlob);
			ExchangeCertificateRpc.OutputTaskMessages(this.serverObject, exchangeCertificateRpc, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (this.GenerateRequest)
			{
				this.ProcessRequestResults(exchangeCertificateRpc.ReturnCert, exchangeCertificateRpc.ReturnCertRequest);
				return;
			}
			if (exchangeCertificateRpc.ReturnConfirmationList != null)
			{
				foreach (KeyValuePair<AllowedServices, LocalizedString> keyValuePair in exchangeCertificateRpc.ReturnConfirmationList)
				{
					if (base.ShouldContinue(keyValuePair.Value))
					{
						ExchangeCertificateRpc exchangeCertificateRpc2 = new ExchangeCertificateRpc();
						exchangeCertificateRpc2.EnableAllowConfirmation = false;
						exchangeCertificateRpc2.EnableServices = keyValuePair.Key;
						AllowedServices key = keyValuePair.Key;
						if (key == AllowedServices.SMTP)
						{
							exchangeCertificateRpc2.EnableUpdateAD = true;
						}
						exchangeCertificateRpc2.EnableByThumbprint = exchangeCertificateRpc.ReturnCert.Thumbprint;
						try
						{
							byte[] inBlob3 = exchangeCertificateRpc2.SerializeInputParameters(exchangeCertificateRpcVersion);
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
							exchangeCertificateRpc.ReturnCert.Services |= keyValuePair.Key;
						}
						catch (RpcException e2)
						{
							ManageExchangeCertificate.WriteRpcError(e2, this.serverObject.Name, new Task.TaskErrorLoggingDelegate(base.WriteError));
						}
						ExchangeCertificateRpc outputValues = new ExchangeCertificateRpc(exchangeCertificateRpcVersion, null, outputBlob);
						ExchangeCertificateRpc.OutputTaskMessages(this.serverObject, outputValues, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
					}
				}
			}
			exchangeCertificateRpc.ReturnCert.Identity = this.serverObject.Fqdn + "\\" + exchangeCertificateRpc.ReturnCert.Thumbprint;
			base.WriteObject(exchangeCertificateRpc.ReturnCert);
		}

		// Token: 0x0600624E RID: 25166 RVA: 0x0019A374 File Offset: 0x00198574
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(AddAccessRuleCryptographicException).IsInstanceOfType(exception) || typeof(AddAccessRuleArgumentException).IsInstanceOfType(exception) || typeof(AddAccessRuleUnauthorizedAccessException).IsInstanceOfType(exception) || typeof(AddAccessRuleCOMException).IsInstanceOfType(exception);
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x0019A3D2 File Offset: 0x001985D2
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 527, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\ExchangeCertificate\\NewExchangeCertificate.cs");
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x0019A400 File Offset: 0x00198600
		private void ValidateParameters()
		{
			if (!this.GenerateRequest && this.BinaryEncoded)
			{
				base.WriteError(new ArgumentException(Strings.CertificateRequestMissingForArgument(this.serverObject.Name), "BinaryEncoded"), ErrorCategory.InvalidArgument, null);
			}
			if (!this.GenerateRequest && !string.IsNullOrEmpty(this.RequestFile))
			{
				base.WriteError(new ArgumentException(Strings.CertificateRequestMissingGenerateRequest(this.serverObject.Name), "RequestFile"), ErrorCategory.InvalidArgument, null);
			}
			if (!string.IsNullOrEmpty(this.RequestFile) && !this.IsValidRequestFile(this.RequestFile))
			{
				base.WriteError(new ArgumentException(Strings.CertificateInvalidRequestFile(this.serverObject.Name), "RequestFile"), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x0019A4D4 File Offset: 0x001986D4
		private void ProcessRequestResults(ExchangeCertificate certificate, string request)
		{
			if (this.BinaryEncoded)
			{
				BinaryFileDataObject binaryFileDataObject = new BinaryFileDataObject();
				binaryFileDataObject.FileData = Convert.FromBase64String(request);
				base.WriteObject(binaryFileDataObject);
				if (this.GenerateRequest && !string.IsNullOrEmpty(this.RequestFile))
				{
					this.WriteRequest(binaryFileDataObject.FileData, string.Empty);
					return;
				}
			}
			else
			{
				string text = ManageExchangeCertificate.WrapCertificateRequestWithPemTags(request);
				base.WriteObject(text);
				if (this.GenerateRequest && !string.IsNullOrEmpty(this.RequestFile))
				{
					this.WriteRequest(null, text);
				}
			}
		}

		// Token: 0x06006252 RID: 25170 RVA: 0x0019A564 File Offset: 0x00198764
		private bool IsValidRequestFile(string fileName)
		{
			bool result;
			try
			{
				if (File.Exists(fileName))
				{
					result = false;
				}
				else
				{
					using (StreamWriter streamWriter = File.CreateText(fileName))
					{
						streamWriter.Write("H");
					}
					if (File.Exists(fileName))
					{
						File.Delete(fileName);
						result = true;
					}
					else
					{
						result = false;
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				result = false;
			}
			catch (DirectoryNotFoundException)
			{
				result = false;
			}
			catch (PathTooLongException)
			{
				result = false;
			}
			catch (ArgumentNullException)
			{
				result = false;
			}
			catch (NotSupportedException)
			{
				result = false;
			}
			catch (ArgumentException)
			{
				result = false;
			}
			catch (SecurityException)
			{
				result = false;
			}
			catch (IOException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06006253 RID: 25171 RVA: 0x0019A64C File Offset: 0x0019884C
		private void WriteRequest(byte[] data, string text)
		{
			try
			{
				string text2 = this.RequestFile;
				string text3 = Path.GetExtension(text2).Replace(".", "").ToUpper();
				text3 = text3.Replace("-", "_");
				if (!Enum.IsDefined(typeof(AllowedCertificateTypes), text3))
				{
					text2 += ".req";
				}
				if (this.BinaryEncoded)
				{
					using (FileStream fileStream = File.Create(text2))
					{
						fileStream.Write(data, 0, data.Length);
						goto IL_96;
					}
				}
				using (StreamWriter streamWriter = File.CreateText(text2))
				{
					streamWriter.Write(text);
				}
				IL_96:;
			}
			catch (IOException ex)
			{
				base.WriteError(new InvalidOperationException(Strings.RequestCertificateFileInvalid(ex.Message)), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06006254 RID: 25172 RVA: 0x0019A73C File Offset: 0x0019893C
		private LocalizedString GetWhatIfMessage()
		{
			this.inputParams.CreateWhatIf = true;
			ExchangeCertificateRpcVersion exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version1;
			byte[] outputBlob = null;
			try
			{
				byte[] inBlob = this.inputParams.SerializeInputParameters(ExchangeCertificateRpcVersion.Version2);
				ExchangeCertificateRpcClient2 exchangeCertificateRpcClient = new ExchangeCertificateRpcClient2(this.serverObject.Name);
				outputBlob = exchangeCertificateRpcClient.CreateCertificate2(0, inBlob);
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
					byte[] inBlob2 = this.inputParams.SerializeInputParameters(exchangeCertificateRpcVersion);
					ExchangeCertificateRpcClient exchangeCertificateRpcClient2 = new ExchangeCertificateRpcClient(this.serverObject.Name);
					outputBlob = exchangeCertificateRpcClient2.CreateCertificate(0, inBlob2);
				}
				catch (RpcException e)
				{
					ManageExchangeCertificate.WriteRpcError(e, this.serverObject.Name, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
			}
			ExchangeCertificateRpc exchangeCertificateRpc = new ExchangeCertificateRpc(exchangeCertificateRpcVersion, null, outputBlob);
			this.inputParams.CreateWhatIf = false;
			return exchangeCertificateRpc.ReturnConfirmation;
		}

		// Token: 0x040035CB RID: 13771
		private const string BinaryEncodedSwitchName = "BinaryEncoded";

		// Token: 0x040035CC RID: 13772
		private const string RequestFileParamName = "RequestFile";

		// Token: 0x040035CD RID: 13773
		private const string ForceSwitchName = "Force";

		// Token: 0x040035CE RID: 13774
		private const string DefaultFileExt = ".req";

		// Token: 0x040035CF RID: 13775
		private Server serverObject;

		// Token: 0x040035D0 RID: 13776
		private ExchangeCertificateRpc inputParams;

		// Token: 0x02000ACE RID: 2766
		private static class ParameterSet
		{
			// Token: 0x040035D1 RID: 13777
			public const string Certificate = "Certificate";

			// Token: 0x040035D2 RID: 13778
			public const string Request = "Request";
		}
	}
}

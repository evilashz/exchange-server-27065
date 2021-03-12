using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020001C9 RID: 457
	[Cmdlet("Enable", "LiveId", SupportsShouldProcess = true, DefaultParameterSetName = "SHA1Thumbprint")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class EnableLiveId : Task
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00045FC8 File Offset: 0x000441C8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.TargetInstance == LiveIdInstanceType.Consumer)
				{
					return Strings.ConfirmationMessageEnableLiveId(this.SiteId.ToString(), this.SiteName, this.TargetInstance.ToString());
				}
				return Strings.ConfirmationMessageEnableLiveId(this.MsoSiteId.ToString(), this.MsoSiteName, this.TargetInstance.ToString());
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00046030 File Offset: 0x00044230
		// (set) Token: 0x06000FBB RID: 4027 RVA: 0x00046051 File Offset: 0x00044251
		[Parameter(Mandatory = false)]
		public LiveIdInstanceType TargetInstance
		{
			get
			{
				return (LiveIdInstanceType)(base.Fields["TargetInstance"] ?? LiveIdInstanceType.Consumer);
			}
			set
			{
				base.Fields["TargetInstance"] = value;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x0004606C File Offset: 0x0004426C
		// (set) Token: 0x06000FBD RID: 4029 RVA: 0x00046095 File Offset: 0x00044295
		[Parameter(Mandatory = false)]
		public uint SiteId
		{
			get
			{
				object obj = base.Fields["SiteId"];
				if (obj != null)
				{
					return (uint)obj;
				}
				return 0U;
			}
			set
			{
				base.Fields["SiteId"] = value;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x000460AD File Offset: 0x000442AD
		// (set) Token: 0x06000FBF RID: 4031 RVA: 0x000460C4 File Offset: 0x000442C4
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string SiteName
		{
			get
			{
				return (string)base.Fields["SiteName"];
			}
			set
			{
				base.Fields["SiteName"] = value;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x000460D7 File Offset: 0x000442D7
		// (set) Token: 0x06000FC1 RID: 4033 RVA: 0x000460EE File Offset: 0x000442EE
		[Parameter(Mandatory = false)]
		public uint AccrualSiteId
		{
			get
			{
				return (uint)base.Fields["AccrualSiteId"];
			}
			set
			{
				base.Fields["AccrualSiteId"] = value;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00046106 File Offset: 0x00044306
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x0004611D File Offset: 0x0004431D
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string AccrualSiteName
		{
			get
			{
				return (string)base.Fields["AccrualSiteName"];
			}
			set
			{
				base.Fields["AccrualSiteName"] = value;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00046130 File Offset: 0x00044330
		// (set) Token: 0x06000FC5 RID: 4037 RVA: 0x00046147 File Offset: 0x00044347
		[Parameter(Mandatory = false)]
		public string InternalSiteName
		{
			get
			{
				return (string)base.Fields["InternalSiteName"];
			}
			set
			{
				base.Fields["InternalSiteName"] = value;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x0004615A File Offset: 0x0004435A
		// (set) Token: 0x06000FC7 RID: 4039 RVA: 0x00046171 File Offset: 0x00044371
		[Parameter(Mandatory = false)]
		public string O365SiteName
		{
			get
			{
				return (string)base.Fields["O365SiteName"];
			}
			set
			{
				base.Fields["O365SiteName"] = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00046184 File Offset: 0x00044384
		// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x0004619B File Offset: 0x0004439B
		[Parameter(Mandatory = true)]
		public uint MsoSiteId
		{
			get
			{
				return (uint)base.Fields["MsoSiteId"];
			}
			set
			{
				base.Fields["MsoSiteId"] = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x000461B3 File Offset: 0x000443B3
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x000461CA File Offset: 0x000443CA
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string MsoSiteName
		{
			get
			{
				return (string)base.Fields["MsoSiteName"];
			}
			set
			{
				base.Fields["MsoSiteName"] = value;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x000461DD File Offset: 0x000443DD
		// (set) Token: 0x06000FCD RID: 4045 RVA: 0x000461F4 File Offset: 0x000443F4
		[Parameter(Mandatory = true, ParameterSetName = "PfxFileAndPassword")]
		[ValidateNotNullOrEmpty]
		public string Password
		{
			get
			{
				return (string)base.Fields["Password"];
			}
			set
			{
				base.Fields["Password"] = value;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x00046207 File Offset: 0x00044407
		// (set) Token: 0x06000FCF RID: 4047 RVA: 0x0004621E File Offset: 0x0004441E
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "PfxFileAndPassword")]
		public string CertFile
		{
			get
			{
				return (string)base.Fields["CertFile"];
			}
			set
			{
				base.Fields["CertFile"] = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00046231 File Offset: 0x00044431
		// (set) Token: 0x06000FD1 RID: 4049 RVA: 0x00046248 File Offset: 0x00044448
		[Parameter(Mandatory = true, ParameterSetName = "IssuedTo")]
		[ValidateNotNullOrEmpty]
		public string IssuedTo
		{
			get
			{
				return (string)base.Fields["IssuedTo"];
			}
			set
			{
				base.Fields["IssuedTo"] = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0004625B File Offset: 0x0004445B
		// (set) Token: 0x06000FD3 RID: 4051 RVA: 0x00046272 File Offset: 0x00044472
		[Parameter(Mandatory = false, ParameterSetName = "SHA1Thumbprint")]
		[ValidateNotNullOrEmpty]
		public string SHA1Thumbprint
		{
			get
			{
				return (string)base.Fields["SHA1Thumbprint"];
			}
			set
			{
				base.Fields["SHA1Thumbprint"] = value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00046285 File Offset: 0x00044485
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x0004629C File Offset: 0x0004449C
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "SHA1Thumbprint")]
		public string MsoSHA1Thumbprint
		{
			get
			{
				return (string)base.Fields["MsoSHA1Thumbprint"];
			}
			set
			{
				base.Fields["MsoSHA1Thumbprint"] = value;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x000462AF File Offset: 0x000444AF
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x000462C6 File Offset: 0x000444C6
		[Parameter(Mandatory = true)]
		public TargetEnvironment TargetEnvironment
		{
			get
			{
				return (TargetEnvironment)base.Fields["TargetEnvironment"];
			}
			set
			{
				base.Fields["TargetEnvironment"] = value;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x000462DE File Offset: 0x000444DE
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x000462F5 File Offset: 0x000444F5
		[Parameter(Mandatory = false)]
		public string Proxy
		{
			get
			{
				return (string)base.Fields["Proxy"];
			}
			set
			{
				base.Fields["Proxy"] = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00046308 File Offset: 0x00044508
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x0004631F File Offset: 0x0004451F
		[Parameter(Mandatory = true)]
		public string MsoRpsNetworkProd
		{
			get
			{
				return (string)base.Fields["MsoRpsNetworkProd"];
			}
			set
			{
				base.Fields["MsoRpsNetworkProd"] = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00046332 File Offset: 0x00044532
		// (set) Token: 0x06000FDD RID: 4061 RVA: 0x00046349 File Offset: 0x00044549
		[Parameter(Mandatory = true)]
		public string MsoRpsNetworkInt
		{
			get
			{
				return (string)base.Fields["MsoRpsNetworkInt"];
			}
			set
			{
				base.Fields["MsoRpsNetworkInt"] = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0004635C File Offset: 0x0004455C
		private static string RPSInstallLocation
		{
			get
			{
				if (EnableLiveId.rpsInstallLocation == null)
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\RPSSvc"))
					{
						string text = (string)registryKey.GetValue("ImagePath");
						EnableLiveId.rpsInstallLocation = Path.GetDirectoryName(text.Replace("\"", ""));
					}
				}
				return EnableLiveId.rpsInstallLocation;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x000463CC File Offset: 0x000445CC
		private static string RPSConfigLocation
		{
			get
			{
				return Path.Combine(EnableLiveId.RPSInstallLocation, "config");
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x000463DD File Offset: 0x000445DD
		private static string RPSLiveIdConfigLocation
		{
			get
			{
				return Path.Combine(EnableLiveId.RPSInstallLocation, "LiveIdConfig");
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x000463EE File Offset: 0x000445EE
		private static string RPSConfigCertsLocation
		{
			get
			{
				return Path.Combine(EnableLiveId.RPSConfigLocation, "certs");
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x000463FF File Offset: 0x000445FF
		private static string RPSServerFile
		{
			get
			{
				return Path.Combine(EnableLiveId.RPSConfigLocation, "RPSServer.xml");
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00046410 File Offset: 0x00044610
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			ServiceControllerStatus serviceControllerStatus = ServiceControllerStatus.Stopped;
			Exception ex = null;
			try
			{
				using (ServiceController serviceController = new ServiceController("RPSSvc"))
				{
					serviceControllerStatus = serviceController.Status;
				}
			}
			catch (Win32Exception ex2)
			{
				ex = ex2;
			}
			catch (InvalidOperationException ex3)
			{
				ex = ex3;
			}
			if (ex != null || serviceControllerStatus != ServiceControllerStatus.Running)
			{
				base.WriteError(new ArgumentException(Strings.RPSSvcNotRunning), ErrorCategory.InvalidArgument, null);
			}
			if (!Directory.Exists(EnableLiveId.RPSInstallLocation) || !Directory.Exists(EnableLiveId.RPSConfigLocation) || !Directory.Exists(EnableLiveId.RPSConfigCertsLocation))
			{
				base.WriteError(new ArgumentException(Strings.CannotFindRPSInstallLocation(EnableLiveId.RPSInstallLocation)), ErrorCategory.InvalidArgument, null);
			}
			if (!File.Exists(EnableLiveId.RPSServerFile))
			{
				base.WriteError(new ArgumentException(Strings.CannotFindRPSServerFile(EnableLiveId.RPSServerFile)), ErrorCategory.InvalidArgument, null);
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(EnableLiveId.RPSConfigCertsLocation);
			FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
			if (files.Length > 0)
			{
				this.WriteWarning(Strings.DirectoryMustBeEmpty(EnableLiveId.RPSConfigCertsLocation));
				this.doNothing = true;
			}
			if (this.MsoSiteId <= 0U)
			{
				base.WriteError(new ArgumentException(Strings.SiteIdMustBePositive(this.MsoSiteId)), ErrorCategory.InvalidArgument, null);
			}
			if (this.SiteId < 0U)
			{
				base.WriteError(new ArgumentException(Strings.SiteIdMustBePositive(this.SiteId)), ErrorCategory.InvalidArgument, null);
			}
			if (base.ParameterSetName == "PfxFileAndPassword")
			{
				if (!File.Exists(this.CertFile))
				{
					base.WriteError(new ArgumentException(Strings.CertFileNotFound(this.CertFile)), ErrorCategory.InvalidArgument, null);
				}
				byte[] array = null;
				try
				{
					array = EnableLiveId.ReadBinaryFile(this.CertFile);
				}
				catch (IOException ex4)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorTryingToReadPfx(this.CertFile, ex4.Message), ex4), ErrorCategory.InvalidOperation, null);
				}
				try
				{
					if (!EnableLiveId.IsPFXBlob(array))
					{
						base.WriteError(new ArgumentException(Strings.CertFileIsNotPfx(this.CertFile)), ErrorCategory.InvalidArgument, null);
					}
				}
				catch (Win32Exception ex5)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorTryingToReadPfx(this.CertFile, ex5.Message), ex5), ErrorCategory.InvalidOperation, null);
				}
				try
				{
					this.x509Certificate2 = new X509Certificate2(array, this.Password, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
					goto IL_3D7;
				}
				catch (CryptographicException ex6)
				{
					base.WriteError(new InvalidOperationException(ex6.Message, ex6), ErrorCategory.InvalidOperation, null);
					goto IL_3D7;
				}
			}
			if (base.ParameterSetName == "IssuedTo")
			{
				string trimmedIssuedTo = this.IssuedTo.Trim();
				SearchResultByNonUniqueKey searchResultByNonUniqueKey = this.FindCertificateBySubject(trimmedIssuedTo, out this.x509Certificate2);
				if (searchResultByNonUniqueKey == SearchResultByNonUniqueKey.NotFound)
				{
					base.WriteError(new ArgumentException(Strings.CantFindCertBySubject(this.IssuedTo)), ErrorCategory.InvalidArgument, null);
				}
				else if (searchResultByNonUniqueKey == SearchResultByNonUniqueKey.FoundMultiple)
				{
					base.WriteError(new ArgumentException(Strings.MultipleCertsFoundBySubject(this.IssuedTo)), ErrorCategory.InvalidArgument, null);
				}
			}
			else
			{
				string text;
				if (this.SiteId > 0U)
				{
					text = this.SHA1Thumbprint.Trim().Replace(" ", "").ToUpper();
					if (text.Length != 40 || Regex.IsMatch(text, "[^0-9A-F]"))
					{
						base.WriteError(new ArgumentException(Strings.InvalidThumbprintFormat(this.SHA1Thumbprint)), ErrorCategory.InvalidArgument, null);
					}
					if (!this.FindCertificateByThumbprint(text, out this.x509Certificate2))
					{
						base.WriteError(new ArgumentException(Strings.CantFindCertByThumbprint(this.SHA1Thumbprint)), ErrorCategory.InvalidArgument, null);
					}
				}
				text = this.MsoSHA1Thumbprint.Trim().Replace(" ", "").ToUpper();
				if (text.Length != 40 || Regex.IsMatch(text, "[^0-9A-F]"))
				{
					base.WriteError(new ArgumentException(Strings.InvalidThumbprintFormat(this.MsoSHA1Thumbprint)), ErrorCategory.InvalidArgument, null);
				}
				if (!this.FindCertificateByThumbprint(text, out this.msox509Certificate2))
				{
					base.WriteError(new ArgumentException(Strings.CantFindCertByThumbprint(this.MsoSHA1Thumbprint)), ErrorCategory.InvalidArgument, null);
				}
			}
			IL_3D7:
			TaskLogger.LogExit();
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00046848 File Offset: 0x00044A48
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.doNothing)
			{
				TaskLogger.LogExit();
				return;
			}
			this.BackupFile(EnableLiveId.RPSServerFile);
			if (base.ParameterSetName == "PfxFileAndPassword")
			{
				this.InstallCertificateTolocalMachineStore(this.x509Certificate2);
			}
			if (this.msox509Certificate2 != null)
			{
				this.ExportCertificateToCerFile(this.msox509Certificate2, Path.Combine(EnableLiveId.RPSConfigLocation, "ExchangeCert.Business.cer"));
			}
			if (this.x509Certificate2 != null)
			{
				this.ExportCertificateToCerFile(this.x509Certificate2, Path.Combine(EnableLiveId.RPSConfigLocation, "ExchangeCert.Consumer.cer"));
			}
			X509Certificate2 x509Certificate;
			if (this.TargetInstance == LiveIdInstanceType.Business && this.msox509Certificate2 != null)
			{
				x509Certificate = this.msox509Certificate2;
			}
			else
			{
				x509Certificate = this.x509Certificate2;
			}
			this.ExportCertificateToCerFile(x509Certificate, Path.Combine(EnableLiveId.RPSConfigCertsLocation, "ExchangeCert.cer"));
			string text = EnableLiveId.RPSServerFile + ".Consumer";
			string text2 = EnableLiveId.RPSServerFile + ".Business";
			this.UpdateRpsServerConfig(EnableLiveId.RPSServerFile, text2, "ExchangeCert", LiveIdInstanceType.Business, this.TargetEnvironment, this.Proxy, this.MsoSiteId, this.MsoSiteName, 0U, null, this.MsoRpsNetworkProd, this.MsoRpsNetworkInt, this.InternalSiteName, this.O365SiteName);
			if (this.SiteId > 0U)
			{
				this.UpdateRpsServerConfig(EnableLiveId.RPSServerFile, text, "ExchangeCert", LiveIdInstanceType.Consumer, this.TargetEnvironment, this.Proxy, this.SiteId, this.SiteName, this.AccrualSiteId, this.AccrualSiteName, null, null, this.InternalSiteName, null);
			}
			string text3;
			if (this.TargetInstance == LiveIdInstanceType.Business)
			{
				text3 = text2;
			}
			else
			{
				text3 = text;
			}
			File.Copy(text3, EnableLiveId.RPSServerFile, true);
			base.WriteVerbose(Strings.RpsFileCreated(text3, EnableLiveId.RPSServerFile));
			if (this.SiteId > 0U)
			{
				this.PrepareConsumerConfig();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000469FC File Offset: 0x00044BFC
		private void UpdateRpsServerConfig(string xmlConfigFile, string xmlTargetFile, string certificateName, LiveIdInstanceType instance, TargetEnvironment environment, string proxy, uint siteId, string siteName, uint accrualSiteId, string accrualSiteName, string rpsNetworkProd, string rpsNetworkInt, string internalSiteName, string o365SiteName)
		{
			base.WriteVerbose(Strings.AboutToUpdateCongigFile(xmlConfigFile, siteId.ToString()));
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlConfigFile);
			XmlElement documentElement = xmlDocument.DocumentElement;
			try
			{
				string text = "/RPSServer/CryptoParams/CryptoEncCertName";
				XmlNode xmlNode = documentElement.SelectSingleNode(text);
				if (xmlNode == null)
				{
					throw new XmlMissingElementException(new LocalizedString(text));
				}
				xmlNode.InnerText = certificateName;
				text = "/RPSServer/CryptoParams/CryptoSignCertName";
				xmlNode = documentElement.SelectSingleNode(text);
				if (xmlNode == null)
				{
					throw new XmlMissingElementException(new LocalizedString(text));
				}
				xmlNode.InnerText = certificateName;
				if (!string.IsNullOrEmpty(rpsNetworkProd))
				{
					text = "/RPSServer/Environment/Production";
					xmlNode = documentElement.SelectSingleNode(text);
					if (xmlNode == null)
					{
						throw new XmlMissingElementException(new LocalizedString(text));
					}
					xmlNode.InnerText = rpsNetworkProd;
				}
				if (!string.IsNullOrEmpty(rpsNetworkInt))
				{
					text = "/RPSServer/Environment/INT";
					xmlNode = documentElement.SelectSingleNode(text);
					if (xmlNode == null)
					{
						throw new XmlMissingElementException(new LocalizedString(text));
					}
					xmlNode.InnerText = rpsNetworkInt;
				}
				text = "/RPSServer/NetworkServices/Url";
				xmlNode = documentElement.SelectSingleNode(text);
				if (xmlNode == null)
				{
					throw new XmlMissingElementException(new LocalizedString(text));
				}
				switch (environment)
				{
				case TargetEnvironment.Integration:
					if (string.IsNullOrEmpty(rpsNetworkInt))
					{
						xmlNode.InnerText = xmlNode.InnerText.Replace("-ppe", "-int");
					}
					else
					{
						xmlNode.InnerText = rpsNetworkInt;
					}
					break;
				case TargetEnvironment.Production:
					if (string.IsNullOrEmpty(rpsNetworkProd))
					{
						xmlNode.InnerText = xmlNode.InnerText.Replace("-ppe", "");
					}
					else
					{
						xmlNode.InnerText = rpsNetworkProd;
					}
					break;
				}
				text = "/RPSServer/NetworkServices/Proxy";
				xmlNode = documentElement.SelectSingleNode(text);
				if (xmlNode == null)
				{
					throw new XmlMissingElementException(new LocalizedString(text));
				}
				if (!string.IsNullOrEmpty(proxy))
				{
					xmlNode.InnerText = proxy;
				}
				else
				{
					xmlNode.InnerText = "";
				}
				xmlNode = null;
				XmlNode xmlNode2 = null;
				XmlNode xmlNode3 = null;
				List<XmlNode> list = new List<XmlNode>(2);
				text = "/RPSServer/Sites/Site";
				using (XmlNodeList xmlNodeList = documentElement.SelectNodes(text))
				{
					if (xmlNodeList == null)
					{
						throw new XmlMissingElementException(new LocalizedString(text));
					}
					foreach (object obj in xmlNodeList)
					{
						XmlElement xmlElement = (XmlElement)obj;
						xmlNode2 = xmlElement;
						string text2 = xmlElement.GetAttribute("SiteName");
						if (text2 != null)
						{
							text2 = text2.Trim();
							if (text2 != null && text2.Equals("default"))
							{
								xmlNode3 = xmlElement;
							}
							else
							{
								switch (environment)
								{
								case TargetEnvironment.Integration:
									if (text2.IndexOf("INT") == -1)
									{
										list.Add(xmlElement);
									}
									else
									{
										xmlNode = xmlElement;
									}
									break;
								case TargetEnvironment.Preproduction:
									if (text2.IndexOf("PPE") == -1)
									{
										list.Add(xmlElement);
									}
									else
									{
										xmlNode = xmlElement;
									}
									break;
								case TargetEnvironment.Production:
									if (text2.IndexOf("PRODUCTION") == -1)
									{
										list.Add(xmlElement);
									}
									else
									{
										xmlNode = xmlElement;
									}
									break;
								}
							}
						}
					}
				}
				if (xmlNode != null)
				{
					xmlNode2 = xmlNode;
				}
				xmlNode = documentElement.SelectSingleNode("/RPSServer/Sites");
				foreach (XmlNode oldChild in list)
				{
					xmlNode.RemoveChild(oldChild);
				}
				xmlNode = xmlNode2;
				this.UpdateSiteInformation(xmlNode, certificateName, siteId, siteName, "https://" + siteName + "/owa", siteName, siteName, siteName);
				if (xmlNode3 == null)
				{
					throw new XmlMissingElementException(new LocalizedString("/RPSServer/Sites/Site/"));
				}
				this.UpdateSiteInformation(xmlNode3, certificateName, siteId, "default", "", "", "", "");
				if (instance == LiveIdInstanceType.Consumer)
				{
					XmlNode xmlNode4 = xmlNode.Clone();
					this.UpdateSiteInformation(xmlNode4, certificateName, accrualSiteId, accrualSiteName, "https://" + accrualSiteName + "/owa", siteName, siteName, siteName);
					documentElement.SelectSingleNode("/RPSServer/Sites").AppendChild(xmlNode4);
				}
				if (!string.IsNullOrEmpty(internalSiteName))
				{
					XmlNode xmlNode5 = xmlNode.Clone();
					this.UpdateSiteInformation(xmlNode5, certificateName, siteId, internalSiteName, "https://" + internalSiteName + "/owa", internalSiteName, internalSiteName, internalSiteName);
					documentElement.SelectSingleNode("/RPSServer/Sites").AppendChild(xmlNode5);
				}
				if (!string.IsNullOrEmpty(o365SiteName))
				{
					XmlNode xmlNode6 = xmlNode.Clone();
					this.UpdateSiteInformation(xmlNode6, certificateName, siteId, o365SiteName, "https://" + o365SiteName + "/owa", o365SiteName, o365SiteName, o365SiteName);
					documentElement.SelectSingleNode("/RPSServer/Sites").AppendChild(xmlNode6);
				}
				xmlDocument.Save(xmlTargetFile);
			}
			catch (XmlMissingElementException ex)
			{
				base.WriteError(new ArgumentException(Strings.CannotFindXPath(xmlConfigFile, ex.Message)), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00046ED8 File Offset: 0x000450D8
		private void UpdateSiteInformation(XmlNode currentNode, string certificateName, uint siteId, string siteName, string returnUrl, string authCookieDomain, string secAuthCookieDomain, string brandIDCookieDomain)
		{
			((XmlElement)currentNode).SetAttribute("SiteName", siteName);
			string text = "AuthPolicy";
			XmlNode xmlNode = currentNode.SelectSingleNode(text);
			if (xmlNode == null)
			{
				throw new XmlMissingElementException(new LocalizedString("/RPSServer/Sites/Site/" + text));
			}
			xmlNode.InnerText = "MBI_KEY";
			text = "SlidingWindow";
			xmlNode = currentNode.SelectSingleNode(text);
			if (xmlNode == null)
			{
				xmlNode = currentNode.OwnerDocument.CreateNode(XmlNodeType.Element, "SlidingWindow", null);
				currentNode.PrependChild(xmlNode);
			}
			xmlNode.InnerText = "7200";
			text = "AutoSSO";
			xmlNode = currentNode.SelectSingleNode(text);
			if (xmlNode == null)
			{
				xmlNode = currentNode.OwnerDocument.CreateNode(XmlNodeType.Element, text, null);
				currentNode.AppendChild(xmlNode);
			}
			xmlNode.InnerText = "16";
			text = "ADMappingPackage";
			xmlNode = currentNode.SelectSingleNode(text);
			if (xmlNode == null)
			{
				xmlNode = currentNode.OwnerDocument.CreateNode(XmlNodeType.Element, text, null);
				currentNode.AppendChild(xmlNode);
			}
			xmlNode.InnerText = "LiveAp";
			text = "SaveSSO";
			xmlNode = currentNode.SelectSingleNode(text);
			if (xmlNode != null)
			{
				XmlComment newChild = currentNode.OwnerDocument.CreateComment("Removed by Enable-LiveID: " + xmlNode.OuterXml);
				currentNode.InsertAfter(newChild, xmlNode);
				currentNode.RemoveChild(xmlNode);
			}
			text = "CookieCertName";
			xmlNode = currentNode.SelectSingleNode(text);
			if (xmlNode == null)
			{
				throw new XmlMissingElementException(new LocalizedString("/RPSServer/Sites/Site/" + text));
			}
			xmlNode.InnerText = certificateName;
			text = "SiteId";
			xmlNode = currentNode.SelectSingleNode(text);
			if (xmlNode == null)
			{
				xmlNode = currentNode.OwnerDocument.CreateNode(XmlNodeType.Element, "SiteId", null);
				currentNode.PrependChild(xmlNode);
			}
			xmlNode.InnerText = siteId.ToString();
			text = "MaxTicketCheckCount";
			xmlNode = currentNode.SelectSingleNode(text);
			if (xmlNode == null)
			{
				throw new XmlMissingElementException(new LocalizedString("/RPSServer/Sites/Site/" + text));
			}
			xmlNode.InnerText = "2";
			text = "ReturnURL";
			xmlNode = currentNode.SelectSingleNode(text);
			if (xmlNode == null)
			{
				throw new XmlMissingElementException(new LocalizedString("/RPSServer/Sites/Site/" + text));
			}
			xmlNode.InnerText = returnUrl;
			text = "AuthCookieDomain";
			xmlNode = currentNode.SelectSingleNode(text);
			if (xmlNode == null)
			{
				throw new XmlMissingElementException(new LocalizedString("/RPSServer/Sites/Site/" + text));
			}
			xmlNode.InnerText = authCookieDomain;
			text = "SecAuthCookieDomain";
			xmlNode = currentNode.SelectSingleNode(text);
			if (xmlNode == null)
			{
				throw new XmlMissingElementException(new LocalizedString("/RPSServer/Sites/Site/" + text));
			}
			xmlNode.InnerText = secAuthCookieDomain;
			text = "BrandIDCookieDomain";
			xmlNode = currentNode.SelectSingleNode(text);
			if (xmlNode == null)
			{
				throw new XmlMissingElementException(new LocalizedString("/RPSServer/Sites/Site/" + text));
			}
			xmlNode.InnerText = brandIDCookieDomain;
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0004715C File Offset: 0x0004535C
		private void InstallCertificateTolocalMachineStore(X509Certificate2 certificate)
		{
			base.WriteVerbose(Strings.AboutToInstallCertificateToMachineStore(certificate.Thumbprint));
			X509Store x509Store = new X509Store(StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.ReadWrite);
			try
			{
				x509Store.Add(certificate);
			}
			finally
			{
				x509Store.Close();
			}
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x000471A8 File Offset: 0x000453A8
		private bool FindCertificateByThumbprint(string thumbprint, out X509Certificate2 x509Certificate2)
		{
			x509Certificate2 = null;
			X509Store x509Store = new X509Store(StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.ReadOnly);
			base.WriteVerbose(Strings.SearchingForCertificate(thumbprint));
			try
			{
				foreach (X509Certificate2 x509Certificate3 in x509Store.Certificates)
				{
					base.WriteVerbose(Strings.TryingCertificate(x509Certificate3.Subject, x509Certificate3.Thumbprint));
					if (x509Certificate3.Thumbprint.Trim().Replace(" ", "").ToUpper().Equals(thumbprint))
					{
						x509Certificate2 = x509Certificate3;
						base.WriteVerbose(Strings.CertificateFound);
						return true;
					}
				}
			}
			finally
			{
				x509Store.Close();
			}
			base.WriteVerbose(Strings.CertificateWasNotFound);
			return false;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00047268 File Offset: 0x00045468
		private SearchResultByNonUniqueKey FindCertificateBySubject(string trimmedIssuedTo, out X509Certificate2 x509Certificate2)
		{
			x509Certificate2 = null;
			X509Store x509Store = new X509Store(StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.ReadOnly);
			base.WriteVerbose(Strings.SearchingForCertificateBySubject(trimmedIssuedTo));
			try
			{
				foreach (X509Certificate2 x509Certificate3 in x509Store.Certificates)
				{
					base.WriteVerbose(Strings.TryingCertificate(x509Certificate3.Subject, x509Certificate3.Thumbprint));
					string text = x509Certificate3.SubjectName.Decode(X500DistinguishedNameFlags.Reversed | X500DistinguishedNameFlags.DoNotUsePlusSign | X500DistinguishedNameFlags.DoNotUseQuotes | X500DistinguishedNameFlags.UseCommas);
					if (string.Equals(trimmedIssuedTo, text, StringComparison.InvariantCultureIgnoreCase) || string.Equals(trimmedIssuedTo, EnableLiveId.GetCNValueFromX500DN(text), StringComparison.InvariantCultureIgnoreCase))
					{
						base.WriteVerbose(Strings.CertificateFound);
						if (x509Certificate2 != null)
						{
							return SearchResultByNonUniqueKey.FoundMultiple;
						}
						x509Certificate2 = x509Certificate3;
					}
				}
			}
			finally
			{
				x509Store.Close();
			}
			if (x509Certificate2 != null)
			{
				return SearchResultByNonUniqueKey.FoundSingle;
			}
			base.WriteVerbose(Strings.CertificateWasNotFound);
			return SearchResultByNonUniqueKey.NotFound;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00047338 File Offset: 0x00045538
		private static string GetCNValueFromX500DN(string x500DN)
		{
			string[] array = x500DN.Split(new char[]
			{
				','
			});
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					'='
				});
				if (array3.Length > 1 && array3[0].Trim().ToUpper().Equals("CN"))
				{
					return array3[1].Trim();
				}
			}
			return string.Empty;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x000473C0 File Offset: 0x000455C0
		private void ExportCertificateToCerFile(X509Certificate2 x509Certificate2, string fileName)
		{
			base.WriteVerbose(Strings.AboutToExportCertificateToFile(x509Certificate2.Thumbprint, fileName));
			byte[] buffer = x509Certificate2.Export(X509ContentType.Cert);
			using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					binaryWriter.Write(buffer);
				}
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00047434 File Offset: 0x00045634
		private static byte[] ReadBinaryFile(string fileName)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			byte[] result = null;
			using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					result = binaryReader.ReadBytes((int)fileInfo.Length);
				}
			}
			return result;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x000474A0 File Offset: 0x000456A0
		private void BackupFile(string fileName)
		{
			string text = fileName + ".backup";
			if (!File.Exists(text))
			{
				File.Copy(fileName, text);
				base.WriteVerbose(Strings.BackupFileCreated(fileName, text));
				return;
			}
			base.WriteVerbose(Strings.BackupFileNotCreated(fileName, text));
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x000474E4 File Offset: 0x000456E4
		private void PrepareConsumerConfig()
		{
			if (!Directory.Exists(EnableLiveId.RPSLiveIdConfigLocation))
			{
				Directory.CreateDirectory(EnableLiveId.RPSLiveIdConfigLocation);
			}
			string text = Path.Combine(EnableLiveId.RPSLiveIdConfigLocation, "rpsserver.xml");
			if (!File.Exists(text))
			{
				File.Copy(Path.Combine(EnableLiveId.RPSConfigLocation, "RPSServer.Xml.Consumer"), text);
			}
			string text2 = Path.Combine(EnableLiveId.RPSLiveIdConfigLocation, "rpscomponent.xml");
			if (!File.Exists(text2))
			{
				File.Copy(Path.Combine(EnableLiveId.RPSConfigLocation, "rpscomponent.xml"), text2);
			}
			string text3 = Path.Combine(EnableLiveId.RPSLiveIdConfigLocation, "certs");
			if (!Directory.Exists(text3))
			{
				Directory.CreateDirectory(text3);
			}
			string text4 = Path.Combine(text3, "ExchangeCert.cer");
			if (!File.Exists(text4))
			{
				File.Copy(Path.Combine(EnableLiveId.RPSConfigLocation, "ExchangeCert.Consumer.cer"), text4);
			}
		}

		// Token: 0x06000FEF RID: 4079
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool PFXIsPFXBlob([In] ref EnableLiveId.CRYPTOAPI_BLOB blob);

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000475AC File Offset: 0x000457AC
		private static bool IsPFXBlob(byte[] pfxBlobData)
		{
			if (pfxBlobData == null)
			{
				throw new ArgumentNullException("pfxBlobData");
			}
			EnableLiveId.CRYPTOAPI_BLOB cryptoapi_BLOB;
			cryptoapi_BLOB.cbData = (uint)pfxBlobData.Length;
			cryptoapi_BLOB.pbData = pfxBlobData;
			bool result = EnableLiveId.PFXIsPFXBlob(ref cryptoapi_BLOB);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error != 0)
			{
				throw new Win32Exception(lastWin32Error);
			}
			return result;
		}

		// Token: 0x04000753 RID: 1875
		private const string DefaultAuthPolicy = "MBI_KEY";

		// Token: 0x04000754 RID: 1876
		private const string RPSImagePathKey = "SYSTEM\\CurrentControlSet\\Services\\RPSSvc";

		// Token: 0x04000755 RID: 1877
		private const string RPSImagePathValue = "ImagePath";

		// Token: 0x04000756 RID: 1878
		private const string certName = "ExchangeCert";

		// Token: 0x04000757 RID: 1879
		private static string rpsInstallLocation;

		// Token: 0x04000758 RID: 1880
		private X509Certificate2 x509Certificate2;

		// Token: 0x04000759 RID: 1881
		private X509Certificate2 msox509Certificate2;

		// Token: 0x0400075A RID: 1882
		private bool doNothing;

		// Token: 0x020001CA RID: 458
		private struct CRYPTOAPI_BLOB
		{
			// Token: 0x0400075B RID: 1883
			internal uint cbData;

			// Token: 0x0400075C RID: 1884
			internal byte[] pbData;
		}
	}
}

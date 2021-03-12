using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AC7 RID: 2759
	[Cmdlet("Install", "ExchangeCertificate", SupportsShouldProcess = true)]
	public class InstallExchangeCertificate : DataAccessTask<Server>
	{
		// Token: 0x17001DBE RID: 7614
		// (get) Token: 0x060061E2 RID: 25058 RVA: 0x00198132 File Offset: 0x00196332
		// (set) Token: 0x060061E3 RID: 25059 RVA: 0x0019813A File Offset: 0x0019633A
		[Parameter(Mandatory = true)]
		public AllowedServices Services
		{
			get
			{
				return this.services;
			}
			set
			{
				this.services = value;
			}
		}

		// Token: 0x17001DBF RID: 7615
		// (get) Token: 0x060061E4 RID: 25060 RVA: 0x00198143 File Offset: 0x00196343
		// (set) Token: 0x060061E5 RID: 25061 RVA: 0x00198169 File Offset: 0x00196369
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

		// Token: 0x17001DC0 RID: 7616
		// (get) Token: 0x060061E6 RID: 25062 RVA: 0x00198181 File Offset: 0x00196381
		// (set) Token: 0x060061E7 RID: 25063 RVA: 0x00198189 File Offset: 0x00196389
		[Parameter(Mandatory = false)]
		public new string DomainController
		{
			get
			{
				return this.domainController;
			}
			set
			{
				this.domainController = value;
			}
		}

		// Token: 0x17001DC1 RID: 7617
		// (get) Token: 0x060061E8 RID: 25064 RVA: 0x00198192 File Offset: 0x00196392
		// (set) Token: 0x060061E9 RID: 25065 RVA: 0x0019819A File Offset: 0x0019639A
		[Parameter(Mandatory = false)]
		public virtual string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
			set
			{
				this.thumbprint = value;
			}
		}

		// Token: 0x17001DC2 RID: 7618
		// (get) Token: 0x060061EA RID: 25066 RVA: 0x001981A3 File Offset: 0x001963A3
		// (set) Token: 0x060061EB RID: 25067 RVA: 0x001981AB File Offset: 0x001963AB
		[Parameter(Mandatory = false)]
		public string WebSiteName
		{
			get
			{
				return this.webSiteName;
			}
			set
			{
				this.webSiteName = value;
			}
		}

		// Token: 0x17001DC3 RID: 7619
		// (get) Token: 0x060061EC RID: 25068 RVA: 0x001981B4 File Offset: 0x001963B4
		// (set) Token: 0x060061ED RID: 25069 RVA: 0x001981BC File Offset: 0x001963BC
		[Parameter(Mandatory = false)]
		public bool InstallInTrustedRootCAIfSelfSigned
		{
			get
			{
				return this.installInTrustedRootCAIfSelfSigned;
			}
			set
			{
				this.installInTrustedRootCAIfSelfSigned = value;
			}
		}

		// Token: 0x17001DC4 RID: 7620
		// (get) Token: 0x060061EE RID: 25070 RVA: 0x001981C5 File Offset: 0x001963C5
		// (set) Token: 0x060061EF RID: 25071 RVA: 0x001981EB File Offset: 0x001963EB
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

		// Token: 0x17001DC5 RID: 7621
		// (get) Token: 0x060061F0 RID: 25072 RVA: 0x00198203 File Offset: 0x00196403
		internal override IConfigurationSession ConfigurationSession
		{
			get
			{
				return (IConfigurationSession)base.DataSession;
			}
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x00198210 File Offset: 0x00196410
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 182, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\ExchangeCertificate\\InstallExchangeCertificate.cs");
		}

		// Token: 0x060061F2 RID: 25074 RVA: 0x00198234 File Offset: 0x00196434
		protected override void InternalValidate()
		{
			bool flag = false;
			try
			{
				this.localServer = ManageExchangeCertificate.FindLocalServer((ITopologyConfigurationSession)this.ConfigurationSession);
			}
			catch (LocalServerNotFoundException)
			{
				flag = true;
			}
			if (flag || !ManageExchangeCertificate.IsServerRoleSupported(this.localServer))
			{
				base.WriteError(new RoleDoesNotSupportExchangeCertificateTasksException(), ErrorCategory.InvalidOperation, null);
			}
			ManageExchangeCertificate.AddUniqueDomainIfValid(this.rawDomains, ComputerInformation.DnsHostName);
			ManageExchangeCertificate.AddUniqueDomainIfValid(this.rawDomains, ComputerInformation.DnsPhysicalHostName);
			ManageExchangeCertificate.AddUniqueDomainIfValid(this.rawDomains, ComputerInformation.DnsFullyQualifiedDomainName);
			ManageExchangeCertificate.AddUniqueDomainIfValid(this.rawDomains, ComputerInformation.DnsPhysicalFullyQualifiedDomainName);
			this.subjectName = TlsCertificateInfo.GetDefaultSubjectName(this.rawDomains);
		}

		// Token: 0x060061F3 RID: 25075 RVA: 0x001982E0 File Offset: 0x001964E0
		protected X509Certificate2 GenerateSelfSignedCertificate()
		{
			TimeSpan validFor = DateTime.UtcNow.AddMonths(60) - DateTime.UtcNow;
			return TlsCertificateInfo.CreateSelfSignCertificate(this.subjectName, this.rawDomains, validFor, CertificateCreationOption.None, 2048, null);
		}

		// Token: 0x060061F4 RID: 25076 RVA: 0x00198320 File Offset: 0x00196520
		protected override void InternalProcessRecord()
		{
			X509Certificate2 x509Certificate = null;
			if (!string.IsNullOrEmpty(this.thumbprint))
			{
				this.thumbprint = ManageExchangeCertificate.UnifyThumbprintFormat(this.thumbprint);
				x509Certificate = this.FindCertificate(this.thumbprint);
				if (x509Certificate == null)
				{
					base.WriteError(new ArgumentException(Strings.CertificateNotFound(this.thumbprint), "Thumbprint"), ErrorCategory.InvalidArgument, this.thumbprint);
				}
			}
			else
			{
				AllowedServices allowedServices = this.Services;
				if (allowedServices != AllowedServices.IIS && allowedServices != (AllowedServices.IMAP | AllowedServices.POP | AllowedServices.IIS))
				{
					if (allowedServices != AllowedServices.SMTP)
					{
						return;
					}
				}
				else
				{
					x509Certificate = this.FindIisCertificate();
				}
				if (x509Certificate == null && this.Services != AllowedServices.SMTP)
				{
					try
					{
						x509Certificate = InstallExchangeCertificate.GetDefaultCertificate();
					}
					catch (ArgumentException exception)
					{
						base.WriteError(exception, ErrorCategory.InvalidData, null);
						return;
					}
				}
				if (x509Certificate == null)
				{
					if (!this.rawDomains.Any<string>())
					{
						base.WriteError(new UnableToResolveValidDomainExchangeCertificateTasksException(ComputerInformation.DnsHostName, ComputerInformation.DnsPhysicalHostName, ComputerInformation.DnsFullyQualifiedDomainName, ComputerInformation.DnsPhysicalFullyQualifiedDomainName), ErrorCategory.InvalidOperation, null);
					}
					try
					{
						x509Certificate = this.GenerateSelfSignedCertificate();
					}
					catch (CryptographicException exception2)
					{
						base.WriteError(exception2, ErrorCategory.InvalidOperation, null);
					}
				}
				if (x509Certificate != null && this.InstallInTrustedRootCAIfSelfSigned && TlsCertificateInfo.IsSelfSignedCertificate(x509Certificate))
				{
					TlsCertificateInfo.TryInstallCertificateInTrustedRootCA(x509Certificate);
				}
			}
			base.WriteVerbose(Strings.CertificateInformation(x509Certificate.Issuer, x509Certificate.NotBefore, x509Certificate.NotAfter, x509Certificate.Subject));
			if ((DateTime)ExDateTime.Now < x509Certificate.NotBefore || (DateTime)ExDateTime.Now > x509Certificate.NotAfter)
			{
				base.WriteError(new CryptographicException(Strings.CertificateStatusDateInvalid), ErrorCategory.InvalidData, null);
			}
			try
			{
				this.EnableForServices(x509Certificate, this.Services);
			}
			catch (IISNotInstalledException)
			{
				base.WriteError(new ArgumentException(Strings.IISNotInstalled, "Services"), ErrorCategory.InvalidArgument, null);
			}
			catch (InvalidOperationException exception3)
			{
				base.WriteError(exception3, ErrorCategory.ObjectNotFound, null);
			}
		}

		// Token: 0x060061F5 RID: 25077 RVA: 0x00198508 File Offset: 0x00196708
		protected void EnableForServices(X509Certificate2 cert, AllowedServices services)
		{
			try
			{
				ManageExchangeCertificate.EnableForServices(cert, services, this.webSiteName, !this.DoNotRequireSsl, (ITopologyConfigurationSession)base.DataSession, this.localServer, null, false, this.NetworkServiceAllowed);
			}
			catch (IISNotInstalledException)
			{
				base.WriteError(new ArgumentException(Strings.IISNotInstalled, "Services"), ErrorCategory.InvalidArgument, null);
			}
			catch (InvalidOperationException exception)
			{
				base.WriteError(exception, ErrorCategory.ObjectNotFound, null);
			}
			catch (LocalizedException exception2)
			{
				base.WriteError(exception2, ErrorCategory.NotSpecified, null);
			}
		}

		// Token: 0x060061F6 RID: 25078 RVA: 0x001985B4 File Offset: 0x001967B4
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(AddAccessRuleCryptographicException).IsInstanceOfType(exception) || typeof(AddAccessRuleArgumentException).IsInstanceOfType(exception) || typeof(AddAccessRuleUnauthorizedAccessException).IsInstanceOfType(exception) || typeof(AddAccessRuleCOMException).IsInstanceOfType(exception);
		}

		// Token: 0x060061F7 RID: 25079 RVA: 0x00198614 File Offset: 0x00196814
		private static X509Certificate2 GetDefaultCertificate()
		{
			string[] names = new string[]
			{
				ComputerInformation.DnsFullyQualifiedDomainName,
				ComputerInformation.DnsHostName,
				ComputerInformation.DnsPhysicalFullyQualifiedDomainName,
				ComputerInformation.DnsPhysicalHostName
			};
			return TlsCertificateInfo.FindCertificate(names, CertificateSelectionOption.PreferedNonSelfSigned);
		}

		// Token: 0x060061F8 RID: 25080 RVA: 0x00198654 File Offset: 0x00196854
		private X509Certificate2 FindIisCertificate()
		{
			string webSiteSslCertificate = IisUtility.GetWebSiteSslCertificate("IIS://localhost/W3SVC/1");
			if (string.IsNullOrEmpty(webSiteSslCertificate))
			{
				return null;
			}
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.ReadWrite | OpenFlags.OpenExistingOnly);
			X509Certificate2Collection x509Certificate2Collection;
			try
			{
				x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, webSiteSslCertificate, false);
			}
			finally
			{
				x509Store.Close();
			}
			if (x509Certificate2Collection.Count > 0)
			{
				return x509Certificate2Collection[0];
			}
			return null;
		}

		// Token: 0x060061F9 RID: 25081 RVA: 0x001986C0 File Offset: 0x001968C0
		private X509Certificate2 FindCertificate(string thumbprint)
		{
			if (string.IsNullOrEmpty(thumbprint))
			{
				return null;
			}
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.OpenExistingOnly);
			X509Certificate2Collection x509Certificate2Collection;
			try
			{
				x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
			}
			finally
			{
				x509Store.Close();
			}
			if (x509Certificate2Collection.Count > 0)
			{
				return x509Certificate2Collection[0];
			}
			return null;
		}

		// Token: 0x040035AD RID: 13741
		private const int KeySize = 2048;

		// Token: 0x040035AE RID: 13742
		private Server localServer;

		// Token: 0x040035AF RID: 13743
		private X500DistinguishedName subjectName;

		// Token: 0x040035B0 RID: 13744
		private List<string> rawDomains = new List<string>();

		// Token: 0x040035B1 RID: 13745
		private string domainController;

		// Token: 0x040035B2 RID: 13746
		private AllowedServices services = AllowedServices.SMTP;

		// Token: 0x040035B3 RID: 13747
		private string thumbprint;

		// Token: 0x040035B4 RID: 13748
		private string webSiteName;

		// Token: 0x040035B5 RID: 13749
		private bool installInTrustedRootCAIfSelfSigned;
	}
}

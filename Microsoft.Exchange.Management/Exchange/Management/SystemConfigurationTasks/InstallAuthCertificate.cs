using System;
using System.Management.Automation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200060E RID: 1550
	[Cmdlet("Install", "AuthCertificate")]
	public class InstallAuthCertificate : Task
	{
		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x060036E0 RID: 14048 RVA: 0x000E34CD File Offset: 0x000E16CD
		// (set) Token: 0x060036E1 RID: 14049 RVA: 0x000E34E4 File Offset: 0x000E16E4
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

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x060036E2 RID: 14050 RVA: 0x000E34F7 File Offset: 0x000E16F7
		// (set) Token: 0x060036E3 RID: 14051 RVA: 0x000E34FF File Offset: 0x000E16FF
		private IConfigurationSession ConfigSession { get; set; }

		// Token: 0x060036E4 RID: 14052 RVA: 0x000E3508 File Offset: 0x000E1708
		protected override void InternalBeginProcessing()
		{
			this.ConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 75, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\OAuth\\InstallAuthCertificate.cs");
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000E3544 File Offset: 0x000E1744
		protected override void InternalProcessRecord()
		{
			if (this.IsAuthCertConfigured())
			{
				return;
			}
			X509Certificate2 certificate = this.GenerateCertificate();
			if (this.IsAuthCertConfigured())
			{
				this.DeleteCertificate(certificate);
				return;
			}
			this.StampAuthCertificate(certificate);
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000E3578 File Offset: 0x000E1778
		private bool IsAuthCertConfigured()
		{
			AuthConfig authConfig = AuthConfig.Read(this.ConfigSession);
			return authConfig == null || !string.IsNullOrEmpty(authConfig.CurrentCertificateThumbprint);
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000E35A4 File Offset: 0x000E17A4
		private void StampAuthCertificate(X509Certificate2 certificate)
		{
			AuthConfig authConfig = AuthConfig.Read(this.ConfigSession);
			authConfig.CurrentCertificateThumbprint = certificate.Thumbprint;
			this.ConfigSession.Save(authConfig);
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000E35D8 File Offset: 0x000E17D8
		private X509Certificate2 GenerateCertificate()
		{
			return TlsCertificateInfo.CreateSelfSignCertificate(new X500DistinguishedName("CN=" + InstallAuthCertificate.CertificateSubject), null, InstallAuthCertificate.CertificateLifeTime, CertificateCreationOption.Exportable, 2048, InstallAuthCertificate.CertificateSubject);
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000E3614 File Offset: 0x000E1814
		private void DeleteCertificate(X509Certificate2 certificate)
		{
			X509Store x509Store = null;
			try
			{
				x509Store = new X509Store(StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.ReadWrite | OpenFlags.OpenExistingOnly);
				x509Store.Remove(certificate);
			}
			catch (CryptographicException)
			{
			}
			finally
			{
				if (x509Store != null)
				{
					x509Store.Close();
				}
			}
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000E3668 File Offset: 0x000E1868
		protected override bool IsKnownException(Exception e)
		{
			return e is CryptographicException || base.IsKnownException(e);
		}

		// Token: 0x0400256F RID: 9583
		private const int KeySize = 2048;

		// Token: 0x04002570 RID: 9584
		internal static readonly string CertificateSubject = "Microsoft Exchange Server Auth Certificate";

		// Token: 0x04002571 RID: 9585
		internal static readonly TimeSpan CertificateLifeTime = TimeSpan.FromDays(1800.0);
	}
}

using System;
using System.Management.Automation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.FederationProvisioning;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009E1 RID: 2529
	[Cmdlet("Get", "FederatedDomainProof")]
	public sealed class GetFederatedDomainProof : Task
	{
		// Token: 0x17001B05 RID: 6917
		// (get) Token: 0x06005A57 RID: 23127 RVA: 0x0017A107 File Offset: 0x00178307
		// (set) Token: 0x06005A58 RID: 23128 RVA: 0x0017A10F File Offset: 0x0017830F
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public SmtpDomain DomainName { get; set; }

		// Token: 0x17001B06 RID: 6918
		// (get) Token: 0x06005A59 RID: 23129 RVA: 0x0017A118 File Offset: 0x00178318
		// (set) Token: 0x06005A5A RID: 23130 RVA: 0x0017A120 File Offset: 0x00178320
		[Parameter(Mandatory = false)]
		public string Thumbprint { get; set; }

		// Token: 0x17001B07 RID: 6919
		// (get) Token: 0x06005A5B RID: 23131 RVA: 0x0017A129 File Offset: 0x00178329
		// (set) Token: 0x06005A5C RID: 23132 RVA: 0x0017A131 File Offset: 0x00178331
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public Fqdn DomainController { get; set; }

		// Token: 0x06005A5D RID: 23133 RVA: 0x0017A254 File Offset: 0x00178454
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (!string.IsNullOrEmpty(this.Thumbprint))
			{
				this.ProcessForCertificate(this.Thumbprint, null);
			}
			else
			{
				FederationTrust federationTrust = this.GetFederationTrust();
				var array = new <>f__AnonymousType25<string, string>[]
				{
					new
					{
						PropertyName = "OrgNextPrivCertificate",
						Thumbprint = federationTrust.OrgNextPrivCertificate
					},
					new
					{
						PropertyName = "OrgPrivCertificate",
						Thumbprint = federationTrust.OrgPrivCertificate
					},
					new
					{
						PropertyName = "OrgPrevPrivCertificate",
						Thumbprint = federationTrust.OrgPrevPrivCertificate
					}
				};
				var array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					var <>f__AnonymousType = array2[i];
					if (!string.IsNullOrEmpty(<>f__AnonymousType.Thumbprint))
					{
						this.ProcessForCertificate(<>f__AnonymousType.Thumbprint, <>f__AnonymousType.PropertyName);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005A5E RID: 23134 RVA: 0x0017A314 File Offset: 0x00178514
		private void ProcessForCertificate(string thumbprint, string propertyName)
		{
			X509Certificate2 certificate = null;
			try
			{
				certificate = FederationCertificate.LoadCertificateWithPrivateKey(thumbprint, new WriteVerboseDelegate(base.WriteVerbose));
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
			}
			byte[] signature = FederatedDomainProofAlgorithm.GetSignature(certificate, this.DomainName.Domain);
			using (HashAlgorithm hashAlgorithm = new SHA512Cng())
			{
				byte[] inArray = hashAlgorithm.ComputeHash(signature);
				base.WriteObject(new FederatedDomainProof(this.DomainName, propertyName, thumbprint, Convert.ToBase64String(inArray)));
			}
		}

		// Token: 0x06005A5F RID: 23135 RVA: 0x0017A3A8 File Offset: 0x001785A8
		private FederationTrust GetFederationTrust()
		{
			IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 130, "GetFederationTrust", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Federation\\GetFederatedDomainProof.cs");
			FederationTrust[] array = configurationSession.Find<FederationTrust>(null, QueryScope.SubTree, null, null, 2);
			if (array == null || array.Length == 0)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.NoFederationTrust), (ErrorCategory)1003, null);
				return null;
			}
			if (array.Length > 1)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.TooManyFederationTrust), (ErrorCategory)1003, null);
				return null;
			}
			return array[0];
		}
	}
}

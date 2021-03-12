using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000ABF RID: 2751
	[Serializable]
	public class ExchangeCertificate : X509Certificate2, ISerializable, IFormattable
	{
		// Token: 0x06006121 RID: 24865 RVA: 0x001955B0 File Offset: 0x001937B0
		private ExchangeCertificate(SerializationInfo info, StreamingContext context)
		{
			byte[] rawData = (byte[])info.GetValue("CertData", typeof(byte[]));
			this.Import(rawData);
			this.selfSigned = (bool)info.GetValue("SelfSigned", typeof(bool));
			this.status = (CertificateStatus)info.GetValue("Status", typeof(CertificateStatus));
			this.rootCAType = (CertificateAuthorityType)info.GetValue("RootCAType", typeof(CertificateAuthorityType));
			this.services = (AllowedServices)info.GetValue("Services", typeof(AllowedServices));
			this.iisServices = (List<IisService>)info.GetValue("IISServices", typeof(List<IisService>));
			this.privateKeyExportable = (bool)info.GetValue("PrivateKeyExportable", typeof(bool));
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "Identity")
				{
					this.Identity = info.GetString("Identity");
				}
			}
		}

		// Token: 0x06006122 RID: 24866 RVA: 0x001956EC File Offset: 0x001938EC
		internal ExchangeCertificate(object[] data)
		{
			if (data == null && data.Length < 8)
			{
				throw new ArgumentException("data");
			}
			this.Import((byte[])data[0]);
			this.selfSigned = (bool)data[1];
			this.status = (CertificateStatus)data[2];
			this.rootCAType = (CertificateAuthorityType)data[3];
			this.services = (AllowedServices)data[4];
			List<IisService> list = new List<IisService>();
			list.AddRange((IisService[])data[5]);
			this.iisServices = list;
			this.privateKeyExportable = (bool)data[6];
			if (data[7] != null)
			{
				this.Identity = (string)data[7];
			}
		}

		// Token: 0x06006123 RID: 24867 RVA: 0x001957A0 File Offset: 0x001939A0
		public ExchangeCertificate(X509Certificate2 cert) : base(cert)
		{
			this.services = AllowedServices.None;
		}

		// Token: 0x06006124 RID: 24868 RVA: 0x001957BC File Offset: 0x001939BC
		internal static ExchangeCertificate GetInternalTransportCertificate(Server server)
		{
			if (server == null)
			{
				throw new ArgumentNullException("server");
			}
			if (server.InternalTransportCertificate == null)
			{
				return null;
			}
			ExchangeCertificate result;
			try
			{
				X509Certificate2 cert = new X509Certificate2(server.InternalTransportCertificate);
				result = new ExchangeCertificate(cert);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06006125 RID: 24869 RVA: 0x0019580C File Offset: 0x00193A0C
		internal static ExchangeCertificate GetCertificateFromStore(string storeName, string thumbprint)
		{
			X509Store store = new X509Store(storeName, StoreLocation.LocalMachine);
			return ExchangeCertificate.GetCertificateFromStore(store, thumbprint);
		}

		// Token: 0x06006126 RID: 24870 RVA: 0x00195828 File Offset: 0x00193A28
		internal static ExchangeCertificate GetCertificateFromStore(StoreName storeName, string thumbprint)
		{
			X509Store store = new X509Store(storeName, StoreLocation.LocalMachine);
			return ExchangeCertificate.GetCertificateFromStore(store, thumbprint);
		}

		// Token: 0x06006127 RID: 24871 RVA: 0x00195844 File Offset: 0x00193A44
		private static ExchangeCertificate GetCertificateFromStore(X509Store store, string thumbprint)
		{
			if (thumbprint == null)
			{
				throw new ArgumentNullException("thumbprint");
			}
			try
			{
				store.Open(OpenFlags.OpenExistingOnly);
			}
			catch (CryptographicException)
			{
				return null;
			}
			ExchangeCertificate result;
			try
			{
				X509Certificate2Collection x509Certificate2Collection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
				if (x509Certificate2Collection.Count == 0)
				{
					result = null;
				}
				else
				{
					result = new ExchangeCertificate(x509Certificate2Collection[0]);
				}
			}
			finally
			{
				store.Close();
			}
			return result;
		}

		// Token: 0x06006128 RID: 24872 RVA: 0x001958BC File Offset: 0x00193ABC
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return ExFormatProvider.FormatX509Certificate(this, format, formatProvider);
		}

		// Token: 0x06006129 RID: 24873 RVA: 0x001958C8 File Offset: 0x00193AC8
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.CheckCertificateChainAndCacheProps();
			info.AddValue("CertData", this.Export(X509ContentType.SerializedCert));
			info.AddValue("SelfSigned", this.selfSigned);
			info.AddValue("Status", this.status);
			info.AddValue("RootCAType", this.rootCAType);
			info.AddValue("Services", this.services);
			info.AddValue("IISServices", this.iisServices);
			info.AddValue("PrivateKeyExportable", this.privateKeyExportable);
			if (this.Identity != null)
			{
				info.AddValue("Identity", this.Identity);
			}
		}

		// Token: 0x17001D6E RID: 7534
		// (get) Token: 0x0600612A RID: 24874 RVA: 0x0019597B File Offset: 0x00193B7B
		public IList<AccessRule> AccessRules
		{
			get
			{
				return TlsCertificateInfo.GetAccessRules(this);
			}
		}

		// Token: 0x17001D6F RID: 7535
		// (get) Token: 0x0600612B RID: 24875 RVA: 0x00195984 File Offset: 0x00193B84
		public IList<SmtpDomainWithSubdomains> CertificateDomains
		{
			get
			{
				IList<string> fqdns = TlsCertificateInfo.GetFQDNs(this);
				List<SmtpDomainWithSubdomains> list = new List<SmtpDomainWithSubdomains>(fqdns.Count);
				foreach (string s in fqdns)
				{
					SmtpDomainWithSubdomains item;
					if (SmtpDomainWithSubdomains.TryParse(s, out item))
					{
						list.Add(item);
					}
				}
				return list.AsReadOnly();
			}
		}

		// Token: 0x17001D70 RID: 7536
		// (get) Token: 0x0600612C RID: 24876 RVA: 0x001959F4 File Offset: 0x00193BF4
		public string CertificateRequest
		{
			get
			{
				return CertificateEnroller.ReadPkcs10Request(this);
			}
		}

		// Token: 0x17001D71 RID: 7537
		// (get) Token: 0x0600612D RID: 24877 RVA: 0x001959FC File Offset: 0x00193BFC
		public List<IisService> IisServices
		{
			get
			{
				return this.iisServices;
			}
		}

		// Token: 0x17001D72 RID: 7538
		// (get) Token: 0x0600612E RID: 24878 RVA: 0x00195A04 File Offset: 0x00193C04
		public bool IsSelfSigned
		{
			get
			{
				this.CheckCertificateChainAndCacheProps();
				return this.selfSigned;
			}
		}

		// Token: 0x17001D73 RID: 7539
		// (get) Token: 0x0600612F RID: 24879 RVA: 0x00195A14 File Offset: 0x00193C14
		public string KeyIdentifier
		{
			get
			{
				if (base.PublicKey == null)
				{
					return string.Empty;
				}
				X509SubjectKeyIdentifierExtension x509SubjectKeyIdentifierExtension = new X509SubjectKeyIdentifierExtension(base.PublicKey, false);
				return x509SubjectKeyIdentifierExtension.SubjectKeyIdentifier;
			}
		}

		// Token: 0x17001D74 RID: 7540
		// (get) Token: 0x06006130 RID: 24880 RVA: 0x00195A42 File Offset: 0x00193C42
		public CertificateAuthorityType RootCAType
		{
			get
			{
				this.CheckCertificateChainAndCacheProps();
				return this.rootCAType;
			}
		}

		// Token: 0x17001D75 RID: 7541
		// (get) Token: 0x06006131 RID: 24881 RVA: 0x00195A50 File Offset: 0x00193C50
		// (set) Token: 0x06006132 RID: 24882 RVA: 0x00195A58 File Offset: 0x00193C58
		public AllowedServices Services
		{
			get
			{
				return this.services;
			}
			internal set
			{
				this.services = value;
			}
		}

		// Token: 0x17001D76 RID: 7542
		// (get) Token: 0x06006133 RID: 24883 RVA: 0x00195A61 File Offset: 0x00193C61
		public CertificateStatus Status
		{
			get
			{
				this.CheckCertificateChainAndCacheProps();
				return this.status;
			}
		}

		// Token: 0x17001D77 RID: 7543
		// (get) Token: 0x06006134 RID: 24884 RVA: 0x00195A70 File Offset: 0x00193C70
		public string SubjectKeyIdentifier
		{
			get
			{
				foreach (X509Extension x509Extension in base.Extensions)
				{
					if (x509Extension is X509SubjectKeyIdentifierExtension)
					{
						X509SubjectKeyIdentifierExtension x509SubjectKeyIdentifierExtension = (X509SubjectKeyIdentifierExtension)x509Extension;
						return x509SubjectKeyIdentifierExtension.SubjectKeyIdentifier;
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x17001D78 RID: 7544
		// (get) Token: 0x06006135 RID: 24885 RVA: 0x00195AB9 File Offset: 0x00193CB9
		public bool PrivateKeyExportable
		{
			get
			{
				this.CheckCertificateChainAndCacheProps();
				return this.privateKeyExportable;
			}
		}

		// Token: 0x17001D79 RID: 7545
		// (get) Token: 0x06006136 RID: 24886 RVA: 0x00195AC7 File Offset: 0x00193CC7
		public int PublicKeySize
		{
			get
			{
				if (base.PublicKey != null && base.PublicKey.Key != null)
				{
					return base.PublicKey.Key.KeySize;
				}
				return 0;
			}
		}

		// Token: 0x17001D7A RID: 7546
		// (get) Token: 0x06006137 RID: 24887 RVA: 0x00195AF0 File Offset: 0x00193CF0
		// (set) Token: 0x06006138 RID: 24888 RVA: 0x00195AF8 File Offset: 0x00193CF8
		public string Identity { get; set; }

		// Token: 0x17001D7B RID: 7547
		// (get) Token: 0x06006139 RID: 24889 RVA: 0x00195B04 File Offset: 0x00193D04
		public string ServicesStringForm
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				if ((this.Services & AllowedServices.IMAP) != AllowedServices.None)
				{
					stringBuilder.Append('I');
				}
				else
				{
					stringBuilder.Append('.');
				}
				if ((this.Services & AllowedServices.POP) != AllowedServices.None)
				{
					stringBuilder.Append('P');
				}
				else
				{
					stringBuilder.Append('.');
				}
				if ((this.Services & AllowedServices.UM) != AllowedServices.None)
				{
					stringBuilder.Append('U');
				}
				else
				{
					stringBuilder.Append('.');
				}
				if ((this.Services & AllowedServices.IIS) != AllowedServices.None)
				{
					stringBuilder.Append('W');
				}
				else
				{
					stringBuilder.Append('.');
				}
				if ((this.Services & AllowedServices.SMTP) != AllowedServices.None)
				{
					stringBuilder.Append('S');
				}
				else
				{
					stringBuilder.Append('.');
				}
				if ((this.Services & AllowedServices.Federation) != AllowedServices.None)
				{
					stringBuilder.Append('F');
				}
				else
				{
					stringBuilder.Append('.');
				}
				if ((this.Services & AllowedServices.UMCallRouter) != AllowedServices.None)
				{
					stringBuilder.Append('C');
				}
				else
				{
					stringBuilder.Append('.');
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600613A RID: 24890 RVA: 0x00195BF4 File Offset: 0x00193DF4
		private static Dictionary<string, CertificateAuthorityType> PhysicalStoreList()
		{
			return new Dictionary<string, CertificateAuthorityType>(4)
			{
				{
					"Root\\.AuthRoot",
					CertificateAuthorityType.ThirdParty
				},
				{
					"Root\\.Default",
					CertificateAuthorityType.Registry
				},
				{
					"Root\\.Enterprise",
					CertificateAuthorityType.Enterprise
				},
				{
					"Root\\.GroupPolicy",
					CertificateAuthorityType.GroupPolicy
				}
			};
		}

		// Token: 0x0600613B RID: 24891 RVA: 0x00195C3C File Offset: 0x00193E3C
		private static CertificateAuthorityType RootSource(string thumbprint)
		{
			X509Store x509Store = null;
			foreach (string text in ExchangeCertificate.physicalStores.Keys)
			{
				try
				{
					x509Store = CertificateStore.Open(StoreType.Physical, text, OpenFlags.OpenExistingOnly);
					X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
					if (x509Certificate2Collection != null && x509Certificate2Collection.Count > 0)
					{
						return ExchangeCertificate.physicalStores[text];
					}
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				finally
				{
					if (x509Store != null)
					{
						x509Store.Close();
					}
					x509Store = null;
				}
			}
			return CertificateAuthorityType.Unknown;
		}

		// Token: 0x0600613C RID: 24892 RVA: 0x00195CF0 File Offset: 0x00193EF0
		internal object[] ExchangeCertificateAsArray()
		{
			object[] array = new object[8];
			this.CheckCertificateChainAndCacheProps();
			array[0] = this.Export(X509ContentType.SerializedCert);
			array[1] = this.selfSigned;
			array[2] = this.status;
			array[3] = this.rootCAType;
			array[4] = this.services;
			array[5] = this.IisServices.ToArray();
			array[6] = this.privateKeyExportable;
			array[7] = this.Identity;
			return array;
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x00195D74 File Offset: 0x00193F74
		private void CheckCertificateChainAndCacheProps()
		{
			if (this.status != CertificateStatus.Unknown)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.CertificateRequest))
			{
				this.status = CertificateStatus.PendingRequest;
				this.selfSigned = false;
				this.rootCAType = CertificateAuthorityType.Unknown;
				return;
			}
			this.privateKeyExportable = TlsCertificateInfo.IsCertificateExportable(this);
			ChainPolicyParameters options = new BaseChainPolicyParameters(ChainPolicyOptions.None);
			ChainMatchIssuer pkixKpServerAuth = AndChainMatchIssuer.PkixKpServerAuth;
			ChainBuildParameter parameter = new ChainBuildParameter(pkixKpServerAuth, TimeSpan.FromSeconds(30.0), false, TimeSpan.Zero);
			using (ChainEngine chainEngine = new ChainEngine())
			{
				using (ChainContext chainContext = chainEngine.Build(this, ChainBuildOptions.CacheEndCert | ChainBuildOptions.RevocationCheckChainExcludeRoot | ChainBuildOptions.RevocationAccumulativeTimeout, parameter))
				{
					if (chainContext == null)
					{
						this.status = CertificateStatus.Unknown;
						this.selfSigned = false;
						this.rootCAType = CertificateAuthorityType.Unknown;
					}
					else
					{
						this.selfSigned = chainContext.IsSelfSigned;
						if (chainContext.Status == TrustStatus.IsUntrustedRoot)
						{
							if (chainContext.IsSelfSigned)
							{
								this.status = CertificateStatus.Valid;
								this.rootCAType = CertificateAuthorityType.None;
							}
							else
							{
								this.status = CertificateStatus.Untrusted;
								this.rootCAType = CertificateAuthorityType.Unknown;
							}
						}
						else
						{
							ChainSummary chainSummary = chainContext.Validate(options);
							ChainValidityStatus chainValidityStatus = chainSummary.Status;
							if (chainValidityStatus <= (ChainValidityStatus)2148081683U)
							{
								if (chainValidityStatus == ChainValidityStatus.Valid)
								{
									this.status = CertificateStatus.Valid;
									goto IL_168;
								}
								switch (chainValidityStatus)
								{
								case (ChainValidityStatus)2148081682U:
								case (ChainValidityStatus)2148081683U:
									break;
								default:
									goto IL_15A;
								}
							}
							else
							{
								if (chainValidityStatus == (ChainValidityStatus)2148204801U)
								{
									this.status = CertificateStatus.DateInvalid;
									goto IL_168;
								}
								switch (chainValidityStatus)
								{
								case (ChainValidityStatus)2148204812U:
									this.status = CertificateStatus.Revoked;
									goto IL_168;
								case (ChainValidityStatus)2148204813U:
									goto IL_15A;
								case (ChainValidityStatus)2148204814U:
									break;
								default:
									goto IL_15A;
								}
							}
							this.status = CertificateStatus.RevocationCheckFailure;
							goto IL_168;
							IL_15A:
							this.status = CertificateStatus.Invalid;
							this.rootCAType = CertificateAuthorityType.Unknown;
							IL_168:
							if (this.status != CertificateStatus.Invalid)
							{
								X509Certificate2 rootCertificate = chainContext.RootCertificate;
								if (rootCertificate == null)
								{
									throw new InvalidOperationException("Root certificate was null!");
								}
								this.rootCAType = ExchangeCertificate.RootSource(rootCertificate.Thumbprint);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600613E RID: 24894 RVA: 0x00195F6C File Offset: 0x0019416C
		private CspKeyContainerInfo GetKeyContainer()
		{
			if (!base.HasPrivateKey)
			{
				return null;
			}
			AsymmetricAlgorithm asymmetricAlgorithm = null;
			try
			{
				asymmetricAlgorithm = base.PrivateKey;
			}
			catch (CryptographicException)
			{
				return null;
			}
			if (asymmetricAlgorithm == null)
			{
				return null;
			}
			ICspAsymmetricAlgorithm cspAsymmetricAlgorithm = asymmetricAlgorithm as ICspAsymmetricAlgorithm;
			if (cspAsymmetricAlgorithm == null)
			{
				return null;
			}
			return cspAsymmetricAlgorithm.CspKeyContainerInfo;
		}

		// Token: 0x0400358C RID: 13708
		internal const string Noun = "ExchangeCertificate";

		// Token: 0x0400358D RID: 13709
		private static readonly Dictionary<string, CertificateAuthorityType> physicalStores = ExchangeCertificate.PhysicalStoreList();

		// Token: 0x0400358E RID: 13710
		private bool selfSigned;

		// Token: 0x0400358F RID: 13711
		private bool privateKeyExportable;

		// Token: 0x04003590 RID: 13712
		private CertificateStatus status;

		// Token: 0x04003591 RID: 13713
		private CertificateAuthorityType rootCAType;

		// Token: 0x04003592 RID: 13714
		private AllowedServices services;

		// Token: 0x04003593 RID: 13715
		private List<IisService> iisServices = new List<IisService>();

		// Token: 0x02000AC0 RID: 2752
		private static class SerializationMemberNames
		{
			// Token: 0x04003595 RID: 13717
			public const string CertData = "CertData";

			// Token: 0x04003596 RID: 13718
			public const string SelfSigned = "SelfSigned";

			// Token: 0x04003597 RID: 13719
			public const string Status = "Status";

			// Token: 0x04003598 RID: 13720
			public const string RootCAType = "RootCAType";

			// Token: 0x04003599 RID: 13721
			public const string Services = "Services";

			// Token: 0x0400359A RID: 13722
			public const string IISServices = "IISServices";

			// Token: 0x0400359B RID: 13723
			public const string PrivateKeyExportable = "PrivateKeyExportable";

			// Token: 0x0400359C RID: 13724
			public const string Identity = "Identity";
		}
	}
}

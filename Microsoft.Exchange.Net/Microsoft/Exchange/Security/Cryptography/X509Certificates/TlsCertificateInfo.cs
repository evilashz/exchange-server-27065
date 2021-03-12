using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AE5 RID: 2789
	internal static class TlsCertificateInfo
	{
		// Token: 0x06003BE2 RID: 15330 RVA: 0x0009A3AC File Offset: 0x000985AC
		public static X509Certificate2 FindCertificate(string name, bool wildcardAllowed)
		{
			return TlsCertificateInfo.FindCertificate(new string[]
			{
				name
			}, wildcardAllowed);
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x0009A3CC File Offset: 0x000985CC
		public static X509Certificate2 FindCertificate(IEnumerable<string> names, bool wildcardAllowed)
		{
			CertificateSelectionOption options = wildcardAllowed ? CertificateSelectionOption.WildcardAllowed : CertificateSelectionOption.None;
			return TlsCertificateInfo.FindCertificate(names, options);
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x0009A3E8 File Offset: 0x000985E8
		public static X509Certificate2 FindCertificate(string name, CertificateSelectionOption options)
		{
			return TlsCertificateInfo.FindCertificate(new string[]
			{
				name
			}, options);
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x0009A408 File Offset: 0x00098608
		public static X509Certificate2 FindCertificate(X509Store store, X509FindType type, object findValue)
		{
			X509Certificate2Collection x509Certificate2Collection = store.Certificates.Find(type, findValue, false);
			if (x509Certificate2Collection.Count <= 0)
			{
				return null;
			}
			if (!TlsCertificateInfo.TlsCertSearcher.IsValidServerKey(x509Certificate2Collection[0]))
			{
				return null;
			}
			return x509Certificate2Collection[0];
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x0009A448 File Offset: 0x00098648
		public static X509Certificate2 FindCertificate(IEnumerable<string> names, CertificateSelectionOption options)
		{
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			X509Certificate2 result;
			using (ChainEngine chainEngine = new ChainEngine())
			{
				x509Store.Open(OpenFlags.ReadOnly);
				try
				{
					result = TlsCertificateInfo.FindCertificate(x509Store, names, options, chainEngine);
				}
				finally
				{
					x509Store.Close();
				}
			}
			return result;
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x0009A4A4 File Offset: 0x000986A4
		public static X509Certificate2 FindCertificate(X509Store store, IEnumerable<string> names, CertificateSelectionOption options, ChainEngine engine)
		{
			return TlsCertificateInfo.FindCertificate(store, names, options, WildcardMatchType.MultiLevel, engine);
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x0009A4B0 File Offset: 0x000986B0
		public static X509Certificate2 FindCertificate(X509Store store, IEnumerable<string> names, CertificateSelectionOption options, WildcardMatchType wildcardMatchType, ChainEngine engine)
		{
			ExTraceGlobals.CertificateTracer.Information((long)names.GetHashCode(), "Finding an appropriate TLS certificate.");
			return new TlsCertificateInfo.TlsCertSearcher(store, engine)
			{
				Options = options,
				WildCardMatchType = wildcardMatchType
			}.Search(names);
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x0009A4F4 File Offset: 0x000986F4
		public static X509Certificate2 FindCertificate(X509Store store, IEnumerable<string> names, CertificateSelectionOption options, WildcardMatchType wildcardMatchType, ChainEngine engine, TlsCertificateInfo.FilterCert filterCert, string subject, string issuer)
		{
			ExTraceGlobals.CertificateTracer.Information((long)names.GetHashCode(), "Finding an appropriate TLS certificate.");
			return new TlsCertificateInfo.TlsCertSearcher(store, engine)
			{
				Options = options,
				WildCardMatchType = wildcardMatchType
			}.Search(names, filterCert, subject, issuer);
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x0009A53B File Offset: 0x0009873B
		internal static X509Certificate2 FindFirstCertWithSubjectDistinguishedName(string certificateSubject)
		{
			return TlsCertificateInfo.FindFirstCertWithSubjectDistinguishedName(certificateSubject, true);
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x0009A544 File Offset: 0x00098744
		internal static X509Certificate2 FindFirstCertWithSubjectDistinguishedName(string certificateSubject, bool checkForValid)
		{
			X509Certificate2Collection x509Certificate2Collection = TlsCertificateInfo.FindAllCertWithSubjectDistinguishedName(certificateSubject, checkForValid);
			if (x509Certificate2Collection == null || x509Certificate2Collection.Count == 0)
			{
				throw new ArgumentException(NetException.CertificateSubjectNotFound(certificateSubject), "certificateSubject");
			}
			return x509Certificate2Collection[0];
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x0009A584 File Offset: 0x00098784
		internal static X509Certificate2 FindCertByThumbprint(string certificateThumbprint)
		{
			X509Certificate2Collection x509Certificate2Collection = null;
			X509Store x509Store = null;
			if (string.IsNullOrEmpty(certificateThumbprint))
			{
				throw new ArgumentException(NetException.EmptyCertThumbprint, "certificateThumbprint");
			}
			try
			{
				x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.OpenExistingOnly);
				x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, true);
				if (x509Certificate2Collection == null || x509Certificate2Collection.Count == 0)
				{
					throw new ArgumentException(NetException.CertificateThumbprintNotFound(certificateThumbprint), "certificateThumbprint");
				}
			}
			finally
			{
				if (x509Store != null)
				{
					x509Store.Close();
				}
			}
			return x509Certificate2Collection[0];
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x0009A614 File Offset: 0x00098814
		internal static bool TryFindFirstCertWithSubjectAndIssuerDistinguishedName(string certificateSubject, string certificateIssuer, out X509Certificate2 tlsCertificate)
		{
			return TlsCertificateInfo.TryFindFirstCertWithSubjectAndIssuerDistinguishedName(certificateSubject, certificateIssuer, true, out tlsCertificate);
		}

		// Token: 0x06003BEE RID: 15342 RVA: 0x0009A620 File Offset: 0x00098820
		internal static bool TryFindFirstCertWithSubjectAndIssuerDistinguishedName(string certificateSubject, string certificateIssuer, bool checkForValid, out X509Certificate2 tlsCertificate)
		{
			X509Certificate2Collection x509Certificate2Collection = TlsCertificateInfo.FindAllCertWithSubjectDistinguishedName(certificateSubject, checkForValid);
			tlsCertificate = null;
			bool result = false;
			if (x509Certificate2Collection != null && x509Certificate2Collection.Count > 0)
			{
				if (!string.IsNullOrEmpty(certificateIssuer))
				{
					foreach (X509Certificate2 x509Certificate in x509Certificate2Collection)
					{
						if (certificateIssuer.Equals(x509Certificate.IssuerName.Name, StringComparison.OrdinalIgnoreCase))
						{
							result = true;
							tlsCertificate = x509Certificate;
							break;
						}
					}
				}
				else
				{
					tlsCertificate = x509Certificate2Collection[0];
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x0009A68F File Offset: 0x0009888F
		internal static X509Certificate2Collection FindAllCertWithSubjectDistinguishedName(string certificateSubject)
		{
			return TlsCertificateInfo.FindAllCertWithSubjectDistinguishedName(certificateSubject, true);
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x0009A698 File Offset: 0x00098898
		internal static X509Certificate2Collection FindAllCertWithSubjectDistinguishedName(string certificateSubject, bool checkForValid)
		{
			X509Certificate2Collection result = null;
			X509Store x509Store = null;
			if (string.IsNullOrEmpty(certificateSubject))
			{
				throw new ArgumentException(NetException.EmptyCertSubject, "certificateSubject");
			}
			try
			{
				x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.OpenExistingOnly);
				result = x509Store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, certificateSubject, checkForValid);
			}
			finally
			{
				if (x509Store != null)
				{
					x509Store.Close();
				}
			}
			return result;
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x0009A704 File Offset: 0x00098904
		public static IEnumerable<X509Certificate2> FindAll(X509Store store, IEnumerable<string> names, CertificateSelectionOption options, ChainEngine engine, out X509Certificate2 best)
		{
			return new TlsCertificateInfo.TlsCertSearcher(store, engine)
			{
				Options = options
			}.FindAll(names, out best);
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x0009A729 File Offset: 0x00098929
		public static List<X509Certificate2> EmptyFilterCert(string subject, string issuer, List<X509Certificate2> certList)
		{
			return certList;
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x0009A72C File Offset: 0x0009892C
		public static List<X509Certificate2> FilterCertBySubjectAndIssuerExceptSubjectCN(string subject, string issuer, List<X509Certificate2> certList)
		{
			List<X509Certificate2> list = new List<X509Certificate2>();
			foreach (X509Certificate2 x509Certificate in certList)
			{
				if (TlsCertificateInfo.SubjectMatichingExceptCN(x509Certificate.Subject, subject) && (string.IsNullOrEmpty(issuer) || string.Equals(x509Certificate.Issuer, issuer, StringComparison.OrdinalIgnoreCase)))
				{
					list.Add(x509Certificate);
				}
			}
			return list;
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x0009A7A8 File Offset: 0x000989A8
		public static X509Certificate2 CreateSelfSignCertificate(IEnumerable<string> subjectAlternativeNames, TimeSpan validFor, CertificateCreationOption options)
		{
			return TlsCertificateInfo.CreateSelfSignCertificate(subjectAlternativeNames, validFor, options, 2048);
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x0009A7B7 File Offset: 0x000989B7
		public static X509Certificate2 CreateSelfSignCertificate(IEnumerable<string> subjectAlternativeNames, TimeSpan validFor, CertificateCreationOption options, int keySizeRequested)
		{
			return TlsCertificateInfo.CreateSelfSignCertificate(null, subjectAlternativeNames, validFor, options, keySizeRequested, null);
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x0009A7C4 File Offset: 0x000989C4
		public static bool IsValidKeySize(int keySize)
		{
			return keySize == 2048 || keySize == 4096 || keySize == 1024;
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x0009A7E0 File Offset: 0x000989E0
		public static X509Certificate2 CreateSelfSignCertificate(X500DistinguishedName subject, IEnumerable<string> subjectAlternativeNames, TimeSpan validFor, CertificateCreationOption options, int keySizeRequested, string friendlyName, bool ephemeral)
		{
			return TlsCertificateInfo.CreateSelfSignCertificate(subject, subjectAlternativeNames, validFor, options, keySizeRequested, friendlyName, ephemeral, null);
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x0009A7F4 File Offset: 0x000989F4
		public static X509Certificate2 CreateSelfSignCertificate(X500DistinguishedName subject, IEnumerable<string> subjectAlternativeNames, TimeSpan validFor, CertificateCreationOption options, int keySizeRequested, string friendlyName, bool ephemeral, string subjectKeyIdentifier)
		{
			Oid signatureAlgorithm = ((options & CertificateCreationOption.DSSProvider) == CertificateCreationOption.None) ? WellKnownOid.Sha256Rsa : WellKnownOid.X957Sha1Dsa;
			return TlsCertificateInfo.CreateSelfSignCertificate(subject, subjectAlternativeNames, validFor, options, keySizeRequested, friendlyName, ephemeral, subjectKeyIdentifier, signatureAlgorithm);
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x0009A82C File Offset: 0x00098A2C
		public static X509Certificate2 CreateSelfSignCertificate(X500DistinguishedName subject, IEnumerable<string> subjectAlternativeNames, TimeSpan validFor, CertificateCreationOption options, int keySizeRequested, string friendlyName, bool ephemeral, string subjectKeyIdentifier, Oid signatureAlgorithm)
		{
			if (!TlsCertificateInfo.IsValidKeySize(keySizeRequested))
			{
				throw new ArgumentException(string.Format("Invalid TLS Certificate Key Size ({0}).", keySizeRequested), "keySizeRequested");
			}
			if ((options & CertificateCreationOption.RSAProvider) != CertificateCreationOption.None && (options & CertificateCreationOption.DSSProvider) != CertificateCreationOption.None)
			{
				throw new ArgumentException(NetException.DSSAndRSA, "options");
			}
			if ((options & CertificateCreationOption.Exportable) != CertificateCreationOption.None && (options & CertificateCreationOption.Archivable) != CertificateCreationOption.None)
			{
				throw new ArgumentException(NetException.ExportAndArchive, "options");
			}
			int num = 0;
			if (subjectAlternativeNames != null)
			{
				foreach (string name in subjectAlternativeNames)
				{
					if (!TlsCertificateInfo.IsValidDnsName(name))
					{
						throw new ArgumentException(NetException.InvalidFQDN(name), "subjectAlternativeNames");
					}
					num++;
				}
			}
			if (num == 0)
			{
				if (subject == null)
				{
					throw new ArgumentException(NetException.EmptyFQDNList, "subjectAlternativeNames");
				}
				subjectAlternativeNames = null;
			}
			if (validFor <= TimeSpan.Zero)
			{
				throw new ArgumentException(NetException.InvalidDuration, "validFor");
			}
			X500DistinguishedName x500DistinguishedName = subject ?? TlsCertificateInfo.GetDefaultSubjectName(subjectAlternativeNames);
			if (x500DistinguishedName == null)
			{
				x500DistinguishedName = new X500DistinguishedName("cn=" + Environment.MachineName, X500DistinguishedNameFlags.UseUTF8Encoding);
			}
			CapiNativeMethods.CryptKeyProvInfo keyProvInfo = default(CapiNativeMethods.CryptKeyProvInfo);
			bool flag = (options & CertificateCreationOption.DSSProvider) == CertificateCreationOption.None;
			if (flag)
			{
				keyProvInfo.ProviderType = CapiNativeMethods.ProviderType.RsaSChannel;
				keyProvInfo.KeySpec = CapiNativeMethods.KeySpec.KeyExchange;
			}
			else
			{
				keyProvInfo.ProviderType = CapiNativeMethods.ProviderType.DssDiffieHellman;
				keyProvInfo.KeySpec = CapiNativeMethods.KeySpec.Signature;
			}
			keyProvInfo.Flags = CapiNativeMethods.AcquireContext.MachineKeyset;
			SafeCryptProvHandle invalidHandle = SafeCryptProvHandle.InvalidHandle;
			for (;;)
			{
				keyProvInfo.ContainerName = Guid.NewGuid().ToString();
				if (CapiNativeMethods.CryptAcquireContext(out invalidHandle, keyProvInfo.ContainerName, keyProvInfo.ProviderName, keyProvInfo.ProviderType, keyProvInfo.Flags | CapiNativeMethods.AcquireContext.Silent))
				{
					invalidHandle.Close();
				}
				else
				{
					if (!CapiNativeMethods.CryptAcquireContext(out invalidHandle, keyProvInfo.ContainerName, keyProvInfo.ProviderName, keyProvInfo.ProviderType, keyProvInfo.Flags | CapiNativeMethods.AcquireContext.NewKeyset | CapiNativeMethods.AcquireContext.Silent))
					{
						break;
					}
					SafeCryptProvHandle invalidHandle2 = SafeCryptProvHandle.InvalidHandle;
					CapiNativeMethods.KeySpec algid = flag ? CapiNativeMethods.KeySpec.KeyExchange : CapiNativeMethods.KeySpec.Signature;
					uint num2 = (uint)(flag ? (keySizeRequested << 16) : 67108864);
					if ((options & CertificateCreationOption.Exportable) == CertificateCreationOption.Exportable)
					{
						num2 |= 1U;
					}
					if ((options & CertificateCreationOption.Archivable) == CertificateCreationOption.Archivable)
					{
						num2 |= 16384U;
					}
					if (!CapiNativeMethods.CryptGenKey(invalidHandle, algid, num2, ref invalidHandle2))
					{
						goto Block_20;
					}
					invalidHandle2.Close();
				}
				if (!invalidHandle.IsInvalid)
				{
					goto Block_21;
				}
			}
			throw new CryptographicException(Marshal.GetLastWin32Error());
			Block_20:
			throw new CryptographicException(Marshal.GetLastWin32Error());
			Block_21:
			DateTime utcNow = DateTime.UtcNow;
			DateTime toTime = utcNow + validFor;
			X509ExtensionCollection x509ExtensionCollection = new X509ExtensionCollection();
			X509KeyUsageExtension extension = flag ? new X509KeyUsageExtension(X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, true) : new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, true);
			x509ExtensionCollection.Add(extension);
			if (subjectAlternativeNames != null)
			{
				X509SubjectAltNameExtension extension2 = new X509SubjectAltNameExtension(subjectAlternativeNames, false);
				x509ExtensionCollection.Add(extension2);
			}
			X509EnhancedKeyUsageExtension extension3 = new X509EnhancedKeyUsageExtension(new OidCollection
			{
				WellKnownOid.PkixKpServerAuth
			}, false);
			x509ExtensionCollection.Add(extension3);
			X509BasicConstraintsExtension extension4 = new X509BasicConstraintsExtension(false, false, 0, true);
			x509ExtensionCollection.Add(extension4);
			if (!string.IsNullOrEmpty(subjectKeyIdentifier))
			{
				X509SubjectKeyIdentifierExtension extension5 = new X509SubjectKeyIdentifierExtension(subjectKeyIdentifier, false);
				x509ExtensionCollection.Add(extension5);
			}
			X509Certificate2 x509Certificate = CapiNativeMethods.CreateSelfSignCertificate(invalidHandle, x500DistinguishedName, 0U, keyProvInfo, signatureAlgorithm.Value, utcNow, toTime, x509ExtensionCollection, friendlyName ?? "Microsoft Exchange");
			if (!ephemeral)
			{
				X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.ReadWrite);
				x509Store.Add(x509Certificate);
				x509Store.Close();
			}
			return x509Certificate;
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x0009AB98 File Offset: 0x00098D98
		public static bool TryInstallCertificateInTrustedRootCA(X509Certificate2 certificate)
		{
			X509Store x509Store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.ReadWrite);
			bool result;
			try
			{
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, certificate.Thumbprint, false);
				if (x509Certificate2Collection.Count == 0)
				{
					x509Store.Add(certificate);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			finally
			{
				x509Store.Close();
			}
			return result;
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x0009ABF8 File Offset: 0x00098DF8
		public static bool IsSelfSignedCertificate(X509Certificate2 cert)
		{
			bool result;
			using (ChainEnginePool chainEnginePool = new ChainEnginePool())
			{
				using (ChainEngine engine = chainEnginePool.GetEngine())
				{
					ChainBuildParameter parameter = new ChainBuildParameter(AndChainMatchIssuer.PkixKpServerAuth, TimeSpan.FromSeconds(10.0), false, TimeSpan.Zero);
					ChainContext chainContext2;
					ChainContext chainContext = chainContext2 = engine.Build(cert, ChainBuildOptions.CacheEndCert | ChainBuildOptions.RevocationCheckChainExcludeRoot | ChainBuildOptions.RevocationAccumulativeTimeout, parameter);
					try
					{
						if (chainContext != null)
						{
							result = chainContext.IsSelfSigned;
						}
						else
						{
							ExTraceGlobals.CertificateTracer.TraceError(0L, "IsSelfSignedCertificate: ChainContext was null.");
							result = false;
						}
					}
					finally
					{
						if (chainContext2 != null)
						{
							((IDisposable)chainContext2).Dispose();
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x0009ACB0 File Offset: 0x00098EB0
		public static X509Certificate2 CreateSelfSignCertificate(X500DistinguishedName subject, IEnumerable<string> subjectAlternativeNames, TimeSpan validFor, CertificateCreationOption options, int keySizeRequested, string friendlyName)
		{
			Oid signatureAlgorithm = ((options & CertificateCreationOption.DSSProvider) == CertificateCreationOption.None) ? WellKnownOid.RsaSha1Rsa : WellKnownOid.X957Sha1Dsa;
			return TlsCertificateInfo.CreateSelfSignCertificate(subject, subjectAlternativeNames, validFor, options, keySizeRequested, friendlyName, false, null, signatureAlgorithm);
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x0009ACE4 File Offset: 0x00098EE4
		public static bool IsCNGProvider(X509Certificate2 certificate)
		{
			CapiNativeMethods.CryptKeyProvInfo cryptKeyProvInfo;
			if (!CapiNativeMethods.GetPrivateKeyInfo(certificate, out cryptKeyProvInfo))
			{
				throw new ArgumentException("certificate");
			}
			return cryptKeyProvInfo.ProviderType == CapiNativeMethods.ProviderType.CNG;
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x0009AD10 File Offset: 0x00098F10
		public static void GetProviderInfo(X509Certificate2 certificate, out string providerName, out string containerName)
		{
			CapiNativeMethods.CryptKeyProvInfo cryptKeyProvInfo;
			if (!CapiNativeMethods.GetPrivateKeyInfo(certificate, out cryptKeyProvInfo))
			{
				throw new ArgumentException("certificate");
			}
			containerName = cryptKeyProvInfo.ContainerName;
			providerName = cryptKeyProvInfo.ProviderName;
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x0009AD44 File Offset: 0x00098F44
		public static string GetKeyAlgorithmName(X509Certificate2 certificate)
		{
			return CapiNativeMethods.GetKeyAlgorithmName(certificate);
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x0009AD4C File Offset: 0x00098F4C
		public static void AddAccessRule(X509Certificate2 certificate, AccessRule rule)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			CapiNativeMethods.CryptKeyProvInfo providerInfo;
			if (!CapiNativeMethods.GetPrivateKeyInfo(certificate, out providerInfo))
			{
				throw new ArgumentException("certificate");
			}
			bool flag;
			if (providerInfo.ProviderType != CapiNativeMethods.ProviderType.CNG)
			{
				flag = TlsCertificateInfo.CAPIAddAccessRule(certificate, rule);
			}
			else
			{
				flag = TlsCertificateInfo.CNGAddAccessRule(certificate, providerInfo, rule);
			}
			if (flag)
			{
				X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.ReadWrite);
				try
				{
					X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, certificate.Thumbprint, false);
					if (x509Certificate2Collection.Count > 0)
					{
						X509Certificate2 x509Certificate = x509Certificate2Collection[0];
						try
						{
							x509Certificate.FriendlyName = x509Certificate.FriendlyName;
						}
						catch (CryptographicException)
						{
						}
					}
				}
				finally
				{
					x509Store.Close();
				}
			}
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x0009AE0C File Offset: 0x0009900C
		private static bool CAPIAddAccessRule(X509Certificate2 certificate, AccessRule rule)
		{
			AsymmetricAlgorithm privateKey;
			try
			{
				privateKey = certificate.PrivateKey;
			}
			catch (CryptographicException inner)
			{
				throw new UnauthorizedAccessException("Certificate", inner);
			}
			if (privateKey == null)
			{
				throw new ArgumentException("Private Key invalid", "Certificate");
			}
			ICspAsymmetricAlgorithm cspAsymmetricAlgorithm = privateKey as ICspAsymmetricAlgorithm;
			if (cspAsymmetricAlgorithm == null)
			{
				throw new ArgumentException("Wrong private key type", "Certificate");
			}
			CspKeyContainerInfo cspKeyContainerInfo = cspAsymmetricAlgorithm.CspKeyContainerInfo;
			CryptoKeySecurity cryptoKeySecurity = cspKeyContainerInfo.CryptoKeySecurity;
			bool flag = false;
			if (!cryptoKeySecurity.ModifyAccessRule(AccessControlModification.Add, rule, out flag))
			{
				throw new UnauthorizedAccessException(SystemStrings.FailedToAddAccessRule);
			}
			if (flag)
			{
				SafeCryptProvHandle safeCryptProvHandle;
				if (!CapiNativeMethods.CryptAcquireContext(out safeCryptProvHandle, cspKeyContainerInfo.KeyContainerName, cspKeyContainerInfo.ProviderName, (CapiNativeMethods.ProviderType)cspKeyContainerInfo.ProviderType, cspKeyContainerInfo.MachineKeyStore ? CapiNativeMethods.AcquireContext.MachineKeyset : ((CapiNativeMethods.AcquireContext)0U)))
				{
					CryptographicException ex = new CryptographicException(Marshal.GetLastWin32Error());
					throw ex;
				}
				using (safeCryptProvHandle)
				{
					byte[] securityDescriptorBinaryForm = cryptoKeySecurity.GetSecurityDescriptorBinaryForm();
					if (!CapiNativeMethods.CryptSetProvParam(safeCryptProvHandle, CapiNativeMethods.SetProvParam.KeySetSecurityDescriptor, securityDescriptorBinaryForm, 4U))
					{
						CryptographicException ex2 = new CryptographicException(Marshal.GetLastWin32Error());
						throw ex2;
					}
				}
			}
			return flag;
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x0009AF1C File Offset: 0x0009911C
		private static bool CNGAddAccessRule(X509Certificate2 certificate, CapiNativeMethods.CryptKeyProvInfo providerInfo, AccessRule rule)
		{
			SafeNCryptHandle safeNCryptHandle;
			int num = CngNativeMethods.NCryptOpenStorageProvider(out safeNCryptHandle, providerInfo.ProviderName, 0U);
			if (num != 0)
			{
				throw new CryptographicException(num);
			}
			using (safeNCryptHandle)
			{
				CngNativeMethods.KeyOptions options = CngNativeMethods.KeyOptions.MachineKeyset;
				SafeNCryptHandle safeNCryptHandle3;
				num = CngNativeMethods.NCryptOpenKey(safeNCryptHandle, out safeNCryptHandle3, providerInfo.ContainerName, 0U, options);
				if (num != 0)
				{
					throw new CryptographicException(num);
				}
				using (safeNCryptHandle3)
				{
					byte[] array = null;
					uint valueSize = 0U;
					uint num2;
					num = CngNativeMethods.NCryptGetProperty(safeNCryptHandle3, "Security Descr", array, valueSize, out num2, CngNativeMethods.PropertyOptions.DACLSecurityInformation);
					if (num != 0)
					{
						throw new CryptographicException(num);
					}
					array = new byte[num2];
					valueSize = num2;
					num = CngNativeMethods.NCryptGetProperty(safeNCryptHandle3, "Security Descr", array, valueSize, out num2, CngNativeMethods.PropertyOptions.DACLSecurityInformation);
					if (num != 0)
					{
						throw new CryptographicException(num);
					}
					CryptoKeySecurity cryptoKeySecurity = new CryptoKeySecurity();
					cryptoKeySecurity.SetSecurityDescriptorBinaryForm(array);
					bool flag = false;
					if (!cryptoKeySecurity.ModifyAccessRule(AccessControlModification.Add, rule, out flag))
					{
						throw new UnauthorizedAccessException(SystemStrings.FailedToAddAccessRule);
					}
					if (!flag)
					{
						return false;
					}
					array = cryptoKeySecurity.GetSecurityDescriptorBinaryForm();
					num = CngNativeMethods.NCryptSetProperty(safeNCryptHandle3, "Security Descr", array, (uint)array.Length, CngNativeMethods.PropertyOptions.DACLSecurityInformation);
					if (num != 0)
					{
						throw new CryptographicException(num);
					}
				}
			}
			return true;
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x0009B050 File Offset: 0x00099250
		public static IList<AccessRule> GetAccessRules(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			CapiNativeMethods.CryptKeyProvInfo cryptKeyProvInfo;
			if (!CapiNativeMethods.GetPrivateKeyInfo(certificate, out cryptKeyProvInfo))
			{
				return null;
			}
			CryptoKeySecurity cryptoKeySecurity = null;
			if (cryptKeyProvInfo.ProviderType != CapiNativeMethods.ProviderType.CNG)
			{
				AsymmetricAlgorithm asymmetricAlgorithm = null;
				try
				{
					asymmetricAlgorithm = certificate.PrivateKey;
				}
				catch (CryptographicException)
				{
					return null;
				}
				if (asymmetricAlgorithm != null)
				{
					ICspAsymmetricAlgorithm cspAsymmetricAlgorithm = asymmetricAlgorithm as ICspAsymmetricAlgorithm;
					if (cspAsymmetricAlgorithm != null)
					{
						CspKeyContainerInfo cspKeyContainerInfo = cspAsymmetricAlgorithm.CspKeyContainerInfo;
						if (cspKeyContainerInfo != null)
						{
							cryptoKeySecurity = cspKeyContainerInfo.CryptoKeySecurity;
						}
					}
				}
			}
			else
			{
				SafeNCryptHandle safeNCryptHandle;
				int num = CngNativeMethods.NCryptOpenStorageProvider(out safeNCryptHandle, cryptKeyProvInfo.ProviderName, 0U);
				if (num != 0)
				{
					return null;
				}
				using (safeNCryptHandle)
				{
					CngNativeMethods.KeyOptions options = CngNativeMethods.KeyOptions.MachineKeyset;
					SafeNCryptHandle safeNCryptHandle3;
					num = CngNativeMethods.NCryptOpenKey(safeNCryptHandle, out safeNCryptHandle3, cryptKeyProvInfo.ContainerName, 0U, options);
					if (num != 0)
					{
						return null;
					}
					using (safeNCryptHandle3)
					{
						byte[] array = null;
						uint valueSize = 0U;
						uint num2;
						num = CngNativeMethods.NCryptGetProperty(safeNCryptHandle3, "Security Descr", array, valueSize, out num2, CngNativeMethods.PropertyOptions.DACLSecurityInformation);
						if (num != 0)
						{
							return null;
						}
						array = new byte[num2];
						valueSize = num2;
						num = CngNativeMethods.NCryptGetProperty(safeNCryptHandle3, "Security Descr", array, valueSize, out num2, CngNativeMethods.PropertyOptions.DACLSecurityInformation);
						if (num != 0)
						{
							return null;
						}
						cryptoKeySecurity = new CryptoKeySecurity();
						cryptoKeySecurity.SetSecurityDescriptorBinaryForm(array);
					}
				}
			}
			if (cryptoKeySecurity == null)
			{
				return null;
			}
			List<AccessRule> list = new List<AccessRule>();
			AuthorizationRuleCollection accessRules = cryptoKeySecurity.GetAccessRules(true, true, typeof(NTAccount));
			foreach (object obj in accessRules)
			{
				AuthorizationRule authorizationRule = (AuthorizationRule)obj;
				AccessRule item = authorizationRule as AccessRule;
				list.Add(item);
			}
			return list.AsReadOnly();
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x0009B234 File Offset: 0x00099434
		public static bool IsCertificateExportable(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			CapiNativeMethods.CryptKeyProvInfo cryptKeyProvInfo;
			if (!CapiNativeMethods.GetPrivateKeyInfo(certificate, out cryptKeyProvInfo))
			{
				return false;
			}
			if (cryptKeyProvInfo.ProviderType != CapiNativeMethods.ProviderType.CNG)
			{
				AsymmetricAlgorithm asymmetricAlgorithm = null;
				try
				{
					asymmetricAlgorithm = certificate.PrivateKey;
				}
				catch (CryptographicException)
				{
					return false;
				}
				if (asymmetricAlgorithm != null)
				{
					ICspAsymmetricAlgorithm cspAsymmetricAlgorithm = asymmetricAlgorithm as ICspAsymmetricAlgorithm;
					if (cspAsymmetricAlgorithm != null)
					{
						CspKeyContainerInfo cspKeyContainerInfo = cspAsymmetricAlgorithm.CspKeyContainerInfo;
						return cspKeyContainerInfo != null && cspKeyContainerInfo.Exportable;
					}
				}
			}
			else
			{
				SafeNCryptHandle safeNCryptHandle;
				int num = CngNativeMethods.NCryptOpenStorageProvider(out safeNCryptHandle, cryptKeyProvInfo.ProviderName, 0U);
				if (num != 0)
				{
					return false;
				}
				using (safeNCryptHandle)
				{
					CngNativeMethods.KeyOptions options = CngNativeMethods.KeyOptions.MachineKeyset;
					SafeNCryptHandle safeNCryptHandle3;
					num = CngNativeMethods.NCryptOpenKey(safeNCryptHandle, out safeNCryptHandle3, cryptKeyProvInfo.ContainerName, 0U, options);
					if (num != 0)
					{
						return false;
					}
					using (safeNCryptHandle3)
					{
						uint valueSize = (uint)Marshal.SizeOf(typeof(uint));
						uint num2;
						uint num3;
						if (CngNativeMethods.NCryptGetProperty(safeNCryptHandle3, "Export Policy", out num2, valueSize, out num3, (CngNativeMethods.PropertyOptions)0U) == 0)
						{
							CngNativeMethods.AllowExportPolicy allowExportPolicy = (CngNativeMethods.AllowExportPolicy)num2;
							return (allowExportPolicy & (CngNativeMethods.AllowExportPolicy.Exportable | CngNativeMethods.AllowExportPolicy.PlaintextExportable)) != (CngNativeMethods.AllowExportPolicy)0;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x0009B364 File Offset: 0x00099564
		public static IList<string> GetFQDNs(X509Certificate2 certificate)
		{
			int num;
			return TlsCertificateInfo.GetFQDNs(certificate, int.MaxValue, out num);
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x0009B380 File Offset: 0x00099580
		public static IList<string> GetFQDNs(X509Certificate2 certificate, int subjectAlternativeNameLimit, out int subjectAlternativeNameCount)
		{
			subjectAlternativeNameCount = 0;
			if (subjectAlternativeNameLimit < 0)
			{
				throw new ArgumentOutOfRangeException("subjectAlternativeNameLimit", subjectAlternativeNameLimit, "subjectAlternativeNameLimit value cannot be negative");
			}
			List<string> list = new List<string>();
			string certNameInfo = CapiNativeMethods.GetCertNameInfo(certificate, 0U, CapiNativeMethods.CertNameType.Attr);
			if (!string.IsNullOrEmpty(certNameInfo) && TlsCertificateInfo.IsValidDnsName(certNameInfo))
			{
				list.Add(certNameInfo);
			}
			X509ExtensionCollection extensions = certificate.Extensions;
			foreach (X509Extension x509Extension in extensions)
			{
				if (string.Compare(x509Extension.Oid.Value, WellKnownOid.SubjectAltName.Value, StringComparison.OrdinalIgnoreCase) == 0)
				{
					X509SubjectAltNameExtension x509SubjectAltNameExtension = X509SubjectAltNameExtension.Create(x509Extension);
					if (x509SubjectAltNameExtension.DnsNames.Count > subjectAlternativeNameLimit)
					{
						subjectAlternativeNameCount = x509SubjectAltNameExtension.DnsNames.Count;
						ExTraceGlobals.CertificateTracer.TraceError<string, int, int>(0L, "Certificate with thumbprint <{0}> contains {1} subject alternative names and exceeds the limit of {2}; ignoring subject alternative names for this certificate.", certificate.Thumbprint, subjectAlternativeNameCount, subjectAlternativeNameLimit);
						break;
					}
					using (IEnumerator<string> enumerator2 = x509SubjectAltNameExtension.DnsNames.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							string text = enumerator2.Current;
							if (TlsCertificateInfo.IsValidDnsName(text))
							{
								bool flag = false;
								foreach (string a in list)
								{
									if (string.Equals(a, text, StringComparison.OrdinalIgnoreCase))
									{
										flag = true;
										break;
									}
								}
								if (!flag)
								{
									list.Add(text);
									subjectAlternativeNameCount++;
								}
							}
						}
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x0009B504 File Offset: 0x00099704
		internal static X500DistinguishedName GetDefaultSubjectName(IEnumerable<string> subjectAlternativeNames)
		{
			if (subjectAlternativeNames == null)
			{
				return null;
			}
			foreach (string text in subjectAlternativeNames)
			{
				if (text.Length < 64)
				{
					return new X500DistinguishedName("cn=" + text, X500DistinguishedNameFlags.UseUTF8Encoding);
				}
			}
			return null;
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x0009B570 File Offset: 0x00099770
		private static bool IsValidDnsName(string name)
		{
			return Dns.IsValidName(name) || (Dns.IsValidWildcardName(name) && name.Length > 2);
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x0009B590 File Offset: 0x00099790
		private static bool SubjectMatichingExceptCN(string subject1, string subject2)
		{
			if (string.Equals(subject1, subject2, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (string.IsNullOrEmpty(subject1) || string.IsNullOrEmpty(subject2))
			{
				return false;
			}
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			string[] array = subject1.Split(new string[]
			{
				","
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				if (!text.Trim().StartsWith("CN", StringComparison.OrdinalIgnoreCase))
				{
					string key = text.Trim().ToLower();
					if (dictionary.ContainsKey(key))
					{
						return false;
					}
					dictionary.Add(key, false);
				}
			}
			string[] array3 = subject2.Split(new string[]
			{
				","
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text2 in array3)
			{
				if (!text2.Trim().StartsWith("CN", StringComparison.OrdinalIgnoreCase))
				{
					string key2 = text2.Trim().ToLower();
					if (!dictionary.ContainsKey(key2))
					{
						return false;
					}
					bool flag = dictionary[key2];
					if (flag)
					{
						return false;
					}
					dictionary[key2] = true;
				}
			}
			return !dictionary.ContainsValue(false);
		}

		// Token: 0x040034B2 RID: 13490
		public const string DefaultFriendlyName = "Microsoft Exchange";

		// Token: 0x02000AE6 RID: 2790
		// (Invoke) Token: 0x06003C0B RID: 15371
		public delegate List<X509Certificate2> FilterCert(string subject, string issuer, List<X509Certificate2> certList);

		// Token: 0x02000AE7 RID: 2791
		private class TlsCertSearcher : CertSearcher
		{
			// Token: 0x06003C0E RID: 15374 RVA: 0x0009B6C6 File Offset: 0x000998C6
			public TlsCertSearcher(X509Store certStore, ChainEngine chainEngine) : base(certStore, chainEngine, TlsCertificateInfo.TlsCertSearcher.usage, TlsCertificateInfo.TlsCertSearcher.defaultSSLChainPolicy)
			{
			}

			// Token: 0x06003C0F RID: 15375 RVA: 0x0009B6DC File Offset: 0x000998DC
			public List<X509Certificate2> FindAll(IEnumerable<string> names, out X509Certificate2 best)
			{
				List<X509Certificate2> list = base.FindAll(names);
				best = base.FindBest(names, list);
				return list;
			}

			// Token: 0x06003C10 RID: 15376 RVA: 0x0009B6FC File Offset: 0x000998FC
			public X509Certificate2 Search(IEnumerable<string> names, TlsCertificateInfo.FilterCert filterCert, string subject, string issuer)
			{
				List<X509Certificate2> certList = base.FindAll(names);
				List<X509Certificate2> certs = filterCert(subject, issuer, certList);
				return base.FindBest(names, certs);
			}

			// Token: 0x06003C11 RID: 15377 RVA: 0x0009B726 File Offset: 0x00099926
			public static bool IsValidServerKey(X509Certificate2 certificate)
			{
				return CertSearcher.IsValidServerKeyUsages(certificate);
			}

			// Token: 0x06003C12 RID: 15378 RVA: 0x0009B72E File Offset: 0x0009992E
			protected override bool IsCertificateValid(X509Certificate2 certificate, IEnumerable<string> names)
			{
				return base.IsFQDNMatch(certificate, names) && certificate.HasPrivateKey && TlsCertificateInfo.TlsCertSearcher.IsValidTLSUsage(certificate);
			}

			// Token: 0x06003C13 RID: 15379 RVA: 0x0009B74C File Offset: 0x0009994C
			private static bool IsValidTLSUsage(X509Certificate2 certificate)
			{
				bool flag = false;
				bool result;
				try
				{
					CapiNativeMethods.AlgorithmId algorithmId = CapiNativeMethods.GetAlgorithmId(certificate);
					CapiNativeMethods.AlgorithmId algorithmId2 = algorithmId;
					if (algorithmId2 != CapiNativeMethods.AlgorithmId.DsaSignature && algorithmId2 != CapiNativeMethods.AlgorithmId.RsaKeyExchange)
					{
						CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "Considering certificate {0}", new object[]
						{
							certificate.Thumbprint
						});
						CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Is neither RSA or DSA, dropping from consideration.", new object[]
						{
							certificate.Thumbprint
						});
						result = false;
					}
					else
					{
						X509KeyUsageFlags intendedKeyUsage;
						try
						{
							intendedKeyUsage = CapiNativeMethods.GetIntendedKeyUsage(certificate);
						}
						catch (CryptographicException ex)
						{
							CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "Considering certificate {0}", new object[]
							{
								certificate.Thumbprint
							});
							CertSearcher.Logger.TraceEvent(TraceEventType.Error, 0, "{0}: Caused a cryptographic exception [{1}] when attempting to determine its usage.", new object[]
							{
								certificate.Thumbprint,
								ex.Message
							});
							return false;
						}
						if (intendedKeyUsage == X509KeyUsageFlags.None)
						{
							flag = true;
						}
						if ((intendedKeyUsage & X509KeyUsageFlags.KeyEncipherment) == X509KeyUsageFlags.KeyEncipherment)
						{
						}
						if ((intendedKeyUsage & (X509KeyUsageFlags.NonRepudiation | X509KeyUsageFlags.DigitalSignature)) != X509KeyUsageFlags.None)
						{
							flag = true;
						}
						if (!flag)
						{
							CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "Considering certificate {0}", new object[]
							{
								certificate.Thumbprint
							});
							CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Is not valid for signing, dropping from consideration.", new object[]
							{
								certificate.Thumbprint
							});
							result = false;
						}
						else
						{
							string[] enhancedKeyUsage;
							try
							{
								enhancedKeyUsage = CapiNativeMethods.GetEnhancedKeyUsage(certificate, CapiNativeMethods.EnhancedKeyUsageSearch.ExtensionAndProperty);
							}
							catch (CryptographicException ex2)
							{
								CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "Considering certificate {0}", new object[]
								{
									certificate.Thumbprint
								});
								CertSearcher.Logger.TraceEvent(TraceEventType.Error, 0, "{0}: Caused a cryptographic exception [{1}] when attempting to determine its enhanced usage.", new object[]
								{
									certificate.Thumbprint,
									ex2.Message
								});
								return false;
							}
							bool flag2 = false;
							if (enhancedKeyUsage != null)
							{
								if (enhancedKeyUsage.Length == 0)
								{
									flag2 = true;
								}
								else
								{
									foreach (string strA in enhancedKeyUsage)
									{
										if (string.CompareOrdinal(strA, WellKnownOid.PkixKpServerAuth.Value) == 0)
										{
											flag2 = true;
											break;
										}
									}
								}
							}
							if (!flag2)
							{
								CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "Considering certificate {0}", new object[]
								{
									certificate.Thumbprint
								});
								CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Has an EKU restriction, dropping from consideration.", new object[]
								{
									certificate.Thumbprint
								});
								result = false;
							}
							else
							{
								result = true;
							}
						}
					}
				}
				catch (CryptographicException ex3)
				{
					CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "Considering certificate {0}", new object[]
					{
						certificate.Thumbprint
					});
					CertSearcher.Logger.TraceEvent(TraceEventType.Error, 0, "{0}: Cryptographic exception [{1}] while analyzing.", new object[]
					{
						certificate.Thumbprint,
						ex3.Message
					});
					result = false;
				}
				return result;
			}

			// Token: 0x040034B3 RID: 13491
			private static readonly ChainMatchIssuer match = AndChainMatchIssuer.PkixKpServerAuth;

			// Token: 0x040034B4 RID: 13492
			private static readonly ChainBuildParameter usage = new ChainBuildParameter(TlsCertificateInfo.TlsCertSearcher.match, TimeSpan.FromSeconds(10.0), false, TimeSpan.Zero);

			// Token: 0x040034B5 RID: 13493
			private static readonly SSLChainPolicyParameters defaultSSLChainPolicy = new SSLChainPolicyParameters(string.Empty, ChainPolicyOptions.None, SSLPolicyAuthorizationOptions.IgnoreCertCNInvalid, SSLPolicyAuthorizationType.Server);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AED RID: 2797
	internal class X509CertificateCollection : X509Certificate2Collection
	{
		// Token: 0x06003C21 RID: 15393 RVA: 0x0009BEDE File Offset: 0x0009A0DE
		public X509CertificateCollection()
		{
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x0009BEE6 File Offset: 0x0009A0E6
		public X509CertificateCollection(X509Certificate2Collection certificates) : base(certificates)
		{
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x0009BEF0 File Offset: 0x0009A0F0
		public static ChainValidityStatus ValidateCertificate(X509Certificate2 certificate, IEnumerable<string> emails, X509KeyUsageFlags expectedUsage, bool checkRevocation, X509Store trustedStore, X509Store chainBuildStore, ref ChainContext context, bool exclusiveTrustMode = false, string uid = null)
		{
			return X509CertificateCollection.ValidateCertificate(certificate, emails, expectedUsage, checkRevocation, trustedStore, chainBuildStore, X509CertificateCollection.DefaultCRLConnectionTimeout, X509CertificateCollection.DefaultCRLRetrievalTimeout, ref context, exclusiveTrustMode, uid);
		}

		// Token: 0x06003C24 RID: 15396 RVA: 0x0009BF1C File Offset: 0x0009A11C
		public static ChainValidityStatus ValidateCertificate(X509Certificate2 certificate, IEnumerable<string> emails, X509KeyUsageFlags expectedUsage, bool checkRevocation, X509Store trustedStore, X509Store chainBuildStore, TimeSpan connectionTimeout, TimeSpan retrievalTimeout, ref ChainContext context, bool exclusiveTrustMode = false, string poolUid = null)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (emails != null && !X509CertificateCollection.IsUserMatch(certificate, emails))
			{
				return ChainValidityStatus.SubjectMismatch;
			}
			if (!X509CertificateCollection.IsValidUsage(certificate, expectedUsage))
			{
				return (ChainValidityStatus)2148204816U;
			}
			ChainValidityStatus result;
			using (ChainEngine chainEngine = X509CertificateCollection.BuildChainEngine(retrievalTimeout, trustedStore, exclusiveTrustMode, poolUid))
			{
				ChainBuildOptions chainBuildOptions = X509CertificateCollection.GetChainBuildOptions(checkRevocation);
				ChainPolicyParameters chainPolicyParameters = X509CertificateCollection.GetChainPolicyParameters(checkRevocation);
				ChainBuildParameter chainBuildParameter = X509CertificateCollection.GetChainBuildParameter(connectionTimeout);
				context = ((chainBuildStore == null) ? chainEngine.Build(certificate, chainBuildOptions, chainBuildParameter) : chainEngine.Build(certificate, chainBuildOptions, chainBuildParameter, chainBuildStore));
				if (context == null)
				{
					result = (ChainValidityStatus)2148204810U;
				}
				else
				{
					ChainSummary chainSummary = context.Validate(chainPolicyParameters);
					result = chainSummary.Status;
				}
			}
			return result;
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x0009BFD8 File Offset: 0x0009A1D8
		public void ImportFromContact(byte[] rawData)
		{
			if (rawData == null)
			{
				throw new ArgumentNullException("rawData");
			}
			int i = 0;
			while (i < rawData.Length)
			{
				if (i > rawData.Length - X509CertificateCollection.fieldSize * 2)
				{
					throw new FormatException("Data stream truncated.");
				}
				X509CertificateCollection.OutlookCertificateTag outlookCertificateTag = (X509CertificateCollection.OutlookCertificateTag)BitConverter.ToUInt16(rawData, i);
				ushort num = BitConverter.ToUInt16(rawData, i + X509CertificateCollection.fieldSize);
				X509CertificateCollection.OutlookCertificateTag outlookCertificateTag2 = outlookCertificateTag;
				switch (outlookCertificateTag2)
				{
				case X509CertificateCollection.OutlookCertificateTag.Version:
				case X509CertificateCollection.OutlookCertificateTag.AsymmetricCapabilities:
				case (X509CertificateCollection.OutlookCertificateTag)5:
				case X509CertificateCollection.OutlookCertificateTag.MessageEncoding:
				case (X509CertificateCollection.OutlookCertificateTag)7:
				case X509CertificateCollection.OutlookCertificateTag.SignatureCertificate:
				case X509CertificateCollection.OutlookCertificateTag.SignSha1Hash:
				case (X509CertificateCollection.OutlookCertificateTag)10:
				case X509CertificateCollection.OutlookCertificateTag.DisplayName:
				case X509CertificateCollection.OutlookCertificateTag.NortelBulkAlgorithm:
				case X509CertificateCollection.OutlookCertificateTag.CertificateTime:
					break;
				case X509CertificateCollection.OutlookCertificateTag.KeyExchangeCertificate:
				{
					int num2 = 2 * X509CertificateCollection.fieldSize;
					int num3 = (int)num - num2;
					if (num3 > 0)
					{
						if (i + num2 > rawData.Length - num3)
						{
							throw new FormatException("Data stream truncated.");
						}
						byte[] array = new byte[num3];
						Array.Copy(rawData, i + num2, array, 0, num3);
						using (SafeCertContextHandle safeCertContextHandle = CapiNativeMethods.CertCreateCertificateContext(CapiNativeMethods.EncodeType.X509Asn, array, array.Length))
						{
							if (safeCertContextHandle == null || safeCertContextHandle.IsInvalid)
							{
								throw new CryptographicException(Marshal.GetLastWin32Error());
							}
							base.Add(new X509Certificate2(safeCertContextHandle.DangerousGetHandle()));
							break;
						}
						goto IL_128;
					}
					break;
				}
				case X509CertificateCollection.OutlookCertificateTag.CertificateChain:
					goto IL_128;
				default:
					switch (outlookCertificateTag2)
					{
					}
					break;
				}
				IL_166:
				i += (int)num;
				continue;
				IL_128:
				int num4 = 2 * Marshal.SizeOf(typeof(ushort));
				int num5 = (int)num - num4;
				if (num5 > 0)
				{
					byte[] array2 = new byte[num5];
					Array.Copy(rawData, i + num4, array2, 0, num5);
					base.Import(array2);
					goto IL_166;
				}
				goto IL_166;
			}
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x0009C168 File Offset: 0x0009A368
		public X509Certificate2 FindSMimeCertificate(IEnumerable<string> emails, X509KeyUsageFlags expectedUsage, bool checkRevocation, X509Store chainCertStore = null, string poolUid = null)
		{
			return this.FindSMimeCertificate(emails, expectedUsage, checkRevocation, X509CertificateCollection.DefaultCRLConnectionTimeout, X509CertificateCollection.DefaultCRLRetrievalTimeout, chainCertStore, poolUid);
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x0009C184 File Offset: 0x0009A384
		public X509Certificate2 FindSMimeCertificate(IEnumerable<string> emails, X509KeyUsageFlags expectedUsage, bool checkRevocation, TimeSpan connectionTimeout, TimeSpan retrievalTimeout, X509Store chainCertStore = null, string poolUid = null)
		{
			X509KeyUsageFlags x509KeyUsageFlags = expectedUsage & (X509KeyUsageFlags.NonRepudiation | X509KeyUsageFlags.DigitalSignature);
			if (x509KeyUsageFlags != X509KeyUsageFlags.None && x509KeyUsageFlags != (X509KeyUsageFlags.NonRepudiation | X509KeyUsageFlags.DigitalSignature))
			{
				throw new ArgumentException("SMIME signature usage flags must be DigitalSignature | NonRepudiation", "expectedUsage");
			}
			List<X509Certificate2> list = new List<X509Certificate2>();
			foreach (X509Certificate2 x509Certificate in this)
			{
				if ((emails == null || X509CertificateCollection.IsUserMatch(x509Certificate, emails)) && X509CertificateCollection.IsValidUsage(x509Certificate, expectedUsage))
				{
					list.Add(x509Certificate);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			list.Sort(new Comparison<X509Certificate2>(CertSearcher.CompareByNotBefore));
			ChainBuildOptions chainBuildOptions = X509CertificateCollection.GetChainBuildOptions(checkRevocation);
			ChainPolicyParameters chainPolicyParameters = X509CertificateCollection.GetChainPolicyParameters(checkRevocation);
			ChainBuildParameter chainBuildParameter = X509CertificateCollection.GetChainBuildParameter(connectionTimeout);
			using (ChainEngine chainEngine = X509CertificateCollection.BuildChainEngine(retrievalTimeout, chainCertStore, chainCertStore != null, poolUid))
			{
				foreach (X509Certificate2 x509Certificate2 in list)
				{
					using (ChainContext chainContext = chainEngine.Build(x509Certificate2, chainBuildOptions, chainBuildParameter))
					{
						if (chainContext != null)
						{
							ChainSummary chainSummary = chainContext.Validate(chainPolicyParameters);
							if (chainSummary.Status == ChainValidityStatus.Valid)
							{
								return x509Certificate2;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x0009C2DC File Offset: 0x0009A4DC
		private static bool IsUserMatch(X509Certificate2 certificate, IEnumerable<string> proxies)
		{
			string certNameInfo;
			try
			{
				certNameInfo = CapiNativeMethods.GetCertNameInfo(certificate, 0U, CapiNativeMethods.CertNameType.Email);
			}
			catch (CryptographicException arg)
			{
				ExTraceGlobals.CertificateTracer.TraceDebug<CryptographicException>(0L, "Failed to get the SubjectAltName of the certificate. Exception: {0}", arg);
				return false;
			}
			if (string.IsNullOrEmpty(certNameInfo))
			{
				return false;
			}
			foreach (string a in proxies)
			{
				if (string.Equals(a, certNameInfo, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x0009C36C File Offset: 0x0009A56C
		private static bool IsValidUsage(X509Certificate2 certificate, X509KeyUsageFlags expectedUsage)
		{
			CapiNativeMethods.AlgorithmId algorithmId = CapiNativeMethods.GetAlgorithmId(certificate);
			bool flag = algorithmId == CapiNativeMethods.AlgorithmId.DiffieHellmanStoreAndForward || algorithmId == CapiNativeMethods.AlgorithmId.DiffieHellmanEphemeral;
			bool flag2 = X509CertificateCollection.IsECCertificate(certificate);
			X509KeyUsageFlags intendedKeyUsage;
			try
			{
				intendedKeyUsage = CapiNativeMethods.GetIntendedKeyUsage(certificate);
			}
			catch (CryptographicException arg)
			{
				ExTraceGlobals.CertificateTracer.TraceDebug<CryptographicException>(0L, "Failed to get the key usage of the certificate. Exception: {0}", arg);
				return false;
			}
			X509KeyUsageFlags x509KeyUsageFlags = expectedUsage & (X509KeyUsageFlags.NonRepudiation | X509KeyUsageFlags.DigitalSignature);
			X509KeyUsageFlags x509KeyUsageFlags2 = expectedUsage & X509KeyUsageFlags.KeyEncipherment;
			if (intendedKeyUsage == X509KeyUsageFlags.None)
			{
				return x509KeyUsageFlags == X509KeyUsageFlags.None || !flag;
			}
			return (x509KeyUsageFlags == X509KeyUsageFlags.None || (!flag && (intendedKeyUsage & x509KeyUsageFlags) != X509KeyUsageFlags.None)) && (x509KeyUsageFlags2 == X509KeyUsageFlags.None || ((flag || flag2 || (intendedKeyUsage & X509KeyUsageFlags.KeyEncipherment) != X509KeyUsageFlags.None) && (!flag2 || (intendedKeyUsage & X509KeyUsageFlags.KeyEncipherment) != X509KeyUsageFlags.None || (intendedKeyUsage & X509KeyUsageFlags.KeyAgreement) != X509KeyUsageFlags.None) && (!flag || (intendedKeyUsage & X509KeyUsageFlags.KeyAgreement) != X509KeyUsageFlags.KeyAgreement || (intendedKeyUsage & X509KeyUsageFlags.DecipherOnly) != X509KeyUsageFlags.DecipherOnly)));
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x0009C438 File Offset: 0x0009A638
		private static ChainBuildOptions GetChainBuildOptions(bool checkRevocation)
		{
			if (!checkRevocation)
			{
				return (ChainBuildOptions.CacheEndCert | ChainBuildOptions.RevocationCheckChainExcludeRoot | ChainBuildOptions.RevocationAccumulativeTimeout) & ~X509CertificateCollection.CheckCRLInCertificateChainOptions;
			}
			return ChainBuildOptions.CacheEndCert | ChainBuildOptions.RevocationCheckChainExcludeRoot | ChainBuildOptions.RevocationAccumulativeTimeout | X509CertificateCollection.CheckCRLInCertificateChainOptions;
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x0009C455 File Offset: 0x0009A655
		private static ChainPolicyParameters GetChainPolicyParameters(bool checkRevocation)
		{
			if (!checkRevocation)
			{
				return X509CertificateCollection.PolicyIgnoreRevocation;
			}
			return X509CertificateCollection.DefaultPolicy;
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x0009C468 File Offset: 0x0009A668
		private static ChainEngine BuildChainEngine(TimeSpan retrievalTimeout, X509Store store, bool exclusiveTrustMode, string poolUid = null)
		{
			if (string.IsNullOrEmpty(poolUid))
			{
				poolUid = "UID_C6FC2B0E-0DA3-4D1F-ACFD-B8CB400EB4A5";
			}
			ChainEnginePool chainEnginePool = null;
			if (!X509CertificateCollection.chainEnginePoolCache.TryGetValue(poolUid, out chainEnginePool))
			{
				chainEnginePool = new ChainEnginePool(10, ChainEngineOptions.CacheEndCert | ChainEngineOptions.UseLocalMachineStore | ChainEngineOptions.EnableCacheAutoUpdate | ChainEngineOptions.EnableShareStore, retrievalTimeout, 0, store, exclusiveTrustMode);
				X509CertificateCollection.chainEnginePoolCache.Add(poolUid, chainEnginePool);
			}
			return chainEnginePool.GetEngine();
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x0009C4B5 File Offset: 0x0009A6B5
		private static ChainBuildParameter GetChainBuildParameter(TimeSpan connectionTimeout)
		{
			return new ChainBuildParameter(AndChainMatchIssuer.EmailProtection, connectionTimeout, false, TimeSpan.Zero);
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x0009C4C8 File Offset: 0x0009A6C8
		private static bool IsECCertificate(X509Certificate2 certificate)
		{
			return string.Equals(certificate.PublicKey.Oid.Value, WellKnownOid.ECPublicKey.Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040034E7 RID: 13543
		private const string defaultChainEnginePoolUid = "UID_C6FC2B0E-0DA3-4D1F-ACFD-B8CB400EB4A5";

		// Token: 0x040034E8 RID: 13544
		private const int DefaultCacheLimit = 0;

		// Token: 0x040034E9 RID: 13545
		private const ChainEngineOptions DefaultOptions = ChainEngineOptions.CacheEndCert | ChainEngineOptions.UseLocalMachineStore | ChainEngineOptions.EnableCacheAutoUpdate | ChainEngineOptions.EnableShareStore;

		// Token: 0x040034EA RID: 13546
		private static readonly TimeSpan DefaultCRLConnectionTimeout = TimeSpan.FromSeconds(10.0);

		// Token: 0x040034EB RID: 13547
		private static readonly TimeSpan DefaultCRLRetrievalTimeout = TimeSpan.FromSeconds(10.0);

		// Token: 0x040034EC RID: 13548
		private static int fieldSize = Marshal.SizeOf(typeof(ushort));

		// Token: 0x040034ED RID: 13549
		private static ChainPolicyParameters DefaultPolicy = new BaseChainPolicyParameters(ChainPolicyOptions.None);

		// Token: 0x040034EE RID: 13550
		private static ChainPolicyParameters PolicyIgnoreRevocation = new BaseChainPolicyParameters(ChainPolicyOptions.IgnoreEndRevUnknown | ChainPolicyOptions.IgnoreCTLSignerRevUnknown | ChainPolicyOptions.IgnoreCARevUnknown | ChainPolicyOptions.IgnoreRootRevUnknown);

		// Token: 0x040034EF RID: 13551
		private static ChainBuildOptions CheckCRLInCertificateChainOptions = ChainBuildOptions.RevocationCheckEndCert | ChainBuildOptions.RevocationCheckChain | ChainBuildOptions.RevocationCheckChainExcludeRoot;

		// Token: 0x040034F0 RID: 13552
		private static MruDictionaryCache<string, ChainEnginePool> chainEnginePoolCache = new MruDictionaryCache<string, ChainEnginePool>(5000, 5);

		// Token: 0x02000AEE RID: 2798
		private enum OutlookCertificateTag : ushort
		{
			// Token: 0x040034F2 RID: 13554
			Version = 1,
			// Token: 0x040034F3 RID: 13555
			AsymmetricCapabilities,
			// Token: 0x040034F4 RID: 13556
			KeyExchangeCertificate,
			// Token: 0x040034F5 RID: 13557
			CertificateChain,
			// Token: 0x040034F6 RID: 13558
			MessageEncoding = 6,
			// Token: 0x040034F7 RID: 13559
			SignatureCertificate = 8,
			// Token: 0x040034F8 RID: 13560
			SignSha1Hash,
			// Token: 0x040034F9 RID: 13561
			DisplayName = 11,
			// Token: 0x040034FA RID: 13562
			NortelBulkAlgorithm,
			// Token: 0x040034FB RID: 13563
			CertificateTime,
			// Token: 0x040034FC RID: 13564
			Defaults = 32,
			// Token: 0x040034FD RID: 13565
			KeyExchangeSha1Hash = 34
		}
	}
}

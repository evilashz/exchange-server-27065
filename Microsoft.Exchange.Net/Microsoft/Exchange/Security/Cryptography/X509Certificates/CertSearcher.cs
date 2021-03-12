﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A85 RID: 2693
	internal abstract class CertSearcher
	{
		// Token: 0x06003A29 RID: 14889 RVA: 0x00093E7C File Offset: 0x0009207C
		protected CertSearcher(X509Store certStore, ChainEngine chainEngine, ChainBuildParameter usage, ChainPolicyParameters policy)
		{
			if (certStore == null)
			{
				throw new ArgumentNullException("certStore");
			}
			this.store = certStore;
			if (chainEngine == null)
			{
				throw new ArgumentNullException("chainEngine");
			}
			this.engine = chainEngine;
			if (usage == null)
			{
				throw new ArgumentNullException("usage");
			}
			this.usage = usage;
			this.policy = ((policy != null) ? policy : new BaseChainPolicyParameters(ChainPolicyOptions.None));
			this.Status = (ChainValidityStatus)2148204810U;
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x00093EED File Offset: 0x000920ED
		// (set) Token: 0x06003A2B RID: 14891 RVA: 0x00093EF5 File Offset: 0x000920F5
		public CertificateSelectionOption Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06003A2C RID: 14892 RVA: 0x00093EFE File Offset: 0x000920FE
		// (set) Token: 0x06003A2D RID: 14893 RVA: 0x00093F06 File Offset: 0x00092106
		public WildcardMatchType WildCardMatchType
		{
			get
			{
				return this.wildCardMatchType;
			}
			set
			{
				this.wildCardMatchType = value;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06003A2E RID: 14894 RVA: 0x00093F0F File Offset: 0x0009210F
		// (set) Token: 0x06003A2F RID: 14895 RVA: 0x00093F17 File Offset: 0x00092117
		public ChainValidityStatus Status
		{
			get
			{
				return this.status;
			}
			private set
			{
				this.status = value;
			}
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x00093F20 File Offset: 0x00092120
		public X509Certificate2 Search(IEnumerable<string> names)
		{
			List<X509Certificate2> certs = this.FindAll(names);
			return this.FindBest(names, certs);
		}

		// Token: 0x06003A31 RID: 14897
		protected abstract bool IsCertificateValid(X509Certificate2 certificate, IEnumerable<string> names);

		// Token: 0x06003A32 RID: 14898 RVA: 0x00093F40 File Offset: 0x00092140
		protected List<X509Certificate2> FindAll(IEnumerable<string> names)
		{
			foreach (string name in names)
			{
				if (!Dns.IsValidName(name))
				{
					if (Dns.IsValidWildcardName(name))
					{
						throw new ArgumentException(NetException.WildcardNotSupported(name), "names");
					}
					throw new ArgumentException(NetException.InvalidFQDN(name), "names");
				}
			}
			CertSearcher.Logger.TraceEvent(TraceEventType.Information, 0, "Searching for a certificate that has one of the following FQDNs : {0}", new object[]
			{
				new EnumerableTracer<string>(names)
			});
			List<X509Certificate2> list = new List<X509Certificate2>();
			foreach (X509Certificate2 x509Certificate in this.store.Certificates)
			{
				if (x509Certificate.Version >= 3 && this.IsCertificateValid(x509Certificate, names))
				{
					list.Add(x509Certificate);
				}
			}
			list.Sort(new Comparison<X509Certificate2>(CertSearcher.CompareByNotBefore));
			return list;
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x0009403C File Offset: 0x0009223C
		protected X509Certificate2 FindBest(IEnumerable<string> names, IEnumerable<X509Certificate2> certs)
		{
			X509Certificate2 x509Certificate = null;
			X509Certificate2 x509Certificate2 = null;
			X509Certificate2 x509Certificate3 = null;
			foreach (X509Certificate2 x509Certificate4 in certs)
			{
				CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "Considering certificate {0}", new object[]
				{
					x509Certificate4.Thumbprint
				});
				if (x509Certificate3 != null)
				{
					CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Ignored. Older than previous certificates.", new object[]
					{
						x509Certificate4.Thumbprint
					});
				}
				else if (CertSearcher.IsValidServerKeyUsages(x509Certificate4))
				{
					using (ChainContext chainContext = this.engine.Build(x509Certificate4, ChainBuildOptions.CacheEndCert | ChainBuildOptions.RevocationCheckChainExcludeRoot | ChainBuildOptions.RevocationAccumulativeTimeout, this.usage))
					{
						if (chainContext == null)
						{
							CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Failed to build a certificate chain.", new object[]
							{
								x509Certificate4.Thumbprint
							});
						}
						else if (chainContext.Status == TrustStatus.IsUntrustedRoot)
						{
							if (!chainContext.IsSelfSigned)
							{
								CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Failed to build a certificate chain to a trusted root.", new object[]
								{
									x509Certificate4.Thumbprint
								});
							}
							else if ((this.options & CertificateSelectionOption.PreferedNonSelfSigned) == CertificateSelectionOption.None)
							{
								this.Status = ChainValidityStatus.ValidSelfSigned;
								CertSearcher.Logger.TraceEvent(TraceEventType.Information, 0, "{0}: Selected.  Self signed certificate.", new object[]
								{
									x509Certificate4.Thumbprint
								});
								x509Certificate3 = x509Certificate4;
							}
							else if (x509Certificate == null)
							{
								x509Certificate = x509Certificate4;
								if (x509Certificate2 == null)
								{
									this.Status = ChainValidityStatus.ValidSelfSigned;
								}
							}
						}
						else
						{
							ChainSummary chainSummary = chainContext.Validate(this.policy);
							if (chainSummary.Status == ChainValidityStatus.Valid)
							{
								if (chainContext.IsSelfSigned && (this.options & CertificateSelectionOption.PreferedNonSelfSigned) != CertificateSelectionOption.None)
								{
									if (x509Certificate == null)
									{
										x509Certificate = x509Certificate4;
										if (x509Certificate2 == null)
										{
											this.Status = ChainValidityStatus.ValidSelfSigned;
										}
									}
								}
								else
								{
									CertSearcher.Logger.TraceEvent(TraceEventType.Information, 0, "{0}: Selected.  PKI issued certificate.", new object[]
									{
										x509Certificate4.Thumbprint
									});
									this.Status = chainSummary.Status;
									x509Certificate3 = x509Certificate4;
								}
							}
							else if (chainSummary.Status == (ChainValidityStatus)2148204801U)
							{
								if (x509Certificate2 != null)
								{
									CertSearcher.Logger.TraceEvent(TraceEventType.Information, 0, "{0}: Selected.  PKI issued certificate but revocation is either offline or has failures.", new object[]
									{
										x509Certificate2.Thumbprint
									});
									x509Certificate3 = x509Certificate2;
								}
								else if (!chainContext.IsSelfSigned)
								{
									this.status = chainSummary.Status;
									CertSearcher.Logger.TraceEvent(TraceEventType.Information, 0, "{0}: Selected.  PKI issued certificate but has expired.", new object[]
									{
										x509Certificate4.Thumbprint
									});
									x509Certificate3 = x509Certificate4;
								}
								else if (x509Certificate != null)
								{
									CertSearcher.Logger.TraceEvent(TraceEventType.Information, 0, "{0}: Selected.  Self signed certificate.", new object[]
									{
										x509Certificate.Thumbprint
									});
									x509Certificate3 = x509Certificate;
								}
								else
								{
									CertSearcher.Logger.TraceEvent(TraceEventType.Information, 0, "{0}: Selected.  Self signed but has expired.", new object[]
									{
										x509Certificate4.Thumbprint
									});
									x509Certificate3 = x509Certificate4;
								}
							}
							else if (chainSummary.Status == (ChainValidityStatus)2148204814U || chainSummary.Status == (ChainValidityStatus)2148081683U || chainSummary.Status == (ChainValidityStatus)2148081682U)
							{
								if (x509Certificate2 == null)
								{
									x509Certificate2 = x509Certificate4;
									this.status = chainSummary.Status;
									CertSearcher.Logger.TraceEvent(TraceEventType.Warning, 0, "{0}: Warning. Has a revocation failure {1}, but is currently the best.", new object[]
									{
										x509Certificate4.Thumbprint,
										chainSummary.Status
									});
								}
								else
								{
									CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Has a revocation failure {1}.", new object[]
									{
										x509Certificate4.Thumbprint,
										chainSummary.Status
									});
								}
							}
							else if (chainSummary.Status == (ChainValidityStatus)2148204809U)
							{
								if ((chainContext.Status & TrustStatus.IsPartialChain) == TrustStatus.IsPartialChain)
								{
									CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Not all the intermediate certificates can be located to build a chain back to a Trusted Root Certificate.", new object[]
									{
										x509Certificate4.Thumbprint
									});
								}
								else
								{
									CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Root certificate is not listed in the Trusted Root Certificate store.", new object[]
									{
										x509Certificate4.Thumbprint
									});
								}
							}
							else
							{
								CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Has an unexpected failure of {1}.", new object[]
								{
									x509Certificate4.Thumbprint,
									chainSummary.Status
								});
							}
						}
					}
				}
			}
			if (x509Certificate3 != null)
			{
				return x509Certificate3;
			}
			if (x509Certificate2 != null)
			{
				CertSearcher.Logger.TraceEvent(TraceEventType.Information, 0, "{0}: Selected.  PKI issued certificate but Revocation is either offline or has failures.", new object[]
				{
					x509Certificate2.Thumbprint
				});
				return x509Certificate2;
			}
			if (x509Certificate != null)
			{
				CertSearcher.Logger.TraceEvent(TraceEventType.Information, 0, "{0}: Selected.  Self signed certificate.", new object[]
				{
					x509Certificate.Thumbprint
				});
			}
			else
			{
				CertSearcher.Logger.TraceEvent(TraceEventType.Information, 0, "No certificate match found.");
			}
			return x509Certificate;
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x00094524 File Offset: 0x00092724
		protected bool IsFQDNMatch(X509Certificate2 certificate, IEnumerable<string> names)
		{
			IList<string> fqdns = TlsCertificateInfo.GetFQDNs(certificate);
			bool flag = (this.options & CertificateSelectionOption.WildcardAllowed) == CertificateSelectionOption.WildcardAllowed;
			foreach (string text in names)
			{
				if (!string.IsNullOrEmpty(text))
				{
					foreach (string text2 in fqdns)
					{
						if (flag && text2[0] == '*')
						{
							if (CertSearcher.MatchWildcardCertNameWithName(text2, text, this.WildCardMatchType))
							{
								return true;
							}
						}
						else if (text.Equals(text2, StringComparison.OrdinalIgnoreCase))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000945F0 File Offset: 0x000927F0
		protected static bool IsValidServerKeyUsages(X509Certificate2 certificate)
		{
			CapiNativeMethods.CryptKeyProvInfo providerInfo;
			if (!CapiNativeMethods.GetPrivateKeyInfo(certificate, out providerInfo))
			{
				CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Certificate does not have an associated private key.", new object[]
				{
					certificate.Thumbprint
				});
				return false;
			}
			if (providerInfo.ProviderType != CapiNativeMethods.ProviderType.CNG)
			{
				return CertSearcher.CapiIsValidServerKeyUsages(certificate, providerInfo);
			}
			return CertSearcher.CNGIsValidServerKeyUsages(certificate, providerInfo);
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x00094644 File Offset: 0x00092844
		private static bool MatchWildcardCertNameWithName(string wildcardCertName, string name, WildcardMatchType matchType)
		{
			if (wildcardCertName.Length <= 2 || wildcardCertName[0] != '*' || wildcardCertName[1] != '.')
			{
				return false;
			}
			string text = wildcardCertName.Substring(1);
			int num = name.Length - text.Length;
			return num > 0 && name.EndsWith(text, StringComparison.OrdinalIgnoreCase) && (matchType == WildcardMatchType.MultiLevel || name.IndexOf('.', 0, num) == -1);
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x000946AC File Offset: 0x000928AC
		private static bool CapiIsValidServerKeyUsages(X509Certificate2 certificate, CapiNativeMethods.CryptKeyProvInfo providerInfo)
		{
			if (providerInfo.KeySpec != CapiNativeMethods.KeySpec.KeyExchange && providerInfo.KeySpec != CapiNativeMethods.KeySpec.Signature)
			{
				CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Does not have a KeyNumber of either Exchange | Signature.", new object[]
				{
					certificate.Thumbprint
				});
				return false;
			}
			SafeCryptProvHandle safeCryptProvHandle;
			if (!CapiNativeMethods.CryptAcquireContext(out safeCryptProvHandle, providerInfo.ContainerName, providerInfo.ProviderName, providerInfo.ProviderType, providerInfo.Flags | CapiNativeMethods.AcquireContext.MachineKeyset | CapiNativeMethods.AcquireContext.Silent))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == -2146893790)
				{
					CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Private key access requires user input such as PIN or password so cannot be used by a service.", new object[]
					{
						certificate.Thumbprint
					});
				}
				else
				{
					CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Unable to access the associated private key for the certificate. {1:X}", new object[]
					{
						certificate.Thumbprint,
						lastWin32Error
					});
				}
				return false;
			}
			using (safeCryptProvHandle)
			{
				uint num = (uint)Marshal.SizeOf(typeof(uint));
				uint extra = 0U;
				uint num2;
				if (!CapiNativeMethods.CryptGetProvParam(safeCryptProvHandle, CapiNativeMethods.ProviderParameter.ImplementationType, out num2, ref num, extra))
				{
					CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Certificate implementation query failed.", new object[]
					{
						certificate.Thumbprint
					});
					return false;
				}
				CapiNativeMethods.ProviderImplementationType providerImplementationType = (CapiNativeMethods.ProviderImplementationType)num2;
				if ((providerImplementationType & CapiNativeMethods.ProviderImplementationType.Removable) == CapiNativeMethods.ProviderImplementationType.Removable && (providerImplementationType & CapiNativeMethods.ProviderImplementationType.Hardware) == CapiNativeMethods.ProviderImplementationType.Hardware)
				{
					CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Private key is stored on a removable device such as a smart card so cannot be used by a service.", new object[]
					{
						certificate.Thumbprint
					});
					return false;
				}
				SafeCryptKeyHandle safeCryptKeyHandle;
				if (!CapiNativeMethods.CryptGetUserKey(safeCryptProvHandle, providerInfo.KeySpec, out safeCryptKeyHandle))
				{
					CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Private key is not accessible at this time. The key is possibly stored in a hardware device which is offline.", new object[]
					{
						certificate.Thumbprint
					});
					return false;
				}
				using (safeCryptKeyHandle)
				{
					uint num3 = 0U;
					num = (uint)Marshal.SizeOf(typeof(uint));
					if (!CapiNativeMethods.CryptGetKeyParam(safeCryptKeyHandle, CapiNativeMethods.KeyParameter.KeyLength, ref num3, ref num, 0U))
					{
						CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Certificate private key length query failed.", new object[]
						{
							certificate.Thumbprint
						});
						return false;
					}
					if (num3 < 1024U)
					{
						CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Has a key size less than 1024 bits.", new object[]
						{
							certificate.Thumbprint
						});
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x00094934 File Offset: 0x00092B34
		private static bool CNGIsValidServerKeyUsages(X509Certificate2 certificate, CapiNativeMethods.CryptKeyProvInfo providerInfo)
		{
			SafeNCryptHandle safeNCryptHandle;
			int num = CngNativeMethods.NCryptOpenStorageProvider(out safeNCryptHandle, providerInfo.ProviderName, 0U);
			if (num != 0)
			{
				CertSearcher.Logger.TraceEvent(TraceEventType.Error, 0, "{0}: Cryptographic provider failure [{1}] while analyzing.", new object[]
				{
					certificate.Thumbprint,
					num
				});
				return false;
			}
			using (safeNCryptHandle)
			{
				uint valueSize = (uint)Marshal.SizeOf(typeof(uint));
				uint num2;
				uint num3;
				num = CngNativeMethods.NCryptGetProperty(safeNCryptHandle, "Impl Type", out num2, valueSize, out num3, (CngNativeMethods.PropertyOptions)0U);
				if (num != 0)
				{
					CertSearcher.Logger.TraceEvent(TraceEventType.Error, 0, "{0}: Cryptographic property retrieval failure [{1}] while analyzing.", new object[]
					{
						certificate.Thumbprint,
						num
					});
					return false;
				}
				CngNativeMethods.ImplemenationType implemenationType = (CngNativeMethods.ImplemenationType)num2;
				if ((implemenationType & CngNativeMethods.ImplemenationType.Hardware) == CngNativeMethods.ImplemenationType.Hardware && (implemenationType & CngNativeMethods.ImplemenationType.Removable) == CngNativeMethods.ImplemenationType.Removable)
				{
					CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Private key is stored on a removable device such as a smart card so cannot be used by a service.", new object[]
					{
						certificate.Thumbprint
					});
					return false;
				}
				CngNativeMethods.KeyOptions keyOptions = CngNativeMethods.KeyOptions.MachineKeyset | CngNativeMethods.KeyOptions.Silent;
				SafeNCryptHandle safeNCryptHandle3;
				num = CngNativeMethods.NCryptOpenKey(safeNCryptHandle, out safeNCryptHandle3, providerInfo.ContainerName, 0U, keyOptions);
				if (num != 0)
				{
					if (num == -2146893802)
					{
						CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Private key is not accessible at this time. The key is possibly stored in a hardware device which is offline.", new object[]
						{
							certificate.Thumbprint
						});
					}
					else if (num == -2146893790)
					{
						CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Private key access requires user input such as PIN or password so cannot be used by a service.", new object[]
						{
							certificate.Thumbprint
						});
					}
					else
					{
						CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Opening private key failed {1}.", new object[]
						{
							certificate.Thumbprint,
							num
						});
					}
					return false;
				}
				using (safeNCryptHandle3)
				{
					num = CngNativeMethods.NCryptGetProperty(safeNCryptHandle3, "Length", out num2, valueSize, out num3, (CngNativeMethods.PropertyOptions)0U);
					if (num != 0)
					{
						CertSearcher.Logger.TraceEvent(TraceEventType.Error, 0, "{0}: Cryptographic property retrieval failure [{1}] while analyzing.", new object[]
						{
							certificate.Thumbprint,
							num
						});
						return false;
					}
					if (num2 < 1024U)
					{
						CertSearcher.Logger.TraceEvent(TraceEventType.Verbose, 0, "{0}: Rejected. Has a key size less than 1024 bits.", new object[]
						{
							certificate.Thumbprint
						});
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x00094BB0 File Offset: 0x00092DB0
		internal static int CompareByNotBefore(X509Certificate2 x, X509Certificate2 y)
		{
			if (x == null)
			{
				if (y == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				return y.NotBefore.CompareTo(x.NotBefore);
			}
		}

		// Token: 0x0400325C RID: 12892
		protected static TraceSource Logger = new TraceSource("Microsoft.Exchange.Security.Certificate");

		// Token: 0x0400325D RID: 12893
		private readonly ChainBuildParameter usage;

		// Token: 0x0400325E RID: 12894
		private readonly ChainPolicyParameters policy;

		// Token: 0x0400325F RID: 12895
		private readonly X509Store store;

		// Token: 0x04003260 RID: 12896
		private readonly ChainEngine engine;

		// Token: 0x04003261 RID: 12897
		private CertificateSelectionOption options;

		// Token: 0x04003262 RID: 12898
		private WildcardMatchType wildCardMatchType;

		// Token: 0x04003263 RID: 12899
		private ChainValidityStatus status;
	}
}

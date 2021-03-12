using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A99 RID: 2713
	internal class ChainSummary
	{
		// Token: 0x06003A7E RID: 14974 RVA: 0x000956B3 File Offset: 0x000938B3
		internal ChainSummary(CapiNativeMethods.CertChainPolicyStatus results)
		{
			this.data = results;
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x000956CE File Offset: 0x000938CE
		public ChainValidityStatus Status
		{
			get
			{
				return this.data.Status;
			}
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x000956DC File Offset: 0x000938DC
		internal static ChainSummary Validate(SafeChainContextHandle chainContext, ChainPolicyParameters options)
		{
			CapiNativeMethods.CertChainPolicyParameters certChainPolicyParameters = new CapiNativeMethods.CertChainPolicyParameters(options.Flags);
			IntPtr pszPolicyOID = IntPtr.Zero;
			SSLChainPolicyParameters sslchainPolicyParameters = options as SSLChainPolicyParameters;
			if (sslchainPolicyParameters != null)
			{
				pszPolicyOID = (IntPtr)4L;
				ChainSummary.SSLExtraChainPolicyParameter sslextraChainPolicyParameter = new ChainSummary.SSLExtraChainPolicyParameter(sslchainPolicyParameters.Type, sslchainPolicyParameters.Options, sslchainPolicyParameters.ServerName);
				CapiNativeMethods.CertChainPolicyStatus results = CapiNativeMethods.CertChainPolicyStatus.Create();
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ChainSummary.SSLExtraChainPolicyParameter)));
				Marshal.StructureToPtr(sslextraChainPolicyParameter, intPtr, false);
				certChainPolicyParameters.ExtraPolicy = intPtr;
				try
				{
					if (!CapiNativeMethods.CertVerifyCertificateChainPolicy(pszPolicyOID, chainContext, ref certChainPolicyParameters, ref results))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return new ChainSummary(results);
			}
			BaseChainPolicyParameters baseChainPolicyParameters = options as BaseChainPolicyParameters;
			if (baseChainPolicyParameters == null)
			{
				throw new ArgumentException(NetException.OnlySSLSupported);
			}
			pszPolicyOID = (IntPtr)1L;
			CapiNativeMethods.CertChainPolicyStatus results2 = CapiNativeMethods.CertChainPolicyStatus.Create();
			if (!CapiNativeMethods.CertVerifyCertificateChainPolicy(pszPolicyOID, chainContext, ref certChainPolicyParameters, ref results2))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return new ChainSummary(results2);
		}

		// Token: 0x040032C4 RID: 12996
		private CapiNativeMethods.CertChainPolicyStatus data = default(CapiNativeMethods.CertChainPolicyStatus);

		// Token: 0x02000A9A RID: 2714
		private struct SSLExtraChainPolicyParameter
		{
			// Token: 0x06003A81 RID: 14977 RVA: 0x000957F0 File Offset: 0x000939F0
			internal SSLExtraChainPolicyParameter(SSLPolicyAuthorizationType type, SSLPolicyAuthorizationOptions checks, string name)
			{
				this.size = (uint)Marshal.SizeOf(typeof(ChainSummary.SSLExtraChainPolicyParameter));
				this.type = type;
				this.checks = checks;
				this.serverName = name;
			}

			// Token: 0x040032C5 RID: 12997
			private uint size;

			// Token: 0x040032C6 RID: 12998
			private SSLPolicyAuthorizationType type;

			// Token: 0x040032C7 RID: 12999
			private SSLPolicyAuthorizationOptions checks;

			// Token: 0x040032C8 RID: 13000
			[MarshalAs(UnmanagedType.LPWStr)]
			private string serverName;
		}
	}
}

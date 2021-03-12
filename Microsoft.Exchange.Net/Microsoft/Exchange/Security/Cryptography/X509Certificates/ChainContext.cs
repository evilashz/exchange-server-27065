using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A8C RID: 2700
	internal class ChainContext : IDisposable
	{
		// Token: 0x06003A4D RID: 14925 RVA: 0x00094E50 File Offset: 0x00093050
		internal ChainContext(SafeChainContextHandle handle)
		{
			this.chainContext = handle;
			ChainContext.CertChainContext certChainContext = (ChainContext.CertChainContext)Marshal.PtrToStructure(this.chainContext.DangerousGetHandle(), typeof(ChainContext.CertChainContext));
			this.trust = certChainContext.TrustStatus;
			this.expires = (certChainContext.HasRevocationFreshnessTime ? (DateTime.UtcNow + TimeSpan.FromSeconds(certChainContext.RevocationFreshnessTime)) : DateTime.UtcNow);
			this.chainCount = certChainContext.Chains;
			this.chainData = certChainContext.ChainData;
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003A4E RID: 14926 RVA: 0x00094EDF File Offset: 0x000930DF
		public TrustStatus Status
		{
			get
			{
				return this.trust.error;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003A4F RID: 14927 RVA: 0x00094EEC File Offset: 0x000930EC
		public TrustInformation TrustInformation
		{
			get
			{
				return this.trust.information;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003A50 RID: 14928 RVA: 0x00094EF9 File Offset: 0x000930F9
		public DateTime Expires
		{
			get
			{
				return this.expires;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06003A51 RID: 14929 RVA: 0x00094F04 File Offset: 0x00093104
		public bool IsSelfSigned
		{
			get
			{
				if (this.chainCount != 1)
				{
					return false;
				}
				if (this.chains == null)
				{
					IntPtr bytes = Marshal.ReadIntPtr(this.chainData);
					return CertificateChain.IsSelfSigned(bytes);
				}
				if (this.chains.Count != 1 || this.chains[0].Elements.Count != 1)
				{
					return false;
				}
				TrustInformation trustInformation = this.chains[0].Elements[0].TrustInformation;
				return (trustInformation & TrustInformation.IsSelfSigned) != TrustInformation.None;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06003A52 RID: 14930 RVA: 0x00094F88 File Offset: 0x00093188
		public X509Certificate2 RootCertificate
		{
			get
			{
				if (this.Status == TrustStatus.IsUntrustedRoot)
				{
					return null;
				}
				IList<CertificateChain> list = this.GetChains();
				if (list.Count == 0)
				{
					ExTraceGlobals.CertificateTracer.TraceError(0L, "Marshalling error.  Chain segment count is zero.");
					throw new InvalidOperationException("Chain count is zero but we found results!");
				}
				CertificateChain certificateChain = list[list.Count - 1];
				if (certificateChain.Elements.Count == 0)
				{
					ExTraceGlobals.CertificateTracer.TraceError(0L, "Marshalling error.  Chain segment has zero elements.");
					throw new InvalidOperationException("Last chain count is zero but we found results!");
				}
				X509Certificate2 certificate = certificateChain.Elements[certificateChain.Elements.Count - 1].Certificate;
				if (certificate == null)
				{
					ExTraceGlobals.CertificateTracer.TraceError(0L, "Marshalling error.  Certificate in last element is null.");
					throw new InvalidOperationException("Root certificate was null!");
				}
				return certificate;
			}
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x00095042 File Offset: 0x00093242
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x00095054 File Offset: 0x00093254
		public IList<CertificateChain> GetChains()
		{
			if (this.chains != null)
			{
				return this.chains;
			}
			List<CertificateChain> list = new List<CertificateChain>(this.chainCount);
			for (int i = 0; i < this.chainCount; i++)
			{
				IntPtr bytes = Marshal.ReadIntPtr(this.chainData, i * Marshal.SizeOf(typeof(IntPtr)));
				list.Add(CertificateChain.Create(bytes));
			}
			this.chains = list.AsReadOnly();
			return this.chains;
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000950C8 File Offset: 0x000932C8
		public ChainSummary Validate(ChainPolicyParameters options)
		{
			return ChainSummary.Validate(this.chainContext, options);
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x000950D6 File Offset: 0x000932D6
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.chainContext != null)
			{
				this.chainContext.Dispose();
			}
		}

		// Token: 0x0400327C RID: 12924
		private SafeChainContextHandle chainContext;

		// Token: 0x0400327D RID: 12925
		private CapiNativeMethods.CertTrustStatus trust;

		// Token: 0x0400327E RID: 12926
		private int chainCount;

		// Token: 0x0400327F RID: 12927
		private IntPtr chainData;

		// Token: 0x04003280 RID: 12928
		private IList<CertificateChain> chains;

		// Token: 0x04003281 RID: 12929
		private DateTime expires;

		// Token: 0x02000A8D RID: 2701
		private struct CertChainContext
		{
			// Token: 0x17000E87 RID: 3719
			// (get) Token: 0x06003A57 RID: 14935 RVA: 0x000950EE File Offset: 0x000932EE
			public CapiNativeMethods.CertTrustStatus TrustStatus
			{
				get
				{
					return this.trustStatus;
				}
			}

			// Token: 0x17000E88 RID: 3720
			// (get) Token: 0x06003A58 RID: 14936 RVA: 0x000950F6 File Offset: 0x000932F6
			public int Chains
			{
				get
				{
					return this.chains;
				}
			}

			// Token: 0x17000E89 RID: 3721
			// (get) Token: 0x06003A59 RID: 14937 RVA: 0x000950FE File Offset: 0x000932FE
			public IntPtr ChainData
			{
				get
				{
					return this.chainData;
				}
			}

			// Token: 0x17000E8A RID: 3722
			// (get) Token: 0x06003A5A RID: 14938 RVA: 0x00095106 File Offset: 0x00093306
			public bool HasRevocationFreshnessTime
			{
				get
				{
					return this.hasRevocationFreshnessTime;
				}
			}

			// Token: 0x17000E8B RID: 3723
			// (get) Token: 0x06003A5B RID: 14939 RVA: 0x0009510E File Offset: 0x0009330E
			public uint RevocationFreshnessTime
			{
				get
				{
					return this.revocationFreshnessTime;
				}
			}

			// Token: 0x04003282 RID: 12930
			private int size;

			// Token: 0x04003283 RID: 12931
			private CapiNativeMethods.CertTrustStatus trustStatus;

			// Token: 0x04003284 RID: 12932
			private int chains;

			// Token: 0x04003285 RID: 12933
			private IntPtr chainData;

			// Token: 0x04003286 RID: 12934
			private int lowerChains;

			// Token: 0x04003287 RID: 12935
			private IntPtr lowerChainData;

			// Token: 0x04003288 RID: 12936
			[MarshalAs(UnmanagedType.Bool)]
			private bool hasRevocationFreshnessTime;

			// Token: 0x04003289 RID: 12937
			private uint revocationFreshnessTime;
		}
	}
}

using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A8E RID: 2702
	internal class ChainEngine : IDisposable
	{
		// Token: 0x06003A5C RID: 14940 RVA: 0x00095116 File Offset: 0x00093316
		internal ChainEngine(IEnginePool pool, SafeChainEngineHandle handle)
		{
			this.parent = pool;
			this.engine = handle;
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x0009512C File Offset: 0x0009332C
		public ChainEngine()
		{
			this.engine = SafeChainEngineHandle.DefaultEngine;
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x0009513F File Offset: 0x0009333F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x00095150 File Offset: 0x00093350
		public ChainContext Build(X509Certificate2 certificate, ChainBuildOptions options, ChainBuildParameter parameter)
		{
			SafeHGlobalHandle invalidHandle = SafeHGlobalHandle.InvalidHandle;
			CapiNativeMethods.CertUsageMatch certUsageMatch = parameter.Match.GetCertUsageMatch(ref invalidHandle);
			SafeChainContextHandle handle;
			using (invalidHandle)
			{
				CapiNativeMethods.CertChainParameter certChainParameter = new CapiNativeMethods.CertChainParameter(certUsageMatch, parameter.UrlRetrievalTimeout, parameter.OverrideRevocationTime, parameter.RevocationFreshnessDelta);
				SafeCertStoreHandle hAdditionalStore = new SafeCertStoreHandle();
				if (!CapiNativeMethods.CertGetCertificateChain(this.engine, certificate.Handle, IntPtr.Zero, hAdditionalStore, ref certChainParameter, options, IntPtr.Zero, out handle))
				{
					return null;
				}
			}
			return new ChainContext(handle);
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000951F0 File Offset: 0x000933F0
		public ChainContext Build(X509Certificate2 certificate, ChainBuildOptions options, ChainBuildParameter parameter, X509Store additionalStore)
		{
			SafeHGlobalHandle invalidHandle = SafeHGlobalHandle.InvalidHandle;
			CapiNativeMethods.CertUsageMatch certUsageMatch = parameter.Match.GetCertUsageMatch(ref invalidHandle);
			SafeChainContextHandle handle;
			bool flag;
			using (invalidHandle)
			{
				CapiNativeMethods.CertChainParameter certChainParameter = new CapiNativeMethods.CertChainParameter(certUsageMatch, parameter.UrlRetrievalTimeout, parameter.OverrideRevocationTime, parameter.RevocationFreshnessDelta);
				using (SafeCertStoreHandle safeCertStoreHandle = SafeCertStoreHandle.Clone(additionalStore))
				{
					flag = CapiNativeMethods.CertGetCertificateChain(this.engine, certificate.Handle, IntPtr.Zero, safeCertStoreHandle, ref certChainParameter, options, IntPtr.Zero, out handle);
				}
			}
			if (!flag)
			{
				return null;
			}
			return new ChainContext(handle);
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000952A4 File Offset: 0x000934A4
		public ChainContext BuildAsAnonymous(X509Certificate2 certificate, ChainBuildOptions options, ChainBuildParameter parameter)
		{
			SafeHGlobalHandle invalidHandle = SafeHGlobalHandle.InvalidHandle;
			CapiNativeMethods.CertUsageMatch certUsageMatch = parameter.Match.GetCertUsageMatch(ref invalidHandle);
			SafeChainContextHandle handle;
			using (invalidHandle)
			{
				CapiNativeMethods.CertChainParameter certChainParameter = new CapiNativeMethods.CertChainParameter(certUsageMatch, parameter.UrlRetrievalTimeout, parameter.OverrideRevocationTime, parameter.RevocationFreshnessDelta);
				using (SafeCertStoreHandle storeHandleFromCertificate = CapiNativeMethods.GetStoreHandleFromCertificate(certificate))
				{
					if (!CapiNativeMethods.CertGetCertificateChain(this.engine, certificate.Handle, IntPtr.Zero, storeHandleFromCertificate, ref certChainParameter, options, IntPtr.Zero, out handle))
					{
						return null;
					}
				}
			}
			return new ChainContext(handle);
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x00095360 File Offset: 0x00093560
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.parent != null)
				{
					this.parent.ReturnTo(this.engine);
					this.parent = null;
					return;
				}
				this.engine.Close();
			}
		}

		// Token: 0x0400328A RID: 12938
		public const ChainBuildOptions DefaultOptions = ChainBuildOptions.CacheEndCert | ChainBuildOptions.RevocationCheckChainExcludeRoot | ChainBuildOptions.RevocationAccumulativeTimeout;

		// Token: 0x0400328B RID: 12939
		private SafeChainEngineHandle engine;

		// Token: 0x0400328C RID: 12940
		private IEnginePool parent;
	}
}

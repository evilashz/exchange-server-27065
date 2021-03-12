using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200014A RID: 330
	internal class CertificateComponent : ITransportComponent
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x0003963C File Offset: 0x0003783C
		public CertificateCache Cache
		{
			get
			{
				return this.cache;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00039644 File Offset: 0x00037844
		public CertificateValidator Validator
		{
			get
			{
				return this.validator;
			}
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0003964C File Offset: 0x0003784C
		public void Load()
		{
			ChainEnginePool pool = new ChainEnginePool();
			this.anonymousValidationResultCache = new CertificateValidationResultCache(Components.Configuration.ProcessTransportRole, "AnonymousCertificateValidationResultCache", Components.TransportAppConfig.SecureMail, ExTraceGlobals.AnonymousCertificateValidationResultCacheTracer);
			this.validator = new CertificateValidator(pool, this.anonymousValidationResultCache, Components.TransportAppConfig.SecureMail);
			this.cache = new CertificateCache(pool);
			this.cache.Open(OpenFlags.ReadOnly);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x000396BC File Offset: 0x000378BC
		public void Unload()
		{
			this.cache.Close();
			this.cache = null;
			this.validator = null;
			this.anonymousValidationResultCache.Dispose();
			this.anonymousValidationResultCache = null;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x000396E9 File Offset: 0x000378E9
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x04000724 RID: 1828
		private CertificateCache cache;

		// Token: 0x04000725 RID: 1829
		private CertificateValidator validator;

		// Token: 0x04000726 RID: 1830
		private CertificateValidationResultCache anonymousValidationResultCache;
	}
}

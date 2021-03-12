using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C8B RID: 3211
	internal struct SchannelCredential
	{
		// Token: 0x060046E5 RID: 18149 RVA: 0x000BE95C File Offset: 0x000BCB5C
		public SchannelCredential(int version, X509Certificate certificate, SchannelCredential.Flags flags, SchannelProtocols protocols)
		{
			this.rootStore = (this.phMappers = (this.palgSupportedAlgs = (this.certContextArray = IntPtr.Zero)));
			this.cCreds = (this.cMappers = (this.cSupportedAlgs = 0));
			this.dwMinimumCipherStrength = (this.dwMaximumCipherStrength = 0);
			this.dwSessionLifespan = (this.reserved = 0);
			this.version = version;
			this.dwFlags = flags;
			this.grbitEnabledProtocols = protocols;
			if (certificate != null)
			{
				this.certContextArray = certificate.Handle;
				this.cCreds = 1;
			}
		}

		// Token: 0x04003BD2 RID: 15314
		public const int CurrentVersion = 4;

		// Token: 0x04003BD3 RID: 15315
		public int version;

		// Token: 0x04003BD4 RID: 15316
		public int cCreds;

		// Token: 0x04003BD5 RID: 15317
		public IntPtr certContextArray;

		// Token: 0x04003BD6 RID: 15318
		private readonly IntPtr rootStore;

		// Token: 0x04003BD7 RID: 15319
		public int cMappers;

		// Token: 0x04003BD8 RID: 15320
		private readonly IntPtr phMappers;

		// Token: 0x04003BD9 RID: 15321
		public int cSupportedAlgs;

		// Token: 0x04003BDA RID: 15322
		private readonly IntPtr palgSupportedAlgs;

		// Token: 0x04003BDB RID: 15323
		public SchannelProtocols grbitEnabledProtocols;

		// Token: 0x04003BDC RID: 15324
		public int dwMinimumCipherStrength;

		// Token: 0x04003BDD RID: 15325
		public int dwMaximumCipherStrength;

		// Token: 0x04003BDE RID: 15326
		public int dwSessionLifespan;

		// Token: 0x04003BDF RID: 15327
		public SchannelCredential.Flags dwFlags;

		// Token: 0x04003BE0 RID: 15328
		public int reserved;

		// Token: 0x02000C8C RID: 3212
		[Flags]
		public enum Flags
		{
			// Token: 0x04003BE2 RID: 15330
			Zero = 0,
			// Token: 0x04003BE3 RID: 15331
			NoSystemMapper = 2,
			// Token: 0x04003BE4 RID: 15332
			NoNameCheck = 4,
			// Token: 0x04003BE5 RID: 15333
			ValidateManual = 8,
			// Token: 0x04003BE6 RID: 15334
			NoDefaultCred = 16,
			// Token: 0x04003BE7 RID: 15335
			ValidateAuto = 32,
			// Token: 0x04003BE8 RID: 15336
			UseDefaultCreds = 64,
			// Token: 0x04003BE9 RID: 15337
			DisableReonnects = 128,
			// Token: 0x04003BEA RID: 15338
			RevocationCheckEndCert = 256,
			// Token: 0x04003BEB RID: 15339
			RevocationCheckChain = 512,
			// Token: 0x04003BEC RID: 15340
			RevocationCheckChainExcludeRoot = 1024,
			// Token: 0x04003BED RID: 15341
			IgnoreNoRevocationCheck = 2048,
			// Token: 0x04003BEE RID: 15342
			IgnoreRevocationOffline = 4096,
			// Token: 0x04003BEF RID: 15343
			CacheOnlyUrlRetrievalOnCreate = 131072,
			// Token: 0x04003BF0 RID: 15344
			SendRootCert = 262144,
			// Token: 0x04003BF1 RID: 15345
			SendAuxRecord = 2097152,
			// Token: 0x04003BF2 RID: 15346
			UseStrongCrypto = 4194304
		}
	}
}

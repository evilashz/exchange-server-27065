using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000065 RID: 101
	public enum CertificateValidationStatus
	{
		// Token: 0x0400019D RID: 413
		Valid,
		// Token: 0x0400019E RID: 414
		ValidSelfSigned,
		// Token: 0x0400019F RID: 415
		EmptyCertificate,
		// Token: 0x040001A0 RID: 416
		SubjectMismatch,
		// Token: 0x040001A1 RID: 417
		SignatureFailure,
		// Token: 0x040001A2 RID: 418
		UntrustedRoot,
		// Token: 0x040001A3 RID: 419
		UntrustedTestRoot,
		// Token: 0x040001A4 RID: 420
		InternalChainFailure,
		// Token: 0x040001A5 RID: 421
		WrongUsage,
		// Token: 0x040001A6 RID: 422
		CertificateExpired,
		// Token: 0x040001A7 RID: 423
		ValidityPeriodNesting,
		// Token: 0x040001A8 RID: 424
		PurposeError,
		// Token: 0x040001A9 RID: 425
		BasicConstraintsError,
		// Token: 0x040001AA RID: 426
		WrongRole,
		// Token: 0x040001AB RID: 427
		NoCNMatch,
		// Token: 0x040001AC RID: 428
		Revoked,
		// Token: 0x040001AD RID: 429
		RevocationOffline,
		// Token: 0x040001AE RID: 430
		CertificateRevoked,
		// Token: 0x040001AF RID: 431
		RevocationFailure,
		// Token: 0x040001B0 RID: 432
		NoRevocationCheck,
		// Token: 0x040001B1 RID: 433
		ExchangeServerAuthCertificate,
		// Token: 0x040001B2 RID: 434
		Other
	}
}

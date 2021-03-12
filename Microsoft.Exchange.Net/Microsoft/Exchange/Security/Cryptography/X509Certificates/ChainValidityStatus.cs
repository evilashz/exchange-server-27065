using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A9B RID: 2715
	internal enum ChainValidityStatus : uint
	{
		// Token: 0x040032CA RID: 13002
		Valid,
		// Token: 0x040032CB RID: 13003
		ValidSelfSigned,
		// Token: 0x040032CC RID: 13004
		EmptyCertificate,
		// Token: 0x040032CD RID: 13005
		SubjectMismatch,
		// Token: 0x040032CE RID: 13006
		SignatureFailure = 2148098052U,
		// Token: 0x040032CF RID: 13007
		UntrustedRoot = 2148204809U,
		// Token: 0x040032D0 RID: 13008
		UntrustedTestRoot = 2148204813U,
		// Token: 0x040032D1 RID: 13009
		InternalChainFailure = 2148204810U,
		// Token: 0x040032D2 RID: 13010
		WrongUsage = 2148204816U,
		// Token: 0x040032D3 RID: 13011
		CertificateExpired = 2148204801U,
		// Token: 0x040032D4 RID: 13012
		ValidityPeriodNesting,
		// Token: 0x040032D5 RID: 13013
		PurposeError = 2148204806U,
		// Token: 0x040032D6 RID: 13014
		BasicConstraintsError = 2148098073U,
		// Token: 0x040032D7 RID: 13015
		WrongRole = 2148204803U,
		// Token: 0x040032D8 RID: 13016
		NoCNMatch = 2148204815U,
		// Token: 0x040032D9 RID: 13017
		Revoked = 2148081680U,
		// Token: 0x040032DA RID: 13018
		RevocationOffline = 2148081683U,
		// Token: 0x040032DB RID: 13019
		CertificateRevoked = 2148204812U,
		// Token: 0x040032DC RID: 13020
		RevocationFailure = 2148204814U,
		// Token: 0x040032DD RID: 13021
		NoRevocationCheck = 2148081682U
	}
}

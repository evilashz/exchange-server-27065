using System;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009BF RID: 2495
	internal enum RmsOperationType
	{
		// Token: 0x04002EA1 RID: 11937
		AcquireLicense = 1,
		// Token: 0x04002EA2 RID: 11938
		AcquireTemplates,
		// Token: 0x04002EA3 RID: 11939
		AcquireTemplateInfo,
		// Token: 0x04002EA4 RID: 11940
		AcquireServerBoxRac,
		// Token: 0x04002EA5 RID: 11941
		AcquireClc,
		// Token: 0x04002EA6 RID: 11942
		AcquirePrelicense,
		// Token: 0x04002EA7 RID: 11943
		FindServiceLocations,
		// Token: 0x04002EA8 RID: 11944
		AcquireCertificationMexData,
		// Token: 0x04002EA9 RID: 11945
		AcquireServerLicensingMexData,
		// Token: 0x04002EAA RID: 11946
		AcquireB2BRac,
		// Token: 0x04002EAB RID: 11947
		AcquireB2BLicense,
		// Token: 0x04002EAC RID: 11948
		RequestDelegationToken
	}
}

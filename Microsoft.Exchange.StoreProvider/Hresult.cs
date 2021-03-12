using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200008E RID: 142
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class Hresult
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x000103A4 File Offset: 0x0000E5A4
		public static int GetScode(int hr)
		{
			return hr & 65535;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000103AD File Offset: 0x0000E5AD
		public static Hresult.Facility GetFacility(int hr)
		{
			return (Hresult.Facility)((hr & 268369920) >> 16);
		}

		// Token: 0x0200008F RID: 143
		internal enum Facility
		{
			// Token: 0x04000581 RID: 1409
			NULL,
			// Token: 0x04000582 RID: 1410
			RPC,
			// Token: 0x04000583 RID: 1411
			DISPATCH,
			// Token: 0x04000584 RID: 1412
			STORAGE,
			// Token: 0x04000585 RID: 1413
			ITF,
			// Token: 0x04000586 RID: 1414
			WIN32 = 7,
			// Token: 0x04000587 RID: 1415
			WINDOWS,
			// Token: 0x04000588 RID: 1416
			SECURITY,
			// Token: 0x04000589 RID: 1417
			CONTROL,
			// Token: 0x0400058A RID: 1418
			CERT,
			// Token: 0x0400058B RID: 1419
			INTERNET,
			// Token: 0x0400058C RID: 1420
			MEDIASERVER,
			// Token: 0x0400058D RID: 1421
			MSMQ,
			// Token: 0x0400058E RID: 1422
			SETUPAPI,
			// Token: 0x0400058F RID: 1423
			SCARD,
			// Token: 0x04000590 RID: 1424
			COMPLUS,
			// Token: 0x04000591 RID: 1425
			AAF,
			// Token: 0x04000592 RID: 1426
			URT,
			// Token: 0x04000593 RID: 1427
			ACS,
			// Token: 0x04000594 RID: 1428
			DPLAY,
			// Token: 0x04000595 RID: 1429
			UMI,
			// Token: 0x04000596 RID: 1430
			SXS,
			// Token: 0x04000597 RID: 1431
			WINDOWS_CE,
			// Token: 0x04000598 RID: 1432
			HTTP,
			// Token: 0x04000599 RID: 1433
			BACKGROUNDCOPY = 32,
			// Token: 0x0400059A RID: 1434
			CONFIGURATION,
			// Token: 0x0400059B RID: 1435
			STATE_MANAGEMENT,
			// Token: 0x0400059C RID: 1436
			METADIRECTORY,
			// Token: 0x0400059D RID: 1437
			WINDOWSUPDATE,
			// Token: 0x0400059E RID: 1438
			DIRECTORYSERVICE
		}
	}
}

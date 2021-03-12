using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009C2 RID: 2498
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IWSManagerPerfCounters
	{
		// Token: 0x0600365A RID: 13914
		void CertifySuccessful(long elapsedMilliseconds);

		// Token: 0x0600365B RID: 13915
		void CertifyFailed();

		// Token: 0x0600365C RID: 13916
		void GetClientLicensorCertSuccessful(long elapsedMilliseconds);

		// Token: 0x0600365D RID: 13917
		void GetClientLicensorCertFailed();

		// Token: 0x0600365E RID: 13918
		void AcquireLicenseSuccessful(long elapsedMilliseconds);

		// Token: 0x0600365F RID: 13919
		void AcquireLicenseFailed();

		// Token: 0x06003660 RID: 13920
		void AcquirePreLicenseSuccessful(long elapsedMilliseconds);

		// Token: 0x06003661 RID: 13921
		void AcquirePreLicenseFailed();

		// Token: 0x06003662 RID: 13922
		void AcquireTemplatesSuccessful(long elapsedMilliseconds);

		// Token: 0x06003663 RID: 13923
		void AcquireTemplatesFailed();

		// Token: 0x06003664 RID: 13924
		void FindServiceLocationsSuccessful(long elapsedMilliseconds);

		// Token: 0x06003665 RID: 13925
		void FindServiceLocationsFailed();

		// Token: 0x06003666 RID: 13926
		void WCFCertifySuccessful();

		// Token: 0x06003667 RID: 13927
		void WCFCertifyFailed();

		// Token: 0x06003668 RID: 13928
		void WCFAcquireServerLicenseSuccessful();

		// Token: 0x06003669 RID: 13929
		void WCFAcquireServerLicenseFailed();
	}
}

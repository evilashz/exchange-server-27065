using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009C3 RID: 2499
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NoopWSManagerPerfCounters : IWSManagerPerfCounters
	{
		// Token: 0x0600366A RID: 13930 RVA: 0x0008ACAF File Offset: 0x00088EAF
		private NoopWSManagerPerfCounters()
		{
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x0008ACB7 File Offset: 0x00088EB7
		public void CertifySuccessful(long elapsedMilliseconds)
		{
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x0008ACB9 File Offset: 0x00088EB9
		public void CertifyFailed()
		{
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x0008ACBB File Offset: 0x00088EBB
		public void GetClientLicensorCertSuccessful(long elapsedMilliseconds)
		{
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x0008ACBD File Offset: 0x00088EBD
		public void GetClientLicensorCertFailed()
		{
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x0008ACBF File Offset: 0x00088EBF
		public void AcquireLicenseSuccessful(long elapsedMilliseconds)
		{
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x0008ACC1 File Offset: 0x00088EC1
		public void AcquireLicenseFailed()
		{
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x0008ACC3 File Offset: 0x00088EC3
		public void AcquirePreLicenseSuccessful(long elapsedMilliseconds)
		{
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x0008ACC5 File Offset: 0x00088EC5
		public void AcquirePreLicenseFailed()
		{
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x0008ACC7 File Offset: 0x00088EC7
		public void AcquireTemplatesSuccessful(long elapsedMilliseconds)
		{
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x0008ACC9 File Offset: 0x00088EC9
		public void AcquireTemplatesFailed()
		{
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x0008ACCB File Offset: 0x00088ECB
		public void FindServiceLocationsSuccessful(long elapsedMilliseconds)
		{
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x0008ACCD File Offset: 0x00088ECD
		public void FindServiceLocationsFailed()
		{
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x0008ACCF File Offset: 0x00088ECF
		public void WCFCertifySuccessful()
		{
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x0008ACD1 File Offset: 0x00088ED1
		public void WCFCertifyFailed()
		{
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x0008ACD3 File Offset: 0x00088ED3
		public void WCFAcquireServerLicenseSuccessful()
		{
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x0008ACD5 File Offset: 0x00088ED5
		public void WCFAcquireServerLicenseFailed()
		{
		}

		// Token: 0x04002EAE RID: 11950
		public static NoopWSManagerPerfCounters Instance = new NoopWSManagerPerfCounters();
	}
}

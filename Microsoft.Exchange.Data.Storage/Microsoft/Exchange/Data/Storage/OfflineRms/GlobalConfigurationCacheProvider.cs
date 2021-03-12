using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.RightsManagementServices.Provider;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000ABF RID: 2751
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GlobalConfigurationCacheProvider : IGlobalConfigurationAndCacheProvider
	{
		// Token: 0x17001BAD RID: 7085
		// (get) Token: 0x06006436 RID: 25654 RVA: 0x001A8485 File Offset: 0x001A6685
		public TraceLevel DiagnosticTraceLevel
		{
			get
			{
				return TraceLevel.Off;
			}
		}

		// Token: 0x06006437 RID: 25655 RVA: 0x001A8488 File Offset: 0x001A6688
		public void CheckInIssuanceLicense(string signatureKey, ISizeTraceableItem issuanceLicense)
		{
		}

		// Token: 0x06006438 RID: 25656 RVA: 0x001A848A File Offset: 0x001A668A
		public ISizeTraceableItem CheckoutIssuanceLicense(string signatureKey)
		{
			return null;
		}

		// Token: 0x06006439 RID: 25657 RVA: 0x001A848D File Offset: 0x001A668D
		public void AddRejectedUser(string signatureKey, string rightsAccountCertificatePrincipal)
		{
		}

		// Token: 0x0600643A RID: 25658 RVA: 0x001A848F File Offset: 0x001A668F
		public bool IsRejectedUser(string signatureKey, string rightsAccountCertificatePrincipal)
		{
			return false;
		}

		// Token: 0x17001BAE RID: 7086
		// (get) Token: 0x0600643B RID: 25659 RVA: 0x001A8492 File Offset: 0x001A6692
		public int MaxNumberOfThreads
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x040038D7 RID: 14551
		private const int MaximumThreadNumberForCSPInstanceCaching = 20;
	}
}

using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002F6 RID: 758
	internal abstract class PlatformCallInfo
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600172B RID: 5931
		public abstract string CallId { get; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600172C RID: 5932
		public abstract PlatformTelephonyAddress CalledParty { get; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600172D RID: 5933
		public abstract PlatformTelephonyAddress CallingParty { get; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600172E RID: 5934
		public abstract ReadOnlyCollection<PlatformDiversionInfo> DiversionInfo { get; }

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600172F RID: 5935
		public abstract string FromTag { get; }

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001730 RID: 5936
		public abstract ReadOnlyCollection<PlatformSignalingHeader> RemoteHeaders { get; }

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001731 RID: 5937
		public abstract string RemoteUserAgent { get; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001732 RID: 5938
		public abstract PlatformSipUri RequestUri { get; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001733 RID: 5939
		public abstract string ToTag { get; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001734 RID: 5940
		public abstract X509Certificate RemoteCertificate { get; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001735 RID: 5941
		public abstract string ApplicationAor { get; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001736 RID: 5942
		// (set) Token: 0x06001737 RID: 5943
		public abstract string RemoteMatchedFQDN { get; set; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001738 RID: 5944
		public abstract IPAddress RemotePeer { get; }

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001739 RID: 5945
		public abstract bool IsInbound { get; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600173A RID: 5946
		public abstract bool IsServiceRequest { get; }
	}
}

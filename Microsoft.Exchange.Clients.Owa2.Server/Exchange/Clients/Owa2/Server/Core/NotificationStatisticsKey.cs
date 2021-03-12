using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200018E RID: 398
	internal sealed class NotificationStatisticsKey
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00035AF2 File Offset: 0x00033CF2
		// (set) Token: 0x06000E2C RID: 3628 RVA: 0x00035AFA File Offset: 0x00033CFA
		public NotificationLocation Location { get; private set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00035B03 File Offset: 0x00033D03
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x00035B0B File Offset: 0x00033D0B
		public Type PayloadType { get; private set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00035B14 File Offset: 0x00033D14
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x00035B1C File Offset: 0x00033D1C
		public bool IsReload { get; private set; }

		// Token: 0x06000E31 RID: 3633 RVA: 0x00035B25 File Offset: 0x00033D25
		public NotificationStatisticsKey(NotificationLocation location, Type payloadType, bool isReload)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			if (payloadType == null)
			{
				throw new ArgumentNullException("payloadType");
			}
			this.Location = location;
			this.PayloadType = payloadType;
			this.IsReload = isReload;
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00035B64 File Offset: 0x00033D64
		public override int GetHashCode()
		{
			return this.Location.GetHashCode() ^ this.PayloadType.GetHashCode() ^ this.IsReload.GetHashCode();
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00035B98 File Offset: 0x00033D98
		public override bool Equals(object obj)
		{
			NotificationStatisticsKey notificationStatisticsKey = obj as NotificationStatisticsKey;
			return notificationStatisticsKey != null && (this.Location.Equals(notificationStatisticsKey.Location) && this.PayloadType.Equals(notificationStatisticsKey.PayloadType)) && this.IsReload.Equals(notificationStatisticsKey.IsReload);
		}
	}
}

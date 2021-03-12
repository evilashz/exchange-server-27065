using System;
using System.Collections.Generic;
using System.Net;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000074 RID: 116
	public class AirSyncDeviceHealth
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0002521C File Offset: 0x0002341C
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x00025224 File Offset: 0x00023424
		public string DeviceID { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0002522D File Offset: 0x0002342D
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00025235 File Offset: 0x00023435
		public string UserEmail { get; set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0002523E File Offset: 0x0002343E
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x00025246 File Offset: 0x00023446
		public HttpStatusCode HttpStatus { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0002524F File Offset: 0x0002344F
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x00025257 File Offset: 0x00023457
		public string AirSyncStatus { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00025260 File Offset: 0x00023460
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x00025268 File Offset: 0x00023468
		public bool HasErrors { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00025271 File Offset: 0x00023471
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x00025279 File Offset: 0x00023479
		public List<ErrorDetail> ErrorDetails { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00025282 File Offset: 0x00023482
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0002528A File Offset: 0x0002348A
		public double RequestTime { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00025293 File Offset: 0x00023493
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0002529B File Offset: 0x0002349B
		public bool IsHanging { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x000252A4 File Offset: 0x000234A4
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x000252AC File Offset: 0x000234AC
		public string CommandName { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x000252B5 File Offset: 0x000234B5
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x000252BD File Offset: 0x000234BD
		public string StartTime { get; set; }

		// Token: 0x0600065A RID: 1626 RVA: 0x000252C6 File Offset: 0x000234C6
		public AirSyncDeviceHealth()
		{
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000252D0 File Offset: 0x000234D0
		public AirSyncDeviceHealth(ActiveSyncRequestData data)
		{
			this.CommandName = data.CommandName;
			this.DeviceID = data.DeviceID;
			this.ErrorDetails = data.ErrorDetails;
			this.HasErrors = data.HasErrors;
			this.IsHanging = data.IsHanging;
			this.HttpStatus = data.HttpStatus;
			this.AirSyncStatus = data.AirSyncStatus;
			this.RequestTime = data.RequestTime;
			this.UserEmail = data.UserEmail;
			this.StartTime = data.StartTime.ToString("yyyy-MM-ddTHH:mm:ss.fff");
		}
	}
}

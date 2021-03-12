using System;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x0200124C RID: 4684
	[DataServiceKey("TotalDevicesCount")]
	[Serializable]
	public sealed class MobileDeviceDashboardSummaryReport : FfoReportObject
	{
		// Token: 0x17003B9B RID: 15259
		// (get) Token: 0x0600BC97 RID: 48279 RVA: 0x002AD18B File Offset: 0x002AB38B
		// (set) Token: 0x0600BC98 RID: 48280 RVA: 0x002AD193 File Offset: 0x002AB393
		[DalConversion("DefaultSerializer", "Platform", new string[]
		{

		})]
		public string Platform { get; set; }

		// Token: 0x17003B9C RID: 15260
		// (get) Token: 0x0600BC99 RID: 48281 RVA: 0x002AD19C File Offset: 0x002AB39C
		// (set) Token: 0x0600BC9A RID: 48282 RVA: 0x002AD1A4 File Offset: 0x002AB3A4
		[DalConversion("DefaultSerializer", "TotalDevicesCount", new string[]
		{

		})]
		public int TotalDevicesCount { get; set; }

		// Token: 0x17003B9D RID: 15261
		// (get) Token: 0x0600BC9B RID: 48283 RVA: 0x002AD1AD File Offset: 0x002AB3AD
		// (set) Token: 0x0600BC9C RID: 48284 RVA: 0x002AD1B5 File Offset: 0x002AB3B5
		[DalConversion("DefaultSerializer", "AllowedDevicesCount", new string[]
		{

		})]
		public int AllowedDevicesCount { get; set; }

		// Token: 0x17003B9E RID: 15262
		// (get) Token: 0x0600BC9D RID: 48285 RVA: 0x002AD1BE File Offset: 0x002AB3BE
		// (set) Token: 0x0600BC9E RID: 48286 RVA: 0x002AD1C6 File Offset: 0x002AB3C6
		[DalConversion("DefaultSerializer", "BlockedDevicesCount", new string[]
		{

		})]
		public int BlockedDevicesCount { get; set; }

		// Token: 0x17003B9F RID: 15263
		// (get) Token: 0x0600BC9F RID: 48287 RVA: 0x002AD1CF File Offset: 0x002AB3CF
		// (set) Token: 0x0600BCA0 RID: 48288 RVA: 0x002AD1D7 File Offset: 0x002AB3D7
		[DalConversion("DefaultSerializer", "QuarantinedDevicesCount", new string[]
		{

		})]
		public int QuarantinedDevicesCount { get; set; }

		// Token: 0x17003BA0 RID: 15264
		// (get) Token: 0x0600BCA1 RID: 48289 RVA: 0x002AD1E0 File Offset: 0x002AB3E0
		// (set) Token: 0x0600BCA2 RID: 48290 RVA: 0x002AD1E8 File Offset: 0x002AB3E8
		[DalConversion("DefaultSerializer", "UnknownDevicesCount", new string[]
		{

		})]
		public int UnknownDevicesCount { get; set; }

		// Token: 0x17003BA1 RID: 15265
		// (get) Token: 0x0600BCA3 RID: 48291 RVA: 0x002AD1F1 File Offset: 0x002AB3F1
		// (set) Token: 0x0600BCA4 RID: 48292 RVA: 0x002AD1F9 File Offset: 0x002AB3F9
		[DalConversion("DefaultSerializer", "LastUpdatedTime", new string[]
		{

		})]
		public DateTime LastUpdatedTime { get; set; }

		// Token: 0x17003BA2 RID: 15266
		// (get) Token: 0x0600BCA5 RID: 48293 RVA: 0x002AD202 File Offset: 0x002AB402
		// (set) Token: 0x0600BCA6 RID: 48294 RVA: 0x002AD20A File Offset: 0x002AB40A
		[ODataInput("StartDate")]
		public DateTime? StartDate { get; set; }

		// Token: 0x17003BA3 RID: 15267
		// (get) Token: 0x0600BCA7 RID: 48295 RVA: 0x002AD213 File Offset: 0x002AB413
		// (set) Token: 0x0600BCA8 RID: 48296 RVA: 0x002AD21B File Offset: 0x002AB41B
		[ODataInput("EndDate")]
		public DateTime? EndDate { get; set; }
	}
}

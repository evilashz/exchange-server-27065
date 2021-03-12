using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data.MessageTrace;
using Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020001C4 RID: 452
	internal class DeviceSnapshot : Schema
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x00039C00 File Offset: 0x00037E00
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x00039C25 File Offset: 0x00037E25
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x00039C37 File Offset: 0x00037E37
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[DeviceSnapshot.OrganizationalUnitRootProperty];
			}
			set
			{
				this[DeviceSnapshot.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00039C4A File Offset: 0x00037E4A
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x00039C5C File Offset: 0x00037E5C
		public string Platform
		{
			get
			{
				return (string)this[DeviceSnapshot.PlatformProperty];
			}
			set
			{
				this[DeviceSnapshot.PlatformProperty] = value;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00039C6A File Offset: 0x00037E6A
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x00039C7C File Offset: 0x00037E7C
		public int TotalDevicesCount
		{
			get
			{
				return (int)this[DeviceSnapshot.TotalDevicesCountProperty];
			}
			set
			{
				this[DeviceSnapshot.TotalDevicesCountProperty] = value;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x00039C8F File Offset: 0x00037E8F
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x00039CA1 File Offset: 0x00037EA1
		public int AllowedDevicesCount
		{
			get
			{
				return (int)this[DeviceSnapshot.AllowedDevicesCountProperty];
			}
			set
			{
				this[DeviceSnapshot.AllowedDevicesCountProperty] = value;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x00039CB4 File Offset: 0x00037EB4
		// (set) Token: 0x0600131B RID: 4891 RVA: 0x00039CC6 File Offset: 0x00037EC6
		public int BlockedDevicesCount
		{
			get
			{
				return (int)this[DeviceSnapshot.BlockedDevicesCountProperty];
			}
			set
			{
				this[DeviceSnapshot.BlockedDevicesCountProperty] = value;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x00039CD9 File Offset: 0x00037ED9
		// (set) Token: 0x0600131D RID: 4893 RVA: 0x00039CEB File Offset: 0x00037EEB
		public int QuarantinedDevicesCount
		{
			get
			{
				return (int)this[DeviceSnapshot.QuarantinedDevicesCountProperty];
			}
			set
			{
				this[DeviceSnapshot.QuarantinedDevicesCountProperty] = value;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x00039CFE File Offset: 0x00037EFE
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x00039D10 File Offset: 0x00037F10
		public int UnknownDevicesCount
		{
			get
			{
				return (int)this[DeviceSnapshot.UnknownDevicesCountProperty];
			}
			set
			{
				this[DeviceSnapshot.UnknownDevicesCountProperty] = value;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00039D23 File Offset: 0x00037F23
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x00039D35 File Offset: 0x00037F35
		public DateTime LastUpdatedTime
		{
			get
			{
				return (DateTime)this[DeviceSnapshot.LastUpdatedTimeProperty];
			}
			set
			{
				this[DeviceSnapshot.LastUpdatedTimeProperty] = value;
			}
		}

		// Token: 0x04000925 RID: 2341
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = CommonReportingSchema.OrganizationalUnitRootProperty;

		// Token: 0x04000926 RID: 2342
		internal static readonly HygienePropertyDefinition PlatformProperty = new HygienePropertyDefinition("Platform", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000927 RID: 2343
		internal static readonly HygienePropertyDefinition TotalDevicesCountProperty = new HygienePropertyDefinition("TotalDevicesCount", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000928 RID: 2344
		internal static readonly HygienePropertyDefinition AllowedDevicesCountProperty = new HygienePropertyDefinition("AllowedDevicesCount", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000929 RID: 2345
		internal static readonly HygienePropertyDefinition BlockedDevicesCountProperty = new HygienePropertyDefinition("BlockedDevicesCount", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400092A RID: 2346
		internal static readonly HygienePropertyDefinition QuarantinedDevicesCountProperty = new HygienePropertyDefinition("QuarantinedDevicesCount", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400092B RID: 2347
		internal static readonly HygienePropertyDefinition UnknownDevicesCountProperty = new HygienePropertyDefinition("UnknownDevicesCount", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400092C RID: 2348
		internal static readonly HygienePropertyDefinition LastUpdatedTimeProperty = new HygienePropertyDefinition("LastUpdatedTime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}

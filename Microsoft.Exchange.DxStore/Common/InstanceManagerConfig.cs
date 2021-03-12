using System;
using System.Runtime.Serialization;
using System.ServiceModel.Description;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000038 RID: 56
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class InstanceManagerConfig
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000387A File Offset: 0x00001A7A
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00003882 File Offset: 0x00001A82
		public IServerNameResolver NameResolver { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000388B File Offset: 0x00001A8B
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00003893 File Offset: 0x00001A93
		[DataMember]
		public string Self { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000389C File Offset: 0x00001A9C
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000038A4 File Offset: 0x00001AA4
		[DataMember]
		public bool IsZeroboxMode { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000038AD File Offset: 0x00001AAD
		// (set) Token: 0x060001CA RID: 458 RVA: 0x000038B5 File Offset: 0x00001AB5
		[DataMember]
		public string ComponentName { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000038BE File Offset: 0x00001ABE
		// (set) Token: 0x060001CC RID: 460 RVA: 0x000038C6 File Offset: 0x00001AC6
		[DataMember]
		public string NetworkAddress { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000038CF File Offset: 0x00001ACF
		// (set) Token: 0x060001CE RID: 462 RVA: 0x000038D7 File Offset: 0x00001AD7
		[DataMember]
		public string BaseStorageDir { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000038E0 File Offset: 0x00001AE0
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000038E8 File Offset: 0x00001AE8
		[DataMember]
		public TimeSpan InstanceMonitorInterval { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000038F1 File Offset: 0x00001AF1
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000038F9 File Offset: 0x00001AF9
		[DataMember]
		public int EndpointPortNumber { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00003902 File Offset: 0x00001B02
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000390A File Offset: 0x00001B0A
		[DataMember]
		public string EndpointProtocolName { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00003913 File Offset: 0x00001B13
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000391B File Offset: 0x00001B1B
		[DataMember]
		public WcfTimeout DefaultTimeout { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00003924 File Offset: 0x00001B24
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000392C File Offset: 0x00001B2C
		[DataMember]
		public TimeSpan ManagerStopTimeout { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00003935 File Offset: 0x00001B35
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000393D File Offset: 0x00001B3D
		[DataMember]
		public CommonSettings Settings { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00003946 File Offset: 0x00001B46
		public string Identity
		{
			get
			{
				return string.Format("Manager/{0}/{1}", this.ComponentName, this.Self);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000395E File Offset: 0x00001B5E
		public virtual ServiceEndpoint GetEndpoint(string target, bool isServerBinding, WcfTimeout timeout = null)
		{
			return EndpointBuilder.GetStoreManagerEndpoint(this, target, isServerBinding, timeout);
		}

		// Token: 0x02000039 RID: 57
		public static class PropertyNames
		{
			// Token: 0x04000124 RID: 292
			public const string BaseStorageDir = "BaseStorageDir";

			// Token: 0x04000125 RID: 293
			public const string DefaultTimeout = "DefaultTimeout";

			// Token: 0x04000126 RID: 294
			public const string EndpointPortNumber = "EndpointPortNumber";

			// Token: 0x04000127 RID: 295
			public const string EndpointProtocolName = "EndpointProtocolName";

			// Token: 0x04000128 RID: 296
			public const string InstanceMonitorIntervalInMSec = "InstanceMonitorIntervalInMSec";

			// Token: 0x04000129 RID: 297
			public const string NetworkAddress = "NetworkAddress";

			// Token: 0x0400012A RID: 298
			public const string ManagerStopTimeoutInMSec = "ManagerStopTimeoutInMSec";
		}

		// Token: 0x0200003A RID: 58
		public static class ContainerNames
		{
			// Token: 0x0400012B RID: 299
			public const string Settings = "Settings";

			// Token: 0x0400012C RID: 300
			public const string Groups = "Groups";
		}
	}
}

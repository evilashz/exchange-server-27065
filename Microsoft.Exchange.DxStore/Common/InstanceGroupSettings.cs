using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000036 RID: 54
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class InstanceGroupSettings : CommonSettings
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00003850 File Offset: 0x00001A50
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00003858 File Offset: 0x00001A58
		[DataMember]
		public string PaxosStorageDir { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00003861 File Offset: 0x00001A61
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00003869 File Offset: 0x00001A69
		[DataMember]
		public string SnapshotStorageDir { get; set; }

		// Token: 0x02000037 RID: 55
		public static class GroupSpecificPropertyNames
		{
			// Token: 0x04000116 RID: 278
			public const string PaxosStorageDir = "PaxosStorageDir";

			// Token: 0x04000117 RID: 279
			public const string SnapshotStorageDir = "SnapshotStorageDir";
		}
	}
}

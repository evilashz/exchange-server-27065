using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000032 RID: 50
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class InstanceGroupMemberConfig
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00003510 File Offset: 0x00001710
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00003518 File Offset: 0x00001718
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00003521 File Offset: 0x00001721
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00003529 File Offset: 0x00001729
		[DataMember]
		public string NetworkAddress { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00003532 File Offset: 0x00001732
		// (set) Token: 0x06000161 RID: 353 RVA: 0x0000353A File Offset: 0x0000173A
		[DataMember]
		public bool IsWitness { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00003543 File Offset: 0x00001743
		// (set) Token: 0x06000163 RID: 355 RVA: 0x0000354B File Offset: 0x0000174B
		[DataMember]
		public bool IsManagedExternally { get; set; }

		// Token: 0x02000033 RID: 51
		public static class PropertyNames
		{
			// Token: 0x040000BB RID: 187
			public const string IsManagedExternally = "IsManagedExternally";

			// Token: 0x040000BC RID: 188
			public const string IsWitness = "IsWitness";

			// Token: 0x040000BD RID: 189
			public const string NetworkAddress = "NetworkAddress";
		}
	}
}

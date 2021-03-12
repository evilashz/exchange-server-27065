using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000063 RID: 99
	internal class DeviceBehaviorData : ComponentData<DeviceBehavior>
	{
		// Token: 0x0600057C RID: 1404 RVA: 0x00020A8D File Offset: 0x0001EC8D
		public DeviceBehaviorData() : base(new DeviceBehavior())
		{
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00020A9A File Offset: 0x0001EC9A
		public DeviceBehaviorData(DeviceBehavior data) : base(data)
		{
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00020AA3 File Offset: 0x0001ECA3
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x00020AAA File Offset: 0x0001ECAA
		public override ushort TypeId
		{
			get
			{
				return DeviceBehaviorData.typeId;
			}
			set
			{
				DeviceBehaviorData.typeId = value;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00020AB2 File Offset: 0x0001ECB2
		public override ICustomSerializable BuildObject()
		{
			return new DeviceBehaviorData();
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00020AB9 File Offset: 0x0001ECB9
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			base.Data.DeserializeData(reader, componentDataPool);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00020AC8 File Offset: 0x0001ECC8
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			base.Data.SerializeData(writer, componentDataPool);
		}

		// Token: 0x040003DB RID: 987
		private static ushort typeId;
	}
}

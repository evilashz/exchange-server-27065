using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000DF RID: 223
	internal class MeetingOrganizerInfoData : ComponentData<MeetingOrganizerInfo>
	{
		// Token: 0x06000CC4 RID: 3268 RVA: 0x0004476E File Offset: 0x0004296E
		public MeetingOrganizerInfoData() : base(new MeetingOrganizerInfo())
		{
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0004477B File Offset: 0x0004297B
		public MeetingOrganizerInfoData(MeetingOrganizerInfo data) : base(data)
		{
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00044784 File Offset: 0x00042984
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x0004478B File Offset: 0x0004298B
		public override ushort TypeId
		{
			get
			{
				return MeetingOrganizerInfoData.typeId;
			}
			set
			{
				MeetingOrganizerInfoData.typeId = value;
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00044793 File Offset: 0x00042993
		public override ICustomSerializable BuildObject()
		{
			return new MeetingOrganizerInfoData();
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0004479A File Offset: 0x0004299A
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			base.Data.DeserializeData(reader, componentDataPool);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x000447A9 File Offset: 0x000429A9
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			base.Data.SerializeData(writer, componentDataPool);
		}

		// Token: 0x040007E7 RID: 2023
		private static ushort typeId;
	}
}

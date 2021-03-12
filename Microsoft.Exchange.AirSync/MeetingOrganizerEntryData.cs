using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000DD RID: 221
	internal class MeetingOrganizerEntryData : ComponentData<MeetingOrganizerEntry>
	{
		// Token: 0x06000CA7 RID: 3239 RVA: 0x00043F02 File Offset: 0x00042102
		public MeetingOrganizerEntryData() : base(new MeetingOrganizerEntry())
		{
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00043F0F File Offset: 0x0004210F
		public MeetingOrganizerEntryData(MeetingOrganizerEntry data) : base(data)
		{
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00043F18 File Offset: 0x00042118
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x00043F1F File Offset: 0x0004211F
		public override ushort TypeId
		{
			get
			{
				return MeetingOrganizerEntryData.typeId;
			}
			set
			{
				MeetingOrganizerEntryData.typeId = value;
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00043F27 File Offset: 0x00042127
		public override ICustomSerializable BuildObject()
		{
			return new MeetingOrganizerEntryData();
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00043F2E File Offset: 0x0004212E
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			base.Data.DeserializeData(reader, componentDataPool);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00043F3D File Offset: 0x0004213D
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			base.Data.SerializeData(writer, componentDataPool);
		}

		// Token: 0x040007DE RID: 2014
		private static ushort typeId;
	}
}

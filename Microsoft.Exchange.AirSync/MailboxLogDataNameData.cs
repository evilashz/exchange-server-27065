using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000D5 RID: 213
	internal class MailboxLogDataNameData : ComponentData<MailboxLogDataName>
	{
		// Token: 0x06000C4D RID: 3149 RVA: 0x00041244 File Offset: 0x0003F444
		public MailboxLogDataNameData()
		{
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0004124C File Offset: 0x0003F44C
		public MailboxLogDataNameData(MailboxLogDataName data) : base(data)
		{
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00041255 File Offset: 0x0003F455
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x0004125C File Offset: 0x0003F45C
		public override ushort TypeId
		{
			get
			{
				return MailboxLogDataNameData.typeId;
			}
			set
			{
				MailboxLogDataNameData.typeId = value;
			}
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00041264 File Offset: 0x0003F464
		public override ICustomSerializable BuildObject()
		{
			return new MailboxLogDataNameData();
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0004126B File Offset: 0x0003F46B
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			base.Data = (MailboxLogDataName)reader.ReadInt32();
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00041279 File Offset: 0x0003F479
		public override void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			writer.Write((int)base.Data);
		}

		// Token: 0x0400078F RID: 1935
		private static ushort typeId;
	}
}

using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000258 RID: 600
	internal class FxProxyPoolSetItemPropertiesMessage : DataMessageBase
	{
		// Token: 0x06001EBB RID: 7867 RVA: 0x0003FB11 File Offset: 0x0003DD11
		public FxProxyPoolSetItemPropertiesMessage(ItemPropertiesBase props)
		{
			this.Props = props;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x0003FB20 File Offset: 0x0003DD20
		private FxProxyPoolSetItemPropertiesMessage(byte[] blob)
		{
			this.Props = CommonUtils.DataContractDeserialize<ItemPropertiesBase>(blob);
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06001EBD RID: 7869 RVA: 0x0003FB34 File Offset: 0x0003DD34
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolSetItemProperties
				};
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x0003FB4E File Offset: 0x0003DD4E
		// (set) Token: 0x06001EBF RID: 7871 RVA: 0x0003FB56 File Offset: 0x0003DD56
		public ItemPropertiesBase Props { get; private set; }

		// Token: 0x06001EC0 RID: 7872 RVA: 0x0003FB5F File Offset: 0x0003DD5F
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new FxProxyPoolSetItemPropertiesMessage(data);
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x0003FB68 File Offset: 0x0003DD68
		protected override int GetSizeInternal()
		{
			byte[] array = CommonUtils.DataContractSerialize<ItemPropertiesBase>(this.Props);
			return array.Length;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0003FB84 File Offset: 0x0003DD84
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolSetItemProperties;
			data = CommonUtils.DataContractSerialize<ItemPropertiesBase>(this.Props);
		}
	}
}

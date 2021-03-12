using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000259 RID: 601
	internal class FxProxyPoolSetPropsMessage : DataMessageBase
	{
		// Token: 0x06001EC3 RID: 7875 RVA: 0x0003FB97 File Offset: 0x0003DD97
		public FxProxyPoolSetPropsMessage(PropValueData[] pvda)
		{
			this.pvda = pvda;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0003FBA6 File Offset: 0x0003DDA6
		private FxProxyPoolSetPropsMessage(byte[] blob)
		{
			this.pvda = CommonUtils.DataContractDeserialize<PropValueData[]>(blob);
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x0003FBBC File Offset: 0x0003DDBC
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolSetProps
				};
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x0003FBD6 File Offset: 0x0003DDD6
		public PropValueData[] PropValues
		{
			get
			{
				return this.pvda;
			}
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x0003FBDE File Offset: 0x0003DDDE
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new FxProxyPoolSetPropsMessage(data);
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x0003FBE8 File Offset: 0x0003DDE8
		protected override int GetSizeInternal()
		{
			int num = 0;
			foreach (PropValueData propValueData in this.pvda)
			{
				num += propValueData.GetApproximateSize();
			}
			return num;
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x0003FC1A File Offset: 0x0003DE1A
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolSetProps;
			data = CommonUtils.DataContractSerialize<PropValueData[]>(this.pvda);
		}

		// Token: 0x04000C76 RID: 3190
		private PropValueData[] pvda;
	}
}

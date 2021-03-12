using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200024D RID: 589
	internal class FxProxyPoolCreateItemMessage : DataMessageBase
	{
		// Token: 0x06001E7A RID: 7802 RVA: 0x0003F3AD File Offset: 0x0003D5AD
		private FxProxyPoolCreateItemMessage(bool createFAI)
		{
			this.createFAI = createFAI;
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x0003F3BC File Offset: 0x0003D5BC
		public static FxProxyPoolCreateItemMessage Regular
		{
			get
			{
				return FxProxyPoolCreateItemMessage.instanceRegular;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x0003F3C3 File Offset: 0x0003D5C3
		public static FxProxyPoolCreateItemMessage FAI
		{
			get
			{
				return FxProxyPoolCreateItemMessage.instanceFAI;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x0003F3CC File Offset: 0x0003D5CC
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolCreateFAIItem,
					DataMessageOpcode.FxProxyPoolCreateItem
				};
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x0003F3EB File Offset: 0x0003D5EB
		public bool CreateFAI
		{
			get
			{
				return this.createFAI;
			}
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0003F3F3 File Offset: 0x0003D5F3
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			if (opcode != DataMessageOpcode.FxProxyPoolCreateFAIItem)
			{
				return FxProxyPoolCreateItemMessage.Regular;
			}
			return FxProxyPoolCreateItemMessage.FAI;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x0003F405 File Offset: 0x0003D605
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = (this.createFAI ? DataMessageOpcode.FxProxyPoolCreateFAIItem : DataMessageOpcode.FxProxyPoolCreateItem);
			data = null;
		}

		// Token: 0x04000C69 RID: 3177
		private static FxProxyPoolCreateItemMessage instanceRegular = new FxProxyPoolCreateItemMessage(false);

		// Token: 0x04000C6A RID: 3178
		private static FxProxyPoolCreateItemMessage instanceFAI = new FxProxyPoolCreateItemMessage(true);

		// Token: 0x04000C6B RID: 3179
		private bool createFAI;
	}
}

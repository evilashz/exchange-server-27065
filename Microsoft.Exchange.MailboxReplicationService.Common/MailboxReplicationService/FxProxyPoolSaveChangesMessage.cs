using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000255 RID: 597
	internal class FxProxyPoolSaveChangesMessage : DataMessageBase
	{
		// Token: 0x06001EA8 RID: 7848 RVA: 0x0003F98C File Offset: 0x0003DB8C
		private FxProxyPoolSaveChangesMessage()
		{
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x0003F994 File Offset: 0x0003DB94
		public static FxProxyPoolSaveChangesMessage Instance
		{
			get
			{
				return FxProxyPoolSaveChangesMessage.instance;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x0003F99C File Offset: 0x0003DB9C
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolSaveChanges
				};
			}
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0003F9B6 File Offset: 0x0003DBB6
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return FxProxyPoolSaveChangesMessage.Instance;
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x0003F9BD File Offset: 0x0003DBBD
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolSaveChanges;
			data = null;
		}

		// Token: 0x04000C70 RID: 3184
		private static FxProxyPoolSaveChangesMessage instance = new FxProxyPoolSaveChangesMessage();
	}
}

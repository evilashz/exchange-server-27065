using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000245 RID: 581
	internal static class DataMessageSerializer
	{
		// Token: 0x06001E50 RID: 7760 RVA: 0x0003ED68 File Offset: 0x0003CF68
		static DataMessageSerializer()
		{
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FlushMessage.Deserialize), FlushMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyImportBufferMessage.Deserialize), FxProxyImportBufferMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyGetObjectDataRequestMessage.Deserialize), FxProxyGetObjectDataRequestMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyGetObjectDataResponseMessage.Deserialize), FxProxyGetObjectDataResponseMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolOpenFolderMessage.Deserialize), FxProxyPoolOpenFolderMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolCloseEntryMessage.Deserialize), FxProxyPoolCloseEntryMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolOpenItemMessage.Deserialize), FxProxyPoolOpenItemMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolCreateFolderMessage.Deserialize), FxProxyPoolCreateFolderMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolSetExtendedAclMessage.Deserialize), FxProxyPoolSetExtendedAclMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolCreateItemMessage.Deserialize), FxProxyPoolCreateItemMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolSetPropsMessage.Deserialize), FxProxyPoolSetPropsMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolSetItemPropertiesMessage.Deserialize), FxProxyPoolSetItemPropertiesMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolSaveChangesMessage.Deserialize), FxProxyPoolSaveChangesMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolWriteToMimeMessage.Deserialize), FxProxyPoolWriteToMimeMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolDeleteItemMessage.Deserialize), FxProxyPoolDeleteItemMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolGetFolderDataRequestMessage.Deserialize), FxProxyPoolGetFolderDataRequestMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolGetFolderDataResponseMessage.Deserialize), FxProxyPoolGetFolderDataResponseMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolGetUploadedIDsRequestMessage.Deserialize), FxProxyPoolGetUploadedIDsRequestMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(FxProxyPoolGetUploadedIDsResponseMessage.Deserialize), FxProxyPoolGetUploadedIDsResponseMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(BufferBatchMessage.Deserialize), BufferBatchMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(PagedDataMessage.Deserialize), PagedDataMessage.SupportedOpcodes);
			DataMessageSerializer.RegisterType(new DataMessageSerializer.CreateDataMessageDelegate(MessageExportResultsMessage.Deserialize), MessageExportResultsMessage.SupportedOpcodes);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0003EF64 File Offset: 0x0003D164
		public static IDataMessage Deserialize(int opcode, byte[] data, bool useCompression)
		{
			DataMessageSerializer.CreateDataMessageDelegate createDataMessageDelegate;
			if (DataMessageSerializer.createMessageByOpcode.TryGetValue((DataMessageOpcode)opcode, out createDataMessageDelegate))
			{
				return createDataMessageDelegate((DataMessageOpcode)opcode, data, useCompression);
			}
			throw new InputDataIsInvalidPermanentException();
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0003EF94 File Offset: 0x0003D194
		private static void RegisterType(DataMessageSerializer.CreateDataMessageDelegate createDelegate, DataMessageOpcode[] opcodes)
		{
			foreach (DataMessageOpcode key in opcodes)
			{
				DataMessageSerializer.createMessageByOpcode.Add(key, createDelegate);
			}
		}

		// Token: 0x04000C63 RID: 3171
		private static Dictionary<DataMessageOpcode, DataMessageSerializer.CreateDataMessageDelegate> createMessageByOpcode = new Dictionary<DataMessageOpcode, DataMessageSerializer.CreateDataMessageDelegate>();

		// Token: 0x02000246 RID: 582
		// (Invoke) Token: 0x06001E54 RID: 7764
		private delegate IDataMessage CreateDataMessageDelegate(DataMessageOpcode opcode, byte[] data, bool useCompression);
	}
}

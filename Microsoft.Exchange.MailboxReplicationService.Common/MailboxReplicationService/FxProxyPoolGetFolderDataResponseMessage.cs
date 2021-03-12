using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000250 RID: 592
	internal class FxProxyPoolGetFolderDataResponseMessage : DataMessageBase
	{
		// Token: 0x06001E8C RID: 7820 RVA: 0x0003F4B8 File Offset: 0x0003D6B8
		public FxProxyPoolGetFolderDataResponseMessage(EntryIdMap<byte[]> folderData)
		{
			this.folderData = folderData;
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x0003F4C8 File Offset: 0x0003D6C8
		private FxProxyPoolGetFolderDataResponseMessage(byte[] blob)
		{
			this.folderData = new EntryIdMap<byte[]>();
			using (MemoryStream memoryStream = new MemoryStream(blob))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					int num = CommonUtils.ReadInt(binaryReader);
					for (int i = 0; i < num; i++)
					{
						byte[] array = CommonUtils.ReadBlob(binaryReader);
						byte[] value = CommonUtils.ReadBlob(binaryReader);
						if (array == null || this.folderData.ContainsKey(array))
						{
							throw new InputDataIsInvalidPermanentException();
						}
						this.folderData.Add(array, value);
					}
				}
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x0003F574 File Offset: 0x0003D774
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolGetFolderDataResponse
				};
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x0003F591 File Offset: 0x0003D791
		public EntryIdMap<byte[]> FolderData
		{
			get
			{
				return this.folderData;
			}
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0003F599 File Offset: 0x0003D799
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new FxProxyPoolGetFolderDataResponseMessage(data);
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x0003F5A4 File Offset: 0x0003D7A4
		protected override int GetSizeInternal()
		{
			int num = 0;
			foreach (KeyValuePair<byte[], byte[]> keyValuePair in this.folderData)
			{
				num += ((keyValuePair.Key != null) ? keyValuePair.Key.Length : 0) + ((keyValuePair.Value != null) ? keyValuePair.Value.Length : 0);
			}
			return num;
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x0003F624 File Offset: 0x0003D824
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolGetFolderDataResponse;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(this.folderData.Count);
					foreach (KeyValuePair<byte[], byte[]> keyValuePair in this.folderData)
					{
						CommonUtils.WriteBlob(binaryWriter, keyValuePair.Key);
						CommonUtils.WriteBlob(binaryWriter, keyValuePair.Value);
					}
					binaryWriter.Flush();
					data = memoryStream.ToArray();
				}
			}
		}

		// Token: 0x04000C6D RID: 3181
		private EntryIdMap<byte[]> folderData;
	}
}

using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000252 RID: 594
	internal class FxProxyPoolGetUploadedIDsResponseMessage : DataMessageBase
	{
		// Token: 0x06001E99 RID: 7833 RVA: 0x0003F73C File Offset: 0x0003D93C
		public FxProxyPoolGetUploadedIDsResponseMessage(List<byte[]> entryIDs)
		{
			this.entryIDs = entryIDs;
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x0003F74C File Offset: 0x0003D94C
		private FxProxyPoolGetUploadedIDsResponseMessage(byte[] blob)
		{
			this.entryIDs = new List<byte[]>();
			using (MemoryStream memoryStream = new MemoryStream(blob))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					int num = CommonUtils.ReadInt(binaryReader);
					for (int i = 0; i < num; i++)
					{
						this.entryIDs.Add(CommonUtils.ReadBlob(binaryReader));
					}
				}
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0003F7D0 File Offset: 0x0003D9D0
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.FxProxyPoolGetUploadedIDsResponse
				};
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x0003F7ED File Offset: 0x0003D9ED
		public List<byte[]> EntryIDs
		{
			get
			{
				return this.entryIDs;
			}
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x0003F7F5 File Offset: 0x0003D9F5
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new FxProxyPoolGetUploadedIDsResponseMessage(data);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x0003F800 File Offset: 0x0003DA00
		protected override int GetSizeInternal()
		{
			int num = 0;
			foreach (byte[] array in this.entryIDs)
			{
				num += ((array != null) ? array.Length : 0);
			}
			return num;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x0003F85C File Offset: 0x0003DA5C
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.FxProxyPoolGetUploadedIDsResponse;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(this.entryIDs.Count);
					foreach (byte[] blob in this.entryIDs)
					{
						CommonUtils.WriteBlob(binaryWriter, blob);
					}
					binaryWriter.Flush();
					data = memoryStream.ToArray();
				}
			}
		}

		// Token: 0x04000C6F RID: 3183
		private List<byte[]> entryIDs;
	}
}

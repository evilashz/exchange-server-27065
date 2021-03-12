using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000010 RID: 16
	internal class PagedTransmitter : IDataExport
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00007E0D File Offset: 0x0000600D
		public PagedTransmitter(string data, bool useCompression)
		{
			this.Initialize(CommonUtils.PackString(data, useCompression));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007E22 File Offset: 0x00006022
		public PagedTransmitter(byte[] data, bool useCompression)
		{
			this.Initialize(useCompression ? CommonUtils.CompressData(data) : data);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00007E3C File Offset: 0x0000603C
		DataExportBatch IDataExport.ExportData()
		{
			if (this.curIndex == -1)
			{
				throw new UnexpectedErrorPermanentException(-2147024809);
			}
			DataExportBatch dataExportBatch = new DataExportBatch();
			if (this.dataToSend == null || this.curIndex + this.chunkSize >= this.dataToSend.Length)
			{
				dataExportBatch.Opcode = 211;
				if (this.dataToSend == null || this.curIndex == 0)
				{
					dataExportBatch.Data = this.dataToSend;
				}
				else
				{
					int num = this.dataToSend.Length - this.curIndex;
					dataExportBatch.Data = new byte[num];
					Array.Copy(this.dataToSend, this.curIndex, dataExportBatch.Data, 0, num);
				}
				dataExportBatch.IsLastBatch = true;
				this.curIndex = -1;
			}
			else
			{
				dataExportBatch.Opcode = 210;
				dataExportBatch.Data = new byte[this.chunkSize];
				Array.Copy(this.dataToSend, this.curIndex, dataExportBatch.Data, 0, this.chunkSize);
				this.curIndex += this.chunkSize;
			}
			return dataExportBatch;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007F3E File Offset: 0x0000613E
		void IDataExport.CancelExport()
		{
			this.curIndex = -1;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007F47 File Offset: 0x00006147
		private void Initialize(byte[] dataToSend)
		{
			this.dataToSend = dataToSend;
			this.chunkSize = 1000000;
			this.curIndex = 0;
		}

		// Token: 0x0400003A RID: 58
		private const int DefaultChunkSize = 1000000;

		// Token: 0x0400003B RID: 59
		private byte[] dataToSend;

		// Token: 0x0400003C RID: 60
		private int curIndex;

		// Token: 0x0400003D RID: 61
		private int chunkSize;
	}
}

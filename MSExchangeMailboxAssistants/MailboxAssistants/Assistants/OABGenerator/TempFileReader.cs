using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.OAB;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001FA RID: 506
	internal sealed class TempFileReader : IDisposable
	{
		// Token: 0x06001382 RID: 4994 RVA: 0x00071C7C File Offset: 0x0006FE7C
		public TempFileReader(GenerationStats stats, FileStream tempFileStream)
		{
			this.stats = stats;
			this.tempFileStream = tempFileStream;
			this.ioCostStream = new IOCostStream(new NoCloseStream(this.tempFileStream));
			this.fileSystemPerformanceTracker = new FileSystemPerformanceTracker("ProduceSortedFlatFile", this.ioCostStream, this.stats);
			this.binaryReader = new BinaryReader(this.ioCostStream);
			this.records = new Queue<TempFileReader.TempFileRecord>(Globals.TempFileRecordBatchSize);
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x00071CF0 File Offset: 0x0006FEF0
		public string FileName
		{
			get
			{
				return this.tempFileStream.Name;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x00071CFD File Offset: 0x0006FEFD
		public bool NoMoreRecords
		{
			get
			{
				return this.readAllRecords && this.records.Count == 0;
			}
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00071D18 File Offset: 0x0006FF18
		public TempFileReader.TempFileRecord PeekNextRecord()
		{
			TempFileReader.TempFileRecord result = null;
			if (this.records.Count == 0 && !this.readAllRecords)
			{
				this.ReadRecords();
			}
			if (this.records.Count > 0)
			{
				result = this.records.Peek();
			}
			return result;
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00071D60 File Offset: 0x0006FF60
		public TempFileReader.TempFileRecord ReadNextRecord()
		{
			TempFileReader.TempFileRecord result = null;
			if (this.records.Count == 0 && !this.readAllRecords)
			{
				this.ReadRecords();
			}
			if (this.records.Count > 0)
			{
				result = this.records.Dequeue();
			}
			return result;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00071DA8 File Offset: 0x0006FFA8
		public void Dispose()
		{
			if (!this.disposed)
			{
				if (this.binaryReader != null)
				{
					this.binaryReader.Dispose();
					this.binaryReader = null;
				}
				if (this.fileSystemPerformanceTracker != null)
				{
					this.fileSystemPerformanceTracker.Dispose();
					this.fileSystemPerformanceTracker = null;
				}
				if (this.ioCostStream != null)
				{
					this.stats.IODuration += this.ioCostStream.Reading;
					this.ioCostStream.Dispose();
					this.ioCostStream = null;
				}
			}
			this.disposed = true;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00071E34 File Offset: 0x00070034
		private void ReadRecords()
		{
			for (int i = 0; i < Globals.TempFileRecordBatchSize; i++)
			{
				if (this.binaryReader.BaseStream.Position == this.binaryReader.BaseStream.Length)
				{
					this.readAllRecords = true;
					break;
				}
				TempFileReader.TempFileRecord tempFileRecord = new TempFileReader.TempFileRecord();
				tempFileRecord.ObjectGuid = this.binaryReader.ReadGuid("AddressListRecord");
				uint num = this.binaryReader.ReadUInt32();
				tempFileRecord.Data = new byte[num];
				BitConverter.GetBytes(num).CopyTo(tempFileRecord.Data, 0);
				this.binaryReader.ReadBytes((int)(num - 4U)).CopyTo(tempFileRecord.Data, 4);
				this.records.Enqueue(tempFileRecord);
			}
			if (this.binaryReader.BaseStream.Position == this.binaryReader.BaseStream.Length)
			{
				this.readAllRecords = true;
			}
		}

		// Token: 0x04000BE7 RID: 3047
		private GenerationStats stats;

		// Token: 0x04000BE8 RID: 3048
		private FileStream tempFileStream;

		// Token: 0x04000BE9 RID: 3049
		private IOCostStream ioCostStream;

		// Token: 0x04000BEA RID: 3050
		private FileSystemPerformanceTracker fileSystemPerformanceTracker;

		// Token: 0x04000BEB RID: 3051
		private Queue<TempFileReader.TempFileRecord> records;

		// Token: 0x04000BEC RID: 3052
		private BinaryReader binaryReader;

		// Token: 0x04000BED RID: 3053
		private bool readAllRecords;

		// Token: 0x04000BEE RID: 3054
		private bool disposed;

		// Token: 0x020001FB RID: 507
		internal sealed class TempFileRecord
		{
			// Token: 0x04000BEF RID: 3055
			public Guid ObjectGuid;

			// Token: 0x04000BF0 RID: 3056
			public byte[] Data;
		}
	}
}

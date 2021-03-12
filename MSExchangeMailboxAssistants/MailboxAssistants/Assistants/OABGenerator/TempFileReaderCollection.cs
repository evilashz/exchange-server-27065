using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.OAB;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001FC RID: 508
	internal class TempFileReaderCollection : IDisposable
	{
		// Token: 0x0600138A RID: 5002 RVA: 0x00071F20 File Offset: 0x00070120
		public TempFileReaderCollection()
		{
			this.topRecords = new SortedDictionary<Guid, TempFileReader>();
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00071F34 File Offset: 0x00070134
		public void Initialize(GenerationStats stats, List<FileStream> tempFiles)
		{
			ExTraceGlobals.AssistantTracer.TraceFunction((long)this.GetHashCode(), "TempFileReaderCollection.Initialize start");
			foreach (FileStream fileStream in tempFiles)
			{
				fileStream.Seek(0L, SeekOrigin.Begin);
				TempFileReader tempFileReader = new TempFileReader(stats, fileStream);
				ExTraceGlobals.AssistantTracer.TraceDebug<string>((long)this.GetHashCode(), "Adding TempFileReader({0}) to collection", tempFileReader.FileName);
				this.AddTempFileReaderToSortedList(tempFileReader);
			}
			ExTraceGlobals.AssistantTracer.TraceFunction((long)this.GetHashCode(), "TempFileReaderCollection.Initialize end");
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00071FDC File Offset: 0x000701DC
		public byte[] GetNextRecord()
		{
			if (this.topRecords.Count == 0)
			{
				return null;
			}
			TempFileReader value;
			using (SortedDictionary<Guid, TempFileReader>.Enumerator enumerator = this.topRecords.GetEnumerator())
			{
				enumerator.MoveNext();
				KeyValuePair<Guid, TempFileReader> keyValuePair = enumerator.Current;
				value = keyValuePair.Value;
				SortedDictionary<Guid, TempFileReader> sortedDictionary = this.topRecords;
				KeyValuePair<Guid, TempFileReader> keyValuePair2 = enumerator.Current;
				sortedDictionary.Remove(keyValuePair2.Key);
			}
			TempFileReader.TempFileRecord tempFileRecord = value.ReadNextRecord();
			this.AddTempFileReaderToSortedList(value);
			return tempFileRecord.Data;
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0007206C File Offset: 0x0007026C
		private void AddTempFileReaderToSortedList(TempFileReader tempFileReader)
		{
			bool flag;
			do
			{
				flag = true;
				TempFileReader.TempFileRecord tempFileRecord = null;
				if (!tempFileReader.NoMoreRecords)
				{
					tempFileRecord = tempFileReader.PeekNextRecord();
				}
				if (tempFileRecord != null)
				{
					if (!this.topRecords.ContainsKey(tempFileRecord.ObjectGuid))
					{
						this.topRecords.Add(tempFileRecord.ObjectGuid, tempFileReader);
					}
					else
					{
						flag = false;
						TempFileReader.TempFileRecord tempFileRecord2 = tempFileReader.ReadNextRecord();
						ExTraceGlobals.AssistantTracer.TraceError<string, Guid>((long)this.GetHashCode(), "TempFileReader({0}) contained a record with ObjectGuid {1} which already exists in the SortedDictionary<> topRecords.  This record is being discarded.", tempFileReader.FileName, tempFileRecord2.ObjectGuid);
					}
				}
				else
				{
					ExTraceGlobals.AssistantTracer.TraceDebug<string>((long)this.GetHashCode(), "TempFileReader({0}) has no more entries; disposing it", tempFileReader.FileName);
					tempFileReader.Dispose();
				}
			}
			while (!flag);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0007210C File Offset: 0x0007030C
		public void Dispose()
		{
			if (!this.disposed)
			{
				foreach (KeyValuePair<Guid, TempFileReader> keyValuePair in this.topRecords)
				{
					if (keyValuePair.Value != null)
					{
						ExTraceGlobals.AssistantTracer.TraceError<string>((long)this.GetHashCode(), "TempFileReader({0}) was not disposed during normal operation; something is wrong.", keyValuePair.Value.FileName);
						keyValuePair.Value.Dispose();
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000BF1 RID: 3057
		private readonly SortedDictionary<Guid, TempFileReader> topRecords;

		// Token: 0x04000BF2 RID: 3058
		private bool disposed;
	}
}

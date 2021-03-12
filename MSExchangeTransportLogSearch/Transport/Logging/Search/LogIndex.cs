using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;
using Microsoft.Exchange.Rpc.LogSearch;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000024 RID: 36
	internal class LogIndex
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public LogIndex(string logFilePath, CsvTable table, CsvField columnToIndex, bool isActive)
		{
			this.logFilePath = logFilePath;
			this.table = table;
			this.columnToIndex = columnToIndex;
			this.columnIdToIndex = table.NameToIndex(columnToIndex.Name);
			this.normalizeMethod = columnToIndex.NormalizeMethod;
			this.indexFileDirectory = Path.Combine(Path.GetDirectoryName(logFilePath), "index");
			this.isActive = isActive;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004E5C File Offset: 0x0000305C
		public void TryReadFromDisk()
		{
			object obj = null;
			if (!IOHelper.TryIOOperation(new IOHelper.FileIOOperation(this.ReadIndexFromDisk), out obj))
			{
				ExTraceGlobals.ServiceTracer.TraceError((long)this.GetHashCode(), "Could not read index from disk due to I/O errors");
				this.indexWasReadFromDisk = false;
				return;
			}
			this.indexWasReadFromDisk = (bool)obj;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004EAC File Offset: 0x000030AC
		public void OnProcessRow(object sender, ProcessRowEventArgs args)
		{
			if (!this.indexWasReadFromDisk)
			{
				try
				{
					this.IndexRow(args.Row);
				}
				catch (InvalidOperationException ex)
				{
					LogSearchService.Logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchLogFileCorrupted, this.logFilePath, new object[]
					{
						this.logFilePath
					});
					ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Logfile corrupt, skipping record: {0}", ex.ToString());
				}
				catch (ArgumentOutOfRangeException ex2)
				{
					LogSearchService.Logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchServiceLogFileTooLarge, this.logFilePath, new object[]
					{
						this.logFilePath
					});
					ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Logfile corrupt, will not be indexed: {0}", ex2.ToString());
					this.indexData.Clear();
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004F88 File Offset: 0x00003188
		public void OnLogFileClosed(object sender, EventArgs args)
		{
			this.MakeInactive();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004F90 File Offset: 0x00003190
		public void OnLogFileDeleted(object sender, EventArgs args)
		{
			this.RemoveIndexFromDisk();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004F98 File Offset: 0x00003198
		public List<uint> GetOffsetsForData(string searchData)
		{
			if (this.isActive)
			{
				return this.GetOffsetsForDataSlow(searchData);
			}
			return this.GetOffsetsForDataFast(searchData);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004FB4 File Offset: 0x000031B4
		private void MakeInactive()
		{
			if (!this.indexWasReadFromDisk)
			{
				this.indexData.Sort(LogIndex.IndexRecordComparer.Default);
				object obj;
				IOHelper.TryIOOperation(new IOHelper.FileIOOperation(this.WriteIndexToDisk), out obj);
			}
			this.ShrinkClusteredEntries();
			List<LogIndex.IndexRecord> list = new List<LogIndex.IndexRecord>(this.indexData);
			IndexBufferPool.Instance.CheckIn(this.indexData);
			this.indexData = list;
			this.isActive = false;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005020 File Offset: 0x00003220
		private void ShrinkClusteredEntries()
		{
			int num = 0;
			for (int i = num + 1; i < this.indexData.Count; i++)
			{
				if (this.indexData[num].HashCode != this.indexData[i].HashCode || (ulong)(this.indexData[i].Offset - this.indexData[num].Offset) > (ulong)((long)LogIndex.ClusterRange))
				{
					num++;
					this.indexData[num] = this.indexData[i];
				}
			}
			if (num + 1 < this.indexData.Count)
			{
				this.indexData.RemoveRange(num + 1, this.indexData.Count - (num + 1));
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000050F4 File Offset: 0x000032F4
		private void IndexRow(ReadOnlyRow row)
		{
			ulong position = (ulong)row.Position;
			if (position > (ulong)-1)
			{
				throw new ArgumentOutOfRangeException(string.Format("Message tracking log files cannot be > 4Gb. Current file offset is {0} bytes", position));
			}
			uint num = (uint)position;
			string field = row.GetField<string>(this.columnIdToIndex);
			if (field == null)
			{
				throw new InvalidOperationException(string.Format("Column {0} not found at offset {1}", this.columnIdToIndex, num));
			}
			string text;
			if (this.normalizeMethod != null)
			{
				text = this.normalizeMethod(field);
			}
			else
			{
				text = field;
			}
			uint hashCode = (uint)text.GetHashCode();
			this.indexData.Add(new LogIndex.IndexRecord(hashCode, num));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005190 File Offset: 0x00003390
		private List<uint> GetOffsetsForDataSlow(string searchData)
		{
			uint hashCode = (uint)searchData.GetHashCode();
			List<uint> list = new List<uint>();
			for (int i = 0; i < this.indexData.Count; i++)
			{
				if (this.indexData[i].HashCode == hashCode)
				{
					list.Add(this.indexData[i].Offset);
				}
			}
			return list;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005200 File Offset: 0x00003400
		private List<uint> GetOffsetsForDataFast(string searchData)
		{
			uint hashCode = (uint)searchData.GetHashCode();
			LogIndex.IndexRecord item = new LogIndex.IndexRecord(hashCode, 0U);
			int num = this.indexData.BinarySearch(item, LogIndex.IndexRecordComparer.Default);
			if (num < 0)
			{
				num = ~num;
			}
			List<uint> list = new List<uint>();
			for (int i = num; i < this.indexData.Count; i++)
			{
				LogIndex.IndexRecord indexRecord = this.indexData[i];
				uint offset = indexRecord.Offset;
				uint hashCode2 = indexRecord.HashCode;
				if (hashCode2 != hashCode)
				{
					break;
				}
				object obj;
				if (!IOHelper.TryIOOperation(() => CsvFieldCache.OpenLogFile(this.logFilePath), out obj) || obj == null)
				{
					ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Search failed, unable to open file: {0}", this.logFilePath);
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_RPC_SERVER_UNAVAILABLE);
				}
				FileStream fileStream = (FileStream)obj;
				try
				{
					CsvFieldCache csvFieldCache = new CsvFieldCache(this.table, fileStream, 32768);
					if (offset != 0U)
					{
						csvFieldCache.Seek((long)((ulong)offset));
					}
					long position = csvFieldCache.Position;
					while (csvFieldCache.MoveNext(false) && position - (long)((ulong)offset) <= (long)LogIndex.ClusterRange)
					{
						object field = csvFieldCache.GetField(this.columnIdToIndex);
						if (field == null)
						{
							break;
						}
						uint hashCode3;
						if (this.normalizeMethod != null)
						{
							hashCode3 = (uint)this.normalizeMethod((string)field).GetHashCode();
						}
						else
						{
							hashCode3 = (uint)field.GetHashCode();
						}
						if (hashCode3 == hashCode)
						{
							if (position > (long)((ulong)-1))
							{
								break;
							}
							list.Add((uint)position);
						}
						position = csvFieldCache.Position;
					}
				}
				finally
				{
					if (fileStream != null)
					{
						fileStream.Close();
					}
				}
			}
			return list;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000053A0 File Offset: 0x000035A0
		private string GetIndexFileName()
		{
			string directoryName = Path.GetDirectoryName(this.logFilePath);
			string fileName = Path.GetFileName(this.logFilePath);
			string text = Path.Combine(directoryName, "index");
			return string.Format("{0}\\{1}.{2}.{3}", new object[]
			{
				text,
				fileName,
				this.columnToIndex.Name,
				"idx"
			});
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000540C File Offset: 0x0000360C
		private void RemoveIndexFromDisk()
		{
			Exception ex = null;
			string indexFileName = this.GetIndexFileName();
			try
			{
				if (File.Exists(indexFileName))
				{
					File.Delete(indexFileName);
				}
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (SecurityException ex3)
			{
				ex = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				ex = ex4;
			}
			catch (ArgumentException ex5)
			{
				ex = ex5;
			}
			catch (NotSupportedException ex6)
			{
				ex = ex6;
			}
			if (ex != null)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Cannot delete index file from disk: {0}", ex.ToString());
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000054B0 File Offset: 0x000036B0
		private object WriteIndexToDisk()
		{
			string indexFileName = this.GetIndexFileName();
			string text = indexFileName + ".tmp";
			if (!Directory.Exists(this.indexFileDirectory))
			{
				Directory.CreateDirectory(this.indexFileDirectory);
			}
			try
			{
				File.Delete(indexFileName);
				File.Delete(text);
			}
			catch (SecurityException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (IOException)
			{
			}
			using (FileStream fileStream = new FileStream(text, FileMode.CreateNew))
			{
				byte[] buffer = new byte[4];
				foreach (LogIndex.IndexRecord indexRecord in this.indexData)
				{
					uint hashCode = indexRecord.HashCode;
					uint offset = indexRecord.Offset;
					LogIndex.UInt32ToBytes(hashCode, ref buffer);
					fileStream.Write(buffer, 0, 4);
					LogIndex.UInt32ToBytes(offset, ref buffer);
					fileStream.Write(buffer, 0, 4);
				}
			}
			File.Move(text, indexFileName);
			return null;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000055E8 File Offset: 0x000037E8
		private object ReadIndexFromDisk()
		{
			string indexFileName = this.GetIndexFileName();
			FileStream fileStream = null;
			if (!File.Exists(indexFileName))
			{
				return false;
			}
			FileInfo fileInfo = new FileInfo(this.logFilePath);
			fileInfo.Refresh();
			long length = fileInfo.Length;
			object result;
			try
			{
				fileStream = new FileStream(indexFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				byte[] array = new byte[8];
				int num;
				while ((num = fileStream.Read(array, 0, array.Length)) > 0)
				{
					if (num != array.Length)
					{
						LogSearchService.Logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchServiceIndexFileCorrupt, null, new object[]
						{
							indexFileName
						});
						return false;
					}
					uint hashCode = BitConverter.ToUInt32(array, 0);
					uint num2 = BitConverter.ToUInt32(array, 4);
					if ((ulong)num2 > (ulong)length)
					{
						LogSearchService.Logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchServiceIndexFileCorrupt, null, new object[]
						{
							indexFileName
						});
						return false;
					}
					this.indexData.Add(new LogIndex.IndexRecord(hashCode, num2));
				}
				this.ShrinkClusteredEntries();
				result = true;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return result;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005714 File Offset: 0x00003914
		private static void UInt32ToBytes(uint integer, ref byte[] byteArray)
		{
			byteArray[0] = (byte)(integer & 255U);
			byteArray[1] = (byte)((integer & 65280U) >> 8);
			byteArray[2] = (byte)((integer & 16711680U) >> 16);
			byteArray[3] = (byte)((integer & 4278190080U) >> 24);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000574E File Offset: 0x0000394E
		public List<LogIndex.IndexRecord> IndexData
		{
			get
			{
				return this.indexData;
			}
		}

		// Token: 0x04000057 RID: 87
		public const string IndexFileSuffix = "idx";

		// Token: 0x04000058 RID: 88
		public const string IndexSubDirectory = "index";

		// Token: 0x04000059 RID: 89
		private const uint MaxOffset = 4294967295U;

		// Token: 0x0400005A RID: 90
		public static int ClusterRange = LogSearchAppConfig.Instance.LogSearchIndexing.ClusterRange;

		// Token: 0x0400005B RID: 91
		private CsvTable table;

		// Token: 0x0400005C RID: 92
		private CsvField columnToIndex;

		// Token: 0x0400005D RID: 93
		private int columnIdToIndex;

		// Token: 0x0400005E RID: 94
		private string logFilePath;

		// Token: 0x0400005F RID: 95
		private string indexFileDirectory;

		// Token: 0x04000060 RID: 96
		private NormalizeColumnDataMethod normalizeMethod;

		// Token: 0x04000061 RID: 97
		private bool indexWasReadFromDisk;

		// Token: 0x04000062 RID: 98
		private bool isActive;

		// Token: 0x04000063 RID: 99
		private List<LogIndex.IndexRecord> indexData = IndexBufferPool.Instance.CheckOut();

		// Token: 0x02000025 RID: 37
		public struct IndexRecord
		{
			// Token: 0x17000014 RID: 20
			// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000576C File Offset: 0x0000396C
			public uint HashCode
			{
				get
				{
					return this.hashCode;
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x060000A8 RID: 168 RVA: 0x00005774 File Offset: 0x00003974
			public uint Offset
			{
				get
				{
					return this.offset;
				}
			}

			// Token: 0x060000A9 RID: 169 RVA: 0x0000577C File Offset: 0x0000397C
			public IndexRecord(uint hashCode, uint offset)
			{
				this.hashCode = hashCode;
				this.offset = offset;
			}

			// Token: 0x04000064 RID: 100
			private uint hashCode;

			// Token: 0x04000065 RID: 101
			private uint offset;
		}

		// Token: 0x02000026 RID: 38
		public class IndexRecordComparer : IComparer<LogIndex.IndexRecord>
		{
			// Token: 0x060000AA RID: 170 RVA: 0x0000578C File Offset: 0x0000398C
			public int Compare(LogIndex.IndexRecord x, LogIndex.IndexRecord y)
			{
				int num = x.HashCode.CompareTo(y.HashCode);
				if (num == 0)
				{
					num = x.Offset.CompareTo(y.Offset);
				}
				return num;
			}

			// Token: 0x04000066 RID: 102
			public static readonly LogIndex.IndexRecordComparer Default = new LogIndex.IndexRecordComparer();
		}
	}
}

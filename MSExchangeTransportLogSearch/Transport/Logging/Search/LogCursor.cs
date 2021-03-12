using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000005 RID: 5
	internal class LogCursor
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002A40 File Offset: 0x00000C40
		public DateTime Begin
		{
			get
			{
				return this.begin;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002A48 File Offset: 0x00000C48
		public DateTime End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002A50 File Offset: 0x00000C50
		public LogCursor(CsvTable table, Version schemaVersion, DateTime begin, DateTime end, List<LogFileInfo> files)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string, string>((long)this.GetHashCode(), "MsExchangeLogSearch constructs LogCursor with begin time {0} and end time {1}", begin.ToString(), end.ToString());
			this.table = table;
			this.schemaVersion = schemaVersion;
			this.begin = begin;
			this.end = end;
			this.files = files;
			this.fieldNameList = table.SerializeFieldNameList(schemaVersion);
			for (int i = 0; i < this.files.Count; i++)
			{
				this.totalBytes += this.files[i].Length;
			}
			this.FindBeginning(begin);
			this.FindEnd(end);
			this.firstIsReady = (this.cursor != null);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002B1A File Offset: 0x00000D1A
		public int Progress
		{
			get
			{
				if (this.idx > this.endIdx)
				{
					return 100;
				}
				return Math.Min((int)Math.Floor((double)this.currentBytes / (double)this.totalBytes * 100.0), 99);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002B53 File Offset: 0x00000D53
		public object GetField(int idx)
		{
			return this.cursor.GetField(idx);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002B61 File Offset: 0x00000D61
		public int SchemaFieldCount
		{
			get
			{
				return this.table.Fields.Length;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002B70 File Offset: 0x00000D70
		public int FieldCount
		{
			get
			{
				return this.cursor.FieldCount;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002B7D File Offset: 0x00000D7D
		public byte[] FieldNameList
		{
			get
			{
				return this.fieldNameList;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002B85 File Offset: 0x00000D85
		public int CopyRow(int srcOffset, byte[] dest, int offset, int count)
		{
			return this.cursor.CopyRow(srcOffset, dest, offset, count);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B98 File Offset: 0x00000D98
		public bool MoveNext()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogCursor MoveNext");
			while (this.MoveNextPrivate())
			{
				bool flag = true;
				if (this.files[this.idx].NeedsTimeCheck)
				{
					object field = this.GetField(0);
					if (field != null)
					{
						DateTime t = (DateTime)field;
						flag = (t >= this.begin && t < this.end);
					}
					else
					{
						ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Message tracking log file {0} has been corrupted.", this.files[this.idx].FullName);
						this.LogFileCorrupted(this.files[this.idx].FullName);
						flag = false;
					}
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002C64 File Offset: 0x00000E64
		private bool MoveNextPrivate()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogCursor MoveNextPrivate");
			while (this.idx <= this.endIdx)
			{
				if (this.cursor != null)
				{
					bool flag;
					if (this.firstIsReady)
					{
						this.firstIsReady = false;
						flag = true;
					}
					else
					{
						long position = this.cursor.Position;
						flag = this.cursor.MoveNext(false);
						this.currentBytes += this.cursor.Position - position;
					}
					if (flag && (this.idx < this.endIdx || this.cursor.Position < this.endOffset))
					{
						if (this.cursor.FieldCount != this.table.Fields.Length)
						{
							this.LogFileCorrupted(this.files[this.idx].FullName);
						}
						return true;
					}
					ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "MsExchangeLogSearch LogCursor MoveNextPrivate close file {0}", this.data.Name);
					this.data.Close();
					this.data = null;
					this.cursor = null;
					this.idx++;
				}
				else
				{
					this.data = CsvFieldCache.OpenLogFile(this.files[this.idx].FullName);
					if (this.data == null)
					{
						this.totalBytes -= this.files[this.idx].Length;
						this.idx++;
					}
					else
					{
						this.cursor = new CsvFieldCache(this.table, this.schemaVersion, this.data, 32768);
						this.totalBytes = this.totalBytes - this.files[this.idx].Length + this.cursor.Length;
					}
				}
			}
			return false;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E48 File Offset: 0x00001048
		public void Close()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogCursor Close");
			if (this.data != null)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "MsExchangeLogSearch LogCursor Close close file {0}", this.data.Name);
				this.data.Close();
				this.data = null;
			}
			this.cursor = null;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002EB0 File Offset: 0x000010B0
		private void FindBeginning(DateTime begin)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogCursor FindBeginning");
			this.idx = 0;
			while (this.idx < this.files.Count)
			{
				this.data = CsvFieldCache.OpenLogFile(this.files[this.idx].FullName);
				if (this.data == null)
				{
					this.totalBytes -= this.files[this.idx].Length;
					this.idx++;
				}
				else
				{
					this.cursor = new CsvFieldCache(this.table, this.schemaVersion, this.data, 32768);
					bool flag = this.table.ClusteredIndex.Find(begin, this.cursor);
					this.totalBytes = this.totalBytes - this.files[this.idx].Length + this.cursor.Length - this.cursor.Position;
					if (flag)
					{
						ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "MsExchangeLogSearch LogCursor FindBeginning found file {0} corrupted", this.data.Name);
						this.LogFileCorrupted(this.data.Name);
					}
					if (!this.cursor.AtEnd)
					{
						return;
					}
					ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "MsExchangeLogSearch LogCursor FindBeginning close file {0}", this.data.Name);
					this.data.Close();
					this.data = null;
					this.cursor = null;
					this.idx++;
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003060 File Offset: 0x00001260
		private void FindEnd(DateTime end)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogCursor FindFindEnd");
			this.endIdx = this.files.Count - 1;
			while (this.endIdx >= this.idx)
			{
				using (FileStream fileStream = CsvFieldCache.OpenLogFile(this.files[this.endIdx].FullName))
				{
					if (fileStream != null)
					{
						CsvFieldCache csvFieldCache = new CsvFieldCache(this.table, this.schemaVersion, fileStream, 32768);
						this.table.ClusteredIndex.Find(end, csvFieldCache);
						this.totalBytes = this.totalBytes - this.files[this.endIdx].Length + csvFieldCache.Position;
						if (csvFieldCache.AtEnd)
						{
							this.endOffset = csvFieldCache.Position + 1L;
						}
						else
						{
							this.endOffset = csvFieldCache.Position;
						}
						break;
					}
					this.totalBytes -= this.files[this.endIdx].Length;
					this.endIdx--;
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000319C File Offset: 0x0000139C
		public bool SearchNext(string column, string value)
		{
			if (this.fileEnumerator == null)
			{
				this.fileEnumerator = this.files.GetEnumerator();
				if (!this.fileEnumerator.MoveNext())
				{
					return false;
				}
				this.offsetEnumerator = this.fileEnumerator.Current.GetOffsetsForData(column.ToLower(), value);
			}
			while (!this.offsetEnumerator.MoveNext())
			{
				if (!this.fileEnumerator.MoveNext())
				{
					return false;
				}
				this.searchFileIdx++;
				this.offsetEnumerator = this.fileEnumerator.Current.GetOffsetsForData(column.ToLower(), value);
			}
			if ((ulong)this.offsetEnumerator.Current > (ulong)this.fileEnumerator.Current.Length)
			{
				return false;
			}
			if (this.data != null)
			{
				this.data.Close();
				this.cursor = null;
			}
			this.data = CsvFieldCache.OpenLogFile(this.fileEnumerator.Current.FullName);
			if (this.data == null)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Unable to open logfile: {0}", this.fileEnumerator.Current.FullName);
				return false;
			}
			this.cursor = new CsvFieldCache(this.table, this.schemaVersion, this.data, 32768);
			this.cursor.Seek((long)((ulong)this.offsetEnumerator.Current));
			if (!this.cursor.MoveNext(false))
			{
				return false;
			}
			this.idx = this.searchFileIdx;
			this.currentBytes += (long)((ulong)this.offsetEnumerator.Current);
			this.totalBytes = this.totalBytes - this.files[this.searchFileIdx].Length + (long)((ulong)this.offsetEnumerator.Current);
			return true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003361 File Offset: 0x00001561
		public void ResetSearchContext()
		{
			this.searchFileIdx = 0;
			this.fileEnumerator = null;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003374 File Offset: 0x00001574
		private void LogFileCorrupted(string filePath)
		{
			LogSearchService.Logger.LogEvent(MSExchangeTransportLogSearchEventLogConstants.Tuple_LogSearchLogFileCorrupted, filePath, new object[]
			{
				filePath
			});
		}

		// Token: 0x04000011 RID: 17
		private List<LogFileInfo> files;

		// Token: 0x04000012 RID: 18
		private CsvTable table;

		// Token: 0x04000013 RID: 19
		private Version schemaVersion;

		// Token: 0x04000014 RID: 20
		private int idx;

		// Token: 0x04000015 RID: 21
		private CsvFieldCache cursor;

		// Token: 0x04000016 RID: 22
		private int endIdx;

		// Token: 0x04000017 RID: 23
		private long endOffset;

		// Token: 0x04000018 RID: 24
		private FileStream data;

		// Token: 0x04000019 RID: 25
		private IEnumerator<LogFileInfo> fileEnumerator;

		// Token: 0x0400001A RID: 26
		private IEnumerator<uint> offsetEnumerator;

		// Token: 0x0400001B RID: 27
		private int searchFileIdx;

		// Token: 0x0400001C RID: 28
		private long totalBytes;

		// Token: 0x0400001D RID: 29
		private long currentBytes;

		// Token: 0x0400001E RID: 30
		private bool firstIsReady;

		// Token: 0x0400001F RID: 31
		private DateTime begin;

		// Token: 0x04000020 RID: 32
		private DateTime end;

		// Token: 0x04000021 RID: 33
		private byte[] fieldNameList;
	}
}

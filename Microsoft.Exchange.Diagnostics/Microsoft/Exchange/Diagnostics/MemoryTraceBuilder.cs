using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000A2 RID: 162
	public class MemoryTraceBuilder : ITraceBuilder
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x0000E12C File Offset: 0x0000C32C
		public MemoryTraceBuilder(int maximumEntries, int maximumBufferSize)
		{
			this.traceBuffer = new char[maximumBufferSize + 1];
			this.entries = new TraceEntry[maximumEntries + 1];
			this.bufferLength = maximumBufferSize + 1;
			this.entryArrayLength = maximumEntries + 1;
			this.nativeThreadId = DiagnosticsNativeMethods.GetCurrentThreadId();
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000E178 File Offset: 0x0000C378
		private int FreeSize
		{
			get
			{
				int num = this.freeBufferIndex - this.startBufferIndex;
				if (num < 0)
				{
					return -num;
				}
				return this.bufferLength - num;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000E1A2 File Offset: 0x0000C3A2
		public int NativeThreadId
		{
			get
			{
				return this.nativeThreadId;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000E1AA File Offset: 0x0000C3AA
		public bool InsideTraceCall
		{
			get
			{
				return this.insideTraceCall;
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		public static List<object> GetTraceArguments(char[] buffer, int startIndex, int length)
		{
			List<object> list = new List<object>();
			int i = length;
			int num = startIndex;
			while (i > 0)
			{
				i -= MemoryTraceBuilder.GetTraceArgument(list, buffer, ref num);
			}
			return list;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000E1DE File Offset: 0x0000C3DE
		public void AddArgument<T>(T traceArgument)
		{
			TraceFormatter<T>.Default.Format(this, traceArgument);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E1EC File Offset: 0x0000C3EC
		public void AddArgument(int traceArgument)
		{
			if (!this.ReserveSpace(3))
			{
				return;
			}
			this.AppendWithoutCheck('\0');
			this.AppendWithoutCheck((char)traceArgument);
			this.AppendWithoutCheck((char)(traceArgument >> 16));
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000E212 File Offset: 0x0000C412
		public void AddArgument(long traceArgument)
		{
			if (!this.ReserveSpace(5))
			{
				return;
			}
			this.AppendWithoutCheck('\u0001');
			this.AppendWithoutCheck((char)traceArgument);
			this.AppendWithoutCheck((char)(traceArgument >> 16));
			this.AppendWithoutCheck((char)(traceArgument >> 32));
			this.AppendWithoutCheck((char)(traceArgument >> 48));
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000E250 File Offset: 0x0000C450
		public unsafe void AddArgument(Guid traceArgument)
		{
			if (!this.ReserveSpace(9))
			{
				return;
			}
			this.AppendWithoutCheck('\u0002');
			char* ptr = (char*)(&traceArgument);
			for (int i = 0; i < sizeof(Guid) / 2; i++)
			{
				this.AppendWithoutCheck(ptr[i]);
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E294 File Offset: 0x0000C494
		public void AddArgument(string traceArgument)
		{
			if (string.IsNullOrEmpty(traceArgument))
			{
				traceArgument = string.Empty;
			}
			if (!this.PrepareStringArgument(traceArgument.Length))
			{
				return;
			}
			for (int i = 0; i < traceArgument.Length; i++)
			{
				this.AppendWithoutCheck(traceArgument[i]);
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000E2E0 File Offset: 0x0000C4E0
		public void AddArgument(char[] traceArgument)
		{
			if (traceArgument == null)
			{
				this.AddArgument(null);
				return;
			}
			if (!this.PrepareStringArgument(traceArgument.Length))
			{
				return;
			}
			for (int i = 0; i < traceArgument.Length; i++)
			{
				this.AppendWithoutCheck(traceArgument[i]);
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000E31C File Offset: 0x0000C51C
		public unsafe void AddArgument(char* traceArgument, int length)
		{
			if (traceArgument == null)
			{
				this.AddArgument(null);
				return;
			}
			if (!this.PrepareStringArgument(length))
			{
				return;
			}
			for (int i = 0; i < length; i++)
			{
				this.AppendWithoutCheck(traceArgument[i]);
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000E359 File Offset: 0x0000C559
		public void BeginEntry(TraceType traceType, Guid componentGuid, int traceTag, long id, string format)
		{
			this.PrepareForBeginEntry();
			this.entries[this.freeEntry] = new TraceEntry(traceType, componentGuid, traceTag, id, format, this.freeBufferIndex, this.nativeThreadId);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000E390 File Offset: 0x0000C590
		public void EndEntry()
		{
			if (this.entrySizeOverLimit)
			{
				this.entries[this.freeEntry].FormatString = "The tracing entry is too big to fit into the tracing buffer.";
				this.entries[this.freeEntry].Length = 0;
				this.freeBufferIndex = this.entries[this.freeEntry].StartIndex;
			}
			else
			{
				this.entries[this.freeEntry].Length = (int)this.UsedBufferSizeSince(this.entries[this.freeEntry].StartIndex);
			}
			this.IncrementFreeEntryIndex();
			this.insideTraceCall = false;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000E434 File Offset: 0x0000C634
		public void Reset()
		{
			this.insideTraceCall = false;
			this.startBufferIndex = 0;
			this.freeBufferIndex = 0;
			this.startEntry = 0;
			this.freeEntry = 0;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000E45C File Offset: 0x0000C65C
		public void Dump(TextWriter writer, bool addHeader, bool verbose)
		{
			if (writer == null)
			{
				return;
			}
			if (addHeader)
			{
				if (verbose)
				{
					writer.WriteLine("ThreadId;ComponentGuid;Instance ID;TraceTag;TraceType;TimeStamp;Message;");
				}
				else
				{
					writer.WriteLine("ThreadId;TimeStamp;Message;");
				}
			}
			List<KeyValuePair<TraceEntry, List<object>>> traceEntries = this.GetTraceEntries();
			foreach (KeyValuePair<TraceEntry, List<object>> keyValuePair in traceEntries)
			{
				TraceEntry key = keyValuePair.Key;
				writer.Write(key.NativeThreadId);
				writer.Write(';');
				if (verbose)
				{
					writer.Write(key.ComponentGuid);
					writer.Write(';');
					writer.Write(key.Id);
					writer.Write(';');
					writer.Write(key.TraceTag);
					writer.Write(';');
					writer.Write(key.TraceType);
					writer.Write(';');
				}
				writer.Write(key.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
				writer.Write(';');
				if (keyValuePair.Value.Count == 0)
				{
					writer.Write(key.FormatString);
				}
				else
				{
					writer.Write(string.Format(key.FormatString, keyValuePair.Value.ToArray()));
				}
				writer.WriteLine(';');
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000E81C File Offset: 0x0000CA1C
		public IEnumerable<TraceEntry> GetTraces()
		{
			List<KeyValuePair<TraceEntry, List<object>>> entries = this.GetTraceEntries();
			foreach (KeyValuePair<TraceEntry, List<object>> pair in entries)
			{
				KeyValuePair<TraceEntry, List<object>> keyValuePair = pair;
				TraceEntry entry = keyValuePair.Key;
				KeyValuePair<TraceEntry, List<object>> keyValuePair2 = pair;
				string traceMessage;
				if (keyValuePair2.Value.Count == 0)
				{
					traceMessage = entry.FormatString;
				}
				else
				{
					string formatString = entry.FormatString;
					KeyValuePair<TraceEntry, List<object>> keyValuePair3 = pair;
					traceMessage = string.Format(formatString, keyValuePair3.Value.ToArray());
				}
				TraceEntry result = new TraceEntry(entry.TraceType, entry.ComponentGuid, entry.TraceTag, entry.Id, traceMessage, 0, entry.NativeThreadId);
				yield return result;
			}
			yield break;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000E83C File Offset: 0x0000CA3C
		public int FindLastEntryIndex(int entriesToFind, TraceType traceType, int traceTag, Guid componentGuid, string formatString)
		{
			int num = 0;
			int num2 = this.freeEntry - 1;
			if (num2 < 0)
			{
				num2 = this.entryArrayLength - 1;
			}
			while (num2 != this.startEntry)
			{
				TraceEntry traceEntry = this.entries[num2];
				if (traceEntry.TraceType == traceType && traceEntry.TraceTag == traceTag && traceEntry.ComponentGuid == componentGuid && object.Equals(traceEntry.FormatString, formatString) && ++num == entriesToFind)
				{
					break;
				}
				if (--num2 < 0)
				{
					num2 = this.entryArrayLength - 1;
				}
			}
			return num2;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000E8CC File Offset: 0x0000CACC
		public void CopyTo(MemoryTraceBuilder destination, int startIndex)
		{
			if ((this.startEntry < this.freeEntry && (startIndex < this.startEntry || startIndex >= this.freeEntry)) || (this.startEntry > this.freeEntry && startIndex < this.startEntry && startIndex >= this.freeEntry))
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			int num = startIndex;
			while (num != this.freeEntry)
			{
				TraceEntry traceEntry = this.entries[num];
				destination.BeginEntry(traceEntry);
				if (destination.ReserveSpace(traceEntry.Length))
				{
					int startIndex2 = traceEntry.StartIndex;
					for (int i = traceEntry.Length; i > 0; i--)
					{
						char nextCharInTraceBuffer = MemoryTraceBuilder.GetNextCharInTraceBuffer(this.traceBuffer, ref startIndex2);
						destination.AppendWithoutCheck(nextCharInTraceBuffer);
					}
				}
				destination.EndEntry();
				if (++num == this.entryArrayLength)
				{
					num = 0;
				}
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
		internal List<KeyValuePair<TraceEntry, List<object>>> GetTraceEntries()
		{
			List<KeyValuePair<TraceEntry, List<object>>> list = new List<KeyValuePair<TraceEntry, List<object>>>();
			for (int num = this.startEntry; num != this.freeEntry; num = (num + 1) % this.entryArrayLength)
			{
				List<object> traceArguments = MemoryTraceBuilder.GetTraceArguments(this.traceBuffer, this.entries[num].StartIndex, this.entries[num].Length);
				list.Add(new KeyValuePair<TraceEntry, List<object>>(this.entries[num], traceArguments));
			}
			return list;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		private static char GetNextCharInTraceBuffer(char[] buffer, ref int index)
		{
			char result = buffer[index];
			index = (index + 1) % buffer.Length;
			return result;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000EA3C File Offset: 0x0000CC3C
		private static int GetTraceArgument(List<object> arguments, char[] buffer, ref int startIndex)
		{
			int result = 0;
			int nextCharInTraceBuffer = (int)MemoryTraceBuilder.GetNextCharInTraceBuffer(buffer, ref startIndex);
			if (nextCharInTraceBuffer == 0)
			{
				int num = (int)MemoryTraceBuilder.GetNextCharInTraceBuffer(buffer, ref startIndex);
				num += (int)((int)MemoryTraceBuilder.GetNextCharInTraceBuffer(buffer, ref startIndex) << 16);
				result = 3;
				arguments.Add(num);
			}
			else if (nextCharInTraceBuffer == 1)
			{
				long num2 = (long)((ulong)MemoryTraceBuilder.GetNextCharInTraceBuffer(buffer, ref startIndex));
				num2 += (long)((long)((ulong)MemoryTraceBuilder.GetNextCharInTraceBuffer(buffer, ref startIndex)) << 16);
				num2 += (long)((long)((ulong)MemoryTraceBuilder.GetNextCharInTraceBuffer(buffer, ref startIndex)) << 32);
				num2 += (long)((long)((ulong)MemoryTraceBuilder.GetNextCharInTraceBuffer(buffer, ref startIndex)) << 48);
				result = 5;
				arguments.Add(num2);
			}
			else if (nextCharInTraceBuffer == 2)
			{
				byte[] array = new byte[16];
				for (int i = 0; i < 16; i += 2)
				{
					char nextCharInTraceBuffer2 = MemoryTraceBuilder.GetNextCharInTraceBuffer(buffer, ref startIndex);
					array[i] = (byte)nextCharInTraceBuffer2;
					array[i + 1] = (byte)(nextCharInTraceBuffer2 >> 8);
				}
				Guid guid = new Guid(array);
				arguments.Add(guid);
				result = 9;
			}
			else if (nextCharInTraceBuffer == 3)
			{
				int nextCharInTraceBuffer3 = (int)MemoryTraceBuilder.GetNextCharInTraceBuffer(buffer, ref startIndex);
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = 0; j < nextCharInTraceBuffer3; j++)
				{
					stringBuilder.Append(MemoryTraceBuilder.GetNextCharInTraceBuffer(buffer, ref startIndex));
				}
				result = nextCharInTraceBuffer3 + 2;
				arguments.Add(stringBuilder.ToString());
			}
			return result;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000EB65 File Offset: 0x0000CD65
		private void PrepareForBeginEntry()
		{
			this.insideTraceCall = true;
			this.currentEntrySize = 0;
			this.entrySizeOverLimit = false;
			if (this.IsEntryArrayFull())
			{
				this.FreeOneEntry();
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000EB8C File Offset: 0x0000CD8C
		private void BeginEntry(TraceEntry traceEntry)
		{
			this.PrepareForBeginEntry();
			this.entries[this.freeEntry] = new TraceEntry(traceEntry.TraceType, traceEntry.ComponentGuid, traceEntry.TraceTag, traceEntry.Id, traceEntry.FormatString, this.freeBufferIndex, traceEntry.NativeThreadId);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000EBEC File Offset: 0x0000CDEC
		private void IncrementFreeEntryIndex()
		{
			if (++this.freeEntry == this.entryArrayLength)
			{
				this.freeEntry = 0;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		private bool PrepareStringArgument(int length)
		{
			int size = length + 2;
			if (!this.ReserveSpace(size))
			{
				return false;
			}
			this.AppendWithoutCheck('\u0003');
			this.AppendWithoutCheck((char)length);
			return true;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000EC48 File Offset: 0x0000CE48
		private char UsedBufferSizeSince(int startIndex)
		{
			int num = this.freeBufferIndex - startIndex;
			if (num < 0)
			{
				num += this.bufferLength;
			}
			return (char)num;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000EC6D File Offset: 0x0000CE6D
		private void Append(char ch)
		{
			while (this.IsBufferFull())
			{
				this.FreeOneEntry();
			}
			this.AppendWithoutCheck(ch);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000EC86 File Offset: 0x0000CE86
		private void AppendWithoutCheck(char ch)
		{
			this.traceBuffer[this.freeBufferIndex] = ch;
			this.IncrementFreeBufferIndex();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		private void IncrementFreeBufferIndex()
		{
			if (++this.freeBufferIndex == this.bufferLength)
			{
				this.freeBufferIndex = 0;
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000ECCC File Offset: 0x0000CECC
		private bool ReserveSpace(int size)
		{
			if (this.entrySizeOverLimit)
			{
				return false;
			}
			if (this.currentEntrySize >= this.bufferLength - size)
			{
				this.entrySizeOverLimit = true;
				return false;
			}
			this.currentEntrySize += size;
			while (this.FreeSize <= size)
			{
				this.FreeOneEntry();
			}
			return true;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		private bool IsEntryArrayFull()
		{
			int num = this.freeEntry + 1;
			if (num == this.entryArrayLength)
			{
				num = 0;
			}
			return num == this.startEntry;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000ED48 File Offset: 0x0000CF48
		private bool IsBufferFull()
		{
			int num = this.freeBufferIndex + 1;
			if (num == this.bufferLength)
			{
				num = 0;
			}
			return num == this.startBufferIndex;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000ED72 File Offset: 0x0000CF72
		private void FreeOneEntry()
		{
			this.IncrementBufferStartIndexBy(this.entries[this.startEntry].Length);
			this.entries[this.startEntry].Clear();
			this.IncrementStartEntryIndex();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000EDAC File Offset: 0x0000CFAC
		private void IncrementStartEntryIndex()
		{
			if (++this.startEntry == this.entryArrayLength)
			{
				this.startEntry = 0;
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000EDD9 File Offset: 0x0000CFD9
		private void IncrementBufferStartIndexBy(int length)
		{
			this.startBufferIndex += length;
			if (this.startBufferIndex >= this.bufferLength)
			{
				this.startBufferIndex -= this.bufferLength;
			}
		}

		// Token: 0x04000327 RID: 807
		private const string BigEntryFormatString = "The tracing entry is too big to fit into the tracing buffer.";

		// Token: 0x04000328 RID: 808
		private const int CharsForInt32 = 3;

		// Token: 0x04000329 RID: 809
		private const int CharsForInt64 = 5;

		// Token: 0x0400032A RID: 810
		private const int CharsForGuid = 9;

		// Token: 0x0400032B RID: 811
		private char[] traceBuffer;

		// Token: 0x0400032C RID: 812
		private int bufferLength;

		// Token: 0x0400032D RID: 813
		private int startBufferIndex;

		// Token: 0x0400032E RID: 814
		private int freeBufferIndex;

		// Token: 0x0400032F RID: 815
		private TraceEntry[] entries;

		// Token: 0x04000330 RID: 816
		private int entryArrayLength;

		// Token: 0x04000331 RID: 817
		private int startEntry;

		// Token: 0x04000332 RID: 818
		private int freeEntry;

		// Token: 0x04000333 RID: 819
		private int nativeThreadId;

		// Token: 0x04000334 RID: 820
		private bool insideTraceCall;

		// Token: 0x04000335 RID: 821
		private int currentEntrySize;

		// Token: 0x04000336 RID: 822
		private bool entrySizeOverLimit;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001A8 RID: 424
	public class CsvFieldCache
	{
		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002AD7D File Offset: 0x00028F7D
		public CsvFieldCache(CsvTable table, Stream data, int readSize)
		{
			this.InitializeMembers(table, CsvFieldCache.LocalVersion, data, readSize);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002AD93 File Offset: 0x00028F93
		public CsvFieldCache(CsvTable table, Version requestedSchemaVersion, Stream data, int readSize)
		{
			this.InitializeMembers(table, requestedSchemaVersion, data, readSize);
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0002ADA6 File Offset: 0x00028FA6
		public bool AtEnd
		{
			get
			{
				return this.row.AtEnd;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0002ADB3 File Offset: 0x00028FB3
		public long Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0002ADBB File Offset: 0x00028FBB
		public long Length
		{
			get
			{
				return this.data.Length;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0002ADC8 File Offset: 0x00028FC8
		public int FieldCount
		{
			get
			{
				return this.row.Count;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0002ADD5 File Offset: 0x00028FD5
		public int SchemaFieldCount
		{
			get
			{
				return this.table.Fields.Length - this.unsupportedFields.Length;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0002ADED File Offset: 0x00028FED
		public int ReadSize
		{
			get
			{
				return this.readSize;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0002ADF5 File Offset: 0x00028FF5
		public CsvTable Schema
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002ADFD File Offset: 0x00028FFD
		public static string NormalizeMessageID(string messageId)
		{
			if (messageId.Length > 2 && messageId[0] == '<' && messageId[messageId.Length - 1] == '>')
			{
				return messageId.Substring(1, messageId.Length - 2);
			}
			return messageId;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002AE36 File Offset: 0x00029036
		public static string NormalizeEmailAddress(string email)
		{
			if (email.Length > 2 && email[0] == '<' && email[email.Length - 1] == '>')
			{
				email = email.Substring(1, email.Length - 2);
			}
			return email.ToLower();
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002AE78 File Offset: 0x00029078
		public static FileStream OpenLogFile(string filePath)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = CsvFieldCache.OpenFileAndSkipPreamble(filePath);
				if (fileStream == null)
				{
					return null;
				}
				string text;
				return CsvFieldCache.SeekToDataStartOffset(fileStream, out text);
			}
			catch (IOException ex)
			{
				ExTraceGlobals.CommonTracer.TraceError<string>(0L, "CsvFieldCache OpenLogFile failed with error {0}", ex.Message);
			}
			catch (UnauthorizedAccessException ex2)
			{
				ExTraceGlobals.CommonTracer.TraceError<string>(0L, "CsvFieldCache OpenLogFile failed with error {0}", ex2.Message);
			}
			if (fileStream != null)
			{
				fileStream.Close();
			}
			return null;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002AF00 File Offset: 0x00029100
		public static FileStream OpenLogFile(string filePath, out string version)
		{
			version = string.Empty;
			FileStream fileStream = null;
			try
			{
				fileStream = CsvFieldCache.OpenFileAndSkipPreamble(filePath);
				if (fileStream == null)
				{
					return null;
				}
				return CsvFieldCache.SeekToDataStartOffset(fileStream, out version);
			}
			catch (IOException ex)
			{
				ExTraceGlobals.CommonTracer.TraceError<string>(0L, "CsvFieldCache OpenLogFile failed with error {0}", ex.Message);
			}
			catch (UnauthorizedAccessException ex2)
			{
				ExTraceGlobals.CommonTracer.TraceError<string>(0L, "CsvFieldCache OpenLogFile failed with error {0}", ex2.Message);
			}
			if (fileStream != null)
			{
				fileStream.Close();
			}
			return null;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002AF8C File Offset: 0x0002918C
		public static string DecodeString(byte[] src, int offset, int count)
		{
			return Encoding.UTF8.GetString(src, offset, count);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002AF9C File Offset: 0x0002919C
		public object GetField(int index)
		{
			int num = index;
			if (this.fieldTranslationTable != null && !this.fieldTranslationTable.TryGetValue(index, out index))
			{
				return null;
			}
			if (index >= this.row.Count)
			{
				return null;
			}
			if (this.cacheVersion[index] == this.version)
			{
				return this.cache[index];
			}
			object obj = this.row.Decode(index, this.decoder[num]);
			this.cache[index] = obj;
			this.cacheVersion[index] = this.version;
			return obj;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002B01C File Offset: 0x0002921C
		public byte[] GetFieldRaw(int index)
		{
			int index2 = index;
			if (this.fieldTranslationTable != null && !this.fieldTranslationTable.TryGetValue(index, out index))
			{
				return null;
			}
			if (index >= this.row.Count)
			{
				return null;
			}
			return this.row.GetRawFieldValue(index2);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002B061 File Offset: 0x00029261
		public int CopyRow(int srcOffset, byte[] dest, int offset, int count)
		{
			return this.row.Copy(srcOffset, dest, offset, count, this.unsupportedFields);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002B079 File Offset: 0x00029279
		public bool MoveNext(bool skipHeaders = false)
		{
			this.InvalidateCache();
			while (this.MoveNextRow())
			{
				if (!skipHeaders || !this.row.IsHeaderRow())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002B09C File Offset: 0x0002929C
		public long Seek(long position)
		{
			this.position = this.data.Seek(position, SeekOrigin.Begin);
			this.row.Reset();
			return this.position;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002B0C4 File Offset: 0x000292C4
		public void SetMappingTableBasedOnCurrentRecord()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>(this.row.Count);
			for (int i = 0; i < this.row.Count; i++)
			{
				string @string = Encoding.ASCII.GetString(this.row.GetRawFieldValue(i));
				int num = this.table.NameToIndex(@string);
				if (num != -1)
				{
					dictionary.Add(num, i);
				}
			}
			this.fieldTranslationTable = dictionary;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002B134 File Offset: 0x00029334
		private static FileStream OpenFileAndSkipPreamble(string fileName)
		{
			ExTraceGlobals.CommonTracer.TraceDebug<string>(0L, "CsvFieldCache OpenLogFile opens new file {0}", fileName);
			FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 32768);
			byte[] preamble = Encoding.UTF8.GetPreamble();
			int i = 0;
			while (i < preamble.Length)
			{
				int num = fileStream.ReadByte();
				if (num == -1)
				{
					fileStream.Close();
					return null;
				}
				if (num != (int)preamble[i++])
				{
					fileStream.Seek((long)(-(long)i), SeekOrigin.Current);
					break;
				}
			}
			return fileStream;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002B1A4 File Offset: 0x000293A4
		private static FileStream SeekToDataStartOffset(FileStream file, out string version)
		{
			int num = -1;
			version = string.Empty;
			byte[] array = new byte[1024];
			bool flag = true;
			int num2;
			int i;
			for (;;)
			{
				num2 = file.Read(array, 0, array.Length);
				if (num2 == 0)
				{
					break;
				}
				for (i = 0; i < num2; i++)
				{
					byte b = array[i];
					if (flag)
					{
						if (b != 35)
						{
							goto Block_3;
						}
						if (i + "Version:".Length < num2)
						{
							string text = CsvFieldCache.DecodeString(array, i + 1, "Version:".Length);
							if (text.Equals("Version:"))
							{
								num = i + "Version:".Length + 1;
							}
						}
						flag = false;
					}
					else if (b == 10)
					{
						flag = true;
						if (num != -1)
						{
							version = CsvFieldCache.DecodeString(array, num, i - num).Trim();
							num = -1;
						}
					}
				}
			}
			file.Close();
			return null;
			Block_3:
			file.Seek((long)(i - num2), SeekOrigin.Current);
			return file;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002B27E File Offset: 0x0002947E
		private void InvalidateCache()
		{
			this.version += 1L;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002B290 File Offset: 0x00029490
		private bool MoveNextRow()
		{
			long num = this.row.ReadNext(this.data);
			if (num == -1L)
			{
				if (this.data.CanSeek)
				{
					this.position = this.data.Position;
				}
				return false;
			}
			this.position += num;
			return true;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002B2E4 File Offset: 0x000294E4
		private void InitializeMembers(CsvTable table, Version schemaVersion, Stream data, int readSize)
		{
			this.table = table;
			this.schemaVersion = schemaVersion;
			this.data = data;
			this.decoder = new CsvDecoderCallback[table.Fields.Length];
			if (this.data.CanSeek)
			{
				this.position = this.data.Position;
			}
			this.readSize = readSize;
			for (int i = 0; i < table.Fields.Length; i++)
			{
				this.decoder[i] = CsvDecoder.GetDecoder(table.Fields[i].Type);
			}
			this.cacheVersion = new long[table.Fields.Length];
			this.cache = new object[table.Fields.Length];
			this.row = new CsvRowBuffer(readSize);
			this.unsupportedFields = table.GetFieldsAddedAfterVersion(this.schemaVersion);
		}

		// Token: 0x04000884 RID: 2180
		public const int DefaultReadSize = 32768;

		// Token: 0x04000885 RID: 2181
		private const byte Pound = 35;

		// Token: 0x04000886 RID: 2182
		private const byte NewLine = 10;

		// Token: 0x04000887 RID: 2183
		private const string VersionString = "Version:";

		// Token: 0x04000888 RID: 2184
		public static readonly Version LocalVersion = new Version("15.00.1497.010");

		// Token: 0x04000889 RID: 2185
		private CsvTable table;

		// Token: 0x0400088A RID: 2186
		private CsvDecoderCallback[] decoder;

		// Token: 0x0400088B RID: 2187
		private Stream data;

		// Token: 0x0400088C RID: 2188
		private object[] cache;

		// Token: 0x0400088D RID: 2189
		private long[] cacheVersion;

		// Token: 0x0400088E RID: 2190
		private long version;

		// Token: 0x0400088F RID: 2191
		private CsvRowBuffer row;

		// Token: 0x04000890 RID: 2192
		private long position;

		// Token: 0x04000891 RID: 2193
		private int readSize;

		// Token: 0x04000892 RID: 2194
		private Version schemaVersion;

		// Token: 0x04000893 RID: 2195
		private int[] unsupportedFields;

		// Token: 0x04000894 RID: 2196
		private Dictionary<int, int> fieldTranslationTable;
	}
}

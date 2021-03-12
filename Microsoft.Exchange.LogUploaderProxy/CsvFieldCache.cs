using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x02000010 RID: 16
	public class CsvFieldCache
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00002953 File Offset: 0x00000B53
		public CsvFieldCache(CsvTable table, Version requestedSchemaVersion, Stream data, int readSize)
		{
			this.schema = table;
			this.csvFieldCacheImpl = new CsvFieldCache(this.schema.CsvTableImpl, requestedSchemaVersion, data, readSize);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000297C File Offset: 0x00000B7C
		public CsvFieldCache(CsvTable table, Stream data, int readSize)
		{
			this.schema = table;
			this.csvFieldCacheImpl = new CsvFieldCache(this.schema.CsvTableImpl, data, readSize);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000029A3 File Offset: 0x00000BA3
		public bool AtEnd
		{
			get
			{
				return this.csvFieldCacheImpl.AtEnd;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000029B0 File Offset: 0x00000BB0
		public long Position
		{
			get
			{
				return this.csvFieldCacheImpl.Position;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000029BD File Offset: 0x00000BBD
		public long Length
		{
			get
			{
				return this.csvFieldCacheImpl.Length;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000029CA File Offset: 0x00000BCA
		public int FieldCount
		{
			get
			{
				return this.csvFieldCacheImpl.FieldCount;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000029D7 File Offset: 0x00000BD7
		public int SchemaFieldCount
		{
			get
			{
				return this.csvFieldCacheImpl.SchemaFieldCount;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000029E4 File Offset: 0x00000BE4
		public int ReadSize
		{
			get
			{
				return this.csvFieldCacheImpl.ReadSize;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000029F1 File Offset: 0x00000BF1
		public CsvTable Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000029F9 File Offset: 0x00000BF9
		internal CsvFieldCache CsvFieldCacheImpl
		{
			get
			{
				return this.csvFieldCacheImpl;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002A01 File Offset: 0x00000C01
		public static string NormalizeMessageID(string messageId)
		{
			return CsvFieldCache.NormalizeMessageID(messageId);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002A09 File Offset: 0x00000C09
		public static string NormalizeEmailAddress(string email)
		{
			return CsvFieldCache.NormalizeEmailAddress(email);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002A11 File Offset: 0x00000C11
		public static FileStream OpenLogFile(string filePath)
		{
			return CsvFieldCache.OpenLogFile(filePath);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002A19 File Offset: 0x00000C19
		public static FileStream OpenLogFile(string filePath, out string version)
		{
			return CsvFieldCache.OpenLogFile(filePath, out version);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002A22 File Offset: 0x00000C22
		public static string DecodeString(byte[] src, int offset, int count)
		{
			return CsvFieldCache.DecodeString(src, offset, count);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002A2C File Offset: 0x00000C2C
		public object GetField(int index)
		{
			return this.csvFieldCacheImpl.GetField(index);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002A3A File Offset: 0x00000C3A
		public byte[] GetFieldRaw(int index)
		{
			return this.csvFieldCacheImpl.GetFieldRaw(index);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002A48 File Offset: 0x00000C48
		public int CopyRow(int srcOffset, byte[] dest, int offset, int count)
		{
			return this.csvFieldCacheImpl.CopyRow(srcOffset, dest, offset, count);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002A5A File Offset: 0x00000C5A
		public bool MoveNext(bool skipHeaders = false)
		{
			return this.csvFieldCacheImpl.MoveNext(skipHeaders);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002A68 File Offset: 0x00000C68
		public long Seek(long position)
		{
			return this.csvFieldCacheImpl.Seek(position);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002A76 File Offset: 0x00000C76
		public void SetMappingTableBasedOnCurrentRecord()
		{
			this.csvFieldCacheImpl.SetMappingTableBasedOnCurrentRecord();
		}

		// Token: 0x0400002C RID: 44
		public const int DefaultReadSize = 32768;

		// Token: 0x0400002D RID: 45
		private CsvFieldCache csvFieldCacheImpl;

		// Token: 0x0400002E RID: 46
		private CsvTable schema;
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x02000013 RID: 19
	public class ReadOnlyRow
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00002B1F File Offset: 0x00000D1F
		public ReadOnlyRow(CsvFieldCache cursor, long position)
		{
			this.schema = cursor.Schema;
			this.readOnlyRowImpl = new ReadOnlyRow(cursor.CsvFieldCacheImpl, position);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002B45 File Offset: 0x00000D45
		public long Position
		{
			get
			{
				return this.readOnlyRowImpl.Position;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002B52 File Offset: 0x00000D52
		public long EndPosition
		{
			get
			{
				return this.readOnlyRowImpl.EndPosition;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002B5F File Offset: 0x00000D5F
		public CsvTable Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002B67 File Offset: 0x00000D67
		public T GetField<T>(int field)
		{
			return this.readOnlyRowImpl.GetField<T>(field);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002B75 File Offset: 0x00000D75
		public T GetField<T>(string fieldName)
		{
			return this.readOnlyRowImpl.GetField<T>(fieldName);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002B83 File Offset: 0x00000D83
		public byte[] GetFieldRaw(string fieldName)
		{
			return this.readOnlyRowImpl.GetFieldRaw(fieldName);
		}

		// Token: 0x04000031 RID: 49
		private ReadOnlyRow readOnlyRowImpl;

		// Token: 0x04000032 RID: 50
		private CsvTable schema;
	}
}

using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001C8 RID: 456
	internal class ReadOnlyRow
	{
		// Token: 0x06000CBB RID: 3259 RVA: 0x0002EF4F File Offset: 0x0002D14F
		public ReadOnlyRow(CsvFieldCache cursor, long position)
		{
			this.cursor = cursor;
			this.position = position;
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0002EF65 File Offset: 0x0002D165
		public long Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0002EF6D File Offset: 0x0002D16D
		public long EndPosition
		{
			get
			{
				return this.cursor.Position;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0002EF7A File Offset: 0x0002D17A
		public CsvTable Schema
		{
			get
			{
				return this.cursor.Schema;
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002EF87 File Offset: 0x0002D187
		public T GetField<T>(int field)
		{
			return (T)((object)this.cursor.GetField(field));
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002EF9C File Offset: 0x0002D19C
		public T GetField<T>(string fieldName)
		{
			int field = this.Schema.NameToIndex(fieldName);
			return this.GetField<T>(field);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002EFBD File Offset: 0x0002D1BD
		public byte[] GetFieldRaw(string fieldName)
		{
			return this.cursor.GetFieldRaw(this.Schema.NameToIndex(fieldName));
		}

		// Token: 0x04000973 RID: 2419
		private readonly long position;

		// Token: 0x04000974 RID: 2420
		private CsvFieldCache cursor;
	}
}

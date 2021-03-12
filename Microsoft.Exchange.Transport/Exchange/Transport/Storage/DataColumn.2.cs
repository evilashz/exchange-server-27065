using System;
using Microsoft.Exchange.Data;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000AB RID: 171
	internal class DataColumn<T> : DataColumn where T : struct, IEquatable<T>
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x000189CE File Offset: 0x00016BCE
		internal DataColumn(JET_coltyp type, bool fixedSize) : base(type, fixedSize)
		{
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000189D8 File Offset: 0x00016BD8
		internal DataColumn(JET_coltyp type, bool fixedSize, int size) : base(type, fixedSize, size)
		{
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x000189E4 File Offset: 0x00016BE4
		private static DataColumn<T>.Formatter<T> Default
		{
			get
			{
				DataColumn<T>.Formatter<T> formatter = DataColumn<T>.formatter;
				if (formatter != null)
				{
					return formatter;
				}
				return DataColumn<T>.CreateFormatter();
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00018A04 File Offset: 0x00016C04
		public T ReadFromCursor(DataTableCursor cursor)
		{
			if (typeof(T) == typeof(string))
			{
				string text = base.StringFromCursor(cursor);
				return (T)((object)text);
			}
			if (typeof(T) == typeof(bool))
			{
				bool flag = base.BoolFromCursor(cursor) ?? false;
				return (T)((object)flag);
			}
			if (typeof(T) == typeof(long))
			{
				long num = base.Int64FromCursor(cursor) ?? 0L;
				return (T)((object)num);
			}
			byte[] data = base.BytesFromCursor(cursor, false, 1);
			return DataColumn<T>.Default.FromBytes(data);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00018ADC File Offset: 0x00016CDC
		public void WriteToCursor(DataTableCursor cursor, T value)
		{
			try
			{
				Api.SetColumn(cursor.Session, cursor.TableId, base.ColumnId, this.BytesFromValue(value));
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, cursor.Connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00018B34 File Offset: 0x00016D34
		internal override ColumnCache NewCacheCell()
		{
			return new ColumnCacheValueType<T>();
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00018B3C File Offset: 0x00016D3C
		internal override void ColumnValueToCache(ColumnValue data, ColumnCache cache)
		{
			BytesColumnValue bytesColumnValue = data as BytesColumnValue;
			if (bytesColumnValue == null)
			{
				ColumnValueOfStruct<T> columnValueOfStruct = (ColumnValueOfStruct<T>)data;
				((ColumnCache<T>)cache).Value = (columnValueOfStruct.Value ?? default(T));
				return;
			}
			if (bytesColumnValue.Value == null)
			{
				((ColumnCache<T>)cache).Value = default(T);
				return;
			}
			if (typeof(T) == typeof(IPvxAddress))
			{
				((ColumnCache<IPvxAddress>)cache).Value = DataColumn<IPvxAddress>.Default.FromBytes(bytesColumnValue.Value);
				return;
			}
			((ColumnCache<byte[]>)cache).Value = bytesColumnValue.Value;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00018BEE File Offset: 0x00016DEE
		internal override byte[] BytesFromCache(ColumnCache cache)
		{
			return this.BytesFromValue(((ColumnCache<T>)cache).Value);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00018C01 File Offset: 0x00016E01
		internal byte[] BytesFromValue(T value)
		{
			return DataColumn<T>.Default.ToBytes(value);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00018C10 File Offset: 0x00016E10
		private static DataColumn<T>.Formatter<T> CreateFormatter()
		{
			Type typeFromHandle = typeof(T);
			if (typeFromHandle == typeof(int))
			{
				DataColumn<T>.FormatterInt32 formatterInt = new DataColumn<T>.FormatterInt32();
				DataColumn<T>.formatter = (formatterInt as DataColumn<T>.Formatter<T>);
			}
			else if (typeFromHandle == typeof(long))
			{
				DataColumn<T>.FormatterInt64 formatterInt2 = new DataColumn<T>.FormatterInt64();
				DataColumn<T>.formatter = (formatterInt2 as DataColumn<T>.Formatter<T>);
			}
			else if (typeFromHandle == typeof(byte))
			{
				DataColumn<T>.FormatterByte formatterByte = new DataColumn<T>.FormatterByte();
				DataColumn<T>.formatter = (formatterByte as DataColumn<T>.Formatter<T>);
			}
			else if (typeFromHandle == typeof(bool))
			{
				DataColumn<T>.FormatterBool formatterBool = new DataColumn<T>.FormatterBool();
				DataColumn<T>.formatter = (formatterBool as DataColumn<T>.Formatter<T>);
			}
			else if (typeFromHandle == typeof(Guid))
			{
				DataColumn<T>.FormatterGuid formatterGuid = new DataColumn<T>.FormatterGuid();
				DataColumn<T>.formatter = (formatterGuid as DataColumn<T>.Formatter<T>);
			}
			else if (typeFromHandle == typeof(DateTime))
			{
				DataColumn<T>.FormatterDateTime formatterDateTime = new DataColumn<T>.FormatterDateTime();
				DataColumn<T>.formatter = (formatterDateTime as DataColumn<T>.Formatter<T>);
			}
			else
			{
				if (!(typeFromHandle == typeof(IPvxAddress)))
				{
					throw new InvalidCastException();
				}
				DataColumn<T>.FormatterIPAddressBytes formatterIPAddressBytes = new DataColumn<T>.FormatterIPAddressBytes();
				DataColumn<T>.formatter = (formatterIPAddressBytes as DataColumn<T>.Formatter<T>);
			}
			return DataColumn<T>.formatter;
		}

		// Token: 0x040002FA RID: 762
		private static DataColumn<T>.Formatter<T> formatter;

		// Token: 0x020000AC RID: 172
		private abstract class Formatter<Td>
		{
			// Token: 0x0600060A RID: 1546
			public abstract Td FromBytes(byte[] data);

			// Token: 0x0600060B RID: 1547
			public abstract byte[] ToBytes(Td value);
		}

		// Token: 0x020000AD RID: 173
		private class FormatterInt32 : DataColumn<T>.Formatter<int>
		{
			// Token: 0x0600060D RID: 1549 RVA: 0x00018D4F File Offset: 0x00016F4F
			public override int FromBytes(byte[] data)
			{
				if (data != null)
				{
					return BitConverter.ToInt32(data, 0);
				}
				return 0;
			}

			// Token: 0x0600060E RID: 1550 RVA: 0x00018D5D File Offset: 0x00016F5D
			public override byte[] ToBytes(int value)
			{
				return BitConverter.GetBytes(value);
			}
		}

		// Token: 0x020000AE RID: 174
		private class FormatterInt64 : DataColumn<T>.Formatter<long>
		{
			// Token: 0x06000610 RID: 1552 RVA: 0x00018D6D File Offset: 0x00016F6D
			public override long FromBytes(byte[] data)
			{
				if (data != null)
				{
					return BitConverter.ToInt64(data, 0);
				}
				return 0L;
			}

			// Token: 0x06000611 RID: 1553 RVA: 0x00018D7C File Offset: 0x00016F7C
			public override byte[] ToBytes(long value)
			{
				return BitConverter.GetBytes(value);
			}
		}

		// Token: 0x020000AF RID: 175
		private class FormatterByte : DataColumn<T>.Formatter<byte>
		{
			// Token: 0x06000613 RID: 1555 RVA: 0x00018D8C File Offset: 0x00016F8C
			public override byte FromBytes(byte[] data)
			{
				if (data != null)
				{
					return data[0];
				}
				return 0;
			}

			// Token: 0x06000614 RID: 1556 RVA: 0x00018D98 File Offset: 0x00016F98
			public override byte[] ToBytes(byte value)
			{
				return new byte[]
				{
					value
				};
			}
		}

		// Token: 0x020000B0 RID: 176
		private class FormatterBool : DataColumn<T>.Formatter<bool>
		{
			// Token: 0x06000616 RID: 1558 RVA: 0x00018DB9 File Offset: 0x00016FB9
			public override bool FromBytes(byte[] data)
			{
				return data != null && BitConverter.ToBoolean(data, 0);
			}

			// Token: 0x06000617 RID: 1559 RVA: 0x00018DC7 File Offset: 0x00016FC7
			public override byte[] ToBytes(bool value)
			{
				return BitConverter.GetBytes(value);
			}
		}

		// Token: 0x020000B1 RID: 177
		private class FormatterGuid : DataColumn<T>.Formatter<Guid>
		{
			// Token: 0x06000619 RID: 1561 RVA: 0x00018DD7 File Offset: 0x00016FD7
			public override Guid FromBytes(byte[] data)
			{
				if (data != null)
				{
					return new Guid(data);
				}
				return Guid.Empty;
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x00018DE8 File Offset: 0x00016FE8
			public override byte[] ToBytes(Guid value)
			{
				return value.ToByteArray();
			}
		}

		// Token: 0x020000B2 RID: 178
		private class FormatterDateTime : DataColumn<T>.Formatter<DateTime>
		{
			// Token: 0x0600061C RID: 1564 RVA: 0x00018DFC File Offset: 0x00016FFC
			public override DateTime FromBytes(byte[] data)
			{
				if (data != null)
				{
					return DateTime.FromOADate(BitConverter.ToDouble(data, 0));
				}
				return default(DateTime);
			}

			// Token: 0x0600061D RID: 1565 RVA: 0x00018E22 File Offset: 0x00017022
			public override byte[] ToBytes(DateTime value)
			{
				return BitConverter.GetBytes(value.ToOADate());
			}
		}

		// Token: 0x020000B3 RID: 179
		private class FormatterIPAddressBytes : DataColumn<T>.Formatter<IPvxAddress>
		{
			// Token: 0x0600061F RID: 1567 RVA: 0x00018E38 File Offset: 0x00017038
			public override IPvxAddress FromBytes(byte[] data)
			{
				if (data != null)
				{
					return new IPvxAddress(data);
				}
				return default(IPvxAddress);
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x00018E58 File Offset: 0x00017058
			public override byte[] ToBytes(IPvxAddress value)
			{
				return value.GetBytes();
			}
		}
	}
}

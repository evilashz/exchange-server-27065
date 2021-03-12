using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003EE RID: 1006
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class OccurrenceStoreObjectId : StoreObjectId, IEquatable<OccurrenceStoreObjectId>, IComparable<OccurrenceStoreObjectId>
	{
		// Token: 0x06002DFD RID: 11773 RVA: 0x000BD48A File Offset: 0x000BB68A
		public OccurrenceStoreObjectId(byte[] entryId, ExDateTime date) : base(entryId, StoreObjectType.CalendarItemOccurrence)
		{
			this.occurrenceDate = date.Date;
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x000BD4A4 File Offset: 0x000BB6A4
		public OccurrenceStoreObjectId(byte[] byteArray, int startingIndex) : base(byteArray, startingIndex + 8 + 1)
		{
			if (byteArray == null)
			{
				throw new ArgumentNullException("byteArray", ServerStrings.ExInvalidIdFormat);
			}
			if (byteArray[startingIndex] != 8)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidIdFormat);
			}
			long num = 0L;
			for (int i = 0; i < 8; i++)
			{
				num <<= 8;
				num |= (long)((ulong)byteArray[startingIndex + 1 + i]);
			}
			this.occurrenceDate = ExDateTime.FromBinary(num);
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000BD510 File Offset: 0x000BB710
		public OccurrenceStoreObjectId(byte dateSize, byte[] dateBytes, byte[] storeObjectIdByteArray) : base(storeObjectIdByteArray, 0)
		{
			if (dateBytes == null)
			{
				throw new ArgumentNullException("dateBytes", ServerStrings.ExInvalidIdFormat);
			}
			if (storeObjectIdByteArray == null)
			{
				throw new ArgumentNullException("storeObjectIdByteArray", ServerStrings.ExInvalidIdFormat);
			}
			if (dateSize != 8)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidIdFormat);
			}
			long num = 0L;
			for (int i = 0; i < 8; i++)
			{
				num <<= 8;
				num |= (long)((ulong)dateBytes[i]);
			}
			this.occurrenceDate = ExDateTime.FromBinary(num);
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000BD58C File Offset: 0x000BB78C
		public override byte[] GetBytes()
		{
			byte[] bytes = base.GetBytes();
			byte[] array = new byte[bytes.Length + 8 + 1];
			array[0] = 8;
			bytes.CopyTo(array, 9);
			long num = this.occurrenceDate.ToBinary();
			long num2 = 8L;
			for (;;)
			{
				long num3 = num2;
				num2 = num3 - 1L;
				if (num3 <= 0L)
				{
					break;
				}
				array[(int)(checked((IntPtr)(unchecked(1L + num2))))] = (byte)(num & 255L);
				num >>= 8;
			}
			return array;
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000BD5F0 File Offset: 0x000BB7F0
		public override bool Equals(object id)
		{
			OccurrenceStoreObjectId id2 = id as OccurrenceStoreObjectId;
			return this.Equals(id2);
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000BD60C File Offset: 0x000BB80C
		public override bool Equals(StoreObjectId id)
		{
			OccurrenceStoreObjectId id2 = id as OccurrenceStoreObjectId;
			return this.Equals(id2);
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000BD628 File Offset: 0x000BB828
		public bool Equals(OccurrenceStoreObjectId id)
		{
			return id != null && base.Equals(id) && this.occurrenceDate.Equals(id.occurrenceDate);
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000BD658 File Offset: 0x000BB858
		public override int CompareTo(object o)
		{
			if (o == null)
			{
				return 1;
			}
			if (!base.GetType().Equals(o.GetType()))
			{
				throw new ArgumentException();
			}
			OccurrenceStoreObjectId o2 = (OccurrenceStoreObjectId)o;
			return this.CompareTo(o2);
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x000BD694 File Offset: 0x000BB894
		public int CompareTo(OccurrenceStoreObjectId o)
		{
			if (o == null)
			{
				return 1;
			}
			int num = base.CompareTo(o);
			if (num != 0)
			{
				return num;
			}
			return this.occurrenceDate.CompareTo(o.occurrenceDate);
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x000BD6C7 File Offset: 0x000BB8C7
		public override StoreObjectId Clone()
		{
			return new OccurrenceStoreObjectId(this.EntryId, this.occurrenceDate);
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x000BD6DA File Offset: 0x000BB8DA
		public ExDateTime OccurrenceId
		{
			get
			{
				return this.occurrenceDate;
			}
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000BD6E2 File Offset: 0x000BB8E2
		internal StoreObjectId GetMasterStoreObjectId()
		{
			return StoreObjectId.FromProviderSpecificId(base.ProviderLevelItemId, StoreObjectType.CalendarItem);
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000BD6F1 File Offset: 0x000BB8F1
		public override void UpdateItemType(StoreObjectType newItemType)
		{
			if (newItemType != StoreObjectType.CalendarItemOccurrence)
			{
				throw new EnumArgumentException("OccurrenceStoreObjectId should always have type of CalendarItemOccurrence", "newItemType");
			}
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000BD708 File Offset: 0x000BB908
		public override int GetByteArrayLength()
		{
			return 9 + base.GetByteArrayLength();
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000BD714 File Offset: 0x000BB914
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.occurrenceDate.GetHashCode();
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000BD73C File Offset: 0x000BB93C
		protected override void WriteBytes(BinaryWriter writer)
		{
			writer.Write(8);
			long num = this.occurrenceDate.ToBinary();
			long num2 = 8L;
			for (;;)
			{
				long num3 = num2;
				num2 = num3 - 1L;
				if (num3 <= 0L)
				{
					break;
				}
				writer.Write((byte)(num >> (int)(8L * num2) & 255L));
			}
			base.WriteBytes(writer);
		}

		// Token: 0x04001924 RID: 6436
		private readonly ExDateTime occurrenceDate;
	}
}

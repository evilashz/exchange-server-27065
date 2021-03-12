using System;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public abstract class MapiObjectId : ObjectId, IEquatable<MapiObjectId>, IComparable<MapiObjectId>, IComparable
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000244C File Offset: 0x0000064C
		public static bool operator ==(MapiObjectId operand1, MapiObjectId operand2)
		{
			return object.Equals(operand1, operand2);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002455 File Offset: 0x00000655
		public static bool operator !=(MapiObjectId operand1, MapiObjectId operand2)
		{
			return !object.Equals(operand1, operand2);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002461 File Offset: 0x00000661
		public virtual bool Equals(MapiObjectId other)
		{
			if (object.ReferenceEquals(null, other))
			{
				return false;
			}
			if (this.MapiEntryId != null)
			{
				return object.Equals(this.MapiEntryId, other.MapiEntryId);
			}
			return base.Equals(other);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002498 File Offset: 0x00000698
		public int CompareTo(MapiObjectId other)
		{
			if (null == other)
			{
				throw new ArgumentException("The object is not a MapiObjectId");
			}
			if (!(null == this.MapiEntryId))
			{
				return this.MapiEntryId.CompareTo(other.MapiEntryId);
			}
			if (!(null == other.MapiEntryId))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024EA File Offset: 0x000006EA
		int IComparable.CompareTo(object obj)
		{
			return this.CompareTo(obj as MapiObjectId);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024F8 File Offset: 0x000006F8
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MapiObjectId);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002506 File Offset: 0x00000706
		public override int GetHashCode()
		{
			if (!(null == this.MapiEntryId))
			{
				return this.MapiEntryId.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002528 File Offset: 0x00000728
		public override byte[] GetBytes()
		{
			return (byte[])this.MapiEntryId;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002535 File Offset: 0x00000735
		public MapiEntryId MapiEntryId
		{
			get
			{
				return this.mapiEntryId;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000253D File Offset: 0x0000073D
		public MapiObjectId()
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002545 File Offset: 0x00000745
		public MapiObjectId(byte[] bytes)
		{
			if (bytes != null)
			{
				this.mapiEntryId = new MapiEntryId(bytes);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000255C File Offset: 0x0000075C
		public MapiObjectId(MapiEntryId mapiEntryId)
		{
			this.mapiEntryId = mapiEntryId;
		}

		// Token: 0x04000004 RID: 4
		private readonly MapiEntryId mapiEntryId;
	}
}

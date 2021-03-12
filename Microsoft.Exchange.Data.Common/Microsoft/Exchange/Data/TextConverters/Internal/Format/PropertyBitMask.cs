using System;
using System.Text;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002BE RID: 702
	internal struct PropertyBitMask
	{
		// Token: 0x06001BF9 RID: 7161 RVA: 0x000D61BA File Offset: 0x000D43BA
		internal PropertyBitMask(uint bits1, uint bits2)
		{
			this.Bits1 = bits1;
			this.Bits2 = bits2;
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x000D61CA File Offset: 0x000D43CA
		public bool IsClear
		{
			get
			{
				return this.Bits1 == 0U && 0U == this.Bits2;
			}
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000D61DF File Offset: 0x000D43DF
		public static PropertyBitMask operator |(PropertyBitMask x, PropertyBitMask y)
		{
			return new PropertyBitMask(x.Bits1 | y.Bits1, x.Bits2 | y.Bits2);
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x000D6204 File Offset: 0x000D4404
		public static PropertyBitMask operator &(PropertyBitMask x, PropertyBitMask y)
		{
			return new PropertyBitMask(x.Bits1 & y.Bits1, x.Bits2 & y.Bits2);
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000D6229 File Offset: 0x000D4429
		public static PropertyBitMask operator ^(PropertyBitMask x, PropertyBitMask y)
		{
			return new PropertyBitMask(x.Bits1 ^ y.Bits1, x.Bits2 ^ y.Bits2);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x000D624E File Offset: 0x000D444E
		public static PropertyBitMask operator ~(PropertyBitMask x)
		{
			return new PropertyBitMask(~x.Bits1, ~x.Bits2);
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000D6265 File Offset: 0x000D4465
		public static bool operator ==(PropertyBitMask x, PropertyBitMask y)
		{
			return x.Bits1 == y.Bits1 && x.Bits2 == y.Bits2;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x000D6289 File Offset: 0x000D4489
		public static bool operator !=(PropertyBitMask x, PropertyBitMask y)
		{
			return x.Bits1 != y.Bits1 || x.Bits2 != y.Bits2;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x000D62B0 File Offset: 0x000D44B0
		public void Or(PropertyBitMask newBits)
		{
			this.Bits1 |= newBits.Bits1;
			this.Bits2 |= newBits.Bits2;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000D62DA File Offset: 0x000D44DA
		public bool IsSet(PropertyId id)
		{
			return 0U != ((id < PropertyId.ListLevel) ? (this.Bits1 & 1U << (int)(id - PropertyId.FontColor)) : (this.Bits2 & 1U << (int)(id - PropertyId.FontColor - 32)));
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000D630E File Offset: 0x000D450E
		public bool IsNotSet(PropertyId id)
		{
			return 0U == ((id < PropertyId.ListLevel) ? (this.Bits1 & 1U << (int)(id - PropertyId.FontColor)) : (this.Bits2 & 1U << (int)(id - PropertyId.FontColor - 32)));
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000D633F File Offset: 0x000D453F
		public void Set(PropertyId id)
		{
			if (id < PropertyId.ListLevel)
			{
				this.Bits1 |= 1U << (int)(id - PropertyId.FontColor);
				return;
			}
			this.Bits2 |= 1U << (int)(id - PropertyId.FontColor - 32);
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x000D6378 File Offset: 0x000D4578
		public void Clear(PropertyId id)
		{
			if (id < PropertyId.ListLevel)
			{
				this.Bits1 &= ~(1U << (int)(id - PropertyId.FontColor));
				return;
			}
			this.Bits2 &= ~(1U << (int)(id - PropertyId.FontColor - 32));
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x000D63B3 File Offset: 0x000D45B3
		public bool IsSubsetOf(PropertyBitMask overrideFlags)
		{
			return (this.Bits1 & ~(overrideFlags.Bits1 != 0U)) == 0U && 0U == (this.Bits2 & ~overrideFlags.Bits2);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000D63DA File Offset: 0x000D45DA
		public void ClearAll()
		{
			this.Bits1 = 0U;
			this.Bits2 = 0U;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x000D63EA File Offset: 0x000D45EA
		public override bool Equals(object obj)
		{
			return obj is PropertyBitMask && this.Bits1 == ((PropertyBitMask)obj).Bits1 && this.Bits2 == ((PropertyBitMask)obj).Bits2;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x000D641C File Offset: 0x000D461C
		public override int GetHashCode()
		{
			return (int)(this.Bits1 ^ this.Bits2);
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000D642C File Offset: 0x000D462C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(896);
			for (PropertyId propertyId = PropertyId.FontColor; propertyId < PropertyId.MaxValue; propertyId += 1)
			{
				if (this.IsSet(propertyId))
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(propertyId.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x000D6488 File Offset: 0x000D4688
		public PropertyBitMask.DefinedPropertyIdEnumerator GetEnumerator()
		{
			return new PropertyBitMask.DefinedPropertyIdEnumerator(this);
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000D6495 File Offset: 0x000D4695
		internal void Set1(uint bits1)
		{
			this.Bits1 = bits1;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x000D649E File Offset: 0x000D469E
		internal void Set2(uint bits2)
		{
			this.Bits2 = bits2;
		}

		// Token: 0x04002139 RID: 8505
		public const PropertyId FirstNonFlag = PropertyId.FontColor;

		// Token: 0x0400213A RID: 8506
		public static readonly PropertyBitMask AllOff = new PropertyBitMask(0U, 0U);

		// Token: 0x0400213B RID: 8507
		public static readonly PropertyBitMask AllOn = new PropertyBitMask(uint.MaxValue, uint.MaxValue);

		// Token: 0x0400213C RID: 8508
		internal uint Bits1;

		// Token: 0x0400213D RID: 8509
		internal uint Bits2;

		// Token: 0x020002BF RID: 703
		public struct DefinedPropertyIdEnumerator
		{
			// Token: 0x06001C0F RID: 7183 RVA: 0x000D64C1 File Offset: 0x000D46C1
			internal DefinedPropertyIdEnumerator(PropertyBitMask mask)
			{
				this.Bits = ((ulong)mask.Bits2 << 32 | (ulong)mask.Bits1);
				this.CurrentBit = 1UL;
				this.CurrentId = ((this.Bits != 0UL) ? PropertyId.MergedCell : PropertyId.MaxValue);
			}

			// Token: 0x1700072E RID: 1838
			// (get) Token: 0x06001C10 RID: 7184 RVA: 0x000D64FB File Offset: 0x000D46FB
			public PropertyId Current
			{
				get
				{
					return this.CurrentId;
				}
			}

			// Token: 0x06001C11 RID: 7185 RVA: 0x000D6504 File Offset: 0x000D4704
			public bool MoveNext()
			{
				while (this.CurrentId != PropertyId.MaxValue)
				{
					if (this.CurrentId != PropertyId.MergedCell)
					{
						this.CurrentBit <<= 1;
					}
					this.CurrentId += 1;
					if (this.CurrentId != PropertyId.MaxValue && 0UL != (this.Bits & this.CurrentBit))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0400213E RID: 8510
			internal ulong Bits;

			// Token: 0x0400213F RID: 8511
			internal ulong CurrentBit;

			// Token: 0x04002140 RID: 8512
			internal PropertyId CurrentId;
		}
	}
}

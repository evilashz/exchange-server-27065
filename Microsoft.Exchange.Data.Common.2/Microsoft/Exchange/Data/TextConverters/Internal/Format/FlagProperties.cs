using System;
using System.Text;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002BD RID: 701
	internal struct FlagProperties
	{
		// Token: 0x06001BDA RID: 7130 RVA: 0x000D5E0D File Offset: 0x000D400D
		internal FlagProperties(uint bits)
		{
			this.bits = bits;
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x000D5E16 File Offset: 0x000D4016
		public bool IsClear
		{
			get
			{
				return 0U == this.bits;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x000D5E21 File Offset: 0x000D4021
		public uint Mask
		{
			get
			{
				return (this.bits & 2863311530U) | (this.bits & 2863311530U) >> 1;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x000D5E3E File Offset: 0x000D403E
		public uint Bits
		{
			get
			{
				return this.bits;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x000D5E46 File Offset: 0x000D4046
		// (set) Token: 0x06001BDF RID: 7135 RVA: 0x000D5E4E File Offset: 0x000D404E
		internal int IntegerBag
		{
			get
			{
				return (int)this.bits;
			}
			set
			{
				this.bits = (uint)value;
			}
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x000D5E57 File Offset: 0x000D4057
		public static bool IsFlagProperty(PropertyId id)
		{
			return id >= PropertyId.FirstFlag && id <= PropertyId.MergedCell;
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x000D5E67 File Offset: 0x000D4067
		public static FlagProperties Merge(FlagProperties baseFlags, FlagProperties overrideFlags)
		{
			return new FlagProperties((baseFlags.bits & ~((overrideFlags.bits & 2863311530U) >> 1)) | overrideFlags.bits);
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x000D5E8E File Offset: 0x000D408E
		public static FlagProperties operator &(FlagProperties x, FlagProperties y)
		{
			return new FlagProperties(x.bits & ((y.bits & 2863311530U) | (y.bits & 2863311530U) >> 1));
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000D5EBA File Offset: 0x000D40BA
		public static FlagProperties operator |(FlagProperties x, FlagProperties y)
		{
			return FlagProperties.Merge(x, y);
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000D5EC4 File Offset: 0x000D40C4
		public static FlagProperties operator ^(FlagProperties x, FlagProperties y)
		{
			uint num = (x.bits ^ y.bits) & x.Mask & y.Mask;
			return new FlagProperties(num | num << 1);
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x000D5EFB File Offset: 0x000D40FB
		public static FlagProperties operator ~(FlagProperties x)
		{
			return new FlagProperties(~((x.bits & 2863311530U) | (x.bits & 2863311530U) >> 1));
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000D5F20 File Offset: 0x000D4120
		public static bool operator ==(FlagProperties x, FlagProperties y)
		{
			return x.bits == y.bits;
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000D5F32 File Offset: 0x000D4132
		public static bool operator !=(FlagProperties x, FlagProperties y)
		{
			return x.bits != y.bits;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000D5F48 File Offset: 0x000D4148
		public void Set(PropertyId id, bool value)
		{
			int num = (int)((id - PropertyId.FirstFlag) * 2);
			if (value)
			{
				this.bits |= 3U << num;
				return;
			}
			this.bits &= ~(1U << num);
			this.bits |= 2U << num;
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x000D5F9A File Offset: 0x000D419A
		public void Remove(PropertyId id)
		{
			this.bits &= ~(3U << (int)((id - PropertyId.FirstFlag) * 2));
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x000D5FB5 File Offset: 0x000D41B5
		public void ClearAll()
		{
			this.bits = 0U;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000D5FBE File Offset: 0x000D41BE
		public bool IsDefined(PropertyId id)
		{
			return 0U != (this.bits & 2U << (int)((id - PropertyId.FirstFlag) * 2));
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x000D5FD8 File Offset: 0x000D41D8
		public bool IsAnyDefined()
		{
			return this.bits != 0U;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x000D5FE6 File Offset: 0x000D41E6
		public bool IsOn(PropertyId id)
		{
			return 0U != (this.bits & 1U << (int)((id - PropertyId.FirstFlag) * 2));
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x000D6000 File Offset: 0x000D4200
		public bool IsDefinedAndOn(PropertyId id)
		{
			return 3U == (this.bits >> (int)((id - PropertyId.FirstFlag) * 2) & 3U);
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x000D6017 File Offset: 0x000D4217
		public bool IsDefinedAndOff(PropertyId id)
		{
			return 2U == (this.bits >> (int)((id - PropertyId.FirstFlag) * 2) & 3U);
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x000D6030 File Offset: 0x000D4230
		public PropertyValue GetPropertyValue(PropertyId id)
		{
			int num = (int)((id - PropertyId.FirstFlag) * 2);
			if ((this.bits & 2U << num) != 0U)
			{
				return new PropertyValue(0U != (this.bits & 1U << num));
			}
			return PropertyValue.Null;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x000D6071 File Offset: 0x000D4271
		public void SetPropertyValue(PropertyId id, PropertyValue value)
		{
			if (value.IsBool)
			{
				this.Set(id, value.Bool);
			}
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x000D608A File Offset: 0x000D428A
		public bool IsSubsetOf(FlagProperties overrideFlags)
		{
			return 0U == (this.bits & 2863311530U & ~(overrideFlags.bits & 2863311530U));
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x000D60AA File Offset: 0x000D42AA
		public void Merge(FlagProperties overrideFlags)
		{
			this.bits = ((this.bits & ~((overrideFlags.bits & 2863311530U) >> 1)) | overrideFlags.bits);
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000D60D1 File Offset: 0x000D42D1
		public void ReverseMerge(FlagProperties baseFlags)
		{
			this.bits = ((baseFlags.bits & ~((this.bits & 2863311530U) >> 1)) | this.bits);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x000D60F7 File Offset: 0x000D42F7
		public override bool Equals(object obj)
		{
			return obj is FlagProperties && this.bits == ((FlagProperties)obj).bits;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000D6116 File Offset: 0x000D4316
		public override int GetHashCode()
		{
			return (int)this.bits;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000D6120 File Offset: 0x000D4320
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(240);
			for (PropertyId propertyId = PropertyId.FirstFlag; propertyId <= PropertyId.MergedCell; propertyId += 1)
			{
				if (this.IsDefined(propertyId))
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(propertyId.ToString());
					stringBuilder.Append(this.IsOn(propertyId) ? ":on" : ":off");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002130 RID: 8496
		private const uint AllDefinedBits = 2863311530U;

		// Token: 0x04002131 RID: 8497
		private const uint AllValueBits = 1431655765U;

		// Token: 0x04002132 RID: 8498
		private const uint ValueBit = 1U;

		// Token: 0x04002133 RID: 8499
		private const uint DefinedBit = 2U;

		// Token: 0x04002134 RID: 8500
		private const uint ValueAndDefinedBits = 3U;

		// Token: 0x04002135 RID: 8501
		public static readonly FlagProperties AllUndefined = new FlagProperties(0U);

		// Token: 0x04002136 RID: 8502
		public static readonly FlagProperties AllOff = new FlagProperties(0U);

		// Token: 0x04002137 RID: 8503
		public static readonly FlagProperties AllOn = new FlagProperties(uint.MaxValue);

		// Token: 0x04002138 RID: 8504
		private uint bits;
	}
}

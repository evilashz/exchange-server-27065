using System;
using System.ComponentModel;
using System.Globalization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001A0 RID: 416
	[ImmutableObject(true)]
	[Serializable]
	internal class EnumObject : IComparable, IComparable<EnumObject>, IFormattable, IConvertible
	{
		// Token: 0x06000EFC RID: 3836 RVA: 0x0002B788 File Offset: 0x00029988
		public EnumObject(Enum enumValue)
		{
			this.value = enumValue;
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0002B797 File Offset: 0x00029997
		public Enum Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x0002B79F File Offset: 0x0002999F
		private string LocalizedDescription
		{
			get
			{
				if (this.localizedDescription == null)
				{
					this.localizedDescription = ((this.Value == null) ? string.Empty : LocalizedDescriptionAttribute.FromEnum(this.Value.GetType(), this.Value));
				}
				return this.localizedDescription;
			}
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0002B7DA File Offset: 0x000299DA
		public static explicit operator EnumObject(Enum enumValue)
		{
			return new EnumObject(enumValue);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0002B7E2 File Offset: 0x000299E2
		public static implicit operator Enum(EnumObject enumObject)
		{
			if (enumObject != null)
			{
				return enumObject.Value;
			}
			return null;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0002B7F0 File Offset: 0x000299F0
		public override bool Equals(object right)
		{
			if (right == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, right))
			{
				return true;
			}
			if (base.GetType() != right.GetType())
			{
				return false;
			}
			EnumObject enumObject = (EnumObject)right;
			return object.Equals(this.Value, enumObject.Value);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0002B83A File Offset: 0x00029A3A
		public override int GetHashCode()
		{
			if (this.Value == null)
			{
				return 0;
			}
			return this.Value.GetType().GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0002B862 File Offset: 0x00029A62
		public override string ToString()
		{
			if (this.Value != null)
			{
				return this.Value.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0002B87D File Offset: 0x00029A7D
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return this.LocalizedDescription;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0002B888 File Offset: 0x00029A88
		public int CompareTo(EnumObject right)
		{
			string strA = this.LocalizedDescription;
			string strB = (right == null) ? string.Empty : right.LocalizedDescription;
			return string.Compare(strA, strB);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0002B8B4 File Offset: 0x00029AB4
		int IComparable.CompareTo(object right)
		{
			EnumObject enumObject = right as EnumObject;
			if (enumObject == null)
			{
				throw new ArgumentException("Argument must be of type EnumObject", "right");
			}
			return this.CompareTo(enumObject);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0002B8E2 File Offset: 0x00029AE2
		public TypeCode GetTypeCode()
		{
			return this.Value.GetTypeCode();
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0002B8EF File Offset: 0x00029AEF
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0002B901 File Offset: 0x00029B01
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0002B913 File Offset: 0x00029B13
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0002B925 File Offset: 0x00029B25
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0002B937 File Offset: 0x00029B37
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0002B949 File Offset: 0x00029B49
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0002B95B File Offset: 0x00029B5B
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0002B96D File Offset: 0x00029B6D
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0002B97F File Offset: 0x00029B7F
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0002B991 File Offset: 0x00029B91
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0002B9A3 File Offset: 0x00029BA3
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0002B9B5 File Offset: 0x00029BB5
		string IConvertible.ToString(IFormatProvider provider)
		{
			return this.ToString(null, provider);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0002B9BF File Offset: 0x00029BBF
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			if (base.GetType() == conversionType)
			{
				return this;
			}
			return ((IConvertible)this.Value).ToType(conversionType, provider);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0002B9DE File Offset: 0x00029BDE
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0002B9F0 File Offset: 0x00029BF0
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0002BA02 File Offset: 0x00029C02
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this.Value, CultureInfo.CurrentCulture);
		}

		// Token: 0x04000320 RID: 800
		private Enum value;

		// Token: 0x04000321 RID: 801
		private string localizedDescription;
	}
}

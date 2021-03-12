using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000B6 RID: 182
	[ImmutableObject(true)]
	[Serializable]
	public abstract class ProxyAddressBase : IComparable, IComparable<ProxyAddressBase>
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x00010BA0 File Offset: 0x0000EDA0
		protected ProxyAddressBase(ProxyAddressPrefix prefix, string valueString, bool isPrimaryAddress) : this(prefix, valueString, isPrimaryAddress, false)
		{
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00010BAC File Offset: 0x0000EDAC
		protected ProxyAddressBase(ProxyAddressPrefix prefix, string valueString, bool isPrimaryAddress, bool suppressAddressValidation)
		{
			if (null == prefix)
			{
				throw new ArgumentNullException("prefix");
			}
			if (valueString == null)
			{
				throw new ArgumentNullException("valueString");
			}
			if (!suppressAddressValidation)
			{
				ProxyAddressBase.ValidateAddressString(valueString);
			}
			this.prefix = prefix;
			this.valueString = valueString;
			this.isPrimaryAddress = isPrimaryAddress;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00010C00 File Offset: 0x0000EE00
		public ProxyAddressPrefix Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00010C08 File Offset: 0x0000EE08
		public bool IsPrimaryAddress
		{
			get
			{
				return this.isPrimaryAddress;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00010C10 File Offset: 0x0000EE10
		public string PrefixString
		{
			get
			{
				if (this.IsPrimaryAddress)
				{
					return this.Prefix.PrimaryPrefix;
				}
				return this.Prefix.SecondaryPrefix;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00010C31 File Offset: 0x0000EE31
		internal string ValueString
		{
			get
			{
				return this.valueString;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00010C39 File Offset: 0x0000EE39
		internal string ProxyAddressBaseString
		{
			get
			{
				return this.PrefixString + ':' + this.ValueString;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00010C54 File Offset: 0x0000EE54
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x00010C7A File Offset: 0x0000EE7A
		internal string RawProxyAddressBaseString
		{
			get
			{
				string result;
				if ((result = this.rawProxyAddressBaseString) == null)
				{
					result = (this.RawProxyAddressBaseString = this.ProxyAddressBaseString);
				}
				return result;
			}
			set
			{
				this.rawProxyAddressBaseString = value;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00010C83 File Offset: 0x0000EE83
		public override string ToString()
		{
			return this.ProxyAddressBaseString;
		}

		// Token: 0x060004AE RID: 1198
		public abstract ProxyAddressBase ToPrimary();

		// Token: 0x060004AF RID: 1199
		public abstract ProxyAddressBase ToSecondary();

		// Token: 0x060004B0 RID: 1200 RVA: 0x00010C8B File Offset: 0x0000EE8B
		public ProxyAddressBase GetSimilarProxy(string address)
		{
			if (this is ProxyAddress)
			{
				return this.Prefix.GetProxyAddress(address, this.IsPrimaryAddress);
			}
			if (this is ProxyAddressTemplate)
			{
				return this.Prefix.GetProxyAddressTemplate(address, this.IsPrimaryAddress);
			}
			return null;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00010CC4 File Offset: 0x0000EEC4
		public static void ValidateAddressString(string valueString)
		{
			if (!ProxyAddressBase.IsAddressStringValid(valueString))
			{
				throw new ArgumentOutOfRangeException(DataStrings.ProxyAddressShouldNotBeAllSpace, null);
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00010CDF File Offset: 0x0000EEDF
		public static bool IsAddressStringValid(string valueString)
		{
			return !string.IsNullOrEmpty(valueString) && !string.IsNullOrEmpty(valueString.Trim());
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00010CF9 File Offset: 0x0000EEF9
		public override int GetHashCode()
		{
			return (StringComparer.OrdinalIgnoreCase.GetHashCode(this.PrefixString) << 5) + StringComparer.OrdinalIgnoreCase.GetHashCode(this.ValueString);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00010D1E File Offset: 0x0000EF1E
		public override bool Equals(object obj)
		{
			return this == obj as ProxyAddressBase;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00010D2C File Offset: 0x0000EF2C
		public static bool Equals(ProxyAddressBase a, ProxyAddressBase b, StringComparison comparisonType)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			bool flag = string.Equals(a.PrefixString, b.PrefixString, comparisonType) && string.Equals(a.ValueString, b.ValueString, comparisonType);
			if (!flag && (a is InvalidProxyAddress || a is InvalidProxyAddressTemplate || b is InvalidProxyAddress || b is InvalidProxyAddressTemplate))
			{
				flag = string.Equals(a.RawProxyAddressBaseString, b.RawProxyAddressBaseString, comparisonType);
			}
			return flag;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00010DA8 File Offset: 0x0000EFA8
		public static bool operator ==(ProxyAddressBase a, ProxyAddressBase b)
		{
			return ProxyAddressBase.Equals(a, b, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00010DB2 File Offset: 0x0000EFB2
		public static bool operator !=(ProxyAddressBase a, ProxyAddressBase b)
		{
			return !(a == b);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ProxyAddressBase))
			{
				throw new ArgumentException(DataStrings.InvalidTypeArgumentException("other", other.GetType(), typeof(ProxyAddressBase)), "other");
			}
			return this.CompareTo((ProxyAddressBase)other);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00010E10 File Offset: 0x0000F010
		public int CompareTo(ProxyAddressBase other)
		{
			if (null == other)
			{
				return 1;
			}
			int num = StringComparer.OrdinalIgnoreCase.Compare(this.PrefixString, other.PrefixString);
			if (num != 0)
			{
				return num;
			}
			return StringComparer.OrdinalIgnoreCase.Compare(this.ValueString, other.ValueString);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00010E5A File Offset: 0x0000F05A
		public bool Equals(ProxyAddressBase other)
		{
			return this == other;
		}

		// Token: 0x040002E6 RID: 742
		internal const char PrefixValueSeparatorCharacter = ':';

		// Token: 0x040002E7 RID: 743
		public static readonly int MaxLength = 1123;

		// Token: 0x040002E8 RID: 744
		private readonly ProxyAddressPrefix prefix;

		// Token: 0x040002E9 RID: 745
		private readonly bool isPrimaryAddress;

		// Token: 0x040002EA RID: 746
		private readonly string valueString;

		// Token: 0x040002EB RID: 747
		private string rawProxyAddressBaseString;
	}
}

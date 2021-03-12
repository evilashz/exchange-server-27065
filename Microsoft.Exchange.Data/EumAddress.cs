using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000061 RID: 97
	[Serializable]
	public struct EumAddress : IEquatable<EumAddress>, IComparable<EumAddress>
	{
		// Token: 0x06000318 RID: 792 RVA: 0x0000D72A File Offset: 0x0000B92A
		public EumAddress(string address)
		{
			if (address != null && address.Length != 0)
			{
				this.address = address;
				return;
			}
			this.address = null;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000D746 File Offset: 0x0000B946
		public EumAddress(string extension, string phoneContext)
		{
			if (string.IsNullOrEmpty(extension))
			{
				throw new ArgumentNullException("extension");
			}
			if (string.IsNullOrEmpty(phoneContext))
			{
				throw new ArgumentNullException("phoneContext");
			}
			this.address = EumAddress.BuildAddressString(extension, phoneContext);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000D77B File Offset: 0x0000B97B
		internal static string BuildAddressString(string extension, string phoneContext)
		{
			return extension + ";" + "phone-context=" + phoneContext;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000D78E File Offset: 0x0000B98E
		public int Length
		{
			get
			{
				if (this.address != null)
				{
					return this.address.Length;
				}
				return 0;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000D7A8 File Offset: 0x0000B9A8
		public string Extension
		{
			get
			{
				if (this.address == null)
				{
					return null;
				}
				int num = this.address.LastIndexOf(";phone-context=");
				if (num != -1)
				{
					return this.address.Substring(0, num);
				}
				return null;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		public string PhoneContext
		{
			get
			{
				if (this.address == null)
				{
					return null;
				}
				int num = this.address.LastIndexOf(";phone-context=");
				if (num != -1)
				{
					return this.address.Substring(num + "phone-context=".Length + ";".Length);
				}
				return null;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000D834 File Offset: 0x0000BA34
		public bool IsValid
		{
			get
			{
				return this.address != null;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000D841 File Offset: 0x0000BA41
		public bool IsSipExtension
		{
			get
			{
				return !string.IsNullOrEmpty(this.Extension) && this.Extension.IndexOf("@", StringComparison.OrdinalIgnoreCase) > 0;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000D868 File Offset: 0x0000BA68
		public static bool IsValidEumAddress(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.IndexOf(";phone-context=") == -1)
			{
				return false;
			}
			string[] separator = new string[]
			{
				";phone-context="
			};
			string[] array = address.Split(separator, StringSplitOptions.None);
			if (array.Length != 2)
			{
				return false;
			}
			EumAddress eumAddress = EumAddress.Parse(address);
			return !string.IsNullOrEmpty(eumAddress.Extension) && !string.IsNullOrEmpty(eumAddress.PhoneContext);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000D8DC File Offset: 0x0000BADC
		public static EumAddress Parse(string proxyAddress)
		{
			EumAddress result = new EumAddress(proxyAddress);
			if (!result.IsValid)
			{
				throw new FormatException(DataStrings.InvalidEumAddress(proxyAddress));
			}
			return result;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000D90C File Offset: 0x0000BB0C
		public static bool operator ==(EumAddress value1, EumAddress value2)
		{
			return value1.Equals(value2);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000D916 File Offset: 0x0000BB16
		public static bool operator !=(EumAddress value1, EumAddress value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000D922 File Offset: 0x0000BB22
		public static explicit operator string(EumAddress address)
		{
			if (address.address == null)
			{
				return string.Empty;
			}
			return address.address;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000D93A File Offset: 0x0000BB3A
		public static explicit operator EumAddress(string address)
		{
			return new EumAddress(address);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000D942 File Offset: 0x0000BB42
		public override int GetHashCode()
		{
			if (this.address == null)
			{
				return 0;
			}
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this.address);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000D95E File Offset: 0x0000BB5E
		public int CompareTo(EumAddress address)
		{
			return StringComparer.OrdinalIgnoreCase.Compare(this.address, address.address);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000D978 File Offset: 0x0000BB78
		public int CompareTo(object address)
		{
			if (address is EumAddress)
			{
				return this.CompareTo((EumAddress)address);
			}
			string text = address as string;
			if (text != null)
			{
				return string.Compare(this.address, text, StringComparison.OrdinalIgnoreCase);
			}
			if (this.address != null)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000D9BD File Offset: 0x0000BBBD
		public byte[] GetBytes()
		{
			if (this.address == null)
			{
				return null;
			}
			return CTSGlobals.AsciiEncoding.GetBytes(this.address);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000D9DC File Offset: 0x0000BBDC
		public override bool Equals(object address)
		{
			if (address is EumAddress)
			{
				return this.Equals((EumAddress)address);
			}
			if (address is ProxyAddress)
			{
				return this.address.Equals(address.ToString(), StringComparison.OrdinalIgnoreCase);
			}
			if (address is string)
			{
				string text = address as string;
				if (text != null)
				{
					return string.Equals(this.address, text, StringComparison.OrdinalIgnoreCase);
				}
			}
			return false;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000DA3A File Offset: 0x0000BC3A
		public bool Equals(EumAddress address)
		{
			return string.Equals(this.address, address.address, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000DA4F File Offset: 0x0000BC4F
		public override string ToString()
		{
			if (this.address == null)
			{
				return string.Empty;
			}
			return this.address;
		}

		// Token: 0x0400013F RID: 319
		public const string UMDialPlanString = "phone-context=";

		// Token: 0x04000140 RID: 320
		public const string UMExtensionDelimiter = ";";

		// Token: 0x04000141 RID: 321
		public static readonly EumAddress Empty = default(EumAddress);

		// Token: 0x04000142 RID: 322
		private string address;
	}
}

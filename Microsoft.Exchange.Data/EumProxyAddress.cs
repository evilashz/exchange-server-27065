using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000BD RID: 189
	[Serializable]
	public sealed class EumProxyAddress : ProxyAddress
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x000117DE File Offset: 0x0000F9DE
		public EumProxyAddress(string address, bool isPrimaryAddress) : base(ProxyAddressPrefix.UM, address, isPrimaryAddress)
		{
			if (Microsoft.Exchange.Data.EumAddress.IsValidEumAddress(address))
			{
				this.eumAddress = address;
				return;
			}
			throw new ArgumentOutOfRangeException(DataStrings.ExceptionInvalidEumAddress(address), null);
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0001180E File Offset: 0x0000FA0E
		public string EumAddress
		{
			get
			{
				return this.eumAddress;
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00011816 File Offset: 0x0000FA16
		public static explicit operator EumAddress(EumProxyAddress value)
		{
			return new EumAddress(value.AddressString);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00011823 File Offset: 0x0000FA23
		private static bool Is1252LetterOrDigit(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9');
		}

		// Token: 0x04000303 RID: 771
		private const string HexDigits = "0123456789ABCDEF";

		// Token: 0x04000304 RID: 772
		private string eumAddress;
	}
}

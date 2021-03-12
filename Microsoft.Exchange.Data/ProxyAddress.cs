using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000B7 RID: 183
	[Serializable]
	public abstract class ProxyAddress : ProxyAddressBase, IComparable<ProxyAddress>
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x00010E6F File Offset: 0x0000F06F
		protected ProxyAddress(ProxyAddressPrefix prefix, string addressString, bool isPrimaryAddress) : this(prefix, addressString, isPrimaryAddress, false)
		{
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00010E7B File Offset: 0x0000F07B
		protected ProxyAddress(ProxyAddressPrefix prefix, string addressString, bool isPrimaryAddress, bool suppressAddressValidation) : base(prefix, addressString, isPrimaryAddress, suppressAddressValidation)
		{
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00010E88 File Offset: 0x0000F088
		public string AddressString
		{
			get
			{
				return base.ValueString;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00010E90 File Offset: 0x0000F090
		public string ProxyAddressString
		{
			get
			{
				return base.ProxyAddressBaseString;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00010E98 File Offset: 0x0000F098
		public override ProxyAddressBase ToPrimary()
		{
			if (base.IsPrimaryAddress)
			{
				return this;
			}
			return ProxyAddress.Parse(base.Prefix.PrimaryPrefix, this.AddressString);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00010EBA File Offset: 0x0000F0BA
		public override ProxyAddressBase ToSecondary()
		{
			if (base.IsPrimaryAddress)
			{
				return ProxyAddress.Parse(base.Prefix.SecondaryPrefix, this.AddressString);
			}
			return this;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00010EDC File Offset: 0x0000F0DC
		private static void BreakPrefixAndAddress(string proxyAddressString, out string prefixString, out string addressString)
		{
			if (proxyAddressString == null)
			{
				throw new ArgumentNullException("proxyAddressString");
			}
			if (proxyAddressString.Length == 0)
			{
				throw new ArgumentException(DataStrings.ExceptionEmptyProxyAddress);
			}
			int num = proxyAddressString.IndexOf(':');
			prefixString = proxyAddressString.Substring(0, Math.Max(num, 0));
			addressString = proxyAddressString.Substring(num + 1);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00010F34 File Offset: 0x0000F134
		internal static ProxyAddress ParseFromAD(string proxyAddressString)
		{
			string prefixString = null;
			string text = null;
			ProxyAddress.BreakPrefixAndAddress(proxyAddressString, out prefixString, out text);
			ProxyAddress proxyAddress = ProxyAddress.Parse(proxyAddressString, prefixString, text);
			if (typeof(X400ProxyAddress) == proxyAddress.GetType() && !((X400ProxyAddress)proxyAddress).EndingWithSemicolon)
			{
				proxyAddress = new InvalidProxyAddress(proxyAddressString, proxyAddress.Prefix, text, proxyAddress.IsPrimaryAddress, new ArgumentOutOfRangeException(DataStrings.InvalidX400AddressSpace(proxyAddressString)));
			}
			return proxyAddress;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		public static ProxyAddress Parse(string proxyAddressString)
		{
			string prefixString = null;
			string addressString = null;
			ProxyAddress.BreakPrefixAndAddress(proxyAddressString, out prefixString, out addressString);
			return ProxyAddress.Parse(proxyAddressString, prefixString, addressString);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		public static bool TryParse(string proxyAddressString, out ProxyAddress result)
		{
			result = null;
			try
			{
				result = ProxyAddress.Parse(proxyAddressString);
			}
			catch (ArgumentException)
			{
			}
			if (result == null || result is InvalidProxyAddress)
			{
				result = null;
				return false;
			}
			return true;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00011010 File Offset: 0x0000F210
		public static ProxyAddress Parse(string prefixString, string addressString)
		{
			return ProxyAddress.Parse(null, prefixString, addressString);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001101C File Offset: 0x0000F21C
		private static ProxyAddress Parse(string proxyAddressString, string prefixString, string addressString)
		{
			if (prefixString == null)
			{
				throw new ArgumentNullException("prefixString");
			}
			if (addressString == null)
			{
				throw new ArgumentNullException("addressString");
			}
			ProxyAddressPrefix proxyAddressPrefix;
			if (prefixString.Length == 0 && SmtpAddress.IsValidSmtpAddress(addressString))
			{
				proxyAddressPrefix = ProxyAddressPrefix.Smtp;
			}
			else
			{
				try
				{
					proxyAddressPrefix = ProxyAddressPrefix.GetPrefix(prefixString);
				}
				catch (ArgumentOutOfRangeException parseException)
				{
					return new InvalidProxyAddress(proxyAddressString, new CustomProxyAddressPrefix("ERROR"), proxyAddressString ?? (prefixString + ':' + addressString), true, parseException);
				}
				catch (ArgumentException ex)
				{
					return new InvalidProxyAddress(proxyAddressString, new CustomProxyAddressPrefix("ERROR"), proxyAddressString ?? (prefixString + ':' + addressString), true, new ArgumentOutOfRangeException(ex.Message, ex));
				}
			}
			bool isPrimaryAddress = StringComparer.Ordinal.Equals(proxyAddressPrefix.PrimaryPrefix, prefixString);
			ProxyAddress result;
			try
			{
				ProxyAddress proxyAddress = proxyAddressPrefix.GetProxyAddress(addressString, isPrimaryAddress);
				proxyAddress.RawProxyAddressBaseString = proxyAddressString;
				result = proxyAddress;
			}
			catch (ArgumentOutOfRangeException parseException2)
			{
				result = new InvalidProxyAddress(proxyAddressString, proxyAddressPrefix, addressString, isPrimaryAddress, parseException2);
			}
			return result;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001112C File Offset: 0x0000F32C
		public int CompareTo(ProxyAddress other)
		{
			return base.CompareTo(other);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00011135 File Offset: 0x0000F335
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001113D File Offset: 0x0000F33D
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00011146 File Offset: 0x0000F346
		public static bool operator ==(ProxyAddress a, ProxyAddress b)
		{
			return a == b;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001114F File Offset: 0x0000F34F
		public static bool operator !=(ProxyAddress a, ProxyAddress b)
		{
			return !(a == b);
		}
	}
}

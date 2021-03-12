using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000BB RID: 187
	[Serializable]
	public abstract class ProxyAddressTemplate : ProxyAddressBase, IComparable<ProxyAddressTemplate>
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x00011600 File Offset: 0x0000F800
		protected ProxyAddressTemplate(ProxyAddressPrefix prefix, string addressTemplateString, bool isPrimaryAddress) : base(prefix, addressTemplateString, isPrimaryAddress)
		{
			if (prefix.DisplayName.Length == 0)
			{
				throw new ArgumentException(DataStrings.ProxyAddressTemplateEmptyPrefixOrValue(this.ProxyAddressTemplateString));
			}
			if (addressTemplateString.Length == 0)
			{
				throw new ArgumentException(DataStrings.ProxyAddressTemplateEmptyPrefixOrValue(this.ProxyAddressTemplateString));
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00011657 File Offset: 0x0000F857
		public string AddressTemplateString
		{
			get
			{
				return base.ValueString;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0001165F File Offset: 0x0000F85F
		public string ProxyAddressTemplateString
		{
			get
			{
				return base.ProxyAddressBaseString;
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00011667 File Offset: 0x0000F867
		public override ProxyAddressBase ToPrimary()
		{
			if (base.IsPrimaryAddress)
			{
				return this;
			}
			return ProxyAddressTemplate.Parse(base.Prefix.PrimaryPrefix, this.AddressTemplateString);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00011689 File Offset: 0x0000F889
		public override ProxyAddressBase ToSecondary()
		{
			if (base.IsPrimaryAddress)
			{
				return ProxyAddressTemplate.Parse(base.Prefix.SecondaryPrefix, this.AddressTemplateString);
			}
			return this;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000116AC File Offset: 0x0000F8AC
		public static ProxyAddressTemplate Parse(string proxyAddressTemplateString)
		{
			if (proxyAddressTemplateString == null)
			{
				throw new ArgumentNullException("proxyAddressTemplateString");
			}
			if (proxyAddressTemplateString.Length == 0)
			{
				throw new ArgumentException(DataStrings.ProxyAddressTemplateEmptyPrefixOrValue(proxyAddressTemplateString));
			}
			int num = proxyAddressTemplateString.IndexOf(':');
			string prefixString = proxyAddressTemplateString.Substring(0, Math.Max(num, 0));
			string addressTemplateString = proxyAddressTemplateString.Substring(num + 1);
			return ProxyAddressTemplate.Parse(proxyAddressTemplateString, prefixString, addressTemplateString);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001170A File Offset: 0x0000F90A
		public static ProxyAddressTemplate Parse(string prefixString, string addressTemplateString)
		{
			return ProxyAddressTemplate.Parse(null, prefixString, addressTemplateString);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00011714 File Offset: 0x0000F914
		private static ProxyAddressTemplate Parse(string proxyAddressTemplateString, string prefixString, string addressTemplateString)
		{
			if (prefixString == null)
			{
				throw new ArgumentNullException("prefixString");
			}
			if (addressTemplateString == null)
			{
				throw new ArgumentNullException("addressTemplateString");
			}
			ProxyAddressPrefix proxyAddressPrefix;
			if (prefixString.Length == 0 && SmtpProxyAddressTemplate.IsValidSmtpAddressTemplate(addressTemplateString))
			{
				proxyAddressPrefix = ProxyAddressPrefix.Smtp;
			}
			else
			{
				proxyAddressPrefix = ProxyAddressPrefix.GetPrefix(prefixString);
			}
			bool flag = StringComparer.Ordinal.Equals(proxyAddressPrefix.PrimaryPrefix, prefixString);
			ProxyAddressTemplate result;
			try
			{
				ProxyAddressTemplate proxyAddressTemplate = proxyAddressPrefix.GetProxyAddressTemplate(addressTemplateString, flag);
				proxyAddressTemplate.RawProxyAddressBaseString = proxyAddressTemplateString;
				result = proxyAddressTemplate;
			}
			catch (ArgumentOutOfRangeException parseException)
			{
				result = new InvalidProxyAddressTemplate(proxyAddressTemplateString, proxyAddressPrefix, addressTemplateString, flag, parseException);
			}
			return result;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000117A4 File Offset: 0x0000F9A4
		public int CompareTo(ProxyAddressTemplate other)
		{
			return base.CompareTo(other);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000117AD File Offset: 0x0000F9AD
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000117B5 File Offset: 0x0000F9B5
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000117BE File Offset: 0x0000F9BE
		public static bool operator ==(ProxyAddressTemplate a, ProxyAddressTemplate b)
		{
			return a == b;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000117C7 File Offset: 0x0000F9C7
		public static bool operator !=(ProxyAddressTemplate a, ProxyAddressTemplate b)
		{
			return !(a == b);
		}
	}
}

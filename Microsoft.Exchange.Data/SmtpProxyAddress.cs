using System;
using System.Text;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	public sealed class SmtpProxyAddress : ProxyAddress
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x000126BD File Offset: 0x000108BD
		public SmtpProxyAddress(string address, bool isPrimaryAddress) : base(ProxyAddressPrefix.Smtp, address, isPrimaryAddress)
		{
			if (SmtpProxyAddress.IsValidProxyAddress(address))
			{
				this.smtpAddress = address;
				return;
			}
			throw new ArgumentOutOfRangeException(DataStrings.ExceptionInvalidSmtpAddress(address));
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x000126EC File Offset: 0x000108EC
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000126F4 File Offset: 0x000108F4
		public static bool IsValidProxyAddress(string address)
		{
			return !string.IsNullOrEmpty(address) && Microsoft.Exchange.Data.SmtpAddress.IsValidSmtpAddress(address);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00012706 File Offset: 0x00010906
		public static explicit operator SmtpAddress(SmtpProxyAddress value)
		{
			return new SmtpAddress(value.AddressString);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00012713 File Offset: 0x00010913
		public static bool TryEncapsulate(ProxyAddress address, string domain, out SmtpProxyAddress smtpProxyAddress)
		{
			return SmtpProxyAddress.TryEncapsulate(address.PrefixString, address.AddressString, domain, out smtpProxyAddress);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00012728 File Offset: 0x00010928
		public static bool TryEncapsulate(string addressType, string address, string domain, out SmtpProxyAddress smtpProxyAddress)
		{
			smtpProxyAddress = null;
			string address2 = SmtpProxyAddress.EncapsulateAddress(addressType, address, domain);
			if (Microsoft.Exchange.Data.SmtpAddress.IsValidSmtpAddress(address2))
			{
				smtpProxyAddress = new SmtpProxyAddress(address2, true);
				return true;
			}
			return false;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00012758 File Offset: 0x00010958
		public static string EncapsulateAddress(string addressType, string address, string domain)
		{
			int num = (domain != null) ? domain.Length : 0;
			StringBuilder stringBuilder = new StringBuilder("IMCEA", "IMCEA".Length + addressType.Length + address.Length + num + 2);
			stringBuilder.Append(addressType);
			stringBuilder.Append('-');
			if (SmtpProxyAddress.HasTwoByteCharValue(address))
			{
				stringBuilder.Append("UTF8");
				stringBuilder.Append('-');
				SmtpProxyAddress.EncapsulateStringWithUTF8(stringBuilder, address);
			}
			else
			{
				SmtpProxyAddress.EncapsulateString(stringBuilder, address);
			}
			if (num != 0)
			{
				stringBuilder.Append('@');
				stringBuilder.Append(domain);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000127F4 File Offset: 0x000109F4
		private static void EncapsulateStringWithUTF8(StringBuilder encapsulated, string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			foreach (byte b in bytes)
			{
				char c = (char)b;
				if (SmtpProxyAddress.Is1252LetterOrDigit(c))
				{
					encapsulated.Append(c);
				}
				else
				{
					char c2 = c;
					switch (c2)
					{
					case '-':
						break;
					case '.':
						goto IL_65;
					case '/':
						encapsulated.Append('_');
						goto IL_98;
					default:
						if (c2 != '=')
						{
							goto IL_65;
						}
						break;
					}
					encapsulated.Append(c);
					goto IL_98;
					IL_65:
					encapsulated.Append('+');
					encapsulated.Append("0123456789ABCDEF"[(int)(b / 16)]);
					encapsulated.Append("0123456789ABCDEF"[(int)(b % 16)]);
				}
				IL_98:;
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000128AC File Offset: 0x00010AAC
		private static void EncapsulateString(StringBuilder encapsulated, string str)
		{
			foreach (char c in str)
			{
				if (SmtpProxyAddress.Is1252LetterOrDigit(c))
				{
					encapsulated.Append(c);
				}
				else
				{
					char c2 = c;
					switch (c2)
					{
					case '-':
						break;
					case '.':
						goto IL_59;
					case '/':
						encapsulated.Append('_');
						goto IL_93;
					default:
						if (c2 != '=')
						{
							goto IL_59;
						}
						break;
					}
					encapsulated.Append(c);
					goto IL_93;
					IL_59:
					int num = Convert.ToInt32(c);
					encapsulated.Append('+');
					encapsulated.Append("0123456789ABCDEF"[num / 16]);
					encapsulated.Append("0123456789ABCDEF"[num % 16]);
				}
				IL_93:;
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001295C File Offset: 0x00010B5C
		public bool TryDeencapsulate(out ProxyAddress proxyAddress)
		{
			return SmtpProxyAddress.TryDeencapsulate(base.AddressString, out proxyAddress);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001296A File Offset: 0x00010B6A
		public static bool TryDeencapsulate(string address, out ProxyAddress proxyAddress)
		{
			proxyAddress = SmtpProxyAddress.Deencapsulate(address);
			return proxyAddress != null && !(proxyAddress is InvalidProxyAddress);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001298D File Offset: 0x00010B8D
		public static bool IsEncapsulatedAddress(string smtpAddress)
		{
			return SmtpProxyAddress.HasEncapsulationPrefix(smtpAddress) && smtpAddress.IndexOf('-') != -1;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000129A8 File Offset: 0x00010BA8
		public static bool TryDeencapsulateExchangeGuid(string address, out Guid exchangeGuid)
		{
			exchangeGuid = Guid.Empty;
			if (string.Compare("ExchangeGuid+", 0, address, 0, "ExchangeGuid+".Length, StringComparison.OrdinalIgnoreCase) == 0)
			{
				try
				{
					exchangeGuid = new Guid(address.Substring("ExchangeGuid+".Length, 36));
				}
				catch (FormatException)
				{
				}
				catch (OverflowException)
				{
				}
			}
			return !Guid.Empty.Equals(exchangeGuid);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00012A34 File Offset: 0x00010C34
		public static string EncapsulateExchangeGuid(string domain, Guid exchangeGuid)
		{
			return string.Format("ExchangeGuid+{0}@{1}", exchangeGuid.ToString("D"), domain);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00012A5C File Offset: 0x00010C5C
		private static ProxyAddress Deencapsulate(string smtpAddress)
		{
			if (!SmtpProxyAddress.HasEncapsulationPrefix(smtpAddress))
			{
				return null;
			}
			int num = smtpAddress.IndexOf('-');
			if (-1 == num)
			{
				return null;
			}
			string text = SmtpProxyAddress.DeencapsulateString(smtpAddress, "IMCEA".Length, num - "IMCEA".Length);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			num++;
			int num2 = smtpAddress.LastIndexOf('@');
			if (num2 == -1 || num2 == smtpAddress.Length - 1)
			{
				return null;
			}
			string text2 = SmtpProxyAddress.DeencapsulateString(smtpAddress, num, num2 - num);
			if (string.IsNullOrEmpty(text2))
			{
				return null;
			}
			ProxyAddress result;
			try
			{
				result = ProxyAddress.Parse(text, text2);
			}
			catch (ArgumentOutOfRangeException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00012B00 File Offset: 0x00010D00
		private static string DeencapsulateString(string encapsulated, int startIndex, int length)
		{
			string text = encapsulated.Substring(startIndex, length);
			if (text.StartsWith("UTF8" + '-'))
			{
				return SmtpProxyAddress.DeencapsulateUTF8String(encapsulated.Substring(startIndex + "UTF8".Length + 1, length - "UTF8".Length - 1));
			}
			return SmtpProxyAddress.DeencapsulateString(text);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00012B60 File Offset: 0x00010D60
		private static string DeencapsulateString(string encapsulated)
		{
			StringBuilder stringBuilder = new StringBuilder(encapsulated.Length);
			int i = 0;
			while (i < encapsulated.Length)
			{
				char c = encapsulated[i];
				if (c != '+')
				{
					if (c != '_')
					{
						goto IL_48;
					}
					stringBuilder.Append('/');
				}
				else
				{
					char value;
					if (!SmtpProxyAddress.ConvertTwoHexCharToChar(encapsulated, i + 1, out value))
					{
						goto IL_48;
					}
					stringBuilder.Append(value);
					i += 2;
				}
				IL_56:
				i++;
				continue;
				IL_48:
				stringBuilder.Append(encapsulated[i]);
				goto IL_56;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00012BD8 File Offset: 0x00010DD8
		private static string DeencapsulateUTF8String(string encapsulated)
		{
			byte[] array = new byte[encapsulated.Length];
			int num = 0;
			int i = 0;
			while (i < encapsulated.Length)
			{
				char c = encapsulated[i];
				if (c != '+')
				{
					if (c != '_')
					{
						goto IL_4E;
					}
					array[num] = 47;
					num++;
				}
				else
				{
					char c2;
					if (!SmtpProxyAddress.ConvertTwoHexCharToChar(encapsulated, i + 1, out c2))
					{
						goto IL_4E;
					}
					array[num] = (byte)c2;
					i += 2;
					num++;
				}
				IL_6D:
				i++;
				continue;
				IL_4E:
				if (encapsulated[i] > 'ÿ')
				{
					return null;
				}
				array[num] = (byte)encapsulated[i];
				num++;
				goto IL_6D;
			}
			string result;
			try
			{
				Encoding encoding = new UTF8Encoding(false, true);
				result = encoding.GetString(array, 0, num);
			}
			catch (DecoderFallbackException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00012C90 File Offset: 0x00010E90
		private static bool Is1252LetterOrDigit(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9');
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00012CB8 File Offset: 0x00010EB8
		private static bool HasTwoByteCharValue(string str)
		{
			foreach (char value in str)
			{
				int num = Convert.ToInt32(value);
				if (num >= 256 || num < 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00012D00 File Offset: 0x00010F00
		private static bool ConvertTwoHexCharToChar(string str, int offset, out char ch)
		{
			ch = '\0';
			if (offset > str.Length - 2)
			{
				return false;
			}
			if (SmtpProxyAddress.Is1252LetterOrDigit(str[offset]) && SmtpProxyAddress.Is1252LetterOrDigit(str[offset + 1]))
			{
				try
				{
					ch = (char)(HexConverter.NumFromHex(str[offset]) * 16 + HexConverter.NumFromHex(str[offset + 1]));
					return true;
				}
				catch (FormatException)
				{
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00012D78 File Offset: 0x00010F78
		private static bool HasEncapsulationPrefix(string smtpAddress)
		{
			return smtpAddress.Length > "IMCEA".Length && smtpAddress.StartsWith("IMCEA", StringComparison.Ordinal);
		}

		// Token: 0x0400030C RID: 780
		private const string IMCEA = "IMCEA";

		// Token: 0x0400030D RID: 781
		private const string UTF8 = "UTF8";

		// Token: 0x0400030E RID: 782
		private const string HexDigits = "0123456789ABCDEF";

		// Token: 0x0400030F RID: 783
		private const string ExchangeGuid = "ExchangeGuid+";

		// Token: 0x04000310 RID: 784
		private const string ExchangeGuidFormatString = "ExchangeGuid+{0}@{1}";

		// Token: 0x04000311 RID: 785
		private readonly string smtpAddress;
	}
}

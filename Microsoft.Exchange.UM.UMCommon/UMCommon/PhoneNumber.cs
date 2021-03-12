using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000121 RID: 289
	[DebuggerDisplay("Number = {phone} ToDisplay = {displayPhone} UriType = {uriType} NumberType = {numberType}")]
	public class PhoneNumber
	{
		// Token: 0x0600095A RID: 2394 RVA: 0x00024C06 File Offset: 0x00022E06
		public PhoneNumber() : this(null)
		{
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00024C0F File Offset: 0x00022E0F
		public PhoneNumber(string phone) : this(phone, PhoneNumberKind.Unknown)
		{
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00024C19 File Offset: 0x00022E19
		public PhoneNumber(string phone, PhoneNumberKind type) : this(phone, type, UMUriType.TelExtn)
		{
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00024C24 File Offset: 0x00022E24
		public PhoneNumber(string phone, PhoneNumberKind type, UMUriType uriType) : this(phone, null, type, uriType)
		{
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00024C30 File Offset: 0x00022E30
		public PhoneNumber(string phone, string displayPhone, PhoneNumberKind type, UMUriType uriType)
		{
			this.phone = ((phone != null) ? phone : string.Empty);
			this.numberType = type;
			this.uriType = uriType;
			this.displayPhone = displayPhone;
			this.ToDial = ((!this.IsEmpty && this.uriType == UMUriType.E164) ? ("+" + this.Number) : this.Number);
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00024C99 File Offset: 0x00022E99
		public static PhoneNumber Empty
		{
			get
			{
				return PhoneNumber.emptyPhone;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00024CA0 File Offset: 0x00022EA0
		public string Number
		{
			get
			{
				return this.phone;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00024CA8 File Offset: 0x00022EA8
		public PhoneNumberKind Kind
		{
			get
			{
				return this.numberType;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00024CB0 File Offset: 0x00022EB0
		public UMUriType UriType
		{
			get
			{
				return this.uriType;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00024CB8 File Offset: 0x00022EB8
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.phone);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00024CC5 File Offset: 0x00022EC5
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x00024CCD File Offset: 0x00022ECD
		public string ToDial { get; private set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00024CD6 File Offset: 0x00022ED6
		public string ToDisplay
		{
			get
			{
				if (string.IsNullOrEmpty(this.displayPhone))
				{
					this.displayPhone = PhoneNumber.PhoneNumberFormatter.FormatNumber(this);
				}
				return this.displayPhone;
			}
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00024CF7 File Offset: 0x00022EF7
		public static PhoneNumber CreateExtension(string number)
		{
			return new PhoneNumber(number, PhoneNumberKind.Extension);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00024D00 File Offset: 0x00022F00
		public static PhoneNumber CreateNational(string number)
		{
			return new PhoneNumber(number, PhoneNumberKind.National);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00024D09 File Offset: 0x00022F09
		public static PhoneNumber CreateInternational(string number)
		{
			return new PhoneNumber(number, PhoneNumberKind.International);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00024D14 File Offset: 0x00022F14
		public static bool TryCreateE164Number(string number, string countryOrRegionCode, out PhoneNumber result)
		{
			result = null;
			bool result2 = false;
			if (!string.IsNullOrEmpty(number) && !string.IsNullOrEmpty(countryOrRegionCode) && Utils.IsNumber(number) && Utils.IsNumber(countryOrRegionCode))
			{
				string text = countryOrRegionCode + number;
				if (text.Length <= 15)
				{
					result = new PhoneNumber(text, PhoneNumberKind.Unknown, UMUriType.E164);
					result2 = true;
				}
			}
			return result2;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00024D65 File Offset: 0x00022F65
		public static bool TryParse(string phone, bool isNullOrEmptyStringValid, out PhoneNumber parsedNumber)
		{
			return PhoneNumber.TryParse(null, phone, isNullOrEmptyStringValid, out parsedNumber);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00024D70 File Offset: 0x00022F70
		public static bool TryParse(string phone, out PhoneNumber parsedNumber)
		{
			return PhoneNumber.TryParse(null, phone, false, out parsedNumber);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00024D7B File Offset: 0x00022F7B
		public static bool TryParse(UMDialPlan dialPlan, string phone, out PhoneNumber parsedNumber)
		{
			return PhoneNumber.TryParse(dialPlan, phone, false, out parsedNumber);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00024D88 File Offset: 0x00022F88
		public static bool TryParse(UMDialPlan dialPlan, string phone, bool isNullOrEmptyStringValid, out PhoneNumber parsedNumber)
		{
			parsedNumber = null;
			bool flag = false;
			PhoneNumberKind type = PhoneNumberKind.Unknown;
			phone = Utils.TrimSpaces(phone);
			if (!string.IsNullOrEmpty(phone))
			{
				phone = phone.ToLowerInvariant().Trim();
				phone = Utils.RemoveSchemePrefix("TEL:", phone);
				UMUriType umuriType = Utils.DetermineNumberType(phone);
				if (umuriType == UMUriType.SipName)
				{
					phone = Utils.RemoveSIPPrefix(phone);
					parsedNumber = new PhoneNumber(phone, type, umuriType);
					flag = true;
				}
				else
				{
					string text = DtmfString.SanitizePhoneNumber(phone);
					if (!string.IsNullOrEmpty(text))
					{
						flag = true;
						string text2 = string.Equals(phone, text, StringComparison.OrdinalIgnoreCase) ? null : phone;
						string text3 = text.StartsWith("+", StringComparison.OrdinalIgnoreCase) ? text.Substring(1) : text;
						parsedNumber = new PhoneNumber(text3, text2, type, umuriType);
					}
				}
				if (flag && dialPlan != null)
				{
					parsedNumber.SetPhoneNumberKindForDialPlan(dialPlan);
				}
				return flag;
			}
			if (isNullOrEmptyStringValid)
			{
				parsedNumber = new PhoneNumber();
				return true;
			}
			return false;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00024E4C File Offset: 0x0002304C
		public static PhoneNumber Parse(string phone)
		{
			if (string.IsNullOrEmpty(phone))
			{
				throw new ArgumentNullException("phone");
			}
			PhoneNumber result = null;
			if (!PhoneNumber.TryParse(phone, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00024E7B File Offset: 0x0002307B
		public static bool IsNullOrEmpty(PhoneNumber number)
		{
			return number == null || number.IsEmpty;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00024E88 File Offset: 0x00023088
		public static bool IsValidPhoneNumber(string phone)
		{
			bool result = false;
			phone = phone.ToLowerInvariant().Trim();
			UMUriType umuriType = Utils.DetermineNumberType(phone);
			if (umuriType == UMUriType.SipName)
			{
				result = true;
			}
			else
			{
				string value = DtmfString.SanitizePhoneNumber(phone);
				if (!string.IsNullOrEmpty(value))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00024EC5 File Offset: 0x000230C5
		public bool IsMatch(string matchString, UMDialPlan dialplan)
		{
			if (matchString == null)
			{
				throw new ArgumentNullException("matchString cannot be null");
			}
			if (dialplan == null)
			{
				throw new ArgumentException("dialplan cannot be null");
			}
			return this.IsMatch(matchString, this.GetOptionalPrefixes(dialplan));
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00024EF4 File Offset: 0x000230F4
		public bool IsMatch(string matchString, List<string> optionalPrefixes)
		{
			if (optionalPrefixes == null)
			{
				throw new ArgumentException("optionalPrefixes cannot be null");
			}
			if (string.IsNullOrEmpty(matchString) || string.IsNullOrEmpty(matchString.Trim()))
			{
				return false;
			}
			if (this.IsEmpty || string.IsNullOrEmpty(this.ToDial))
			{
				return false;
			}
			PhoneNumber phoneNumber = null;
			if (PhoneNumber.TryParse(matchString, out phoneNumber))
			{
				matchString = phoneNumber.ToDial;
			}
			string text = this.ToDial;
			foreach (string text2 in optionalPrefixes)
			{
				if (text.StartsWith(text2, StringComparison.OrdinalIgnoreCase) && text.Length > text2.Length)
				{
					text = text.Substring(text2.Length);
					break;
				}
			}
			bool result = false;
			if (matchString.EndsWith(text, StringComparison.OrdinalIgnoreCase))
			{
				if (matchString.Length == text.Length)
				{
					result = true;
				}
				else
				{
					int length = matchString.Length - text.Length;
					string item = matchString.Substring(0, length);
					if (optionalPrefixes.Contains(item))
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00025000 File Offset: 0x00023200
		public List<string> GetOptionalPrefixes(UMDialPlan dialPlan)
		{
			List<string> list = new List<string>(7);
			string text = dialPlan.CountryOrRegionCode ?? string.Empty;
			string text2 = dialPlan.NationalNumberPrefix ?? string.Empty;
			string text3 = dialPlan.InternationalAccessCode ?? string.Empty;
			bool flag = !string.IsNullOrEmpty(text);
			bool flag2 = !string.IsNullOrEmpty(text2);
			bool flag3 = !string.IsNullOrEmpty(text3);
			if (!this.ToDial.StartsWith("+", StringComparison.OrdinalIgnoreCase) || !flag || this.ToDial.StartsWith("+" + text, StringComparison.OrdinalIgnoreCase))
			{
				if (flag && flag2)
				{
					list.Add("+" + text + text2);
				}
				if (flag)
				{
					list.Add("+" + text);
				}
				if (flag3)
				{
					if (flag && flag2)
					{
						list.Add(text3 + text + text2);
					}
					if (flag)
					{
						list.Add(text3 + text);
					}
				}
				if (flag && flag2)
				{
					list.Add(text + text2);
				}
				if (flag)
				{
					list.Add(text);
				}
				if (flag2)
				{
					list.Add(text2);
				}
			}
			list.Add("+");
			list.Add(string.Empty);
			return list;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00025134 File Offset: 0x00023334
		public PhoneNumber Extend(UMDialPlan dialPlan)
		{
			string text = null;
			PhoneNumber phoneNumber = this;
			if (dialPlan.TryMapNumberingPlan(this.Number, out text) && !PhoneNumber.TryParse(text, out phoneNumber))
			{
				phoneNumber = this;
			}
			PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, data, "PhoneNumber.Extend using extended number '_PhoneNumber'", new object[0]);
			return phoneNumber;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00025188 File Offset: 0x00023388
		public bool StartsWithTrunkAccessCode(UMDialPlan dialPlan)
		{
			string text = Utils.TrimSpaces(dialPlan.OutsideLineAccessCode);
			return text != null && this.Number.StartsWith(text, StringComparison.Ordinal);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x000251B3 File Offset: 0x000233B3
		public bool IsValid(UMDialPlan dialPlan)
		{
			return PhoneNumberKind.Extension != this.Kind || this.Number.Length == dialPlan.NumberOfDigitsInExtension;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x000251D4 File Offset: 0x000233D4
		public PhoneNumber Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x000251E0 File Offset: 0x000233E0
		public PhoneNumber Clone(UMDialPlan dialPlan)
		{
			if (this.IsEmpty)
			{
				return PhoneNumber.Empty;
			}
			PhoneNumber phoneNumber = new PhoneNumber(this.phone, this.numberType, this.uriType);
			if (dialPlan != null)
			{
				phoneNumber.SetPhoneNumberKindForDialPlan(dialPlan);
			}
			return phoneNumber;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00025220 File Offset: 0x00023420
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} ({1}:{2})", new object[]
			{
				this.phone,
				this.numberType,
				this.uriType
			});
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0002526C File Offset: 0x0002346C
		public string RenderUserPart(UMDialPlan dialPlan)
		{
			if (dialPlan.URIType != UMUriType.SipName)
			{
				return this.ToDial;
			}
			switch (this.UriType)
			{
			case UMUriType.TelExtn:
				return this.ToDial + ";phone-context=user-default";
			case UMUriType.E164:
				return this.ToDial;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x000252C0 File Offset: 0x000234C0
		internal PhoneNumber GetPstnCallbackTelephoneNumber(ContactInfo contact, UMDialPlan dialPlan)
		{
			PhoneNumber phoneNumber = PhoneNumber.Empty;
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "GetPstnCallbackTelephoneNumber: contact:{0}, dialPlan:{1}, callerId(this):{2}", new object[]
			{
				contact,
				dialPlan,
				this
			});
			if (this.IsEmpty || this.UriType == UMUriType.E164)
			{
				phoneNumber = this;
			}
			else
			{
				PhoneNumber phoneNumber2 = null;
				if (this.UriType == UMUriType.TelExtn)
				{
					phoneNumber2 = this;
				}
				else if (this.UriType == UMUriType.SipName && !PhoneNumber.TryParse(contact.SipLine, out phoneNumber2))
				{
					PhoneNumber.TryParse(contact.BusinessPhone, out phoneNumber2);
				}
				if (phoneNumber2 != null)
				{
					phoneNumber = ((dialPlan != null) ? phoneNumber2.Extend(dialPlan) : phoneNumber2);
					PIIMessage piimessage = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber2);
					PIIMessage piimessage2 = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber);
					PIIMessage[] data = new PIIMessage[]
					{
						piimessage,
						piimessage2
					};
					CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, data, "_PhoneNumber(1).Extend({0}) -> _PhoneNumber(2)", new object[]
					{
						dialPlan
					});
					if (string.Equals(phoneNumber.Number, phoneNumber2.Number, StringComparison.OrdinalIgnoreCase))
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "Looping through SanitizedPhoneNumbers", new object[0]);
						string text = string.Empty;
						foreach (string text2 in contact.SanitizedPhoneNumbers)
						{
							if (text2.EndsWith(phoneNumber2.Number, StringComparison.OrdinalIgnoreCase) && text2.Length > text.Length)
							{
								text = text2;
							}
							PIIMessage piimessage3 = PIIMessage.Create(PIIType._PhoneNumber, text2);
							PIIMessage piimessage4 = PIIMessage.Create(PIIType._PhoneNumber, text);
							PIIMessage[] data2 = new PIIMessage[]
							{
								piimessage3,
								piimessage4
							};
							CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, data2, "Analyzing:_PhoneNumber(1) BestMatch:_PhoneNumber(2)", new object[0]);
						}
						PhoneNumber phoneNumber3;
						if (!string.IsNullOrEmpty(text) && PhoneNumber.TryParse(text, out phoneNumber3))
						{
							phoneNumber = phoneNumber3;
						}
					}
				}
			}
			PIIMessage data3 = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber);
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, data3, "Returning CallbackNumber:_PhoneNumber", new object[0]);
			return phoneNumber;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x000254C0 File Offset: 0x000236C0
		private void SetPhoneNumberKindForDialPlan(UMDialPlan dialPlan)
		{
			this.numberType = PhoneNumberKind.Unknown;
			if (this.IsEmpty || this.uriType == UMUriType.SipName)
			{
				return;
			}
			string text = Utils.TrimSpaces(dialPlan.OutsideLineAccessCode);
			string text2 = Utils.TrimSpaces(dialPlan.InternationalAccessCode);
			string text3 = Utils.TrimSpaces(dialPlan.CountryOrRegionCode);
			if ((dialPlan.URIType == UMUriType.TelExtn || dialPlan.URIType == UMUriType.SipName) && this.phone.Length == dialPlan.NumberOfDigitsInExtension)
			{
				this.numberType = PhoneNumberKind.Extension;
				return;
			}
			if (text3 != null && this.UriType == UMUriType.E164)
			{
				if (this.phone.StartsWith(text3, StringComparison.Ordinal))
				{
					this.numberType = PhoneNumberKind.National;
					return;
				}
				this.numberType = PhoneNumberKind.International;
				return;
			}
			else
			{
				if (text != null && text2 != null && this.phone.StartsWith(text + text2, StringComparison.Ordinal))
				{
					this.numberType = PhoneNumberKind.International;
					return;
				}
				if (text2 != null && this.phone.StartsWith(text2, StringComparison.Ordinal))
				{
					this.numberType = PhoneNumberKind.International;
					return;
				}
				this.numberType = PhoneNumberKind.National;
				return;
			}
		}

		// Token: 0x04000536 RID: 1334
		private const int E164Length = 15;

		// Token: 0x04000537 RID: 1335
		private const int SIPNameLength = 32;

		// Token: 0x04000538 RID: 1336
		private static PhoneNumber emptyPhone = new PhoneNumber();

		// Token: 0x04000539 RID: 1337
		private PhoneNumberKind numberType;

		// Token: 0x0400053A RID: 1338
		private string phone;

		// Token: 0x0400053B RID: 1339
		private string displayPhone;

		// Token: 0x0400053C RID: 1340
		private UMUriType uriType;

		// Token: 0x02000122 RID: 290
		private abstract class PhoneNumberFormatter
		{
			// Token: 0x0600097F RID: 2431 RVA: 0x000255B2 File Offset: 0x000237B2
			static PhoneNumberFormatter()
			{
				PhoneNumber.PhoneNumberFormatter.genericE164Formatter = new PhoneNumber.PhoneNumberFormatter.GenericE164Formatter();
			}

			// Token: 0x06000980 RID: 2432 RVA: 0x000255C8 File Offset: 0x000237C8
			internal static string FormatNumber(PhoneNumber number)
			{
				string text = null;
				if (number.UriType == UMUriType.E164)
				{
					text = PhoneNumber.PhoneNumberFormatter.genericE164Formatter.Format(number);
				}
				if (text == null)
				{
					text = PhoneNumber.PhoneNumberFormatter.defaultFormatter.Format(number);
				}
				return text;
			}

			// Token: 0x06000981 RID: 2433
			protected abstract string Format(PhoneNumber phoneNumber);

			// Token: 0x0400053E RID: 1342
			private static PhoneNumber.PhoneNumberFormatter.GenericE164Formatter genericE164Formatter;

			// Token: 0x0400053F RID: 1343
			private static PhoneNumber.PhoneNumberFormatter.DefaultFormatter defaultFormatter = new PhoneNumber.PhoneNumberFormatter.DefaultFormatter();

			// Token: 0x02000123 RID: 291
			protected class FormatterPattern
			{
				// Token: 0x06000983 RID: 2435 RVA: 0x00025604 File Offset: 0x00023804
				internal FormatterPattern(string format)
				{
					if (string.IsNullOrEmpty(format))
					{
						throw new ArgumentException("Telephone pattern cannot be null or empty", "format");
					}
					int num = 0;
					int i = 0;
					while (i < format.Length)
					{
						if (format[i] == 'i')
						{
							try
							{
								do
								{
									i++;
								}
								while (format[i] != 'i');
								goto IL_85;
							}
							catch (IndexOutOfRangeException innerException)
							{
								throw new ArgumentException("Pattern " + format + " is not valid", "format", innerException);
							}
							goto IL_5D;
						}
						goto IL_5D;
						IL_85:
						i++;
						continue;
						IL_5D:
						if (format[i] == 'x' || format[i] == 's' || char.IsDigit(format[i]))
						{
							num++;
							goto IL_85;
						}
						goto IL_85;
					}
					this.Format = format;
					this.NumberOfDigits = num;
				}

				// Token: 0x17000235 RID: 565
				// (get) Token: 0x06000984 RID: 2436 RVA: 0x000256C4 File Offset: 0x000238C4
				// (set) Token: 0x06000985 RID: 2437 RVA: 0x000256CC File Offset: 0x000238CC
				internal string Format { get; private set; }

				// Token: 0x17000236 RID: 566
				// (get) Token: 0x06000986 RID: 2438 RVA: 0x000256D5 File Offset: 0x000238D5
				// (set) Token: 0x06000987 RID: 2439 RVA: 0x000256DD File Offset: 0x000238DD
				internal int NumberOfDigits { get; private set; }

				// Token: 0x06000988 RID: 2440 RVA: 0x000256E8 File Offset: 0x000238E8
				internal bool TryFormat(string argValue, out string result)
				{
					result = null;
					if (!string.IsNullOrEmpty(argValue))
					{
						StringBuilder stringBuilder = new StringBuilder(this.Format.Length);
						if (this.NumberOfDigits == argValue.Length)
						{
							int i = 0;
							int num = 0;
							while (i < this.Format.Length)
							{
								char c = this.Format[i];
								if (c == 's')
								{
									num++;
								}
								else if (c == 'x')
								{
									stringBuilder.Append(argValue[num]);
									num++;
								}
								else if (c == 'i')
								{
									i++;
									while (this.Format[i] != 'i')
									{
										stringBuilder.Append(this.Format[i]);
										i++;
									}
								}
								else if (char.IsDigit(c))
								{
									if (c != argValue[num])
									{
										return false;
									}
									stringBuilder.Append(argValue[num]);
									num++;
								}
								else
								{
									stringBuilder.Append(this.Format[i]);
								}
								i++;
							}
							result = stringBuilder.ToString();
							return true;
						}
					}
					return false;
				}
			}

			// Token: 0x02000124 RID: 292
			protected class GenericE164Formatter : PhoneNumber.PhoneNumberFormatter
			{
				// Token: 0x06000989 RID: 2441 RVA: 0x000257F8 File Offset: 0x000239F8
				internal GenericE164Formatter()
				{
					this.countryPatterns = new Dictionary<string, PhoneNumber.PhoneNumberFormatter.PatternCollection>(50);
					this.countryPatterns.Add("1", new PhoneNumber.PhoneNumberFormatter.PatternCollection(new PhoneNumber.PhoneNumberFormatter.FormatterPattern[]
					{
						new PhoneNumber.PhoneNumberFormatter.FormatterPattern("s(xxx) xxx-xxxx")
					}, new PhoneNumber.PhoneNumberFormatter.FormatterPattern[]
					{
						new PhoneNumber.PhoneNumberFormatter.FormatterPattern("+x (xxx) xxx-xxxx")
					}));
				}

				// Token: 0x0600098A RID: 2442 RVA: 0x00025858 File Offset: 0x00023A58
				protected override string Format(PhoneNumber phoneNumber)
				{
					string countryCode = this.GetCountryCode(phoneNumber);
					if (countryCode != null)
					{
						PhoneNumber.PhoneNumberFormatter.PatternCollection patternCollection = null;
						string result = null;
						if (this.countryPatterns.TryGetValue(countryCode, out patternCollection))
						{
							PhoneNumber.PhoneNumberFormatter.FormatterPattern[] array;
							if (phoneNumber.Kind == PhoneNumberKind.National)
							{
								array = patternCollection.National;
							}
							else
							{
								array = patternCollection.International;
							}
							if (array != null)
							{
								foreach (PhoneNumber.PhoneNumberFormatter.FormatterPattern formatterPattern in array)
								{
									if (formatterPattern.TryFormat(phoneNumber.Number, out result))
									{
										return result;
									}
								}
								PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber.Number);
								CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, data, "Failed to format number _PhoneNumber ", new object[0]);
							}
						}
					}
					return null;
				}

				// Token: 0x0600098B RID: 2443 RVA: 0x00025904 File Offset: 0x00023B04
				private string GetCountryCode(PhoneNumber number)
				{
					E164Number e164Number;
					if (E164Number.TryParseWithoutFormulating(number.ToDial, out e164Number) && e164Number.CountryCode != null)
					{
						return e164Number.CountryCode;
					}
					PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, number.ToDial);
					CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, data, "Unable to parse E164 number _PhoneNumber to retrieve the country code. ", new object[0]);
					return null;
				}

				// Token: 0x04000542 RID: 1346
				private Dictionary<string, PhoneNumber.PhoneNumberFormatter.PatternCollection> countryPatterns;
			}

			// Token: 0x02000125 RID: 293
			protected class DefaultFormatter : PhoneNumber.PhoneNumberFormatter
			{
				// Token: 0x0600098C RID: 2444 RVA: 0x00025955 File Offset: 0x00023B55
				protected override string Format(PhoneNumber phoneNumber)
				{
					return phoneNumber.ToDial;
				}
			}

			// Token: 0x02000126 RID: 294
			protected class PatternCollection
			{
				// Token: 0x0600098E RID: 2446 RVA: 0x00025965 File Offset: 0x00023B65
				internal PatternCollection(PhoneNumber.PhoneNumberFormatter.FormatterPattern[] national, PhoneNumber.PhoneNumberFormatter.FormatterPattern[] international)
				{
					this.National = national;
					this.International = international;
				}

				// Token: 0x17000237 RID: 567
				// (get) Token: 0x0600098F RID: 2447 RVA: 0x0002597B File Offset: 0x00023B7B
				// (set) Token: 0x06000990 RID: 2448 RVA: 0x00025983 File Offset: 0x00023B83
				internal PhoneNumber.PhoneNumberFormatter.FormatterPattern[] National { get; private set; }

				// Token: 0x17000238 RID: 568
				// (get) Token: 0x06000991 RID: 2449 RVA: 0x0002598C File Offset: 0x00023B8C
				// (set) Token: 0x06000992 RID: 2450 RVA: 0x00025994 File Offset: 0x00023B94
				internal PhoneNumber.PhoneNumberFormatter.FormatterPattern[] International { get; private set; }
			}
		}
	}
}

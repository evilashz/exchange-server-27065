using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Security.AntiXss;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Core.Internal;
using Microsoft.Exchange.Clients.Owa.Core.Transcoding;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.CommonHelpProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.DocumentLibrary;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.TextConverters.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InstantMessaging;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200028B RID: 651
	public static class Utilities
	{
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x000863A4 File Offset: 0x000845A4
		private static OwaExceptionEventManager StoreConnectionTransientManager
		{
			get
			{
				if (Utilities.storeConnectionTransientManager == null)
				{
					OwaExceptionEventManager value = new OwaExceptionEventManager(Globals.StoreTransientExceptionEventLogFrequencyInSeconds, Globals.StoreTransientExceptionEventLogThreshold);
					Interlocked.CompareExchange<OwaExceptionEventManager>(ref Utilities.storeConnectionTransientManager, value, null);
				}
				return Utilities.storeConnectionTransientManager;
			}
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000863DC File Offset: 0x000845DC
		public static string GetShortServerNameFromFqdn(string fqdn)
		{
			if (fqdn == null)
			{
				return null;
			}
			int num = fqdn.IndexOf(".", StringComparison.InvariantCultureIgnoreCase);
			if (num >= 0)
			{
				fqdn = fqdn.Substring(0, num);
			}
			return fqdn;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0008640C File Offset: 0x0008460C
		public static string GetSenderSmtpAddress(string itemId, UserContext userContext)
		{
			if (string.IsNullOrEmpty(itemId))
			{
				throw new ArgumentNullException("itemId");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
			{
				MessageItemSchema.SenderAddressType,
				MessageItemSchema.SenderEmailAddress
			};
			string text = string.Empty;
			object obj = null;
			string text2 = string.Empty;
			using (Item item = Utilities.GetItem<Item>(userContext, OwaStoreObjectId.CreateFromString(itemId), prefetchProperties))
			{
				obj = item.TryGetProperty(MessageItemSchema.SenderAddressType);
				if (!(obj is PropertyError))
				{
					text = (string)obj;
				}
				obj = item.TryGetProperty(MessageItemSchema.SenderEmailAddress);
			}
			if (!(obj is PropertyError))
			{
				text2 = (string)obj;
			}
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
			{
				return string.Empty;
			}
			if (string.CompareOrdinal(text, "EX") == 0)
			{
				try
				{
					IRecipientSession recipientSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, userContext);
					ADRecipient adrecipient = recipientSession.FindByLegacyExchangeDN(text2);
					userContext.LastRecipientSessionDCServerName = recipientSession.LastUsedDc;
					if (adrecipient != null)
					{
						return adrecipient.PrimarySmtpAddress.ToString();
					}
					goto IL_132;
				}
				catch (NonUniqueRecipientException ex)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Utilities.GetSenderSmtpAddress: NonUniqueRecipientException was thrown by FindByLegacyExchangeDN: {0}", ex.Message);
					goto IL_132;
				}
			}
			if (string.CompareOrdinal(text, "SMTP") == 0)
			{
				return text2;
			}
			IL_132:
			return string.Empty;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00086570 File Offset: 0x00084770
		internal static void RenderSizeWithUnits(TextWriter writer, long bytes, bool roundToWholeNumber)
		{
			Utilities.RenderSizeWithUnits(writer, bytes, roundToWholeNumber, true);
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0008657C File Offset: 0x0008477C
		internal static void RenderSizeWithUnits(TextWriter writer, long bytes, bool roundToWholeNumber, bool requireHtmlEncode)
		{
			Strings.IDs units = Utilities.PreRenderSizeWithUnits(writer, bytes, roundToWholeNumber);
			Utilities.WriteUnits(null, writer, units, requireHtmlEncode);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0008659C File Offset: 0x0008479C
		internal static void RenderSizeWithUnits(UserContext userContext, TextWriter writer, long bytes, bool roundToWholeNumber)
		{
			Strings.IDs units = Utilities.PreRenderSizeWithUnits(writer, bytes, roundToWholeNumber);
			Utilities.WriteUnits(userContext, writer, units, true);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x000865BC File Offset: 0x000847BC
		private static Strings.IDs PreRenderSizeWithUnits(TextWriter writer, long bytes, bool roundToWholeNumber)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			double num = (double)bytes;
			Strings.IDs result;
			if (bytes >= 1073741824L)
			{
				num /= 1073741824.0;
				result = -470326212;
			}
			else if (bytes >= 1048576L)
			{
				num /= 1048576.0;
				result = -1611859650;
			}
			else if (bytes >= 1024L)
			{
				num /= 1024.0;
				result = 2096762107;
			}
			else
			{
				result = 1954677924;
			}
			if (roundToWholeNumber)
			{
				writer.Write((long)Math.Round(num));
			}
			else
			{
				writer.Write("{0:0.##}", num);
			}
			writer.Write(" ");
			return result;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00086668 File Offset: 0x00084868
		private static void WriteUnits(UserContext userContext, TextWriter writer, Strings.IDs units, bool requireHtmlEncode)
		{
			if (!requireHtmlEncode)
			{
				writer.Write(LocalizedStrings.GetNonEncoded(units));
				return;
			}
			if (userContext == null)
			{
				writer.Write(LocalizedStrings.GetHtmlEncoded(units));
				return;
			}
			string name = userContext.UserCulture.Name;
			writer.Write(LocalizedStrings.GetHtmlEncodedFromKey(name, units));
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x000866B0 File Offset: 0x000848B0
		internal static Dictionary<T, Strings.IDs> CreateEnumLocalizedStringMap<T>(EnumInfo<T>[] enumInfoTable) where T : struct
		{
			Dictionary<T, Strings.IDs> dictionary = new Dictionary<T, Strings.IDs>(enumInfoTable.Length);
			foreach (EnumInfo<T> enumInfo in enumInfoTable)
			{
				dictionary.Add(enumInfo.EnumValue, enumInfo.StringIdValue);
			}
			return dictionary;
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x000866EC File Offset: 0x000848EC
		public static void MakePageCacheable(HttpResponse response)
		{
			Utilities.MakePageCacheable(response, null);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00086708 File Offset: 0x00084908
		public static void MakePageCacheable(HttpResponse response, int? expireDays)
		{
			if (expireDays == null)
			{
				expireDays = new int?(30);
			}
			DateTime expires = DateTime.UtcNow.Add(new TimeSpan(expireDays.Value, 0, 0, 0, 0)).ToLocalTime();
			response.Cache.SetCacheability(HttpCacheability.Private);
			response.Cache.SetExpires(expires);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00086765 File Offset: 0x00084965
		internal static void MakePageNoCacheNoStore(HttpResponse response)
		{
			response.Cache.SetCacheability(HttpCacheability.NoCache);
			response.Cache.SetNoStore();
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0008677E File Offset: 0x0008497E
		public static bool IsHexChar(char c)
		{
			return char.IsDigit(c) || (char.ToUpperInvariant(c) >= 'A' && char.ToUpperInvariant(c) <= 'F');
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000867A4 File Offset: 0x000849A4
		internal static bool IsValidApprovalRequest(MessageItem message)
		{
			if (!ObjectClass.IsOfClass(message.ClassName, "IPM.Note.Microsoft.Approval.Request"))
			{
				return false;
			}
			if (Utilities.IsSMime(message))
			{
				return false;
			}
			VotingInfo votingInfo = message.VotingInfo;
			if (votingInfo == null)
			{
				return false;
			}
			string[] array = (string[])votingInfo.GetOptionsList();
			return array != null && array.Length == 2;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000867F4 File Offset: 0x000849F4
		internal static bool IsValidUndecidedApprovalRequest(MessageItem message)
		{
			if (!Utilities.IsValidApprovalRequest(message))
			{
				return false;
			}
			int? valueAsNullable = message.GetValueAsNullable<int>(MessageItemSchema.LastVerbExecuted);
			if (valueAsNullable != null && valueAsNullable.Value >= 1 && valueAsNullable.Value < 100)
			{
				return false;
			}
			int? valueAsNullable2 = message.GetValueAsNullable<int>(MessageItemSchema.ApprovalDecision);
			return valueAsNullable2 == null || valueAsNullable2.Value < 1 || valueAsNullable2.Value >= 100;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00086864 File Offset: 0x00084A64
		public static bool IsValidGuid(string guid)
		{
			if (guid == null || guid.Length != 32)
			{
				return false;
			}
			for (int i = 0; i < 32; i++)
			{
				if (!Utilities.IsHexChar(guid[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000868A0 File Offset: 0x00084AA0
		internal static bool IsOnBehalfOf(Participant sender, Participant from)
		{
			return !(sender == null) && !string.IsNullOrEmpty(sender.EmailAddress) && !(from == null) && !string.IsNullOrEmpty(from.EmailAddress) && 0 != string.Compare(sender.EmailAddress, from.EmailAddress, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x000868F4 File Offset: 0x00084AF4
		public static string GetNewGuid()
		{
			return Guid.NewGuid().ToString("N");
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00086913 File Offset: 0x00084B13
		public static void JavascriptEncode(string s, TextWriter writer)
		{
			Utilities.JavascriptEncode(s, writer, false);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00086920 File Offset: 0x00084B20
		public static void JavascriptEncode(string s, TextWriter writer, bool escapeNonAscii)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int i = 0;
			while (i < s.Length)
			{
				char c = s[i];
				if (c <= '"')
				{
					if (c != '\n')
					{
						if (c != '\r')
						{
							switch (c)
							{
							case '!':
							case '"':
								goto IL_78;
							default:
								goto IL_B3;
							}
						}
						else
						{
							writer.Write('\\');
							writer.Write('r');
						}
					}
					else
					{
						writer.Write('\\');
						writer.Write('n');
					}
				}
				else if (c <= '/')
				{
					if (c != '\'' && c != '/')
					{
						goto IL_B3;
					}
					goto IL_78;
				}
				else
				{
					switch (c)
					{
					case '<':
					case '>':
						goto IL_78;
					case '=':
						goto IL_B3;
					default:
						if (c == '\\')
						{
							goto IL_78;
						}
						goto IL_B3;
					}
				}
				IL_E7:
				i++;
				continue;
				IL_78:
				writer.Write('\\');
				writer.Write(s[i]);
				goto IL_E7;
				IL_B3:
				if (escapeNonAscii && s[i] > '\u007f')
				{
					writer.Write("\\u{0:x4}", (ushort)s[i]);
					goto IL_E7;
				}
				writer.Write(s[i]);
				goto IL_E7;
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00086A24 File Offset: 0x00084C24
		public static string JavascriptEncode(string s, bool escapeNonAscii)
		{
			if (s == null)
			{
				return string.Empty;
			}
			StringBuilder sb = new StringBuilder();
			string result;
			using (StringWriter stringWriter = new StringWriter(sb))
			{
				Utilities.JavascriptEncode(s, stringWriter, escapeNonAscii);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00086A74 File Offset: 0x00084C74
		public static SanitizedHtmlString JavascriptEncode(SanitizedHtmlString s, bool escapeNonAscii)
		{
			if (s == null)
			{
				return SanitizedHtmlString.Empty;
			}
			StringBuilder builder = new StringBuilder();
			SanitizedHtmlString result;
			using (SanitizingStringWriter<OwaHtml> sanitizingStringWriter = new SanitizingStringWriter<OwaHtml>(builder))
			{
				Utilities.JavascriptEncode(s.ToString(), sanitizingStringWriter, escapeNonAscii);
				result = sanitizingStringWriter.ToSanitizedString<SanitizedHtmlString>();
			}
			return result;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00086AC8 File Offset: 0x00084CC8
		public static string JavascriptEncode(string s)
		{
			return Utilities.JavascriptEncode(s, false);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00086AD1 File Offset: 0x00084CD1
		public static SanitizedHtmlString JavascriptEncode(SanitizedHtmlString s)
		{
			return Utilities.JavascriptEncode(s, false);
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00086ADA File Offset: 0x00084CDA
		public static void HtmlEncode(string s, TextWriter writer)
		{
			Utilities.HtmlEncode(s, writer, false);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00086AE4 File Offset: 0x00084CE4
		public static void SanitizeHtmlEncode(string s, TextWriter writer)
		{
			if (writer is SanitizingTextWriter<OwaHtml>)
			{
				writer.Write(s);
				return;
			}
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString(Utilities.HtmlEncode(s, false));
			sanitizedHtmlString.DecreeToBeTrusted();
			writer.Write(sanitizedHtmlString);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00086B1C File Offset: 0x00084D1C
		public static void HtmlEncode(string s, TextWriter writer, bool encodeSpaces)
		{
			if (s == null || s.Length == 0)
			{
				return;
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (encodeSpaces)
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (s[i] == ' ')
					{
						writer.Write("&nbsp;");
					}
					else
					{
						writer.Write(AntiXssEncoder.HtmlEncode(s.Substring(i, 1), false));
					}
				}
				return;
			}
			HttpUtility.HtmlEncode(s, writer);
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00086B8C File Offset: 0x00084D8C
		public static void SanitizeHtmlEncode(string s, TextWriter writer, bool encodeSpaces)
		{
			if (writer is SanitizingTextWriter<OwaHtml>)
			{
				writer.Write(s);
				return;
			}
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString(Utilities.HtmlEncode(s, encodeSpaces));
			sanitizedHtmlString.DecreeToBeTrusted();
			writer.Write(sanitizedHtmlString);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00086BC3 File Offset: 0x00084DC3
		public static string HtmlEncode(string s)
		{
			return AntiXssEncoder.HtmlEncode(s, false);
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00086BCC File Offset: 0x00084DCC
		public static SanitizedHtmlString SanitizeHtmlEncode(string s)
		{
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString(AntiXssEncoder.HtmlEncode(s, false));
			sanitizedHtmlString.DecreeToBeTrusted();
			return sanitizedHtmlString;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00086BF0 File Offset: 0x00084DF0
		public static string HtmlEncode(string s, bool encodeSpaces)
		{
			if (encodeSpaces)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < s.Length; i++)
				{
					if (s[i] == ' ')
					{
						stringBuilder.Append("&nbsp;");
					}
					else
					{
						stringBuilder.Append(AntiXssEncoder.HtmlEncode(s.Substring(i, 1), false));
					}
				}
				return stringBuilder.ToString();
			}
			return HttpUtility.HtmlEncode(s);
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00086C54 File Offset: 0x00084E54
		public static SanitizedHtmlString SanitizeHtmlEncode(string s, bool encodeSpaces)
		{
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString(Utilities.HtmlEncode(s, encodeSpaces));
			sanitizedHtmlString.DecreeToBeTrusted();
			return sanitizedHtmlString;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00086C75 File Offset: 0x00084E75
		public static void HtmlEncode(string s, StringBuilder stringBuilder)
		{
			if (stringBuilder == null)
			{
				throw new ArgumentNullException("stringBuilder");
			}
			stringBuilder.Append(AntiXssEncoder.HtmlEncode(s, false));
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00086C94 File Offset: 0x00084E94
		public static void SanitizeHtmlEncode(string s, StringBuilder stringBuilder)
		{
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString(AntiXssEncoder.HtmlEncode(s, false));
			sanitizedHtmlString.DecreeToBeTrusted();
			stringBuilder.Append(sanitizedHtmlString);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00086CBC File Offset: 0x00084EBC
		public static string UrlEncode(string s)
		{
			return HttpUtility.UrlEncode(s);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00086CC4 File Offset: 0x00084EC4
		public static string ValidTokenBase64Encode(byte[] byteArray)
		{
			if (byteArray == null)
			{
				throw new ArgumentNullException("byteArray");
			}
			int num = (int)(1.3333333333333333 * (double)byteArray.Length);
			if (num % 4 != 0)
			{
				num += 4 - num % 4;
			}
			char[] array = new char[num];
			Convert.ToBase64CharArray(byteArray, 0, byteArray.Length, array, 0);
			int num2 = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == '\\')
				{
					array[i] = '-';
				}
				else if (array[i] == '=')
				{
					num2++;
				}
			}
			return new string(array, 0, array.Length - num2);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00086D48 File Offset: 0x00084F48
		public static byte[] ValidTokenBase64Decode(string tokenValidBase64String)
		{
			if (tokenValidBase64String == null)
			{
				throw new ArgumentNullException("tokenValidBase64String");
			}
			long num = (long)tokenValidBase64String.Length;
			if (tokenValidBase64String.Length % 4 != 0)
			{
				num += (long)(4 - tokenValidBase64String.Length % 4);
			}
			char[] array = new char[num];
			tokenValidBase64String.CopyTo(0, array, 0, tokenValidBase64String.Length);
			for (long num2 = 0L; num2 < (long)tokenValidBase64String.Length; num2 += 1L)
			{
				checked
				{
					if (array[(int)((IntPtr)num2)] == '-')
					{
						array[(int)((IntPtr)num2)] = '\\';
					}
				}
			}
			for (long num3 = (long)tokenValidBase64String.Length; num3 < (long)array.Length; num3 += 1L)
			{
				array[(int)(checked((IntPtr)num3))] = '=';
			}
			return Convert.FromBase64CharArray(array, 0, array.Length);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00086DE4 File Offset: 0x00084FE4
		public static string ConvertFromFontSize(int fontSize)
		{
			string result = "12";
			switch (fontSize)
			{
			case 1:
				result = "8";
				break;
			case 2:
				result = "10";
				break;
			case 3:
				result = "12";
				break;
			case 4:
				result = "14";
				break;
			case 5:
				result = "18";
				break;
			case 6:
				result = "24";
				break;
			case 7:
				result = "36";
				break;
			}
			return result;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00086E56 File Offset: 0x00085056
		public static void RewritePathToError(OwaContext owaContext, string errorDescription)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			if (errorDescription == null)
			{
				throw new ArgumentNullException("errorDescription");
			}
			Utilities.RewritePathToError(owaContext, errorDescription, null);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00086E7C File Offset: 0x0008507C
		public static void RewritePathToError(OwaContext owaContext, string errorDescription, string errorDetailedDescription)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "Utilities.RewritePathToError");
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			if (errorDescription == null)
			{
				throw new ArgumentNullException("errorDescription");
			}
			owaContext.ErrorInformation = new ErrorInformation
			{
				Message = errorDescription,
				MessageDetails = errorDetailedDescription
			};
			owaContext.HttpContext.RewritePath(OwaUrl.ErrorPage.ImplicitUrl);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00086EE8 File Offset: 0x000850E8
		public static void EndResponse(HttpContext httpContext, HttpStatusCode statusCode)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug<int>(0L, "Utilities.EndResponse: statusCode={0}", (int)statusCode);
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			Utilities.MakePageNoCacheNoStore(httpContext.Response);
			httpContext.Response.StatusCode = (int)statusCode;
			try
			{
				httpContext.Response.Flush();
				httpContext.ApplicationInstance.CompleteRequest();
			}
			catch (HttpException arg)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<HttpException>(0L, "Failed to flush and send response to client. {0}", arg);
			}
			httpContext.Response.End();
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00086F74 File Offset: 0x00085174
		public static void TransferToErrorPage(OwaContext owaContext, string errorDescription)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			if (errorDescription == null)
			{
				throw new ArgumentNullException("errorDescription");
			}
			Utilities.TransferToErrorPage(owaContext, errorDescription, null);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00086F9A File Offset: 0x0008519A
		public static void TransferToErrorPage(OwaContext owaContext, string errorDescription, string errorDetailedDescription)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			if (errorDescription == null)
			{
				throw new ArgumentNullException("errorDescription");
			}
			Utilities.TransferToErrorPage(owaContext, errorDescription, errorDetailedDescription, ThemeFileId.Error, false);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00086FC3 File Offset: 0x000851C3
		public static void TransferToErrorPage(OwaContext owaContext, string errorDescription, string errorDetailedDescription, ThemeFileId icon, bool hideDebugInformation)
		{
			Utilities.TransferToErrorPage(owaContext, errorDescription, errorDetailedDescription, icon, hideDebugInformation, false);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00086FD4 File Offset: 0x000851D4
		public static void TransferToErrorPage(OwaContext owaContext, string errorDescription, string errorDetailedDescription, ThemeFileId icon, bool hideDebugInformation, bool isDetailedErrorHtmlEncoded)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			if (errorDescription == null)
			{
				throw new ArgumentNullException("errorDescription");
			}
			Utilities.TransferToErrorPage(owaContext, new ErrorInformation
			{
				Message = errorDescription,
				MessageDetails = errorDetailedDescription,
				Icon = icon,
				HideDebugInformation = hideDebugInformation,
				IsDetailedErrorHtmlEncoded = isDetailedErrorHtmlEncoded,
				ExternalPageLink = Utilities.GenerateExternalLink(owaContext)
			});
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0008703C File Offset: 0x0008523C
		public static void TransferToErrorPage(OwaContext owaContext, ErrorInformation errorInformation)
		{
			ExTraceGlobals.CoreTracer.TraceDebug(0L, "Utilities.TransferToErrorPage");
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			if (errorInformation == null)
			{
				throw new ArgumentNullException("errorInformation");
			}
			owaContext.ErrorInformation = errorInformation;
			owaContext.HttpContext.Server.Transfer(OwaUrl.ErrorPage.ImplicitUrl);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00087097 File Offset: 0x00085297
		public static void DisableContentEncodingForThisResponse(HttpResponse response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			response.AddHeader("Content-Encoding", "none");
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x000870B8 File Offset: 0x000852B8
		internal static FolderVirtualListViewFilter GetFavoritesFilterViewParameter(UserContext userContext, Folder folder)
		{
			if (userContext.IsInMyMailbox(folder) && folder is SearchFolder)
			{
				object obj = folder.TryGetProperty(FolderSchema.SearchFolderAllowAgeout);
				if (obj is bool && !(bool)obj)
				{
					return FolderVirtualListViewFilter.ParseFromPropertyValue(folder.TryGetProperty(ViewStateProperties.FilteredViewLabel));
				}
			}
			return null;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00087104 File Offset: 0x00085304
		internal static bool IsFolderNameConflictError(FolderSaveResult result)
		{
			if (result.OperationResult != OperationResult.Succeeded && result.PropertyErrors != null)
			{
				foreach (PropertyError propertyError in result.PropertyErrors)
				{
					if (propertyError.PropertyErrorCode == PropertyErrorCode.FolderNameConflict)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0008714C File Offset: 0x0008534C
		internal static bool IsFavoritesFilterFolder(UserContext userContext, Folder folder)
		{
			return Utilities.GetFavoritesFilterViewParameter(userContext, folder) != null;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0008715B File Offset: 0x0008535B
		internal static StoreObjectId TryGetDefaultFolderId(MailboxSession session, DefaultFolderType type)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			return session.GetDefaultFolderId(type);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00087174 File Offset: 0x00085374
		internal static OwaStoreObjectId TryGetDefaultFolderId(UserContext userContext, MailboxSession session, DefaultFolderType defaultFolderType)
		{
			StoreObjectId storeObjectId = Utilities.TryGetDefaultFolderId(session, defaultFolderType);
			if (storeObjectId != null)
			{
				return OwaStoreObjectId.CreateFromSessionFolderId(userContext, session, storeObjectId);
			}
			return null;
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00087198 File Offset: 0x00085398
		internal static StoreObjectId GetDefaultFolderId(MailboxSession session, DefaultFolderType type)
		{
			StoreObjectId storeObjectId = Utilities.TryGetDefaultFolderId(session, type);
			if (storeObjectId == null)
			{
				throw new OwaDefaultFolderIdUnavailableException(string.Format("XSO returned null for default folder id {0}.", type.ToString()));
			}
			return storeObjectId;
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x000871CC File Offset: 0x000853CC
		internal static StoreObjectId TryGetDefaultFolderId(UserContext userContext, ExchangePrincipal exchangePrincipal, DefaultFolderType type)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (exchangePrincipal == null)
			{
				throw new ArgumentNullException("exchangePrincipal");
			}
			if (type == DefaultFolderType.None)
			{
				throw new ArgumentException("type");
			}
			StoreObjectId result;
			using (OwaStoreObjectIdSessionHandle owaStoreObjectIdSessionHandle = new OwaStoreObjectIdSessionHandle(exchangePrincipal, userContext))
			{
				MailboxSession session = owaStoreObjectIdSessionHandle.Session as MailboxSession;
				result = Utilities.TryGetDefaultFolderId(session, type);
			}
			return result;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0008723C File Offset: 0x0008543C
		internal static StoreObjectId TryGetDefaultFolderId(UserContext userContext, ExchangePrincipal exchangePrincipal, DefaultFolderType type, out MailboxSession session)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (exchangePrincipal == null)
			{
				throw new ArgumentNullException("exchangePrincipal");
			}
			if (type == DefaultFolderType.None)
			{
				throw new ArgumentException("type");
			}
			OwaStoreObjectIdSessionHandle owaStoreObjectIdSessionHandle = new OwaStoreObjectIdSessionHandle(exchangePrincipal, userContext);
			userContext.AddSessionHandle(owaStoreObjectIdSessionHandle);
			session = (owaStoreObjectIdSessionHandle.Session as MailboxSession);
			return Utilities.TryGetDefaultFolderId(session, type);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00087297 File Offset: 0x00085497
		internal static OwaStoreObjectId GetDefaultFolderId(UserContext userContext, MailboxSession session, DefaultFolderType defaultFolderType)
		{
			return OwaStoreObjectId.CreateFromSessionFolderId(userContext, session, Utilities.GetDefaultFolderId(session, defaultFolderType));
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000872A8 File Offset: 0x000854A8
		internal static bool IsDefaultFolderId(StoreSession session, StoreObjectId folderId, DefaultFolderType defaultFolderType)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			MailboxSession mailboxSession = session as MailboxSession;
			if (mailboxSession == null)
			{
				return false;
			}
			StoreObjectId storeObjectId = Utilities.TryGetDefaultFolderId(mailboxSession, defaultFolderType);
			return storeObjectId != null && folderId.Equals(storeObjectId);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x000872F1 File Offset: 0x000854F1
		internal static bool IsItemInDefaultFolder(StoreObject storeObject, DefaultFolderType type)
		{
			return Utilities.IsDefaultFolderId(storeObject.Session, storeObject.ParentId, type);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00087308 File Offset: 0x00085508
		internal static bool IsItemInDefaultFolder(IStorePropertyBag storePropertyBag, DefaultFolderType type, MailboxSession session)
		{
			StoreObjectId property = ItemUtility.GetProperty<StoreObjectId>(storePropertyBag, StoreObjectSchema.ParentItemId, null);
			return property != null && Utilities.IsDefaultFolderId(session, property, type);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0008732F File Offset: 0x0008552F
		internal static bool IsDefaultFolderId(UserContext userContext, OwaStoreObjectId folderId, DefaultFolderType defaultFolderType)
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			return !folderId.IsPublic && Utilities.IsDefaultFolderId(folderId.GetSession(userContext), folderId.StoreObjectId, defaultFolderType);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0008735C File Offset: 0x0008555C
		internal static bool IsDefaultFolder(Folder folder, DefaultFolderType defaultFolderType)
		{
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			return Utilities.IsDefaultFolderId(folder.Session, folder.Id.ObjectId, defaultFolderType);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00087384 File Offset: 0x00085584
		internal static bool IsFolderSharedOut(Folder folder)
		{
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			return Utilities.IsOneOfTheFolderFlagsSet(folder, new ExtendedFolderFlags[]
			{
				ExtendedFolderFlags.SharedOut,
				ExtendedFolderFlags.SharedViaExchange
			});
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x000873C0 File Offset: 0x000855C0
		internal static bool IsFolderSharedOut(ExtendedFolderFlags folderShareFlag)
		{
			return Utilities.IsOneOfTheFolderFlagsSet(folderShareFlag, new ExtendedFolderFlags[]
			{
				ExtendedFolderFlags.SharedOut,
				ExtendedFolderFlags.SharedViaExchange
			});
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x000873F0 File Offset: 0x000855F0
		internal static bool IsOneOfTheFolderFlagsSet(object folderFlagValue, params ExtendedFolderFlags[] folderFlags)
		{
			int valueToTest = (folderFlagValue is PropertyError) ? 0 : ((int)folderFlagValue);
			foreach (ExtendedFolderFlags flag in folderFlags)
			{
				if (Utilities.IsFlagSet(valueToTest, (int)flag))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00087438 File Offset: 0x00085638
		internal static bool IsOneOfTheFolderFlagsSet(Folder folder, params ExtendedFolderFlags[] folderFlags)
		{
			return Utilities.IsOneOfTheFolderFlagsSet(folder.TryGetProperty(FolderSchema.ExtendedFolderFlags), folderFlags);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0008744C File Offset: 0x0008564C
		internal static bool CanFolderBeRenamed(UserContext userContext, Folder folder)
		{
			if (Utilities.IsPublic(folder))
			{
				return !userContext.IsPublicFolderRootId(folder.Id.ObjectId);
			}
			return !Utilities.IsSpecialFolderForSession(folder.Session as MailboxSession, folder.Id.ObjectId) && !Utilities.IsELCFolder(folder) && !Utilities.IsOutlookSearchFolder(folder) && !Utilities.IsOneOfTheFolderFlagsSet(folder, new ExtendedFolderFlags[]
			{
				ExtendedFolderFlags.RemoteHierarchy
			});
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x000874BE File Offset: 0x000856BE
		internal static DefaultFolderType GetDefaultFolderType(Folder folder)
		{
			return Utilities.GetDefaultFolderType(folder.Session, folder.Id.ObjectId);
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x000874D8 File Offset: 0x000856D8
		internal static DefaultFolderType GetDefaultFolderType(StoreSession storeSession, StoreObjectId folderId)
		{
			MailboxSession mailboxSession = storeSession as MailboxSession;
			if (mailboxSession == null)
			{
				return DefaultFolderType.None;
			}
			return mailboxSession.IsDefaultFolderType(folderId);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x000874F8 File Offset: 0x000856F8
		internal static bool IsSpecialFolder(StoreObjectId id, UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return Utilities.IsSpecialFolderForSession(userContext.MailboxSession, id);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00087514 File Offset: 0x00085714
		internal static bool IsSpecialFolderForSession(MailboxSession session, StoreObjectId folderId)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			return Utilities.IsSpecialFolderType(session.IsDefaultFolderType(folderId));
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0008753E File Offset: 0x0008573E
		internal static bool IsSpecialFolderType(DefaultFolderType defaultFolderType)
		{
			return defaultFolderType != DefaultFolderType.None;
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00087547 File Offset: 0x00085747
		internal static string GetFolderNameWithSessionName(Folder folder)
		{
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			return string.Format(LocalizedStrings.GetNonEncoded(-83764036), Utilities.GetSessionMailboxDisplayName(folder), folder.DisplayName);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00087574 File Offset: 0x00085774
		internal static Folder SafeFolderBind(MailboxSession mailboxSession, DefaultFolderType defaultFolderType, params PropertyDefinition[] returnProperties)
		{
			StoreObjectId storeObjectId = Utilities.TryGetDefaultFolderId(mailboxSession, defaultFolderType);
			Folder result = null;
			if (storeObjectId != null)
			{
				try
				{
					result = Folder.Bind(mailboxSession, defaultFolderType, returnProperties);
				}
				catch (ObjectNotFoundException ex)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Failed to bind to folder: Error: {0}", ex.Message);
				}
			}
			return result;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000875C4 File Offset: 0x000857C4
		internal static Folder SafeFolderBind(MailboxSession mailboxSession, StoreObjectId folderId, params PropertyDefinition[] returnProperties)
		{
			Folder result = null;
			try
			{
				result = Folder.Bind(mailboxSession, folderId, returnProperties);
			}
			catch (ObjectNotFoundException ex)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Failed to bind to folder: Error: {0}", ex.Message);
			}
			return result;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0008760C File Offset: 0x0008580C
		private static void ThrowInvalidIdFormatException(string storeObjectId, string changeKey, Exception innerException)
		{
			throw new OwaInvalidIdFormatException(string.Format("Invalid id format. Store object id: {0}. Change key: {1}", (storeObjectId == null) ? "null" : storeObjectId, (changeKey == null) ? "null" : changeKey), innerException);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00087634 File Offset: 0x00085834
		internal static VersionedId CreateItemId(MailboxSession mailboxSession, StoreObjectId storeObjectId, string changeKey)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (storeObjectId == null)
			{
				throw new ArgumentNullException("storeObjectId");
			}
			return Utilities.CreateItemId(mailboxSession, storeObjectId.ToBase64String(), changeKey);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0008765F File Offset: 0x0008585F
		internal static VersionedId CreateItemId(MailboxSession mailboxSession, string storeObjectId, string changeKey)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			return Utilities.CreateItemId(storeObjectId, changeKey);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00087676 File Offset: 0x00085876
		internal static VersionedId CreateItemId(StoreObjectId storeObjectId, string changeKey)
		{
			if (storeObjectId == null)
			{
				throw new ArgumentNullException("storeObjectId");
			}
			return Utilities.CreateItemId(storeObjectId.ToBase64String(), changeKey);
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00087694 File Offset: 0x00085894
		internal static VersionedId CreateItemId(string storeObjectId, string changeKey)
		{
			if (string.IsNullOrEmpty(storeObjectId))
			{
				throw new OwaInvalidIdFormatException("Missing store object id");
			}
			if (string.IsNullOrEmpty(changeKey))
			{
				throw new OwaInvalidIdFormatException("Missing change key");
			}
			VersionedId result = null;
			try
			{
				result = VersionedId.Deserialize(storeObjectId, changeKey);
			}
			catch (ArgumentException innerException)
			{
				Utilities.ThrowInvalidIdFormatException(storeObjectId, changeKey, innerException);
			}
			catch (FormatException innerException2)
			{
				Utilities.ThrowInvalidIdFormatException(storeObjectId, changeKey, innerException2);
			}
			catch (CorruptDataException innerException3)
			{
				Utilities.ThrowInvalidIdFormatException(storeObjectId, changeKey, innerException3);
			}
			return result;
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00087720 File Offset: 0x00085920
		internal static StoreObjectId CreateStoreObjectId(MailboxSession mailboxSession, string storeObjectId)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (string.IsNullOrEmpty(storeObjectId))
			{
				throw new OwaInvalidIdFormatException("Missing store object id");
			}
			return Utilities.CreateStoreObjectId(storeObjectId);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0008774C File Offset: 0x0008594C
		internal static StoreObjectId CreateStoreObjectId(string storeObjectIdString)
		{
			if (storeObjectIdString == null)
			{
				throw new ArgumentNullException("storeObjectIdString");
			}
			StoreObjectId result = null;
			try
			{
				result = StoreObjectId.Deserialize(storeObjectIdString);
			}
			catch (ArgumentException innerException)
			{
				Utilities.ThrowInvalidIdFormatException(storeObjectIdString, null, innerException);
			}
			catch (FormatException innerException2)
			{
				Utilities.ThrowInvalidIdFormatException(storeObjectIdString, null, innerException2);
			}
			catch (CorruptDataException innerException3)
			{
				Utilities.ThrowInvalidIdFormatException(storeObjectIdString, null, innerException3);
			}
			return result;
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x000877BC File Offset: 0x000859BC
		internal static byte[] CreateInstanceKey(string instanceKeyString)
		{
			if (instanceKeyString == null)
			{
				throw new ArgumentNullException("instanceKeyString");
			}
			byte[] result = null;
			try
			{
				result = Convert.FromBase64String(instanceKeyString);
			}
			catch (ArgumentException innerException)
			{
				Utilities.ThrowInvalidIdFormatException(instanceKeyString, null, innerException);
			}
			catch (FormatException innerException2)
			{
				Utilities.ThrowInvalidIdFormatException(instanceKeyString, null, innerException2);
			}
			catch (CorruptDataException innerException3)
			{
				Utilities.ThrowInvalidIdFormatException(instanceKeyString, null, innerException3);
			}
			return result;
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0008782C File Offset: 0x00085A2C
		internal static StoreId TryGetStoreId(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			OwaStoreObjectId owaStoreObjectId = objectId as OwaStoreObjectId;
			if (owaStoreObjectId != null)
			{
				return owaStoreObjectId.StoreId;
			}
			ConversationId conversationId = objectId as ConversationId;
			if (conversationId != null)
			{
				return conversationId;
			}
			StoreId storeId = objectId as StoreId;
			if (storeId != null)
			{
				return StoreId.GetStoreObjectId(storeId);
			}
			return null;
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00087878 File Offset: 0x00085A78
		public static string ProviderSpecificIdFromStoreObjectId(string storeObjectId)
		{
			if (storeObjectId == null)
			{
				throw new ArgumentNullException("storeObjectId");
			}
			string result = null;
			try
			{
				result = Convert.ToBase64String(Utilities.CreateStoreObjectId(storeObjectId).ProviderLevelItemId);
			}
			catch (ArgumentException innerException)
			{
				Utilities.ThrowInvalidIdFormatException(storeObjectId, null, innerException);
			}
			return result;
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x000878C4 File Offset: 0x00085AC4
		internal static OwaStoreObjectIdType GetOwaStoreObjectIdType(UserContext userContext, StoreObject storeObject)
		{
			if (storeObject == null)
			{
				throw new ArgumentNullException("storeObject");
			}
			OwaStoreObjectIdType result = OwaStoreObjectIdType.MailBoxObject;
			if (Utilities.IsPublic(storeObject))
			{
				if (storeObject is Item)
				{
					result = OwaStoreObjectIdType.PublicStoreItem;
				}
				else
				{
					result = OwaStoreObjectIdType.PublicStoreFolder;
				}
			}
			else if (userContext.IsInOtherMailbox(storeObject))
			{
				result = OwaStoreObjectIdType.OtherUserMailboxObject;
			}
			else if (Utilities.IsInArchiveMailbox(storeObject))
			{
				result = OwaStoreObjectIdType.ArchiveMailboxObject;
			}
			return result;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00087911 File Offset: 0x00085B11
		internal static bool IsPublic(StoreObject storeObject)
		{
			if (storeObject == null)
			{
				throw new ArgumentNullException("storeObject");
			}
			return storeObject.Session is PublicFolderSession;
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0008792F File Offset: 0x00085B2F
		internal static bool IsOtherMailbox(StoreObject storeObject)
		{
			if (storeObject == null)
			{
				throw new ArgumentNullException("storeObject");
			}
			return OwaContext.Current.UserContext.IsInOtherMailbox(storeObject);
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0008794F File Offset: 0x00085B4F
		internal static bool IsInArchiveMailbox(StoreObject storeObject)
		{
			if (storeObject == null)
			{
				throw new ArgumentNullException("storeObject");
			}
			return Utilities.IsArchiveMailbox(storeObject.Session);
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0008796C File Offset: 0x00085B6C
		internal static bool IsArchiveMailbox(StoreSession storeSession)
		{
			MailboxSession mailboxSession = storeSession as MailboxSession;
			return mailboxSession != null && Utilities.IsArchiveMailbox(mailboxSession);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0008798B File Offset: 0x00085B8B
		private static bool IsArchiveMailbox(MailboxSession session)
		{
			return session.MailboxOwner.MailboxInfo.IsArchive;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000879A0 File Offset: 0x00085BA0
		internal static string GetMailboxSessionLegacyDN(StoreObject storeObject)
		{
			if (storeObject == null)
			{
				throw new ArgumentNullException("storeObject");
			}
			MailboxSession mailboxSession = storeObject.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new OwaInvalidOperationException("Store object must belong to Mailbox Session");
			}
			return mailboxSession.MailboxOwnerLegacyDN;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x000879DC File Offset: 0x00085BDC
		internal static string GetSessionMailboxDisplayName(StoreObject storeObject)
		{
			if (storeObject == null)
			{
				throw new ArgumentNullException("storeObject");
			}
			MailboxSession mailboxSession = storeObject.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new OwaInvalidOperationException("Store object must belong to Mailbox Session");
			}
			return mailboxSession.DisplayName;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00087A18 File Offset: 0x00085C18
		internal static bool IsValidLegacyDN(string address)
		{
			if (string.IsNullOrEmpty(address))
			{
				throw new ArgumentNullException("address");
			}
			LegacyDN legacyDN;
			return LegacyDN.TryParse(address, out legacyDN);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00087A40 File Offset: 0x00085C40
		internal static IExchangePrincipal GetFolderOwnerExchangePrincipal(OwaStoreObjectId folderId, UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (folderId == null)
			{
				return userContext.ExchangePrincipal;
			}
			MailboxSession mailboxSession = (MailboxSession)folderId.GetSession(userContext);
			return mailboxSession.MailboxOwner;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00087A78 File Offset: 0x00085C78
		public static string GetQueryStringParameter(HttpRequest httpRequest, string name)
		{
			return Utilities.GetQueryStringParameter(httpRequest, name, true);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00087A84 File Offset: 0x00085C84
		public static string GetQueryStringParameter(HttpRequest httpRequest, string name, bool required)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			string text = httpRequest.QueryString[name];
			if (text == null && required)
			{
				throw new OwaInvalidRequestException(string.Format("Required URL parameter missing: {0}", name));
			}
			return text;
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00087AD7 File Offset: 0x00085CD7
		public static string[] GetQueryStringArrayParameter(HttpRequest httpRequest, string name, bool required)
		{
			return Utilities.GetQueryStringArrayParameter(httpRequest, name, required, 0);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00087AE4 File Offset: 0x00085CE4
		public static string[] GetQueryStringArrayParameter(HttpRequest httpRequest, string name, bool required, int maxLength)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			string[] values = httpRequest.QueryString.GetValues(name);
			if (values == null)
			{
				if (required)
				{
					throw new OwaInvalidRequestException(string.Format("Required URL parameter missing: {0}", name));
				}
			}
			else if (maxLength > 0 && values.Length > maxLength)
			{
				throw new OwaInvalidRequestException(string.Format("Parameter has too many values: {0}", name));
			}
			return values;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00087B52 File Offset: 0x00085D52
		public static ExDateTime GetQueryStringParameterDateTime(HttpRequest httpRequest, string name, ExTimeZone timeZone)
		{
			return Utilities.GetQueryStringParameterDateTime(httpRequest, name, timeZone, true);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00087B60 File Offset: 0x00085D60
		private static ExDateTime GetQueryStringParameterDateTime(HttpRequest httpRequest, string name, ExTimeZone timeZone, bool required)
		{
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			ExDateTime result = ExDateTime.MinValue;
			string queryStringParameter = Utilities.GetQueryStringParameter(httpRequest, name, required);
			if (queryStringParameter == null)
			{
				return result;
			}
			try
			{
				result = DateTimeUtilities.ParseIsoDate(queryStringParameter, timeZone);
			}
			catch (OwaParsingErrorException)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string, string>(0L, "Invalid date '{0}' provided on URL '{1}'", name, queryStringParameter);
				throw new OwaInvalidRequestException("Invalid date time on URL");
			}
			return result;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00087BCC File Offset: 0x00085DCC
		public static ExDateTime[] GetQueryStringParameterDateTimeArray(HttpRequest httpRequest, string name, ExTimeZone timeZone, bool required, int maxLength)
		{
			string[] queryStringArrayParameter = Utilities.GetQueryStringArrayParameter(httpRequest, name, required, maxLength);
			if (queryStringArrayParameter == null)
			{
				return null;
			}
			ExDateTime[] array = new ExDateTime[queryStringArrayParameter.Length];
			for (int i = 0; i < queryStringArrayParameter.Length; i++)
			{
				try
				{
					array[i] = DateTimeUtilities.ParseIsoDate(queryStringArrayParameter[i], timeZone);
				}
				catch (OwaParsingErrorException)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string, string>(0L, "Invalid date '{0}' provided on URL '{1}'", name, queryStringArrayParameter[i]);
					throw new OwaInvalidRequestException("Invalid date time on URL");
				}
			}
			return array;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00087C4C File Offset: 0x00085E4C
		public static string GetFormParameter(HttpRequest httpRequest, string name)
		{
			return Utilities.GetFormParameter(httpRequest, name, true);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00087C58 File Offset: 0x00085E58
		public static string GetFormParameter(HttpRequest httpRequest, string name, bool required)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name", "name cannot be null or empty");
			}
			string text = httpRequest.Form[name];
			if (text == null && required)
			{
				throw new OwaInvalidRequestException(string.Format("Required form parameter missing: {0}", name));
			}
			return text;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00087CB0 File Offset: 0x00085EB0
		public static SecureString GetSecureFormParameter(HttpRequest httpRequest, string name)
		{
			return Utilities.GetSecureFormParameter(httpRequest, name, true);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00087CBA File Offset: 0x00085EBA
		public static SecureString GetSecureFormParameter(HttpRequest httpRequest, string name, bool required)
		{
			return Utilities.SecureStringFromString(Utilities.GetFormParameter(httpRequest, name, required));
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00087CC9 File Offset: 0x00085EC9
		public static SecureString SecureStringFromString(string regularString)
		{
			return regularString.AsSecureString();
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00087CD4 File Offset: 0x00085ED4
		public static bool SecureStringEquals(SecureString a, SecureString b)
		{
			if (a == null || b == null || a.Length != b.Length)
			{
				return false;
			}
			using (SecureArray<char> secureArray = a.ConvertToSecureCharArray())
			{
				using (SecureArray<char> secureArray2 = b.ConvertToSecureCharArray())
				{
					for (int i = 0; i < a.Length; i++)
					{
						if (secureArray.ArrayValue[i] != secureArray2.ArrayValue[i])
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00087D64 File Offset: 0x00085F64
		internal static void HandleException(OwaContext owaContext, Exception exception)
		{
			Utilities.HandleException(owaContext, exception, false);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00087D70 File Offset: 0x00085F70
		internal static string FormatExceptionNameAndMessage(string prefix, Exception e)
		{
			StringBuilder stringBuilder = new StringBuilder(prefix);
			while (e != null)
			{
				stringBuilder.AppendFormat("[{0}:{1}]", e.GetType().FullName, e.Message);
				e = e.InnerException;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00087DB4 File Offset: 0x00085FB4
		internal static void HandleException(OwaContext owaContext, Exception exception, bool showErrorInPage)
		{
			Microsoft.Exchange.Clients.Owa.Core.Culture.SetThreadCulture(owaContext);
			HttpContext httpContext = owaContext.HttpContext;
			ExTraceGlobals.CoreTracer.TraceDebug<Type, string>(0L, "Exception: Type: {0} Error: {1}.", exception.GetType(), exception.Message);
			Utilities.MakePageNoCacheNoStore(httpContext.Response);
			string str;
			if (!Utilities.ExceptionCodeMap.TryGetValue(exception.GetType(), out str))
			{
				str = "UE:" + exception.GetType().ToString();
			}
			owaContext.HttpContext.Response.AppendToLog("&ex=" + str);
			if (exception is HttpException)
			{
				HttpException ex = (HttpException)exception;
				httpContext.Response.AppendToLog(string.Format("&BadRequest=BasicHttpException:{0}", ex.GetHttpCode()));
				httpContext.Response.AppendToLog(Utilities.FormatExceptionNameAndMessage("&exception=", exception));
				Utilities.EndResponse(httpContext, HttpStatusCode.BadRequest);
				return;
			}
			if (exception is OwaRenderingEmbeddedReadingPaneException)
			{
				owaContext.UserContext.DisableEmbeddedReadingPane();
				if (owaContext.FormsRegistryContext.ApplicationElement == ApplicationElement.StartPage)
				{
					owaContext.HttpContext.Response.Clear();
					string explicitUrl = OwaUrl.ApplicationRoot.GetExplicitUrl(owaContext);
					owaContext.HttpContext.Response.Redirect(explicitUrl);
					return;
				}
				exception = (exception as OwaRenderingEmbeddedReadingPaneException).InnerException;
			}
			if (exception is OwaInvalidRequestException || exception is OwaInvalidIdFormatException)
			{
				httpContext.Response.AppendToLog("&BadRequest=BasicInvlaidRequest");
				httpContext.Response.AppendToLog(Utilities.FormatExceptionNameAndMessage("&exception=", exception));
				Utilities.EndResponse(httpContext, HttpStatusCode.BadRequest);
				return;
			}
			if (exception is OwaSegmentationException || exception is OwaForbiddenRequestException)
			{
				Utilities.EndResponse(httpContext, HttpStatusCode.Forbidden);
				return;
			}
			if (exception is OwaDelegatorMailboxFailoverException)
			{
				OwaDelegatorMailboxFailoverException ex2 = exception as OwaDelegatorMailboxFailoverException;
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Delegator {0}'s mailbox failover occurs.", ex2.MailboxOwnerLegacyDN);
			}
			if (exception is MailboxInSiteFailoverException)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "User {0}'s mailbox in-site failover occurs.", owaContext.ExchangePrincipal.LegacyDn);
				if (owaContext.UserContext != null)
				{
					owaContext.UserContext.DisconnectMailboxSession();
				}
			}
			if (exception is MailboxCrossSiteFailoverException || exception is WrongServerException)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "User {0}'s mailbox cross-site failover occurs.", owaContext.ExchangePrincipal.LegacyDn);
				UserContextCookie userContextCookie = UserContextCookie.GetUserContextCookie(owaContext);
				if (userContextCookie != null)
				{
					Utilities.DeleteCookie(httpContext.Response, userContextCookie.CookieName);
				}
				if (owaContext.IsProxyRequest)
				{
					httpContext.Response.AddHeader("mailboxCrossSiteFailover", "true");
				}
			}
			if (exception is WrongCASServerBecauseOfOutOfDateDNSCacheException)
			{
				Utilities.DeleteFBASessionCookies(httpContext.Response);
			}
			if (exception is OwaURLIsOutOfDateException)
			{
				Utilities.DeleteFBASessionCookies(httpContext.Response);
			}
			if (exception is OverBudgetException)
			{
				OverBudgetException ex3 = (OverBudgetException)exception;
				httpContext.Response.AppendToLog(string.Format("&OverBudget({0}/{1}),Owner:{2}[{3}]", new object[]
				{
					ex3.IsServiceAccountBudget ? "ServiceAccount" : "Normal",
					ex3.PolicyPart,
					ex3.Owner,
					ex3.Snapshot
				}));
			}
			string queryStringParameter = Utilities.GetQueryStringParameter(httpContext.Request, "prntFId", false);
			string previousPageUrl = null;
			if (!string.IsNullOrEmpty(queryStringParameter))
			{
				previousPageUrl = OwaUrl.ApplicationRoot.GetExplicitUrl(owaContext) + "?ae=Folder&t=IPF.DocumentLibrary&URL=" + Utilities.UrlEncode(queryStringParameter);
			}
			string externalPageLink = Utilities.GenerateExternalLink(owaContext);
			bool isBasicAuthentication = Utilities.IsBasicAuthentication(httpContext.Request);
			ErrorInformation exceptionHandlingInformation = Utilities.GetExceptionHandlingInformation(exception, owaContext.MailboxIdentity, Utilities.IsWebPartRequest(owaContext), previousPageUrl, externalPageLink, isBasicAuthentication, owaContext, !showErrorInPage);
			if (owaContext.FormsRegistryContext.ApplicationElement == ApplicationElement.StartPage)
			{
				string text = (owaContext.ExchangePrincipal != null) ? owaContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString() : string.Empty;
				string text2 = (owaContext.HttpContext.Request != null) ? owaContext.HttpContext.Request.Url.ToString() : string.Empty;
				if (exception is OwaTransientException || exception is StorageTransientException || exception is ADTransientException || exception is ThreadAbortException)
				{
					OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_OwaStartPageInitializationWarning, null, new object[]
					{
						text,
						text2,
						exception
					});
				}
				else
				{
					OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_OwaStartPageInitializationError, null, new object[]
					{
						text,
						text2,
						exception
					});
				}
			}
			try
			{
				if (!owaContext.ErrorSent)
				{
					owaContext.ErrorSent = true;
					httpContext.Response.Clear();
					OwaRequestType requestType = owaContext.RequestType;
					if (requestType == OwaRequestType.Invalid)
					{
						requestType = Utilities.GetRequestType(owaContext.HttpContext.Request);
					}
					StringBuilder stringBuilder = new StringBuilder();
					StringWriter stringWriter = new StringWriter(stringBuilder);
					if (Utilities.IsOehOrSubPageContentRequest(owaContext) && !showErrorInPage)
					{
						ExTraceGlobals.CoreTracer.TraceDebug(0L, "OEH error response");
						OwaEventHttpHandler.RenderError(owaContext, stringWriter, exceptionHandlingInformation.Message, exceptionHandlingInformation.MessageDetails, exceptionHandlingInformation.OwaEventHandlerErrorCode, exceptionHandlingInformation.HideDebugInformation ? null : exception);
					}
					else
					{
						ExTraceGlobals.CoreTracer.TraceDebug(0L, "Error page error response");
						owaContext.ErrorInformation = exceptionHandlingInformation;
						httpContext.Server.Execute(exceptionHandlingInformation.OwaUrl.ImplicitUrl, stringWriter);
					}
					stringWriter.Close();
					httpContext.Response.Write(stringBuilder);
					try
					{
						if (requestType == OwaRequestType.ICalHttpHandler)
						{
							httpContext.Response.TrySkipIisCustomErrors = true;
							httpContext.Response.StatusCode = 503;
						}
						else
						{
							httpContext.Response.StatusCode = 200;
						}
						httpContext.Response.AppendHeader("Content-Length", httpContext.Response.Output.Encoding.GetByteCount(stringBuilder.ToString()).ToString());
					}
					catch (HttpException arg)
					{
						ExTraceGlobals.CoreTracer.TraceDebug<HttpException>(0L, "Failed to set the header status after submitting watson and rendering error page. {0}", arg);
					}
					try
					{
						httpContext.Response.Flush();
						httpContext.ApplicationInstance.CompleteRequest();
					}
					catch (HttpException arg2)
					{
						ExTraceGlobals.CoreTracer.TraceDebug<HttpException>(0L, "Failed to flush and send response to client after submitting watson and rendering error page. {0}", arg2);
					}
				}
			}
			finally
			{
				if (exceptionHandlingInformation.SendWatsonReport && Globals.SendWatsonReports)
				{
					ExTraceGlobals.CoreTracer.TraceDebug(0L, "Sending watson report");
					ExWatson.AddExtraData(Utilities.GetExtraWatsonData(owaContext));
					if (exception.Data.Contains("ActiveEntryCallStack"))
					{
						ExWatson.AddExtraData(exception.Data["ActiveEntryCallStack"].ToString());
					}
					ReportOptions options = (exception is AccessViolationException || exception is InvalidProgramException || exception is TypeInitializationException) ? ReportOptions.ReportTerminateAfterSend : ReportOptions.None;
					ExWatson.SendReport(exception, options, null);
				}
				if (exception is AccessViolationException)
				{
					ExTraceGlobals.CoreTracer.TraceDebug(0L, "Shutting down OWA due to unrecoverable exception");
					Environment.Exit(1);
				}
				else if ((exception is InvalidProgramException || exception is TypeInitializationException) && Interlocked.Exchange(ref Utilities.queuedDelayedRestart, 1) == 0)
				{
					new Thread(new ThreadStart(Utilities.DelayedRestartUponUnexecutableCode)).Start();
				}
				httpContext.Response.End();
			}
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000884A4 File Offset: 0x000866A4
		internal static bool IsOehOrSubPageContentRequest(OwaContext owaContext)
		{
			return owaContext.RequestType == OwaRequestType.Oeh || Utilities.IsSubPageContentRequest(owaContext) || owaContext.RequestType == OwaRequestType.ProxyToEwsEventHandler;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000884C4 File Offset: 0x000866C4
		internal static bool IsSubPageContentRequest(OwaContext owaContext)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(owaContext.HttpContext.Request, "SP", false);
			return queryStringParameter != null && queryStringParameter == "1";
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x000884FC File Offset: 0x000866FC
		internal static bool IsPrefetchRequest(OwaContext owaContext)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(owaContext.HttpContext.Request, "pf", false);
			return queryStringParameter != null && queryStringParameter == "1";
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00088530 File Offset: 0x00086730
		internal static IRecipientSession CreateScopedRecipientSession(bool readOnly, ConsistencyMode consistencyMode, string domain)
		{
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
				return DirectorySessionFactory.Default.CreateRootOrgRecipientSession(null, null, CultureInfo.InvariantCulture.LCID, readOnly, consistencyMode, null, sessionSettings, 3731, "CreateScopedRecipientSession", "f:\\15.00.1497\\sources\\dev\\clients\\src\\owa\\bin\\core\\Utilities.cs");
			}
			if (domain == null)
			{
				throw new ArgumentException("Domain");
			}
			ADSessionSettings sessionSettings2 = ADSessionSettings.FromTenantAcceptedDomain(domain);
			return DirectorySessionFactory.Default.CreateTenantRecipientSession(null, null, CultureInfo.InvariantCulture.LCID, readOnly, consistencyMode, null, sessionSettings2, 3717, "CreateScopedRecipientSession", "f:\\15.00.1497\\sources\\dev\\clients\\src\\owa\\bin\\core\\Utilities.cs");
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x000885D0 File Offset: 0x000867D0
		internal static IRecipientSession CreateADRecipientSession(ConsistencyMode consistencyMode, UserContext userContext)
		{
			return Utilities.CreateADRecipientSession(true, consistencyMode, userContext);
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x000885DA File Offset: 0x000867DA
		internal static IRecipientSession CreateADRecipientSession(bool readOnly, ConsistencyMode consistencyMode, UserContext userContext)
		{
			return Utilities.CreateADRecipientSession(CultureInfo.CurrentCulture.LCID, readOnly, consistencyMode, false, userContext, true);
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x000885F0 File Offset: 0x000867F0
		internal static IRecipientSession CreateADRecipientSession(int lcid, bool readOnly, ConsistencyMode consistencyMode, bool useDirectorySearchRoot, UserContext userContext)
		{
			return Utilities.CreateADRecipientSession(lcid, readOnly, consistencyMode, useDirectorySearchRoot, userContext, true);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00088600 File Offset: 0x00086800
		internal static IRecipientSession CreateADRecipientSession(int lcid, bool readOnly, ConsistencyMode consistencyMode, bool useDirectorySearchRoot, UserContext userContext, bool scopeToGal)
		{
			ADSessionSettings adsessionSettings;
			if (scopeToGal)
			{
				adsessionSettings = ADSessionSettings.FromOrganizationIdWithAddressListScopeServiceOnly(userContext.ExchangePrincipal.MailboxInfo.OrganizationId, (userContext.GlobalAddressList != null) ? userContext.GlobalAddressList.Id : null);
			}
			else if (userContext.ExchangePrincipal.MailboxInfo.Configuration.AddressBookPolicy != null)
			{
				adsessionSettings = ADSessionSettings.FromOrganizationIdWithAddressListScopeServiceOnly(userContext.ExchangePrincipal.MailboxInfo.OrganizationId, userContext.GlobalAddressListId);
			}
			else
			{
				adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(userContext.ExchangePrincipal.MailboxInfo.OrganizationId);
			}
			adsessionSettings.AccountingObject = OwaContext.TryGetCurrentBudget();
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, useDirectorySearchRoot ? userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN : null, lcid, readOnly, consistencyMode, null, adsessionSettings, 3859, "CreateADRecipientSession", "f:\\15.00.1497\\sources\\dev\\clients\\src\\owa\\bin\\core\\Utilities.cs");
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x000886D8 File Offset: 0x000868D8
		internal static ADSessionSettings CreateScopedADSessionSettings(string domain)
		{
			ADSessionSettings result;
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
			{
				result = ADSessionSettings.FromTenantAcceptedDomain(domain);
			}
			else
			{
				result = ADSessionSettings.FromRootOrgScopeSet();
			}
			return result;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00088718 File Offset: 0x00086918
		internal static IConfigurationSession CreateConfigurationSessionScoped(bool readOnly, ConsistencyMode consistencyMode, ADObjectId adObjectId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(adObjectId);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(readOnly, consistencyMode, sessionSettings, 3905, "CreateConfigurationSessionScoped", "f:\\15.00.1497\\sources\\dev\\clients\\src\\owa\\bin\\core\\Utilities.cs");
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0008874C File Offset: 0x0008694C
		internal static ITopologyConfigurationSession CreateTopologyConfigurationSessionScopedToRootOrg(bool readOnly, ConsistencyMode consistencyMode)
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(readOnly, consistencyMode, ADSessionSettings.FromRootOrgScopeSet(), 3923, "CreateTopologyConfigurationSessionScopedToRootOrg", "f:\\15.00.1497\\sources\\dev\\clients\\src\\owa\\bin\\core\\Utilities.cs");
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0008877C File Offset: 0x0008697C
		internal static ITopologyConfigurationSession CreateADSystemConfigurationSessionScopedToFirstOrg(bool readOnly, ConsistencyMode consistencyMode)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), OrganizationId.ForestWideOrgId, null, false);
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(readOnly, consistencyMode, sessionSettings, 3959, "CreateADSystemConfigurationSessionScopedToFirstOrg", "f:\\15.00.1497\\sources\\dev\\clients\\src\\owa\\bin\\core\\Utilities.cs");
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x000887B8 File Offset: 0x000869B8
		internal static IConfigurationSession CreateADSystemConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, UserContext userContext)
		{
			ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(userContext.ExchangePrincipal.MailboxInfo.OrganizationId);
			adsessionSettings.AccountingObject = OwaContext.TryGetCurrentBudget();
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(readOnly, consistencyMode, adsessionSettings, 3982, "CreateADSystemConfigurationSession", "f:\\15.00.1497\\sources\\dev\\clients\\src\\owa\\bin\\core\\Utilities.cs");
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00088804 File Offset: 0x00086A04
		internal static IConfigurationSession CreateADSystemConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, OrganizationId organizationId)
		{
			ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
			adsessionSettings.AccountingObject = OwaContext.TryGetCurrentBudget();
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(readOnly, consistencyMode, adsessionSettings, 4004, "CreateADSystemConfigurationSession", "f:\\15.00.1497\\sources\\dev\\clients\\src\\owa\\bin\\core\\Utilities.cs");
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00088840 File Offset: 0x00086A40
		internal static OutboundConversionOptions CreateOutboundConversionOptions(UserContext userContext)
		{
			OutboundConversionOptions outboundConversionOptions = new OutboundConversionOptions(OwaConfigurationManager.Configuration.DefaultAcceptedDomain.DomainName.ToString());
			outboundConversionOptions.UserADSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, userContext);
			outboundConversionOptions.LoadPerOrganizationCharsetDetectionOptions(userContext.ExchangePrincipal.MailboxInfo.OrganizationId);
			return outboundConversionOptions;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0008888C File Offset: 0x00086A8C
		internal static InboundConversionOptions CreateInboundConversionOptions(UserContext userContext)
		{
			InboundConversionOptions inboundConversionOptions = new InboundConversionOptions(OwaConfigurationManager.Configuration.DefaultAcceptedDomain.DomainName.ToString());
			inboundConversionOptions.UserADSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, userContext);
			inboundConversionOptions.LoadPerOrganizationCharsetDetectionOptions(userContext.ExchangePrincipal.MailboxInfo.OrganizationId);
			return inboundConversionOptions;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x000888D7 File Offset: 0x00086AD7
		private static void DelayedRestartUponUnexecutableCode()
		{
			Thread.Sleep(90000);
			OwaDiagnostics.Logger.LogEvent(ClientsEventLogConstants.Tuple_OwaRestartingAfterFailedLoad, string.Empty, new object[0]);
			Environment.Exit(1);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00088904 File Offset: 0x00086B04
		public static string GetExtraWatsonData(OwaContext owaContext)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			StringBuilder stringBuilder = new StringBuilder();
			UserContext userContext = owaContext.TryGetUserContext();
			if (userContext != null)
			{
				TimeSpan timeSpan = TimeSpan.FromSeconds((double)((userContext.LastAccessedTime - userContext.SessionBeginTime) / Stopwatch.Frequency));
				stringBuilder.AppendLine();
				stringBuilder.Append("Session Length: ");
				stringBuilder.Append(timeSpan.ToString());
				stringBuilder.AppendLine();
				stringBuilder.Append("OWA Version: ");
				stringBuilder.Append(Globals.ApplicationVersion);
				stringBuilder.AppendLine();
				stringBuilder.Append("User Culture: ");
				if (Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture(owaContext) != null && Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture(owaContext).Name != null)
				{
					stringBuilder.Append(Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture(owaContext).Name);
				}
				else
				{
					stringBuilder.Append("Not Found");
				}
				string tcmidvalue = Utilities.GetTCMIDValue(owaContext);
				if (!string.IsNullOrEmpty(tcmidvalue))
				{
					stringBuilder.AppendLine();
					stringBuilder.Append("TestCaseID: ");
					stringBuilder.Append(tcmidvalue);
				}
				if (!Globals.DisableBreadcrumbs)
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
					stringBuilder.AppendLine(userContext.DumpBreadcrumbs());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00088A2C File Offset: 0x00086C2C
		internal static void WriteErrorToWebPart(OwaContext owaContext)
		{
			if (owaContext.ErrorInformation == null)
			{
				throw new OwaInvalidOperationException("owaContext.ErrorInformation may not be null");
			}
			HttpContext httpContext = owaContext.HttpContext;
			ExTraceGlobals.WebPartRequestTracer.TraceDebug<OwaEventHandlerErrorCode, string>(0L, "Invalid web part request: Type: {0} Error: {1}.", owaContext.ErrorInformation.OwaEventHandlerErrorCode, owaContext.ErrorInformation.Message);
			Utilities.MakePageNoCacheNoStore(httpContext.Response);
			StringBuilder stringBuilder = new StringBuilder();
			StringWriter stringWriter = new StringWriter(stringBuilder);
			WebPartUtilities.RenderError(owaContext, stringWriter);
			stringWriter.Close();
			httpContext.Response.Clear();
			httpContext.Response.Write(stringBuilder);
			httpContext.Response.StatusCode = 200;
			httpContext.Response.AppendHeader("Content-Length", httpContext.Response.Output.Encoding.GetByteCount(stringBuilder.ToString()).ToString());
			try
			{
				httpContext.Response.Flush();
				httpContext.ApplicationInstance.CompleteRequest();
			}
			catch (HttpException arg)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<HttpException>(0L, "Failed to flush and send response to client. {0}", arg);
			}
			httpContext.Response.End();
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00088B44 File Offset: 0x00086D44
		internal static bool IsWebPartRequest(OwaContext owaContext)
		{
			return owaContext.RequestType == OwaRequestType.WebPart || (owaContext.SessionContext != null && owaContext.SessionContext.IsWebPartRequest);
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00088B68 File Offset: 0x00086D68
		internal static bool IsWebPartDelegateAccessRequest(OwaContext owaContext)
		{
			return Utilities.IsWebPartRequest(owaContext) && owaContext.UserContext.IsExplicitLogonOthersMailbox;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00088B7F File Offset: 0x00086D7F
		internal static ErrorInformation GetExceptionHandlingInformation(Exception exception, OwaIdentity mailboxIdentity)
		{
			return Utilities.GetExceptionHandlingInformation(exception, mailboxIdentity, false);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00088B89 File Offset: 0x00086D89
		internal static ErrorInformation GetExceptionHandlingInformation(Exception exception, OwaIdentity mailboxIdentity, bool isWebPartRequest)
		{
			return Utilities.GetExceptionHandlingInformation(exception, mailboxIdentity, false, null, null, false);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00088B96 File Offset: 0x00086D96
		internal static ErrorInformation GetExceptionHandlingInformation(Exception exception, OwaIdentity mailboxIdentity, bool isWebPartRequest, string previousPageUrl, string externalPageLink, bool isBasicAuthentication)
		{
			return Utilities.GetExceptionHandlingInformation(exception, mailboxIdentity, isWebPartRequest, previousPageUrl, externalPageLink, isBasicAuthentication, null, false);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00088BA8 File Offset: 0x00086DA8
		internal static ErrorInformation GetExceptionHandlingInformation(Exception exception, OwaIdentity mailboxIdentity, bool isWebPartRequest, string previousPageUrl, string externalPageLink, bool isBasicAuthentication, OwaContext owaContext, bool showErrorInDialogForOeh)
		{
			string message = null;
			string messageDetails = null;
			bool hideDebugInformation = false;
			bool sendWatsonReport = false;
			bool isErrorMessageHtmlEncoded = false;
			bool isDetailedErrorHtmlEncoded = false;
			OwaEventHandlerErrorCode owaEventHandlerErrorCode = OwaEventHandlerErrorCode.NotSet;
			ThemeFileId icon = ThemeFileId.Error;
			ThemeFileId background = ThemeFileId.None;
			OwaUrl owaUrl = OwaUrl.ErrorPage;
			UserContext userContext = (owaContext != null) ? owaContext.TryGetUserContext() : null;
			ObjectNotFoundException ex = exception as ObjectNotFoundException;
			OwaEventHandlerException ex2 = exception as OwaEventHandlerException;
			if (ex2 != null)
			{
				message = ex2.Description;
				owaEventHandlerErrorCode = ex2.ErrorCode;
				hideDebugInformation = (ex2.HideDebugInformation || ex2.ErrorCode == OwaEventHandlerErrorCode.ConflictResolution);
			}
			else if (exception is OwaNotSupportedException)
			{
				message = exception.Message;
				hideDebugInformation = true;
			}
			else if (exception is OwaClientNotSupportedException)
			{
				message = LocalizedStrings.GetNonEncoded(427734258);
				hideDebugInformation = true;
				sendWatsonReport = false;
			}
			else if (exception is OwaExistentNotificationPipeException)
			{
				message = LocalizedStrings.GetNonEncoded(1295605912);
				sendWatsonReport = false;
				hideDebugInformation = !Globals.SendClientWatsonReports;
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.ExistentNotificationPipeError;
			}
			else if (exception is OwaNotificationPipeException)
			{
				message = LocalizedStrings.GetNonEncoded(-771052428);
				sendWatsonReport = false;
			}
			else if (exception is OwaOperationNotSupportedException)
			{
				message = exception.Message;
				hideDebugInformation = true;
			}
			else if (exception is OwaADObjectNotFoundException)
			{
				if (userContext != null)
				{
					userContext.PreferredDC = string.Empty;
				}
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(-950823100);
				messageDetails = LocalizedStrings.GetNonEncoded(970481710);
			}
			else if (exception is OwaInvalidCanary14Exception || exception is OwaCanaryException)
			{
				owaContext.HttpContext.Response.AppendToLog(Utilities.FormatExceptionNameAndMessage("&exception=", exception));
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(222445511);
				messageDetails = LocalizedStrings.GetNonEncoded(-1291627735);
				externalPageLink = null;
				OwaInvalidCanary14Exception ex3 = exception as OwaInvalidCanary14Exception;
				if (ex3 != null)
				{
					UserContextCookie userContextCookie = ex3.UserContextCookie;
					if (userContextCookie != null)
					{
						Utilities.DeleteCookie(owaContext.HttpContext.Response, userContextCookie.CookieName);
						UserContextCookie userContextCookie2 = userContextCookie.CloneWithNewCanary();
						owaContext.HttpContext.Response.Cookies.Set(userContextCookie2.HttpCookie);
					}
				}
			}
			else if (exception is OwaLockTimeoutException)
			{
				hideDebugInformation = true;
				OwaSingleCounters.RequestTimeouts.Increment();
				message = LocalizedStrings.GetNonEncoded(-116001901);
				if (owaContext != null)
				{
					owaContext.HttpContext.Response.AppendToLog("&s=ReqTimeout");
				}
			}
			else if (isWebPartRequest && ex != null)
			{
				sendWatsonReport = false;
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(1622692336);
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.WebPartActionPermissionsError;
			}
			else if (exception is OwaLostContextException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(-477257421);
			}
			else if (ex != null || exception is ObjectNotFoundException)
			{
				if (exception is ObjectNotFoundException)
				{
					icon = ThemeFileId.Warning;
					hideDebugInformation = true;
				}
				if (exception.InnerException != null && exception.InnerException is DataValidationException)
				{
					message = LocalizedStrings.GetNonEncoded(404614840);
				}
				else
				{
					hideDebugInformation = true;
					message = LocalizedStrings.GetNonEncoded(-289549140);
					messageDetails = LocalizedStrings.GetNonEncoded(-1807976350);
				}
			}
			else if (exception is OwaBodyConversionFailedException)
			{
				message = LocalizedStrings.GetNonEncoded(1825027020);
			}
			else if (exception is ObjectExistedException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(-1399945920);
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.FolderNameExists;
			}
			else if (exception is OwaDelegatorMailboxFailoverException)
			{
				sendWatsonReport = false;
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(1005365831);
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.MailboxFailoverWithoutRedirection;
			}
			else if (exception is OwaArchiveInTransitException)
			{
				sendWatsonReport = false;
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(-1086762792);
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.MailboxInTransitError;
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ArchiveMailboxAccessFailedWarning, string.Empty, new object[]
				{
					mailboxIdentity.SafeGetRenderableName(),
					exception.ToString()
				});
			}
			else if (exception is OwaArchiveNotAvailableException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(604008388);
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ArchiveMailboxAccessFailedWarning, string.Empty, new object[]
				{
					mailboxIdentity.SafeGetRenderableName(),
					exception.ToString()
				});
			}
			else if (exception is MailboxInSiteFailoverException)
			{
				sendWatsonReport = false;
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(26604436);
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.MailboxFailoverWithoutRedirection;
			}
			else if (exception is MailboxCrossSiteFailoverException || exception is WrongServerException)
			{
				sendWatsonReport = false;
				hideDebugInformation = true;
				ExTraceGlobals.CoreTracer.TraceDebug(0L, "No CAS server is available for redirection or proxy");
				message = LocalizedStrings.GetNonEncoded(26604436);
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.MailboxFailoverWithoutRedirection;
			}
			else if (exception is MailboxInTransitException)
			{
				sendWatsonReport = false;
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(-1739093686);
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.MailboxInTransitError;
			}
			else if (exception is ResourceUnhealthyException)
			{
				sendWatsonReport = false;
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(198161982);
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ErrorResourceUnhealthy, string.Empty, new object[]
				{
					mailboxIdentity.SafeGetRenderableName(),
					exception.ToString()
				});
			}
			else if (exception is ConnectionFailedPermanentException || exception is ServerNotFoundException)
			{
				message = string.Format(LocalizedStrings.GetNonEncoded(-765910865), mailboxIdentity.SafeGetRenderableName());
			}
			else if (exception is ConnectionFailedTransientException || exception is MailboxOfflineException)
			{
				message = LocalizedStrings.GetNonEncoded(198161982);
				hideDebugInformation = true;
				Utilities.RegisterMailboxException(userContext, exception);
			}
			else if (exception is InvalidLicenseException)
			{
				message = string.Format(LocalizedStrings.GetNonEncoded(468041898), mailboxIdentity.SafeGetRenderableName());
				hideDebugInformation = true;
				sendWatsonReport = false;
			}
			else if (exception is InstantMessagingException)
			{
				if ((exception as InstantMessagingException).Code == 18204)
				{
					message = LocalizedStrings.GetNonEncoded(-374220215);
					OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_FailedToEstablishIMConnection, string.Empty, new object[]
					{
						(exception.Message != null) ? exception.Message : string.Empty
					});
					sendWatsonReport = false;
					hideDebugInformation = true;
				}
				else
				{
					sendWatsonReport = true;
					message = LocalizedStrings.GetNonEncoded(641346049);
					owaEventHandlerErrorCode = OwaEventHandlerErrorCode.UnexpectedError;
				}
			}
			else if (exception is SendAsDeniedException)
			{
				message = LocalizedStrings.GetNonEncoded(2059222100);
				hideDebugInformation = true;
			}
			else if (exception is ADTransientException)
			{
				if (userContext != null)
				{
					userContext.PreferredDC = string.Empty;
				}
				message = LocalizedStrings.GetNonEncoded(634294555);
			}
			else if (exception is ADOperationException)
			{
				if (userContext != null)
				{
					userContext.PreferredDC = string.Empty;
				}
				message = LocalizedStrings.GetNonEncoded(-256207770);
			}
			else if (exception is DataValidationException)
			{
				if (userContext != null)
				{
					userContext.PreferredDC = string.Empty;
				}
				message = LocalizedStrings.GetNonEncoded(-256207770);
			}
			else if (exception is InvalidObjectOperationException)
			{
				if (userContext != null)
				{
					userContext.PreferredDC = string.Empty;
				}
				Exception innerException = exception.InnerException;
				string text = string.Empty;
				if (innerException != null)
				{
					text = innerException.GetType().ToString() + ": " + innerException.ToString();
				}
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_MailboxServerVersionConfiguration, mailboxIdentity.SafeGetRenderableName(), new object[]
				{
					text
				});
				message = LocalizedStrings.GetNonEncoded(578437863);
			}
			else if (exception is SaveConflictException || exception is OwaSaveConflictException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(-482397486);
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.ConflictResolution;
			}
			else if (exception is FolderSaveException)
			{
				message = LocalizedStrings.GetNonEncoded(1487149567);
			}
			else if (exception is RecurrenceFormatException)
			{
				message = LocalizedStrings.GetNonEncoded(2014226498);
			}
			else if (exception is ObjectValidationException)
			{
				message = LocalizedStrings.GetNonEncoded(-1670564952);
			}
			else if (exception is CorruptDataException)
			{
				if (exception is PropertyValidationException && Utilities.CheckForDLMemberSizeTooLargeConstraint(((PropertyValidationException)exception).PropertyValidationErrors))
				{
					message = LocalizedStrings.GetNonEncoded(1763264010);
					hideDebugInformation = true;
				}
				else
				{
					message = LocalizedStrings.GetNonEncoded(-1670564952);
				}
			}
			else if (exception is InvalidSharingMessageException || exception is InvalidSharingDataException || exception is InvalidExternalSharingInitiatorException)
			{
				sendWatsonReport = false;
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(-1805619908);
			}
			else if (exception is OccurrenceCrossingBoundaryException)
			{
				message = LocalizedStrings.GetNonEncoded(-921576274);
				hideDebugInformation = true;
			}
			else if (exception is OccurrenceTimeSpanTooBigException)
			{
				message = LocalizedStrings.GetNonEncoded(466060253);
				hideDebugInformation = true;
			}
			else if (exception is ParserException)
			{
				message = LocalizedStrings.GetNonEncoded(1991715079);
				hideDebugInformation = true;
			}
			else if (exception is RecurrenceEndDateTooBigException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(-1642530753);
			}
			else if (exception is RecurrenceStartDateTooSmallException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(-323965365);
			}
			else if (exception is RecurrenceHasNoOccurrenceException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(1564162812);
			}
			else if (exception is QuotaExceededException || exception is MessageTooBigException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(-640701623);
			}
			else if (exception is SubmissionQuotaExceededException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(178029729);
			}
			else if (exception is MessageSubmissionExceededException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(-1381793955);
			}
			else if (exception is AttachmentExceededException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(-2137146650);
			}
			else if (exception is ResourcesException || exception is NoMoreConnectionsException)
			{
				message = LocalizedStrings.GetNonEncoded(-639453714);
			}
			else if (exception is AccountDisabledException)
			{
				message = LocalizedStrings.GetNonEncoded(531497785);
			}
			else if (isWebPartRequest && exception is OwaDefaultFolderIdUnavailableException)
			{
				sendWatsonReport = false;
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(1622692336);
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.WebPartActionPermissionsError;
			}
			else if (exception is OwaAccessDeniedException)
			{
				sendWatsonReport = false;
				hideDebugInformation = true;
				message = exception.Message;
				if (((OwaAccessDeniedException)exception).IsWebPartFailure)
				{
					owaEventHandlerErrorCode = OwaEventHandlerErrorCode.WebPartActionPermissionsError;
				}
			}
			else if (exception is AccessDeniedException)
			{
				message = LocalizedStrings.GetNonEncoded(995407892);
				if (isWebPartRequest)
				{
					sendWatsonReport = false;
					message = LocalizedStrings.GetNonEncoded(1622692336);
					owaEventHandlerErrorCode = OwaEventHandlerErrorCode.WebPartActionPermissionsError;
				}
				else
				{
					AccessDeniedException ex4 = (AccessDeniedException)exception;
					if (ex4.InnerException != null)
					{
						Exception innerException2 = ex4.InnerException;
						if (innerException2 is MapiExceptionPasswordChangeRequired || innerException2 is MapiExceptionPasswordExpired)
						{
							message = LocalizedStrings.GetNonEncoded(540943741);
						}
					}
				}
				hideDebugInformation = true;
			}
			else if (isWebPartRequest && exception is ArgumentNullException)
			{
				sendWatsonReport = false;
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(1622692336);
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.WebPartActionPermissionsError;
			}
			else if (isWebPartRequest && exception is StoragePermanentException)
			{
				sendWatsonReport = false;
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(1622692336);
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.WebPartActionPermissionsError;
			}
			else if (exception is PropertyErrorException)
			{
				message = LocalizedStrings.GetNonEncoded(641346049);
			}
			else if (exception is OwaInstantMessageEventHandlerTransientException)
			{
				message = LocalizedStrings.GetNonEncoded(-1611030258);
				sendWatsonReport = false;
				hideDebugInformation = true;
			}
			else if (exception is OwaUserNotIMEnabledException)
			{
				message = exception.Message;
				sendWatsonReport = false;
				hideDebugInformation = true;
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.UserNotIMEnabled;
			}
			else if (exception is OwaIMOperationNotAllowedToSelf)
			{
				message = exception.Message;
				sendWatsonReport = false;
				hideDebugInformation = true;
				owaEventHandlerErrorCode = OwaEventHandlerErrorCode.IMOperationNotAllowedToSelf;
			}
			else if (exception is OwaInvalidOperationException)
			{
				message = LocalizedStrings.GetNonEncoded(641346049);
			}
			else if (exception is CorruptDataException)
			{
				icon = ThemeFileId.Warning;
				message = LocalizedStrings.GetNonEncoded(-1670564952);
			}
			else if (exception is AccessDeniedException)
			{
				icon = ThemeFileId.Warning;
				if (!isBasicAuthentication)
				{
					message = LocalizedStrings.GetNonEncoded(-1177184444);
				}
				else
				{
					message = LocalizedStrings.GetNonEncoded(234621291);
				}
				hideDebugInformation = true;
			}
			else if (exception is ConnectionException)
			{
				icon = ThemeFileId.Warning;
				message = LocalizedStrings.GetNonEncoded(678272416);
				hideDebugInformation = true;
			}
			else if (exception is PropertyErrorException)
			{
				icon = ThemeFileId.Warning;
				message = LocalizedStrings.GetNonEncoded(-566073559);
				hideDebugInformation = true;
			}
			else if (exception is PathTooLongException)
			{
				icon = ThemeFileId.Warning;
				message = LocalizedStrings.GetNonEncoded(-785304559);
				hideDebugInformation = true;
			}
			else if (exception is UnknownErrorException || exception is DocumentLibraryException)
			{
				icon = ThemeFileId.Warning;
				message = LocalizedStrings.GetNonEncoded(-785304559);
				hideDebugInformation = true;
			}
			else if (exception is OwaChangePasswordTransientException)
			{
				message = Strings.ChangePasswordTransientError;
				messageDetails = exception.Message;
				hideDebugInformation = true;
			}
			else if (exception is OwaSpellCheckerException)
			{
				message = LocalizedStrings.GetNonEncoded(1615042268);
			}
			else if (exception is VirusDetectedException)
			{
				message = LocalizedStrings.GetNonEncoded(-589723291);
			}
			else if (exception is VirusScanInProgressException)
			{
				message = LocalizedStrings.GetNonEncoded(-1019777596);
			}
			else if (exception is VirusMessageDeletedException)
			{
				message = LocalizedStrings.GetNonEncoded(1164605313);
			}
			else if (exception is OwaProxyException)
			{
				OwaProxyException ex5 = (OwaProxyException)exception;
				message = ex5.LocalizedError;
				hideDebugInformation = ex5.HideDebugInformation;
			}
			else if (exception is OwaExplicitLogonException)
			{
				OwaExplicitLogonException ex6 = (OwaExplicitLogonException)exception;
				icon = ThemeFileId.Warning;
				message = ex6.LocalizedError;
				hideDebugInformation = true;
			}
			else if (exception is OwaInvalidWebPartRequestException)
			{
				sendWatsonReport = false;
				hideDebugInformation = true;
			}
			else if (exception is OwaNoReplicaOfCurrentServerVersionException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(-448460673);
			}
			else if (exception is OwaNoReplicaException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(1179266056);
			}
			else if (exception is TranscodingServerBusyException)
			{
				message = LocalizedStrings.GetNonEncoded(162094648);
			}
			else if (exception is TranscodingUnconvertibleFileException)
			{
				message = LocalizedStrings.GetNonEncoded(794771794);
			}
			else if (exception is TranscodingFatalFaultException)
			{
				message = LocalizedStrings.GetNonEncoded(-211811108);
			}
			else if (exception is TranscodingOverMaximumFileSizeException)
			{
				message = LocalizedStrings.GetNonEncoded(-148502085);
			}
			else if (exception is TranscodingTimeoutException)
			{
				message = LocalizedStrings.GetNonEncoded(1972219525);
			}
			else if (exception is TranscodingErrorFileException)
			{
				message = LocalizedStrings.GetNonEncoded(-437471318);
			}
			else if (exception is NoReplicaException)
			{
				hideDebugInformation = true;
				message = LocalizedStrings.GetNonEncoded(1179266056);
			}
			else
			{
				if (exception is StorageTransientException)
				{
					message = LocalizedStrings.GetNonEncoded(-238819799);
					Utilities.RegisterMailboxException(userContext, exception);
					hideDebugInformation = !Globals.SendClientWatsonReports;
					if (!(exception.InnerException is MapiExceptionRpcServerTooBusy))
					{
						goto IL_118F;
					}
					sendWatsonReport = false;
					string text2 = string.Empty;
					try
					{
						try
						{
							if (userContext != null && userContext.HasValidMailboxSession())
							{
								text2 = userContext.MailboxSession.MailboxOwner.MailboxInfo.Location.ServerFqdn;
							}
						}
						catch (StorageTransientException)
						{
						}
						catch (StoragePermanentException)
						{
						}
						goto IL_118F;
					}
					finally
					{
						OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_ErrorMailboxServerTooBusy, string.Empty, new object[]
						{
							text2,
							mailboxIdentity.SafeGetRenderableName(),
							exception.ToString()
						});
					}
				}
				if (exception is RulesTooBigException)
				{
					message = LocalizedStrings.GetNonEncoded(-791981113);
					hideDebugInformation = true;
				}
				else if (exception is DuplicateActionException)
				{
					hideDebugInformation = true;
					message = LocalizedStrings.GetNonEncoded(-555068615);
				}
				else if (exception is ConversionFailedException && ((ConversionFailedException)exception).ConversionFailureReason == ConversionFailureReason.CorruptContent)
				{
					hideDebugInformation = true;
					message = LocalizedStrings.GetNonEncoded(-1670564952);
				}
				else if (exception is RightsManagementPermanentException)
				{
					sendWatsonReport = false;
					hideDebugInformation = true;
					RightsManagementPermanentException ex7 = exception as RightsManagementPermanentException;
					RightsManagementFailureCode failureCode = ex7.FailureCode;
					if (failureCode != RightsManagementFailureCode.UserRightNotGranted)
					{
						switch (failureCode)
						{
						case RightsManagementFailureCode.InternalLicensingDisabled:
							break;
						case RightsManagementFailureCode.ExternalLicensingDisabled:
							message = string.Format(LocalizedStrings.GetNonEncoded(1397740097), string.Empty);
							goto IL_118F;
						default:
							switch (failureCode)
							{
							case RightsManagementFailureCode.ServerRightNotGranted:
								message = LocalizedStrings.GetNonEncoded(784482022);
								goto IL_118F;
							case RightsManagementFailureCode.FeatureDisabled:
								goto IL_E04;
							}
							hideDebugInformation = false;
							message = exception.Message;
							goto IL_118F;
						}
						IL_E04:
						message = string.Format(LocalizedStrings.GetNonEncoded(1049269714), string.Empty);
					}
					else
					{
						message = LocalizedStrings.GetNonEncoded(1508237301);
					}
				}
				else if (exception is IOException && Utilities.IsDiskFullException(exception))
				{
					hideDebugInformation = true;
					sendWatsonReport = false;
					message = LocalizedStrings.GetNonEncoded(-1729839551);
				}
				else if (exception is StoragePermanentException)
				{
					message = LocalizedStrings.GetNonEncoded(861904327);
				}
				else if (exception is TransientException)
				{
					message = LocalizedStrings.GetNonEncoded(-1729839551);
				}
				else if (exception is HttpException)
				{
					HttpException ex8 = (HttpException)exception;
					message = string.Format(LocalizedStrings.GetNonEncoded(1331629462), ex8.GetHttpCode());
				}
				else if (exception is OwaInvalidConfigurationException)
				{
					hideDebugInformation = true;
					message = exception.Message;
				}
				else if ((exception is OwaAsyncOperationException && exception.InnerException != null && exception.InnerException is OwaAsyncRequestTimeoutException) || exception is OwaAsyncRequestTimeoutException)
				{
					hideDebugInformation = true;
					message = LocalizedStrings.GetNonEncoded(-793045165);
				}
				else if (exception is OwaNeedsSMimeControlToEditDraftException)
				{
					message = exception.Message;
					hideDebugInformation = true;
				}
				else if (exception is OwaCannotEditIrmDraftException)
				{
					message = exception.Message;
					hideDebugInformation = true;
				}
				else if (exception is OverBudgetException)
				{
					sendWatsonReport = false;
					hideDebugInformation = true;
					if (Utilities.IsOehOrSubPageContentRequest(OwaContext.Current))
					{
						message = LocalizedStrings.GetNonEncoded(1856724252);
					}
					else
					{
						message = LocalizedStrings.GetNonEncoded(-1416371944);
						messageDetails = LocalizedStrings.GetNonEncoded(1856724252);
					}
				}
				else if (exception is OwaBrowserUpdateRequiredException)
				{
					sendWatsonReport = false;
					hideDebugInformation = true;
					icon = ThemeFileId.WarningIcon;
					background = ThemeFileId.ErrorBackground;
					message = LocalizedStrings.GetNonEncoded(-1348879678);
					if (!Utilities.IsOehOrSubPageContentRequest(OwaContext.Current))
					{
						messageDetails = ((OwaBrowserUpdateRequiredException)exception).GetErrorDetails();
					}
					isDetailedErrorHtmlEncoded = true;
					owaUrl = OwaUrl.Error2Page;
				}
				else if (exception is OwaDisabledException)
				{
					sendWatsonReport = false;
					hideDebugInformation = true;
					icon = ThemeFileId.WarningIcon;
					background = ThemeFileId.ErrorBackground;
					message = LocalizedStrings.GetNonEncoded(1028401106);
					messageDetails = LocalizedStrings.GetNonEncoded(1613045632);
					isDetailedErrorHtmlEncoded = true;
					owaUrl = OwaUrl.Error2Page;
				}
				else if (exception is OwaLightDisabledException)
				{
					sendWatsonReport = false;
					hideDebugInformation = true;
					icon = ThemeFileId.WarningIcon;
					background = ThemeFileId.ErrorBackground;
					message = LocalizedStrings.GetNonEncoded(1028401106);
					messageDetails = LocalizedStrings.GetNonEncoded(-1048443402);
					isDetailedErrorHtmlEncoded = true;
					owaUrl = OwaUrl.Error2Page;
				}
				else if (exception is COMException || exception.InnerException is COMException)
				{
					sendWatsonReport = !Utilities.ShouldIgnoreException((exception is COMException) ? exception : exception.InnerException);
					hideDebugInformation = true;
					message = LocalizedStrings.GetNonEncoded(641346049);
					owaEventHandlerErrorCode = OwaEventHandlerErrorCode.UnexpectedError;
				}
				else if (exception is OwaSharedFromOlderVersionException)
				{
					sendWatsonReport = false;
					hideDebugInformation = true;
					message = LocalizedStrings.GetHtmlEncoded(1354015881);
				}
				else if (exception is OwaRespondOlderVersionMeetingException)
				{
					sendWatsonReport = false;
					hideDebugInformation = true;
					message = string.Format(CultureInfo.CurrentCulture, LocalizedStrings.GetNonEncoded(1896884103), new object[]
					{
						((OwaRespondOlderVersionMeetingException)exception).SharerDisplayName
					});
				}
				else if (exception is ThreadAbortException)
				{
					sendWatsonReport = false;
					message = LocalizedStrings.GetHtmlEncoded(641346049);
				}
				else if (exception is OwaCreateClientSecurityContextFailedException)
				{
					sendWatsonReport = false;
					hideDebugInformation = true;
					message = LocalizedStrings.GetHtmlEncoded(484783375);
				}
				else if (exception is OwaUnsupportedConversationItemException)
				{
					sendWatsonReport = false;
					hideDebugInformation = true;
					message = LocalizedStrings.GetHtmlEncoded(-1147215991);
				}
				else if (exception is OwaURLIsOutOfDateException)
				{
					sendWatsonReport = false;
					hideDebugInformation = true;
					message = string.Format(LocalizedStrings.GetNonEncoded(516417563), OwaContext.Current.LocalHostName);
					messageDetails = LocalizedStrings.GetNonEncoded(-1085493500);
				}
				else if (exception is WrongCASServerBecauseOfOutOfDateDNSCacheException)
				{
					sendWatsonReport = false;
					hideDebugInformation = true;
					message = LocalizedStrings.GetNonEncoded(-23402676);
				}
				else
				{
					sendWatsonReport = true;
					message = LocalizedStrings.GetNonEncoded(641346049);
					owaEventHandlerErrorCode = OwaEventHandlerErrorCode.UnexpectedError;
				}
			}
			IL_118F:
			return new ErrorInformation
			{
				Exception = exception,
				Message = message,
				MessageDetails = messageDetails,
				OwaEventHandlerErrorCode = owaEventHandlerErrorCode,
				HideDebugInformation = hideDebugInformation,
				IsErrorMessageHtmlEncoded = isErrorMessageHtmlEncoded,
				IsDetailedErrorHtmlEncoded = isDetailedErrorHtmlEncoded,
				SendWatsonReport = sendWatsonReport,
				Icon = icon,
				Background = background,
				OwaUrl = owaUrl,
				PreviousPageUrl = previousPageUrl,
				ExternalPageLink = externalPageLink
			};
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00089DE4 File Offset: 0x00087FE4
		public static void RenderDebugInformation(TextWriter writer, OwaContext owaContext, Exception exception)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			HttpRequest request = owaContext.HttpContext.Request;
			Utilities.RenderDebugHeader(writer, "Request");
			Utilities.RenderDebugInformation(writer, "requestUrl", "Url", request.Url.OriginalString);
			Utilities.RenderDebugInformation(writer, "userHostAddress", "User host address", request.UserHostAddress);
			if (owaContext.ExchangePrincipal != null)
			{
				Utilities.RenderDebugInformation(writer, "userName", "User", owaContext.ExchangePrincipal.MailboxInfo.DisplayName);
				Utilities.RenderDebugInformation(writer, "exAddress", "EX Address", owaContext.ExchangePrincipal.LegacyDn);
				Utilities.RenderDebugInformation(writer, "smtpAddress", "SMTP Address", owaContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
			}
			Utilities.RenderDebugInformation(writer, "owaVersion", "OWA version", Globals.ApplicationVersion);
			if (owaContext.TryGetUserContext() != null && owaContext.UserContext.IsSafeToAccessFromCurrentThread())
			{
				UserContext userContext = owaContext.UserContext;
				if (!userContext.IsProxy)
				{
					if (!userContext.HasValidMailboxSession())
					{
						goto IL_16C;
					}
					try
					{
						Utilities.RenderDebugInformation(writer, "mailboxServer", "Mailbox server", userContext.MailboxSession.MailboxOwner.MailboxInfo.Location.ServerFqdn);
						goto IL_16C;
					}
					catch (StorageTransientException)
					{
						goto IL_16C;
					}
					catch (StoragePermanentException)
					{
						goto IL_16C;
					}
				}
				if (owaContext.SecondCasUri != null)
				{
					Utilities.RenderDebugInformation(writer, "secondCAS", "Second CAS for proxy", owaContext.SecondCasUri.ToString());
				}
			}
			IL_16C:
			if (exception != null)
			{
				Utilities.RenderDebugHeader(writer, "Exception");
				Utilities.RenderExceptionInformation(writer, exception);
				Exception innerException = exception.InnerException;
				int num = 0;
				while (innerException != null && num < 4)
				{
					Utilities.RenderDebugHeader(writer, "Inner Exception");
					Utilities.RenderExceptionInformation(writer, innerException);
					innerException = innerException.InnerException;
					num++;
				}
			}
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00089FC0 File Offset: 0x000881C0
		private static void RenderExceptionInformation(TextWriter writer, Exception exception)
		{
			Utilities.RenderDebugInformation(writer, "exceptionType", "Exception type", exception.GetType().ToString());
			Utilities.RenderDebugInformation(writer, "exceptionMessage", "Exception message", exception.Message);
			Utilities.RenderDebugHeader(writer, "Call stack");
			if (exception.StackTrace == null)
			{
				writer.Write("<div><i>No callstack available</i></div>");
				return;
			}
			writer.Write("<div id=exceptionCallStack>");
			string text = " at ";
			string stackTrace = exception.StackTrace;
			int num = stackTrace.IndexOf(text, StringComparison.Ordinal);
			if (num == -1)
			{
				writer.Write(stackTrace);
			}
			else
			{
				num += text.Length;
				for (;;)
				{
					int num2 = stackTrace.IndexOf(text, num, StringComparison.Ordinal);
					if (num2 == -1)
					{
						break;
					}
					writer.Write("<div nowrap>");
					writer.Write(stackTrace.Substring(num, num2 - num));
					writer.Write("</div>");
					num = num2 + text.Length;
					if (num >= stackTrace.Length)
					{
						goto IL_DC;
					}
				}
				writer.Write(stackTrace.Substring(num));
			}
			IL_DC:
			writer.Write("</div>");
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0008A0B4 File Offset: 0x000882B4
		private static void RenderDebugInformation(TextWriter writer, string id, string label, string value)
		{
			writer.Write(string.Format("{0}: <span id={1}>{2}</span><br>", label, id, Utilities.HtmlEncode(value)));
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x0008A0CE File Offset: 0x000882CE
		private static void RenderDebugHeader(TextWriter writer, string label)
		{
			writer.Write(string.Format("<br><b>{0}</b><br>", label));
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0008A0E1 File Offset: 0x000882E1
		internal static string GetItemIdString(StoreObjectId itemId, Folder containerFolder)
		{
			return OwaStoreObjectId.CreateFromItemId(itemId, containerFolder).ToString();
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0008A0EF File Offset: 0x000882EF
		internal static string GetItemIdString(StoreObjectId itemId, OwaStoreObjectId relatedOwaStoreObjectId)
		{
			return OwaStoreObjectId.CreateFromStoreObjectId(itemId, relatedOwaStoreObjectId).ToString();
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0008A0FD File Offset: 0x000882FD
		internal static string GetIdAsString(StoreObject storeObject)
		{
			return OwaStoreObjectId.CreateFromStoreObject(storeObject).ToString();
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0008A10A File Offset: 0x0008830A
		internal static T GetItemForRequest<T>(OwaContext owaContext, out Item parentItem, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return Utilities.GetItemForRequest<T>(owaContext, out parentItem, false, prefetchProperties);
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0008A115 File Offset: 0x00088315
		internal static T GetItemForRequest<T>(OwaContext owaContext, out Item parentItem, bool forceAsMessageItem, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return Utilities.GetItemById<T>(owaContext, out parentItem, Utilities.GetQueryStringParameter(owaContext.HttpContext.Request, "id"), forceAsMessageItem, prefetchProperties);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0008A138 File Offset: 0x00088338
		internal static T GetItemById<T>(OwaContext owaContext, out Item parentItem, string owaStoreObjectId, bool forceAsMessageItem, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			if (forceAsMessageItem && typeof(T).Name != "MessageItem")
			{
				throw new ArgumentException("To force bind as a MessageItem, the typename T should be set to MessageItem.");
			}
			HttpContext httpContext = owaContext.HttpContext;
			UserContext userContext = owaContext.UserContext;
			bool flag = Utilities.GetQueryStringParameter(httpContext.Request, "attcnt", false) != null;
			Item item = null;
			parentItem = null;
			Attachment attachment = null;
			ItemAttachment itemAttachment = null;
			if (flag)
			{
				try
				{
					parentItem = Utilities.GetItem<Item>(userContext, owaStoreObjectId, prefetchProperties);
					if (userContext.IsIrmEnabled && !userContext.IsBasicExperience)
					{
						Utilities.IrmDecryptIfRestricted(parentItem, userContext, true);
					}
					attachment = Utilities.GetAttachment(parentItem, httpContext.Request, userContext);
					itemAttachment = (attachment as ItemAttachment);
					if (itemAttachment == null)
					{
						throw new OwaInvalidRequestException("Attachment is not an item attachment");
					}
					attachment = null;
					if (forceAsMessageItem)
					{
						item = itemAttachment.GetItemAsMessage(prefetchProperties);
					}
					else
					{
						item = itemAttachment.GetItem(prefetchProperties);
					}
					if (!(item is T))
					{
						throw new OwaInvalidRequestException("Wrong item class supplied");
					}
					OwaContext.Current.AddObjectToDisposeOnEndRequest(itemAttachment);
					goto IL_14C;
				}
				catch
				{
					if (parentItem != null)
					{
						parentItem.Dispose();
						parentItem = null;
					}
					if (item != null)
					{
						item.Dispose();
						item = null;
					}
					if (attachment != null)
					{
						attachment.Dispose();
						attachment = null;
					}
					if (itemAttachment != null)
					{
						itemAttachment.Dispose();
						itemAttachment = null;
					}
					throw;
				}
			}
			parentItem = null;
			OwaStoreObjectId owaStoreObjectId2 = OwaStoreObjectId.CreateFromString(owaStoreObjectId);
			item = Utilities.GetItem<T>(userContext, owaStoreObjectId2, forceAsMessageItem, prefetchProperties);
			IL_14C:
			return (T)((object)item);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x0008A2A8 File Offset: 0x000884A8
		internal static T GetItem<T>(UserContext userContext, string idString, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			if (string.IsNullOrEmpty(idString))
			{
				throw new ArgumentNullException("idString");
			}
			OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromString(idString);
			return Utilities.GetItem<T>(userContext, owaStoreObjectId, prefetchProperties);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x0008A2D8 File Offset: 0x000884D8
		internal static T GetItem<T>(UserContext userContext, string idString, string changeKey, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			if (string.IsNullOrEmpty(idString))
			{
				throw new ArgumentNullException("idString");
			}
			if (changeKey == null)
			{
				throw new ArgumentNullException("changeKey");
			}
			OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromString(idString);
			return Utilities.GetItem<T>(userContext, owaStoreObjectId, changeKey, prefetchProperties);
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0008A316 File Offset: 0x00088516
		internal static T GetItem<T>(UserContext userContext, StoreObjectId storeObjectId, string changeKey, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			if (storeObjectId == null)
			{
				throw new ArgumentNullException("storeObjectId");
			}
			if (changeKey == null)
			{
				throw new ArgumentNullException("changeKey");
			}
			return Utilities.GetItem<T>(userContext, Utilities.CreateItemId(userContext.MailboxSession, storeObjectId, changeKey), prefetchProperties);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0008A348 File Offset: 0x00088548
		internal static T GetItem<T>(UserContext userContext, StoreId storeId, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return Utilities.GetItem<T>(userContext, storeId, ItemBindOption.None, prefetchProperties);
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0008A353 File Offset: 0x00088553
		internal static T GetItem<T>(UserContext userContext, StoreId storeId, ItemBindOption itemBindOption, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return Utilities.GetItem<T>(userContext, storeId, false, itemBindOption, prefetchProperties);
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0008A35F File Offset: 0x0008855F
		internal static T GetItem<T>(UserContext userContext, StoreId storeId, bool forceAsMessageItem, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return Utilities.GetItem<T>(userContext.MailboxSession, storeId, forceAsMessageItem, ItemBindOption.None, prefetchProperties);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0008A370 File Offset: 0x00088570
		internal static T GetItem<T>(UserContext userContext, StoreId storeId, bool forceAsMessageItem, ItemBindOption itemBindOption, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (storeId == null)
			{
				throw new ArgumentNullException("storeId");
			}
			if (forceAsMessageItem && typeof(T).Name != "MessageItem")
			{
				throw new ArgumentException("To force bind as a MessageItem, the typename T should be set to MessageItem.");
			}
			return Utilities.GetItem<T>(userContext.MailboxSession, storeId, forceAsMessageItem, itemBindOption, prefetchProperties);
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0008A3D2 File Offset: 0x000885D2
		internal static T GetItem<T>(UserContext userContext, OwaStoreObjectId owaStoreObjectId, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return Utilities.GetItem<T>(userContext, owaStoreObjectId, false, prefetchProperties);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0008A3DD File Offset: 0x000885DD
		internal static T GetItem<T>(UserContext userContext, OwaStoreObjectId owaStoreObjectId, string changeKey, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return Utilities.GetItem<T>(userContext, owaStoreObjectId, changeKey, false, prefetchProperties);
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0008A3EC File Offset: 0x000885EC
		internal static T GetItem<T>(UserContext userContext, OwaStoreObjectId owaStoreObjectId, bool forceAsMessageItem, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (owaStoreObjectId == null)
			{
				throw new ArgumentNullException("owaStoreObjectId");
			}
			StoreSession session = owaStoreObjectId.GetSession(userContext);
			if (owaStoreObjectId.StoreObjectId == null)
			{
				throw new OwaInvalidRequestException("StoreObjectId is null");
			}
			return Utilities.GetItem<T>(session, owaStoreObjectId.StoreObjectId, forceAsMessageItem, prefetchProperties);
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0008A440 File Offset: 0x00088640
		internal static T GetItem<T>(UserContext userContext, OwaStoreObjectId owaStoreObjectId, string changeKey, bool forceAsMessageItem, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (owaStoreObjectId == null)
			{
				throw new ArgumentNullException("owaStoreObjectId");
			}
			if (changeKey == null)
			{
				throw new ArgumentNullException("changeKey");
			}
			StoreId storeId = Utilities.CreateItemId(owaStoreObjectId.StoreObjectId, changeKey);
			StoreSession session = owaStoreObjectId.GetSession(userContext);
			return Utilities.GetItem<T>(session, storeId, forceAsMessageItem, prefetchProperties);
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0008A496 File Offset: 0x00088696
		internal static T GetItem<T>(StoreSession storeSession, StoreId storeId, bool forceAsMessageItem, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return Utilities.GetItem<T>(storeSession, storeId, forceAsMessageItem, ItemBindOption.None, prefetchProperties);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0008A4A4 File Offset: 0x000886A4
		internal static T GetItem<T>(StoreSession storeSession, StoreId storeId, bool forceAsMessageItem, ItemBindOption itemBindOption, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			if (storeSession == null)
			{
				throw new ArgumentNullException("storeSession");
			}
			if (storeId == null)
			{
				throw new ArgumentNullException("storeId");
			}
			if (forceAsMessageItem && typeof(T).Name != "MessageItem")
			{
				throw new ArgumentException("To force bind as a MessageItem, the typename T should be set to MessageItem.");
			}
			Type typeFromHandle = typeof(T);
			VersionedId versionedId = storeId as VersionedId;
			StoreObjectId storeObjectId;
			if (versionedId != null)
			{
				storeObjectId = versionedId.ObjectId;
			}
			else
			{
				storeObjectId = (StoreObjectId)storeId;
			}
			if (storeObjectId == null)
			{
				throw new OwaInvalidRequestException("The given item Id is null");
			}
			if (!IdConverter.IsMessageId(storeObjectId))
			{
				throw new OwaInvalidRequestException("The given Id is not a valid item Id. Item Id:" + storeObjectId);
			}
			Item item;
			try
			{
				if (typeFromHandle == typeof(MessageItem))
				{
					if (forceAsMessageItem)
					{
						item = Item.BindAsMessage(storeSession, storeId, prefetchProperties);
					}
					else
					{
						item = MessageItem.Bind(storeSession, storeId, itemBindOption, prefetchProperties);
					}
				}
				else if (typeFromHandle == typeof(CalendarItem))
				{
					item = Item.Bind(storeSession, storeId, itemBindOption, prefetchProperties);
				}
				else if (typeFromHandle == typeof(CalendarItemBase))
				{
					item = Item.Bind(storeSession, storeId, itemBindOption, prefetchProperties);
				}
				else if (typeFromHandle == typeof(SharingMessageItem))
				{
					item = SharingMessageItem.Bind(storeSession, storeId, prefetchProperties);
				}
				else if (typeFromHandle == typeof(Contact))
				{
					item = Item.Bind(storeSession, storeId, itemBindOption, prefetchProperties);
				}
				else if (typeFromHandle == typeof(DistributionList))
				{
					item = Item.Bind(storeSession, storeId, itemBindOption, prefetchProperties);
				}
				else if (typeFromHandle == typeof(MeetingRequest))
				{
					item = MessageItem.Bind(storeSession, storeId, itemBindOption, prefetchProperties);
				}
				else if (typeFromHandle == typeof(MeetingCancellation))
				{
					item = MessageItem.Bind(storeSession, storeId, itemBindOption, prefetchProperties);
				}
				else if (typeFromHandle == typeof(MeetingResponse))
				{
					item = MessageItem.Bind(storeSession, storeId, itemBindOption, prefetchProperties);
				}
				else if (typeFromHandle == typeof(Task))
				{
					item = Item.Bind(storeSession, storeId, itemBindOption, prefetchProperties);
				}
				else if (typeFromHandle == typeof(PostItem))
				{
					item = Item.Bind(storeSession, storeId, itemBindOption, prefetchProperties);
				}
				else
				{
					if (!(typeFromHandle == typeof(Item)))
					{
						throw new ArgumentException(typeFromHandle.ToString() + " is not a supported item type");
					}
					item = Item.Bind(storeSession, storeId, itemBindOption, prefetchProperties);
				}
			}
			catch (WrongObjectTypeException innerException)
			{
				throw new OwaInvalidRequestException("Wrong item class supplied", innerException);
			}
			if (!(item is T))
			{
				if (item != null)
				{
					item.Dispose();
				}
				throw new OwaInvalidRequestException("Item type is different than expected");
			}
			return (T)((object)item);
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0008A744 File Offset: 0x00088944
		internal static T GetFolder<T>(UserContext userContext, OwaStoreObjectId folderId, params PropertyDefinition[] prefetchProperties) where T : Folder
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			StoreSession session = folderId.GetSession(userContext);
			return Utilities.GetFolder<T>(session, folderId.StoreObjectId, prefetchProperties);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0008A784 File Offset: 0x00088984
		internal static T GetFolderForContent<T>(UserContext userContext, OwaStoreObjectId folderId, params PropertyDefinition[] prefetchProperties) where T : Folder
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			StoreSession sessionForFolderContent = folderId.GetSessionForFolderContent(userContext);
			return Utilities.GetFolder<T>(sessionForFolderContent, folderId.StoreObjectId, prefetchProperties);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0008A7C4 File Offset: 0x000889C4
		private static T GetFolder<T>(StoreSession storeSession, StoreObjectId folderId, params PropertyDefinition[] prefetchProperties) where T : Folder
		{
			Type typeFromHandle = typeof(T);
			Folder folder;
			if (typeFromHandle == typeof(CalendarFolder))
			{
				folder = CalendarFolder.Bind(storeSession, folderId, prefetchProperties);
			}
			else
			{
				folder = Folder.Bind(storeSession, folderId, prefetchProperties);
			}
			return (T)((object)folder);
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0008A80C File Offset: 0x00088A0C
		internal static string GetParentFolderName(Item item, StoreObjectId parentFolderId, UserContext userContext)
		{
			string text = null;
			string legacyDN = null;
			string arg = null;
			OwaStoreObjectIdType owaStoreObjectIdType = Utilities.GetOwaStoreObjectIdType(userContext, item);
			if (owaStoreObjectIdType == OwaStoreObjectIdType.OtherUserMailboxObject)
			{
				legacyDN = Utilities.GetMailboxSessionLegacyDN(item);
				arg = Utilities.GetSessionMailboxDisplayName(item);
			}
			if (owaStoreObjectIdType == OwaStoreObjectIdType.ArchiveMailboxObject)
			{
				legacyDN = Utilities.GetMailboxSessionLegacyDN(item);
			}
			else if (owaStoreObjectIdType == OwaStoreObjectIdType.PublicStoreItem)
			{
				owaStoreObjectIdType = OwaStoreObjectIdType.PublicStoreFolder;
			}
			OwaStoreObjectId folderId = OwaStoreObjectId.CreateFromFolderId(parentFolderId, owaStoreObjectIdType, legacyDN);
			using (Folder folder = Utilities.GetFolder<Folder>(userContext, folderId, new PropertyDefinition[0]))
			{
				text = folder.DisplayName;
			}
			if (owaStoreObjectIdType == OwaStoreObjectIdType.OtherUserMailboxObject)
			{
				text = string.Format(LocalizedStrings.GetNonEncoded(-83764036), arg, text);
			}
			return text;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0008A8A4 File Offset: 0x00088AA4
		internal static StoreObjectId GetParentFolderId(Item parentItem, Item item)
		{
			StoreObjectId parentId;
			if (parentItem != null)
			{
				parentId = parentItem.ParentId;
			}
			else
			{
				parentId = item.ParentId;
			}
			return parentId;
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0008A8C8 File Offset: 0x00088AC8
		internal static OwaStoreObjectId GetParentFolderId(OwaStoreObjectId itemId)
		{
			switch (itemId.OwaStoreObjectIdType)
			{
			case OwaStoreObjectIdType.PublicStoreFolder:
				return itemId;
			case OwaStoreObjectIdType.PublicStoreItem:
				return OwaStoreObjectId.CreateFromPublicFolderId(IdConverter.GetParentIdFromMessageId(itemId.StoreObjectId));
			case OwaStoreObjectIdType.Conversation:
				return OwaStoreObjectId.CreateFromFolderId(itemId.ParentFolderId, OwaStoreObjectIdType.MailBoxObject);
			case OwaStoreObjectIdType.OtherUserMailboxObject:
				return OwaStoreObjectId.CreateFromOtherUserMailboxFolderId(IdConverter.GetParentIdFromMessageId(itemId.StoreObjectId), itemId.MailboxOwnerLegacyDN);
			case OwaStoreObjectIdType.ArchiveMailboxObject:
				return OwaStoreObjectId.CreateFromArchiveMailboxFolderId(IdConverter.GetParentIdFromMessageId(itemId.StoreObjectId), itemId.MailboxOwnerLegacyDN);
			case OwaStoreObjectIdType.ArchiveConversation:
				return OwaStoreObjectId.CreateFromArchiveMailboxFolderId(itemId.ParentFolderId, itemId.MailboxOwnerLegacyDN);
			default:
				return OwaStoreObjectId.CreateFromFolderId(IdConverter.GetParentIdFromMessageId(itemId.StoreObjectId), OwaStoreObjectIdType.MailBoxObject);
			}
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0008A970 File Offset: 0x00088B70
		internal static T CreateItem<T>(OwaStoreObjectId folderId) where T : Item
		{
			Type typeFromHandle = typeof(T);
			StoreObjectType itemType;
			if (typeFromHandle == typeof(CalendarItem))
			{
				itemType = StoreObjectType.CalendarItem;
			}
			else if (typeFromHandle == typeof(Contact))
			{
				itemType = StoreObjectType.Contact;
			}
			else if (typeFromHandle == typeof(DistributionList))
			{
				itemType = StoreObjectType.DistributionList;
			}
			else if (typeFromHandle == typeof(Task))
			{
				itemType = StoreObjectType.Task;
			}
			else if (typeFromHandle == typeof(PostItem))
			{
				itemType = StoreObjectType.Post;
			}
			else
			{
				itemType = StoreObjectType.Message;
			}
			return Utilities.CreateItem(itemType, folderId) as T;
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0008AA14 File Offset: 0x00088C14
		internal static Item CreateItem(StoreObjectType itemType, OwaStoreObjectId folderId)
		{
			UserContext userContext = OwaContext.Current.UserContext;
			StoreObjectId folderStoreObjectId = null;
			StoreSession storeSession;
			if (folderId == null)
			{
				storeSession = userContext.MailboxSession;
			}
			else
			{
				storeSession = folderId.GetSessionForFolderContent(userContext);
				folderStoreObjectId = folderId.StoreObjectId;
			}
			return Utilities.CreateItem(itemType, folderStoreObjectId, storeSession);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0008AA54 File Offset: 0x00088C54
		private static Item CreateItem(StoreObjectType itemType, StoreObjectId folderStoreObjectId, StoreSession storeSession)
		{
			UserContext userContext = OwaContext.Current.UserContext;
			Item item;
			if (itemType != StoreObjectType.Message)
			{
				switch (itemType)
				{
				case StoreObjectType.CalendarItem:
					if (folderStoreObjectId == null)
					{
						folderStoreObjectId = userContext.CalendarFolderId;
					}
					item = CalendarItem.Create(storeSession, folderStoreObjectId);
					item[ItemSchema.ConversationIndexTracking] = true;
					return item;
				case StoreObjectType.Contact:
					if (folderStoreObjectId == null)
					{
						folderStoreObjectId = userContext.ContactsFolderId;
					}
					return Contact.Create(storeSession, folderStoreObjectId);
				case StoreObjectType.DistributionList:
					if (folderStoreObjectId == null)
					{
						folderStoreObjectId = userContext.ContactsFolderId;
					}
					return DistributionList.Create(storeSession, folderStoreObjectId);
				case StoreObjectType.Task:
					if (folderStoreObjectId == null)
					{
						folderStoreObjectId = userContext.TasksFolderId;
					}
					return Task.Create(storeSession, folderStoreObjectId);
				case StoreObjectType.Post:
					return PostItem.Create(storeSession, folderStoreObjectId);
				}
			}
			item = MessageItem.Create(userContext.MailboxSession, userContext.DraftsFolderId);
			item[ItemSchema.ConversationIndexTracking] = true;
			return item;
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0008AB38 File Offset: 0x00088D38
		internal static void SetPostSender(PostItem postItem, UserContext userContext, bool isTargetFolderPublic)
		{
			if (userContext.IsExplicitLogon && isTargetFolderPublic)
			{
				postItem.From = (postItem.Sender = new Participant(userContext.LogonIdentity.GetOWAMiniRecipient()));
				return;
			}
			postItem.From = (postItem.Sender = new Participant(userContext.ExchangePrincipal));
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0008AB8C File Offset: 0x00088D8C
		internal static Item CreateImplicitDraftItem(StoreObjectType itemType, OwaStoreObjectId destinationFolderId)
		{
			OwaStoreObjectId scratchPadForImplicitDraft = Utilities.GetScratchPadForImplicitDraft(itemType, destinationFolderId);
			Item item = Utilities.CreateItem(itemType, scratchPadForImplicitDraft);
			if (itemType == StoreObjectType.CalendarItem)
			{
				CalendarItem calendarItem = item as CalendarItem;
				calendarItem.StartTime = ExDateTime.Now.AddYears(1);
				calendarItem.EndTime = ExDateTime.Now.AddYears(1);
				Utilities.SaveItem(calendarItem);
				item.Load();
			}
			return item;
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0008ABEC File Offset: 0x00088DEC
		internal static OwaStoreObjectId GetScratchPadForImplicitDraft(StoreObjectType itemType, OwaStoreObjectId destinationFolderId)
		{
			UserContext userContext = OwaContext.Current.UserContext;
			if (itemType == StoreObjectType.Message)
			{
				return OwaStoreObjectId.CreateFromMailboxFolderId(userContext.DraftsFolderId);
			}
			if (destinationFolderId == null)
			{
				return null;
			}
			if (destinationFolderId.IsPublic)
			{
				return userContext.GetDeletedItemsFolderId(userContext.MailboxSession);
			}
			return destinationFolderId;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0008AC30 File Offset: 0x00088E30
		internal static T GetFolderProperty<T>(Folder folder, PropertyDefinition propertyDefinition, T defaultValue)
		{
			object obj = folder.TryGetProperty(propertyDefinition);
			if (obj is PropertyError || obj == null)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0008AC58 File Offset: 0x00088E58
		internal static Attachment GetAttachment(Item item, HttpRequest request, UserContext userContext)
		{
			return Utilities.GetAttachment(item, request, null, userContext);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0008AC63 File Offset: 0x00088E63
		internal static Attachment GetAttachment(Item item, HttpRequest request, List<AttachmentId> attachmentIdList, UserContext userContext)
		{
			if (attachmentIdList == null)
			{
				attachmentIdList = new List<AttachmentId>();
			}
			Utilities.FillAttachmentIdList(item, request, attachmentIdList);
			return Utilities.GetAttachment(item, attachmentIdList, userContext);
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0008AC80 File Offset: 0x00088E80
		internal static void FillAttachmentIdList(Item item, HttpRequest request, List<AttachmentId> attachmentIdList)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			int embeddedItemNestingLevel = AttachmentUtility.GetEmbeddedItemNestingLevel(request);
			for (int i = 0; i < embeddedItemNestingLevel; i++)
			{
				string name = "attid" + i.ToString(CultureInfo.InvariantCulture);
				string queryStringParameter = Utilities.GetQueryStringParameter(request, name);
				AttachmentId item2 = null;
				try
				{
					item2 = item.CreateAttachmentId(queryStringParameter);
				}
				catch (CorruptDataException innerException)
				{
					Utilities.ThrowInvalidIdFormatException(queryStringParameter, null, innerException);
				}
				attachmentIdList.Add(item2);
			}
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0008AD10 File Offset: 0x00088F10
		internal static Attachment GetAttachment(Item item, List<AttachmentId> attachmentIdList, UserContext userContext)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (attachmentIdList == null)
			{
				throw new ArgumentNullException("attachmentIdList");
			}
			int count = attachmentIdList.Count;
			if (count == 0)
			{
				throw new ArgumentException("attachmentIdList");
			}
			for (int i = 0; i < count; i++)
			{
				AttachmentId id = attachmentIdList[i];
				AttachmentCollection attachmentCollection = Utilities.GetAttachmentCollection(item, true, userContext);
				bool flag = false;
				ItemAttachment itemAttachment = null;
				item = null;
				try
				{
					Attachment attachment = attachmentCollection.Open(id);
					if (i == count - 1)
					{
						flag = true;
						return attachment;
					}
					itemAttachment = (attachment as ItemAttachment);
					if (itemAttachment == null)
					{
						if (attachment != null)
						{
							attachment.Dispose();
						}
						throw new OwaInvalidRequestException("Attachment is not an item attachment");
					}
					item = itemAttachment.GetItem();
					if (userContext.IsIrmEnabled && !userContext.IsBasicExperience)
					{
						Utilities.IrmDecryptIfRestricted(item, userContext, true);
					}
				}
				finally
				{
					if (!flag)
					{
						if (itemAttachment != null)
						{
							OwaContext.Current.AddObjectToDisposeOnEndRequest(itemAttachment);
						}
						if (item != null)
						{
							OwaContext.Current.AddObjectToDisposeOnEndRequest(item);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0008AE1C File Offset: 0x0008901C
		internal static void ValidateCalendarItemBaseStoreObject(CalendarItemBase calendarItemBase)
		{
			if (calendarItemBase == null)
			{
				throw new ArgumentNullException("calendarItemBase");
			}
			CalendarItemOccurrence calendarItemOccurrence = calendarItemBase as CalendarItemOccurrence;
			if (calendarItemOccurrence != null)
			{
				calendarItemOccurrence.OccurrencePropertyBag.MasterCalendarItem.Validate();
				return;
			}
			calendarItemBase.Validate();
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0008AE5C File Offset: 0x0008905C
		private static bool CheckForDLMemberSizeTooLargeConstraint(PropertyValidationError[] validationErrors)
		{
			if (validationErrors == null)
			{
				return false;
			}
			foreach (PropertyValidationError propertyValidationError in validationErrors)
			{
				if (propertyValidationError.PropertyDefinition == DistributionListSchema.Members || propertyValidationError.PropertyDefinition == DistributionListSchema.OneOffMembers)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0008AEA3 File Offset: 0x000890A3
		internal static void SaveItem(Item item)
		{
			Utilities.SaveItem(item, true);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0008AEAC File Offset: 0x000890AC
		internal static void SaveItem(Item item, bool updatePerfCounter)
		{
			Utilities.SaveItem(item, updatePerfCounter, SaveMode.ResolveConflicts);
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0008AEB8 File Offset: 0x000890B8
		internal static void SaveItem(Item item, bool updatePerfCounter, SaveMode saveMode)
		{
			ConflictResolutionResult conflictResolutionResult = item.Save(saveMode);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				ExTraceGlobals.CoreTracer.TraceDebug(0L, "Saving item failed due to conflict resolution.");
				throw new OwaEventHandlerException("ACR failed", LocalizedStrings.GetNonEncoded(-482397486), OwaEventHandlerErrorCode.ConflictResolution);
			}
			if (updatePerfCounter && Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.ItemsUpdated.Increment();
			}
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0008AF14 File Offset: 0x00089114
		public static void WriteLatestUrlToAttachment(TextWriter writer, string itemId, string extension)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (itemId == null)
			{
				throw new ArgumentNullException("itemId");
			}
			if (extension == null)
			{
				extension = string.Empty;
			}
			writer.Write("attachment.ashx?attach=1&id=");
			Utilities.WriteDoubleEncodedStringToUrl(writer, itemId);
			writer.Write("&MSWMExt=");
			writer.Write(Utilities.UrlEncode(extension));
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x0008AF70 File Offset: 0x00089170
		public static void WriteDoubleEncodedStringToUrl(TextWriter writer, string input)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentNullException("input", "input cannot be null or empty");
			}
			Encoding unicode = Encoding.Unicode;
			byte[] bytes = unicode.GetBytes(Utilities.UrlEncode(input));
			writer.Write(Convert.ToBase64String(bytes));
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0008AFC4 File Offset: 0x000891C4
		public static string WriteDoubleEncodedStringToUrl(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentNullException("input", "input cannot be null or empty");
			}
			Encoding unicode = Encoding.Unicode;
			byte[] bytes = unicode.GetBytes(Utilities.UrlEncode(input));
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0008B004 File Offset: 0x00089204
		public static string GetStringfromBase64String(string base64Input)
		{
			if (string.IsNullOrEmpty(base64Input))
			{
				throw new ArgumentNullException("base64Input", "base64Input cannot be null or empty");
			}
			Encoding unicode = Encoding.Unicode;
			byte[] bytes = Convert.FromBase64String(base64Input);
			return HttpUtility.UrlDecode(unicode.GetString(bytes));
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0008B044 File Offset: 0x00089244
		public static Guid GetGuidFromBase64String(string base64Input)
		{
			if (string.IsNullOrEmpty(base64Input))
			{
				throw new OwaInvalidRequestException("Missing Base64 String");
			}
			Guid result;
			try
			{
				result = new Guid(Convert.FromBase64String(base64Input));
			}
			catch (ArgumentException innerException)
			{
				throw new OwaInvalidRequestException("Invalid base64 string", innerException);
			}
			catch (FormatException innerException2)
			{
				throw new OwaInvalidRequestException("Invalid base64 string", innerException2);
			}
			catch (OverflowException innerException3)
			{
				throw new OwaInvalidRequestException("Invalid base64 string", innerException3);
			}
			return result;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0008B0C4 File Offset: 0x000892C4
		public static string GetBase64StringFromGuid(Guid guid)
		{
			return Convert.ToBase64String(guid.ToByteArray());
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0008B0D2 File Offset: 0x000892D2
		public static string GetBase64StringFromADObjectId(ADObjectId adObjectId)
		{
			if (adObjectId == null)
			{
				throw new ArgumentNullException("adObjectId");
			}
			return Utilities.GetBase64StringFromGuid(adObjectId.ObjectGuid);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0008B0F0 File Offset: 0x000892F0
		public static Stopwatch StartWatch()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			return stopwatch;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0008B10A File Offset: 0x0008930A
		public static long StopWatch(Stopwatch watch, string traceMessage)
		{
			watch.Stop();
			ExTraceGlobals.CoreTracer.TraceDebug<string, long>(0L, "{0}: {1} ms.", traceMessage, watch.ElapsedMilliseconds);
			return watch.ElapsedMilliseconds;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0008B130 File Offset: 0x00089330
		public static void CropAndRenderText(TextWriter writer, string text, int maxCharacters)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (maxCharacters <= 0)
			{
				throw new ArgumentOutOfRangeException("maxCharacters", "maxCharacters has to be greater than zero");
			}
			int num = (maxCharacters < text.Length) ? maxCharacters : text.Length;
			Utilities.HtmlEncode(text.Substring(0, num), writer);
			if (num < text.Length)
			{
				writer.Write("...");
			}
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0008B1A4 File Offset: 0x000893A4
		public static string GetDefaultFontName()
		{
			CultureInfo userCulture = Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture();
			return Utilities.GetDefaultFontName(userCulture);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0008B1BD File Offset: 0x000893BD
		public static string GetDefaultFontName(CultureInfo userCulture)
		{
			if (Utilities.IsViet(userCulture))
			{
				return "Helvetica";
			}
			return "Tahoma";
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0008B1D4 File Offset: 0x000893D4
		public static bool IsViet()
		{
			CultureInfo userCulture = Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture();
			return Utilities.IsViet(userCulture);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0008B1ED File Offset: 0x000893ED
		public static bool IsViet(CultureInfo userCulture)
		{
			if (userCulture == null)
			{
				throw new ArgumentNullException("userCulture");
			}
			return userCulture.LCID == 1066;
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0008B20A File Offset: 0x0008940A
		public static void RenderDefaultFontNameIfNecessary(TextWriter writer)
		{
			if (Utilities.IsViet())
			{
				writer.Write(Utilities.GetDefaultFontName());
				writer.Write(", ");
			}
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0008B22C File Offset: 0x0008942C
		internal static string GenerateWhen(Item item)
		{
			MeetingMessage meetingMessage = item as MeetingMessage;
			if (meetingMessage != null)
			{
				return meetingMessage.GenerateWhen(CultureInfo.CurrentCulture);
			}
			CalendarItemBase calendarItemBase = item as CalendarItemBase;
			if (calendarItemBase != null)
			{
				return calendarItemBase.GenerateWhen();
			}
			throw new ArgumentException("Unsupported type, this is a bug");
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0008B26C File Offset: 0x0008946C
		public static bool IsDownLevelClient(HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request cannot be null");
			}
			ExTraceGlobals.CoreCallTracer.TraceDebug<string>(0L, "Utilities.IsDownLevelClient. user-agent = {0}", (request.UserAgent == null) ? string.Empty : request.UserAgent);
			string a;
			UserAgentParser.UserAgentVersion userAgentVersion;
			string a2;
			UserAgentParser.Parse(request.UserAgent, out a, out userAgentVersion, out a2);
			return (!string.Equals(a, "MSIE", StringComparison.OrdinalIgnoreCase) || (userAgentVersion.Build < 8 && (userAgentVersion.Build != 7 || request.UserAgent.IndexOf("Trident") <= 0)) || (!string.Equals(a2, "Windows NT", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Windows 98; Win 9x 4.90", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Windows 2000", StringComparison.OrdinalIgnoreCase))) && ((!string.Equals(a, "Safari", StringComparison.OrdinalIgnoreCase) || userAgentVersion.Build < 4 || (!string.Equals(a2, "Macintosh", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Windows NT", StringComparison.OrdinalIgnoreCase))) && ((!string.Equals(a2, "iPhone", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "iPad", StringComparison.OrdinalIgnoreCase)) || userAgentVersion.Build < 5)) && (!string.Equals(a2, "Android", StringComparison.OrdinalIgnoreCase) || userAgentVersion.Build < 4) && (!string.Equals(a, "Firefox", StringComparison.OrdinalIgnoreCase) || userAgentVersion.Build < 4 || (!string.Equals(a2, "Windows NT", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Windows 98; Win 9x 4.90", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Windows 2000", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Macintosh", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Linux", StringComparison.OrdinalIgnoreCase))) && (!string.Equals(a, "Chrome", StringComparison.OrdinalIgnoreCase) || userAgentVersion.Build < 1 || (!string.Equals(a2, "Windows NT", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Macintosh", StringComparison.OrdinalIgnoreCase) && !string.Equals(a2, "Linux", StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0008B448 File Offset: 0x00089648
		internal static ClientBrowserStatus GetClientBrowserStatus(HttpBrowserCapabilities browserCapabilities)
		{
			if (browserCapabilities == null)
			{
				throw new ArgumentNullException("browserCapabilities");
			}
			if (string.Equals(browserCapabilities.Browser, "IE", StringComparison.InvariantCultureIgnoreCase) && browserCapabilities.MajorVersion >= 7)
			{
				foreach (string text in browserCapabilities["extra"].Split(new char[]
				{
					';'
				}))
				{
					if (string.Equals(text.Trim(), "x64", StringComparison.InvariantCultureIgnoreCase))
					{
						return ClientBrowserStatus.IE7OrLaterIn64Bit;
					}
				}
				return ClientBrowserStatus.IE7OrLaterIn32Bit;
			}
			return ClientBrowserStatus.Others;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0008B4D0 File Offset: 0x000896D0
		public static bool IsIEClient(HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request cannot be null");
			}
			ExTraceGlobals.CoreCallTracer.TraceDebug<string>(0L, "Utilities.IsIEClient. user-agent = {0}", (request.UserAgent == null) ? string.Empty : request.UserAgent);
			return !string.IsNullOrEmpty(request.UserAgent) && (request.UserAgent.IndexOf("MSIE", StringComparison.InvariantCultureIgnoreCase) != -1 || request.UserAgent.Equals("Mozilla/5.0 (Windows NT; owaauth)"));
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0008B548 File Offset: 0x00089748
		internal static void AddOwaConditionAdvisorIfNecessary(UserContext userContext, Folder folder, EventObjectType eventObjectType, EventType eventType)
		{
			if (OwaMapiNotificationManager.IsNotificationEnabled(userContext) && !Utilities.IsPublic(folder))
			{
				OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromSessionFolderId(userContext, folder.Session, folder.Id.ObjectId);
				if (userContext.IsPushNotificationsEnabled)
				{
					MailboxSession mailboxSession = folder.Session as MailboxSession;
					if (mailboxSession != null)
					{
						userContext.MapiNotificationManager.SubscribeForFolderChanges(owaStoreObjectId, mailboxSession);
					}
				}
				if (userContext.IsPullNotificationsEnabled)
				{
					Dictionary<OwaStoreObjectId, OwaConditionAdvisor> conditionAdvisorTable = userContext.NotificationManager.ConditionAdvisorTable;
					if (conditionAdvisorTable == null || !conditionAdvisorTable.ContainsKey(owaStoreObjectId))
					{
						userContext.NotificationManager.CreateOwaConditionAdvisor(userContext, folder.Session as MailboxSession, owaStoreObjectId, eventObjectType, eventType);
					}
				}
			}
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0008B5DC File Offset: 0x000897DC
		internal static void SetWebBeaconPolicy(bool isRequestCallbackForWebBeacons, Item item, params PropertyDefinition[] prefetchProperties)
		{
			if (isRequestCallbackForWebBeacons)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				item.OpenAsReadWrite();
				item[ItemSchema.BlockStatus] = BlockStatus.NoNeverAgain;
				ConflictResolutionResult conflictResolutionResult = item.Save(SaveMode.ResolveConflicts);
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					throw new OwaSaveConflictException(LocalizedStrings.GetNonEncoded(-482397486), conflictResolutionResult);
				}
				item.Load(prefetchProperties);
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0008B63C File Offset: 0x0008983C
		public static int GetEmbeddedDepth(HttpRequest request)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(request, "attcnt", false);
			if (queryStringParameter == null)
			{
				return 0;
			}
			int result;
			if (!int.TryParse(queryStringParameter, out result))
			{
				throw new OwaInvalidRequestException("Invalid attachment count querystring parameter");
			}
			return result;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0008B674 File Offset: 0x00089874
		public static bool IsOwa15Url(HttpRequest request)
		{
			if (request.Url.LocalPath.EndsWith("default.aspx", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith("sessiondata.ashx", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith("remotenotification.ashx", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith("service.svc", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".manifest", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (request.Url.LocalPath.EndsWith("/owa", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith("/owa/", StringComparison.OrdinalIgnoreCase))
			{
				if (string.IsNullOrEmpty(request.Url.Query))
				{
					return true;
				}
				foreach (string name in Utilities.Owa15ParameterNames)
				{
					if (Utilities.GetQueryStringParameter(request, name, false) != null)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0008B768 File Offset: 0x00089968
		public static bool IsOwaUrl(Uri requestUrl, OwaUrl owaUrl, bool exactMatch)
		{
			return Utilities.IsOwaUrl(requestUrl, owaUrl, exactMatch, true);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0008B774 File Offset: 0x00089974
		public static bool IsOwaUrl(Uri requestUrl, OwaUrl owaUrl, bool exactMatch, bool useLocal)
		{
			if (requestUrl == null)
			{
				throw new ArgumentNullException("requestUrl");
			}
			if (owaUrl == null)
			{
				throw new ArgumentNullException("owaUrl");
			}
			int length = owaUrl.ImplicitUrl.Length;
			string text = useLocal ? requestUrl.LocalPath : requestUrl.PathAndQuery;
			bool flag = string.Compare(text, 0, owaUrl.ImplicitUrl, 0, length, StringComparison.OrdinalIgnoreCase) == 0;
			if (exactMatch)
			{
				flag = (flag && length == text.Length);
			}
			return flag;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0008B7EC File Offset: 0x000899EC
		public static OwaRequestType GetRequestType(HttpRequest request)
		{
			OwaRequestType result;
			if (Globals.OwaVDirType == OWAVDirType.Calendar && (Utilities.IsOwaUrl(request.Url, OwaUrl.PublishedCalendar, true) || Utilities.IsOwaUrl(request.Url, OwaUrl.ReachPublishedCalendar, true)))
			{
				result = OwaRequestType.PublishedCalendarView;
			}
			else if (Globals.OwaVDirType == OWAVDirType.Calendar && (Utilities.IsOwaUrl(request.Url, OwaUrl.PublishedICal, true) || Utilities.IsOwaUrl(request.Url, OwaUrl.ReachPublishedICal, true)))
			{
				result = OwaRequestType.ICalHttpHandler;
			}
			else if (Utilities.IsResourceRequest(request))
			{
				result = OwaRequestType.Resource;
			}
			else if (WebPartUtilities.IsCmdWebPart(request))
			{
				result = OwaRequestType.Form15;
			}
			else if (Utilities.IsMonitoringPingRequest(request))
			{
				result = OwaRequestType.HealthPing;
			}
			else if (Utilities.IsOwaUrl(request.Url, OwaUrl.Oeh, true))
			{
				if (Utilities.IsOwaUrl(request.Url, OwaUrl.ProxyEws, false, false))
				{
					result = OwaRequestType.ProxyToEwsEventHandler;
				}
				else if (string.Equals(request.Params["ns"], "WebReady", StringComparison.InvariantCultureIgnoreCase))
				{
					result = OwaRequestType.WebReadyRequest;
				}
				else
				{
					result = OwaRequestType.Oeh;
				}
			}
			else if (Utilities.IsOwaUrl(request.Url, OwaUrl.LanguagePage, true))
			{
				result = OwaRequestType.LanguagePage;
			}
			else if (Utilities.IsOwaUrl(request.Url, OwaUrl.Default14Page, true))
			{
				result = OwaRequestType.Form15;
			}
			else if (Utilities.IsOwaUrl(request.Url, OwaUrl.AttachmentHandler, true))
			{
				result = OwaRequestType.Attachment;
			}
			else if (Utilities.IsOwaUrl(request.Url, OwaUrl.WebReadyUrl, false, true))
			{
				result = OwaRequestType.WebReadyRequest;
			}
			else if (Utilities.IsOwaUrl(request.Url, OwaUrl.LanguagePost, true))
			{
				result = OwaRequestType.LanguagePost;
			}
			else if (Utilities.IsOwaUrl(request.Url, OwaUrl.Logoff, true))
			{
				result = OwaRequestType.Logoff;
			}
			else if (Utilities.IsOwaUrl(request.Url, OwaUrl.ProxyLogon, true))
			{
				result = OwaRequestType.ProxyLogon;
			}
			else if (Utilities.IsOwaUrl(request.Url, OwaUrl.ProxyPing, true))
			{
				result = OwaRequestType.ProxyPing;
			}
			else if (Utilities.IsOwaUrl(request.Url, OwaUrl.KeepAlive, true))
			{
				result = OwaRequestType.KeepAlive;
			}
			else if (request.Url.LocalPath.EndsWith(".owa", StringComparison.OrdinalIgnoreCase))
			{
				result = OwaRequestType.Invalid;
			}
			else if (Utilities.IsOwaUrl(request.Url, OwaUrl.AuthFolder, false))
			{
				result = OwaRequestType.Authorize;
			}
			else if (request.Url.LocalPath.EndsWith(".aspx", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".ashx", StringComparison.OrdinalIgnoreCase))
			{
				result = OwaRequestType.Aspx;
			}
			else if (request.Url.LocalPath.EndsWith(Utilities.VirtualDirectoryNameWithLeadingSlash, StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(Utilities.VirtualDirectoryNameWithLeadingAndTrailingSlash, StringComparison.OrdinalIgnoreCase))
			{
				result = (Utilities.IsOwa15Url(request) ? OwaRequestType.Form15 : OwaRequestType.Form14);
			}
			else if (request.Url.LocalPath.EndsWith("service.svc", StringComparison.OrdinalIgnoreCase))
			{
				result = OwaRequestType.ServiceRequest;
			}
			else
			{
				result = OwaRequestType.Invalid;
			}
			return result;
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0008BAAC File Offset: 0x00089CAC
		private static bool IsResourceRequest(HttpRequest request)
		{
			return request.Url.LocalPath.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".css", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".xap", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".js", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".htm", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".html", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".msi", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".ico", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".manifest", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".eot", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".woff", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".svg", StringComparison.OrdinalIgnoreCase) || request.Url.LocalPath.EndsWith(".crx", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0008BC8D File Offset: 0x00089E8D
		private static bool IsMonitoringPingRequest(HttpRequest request)
		{
			return string.Compare(request.Url.PathAndQuery, OwaUrl.HealthPing.ImplicitUrl, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0008BCB0 File Offset: 0x00089EB0
		internal static bool ShouldRenderExpiringPasswordInfobar(UserContext userContext, out int daysToExpiration)
		{
			IExchangePrincipal mailboxOwner = userContext.MailboxSession.MailboxOwner;
			daysToExpiration = -1;
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
			{
				return false;
			}
			if (userContext.IsWebPartRequest || userContext.IsExplicitLogonOthersMailbox)
			{
				return false;
			}
			if (!userContext.IsFeatureEnabled(Feature.ChangePassword))
			{
				return false;
			}
			if (!userContext.MessageViewFirstRender)
			{
				return false;
			}
			if (userContext.MailboxIdentity.IsCrossForest(mailboxOwner.MasterAccountSid))
			{
				return false;
			}
			ExDateTime passwordExpirationDate = DirectoryHelper.GetPasswordExpirationDate(mailboxOwner.ObjectId, userContext.MailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
			if (ExDateTime.MaxValue == passwordExpirationDate)
			{
				return false;
			}
			ExDateTime exDateTime = userContext.TimeZone.ConvertDateTime(passwordExpirationDate);
			ExDateTime exDateTime2 = DateTimeUtilities.GetLocalTime();
			if (exDateTime2.CompareTo(exDateTime) > 0)
			{
				daysToExpiration = 0;
				return true;
			}
			if (ExTraceGlobals.ChangePasswordTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ChangePasswordTracer.TraceDebug(0L, "Password expiration for {0}: Expiration UTC date: {1} / Expiration local user date {2} / User current Local date: {3} ", new object[]
				{
					userContext.LogonIdentity.SafeGetRenderableName(),
					(DateTime)passwordExpirationDate.ToUtc(),
					(DateTime)exDateTime,
					exDateTime2
				});
			}
			exDateTime2 = exDateTime2.Date;
			int days = exDateTime.Date.Subtract(exDateTime2).Days;
			if (days < 14)
			{
				daysToExpiration = days;
				return true;
			}
			return false;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0008BE10 File Offset: 0x0008A010
		public static bool WhiteSpaceOnlyOrNullEmpty(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return true;
			}
			foreach (char c in s)
			{
				if (!char.IsWhiteSpace(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0008BE51 File Offset: 0x0008A051
		public static bool IsGetRequest(HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			return string.Equals(request.HttpMethod, "get", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0008BE72 File Offset: 0x0008A072
		public static bool IsPostRequest(HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			return string.Equals(request.HttpMethod, "post", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0008BE93 File Offset: 0x0008A093
		internal static Utilities.ChangePasswordResult ChangePassword(OwaIdentity owaIdentity, SecureString oldPassword, SecureString newPassword)
		{
			return Utilities.ChangePasswordNUCP(owaIdentity, oldPassword, newPassword);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0008BE9D File Offset: 0x0008A09D
		internal static Utilities.ChangePasswordResult ChangePassword(string logonName, SecureString oldPassword, SecureString newPassword)
		{
			return Utilities.ChangePasswordNUCP(logonName, oldPassword, newPassword);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0008BEA8 File Offset: 0x0008A0A8
		internal static Utilities.ChangePasswordResult ChangePasswordNUCP(OwaIdentity identity, SecureString oldPassword, SecureString newPassword)
		{
			if (identity == null || oldPassword == null || newPassword == null)
			{
				throw new ArgumentNullException();
			}
			string logonName;
			try
			{
				logonName = identity.GetLogonName();
			}
			catch (OwaIdentityException)
			{
				ExTraceGlobals.ChangePasswordTracer.TraceDebug<string>(0L, "ChangePassword failed to retrieve user name for : {0}", identity.UniqueId);
				return Utilities.ChangePasswordResult.OtherError;
			}
			return Utilities.ChangePasswordNUCP(logonName, oldPassword, newPassword);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0008BF04 File Offset: 0x0008A104
		internal static Utilities.ChangePasswordResult ChangePasswordNUCP(string logonName, SecureString oldPassword, SecureString newPassword)
		{
			if (logonName == null || oldPassword == null || newPassword == null)
			{
				throw new ArgumentNullException();
			}
			string text = null;
			string text2 = null;
			if (!Utilities.IsDomainSlashUser(logonName))
			{
				if (Utilities.IsUserPrincipalName(logonName))
				{
					try
					{
						text = NativeHelpers.GetDomainName();
						text2 = logonName;
						goto IL_83;
					}
					catch (CannotGetDomainInfoException ex)
					{
						ExTraceGlobals.ChangePasswordTracer.TraceError<string, string>(0L, "Change password for UPN {0} failed to get the domain name. Error: {1}", logonName, ex.Message);
						return Utilities.ChangePasswordResult.OtherError;
					}
				}
				ExTraceGlobals.ChangePasswordTracer.TraceError<string>(0L, "Change password failed due to bad user name: {0}", logonName);
				return Utilities.ChangePasswordResult.OtherError;
			}
			string[] array = logonName.Split(new char[]
			{
				'\\'
			});
			text = array[0];
			text2 = array[1];
			IL_83:
			ExTraceGlobals.ChangePasswordTracer.TraceDebug<string, string>(0L, "Attempting to call NetUserChangePassword with domain: {0} and user: {1}", text, text2);
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(oldPassword);
				intPtr2 = Marshal.SecureStringToGlobalAllocUnicode(newPassword);
				uint num = SafeNativeMethods.NetUserChangePassword(text, text2, intPtr, intPtr2);
				if (num != 0U)
				{
					ExTraceGlobals.ChangePasswordTracer.TraceError<uint>(0L, "NetUserChangePassword failed with error code = {0}", num);
					uint num2 = num;
					if (num2 == 5U)
					{
						return Utilities.ChangePasswordResult.LockedOut;
					}
					if (num2 == 86U)
					{
						return Utilities.ChangePasswordResult.InvalidCredentials;
					}
					if (num2 != 2245U)
					{
						return Utilities.ChangePasswordResult.OtherError;
					}
					return Utilities.ChangePasswordResult.BadNewPassword;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr2);
				}
			}
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.PasswordChanges.Increment();
			}
			ExTraceGlobals.ChangePasswordTracer.TraceDebug(0L, "Password was changed succesfully");
			return Utilities.ChangePasswordResult.Success;
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0008C088 File Offset: 0x0008A288
		public static bool IsChangePasswordLogoff(HttpRequest request)
		{
			return Utilities.GetQueryStringParameter(request, "ChgPwd", false) == "1";
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0008C0A0 File Offset: 0x0008A2A0
		public static void RewriteAndSanitizeWebReadyHtml(string documentId, Stream inputStream, Stream outputStream)
		{
			if (string.IsNullOrEmpty(documentId))
			{
				throw new ArgumentException("documentId cannot be null or empty");
			}
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			try
			{
				HtmlToHtml htmlToHtml = new HtmlToHtml();
				TextConvertersInternalHelpers.SetPreserveDisplayNoneStyle(htmlToHtml, true);
				htmlToHtml.InputEncoding = Encoding.UTF8;
				htmlToHtml.OutputEncoding = Encoding.UTF8;
				htmlToHtml.FilterHtml = true;
				htmlToHtml.HtmlTagCallback = new HtmlTagCallback(new OwaSafeHtmlWebReadyCallbacks(documentId).ProcessTag);
				htmlToHtml.Convert(inputStream, outputStream);
			}
			catch (InvalidCharsetException innerException)
			{
				throw new OwaBodyConversionFailedException("Sanitize Html Failed", innerException);
			}
			catch (TextConvertersException innerException2)
			{
				throw new OwaBodyConversionFailedException("Sanitize Html Failed", innerException2);
			}
			catch (StoragePermanentException innerException3)
			{
				throw new OwaBodyConversionFailedException("Body Conversion Failed", innerException3);
			}
			catch (StorageTransientException innerException4)
			{
				throw new OwaBodyConversionFailedException("Body Conversion Failed", innerException4);
			}
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0008C194 File Offset: 0x0008A394
		public static void DeleteFBASessionCookies(HttpResponse response)
		{
			Utilities.DeleteCookie(response, "sessionid");
			Utilities.DeleteCookie(response, "cadata");
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0008C1AC File Offset: 0x0008A3AC
		public static void DeleteCookie(HttpResponse response, string name)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name can not be null or empty string");
			}
			bool flag = false;
			for (int i = 0; i < response.Cookies.Count; i++)
			{
				HttpCookie httpCookie = response.Cookies[i];
				if (httpCookie.Name != null && string.Equals(httpCookie.Name, name, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				response.Cookies.Add(new HttpCookie(name, string.Empty));
			}
			response.Cookies[name].Expires = (DateTime)ExDateTime.UtcNow.AddYears(-30);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0008C258 File Offset: 0x0008A458
		public static string GetTCMIDValue(OwaContext owaContext)
		{
			HttpCookieCollection cookies = owaContext.HttpContext.Request.Cookies;
			if (cookies != null)
			{
				HttpCookie httpCookie = cookies.Get("TCMID");
				if (httpCookie != null)
				{
					return httpCookie.Value;
				}
			}
			NameValueCollection headers = owaContext.HttpContext.Request.Headers;
			if (headers != null)
			{
				string text = headers["TCMID"];
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			return string.Empty;
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0008C2C0 File Offset: 0x0008A4C0
		public static string GetCurrentCanary(ISessionContext sessionContext)
		{
			if (sessionContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			UserContext userContext = sessionContext as UserContext;
			if (userContext == null)
			{
				return sessionContext.Canary;
			}
			UserContextKey userContextKey = userContext.Key.CloneWithRenewedCanary();
			return userContextKey.Canary.ToString();
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0008C304 File Offset: 0x0008A504
		public static void RenderCanaryHidden(TextWriter writer, UserContext userContext)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			writer.Write("<input type=hidden name=\"");
			writer.Write("hidcanary");
			writer.Write("\" value=\"");
			Utilities.HtmlEncode(Utilities.GetCurrentCanary(userContext), writer);
			writer.Write("\">");
			string canary15CookieValue = Utilities.GetCanary15CookieValue();
			if (canary15CookieValue != null)
			{
				writer.Write("<input type=hidden name=\"");
				writer.Write("X-OWA-CANARY");
				writer.Write("\" value=\"");
				writer.Write(canary15CookieValue);
				writer.Write("\">");
			}
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0008C3A4 File Offset: 0x0008A5A4
		public static string GetCanaryRequestParameter()
		{
			string text = "&canary=" + Utilities.UrlEncode(Utilities.GetCurrentCanary(OwaContext.Current.UserContext));
			string canary15CookieValue = Utilities.GetCanary15CookieValue();
			if (canary15CookieValue != null)
			{
				text = text + "&X-OWA-CANARY=" + canary15CookieValue;
			}
			return text;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0008C3E8 File Offset: 0x0008A5E8
		internal static void VerifyCanary(UserContextCookie userContextCookie, HttpRequest httpRequest)
		{
			if (userContextCookie == null)
			{
				throw new ArgumentNullException("userContextCookie");
			}
			if (httpRequest == null)
			{
				throw new ArgumentNullException("HttpRequest");
			}
			string canaryFromRequest;
			if (Utilities.IsPostRequest(httpRequest))
			{
				canaryFromRequest = Utilities.GetFormParameter(httpRequest, "hidcanary", false);
			}
			else
			{
				canaryFromRequest = Utilities.GetQueryStringParameter(httpRequest, "canary", false);
			}
			Utilities.ValidateCanary(canaryFromRequest, userContextCookie);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0008C440 File Offset: 0x0008A640
		internal static void VerifyEventHandlerCanary(OwaEventHandlerBase eventHandler)
		{
			if (eventHandler == null)
			{
				throw new ArgumentNullException("eventHandler");
			}
			if (eventHandler.EventInfo.Name == "RenderADPhoto" || eventHandler.EventInfo.Name == "RenderImage")
			{
				return;
			}
			UserContextCookie userContextCookie = UserContextCookie.GetUserContextCookie(eventHandler.OwaContext);
			Utilities.ValidateCanary((string)eventHandler.GetParameter("canary"), userContextCookie);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0008C4AC File Offset: 0x0008A6AC
		internal static void VerifySearchCanaryInGetRequest(HttpRequest httpRequest)
		{
			if (Utilities.IsGetRequest(httpRequest))
			{
				string queryStringParameter = Utilities.GetQueryStringParameter(httpRequest, "canary", true);
				if (!OwaContext.Current.UserContext.Key.Canary.ValidateCanary(queryStringParameter))
				{
					throw new OwaInvalidCanary14Exception(null, "Invalid canary in search query");
				}
			}
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0008C4F8 File Offset: 0x0008A6F8
		internal static void ValidateCanary(string canaryFromRequest, UserContextCookie userContextCookie)
		{
			string text = (userContextCookie.ContextCanary == null) ? null : userContextCookie.ContextCanary.ToString();
			if (string.IsNullOrEmpty(canaryFromRequest) || string.IsNullOrEmpty(text) || !userContextCookie.ContextCanary.ValidateCanary(canaryFromRequest))
			{
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.InvalidCanaryRequests.Increment();
				}
				ExTraceGlobals.CoreTracer.TraceDebug<string, string>(0L, "Utilities.ValidateCanary(): Invalid canary. Request Canary: '{0}'  Cookie Canary: '{1}'", (canaryFromRequest != null) ? canaryFromRequest : "null", (text != null) ? text : "null");
				throw new OwaInvalidCanary14Exception(userContextCookie, "Invalid canary in request");
			}
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0008C580 File Offset: 0x0008A780
		internal static bool IsELCRootFolder(OwaStoreObjectId folderId, UserContext userContext)
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return !folderId.IsPublic && !folderId.IsArchive && !folderId.IsOtherMailbox && !folderId.IsGSCalendar && Utilities.IsELCRootFolder(folderId.StoreObjectId, userContext);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0008C5D8 File Offset: 0x0008A7D8
		internal static bool IsELCRootFolder(StoreObjectId folderId, UserContext userContext)
		{
			bool result;
			using (Folder folder = Folder.Bind(userContext.MailboxSession, folderId, new PropertyDefinition[]
			{
				FolderSchema.AdminFolderFlags
			}))
			{
				result = Utilities.IsELCRootFolder(folder.TryGetProperty(FolderSchema.AdminFolderFlags));
			}
			return result;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0008C630 File Offset: 0x0008A830
		internal static bool IsELCRootFolder(Folder folder)
		{
			return Utilities.IsELCRootFolder(folder.TryGetProperty(FolderSchema.AdminFolderFlags));
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0008C642 File Offset: 0x0008A842
		public static bool IsELCRootFolder(object adminFolderFlags)
		{
			return adminFolderFlags is int && ((int)adminFolderFlags & 16) > 0;
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0008C65A File Offset: 0x0008A85A
		public static bool IsELCFolder(int adminFolderFlags)
		{
			return (adminFolderFlags & 1) > 0 || (adminFolderFlags & 2) > 0;
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0008C66C File Offset: 0x0008A86C
		internal static bool IsELCFolder(Folder folder)
		{
			object obj = folder.TryGetProperty(FolderSchema.AdminFolderFlags);
			return obj != null && !(obj is PropertyError) && Utilities.IsELCFolder((int)obj);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0008C69D File Offset: 0x0008A89D
		internal static OutlookModule GetModuleForFolder(Folder folder, UserContext userContext)
		{
			if (Utilities.IsDefaultFolder(folder, DefaultFolderType.ToDoSearch))
			{
				return OutlookModule.Tasks;
			}
			return Utilities.GetModuleForObjectClass(folder.ClassName);
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0008C6B8 File Offset: 0x0008A8B8
		internal static OutlookModule GetModuleForObjectClass(string objectClass)
		{
			if (objectClass == null)
			{
				return OutlookModule.None;
			}
			if (ObjectClass.IsMessageFolder(objectClass) || ObjectClass.IsMessage(objectClass, false) || ObjectClass.IsMeetingMessage(objectClass) || ObjectClass.IsTaskRequest(objectClass) || ObjectClass.IsReport(objectClass))
			{
				return OutlookModule.Mail;
			}
			if (ObjectClass.IsTaskFolder(objectClass) || ObjectClass.IsTask(objectClass))
			{
				return OutlookModule.Tasks;
			}
			if (ObjectClass.IsContactsFolder(objectClass) || ObjectClass.IsContact(objectClass) || ObjectClass.IsDistributionList(objectClass))
			{
				return OutlookModule.Contacts;
			}
			if (ObjectClass.IsCalendarFolder(objectClass) || ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(objectClass))
			{
				return OutlookModule.Calendar;
			}
			if (ObjectClass.IsJournalFolder(objectClass) || ObjectClass.IsJournalItem(objectClass))
			{
				return OutlookModule.Journal;
			}
			if (ObjectClass.IsNotesFolder(objectClass) || ObjectClass.IsNotesItem(objectClass))
			{
				return OutlookModule.Notes;
			}
			return OutlookModule.None;
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0008C758 File Offset: 0x0008A958
		public static bool IsFolderSegmentedOut(string folderType, UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return !string.IsNullOrEmpty(folderType) && !ObjectClass.IsMessageFolder(folderType) && ((!userContext.IsFeatureEnabled(Feature.Calendar) && ObjectClass.IsCalendarFolder(folderType)) || (!userContext.IsFeatureEnabled(Feature.Contacts) && ObjectClass.IsContactsFolder(folderType)) || (!userContext.IsFeatureEnabled(Feature.Tasks) && ObjectClass.IsTaskFolder(folderType)) || (!userContext.IsFeatureEnabled(Feature.Journal) && ObjectClass.IsJournalFolder(folderType)) || (!userContext.IsFeatureEnabled(Feature.StickyNotes) && ObjectClass.IsOfClass(folderType, "IPF.StickyNote")));
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0008C7F4 File Offset: 0x0008A9F4
		public static BrowserPlatform GetBrowserPlatform(string userAgent)
		{
			if (userAgent == null)
			{
				return BrowserPlatform.Other;
			}
			string text = null;
			string text2 = null;
			UserAgentParser.UserAgentVersion userAgentVersion;
			UserAgentParser.Parse(userAgent, out text, out userAgentVersion, out text2);
			if (string.Equals(text2, "Macintosh", StringComparison.OrdinalIgnoreCase))
			{
				return BrowserPlatform.Macintosh;
			}
			if (-1 != text2.IndexOf("Windows", StringComparison.OrdinalIgnoreCase))
			{
				return BrowserPlatform.Windows;
			}
			return BrowserPlatform.Other;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0008C838 File Offset: 0x0008AA38
		public static BrowserType GetBrowserType(string userAgent)
		{
			if (userAgent == null)
			{
				return BrowserType.Other;
			}
			string a = null;
			string text = null;
			UserAgentParser.UserAgentVersion userAgentVersion;
			UserAgentParser.Parse(userAgent, out a, out userAgentVersion, out text);
			if (string.Equals(a, "MSIE", StringComparison.OrdinalIgnoreCase))
			{
				return BrowserType.IE;
			}
			if (string.Equals(a, "Opera", StringComparison.OrdinalIgnoreCase))
			{
				return BrowserType.Opera;
			}
			if (string.Equals(a, "Safari", StringComparison.OrdinalIgnoreCase))
			{
				return BrowserType.Safari;
			}
			if (string.Equals(a, "Firefox", StringComparison.OrdinalIgnoreCase))
			{
				return BrowserType.Firefox;
			}
			if (string.Equals(a, "Chrome", StringComparison.OrdinalIgnoreCase))
			{
				return BrowserType.Chrome;
			}
			return BrowserType.Other;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0008C8AC File Offset: 0x0008AAAC
		internal static bool CreateExchangeParticipant(out Participant exchangeParticipant, string displayName, string routingAddress, string routingType, AddressOrigin addressOrigin, StoreObjectId storeObjectId, EmailAddressIndex emailAddressIndex)
		{
			bool result = false;
			if (string.IsNullOrEmpty(routingAddress) && !Utilities.IsMapiPDL(routingType))
			{
				exchangeParticipant = new Participant(displayName, null, null);
				result = true;
			}
			else
			{
				ParticipantOrigin origin = null;
				if (addressOrigin == AddressOrigin.Store && storeObjectId != null)
				{
					EmailAddressIndex emailAddressIndex2 = Participant.RoutingTypeEquals(routingType, "MAPIPDL") ? EmailAddressIndex.None : ((emailAddressIndex == EmailAddressIndex.None) ? EmailAddressIndex.Email1 : emailAddressIndex);
					origin = new StoreParticipantOrigin(storeObjectId, emailAddressIndex2);
				}
				else if (addressOrigin == AddressOrigin.Directory)
				{
					origin = new DirectoryParticipantOrigin();
				}
				exchangeParticipant = new Participant(displayName, routingAddress, routingType, origin, new KeyValuePair<PropertyDefinition, object>[0]);
			}
			return result;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0008C926 File Offset: 0x0008AB26
		internal static bool CreateExchangeParticipant(out Participant exchangeParticipant, RecipientAddress recipientAddress)
		{
			return Utilities.CreateExchangeParticipant(out exchangeParticipant, recipientAddress.DisplayName, recipientAddress.RoutingAddress, recipientAddress.RoutingType, recipientAddress.AddressOrigin, recipientAddress.StoreObjectId, recipientAddress.EmailAddressIndex);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0008C954 File Offset: 0x0008AB54
		internal static bool RecipientsOnlyHaveEmptyPDL<TRecipient>(UserContext userContext, IRecipientBaseCollection<TRecipient> recipients) where TRecipient : IRecipientBase
		{
			if (recipients.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < recipients.Count; i++)
			{
				TRecipient trecipient = recipients[i];
				if (trecipient.Participant.RoutingType != "MAPIPDL")
				{
					return false;
				}
			}
			for (int j = 0; j < recipients.Count; j++)
			{
				TRecipient trecipient2 = recipients[j];
				if (trecipient2.Participant.RoutingType == "MAPIPDL")
				{
					TRecipient trecipient3 = recipients[j];
					if (trecipient3.Participant.Origin is StoreParticipantOrigin)
					{
						TRecipient trecipient4 = recipients[j];
						StoreObjectId originItemId = ((StoreParticipantOrigin)trecipient4.Participant.Origin).OriginItemId;
						if (originItemId != null && DistributionList.ExpandDeep(userContext.MailboxSession, originItemId).Length > 0)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0008CA41 File Offset: 0x0008AC41
		internal static bool ValidateRequest(HttpContext context, out string reason)
		{
			reason = string.Empty;
			if (Globals.OwaVDirType == OWAVDirType.OWA && context.Request.UserAgent == null)
			{
				reason = "Request has no user agent";
				return false;
			}
			return true;
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0008CA68 File Offset: 0x0008AC68
		internal static Participant CreateParticipantFromQueryString(UserContext userContext, HttpRequest httpRequest)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(httpRequest, "to", false);
			if (string.IsNullOrEmpty(queryStringParameter))
			{
				return null;
			}
			string queryStringParameter2 = Utilities.GetQueryStringParameter(httpRequest, "nm", false);
			string queryStringParameter3 = Utilities.GetQueryStringParameter(httpRequest, "ao", false);
			string queryStringParameter4 = Utilities.GetQueryStringParameter(httpRequest, "rt", false);
			if (string.IsNullOrEmpty(queryStringParameter3))
			{
				return new Participant(queryStringParameter2, queryStringParameter, queryStringParameter4);
			}
			string queryStringParameter5 = Utilities.GetQueryStringParameter(httpRequest, "stId", false);
			int num;
			if (!int.TryParse(queryStringParameter3, out num))
			{
				throw new OwaInvalidRequestException("Invalid address origin querystring parameter");
			}
			Participant.Builder builder = new Participant.Builder(queryStringParameter2, queryStringParameter, queryStringParameter4 ?? "SMTP");
			switch (num)
			{
			case 1:
			{
				StoreObjectId originItemId = Utilities.CreateStoreObjectId(userContext.MailboxSession, queryStringParameter5);
				EmailAddressIndex emailAddressIndex = EmailAddressIndex.None;
				if (!Utilities.IsMapiPDL(queryStringParameter4))
				{
					string queryStringParameter6 = Utilities.GetQueryStringParameter(httpRequest, "ei", true);
					int num2;
					if (!int.TryParse(queryStringParameter6, out num2))
					{
						throw new OwaInvalidRequestException("Invalid email address index querystring parameter");
					}
					emailAddressIndex = (EmailAddressIndex)num2;
				}
				builder.Origin = new StoreParticipantOrigin(originItemId, emailAddressIndex);
				break;
			}
			case 2:
				builder.RoutingType = "EX";
				break;
			default:
				throw new OwaInvalidRequestException("Invalid address origin in URL");
			}
			return builder.ToParticipant();
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0008CB88 File Offset: 0x0008AD88
		internal static MessageItem CreateDraftMessageFromQueryString(UserContext userContext, HttpRequest httpRequest)
		{
			return (MessageItem)Utilities.CreateDraftMessageOrMeetingRequestFromQueryString(userContext, httpRequest, true, new PropertyDefinition[0]);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0008CB9D File Offset: 0x0008AD9D
		internal static CalendarItemBase CreateDraftMeetingRequestFromQueryString(UserContext userContext, HttpRequest httpRequest, params PropertyDefinition[] properties)
		{
			return (CalendarItemBase)Utilities.CreateDraftMessageOrMeetingRequestFromQueryString(userContext, httpRequest, false, properties);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0008CBB0 File Offset: 0x0008ADB0
		private static Item CreateDraftMessageOrMeetingRequestFromQueryString(UserContext userContext, HttpRequest httpRequest, bool isMessage, params PropertyDefinition[] properties)
		{
			Item item = null;
			MessageItem messageItem = null;
			CalendarItemBase calendarItemBase = null;
			Participant participant = Utilities.CreateParticipantFromQueryString(userContext, httpRequest);
			if (participant != null)
			{
				if (isMessage)
				{
					messageItem = (item = MessageItem.Create(userContext.MailboxSession, userContext.DraftsFolderId));
				}
				else
				{
					calendarItemBase = (item = CalendarItem.Create(userContext.MailboxSession, userContext.CalendarFolderId));
				}
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsCreated.Increment();
				}
				string queryStringParameter = Utilities.GetQueryStringParameter(httpRequest, "subject", false);
				if (!string.IsNullOrEmpty(queryStringParameter))
				{
					if (isMessage)
					{
						messageItem.Subject = queryStringParameter;
					}
					else
					{
						calendarItemBase.Subject = queryStringParameter;
					}
				}
				if (isMessage)
				{
					messageItem.Recipients.Add(participant, RecipientItemType.To);
				}
				else
				{
					calendarItemBase.IsMeeting = true;
					calendarItemBase.AttendeeCollection.Add(participant, AttendeeType.Required, null, null, false);
				}
				item[ItemSchema.ConversationIndexTracking] = true;
				item.Save(SaveMode.ResolveConflicts);
				item.Load(properties);
			}
			return item;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0008CCA0 File Offset: 0x0008AEA0
		internal static string GetDefaultFolderDisplayName(MailboxSession mailboxSession, DefaultFolderType defaultFolderType)
		{
			string displayName;
			using (Folder folder = Folder.Bind(mailboxSession, defaultFolderType, new PropertyDefinition[]
			{
				FolderSchema.DisplayName
			}))
			{
				displayName = folder.DisplayName;
			}
			return displayName;
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0008CCEC File Offset: 0x0008AEEC
		internal static T GetParticipantProperty<T>(Participant participant, PropertyDefinition propertyDefinition, T defaultValue)
		{
			object obj = participant.TryGetProperty(propertyDefinition);
			if (obj == null || obj is PropertyError)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x0008CD14 File Offset: 0x0008AF14
		public static bool IsJapanese
		{
			get
			{
				return Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture().LCID == 1041;
			}
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0008CD28 File Offset: 0x0008AF28
		private static string GenerateExternalLink(OwaContext owaContext)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(owaContext.HttpContext.Request, "URL", false);
			if (string.IsNullOrEmpty(queryStringParameter))
			{
				return null;
			}
			string text = Redir.BuildRedirUrl(owaContext.UserContext, queryStringParameter) + "&NoDocLnkCls=1";
			return string.Format(LocalizedStrings.GetHtmlEncoded(-1396387455), string.Concat(new string[]
			{
				"<br><a href=\"",
				text,
				"\" target=\"_blank\" class=lnk>",
				Utilities.HtmlEncode(queryStringParameter),
				"</a>"
			}));
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0008CDB0 File Offset: 0x0008AFB0
		internal static string GetLatestVoiceMailFileName(IStorePropertyBag propertyBag)
		{
			if (propertyBag == null)
			{
				throw new ArgumentNullException("propertyBag");
			}
			string property = ItemUtility.GetProperty<string>(propertyBag, MessageItemSchema.VoiceMessageAttachmentOrder, null);
			if (string.IsNullOrEmpty(property))
			{
				return null;
			}
			string text = null;
			string[] array = property.Split(new char[]
			{
				';'
			});
			for (int i = array.Length - 1; i >= 0; i--)
			{
				text = array[i].Trim();
				if (text.Length > 0)
				{
					break;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0008CE28 File Offset: 0x0008B028
		internal static Attachment GetLatestVoiceMailAttachment(Item item, UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			AttachmentCollection attachmentCollection = Utilities.GetAttachmentCollection(item, false, userContext);
			if (attachmentCollection == null)
			{
				return null;
			}
			AttachmentId attachmentId = null;
			item.Load(new PropertyDefinition[]
			{
				MessageItemSchema.VoiceMessageAttachmentOrder
			});
			string latestVoiceMailFileName = Utilities.GetLatestVoiceMailFileName(item);
			if (latestVoiceMailFileName == null)
			{
				return null;
			}
			foreach (AttachmentHandle handle in attachmentCollection)
			{
				using (Attachment attachment = attachmentCollection.Open(handle))
				{
					if (!(attachment is ItemAttachment))
					{
						if (string.Equals(latestVoiceMailFileName, attachment.FileName, StringComparison.OrdinalIgnoreCase))
						{
							AttachmentPolicy.Level attachmentLevel = AttachmentLevelLookup.GetAttachmentLevel(attachment, userContext);
							if (attachmentLevel == AttachmentPolicy.Level.Block)
							{
								return null;
							}
							attachmentId = attachment.Id;
							break;
						}
					}
				}
			}
			if (attachmentId == null)
			{
				return null;
			}
			if (userContext.IsIrmEnabled && !userContext.IsBasicExperience && Utilities.IsIrmRestrictedAndDecrypted(item) && Utilities.IsProtectedVoiceMessage(latestVoiceMailFileName))
			{
				RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
				rightsManagedMessageItem.UnprotectAttachment(attachmentId);
				return rightsManagedMessageItem.ProtectedAttachmentCollection.Open(attachmentId);
			}
			return attachmentCollection.Open(attachmentId);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0008CF5C File Offset: 0x0008B15C
		public static bool IsProtectedVoiceMessage(string fileName)
		{
			return !string.IsNullOrEmpty(fileName) && (fileName.EndsWith(".umrmmp3", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".umrmwav", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".umrmwma", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0008CF93 File Offset: 0x0008B193
		public static bool IsMapiPDL(string routingType)
		{
			return routingType != null && string.CompareOrdinal(routingType, "MAPIPDL") == 0;
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0008CFA8 File Offset: 0x0008B1A8
		public static SanitizedHtmlString GetNoScriptHtml()
		{
			string htmlEncoded = LocalizedStrings.GetHtmlEncoded(719849305);
			return SanitizedHtmlString.Format(htmlEncoded, new object[]
			{
				"<a href=\"http://www.microsoft.com/windows/ie/downloads/default.mspx\">",
				"</a>"
			});
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0008CFDE File Offset: 0x0008B1DE
		public static bool IsBasicAuthentication(HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			return 0 == CultureInfo.InvariantCulture.CompareInfo.Compare(request.ServerVariables["AUTH_TYPE"], "Basic", CompareOptions.IgnoreCase);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0008D018 File Offset: 0x0008B218
		public static string BuildHelpHref(string helpFile, string helpAnchor)
		{
			if (helpFile == null)
			{
				throw new ArgumentNullException("helpFile");
			}
			if (helpAnchor == null)
			{
				throw new ArgumentNullException("helpAnchor");
			}
			if (OwaContext.Current.UserContext.IsBasicExperience)
			{
				return string.Concat(new string[]
				{
					"help/",
					Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserHelpLanguage(),
					"/light/",
					helpFile,
					(helpAnchor.Length != 0) ? ("#" + helpAnchor) : string.Empty
				});
			}
			return string.Concat(new string[]
			{
				"help/",
				Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserHelpLanguage(),
				"/premium/",
				helpFile,
				(helpAnchor.Length != 0) ? ("#" + helpAnchor) : string.Empty
			});
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0008D0E0 File Offset: 0x0008B2E0
		public static string BuildEhcHref(string helpId)
		{
			OrganizationProperties organizationProperties = null;
			if (OwaContext.Current != null && OwaContext.Current.MailboxIdentity != null)
			{
				organizationProperties = OwaContext.Current.MailboxIdentity.UserOrganizationProperties;
			}
			return HelpProvider.ConstructHelpRenderingUrl(OwaContext.Current.Culture.LCID, HelpProvider.OwaHelpExperience.Light, helpId, HelpProvider.RenderingMode.Mouse, null, organizationProperties).ToString();
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0008D130 File Offset: 0x0008B330
		public static string BuildPrivacyStatmentHref(UserContext userContext)
		{
			if (userContext != null && userContext.ExchangePrincipal != null)
			{
				bool? privacyLinkDisplayEnabled = HelpProvider.GetPrivacyLinkDisplayEnabled(userContext.ExchangePrincipal.MailboxInfo.OrganizationId);
				if (privacyLinkDisplayEnabled != null && !privacyLinkDisplayEnabled.Value)
				{
					return string.Empty;
				}
				Uri uri;
				if (HelpProvider.TryGetPrivacyStatementUrl(userContext.ExchangePrincipal.MailboxInfo.OrganizationId, out uri))
				{
					return Utilities.AppendLCID(uri.ToString());
				}
			}
			return string.Empty;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0008D1A0 File Offset: 0x0008B3A0
		private static string AppendLCID(string url)
		{
			if (!string.IsNullOrEmpty(url) && OwaContext.Current != null)
			{
				return string.Format("{0}&clcid={1}", url, Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture().LCID);
			}
			return url;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0008D1D0 File Offset: 0x0008B3D0
		public static string BuildCommunitySiteHref(UserContext userContext)
		{
			Uri uri;
			if (userContext != null && userContext.ExchangePrincipal != null && HelpProvider.TryGetCommunityUrl(userContext.ExchangePrincipal.MailboxInfo.OrganizationId, out uri))
			{
				return uri.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0008D20D File Offset: 0x0008B40D
		internal static void RenderImageAltAttribute(TextWriter writer, ISessionContext sessionContext, ThemeFileId themeFileId)
		{
			Utilities.RenderImageAltAttribute(writer, sessionContext, themeFileId, -1018465893);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0008D21C File Offset: 0x0008B41C
		internal static void RenderImageAltAttribute(TextWriter writer, ISessionContext sessionContext, ThemeFileId themeFileId, Strings.IDs tooltipStringId)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (tooltipStringId == -1018465893)
			{
				Strings.IDs ds;
				if (Utilities.altTable.TryGetValue(themeFileId, out ds))
				{
					tooltipStringId = ds;
				}
				Utilities.RenderImageAltOrTitleAttribute(writer, sessionContext, tooltipStringId, true);
				return;
			}
			Utilities.RenderImageAltOrTitleAttribute(writer, sessionContext, tooltipStringId, false);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0008D264 File Offset: 0x0008B464
		private static void RenderImageAltOrTitleAttribute(TextWriter writer, ISessionContext sessionContext, Strings.IDs altId, bool useAlt)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (altId != -1018465893)
			{
				if (useAlt)
				{
					writer.Write("alt=\"");
				}
				else
				{
					writer.Write("title=\"");
				}
				writer.Write(SanitizedHtmlString.FromStringId(altId, sessionContext.UserCulture));
				writer.Write("\"");
				return;
			}
			if (useAlt)
			{
				writer.Write("alt=\"\"");
			}
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0008D2CE File Offset: 0x0008B4CE
		internal static bool IsFatalFreeBusyError(Exception exception)
		{
			return exception != null && !(exception is WorkingHoursXmlMalformedException);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0008D2E1 File Offset: 0x0008B4E1
		internal static bool IsCustomRoutingType(string routingAddress, string routingType)
		{
			return !string.IsNullOrEmpty(routingAddress) && !string.IsNullOrEmpty(routingType) && string.CompareOrdinal(routingType, "EX") != 0 && string.CompareOrdinal(routingType, "SMTP") != 0 && string.CompareOrdinal(routingType, "MAPIPDL") != 0;
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0008D320 File Offset: 0x0008B520
		internal static string RemoveHTMLDirectionCharacters(string input)
		{
			return input.Replace(Globals.HtmlDirectionCharacterString, null);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0008D330 File Offset: 0x0008B530
		internal static bool MakeModifiedCalendarItemOccurrence(Item item)
		{
			CalendarItemOccurrence calendarItemOccurrence = item as CalendarItemOccurrence;
			if (calendarItemOccurrence != null && calendarItemOccurrence.CalendarItemType != CalendarItemType.Exception)
			{
				calendarItemOccurrence.MakeModifiedOccurrence();
				return true;
			}
			return false;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0008D359 File Offset: 0x0008B559
		internal static uint GetBitMask(uint count)
		{
			return Utilities.pow2minus1[(int)((UIntPtr)count)];
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0008D364 File Offset: 0x0008B564
		internal static uint RotateLeft(uint x, uint count, uint length)
		{
			if (count < 0U || count > 32U)
			{
				throw new ArgumentException("count must be >=0 and <=32");
			}
			if (length < 0U || length > 32U)
			{
				throw new ArgumentException("length must be >=0 and <=32");
			}
			if (count > length)
			{
				throw new ArgumentException("Count must be <= length");
			}
			uint num = x >> (int)(length - count) & Utilities.GetBitMask(count);
			uint num2 = x << (int)count & Utilities.GetBitMask(length);
			return num | num2;
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0008D3C9 File Offset: 0x0008B5C9
		internal static Uri TryParseUri(string uriString)
		{
			return Utilities.TryParseUri(uriString, UriKind.Absolute);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0008D3D4 File Offset: 0x0008B5D4
		internal static Uri TryParseUri(string uriString, UriKind uriKind)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			Uri result = null;
			if (!Uri.TryCreate(uriString, uriKind, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0008D3FF File Offset: 0x0008B5FF
		internal static uint RotateRight(uint x, uint count, uint length)
		{
			return Utilities.RotateLeft(x, length - count, length);
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0008D40C File Offset: 0x0008B60C
		internal static string GetHomePageForMailboxUser(OwaContext owaContext)
		{
			string text = owaContext.MailboxIdentity.GetOWAMiniRecipient()[ADRecipientSchema.WebPage] as string;
			if (!string.IsNullOrEmpty(text))
			{
				return Redir.BuildRedirUrl(owaContext.UserContext, text);
			}
			return null;
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0008D44C File Offset: 0x0008B64C
		internal static bool CanCreateItemInFolder(UserContext userContext, OwaStoreObjectId folderOwaStoreObjectId)
		{
			bool result;
			using (Folder folder = Utilities.GetFolder<Folder>(userContext, folderOwaStoreObjectId, new PropertyDefinition[]
			{
				StoreObjectSchema.EffectiveRights
			}))
			{
				result = Utilities.CanCreateItemInFolder(folder);
			}
			return result;
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x0008D498 File Offset: 0x0008B698
		internal static bool CanCreateItemInFolder(Folder folder)
		{
			EffectiveRights folderProperty = Utilities.GetFolderProperty<EffectiveRights>(folder, StoreObjectSchema.EffectiveRights, EffectiveRights.None);
			return (folderProperty & EffectiveRights.CreateContents) == EffectiveRights.CreateContents;
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0008D4BC File Offset: 0x0008B6BC
		internal static bool CanModifyFolderProperties(Folder folder)
		{
			EffectiveRights folderProperty = Utilities.GetFolderProperty<EffectiveRights>(folder, StoreObjectSchema.EffectiveRights, EffectiveRights.None);
			return (folderProperty & EffectiveRights.Modify) != EffectiveRights.None;
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0008D4E0 File Offset: 0x0008B6E0
		internal static bool CanReadItemInFolder(Folder folder)
		{
			EffectiveRights folderProperty = Utilities.GetFolderProperty<EffectiveRights>(folder, StoreObjectSchema.EffectiveRights, EffectiveRights.None);
			return (folderProperty & EffectiveRights.Read) == EffectiveRights.Read;
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0008D500 File Offset: 0x0008B700
		internal static bool IsItemInExternalSharedInFolder(UserContext userContext, Item item)
		{
			bool result = false;
			StoreObjectId storeObjectId = null;
			try
			{
				storeObjectId = item.ParentId;
			}
			catch (InvalidOperationException)
			{
			}
			if (storeObjectId != null)
			{
				OwaStoreObjectId folderId = OwaStoreObjectId.CreateFromSessionFolderId(userContext, item.Session, storeObjectId);
				result = Utilities.IsExternalSharedInFolder(userContext, folderId);
			}
			return result;
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0008D548 File Offset: 0x0008B748
		internal static bool IsExternalSharedInFolder(UserContext userContext, OwaStoreObjectId folderId)
		{
			bool result = false;
			try
			{
				using (Folder folder = Utilities.GetFolder<Folder>(userContext, folderId, new PropertyDefinition[]
				{
					FolderSchema.ExtendedFolderFlags
				}))
				{
					result = Utilities.IsExternalSharedInFolder(folder);
				}
			}
			catch (ObjectNotFoundException)
			{
			}
			return result;
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0008D5A4 File Offset: 0x0008B7A4
		internal static bool IsExternalSharedInFolder(Folder folder)
		{
			return Utilities.IsCrossOrgFolder(folder) || Utilities.IsWebCalendarFolder(folder);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0008D5B8 File Offset: 0x0008B7B8
		internal static bool IsExternalSharedInFolder(object extendedFolderFlags)
		{
			return Utilities.IsOneOfTheFolderFlagsSet(extendedFolderFlags, new ExtendedFolderFlags[]
			{
				ExtendedFolderFlags.ExchangeCrossOrgShareFolder,
				ExtendedFolderFlags.WebCalFolder
			});
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0008D5E4 File Offset: 0x0008B7E4
		internal static bool IsCrossOrgFolder(Folder folder)
		{
			return Utilities.IsOneOfTheFolderFlagsSet(folder, new ExtendedFolderFlags[]
			{
				ExtendedFolderFlags.ExchangeCrossOrgShareFolder
			});
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0008D608 File Offset: 0x0008B808
		internal static bool IsWebCalendarFolder(Folder folder)
		{
			return Utilities.IsOneOfTheFolderFlagsSet(folder, new ExtendedFolderFlags[]
			{
				ExtendedFolderFlags.WebCalFolder
			});
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0008D62C File Offset: 0x0008B82C
		internal static bool IsWebCalendarFolder(object extendedFolderFlags)
		{
			return Utilities.IsOneOfTheFolderFlagsSet(extendedFolderFlags, new ExtendedFolderFlags[]
			{
				ExtendedFolderFlags.WebCalFolder
			});
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0008D650 File Offset: 0x0008B850
		internal static bool IsPublishedOutFolder(Folder folder)
		{
			return Utilities.IsOneOfTheFolderFlagsSet(folder, new ExtendedFolderFlags[]
			{
				ExtendedFolderFlags.ExchangePublishedCalendar
			});
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0008D674 File Offset: 0x0008B874
		internal static bool IsPublishedOutFolder(object extendedFolderFlags)
		{
			return Utilities.IsOneOfTheFolderFlagsSet(extendedFolderFlags, new ExtendedFolderFlags[]
			{
				ExtendedFolderFlags.ExchangePublishedCalendar
			});
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0008D698 File Offset: 0x0008B898
		internal static string GetMailboxFolderDisplayName(DefaultFolderType folderType, MailboxSession mailboxSession, string valueOfDisplayNameProperty, bool shouldAddSessionName)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			string result;
			if (folderType == DefaultFolderType.Root)
			{
				result = Utilities.GetMailboxOwnerDisplayName(mailboxSession);
			}
			else if (folderType == DefaultFolderType.ToDoSearch)
			{
				result = LocalizedStrings.GetNonEncoded(-1954334922);
			}
			else if (folderType == DefaultFolderType.SearchFolders)
			{
				result = LocalizedStrings.GetNonEncoded(1545482161);
			}
			else if (shouldAddSessionName)
			{
				result = string.Format(LocalizedStrings.GetNonEncoded(-83764036), Utilities.GetMailboxOwnerDisplayName(mailboxSession), valueOfDisplayNameProperty);
			}
			else
			{
				result = valueOfDisplayNameProperty;
			}
			return result;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0008D704 File Offset: 0x0008B904
		internal static string GetMailboxOwnerDisplayName(MailboxSession mailboxSession)
		{
			if (mailboxSession.MailboxOwner.MailboxInfo.IsArchive)
			{
				string archiveName = mailboxSession.MailboxOwner.MailboxInfo.ArchiveName;
				if (!string.IsNullOrEmpty(archiveName))
				{
					return archiveName;
				}
			}
			else
			{
				bool isAggregated = mailboxSession.MailboxOwner.MailboxInfo.IsAggregated;
			}
			return mailboxSession.MailboxOwner.MailboxInfo.DisplayName;
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0008D760 File Offset: 0x0008B960
		internal static string GetDisplayNameByFolder(Folder folder, UserContext userContext)
		{
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			if (!Utilities.IsPublic(folder))
			{
				MailboxSession mailboxSession = folder.Session as MailboxSession;
				return Utilities.GetMailboxFolderDisplayName(mailboxSession.IsDefaultFolderType(folder.Id), mailboxSession, folder.DisplayName, !userContext.IsInMyMailbox(folder));
			}
			if (userContext.IsPublicFolderRootId(folder.Id.ObjectId))
			{
				return LocalizedStrings.GetNonEncoded(-1116491328);
			}
			return folder.DisplayName;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0008D7D6 File Offset: 0x0008B9D6
		internal static string[] ParseRecipientChunk(string address)
		{
			return ParseRecipientHelper.ParseRecipientChunk(address);
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0008D7E0 File Offset: 0x0008B9E0
		internal static AvailabilityQueryResult ExecuteAvailabilityQuery(AvailabilityQuery availabilityQuery)
		{
			if (availabilityQuery == null)
			{
				throw new ArgumentNullException("availabilityQuery");
			}
			AvailabilityQueryResult result = null;
			UserContext userContext = OwaContext.Current.UserContext;
			Stopwatch watch = Utilities.StartWatch();
			int numLocksToRestore = 0;
			try
			{
				userContext.DangerousBeginUnlockedAction(true, out numLocksToRestore);
				Utilities.ExecuteAvailabilityQuery(OwaContext.Current, availabilityQuery, out result);
			}
			finally
			{
				if (!userContext.DangerousEndUnlockedAction(true, numLocksToRestore))
				{
					throw new OwaInvalidOperationException("UC went inactive while doing UC lock free operation, terminating request");
				}
			}
			Utilities.StopWatch(watch, "Utilities.ExecuteAvailabilityQuery (Execute Availability Query)");
			return result;
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0008D85C File Offset: 0x0008BA5C
		internal static bool ExecuteAvailabilityQuery(OwaContext owaContext, AvailabilityQuery query, out AvailabilityQueryResult result)
		{
			return Utilities.ExecuteAvailabilityQuery(owaContext, query, false, false, out result);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0008D868 File Offset: 0x0008BA68
		internal static bool ExecuteAvailabilityQuery(OwaContext owaContext, AvailabilityQuery query, bool expectFreeBusyResults, out AvailabilityQueryResult result)
		{
			return Utilities.ExecuteAvailabilityQuery(owaContext, query, expectFreeBusyResults, false, out result);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0008D874 File Offset: 0x0008BA74
		internal static bool ExecuteAvailabilityQuery(OwaContext owaContext, AvailabilityQuery query, bool expectFreeBusyResults, bool expectMergedFreeBusyResults, out AvailabilityQueryResult result)
		{
			result = null;
			LatencyDetectionContext latencyDetectionContext = Utilities.OwaAvailabilityContextFactory.CreateContext(Globals.ApplicationVersion, owaContext.HttpContext.Request.Url.PathAndQuery, new IPerformanceDataProvider[0]);
			try
			{
				if (string.IsNullOrEmpty(query.ClientContext.MessageId))
				{
					query.ClientContext.MessageId = AvailabilityQuery.CreateNewMessageId();
				}
				query.RequestLogger.StartLog();
				result = query.Execute();
			}
			catch (ClientDisconnectedException)
			{
				return false;
			}
			catch
			{
				ExTraceGlobals.CalendarTracer.TraceDebug(0L, "The availability query threw exception.");
				PerformanceCounterManager.AddAvailabilityServiceResult(false);
				throw;
			}
			finally
			{
				query.RequestLogger.EndLog();
				query.RequestLogger.LogToResponse(owaContext.HttpContext.Response);
				latencyDetectionContext.StopAndFinalizeCollection();
				owaContext.AvailabilityQueryCount += 1U;
				owaContext.AvailabilityQueryLatency += (long)latencyDetectionContext.Elapsed.TotalMilliseconds;
			}
			if (result == null)
			{
				ExTraceGlobals.CalendarTracer.TraceDebug(0L, "The availability query returned a null result.");
				PerformanceCounterManager.AddAvailabilityServiceResult(false);
				return false;
			}
			if (expectFreeBusyResults)
			{
				if (result.FreeBusyResults == null)
				{
					string message = "The availability query returned no FreeBusy result.";
					ExTraceGlobals.CalendarTracer.TraceDebug(0L, message);
					PerformanceCounterManager.AddAvailabilityServiceResult(false);
					return false;
				}
				if (result.FreeBusyResults.Length != 1)
				{
					string formatString = "The availability query returned the wrong number ({0}) of FreeBusy results.";
					ExTraceGlobals.CalendarTracer.TraceDebug<int>(0L, formatString, result.FreeBusyResults.Length);
					PerformanceCounterManager.AddAvailabilityServiceResult(false);
					return false;
				}
				FreeBusyQueryResult freeBusyQueryResult = result.FreeBusyResults[0];
				if (freeBusyQueryResult != null && Utilities.IsFatalFreeBusyError(freeBusyQueryResult.ExceptionInfo))
				{
					ExTraceGlobals.CalendarTracer.TraceDebug<LocalizedException>(0L, "An error happened trying to get free/busy info. Exception: {0}", freeBusyQueryResult.ExceptionInfo);
					PerformanceCounterManager.AddAvailabilityServiceResult(false);
					return false;
				}
				if (expectMergedFreeBusyResults && (freeBusyQueryResult == null || freeBusyQueryResult.MergedFreeBusy == null))
				{
					ExTraceGlobals.CalendarTracer.TraceDebug(0L, "The availability query returned no MergedFreeBusy result.");
					PerformanceCounterManager.AddAvailabilityServiceResult(false);
					return false;
				}
			}
			PerformanceCounterManager.AddAvailabilityServiceResult(true);
			return true;
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0008DA70 File Offset: 0x0008BC70
		internal static List<OWARecipient> LoadAndSortDistributionListMembers(IADDistributionList distributionList, bool fetchCert)
		{
			if (distributionList == null)
			{
				throw new ArgumentNullException("distributionList");
			}
			ADRecipient adrecipient = distributionList as ADRecipient;
			if (adrecipient != null)
			{
				if (adrecipient.RecipientType == RecipientType.DynamicDistributionGroup)
				{
					return new List<OWARecipient>(0);
				}
				ADGroup adgroup = adrecipient as ADGroup;
				if (adgroup == null)
				{
					throw new InvalidOperationException("AD DL object which type is not dynamic DL must be ADGroup class");
				}
				if (adgroup.HiddenGroupMembershipEnabled)
				{
					return new List<OWARecipient>(0);
				}
			}
			int pageSize = 10000;
			PropertyDefinition[] array = fetchCert ? Utilities.smimeDistributionListMemberPropertyDefinitions : Utilities.distributionListMemberPropertyDefinitions;
			ADPagedReader<ADRawEntry> adpagedReader = distributionList.Expand(pageSize, array);
			List<OWARecipient> result;
			using (IEnumerator<ADRawEntry> enumerator = adpagedReader.GetEnumerator())
			{
				List<OWARecipient> list = new List<OWARecipient>(adpagedReader.LastRetrievedCount);
				while (enumerator.MoveNext())
				{
					ADRawEntry adrawEntry = enumerator.Current;
					object[] properties = adrawEntry.GetProperties(array);
					OWARecipient owarecipient = new OWARecipient();
					owarecipient.Id = (properties[0] as ADObjectId);
					if (owarecipient.Id != null)
					{
						owarecipient.DisplayName = (properties[1] as string);
						owarecipient.PhoneticDisplayName = (properties[2] as string);
						owarecipient.UserRecipientType = (RecipientType)properties[3];
						owarecipient.Alias = (properties[4] as string);
						owarecipient.LegacyDN = (properties[5] as string);
						if (fetchCert && !owarecipient.IsDistributionList)
						{
							owarecipient.HasValidDigitalId = (Utilities.GetADRecipientCertificate(enumerator.Current, false) != null);
						}
						list.Add(owarecipient);
					}
				}
				list.Sort();
				result = list;
			}
			return result;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0008DBEC File Offset: 0x0008BDEC
		internal static List<OWARecipient> LoadAndSortDistributionListMembers(IADDistributionList distributionList)
		{
			return Utilities.LoadAndSortDistributionListMembers(distributionList, false);
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0008DBF8 File Offset: 0x0008BDF8
		internal static string GetMultiCalendarFreeBusyDataForDatePicker(Duration timeWindow, OwaStoreObjectId[] calendarFolderOwaIds, UserContext userContext)
		{
			if (timeWindow == null)
			{
				throw new ArgumentNullException("timeWindow");
			}
			if (calendarFolderOwaIds == null)
			{
				throw new ArgumentNullException("calendarFolderOwaIds");
			}
			if (calendarFolderOwaIds.Length == 0)
			{
				throw new ArgumentException("Must pass at least one folder id");
			}
			string[] array = new string[calendarFolderOwaIds.Length];
			for (int i = 0; i < calendarFolderOwaIds.Length; i++)
			{
				array[i] = Utilities.GetFreeBusyDataForDatePicker(timeWindow, calendarFolderOwaIds[i], userContext);
			}
			int length = array[0].Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int j = 0; j < length; j++)
			{
				char value = '0';
				foreach (string text in array)
				{
					if (text[j] != '0')
					{
						value = text[j];
						break;
					}
				}
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0008DCC0 File Offset: 0x0008BEC0
		internal static string GetFreeBusyDataForDatePicker(Duration timeWindow, OwaStoreObjectId calendarFolderId, UserContext userContext)
		{
			if (calendarFolderId.IsGSCalendar)
			{
				return "000000000000000000000000000000000000000000";
			}
			int num = 42;
			timeWindow.EndTime.Date - timeWindow.StartTime.Date;
			CalendarEvent[] array = null;
			using (Folder folder = Utilities.GetFolder<Folder>(userContext, calendarFolderId, new PropertyDefinition[0]))
			{
				CalendarFolder calendarFolder = folder as CalendarFolder;
				if (calendarFolder == null)
				{
					return "000000000000000000000000000000000000000000";
				}
				StorePropertyDefinition[] properties = new StorePropertyDefinition[]
				{
					CalendarItemInstanceSchema.StartTime,
					CalendarItemInstanceSchema.EndTime,
					CalendarItemBaseSchema.FreeBusyStatus
				};
				DateRange[] dateRanges = new DateRange[]
				{
					new DateRange(new ExDateTime(userContext.TimeZone, timeWindow.StartTime), new ExDateTime(userContext.TimeZone, timeWindow.EndTime))
				};
				CalendarDataSource calendarDataSource = new CalendarDataSource(userContext, calendarFolder, dateRanges, properties);
				if (calendarDataSource.Count > 0)
				{
					array = new CalendarEvent[calendarDataSource.Count];
					for (int i = 0; i < calendarDataSource.Count; i++)
					{
						array[i] = new CalendarEvent();
						array[i].StartTime = (DateTime)calendarDataSource.GetStartTime(i);
						array[i].EndTime = (DateTime)calendarDataSource.GetEndTime(i);
						object busyType = calendarDataSource.GetBusyType(i);
						if (!(busyType is int) || (int)busyType < 0 || (int)busyType > 3)
						{
							ExTraceGlobals.CalendarTracer.TraceDebug<DateTime, DateTime, object>(0L, "Calendar event with start time {1} and end time {2} has invalid busy type: {3}. This is being returned as BusyType.Tentative", array[i].StartTime, array[i].EndTime, busyType);
							array[i].BusyType = Microsoft.Exchange.InfoWorker.Common.Availability.BusyType.Tentative;
						}
						else
						{
							array[i].BusyType = (Microsoft.Exchange.InfoWorker.Common.Availability.BusyType)busyType;
						}
					}
				}
			}
			if (array == null)
			{
				return "000000000000000000000000000000000000000000";
			}
			FreeBusyQueryResult freeBusyQueryResult = new FreeBusyQueryResult(FreeBusyViewType.FreeBusy, array, null, null);
			string text = freeBusyQueryResult.GetFreeBusyByDay(timeWindow, userContext.TimeZone);
			if (text == null)
			{
				ExTraceGlobals.CalendarTracer.TraceDebug(0L, "Free/Busy string is invalid.");
				throw new OwaEventHandlerException("Free/Busy string is invalid", LocalizedStrings.GetNonEncoded(-868715791));
			}
			if (text.Length > num)
			{
				text = text.Substring(0, num);
			}
			return text;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0008DEF8 File Offset: 0x0008C0F8
		internal static bool ShouldSendChangeKeyForException(Exception exception)
		{
			return exception is MessageSubmissionExceededException || exception is AttachmentExceededException || exception is SendAsDeniedException || exception is FolderSaveException || exception is RecurrenceFormatException || exception is CorruptDataException || exception is OccurrenceCrossingBoundaryException || exception is OccurrenceTimeSpanTooBigException || exception is RecurrenceEndDateTooBigException || exception is RecurrenceStartDateTooSmallException || exception is RecurrenceHasNoOccurrenceException || exception is MessageTooBigException || exception is SubmissionQuotaExceededException;
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0008DF70 File Offset: 0x0008C170
		internal static bool ShouldSuppressReadReceipt(UserContext userContext)
		{
			return userContext.UserOptions.ReadReceipt != Microsoft.Exchange.Clients.Owa.Core.ReadReceiptResponse.AlwaysSend;
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0008DF83 File Offset: 0x0008C183
		internal static bool ShouldSuppressReadReceipt(UserContext userContext, Item item)
		{
			return Utilities.ShouldSuppressReadReceipt(userContext) || JunkEmailUtilities.IsInJunkEmailFolder(item, false, userContext) || Utilities.IsPublic(item);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0008DF9F File Offset: 0x0008C19F
		internal static bool ShouldSuppressReadReceipt(UserContext userContext, OwaStoreObjectId itemId)
		{
			return Utilities.ShouldSuppressReadReceipt(userContext) || JunkEmailUtilities.IsInJunkEmailFolder(itemId, userContext) || itemId.IsPublic;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0008DFBA File Offset: 0x0008C1BA
		internal static void BasicMarkUserMailboxItemsAsRead(UserContext userContext, StoreObjectId[] sourceIds, JunkEmailStatus junkEmailStatus, bool markUnread)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (sourceIds == null)
			{
				throw new ArgumentNullException("sourceIds");
			}
			Utilities.MarkItemsAsRead(userContext.MailboxSession, sourceIds, junkEmailStatus, markUnread, userContext);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0008DFE8 File Offset: 0x0008C1E8
		internal static void MarkItemsAsRead(UserContext userContext, OwaStoreObjectId[] sourceIds, JunkEmailStatus junkEmailStatus, bool markUnread)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (sourceIds == null)
			{
				throw new ArgumentNullException("sourceIds");
			}
			if (sourceIds.Length == 0)
			{
				throw new ArgumentOutOfRangeException("sourceIds", "sourceIds should contain more than one element");
			}
			StoreSession session = sourceIds[0].GetSession(userContext);
			Utilities.MarkItemsAsRead(session, OwaStoreObjectId.ConvertToStoreObjectIdArray(sourceIds), junkEmailStatus, markUnread, userContext);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0008E040 File Offset: 0x0008C240
		private static void MarkItemsAsRead(StoreSession storeSession, StoreObjectId[] sourceIds, JunkEmailStatus junkEmailStatus, bool markUnread, UserContext userContext)
		{
			StoreObjectId[] array = null;
			StoreObjectId[] array2 = null;
			if (storeSession is PublicFolderSession || userContext.IsOtherMailbox(storeSession))
			{
				array = sourceIds;
			}
			else
			{
				switch (junkEmailStatus)
				{
				case JunkEmailStatus.NotJunk:
					array2 = sourceIds;
					break;
				case JunkEmailStatus.Junk:
					array = sourceIds;
					break;
				case JunkEmailStatus.Unknown:
					JunkEmailUtilities.SortJunkEmailIds(userContext, sourceIds, out array, out array2);
					break;
				}
			}
			if (array != null && array.Length > 0)
			{
				if (markUnread)
				{
					storeSession.MarkAsUnread(true, array);
				}
				else
				{
					storeSession.MarkAsRead(true, array);
				}
			}
			if (array2 != null && array2.Length > 0)
			{
				if (markUnread)
				{
					storeSession.MarkAsUnread(Utilities.ShouldSuppressReadReceipt(userContext), array2);
					return;
				}
				storeSession.MarkAsRead(Utilities.ShouldSuppressReadReceipt(userContext), array2);
			}
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0008E0D8 File Offset: 0x0008C2D8
		internal static AggregateOperationResult DeleteItems(UserContext userContext, DeleteItemFlags deleteItemFlags, params StoreId[] storeIds)
		{
			if (storeIds.Length == 0)
			{
				return new AggregateOperationResult(OperationResult.Succeeded, new GroupOperationResult[0]);
			}
			if (!IdConverter.IsMessageId(StoreId.GetStoreObjectId(storeIds[0])))
			{
				throw new ArgumentException("store Ids is not an item");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			Utilities.AbortSubmissionsBeforeDelete(userContext.MailboxSession, storeIds);
			AggregateOperationResult result = userContext.MailboxSession.Delete(deleteItemFlags, storeIds);
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.ItemsDeleted.IncrementBy((long)storeIds.Length);
			}
			return result;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0008E150 File Offset: 0x0008C350
		internal static AggregateOperationResult DeleteFolders(MailboxSession mailboxSession, DeleteItemFlags deleteItemFlags, params StoreId[] folderIds)
		{
			if (folderIds.Length == 0)
			{
				return new AggregateOperationResult(OperationResult.Succeeded, new GroupOperationResult[0]);
			}
			if (!IdConverter.IsFolderId(StoreId.GetStoreObjectId(folderIds[0])))
			{
				throw new ArgumentException("store Ids is not an folder");
			}
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			return mailboxSession.Delete(deleteItemFlags, folderIds);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0008E1A1 File Offset: 0x0008C3A1
		internal static AggregateOperationResult Delete(UserContext userContext, bool permanentDelete, params OwaStoreObjectId[] objectIds)
		{
			return Utilities.Delete(userContext, permanentDelete, DeleteItemFlags.None, objectIds);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0008E1AC File Offset: 0x0008C3AC
		internal static void Delete(UserContext userContext, bool permanentDelete, bool doThrow, params OwaStoreObjectId[] objectIds)
		{
			OperationResult operationResult = Utilities.Delete(userContext, permanentDelete, objectIds).OperationResult;
			if (operationResult != OperationResult.Succeeded && doThrow)
			{
				throw new OwaEventHandlerException("Deleting an item fails", LocalizedStrings.GetNonEncoded(1167467453), true);
			}
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0008E1E4 File Offset: 0x0008C3E4
		internal static AggregateOperationResult Delete(UserContext userContext, bool permanentDelete, DeleteItemFlags extraDeleteFlags, params OwaStoreObjectId[] objectIds)
		{
			if (objectIds.Length == 0)
			{
				return new AggregateOperationResult(OperationResult.Succeeded, new GroupOperationResult[0]);
			}
			DeleteItemFlags deleteItemFlags = permanentDelete ? DeleteItemFlags.SoftDelete : DeleteItemFlags.MoveToDeletedItems;
			deleteItemFlags |= extraDeleteFlags;
			return Utilities.Delete(userContext, deleteItemFlags, objectIds);
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0008E218 File Offset: 0x0008C418
		internal static AggregateOperationResult Delete(UserContext userContext, DeleteItemFlags deleteItemFlags, params OwaStoreObjectId[] objectIds)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (objectIds == null)
			{
				throw new ArgumentNullException("objectIds");
			}
			if (objectIds.Length == 0)
			{
				return new AggregateOperationResult(OperationResult.Succeeded, new GroupOperationResult[0]);
			}
			StoreSession session = objectIds[0].GetSession(userContext);
			if (session is MailboxSession && IdConverter.IsMessageId(objectIds[0].StoreObjectId))
			{
				Utilities.AbortSubmissionsBeforeDelete(session as MailboxSession, OwaStoreObjectId.ConvertToStoreObjectIdArray(objectIds));
			}
			AggregateOperationResult aggregateOperationResult;
			if (!objectIds[0].IsOtherMailbox)
			{
				if (objectIds.Length == 1)
				{
					aggregateOperationResult = session.Delete(deleteItemFlags, new StoreId[]
					{
						objectIds[0].StoreObjectId
					});
				}
				else
				{
					StoreObjectId[] ids = OwaStoreObjectId.ConvertToStoreObjectIdArray(objectIds);
					aggregateOperationResult = session.Delete(deleteItemFlags, ids);
				}
			}
			else
			{
				List<OwaStoreObjectId> list = new List<OwaStoreObjectId>(objectIds.Length);
				List<OwaStoreObjectId> list2 = new List<OwaStoreObjectId>(objectIds.Length);
				foreach (OwaStoreObjectId owaStoreObjectId in objectIds)
				{
					if (owaStoreObjectId.StoreObjectId is OccurrenceStoreObjectId)
					{
						list2.Add(owaStoreObjectId);
					}
					else
					{
						list.Add(owaStoreObjectId);
					}
				}
				AggregateOperationResult first = session.Delete(deleteItemFlags, OwaStoreObjectId.ConvertToStoreObjectIdArray(list2.ToArray()));
				AggregateOperationResult second = Utilities.CopyOrMoveItems(userContext, false, Utilities.TryGetDefaultFolderId(userContext, userContext.MailboxSession, DefaultFolderType.DeletedItems), new DeleteItemFlags?(deleteItemFlags), list.ToArray());
				aggregateOperationResult = AggregateOperationResult.Merge(first, second);
			}
			if (aggregateOperationResult.OperationResult == OperationResult.Succeeded && !Folder.IsFolderId(objectIds[0].StoreObjectId) && Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.ItemsDeleted.IncrementBy((long)objectIds.Length);
			}
			return aggregateOperationResult;
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0008E390 File Offset: 0x0008C590
		internal static void AbortSubmissionsBeforeDelete(MailboxSession mailboxSession, StoreId[] itemIds)
		{
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts);
			StoreObjectId defaultFolderId2 = mailboxSession.GetDefaultFolderId(DefaultFolderType.Outbox);
			foreach (StoreId storeId in itemIds)
			{
				if (IdConverter.GetParentIdFromMessageId(StoreId.GetStoreObjectId(storeId)).Equals(defaultFolderId) || IdConverter.GetParentIdFromMessageId(StoreId.GetStoreObjectId(storeId)).Equals(defaultFolderId2))
				{
					using (Item item = Utilities.GetItem<Item>(mailboxSession, storeId, false, new PropertyDefinition[]
					{
						MessageItemSchema.HasBeenSubmitted
					}))
					{
						if (item is MessageItem)
						{
							MessageItem messageItem = (MessageItem)item;
							if (messageItem.GetValueOrDefault<bool>(MessageItemSchema.HasBeenSubmitted))
							{
								messageItem.AbortSubmit();
							}
						}
					}
				}
			}
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0008E450 File Offset: 0x0008C650
		internal static AggregateOperationResult CopyOrMoveItems(UserContext userContext, bool isCopy, OwaStoreObjectId destinationFolderId, params OwaStoreObjectId[] ids)
		{
			return Utilities.CopyOrMoveItems(userContext, isCopy, destinationFolderId, null, ids);
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0008E470 File Offset: 0x0008C670
		internal static AggregateOperationResult CopyOrMoveItems(UserContext userContext, bool isCopy, OwaStoreObjectId destinationFolderId, DeleteItemFlags? deleteFlags, params OwaStoreObjectId[] ids)
		{
			if (destinationFolderId == null)
			{
				throw new ArgumentNullException("destinationFolderId");
			}
			if (ids == null)
			{
				throw new ArgumentNullException("ids");
			}
			if (ids.Length == 0)
			{
				return new AggregateOperationResult(OperationResult.Succeeded, new GroupOperationResult[0]);
			}
			StoreSession sessionForFolderContent = destinationFolderId.GetSessionForFolderContent(userContext);
			StoreSession session = ids[0].GetSession(userContext);
			StoreObjectId[] ids2 = OwaStoreObjectId.ConvertToStoreObjectIdArray(ids);
			if (isCopy)
			{
				return session.Copy(sessionForFolderContent, destinationFolderId.StoreObjectId, ids2);
			}
			return session.Move(sessionForFolderContent, destinationFolderId.StoreObjectId, deleteFlags, ids2);
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0008E4EC File Offset: 0x0008C6EC
		internal static AggregateOperationResult CopyOrMoveFolder(UserContext userContext, bool isCopy, OwaStoreObjectId destinationFolderId, params OwaStoreObjectId[] sourceFolderIds)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (destinationFolderId == null)
			{
				throw new ArgumentNullException("destinationFolderId");
			}
			if (sourceFolderIds == null)
			{
				throw new ArgumentNullException("sourceFolderIds");
			}
			if (sourceFolderIds.Length == 0)
			{
				return new AggregateOperationResult(OperationResult.Succeeded, new GroupOperationResult[0]);
			}
			return Utilities.GetSessionsForCopyOrMoveFolder(userContext, destinationFolderId, sourceFolderIds, isCopy);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0008E540 File Offset: 0x0008C740
		private static AggregateOperationResult GetSessionsForCopyOrMoveFolder(UserContext userContext, OwaStoreObjectId destinationFolderId, OwaStoreObjectId[] sourceFolderIds, bool isCopy)
		{
			StoreSession storeSession;
			StoreSession destinationSession;
			if (isCopy || (!isCopy && sourceFolderIds[0].IsPublic != destinationFolderId.IsPublic))
			{
				storeSession = sourceFolderIds[0].GetSessionForFolderContent(userContext);
				destinationSession = destinationFolderId.GetSession(userContext);
			}
			else if (!isCopy && sourceFolderIds[0].IsPublic && destinationFolderId.IsPublic)
			{
				storeSession = sourceFolderIds[0].GetSession(userContext);
				destinationSession = storeSession;
			}
			else
			{
				storeSession = sourceFolderIds[0].GetSession(userContext);
				destinationSession = destinationFolderId.GetSession(userContext);
			}
			StoreObjectId[] ids = OwaStoreObjectId.ConvertToStoreObjectIdArray(sourceFolderIds);
			if (isCopy)
			{
				return storeSession.Copy(destinationSession, destinationFolderId.StoreObjectId, ids);
			}
			return storeSession.Move(destinationSession, destinationFolderId.StoreObjectId, ids);
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0008E5D8 File Offset: 0x0008C7D8
		internal static Folder CreateSubFolder(OwaStoreObjectId destinationFolderId, StoreObjectType folderType, string folderName, UserContext userContext)
		{
			if (destinationFolderId == null)
			{
				throw new ArgumentNullException("destinationFolderId");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (string.IsNullOrEmpty(folderName))
			{
				throw new ArgumentNullException("folderName");
			}
			StoreSession session = destinationFolderId.GetSession(userContext);
			return Folder.Create(session, destinationFolderId.StoreObjectId, folderType, folderName, CreateMode.CreateNew);
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0008E62B File Offset: 0x0008C82B
		public static string GetDefaultCultureFontCssFileUrl(OwaContext owaContext)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			return Microsoft.Exchange.Clients.Owa.Core.Culture.GetDefaultCultureCssFontFileName(owaContext);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0008E641 File Offset: 0x0008C841
		public static string GetFontCssFileUrlForUICulture()
		{
			return Microsoft.Exchange.Clients.Owa.Core.Culture.GetCssFontFileNameFromCulture();
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0008E648 File Offset: 0x0008C848
		internal static void JunkEmailRuleSynchronizeContactsCache(JunkEmailRule junkEmailRule)
		{
			try
			{
				junkEmailRule.SynchronizeContactsCache();
			}
			catch (JunkEmailValidationException)
			{
				ExTraceGlobals.CoreTracer.TraceDebug(0L, "JunkEmailValidationException was thrown by JunkEmailRule.SynchronizeContactsCache method");
			}
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0008E684 File Offset: 0x0008C884
		internal static ADRecipient GetRecipientByLegacyExchangeDN(IRecipientSession session, string legacyExchangeDN)
		{
			if (string.IsNullOrEmpty(legacyExchangeDN))
			{
				ExTraceGlobals.CoreTracer.TraceDebug(0L, "Utilities.GetRecipientByLegacyExchangeDN: legacyExchangeDN is null or empty");
				return null;
			}
			ADRecipient result = null;
			try
			{
				result = session.FindByLegacyExchangeDN(legacyExchangeDN);
			}
			catch (NonUniqueRecipientException arg)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<NonUniqueRecipientException>(0L, "Utilities.GetRecipientByLegacyExchangeDN: NonUniqueRecipientException was thrown by FindByLegacyExchangeDN: {0}", arg);
			}
			return result;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0008E6E0 File Offset: 0x0008C8E0
		internal static ADRawEntry GetAdRecipientByLegacyExchangeDN(IRecipientSession session, string legacyExchangeDN)
		{
			if (string.IsNullOrEmpty(legacyExchangeDN))
			{
				ExTraceGlobals.CoreTracer.TraceDebug(0L, "Utilities.GetAdRecipientByLegacyExchangeDN: legacyExchangeDN is null or empty");
				return null;
			}
			Result<ADRawEntry>[] array = null;
			string[] legacyExchangeDNs = new string[]
			{
				legacyExchangeDN
			};
			try
			{
				array = session.FindByLegacyExchangeDNs(legacyExchangeDNs, Utilities.adFindByExchangeLegacyDnProperties);
			}
			catch (NonUniqueRecipientException arg)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<NonUniqueRecipientException>(0L, "Utilities.GetAdRecipientByLegacyExchangeDN: NonUniqueRecipientException was thrown by FindByLegacyExchangeDN: {0}", arg);
			}
			if (array == null || array.Length <= 0)
			{
				return null;
			}
			return array[0].Data;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0008E764 File Offset: 0x0008C964
		internal static ADRecipient CreateADRecipientFromProxyAddress(ADObjectId objectId, string routingAddress, IRecipientSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			ADRecipient adrecipient = null;
			if (objectId != null)
			{
				adrecipient = session.Read(objectId);
			}
			if (adrecipient == null && !string.IsNullOrEmpty(routingAddress))
			{
				try
				{
					CustomProxyAddress proxyAddress = new CustomProxyAddress((CustomProxyAddressPrefix)ProxyAddressPrefix.LegacyDN, routingAddress, true);
					adrecipient = session.FindByProxyAddress(proxyAddress);
				}
				catch (NonUniqueRecipientException ex)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Failed to get unique recipient: Error: {0}", ex.Message);
				}
			}
			return adrecipient;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x0008E7E0 File Offset: 0x0008C9E0
		internal static ADRecipient CreateADRecipientPrimarySmtpAddress(UserContext userContext, bool readOnly, out IRecipientSession recipientSession)
		{
			recipientSession = Utilities.CreateADRecipientSession(readOnly, ConsistencyMode.IgnoreInvalid, userContext);
			SmtpProxyAddress proxyAddress = new SmtpProxyAddress(userContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), true);
			return recipientSession.FindByProxyAddress(proxyAddress);
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0008E824 File Offset: 0x0008CA24
		private static void RenderScriptTag(TextWriter writer, string fileName, int index, ScriptFlags scriptFlags)
		{
			string s;
			if (fileName == "clientstrings.aspx")
			{
				s = Utilities.GetClientStringsFileNameWithPath();
			}
			else if (fileName == "smallicons.aspx")
			{
				s = Utilities.GetSmallIconsFileNameWithPath();
			}
			else
			{
				s = Utilities.GetScriptFullPath(fileName);
			}
			writer.Write("<script id=_scr");
			writer.Write(index);
			writer.Write(" _sid=\"");
			writer.Write(fileName);
			writer.Write("\" type=\"text/javascript\"");
			if (Utilities.IsFlagSet((int)scriptFlags, 2))
			{
				writer.Write(" _src=\"");
			}
			else
			{
				writer.Write(" src=\"");
			}
			writer.Write(Utilities.SanitizeHtmlEncode(s));
			writer.Write("\"></script>\n");
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0008E8CC File Offset: 0x0008CACC
		public static string GetScriptFullPath(string fileName)
		{
			string result;
			if (fileName.EndsWith(".js", StringComparison.InvariantCultureIgnoreCase))
			{
				result = Globals.ContentDeliveryNetworkEndpoint + Utilities.PremiumScriptPath + fileName;
			}
			else
			{
				result = Utilities.PremiumScriptPath + fileName;
			}
			return result;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0008E909 File Offset: 0x0008CB09
		private static string GetClientStringsFileNameWithPath()
		{
			return "forms/premium/clientstrings.aspx?v=" + Globals.ApplicationVersion + "&l=" + CultureInfo.CurrentUICulture.Name;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0008E929 File Offset: 0x0008CB29
		private static string GetSmallIconsFileNameWithPath()
		{
			return "forms/premium/smallicons.aspx?v=" + Globals.ApplicationVersion;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0008E93A File Offset: 0x0008CB3A
		public static void RenderScriptTagStart(TextWriter writer)
		{
			writer.Write("<script type=\"text/javascript\">");
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0008E947 File Offset: 0x0008CB47
		public static void RenderScriptTagEnd(TextWriter writer)
		{
			writer.Write("</script>");
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0008E954 File Offset: 0x0008CB54
		public static void RenderScripts(TextWriter writer, ISessionContext sessionContext, ScriptFlags scriptFlags, params string[] fileNames)
		{
			Utilities.RenderScriptTagStart(writer);
			Utilities.RenderInlineScripts(writer, sessionContext);
			Utilities.RenderScriptTagEnd(writer);
			Utilities.RenderExternalScripts(writer, scriptFlags, fileNames);
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0008E971 File Offset: 0x0008CB71
		public static void RenderOWAFlag(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("var g_fOwa=1;");
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0008E98C File Offset: 0x0008CB8C
		public static void RenderBootUpScripts(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("var onWL=0;");
			writer.Write("var oJS = new Object();");
			writer.Write(" function isJS(f) {return oJS[f]?1:0;}; function stJS(f){oJS[f]=true;};");
			writer.Write(" var _wl=0;");
			writer.Write(" function _e(_this, s, event) {if (_wl) eval(s);};");
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0008E9DE File Offset: 0x0008CBDE
		public static void RenderInlineScripts(TextWriter writer, ISessionContext sessionContext)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			Utilities.RenderOWAFlag(writer);
			Utilities.RenderBootUpScripts(writer);
			Utilities.RenderGlobalJavascriptVariables(writer, sessionContext);
			Utilities.RenderScriptToEnforceUTF8ForPage(writer);
			Utilities.RenderScriptDisplayPictureOnLoad(writer);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0008EA10 File Offset: 0x0008CC10
		public static void RenderExternalScripts(TextWriter writer, ScriptFlags scriptFlags, IEnumerable<string> fileNames)
		{
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			Utilities.RenderScriptTag(writer, "clientstrings.aspx", 0, scriptFlags & ~ScriptFlags.DeferredLoading);
			if (Utilities.IsFlagSet((int)scriptFlags, 1))
			{
				Utilities.RenderScriptTag(writer, "uglobal.js", 1, scriptFlags & ~ScriptFlags.DeferredLoading);
			}
			int num = 0;
			foreach (string fileName in fileNames)
			{
				Utilities.RenderScriptTag(writer, fileName, num + 2, scriptFlags);
				num++;
			}
			if (Utilities.IsFlagSet((int)scriptFlags, 2))
			{
				Utilities.RenderScriptTagStart(writer);
				writer.Write("for (var i = 0; i < " + (num + 2) + "; i++){");
				writer.Write(" var o =window.document.getElementById(\"_scr\" + i);");
				writer.Write(" if (o && o.getAttribute(\"_src\")) o.setAttribute(\"src\",o.getAttribute(\"_src\"));");
				writer.Write("}\n");
				Utilities.RenderScriptTagEnd(writer);
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0008EAEC File Offset: 0x0008CCEC
		public static void RenderGlobalJavascriptVariables(TextWriter writer, ISessionContext sessionContext)
		{
			Utilities.RenderCDNEndpointVariable(writer);
			writer.Write(" var a_fEnbSMm=");
			writer.Write(sessionContext.IsFeatureEnabled(Feature.SMime) ? "1" : "0");
			writer.Write(";");
			UserContext userContext = sessionContext as UserContext;
			if (userContext != null)
			{
				RenderingUtilities.RenderStringVariable(writer, "a_sMailboxGuid", Utilities.JavascriptEncode(userContext.ExchangePrincipal.MailboxInfo.MailboxGuid.ToString()));
				if (userContext.IsDifferentMailbox)
				{
					RenderingUtilities.RenderInteger(writer, "a_fIsExpLgn", 1);
					RenderingUtilities.RenderStringVariable(writer, "a_sMailboxUniqueId", Utilities.JavascriptEncode(userContext.MailboxIdentity.UniqueId));
				}
				userContext.RenderCustomizedFormRegistry(writer);
			}
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0008EBA3 File Offset: 0x0008CDA3
		public static void RenderCDNEndpointVariable(TextWriter writer)
		{
			if (!string.IsNullOrEmpty(Globals.ContentDeliveryNetworkEndpoint))
			{
				writer.Write("var a_sCDN = \"");
				writer.Write(Globals.ContentDeliveryNetworkEndpoint);
				writer.Write("\";");
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0008EBD2 File Offset: 0x0008CDD2
		public static void RenderClientStrings(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<script type=\"text/javascript\" src=\"" + Utilities.GetClientStringsFileNameWithPath() + "\"></script>");
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0008EBFC File Offset: 0x0008CDFC
		public static void RenderScriptHandler(SanitizingStringBuilder<OwaHtml> stringBuilder, string eventName, string handlerCode)
		{
			if (stringBuilder == null)
			{
				throw new ArgumentNullException("stringBuilder");
			}
			stringBuilder.Append<SanitizedEventHandlerString>(Utilities.GetScriptHandler(eventName, handlerCode));
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0008EC19 File Offset: 0x0008CE19
		public static void RenderScriptHandler(TextWriter writer, string eventName, string handlerCode)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write(new SanitizedEventHandlerString(eventName, handlerCode, false));
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0008EC37 File Offset: 0x0008CE37
		public static void RenderScriptHandler(TextWriter writer, string eventName, string handlerCode, bool returnFalse)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write(new SanitizedEventHandlerString(eventName, handlerCode, returnFalse));
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0008EC55 File Offset: 0x0008CE55
		public static SanitizedEventHandlerString GetScriptHandler(string eventName, string handlerCode)
		{
			return new SanitizedEventHandlerString(eventName, handlerCode, false);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0008EC5F File Offset: 0x0008CE5F
		internal static bool IsADDistributionList(RecipientType recipientType)
		{
			return recipientType == RecipientType.Group || recipientType == RecipientType.MailUniversalDistributionGroup || recipientType == RecipientType.MailUniversalSecurityGroup || recipientType == RecipientType.MailNonUniversalGroup || recipientType == RecipientType.DynamicDistributionGroup;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0008EC79 File Offset: 0x0008CE79
		internal static bool IsADDistributionList(MultiValuedProperty<string> objectClass)
		{
			return objectClass.Contains(ADGroup.MostDerivedClass) || objectClass.Contains(ADDynamicGroup.MostDerivedClass);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0008EC98 File Offset: 0x0008CE98
		internal static string GetContentTypeString(OwaEventContentType contentType)
		{
			switch (contentType)
			{
			case OwaEventContentType.Html:
				return "text/html";
			case OwaEventContentType.Javascript:
				return "application/x-javascript";
			case OwaEventContentType.PlainText:
				return "text/plain";
			case OwaEventContentType.Css:
				return "text/css";
			case OwaEventContentType.Jpeg:
				return "image/jpeg";
			default:
				throw new ArgumentOutOfRangeException("contentType");
			}
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0008ECF0 File Offset: 0x0008CEF0
		internal static string GetItemIdQueryString(HttpRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			NameValueCollection queryString = request.QueryString;
			for (int i = 0; i < queryString.Count; i++)
			{
				string key = queryString.GetKey(i);
				if (string.CompareOrdinal(key, "id") == 0 || string.CompareOrdinal(key, "attcnt") == 0 || key.StartsWith("attid", StringComparison.Ordinal))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append('&');
					}
					stringBuilder.Append(key);
					stringBuilder.Append('=');
					stringBuilder.Append(Utilities.UrlEncode(queryString.GetValues(i)[0]));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0008ED8C File Offset: 0x0008CF8C
		internal static bool IsClearSigned(IStorePropertyBag storePropertyBag)
		{
			if (storePropertyBag == null)
			{
				throw new ArgumentNullException("storePropertyBag");
			}
			string property = ItemUtility.GetProperty<string>(storePropertyBag, StoreObjectSchema.ItemClass, string.Empty);
			return ObjectClass.IsOfClass(property, "IPM.Note.Secure.Sign") || ObjectClass.IsSmimeClearSigned(property);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0008EDD0 File Offset: 0x0008CFD0
		internal static bool IsSMime(IStorePropertyBag storePropertyBag)
		{
			if (storePropertyBag == null)
			{
				throw new ArgumentNullException("storePropertyBag");
			}
			string property = ItemUtility.GetProperty<string>(storePropertyBag, StoreObjectSchema.ItemClass, string.Empty);
			return ObjectClass.IsSmime(property);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0008EE04 File Offset: 0x0008D004
		internal static bool IsOpaqueSigned(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			string property = ItemUtility.GetProperty<string>(item, StoreObjectSchema.ItemClass, string.Empty);
			return (ObjectClass.IsOfClass(property, "IPM.Note.Secure") || (ObjectClass.IsSmime(property) && !ObjectClass.IsSmimeClearSigned(property))) && ConvertUtils.IsMessageOpaqueSigned(item);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0008EE58 File Offset: 0x0008D058
		internal static bool IsEncrypted(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			string property = ItemUtility.GetProperty<string>(item, StoreObjectSchema.ItemClass, string.Empty);
			return ObjectClass.IsOfClass(property, "IPM.Note.SMIME") && !Utilities.IsOpaqueSigned(item);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0008EE9C File Offset: 0x0008D09C
		internal static void DisconnectStoreSession(StoreSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (session.IsConnected)
			{
				session.Disconnect();
			}
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0008EEBC File Offset: 0x0008D0BC
		internal static void DisconnectStoreSessionSafe(StoreSession session)
		{
			try
			{
				Utilities.DisconnectStoreSession(session);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>(0L, "Unexpected exception in DisconnetStoreSession. Exception: {0}", ex.Message);
			}
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0008EEFC File Offset: 0x0008D0FC
		internal static void ReconnectStoreSession(StoreSession session, UserContext userContext)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			session.AccountingObject = OwaContext.TryGetCurrentBudget();
			MailboxSession mailboxSession = session as MailboxSession;
			if (!session.IsConnected)
			{
				if (mailboxSession != null)
				{
					bool flag = mailboxSession.ConnectWithStatus();
					if (flag && userContext.MapiNotificationManager != null)
					{
						userContext.MapiNotificationManager.HandleConnectionDroppedNotification();
						return;
					}
				}
				else
				{
					session.Connect();
				}
			}
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0008EF5A File Offset: 0x0008D15A
		internal static bool IsSMimeControlNeededForEditForm(string smimeParameter, OwaContext owaContext)
		{
			return Utilities.IsSMimeControlNeededForEditForm(Utilities.CheckClientSMimeControlStatus(smimeParameter, owaContext), owaContext);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0008EF69 File Offset: 0x0008D169
		public static bool IsSMimeFeatureUsable(OwaContext owaContext)
		{
			return owaContext.UserContext.IsFeatureEnabled(Feature.SMime) && owaContext.UserContext.ClientBrowserStatus == ClientBrowserStatus.IE7OrLaterIn32Bit;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0008EF8E File Offset: 0x0008D18E
		internal static bool CheckSMimeEditFormBasicRequirement(ClientSMimeControlStatus clientSMimeControlStatus, OwaContext owaContext)
		{
			return !owaContext.UserContext.IsExplicitLogonOthersMailbox && Utilities.IsFlagSet((int)clientSMimeControlStatus, 16);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0008EFA7 File Offset: 0x0008D1A7
		internal static bool IsSMimeControlNeededForEditForm(ClientSMimeControlStatus clientSMimeControlStatus, OwaContext owaContext)
		{
			return Utilities.CheckSMimeEditFormBasicRequirement(clientSMimeControlStatus, owaContext) && Utilities.IsClientSMimeControlUsable(clientSMimeControlStatus);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0008EFBC File Offset: 0x0008D1BC
		public static string ReadSMimeControlVersionOnServer()
		{
			if (string.IsNullOrEmpty(Utilities.smimeVersion))
			{
				int num = 0;
				string szDatabasePath = HttpRuntime.AppDomainAppPath + "\\smime\\owasmime.msi";
				SafeMsiHandle safeMsiHandle = null;
				SafeMsiHandle safeMsiHandle2 = null;
				SafeMsiHandle safeMsiHandle3 = null;
				StringBuilder stringBuilder = new StringBuilder(64);
				int num2 = 64;
				try
				{
					num = SafeNativeMethods.MsiOpenDatabase(szDatabasePath, 0, out safeMsiHandle);
					if (num == 0)
					{
						num = SafeNativeMethods.MsiDatabaseOpenView(safeMsiHandle, "SELECT Value FROM Property WHERE Property='ProductVersion'", out safeMsiHandle2);
					}
					if (num == 0)
					{
						using (SafeMsiHandle safeMsiHandle4 = new SafeMsiHandle(IntPtr.Zero))
						{
							num = SafeNativeMethods.MsiViewExecute(safeMsiHandle2, safeMsiHandle4);
						}
					}
					if (num == 0)
					{
						num = SafeNativeMethods.MsiViewFetch(safeMsiHandle2, out safeMsiHandle3);
					}
					if (num == 0)
					{
						num = SafeNativeMethods.MsiRecordGetString(safeMsiHandle3, 1, stringBuilder, ref num2);
					}
				}
				finally
				{
					if (safeMsiHandle3 != null)
					{
						safeMsiHandle3.Close();
					}
					if (safeMsiHandle2 != null)
					{
						safeMsiHandle2.Close();
					}
					if (safeMsiHandle != null)
					{
						safeMsiHandle.Close();
					}
					if (num != 0)
					{
						ExTraceGlobals.CoreCallTracer.TraceDebug<int>(0L, "Failed to open owasmime.msi to get version information. Error Code: {0}", num);
						using (SafeMsiHandle safeMsiHandle5 = SafeNativeMethods.MsiGetLastErrorRecord())
						{
							if (!safeMsiHandle5.IsInvalid)
							{
								StringBuilder stringBuilder2 = new StringBuilder();
								int num3 = 0;
								using (SafeMsiHandle safeMsiHandle6 = new SafeMsiHandle(IntPtr.Zero))
								{
									int num4 = SafeNativeMethods.MsiFormatRecord(safeMsiHandle6, safeMsiHandle5, stringBuilder2, ref num3);
									if (234 == num4)
									{
										num3++;
										stringBuilder2 = new StringBuilder(num3);
										if (SafeNativeMethods.MsiFormatRecord(safeMsiHandle6, safeMsiHandle5, stringBuilder2, ref num3) == 0)
										{
											ExTraceGlobals.CoreCallTracer.TraceDebug<string>(0L, "Extended error from MSI: {0}", stringBuilder2.ToString());
										}
									}
								}
							}
						}
					}
				}
				Utilities.smimeVersion = stringBuilder.ToString();
			}
			return Utilities.smimeVersion;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0008F174 File Offset: 0x0008D374
		internal static ClientSMimeControlStatus CheckClientSMimeControlStatus(string smimeParameter, OwaContext owaContext)
		{
			if (smimeParameter == null || !Utilities.IsSMimeFeatureUsable(owaContext))
			{
				return ClientSMimeControlStatus.NotInstalled;
			}
			SmimeParameterParser smimeParameterParser = new SmimeParameterParser(smimeParameter);
			if (smimeParameterParser.SmimeControlVersion == null)
			{
				return ClientSMimeControlStatus.NotInstalled;
			}
			try
			{
				ClientSMimeControlStatus clientSMimeControlStatus = ClientSMimeControlStatus.None;
				if (smimeParameterParser.ConnectionIsSSL)
				{
					clientSMimeControlStatus |= ClientSMimeControlStatus.ConnectionIsSSL;
				}
				Version v = new Version(smimeParameterParser.SmimeControlVersion);
				Version v2 = new Version(Utilities.ReadSMimeControlVersionOnServer());
				if (v < v2)
				{
					if (OwaRegistryKeys.ForceSMimeClientUpgrade)
					{
						clientSMimeControlStatus |= ClientSMimeControlStatus.MustUpdate;
					}
					else
					{
						clientSMimeControlStatus |= ClientSMimeControlStatus.Outdated;
					}
				}
				else
				{
					clientSMimeControlStatus |= ClientSMimeControlStatus.OK;
				}
				return clientSMimeControlStatus;
			}
			catch (ArgumentException)
			{
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			return ClientSMimeControlStatus.NotInstalled;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0008F220 File Offset: 0x0008D420
		public static bool IsClientSMimeControlUsable(ClientSMimeControlStatus status)
		{
			return Utilities.IsFlagSet((int)status, 8) || Utilities.IsFlagSet((int)status, 4);
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0008F234 File Offset: 0x0008D434
		public static int? GetMaximumMessageSize(UserContext userContext)
		{
			object obj = userContext.MailboxSession.Mailbox.TryGetProperty(MailboxSchema.MaxUserMessageSize);
			if (!(obj is PropertyError))
			{
				return new int?((int)obj);
			}
			return null;
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0008F274 File Offset: 0x0008D474
		public static Uri AppendSmtpAddressToUrl(Uri url, string smtpAddress)
		{
			UriBuilder uriBuilder = new UriBuilder(url);
			if (!uriBuilder.Path.EndsWith("/", StringComparison.Ordinal))
			{
				UriBuilder uriBuilder2 = uriBuilder;
				uriBuilder2.Path += "/";
			}
			UriBuilder uriBuilder3 = uriBuilder;
			uriBuilder3.Path += smtpAddress;
			return uriBuilder.Uri;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0008F2CC File Offset: 0x0008D4CC
		public static string BuildErrorMessageForFailoverRedirection(bool showErrorInDialogForOeh, Uri failoverRedirectionUrl)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(102674960));
			if (failoverRedirectionUrl != null)
			{
				stringBuilder.Append("<div");
				if (showErrorInDialogForOeh)
				{
					stringBuilder.Append(" style=\"display:none\"");
				}
				stringBuilder.Append(">");
				string text = string.Format(CultureInfo.InvariantCulture, "<span id=\"spRedirUrl\">{0}</span>", new object[]
				{
					Utilities.HtmlEncode(failoverRedirectionUrl.ToString())
				});
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, LocalizedStrings.GetHtmlEncoded(-1407904215), new object[]
				{
					text
				});
				stringBuilder.Append("</div>");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0008F37A File Offset: 0x0008D57A
		public static void RenderDirectionEnhancedValue(TextWriter output, SanitizedHtmlString value, bool isRtl)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Utilities.RenderDirectionEnhancedValue(output, value.ToString(), isRtl);
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0008F3A8 File Offset: 0x0008D5A8
		public static void RenderDirectionEnhancedValue(TextWriter output, string value, bool isRtl)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			SanitizedHtmlString sanitizedStringWithoutEncoding = SanitizedHtmlString.GetSanitizedStringWithoutEncoding(isRtl ? "&#x200F;" : "&#x200E;");
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				switch (c)
				{
				case '(':
					goto IL_65;
				case ')':
					goto IL_7B;
				default:
					switch (c)
					{
					case '[':
						goto IL_65;
					case ']':
						goto IL_7B;
					}
					output.Write(value[i]);
					break;
				}
				IL_9E:
				i++;
				continue;
				IL_65:
				output.Write(sanitizedStringWithoutEncoding);
				output.Write(value[i]);
				goto IL_9E;
				IL_7B:
				output.Write(value[i]);
				output.Write(sanitizedStringWithoutEncoding);
				goto IL_9E;
			}
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0008F460 File Offset: 0x0008D660
		public static string SanitizeHtml(string unsafeHtml)
		{
			if (unsafeHtml == null)
			{
				throw new ArgumentNullException("unsafeHtml");
			}
			string result;
			using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(unsafeHtml)))
			{
				try
				{
					HtmlToHtml htmlToHtml = new HtmlToHtml();
					TextConvertersInternalHelpers.SetPreserveDisplayNoneStyle(htmlToHtml, true);
					htmlToHtml.InputEncoding = Encoding.UTF8;
					htmlToHtml.OutputEncoding = Encoding.UTF8;
					htmlToHtml.FilterHtml = true;
					using (ConverterStream converterStream = new ConverterStream(stream, htmlToHtml, ConverterStreamAccess.Read))
					{
						using (StreamReader streamReader = new StreamReader(converterStream, Encoding.UTF8))
						{
							result = streamReader.ReadToEnd();
						}
					}
				}
				catch (ExchangeDataException innerException)
				{
					throw new OwaBodyConversionFailedException("Sanitize Html Failed", innerException);
				}
				catch (StoragePermanentException innerException2)
				{
					throw new OwaBodyConversionFailedException("Html Conversion Failed", innerException2);
				}
				catch (StorageTransientException innerException3)
				{
					throw new OwaBodyConversionFailedException("Html Conversion Failed", innerException3);
				}
			}
			return result;
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x0008F570 File Offset: 0x0008D770
		public static string DecodeIDNDomain(string smtpAddress)
		{
			return Utilities.DecodeIDNDomain(new SmtpAddress(smtpAddress));
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0008F580 File Offset: 0x0008D780
		public static string DecodeIDNDomain(SmtpAddress smtpAddress)
		{
			string domain = smtpAddress.Domain;
			if (!string.IsNullOrEmpty(domain))
			{
				IdnMapping idnMapping = new IdnMapping();
				string unicode = idnMapping.GetUnicode(domain);
				return smtpAddress.Local + "@" + unicode;
			}
			return smtpAddress.ToString();
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0008F5CB File Offset: 0x0008D7CB
		public static string GetTDClassForWebReadyViewHead(bool isBasicExperience)
		{
			if (isBasicExperience)
			{
				return "bigFont";
			}
			return string.Empty;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0008F5DC File Offset: 0x0008D7DC
		internal static X509Certificate2 GetADRecipientCertificate(ADRawEntry adRecipient, bool checkRevocation)
		{
			byte[][] array = Utilities.FindCertificatesForADRecipient(adRecipient);
			if (array.Length == 0)
			{
				return null;
			}
			string[] array2;
			if (OwaRegistryKeys.UseSecondaryProxiesWhenFindingCertificates)
			{
				ProxyAddressCollection proxyAddressCollection = adRecipient[ADRecipientSchema.EmailAddresses] as ProxyAddressCollection;
				if (proxyAddressCollection != null && proxyAddressCollection.Count > 0)
				{
					array2 = new string[proxyAddressCollection.Count];
					for (int i = 0; i < proxyAddressCollection.Count; i++)
					{
						array2[i] = proxyAddressCollection[i].AddressString;
					}
				}
				else
				{
					array2 = new string[0];
				}
			}
			else
			{
				array2 = new string[]
				{
					adRecipient[ADRecipientSchema.PrimarySmtpAddress].ToString()
				};
			}
			return Utilities.FindBestCertificate(array, array2, false, checkRevocation);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0008F678 File Offset: 0x0008D878
		public static void RenderScriptToEnforceUTF8ForPage(TextWriter writer)
		{
			OwaContext owaContext = OwaContext.Current;
			ISessionContext sessionContext = owaContext.SessionContext;
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString(Utilities.JavascriptEncode(Utilities.NCREncode(LocalizedStrings.GetNonEncoded(257251160))));
			sanitizedHtmlString.DecreeToBeTrusted();
			writer.Write("function _htmlDec(s){var o=document.createElement(\"DIV\");o.innerHTML=s;return o.innerText||o.textContent;}");
			writer.Write("function chkEn(){");
			RenderingUtilities.RenderStringVariable(writer, "s1", Utilities.comparingStringForEncodingDetecting);
			writer.Write("if(_htmlDec(\"&#20320;&#22909;&#65;\")!=s1){alert(_htmlDec(\"");
			writer.Write(sanitizedHtmlString);
			writer.Write("\"));try{window.top.document.location=\"");
			writer.Write(Utilities.JavascriptEncode(OwaUrl.Logoff.GetExplicitUrl(owaContext)));
			if (sessionContext.IsBasicExperience)
			{
				writer.Write("?canary=");
				writer.Write(Utilities.JavascriptEncode(Utilities.UrlEncode(Utilities.GetCurrentCanary(sessionContext))));
				string canary15CookieValue = Utilities.GetCanary15CookieValue();
				if (canary15CookieValue != null)
				{
					writer.Write(Utilities.JavascriptEncode("&X-OWA-CANARY=" + canary15CookieValue));
				}
			}
			writer.Write("\";}catch(e){}}}");
			writer.Write("chkEn();");
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0008F769 File Offset: 0x0008D969
		public static void RenderScriptDisplayPictureOnLoad(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteLine(string.Empty);
			writer.WriteLine("function clipDispPic(oImg, maxSize)\r\n            {\r\n                var tmpImg = new Image();\r\n                tmpImg.src = oImg.src;\r\n                if(tmpImg.width < tmpImg.height)\r\n                { \r\n                oImg.style.maxWidth=maxSize+'px';\r\n                if(tmpImg.height > maxSize)\r\n                { \r\n                    var k = 1;\r\n                    if(tmpImg.width > maxSize)\r\n                        k = maxSize / tmpImg.width;\r\n                    oImg.style.top=(-1*(((k*tmpImg.height)-maxSize)/2)).toString()+'px';\r\n                }\r\n                }\r\n                else \r\n                { \r\n                oImg.style.maxHeight=maxSize+'px';\r\n                if(tmpImg.width > maxSize)\r\n                { \r\n                    var k =1;\r\n                    if(tmpImg.height > maxSize)\r\n                        k = maxSize / tmpImg.height;\r\n                    oImg.style.left=(-1*(((k*tmpImg.width)-maxSize)/2)).toString()+'px';\r\n                }\r\n                }\r\n                oImg.parentNode.className = '';\r\n                oImg.style.opacity='1'; \r\n                oImg.style.filter='alpha(opacity=100)';\r\n            }");
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0008F790 File Offset: 0x0008D990
		internal static string NCREncode(string input)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in input)
			{
				stringBuilder.Append("&#");
				uint num = (uint)c;
				stringBuilder.Append(num.ToString());
				stringBuilder.Append(";");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0008F7F4 File Offset: 0x0008D9F4
		internal static X509Certificate2 FindBestCertificate(byte[][] certBlobs, IEnumerable<string> emails, bool isContact, bool checkRevocation)
		{
			if (certBlobs == null)
			{
				return null;
			}
			X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
			foreach (byte[] rawData in certBlobs)
			{
				if (isContact)
				{
					x509CertificateCollection.ImportFromContact(rawData);
				}
				else
				{
					x509CertificateCollection.Import(rawData);
				}
			}
			return x509CertificateCollection.FindSMimeCertificate(emails, X509KeyUsageFlags.KeyEncipherment, checkRevocation, TimeSpan.FromMilliseconds((double)OwaRegistryKeys.CRLConnectionTimeout), TimeSpan.FromMilliseconds((double)OwaRegistryKeys.CRLRetrievalTimeout), null, null);
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0008F858 File Offset: 0x0008DA58
		internal static byte[][] FindCertificatesForADRecipient(ADRawEntry adRecipient)
		{
			if (adRecipient == null)
			{
				throw new ArgumentNullException("adRecipient");
			}
			byte[][] origin = Utilities.MultiValuePropertyToByteArray(adRecipient[ADRecipientSchema.Certificate] as MultiValuedProperty<byte[]>);
			byte[][] appendant = Utilities.MultiValuePropertyToByteArray(adRecipient[ADRecipientSchema.SMimeCertificate] as MultiValuedProperty<byte[]>);
			return Utilities.AppendArray(origin, appendant);
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0008F8A8 File Offset: 0x0008DAA8
		internal static bool IsSMimeButNotSecureSign(Item message)
		{
			return !ObjectClass.IsOfClass(message.ClassName, "IPM.Note.Secure.Sign") && ObjectClass.IsSmime(message.ClassName);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0008F8CC File Offset: 0x0008DACC
		internal static Item OpenSMimeContent(Item smimeMessage)
		{
			Item item = null;
			if (ItemConversion.TryOpenSMimeContent(smimeMessage as MessageItem, OwaContext.Current.UserContext.Configuration.DefaultAcceptedDomain.DomainName.ToString(), out item))
			{
				OwaContext.Current.AddObjectToDisposeOnEndRequest(item);
				return item;
			}
			return null;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0008F918 File Offset: 0x0008DB18
		internal static AttachmentCollection GetAttachmentCollection(Item message, bool unpackAttachmentForSmimeMessage, UserContext userContext)
		{
			AttachmentCollection attachmentCollection = null;
			if (unpackAttachmentForSmimeMessage && Utilities.IsSMimeButNotSecureSign(message))
			{
				Item item = Utilities.OpenSMimeContent(message);
				if (item != null)
				{
					attachmentCollection = item.AttachmentCollection;
				}
			}
			else if (userContext.IsIrmEnabled && !userContext.IsBasicExperience && Utilities.IsIrmDecrypted(message))
			{
				attachmentCollection = ((RightsManagedMessageItem)message).ProtectedAttachmentCollection;
			}
			if (attachmentCollection == null)
			{
				attachmentCollection = message.AttachmentCollection;
			}
			return attachmentCollection;
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0008F974 File Offset: 0x0008DB74
		internal static bool IsWebBeaconsAllowed(IStorePropertyBag storePropertyBag)
		{
			return ItemUtility.GetProperty<int>(storePropertyBag, ItemSchema.BlockStatus, 0) == 3;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0008F985 File Offset: 0x0008DB85
		internal static bool IsFlagSet(int valueToTest, int flag)
		{
			return (valueToTest & flag) == flag;
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0008F990 File Offset: 0x0008DB90
		internal static bool IsAllDayEvent(ExDateTime start, ExDateTime end)
		{
			return start.TimeOfDay.TotalSeconds == 0.0 && end.TimeOfDay.TotalSeconds == 0.0 && start < end;
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0008F9DC File Offset: 0x0008DBDC
		internal static object[][] FetchRowsFromQueryResult(QueryResult queryResult, int rowCount)
		{
			if (queryResult == null)
			{
				throw new ArgumentNullException("queryResult");
			}
			object[][] array = new object[0][];
			object[][] rows;
			do
			{
				rows = queryResult.GetRows(rowCount - array.Length);
				if (rows.Length > 0)
				{
					if (array.Length == 0)
					{
						array = rows;
					}
					else
					{
						object[][] array2 = new object[array.Length + rows.Length][];
						array.CopyTo(array2, 0);
						rows.CopyTo(array2, array.Length);
						array = array2;
					}
				}
			}
			while (rows.Length > 0 && array.Length < rowCount);
			return array;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0008FA4C File Offset: 0x0008DC4C
		internal static Dictionary<PropertyDefinition, int> GetPropertyToIndexMap(PropertyDefinition[] properties)
		{
			Dictionary<PropertyDefinition, int> dictionary = new Dictionary<PropertyDefinition, int>(properties.Length);
			for (int i = 0; i < properties.Length; i++)
			{
				dictionary[properties[i]] = i;
			}
			return dictionary;
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0008FA7C File Offset: 0x0008DC7C
		internal static void CheckAndThrowForRequiredProperty(Dictionary<PropertyDefinition, int> propertyMap, params PropertyDefinition[] requiredProperties)
		{
			foreach (PropertyDefinition propertyDefinition in requiredProperties)
			{
				if (!propertyMap.ContainsKey(propertyDefinition))
				{
					throw new InvalidOperationException("Cannot find required property " + propertyDefinition.GetType().ToString());
				}
			}
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0008FAC4 File Offset: 0x0008DCC4
		internal static string GetRandomNameForTempFilteredView(UserContext userContext)
		{
			return userContext.Key.MailboxUniqueKey + Guid.NewGuid().ToString();
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0008FAF4 File Offset: 0x0008DCF4
		internal static QueryFilter GetObjectClassTypeFilter(bool isFolder, bool exclusive, params string[] objectTypes)
		{
			PropertyDefinition property = isFolder ? StoreObjectSchema.ContainerClass : StoreObjectSchema.ItemClass;
			List<QueryFilter> list = new List<QueryFilter>(2 * objectTypes.Length);
			foreach (string text in objectTypes)
			{
				if (text != null)
				{
					list.Add(new TextFilter(property, text, MatchOptions.FullString, MatchFlags.IgnoreCase));
					list.Add(new TextFilter(property, text + ".", MatchOptions.Prefix, MatchFlags.IgnoreCase));
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			QueryFilter queryFilter = new OrFilter(list.ToArray());
			if (!exclusive)
			{
				return queryFilter;
			}
			return new NotFilter(queryFilter);
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0008FB84 File Offset: 0x0008DD84
		internal static QueryFilter GetObjectClassTypeFilter(bool isFolder, params string[] objectTypes)
		{
			return Utilities.GetObjectClassTypeFilter(isFolder, false, objectTypes);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0008FB90 File Offset: 0x0008DD90
		internal static int CompareByteArrays(byte[] array1, byte[] array2)
		{
			if (array1 == array2)
			{
				return 0;
			}
			if (array1 == null)
			{
				return -1;
			}
			if (array2 == null)
			{
				return 1;
			}
			int num = 0;
			int num2 = 0;
			while (num == 0 && num2 < array1.Length && num2 < array2.Length)
			{
				num = array1[num2].CompareTo(array2[num2]);
				num2++;
			}
			if (num == 0)
			{
				num = array1.Length.CompareTo(array2.Length);
			}
			return num;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0008FBEC File Offset: 0x0008DDEC
		internal static bool IsOutlookSearchFolder(Folder folder)
		{
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			object obj = folder.TryGetProperty(FolderSchema.IsOutlookSearchFolder);
			return obj != null && !(obj is PropertyError) && (bool)obj;
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0008FC26 File Offset: 0x0008DE26
		internal static bool IsMobileRoutingType(string routingType)
		{
			return !string.IsNullOrEmpty(routingType) && string.Equals(routingType, "MOBILE", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0008FC40 File Offset: 0x0008DE40
		internal static string NormalizePhoneNumber(string input)
		{
			E164Number e164Number;
			if (!E164Number.TryParse(input, out e164Number))
			{
				return null;
			}
			return e164Number.Number;
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0008FC60 File Offset: 0x0008DE60
		internal static string RedirectionUrl(OwaContext owaContext)
		{
			Uri uri;
			if (owaContext.IsExplicitLogon)
			{
				uri = Utilities.AppendSmtpAddressToUrl(owaContext.SecondCasUri.Uri, owaContext.HttpContext.Request.Headers["X-OWA-ExplicitLogonUser"]);
			}
			else
			{
				uri = owaContext.SecondCasUri.Uri;
			}
			return uri.ToString();
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0008FCB8 File Offset: 0x0008DEB8
		internal static bool IsEcpUrl(string urlString)
		{
			if (string.IsNullOrEmpty(urlString))
			{
				return false;
			}
			if (urlString.StartsWith(Utilities.EcpVdir, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			Uri uri = Utilities.TryParseUri(urlString);
			return uri != null && uri.AbsolutePath.StartsWith(Utilities.EcpVdir, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0008FD04 File Offset: 0x0008DF04
		internal static bool IsEacUrl(string urlString)
		{
			if (!Utilities.IsEcpUrl(urlString))
			{
				return false;
			}
			int num = urlString.IndexOf('?');
			if (num > 0)
			{
				string[] source = urlString.Substring(num + 1).Split(new char[]
				{
					'&'
				});
				return !source.Contains("rfr=owa") && !source.Contains("rfr=olk");
			}
			return true;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0008FD64 File Offset: 0x0008DF64
		internal static bool IsSafeUrl(string urlString)
		{
			if (string.IsNullOrEmpty(urlString))
			{
				return false;
			}
			Uri uri;
			if (null == (uri = Utilities.TryParseUri(urlString)))
			{
				return false;
			}
			string scheme = uri.Scheme;
			return !string.IsNullOrEmpty(scheme) && Uri.CheckSchemeName(scheme) && TextConvertersInternalHelpers.IsUrlSchemaSafe(scheme);
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0008FDB4 File Offset: 0x0008DFB4
		internal static bool TryDecodeImceaAddress(string imceaAddress, ref string type, ref string address)
		{
			ProxyAddress proxyAddress;
			if (!SmtpProxyAddress.TryDeencapsulate(imceaAddress, out proxyAddress))
			{
				return false;
			}
			type = proxyAddress.PrefixString;
			address = proxyAddress.AddressString;
			return true;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0008FDE0 File Offset: 0x0008DFE0
		internal static SanitizedHtmlString GetAlternateBodyForIrm(UserContext userContext, Microsoft.Exchange.Data.Storage.BodyFormat bodyFormat, RightsManagedMessageDecryptionStatus decryptionStatus, bool isProtectedVoicemail)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			if (bodyFormat == Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml)
			{
				sanitizingStringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<font face=\"{0}\" size=\"2\">", new object[]
				{
					Utilities.GetDefaultFontName()
				});
				if (decryptionStatus.FailureCode != RightsManagementFailureCode.MissingLicense)
				{
					StringBuilder stringBuilder = new StringBuilder();
					userContext.RenderThemeImage(stringBuilder, ThemeFileId.Error, null, new object[0]);
					sanitizingStringBuilder.AppendFormat(stringBuilder.ToString(), new object[0]);
					sanitizingStringBuilder.Append("&nbsp;");
				}
			}
			RightsManagementFailureCode failureCode = decryptionStatus.FailureCode;
			if (failureCode > RightsManagementFailureCode.PreLicenseAcquisitionFailed)
			{
				switch (failureCode)
				{
				case RightsManagementFailureCode.FailedToExtractTargetUriFromMex:
				case RightsManagementFailureCode.FailedToDownloadMexData:
					sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1314141112));
					goto IL_448;
				case RightsManagementFailureCode.GetServerInfoFailed:
					goto IL_2BD;
				case RightsManagementFailureCode.InternalLicensingDisabled:
					break;
				case RightsManagementFailureCode.ExternalLicensingDisabled:
					sanitizingStringBuilder.AppendFormat(LocalizedStrings.GetNonEncoded(1397740097), new object[]
					{
						Utilities.GetOfficeDownloadAnchor(bodyFormat, userContext.UserCulture)
					});
					goto IL_448;
				default:
					switch (failureCode)
					{
					case RightsManagementFailureCode.ServerRightNotGranted:
						sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(784482022));
						goto IL_448;
					case RightsManagementFailureCode.InvalidLicensee:
						goto IL_1E8;
					case RightsManagementFailureCode.FeatureDisabled:
						break;
					case RightsManagementFailureCode.NotSupported:
						sanitizingStringBuilder.AppendFormat(isProtectedVoicemail ? LocalizedStrings.GetNonEncoded(106943791) : LocalizedStrings.GetNonEncoded(1049269714), new object[]
						{
							Utilities.GetOfficeDownloadAnchor(bodyFormat, userContext.UserCulture)
						});
						goto IL_448;
					case RightsManagementFailureCode.CorruptData:
						sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(684230472));
						goto IL_448;
					case RightsManagementFailureCode.MissingLicense:
					{
						MissingRightsManagementLicenseException ex = (MissingRightsManagementLicenseException)decryptionStatus.Exception;
						if (bodyFormat == Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml)
						{
							sanitizingStringBuilder.AppendFormat("<div id=\"divIrmReqSpinner\" sReqCorrelator=\"{0}\" style=\"text-align:center;\">", new object[]
							{
								ex.RequestCorrelator
							});
							StringBuilder stringBuilder2 = new StringBuilder();
							userContext.RenderThemeImage(stringBuilder2, ThemeFileId.ProgressSmall, "prg", new object[0]);
							sanitizingStringBuilder.AppendFormat(stringBuilder2.ToString(), new object[0]);
							sanitizingStringBuilder.Append("&nbsp;");
						}
						sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(-695375226));
						if (bodyFormat == Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml)
						{
							sanitizingStringBuilder.Append("</div>");
							goto IL_448;
						}
						goto IL_448;
					}
					default:
						if (failureCode != RightsManagementFailureCode.Success)
						{
							goto IL_2BD;
						}
						goto IL_448;
					}
					break;
				}
				sanitizingStringBuilder.AppendFormat(isProtectedVoicemail ? LocalizedStrings.GetNonEncoded(106943791) : LocalizedStrings.GetNonEncoded(1049269714), new object[]
				{
					Utilities.GetOfficeDownloadAnchor(bodyFormat, userContext.UserCulture)
				});
				goto IL_448;
			}
			if (failureCode == RightsManagementFailureCode.UserRightNotGranted)
			{
				sanitizingStringBuilder.AppendFormat(LocalizedStrings.GetNonEncoded(-1796455575), new object[]
				{
					Utilities.GetOfficeDownloadAnchor(bodyFormat, userContext.UserCulture)
				});
				goto IL_448;
			}
			if (failureCode != RightsManagementFailureCode.PreLicenseAcquisitionFailed)
			{
				goto IL_2BD;
			}
			IL_1E8:
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(-1489754529));
			goto IL_448;
			IL_2BD:
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(360598592));
			Exception exception = decryptionStatus.Exception;
			if (Globals.ShowDebugInformation && bodyFormat == Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml && exception != null && exception.InnerException != null)
			{
				sanitizingStringBuilder.AppendFormat("<hr><div onclick=\"document.getElementById('divDtls').style.display='';this.style.display='none';\" style=\"cursor: pointer; color: #3165cd;\">", new object[0]);
				StringBuilder stringBuilder3 = new StringBuilder();
				userContext.RenderThemeImage(stringBuilder3, ThemeFileId.Expand, null, new object[0]);
				sanitizingStringBuilder.AppendFormat(stringBuilder3.ToString(), new object[0]);
				sanitizingStringBuilder.AppendFormat("&nbsp;{0}</div><br><div id=\"divDtls\" style='display:none'>", new object[]
				{
					LocalizedStrings.GetNonEncoded(-610047827)
				});
				string text = string.Empty;
				RightsManagementFailureCode failureCode2 = decryptionStatus.FailureCode;
				Exception innerException = exception.InnerException;
				if (innerException is RightsManagementException)
				{
					RightsManagementException ex2 = (RightsManagementException)innerException;
					text = ex2.RmsUrl;
				}
				int num = 0;
				while (num < 10 && innerException.InnerException != null)
				{
					innerException = innerException.InnerException;
					num++;
				}
				sanitizingStringBuilder.AppendFormat(LocalizedStrings.GetHtmlEncoded(1633606253), new object[]
				{
					innerException.Message
				});
				if (!string.IsNullOrEmpty(text))
				{
					sanitizingStringBuilder.Append("<br>");
					sanitizingStringBuilder.AppendFormat(LocalizedStrings.GetHtmlEncoded(2115316283), new object[]
					{
						text
					});
				}
				if (failureCode2 != RightsManagementFailureCode.Success)
				{
					sanitizingStringBuilder.Append("<br>");
					sanitizingStringBuilder.AppendFormat(LocalizedStrings.GetHtmlEncoded(970140031), new object[]
					{
						failureCode2
					});
				}
				sanitizingStringBuilder.Append("</div>");
			}
			IL_448:
			if (bodyFormat == Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml)
			{
				sanitizingStringBuilder.Append("</font>");
			}
			if (decryptionStatus.Failed)
			{
				OwaContext.Current.HttpContext.Response.AppendHeader("X-OWA-DoNotCache", "1");
			}
			return sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>();
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00090274 File Offset: 0x0008E474
		internal static bool IsIrmRestrictedAndNotDecrypted(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			return rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted && !rightsManagedMessageItem.IsDecoded;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000902AC File Offset: 0x0008E4AC
		internal static bool IsIrmRestrictedAndDecrypted(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			return rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted && rightsManagedMessageItem.IsDecoded;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x000902E4 File Offset: 0x0008E4E4
		internal static bool IsIrmDecrypted(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			return rightsManagedMessageItem != null && rightsManagedMessageItem.IsDecoded;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00090314 File Offset: 0x0008E514
		internal static bool IsIrmRestricted(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			return rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted;
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x00090344 File Offset: 0x0008E544
		internal static bool IrmDecryptIfRestricted(Item item, UserContext userContext, bool acquireLicenses)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			if (rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted)
			{
				if (!rightsManagedMessageItem.IsDecoded)
				{
					rightsManagedMessageItem.Decode(Utilities.CreateOutboundConversionOptions(userContext), acquireLicenses);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x00090398 File Offset: 0x0008E598
		internal static bool IrmDecryptIfRestricted(Item item, UserContext userContext)
		{
			return Utilities.IrmDecryptIfRestricted(item, userContext, false);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x000903A4 File Offset: 0x0008E5A4
		internal static void IrmRemoveRestriction(Item item, UserContext userContext)
		{
			if (userContext.IsIrmEnabled && Utilities.IsIrmRestricted(item))
			{
				RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
				rightsManagedMessageItem.OpenAsReadWrite();
				rightsManagedMessageItem.Decode(Utilities.CreateOutboundConversionOptions(userContext), true);
				rightsManagedMessageItem.SetRestriction(null);
				ConflictResolutionResult conflictResolutionResult = rightsManagedMessageItem.Save(SaveMode.ResolveConflicts);
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					throw new OwaSaveConflictException(LocalizedStrings.GetNonEncoded(-482397486), conflictResolutionResult);
				}
				rightsManagedMessageItem.Load();
			}
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0009040C File Offset: 0x0008E60C
		internal static bool IrmDecryptForReplyForward(OwaContext owaContext, ref Item currentItem, ref Item currentParentItem, ref Microsoft.Exchange.Data.Storage.BodyFormat bodyType, out RightsManagedMessageDecryptionStatus decryptionStatus)
		{
			UserContext userContext = owaContext.UserContext;
			if (!Utilities.IsIrmRestricted(currentItem))
			{
				decryptionStatus = RightsManagedMessageDecryptionStatus.Success;
				return false;
			}
			RightsManagedMessageItem rightsManagedMessageItem = (RightsManagedMessageItem)currentItem;
			if (!rightsManagedMessageItem.CanDecode)
			{
				decryptionStatus = RightsManagedMessageDecryptionStatus.NotSupported;
				return false;
			}
			if (Utilities.IrmDecryptIfRestricted(currentItem, userContext, true) && !rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.Edit))
			{
				bodyType = ReplyForwardUtilities.GetReplyForwardBodyFormat(currentItem, userContext);
				currentItem.Dispose();
				currentItem = null;
				if (currentParentItem != null)
				{
					currentParentItem.Dispose();
					currentParentItem = null;
				}
				currentItem = ReplyForwardUtilities.GetItemForRequest(owaContext, out currentParentItem);
				decryptionStatus = RightsManagedMessageDecryptionStatus.Success;
				return true;
			}
			decryptionStatus = RightsManagedMessageDecryptionStatus.Success;
			return false;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x000904B4 File Offset: 0x0008E6B4
		internal static SanitizedHtmlString GetOfficeDownloadAnchor(Microsoft.Exchange.Data.Storage.BodyFormat bodyFormat, CultureInfo userCulture)
		{
			if (bodyFormat == Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml)
			{
				SanitizedHtmlString sanitizedHtmlString = SanitizedHtmlString.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", new object[]
				{
					LocalizedStrings.GetNonEncoded(1124412272),
					LocalizedStrings.GetNonEncoded(-1065109671)
				});
				return SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-539149404, userCulture), new object[]
				{
					sanitizedHtmlString
				});
			}
			return SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1235477635, userCulture), new object[]
			{
				SanitizedHtmlString.GetNonEncoded(1124412272)
			});
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x00090539 File Offset: 0x0008E739
		internal static string RemoveHtmlComments(string htmlString)
		{
			return Utilities.htmlCommentStriper.Replace(htmlString, string.Empty);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0009054C File Offset: 0x0008E74C
		private static bool ShouldIgnoreException(Exception exception)
		{
			COMException ex = exception as COMException;
			return ex != null && (ex.ErrorCode == -2147023901 || ex.ErrorCode == -2147024832 || ex.ErrorCode == -2147024895 || ex.ErrorCode == -2147024890 || ex.ErrorCode == -2147023667);
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x000905A8 File Offset: 0x0008E7A8
		private static byte[][] MultiValuePropertyToByteArray(MultiValuedProperty<byte[]> property)
		{
			byte[][] array = null;
			if (property != null)
			{
				array = new byte[property.Count][];
				property.CopyTo(array, 0);
			}
			return array;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x000905D0 File Offset: 0x0008E7D0
		private static byte[][] AppendArray(byte[][] origin, byte[][] appendant)
		{
			byte[][] array = origin ?? Utilities.EmptyArrayOfByteArrays;
			byte[][] array2 = appendant ?? Utilities.EmptyArrayOfByteArrays;
			int num = array.Length;
			int num2 = array2.Length;
			int num3 = num + num2;
			byte[][] array3 = (num3 != 0) ? new byte[num3][] : Utilities.EmptyArrayOfByteArrays;
			Array.Copy(array, array3, num);
			Array.Copy(array2, 0, array3, num, num2);
			return array3;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0009062A File Offset: 0x0008E82A
		internal static bool IsRequestCallbackForPhishing(HttpRequest request)
		{
			return !string.IsNullOrEmpty(Utilities.GetQueryStringParameter(request, "ph", false));
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00090640 File Offset: 0x0008E840
		internal static bool IsRequestCallbackForWebBeacons(HttpRequest request)
		{
			return !string.IsNullOrEmpty(Utilities.GetQueryStringParameter(request, "cb", false));
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00090658 File Offset: 0x0008E858
		internal static void PutOwaSubPageIntoPlaceHolder(PlaceHolder placeHolder, string id, OwaSubPage owaSubPage, QueryStringParameters queryStringParameters, string extraAttribute, bool isHidden)
		{
			if (placeHolder == null)
			{
				throw new ArgumentNullException("placeHolder");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (owaSubPage == null)
			{
				throw new ArgumentNullException("owaSubPage");
			}
			if (queryStringParameters == null)
			{
				throw new ArgumentNullException("queryStringParameters");
			}
			owaSubPage.QueryStringParameters = queryStringParameters;
			placeHolder.Controls.Add(owaSubPage);
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append("<div id=\"");
			stringBuilder.Append(id);
			stringBuilder.Append("\" url=\"");
			Utilities.HtmlEncode(queryStringParameters.QueryString, stringBuilder);
			stringBuilder.Append("\" _PageType=\"");
			Utilities.HtmlEncode(owaSubPage.PageType, stringBuilder);
			stringBuilder.Append("\"");
			if (!string.IsNullOrEmpty(extraAttribute))
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(extraAttribute);
			}
			if (isHidden)
			{
				stringBuilder.Append(" style=\"display:none\"");
			}
			stringBuilder.Append(">");
			owaSubPage.RenderExternalScriptFiles(stringBuilder);
			placeHolder.Controls.AddAt(0, new LiteralControl(stringBuilder.ToString()));
			placeHolder.Controls.Add(new LiteralControl("</div>"));
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00090778 File Offset: 0x0008E978
		internal static bool HasArchive(UserContext userContext)
		{
			return userContext.ExchangePrincipal.GetArchiveMailbox() != null;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0009078B File Offset: 0x0008E98B
		internal static ulong SetSegmentationFlags(int segmentationBits1, int segmentationBits2)
		{
			return (ulong)segmentationBits1 + (ulong)((ulong)((long)segmentationBits2) << 32);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00090798 File Offset: 0x0008E998
		internal static uint[] GetSegmentationBitsForJavascript(UserContext userContext)
		{
			uint[] array = new uint[2];
			ulong num = userContext.SegmentationFlags;
			if (userContext.RestrictedCapabilitiesFlags != 0UL)
			{
				num &= userContext.RestrictedCapabilitiesFlags;
			}
			array[0] = (uint)num;
			array[1] = (uint)(num >> 32);
			return array;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x000907D4 File Offset: 0x0008E9D4
		internal static int GetTimeZoneOffset(UserContext userContext)
		{
			int num = (int)DateTimeUtilities.GetLocalTime().Bias.TotalMinutes;
			if (num != userContext.RemindersTimeZoneOffset)
			{
				userContext.RemindersTimeZoneOffset = num;
			}
			return num;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0009080C File Offset: 0x0008EA0C
		internal static StreamWriter CreateStreamWriter(Stream stream)
		{
			UTF8Encoding encoding = new UTF8Encoding(false, false);
			return new StreamWriter(stream, encoding);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00090828 File Offset: 0x0008EA28
		internal static string GetStringHash(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			string result;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(text.ToLowerInvariant());
				result = Convert.ToBase64String(sha256Cng.ComputeHash(bytes));
			}
			return result;
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00090884 File Offset: 0x0008EA84
		private static void RegisterMailboxException(UserContext owaUserContext, Exception exception)
		{
			string serverFqdn = string.Empty;
			if (!Globals.StoreTransientExceptionEventLogEnabled)
			{
				return;
			}
			try
			{
				try
				{
					if (owaUserContext != null && owaUserContext.ExchangePrincipal != null)
					{
						serverFqdn = owaUserContext.ExchangePrincipal.MailboxInfo.Location.ServerFqdn;
					}
				}
				catch (StorageTransientException)
				{
				}
				catch (StoragePermanentException)
				{
				}
				finally
				{
					string[] array;
					if (Utilities.StoreConnectionTransientManager.RegisterException(exception, serverFqdn, out array) && array != null)
					{
						int num = 1;
						foreach (string text in array)
						{
							OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_StorageTransientExceptionWarning, string.Empty, new object[]
							{
								num,
								array.Length,
								text
							});
							num++;
						}
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CoreTracer.TraceError<string>(0L, "Exception happened while attempting to log information about mailbox failures. Exception: {0}", ex.ToString());
				ExWatson.SendReport(ex, ReportOptions.None, null);
			}
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00090990 File Offset: 0x0008EB90
		private static bool IsDiskFullException(Exception e)
		{
			return Marshal.GetHRForException(e) == -2147024784;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000909A0 File Offset: 0x0008EBA0
		private static bool IsUserPrincipalName(string logonName)
		{
			int num = logonName.IndexOf('@');
			if (num > 0)
			{
				int num2 = logonName.LastIndexOf('@');
				if (num2 < logonName.Length - 1 && num == num2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000909D8 File Offset: 0x0008EBD8
		private static bool IsDomainSlashUser(string logonName)
		{
			int num = logonName.IndexOf('\\');
			if (num > 0)
			{
				int num2 = logonName.LastIndexOf('\\');
				if (num2 < logonName.Length - 1 && num == num2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00090A10 File Offset: 0x0008EC10
		internal static string GetCanary15CookieValue()
		{
			if (OwaContext.Current != null)
			{
				Canary15Cookie canary15Cookie = Canary15Cookie.TryCreateFromHttpContext(OwaContext.Current.HttpContext, OwaContext.Current.LogonIdentity.UniqueId, Canary15Profile.Owa);
				if (canary15Cookie != null)
				{
					return canary15Cookie.Value;
				}
			}
			return null;
		}

		// Token: 0x04001200 RID: 4608
		private const int GuidLength = 32;

		// Token: 0x04001201 RID: 4609
		public const string SilverlightMinimumRuntimeVersion = "2.0.31005.0";

		// Token: 0x04001202 RID: 4610
		public const string UrlQueryParameter = "URL";

		// Token: 0x04001203 RID: 4611
		public const string NoDocumentsLinkClassificationInRedir = "NoDocLnkCls";

		// Token: 0x04001204 RID: 4612
		public const string EmailUrlParameter = "email";

		// Token: 0x04001205 RID: 4613
		public const string MobileRoutingType = "MOBILE";

		// Token: 0x04001206 RID: 4614
		internal const string MailboxCrossSiteFailoverHeader = "mailboxCrossSiteFailover";

		// Token: 0x04001207 RID: 4615
		internal const string ClientStringsFilename = "clientstrings.aspx";

		// Token: 0x04001208 RID: 4616
		private const string SmallIconsFilename = "smallicons.aspx";

		// Token: 0x04001209 RID: 4617
		private const int ErrorAborted = -2147023901;

		// Token: 0x0400120A RID: 4618
		private const int ErrorNonExistentConnection = -2147023667;

		// Token: 0x0400120B RID: 4619
		private const int ErrorNetworkNameNotAvailable = -2147024832;

		// Token: 0x0400120C RID: 4620
		private const int ErrorInvalidHandle = -2147024890;

		// Token: 0x0400120D RID: 4621
		private const int ErrorIncorrectFunction = -2147024895;

		// Token: 0x0400120E RID: 4622
		private const string ExtensionGif = ".gif";

		// Token: 0x0400120F RID: 4623
		private const string ExtensionJpg = ".jpg";

		// Token: 0x04001210 RID: 4624
		private const string ExtensionCss = ".css";

		// Token: 0x04001211 RID: 4625
		private const string ExtensionXap = ".xap";

		// Token: 0x04001212 RID: 4626
		private const string ExtensionJs = ".js";

		// Token: 0x04001213 RID: 4627
		private const string ExtensionWav = ".wav";

		// Token: 0x04001214 RID: 4628
		private const string ExtensionMp3 = ".mp3";

		// Token: 0x04001215 RID: 4629
		private const string ExtensionHtm = ".htm";

		// Token: 0x04001216 RID: 4630
		private const string ExtensionHtml = ".html";

		// Token: 0x04001217 RID: 4631
		private const string ExtensionPng = ".png";

		// Token: 0x04001218 RID: 4632
		private const string ExtensionMSI = ".msi";

		// Token: 0x04001219 RID: 4633
		private const string ExtensionICO = ".ico";

		// Token: 0x0400121A RID: 4634
		private const string ExtensionManifest = ".manifest";

		// Token: 0x0400121B RID: 4635
		private const string ExtensionTTF = ".ttf";

		// Token: 0x0400121C RID: 4636
		private const string ExtensionEOT = ".eot";

		// Token: 0x0400121D RID: 4637
		private const string ExtensionWOFF = ".woff";

		// Token: 0x0400121E RID: 4638
		private const string ExtensionSVG = ".svg";

		// Token: 0x0400121F RID: 4639
		private const string ExtensionChromeWebApp = ".crx";

		// Token: 0x04001220 RID: 4640
		private const int NetUserChangePasswordSuccess = 0;

		// Token: 0x04001221 RID: 4641
		private const int NetUserChangePasswordAccessDenied = 5;

		// Token: 0x04001222 RID: 4642
		private const int NetUserChangePasswordInvalidOldPassword = 86;

		// Token: 0x04001223 RID: 4643
		private const int NetUserChangePasswordDoesNotMeetPolicyRequirement = 2245;

		// Token: 0x04001224 RID: 4644
		private const string PremiumScriptFolder = "/scripts/premium/";

		// Token: 0x04001225 RID: 4645
		private const string UnknownExceptionPrefix = "UE:";

		// Token: 0x04001226 RID: 4646
		public const string CanaryFormParameter = "hidcanary";

		// Token: 0x04001227 RID: 4647
		public const string CanaryQueryOehParameter = "canary";

		// Token: 0x04001228 RID: 4648
		public const string Canary15Name = "X-OWA-CANARY";

		// Token: 0x04001229 RID: 4649
		public static readonly TimeSpan DefaultMinAvailabilityThreshold = TimeSpan.FromSeconds(15.0);

		// Token: 0x0400122A RID: 4650
		private static readonly LatencyDetectionContextFactory OwaAvailabilityContextFactory = LatencyDetectionContextFactory.CreateFactory("OWA Availability Query", Utilities.DefaultMinAvailabilityThreshold, Utilities.DefaultMinAvailabilityThreshold);

		// Token: 0x0400122B RID: 4651
		private static readonly string comparingStringForEncodingDetecting = Encoding.UTF8.GetString(new byte[]
		{
			228,
			189,
			160,
			229,
			165,
			189,
			65
		});

		// Token: 0x0400122C RID: 4652
		private static readonly string VirtualDirectoryNameWithLeadingSlash = HttpRuntime.AppDomainAppVirtualPath;

		// Token: 0x0400122D RID: 4653
		private static readonly string VirtualDirectoryNameWithLeadingAndTrailingSlash = HttpRuntime.AppDomainAppVirtualPath + "/";

		// Token: 0x0400122E RID: 4654
		private static readonly byte[][] EmptyArrayOfByteArrays = new byte[0][];

		// Token: 0x0400122F RID: 4655
		private static readonly string[] Owa15ParameterNames = new string[]
		{
			"owa15",
			"animation",
			"appcache",
			"cmd",
			"diag",
			"exsvurl",
			"layout",
			"mergerowsvalidation",
			"modurl",
			"offline",
			"prefetch",
			"realm",
			"server",
			"sessiontimeout",
			"sync",
			"tracelevel",
			"viewmodel",
			"wa",
			"theme"
		};

		// Token: 0x04001230 RID: 4656
		private static readonly Dictionary<Type, string> ExceptionCodeMap = new Dictionary<Type, string>
		{
			{
				typeof(OwaRenderingEmbeddedReadingPaneException),
				"E001"
			},
			{
				typeof(OwaInvalidRequestException),
				"E002"
			},
			{
				typeof(OwaInvalidIdFormatException),
				"E003"
			},
			{
				typeof(OwaSegmentationException),
				"E004"
			},
			{
				typeof(OwaForbiddenRequestException),
				"E005"
			},
			{
				typeof(OwaDelegatorMailboxFailoverException),
				"E006"
			},
			{
				typeof(WrongCASServerBecauseOfOutOfDateDNSCacheException),
				"E007"
			},
			{
				typeof(OwaURLIsOutOfDateException),
				"E008"
			},
			{
				typeof(OwaEventHandlerException),
				"E009"
			},
			{
				typeof(OwaNotSupportedException),
				"E010"
			},
			{
				typeof(OwaClientNotSupportedException),
				"E011"
			},
			{
				typeof(OwaExistentNotificationPipeException),
				"E012"
			},
			{
				typeof(OwaNotificationPipeException),
				"E013"
			},
			{
				typeof(OwaOperationNotSupportedException),
				"E014"
			},
			{
				typeof(OwaADObjectNotFoundException),
				"E015"
			},
			{
				typeof(OwaInvalidCanary14Exception),
				"E016"
			},
			{
				typeof(OwaCanaryException),
				"E016"
			},
			{
				typeof(OwaLockTimeoutException),
				"E017"
			},
			{
				typeof(OwaLostContextException),
				"E018"
			},
			{
				typeof(OwaBodyConversionFailedException),
				"E019"
			},
			{
				typeof(OwaArchiveInTransitException),
				"E020"
			},
			{
				typeof(OwaArchiveNotAvailableException),
				"E021"
			},
			{
				typeof(OwaSaveConflictException),
				"E022"
			},
			{
				typeof(OwaAccessDeniedException),
				"E023"
			},
			{
				typeof(OwaInstantMessageEventHandlerTransientException),
				"E024"
			},
			{
				typeof(OwaUserNotIMEnabledException),
				"E025"
			},
			{
				typeof(OwaIMOperationNotAllowedToSelf),
				"E026"
			},
			{
				typeof(OwaInvalidOperationException),
				"E027"
			},
			{
				typeof(OwaChangePasswordTransientException),
				"E028"
			},
			{
				typeof(OwaSpellCheckerException),
				"E029"
			},
			{
				typeof(OwaProxyException),
				"E030"
			},
			{
				typeof(OwaExplicitLogonException),
				"E031"
			},
			{
				typeof(OwaInvalidWebPartRequestException),
				"E032"
			},
			{
				typeof(OwaNoReplicaOfCurrentServerVersionException),
				"E033"
			},
			{
				typeof(OwaNoReplicaException),
				"E034"
			},
			{
				typeof(TranscodingServerBusyException),
				"E035"
			},
			{
				typeof(TranscodingUnconvertibleFileException),
				"E036"
			},
			{
				typeof(TranscodingFatalFaultException),
				"E037"
			},
			{
				typeof(TranscodingOverMaximumFileSizeException),
				"E038"
			},
			{
				typeof(TranscodingTimeoutException),
				"E039"
			},
			{
				typeof(TranscodingErrorFileException),
				"E040"
			},
			{
				typeof(OwaInvalidConfigurationException),
				"E041"
			},
			{
				typeof(OwaAsyncOperationException),
				"E042"
			},
			{
				typeof(OwaAsyncRequestTimeoutException),
				"E043"
			},
			{
				typeof(OwaNeedsSMimeControlToEditDraftException),
				"E044"
			},
			{
				typeof(OwaCannotEditIrmDraftException),
				"E045"
			},
			{
				typeof(OwaBrowserUpdateRequiredException),
				"E046"
			},
			{
				typeof(OwaSharedFromOlderVersionException),
				"E047"
			},
			{
				typeof(OwaRespondOlderVersionMeetingException),
				"E048"
			},
			{
				typeof(OwaCreateClientSecurityContextFailedException),
				"E049"
			},
			{
				typeof(OwaUnsupportedConversationItemException),
				"E050"
			},
			{
				typeof(OwaNotificationPipeWriteException),
				"E051"
			},
			{
				typeof(OwaDisabledException),
				"E052"
			},
			{
				typeof(OwaLightDisabledException),
				"E053"
			},
			{
				typeof(MailboxInSiteFailoverException),
				"E101"
			},
			{
				typeof(MailboxCrossSiteFailoverException),
				"E102"
			},
			{
				typeof(WrongServerException),
				"E103"
			},
			{
				typeof(ObjectNotFoundException),
				"E104"
			},
			{
				typeof(ObjectValidationException),
				"E105"
			},
			{
				typeof(CorruptDataException),
				"E106"
			},
			{
				typeof(PropertyValidationException),
				"E107"
			},
			{
				typeof(InvalidSharingMessageException),
				"E108"
			},
			{
				typeof(InvalidSharingDataException),
				"E109"
			},
			{
				typeof(InvalidExternalSharingInitiatorException),
				"E110"
			},
			{
				typeof(VirusDetectedException),
				"E111"
			},
			{
				typeof(VirusScanInProgressException),
				"E112"
			},
			{
				typeof(VirusMessageDeletedException),
				"E113"
			},
			{
				typeof(NoReplicaException),
				"E114"
			},
			{
				typeof(StorageTransientException),
				"E116"
			},
			{
				typeof(RulesTooBigException),
				"E117"
			},
			{
				typeof(DuplicateActionException),
				"E118"
			},
			{
				typeof(ObjectExistedException),
				"E119"
			},
			{
				typeof(MailboxInTransitException),
				"E120"
			},
			{
				typeof(ConnectionFailedPermanentException),
				"E121"
			},
			{
				typeof(ConnectionFailedTransientException),
				"E122"
			},
			{
				typeof(MailboxOfflineException),
				"E123"
			},
			{
				typeof(SendAsDeniedException),
				"E124"
			},
			{
				typeof(RecurrenceFormatException),
				"E125"
			},
			{
				typeof(OccurrenceTimeSpanTooBigException),
				"E126"
			},
			{
				typeof(QuotaExceededException),
				"E127"
			},
			{
				typeof(MessageTooBigException),
				"E128"
			},
			{
				typeof(SubmissionQuotaExceededException),
				"E129"
			},
			{
				typeof(MessageSubmissionExceededException),
				"E130"
			},
			{
				typeof(AttachmentExceededException),
				"E131"
			},
			{
				typeof(ResourcesException),
				"E132"
			},
			{
				typeof(NoMoreConnectionsException),
				"E133"
			},
			{
				typeof(AccountDisabledException),
				"E134"
			},
			{
				typeof(AccessDeniedException),
				"E135"
			},
			{
				typeof(StoragePermanentException),
				"E136"
			},
			{
				typeof(ServerNotFoundException),
				"E137"
			},
			{
				typeof(SaveConflictException),
				"E138"
			},
			{
				typeof(FolderSaveException),
				"E139"
			},
			{
				typeof(OccurrenceCrossingBoundaryException),
				"E140"
			},
			{
				typeof(ParserException),
				"E141"
			},
			{
				typeof(RecurrenceEndDateTooBigException),
				"E142"
			},
			{
				typeof(RecurrenceStartDateTooSmallException),
				"E143"
			},
			{
				typeof(RecurrenceHasNoOccurrenceException),
				"E144"
			},
			{
				typeof(PropertyErrorException),
				"E145"
			},
			{
				typeof(ConversionFailedException),
				"E146"
			},
			{
				typeof(RightsManagementPermanentException),
				"E147"
			},
			{
				typeof(DataValidationException),
				"E148"
			},
			{
				typeof(InvalidObjectOperationException),
				"E149"
			},
			{
				typeof(TransientException),
				"E150"
			},
			{
				typeof(CorruptDataException),
				"E201"
			},
			{
				typeof(AccessDeniedException),
				"E202"
			},
			{
				typeof(ConnectionException),
				"E203"
			},
			{
				typeof(PropertyErrorException),
				"E204"
			},
			{
				typeof(PathTooLongException),
				"E205"
			},
			{
				typeof(UnknownErrorException),
				"E206"
			},
			{
				typeof(DocumentLibraryException),
				"E207"
			},
			{
				typeof(ObjectNotFoundException),
				"E208"
			},
			{
				typeof(ADTransientException),
				"E301"
			},
			{
				typeof(ADOperationException),
				"E302"
			},
			{
				typeof(OverBudgetException),
				"E303"
			},
			{
				typeof(ResourceUnhealthyException),
				"E304"
			},
			{
				typeof(COMException),
				"E401"
			},
			{
				typeof(ThreadAbortException),
				"E402"
			},
			{
				typeof(InvalidOperationException),
				"E403"
			},
			{
				typeof(NullReferenceException),
				"E404"
			},
			{
				typeof(OutOfMemoryException),
				"E405"
			},
			{
				typeof(ArgumentException),
				"E406"
			},
			{
				typeof(IndexOutOfRangeException),
				"E407"
			},
			{
				typeof(ArgumentOutOfRangeException),
				"E408"
			},
			{
				typeof(HttpException),
				"E409"
			},
			{
				typeof(ArgumentNullException),
				"E410"
			},
			{
				typeof(InstantMessagingException),
				"E501"
			}
		};

		// Token: 0x04001231 RID: 4657
		internal static string PremiumScriptPath = Globals.ApplicationVersion + "/scripts/premium/";

		// Token: 0x04001232 RID: 4658
		internal static readonly string EcpVdir = "/ecp/";

		// Token: 0x04001233 RID: 4659
		private static OwaExceptionEventManager storeConnectionTransientManager;

		// Token: 0x04001234 RID: 4660
		private static Dictionary<ThemeFileId, Strings.IDs> altTable = new Dictionary<ThemeFileId, Strings.IDs>
		{
			{
				ThemeFileId.FirstPage,
				-946066775
			},
			{
				ThemeFileId.PreviousPage,
				-1907861992
			},
			{
				ThemeFileId.NextPage,
				1548165396
			},
			{
				ThemeFileId.LastPage,
				-991618511
			},
			{
				ThemeFileId.ContactDL,
				-1878983012
			},
			{
				ThemeFileId.ResourceAttendee,
				191819257
			},
			{
				ThemeFileId.RequiredAttendee,
				749312262
			},
			{
				ThemeFileId.OptionalAttendee,
				107204003
			},
			{
				ThemeFileId.ImportanceLow,
				-691921988
			},
			{
				ThemeFileId.ImportanceHigh,
				-1170704990
			},
			{
				ThemeFileId.Attachment2,
				-1498653219
			}
		};

		// Token: 0x04001235 RID: 4661
		private static PropertyDefinition[] adFindByExchangeLegacyDnProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.PrimarySmtpAddress,
			ADObjectSchema.Id,
			ADOrgPersonSchema.MobilePhone
		};

		// Token: 0x04001236 RID: 4662
		private static Regex htmlCommentStriper = new Regex("<!--.*?-->", RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04001237 RID: 4663
		private static int queuedDelayedRestart;

		// Token: 0x04001238 RID: 4664
		private static uint[] pow2minus1 = new uint[]
		{
			0U,
			1U,
			3U,
			7U,
			15U,
			31U,
			63U,
			127U,
			255U,
			511U,
			1023U,
			2047U,
			4095U,
			8191U,
			16383U,
			32767U,
			65535U,
			131071U,
			262143U,
			524287U,
			1048575U,
			2097151U,
			4194303U,
			8388607U,
			16777215U,
			33554431U,
			67108863U,
			134217727U,
			268435455U,
			536870911U,
			1073741823U,
			2147483647U,
			uint.MaxValue
		};

		// Token: 0x04001239 RID: 4665
		private static PropertyDefinition[] distributionListMemberPropertyDefinitions = new PropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.PhoneticDisplayName,
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.Alias,
			ADRecipientSchema.LegacyExchangeDN
		};

		// Token: 0x0400123A RID: 4666
		private static PropertyDefinition[] smimeDistributionListMemberPropertyDefinitions = new PropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.PhoneticDisplayName,
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.Alias,
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.Certificate,
			ADRecipientSchema.SMimeCertificate,
			ADRecipientSchema.EmailAddresses
		};

		// Token: 0x0400123B RID: 4667
		private static string smimeVersion;

		// Token: 0x0200028C RID: 652
		public enum ChangePasswordResult
		{
			// Token: 0x0400123D RID: 4669
			Success,
			// Token: 0x0400123E RID: 4670
			InvalidCredentials,
			// Token: 0x0400123F RID: 4671
			LockedOut,
			// Token: 0x04001240 RID: 4672
			BadNewPassword,
			// Token: 0x04001241 RID: 4673
			OtherError
		}
	}
}

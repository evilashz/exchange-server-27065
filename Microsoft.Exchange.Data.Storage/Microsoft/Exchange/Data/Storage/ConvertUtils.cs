using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005C9 RID: 1481
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ConvertUtils
	{
		// Token: 0x06003CFA RID: 15610 RVA: 0x000F9154 File Offset: 0x000F7354
		internal static T CallCtsWithReturnValue<T>(Trace tracer, string methodName, LocalizedString exceptionString, ConvertUtils.CtsCallWithReturnValue<T> ctsCall)
		{
			T returnValue = default(T);
			ConvertUtils.CallCts(tracer, methodName, exceptionString, delegate
			{
				returnValue = ctsCall();
			});
			return returnValue;
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x000F9194 File Offset: 0x000F7394
		internal static void CallCts(Trace tracer, string methodName, LocalizedString exceptionString, ConvertUtils.CtsCall ctsCall)
		{
			try
			{
				ctsCall();
			}
			catch (ExchangeDataException ex)
			{
				StorageGlobals.ContextTraceError<string, ExchangeDataException>(tracer, "{0}: ExchangeDataException, {1}", methodName, ex);
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent, exceptionString, ex);
			}
			catch (IOException ex2)
			{
				StorageGlobals.ContextTraceError<string, IOException>(tracer, "{0}: IOException, {1}", methodName, ex2);
				if (StorageGlobals.IsDiskFullException(ex2))
				{
					throw new StorageTransientException(exceptionString, ex2);
				}
				throw new StoragePermanentException(exceptionString, ex2);
			}
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x000F9204 File Offset: 0x000F7404
		internal static string ExtractMimeContentId(string value)
		{
			if (value.Length >= 2 && value[0] == '<' && value[value.Length - 1] == '>')
			{
				return value.Substring(1, value.Length - 2);
			}
			return value;
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x000F9240 File Offset: 0x000F7440
		internal static int GetCodepageFromCharset(string charset)
		{
			int result;
			if (ConvertUtils.TryGetCodepageFromCharset(charset, out result))
			{
				return result;
			}
			throw new NotSupportedException(ServerStrings.ExUnsupportedCharset(charset));
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x000F926C File Offset: 0x000F746C
		internal static int GetInternetEncodingFromCharset(string charset)
		{
			int result;
			if (ConvertUtils.TryGetInternetEncodingFromCharset(charset, out result))
			{
				return result;
			}
			throw new NotSupportedException(ServerStrings.ExUnsupportedCharset(charset));
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x000F9295 File Offset: 0x000F7495
		internal static bool TryGetCodepageFromCharset(string charset, out int codepage)
		{
			return ConvertUtils.TryGetCodepageFromCharset(charset, false, out codepage);
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x000F929F File Offset: 0x000F749F
		internal static bool TryGetInternetEncodingFromCharset(string charset, out int codepage)
		{
			return ConvertUtils.TryGetCodepageFromCharset(charset, true, out codepage);
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x000F92AC File Offset: 0x000F74AC
		internal static Charset GetCharsetFromCharsetName(string charsetName)
		{
			Charset result;
			if (ConvertUtils.TryGetCharsetFromCharsetName(charsetName, out result))
			{
				return result;
			}
			throw new NotSupportedException(ServerStrings.ExUnsupportedCharset(charsetName));
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x000F92D8 File Offset: 0x000F74D8
		internal static bool TryGetCharsetFromCharsetName(string charsetName, out Charset charset)
		{
			charset = null;
			if (string.IsNullOrEmpty(charsetName))
			{
				return false;
			}
			if (charsetName.StartsWith("InternalXsoCodepage-"))
			{
				int length = "InternalXsoCodepage-".Length;
				int codePage = int.Parse(charsetName.Substring(length, charsetName.Length - length));
				if (Charset.TryGetCharset(codePage, out charset) && charset.IsAvailable)
				{
					return true;
				}
			}
			else if (Charset.TryGetCharset(charsetName, out charset) && charset.IsAvailable)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x000F934C File Offset: 0x000F754C
		private static bool TryGetCodepageFromCharset(string charsetName, bool isInternetEncoding, out int codepage)
		{
			codepage = 0;
			if (charsetName == null)
			{
				return false;
			}
			if (charsetName.StartsWith("InternalXsoCodepage-"))
			{
				int length = "InternalXsoCodepage-".Length;
				codepage = int.Parse(charsetName.Substring(length, charsetName.Length - length));
				return true;
			}
			Charset charset;
			if (!Charset.TryGetCharset(charsetName, out charset))
			{
				return false;
			}
			if (isInternetEncoding)
			{
				codepage = charset.CodePage;
				return true;
			}
			codepage = ConvertUtils.MapItemWindowsCharset(charset).CodePage;
			return true;
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x000F93B8 File Offset: 0x000F75B8
		public static string GetCharsetFromCodepage(int codepage)
		{
			string result;
			if (ConvertUtils.TryGetCharsetFromCodepage(codepage, out result))
			{
				return result;
			}
			throw new NotSupportedException(ServerStrings.ExUnsupportedCodepage(codepage));
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x000F93E4 File Offset: 0x000F75E4
		private static bool TryGetCharsetFromCodepage(int codepage, out string charsetName)
		{
			Charset charset;
			if (Charset.TryGetCharset(codepage, out charset))
			{
				charsetName = charset.Name;
				return true;
			}
			charsetName = null;
			return false;
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x000F9409 File Offset: 0x000F7609
		internal static string WrapCodepageToCharset(int codepage)
		{
			return "InternalXsoCodepage-" + codepage.ToString();
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x000F941C File Offset: 0x000F761C
		private static int TransformCodepage(int codepage)
		{
			if (codepage <= 50932)
			{
				switch (codepage)
				{
				case 1200:
				case 1201:
					return 65000;
				default:
					if (codepage != 50225)
					{
						if (codepage != 50932)
						{
							return codepage;
						}
						return 50220;
					}
					break;
				}
			}
			else
			{
				if (codepage == 50936)
				{
					return 936;
				}
				switch (codepage)
				{
				case 50949:
					break;
				case 50950:
					return 950;
				default:
					if (codepage != 51256)
					{
						return codepage;
					}
					return 28596;
				}
			}
			return 51949;
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x000F94A4 File Offset: 0x000F76A4
		internal static bool TryGetValidCharset(int codepage, out Charset charset)
		{
			if (Charset.TryGetCharset(codepage, out charset) && charset.IsAvailable)
			{
				return true;
			}
			charset = null;
			return false;
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x000F94C0 File Offset: 0x000F76C0
		internal static bool TryTransformCharset(ref Charset charset)
		{
			int num = ConvertUtils.TransformCodepage(charset.CodePage);
			if (num == charset.CodePage)
			{
				return true;
			}
			Charset charset2;
			if (ConvertUtils.TryGetValidCharset(num, out charset2))
			{
				charset = charset2;
				return true;
			}
			return false;
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x000F94F8 File Offset: 0x000F76F8
		internal static Charset GetItemOutboundMimeCharset(Item item, OutboundConversionOptions options)
		{
			Charset charset = ConvertUtils.GetItemOutboundMimeCharsetInternal(item, options);
			string className = item.ClassName;
			if (options.DetectionOptions.PreferredCharset == null && charset.CodePage != 54936 && (ObjectClass.IsTaskRequest(className) || ObjectClass.IsMeetingMessage(className) || ObjectClass.IsCalendarItem(className)))
			{
				charset = Charset.GetCharset(65001);
			}
			return charset;
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x000F9554 File Offset: 0x000F7754
		private static Charset DetectOutboundCharset(Item item, OutboundConversionOptions options, object internetCpidObj, bool trustInternetCpid)
		{
			if (options != null && options.DetectionOptions.PreferredCharset != null && (options.DetectionOptions.RequiredCoverage == 0 || (!(internetCpidObj is PropertyError) && trustInternetCpid && options.DetectionOptions.PreferredCharset.CodePage == (int)internetCpidObj)))
			{
				Charset preferredCharset = options.DetectionOptions.PreferredCharset;
				if (ConvertUtils.TryTransformCharset(ref preferredCharset))
				{
					return preferredCharset;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			item.CoreItem.GetCharsetDetectionData(stringBuilder, CharsetDetectionDataFlags.Complete | CharsetDetectionDataFlags.NoMessageDecoding);
			if (item.Body.IsBodyDefined)
			{
				using (TextReader textReader = item.Body.OpenTextReader(BodyFormat.TextPlain))
				{
					char[] array = new char[32768];
					int charCount = textReader.ReadBlock(array, 0, array.Length);
					stringBuilder.Append(array, 0, charCount);
				}
			}
			OutboundCodePageDetector outboundCodePageDetector = new OutboundCodePageDetector();
			outboundCodePageDetector.AddText(stringBuilder.ToString());
			if (options != null && options.DetectionOptions.PreferredCharset != null && outboundCodePageDetector.GetCodePageCoverage(options.DetectionOptions.PreferredCharset.CodePage) >= options.DetectionOptions.RequiredCoverage)
			{
				Charset preferredCharset = options.DetectionOptions.PreferredCharset;
				if (ConvertUtils.TryTransformCharset(ref preferredCharset))
				{
					return preferredCharset;
				}
			}
			if (!(internetCpidObj is PropertyError) && !trustInternetCpid)
			{
				int num = (options != null) ? options.DetectionOptions.RequiredCoverage : 100;
				Charset preferredCharset;
				if (Charset.TryGetCharset((int)internetCpidObj, out preferredCharset) && preferredCharset.IsDetectable && outboundCodePageDetector.GetCodePageCoverage((int)internetCpidObj) >= num && ConvertUtils.TryTransformCharset(ref preferredCharset))
				{
					return preferredCharset;
				}
			}
			if (!trustInternetCpid || (internetCpidObj is PropertyError && !item.Body.IsBodyDefined))
			{
				int codePage = outboundCodePageDetector.GetCodePage();
				Charset preferredCharset;
				if (Charset.TryGetCharset(codePage, out preferredCharset) && ConvertUtils.TryTransformCharset(ref preferredCharset))
				{
					return preferredCharset;
				}
			}
			return null;
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x000F9710 File Offset: 0x000F7910
		private static Charset GetItemOutboundMimeCharsetInternal(Item item, OutboundConversionOptions options)
		{
			object obj = item.TryGetProperty(InternalSchema.InternetCpid);
			bool valueOrDefault = item.GetValueOrDefault<bool>(InternalSchema.IsAutoForwarded, false);
			string className = item.ClassName;
			Charset charset = null;
			if (valueOrDefault || (obj is PropertyError && !item.Body.IsBodyDefined) || (options != null && options.DetectionOptions.PreferredCharset != null && (options.DetectionOptions.RequiredCoverage < 100 || ObjectClass.IsTaskRequest(className) || ObjectClass.IsMeetingMessage(className))))
			{
				charset = ConvertUtils.DetectOutboundCharset(item, options, obj, !valueOrDefault);
				if (charset != null)
				{
					if (!item.CharsetDetector.IsItemCharsetKnownWithoutDetection(BodyCharsetFlags.DisableCharsetDetection, charset, out charset))
					{
						throw new InvalidOperationException();
					}
					return charset;
				}
			}
			if (!(obj is PropertyError) && Charset.TryGetCharset((int)obj, out charset) && ConvertUtils.TryTransformCharset(ref charset))
			{
				return charset;
			}
			object obj2 = item.TryGetProperty(InternalSchema.Codepage);
			if (!(obj2 is PropertyError) && Charset.TryGetCharset((int)obj2, out charset))
			{
				charset = charset.Culture.MimeCharset;
				if (ConvertUtils.TryTransformCharset(ref charset))
				{
					return charset;
				}
			}
			return Charset.GetCharset(65001);
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x000F9818 File Offset: 0x000F7A18
		internal static bool TryGetWindowsCodepageFromInternetCpid(int internetCpid, out int windowsCodepage)
		{
			Charset charset;
			if (Charset.TryGetCharset(internetCpid, out charset))
			{
				charset = ConvertUtils.MapItemWindowsCharset(charset);
				if (charset != null && charset.IsAvailable)
				{
					windowsCodepage = charset.CodePage;
					return true;
				}
			}
			windowsCodepage = 0;
			return false;
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x000F9850 File Offset: 0x000F7A50
		internal static Charset GetItemMimeCharset(ICorePropertyBag propertyBag)
		{
			int valueOrDefault = propertyBag.GetValueOrDefault<int>(InternalSchema.InternetCpid, 0);
			Charset mimeCharset;
			if (ConvertUtils.TryGetValidCharset(valueOrDefault, out mimeCharset))
			{
				return mimeCharset;
			}
			int num = propertyBag.GetValueOrDefault<int>(InternalSchema.Codepage, 0);
			bool flag = false;
			if (num == 1252)
			{
				num = 28605;
				flag = true;
			}
			if (Charset.TryGetCharset(num, out mimeCharset))
			{
				if (!flag)
				{
					mimeCharset = mimeCharset.Culture.MimeCharset;
				}
				if (mimeCharset != null && mimeCharset.IsAvailable)
				{
					return mimeCharset;
				}
			}
			return Culture.Default.MimeCharset;
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x000F98C5 File Offset: 0x000F7AC5
		internal static Charset GetItemWindowsCharset(Item item)
		{
			return ConvertUtils.GetItemWindowsCharset(item, null);
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x000F98D0 File Offset: 0x000F7AD0
		public static Charset GetItemWindowsCharset(Item item, OutboundConversionOptions options)
		{
			int valueOrDefault = item.GetValueOrDefault<int>(InternalSchema.InternetCpid, 0);
			Charset charset;
			if (Charset.TryGetCharset(valueOrDefault, out charset))
			{
				charset = ConvertUtils.MapItemWindowsCharset(charset);
				if (charset != null && charset.IsAvailable)
				{
					return charset;
				}
			}
			int valueOrDefault2 = item.GetValueOrDefault<int>(InternalSchema.Codepage, 0);
			if (ConvertUtils.TryGetValidCharset(valueOrDefault2, out charset))
			{
				return charset;
			}
			if (options != null && options.DetectionOptions.PreferredCharset != null)
			{
				charset = ConvertUtils.MapItemWindowsCharset(options.DetectionOptions.PreferredCharset);
				if (charset != null && charset.IsAvailable)
				{
					return charset;
				}
			}
			return Charset.DefaultWindowsCharset;
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x000F9954 File Offset: 0x000F7B54
		public static Charset MapItemWindowsCharset(Charset charset)
		{
			Charset windowsCharset = charset.Culture.WindowsCharset;
			if (windowsCharset.CodePage == 1200)
			{
				return Charset.DefaultWindowsCharset;
			}
			return windowsCharset;
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x000F9981 File Offset: 0x000F7B81
		public static int GetSystemDefaultCodepage()
		{
			return CodePageMap.GetWindowsCodePage(ConvertUtils.GetSystemDefaultEncoding());
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06003D13 RID: 15635 RVA: 0x000F9990 File Offset: 0x000F7B90
		public static Charset UnicodeCharset
		{
			get
			{
				if (ConvertUtils.unicodeCharset == null)
				{
					Charset charset;
					Charset.TryGetCharset(1200, out charset);
					return ConvertUtils.unicodeCharset = charset;
				}
				return ConvertUtils.unicodeCharset;
			}
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000F99C0 File Offset: 0x000F7BC0
		public static Encoding GetSystemDefaultEncoding()
		{
			Encoding result = null;
			Culture.Default.MimeCharset.TryGetEncoding(out result);
			return result;
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x000F99E2 File Offset: 0x000F7BE2
		internal static CultureInfo GetItemCultureInfo(Item item)
		{
			if (item.Session != null)
			{
				return item.Session.InternalCulture;
			}
			return CultureInfo.CurrentCulture;
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x000F99FD File Offset: 0x000F7BFD
		internal static string GetFailedInboundConversionsDirectory(string logDirectoryPath)
		{
			return ConvertUtils.GetConversionsDirectory(logDirectoryPath, "ContentConversionTracing\\InboundFailures\\", true);
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x000F9A0B File Offset: 0x000F7C0B
		internal static string GetFailedOutboundConversionsDirectory(string logDirectoryPath)
		{
			return ConvertUtils.GetConversionsDirectory(logDirectoryPath, "ContentConversionTracing\\OutboundFailures\\", true);
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06003D18 RID: 15640 RVA: 0x000F9A1C File Offset: 0x000F7C1C
		internal static string ExchangeSetupPath
		{
			get
			{
				string result = null;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
				{
					if (registryKey != null)
					{
						result = (registryKey.GetValue("MsiInstallPath") as string);
					}
				}
				return result;
			}
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000F9A6C File Offset: 0x000F7C6C
		internal static string GetOleConversionsDirectory(string subdir, bool checkIfFull)
		{
			string exchangeSetupPath = ConvertUtils.ExchangeSetupPath;
			if (exchangeSetupPath != null)
			{
				return ConvertUtils.GetConversionsDirectory(exchangeSetupPath, subdir, checkIfFull);
			}
			return null;
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000F9A8C File Offset: 0x000F7C8C
		internal static string GetConversionsDirectory(string parentDirectory, string subdir, bool checkIfFull)
		{
			if (parentDirectory != null && Directory.Exists(parentDirectory))
			{
				try
				{
					string text = Path.Combine(parentDirectory, subdir);
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					else if (checkIfFull)
					{
						DirectoryInfo directoryInfo = new DirectoryInfo(text);
						FileInfo[] files = directoryInfo.GetFiles();
						long num = 0L;
						foreach (FileInfo fileInfo in files)
						{
							num += fileInfo.Length;
						}
						if (num > StorageLimits.Instance.ConversionsFolderMaxTotalMessageSize)
						{
							text = null;
						}
					}
					return text;
				}
				catch (UnauthorizedAccessException arg)
				{
					ExTraceGlobals.CcGenericTracer.TraceError<UnauthorizedAccessException>(0L, "Exception hit when accessing ContentConversion trace logging directory: {0}", arg);
				}
				catch (AccessDeniedException arg2)
				{
					ExTraceGlobals.CcGenericTracer.TraceError<AccessDeniedException>(0L, "Exception hit when accessing ContentConversion trace logging directory: {0}", arg2);
				}
				catch (IOException arg3)
				{
					ExTraceGlobals.CcGenericTracer.TraceError<IOException>(0L, "Exception hit when accessing ContentConversion trace logging directory: {0}", arg3);
				}
			}
			return null;
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000F9B84 File Offset: 0x000F7D84
		private static bool TryGetHexVal(char ch, out int value)
		{
			if (ch >= '0' && ch <= '9')
			{
				value = Convert.ToInt32((int)(ch - '0'));
				return true;
			}
			ch = char.ToUpperInvariant(ch);
			if (ch >= 'A' && ch <= 'F')
			{
				value = Convert.ToInt32((int)(ch - 'A')) + 10;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x000F9BC4 File Offset: 0x000F7DC4
		internal static bool TryDecodeHexByte(string source, int offset, out byte value)
		{
			int num;
			int num2;
			if (ConvertUtils.TryGetHexVal(source[offset], out num) && ConvertUtils.TryGetHexVal(source[offset + 1], out num2))
			{
				value = (byte)(num << 4 | num2);
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x000F9C01 File Offset: 0x000F7E01
		internal static bool IsString8Property(TnefPropertyType propertyType)
		{
			return propertyType == TnefPropertyType.String8 || propertyType == (TnefPropertyType)4126;
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x000F9C12 File Offset: 0x000F7E12
		internal static PropType GetPropertyBaseType(TnefPropertyTag propertyTag)
		{
			return ConvertUtils.GetPropertyType(propertyTag.ValueTnefType);
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x000F9C20 File Offset: 0x000F7E20
		internal static PropType GetPropertyType(TnefPropertyType type)
		{
			return (PropType)type;
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x000F9C24 File Offset: 0x000F7E24
		internal static bool IsSmimeMessage(ICoreItem coreItem, out bool isMultipartSigned, out bool isOpaqueSigned, out StreamAttachment attachment)
		{
			isMultipartSigned = false;
			isOpaqueSigned = false;
			attachment = null;
			string valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			if (!ObjectClass.IsSmime(valueOrDefault))
			{
				return false;
			}
			IList<AttachmentHandle> allHandles = coreItem.AttachmentCollection.GetAllHandles();
			if (allHandles.Count != 1)
			{
				return false;
			}
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreAttachment coreAttachment = coreItem.AttachmentCollection.Open(allHandles[0]);
				disposeGuard.Add<CoreAttachment>(coreAttachment);
				StreamAttachment streamAttachment = (StreamAttachment)AttachmentCollection.CreateTypedAttachment(coreAttachment, new AttachmentType?(AttachmentType.Stream));
				if (streamAttachment == null)
				{
					return false;
				}
				disposeGuard.Add<StreamAttachment>(streamAttachment);
				if (ObjectClass.IsSmimeClearSigned(valueOrDefault))
				{
					isMultipartSigned = ConvertUtils.IsMessageMultipartSigned(coreItem, streamAttachment);
					if (isMultipartSigned)
					{
						attachment = streamAttachment;
						disposeGuard.Success();
						return true;
					}
				}
				isOpaqueSigned = ConvertUtils.IsMessageOpaqueSigned(coreItem, streamAttachment);
				if (isOpaqueSigned)
				{
					attachment = streamAttachment;
					disposeGuard.Success();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x000F9D24 File Offset: 0x000F7F24
		private static bool IsMessageMultipartSigned(ICoreItem coreItem, StreamAttachment attachment)
		{
			string valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			if (!ObjectClass.IsSmimeClearSigned(valueOrDefault))
			{
				return false;
			}
			string contentType = attachment.ContentType;
			return string.Equals(attachment.ContentType, "multipart/signed", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x000F9D70 File Offset: 0x000F7F70
		public static bool IsMessageOpaqueSigned(Item item)
		{
			Util.ThrowOnNullArgument(item, "item");
			ICoreItem coreItem = item.CoreItem;
			string valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			if (!ObjectClass.IsSmime(valueOrDefault))
			{
				return false;
			}
			if (coreItem.AttachmentCollection.Count != 1)
			{
				return false;
			}
			IList<AttachmentHandle> allHandles = coreItem.AttachmentCollection.GetAllHandles();
			bool result;
			using (CoreAttachment coreAttachment = coreItem.AttachmentCollection.Open(allHandles[0]))
			{
				using (StreamAttachment streamAttachment = (StreamAttachment)AttachmentCollection.CreateTypedAttachment(coreAttachment, new AttachmentType?(AttachmentType.Stream)))
				{
					if (streamAttachment == null)
					{
						result = false;
					}
					else
					{
						result = ConvertUtils.IsMessageOpaqueSigned(coreItem, streamAttachment);
					}
				}
			}
			return result;
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x000F9E3C File Offset: 0x000F803C
		private static bool IsMessageOpaqueSigned(ICoreItem coreItem, StreamAttachment attachment)
		{
			Util.ThrowOnNullArgument(coreItem, "coreItem");
			string valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			if (!ObjectClass.IsSmime(valueOrDefault))
			{
				return false;
			}
			coreItem.PropertyBag.Load(new PropertyDefinition[]
			{
				InternalSchema.NamedContentType
			});
			string valueOrDefault2 = coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.NamedContentType);
			if (valueOrDefault2 != null)
			{
				byte[] bytes = CTSGlobals.AsciiEncoding.GetBytes(valueOrDefault2);
				ContentTypeHeader header = (ContentTypeHeader)Header.Create(HeaderId.ContentType);
				MimeInternalHelpers.SetHeaderRawValue(header, bytes);
				string headerParameter = MimeHelpers.GetHeaderParameter(header, "smime-type", 100);
				if (headerParameter == null || (!ConvertUtils.MimeStringEquals(headerParameter, "signed-data") && !ConvertUtils.MimeStringEquals(headerParameter, "certs-only")))
				{
					return false;
				}
			}
			string contentType = attachment.ContentType;
			if (string.Compare(contentType, "application/pkcs7-mime", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(contentType, "application/x-pkcs7-mime", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (string.Compare(contentType, "application/octet-stream", StringComparison.OrdinalIgnoreCase) == 0)
			{
				string fileName = attachment.FileName;
				string strA = string.Empty;
				if (fileName != null)
				{
					string[] array = fileName.Split(new char[]
					{
						'.'
					});
					if (array.Length > 0)
					{
						strA = array[array.Length - 1];
					}
				}
				return string.Compare(strA, "p7m", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(strA, "p7c", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(strA, "p7s", StringComparison.OrdinalIgnoreCase) == 0;
			}
			return false;
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x000F9FA4 File Offset: 0x000F81A4
		internal static Item OpenAttachedItem(ItemAttachment attachment)
		{
			Item result;
			try
			{
				result = attachment.GetItem(InternalSchema.ContentConversionProperties);
			}
			catch (ObjectNotFoundException arg)
			{
				StorageGlobals.ContextTraceDebug<ObjectNotFoundException>(ExTraceGlobals.CcOutboundGenericTracer, "ConvertUtils::OpenAttachedItem - unable to open embedded message, exc = {0}", arg);
				result = null;
			}
			return result;
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x000F9FE8 File Offset: 0x000F81E8
		internal static bool IsValidPCL(int pcl)
		{
			return pcl <= 8 && pcl >= 1;
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x000F9FF7 File Offset: 0x000F81F7
		internal static bool MimeStringEquals(string string1, string string2)
		{
			return string.Equals(string1, string2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x000FA001 File Offset: 0x000F8201
		internal static bool IsRecipientTransmittable(RecipientItemType recipientItemType)
		{
			return recipientItemType == RecipientItemType.To || recipientItemType == RecipientItemType.Cc || recipientItemType == RecipientItemType.Bcc;
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x000FA014 File Offset: 0x000F8214
		internal static bool IsPropertyTransmittable(NativeStorePropertyDefinition property)
		{
			if (property is NamedPropertyDefinition)
			{
				return true;
			}
			PropertyTagPropertyDefinition propertyTagPropertyDefinition = (PropertyTagPropertyDefinition)property;
			return propertyTagPropertyDefinition.IsTransmittable;
		}

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06003D29 RID: 15657 RVA: 0x000FA038 File Offset: 0x000F8238
		internal static UnicodeEncoding UnicodeEncoding
		{
			get
			{
				if (ConvertUtils.unicodeEncoding == null)
				{
					ConvertUtils.unicodeEncoding = new UnicodeEncoding(false, false);
				}
				return ConvertUtils.unicodeEncoding;
			}
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x000FA054 File Offset: 0x000F8254
		public static void SaveDefaultImage(Stream contentStream)
		{
			using (Bitmap bitmap = new Bitmap(1, 1))
			{
				bitmap.Save(contentStream, ImageFormat.Jpeg);
			}
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x000FA094 File Offset: 0x000F8294
		public static double GetOADate(DateTime dateTime)
		{
			return dateTime.ToOADate();
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x000FA09D File Offset: 0x000F829D
		public static DateTime GetDateTimeFromOADate(double dateTime)
		{
			return DateTime.FromOADate(dateTime);
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x000FA0A8 File Offset: 0x000F82A8
		public static bool TryGetImageThumbnail(Stream imageStream, int maxSideInPixels, out byte[] thumbnailByteArray)
		{
			thumbnailByteArray = null;
			Image image;
			try
			{
				image = Image.FromStream(imageStream, true, false);
			}
			catch (ArgumentException arg)
			{
				ExTraceGlobals.StorageTracer.Information<ArgumentException>(0L, "StreamAttachmentBase::TryGetImageThumbnail. Failed to generate thumbnail due to argument exception {0}", arg);
				return false;
			}
			catch (Exception arg2)
			{
				ExTraceGlobals.StorageTracer.TraceError<Exception>(0L, "StreamAttachmentBase::TryGetImageThumbnail. Failed to generate thumbnail due to exception {0}", arg2);
				return false;
			}
			bool result;
			try
			{
				int height = image.Height;
				int width = image.Width;
				int num = Math.Max(height, width);
				int num2 = Math.Min(height, width);
				if (num2 < maxSideInPixels)
				{
					result = false;
				}
				else
				{
					bool flag = height == num;
					int thumbWidth;
					int thumbHeight;
					if (flag)
					{
						thumbWidth = maxSideInPixels;
						thumbHeight = num / num2 * maxSideInPixels;
					}
					else
					{
						thumbHeight = maxSideInPixels;
						thumbWidth = num / num2 * maxSideInPixels;
					}
					using (MemoryStream memoryStream = new MemoryStream())
					{
						try
						{
							using (Image thumbnailImage = image.GetThumbnailImage(thumbWidth, thumbHeight, () => false, IntPtr.Zero))
							{
								thumbnailImage.Save(memoryStream, ImageFormat.Jpeg);
							}
							thumbnailByteArray = memoryStream.ToArray();
							result = true;
						}
						catch (Exception arg3)
						{
							ExTraceGlobals.StorageTracer.TraceError<Exception>(0L, "StreamAttachmentBase::TryGetImageThumbnail. Failed to generate thumbnail due to exception {0}", arg3);
							result = false;
						}
					}
				}
			}
			finally
			{
				if (image != null)
				{
					image.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x000FA22C File Offset: 0x000F842C
		public static InboundConversionOptions GetInboundConversionOptions()
		{
			return new InboundConversionOptions(new EmptyRecipientCache());
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x000FA238 File Offset: 0x000F8438
		public static DateTime GetDateTimeFromXml(string date)
		{
			return XmlConvert.ToDateTime(date, XmlDateTimeSerializationMode.Utc);
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x000FA241 File Offset: 0x000F8441
		public static string GetXmlFromDateTime(DateTime date)
		{
			return XmlConvert.ToString(date, XmlDateTimeSerializationMode.Utc);
		}

		// Token: 0x0400206F RID: 8303
		internal const int CodePageInvalid = 0;

		// Token: 0x04002070 RID: 8304
		internal const int CodePageShiftJIS = 932;

		// Token: 0x04002071 RID: 8305
		internal const int CodePageGb2312 = 936;

		// Token: 0x04002072 RID: 8306
		internal const int CodePageKSC5601 = 949;

		// Token: 0x04002073 RID: 8307
		internal const int CodePageBig5 = 950;

		// Token: 0x04002074 RID: 8308
		internal const int CodePageUnicode = 1200;

		// Token: 0x04002075 RID: 8309
		internal const int CodePageUnicodeFEFF = 1201;

		// Token: 0x04002076 RID: 8310
		internal const int CodePageWindowsAnsi = 1252;

		// Token: 0x04002077 RID: 8311
		internal const int CodePageUsAscii = 20127;

		// Token: 0x04002078 RID: 8312
		internal const int CodePageIso88591 = 28591;

		// Token: 0x04002079 RID: 8313
		internal const int CodePageIso88956 = 28596;

		// Token: 0x0400207A RID: 8314
		internal const int CodePageIso885915 = 28605;

		// Token: 0x0400207B RID: 8315
		internal const int CodePageIso2022Jp = 50220;

		// Token: 0x0400207C RID: 8316
		internal const int CodePageEscJp = 50221;

		// Token: 0x0400207D RID: 8317
		internal const int CodePageSioJp = 50222;

		// Token: 0x0400207E RID: 8318
		internal const int CodePageIso2022Kr = 50225;

		// Token: 0x0400207F RID: 8319
		internal const int CodePageIso2022Chs = 50227;

		// Token: 0x04002080 RID: 8320
		internal const int CodePageIso2022Cht = 50229;

		// Token: 0x04002081 RID: 8321
		internal const int CodePageAutodetectSJis = 50932;

		// Token: 0x04002082 RID: 8322
		internal const int CodePageAutodetectChs = 50936;

		// Token: 0x04002083 RID: 8323
		internal const int CodePageAutodetectKr = 50949;

		// Token: 0x04002084 RID: 8324
		internal const int CodePageAutodetectCht = 50950;

		// Token: 0x04002085 RID: 8325
		internal const int CodePageAutodetectArabic = 51256;

		// Token: 0x04002086 RID: 8326
		internal const int CodePageEucJp = 51932;

		// Token: 0x04002087 RID: 8327
		internal const int CodePageEucChs = 51936;

		// Token: 0x04002088 RID: 8328
		internal const int CodePageEucKr = 51949;

		// Token: 0x04002089 RID: 8329
		internal const int CodePageEucCht = 51950;

		// Token: 0x0400208A RID: 8330
		internal const int CodePageHzChs = 52936;

		// Token: 0x0400208B RID: 8331
		internal const int CodePageGb18030 = 54936;

		// Token: 0x0400208C RID: 8332
		internal const int CodePageUtf7 = 65000;

		// Token: 0x0400208D RID: 8333
		internal const int CodePageUtf8 = 65001;

		// Token: 0x0400208E RID: 8334
		private const string TargetDirDataKeyPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";

		// Token: 0x0400208F RID: 8335
		private const string TargetDirDataKeyName = "MsiInstallPath";

		// Token: 0x04002090 RID: 8336
		private const string InboundFailedConversionsSubdir = "ContentConversionTracing\\InboundFailures\\";

		// Token: 0x04002091 RID: 8337
		private const string OutboundFailedConversionsSubdir = "ContentConversionTracing\\OutboundFailures\\";

		// Token: 0x04002092 RID: 8338
		private const string CodepagePrefix = "InternalXsoCodepage-";

		// Token: 0x04002093 RID: 8339
		private const string CodePage = "Codepage";

		// Token: 0x04002094 RID: 8340
		internal const uint PropertyTypeMask = 4095U;

		// Token: 0x04002095 RID: 8341
		internal const int MaxAttachmentDataCacheSize = 4096;

		// Token: 0x04002096 RID: 8342
		internal const int MinimalCtsCoverterBufferSize = 1024;

		// Token: 0x04002097 RID: 8343
		private const int TnefPriorityHigh = 3;

		// Token: 0x04002098 RID: 8344
		private const int TnefPriorityNormal = 2;

		// Token: 0x04002099 RID: 8345
		private const int TnefPriorityLow = 1;

		// Token: 0x0400209A RID: 8346
		private const int MapiPriorityHigh = 1;

		// Token: 0x0400209B RID: 8347
		private const int MapiPriorityNormal = 0;

		// Token: 0x0400209C RID: 8348
		private const int MapiPriorityLow = -1;

		// Token: 0x0400209D RID: 8349
		private static Charset unicodeCharset = null;

		// Token: 0x0400209E RID: 8350
		private static UnicodeEncoding unicodeEncoding = null;

		// Token: 0x0400209F RID: 8351
		internal static byte[] OidMacBinary = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			20,
			3,
			11,
			1
		};

		// Token: 0x020005CA RID: 1482
		// (Invoke) Token: 0x06003D34 RID: 15668
		internal delegate void CtsCall();

		// Token: 0x020005CB RID: 1483
		// (Invoke) Token: 0x06003D38 RID: 15672
		internal delegate T CtsCallWithReturnValue<T>();
	}
}

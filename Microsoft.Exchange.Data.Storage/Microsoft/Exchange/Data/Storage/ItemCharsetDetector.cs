using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005F4 RID: 1524
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ItemCharsetDetector
	{
		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x001040EF File Offset: 0x001022EF
		// (set) Token: 0x06003EA5 RID: 16037 RVA: 0x001040F7 File Offset: 0x001022F7
		internal BodyCharsetFlags CharsetFlags
		{
			get
			{
				return this.charsetFlags;
			}
			set
			{
				this.charsetFlags = value;
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06003EA6 RID: 16038 RVA: 0x00104100 File Offset: 0x00102300
		// (set) Token: 0x06003EA7 RID: 16039 RVA: 0x00104108 File Offset: 0x00102308
		internal bool NoMessageDecoding
		{
			get
			{
				return this.noMessageDecoding;
			}
			set
			{
				this.noMessageDecoding = value;
			}
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x00104111 File Offset: 0x00102311
		public ItemCharsetDetector(CoreItem coreItem)
		{
			this.coreItem = coreItem;
		}

		// Token: 0x06003EA9 RID: 16041 RVA: 0x0010412B File Offset: 0x0010232B
		public void ResetCachedBody()
		{
			this.cachedBodyData = null;
		}

		// Token: 0x06003EAA RID: 16042 RVA: 0x00104134 File Offset: 0x00102334
		public void SetCachedBody(char[] cachedBodyData)
		{
			this.cachedBodyData = cachedBodyData;
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06003EAB RID: 16043 RVA: 0x0010413D File Offset: 0x0010233D
		// (set) Token: 0x06003EAC RID: 16044 RVA: 0x00104145 File Offset: 0x00102345
		internal CharsetDetectionOptions DetectionOptions
		{
			get
			{
				return this.detectionOptions;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.detectionOptions = new CharsetDetectionOptions(value);
			}
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x0010415C File Offset: 0x0010235C
		public Charset SetCachedBodyDataAndDetectCharset(char[] cachedBodyData, Charset userCharset, BodyCharsetFlags charsetFlags)
		{
			this.cachedBodyData = cachedBodyData;
			this.CharsetFlags = charsetFlags;
			if (userCharset == null)
			{
				userCharset = this.detectionOptions.PreferredCharset;
			}
			Charset charset;
			if (this.IsItemCharsetKnownWithoutDetection(charsetFlags, userCharset, out charset))
			{
				this.SetItemCharset(charset);
				return charset;
			}
			MemoryStream memoryStream;
			int num = this.DetectCpidWithOptions(userCharset, out memoryStream);
			if (!Charset.TryGetCharset(num, out charset) || !charset.IsAvailable)
			{
				ExTraceGlobals.StorageTracer.TraceError<int>((long)this.GetHashCode(), "SetCachedBodyAndDetectCharset: stamping codepage {0} not a valid charset", num);
			}
			this.SetItemCharset(charset);
			return charset;
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x001041D8 File Offset: 0x001023D8
		private void SetItemCharset(Charset charset)
		{
			this.coreItem.LocationIdentifierHelperInstance.SetLocationIdentifier(34677U);
			this.coreItem.PropertyBag.SetProperty(InternalSchema.InternetCpid, charset.CodePage);
			this.coreItem.PropertyBag.SetProperty(InternalSchema.Codepage, ConvertUtils.MapItemWindowsCharset(charset).CodePage);
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x0010423F File Offset: 0x0010243F
		private bool IsCharsetDetectionDisabled(BodyCharsetFlags flags)
		{
			return this.DetectionOptions.RequiredCoverage == 0 || (flags & BodyCharsetFlags.CharsetDetectionMask) == BodyCharsetFlags.DisableCharsetDetection;
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x0010425C File Offset: 0x0010245C
		public void ValidateItemCharset()
		{
			if (this.IsCharsetDetectionDisabled(this.CharsetFlags))
			{
				return;
			}
			int valueOrDefault = this.coreItem.PropertyBag.GetValueOrDefault<int>(InternalSchema.InternetCpid);
			if (ItemCharsetDetector.IsMultipleLanguageCodePage(valueOrDefault))
			{
				if (this.coreItem.Body.RawFormat == BodyFormat.TextHtml && this.coreItem.Body.ForceRedetectHtmlBodyCharset)
				{
					Charset charset;
					Charset targetCharset;
					if (this.TryGetHtmlCharsetFromMetaTag(4096U, out charset) && charset.CodePage != valueOrDefault && Charset.TryGetCharset(valueOrDefault, out targetCharset))
					{
						this.RestampItemCharset(targetCharset, null, charset);
					}
					this.coreItem.Body.ForceRedetectHtmlBodyCharset = false;
				}
				return;
			}
			Charset charset2;
			if (ConvertUtils.TryGetValidCharset(valueOrDefault, out charset2) && charset2.IsDetectable)
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.coreItem.GetCharsetDetectionData(stringBuilder, CharsetDetectionDataFlags.None);
				if (stringBuilder.Length == 0)
				{
					return;
				}
				OutboundCodePageDetector outboundCodePageDetector = new OutboundCodePageDetector();
				outboundCodePageDetector.AddText(stringBuilder.ToString());
				if (outboundCodePageDetector.GetCodePageCoverage(valueOrDefault) >= this.DetectionOptions.RequiredCoverage)
				{
					return;
				}
			}
			MemoryStream cachedHtml;
			int num = this.DetectCpidWithOptions(null, out cachedHtml);
			Charset charset3;
			if (Charset.TryGetCharset(num, out charset3) && charset3.IsAvailable)
			{
				this.RestampItemCharset(charset3, cachedHtml, null);
				return;
			}
			ExTraceGlobals.StorageTracer.TraceError<int>((long)this.GetHashCode(), "ValidateItemCharset: detected codepage {0} is not valid", num);
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x00104397 File Offset: 0x00102597
		public static bool IsMultipleLanguageCodePage(int internetCpid)
		{
			return internetCpid == 65001 || internetCpid == 65000 || internetCpid == 1200 || internetCpid == 1201 || internetCpid == 54936;
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x001043C4 File Offset: 0x001025C4
		private int DetectCpidWithOptions(Charset userCharset, out MemoryStream cachedHtmlBody)
		{
			cachedHtmlBody = null;
			int cpid;
			if (this.IsCharsetDetectionDisabled(this.CharsetFlags) && userCharset != null)
			{
				cpid = userCharset.CodePage;
			}
			else
			{
				cpid = this.DetectCpid(userCharset, out cachedHtmlBody);
			}
			return this.OverrideCodePage(this.CharsetFlags, cpid);
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x00104408 File Offset: 0x00102608
		private OutboundCodePageDetector BuildCodePageDetector(out MemoryStream cachedHtmlBody)
		{
			cachedHtmlBody = null;
			OutboundCodePageDetector outboundCodePageDetector = new OutboundCodePageDetector();
			StringBuilder stringBuilder = new StringBuilder();
			CharsetDetectionDataFlags charsetDetectionDataFlags = CharsetDetectionDataFlags.Complete;
			if (this.coreItem.CharsetDetector.NoMessageDecoding && ItemBuilder.ReadStoreObjectTypeFromPropertyBag(this.coreItem.PropertyBag) == StoreObjectType.RightsManagedMessage)
			{
				charsetDetectionDataFlags |= CharsetDetectionDataFlags.NoMessageDecoding;
			}
			this.coreItem.GetCharsetDetectionData(stringBuilder, charsetDetectionDataFlags);
			outboundCodePageDetector.AddText(stringBuilder.ToString());
			if (this.cachedBodyData == null)
			{
				this.cachedBodyData = this.LoadCachedBodyData(out cachedHtmlBody);
			}
			if (this.cachedBodyData != null)
			{
				outboundCodePageDetector.AddText(this.cachedBodyData);
			}
			return outboundCodePageDetector;
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x00104494 File Offset: 0x00102694
		private int DetectCpid(Charset userCharset, out MemoryStream cachedHtmlBody)
		{
			OutboundCodePageDetector outboundCodePageDetector = this.BuildCodePageDetector(out cachedHtmlBody);
			int codePage;
			if (userCharset != null && userCharset.IsDetectable && outboundCodePageDetector.GetCodePageCoverage(userCharset.CodePage) >= this.DetectionOptions.RequiredCoverage)
			{
				codePage = userCharset.CodePage;
			}
			else
			{
				codePage = outboundCodePageDetector.GetCodePage(userCharset, false);
			}
			return codePage;
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x001044E4 File Offset: 0x001026E4
		private void RestampItemCharset(Charset targetCharset, MemoryStream cachedHtml, Charset htmlCharsetDetectedFromMetaTag = null)
		{
			Charset charset = htmlCharsetDetectedFromMetaTag ?? ConvertUtils.GetItemMimeCharset(this.coreItem.PropertyBag);
			BodyFormat rawFormat = this.coreItem.Body.RawFormat;
			if (rawFormat == BodyFormat.TextHtml)
			{
				using (MemoryStream memoryStream = cachedHtml ?? this.LoadHtmlBodyInMemory())
				{
					memoryStream.Position = 0L;
					using (Stream stream = this.coreItem.Body.InternalOpenBodyStream(InternalSchema.HtmlBody, PropertyOpenMode.Create))
					{
						using (Stream stream2 = new ConverterStream(stream, new HtmlToHtml
						{
							InputEncoding = charset.GetEncoding(),
							OutputEncoding = targetCharset.GetEncoding(),
							DetectEncodingFromMetaTag = false
						}, ConverterStreamAccess.Write))
						{
							Util.StreamHandler.CopyStreamData(memoryStream, stream2);
						}
					}
				}
			}
			this.SetItemCharset(targetCharset);
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x001045DC File Offset: 0x001027DC
		private bool TryGetHtmlCharsetFromMetaTag(uint maxBytesToSearch, out Charset htmlBodyCharset)
		{
			htmlBodyCharset = null;
			using (MemoryStream memoryStream = this.LoadHtmlBodyInMemory())
			{
				memoryStream.Position = 0L;
				Charset itemMimeCharset = ConvertUtils.GetItemMimeCharset(this.coreItem.PropertyBag);
				using (HtmlReader htmlReader = new HtmlReader(memoryStream, itemMimeCharset.GetEncoding()))
				{
					while (htmlReader.ReadNextToken())
					{
						if ((long)htmlReader.CurrentOffset > (long)((ulong)maxBytesToSearch))
						{
							return false;
						}
						if (htmlReader.TokenKind == HtmlTokenKind.EmptyElementTag && htmlReader.TagId == HtmlTagId.Meta)
						{
							bool flag = false;
							bool flag2 = false;
							string text = string.Empty;
							HtmlAttributeReader attributeReader = htmlReader.AttributeReader;
							while (!flag && attributeReader.ReadNext())
							{
								if (attributeReader.Id == HtmlAttributeId.Charset && attributeReader.HasValue)
								{
									text = attributeReader.ReadValue();
									flag = true;
								}
								else if (attributeReader.Id == HtmlAttributeId.Content && attributeReader.HasValue)
								{
									text = attributeReader.ReadValue();
									if (flag2)
									{
										flag = true;
									}
								}
								else if (attributeReader.Id == HtmlAttributeId.HttpEquiv && attributeReader.HasValue)
								{
									string a = attributeReader.ReadValue();
									if (string.Equals(a, "content-type", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "charset", StringComparison.OrdinalIgnoreCase))
									{
										flag2 = true;
										if (!string.IsNullOrEmpty(text))
										{
											flag = true;
										}
									}
								}
							}
							if (flag)
							{
								text = HtmlReader.CharsetFromString(text, flag2);
								if (!string.IsNullOrEmpty(text) && Charset.TryGetCharset(text, out htmlBodyCharset))
								{
									return true;
								}
								return false;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x00104788 File Offset: 0x00102988
		private char[] LoadCachedBodyData(out MemoryStream htmlBody)
		{
			htmlBody = null;
			if (!this.coreItem.Body.IsBodyDefined)
			{
				return null;
			}
			BodyReadConfiguration configuration = new BodyReadConfiguration(BodyFormat.TextPlain);
			if (this.coreItem.Body.RawFormat == BodyFormat.TextHtml)
			{
				htmlBody = this.LoadHtmlBodyInMemory();
				htmlBody.Position = 0L;
				using (TextReader textReader = new BodyTextReader(this.coreItem, configuration, new StreamWrapper(htmlBody, false)))
				{
					return Util.StreamHandler.ReadCharBuffer(textReader, 16384);
				}
			}
			char[] result;
			using (TextReader textReader2 = new BodyTextReader(this.coreItem, configuration, null))
			{
				result = Util.StreamHandler.ReadCharBuffer(textReader2, 16384);
			}
			return result;
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x00104848 File Offset: 0x00102A48
		private MemoryStream LoadHtmlBodyInMemory()
		{
			MemoryStream memoryStream = new MemoryStream();
			using (Stream stream = this.coreItem.Body.InternalOpenBodyStream(InternalSchema.HtmlBody, PropertyOpenMode.ReadOnly))
			{
				Util.StreamHandler.CopyStreamData(stream, memoryStream);
			}
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x001048A0 File Offset: 0x00102AA0
		private int OverrideCodePage(BodyCharsetFlags charsetFlags, int cpid)
		{
			if (cpid == 936)
			{
				if ((charsetFlags & BodyCharsetFlags.PreferGB18030) != BodyCharsetFlags.PreferGB18030)
				{
					return 936;
				}
				return 54936;
			}
			else if (cpid == 28591)
			{
				if ((charsetFlags & BodyCharsetFlags.PreferIso885915) != BodyCharsetFlags.PreferIso885915)
				{
					return 28591;
				}
				return 28605;
			}
			else if (cpid == 932)
			{
				if ((charsetFlags & BodyCharsetFlags.DoNotPreferIso2022jp) == BodyCharsetFlags.DoNotPreferIso2022jp)
				{
					return 932;
				}
				return this.detectionOptions.PreferredInternetCodePageForShiftJis;
			}
			else
			{
				if (cpid == 1200 || cpid == 1201)
				{
					return 65001;
				}
				return cpid;
			}
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x00104930 File Offset: 0x00102B30
		private Charset OverrideCharset(BodyCharsetFlags flags, Charset charset)
		{
			int num = this.OverrideCodePage(flags, charset.CodePage);
			if (charset.CodePage != num)
			{
				return Charset.GetCharset(num);
			}
			return charset;
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x0010495C File Offset: 0x00102B5C
		internal bool IsItemCharsetKnownWithoutDetection(BodyCharsetFlags flags, Charset userCharset, out Charset targetCharset)
		{
			targetCharset = null;
			if (userCharset == null)
			{
				userCharset = this.detectionOptions.PreferredCharset;
			}
			if (userCharset == null)
			{
				return false;
			}
			if (this.IsCharsetDetectionDisabled(flags))
			{
				targetCharset = this.OverrideCharset(flags, userCharset);
				return true;
			}
			if (userCharset.CodePage == 54936)
			{
				targetCharset = userCharset;
				return true;
			}
			if (BodyCharsetFlags.PreserveUnicode == (BodyCharsetFlags.PreserveUnicode & flags))
			{
				Charset charset = this.OverrideCharset(flags, userCharset);
				if (charset.CodePage == 65000 || charset.CodePage == 65001)
				{
					targetCharset = charset;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400217B RID: 8571
		private const int DefaultDetectionBufferCharacters = 16384;

		// Token: 0x0400217C RID: 8572
		private readonly CoreItem coreItem;

		// Token: 0x0400217D RID: 8573
		private char[] cachedBodyData;

		// Token: 0x0400217E RID: 8574
		private BodyCharsetFlags charsetFlags;

		// Token: 0x0400217F RID: 8575
		private CharsetDetectionOptions detectionOptions = new CharsetDetectionOptions();

		// Token: 0x04002180 RID: 8576
		private bool noMessageDecoding;
	}
}

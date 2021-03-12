using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.TextConverters.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005A6 RID: 1446
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Body : IBody
	{
		// Token: 0x06003B2A RID: 15146 RVA: 0x000F3164 File Offset: 0x000F1364
		internal Body(ICoreItem coreItem)
		{
			Util.ThrowOnNullArgument(coreItem, "coreItem");
			this.coreItem = coreItem;
			this.bodyReadStreams = new List<Body.IBodyStream>(1);
			this.ForceRedetectHtmlBodyCharset = false;
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06003B2B RID: 15147 RVA: 0x000F31BC File Offset: 0x000F13BC
		public string Charset
		{
			get
			{
				string result = string.Empty;
				switch (this.CheckBody())
				{
				case BodyFormat.TextPlain:
				case BodyFormat.TextHtml:
				case BodyFormat.ApplicationRtf:
					result = ConvertUtils.GetItemMimeCharset(this.coreItem.PropertyBag).Name;
					break;
				}
				return result;
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06003B2C RID: 15148 RVA: 0x000F3208 File Offset: 0x000F1408
		public Uri ContentBase
		{
			get
			{
				string valueOrDefault = this.coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.BodyContentBase, null);
				Uri result = null;
				if (Uri.TryCreate(valueOrDefault, UriKind.Absolute, out result))
				{
					return result;
				}
				return null;
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06003B2D RID: 15149 RVA: 0x000F323C File Offset: 0x000F143C
		public Uri ContentLocation
		{
			get
			{
				string valueOrDefault = this.coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.BodyContentLocation, null);
				Uri result = null;
				if (Uri.TryCreate(valueOrDefault, UriKind.RelativeOrAbsolute, out result))
				{
					return result;
				}
				return null;
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06003B2E RID: 15150 RVA: 0x000F3270 File Offset: 0x000F1470
		public BodyFormat Format
		{
			get
			{
				return this.CheckBody();
			}
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06003B2F RID: 15151 RVA: 0x000F3278 File Offset: 0x000F1478
		public string PreviewText
		{
			get
			{
				try
				{
					if (this.cachedPreviewText == null)
					{
						BodyReadConfiguration configuration = new BodyReadConfiguration(BodyFormat.TextPlain)
						{
							ShouldOutputAnchorLinks = false,
							ShouldOutputImageLinks = false
						};
						using (TextReader textReader = this.OpenTextReader(configuration))
						{
							this.cachedPreviewText = new string(Util.StreamHandler.ReadCharBuffer(textReader, 255));
						}
					}
				}
				catch (ConversionFailedException arg)
				{
					ExTraceGlobals.CcBodyTracer.TraceError<ConversionFailedException>((long)this.GetHashCode(), "Body.PreviewText: throwing {0}", arg);
					this.cachedPreviewText = string.Empty;
				}
				return this.cachedPreviewText;
			}
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x000F3318 File Offset: 0x000F1518
		public long Size
		{
			get
			{
				long result = 0L;
				this.ChooseBestBody();
				BodyFormat rawFormat = this.RawFormat;
				if (this.noBody)
				{
					return result;
				}
				StorePropertyDefinition propertyDefinition = null;
				try
				{
					switch (rawFormat)
					{
					case BodyFormat.TextPlain:
						propertyDefinition = InternalSchema.TextBody;
						break;
					case BodyFormat.TextHtml:
						propertyDefinition = InternalSchema.HtmlBody;
						break;
					case BodyFormat.ApplicationRtf:
						propertyDefinition = InternalSchema.RtfBody;
						break;
					}
					using (Stream stream = this.coreItem.PropertyBag.OpenPropertyStream(propertyDefinition, PropertyOpenMode.ReadOnly))
					{
						result = stream.Length;
					}
				}
				catch (ObjectNotFoundException)
				{
				}
				return result;
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06003B31 RID: 15153 RVA: 0x000F33BC File Offset: 0x000F15BC
		public bool IsBodyChanged
		{
			get
			{
				return this.isBodyChanged;
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06003B32 RID: 15154 RVA: 0x000F33C4 File Offset: 0x000F15C4
		public bool IsPreviewInvalid
		{
			get
			{
				return this.isPreviewInvalid;
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06003B33 RID: 15155 RVA: 0x000F33CC File Offset: 0x000F15CC
		// (set) Token: 0x06003B34 RID: 15156 RVA: 0x000F33D4 File Offset: 0x000F15D4
		public bool ForceRedetectHtmlBodyCharset { get; set; }

		// Token: 0x06003B35 RID: 15157 RVA: 0x000F33DD File Offset: 0x000F15DD
		public void NotifyPreviewNeedsUpdated()
		{
			this.isPreviewInvalid = true;
			this.cachedPreviewText = null;
			this.ResetBodyFormat();
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06003B36 RID: 15158 RVA: 0x000F33F4 File Offset: 0x000F15F4
		internal Charset RawCharset
		{
			get
			{
				switch (this.RawFormat)
				{
				case BodyFormat.TextPlain:
					return ConvertUtils.UnicodeCharset;
				case BodyFormat.TextHtml:
				case BodyFormat.ApplicationRtf:
					return ConvertUtils.GetItemMimeCharset(this.coreItem.PropertyBag);
				default:
					return ConvertUtils.UnicodeCharset;
				}
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06003B37 RID: 15159 RVA: 0x000F343A File Offset: 0x000F163A
		public bool IsBodyDefined
		{
			get
			{
				this.CheckBody();
				return !this.noBody;
			}
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06003B38 RID: 15160 RVA: 0x000F344C File Offset: 0x000F164C
		internal BodyFormat RawFormat
		{
			get
			{
				this.CalculateRawFormat();
				return (BodyFormat)this.rawBodyFormat;
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06003B39 RID: 15161 RVA: 0x000F345A File Offset: 0x000F165A
		internal bool IsRtfEmbeddedBody
		{
			get
			{
				this.CheckBody();
				return this.isEmbeddedPlainText;
			}
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x000F346C File Offset: 0x000F166C
		public static void CopyBody(Item source, Item target, bool disableCharsetDetection = false)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			bool removeMungageData = target is CalendarItemBase;
			Body.CopyBody(source.Body, target.Body, source.Session.PreferedCulture, removeMungageData, disableCharsetDetection);
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x000F34BD File Offset: 0x000F16BD
		public static void CopyBody(Body source, Body target, CultureInfo cultureInfo, bool removeMungageData, bool disableCharsetDetection = false)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			Body.InternalCopyBody(source, target, cultureInfo, removeMungageData, null, BodyInjectionFormat.Text, disableCharsetDetection);
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x000F34E8 File Offset: 0x000F16E8
		public void CopyBodyInjectingText(IBody targetBody, BodyInjectionFormat injectionFormat, string prefixInjectionText, string suffixInjectionText)
		{
			if (string.IsNullOrEmpty(prefixInjectionText) && string.IsNullOrEmpty(suffixInjectionText))
			{
				return;
			}
			BodyFormat bodyFormat = this.Format;
			if (bodyFormat == BodyFormat.ApplicationRtf)
			{
				bodyFormat = BodyFormat.TextHtml;
			}
			using (Stream stream = this.OpenReadStream(new BodyReadConfiguration(bodyFormat, this.Charset)))
			{
				BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(bodyFormat, this.Charset);
				bodyWriteConfiguration.SetTargetFormat(this.Format, this.Charset);
				bodyWriteConfiguration.AddInjectedText(prefixInjectionText, suffixInjectionText, injectionFormat);
				using (Stream stream2 = targetBody.OpenWriteStream(bodyWriteConfiguration))
				{
					Util.StreamHandler.CopyStreamData(stream, stream2, null, 0, 65536);
				}
			}
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x000F35A4 File Offset: 0x000F17A4
		public int GetLastNBytesAsString(int lastNBytesToRead, out string readString)
		{
			int result;
			using (Stream stream = BodyReadStream.OpenBodyStream(this.coreItem))
			{
				readString = null;
				byte[] bytes;
				int num = Util.StreamHandler.ReadLastBytesOfStream(stream, lastNBytesToRead, out bytes);
				if (num > 0)
				{
					readString = this.GetBodyEncoding().GetString(bytes, 0, num);
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x000F3600 File Offset: 0x000F1800
		public Stream OpenReadStream(BodyReadConfiguration configuration)
		{
			return this.InternalOpenReadStream(configuration, true);
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x000F360C File Offset: 0x000F180C
		public TextReader OpenTextReader(BodyFormat bodyFormat)
		{
			BodyReadConfiguration configuration = new BodyReadConfiguration(bodyFormat);
			return this.OpenTextReader(configuration);
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x000F3628 File Offset: 0x000F1828
		public TextReader OpenTextReader(BodyReadConfiguration configuration)
		{
			Body.CheckNull(configuration, "configuration");
			this.CheckStreamingExceptions();
			TextReader result;
			lock (this.bodyStreamsLock)
			{
				this.CheckOpenBodyStreamForRead();
				BodyTextReader bodyTextReader = new BodyTextReader(this.coreItem, configuration, null);
				this.bodyReadStreams.Add(bodyTextReader);
				result = bodyTextReader;
			}
			return result;
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x000F3698 File Offset: 0x000F1898
		public Stream OpenWriteStream(BodyWriteConfiguration configuration)
		{
			Body.CheckNull(configuration, "configuration");
			if ((configuration.SourceFormat == BodyFormat.TextPlain || configuration.SourceFormat == BodyFormat.TextHtml) && configuration.SourceCharset == null)
			{
				throw new InvalidOperationException("Body.OpenWriteStream - source charset is undefined");
			}
			return this.InternalOpenWriteStream(configuration, null);
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x000F36D4 File Offset: 0x000F18D4
		public TextWriter OpenTextWriter(BodyFormat bodyFormat)
		{
			BodyWriteConfiguration configuration = new BodyWriteConfiguration(bodyFormat);
			return this.OpenTextWriter(configuration);
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x000F36F0 File Offset: 0x000F18F0
		public TextWriter OpenTextWriter(BodyWriteConfiguration configuration)
		{
			Body.CheckNull(configuration, "configuration");
			BodyTextWriter result;
			lock (this.bodyStreamsLock)
			{
				this.CheckOpenBodyStreamForWrite();
				result = new BodyTextWriter(this.coreItem, configuration, null);
				this.bodyWriteStream = result;
			}
			this.BodyChanged(configuration);
			this.bodyStreamingException = null;
			this.isBodyChanged = true;
			this.cachedPreviewText = null;
			return result;
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x000F3770 File Offset: 0x000F1970
		public byte[] CalculateBodyTag()
		{
			int num;
			return this.CalculateBodyTag(out num);
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x000F3788 File Offset: 0x000F1988
		public byte[] CalculateBodyTag(out int latestMessagePartWordCount)
		{
			latestMessagePartWordCount = int.MinValue;
			if (this.IsBodyDefined && this.Size / 2048L <= 2048L)
			{
				try
				{
					ConversationBodyScanner conversationBodyScanner = this.GetConversationBodyScanner();
					latestMessagePartWordCount = conversationBodyScanner.CalculateLatestMessagePartWordCount();
					BodyFragmentInfo bodyFragmentInfo = new BodyFragmentInfo(conversationBodyScanner);
					return bodyFragmentInfo.BodyTag.ToByteArray();
				}
				catch (TextConvertersException)
				{
					return new byte[Body.BodyTagLength];
				}
			}
			if (ObjectClass.IsSmime(this.coreItem.ClassName()) && !ObjectClass.IsSmimeClearSigned(this.coreItem.ClassName()))
			{
				Item item = null;
				try
				{
					InboundConversionOptions inboundConversionOptions = ConvertUtils.GetInboundConversionOptions();
					if (ItemConversion.TryOpenSMimeContent(this.coreItem, inboundConversionOptions, out item))
					{
						return item.Body.CalculateBodyTag(out latestMessagePartWordCount);
					}
				}
				finally
				{
					if (item != null)
					{
						item.Dispose();
					}
				}
			}
			return new byte[12];
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x000F386C File Offset: 0x000F1A6C
		public void ResetBodyFormat()
		{
			this.bodyFormat = -1;
			this.rawBodyFormat = -1;
			this.bodyFormatDecision = -1;
			this.coreItem.CharsetDetector.ResetCachedBody();
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x000F3894 File Offset: 0x000F1A94
		private static void InternalCopyBody(Body source, Body target, CultureInfo cultureInfo, bool removeMungageData, string prefix, BodyInjectionFormat prefixFormat, bool disableCharsetDetection = false)
		{
			BodyReadConfiguration bodyReadConfiguration = new BodyReadConfiguration(source.RawFormat, source.RawCharset.Name);
			BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(source.RawFormat, source.RawCharset.Name);
			if (disableCharsetDetection)
			{
				bodyWriteConfiguration.SetTargetFormat(source.RawFormat, source.Charset, BodyCharsetFlags.DisableCharsetDetection);
			}
			else
			{
				bodyWriteConfiguration.SetTargetFormat(source.RawFormat, source.Charset);
			}
			if (!string.IsNullOrEmpty(prefix))
			{
				bodyWriteConfiguration.AddInjectedText(prefix, null, prefixFormat);
			}
			bool flag = false;
			if (removeMungageData)
			{
				flag = Body.CopyBodyWithoutMungage(source, target, cultureInfo, bodyReadConfiguration, bodyWriteConfiguration);
			}
			if (!flag)
			{
				using (Stream stream = source.OpenReadStream(bodyReadConfiguration))
				{
					using (Stream stream2 = target.OpenWriteStream(bodyWriteConfiguration))
					{
						Util.StreamHandler.CopyStreamData(stream, stream2, null, 0, 16384);
					}
				}
			}
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x000F3984 File Offset: 0x000F1B84
		internal static Stream GetEmptyStream()
		{
			byte[] buffer = new byte[1];
			return new MemoryStream(buffer, 0, 0, false);
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x000F39A1 File Offset: 0x000F1BA1
		internal static bool IsUtfCpid(int codepage)
		{
			return codepage == 1200 || codepage == 1201 || codepage == 65000 || codepage == 65001 || codepage == 54936;
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x000F39D0 File Offset: 0x000F1BD0
		internal static StorePropertyDefinition GetBodyProperty(BodyFormat bodyFormat)
		{
			switch (bodyFormat)
			{
			case BodyFormat.TextPlain:
				return InternalSchema.TextBody;
			case BodyFormat.TextHtml:
				return InternalSchema.HtmlBody;
			case BodyFormat.ApplicationRtf:
				return InternalSchema.RtfBody;
			default:
				throw new ArgumentOutOfRangeException("bodyFormat", string.Format("Invalid body format: {0}.", bodyFormat));
			}
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x000F3A24 File Offset: 0x000F1C24
		internal static int ReadChars(TextReader reader, char[] buffer, int length)
		{
			int num;
			int num2;
			for (num = 0; num != length; num += num2)
			{
				num2 = reader.Read(buffer, num, length - num);
				if (num2 == 0)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x000F3A4D File Offset: 0x000F1C4D
		internal void Reset()
		{
			this.ResetBodyFormat();
			this.isBodyChanged = false;
			this.isPreviewInvalid = false;
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x000F3A63 File Offset: 0x000F1C63
		internal Stream TryOpenReadStream(BodyReadConfiguration configuration)
		{
			return this.InternalOpenReadStream(configuration, false);
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x000F3A70 File Offset: 0x000F1C70
		internal ConversationBodyTextReader OpenConversationTextReader(BodyReadConfiguration configuration, long bytesLoadedForConversation, long maxBytesForConversation)
		{
			Body.CheckNull(configuration, "configuration");
			this.CheckStreamingExceptions();
			ConversationBodyTextReader result;
			lock (this.bodyStreamsLock)
			{
				this.CheckOpenBodyStreamForRead();
				ConversationBodyTextReader conversationBodyTextReader = new ConversationBodyTextReader(this.coreItem, configuration, null, bytesLoadedForConversation, maxBytesForConversation);
				this.bodyReadStreams.Add(conversationBodyTextReader);
				result = conversationBodyTextReader;
			}
			return result;
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x000F3AE0 File Offset: 0x000F1CE0
		internal Stream InternalOpenWriteStream(BodyWriteConfiguration configuration, Stream outputStream)
		{
			Stream result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				BodyWriteStream bodyWriteStream;
				lock (this.bodyStreamsLock)
				{
					this.CheckOpenBodyStreamForWrite();
					bodyWriteStream = new BodyWriteStream(this.coreItem, configuration, outputStream);
					disposeGuard.Add<BodyWriteStream>(bodyWriteStream);
					this.bodyWriteStream = bodyWriteStream;
				}
				this.BodyChanged(configuration);
				this.bodyStreamingException = null;
				this.isBodyChanged = true;
				this.cachedPreviewText = null;
				disposeGuard.Success();
				result = bodyWriteStream;
			}
			return result;
		}

		// Token: 0x06003B50 RID: 15184 RVA: 0x000F3B8C File Offset: 0x000F1D8C
		internal void SetBodyStreamingException(ExchangeDataException exc)
		{
			this.bodyStreamingException = exc;
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000F3B98 File Offset: 0x000F1D98
		internal void CheckStreamingExceptions()
		{
			if (this.bodyStreamingException != null)
			{
				Exception ex = new CorruptDataException(ServerStrings.ConversionBodyCorrupt, this.bodyStreamingException);
				ExTraceGlobals.CcBodyTracer.TraceError<Exception>((long)this.GetHashCode(), "Body.CheckStreamingExceptions: throwing {0}", ex);
				throw ex;
			}
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x000F3BD7 File Offset: 0x000F1DD7
		internal void ValidateBody()
		{
			this.coreItem.Body.CheckStreamingExceptions();
			this.coreItem.CharsetDetector.ValidateItemCharset();
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x000F3BFC File Offset: 0x000F1DFC
		internal Stream InternalOpenBodyStream(StorePropertyDefinition bodyProperty, PropertyOpenMode openMode)
		{
			if (openMode != PropertyOpenMode.ReadOnly)
			{
				IDirectPropertyBag directPropertyBag = (IDirectPropertyBag)this.coreItem.PropertyBag;
				directPropertyBag.SetValue(InternalSchema.TextBody, Body.TextNotFoundPropertyError);
				directPropertyBag.SetValue(InternalSchema.HtmlBody, Body.HtmlNotFoundPropertyError);
				directPropertyBag.SetValue(InternalSchema.RtfBody, Body.RtfNotFoundPropertyError);
			}
			return this.coreItem.PropertyBag.OpenPropertyStream(bodyProperty, openMode);
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x000F3C60 File Offset: 0x000F1E60
		internal Encoding GetBodyEncoding()
		{
			Charset itemMimeCharset = ConvertUtils.GetItemMimeCharset(this.coreItem.PropertyBag);
			Encoding result = null;
			itemMimeCharset.TryGetEncoding(out result);
			return result;
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x000F3C8C File Offset: 0x000F1E8C
		public ConversationBodyScanner GetConversationBodyScanner()
		{
			long num = 0L;
			return this.GetConversationBodyScanner(null, -1L, 0L, false, false, out num);
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x000F3CAC File Offset: 0x000F1EAC
		public ConversationBodyScanner GetConversationBodyScanner(HtmlCallbackBase callback, long maxBytes, long bytesLoaded, bool fixCharset, bool filterHtml, out long bytesRead)
		{
			ConversationBodyScanner conversationBodyScanner = null;
			StorePropertyDefinition bodyProperty = null;
			BodyFormat bodyFormat;
			if (fixCharset)
			{
				bodyFormat = this.RawFormat;
			}
			else
			{
				bodyFormat = this.Format;
			}
			switch (bodyFormat)
			{
			case BodyFormat.TextPlain:
			{
				TextConversationBodyScanner textConversationBodyScanner = new TextConversationBodyScanner();
				conversationBodyScanner = textConversationBodyScanner;
				if (fixCharset)
				{
					textConversationBodyScanner.InputEncoding = ConvertUtils.UnicodeEncoding;
				}
				else
				{
					textConversationBodyScanner.InputEncoding = this.GetBodyEncoding();
				}
				if (this.IsRtfEmbeddedBody)
				{
					bodyProperty = ItemSchema.RtfBody;
				}
				else
				{
					bodyProperty = ItemSchema.TextBody;
				}
				break;
			}
			case BodyFormat.TextHtml:
			{
				HtmlConversationBodyScanner htmlConversationBodyScanner = new HtmlConversationBodyScanner();
				conversationBodyScanner = htmlConversationBodyScanner;
				htmlConversationBodyScanner.InputEncoding = this.GetBodyEncoding();
				htmlConversationBodyScanner.DetectEncodingFromMetaTag = false;
				bodyProperty = ItemSchema.HtmlBody;
				break;
			}
			case BodyFormat.ApplicationRtf:
				conversationBodyScanner = new RtfConversationBodyScanner();
				bodyProperty = ItemSchema.RtfBody;
				break;
			}
			conversationBodyScanner.FilterHtml = filterHtml;
			if (callback != null)
			{
				conversationBodyScanner.HtmlCallback = new HtmlTagCallback(callback.ProcessTag);
			}
			if (this.IsBodyDefined)
			{
				using (Stream stream = this.InternalOpenBodyStream(bodyProperty, PropertyOpenMode.ReadOnly))
				{
					bytesRead = stream.Length;
					if (maxBytes > -1L && bytesRead + bytesLoaded > maxBytes)
					{
						throw new MessageLoadFailedInConversationException(new LocalizedString("Message body size exceeded the conversation threshold for loading"));
					}
					if (this.RawFormat == BodyFormat.ApplicationRtf)
					{
						using (Stream stream2 = new ConverterStream(stream, new RtfCompressedToRtf(), ConverterStreamAccess.Read))
						{
							conversationBodyScanner.Load(stream2);
							goto IL_12F;
						}
					}
					conversationBodyScanner.Load(stream);
					IL_12F:
					return conversationBodyScanner;
				}
			}
			bytesRead = 0L;
			MemoryStream sourceStream = new MemoryStream(0);
			conversationBodyScanner.Load(sourceStream);
			return conversationBodyScanner;
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x000F3E28 File Offset: 0x000F2028
		private static bool CopyBodyWithoutMungage(Body source, Body target, CultureInfo cultureInfo, BodyReadConfiguration readConfiguration, BodyWriteConfiguration writeConfiguration)
		{
			bool result = false;
			switch (source.RawFormat)
			{
			case BodyFormat.TextPlain:
			{
				bool flag = true;
				goto IL_2B;
			}
			case BodyFormat.ApplicationRtf:
			{
				bool flag = false;
				goto IL_2B;
			}
			}
			return false;
			IL_2B:
			using (Stream stream = source.OpenReadStream(readConfiguration))
			{
				Stream stream2 = null;
				try
				{
					bool flag;
					stream2 = (flag ? stream : new ConverterStream(stream, new RtfCompressedToRtf(), ConverterStreamAccess.Read));
					byte[] array = new byte[16384];
					int num = stream2.Read(array, 0, array.Length);
					if (num > 0)
					{
						string @string = readConfiguration.Encoding.GetString(array, 0, num);
						string pattern = string.Format("{0}[\\s\\S]+(^.*{1}[\\s\\S]+)?^.*\\*~\\*~\\*~\\*~\\*~\\*~\\*~\\*~\\*~\\*(\\\\line)?\\r\\n", Regex.Escape(ClientStrings.WhenPart.ToString(cultureInfo)), Regex.Escape(ClientStrings.WherePart.ToString(cultureInfo)));
						Match match = Regex.Match(@string, pattern, RegexOptions.Multiline);
						if (match.Success)
						{
							string s = @string.Remove(match.Index, match.Length);
							byte[] bytes = writeConfiguration.SourceEncoding.GetBytes(s);
							using (Stream stream3 = target.OpenWriteStream(writeConfiguration))
							{
								Stream stream4 = null;
								try
								{
									stream4 = (flag ? stream3 : new ConverterStream(stream3, new RtfToRtfCompressed(), ConverterStreamAccess.Write));
									stream4.Write(bytes, 0, bytes.Length);
									if (num == array.Length)
									{
										Util.StreamHandler.CopyStreamData(stream2, stream4, null, 0, 16384);
									}
									result = true;
								}
								finally
								{
									if (!flag && stream4 != null)
									{
										stream4.Dispose();
									}
								}
							}
						}
					}
				}
				finally
				{
					bool flag;
					if (!flag && stream2 != null)
					{
						stream2.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x000F4018 File Offset: 0x000F2218
		private static void CheckNull(object arg, string argName)
		{
			if (arg == null)
			{
				throw new ArgumentNullException(argName);
			}
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x000F4024 File Offset: 0x000F2224
		private bool IsAnyBodyPropDirty()
		{
			foreach (StorePropertyDefinition propertyDefinition in Body.BodyProps)
			{
				if (this.coreItem.PropertyBag.IsPropertyDirty(propertyDefinition))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x000F4074 File Offset: 0x000F2274
		private void CheckOpenBodyStreamForRead()
		{
			if (this.bodyWriteStream != null && !this.bodyWriteStream.IsDisposed())
			{
				throw new NoSupportException(ServerStrings.ExTooManyObjects("BodyConversionStream", 1, 1));
			}
			if (this.bodyReadStreams.Count > 0)
			{
				this.bodyReadStreams = (from x in this.bodyReadStreams
				where x != null && !x.IsDisposed()
				select x).ToList<Body.IBodyStream>();
			}
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x000F40EC File Offset: 0x000F22EC
		private void CheckOpenBodyStreamForWrite()
		{
			if (this.bodyWriteStream != null && !this.bodyWriteStream.IsDisposed())
			{
				throw new NoSupportException(ServerStrings.ExTooManyObjects("BodyConversionStream", 1, 1));
			}
			foreach (Body.IBodyStream bodyStream in this.bodyReadStreams)
			{
				if (bodyStream != null && !bodyStream.IsDisposed())
				{
					throw new NoSupportException(ServerStrings.ExTooManyObjects("BodyConversionStream", 1, 1));
				}
			}
			this.bodyReadStreams.Clear();
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x000F4188 File Offset: 0x000F2388
		private void BodyChanged(BodyWriteConfiguration configuration)
		{
			if (this.coreItem.Schema is CalendarItemBaseSchema)
			{
				this.coreItem.LocationIdentifierHelperInstance.SetLocationIdentifier(65525U, LastChangeAction.SetBody);
			}
			if (configuration.TargetFormat == BodyFormat.ApplicationRtf && configuration.SourceFormat == BodyFormat.TextPlain && string.IsNullOrEmpty(configuration.InjectPrefix) && string.IsNullOrEmpty(configuration.InjectSuffix))
			{
				this.bodyFormat = 1;
				this.rawBodyFormat = 3;
				this.isEmbeddedPlainText = true;
			}
			else
			{
				this.bodyFormat = (int)configuration.TargetFormat;
				this.rawBodyFormat = (int)configuration.TargetFormat;
				this.isEmbeddedPlainText = false;
			}
			this.noBody = false;
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x000F4228 File Offset: 0x000F2428
		private Stream InternalOpenReadStream(BodyReadConfiguration configuration, bool createEmptyStreamIfNotFound)
		{
			Body.CheckNull(configuration, "configuration");
			this.CheckStreamingExceptions();
			Stream result;
			lock (this.bodyStreamsLock)
			{
				this.CheckOpenBodyStreamForRead();
				BodyReadStream bodyReadStream = BodyReadStream.TryCreateBodyReadStream(this.coreItem, configuration, createEmptyStreamIfNotFound);
				this.bodyReadStreams.Add(bodyReadStream);
				result = bodyReadStream;
			}
			return result;
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x000F4298 File Offset: 0x000F2498
		private BodyFormat CheckBody()
		{
			this.ChooseBestBody();
			return (BodyFormat)this.bodyFormat;
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x000F42A8 File Offset: 0x000F24A8
		private void CalculateRawFormat()
		{
			if (this.rawBodyFormat != -1)
			{
				return;
			}
			BodyFormat bodyFormat = BodyFormat.TextPlain;
			bool flag = false;
			bool flag2 = false;
			BodyFormat bodyFormat2 = BodyFormat.TextPlain;
			bool flag3 = false;
			bool flag4 = false;
			int num = 0;
			object obj = this.coreItem.PropertyBag.TryGetProperty(InternalSchema.TextBody);
			object obj2 = this.coreItem.PropertyBag.TryGetProperty(InternalSchema.HtmlBody);
			object obj3 = this.coreItem.PropertyBag.TryGetProperty(InternalSchema.RtfBody);
			object obj4 = this.coreItem.PropertyBag.TryGetProperty(InternalSchema.RtfInSync);
			PropertyErrorCode propertyErrorCode = (PropertyErrorCode)(-1);
			PropertyError propertyError;
			if ((propertyError = (obj as PropertyError)) != null)
			{
				propertyErrorCode = ((propertyError.PropertyErrorCode == PropertyErrorCode.RequireStreamed) ? PropertyErrorCode.NotEnoughMemory : propertyError.PropertyErrorCode);
			}
			PropertyErrorCode propertyErrorCode2 = (PropertyErrorCode)(-1);
			if ((propertyError = (obj2 as PropertyError)) != null)
			{
				propertyErrorCode2 = ((propertyError.PropertyErrorCode == PropertyErrorCode.RequireStreamed) ? PropertyErrorCode.NotEnoughMemory : propertyError.PropertyErrorCode);
			}
			PropertyErrorCode propertyErrorCode3 = (PropertyErrorCode)(-1);
			if ((propertyError = (obj3 as PropertyError)) != null)
			{
				propertyErrorCode3 = ((propertyError.PropertyErrorCode == PropertyErrorCode.RequireStreamed) ? PropertyErrorCode.NotEnoughMemory : propertyError.PropertyErrorCode);
			}
			bool flag5 = false;
			if (!(obj4 is PropertyError))
			{
				flag5 = (bool)obj4;
			}
			if (!this.IsAnyBodyPropDirty())
			{
				object obj5 = this.coreItem.PropertyBag.TryGetProperty(InternalSchema.NativeBodyInfo);
				if (!(obj5 is PropertyError))
				{
					flag2 = true;
					switch ((int)obj5)
					{
					case 0:
						flag = true;
						bodyFormat = BodyFormat.TextPlain;
						num = 1;
						break;
					case 1:
						if (propertyErrorCode == PropertyErrorCode.NotFound)
						{
							flag2 = false;
						}
						else
						{
							bodyFormat = BodyFormat.TextPlain;
							num = 3;
						}
						break;
					case 2:
						if (propertyErrorCode3 == PropertyErrorCode.NotFound)
						{
							flag2 = false;
						}
						else
						{
							bodyFormat = BodyFormat.ApplicationRtf;
							num = 4;
						}
						break;
					case 3:
						if (propertyErrorCode2 == PropertyErrorCode.NotFound)
						{
							flag2 = false;
						}
						else
						{
							bodyFormat = BodyFormat.TextHtml;
							num = 2;
						}
						break;
					default:
						flag2 = false;
						break;
					}
				}
			}
			if (!flag2)
			{
				if (propertyErrorCode == PropertyErrorCode.NotFound && propertyErrorCode2 == PropertyErrorCode.NotFound && propertyErrorCode3 == PropertyErrorCode.NotFound)
				{
					flag3 = true;
					bodyFormat2 = BodyFormat.TextPlain;
					num = 11;
					flag4 = true;
				}
				else if (propertyErrorCode == PropertyErrorCode.NotEnoughMemory && propertyErrorCode2 == PropertyErrorCode.NotFound && propertyErrorCode3 == PropertyErrorCode.NotFound)
				{
					bodyFormat2 = BodyFormat.TextPlain;
					num = 12;
					flag4 = true;
				}
				else if ((propertyErrorCode == PropertyErrorCode.NotEnoughMemory && propertyErrorCode3 == PropertyErrorCode.NotEnoughMemory && propertyErrorCode2 == PropertyErrorCode.NotFound) || (propertyErrorCode == PropertyErrorCode.NotEnoughMemory && propertyErrorCode3 == PropertyErrorCode.NotEnoughMemory && propertyErrorCode2 == PropertyErrorCode.NotEnoughMemory && flag5))
				{
					bodyFormat2 = BodyFormat.ApplicationRtf;
					num = 13;
					flag4 = true;
				}
				else if ((propertyErrorCode == PropertyErrorCode.NotEnoughMemory || propertyErrorCode == (PropertyErrorCode)(-1)) && propertyErrorCode3 == PropertyErrorCode.NotEnoughMemory && propertyErrorCode2 == PropertyErrorCode.NotEnoughMemory && !flag5)
				{
					bodyFormat2 = BodyFormat.TextHtml;
					num = 14;
					flag4 = true;
				}
				if (!flag4)
				{
					if (propertyErrorCode2 == (PropertyErrorCode)(-1) || propertyErrorCode2 == PropertyErrorCode.NotEnoughMemory)
					{
						if ((propertyErrorCode3 == (PropertyErrorCode)(-1) || propertyErrorCode3 == PropertyErrorCode.NotEnoughMemory) && flag5)
						{
							bodyFormat2 = BodyFormat.ApplicationRtf;
							num = 21;
						}
						else
						{
							bodyFormat2 = BodyFormat.TextHtml;
							num = 22;
						}
						flag4 = true;
					}
					else if (propertyErrorCode3 == (PropertyErrorCode)(-1) || propertyErrorCode3 == PropertyErrorCode.NotEnoughMemory)
					{
						if ((propertyErrorCode == (PropertyErrorCode)(-1) || propertyErrorCode == PropertyErrorCode.NotEnoughMemory) && !flag5)
						{
							bodyFormat2 = BodyFormat.TextPlain;
							num = 23;
						}
						else
						{
							bodyFormat2 = BodyFormat.ApplicationRtf;
							num = 24;
						}
						flag4 = true;
					}
					else
					{
						bodyFormat2 = BodyFormat.TextPlain;
						num = 25;
						flag4 = true;
					}
				}
			}
			this.bodyFormatDecision = num;
			if (!flag4)
			{
				this.noBody = flag;
				this.rawBodyFormat = (int)bodyFormat;
			}
			else
			{
				this.noBody = flag3;
				this.rawBodyFormat = (int)bodyFormat2;
			}
			ExTraceGlobals.CcBodyTracer.TraceDebug<int, bool, int>((long)this.GetHashCode(), "Body.CalculateRawFormat: BodyFormat={0}, missing={1}, decision point={2}", this.rawBodyFormat, this.noBody, this.bodyFormatDecision);
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x000F4648 File Offset: 0x000F2848
		private void ChooseBestBody()
		{
			if (this.bodyFormat != -1)
			{
				return;
			}
			this.CalculateRawFormat();
			this.bodyFormat = this.rawBodyFormat;
			this.isEmbeddedPlainText = false;
			if (this.bodyFormat == 3)
			{
				ConvertUtils.CallCts(ExTraceGlobals.CcBodyTracer, "Body.ChooseBestBody", ServerStrings.ConversionBodyConversionFailed, delegate
				{
					using (Stream stream = this.coreItem.PropertyBag.OpenPropertyStream(InternalSchema.RtfBody, PropertyOpenMode.ReadOnly))
					{
						using (Stream stream2 = new ConverterStream(stream, new RtfCompressedToRtf(), ConverterStreamAccess.Read))
						{
							using (Stream stream3 = TextConvertersInternalHelpers.CreateRtfPreviewStream(stream2, 4096))
							{
								if (TextConvertersInternalHelpers.RtfHasEncapsulatedText(stream3))
								{
									this.bodyFormat = 1;
									this.isEmbeddedPlainText = true;
									this.bodyFormatDecision = 31;
									ExTraceGlobals.CcBodyTracer.TraceDebug<BodyFormat, bool, int>((long)this.GetHashCode(), "Body.ChooseBestBody: BodyFormat={0}, missing={1}, decision point={2}", BodyFormat.TextPlain, false, this.bodyFormatDecision);
								}
							}
						}
					}
				});
			}
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x000F46A8 File Offset: 0x000F28A8
		public string GetPartialHtmlBody(int length, HtmlCallbackBase htmlCallbacks, bool filterHtml, int styleSheetLimit)
		{
			BodyReadConfiguration bodyReadConfiguration = new BodyReadConfiguration(BodyFormat.TextHtml);
			bodyReadConfiguration.SetHtmlOptions(filterHtml ? HtmlStreamingFlags.FilterHtml : HtmlStreamingFlags.None, htmlCallbacks, new int?(styleSheetLimit));
			string result;
			using (TextReader textReader = this.OpenTextReader(bodyReadConfiguration))
			{
				char[] array = new char[length];
				int length2 = Body.ReadChars(textReader, array, length);
				result = new string(array, 0, length2);
			}
			return result;
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x000F4714 File Offset: 0x000F2914
		public byte[] GetPartialRtfCompressedBody(int nBytes)
		{
			BodyReadConfiguration configuration = new BodyReadConfiguration(BodyFormat.ApplicationRtf);
			byte[] result;
			using (Stream stream = this.OpenReadStream(configuration))
			{
				byte[] array = new byte[nBytes];
				int num;
				int num2;
				for (num = 0; num != nBytes; num += num2)
				{
					num2 = stream.Read(array, num, nBytes - num);
					if (num2 == 0)
					{
						break;
					}
				}
				if (num == nBytes)
				{
					result = array;
				}
				else
				{
					byte[] array2 = new byte[num];
					Array.Copy(array, array2, num);
					result = array2;
				}
			}
			return result;
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x000F4794 File Offset: 0x000F2994
		public string GetPartialTextBody(int length)
		{
			BodyReadConfiguration configuration = new BodyReadConfiguration(BodyFormat.TextPlain);
			string result;
			using (TextReader textReader = this.OpenTextReader(configuration))
			{
				char[] array = new char[length];
				int length2 = Body.ReadChars(textReader, array, length);
				result = new string(array, 0, length2);
			}
			return result;
		}

		// Token: 0x04001F96 RID: 8086
		internal const string OutlookHeader = "*~*~*~*~*~*~*~*~*~*";

		// Token: 0x04001F97 RID: 8087
		private const int PreviewSize = 255;

		// Token: 0x04001F98 RID: 8088
		private const int DefaultEmbeddedRtfPreviewBufferSize = 4096;

		// Token: 0x04001F99 RID: 8089
		private const int CopyBufferSize = 16384;

		// Token: 0x04001F9A RID: 8090
		private const int InvalidBodyFormat = -1;

		// Token: 0x04001F9B RID: 8091
		private const PropertyErrorCode NoPropertyError = (PropertyErrorCode)(-1);

		// Token: 0x04001F9C RID: 8092
		internal static int BodyTagLength = 12;

		// Token: 0x04001F9D RID: 8093
		internal static StorePropertyDefinition[] BodyProps = new StorePropertyDefinition[]
		{
			InternalSchema.TextBody,
			InternalSchema.HtmlBody,
			InternalSchema.RtfBody,
			InternalSchema.RtfInSync
		};

		// Token: 0x04001F9E RID: 8094
		internal static HashSet<StorePropertyDefinition> BodyPropSet = new HashSet<StorePropertyDefinition>(Body.BodyProps);

		// Token: 0x04001F9F RID: 8095
		private static readonly PropertyError TextNotFoundPropertyError = new PropertyError(InternalSchema.TextBody, PropertyErrorCode.NotFound);

		// Token: 0x04001FA0 RID: 8096
		private static readonly PropertyError HtmlNotFoundPropertyError = new PropertyError(InternalSchema.HtmlBody, PropertyErrorCode.NotFound);

		// Token: 0x04001FA1 RID: 8097
		private static readonly PropertyError RtfNotFoundPropertyError = new PropertyError(InternalSchema.RtfBody, PropertyErrorCode.NotFound);

		// Token: 0x04001FA2 RID: 8098
		private readonly object bodyStreamsLock = new object();

		// Token: 0x04001FA3 RID: 8099
		private ICoreItem coreItem;

		// Token: 0x04001FA4 RID: 8100
		private int bodyFormat = -1;

		// Token: 0x04001FA5 RID: 8101
		private int rawBodyFormat = -1;

		// Token: 0x04001FA6 RID: 8102
		private bool isEmbeddedPlainText;

		// Token: 0x04001FA7 RID: 8103
		private bool noBody;

		// Token: 0x04001FA8 RID: 8104
		private bool isBodyChanged;

		// Token: 0x04001FA9 RID: 8105
		private bool isPreviewInvalid;

		// Token: 0x04001FAA RID: 8106
		private string cachedPreviewText;

		// Token: 0x04001FAB RID: 8107
		private int bodyFormatDecision = -1;

		// Token: 0x04001FAC RID: 8108
		private List<Body.IBodyStream> bodyReadStreams;

		// Token: 0x04001FAD RID: 8109
		private Body.IBodyStream bodyWriteStream;

		// Token: 0x04001FAE RID: 8110
		private ExchangeDataException bodyStreamingException;

		// Token: 0x020005A7 RID: 1447
		internal interface IBodyStream
		{
			// Token: 0x06003B67 RID: 15207
			bool IsDisposed();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Filtering;
using Microsoft.Filtering.Results;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000020 RID: 32
	internal class MailMessage
	{
		// Token: 0x060000FC RID: 252 RVA: 0x0000648C File Offset: 0x0000468C
		public MailMessage(MailItem mailItem)
		{
			this.mailItem = mailItem;
			this.headers = new MailMessage.MessageHeaders(mailItem);
			this.attachmentInfos = null;
			this.attachmentStreamIdentities = null;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000064B5 File Offset: 0x000046B5
		public virtual MailMessage.MessageHeaders Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000064BD File Offset: 0x000046BD
		public string Subject
		{
			get
			{
				return this.mailItem.Message.Subject;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000064D0 File Offset: 0x000046D0
		public string Sender
		{
			get
			{
				EmailRecipient sender = this.mailItem.Message.Sender;
				if (sender == null)
				{
					return null;
				}
				return sender.SmtpAddress;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000064F9 File Offset: 0x000046F9
		public string MessageId
		{
			get
			{
				return this.GetStringFromFirstHeader(HeaderId.MessageId);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006503 File Offset: 0x00004703
		public string InReplyTo
		{
			get
			{
				return this.GetStringFromFirstHeader(HeaderId.InReplyTo);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000650D File Offset: 0x0000470D
		public string References
		{
			get
			{
				return this.GetStringFromFirstHeader(HeaderId.References);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00006517 File Offset: 0x00004717
		public string ReturnPath
		{
			get
			{
				return this.GetStringFromFirstHeader(HeaderId.ReturnPath);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00006521 File Offset: 0x00004721
		public string Comments
		{
			get
			{
				return this.GetStringFromFirstHeader(HeaderId.Comments);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000652B File Offset: 0x0000472B
		public string Keywords
		{
			get
			{
				return this.GetStringFromFirstHeader(HeaderId.Keywords);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006535 File Offset: 0x00004735
		public string ResentMessageId
		{
			get
			{
				return this.GetStringFromFirstHeader(HeaderId.ResentMessageId);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006540 File Offset: 0x00004740
		public string From
		{
			get
			{
				EmailRecipient from = this.mailItem.Message.From;
				if (from == null)
				{
					return null;
				}
				return from.SmtpAddress;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000656C File Offset: 0x0000476C
		public List<string> To
		{
			get
			{
				EmailRecipientCollection to = this.mailItem.Message.To;
				List<string> list = new List<string>();
				foreach (EmailRecipient emailRecipient in to)
				{
					list.Add(StringUtil.Unwrap(emailRecipient.SmtpAddress));
				}
				return list;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000065DC File Offset: 0x000047DC
		public List<string> Cc
		{
			get
			{
				EmailRecipientCollection cc = this.mailItem.Message.Cc;
				List<string> list = new List<string>();
				foreach (EmailRecipient emailRecipient in cc)
				{
					list.Add(StringUtil.Unwrap(emailRecipient.SmtpAddress));
				}
				return list;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000664C File Offset: 0x0000484C
		public List<string> ToCc
		{
			get
			{
				EmailRecipientCollection emailRecipientCollection = this.mailItem.Message.To;
				List<string> list = new List<string>();
				foreach (EmailRecipient emailRecipient in emailRecipientCollection)
				{
					list.Add(StringUtil.Unwrap(emailRecipient.SmtpAddress));
				}
				emailRecipientCollection = this.mailItem.Message.Cc;
				foreach (EmailRecipient emailRecipient2 in emailRecipientCollection)
				{
					list.Add(StringUtil.Unwrap(emailRecipient2.SmtpAddress));
				}
				return list;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006718 File Offset: 0x00004918
		public List<string> Bcc
		{
			get
			{
				EmailRecipientCollection bcc = this.mailItem.Message.Bcc;
				List<string> list = new List<string>();
				foreach (EmailRecipient emailRecipient in bcc)
				{
					list.Add(StringUtil.Unwrap(emailRecipient.SmtpAddress));
				}
				return list;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00006788 File Offset: 0x00004988
		public List<string> ReplyTo
		{
			get
			{
				return this.GetAddressesFromFirstHeader(HeaderId.ReplyTo);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006794 File Offset: 0x00004994
		public string EnvelopeFrom
		{
			get
			{
				return this.mailItem.FromAddress.ToString();
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000067BC File Offset: 0x000049BC
		public List<string> EnvelopeRecipients
		{
			get
			{
				List<string> list = new List<string>();
				foreach (EnvelopeRecipient envelopeRecipient in this.mailItem.Recipients)
				{
					list.Add(envelopeRecipient.Address.ToString());
				}
				return list;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006830 File Offset: 0x00004A30
		public string Auth
		{
			get
			{
				return this.mailItem.OriginalAuthenticator;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000683D File Offset: 0x00004A3D
		public bool NeedsTracing
		{
			get
			{
				return ((ITransportMailItemWrapperFacade)this.mailItem).TransportMailItem.PipelineTracingEnabled;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006854 File Offset: 0x00004A54
		public IDictionary<string, object> ExtendedProperties
		{
			get
			{
				return this.mailItem.Properties;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00006864 File Offset: 0x00004A64
		public MessageBodies Body
		{
			get
			{
				MessageBodies result;
				if ((result = this.body) == null)
				{
					result = (this.body = new MessageBodies(this.mailItem.Message, 0));
				}
				return result;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006898 File Offset: 0x00004A98
		public object SclValue
		{
			get
			{
				Header header = this.mailItem.Message.MimeDocument.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-SCL");
				if (header != null)
				{
					try
					{
						int num;
						if (int.TryParse(header.Value, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num) && num <= 9 && num >= -1)
						{
							return num;
						}
					}
					catch (ExchangeDataException)
					{
						return null;
					}
				}
				return null;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006910 File Offset: 0x00004B10
		public ulong Size
		{
			get
			{
				return (ulong)this.mailItem.MimeStreamLength;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006920 File Offset: 0x00004B20
		public ulong MaxAttachmentSize
		{
			get
			{
				ulong num = 0UL;
				AttachmentCollection attachments = this.mailItem.Message.Attachments;
				foreach (Attachment attachment in attachments)
				{
					Stream stream;
					long num2;
					if (attachment.TryGetContentReadStream(out stream))
					{
						num2 = stream.Length;
					}
					else
					{
						num2 = attachment.MimePart.GetRawContentReadStream().Length;
					}
					if (num2 < 0L)
					{
						num2 = 0L;
					}
					if (num2 > (long)num)
					{
						num = (ulong)num2;
					}
				}
				return num;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000069D0 File Offset: 0x00004BD0
		public virtual List<string> AttachmentNames
		{
			get
			{
				return (from attachment in this.GetAttachmentInfos()
				where !TransportUtils.IsAttachmentExemptFromFilenameMatching(attachment)
				select attachment.Name).ToList<string>();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00006A40 File Offset: 0x00004C40
		public virtual List<string> AttachmentExtensions
		{
			get
			{
				return (from attachment in this.GetAttachmentInfos()
				where !TransportUtils.IsAttachmentExemptFromFilenameMatching(attachment)
				select attachment.Extension).ToList<string>();
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00006A9C File Offset: 0x00004C9C
		public virtual List<IDictionary<string, string>> AttachmentProperties
		{
			get
			{
				List<IDictionary<string, string>> list = new List<IDictionary<string, string>>();
				foreach (StreamIdentity identity in this.GetUnifiedContentResults().Streams.Where(new Func<StreamIdentity, bool>(MailMessage.IsAttachmentPart)))
				{
					Dictionary<string, string> item = new Dictionary<string, string>(RuleAgentResultUtils.GetCustomProperties(identity), StringComparer.InvariantCultureIgnoreCase);
					list.Add(item);
				}
				return list;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006B20 File Offset: 0x00004D20
		public virtual List<string> AttachmentTypes
		{
			get
			{
				return (from x in this.GetAttachmentInfos()
				select x.FileType).ToList<string>();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00006B50 File Offset: 0x00004D50
		public virtual List<string> ContentCharacterSets
		{
			get
			{
				if (this.contentCharacterSets != null)
				{
					return this.contentCharacterSets;
				}
				if (this.mailItem.MimeDocument != null)
				{
					this.contentCharacterSets = MailMessage.GetCharsets(this.mailItem.MimeDocument.RootPart);
				}
				else
				{
					this.contentCharacterSets = new List<string>();
				}
				return this.contentCharacterSets;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006BA7 File Offset: 0x00004DA7
		public bool UnifiedContentIsValid
		{
			get
			{
				return this.filteringResults != null;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006BB5 File Offset: 0x00004DB5
		public FilteringResults GetUnifiedContentResults()
		{
			if (this.UnifiedContentIsValid)
			{
				return this.filteringResults;
			}
			this.filteringResults = UnifiedContentServiceInvoker.GetUnifiedContentResults(this.mailItem);
			return this.filteringResults;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006BE0 File Offset: 0x00004DE0
		private static string GetCharset(MimePart mimePart)
		{
			ComplexHeader complexHeader = mimePart.Headers.FindFirst(HeaderId.ContentType) as ComplexHeader;
			if (complexHeader == null)
			{
				return null;
			}
			MimeParameter mimeParameter = complexHeader["charset"];
			if (mimeParameter == null)
			{
				return null;
			}
			string text;
			if (!mimeParameter.TryGetValue(out text) || string.IsNullOrEmpty(text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006C2C File Offset: 0x00004E2C
		private static bool IsAttachmentPart(StreamIdentity streamIdentity)
		{
			bool flag = -1 == streamIdentity.ParentId;
			bool flag2 = streamIdentity.Properties.ContainsKey("UnifiedContent::PropertyKeys::ExtendedContent");
			bool flag3 = streamIdentity.Properties.ContainsKey("Parsing::ParsingKeys::MessageBody");
			bool flag4 = 0 == streamIdentity.ParentId;
			return !flag && !flag2 && (!flag3 || !flag4);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006C88 File Offset: 0x00004E88
		public IEnumerable<StreamIdentity> GetAttachmentStreamIdentities()
		{
			if (this.attachmentStreamIdentities != null)
			{
				return (IEnumerable<StreamIdentity>)this.attachmentStreamIdentities;
			}
			IEnumerable<StreamIdentity> result = from streamIdentity in this.GetUnifiedContentResults().Streams
			where MailMessage.IsAttachmentPart(streamIdentity)
			select streamIdentity;
			this.attachmentStreamIdentities = result;
			return result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006CEC File Offset: 0x00004EEC
		public IEnumerable<StreamIdentity> GetSupportedAttachmentStreamIdentities()
		{
			return from streamIdentity in this.GetAttachmentStreamIdentities()
			where !RuleAgentResultUtils.IsUnsupported(streamIdentity)
			select streamIdentity;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006D23 File Offset: 0x00004F23
		public void ResetUnifiedContent()
		{
			this.SetUnifiedContent(null);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006D2C File Offset: 0x00004F2C
		internal void SetUnifiedContent(FilteringResults filteringResults)
		{
			this.filteringResults = filteringResults;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006D38 File Offset: 0x00004F38
		internal static List<string> GetCharsets(MimePart mimePart)
		{
			List<string> list = new List<string>();
			if (mimePart == null)
			{
				return list;
			}
			string charset = MailMessage.GetCharset(mimePart);
			if (!string.IsNullOrEmpty(charset))
			{
				list.Add(charset);
			}
			for (MimePart mimePart2 = mimePart.FirstChild as MimePart; mimePart2 != null; mimePart2 = (mimePart2.NextSibling as MimePart))
			{
				list.AddRange(MailMessage.GetCharsets(mimePart2));
			}
			return list;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006DA4 File Offset: 0x00004FA4
		internal static string GetAttachmentName(StreamIdentity attachmentIdentity, bool shouldAppendMsgToMailAttachmentNames)
		{
			if (shouldAppendMsgToMailAttachmentNames && !string.IsNullOrWhiteSpace(attachmentIdentity.Name) && !attachmentIdentity.Name.EndsWith(".msg", StringComparison.OrdinalIgnoreCase))
			{
				if (attachmentIdentity.Types.Any((StreamType binaryType) => binaryType == 1082130439 || binaryType == 8388610))
				{
					return attachmentIdentity.Name + ".msg";
				}
			}
			return attachmentIdentity.Name;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006E18 File Offset: 0x00005018
		private List<AttachmentInfo> GetAttachmentInfos()
		{
			if (this.attachmentInfos != null)
			{
				return this.attachmentInfos;
			}
			Dictionary<int, KeyValuePair<int, AttachmentInfo>> dictionary = new Dictionary<int, KeyValuePair<int, AttachmentInfo>>();
			this.attachmentInfos = new List<AttachmentInfo>();
			bool shouldAppendMsgToMailAttachmentNames = TransportUtils.IsMsgAttachmentNameEnabled(this.mailItem);
			try
			{
				foreach (StreamIdentity streamIdentity in this.GetAttachmentStreamIdentities())
				{
					string attachmentName = MailMessage.GetAttachmentName(streamIdentity, shouldAppendMsgToMailAttachmentNames);
					dictionary.Add(streamIdentity.Id, new KeyValuePair<int, AttachmentInfo>(streamIdentity.ParentId, new AttachmentInfo(attachmentName, TransportUtils.GetFileExtension(attachmentName), (from t in streamIdentity.Types
					select t).ToArray<uint>())));
				}
			}
			catch (NotSupportedException ex)
			{
				string message = TransportRulesStrings.AttachmentReadError(string.Format("NotSupportedException encountered while getting attachment content. Most likely reason - FIPS not configured properly. Check if TextExtractionHandler is enabled in FIP-FS\\Data\\configuration.xml. {0}", ex.Message));
				ExTraceGlobals.TransportRulesEngineTracer.TraceDebug(0L, message);
				throw new TransportRulePermanentException(message);
			}
			foreach (KeyValuePair<int, KeyValuePair<int, AttachmentInfo>> keyValuePair in dictionary)
			{
				AttachmentInfo value = keyValuePair.Value.Value;
				KeyValuePair<int, AttachmentInfo> keyValuePair2;
				if (keyValuePair.Value.Key != 0 && dictionary.TryGetValue(keyValuePair.Value.Key, out keyValuePair2))
				{
					value.Parent = keyValuePair2.Value;
				}
				this.attachmentInfos.Add(value);
			}
			return this.attachmentInfos;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006FC4 File Offset: 0x000051C4
		private string GetStringFromFirstHeader(HeaderId headerId)
		{
			Header header = this.mailItem.Message.MimeDocument.RootPart.Headers.FindFirst(headerId);
			if (header == null)
			{
				return null;
			}
			return header.Value;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007000 File Offset: 0x00005200
		private List<string> GetAddressesFromFirstHeader(HeaderId headerId)
		{
			AddressHeader addressHeader = (AddressHeader)this.mailItem.Message.MimeDocument.RootPart.Headers.FindFirst(headerId);
			if (addressHeader == null)
			{
				return null;
			}
			List<string> result;
			using (MimeNode.Enumerator<AddressItem> enumerator = addressHeader.GetEnumerator())
			{
				enumerator.Reset();
				List<string> list = new List<string>();
				while (enumerator.MoveNext())
				{
					AddressItem addressItem = enumerator.Current;
					MimeRecipient mimeRecipient = addressItem as MimeRecipient;
					if (mimeRecipient != null)
					{
						list.Add(mimeRecipient.Email.ToString());
					}
					else
					{
						MimeGroup mimeGroup = enumerator.Current as MimeGroup;
						if (mimeGroup != null)
						{
							foreach (MimeRecipient mimeRecipient2 in mimeGroup)
							{
								list.Add(mimeRecipient2.Email.ToString());
							}
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x040000F2 RID: 242
		private const string MsgAttachmentExtension = ".msg";

		// Token: 0x040000F3 RID: 243
		private MailItem mailItem;

		// Token: 0x040000F4 RID: 244
		private MailMessage.MessageHeaders headers;

		// Token: 0x040000F5 RID: 245
		private MessageBodies body;

		// Token: 0x040000F6 RID: 246
		private List<AttachmentInfo> attachmentInfos;

		// Token: 0x040000F7 RID: 247
		private object attachmentStreamIdentities;

		// Token: 0x040000F8 RID: 248
		private FilteringResults filteringResults;

		// Token: 0x040000F9 RID: 249
		private List<string> contentCharacterSets;

		// Token: 0x02000021 RID: 33
		public class MessageHeaders
		{
			// Token: 0x06000131 RID: 305 RVA: 0x000070FC File Offset: 0x000052FC
			public MessageHeaders(MailItem mailItem)
			{
				if (mailItem != null)
				{
					this.mailItem = mailItem;
				}
			}

			// Token: 0x17000050 RID: 80
			public virtual List<string> this[string index]
			{
				get
				{
					List<string> list = new List<string>();
					if (index == null || this.mailItem == null)
					{
						return list;
					}
					string key;
					switch (key = index.ToLower())
					{
					case "from":
						MailMessage.MessageHeaders.AddToStringList(this.mailItem.Message.From, list);
						return list;
					case "to":
						MailMessage.MessageHeaders.AddToStringList(this.mailItem.Message.To, list);
						return list;
					case "cc":
						MailMessage.MessageHeaders.AddToStringList(this.mailItem.Message.Cc, list);
						return list;
					case "bcc":
						MailMessage.MessageHeaders.AddToStringList(this.mailItem.Message.Bcc, list);
						return list;
					case "reply-to":
						MailMessage.MessageHeaders.AddToStringList(this.mailItem.Message.ReplyTo, list);
						return list;
					case "subject":
						list.Add(this.mailItem.Message.Subject);
						return list;
					case "message-id":
						list.Add(this.mailItem.Message.MessageId);
						return list;
					}
					Header[] array = this.mailItem.Message.MimeDocument.RootPart.Headers.FindAll(index);
					foreach (Header header in array)
					{
						AddressHeader addressHeader = header as AddressHeader;
						if (addressHeader == null)
						{
							if (header.HeaderId == HeaderId.ContentType && "multipart/report".Equals(header.Value, StringComparison.OrdinalIgnoreCase) && header is ComplexHeader)
							{
								string text = Convert.ToString(header.Value);
								MimeParameter mimeParameter = (header as ComplexHeader)["report-type"];
								if (mimeParameter != null && mimeParameter.Value != null)
								{
									text = string.Format("{0};\r\n\t{1}={2}", text, "report-type", mimeParameter.Value);
								}
								list.Add(text);
							}
							else
							{
								list.Add(header.Value);
							}
						}
						else
						{
							using (MimeNode.Enumerator<AddressItem> enumerator = addressHeader.GetEnumerator())
							{
								enumerator.Reset();
								while (enumerator.MoveNext())
								{
									AddressItem addressItem = enumerator.Current;
									MimeRecipient mimeRecipient = addressItem as MimeRecipient;
									if (mimeRecipient != null)
									{
										list.Add(mimeRecipient.DisplayName);
										list.Add(mimeRecipient.Email.ToString());
									}
									else
									{
										MimeGroup mimeGroup = enumerator.Current as MimeGroup;
										if (mimeGroup != null)
										{
											foreach (MimeRecipient mimeRecipient2 in mimeGroup)
											{
												list.Add(mimeRecipient2.DisplayName);
												list.Add(mimeRecipient2.Email.ToString());
											}
										}
									}
								}
							}
						}
					}
					return list;
				}
			}

			// Token: 0x06000133 RID: 307 RVA: 0x0000745C File Offset: 0x0000565C
			private static void AddToStringList(EmailRecipientCollection recipients, List<string> strings)
			{
				foreach (EmailRecipient emailRecipient in recipients)
				{
					strings.Add(emailRecipient.SmtpAddress);
					strings.Add(emailRecipient.DisplayName);
				}
			}

			// Token: 0x06000134 RID: 308 RVA: 0x000074BC File Offset: 0x000056BC
			private static void AddToStringList(EmailRecipient recipient, List<string> strings)
			{
				if (recipient != null)
				{
					strings.Add(recipient.SmtpAddress);
					strings.Add(recipient.DisplayName);
				}
			}

			// Token: 0x04000103 RID: 259
			private MailItem mailItem;
		}
	}
}

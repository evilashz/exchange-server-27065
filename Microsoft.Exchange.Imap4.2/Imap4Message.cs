using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000014 RID: 20
	internal class Imap4Message : ProtocolMessage, IReadOnlyPropertyBag
	{
		// Token: 0x060000DB RID: 219 RVA: 0x000073E3 File Offset: 0x000055E3
		internal Imap4Message(Imap4Mailbox mailbox, int index, object[] messageData) : this(mailbox, index, (int)messageData[0], (int)messageData[1], Imap4Message.Parse(messageData), (int)messageData[10])
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000740C File Offset: 0x0000560C
		internal Imap4Message(Imap4Mailbox mailbox, int index, int imapId, int docId, Imap4Flags flags, int size) : base(index, imapId, docId, size)
		{
			this.mailbox = mailbox;
			this.flagsHaveBeenChanged = false;
			this.flags = flags;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00007430 File Offset: 0x00005630
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00007444 File Offset: 0x00005644
		public override bool IsNotRfc822Renderable
		{
			get
			{
				return (this.flags & Imap4Flags.MimeFailed) != Imap4Flags.None;
			}
			set
			{
				if (value)
				{
					this.flags |= Imap4Flags.MimeFailed;
					return;
				}
				this.flags &= ~Imap4Flags.MimeFailed;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000746E File Offset: 0x0000566E
		public override ResponseFactory Factory
		{
			get
			{
				return this.mailbox.Factory;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000747B File Offset: 0x0000567B
		public override StoreObjectId Uid
		{
			get
			{
				return this.mailbox.DataAccessView.GetStoreObjectId(base.Id);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00007493 File Offset: 0x00005693
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000074B3 File Offset: 0x000056B3
		internal Imap4Flags Flags
		{
			get
			{
				return this.flags | ((base.Id > this.mailbox.LastSeenArticleId) ? Imap4Flags.Recent : Imap4Flags.None);
			}
			set
			{
				if (this.flags != value)
				{
					this.flagsHaveBeenChanged = true;
					this.flags = value;
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000074CC File Offset: 0x000056CC
		internal bool IsMarkedForDeletion
		{
			get
			{
				return (this.flags & Imap4Flags.Deleted) != Imap4Flags.None;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000074DC File Offset: 0x000056DC
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000074E4 File Offset: 0x000056E4
		internal bool FlagsHaveBeenChanged
		{
			get
			{
				return this.flagsHaveBeenChanged;
			}
			set
			{
				this.flagsHaveBeenChanged = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000074F0 File Offset: 0x000056F0
		private static Dictionary<PropertyDefinition, int> PropertyLookupTable
		{
			get
			{
				if (Imap4Message.propertyLookupTable == null)
				{
					lock (Imap4Message.MessageProperties)
					{
						if (Imap4Message.propertyLookupTable == null)
						{
							Imap4Message.propertyLookupTable = new Dictionary<PropertyDefinition, int>(Imap4Message.MessageProperties.Length);
							for (int i = 0; i < Imap4Message.MessageProperties.Length; i++)
							{
								Imap4Message.propertyLookupTable.Add(Imap4Message.MessageProperties[i], i);
							}
						}
					}
				}
				return Imap4Message.propertyLookupTable;
			}
		}

		// Token: 0x1700005C RID: 92
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				int num;
				if (Imap4Message.PropertyLookupTable.TryGetValue(propertyDefinition, out num))
				{
					switch (num)
					{
					case 0:
						return base.Id;
					case 2:
						return this.IsMarkedForDeletion;
					case 3:
						return (this.Flags & Imap4Flags.Answered) != Imap4Flags.None;
					case 4:
						return (this.Flags & Imap4Flags.Flagged) != Imap4Flags.None;
					case 5:
						return (this.Flags & Imap4Flags.MdnSent) != Imap4Flags.None;
					case 6:
						return this.IsNotRfc822Renderable;
					case 7:
						return (this.Flags & Imap4Flags.Draft) != Imap4Flags.None;
					case 8:
						return (this.Flags & Imap4Flags.Seen) != Imap4Flags.None;
					}
				}
				throw new NotSupportedException(propertyDefinition.Name);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000765C File Offset: 0x0000585C
		public static string GetEnvelope(MimePartHeaders headers)
		{
			Encoding encoding = headers.Charset.GetEncoding();
			string stringValue = null;
			DateHeader dateHeader = headers[HeaderId.Date] as DateHeader;
			if (dateHeader != null)
			{
				stringValue = Rfc822Date.FormatLong((ExDateTime)dateHeader.DateTime);
			}
			return string.Format("({0} {1} {2} {3} {4} {5} {6} {7} {8} {9})", new object[]
			{
				Imap4Message.GetStringOrNil(Imap4Message.GetEncodedString(stringValue, encoding)),
				Imap4Message.GetStringOrNil(headers[HeaderId.Subject], encoding),
				Imap4Message.FormatAddresses(headers[HeaderId.From], encoding),
				Imap4Message.FormatAddresses(headers[HeaderId.Sender], encoding),
				Imap4Message.FormatAddresses(headers[HeaderId.ReplyTo], encoding),
				Imap4Message.FormatAddresses(headers[HeaderId.To], encoding),
				Imap4Message.FormatAddresses(headers[HeaderId.Cc], encoding),
				Imap4Message.FormatAddresses(headers[HeaderId.Bcc], encoding),
				Imap4Message.GetStringOrNil(headers[HeaderId.InReplyTo], encoding),
				Imap4Message.GetStringOrNil(headers[HeaderId.MessageId], encoding)
			});
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007750 File Offset: 0x00005950
		public static string GetListOfParameters(ComplexHeader complexHeader, Encoding encoding)
		{
			if (complexHeader == null)
			{
				return "NIL";
			}
			StringBuilder stringBuilder = new StringBuilder(128);
			foreach (MimeParameter mimeParameter in complexHeader)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" ");
				}
				string text = null;
				DecodingResults decodingResults;
				if (!mimeParameter.TryGetValue(Imap4Message.decodingOptions, out decodingResults, out text) || MimeInternalHelpers.IsEncodingRequired(text, false))
				{
					text = Imap4Message.GetEncodedString(mimeParameter.Value, encoding);
				}
				stringBuilder.Append(Imap4Message.GetStringOrNil(Imap4Message.GetEncodedString(mimeParameter.Name, encoding)));
				stringBuilder.Append(" ");
				stringBuilder.Append(Imap4Message.GetStringOrNil(text));
			}
			return Imap4Message.InParenthesisOrNil(stringBuilder.ToString());
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00007828 File Offset: 0x00005A28
		public static string GetBodyStructure(MimePartInfo mimeStructure, bool fullStructure)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			Encoding encoding = mimeStructure.Charset.GetEncoding();
			MimePartHeaders mimePartHeaders = mimeStructure.Headers ?? new MimePartHeaders(mimeStructure.Charset);
			string text = null;
			string stringValue = null;
			ContentTypeHeader contentTypeHeader = mimePartHeaders[HeaderId.ContentType] as ContentTypeHeader;
			if (contentTypeHeader != null)
			{
				text = Imap4Message.GetEncodedString(contentTypeHeader.MediaType, encoding);
				stringValue = Imap4Message.GetEncodedString(contentTypeHeader.SubType, encoding);
			}
			stringBuilder.Append("(");
			if (mimeStructure.Children != null && (mimeStructure.Children.Count > 1 || (mimeStructure.Children.Count == 1 && mimeStructure.IsMultipart)))
			{
				foreach (MimePartInfo mimeStructure2 in mimeStructure.Children)
				{
					stringBuilder.Append(Imap4Message.GetBodyStructure(mimeStructure2, fullStructure));
				}
				stringBuilder.AppendFormat(" {0}", Imap4Message.GetStringOrNil(stringValue));
				if (fullStructure)
				{
					stringBuilder.AppendFormat(" {0} {1} {2}", Imap4Message.GetListOfParameters(contentTypeHeader, encoding), Imap4Message.GetStringOrNil(mimePartHeaders[HeaderId.ContentDescription], encoding), Imap4Message.GetStringOrNil(mimePartHeaders[HeaderId.ContentLanguage], encoding));
				}
			}
			else
			{
				stringBuilder.AppendFormat("{0} {1} {2} {3} {4} {5} {6}", new object[]
				{
					Imap4Message.GetStringOrNil(text),
					Imap4Message.GetStringOrNil(stringValue),
					Imap4Message.GetListOfParameters(contentTypeHeader, encoding),
					Imap4Message.GetStringOrNil(mimePartHeaders[HeaderId.ContentId], encoding),
					Imap4Message.GetStringOrNil(mimePartHeaders[HeaderId.ContentDescription], encoding),
					Imap4Message.GetStringOrDefaultValue(mimePartHeaders[HeaderId.ContentTransferEncoding], encoding, "\"7BIT\""),
					mimeStructure.BodySize
				});
				if (mimeStructure.AttachedItemStructure != null)
				{
					stringBuilder.AppendFormat(" {0} {1}", Imap4Message.GetEnvelope(mimeStructure.AttachedItemStructure.Headers), Imap4Message.GetBodyStructure(mimeStructure.AttachedItemStructure, fullStructure));
				}
				if (string.Compare(text, "text", StringComparison.OrdinalIgnoreCase) == 0 || mimeStructure.AttachedItemStructure != null)
				{
					stringBuilder.AppendFormat(" {0}", mimeStructure.BodyLineCount);
				}
				if (fullStructure)
				{
					ContentDispositionHeader contentDispositionHeader = mimePartHeaders[HeaderId.ContentDisposition] as ContentDispositionHeader;
					if (contentDispositionHeader != null)
					{
						stringBuilder.AppendFormat(" {0} ({1} {2}) {3} {4}", new object[]
						{
							Imap4Message.GetStringOrNil(mimePartHeaders[HeaderId.ContentMD5], encoding),
							Imap4Message.GetStringOrNil(contentDispositionHeader, encoding),
							Imap4Message.GetListOfParameters(contentDispositionHeader, encoding),
							Imap4Message.GetStringOrNil(mimePartHeaders[HeaderId.ContentLanguage], encoding),
							Imap4Message.GetStringOrNil(mimePartHeaders[HeaderId.ContentLocation], encoding)
						});
					}
					else
					{
						stringBuilder.AppendFormat(" {0} {1} {2} {3}", new object[]
						{
							Imap4Message.GetStringOrNil(mimePartHeaders[HeaderId.ContentMD5], encoding),
							Imap4Message.GetStringOrNil(contentDispositionHeader, encoding),
							Imap4Message.GetStringOrNil(mimePartHeaders[HeaderId.ContentLanguage], encoding),
							Imap4Message.GetStringOrNil(mimePartHeaders[HeaderId.ContentLocation], encoding)
						});
					}
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00007B40 File Offset: 0x00005D40
		public static Imap4Flags Parse(object[] messageProperties)
		{
			object delMarked = messageProperties[2];
			object answered = messageProperties[3];
			object tagged = messageProperties[4];
			object notificationSent = messageProperties[5];
			object conversionFailed = messageProperties[6];
			object isDraft = messageProperties[7];
			object isRead = messageProperties[8];
			return Imap4FlagsHelper.Parse(delMarked, answered, tagged, notificationSent, conversionFailed, isDraft, isRead);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00007B7B File Offset: 0x00005D7B
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			throw new NotImplementedException("GetProperties");
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007B87 File Offset: 0x00005D87
		public override string ToString()
		{
			return string.Format("{0}, folder {1}", base.ToString(), this.mailbox);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007BA0 File Offset: 0x00005DA0
		internal bool Reload(Imap4Flags flags, Dictionary<int, Imap4Flags> orginalFlags)
		{
			ProtocolBaseServices.SessionTracer.TraceDebug(this.Factory.Session.SessionId, "Message.Reload is called");
			if (!base.IsDeleted)
			{
				Imap4Flags imap4Flags = this.flags;
				Imap4Flags value = this.Flags;
				this.flags = flags;
				if (imap4Flags != this.flags)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug<Imap4Flags, Imap4Flags>(this.Factory.Session.SessionId, "Old flags {0} new flags {1}", imap4Flags, this.flags);
					this.flagsHaveBeenChanged = true;
					if (!orginalFlags.ContainsKey(base.Id))
					{
						orginalFlags.Add(base.Id, value);
					}
				}
				return imap4Flags != this.flags;
			}
			return false;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007C48 File Offset: 0x00005E48
		internal void SaveFlags(StoreObjectId uid, Folder folder, Imap4Flags newFlags, List<StoreId> markAsRead, List<StoreId> markAsUnread)
		{
			bool flag = false;
			Imap4Flags imap4Flags = this.Flags;
			this.Flags = newFlags;
			if (((imap4Flags ^ newFlags) & Imap4Flags.ItemStatus) != Imap4Flags.None)
			{
				MessageStatusFlags messageStatusFlags = MessageStatusFlags.None;
				if ((this.flags & Imap4Flags.Answered) != Imap4Flags.None)
				{
					messageStatusFlags |= MessageStatusFlags.Answered;
					flag = ((imap4Flags & Imap4Flags.Answered) == Imap4Flags.None);
				}
				if ((this.flags & Imap4Flags.Deleted) != Imap4Flags.None)
				{
					messageStatusFlags |= MessageStatusFlags.DeleteMarked;
				}
				if ((this.flags & Imap4Flags.Flagged) != Imap4Flags.None)
				{
					messageStatusFlags |= MessageStatusFlags.Tagged;
				}
				if ((this.flags & Imap4Flags.MdnSent) != Imap4Flags.None)
				{
					messageStatusFlags |= MessageStatusFlags.MessageDeliveryNotificationSent;
				}
				MessageStatusFlags statusMask = MessageStatusFlags.Tagged | MessageStatusFlags.DeleteMarked | MessageStatusFlags.Answered | MessageStatusFlags.MessageDeliveryNotificationSent;
				folder.SetItemStatus(uid, messageStatusFlags, statusMask);
			}
			if (((imap4Flags ^ newFlags) & Imap4Flags.Draft) != Imap4Flags.None)
			{
				MessageFlags messageFlags = MessageFlags.None;
				if ((this.flags & Imap4Flags.Draft) != Imap4Flags.None)
				{
					messageFlags |= MessageFlags.IsDraft;
				}
				MessageFlags flagsMask = MessageFlags.IsDraft;
				folder.SetItemFlags(uid, messageFlags, flagsMask);
			}
			if (((imap4Flags ^ newFlags) & Imap4Flags.Seen) != Imap4Flags.None)
			{
				if ((this.flags & Imap4Flags.Seen) != Imap4Flags.None)
				{
					markAsRead.Add(uid);
				}
				else
				{
					markAsUnread.Add(uid);
				}
			}
			bool flag2 = ((imap4Flags ^ newFlags) & Imap4Flags.Flagged) != Imap4Flags.None;
			if (!flag)
			{
				if (!flag2)
				{
					return;
				}
			}
			try
			{
				using (MessageItem messageItem = MessageItem.Bind(folder.Session, uid, new PropertyDefinition[]
				{
					ItemSchema.IconIndex
				}))
				{
					if (messageItem.IconIndex != IconIndex.MailReplied || flag2)
					{
						messageItem.OpenAsReadWrite();
						if (messageItem.IconIndex != IconIndex.MailReplied)
						{
							messageItem.IconIndex = IconIndex.MailReplied;
						}
						if (flag2)
						{
							if ((newFlags & Imap4Flags.Flagged) != Imap4Flags.None)
							{
								messageItem.SetFlag(ClientStrings.Followup, null, null);
							}
							else
							{
								messageItem.ClearFlag();
							}
						}
						messageItem.Save(SaveMode.ResolveConflicts);
					}
				}
			}
			catch (WrongObjectTypeException ex)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(this.Factory.Session.SessionId, "Exception caught when try to set IconIndex: {1}", ex.Message);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00007E18 File Offset: 0x00006018
		private static string GetEncodedString(string stringValue, Encoding encoding)
		{
			if (string.IsNullOrEmpty(stringValue))
			{
				return null;
			}
			if (encoding != null)
			{
				stringValue = ProtocolMessage.Rfc2047Encode(stringValue, encoding);
			}
			return stringValue;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007E34 File Offset: 0x00006034
		private static string GetStringOrDefaultValue(string stringValue, string defaultValue)
		{
			if (string.IsNullOrEmpty(stringValue))
			{
				return defaultValue;
			}
			stringValue = stringValue.Replace("\\", "\\\\");
			stringValue = stringValue.Replace("\"", "\\\"");
			stringValue = "\"" + stringValue + "\"";
			return stringValue;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00007E82 File Offset: 0x00006082
		private static string GetStringOrNil(string stringValue)
		{
			return Imap4Message.GetStringOrDefaultValue(stringValue, "NIL");
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007E90 File Offset: 0x00006090
		private static string GetStringOrDefaultValue(Header header, Encoding encoding, string defaultValue)
		{
			if (header == null)
			{
				return defaultValue;
			}
			string text = null;
			TextHeader textHeader = header as TextHeader;
			AsciiTextHeader asciiTextHeader = header as AsciiTextHeader;
			if (textHeader != null)
			{
				DecodingResults decodingResults;
				if (!textHeader.TryGetValue(Imap4Message.decodingOptions, out decodingResults, out text) || MimeInternalHelpers.IsEncodingRequired(text, false))
				{
					text = Imap4Message.GetEncodedString(textHeader.Value, encoding);
				}
			}
			else if (asciiTextHeader != null)
			{
				text = header.Value;
			}
			else
			{
				text = Imap4Message.GetEncodedString(header.Value, encoding);
			}
			return Imap4Message.GetStringOrDefaultValue(text, defaultValue);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007EFE File Offset: 0x000060FE
		private static string GetStringOrNil(Header header, Encoding encoding)
		{
			return Imap4Message.GetStringOrDefaultValue(header, encoding, "NIL");
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007F0C File Offset: 0x0000610C
		private static string InParenthesisOrNil(string stringValue)
		{
			if (string.IsNullOrEmpty(stringValue) || stringValue == "NIL")
			{
				return "NIL";
			}
			return "(" + stringValue + ")";
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007F3C File Offset: 0x0000613C
		private static string FormatAddress(MimeRecipient recipient, Encoding encoding)
		{
			string text = null;
			DecodingResults decodingResults;
			if (!recipient.TryGetDisplayName(Imap4Message.decodingOptions, out decodingResults, out text) || MimeInternalHelpers.IsEncodingRequired(text, false))
			{
				text = Imap4Message.GetEncodedString(recipient.DisplayName, encoding);
			}
			string email = recipient.Email;
			int num = email.IndexOf('@');
			if (num > 0)
			{
				return string.Format("({0} NIL {1} {2})", Imap4Message.GetStringOrNil(text), Imap4Message.GetStringOrNil(email.Substring(0, num)), Imap4Message.GetStringOrNil(email.Substring(num + 1)));
			}
			return string.Format("({0} NIL {1} \".MISSING-HOST-NAME.\")", Imap4Message.GetStringOrNil(text), Imap4Message.GetStringOrNil(email));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007FC8 File Offset: 0x000061C8
		private static string FormatAddresses(Header header, Encoding encoding)
		{
			AddressHeader addressHeader = header as AddressHeader;
			if (addressHeader == null)
			{
				return "NIL";
			}
			StringBuilder stringBuilder = new StringBuilder(256);
			foreach (AddressItem addressItem in addressHeader)
			{
				MimeRecipient mimeRecipient = addressItem as MimeRecipient;
				if (mimeRecipient != null)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(Imap4Message.FormatAddress(mimeRecipient, encoding));
				}
			}
			return Imap4Message.InParenthesisOrNil(stringBuilder.ToString());
		}

		// Token: 0x0400009F RID: 159
		private const string Nil = "NIL";

		// Token: 0x040000A0 RID: 160
		private const string SevenBit = "\"7BIT\"";

		// Token: 0x040000A1 RID: 161
		public static readonly PropertyDefinition[] MessageProperties = new PropertyDefinition[]
		{
			ItemSchema.ImapId,
			ItemSchema.DocumentId,
			MessageItemSchema.MessageDelMarked,
			MessageItemSchema.MessageAnswered,
			MessageItemSchema.MessageTagged,
			MessageItemSchema.MessageDeliveryNotificationSent,
			MessageItemSchema.MimeConversionFailed,
			MessageItemSchema.IsDraft,
			MessageItemSchema.IsRead,
			MessageItemSchema.MessageHidden,
			ItemSchema.Size
		};

		// Token: 0x040000A2 RID: 162
		public static readonly SortBy[] SortBys = new SortBy[]
		{
			new SortBy(ItemSchema.ImapId, SortOrder.Ascending),
			new SortBy(ItemSchema.MessageStatus, SortOrder.Ascending),
			new SortBy(MessageItemSchema.Flags, SortOrder.Ascending),
			new SortBy(ItemSchema.Size, SortOrder.Ascending)
		};

		// Token: 0x040000A3 RID: 163
		private static readonly PropertyDefinition[] PropertiesToChange = new PropertyDefinition[]
		{
			MessageItemSchema.MessageDelMarked,
			MessageItemSchema.MessageAnswered,
			MessageItemSchema.MessageTagged,
			MessageItemSchema.MessageDeliveryNotificationSent,
			MessageItemSchema.MimeConversionFailed,
			MessageItemSchema.IsDraft,
			MessageItemSchema.IsRead
		};

		// Token: 0x040000A4 RID: 164
		private static Dictionary<PropertyDefinition, int> propertyLookupTable;

		// Token: 0x040000A5 RID: 165
		private static DecodingOptions decodingOptions = new DecodingOptions(DecodingFlags.FallbackToRaw);

		// Token: 0x040000A6 RID: 166
		private Imap4Mailbox mailbox;

		// Token: 0x040000A7 RID: 167
		private Imap4Flags flags;

		// Token: 0x040000A8 RID: 168
		private bool flagsHaveBeenChanged;

		// Token: 0x02000015 RID: 21
		public struct PropertyIndex
		{
			// Token: 0x040000A9 RID: 169
			public const int ImapId = 0;

			// Token: 0x040000AA RID: 170
			public const int DocumentId = 1;

			// Token: 0x040000AB RID: 171
			public const int MessageDelMarked = 2;

			// Token: 0x040000AC RID: 172
			public const int MessageAnswered = 3;

			// Token: 0x040000AD RID: 173
			public const int MessageTagged = 4;

			// Token: 0x040000AE RID: 174
			public const int MessageDeliveryNotificationSent = 5;

			// Token: 0x040000AF RID: 175
			public const int MimeConversionFailed = 6;

			// Token: 0x040000B0 RID: 176
			public const int IsDraft = 7;

			// Token: 0x040000B1 RID: 177
			public const int IsRead = 8;

			// Token: 0x040000B2 RID: 178
			public const int MessageHidden = 9;

			// Token: 0x040000B3 RID: 179
			public const int Size = 10;
		}
	}
}

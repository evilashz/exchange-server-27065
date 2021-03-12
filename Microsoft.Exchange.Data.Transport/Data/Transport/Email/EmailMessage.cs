using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000CB RID: 203
	public class EmailMessage : IDisposable
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x0000A959 File Offset: 0x00008B59
		internal EmailMessage(MessageImplementation message)
		{
			this.message = message;
			this.Synchronize();
			this.accessToken = new EmailMessage.EmailMessageThreadAccessToken(this);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000A981 File Offset: 0x00008B81
		public static EmailMessage Create()
		{
			return EmailMessage.Create(BodyFormat.Text, false, Charset.DefaultMimeCharset.Name);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000A994 File Offset: 0x00008B94
		public static EmailMessage Create(BodyFormat bodyFormat)
		{
			return EmailMessage.Create(bodyFormat, false, Charset.DefaultMimeCharset.Name);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000A9A7 File Offset: 0x00008BA7
		public static EmailMessage Create(BodyFormat bodyFormat, bool createAlternative)
		{
			return EmailMessage.Create(bodyFormat, createAlternative, Charset.DefaultMimeCharset.Name);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000A9BC File Offset: 0x00008BBC
		public static EmailMessage Create(BodyFormat bodyFormat, bool createAlternative, string charsetName)
		{
			if (bodyFormat != BodyFormat.Text && bodyFormat != BodyFormat.Html)
			{
				throw new ArgumentException(EmailMessageStrings.CannotCreateSpecifiedBodyFormat(bodyFormat.ToString()));
			}
			if (bodyFormat == BodyFormat.Text && createAlternative)
			{
				throw new ArgumentException(EmailMessageStrings.CannotCreateAlternativeBody);
			}
			Charset.GetCharset(charsetName);
			MimeTnefMessage mimeTnefMessage = new MimeTnefMessage(bodyFormat, createAlternative, charsetName);
			return new EmailMessage(mimeTnefMessage);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000AA14 File Offset: 0x00008C14
		public static EmailMessage Create(MimeDocument mimeDocument)
		{
			if (mimeDocument == null)
			{
				throw new ArgumentNullException("document");
			}
			if (mimeDocument.RootPart == null)
			{
				throw new ArgumentException(EmailMessageStrings.MimeDocumentRootPartMustNotBeNull);
			}
			MimeTnefMessage mimeTnefMessage = new MimeTnefMessage(mimeDocument);
			return new EmailMessage(mimeTnefMessage);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000AA54 File Offset: 0x00008C54
		public static EmailMessage Create(Stream source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!source.CanRead)
			{
				throw new ArgumentException("Stream must support Read", "source");
			}
			MimeDocument mimeDocument = new MimeDocument();
			mimeDocument.Load(source, CachingMode.Copy);
			MimeTnefMessage mimeTnefMessage = new MimeTnefMessage(mimeDocument);
			return new EmailMessage(mimeTnefMessage);
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0000AAF0 File Offset: 0x00008CF0
		public EmailRecipient From
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipient from;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					from = this.message.From;
				}
				return from;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfReadOnly("set_From");
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.message.From = value;
				}
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000AB44 File Offset: 0x00008D44
		public EmailRecipientCollection To
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipientCollection to;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					to = this.message.To;
				}
				return to;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000AB8C File Offset: 0x00008D8C
		public EmailRecipientCollection Cc
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipientCollection cc;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					cc = this.message.Cc;
				}
				return cc;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000ABD4 File Offset: 0x00008DD4
		public EmailRecipientCollection Bcc
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipientCollection bcc;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					bcc = this.message.Bcc;
				}
				return bcc;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000AC1C File Offset: 0x00008E1C
		public EmailRecipientCollection ReplyTo
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipientCollection replyTo;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					replyTo = this.message.ReplyTo;
				}
				return replyTo;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000AC64 File Offset: 0x00008E64
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0000ACAC File Offset: 0x00008EAC
		public EmailRecipient DispositionNotificationTo
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipient dispositionNotificationTo;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					dispositionNotificationTo = this.message.DispositionNotificationTo;
				}
				return dispositionNotificationTo;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.message.DispositionNotificationTo = value;
				}
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0000AD3C File Offset: 0x00008F3C
		public EmailRecipient Sender
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipient sender;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					sender = this.message.Sender;
				}
				return sender;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfReadOnly("set_Sender");
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.message.Sender = value;
				}
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000AD90 File Offset: 0x00008F90
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public DateTime Date
		{
			get
			{
				this.ThrowIfDisposed();
				DateTime date;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					date = this.message.Date;
				}
				return date;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfReadOnly("set_Date");
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.message.Date = value;
				}
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000AE2C File Offset: 0x0000902C
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0000AE74 File Offset: 0x00009074
		public DateTime Expires
		{
			get
			{
				this.ThrowIfDisposed();
				DateTime expires;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					expires = this.message.Expires;
				}
				return expires;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfReadOnly("set_Expires");
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.message.Expires = value;
				}
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000AEC8 File Offset: 0x000090C8
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0000AF10 File Offset: 0x00009110
		public DateTime ReplyBy
		{
			get
			{
				this.ThrowIfDisposed();
				DateTime replyBy;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					replyBy = this.message.ReplyBy;
				}
				return replyBy;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfReadOnly("set_ReplyBy");
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.message.ReplyBy = value;
				}
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000AF64 File Offset: 0x00009164
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0000AFAC File Offset: 0x000091AC
		public string Subject
		{
			get
			{
				this.ThrowIfDisposed();
				string subject;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					subject = this.message.Subject;
				}
				return subject;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfReadOnly("set_Subject");
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.message.Subject = value;
				}
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000B000 File Offset: 0x00009200
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x0000B048 File Offset: 0x00009248
		public string MessageId
		{
			get
			{
				this.ThrowIfDisposed();
				string messageId;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					messageId = this.message.MessageId;
				}
				return messageId;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfReadOnly("set_MessageId");
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.message.MessageId = value;
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000B09C File Offset: 0x0000929C
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x0000B0E4 File Offset: 0x000092E4
		public Importance Importance
		{
			get
			{
				this.ThrowIfDisposed();
				Importance importance;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					importance = this.message.Importance;
				}
				return importance;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfReadOnly("set_Importance");
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.message.Importance = value;
				}
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000B138 File Offset: 0x00009338
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x0000B180 File Offset: 0x00009380
		public Priority Priority
		{
			get
			{
				this.ThrowIfDisposed();
				Priority priority;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					priority = this.message.Priority;
				}
				return priority;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfReadOnly("set_Priority");
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.message.Priority = value;
				}
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000B1D4 File Offset: 0x000093D4
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x0000B21C File Offset: 0x0000941C
		public Sensitivity Sensitivity
		{
			get
			{
				this.ThrowIfDisposed();
				Sensitivity sensitivity;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					sensitivity = this.message.Sensitivity;
				}
				return sensitivity;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfReadOnly("set_Sensitivity");
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.message.Sensitivity = value;
				}
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000B270 File Offset: 0x00009470
		public string MapiMessageClass
		{
			get
			{
				this.ThrowIfDisposed();
				string mapiMessageClass;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					mapiMessageClass = this.message.MapiMessageClass;
				}
				return mapiMessageClass;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000B2B8 File Offset: 0x000094B8
		public MimeDocument MimeDocument
		{
			get
			{
				this.ThrowIfDisposed();
				MimeDocument mimeDocument;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					mimeDocument = this.message.MimeDocument;
				}
				return mimeDocument;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000B300 File Offset: 0x00009500
		public MimePart RootPart
		{
			get
			{
				this.ThrowIfDisposed();
				MimePart rootPart;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					rootPart = this.message.RootPart;
				}
				return rootPart;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000B348 File Offset: 0x00009548
		public MimePart CalendarPart
		{
			get
			{
				this.ThrowIfDisposed();
				MimePart calendarPart;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					calendarPart = this.message.CalendarPart;
				}
				return calendarPart;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0000B390 File Offset: 0x00009590
		public MimePart TnefPart
		{
			get
			{
				this.ThrowIfDisposed();
				MimePart tnefPart;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					tnefPart = this.message.TnefPart;
				}
				return tnefPart;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public Body Body
		{
			get
			{
				this.ThrowIfDisposed();
				Body result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.Synchronize();
					if (this.body == null)
					{
						this.body = new Body(this);
					}
					result = this.body;
				}
				return result;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000B438 File Offset: 0x00009638
		public AttachmentCollection Attachments
		{
			get
			{
				this.ThrowIfDisposed();
				AttachmentCollection result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.Synchronize();
					if (this.attachmentCollection == null)
					{
						this.attachmentCollection = new AttachmentCollection(this);
					}
					result = this.attachmentCollection;
				}
				return result;
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000B498 File Offset: 0x00009698
		private void Synchronize()
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				this.message.Synchronize();
				if (this.version != this.message.Version)
				{
					this.version = this.message.Version;
					if (this.attachmentCollection != null)
					{
						this.attachmentCollection.InvalidateEnumerators();
					}
				}
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0000B514 File Offset: 0x00009714
		public bool IsInterpersonalMessage
		{
			get
			{
				this.ThrowIfDisposed();
				bool isInterpersonalMessage;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					isInterpersonalMessage = this.message.IsInterpersonalMessage;
				}
				return isInterpersonalMessage;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000B55C File Offset: 0x0000975C
		public bool IsSystemMessage
		{
			get
			{
				this.ThrowIfDisposed();
				bool isSystemMessage;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					isSystemMessage = this.message.IsSystemMessage;
				}
				return isSystemMessage;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000B5A4 File Offset: 0x000097A4
		public bool IsOpaqueMessage
		{
			get
			{
				this.ThrowIfDisposed();
				bool isOpaqueMessage;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					isOpaqueMessage = this.message.IsOpaqueMessage;
				}
				return isOpaqueMessage;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000B5EC File Offset: 0x000097EC
		public MessageSecurityType MessageSecurityType
		{
			get
			{
				this.ThrowIfDisposed();
				MessageSecurityType messageSecurityType;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					messageSecurityType = this.message.MessageSecurityType;
				}
				return messageSecurityType;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000B634 File Offset: 0x00009834
		internal EmailRecipientCollection BccFromOrgHeader
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipientCollection bccFromOrgHeader;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					bccFromOrgHeader = this.message.BccFromOrgHeader;
				}
				return bccFromOrgHeader;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000B67C File Offset: 0x0000987C
		internal bool IsReadOnly
		{
			get
			{
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					MimeDocument mimeDocument = this.MimeDocument;
					if (mimeDocument != null)
					{
						result = mimeDocument.IsReadOnly;
					}
					else
					{
						MimePart mimePart = this.RootPart;
						MimePart mimePart2 = mimePart;
						while (mimePart != null)
						{
							mimePart2 = mimePart;
							mimePart = (mimePart.Parent as MimePart);
						}
						if (mimePart2 != null)
						{
							mimeDocument = mimePart2.ParentDocument;
							if (mimeDocument != null)
							{
								return mimeDocument.IsReadOnly;
							}
						}
						result = false;
					}
				}
				return result;
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000B6FC File Offset: 0x000098FC
		internal void SetReadOnly(bool makeReadOnly)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeDocument mimeDocument = this.MimeDocument;
				if (mimeDocument == null)
				{
					throw new InvalidOperationException("An EmailMessage must be built on a MimeDocument in order to be read-only.");
				}
				if (makeReadOnly != mimeDocument.IsReadOnly)
				{
					this.SetReadOnly(mimeDocument, makeReadOnly, false);
					mimeDocument.SetReadOnlyInternal(makeReadOnly);
				}
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000B764 File Offset: 0x00009964
		private void SetReadOnly(MimeDocument parentDocument, bool makeReadOnly, bool isEmbedded)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (makeReadOnly)
				{
					if (this.MimeDocument != null)
					{
						this.MimeDocument.CompleteParse();
					}
					else if (this.RootPart != null)
					{
						parentDocument.BuildDomAndCompleteParse(this.RootPart);
					}
					List<EmailMessage> list = new List<EmailMessage>(this.Attachments.Count);
					foreach (Attachment attachment in this.Attachments)
					{
						if (attachment.IsEmbeddedMessage)
						{
							EmailMessage embeddedMessage = attachment.EmbeddedMessage;
							embeddedMessage.SetReadOnly(parentDocument, makeReadOnly, true);
							list.Add(embeddedMessage);
						}
					}
					if (list.Count > 0)
					{
						this.readOnlyEmbeddedMessages = list;
					}
					string mapiMessageClass = this.MapiMessageClass;
					int num = this.To.Count;
					num += this.Cc.Count;
					num += this.Bcc.Count;
					num += this.ReplyTo.Count;
					Body body = this.Body;
					AttachmentCollection attachments = this.Attachments;
					this.Synchronize();
				}
				else
				{
					this.readOnlyEmbeddedMessages = null;
				}
				this.message.SetReadOnly(makeReadOnly);
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		internal static bool IsDocumentReadOnly(MimeDocument document)
		{
			return document.IsReadOnly;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000B8D0 File Offset: 0x00009AD0
		internal static void SetDocumentReadOnly(MimeDocument document, bool makeReadOnly)
		{
			document.SetReadOnly(makeReadOnly);
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000B8DC File Offset: 0x00009ADC
		internal PureTnefMessage PureTnefMessage
		{
			get
			{
				this.ThrowIfDisposed();
				PureTnefMessage result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					PureTnefMessage pureTnefMessage = this.message as PureTnefMessage;
					result = pureTnefMessage;
				}
				return result;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000B928 File Offset: 0x00009B28
		internal IMapiPropertyAccess MapiProperties
		{
			get
			{
				this.ThrowIfDisposed();
				IMapiPropertyAccess mapiProperties;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					mapiProperties = this.message.MapiProperties;
				}
				return mapiProperties;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000B970 File Offset: 0x00009B70
		internal bool IsPublicFolderReplicationMessage
		{
			get
			{
				this.ThrowIfDisposed();
				bool isPublicFolderReplicationMessage;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					isPublicFolderReplicationMessage = this.message.IsPublicFolderReplicationMessage;
				}
				return isPublicFolderReplicationMessage;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		internal BodyStructure BodyStructure
		{
			get
			{
				this.ThrowIfDisposed();
				BodyStructure result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					PureMimeMessage pureMimeMessage = (this.message as MimeTnefMessage).PureMimeMessage;
					if (pureMimeMessage == null)
					{
						result = BodyStructure.Undefined;
					}
					else
					{
						result = pureMimeMessage.BodyStructure;
					}
				}
				return result;
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000BA14 File Offset: 0x00009C14
		public void Normalize()
		{
			this.Normalize(false);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000BA20 File Offset: 0x00009C20
		public void Normalize(bool allowUTF8)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("Normalize");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.attachmentCollection != null)
				{
					this.attachmentCollection.InvalidateEnumerators();
				}
				this.message.Normalize(allowUTF8);
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000BA88 File Offset: 0x00009C88
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000BA97 File Offset: 0x00009C97
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.ContentManager != null)
				{
					this.ContentManager.Dispose();
					this.ContentManager = null;
				}
				if (this.message != null)
				{
					this.message.Dispose();
					this.message = null;
				}
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		internal void Normalize(NormalizeOptions normalizeOptions, bool allowUTF8 = false)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("Normalize");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.message.Normalize(normalizeOptions, allowUTF8);
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000BB24 File Offset: 0x00009D24
		internal object GetMapiProperty(TnefPropertyTag tag)
		{
			this.ThrowIfDisposed();
			object mapiProperty;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				mapiProperty = this.message.GetMapiProperty(tag);
			}
			return mapiProperty;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000BB70 File Offset: 0x00009D70
		internal bool TryGetMapiProperty<T>(TnefPropertyTag propertyTag, out T propValue)
		{
			this.ThrowIfDisposed();
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				propValue = default(T);
				if (this.message.MapiProperties == null)
				{
					result = false;
				}
				else
				{
					object property = this.message.MapiProperties.GetProperty(propertyTag);
					if (property is T)
					{
						propValue = (T)((object)property);
						result = true;
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000BBF0 File Offset: 0x00009DF0
		internal void CopyTo(EmailMessage destination)
		{
			this.ThrowIfDisposed();
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				using (ThreadAccessGuard.EnterPublic(destination.accessToken))
				{
					this.message.CopyTo(destination.message);
				}
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000BC60 File Offset: 0x00009E60
		internal Charset TnefTextCharset
		{
			get
			{
				Charset textCharset;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					PureTnefMessage pureTnefMessage = this.message as PureTnefMessage;
					if (pureTnefMessage == null)
					{
						MimeTnefMessage mimeTnefMessage = this.message as MimeTnefMessage;
						if (mimeTnefMessage != null)
						{
							pureTnefMessage = mimeTnefMessage.PureTnefMessage;
						}
					}
					if (pureTnefMessage == null)
					{
						throw new NotSupportedException("Message does not contain TNEF data");
					}
					textCharset = pureTnefMessage.TextCharset;
				}
				return textCharset;
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		internal bool TryGetTnefBinaryCharset(out Charset charset)
		{
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				PureTnefMessage pureTnefMessage = this.message as PureTnefMessage;
				if (pureTnefMessage == null)
				{
					MimeTnefMessage mimeTnefMessage = this.message as MimeTnefMessage;
					if (mimeTnefMessage != null && mimeTnefMessage.HasTnef)
					{
						pureTnefMessage = mimeTnefMessage.PureTnefMessage;
					}
				}
				if (pureTnefMessage != null)
				{
					charset = pureTnefMessage.BinaryCharset;
					result = true;
				}
				else
				{
					charset = null;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000BD48 File Offset: 0x00009F48
		internal BodyFormat Body_GetBodyFormat()
		{
			this.ThrowIfDisposed();
			BodyFormat bodyFormat;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				IBody body = this.message.GetBody();
				bodyFormat = body.GetBodyFormat();
			}
			return bodyFormat;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000BD98 File Offset: 0x00009F98
		internal bool Body_ConversionNeeded(int[] validCodepages)
		{
			IBody body = this.message.GetBody();
			return body.ConversionNeeded(validCodepages);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		internal string Body_GetCharsetName()
		{
			this.ThrowIfDisposed();
			string charsetName;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				IBody body = this.message.GetBody();
				charsetName = body.GetCharsetName();
			}
			return charsetName;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000BE08 File Offset: 0x0000A008
		internal MimePart Body_GetMimePart()
		{
			this.ThrowIfDisposed();
			MimePart mimePart;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				IBody body = this.message.GetBody();
				mimePart = body.GetMimePart();
			}
			return mimePart;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000BE58 File Offset: 0x0000A058
		internal Stream Body_GetContentReadStream()
		{
			this.ThrowIfDisposed();
			Stream contentReadStream;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				IBody body = this.message.GetBody();
				contentReadStream = body.GetContentReadStream();
			}
			return contentReadStream;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000BEA8 File Offset: 0x0000A0A8
		internal bool Body_TryGetContentReadStream(out Stream stream)
		{
			this.ThrowIfDisposed();
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				IBody body = this.message.GetBody();
				result = body.TryGetContentReadStream(out stream);
			}
			return result;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		internal Stream Body_GetContentWriteStream(Charset charset)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("Body_GetContentWriteStream");
			Stream contentWriteStream;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				IBody body = this.message.GetBody();
				contentWriteStream = body.GetContentWriteStream(charset);
			}
			return contentWriteStream;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000BF54 File Offset: 0x0000A154
		internal AttachmentCookie AttachmentCollection_AddAttachment(Attachment attachment)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("AttachmentCollection_AddAttachment");
			AttachmentCookie result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.AttachmentCollection_AddAttachment(attachment);
			}
			return result;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		internal bool AttachmentCollection_RemoveAttachment(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("AttachmentCollection_RemoveAttachment");
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.AttachmentCollection_RemoveAttachment(cookie);
			}
			return result;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		internal void AttachmentCollection_ClearAttachments()
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("AttachmentCollection_ClearAttachment");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.message.AttachmentCollection_ClearAttachments();
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000C050 File Offset: 0x0000A250
		internal int AttachmentCollection_Count()
		{
			this.ThrowIfDisposed();
			int result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.AttachmentCollection_Count();
			}
			return result;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000C098 File Offset: 0x0000A298
		internal object AttachmentCollection_Indexer(int publicIndex)
		{
			this.ThrowIfDisposed();
			object result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.AttachmentCollection_Indexer(publicIndex);
			}
			return result;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		internal AttachmentCookie AttachmentCollection_CacheAttachment(int publicIndex, object attachment)
		{
			this.ThrowIfDisposed();
			AttachmentCookie result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.AttachmentCollection_CacheAttachment(publicIndex, attachment);
			}
			return result;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000C130 File Offset: 0x0000A330
		internal string Attachment_GetContentType(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			string result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetContentType(cookie);
			}
			return result;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000C17C File Offset: 0x0000A37C
		internal void Attachment_SetContentType(AttachmentCookie cookie, string contentType)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("Attachment_SetContentType");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.message.Attachment_SetContentType(cookie, contentType);
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
		internal AttachmentMethod Attachment_GetAttachmentMethod(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			AttachmentMethod result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetAttachmentMethod(cookie);
			}
			return result;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000C21C File Offset: 0x0000A41C
		internal InternalAttachmentType Attachment_GetAttachmentType(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			InternalAttachmentType result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetAttachmentType(cookie);
			}
			return result;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000C268 File Offset: 0x0000A468
		internal void Attachment_SetAttachmentType(AttachmentCookie cookie, InternalAttachmentType attachmentType)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("Attachment_SetAttachmentType");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.message.Attachment_SetAttachmentType(cookie, attachmentType);
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		internal EmailMessage Attachment_GetEmbeddedMessage(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			EmailMessage result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetEmbeddedMessage(cookie);
			}
			return result;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000C308 File Offset: 0x0000A508
		internal void Attachment_SetEmbeddedMessage(AttachmentCookie cookie, EmailMessage embeddedMessage)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("Attachment_SetEmbeddedMessage");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.message.Attachment_SetEmbeddedMessage(cookie, embeddedMessage);
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000C35C File Offset: 0x0000A55C
		internal MimePart Attachment_GetMimePart(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			MimePart result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetMimePart(cookie);
			}
			return result;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000C3A8 File Offset: 0x0000A5A8
		internal string Attachment_GetFileName(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			string result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetFileName(cookie, ref this.sequenceNumber);
			}
			return result;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		internal void Attachment_SetFileName(AttachmentCookie cookie, string fileName)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("Attachment_SetFileName");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.message.Attachment_SetFileName(cookie, fileName);
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000C44C File Offset: 0x0000A64C
		internal string Attachment_GetContentDisposition(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			string result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetContentDisposition(cookie);
			}
			return result;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000C498 File Offset: 0x0000A698
		internal void Attachment_SetContentDisposition(AttachmentCookie cookie, string contentDisposition)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("Attachment_SetContentDisposition");
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.message.Attachment_SetContentDisposition(cookie, contentDisposition);
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		internal bool Attachment_IsAppleDouble(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_IsAppleDouble(cookie);
			}
			return result;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000C538 File Offset: 0x0000A738
		internal Stream Attachment_GetContentReadStream(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			Stream result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetContentReadStream(cookie);
			}
			return result;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000C584 File Offset: 0x0000A784
		internal bool Attachment_TryGetContentReadStream(AttachmentCookie cookie, out Stream result)
		{
			this.ThrowIfDisposed();
			bool result2;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result2 = this.message.Attachment_TryGetContentReadStream(cookie, out result);
			}
			return result2;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
		internal Stream Attachment_GetContentWriteStream(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			this.ThrowIfReadOnly("Attachment_GetContentWriteStream");
			Stream result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetContentWriteStream(cookie);
			}
			return result;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000C624 File Offset: 0x0000A824
		internal int Attachment_GetRenderingPosition(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			int result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetRenderingPosition(cookie);
			}
			return result;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000C670 File Offset: 0x0000A870
		internal string Attachment_GetAttachContentID(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			string result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetAttachContentID(cookie);
			}
			return result;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		internal string Attachment_GetAttachContentLocation(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			string result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetAttachContentLocation(cookie);
			}
			return result;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000C708 File Offset: 0x0000A908
		internal byte[] Attachment_GetAttachRendering(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			byte[] result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetAttachRendering(cookie);
			}
			return result;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000C754 File Offset: 0x0000A954
		internal int Attachment_GetAttachmentFlags(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			int result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetAttachmentFlags(cookie);
			}
			return result;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		internal bool Attachment_GetAttachHidden(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetAttachHidden(cookie);
			}
			return result;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		internal int Attachment_GetHashCode(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			int result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.message.Attachment_GetHashCode(cookie);
			}
			return result;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000C838 File Offset: 0x0000AA38
		internal void Attachment_Dispose(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.message.Attachment_Dispose(cookie);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000C880 File Offset: 0x0000AA80
		private void ThrowIfDisposed()
		{
			if (this.message == null)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000C89B File Offset: 0x0000AA9B
		private void ThrowIfReadOnly(string method)
		{
			if (this.IsReadOnly)
			{
				throw new ReadOnlyMimeException(method);
			}
		}

		// Token: 0x040002B1 RID: 689
		private EmailMessage.EmailMessageThreadAccessToken accessToken;

		// Token: 0x040002B2 RID: 690
		internal static bool TestabilityEnableBetterFuzzing;

		// Token: 0x040002B3 RID: 691
		private MessageImplementation message;

		// Token: 0x040002B4 RID: 692
		private Body body;

		// Token: 0x040002B5 RID: 693
		private AttachmentCollection attachmentCollection;

		// Token: 0x040002B6 RID: 694
		private List<EmailMessage> readOnlyEmbeddedMessages;

		// Token: 0x040002B7 RID: 695
		private int version = -1;

		// Token: 0x040002B8 RID: 696
		private int sequenceNumber;

		// Token: 0x040002B9 RID: 697
		internal IDisposable ContentManager;

		// Token: 0x020000CC RID: 204
		private class EmailMessageThreadAccessToken : ObjectThreadAccessToken
		{
			// Token: 0x06000508 RID: 1288 RVA: 0x0000C8AE File Offset: 0x0000AAAE
			internal EmailMessageThreadAccessToken(EmailMessage parent)
			{
			}
		}
	}
}

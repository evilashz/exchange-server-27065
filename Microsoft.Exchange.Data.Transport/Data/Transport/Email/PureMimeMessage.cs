using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000F4 RID: 244
	internal class PureMimeMessage : MessageImplementation, IBody
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x00010D9C File Offset: 0x0000EF9C
		public PureMimeMessage(BodyFormat bodyFormat, bool createAlternative, string charsetName)
		{
			this.mimeDocument = new MimeDocument();
			MimePart mimePart = new MimePart();
			this.mimeDocument.RootPart = mimePart;
			this.rootPart = mimePart;
			mimePart.Headers.AppendChild(new AsciiTextHeader("MIME-Version", "1.0"));
			this.bodyList = new List<MimePart>();
			if (createAlternative)
			{
				ContentTypeHeader newChild = new ContentTypeHeader("multipart/alternative");
				mimePart.Headers.AppendChild(newChild);
				MimePart mimePart2 = Utility.CreateBodyPart("text/plain", charsetName);
				mimePart.AppendChild(mimePart2);
				this.bodyList.Add(mimePart2);
				MimePart mimePart3 = Utility.CreateBodyPart("text/html", charsetName);
				mimePart.AppendChild(mimePart3);
				this.bodyList.Add(mimePart3);
				this.bodyStructure = BodyStructure.AlternativeBodies;
			}
			else
			{
				this.bodyList.Add(this.rootPart);
				string value = (bodyFormat == BodyFormat.Text) ? "text/plain" : "text/html";
				ContentTypeHeader contentTypeHeader = new ContentTypeHeader(value);
				MimeParameter newChild2 = new MimeParameter("charset", charsetName);
				contentTypeHeader.AppendChild(newChild2);
				mimePart.Headers.AppendChild(contentTypeHeader);
				this.bodyStructure = BodyStructure.SingleBody;
			}
			this.messageType = MessageType.Normal;
			this.PickBestBody();
			this.UpdateMimeVersion();
			this.defaultBodyCharset = Charset.GetCharset(charsetName);
			this.accessToken = new PureMimeMessage.PureMimeMessageThreadAccessToken(this);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00010F08 File Offset: 0x0000F108
		public PureMimeMessage(MimeDocument mimeDocument)
		{
			this.mimeDocument = mimeDocument;
			this.rootPart = mimeDocument.RootPart;
			this.GetCharsetFromMimeDocument(mimeDocument);
			if (mimeDocument.IsReadOnly)
			{
				this.Synchronize();
			}
			this.accessToken = new PureMimeMessage.PureMimeMessageThreadAccessToken(this);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00010F78 File Offset: 0x0000F178
		public PureMimeMessage(MimePart rootPart)
		{
			this.rootPart = rootPart;
			MimeDocument mimeDocument;
			MimeNode mimeNode;
			rootPart.GetMimeDocumentOrTreeRoot(out mimeDocument, out mimeNode);
			if (mimeDocument != null)
			{
				this.GetCharsetFromMimeDocument(mimeDocument);
				if (mimeDocument.IsReadOnly)
				{
					this.Synchronize();
				}
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00010FDC File Offset: 0x0000F1DC
		public PureMimeMessage(Stream source)
		{
			this.mimeDocument = new MimeDocument();
			this.mimeDocument.Load(source, CachingMode.Copy);
			this.rootPart = this.mimeDocument.RootPart;
			this.GetCharsetFromMimeDocument(this.mimeDocument);
			this.accessToken = new PureMimeMessage.PureMimeMessageThreadAccessToken(this);
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00011059 File Offset: 0x0000F259
		internal override ObjectThreadAccessToken AccessToken
		{
			get
			{
				return this.accessToken;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00011064 File Offset: 0x0000F264
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x000110C0 File Offset: 0x0000F2C0
		public override EmailRecipient From
		{
			get
			{
				EmailRecipient fromRecipient;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (!this.IsSynchronized || this.FromRecipient == null)
					{
						this.FromRecipient = this.GetRecipient(HeaderId.From);
					}
					fromRecipient = this.FromRecipient;
				}
				return fromRecipient;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (value != null && (value.MimeRecipient.Parent != null || value.TnefRecipient.TnefMessage != null))
					{
						throw new ArgumentException(EmailMessageStrings.RecipientAlreadyHasParent, "value");
					}
					this.SetRecipient(HeaderId.From, value);
					this.FromRecipient = value;
				}
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00011134 File Offset: 0x0000F334
		public override EmailRecipientCollection To
		{
			get
			{
				EmailRecipientCollection toRecipients;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (!this.IsSynchronized || this.ToRecipients == null)
					{
						this.ToRecipients = this.GetRecipientCollection(RecipientType.To);
					}
					toRecipients = this.ToRecipients;
				}
				return toRecipients;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00011190 File Offset: 0x0000F390
		public override EmailRecipientCollection Cc
		{
			get
			{
				EmailRecipientCollection ccRecipients;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (!this.IsSynchronized || this.CcRecipients == null)
					{
						this.CcRecipients = this.GetRecipientCollection(RecipientType.Cc);
					}
					ccRecipients = this.CcRecipients;
				}
				return ccRecipients;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x000111EC File Offset: 0x0000F3EC
		public override EmailRecipientCollection Bcc
		{
			get
			{
				EmailRecipientCollection bccRecipients;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (!this.IsSynchronized || this.BccRecipients == null)
					{
						this.BccRecipients = this.GetRecipientCollection(RecipientType.Bcc);
					}
					bccRecipients = this.BccRecipients;
				}
				return bccRecipients;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00011248 File Offset: 0x0000F448
		public override EmailRecipientCollection ReplyTo
		{
			get
			{
				EmailRecipientCollection replyToRecipients;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (!this.IsSynchronized || this.ReplyToRecipients == null)
					{
						this.ReplyToRecipients = this.GetRecipientCollection(RecipientType.ReplyTo);
					}
					replyToRecipients = this.ReplyToRecipients;
				}
				return replyToRecipients;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x000112A4 File Offset: 0x0000F4A4
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x00011300 File Offset: 0x0000F500
		public override EmailRecipient DispositionNotificationTo
		{
			get
			{
				EmailRecipient dntRecipient;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (!this.IsSynchronized || this.DntRecipient == null)
					{
						this.DntRecipient = this.GetRecipient(HeaderId.DispositionNotificationTo);
					}
					dntRecipient = this.DntRecipient;
				}
				return dntRecipient;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (value != null && (value.MimeRecipient.Parent != null || value.TnefRecipient.TnefMessage != null))
					{
						throw new ArgumentException(EmailMessageStrings.RecipientAlreadyHasParent, "value");
					}
					this.SetRecipient(HeaderId.DispositionNotificationTo, value);
					this.DntRecipient = value;
				}
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00011374 File Offset: 0x0000F574
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x000113E4 File Offset: 0x0000F5E4
		public override EmailRecipient Sender
		{
			get
			{
				EmailRecipient senderRecipient;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (!this.IsSynchronized || this.SenderRecipient == null)
					{
						this.SenderRecipient = this.GetRecipient(HeaderId.Sender);
						if (this.SenderRecipient == null)
						{
							this.SenderRecipient = this.GetRecipient(HeaderId.From);
						}
					}
					senderRecipient = this.SenderRecipient;
				}
				return senderRecipient;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (value != null && (value.MimeRecipient.Parent != null || value.TnefRecipient.TnefMessage != null))
					{
						throw new ArgumentException(EmailMessageStrings.RecipientAlreadyHasParent, "value");
					}
					this.SetRecipient(HeaderId.Sender, value);
					this.SenderRecipient = value;
				}
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x00011458 File Offset: 0x0000F658
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x00011498 File Offset: 0x0000F698
		public override DateTime Date
		{
			get
			{
				DateTime headerDate;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					headerDate = this.GetHeaderDate(HeaderId.Date);
				}
				return headerDate;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.SetHeaderDate(HeaderId.Date, value);
				}
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x000114D8 File Offset: 0x0000F6D8
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x00011580 File Offset: 0x0000F780
		public override DateTime Expires
		{
			get
			{
				DateTime result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					DateTime dateTime = DateTime.MinValue;
					foreach (Header header in this.rootPart.Headers)
					{
						DateHeader dateHeader = header as DateHeader;
						if (dateHeader != null && (HeaderId.ExpiryDate == dateHeader.HeaderId || HeaderId.Expires == dateHeader.HeaderId))
						{
							dateTime = dateHeader.UtcDateTime;
						}
					}
					result = dateTime;
				}
				return result;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					bool isSynchronized = this.IsSynchronized;
					bool flag = value == DateTime.MinValue;
					foreach (Header header in this.rootPart.Headers)
					{
						if (HeaderId.ExpiryDate == header.HeaderId)
						{
							header.RemoveFromParent();
						}
						else if (HeaderId.Expires == header.HeaderId)
						{
							DateHeader dateHeader = header as DateHeader;
							if (flag || dateHeader == null)
							{
								header.RemoveFromParent();
							}
							else
							{
								dateHeader.DateTime = value;
								flag = true;
							}
						}
					}
					if (!flag)
					{
						DateHeader dateHeader2 = Header.Create(HeaderId.Expires) as DateHeader;
						dateHeader2.DateTime = value;
						this.rootPart.Headers.AppendChild(dateHeader2);
					}
					if (isSynchronized)
					{
						this.UpdateMimeVersion();
					}
				}
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0001167C File Offset: 0x0000F87C
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x000116BC File Offset: 0x0000F8BC
		public override DateTime ReplyBy
		{
			get
			{
				DateTime headerDate;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					headerDate = this.GetHeaderDate(HeaderId.ReplyBy);
				}
				return headerDate;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.SetHeaderDate(HeaderId.ReplyBy, value);
				}
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x000116FC File Offset: 0x0000F8FC
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x00011764 File Offset: 0x0000F964
		public override string Subject
		{
			get
			{
				string result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					string text = this.GetHeaderString(HeaderId.Subject);
					if (string.IsNullOrEmpty(text) && this.HasCalendar)
					{
						text = this.calendarMessage.Subject;
					}
					result = (text ?? string.Empty);
				}
				return result;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.SetHeaderString(HeaderId.Subject, value);
					bool isSynchronized = this.IsSynchronized;
					this.RootPart.Headers.RemoveAll("Thread-Topic");
					if (isSynchronized)
					{
						this.UpdateMimeVersion();
					}
					if (this.HasCalendar)
					{
						this.calendarMessage.Subject = value;
					}
				}
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x000117DC File Offset: 0x0000F9DC
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x00011824 File Offset: 0x0000FA24
		public override string MessageId
		{
			get
			{
				string result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = (this.GetHeaderString(HeaderId.MessageId) ?? string.Empty);
				}
				return result;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.SetHeaderString(HeaderId.MessageId, value);
				}
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00011864 File Offset: 0x0000FA64
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x000118E0 File Offset: 0x0000FAE0
		public override Importance Importance
		{
			get
			{
				Importance result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					int num = 0;
					if (this.TryGetHeaderEnum(HeaderId.Importance, EnumUtility.ImportanceMap, ref num))
					{
						result = (Importance)num;
					}
					else if (this.TryGetHeaderEnum(HeaderId.XPriority, EnumUtility.XPriorityMap, ref num))
					{
						result = (Importance)num;
					}
					else if (this.TryGetHeaderEnum(HeaderId.XMSMailPriority, EnumUtility.ImportanceMap, ref num))
					{
						result = (Importance)num;
					}
					else
					{
						result = (Importance)num;
					}
				}
				return result;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.SetHeaderEnum(HeaderId.Importance, EnumUtility.ImportanceMap, (int)value);
					this.SetHeaderEnum(HeaderId.XPriority, EnumUtility.XPriorityMap, (int)value);
					this.RemoveHeaders(HeaderId.XMSMailPriority);
				}
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0001193C File Offset: 0x0000FB3C
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x00011988 File Offset: 0x0000FB88
		public override Priority Priority
		{
			get
			{
				Priority result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					int num = 0;
					this.TryGetHeaderEnum(HeaderId.Priority, EnumUtility.PriorityMap, ref num);
					result = (Priority)num;
				}
				return result;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.SetHeaderEnum(HeaderId.Priority, EnumUtility.PriorityMap, (int)value);
				}
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x000119CC File Offset: 0x0000FBCC
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x00011A18 File Offset: 0x0000FC18
		public override Sensitivity Sensitivity
		{
			get
			{
				Sensitivity result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					int num = 0;
					this.TryGetHeaderEnum(HeaderId.Sensitivity, EnumUtility.SensitivityMap, ref num);
					result = (Sensitivity)num;
				}
				return result;
			}
			set
			{
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.SetHeaderEnum(HeaderId.Sensitivity, EnumUtility.SensitivityMap, (int)value);
				}
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00011A5C File Offset: 0x0000FC5C
		public override string MapiMessageClass
		{
			get
			{
				string mapiMessageClass;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.Synchronize();
					if (this.HasCalendar)
					{
						mapiMessageClass = this.calendarMessage.MapiMessageClass;
					}
					else
					{
						mapiMessageClass = this.messageClass;
					}
				}
				return mapiMessageClass;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00011AB8 File Offset: 0x0000FCB8
		public override MimeDocument MimeDocument
		{
			get
			{
				MimeDocument result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.mimeDocument;
				}
				return result;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00011AF8 File Offset: 0x0000FCF8
		public override MimePart RootPart
		{
			get
			{
				MimePart result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.rootPart;
				}
				return result;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00011B38 File Offset: 0x0000FD38
		public override MimePart CalendarPart
		{
			get
			{
				MimePart result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.Synchronize();
					result = this.calendarPart;
				}
				return result;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00011B7C File Offset: 0x0000FD7C
		public override MimePart TnefPart
		{
			get
			{
				MimePart result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.tnefPart;
				}
				return result;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00011BBC File Offset: 0x0000FDBC
		public override bool IsInterpersonalMessage
		{
			get
			{
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.Synchronize();
					MessageFlags messageFlags = MessageTypeTable.GetMessageFlags(this.messageType);
					result = ((messageFlags & MessageFlags.Normal) != MessageFlags.None);
				}
				return result;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00011C10 File Offset: 0x0000FE10
		public override bool IsSystemMessage
		{
			get
			{
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.Synchronize();
					MessageFlags messageFlags = MessageTypeTable.GetMessageFlags(this.messageType);
					result = ((messageFlags & MessageFlags.System) > MessageFlags.None);
				}
				return result;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00011C60 File Offset: 0x0000FE60
		public override bool IsOpaqueMessage
		{
			get
			{
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.Synchronize();
					MessageSecurityType messageSecurityType = MessageTypeTable.GetMessageSecurityType(this.messageType);
					result = (messageSecurityType == MessageSecurityType.Encrypted || messageSecurityType == MessageSecurityType.OpaqueSigned);
				}
				return result;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		public override MessageSecurityType MessageSecurityType
		{
			get
			{
				MessageSecurityType result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					this.Synchronize();
					MessageSecurityType messageSecurityType = MessageTypeTable.GetMessageSecurityType(this.messageType);
					result = messageSecurityType;
				}
				return result;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00011D00 File Offset: 0x0000FF00
		public override bool IsPublicFolderReplicationMessage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00011D04 File Offset: 0x0000FF04
		internal bool IsSynchronized
		{
			get
			{
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = (this.Version == this.version);
				}
				return result;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00011D4C File Offset: 0x0000FF4C
		internal override int Version
		{
			get
			{
				if (this.mimeDocument == null)
				{
					return this.rootPart.Version;
				}
				return this.mimeDocument.Version;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00011D70 File Offset: 0x0000FF70
		internal override EmailRecipientCollection BccFromOrgHeader
		{
			get
			{
				EmailRecipientCollection bccFromOrgHeaderRecipients;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (!this.IsSynchronized || this.BccFromOrgHeaderRecipients == null)
					{
						this.BccFromOrgHeaderRecipients = this.GetRecipientCollection(RecipientType.Bcc, HeaderId.XExchangeBcc);
					}
					bccFromOrgHeaderRecipients = this.BccFromOrgHeaderRecipients;
				}
				return bccFromOrgHeaderRecipients;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00011DCC File Offset: 0x0000FFCC
		internal bool HasCalendar
		{
			get
			{
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.CalendarPart == null)
					{
						result = false;
					}
					else if (!this.LoadCalendar())
					{
						result = false;
					}
					else
					{
						result = true;
					}
				}
				return result;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00011E1C File Offset: 0x0001001C
		private bool UseCalendarBody
		{
			get
			{
				bool result;
				using (ThreadAccessGuard.EnterPrivate(this.accessToken))
				{
					if (this.bodyMimePart == null)
					{
						result = false;
					}
					else if (this.bodyMimePart != this.calendarPart)
					{
						result = false;
					}
					else if (!this.HasCalendar)
					{
						result = false;
					}
					else
					{
						result = true;
					}
				}
				return result;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00011E80 File Offset: 0x00010080
		internal string TnefCorrelator
		{
			get
			{
				string tnefCorrelator;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					tnefCorrelator = Utility.GetTnefCorrelator(this.rootPart);
				}
				return tnefCorrelator;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00011EC4 File Offset: 0x000100C4
		internal BodyStructure BodyStructure
		{
			get
			{
				BodyStructure result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.bodyStructure;
				}
				return result;
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00011F04 File Offset: 0x00010104
		private static bool CheckKeyPart(MimePart currentPart, bool match, ref bool expected, ref MimePart referencePart)
		{
			if (match && expected)
			{
				referencePart = currentPart;
				expected = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00011F16 File Offset: 0x00010116
		private static bool IsVoiceContentType(string contentType)
		{
			return contentType == "audio/wav" || contentType == "audio/wma" || contentType == "audio/mp3" || contentType == "audio/gsm";
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00011F50 File Offset: 0x00010150
		private static bool FileNameIndicatesSmime(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				foreach (string value in PureMimeMessage.smimeExtensions)
				{
					if (name.EndsWith(value, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00011F90 File Offset: 0x00010190
		private static HeaderId GetHeaderId(RecipientType recipientType)
		{
			switch (recipientType)
			{
			case RecipientType.To:
				return HeaderId.To;
			case RecipientType.From:
				return HeaderId.From;
			case RecipientType.Cc:
				return HeaderId.Cc;
			case RecipientType.Bcc:
				return HeaderId.Bcc;
			case RecipientType.ReplyTo:
				return HeaderId.ReplyTo;
			default:
				return HeaderId.Unknown;
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00011FC8 File Offset: 0x000101C8
		internal static void GetMessageClassSuffix(IEnumerable<KeyValuePair<string, string>> map, string key, ref string suffix)
		{
			bool flag = suffix == null;
			foreach (KeyValuePair<string, string> keyValuePair in map)
			{
				if (!flag && suffix == keyValuePair.Value)
				{
					flag = true;
				}
				if (key.Contains(keyValuePair.Key) && flag)
				{
					suffix = keyValuePair.Value;
				}
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00012040 File Offset: 0x00010240
		internal override void SetReadOnly(bool makeReadOnly)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.calendarRelayStorage != null)
				{
					this.calendarRelayStorage.SetReadOnly(makeReadOnly);
				}
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001208C File Offset: 0x0001028C
		private static bool IsAppleSingleAttachment(MimeAttachmentData data)
		{
			return data.AttachmentPart.ContentType == "application/applefile";
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000120A4 File Offset: 0x000102A4
		private static Dictionary<string, string> ReadReportHeaders(Stream stream)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			using (Stream stream2 = new SuppressCloseStream(stream))
			{
				using (MimeReader mimeReader = new MimeReader(stream2))
				{
					long position = stream.Position;
					if (!mimeReader.ReadNextPart())
					{
						return null;
					}
					MimeHeaderReader headerReader = mimeReader.HeaderReader;
					while (headerReader.ReadNextHeader())
					{
						DecodingResults decodingResults;
						string text;
						headerReader.TryGetValue(Utility.DecodeOrFallBack, out decodingResults, out text);
						if (text != null)
						{
							dictionary[headerReader.Name] = Utility.RemoveMimeHeaderComments(text);
						}
					}
					stream.Position = position + mimeReader.StreamOffset;
				}
			}
			return dictionary;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00012160 File Offset: 0x00010360
		private static Charset DetermineTextPartCharset(MimePart part, Charset defaultBodyCharset, out bool defaulted)
		{
			string parameterValue = Utility.GetParameterValue(part, HeaderId.ContentType, "charset");
			Charset result;
			if (Charset.TryGetCharset(parameterValue, out result))
			{
				defaulted = false;
				return result;
			}
			defaulted = true;
			return defaultBodyCharset;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00012190 File Offset: 0x00010390
		internal void SetTnefPart(MimePart tnefPart)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.tnefPart = tnefPart;
				this.Synchronize();
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000121D4 File Offset: 0x000103D4
		internal override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.bodyData != null)
				{
					this.bodyData.Dispose();
					this.bodyData = null;
				}
				if (this.mimeDocument != null)
				{
					this.mimeDocument.Dispose();
					this.mimeDocument = null;
					this.rootPart = null;
				}
				else if (this.rootPart != null)
				{
					this.rootPart.Dispose();
					this.rootPart = null;
				}
				if (this.calendarRelayStorage != null)
				{
					this.calendarRelayStorage.Release();
					this.calendarRelayStorage = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001225C File Offset: 0x0001045C
		public override void Normalize(bool allowUTF8 = false)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.Normalize(NormalizeOptions.NormalizeMime, allowUTF8);
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000122A0 File Offset: 0x000104A0
		internal override void Normalize(NormalizeOptions normalizeOptions, bool allowUTF8)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if ((normalizeOptions & NormalizeOptions.NormalizeMimeStructure) != (NormalizeOptions)0)
				{
					this.NormalizeStructure(false);
				}
				if ((normalizeOptions & NormalizeOptions.NormalizeMessageId) != (NormalizeOptions)0)
				{
					this.NormalizeMessageId(allowUTF8);
				}
				if ((normalizeOptions & NormalizeOptions.NormalizeCte) != (NormalizeOptions)0)
				{
					this.NormalizeCte();
				}
				if ((normalizeOptions & NormalizeOptions.MergeAddressHeaders) != (NormalizeOptions)0)
				{
					bool isSynchronized = this.IsSynchronized;
					Utility.NormalizeHeaders(this.rootPart, Utility.HeaderNormalization.MergeAddressHeaders);
					if (isSynchronized)
					{
						this.UpdateMimeVersion();
					}
				}
				if ((normalizeOptions & NormalizeOptions.RemoveDuplicateHeaders) != (NormalizeOptions)0)
				{
					bool isSynchronized2 = this.IsSynchronized;
					Utility.NormalizeHeaders(this.rootPart, Utility.HeaderNormalization.PruneRestrictedHeaders);
					if (isSynchronized2)
					{
						this.UpdateMimeVersion();
					}
				}
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001233C File Offset: 0x0001053C
		private void NormalizeMessageId(bool allowUTF8)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.rootPart != null)
				{
					Header lastHeaderRemoveDuplicates = this.GetLastHeaderRemoveDuplicates(HeaderId.MessageId);
					if (lastHeaderRemoveDuplicates == null)
					{
						Header header = Header.Create(HeaderId.MessageId);
						header.Value = this.CreateMessageId(allowUTF8);
						this.rootPart.Headers.AppendChild(header);
					}
					else
					{
						string headerValue = Utility.GetHeaderValue(lastHeaderRemoveDuplicates);
						if (string.IsNullOrEmpty(headerValue) || headerValue.Trim().Length == 0)
						{
							lastHeaderRemoveDuplicates.Value = this.CreateMessageId(allowUTF8);
						}
						else if (headerValue.Length > PureMimeMessage.maxMessageIdLength)
						{
							lastHeaderRemoveDuplicates.Value = this.CreateMessageId(allowUTF8);
						}
						else
						{
							foreach (char c in headerValue)
							{
								if ((!allowUTF8 || c < '\u0080') && (c < ' ' || c > '~'))
								{
									lastHeaderRemoveDuplicates.Value = this.CreateMessageId(allowUTF8);
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00012444 File Offset: 0x00010644
		private void NormalizeCte()
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				using (MimePart.SubtreeEnumerator enumerator = this.rootPart.Subtree.GetEnumerator(MimePart.SubtreeEnumerationOptions.RevisitParent, false))
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.LastVisit && !enumerator.FirstVisit)
						{
							this.NormalizeParentCte(enumerator.Current);
						}
					}
				}
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000124D8 File Offset: 0x000106D8
		private void NormalizeParentCte(MimePart parent)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				ContentTransferEncoding contentTransferEncoding = ContentTransferEncoding.SevenBit;
				foreach (MimePart mimePart in parent)
				{
					if (mimePart.ContentTransferEncoding == ContentTransferEncoding.Binary)
					{
						contentTransferEncoding = ContentTransferEncoding.Binary;
						break;
					}
					if (mimePart.ContentTransferEncoding == ContentTransferEncoding.EightBit)
					{
						contentTransferEncoding = ContentTransferEncoding.EightBit;
					}
				}
				if (ContentTransferEncoding.Binary == contentTransferEncoding || ContentTransferEncoding.EightBit == contentTransferEncoding)
				{
					string value = (ContentTransferEncoding.Binary == contentTransferEncoding) ? "binary" : "8bit";
					Header header = parent.Headers.FindFirst(HeaderId.ContentTransferEncoding);
					if (header != null)
					{
						if (contentTransferEncoding != MimePart.GetEncodingType(header.FirstRawToken))
						{
							header.Value = value;
						}
					}
					else
					{
						header = Header.Create(HeaderId.ContentTransferEncoding);
						header.Value = value;
						parent.Headers.AppendChild(header);
					}
				}
				else
				{
					Header header2 = parent.Headers.FindFirst(HeaderId.ContentTransferEncoding);
					if (header2 != null)
					{
						contentTransferEncoding = MimePart.GetEncodingType(header2.FirstRawToken);
						if (ContentTransferEncoding.Binary == contentTransferEncoding || ContentTransferEncoding.EightBit == contentTransferEncoding)
						{
							header2.RemoveFromParent();
						}
					}
				}
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000125F0 File Offset: 0x000107F0
		internal void NormalizeStructure(bool forceRebuild)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (forceRebuild || this.rebuildStructureAtNextOpportunity)
				{
					this.RebuildMessage();
				}
				else
				{
					this.Synchronize();
					MessageFlags messageFlags = MessageTypeTable.GetMessageFlags(this.messageType);
					if ((messageFlags & MessageFlags.Normal) == MessageFlags.None || this.rebuildStructureAtNextOpportunity)
					{
						this.RebuildMessage();
					}
				}
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00012660 File Offset: 0x00010860
		internal void InvalidateCalendarContent()
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.calendarRelayStorage == null)
				{
					this.calendarRelayStorage = new RelayStorage(this.calendarMessage);
				}
				else
				{
					this.calendarRelayStorage.Invalidate();
				}
				bool contentDirty = this.calendarPart.ContentDirty;
				bool isSynchronized = this.IsSynchronized;
				ContentTransferEncoding contentTransferEncoding = this.calendarPart.ContentTransferEncoding;
				if (contentTransferEncoding == ContentTransferEncoding.Unknown || ContentTransferEncoding.SevenBit == contentTransferEncoding || ContentTransferEncoding.EightBit == contentTransferEncoding)
				{
					Header header = this.calendarPart.Headers.FindFirst(HeaderId.ContentTransferEncoding);
					if (header == null)
					{
						header = Header.Create(HeaderId.ContentTransferEncoding);
						this.calendarPart.Headers.AppendChild(header);
					}
					header.Value = "quoted-printable";
				}
				this.calendarPart.SetStorage(this.calendarRelayStorage, 0L, long.MaxValue);
				if (isSynchronized)
				{
					this.UpdateMimeVersion();
				}
				this.calendarPart.ContentDirty = contentDirty;
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00012754 File Offset: 0x00010954
		internal void SetPartCharset(MimePart part, string charsetName)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				bool isSynchronized = this.IsSynchronized;
				Utility.SetParameterValue(part, HeaderId.ContentType, "charset", charsetName);
				if (isSynchronized)
				{
					this.UpdateMimeVersion();
				}
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x000127A8 File Offset: 0x000109A8
		internal EmailRecipientCollection GetRecipientCollection(RecipientType recipientType)
		{
			HeaderId headerId = PureMimeMessage.GetHeaderId(recipientType);
			return this.GetRecipientCollection(recipientType, headerId);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000127C4 File Offset: 0x000109C4
		internal EmailRecipientCollection GetRecipientCollection(RecipientType recipientType, HeaderId headerId)
		{
			EmailRecipientCollection result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				EmailRecipientCollection emailRecipientCollection = new EmailRecipientCollection(this, recipientType, this.MimeDocument != null && this.MimeDocument.IsReadOnly);
				int num = 0;
				for (Header header = this.rootPart.Headers.FindFirst(headerId); header != null; header = this.rootPart.Headers.FindNext(header))
				{
					num++;
					if (1 == num)
					{
						emailRecipientCollection.Cache = header;
					}
					else
					{
						emailRecipientCollection.Cache = null;
					}
					foreach (MimeNode mimeNode in header)
					{
						MimeRecipient mimeRecipient = mimeNode as MimeRecipient;
						if (mimeRecipient != null)
						{
							emailRecipientCollection.InternalAdd(new EmailRecipient(mimeRecipient));
						}
						else
						{
							MimeGroup mimeGroup = mimeNode as MimeGroup;
							foreach (MimeRecipient recipient in mimeGroup)
							{
								emailRecipientCollection.InternalAdd(new EmailRecipient(recipient));
							}
						}
					}
				}
				result = emailRecipientCollection;
			}
			return result;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00012908 File Offset: 0x00010B08
		internal override void AddRecipient(RecipientType recipientType, ref object cachedHeader, EmailRecipient newRecipient)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				bool isSynchronized = this.IsSynchronized;
				AddressHeader recipientHeader = this.GetRecipientHeader(recipientType, ref cachedHeader);
				recipientHeader.AppendChild(newRecipient.MimeRecipient);
				if (isSynchronized)
				{
					this.UpdateMimeVersion();
				}
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00012964 File Offset: 0x00010B64
		internal override void RemoveRecipient(RecipientType recipientType, ref object cachedHeader, EmailRecipient oldRecipient)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				bool isSynchronized = this.IsSynchronized;
				MimeNode parent = oldRecipient.MimeRecipient.Parent;
				oldRecipient.MimeRecipient.RemoveFromParent();
				if (parent is MimeGroup && !parent.HasChildren)
				{
					parent.RemoveFromParent();
				}
				AddressHeader recipientHeader = this.GetRecipientHeader(recipientType, ref cachedHeader);
				if (!recipientHeader.HasChildren)
				{
					recipientHeader.RemoveFromParent();
					cachedHeader = null;
				}
				if (isSynchronized)
				{
					this.UpdateMimeVersion();
				}
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000129F0 File Offset: 0x00010BF0
		internal override void ClearRecipients(RecipientType recipientType, ref object cachedHeader)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				bool isSynchronized = this.IsSynchronized;
				if (cachedHeader != null)
				{
					AddressHeader addressHeader = cachedHeader as AddressHeader;
					addressHeader.RemoveAll();
					addressHeader.RemoveFromParent();
					cachedHeader = null;
				}
				else
				{
					HeaderId headerId = PureMimeMessage.GetHeaderId(recipientType);
					Header header2;
					for (Header header = this.rootPart.Headers.FindFirst(headerId); header != null; header = header2)
					{
						header2 = this.rootPart.Headers.FindNext(header);
						header.RemoveAll();
						header.RemoveFromParent();
					}
				}
				if (isSynchronized)
				{
					this.UpdateMimeVersion();
				}
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00012A9C File Offset: 0x00010C9C
		internal override AttachmentCookie AttachmentCollection_AddAttachment(Attachment attachment)
		{
			AttachmentCookie result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.IsOpaqueMessage)
				{
					throw new InvalidOperationException(EmailMessageStrings.CannotAddAttachment);
				}
				this.NormalizeStructure(false);
				if (this.multipartMixed == null)
				{
					this.multipartMixed = new MimePart("multipart/mixed");
					MimePart mimePart;
					if (this.multipartSigned == null)
					{
						mimePart = this.rootPart;
						this.SetRootPart(this.multipartMixed);
					}
					else
					{
						mimePart = (this.multipartSigned.FirstChild as MimePart);
						this.multipartSigned.ReplaceChild(this.multipartMixed, mimePart);
					}
					this.multipartMixed.AppendChild(mimePart);
				}
				MimePart mimePart2 = new MimePart();
				this.multipartMixed.AppendChild(mimePart2);
				int index;
				MimeAttachmentData attachmentData = this.GetAttachmentData(mimePart2, InternalAttachmentType.Regular, this.RootPart.Version, out index);
				attachmentData.Attachment = attachment;
				AttachmentCookie attachmentCookie = new AttachmentCookie(index, this);
				result = attachmentCookie;
			}
			return result;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00012B98 File Offset: 0x00010D98
		internal override bool AttachmentCollection_RemoveAttachment(AttachmentCookie cookie)
		{
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData dataAtPrivateIndex = this.mimeAttachments.GetDataAtPrivateIndex(cookie.Index);
				this.mimeAttachments.RemoveAtPrivateIndex(cookie.Index);
				bool flag = this.RemoveAttachment(dataAtPrivateIndex.AttachmentPart);
				dataAtPrivateIndex.Invalidate();
				result = flag;
			}
			return result;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00012C0C File Offset: 0x00010E0C
		internal override void AttachmentCollection_ClearAttachments()
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				while (0 < this.mimeAttachments.Count)
				{
					MimeAttachmentData dataAtPublicIndex = this.mimeAttachments.GetDataAtPublicIndex(0);
					this.RemoveAttachment(dataAtPublicIndex.AttachmentPart);
					int privateIndex = this.mimeAttachments.GetPrivateIndex(0);
					this.mimeAttachments.RemoveAtPrivateIndex(privateIndex);
					dataAtPublicIndex.Invalidate();
				}
				this.mimeAttachments.Clear();
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00012C98 File Offset: 0x00010E98
		internal override int AttachmentCollection_Count()
		{
			int count;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				count = this.mimeAttachments.Count;
			}
			return count;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00012CDC File Offset: 0x00010EDC
		internal override object AttachmentCollection_Indexer(int publicIndex)
		{
			object attachment;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData dataAtPublicIndex = this.mimeAttachments.GetDataAtPublicIndex(publicIndex);
				attachment = dataAtPublicIndex.Attachment;
			}
			return attachment;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00012D28 File Offset: 0x00010F28
		internal override AttachmentCookie AttachmentCollection_CacheAttachment(int publicIndex, object attachment)
		{
			AttachmentCookie result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData dataAtPublicIndex = this.mimeAttachments.GetDataAtPublicIndex(publicIndex);
				dataAtPublicIndex.Attachment = attachment;
				int privateIndex = this.mimeAttachments.GetPrivateIndex(publicIndex);
				AttachmentCookie attachmentCookie = new AttachmentCookie(privateIndex, this);
				result = attachmentCookie;
			}
			return result;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00012D90 File Offset: 0x00010F90
		internal bool RemoveAttachment(MimePart part)
		{
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				bool isSynchronized = this.IsSynchronized;
				bool flag = false;
				if (this.rootPart == part)
				{
					this.CreateEmptyMessage();
				}
				else
				{
					MimePart mimePart = part.Parent as MimePart;
					if (mimePart != null)
					{
						part.RemoveFromParent();
						flag = true;
						this.RemoveDegenerateMultiparts(mimePart);
					}
				}
				if (isSynchronized)
				{
					this.UpdateMimeVersion();
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00012E0C File Offset: 0x0001100C
		private void AddRelatedAttachment(MimePart part)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.multipartRelated == null)
				{
					MimePart mimePart = null;
					foreach (MimePart mimePart2 in this.bodyList)
					{
						if ("text/html" == mimePart2.ContentType)
						{
							mimePart = mimePart2;
							break;
						}
					}
					if (mimePart == null)
					{
						throw new InvalidOperationException(EmailMessageStrings.CanOnlyAddInlineAttachmentsToHtmlBody);
					}
					this.multipartRelated = new MimePart("multipart/related");
					this.InsertMultipart(this.multipartRelated, mimePart);
				}
				this.multipartRelated.AppendChild(part);
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00012ED4 File Offset: 0x000110D4
		private void AddRegularAttachment(MimePart part)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.multipartMixed == null)
				{
					this.multipartMixed = new MimePart("multipart/mixed");
					MimePart oldPart;
					if (this.RootPart == this.multipartSigned)
					{
						oldPart = (this.multipartSigned.FirstChild as MimePart);
					}
					else
					{
						oldPart = this.RootPart;
					}
					this.InsertMultipart(this.multipartMixed, oldPart);
				}
				this.multipartMixed.AppendChild(part);
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00012F68 File Offset: 0x00011168
		private void InsertMultipart(MimePart newContainer, MimePart oldPart)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (oldPart.Parent == null)
				{
					this.SetRootPart(newContainer);
				}
				else
				{
					oldPart.Parent.ReplaceChild(newContainer, oldPart);
				}
				newContainer.AppendChild(oldPart);
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00012FC8 File Offset: 0x000111C8
		internal override IBody GetBody()
		{
			IBody result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.Synchronize();
				if (this.UseCalendarBody)
				{
					result = this.calendarMessage;
				}
				else
				{
					result = this;
				}
			}
			return result;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00013018 File Offset: 0x00011218
		BodyFormat IBody.GetBodyFormat()
		{
			BodyFormat bodyFormat;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				bodyFormat = this.bodyData.BodyFormat;
			}
			return bodyFormat;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001305C File Offset: 0x0001125C
		string IBody.GetCharsetName()
		{
			string charsetName;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				charsetName = this.bodyData.CharsetName;
			}
			return charsetName;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000130A0 File Offset: 0x000112A0
		MimePart IBody.GetMimePart()
		{
			MimePart result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.bodyMimePart;
			}
			return result;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000130E0 File Offset: 0x000112E0
		Stream IBody.GetContentReadStream()
		{
			Stream result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.bodyMimePart == null)
				{
					result = DataStorage.NewEmptyReadStream();
				}
				else
				{
					result = this.bodyData.ConvertReadStreamFormat(Utility.GetContentReadStream(this.bodyMimePart));
				}
			}
			return result;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00013140 File Offset: 0x00011340
		bool IBody.TryGetContentReadStream(out Stream stream)
		{
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.bodyMimePart == null)
				{
					stream = null;
					result = false;
				}
				else
				{
					bool flag = this.bodyMimePart.TryGetContentReadStream(out stream);
					if (flag)
					{
						stream = this.bodyData.ConvertReadStreamFormat(stream);
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000131A8 File Offset: 0x000113A8
		Stream IBody.GetContentWriteStream(Charset charset)
		{
			Stream result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.bodyData.ReleaseStorage();
				bool isSynchronized = this.IsSynchronized;
				if (charset != null && charset != this.bodyData.Charset)
				{
					Charset charset2 = Utility.TranslateWriteStreamCharset(charset);
					this.SetPartCharset(this.bodyMimePart, charset2.Name);
					this.bodyData.SetNewCharset(charset2);
				}
				ContentTransferEncoding contentTransferEncoding = this.bodyMimePart.ContentTransferEncoding;
				if (contentTransferEncoding == ContentTransferEncoding.Unknown || ContentTransferEncoding.SevenBit == contentTransferEncoding || ContentTransferEncoding.EightBit == contentTransferEncoding)
				{
					this.bodyMimePart.UpdateTransferEncoding(ContentTransferEncoding.QuotedPrintable);
				}
				this.bodyMimePart.SetStorage(null, 0L, 0L);
				this.bodyWriteStreamMimePart = this.bodyMimePart;
				if (isSynchronized)
				{
					this.UpdateMimeVersion();
				}
				Stream stream = new BodyContentWriteStream(this);
				stream = this.bodyData.ConvertWriteStreamFormat(stream, charset);
				result = stream;
			}
			return result;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001328C File Offset: 0x0001148C
		void IBody.SetNewContent(DataStorage storage, long start, long end)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.Synchronize();
				this.bodyWriteStreamMimePart.SetStorage(storage, start, end);
				if (this.bodyWriteStreamMimePart == this.bodyMimePart)
				{
					this.bodyData.SetStorage(storage, start, end);
					this.UpdateMimeVersion();
					this.BodyModified();
				}
				this.bodyWriteStreamMimePart = null;
				this.UpdateMimeVersion();
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001330C File Offset: 0x0001150C
		bool IBody.ConversionNeeded(int[] validCodepages)
		{
			return false;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00013310 File Offset: 0x00011510
		private void PickBestBody()
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				this.Synchronize();
				MimePart mimePart = null;
				MimePart mimePart2 = null;
				MimePart mimePart3 = null;
				foreach (MimePart mimePart4 in this.bodyList)
				{
					string contentType = mimePart4.ContentType;
					if (contentType == "text/html")
					{
						this.SetBody(mimePart4, BodyFormat.Html, InternalBodyFormat.Html);
						return;
					}
					if (mimePart2 == null && contentType == "text/enriched")
					{
						mimePart2 = mimePart4;
					}
					if (mimePart == null && contentType == "text/plain")
					{
						mimePart = mimePart4;
					}
					if (mimePart3 == null && contentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase))
					{
						mimePart3 = mimePart4;
					}
				}
				if (mimePart2 != null)
				{
					this.SetBody(mimePart2, BodyFormat.Html, InternalBodyFormat.Enriched);
				}
				else if (mimePart != null)
				{
					this.SetBody(mimePart, BodyFormat.Text, InternalBodyFormat.Text);
				}
				else if (mimePart3 != null)
				{
					this.SetBody(mimePart3, BodyFormat.Text, InternalBodyFormat.Text);
				}
				else
				{
					this.bodyData.SetFormat(BodyFormat.None, InternalBodyFormat.None, null);
					this.bodyData.SetStorage(null, 0L, 0L);
				}
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00013438 File Offset: 0x00011638
		private void SetBody(MimePart part, BodyFormat format, InternalBodyFormat internalFormat)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				this.bodyMimePart = part;
				bool flag;
				Charset charset = PureMimeMessage.DetermineTextPartCharset(part, this.defaultBodyCharset, out flag);
				this.bodyData.SetFormat(format, internalFormat, charset);
				this.bodyData.SetStorage(part.Storage, part.DataStart, part.DataEnd);
				if (flag || FeInboundCharsetDetector.IsSupportedFarEastCharset(charset) || charset.CodePage == 20127)
				{
					Stream stream = null;
					try
					{
						if (this.bodyMimePart.TryGetContentReadStream(out stream))
						{
							this.bodyData.ValidateCharset(flag, stream);
						}
					}
					finally
					{
						if (stream != null)
						{
							stream.Dispose();
							stream = null;
						}
					}
				}
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00013500 File Offset: 0x00011700
		internal void BodyModified()
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				Charset charset = this.bodyData.Charset;
				bool flag = BodyFormat.Text != this.bodyData.BodyFormat;
				bool flag2 = BodyFormat.Html == this.bodyData.BodyFormat && this.bodyData.InternalBodyFormat != InternalBodyFormat.Enriched;
				foreach (MimePart mimePart in this.bodyList)
				{
					if (mimePart != this.bodyMimePart)
					{
						if (mimePart == this.calendarPart && this.HasCalendar)
						{
							BodyData bodyData = this.calendarMessage.BodyData;
							if (bodyData != null && bodyData.HasContent)
							{
								if (bodyData.Charset != Charset.UTF8 && bodyData.Charset != charset)
								{
									this.SetPartCharset(mimePart, Charset.UTF8.Name);
									bodyData.SetNewCharset(Charset.UTF8);
									this.calendarMessage.TargetCharset = Charset.UTF8;
								}
								DataStorage dataStorage;
								long num;
								long num2;
								this.bodyData.GetStorage(InternalBodyFormat.Text, bodyData.Charset, out dataStorage, out num, out num2);
								bodyData.SetStorage(dataStorage, num, num2);
								dataStorage.Release();
								this.calendarMessage.TouchBody();
							}
						}
						else
						{
							string contentType = mimePart.ContentType;
							DataStorage dataStorage;
							long num;
							long num2;
							if (flag && contentType == "text/plain")
							{
								flag = false;
								this.SetPartCharset(mimePart, this.bodyData.CharsetName);
								this.bodyData.GetStorage(InternalBodyFormat.Text, charset, out dataStorage, out num, out num2);
							}
							else if (flag2 && contentType == "text/enriched")
							{
								flag2 = false;
								this.SetPartCharset(mimePart, this.bodyData.CharsetName);
								this.bodyData.GetStorage(InternalBodyFormat.Enriched, charset, out dataStorage, out num, out num2);
							}
							else
							{
								bool isSynchronized = this.IsSynchronized;
								mimePart.RemoveFromParent();
								if (isSynchronized)
								{
									this.UpdateMimeVersion();
									continue;
								}
								continue;
							}
							bool contentDirty = mimePart.ContentDirty;
							bool isSynchronized2 = this.IsSynchronized;
							Utility.SynchronizeEncoding(this.bodyData, mimePart);
							mimePart.SetStorage(dataStorage, num, num2);
							dataStorage.Release();
							ContentTransferEncoding contentTransferEncoding = mimePart.ContentTransferEncoding;
							if (contentTransferEncoding == ContentTransferEncoding.Unknown || contentTransferEncoding == ContentTransferEncoding.SevenBit || contentTransferEncoding == ContentTransferEncoding.EightBit)
							{
								mimePart.UpdateTransferEncoding(ContentTransferEncoding.QuotedPrintable);
							}
							if (isSynchronized2)
							{
								this.UpdateMimeVersion();
							}
							mimePart.ContentDirty = contentDirty;
						}
					}
				}
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001378C File Offset: 0x0001198C
		private MimeAttachmentData DataFromCookie(AttachmentCookie cookie)
		{
			MimeAttachmentData result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				MimeAttachmentData dataAtPrivateIndex = this.mimeAttachments.GetDataAtPrivateIndex(cookie.Index);
				if (dataAtPrivateIndex == null)
				{
					throw new InvalidOperationException(EmailMessageStrings.AttachmentRemovedFromMessage);
				}
				result = dataAtPrivateIndex;
			}
			return result;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000137E8 File Offset: 0x000119E8
		internal override MimePart Attachment_GetMimePart(AttachmentCookie cookie)
		{
			MimePart result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				MimePart mimePart = mimeAttachmentData.DataPart ?? mimeAttachmentData.AttachmentPart;
				result = mimePart;
			}
			return result;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001383C File Offset: 0x00011A3C
		internal override string Attachment_GetContentType(AttachmentCookie cookie)
		{
			string result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				if (PureMimeMessage.IsAppleSingleAttachment(mimeAttachmentData))
				{
					result = "application/octet-stream";
				}
				else
				{
					MimePart mimePart = mimeAttachmentData.DataPart ?? mimeAttachmentData.AttachmentPart;
					result = mimePart.ContentType;
				}
			}
			return result;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000138A4 File Offset: 0x00011AA4
		internal override void Attachment_SetContentType(AttachmentCookie cookie, string contentType)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				this.PromoteIfAppleDoubleAttachment(mimeAttachmentData);
				ContentTypeHeader contentTypeHeader = mimeAttachmentData.AttachmentPart.Headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
				if (contentTypeHeader == null)
				{
					contentTypeHeader = new ContentTypeHeader();
					mimeAttachmentData.AttachmentPart.Headers.AppendChild(contentTypeHeader);
				}
				contentTypeHeader.Value = contentType;
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00013924 File Offset: 0x00011B24
		internal override AttachmentMethod Attachment_GetAttachmentMethod(AttachmentCookie cookie)
		{
			AttachmentMethod result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				MimePart mimePart = mimeAttachmentData.DataPart ?? mimeAttachmentData.AttachmentPart;
				if (mimePart.IsEmbeddedMessage)
				{
					result = AttachmentMethod.EmbeddedMessage;
				}
				else
				{
					result = AttachmentMethod.AttachByValue;
				}
			}
			return result;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00013984 File Offset: 0x00011B84
		internal override InternalAttachmentType Attachment_GetAttachmentType(AttachmentCookie cookie)
		{
			InternalAttachmentType internalAttachmentType;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				internalAttachmentType = mimeAttachmentData.InternalAttachmentType;
			}
			return internalAttachmentType;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000139CC File Offset: 0x00011BCC
		internal override void Attachment_SetAttachmentType(AttachmentCookie cookie, InternalAttachmentType attachmentType)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				this.PromoteIfAppleDoubleAttachment(mimeAttachmentData);
				if (mimeAttachmentData.InternalAttachmentType != attachmentType)
				{
					mimeAttachmentData.InternalAttachmentType = attachmentType;
					if (InternalAttachmentType.Related == mimeAttachmentData.InternalAttachmentType)
					{
						this.RemoveAttachment(mimeAttachmentData.AttachmentPart);
						this.AddRelatedAttachment(mimeAttachmentData.AttachmentPart);
					}
					else
					{
						this.RemoveAttachment(mimeAttachmentData.AttachmentPart);
						this.AddRegularAttachment(mimeAttachmentData.AttachmentPart);
					}
				}
				this.Attachment_SetContentDisposition(cookie, (attachmentType == InternalAttachmentType.Regular) ? "attachment" : "inline");
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00013A74 File Offset: 0x00011C74
		internal override EmailMessage Attachment_GetEmbeddedMessage(AttachmentCookie cookie)
		{
			EmailMessage result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				MimePart mimePart = mimeAttachmentData.DataPart ?? mimeAttachmentData.AttachmentPart;
				if (!mimePart.IsEmbeddedMessage)
				{
					result = null;
				}
				else
				{
					if (mimePart.FirstChild == null)
					{
						bool isSynchronized = this.IsSynchronized;
						MimePart mimePart2 = new MimePart("text/plain");
						mimePart2.Headers.AppendChild(new AsciiTextHeader("MIME-Version", "1.0"));
						mimePart.AppendChild(mimePart2);
						if (isSynchronized)
						{
							this.UpdateMimeVersion();
						}
					}
					if (mimeAttachmentData.EmbeddedMessage == null || mimeAttachmentData.EmbeddedMessage.RootPart != mimePart.FirstChild)
					{
						MimeTnefMessage message = new MimeTnefMessage((MimePart)mimePart.FirstChild);
						mimeAttachmentData.EmbeddedMessage = new EmailMessage(message);
					}
					result = mimeAttachmentData.EmbeddedMessage;
				}
			}
			return result;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00013B60 File Offset: 0x00011D60
		internal override void Attachment_SetEmbeddedMessage(AttachmentCookie cookie, EmailMessage value)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (value.RootPart == null)
				{
					throw new InvalidOperationException(EmailMessageStrings.CannotAttachEmbeddedMapiMessageToMime);
				}
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				this.PromoteIfAppleDoubleAttachment(mimeAttachmentData);
				if (value != mimeAttachmentData.EmbeddedMessage)
				{
					if (mimeAttachmentData.EmbeddedMessage == null)
					{
						MimeTnefMessage message = new MimeTnefMessage(new MimePart());
						mimeAttachmentData.EmbeddedMessage = new EmailMessage(message);
					}
					value.CopyTo(mimeAttachmentData.EmbeddedMessage);
					mimeAttachmentData.AttachmentPart.RemoveAll();
					mimeAttachmentData.AttachmentPart.AppendChild(mimeAttachmentData.EmbeddedMessage.RootPart);
					Utility.UpdateTransferEncoding(mimeAttachmentData.AttachmentPart, mimeAttachmentData.EmbeddedMessage.RootPart.ContentTransferEncoding);
				}
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00013C2C File Offset: 0x00011E2C
		internal override string Attachment_GetFileName(AttachmentCookie cookie, ref int sequenceNumber)
		{
			string fileName;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				if (mimeAttachmentData.FileName != null)
				{
					fileName = mimeAttachmentData.FileName;
				}
				else
				{
					string rawFileName = Utility.GetRawFileName(mimeAttachmentData.AttachmentPart);
					mimeAttachmentData.FileName = Utility.SanitizeOrRegenerateFileName(rawFileName, ref sequenceNumber);
					fileName = mimeAttachmentData.FileName;
				}
			}
			return fileName;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00013C9C File Offset: 0x00011E9C
		internal override void Attachment_SetFileName(AttachmentCookie cookie, string value)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				this.PromoteIfAppleDoubleAttachment(mimeAttachmentData);
				InternalAttachmentType internalAttachmentType = this.Attachment_GetAttachmentType(cookie);
				AttachmentType attachmentType = (internalAttachmentType == InternalAttachmentType.Regular) ? AttachmentType.Regular : AttachmentType.Inline;
				Utility.SetFileName(mimeAttachmentData.AttachmentPart, attachmentType, value);
				mimeAttachmentData.FileName = value;
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00013D04 File Offset: 0x00011F04
		internal override string Attachment_GetContentDisposition(AttachmentCookie cookie)
		{
			string headerValue;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				ContentDispositionHeader header = mimeAttachmentData.AttachmentPart.Headers.FindFirst(HeaderId.ContentDisposition) as ContentDispositionHeader;
				headerValue = Utility.GetHeaderValue(header);
			}
			return headerValue;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00013D64 File Offset: 0x00011F64
		internal override void Attachment_SetContentDisposition(AttachmentCookie cookie, string value)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				this.PromoteIfAppleDoubleAttachment(mimeAttachmentData);
				Header header = mimeAttachmentData.AttachmentPart.Headers.FindFirst(HeaderId.ContentDisposition);
				if (header == null)
				{
					header = Header.Create(HeaderId.ContentDisposition);
					mimeAttachmentData.AttachmentPart.Headers.AppendChild(header);
				}
				header.Value = value;
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00013DE0 File Offset: 0x00011FE0
		internal override bool Attachment_IsAppleDouble(AttachmentCookie cookie)
		{
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				bool flag = mimeAttachmentData.AttachmentPart.ContentType == "multipart/appledouble";
				result = flag;
			}
			return result;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00013E38 File Offset: 0x00012038
		internal override Stream Attachment_GetContentReadStream(AttachmentCookie cookie)
		{
			Stream result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				Stream contentReadStream2;
				if (PureMimeMessage.IsAppleSingleAttachment(mimeAttachmentData))
				{
					Stream contentReadStream = Utility.GetContentReadStream(mimeAttachmentData.AttachmentPart);
					MimeAppleTranscoder.GetDataForkFromAppleSingle(contentReadStream, out contentReadStream2);
				}
				else
				{
					MimePart part = mimeAttachmentData.DataPart ?? mimeAttachmentData.AttachmentPart;
					contentReadStream2 = Utility.GetContentReadStream(part);
				}
				result = contentReadStream2;
			}
			return result;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00013EB4 File Offset: 0x000120B4
		internal override bool Attachment_TryGetContentReadStream(AttachmentCookie cookie, out Stream result)
		{
			bool result2;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				MimePart mimePart = mimeAttachmentData.DataPart ?? mimeAttachmentData.AttachmentPart;
				result2 = mimePart.TryGetContentReadStream(out result);
			}
			return result2;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00013F0C File Offset: 0x0001210C
		internal override Stream Attachment_GetContentWriteStream(AttachmentCookie cookie)
		{
			Stream contentWriteStream;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				this.PromoteIfAppleDoubleAttachment(mimeAttachmentData);
				ContentTransferEncoding contentTransferEncoding;
				if (mimeAttachmentData.AttachmentPart.IsEmbeddedMessage)
				{
					contentTransferEncoding = ContentTransferEncoding.SevenBit;
					Utility.UpdateTransferEncoding(mimeAttachmentData.AttachmentPart, contentTransferEncoding);
				}
				else
				{
					contentTransferEncoding = mimeAttachmentData.AttachmentPart.ContentTransferEncoding;
					if (contentTransferEncoding == ContentTransferEncoding.Unknown || ContentTransferEncoding.SevenBit == contentTransferEncoding || ContentTransferEncoding.EightBit == contentTransferEncoding)
					{
						contentTransferEncoding = ContentTransferEncoding.Base64;
					}
				}
				if (PureMimeMessage.IsAppleSingleAttachment(mimeAttachmentData))
				{
					this.Attachment_SetContentType(cookie, "application/octet-stream");
				}
				contentWriteStream = mimeAttachmentData.AttachmentPart.GetContentWriteStream(contentTransferEncoding);
			}
			return contentWriteStream;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00013FA8 File Offset: 0x000121A8
		internal override int Attachment_GetRenderingPosition(AttachmentCookie cookie)
		{
			return -1;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00013FAC File Offset: 0x000121AC
		internal override string Attachment_GetAttachContentID(AttachmentCookie cookie)
		{
			string headerValue;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				Header header = mimeAttachmentData.AttachmentPart.Headers.FindFirst(HeaderId.ContentId);
				headerValue = Utility.GetHeaderValue(header);
			}
			return headerValue;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00014004 File Offset: 0x00012204
		internal override string Attachment_GetAttachContentLocation(AttachmentCookie cookie)
		{
			string headerValue;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				Header header = mimeAttachmentData.AttachmentPart.Headers.FindFirst(HeaderId.ContentLocation);
				headerValue = Utility.GetHeaderValue(header);
			}
			return headerValue;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001405C File Offset: 0x0001225C
		internal override byte[] Attachment_GetAttachRendering(AttachmentCookie cookie)
		{
			return null;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001405F File Offset: 0x0001225F
		internal override int Attachment_GetAttachmentFlags(AttachmentCookie cookie)
		{
			return 0;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00014062 File Offset: 0x00012262
		internal override bool Attachment_GetAttachHidden(AttachmentCookie cookie)
		{
			return false;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00014068 File Offset: 0x00012268
		internal override int Attachment_GetHashCode(AttachmentCookie cookie)
		{
			int hashCode;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				hashCode = mimeAttachmentData.AttachmentPart.GetHashCode();
			}
			return hashCode;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000140B4 File Offset: 0x000122B4
		internal override void Attachment_Dispose(AttachmentCookie cookie)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeAttachmentData mimeAttachmentData = this.DataFromCookie(cookie);
				if (mimeAttachmentData.EmbeddedMessage != null)
				{
					mimeAttachmentData.EmbeddedMessage.Dispose();
					mimeAttachmentData.EmbeddedMessage = null;
				}
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001410C File Offset: 0x0001230C
		private void PromoteIfAppleDoubleAttachment(MimeAttachmentData data)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				MimePart mimePart;
				MimePart mimePart2;
				if (Utility.TryGetAppleDoubleParts(data.AttachmentPart, out mimePart, out mimePart2))
				{
					bool isSynchronized = this.IsSynchronized;
					MimePart mimePart3 = data.AttachmentPart.Parent as MimePart;
					if (mimePart3 == null)
					{
						this.SetRootPart(mimePart);
					}
					else
					{
						mimePart.RemoveFromParent();
						mimePart3.ReplaceChild(mimePart, data.AttachmentPart);
					}
					data.AttachmentPart = mimePart;
					if (isSynchronized)
					{
						this.UpdateMimeVersion();
					}
				}
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000141A0 File Offset: 0x000123A0
		internal override void CopyTo(MessageImplementation destination)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				PureMimeMessage pureMimeMessage = (PureMimeMessage)destination;
				using (ThreadAccessGuard.EnterPublic(pureMimeMessage.accessToken))
				{
					base.CopyTo(destination);
					pureMimeMessage.ToRecipients = null;
					pureMimeMessage.CcRecipients = null;
					pureMimeMessage.BccRecipients = null;
					pureMimeMessage.ReplyToRecipients = null;
					pureMimeMessage.FromRecipient = null;
					pureMimeMessage.SenderRecipient = null;
					pureMimeMessage.DntRecipient = null;
					pureMimeMessage.mimeAttachments = null;
					pureMimeMessage.messageType = MessageType.Undefined;
					pureMimeMessage.bodyStructure = BodyStructure.Undefined;
					pureMimeMessage.bodyTypes = BodyTypes.None;
					pureMimeMessage.version = -1;
					pureMimeMessage.placeholderBody = null;
					pureMimeMessage.multipartRelated = null;
					pureMimeMessage.multipartMixed = null;
					pureMimeMessage.multipartAlternative = null;
					pureMimeMessage.multipartSigned = null;
					pureMimeMessage.signaturePart = null;
					pureMimeMessage.messageClass = "IPM.Note";
					pureMimeMessage.tnefPart = null;
					pureMimeMessage.calendarPart = null;
					pureMimeMessage.bodyList = null;
					this.rootPart.CopyTo(pureMimeMessage.rootPart);
				}
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000142B8 File Offset: 0x000124B8
		private void RemoveDegenerateMultiparts(MimePart parentPart)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				for (;;)
				{
					MimePart mimePart = parentPart.Parent as MimePart;
					if (parentPart.FirstChild == null)
					{
						if (mimePart == null || mimePart.IsEmbeddedMessage)
						{
							break;
						}
						parentPart.RemoveFromParent();
					}
					else
					{
						if (!Utility.HasExactlyOneChild(parentPart))
						{
							goto IL_C2;
						}
						if (mimePart == null)
						{
							this.SetRootPart(parentPart.FirstChild as MimePart);
						}
						else
						{
							MimeNode firstChild = parentPart.FirstChild;
							firstChild.RemoveFromParent();
							mimePart.ReplaceChild(firstChild, parentPart);
						}
					}
					if (this.multipartMixed == parentPart)
					{
						this.multipartMixed = null;
					}
					else if (this.multipartRelated == parentPart)
					{
						this.multipartRelated = null;
					}
					else if (this.multipartAlternative == parentPart)
					{
						this.multipartAlternative = null;
					}
					else if (this.multipartSigned == parentPart)
					{
						this.multipartSigned = null;
					}
					parentPart = mimePart;
					if (parentPart == null)
					{
						goto IL_C2;
					}
				}
				this.CreateEmptyMessage();
				IL_C2:;
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000143A4 File Offset: 0x000125A4
		internal void UpdateMimeVersion()
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.version = this.Version;
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000143E8 File Offset: 0x000125E8
		internal void RebuildMessage()
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.Synchronize();
				this.multipartMixed = null;
				this.multipartRelated = null;
				this.multipartAlternative = null;
				MimePart mimePart = this.NormalizeBodies();
				mimePart = this.NormalizeAttachments(mimePart);
				if (mimePart == null)
				{
					this.CreateEmptyMessage();
					mimePart = this.RootPart;
				}
				if (this.multipartSigned != null)
				{
					mimePart.RemoveFromParent();
					this.multipartSigned.InsertAfter(mimePart, null);
					if (this.multipartSigned.FirstChild.NextSibling != this.signaturePart)
					{
						this.multipartSigned.RemoveChild(this.multipartSigned.FirstChild.NextSibling);
					}
					mimePart = this.multipartSigned;
				}
				this.SetRootPart(mimePart);
				this.rebuildStructureAtNextOpportunity = false;
				this.Synchronize();
				this.UpdateMimeVersion();
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000144CC File Offset: 0x000126CC
		internal override void Synchronize()
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (!this.IsSynchronized)
				{
					this.ResetSynchronization();
					this.messageClass = "IPM.Note";
					this.version = -1;
					this.messageType = MessageType.Undefined;
					this.bodyStructure = BodyStructure.Undefined;
					this.bodyTypes = BodyTypes.None;
					this.DetectMessageType();
					this.UpdateMimeVersion();
					this.RetireOldMimeAttachmentData();
					this.PickBestBody();
				}
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00014550 File Offset: 0x00012750
		private void RetireOldMimeAttachmentData()
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				for (int i = 0; i < this.mimeAttachments.InternalList.Count; i++)
				{
					MimeAttachmentData mimeAttachmentData = this.mimeAttachments.InternalList[i];
					if (mimeAttachmentData != null && !mimeAttachmentData.Referenced)
					{
						this.mimeAttachments.RemoveAtPrivateIndex(i);
					}
				}
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000145CC File Offset: 0x000127CC
		[Conditional("DEBUG")]
		private void ValidateStructure()
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				MessageFlags messageFlags = MessageTypeTable.GetMessageFlags(this.messageType);
				if ((messageFlags & MessageFlags.Normal) > MessageFlags.None)
				{
					int count = this.bodyList.Count;
					foreach (MimeAttachmentData mimeAttachmentData in this.mimeAttachments.InternalList)
					{
						if (mimeAttachmentData != null && InternalAttachmentType.Related != mimeAttachmentData.InternalAttachmentType && this.multipartMixed == null)
						{
							MimeNode parent = mimeAttachmentData.AttachmentPart.Parent;
						}
					}
				}
				MimePart mimePart = this.multipartAlternative;
				MimePart mimePart2 = this.multipartRelated;
				MimePart mimePart3 = this.multipartMixed;
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00014698 File Offset: 0x00012898
		private void FindBodiesAndAttachmentsHeuristically(MimePart root)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				this.ResetDetection();
				this.messageType = MessageType.Unknown;
				this.multipartMixed = null;
				this.multipartAlternative = null;
				this.multipartRelated = null;
				bool flag = true;
				MimePart mimePart = null;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = true;
				bool flag5 = true;
				bool flag6 = true;
				MimePart.SubtreeEnumerator subtreeEnumerator = new MimePart.SubtreeEnumerator(root, MimePart.SubtreeEnumerationOptions.RevisitParent, false);
				while (subtreeEnumerator.MoveNext())
				{
					MimePart mimePart2 = subtreeEnumerator.Current;
					string contentType = mimePart2.ContentType;
					if (!subtreeEnumerator.FirstVisit)
					{
						if (subtreeEnumerator.LastVisit)
						{
							if (mimePart2 == this.multipartAlternative)
							{
								flag2 = false;
							}
							if (mimePart2 == this.multipartRelated)
							{
								flag3 = false;
							}
						}
					}
					else
					{
						flag = (mimePart2 == this.RootPart || (flag && mimePart2 == mimePart2.Parent.FirstChild) || mimePart2 == mimePart);
						contentType == "multipart/signed";
						bool match = contentType == "multipart/mixed";
						bool match2 = contentType == "multipart/alternative";
						bool flag7 = contentType == "multipart/related";
						bool flag8 = contentType == "multipart/appledouble";
						contentType == "multipart/report";
						MimePart dataPart = null;
						MimePart mimePart3;
						if (flag8 && !Utility.TryGetAppleDoubleParts(mimePart2, out dataPart, out mimePart3))
						{
							flag8 = false;
							flag2 = false;
						}
						if (mimePart2 != this.placeholderBody && (!mimePart2.IsMultipart || flag8))
						{
							ContentDispositionHeader contentDispositionHeader = mimePart2.Headers.FindFirst(HeaderId.ContentDisposition) as ContentDispositionHeader;
							bool flag9 = contentDispositionHeader == null || "inline".Equals(Utility.GetHeaderValue(contentDispositionHeader), StringComparison.OrdinalIgnoreCase);
							bool flag10 = flag && Utility.IsBodyContentType(contentType);
							bool flag11 = this.multipartAlternative != null && this.multipartAlternative == mimePart2.Parent;
							bool flag12 = flag2 && mimePart2.Parent != null && this.multipartAlternative == mimePart2.Parent.Parent && (mimePart2 == mimePart || (mimePart == null && mimePart2 == mimePart2.Parent.FirstChild));
							if (flag9 && (flag10 || flag11 || flag12))
							{
								this.bodyList.Add(mimePart2);
							}
							else if (this.multipartSigned == null || mimePart2.Parent != this.multipartSigned)
							{
								InternalAttachmentType internalAttachmentType = flag3 ? InternalAttachmentType.Related : InternalAttachmentType.Regular;
								if (flag8)
								{
									this.GetAttachmentData(mimePart2, dataPart, this.Version);
									subtreeEnumerator.SkipChildren();
								}
								else
								{
									if (internalAttachmentType == InternalAttachmentType.Regular)
									{
										internalAttachmentType = Utility.CheckContentDisposition(mimePart2);
									}
									this.GetAttachmentData(mimePart2, internalAttachmentType, this.Version);
								}
							}
						}
						else if (flag7)
						{
							if ((flag || mimePart2.Parent == this.multipartAlternative) && PureMimeMessage.CheckKeyPart(mimePart2, flag7, ref flag6, ref this.multipartRelated) && !subtreeEnumerator.LastVisit)
							{
								flag3 = true;
							}
							mimePart = Utility.GetStartChild(mimePart2);
							if (mimePart != null)
							{
								flag = false;
							}
						}
						PureMimeMessage.CheckKeyPart(mimePart2, match, ref flag4, ref this.multipartMixed);
						if (flag && PureMimeMessage.CheckKeyPart(mimePart2, match2, ref flag5, ref this.multipartAlternative) && !subtreeEnumerator.LastVisit)
						{
							flag2 = true;
						}
					}
				}
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000149B4 File Offset: 0x00012BB4
		private bool LoadCalendar()
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.calendarMessage != null)
				{
					if (!this.calendarPart.ContentDirty)
					{
						return true;
					}
					this.calendarMessage = null;
				}
				this.calendarPart.ContentDirty = false;
				bool flag;
				PureCalendarMessage pureCalendarMessage = new PureCalendarMessage(this, this.calendarPart, PureMimeMessage.DetermineTextPartCharset(this.calendarPart, Charset.UTF8, out flag));
				if (!pureCalendarMessage.Load())
				{
					result = false;
				}
				else
				{
					this.calendarMessage = pureCalendarMessage;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00014A4C File Offset: 0x00012C4C
		internal override IMapiPropertyAccess MapiProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00014A50 File Offset: 0x00012C50
		private AddressHeader GetRecipientHeader(RecipientType recipientType, ref object cached)
		{
			AddressHeader result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				AddressHeader addressHeader = cached as AddressHeader;
				if (addressHeader == null)
				{
					HeaderId headerId = PureMimeMessage.GetHeaderId(recipientType);
					addressHeader = this.GetRecipientHeader(headerId);
					cached = addressHeader;
				}
				result = addressHeader;
			}
			return result;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00014AA8 File Offset: 0x00012CA8
		private AddressHeader GetRecipientHeader(HeaderId headerId)
		{
			AddressHeader result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				AddressHeader addressHeader = null;
				Header header2;
				for (Header header = this.rootPart.Headers.FindFirst(headerId); header != null; header = header2)
				{
					header2 = this.rootPart.Headers.FindNext(header);
					if (addressHeader == null)
					{
						addressHeader = (header as AddressHeader);
					}
					else
					{
						header.RemoveFromParent();
						foreach (MimeNode child in header)
						{
							Utility.MoveChildToNewParent(addressHeader, child);
						}
					}
				}
				if (addressHeader == null)
				{
					addressHeader = (Header.Create(headerId) as AddressHeader);
					this.rootPart.Headers.AppendChild(addressHeader);
				}
				result = addressHeader;
			}
			return result;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00014B84 File Offset: 0x00012D84
		private EmailRecipient GetRecipient(HeaderId headerId)
		{
			EmailRecipient result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				AddressHeader addressHeader = Utility.FindLastHeader(this.rootPart, headerId) as AddressHeader;
				if (addressHeader == null)
				{
					result = null;
				}
				else
				{
					foreach (MimeNode mimeNode in addressHeader)
					{
						MimeRecipient mimeRecipient = mimeNode as MimeRecipient;
						if (mimeRecipient == null)
						{
							MimeGroup mimeGroup = mimeNode as MimeGroup;
							mimeRecipient = (mimeGroup.FirstChild as MimeRecipient);
							if (mimeRecipient == null)
							{
								continue;
							}
						}
						return new EmailRecipient(mimeRecipient);
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00014C3C File Offset: 0x00012E3C
		private void SetRecipient(HeaderId headerId, EmailRecipient value)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				bool isSynchronized = this.IsSynchronized;
				AddressHeader addressHeader = this.GetLastHeaderRemoveDuplicates(headerId) as AddressHeader;
				if (value != null)
				{
					if (addressHeader == null)
					{
						addressHeader = (Header.Create(headerId) as AddressHeader);
						this.rootPart.Headers.AppendChild(addressHeader);
					}
					addressHeader.RemoveAll();
					addressHeader.AppendChild(value.MimeRecipient);
				}
				else if (addressHeader != null)
				{
					addressHeader.RemoveFromParent();
				}
				if (isSynchronized)
				{
					this.UpdateMimeVersion();
				}
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00014CD0 File Offset: 0x00012ED0
		private string GetHeaderString(HeaderId headerId)
		{
			string headerValue;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				Header header = Utility.FindLastHeader(this.rootPart, headerId);
				headerValue = Utility.GetHeaderValue(header);
			}
			return headerValue;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00014D1C File Offset: 0x00012F1C
		private DateTime GetHeaderDate(HeaderId headerId)
		{
			DateTime result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				DateHeader dateHeader = Utility.FindLastHeader(this.rootPart, headerId) as DateHeader;
				if (dateHeader == null)
				{
					result = DateTime.MinValue;
				}
				else
				{
					result = dateHeader.UtcDateTime;
				}
			}
			return result;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00014D78 File Offset: 0x00012F78
		private bool TryGetHeaderEnum(HeaderId headerId, EnumUtility.StringIntPair[] map, ref int result)
		{
			bool result2;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				string headerString = this.GetHeaderString(headerId);
				result2 = EnumUtility.TryGetInt(map, headerString, ref result);
			}
			return result2;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00014DC0 File Offset: 0x00012FC0
		private Header GetLastHeaderRemoveDuplicates(HeaderId headerId)
		{
			Header result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				Header header = null;
				for (Header header2 = this.rootPart.Headers.FindFirst(headerId); header2 != null; header2 = this.rootPart.Headers.FindNext(header2))
				{
					if (header != null)
					{
						header.RemoveFromParent();
					}
					header = header2;
				}
				result = header;
			}
			return result;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00014E30 File Offset: 0x00013030
		private Header GetLastHeader(HeaderId headerId)
		{
			Header result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				Header header = null;
				for (Header header2 = this.rootPart.Headers.FindFirst(headerId); header2 != null; header2 = this.rootPart.Headers.FindNext(header2))
				{
					header = header2;
				}
				result = header;
			}
			return result;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00014E98 File Offset: 0x00013098
		private void SetHeaderString(HeaderId headerId, string value)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				bool isSynchronized = this.IsSynchronized;
				Header header = this.GetLastHeaderRemoveDuplicates(headerId);
				if (value != null)
				{
					if (header == null)
					{
						header = Header.Create(headerId);
						this.rootPart.Headers.AppendChild(header);
					}
					header.Value = value;
				}
				else if (header != null)
				{
					header.RemoveFromParent();
				}
				if (isSynchronized)
				{
					this.UpdateMimeVersion();
				}
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00014F18 File Offset: 0x00013118
		private void SetHeaderDate(HeaderId headerId, DateTime value)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				bool isSynchronized = this.IsSynchronized;
				DateHeader dateHeader = this.GetLastHeaderRemoveDuplicates(headerId) as DateHeader;
				if (value != DateTime.MinValue)
				{
					if (dateHeader == null)
					{
						dateHeader = (Header.Create(headerId) as DateHeader);
						this.rootPart.Headers.AppendChild(dateHeader);
					}
					dateHeader.DateTime = value;
				}
				else if (dateHeader != null)
				{
					dateHeader.RemoveFromParent();
				}
				if (isSynchronized)
				{
					this.UpdateMimeVersion();
				}
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00014FAC File Offset: 0x000131AC
		private void SetHeaderEnum(HeaderId headerId, EnumUtility.StringIntPair[] map, int value)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				bool isSynchronized = this.IsSynchronized;
				if (value == 0)
				{
					this.RemoveHeaders(headerId);
				}
				else
				{
					string @string = EnumUtility.GetString(map, value);
					this.SetHeaderString(headerId, @string);
				}
				if (isSynchronized)
				{
					this.UpdateMimeVersion();
				}
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00015010 File Offset: 0x00013210
		private void RemoveHeaders(HeaderId headerId)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				Header header2;
				for (Header header = this.rootPart.Headers.FindFirst(headerId); header != null; header = header2)
				{
					header2 = this.rootPart.Headers.FindNext(header);
					header.RemoveFromParent();
				}
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00015078 File Offset: 0x00013278
		private string CreateMessageId(bool allowUTF8)
		{
			string result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				string text = Guid.NewGuid().ToString();
				int num = PureMimeMessage.maxMessageIdLength - text.Length - 3;
				string text2 = null;
				try
				{
					string dnsPhysicalFullyQualifiedDomainName = ComputerInformation.DnsPhysicalFullyQualifiedDomainName;
					if (MimeAddressParser.IsValidDomain(dnsPhysicalFullyQualifiedDomainName, 0, false, allowUTF8) && dnsPhysicalFullyQualifiedDomainName.Length <= num)
					{
						text2 = dnsPhysicalFullyQualifiedDomainName;
					}
				}
				catch (Exception)
				{
					text2 = null;
				}
				if (string.IsNullOrEmpty(text2))
				{
					text2 = "localhost";
				}
				StringBuilder stringBuilder = new StringBuilder(text.Length + text2.Length + 3);
				stringBuilder.Append('<');
				stringBuilder.Append(text);
				stringBuilder.Append('@');
				stringBuilder.Append(text2);
				stringBuilder.Append('>');
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001518C File Offset: 0x0001338C
		private MimePart NormalizeBodies()
		{
			MimePart result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				this.bodyList.RemoveAll((MimePart part) => !Utility.IsBodyContentType(part.ContentType));
				this.bodyTypes = BodyTypes.None;
				List<MimePart> duplicates = new List<MimePart>();
				foreach (MimePart mimePart in this.bodyList)
				{
					BodyTypes bodyType = Utility.GetBodyType(mimePart.ContentType);
					if ((this.bodyTypes & bodyType) != BodyTypes.None || bodyType < this.bodyTypes)
					{
						duplicates.Add(mimePart);
					}
					else
					{
						this.bodyTypes |= bodyType;
					}
				}
				this.bodyList.RemoveAll((MimePart part) => duplicates.Contains(part));
				MimePart mimePart2 = null;
				if (this.bodyList.Count == 0)
				{
					if (this.NeedPlaceholderBody())
					{
						if (this.placeholderBody == null)
						{
							this.placeholderBody = new MimePart();
						}
						mimePart2 = this.placeholderBody;
					}
				}
				else if (1 == this.bodyList.Count)
				{
					MimePart mimePart3 = this.bodyList[0];
					mimePart3.RemoveFromParent();
					mimePart2 = mimePart3;
				}
				else
				{
					mimePart2 = new MimePart("multipart/alternative");
					this.multipartAlternative = mimePart2;
					foreach (MimePart child in this.bodyList)
					{
						Utility.MoveChildToNewParent(mimePart2, child);
					}
				}
				result = mimePart2;
			}
			return result;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00015374 File Offset: 0x00013574
		private MimePart NormalizeAttachments(MimePart root)
		{
			MimePart result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.tnefPart != null)
				{
					root = this.NormalizeRegularAttachment(root, this.tnefPart);
				}
				List<MimeAttachmentData> internalList = this.mimeAttachments.InternalList;
				for (int i = 0; i < internalList.Count; i++)
				{
					MimeAttachmentData mimeAttachmentData = internalList[i];
					if (mimeAttachmentData != null)
					{
						if (mimeAttachmentData.InternalAttachmentType == InternalAttachmentType.Regular || mimeAttachmentData.InternalAttachmentType == InternalAttachmentType.Inline)
						{
							root = this.NormalizeRegularAttachment(root, mimeAttachmentData.AttachmentPart);
						}
						else
						{
							root = this.NormalizeRelatedAttachment(root, mimeAttachmentData.AttachmentPart, i);
						}
					}
				}
				result = root;
			}
			return result;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00015420 File Offset: 0x00013620
		private MimePart NormalizeRegularAttachment(MimePart root, MimePart attachmentPart)
		{
			MimePart result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (root == null)
				{
					result = attachmentPart;
				}
				else
				{
					if (this.multipartMixed == null)
					{
						this.multipartMixed = new MimePart("multipart/mixed");
						Utility.MoveChildToNewParent(this.multipartMixed, root);
						root = this.multipartMixed;
					}
					Utility.MoveChildToNewParent(this.multipartMixed, attachmentPart);
					result = root;
				}
			}
			return result;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00015498 File Offset: 0x00013698
		private MimePart NormalizeRelatedAttachment(MimePart root, MimePart attachmentPart, int index)
		{
			MimePart result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.bodyList.Count == 0)
				{
					this.mimeAttachments.RemoveAtPrivateIndex(index);
					result = root;
				}
				else
				{
					if (this.multipartRelated == null)
					{
						this.multipartRelated = new MimePart("multipart/related");
						if (this.multipartMixed == null)
						{
							if (root == this.multipartAlternative || Utility.IsBodyContentType(root.ContentType))
							{
								Utility.MoveChildToNewParent(this.multipartRelated, root);
								root = this.multipartRelated;
							}
						}
						else
						{
							Utility.MoveChildToNewParent(this.multipartRelated, this.multipartMixed.FirstChild);
							this.multipartMixed.InsertAfter(this.multipartRelated, null);
						}
					}
					Utility.MoveChildToNewParent(this.multipartRelated, attachmentPart);
					result = root;
				}
			}
			return result;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00015574 File Offset: 0x00013774
		private void CreateEmptyMessage()
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				this.ResetSynchronization();
				this.bodyData.SetFormat(BodyFormat.Text, InternalBodyFormat.Text, this.defaultBodyCharset);
				this.bodyData.ReleaseStorage();
				this.bodyMimePart = new MimePart("text/plain");
				this.bodyStructure = BodyStructure.SingleBody;
				this.bodyTypes = BodyTypes.Text;
				this.messageType = MessageType.Normal;
				this.SetRootPart(this.bodyMimePart);
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00015600 File Offset: 0x00013800
		private void ResetSynchronization()
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.bodyList == null)
				{
					this.bodyList = new List<MimePart>();
				}
				if (this.mimeAttachments == null)
				{
					this.mimeAttachments = new AttachmentDataCollection<MimeAttachmentData>();
				}
				this.ToRecipients = null;
				this.CcRecipients = null;
				this.BccRecipients = null;
				this.ReplyToRecipients = null;
				this.FromRecipient = null;
				this.SenderRecipient = null;
				this.DntRecipient = null;
				this.bodyData.SetFormat(BodyFormat.None, InternalBodyFormat.None, this.defaultBodyCharset);
				this.bodyData.ReleaseStorage();
				this.ResetDetection();
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000156B0 File Offset: 0x000138B0
		private void ResetDetection()
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				this.mimeAttachments.Reset();
				this.bodyList.Clear();
				this.bodyMimePart = null;
				this.bodyTypes = BodyTypes.None;
				this.bodyStructure = BodyStructure.Undefined;
				this.messageType = MessageType.Undefined;
				this.messageClass = "IPM.Note";
				this.multipartMixed = null;
				this.multipartRelated = null;
				this.multipartAlternative = null;
				this.multipartSigned = null;
				this.signaturePart = null;
				if (this.calendarMessage != null)
				{
					this.calendarMessage = null;
					if (this.calendarRelayStorage != null)
					{
						this.calendarRelayStorage.Release();
						this.calendarRelayStorage = null;
					}
				}
				this.calendarPart = null;
				this.rebuildStructureAtNextOpportunity = false;
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001577C File Offset: 0x0001397C
		private MimePart SetRootPart(MimePart newRoot)
		{
			MimePart result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (newRoot == this.rootPart)
				{
					result = newRoot;
				}
				else
				{
					newRoot.RemoveFromParent();
					foreach (Header header in newRoot.Headers)
					{
						switch (header.HeaderId)
						{
						case HeaderId.ContentDescription:
						case HeaderId.ContentDisposition:
						case HeaderId.ContentMD5:
						case HeaderId.ContentTransferEncoding:
						case HeaderId.ContentType:
							break;
						default:
							header.RemoveFromParent();
							break;
						}
					}
					foreach (Header header2 in this.rootPart.Headers)
					{
						switch (header2.HeaderId)
						{
						case HeaderId.ContentDescription:
						case HeaderId.ContentDisposition:
						case HeaderId.ContentMD5:
						case HeaderId.ContentTransferEncoding:
						case HeaderId.ContentType:
							break;
						default:
							Utility.MoveChildToNewParent(newRoot.Headers, header2);
							break;
						}
					}
					if (this.mimeDocument != null)
					{
						this.mimeDocument.RootPart = newRoot;
						this.rootPart = newRoot;
					}
					else
					{
						MimeNode parent = this.rootPart.Parent;
						if (parent != null)
						{
							parent.ReplaceChild(newRoot, this.rootPart);
						}
						this.rootPart = newRoot;
					}
					result = newRoot;
				}
			}
			return result;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00015928 File Offset: 0x00013B28
		private void DetectMessageType()
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (!this.IsSynchronized)
				{
					MimePart mimePart = this.RootPart;
					ContentTypeHeader contentTypeHeader = mimePart.Headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
					if (contentTypeHeader == null)
					{
						this.messageType = MessageType.Normal;
						this.bodyTypes = BodyTypes.Text;
						this.bodyStructure = BodyStructure.SingleBody;
					}
					else
					{
						string headerValue = Utility.GetHeaderValue(contentTypeHeader);
						if (!this.DetectTnefStructure(headerValue) && !this.DetectVoiceMessage(headerValue) && !this.DetectJournalMessage(headerValue) && !this.DetectDsnOrMdn(headerValue, contentTypeHeader) && !this.DetectRightsProtectedMessage(headerValue) && !this.DetectFaxMessage(headerValue) && !this.DetectUMPartnerMessage(headerValue) && !this.DetectQuotaMessage(headerValue) && !this.DetectApprovalMessage(headerValue))
						{
							if (this.DetectSmime(headerValue, contentTypeHeader))
							{
								this.DetectInfoPathFormMessage();
							}
							else if (!this.DetectOpenPgp(headerValue, contentTypeHeader) && !this.DetectAdReplicationMessage(headerValue))
							{
								if (!this.DetectNormalMessage(mimePart, headerValue))
								{
									this.FindBodiesAndAttachmentsHeuristically(mimePart);
								}
								this.DetectSharingMessage();
								this.DetectInfoPathFormMessage();
								this.DetectCustomClassMessage();
							}
						}
					}
				}
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00015A50 File Offset: 0x00013C50
		private bool DetectTnefStructure(string rootContentType)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (!this.DetectLegacyTnef(rootContentType) && !this.DetectSummaryTnef(rootContentType) && !this.DetectSuperLegacyTnefWithAttachments(rootContentType))
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00015AA8 File Offset: 0x00013CA8
		private bool DetectLegacyTnef(string rootContentType)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (rootContentType != "multipart/mixed" || !Utility.HasExactlyTwoChildren(this.RootPart))
				{
					result = false;
				}
				else
				{
					MimePart mimePart = this.RootPart.FirstChild as MimePart;
					if (mimePart.ContentType != "text/plain")
					{
						result = false;
					}
					else
					{
						MimePart mimePart2 = mimePart.NextSibling as MimePart;
						if (!(mimePart2.ContentType == "application/ms-tnef"))
						{
							if (!(mimePart2.ContentType == "application/octet-stream"))
							{
								return false;
							}
							string parameterValue = Utility.GetParameterValue(mimePart2, HeaderId.ContentType, "name");
							string parameterValue2 = Utility.GetParameterValue(mimePart2, HeaderId.ContentDisposition, "filename");
							if (parameterValue != "winmail.dat" && parameterValue2 != "winmail.dat")
							{
								return false;
							}
						}
						this.messageType = MessageType.LegacyTnef;
						this.bodyStructure = BodyStructure.SingleBody;
						this.AddBody(mimePart);
						this.AddAttachment(mimePart2, InternalAttachmentType.Regular);
						this.multipartMixed = this.RootPart;
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00015BD0 File Offset: 0x00013DD0
		private bool DetectSummaryTnef(string rootContentType)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (rootContentType != "application/ms-tnef")
				{
					result = false;
				}
				else
				{
					string parameterValue = Utility.GetParameterValue(this.RootPart, HeaderId.ContentType, "name");
					if (parameterValue != "winmail.dat")
					{
						result = false;
					}
					else
					{
						this.messageType = MessageType.SummaryTnef;
						this.AddAttachment(this.RootPart, InternalAttachmentType.Regular);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00015C54 File Offset: 0x00013E54
		private bool DetectSuperLegacyTnefWithAttachments(string rootContentType)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (rootContentType != "multipart/mixed")
				{
					result = false;
				}
				else
				{
					MimePart mimePart = this.RootPart.FirstChild as MimePart;
					if (mimePart == null || mimePart.ContentType != "text/plain")
					{
						result = false;
					}
					else
					{
						MimePart mimePart2 = this.RootPart.LastChild as MimePart;
						if (mimePart2 == null || mimePart2 == mimePart)
						{
							result = false;
						}
						else if (this.RootPart.Headers.FindFirst("X-ConvertedToMime") == null)
						{
							result = false;
						}
						else if (mimePart2.ContentType == "application/octet-stream")
						{
							string parameterValue = Utility.GetParameterValue(mimePart2, HeaderId.ContentType, "name");
							if (parameterValue != "winmail.dat")
							{
								result = false;
							}
							else
							{
								this.messageType = MessageType.LegacyTnef;
								this.bodyStructure = BodyStructure.SingleBody;
								this.AddBody(mimePart);
								if (!this.AddAttachments(this.RootPart, mimePart, InternalAttachmentType.Regular))
								{
									result = false;
								}
								else
								{
									this.multipartMixed = this.RootPart;
									result = true;
								}
							}
						}
						else
						{
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00015D80 File Offset: 0x00013F80
		private bool DetectAdReplicationMessage(string rootContentType)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (rootContentType != "image/gif")
				{
					result = false;
				}
				else if (ContentTransferEncoding.Base64 != this.RootPart.ContentTransferEncoding)
				{
					result = false;
				}
				else
				{
					EmailRecipient from = this.From;
					if (from == null)
					{
						result = false;
					}
					else
					{
						EmailRecipientCollection to = this.To;
						if (to.Count != 1)
						{
							result = false;
						}
						else
						{
							EmailRecipient emailRecipient = to[0];
							string smtpAddress = from.SmtpAddress;
							string smtpAddress2 = emailRecipient.SmtpAddress;
							string text = "_IsmService";
							if (!smtpAddress.StartsWith(text, StringComparison.Ordinal))
							{
								result = false;
							}
							else if (!smtpAddress2.StartsWith(text, StringComparison.Ordinal))
							{
								result = false;
							}
							else
							{
								string text2 = "_msdcs";
								int num = 37;
								int num2 = text.Length + num;
								int num3 = num2 + text2.Length + 1;
								if (num3 > smtpAddress.Length || num3 > smtpAddress2.Length)
								{
									result = false;
								}
								else if (smtpAddress[num2] != '.' || smtpAddress2[num2] != '.')
								{
									result = false;
								}
								else
								{
									string text3 = smtpAddress.Substring(num2 + 1, text2.Length);
									if (!text3.Equals(text2))
									{
										result = false;
									}
									else
									{
										text3 = smtpAddress2.Substring(num2 + 1, text2.Length);
										if (!text3.Equals(text2))
										{
											result = false;
										}
										else
										{
											this.messageType = MessageType.AdReplicationMessage;
											this.AddAttachment(this.RootPart, InternalAttachmentType.Regular);
											result = true;
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00015F20 File Offset: 0x00014120
		private bool DetectRightsProtectedMessage(string contentType)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (contentType != "multipart/mixed" || this.RootPart.FirstChild == null)
				{
					result = false;
				}
				else if (!Utility.HasExactlyTwoChildren(this.RootPart))
				{
					result = false;
				}
				else
				{
					MimePart mimePart = this.RootPart.FirstChild as MimePart;
					MimePart mimePart2 = mimePart.NextSibling as MimePart;
					if (!mimePart2.ContentType.Equals("application/x-microsoft-rpmsg-message", StringComparison.OrdinalIgnoreCase))
					{
						result = false;
					}
					else
					{
						AsciiTextHeader asciiTextHeader = this.RootPart.Headers.FindFirst(HeaderId.ContentClass) as AsciiTextHeader;
						if (asciiTextHeader == null)
						{
							result = false;
						}
						else
						{
							string headerValue = Utility.GetHeaderValue(asciiTextHeader);
							if (string.IsNullOrEmpty(headerValue))
							{
								result = false;
							}
							else
							{
								if (headerValue.Equals("IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA", StringComparison.OrdinalIgnoreCase))
								{
									this.messageClass = "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA";
								}
								else if (headerValue.Equals("IPM.Note.rpmsg.Microsoft.Voicemail.UM", StringComparison.OrdinalIgnoreCase))
								{
									this.messageClass = "IPM.Note.rpmsg.Microsoft.Voicemail.UM";
								}
								else if (!headerValue.Equals("rpmsg.message", StringComparison.OrdinalIgnoreCase))
								{
									return false;
								}
								if (!this.AddBody(mimePart))
								{
									if (!mimePart.ContentType.Equals("multipart/alternative", StringComparison.OrdinalIgnoreCase) || !this.FindBodiesAndRelatedAttachments(mimePart))
									{
										this.ResetDetection();
										return false;
									}
								}
								else
								{
									this.bodyStructure = BodyStructure.SingleBody;
								}
								this.messageType = MessageType.MsRightsProtected;
								this.AddAttachment(mimePart2, InternalAttachmentType.Regular);
								this.multipartMixed = this.RootPart;
								if (this.mimeAttachments.Count != 1)
								{
									this.ResetDetection();
									result = false;
								}
								else
								{
									result = true;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000160C8 File Offset: 0x000142C8
		private bool DetectOpenPgp(string contentType, ContentTypeHeader contentTypeHeader)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (contentType != "multipart/encrypted")
				{
					result = false;
				}
				else
				{
					MimePart mimePart = this.RootPart.FirstChild as MimePart;
					if (mimePart == null || mimePart.ContentType != "application/pgp-encrypted")
					{
						result = false;
					}
					else
					{
						MimePart mimePart2 = mimePart.NextSibling as MimePart;
						if (mimePart2 == null || mimePart2.ContentType != "application/octet-stream")
						{
							result = false;
						}
						else
						{
							MimePart mimePart3 = mimePart2.NextSibling as MimePart;
							if (mimePart3 != null)
							{
								result = false;
							}
							else
							{
								string parameterValue = Utility.GetParameterValue(contentTypeHeader, "protocol");
								if (string.IsNullOrEmpty(parameterValue))
								{
									result = false;
								}
								else if (!parameterValue.Equals("application/pgp-encrypted", StringComparison.OrdinalIgnoreCase))
								{
									result = false;
								}
								else
								{
									this.messageType = MessageType.PgpEncrypted;
									this.AddAttachment(mimePart, InternalAttachmentType.Regular);
									this.AddAttachment(mimePart2, InternalAttachmentType.Regular);
									result = true;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000161C8 File Offset: 0x000143C8
		private bool DetectSmime(string contentType, ContentTypeHeader contentTypeHeader)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (contentType == "application/pkcs7-mime" || contentType == "application/x-pkcs7-mime")
				{
					string parameterValue = Utility.GetParameterValue(contentTypeHeader, "smime-type");
					if (parameterValue == null)
					{
						this.messageType = MessageType.SmimeEncrypted;
					}
					else if (parameterValue.Equals("signed-data", StringComparison.OrdinalIgnoreCase))
					{
						this.messageType = MessageType.SmimeOpaqueSigned;
					}
					else if (parameterValue.Equals("enveloped-data", StringComparison.OrdinalIgnoreCase))
					{
						this.messageType = MessageType.SmimeEncrypted;
					}
					else
					{
						if (!parameterValue.Equals("certs-only", StringComparison.OrdinalIgnoreCase))
						{
							return false;
						}
						this.messageType = MessageType.SmimeOpaqueSigned;
					}
					this.messageClass = "IPM.Note.SMIME";
					result = true;
				}
				else
				{
					if (contentType == "application/octet-stream")
					{
						string parameterValue2 = Utility.GetParameterValue(contentTypeHeader, "name");
						string parameterValue3 = Utility.GetParameterValue(this.RootPart, HeaderId.ContentDisposition, "filename");
						if (PureMimeMessage.FileNameIndicatesSmime(parameterValue2) || PureMimeMessage.FileNameIndicatesSmime(parameterValue3))
						{
							this.messageClass = "IPM.Note.SMIME";
							this.messageType = MessageType.SmimeOpaqueSigned;
							return true;
						}
					}
					else if (contentType == "multipart/signed")
					{
						if (!Utility.HasExactlyTwoChildren(this.RootPart))
						{
							return false;
						}
						MimePart mimePart = this.RootPart.FirstChild as MimePart;
						if (mimePart == null)
						{
							return false;
						}
						MimePart mimePart2 = mimePart.NextSibling as MimePart;
						if (mimePart2 == null)
						{
							return false;
						}
						string contentType2 = mimePart2.ContentType;
						if (contentType2 == "application/pkcs7-signature" || contentType2 == "application/x-pkcs7-signature" || contentType2 == "application/pgp-signature")
						{
							string contentType3 = mimePart.ContentType;
							if (contentType3 == "application/pkcs7-mime" || contentType3 == "application/x-pkcs7-mime")
							{
								this.messageType = MessageType.SmimeSignedEncrypted;
								this.messageClass = "IPM.Note.SMIME";
							}
							else
							{
								if (this.DetectNormalMessage(mimePart, null))
								{
									this.messageType = MessageType.SmimeSignedNormal;
								}
								else
								{
									this.FindBodiesAndAttachmentsHeuristically(mimePart);
									this.messageType = MessageType.SmimeSignedUnknown;
								}
								this.messageClass = "IPM.Note.SMIME.MultipartSigned";
							}
							this.multipartSigned = this.RootPart;
							this.signaturePart = mimePart2;
							return true;
						}
						return false;
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00016418 File Offset: 0x00014618
		private bool DetectDsnOrMdn(string contentType, ContentTypeHeader contentTypeHeader)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (contentType != "multipart/report")
				{
					result = false;
				}
				else
				{
					string text = Utility.GetParameterValue(contentTypeHeader, "report-type");
					if (string.IsNullOrEmpty(text))
					{
						result = false;
					}
					else
					{
						text = text.ToLower();
						MimePart mimePart = this.RootPart.FirstChild as MimePart;
						MimePart mimePart2 = (mimePart == null) ? null : (mimePart.NextSibling as MimePart);
						Stream stream = null;
						try
						{
							if (mimePart2 != null && (mimePart2.ContentType == "message/delivery-status" || mimePart2.ContentType == "message/disposition-notification") && mimePart2.TryGetContentReadStream(out stream))
							{
								KeyValuePair<string, string>[] map = null;
								string text2 = null;
								if (string.Equals(text, "delivery-status", StringComparison.OrdinalIgnoreCase))
								{
									text2 = "action";
									map = PureMimeMessage.actionToClassSuffix;
									this.messageType = MessageType.Dsn;
									PureMimeMessage.ReadReportHeaders(stream);
								}
								else if (string.Equals(text, "disposition-notification", StringComparison.OrdinalIgnoreCase))
								{
									text2 = "disposition";
									map = PureMimeMessage.dispositionToClassSuffix;
									this.messageType = MessageType.Mdn;
								}
								if (!string.IsNullOrEmpty(text2))
								{
									Dictionary<string, string> dictionary = PureMimeMessage.ReadReportHeaders(stream);
									string text3 = null;
									while (dictionary != null && dictionary.Count > 0)
									{
										string text4;
										if (dictionary.TryGetValue(text2, out text4))
										{
											PureMimeMessage.GetMessageClassSuffix(map, text4.ToLowerInvariant(), ref text3);
										}
										dictionary = PureMimeMessage.ReadReportHeaders(stream);
									}
									if (!string.IsNullOrEmpty(text3) && this.FindBodiesAndRelatedAttachments(mimePart) && this.AddAttachments(this.RootPart, mimePart, InternalAttachmentType.Regular))
									{
										this.messageClass = "Report.IPM.Note." + text3;
										return true;
									}
								}
							}
						}
						finally
						{
							if (stream != null)
							{
								stream.Dispose();
								stream = null;
							}
						}
						this.ResetDetection();
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00016600 File Offset: 0x00014800
		private bool DetectJournalMessage(string contentType)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (contentType != "multipart/mixed")
				{
					result = false;
				}
				else if (this.RootPart.Headers.FindFirst("X-MS-Journal-Report") == null)
				{
					result = false;
				}
				else
				{
					MimePart mimePart = this.RootPart.FirstChild as MimePart;
					if (mimePart == null || mimePart.ContentType != "text/plain")
					{
						result = false;
					}
					else
					{
						MimePart mimePart2 = mimePart.NextSibling as MimePart;
						if (mimePart2 == null)
						{
							result = false;
						}
						else
						{
							bool flag = false;
							if (mimePart2.ContentType != "message/rfc822")
							{
								if (!mimePart2.ContentType.StartsWith("application/", StringComparison.OrdinalIgnoreCase))
								{
									return false;
								}
								flag = true;
							}
							MimePart mimePart3 = mimePart2.NextSibling as MimePart;
							if (mimePart3 != null)
							{
								if (flag || mimePart3.ContentType != "message/rfc822")
								{
									return false;
								}
								MimePart mimePart4 = mimePart3.NextSibling as MimePart;
								if (mimePart4 != null)
								{
									return false;
								}
							}
							if (!this.AddBody(mimePart))
							{
								this.ResetDetection();
								result = false;
							}
							else if (!this.AddAttachment(mimePart2, InternalAttachmentType.Regular))
							{
								this.ResetDetection();
								result = false;
							}
							else if (mimePart3 != null && !this.AddAttachment(mimePart3, InternalAttachmentType.Regular))
							{
								this.ResetDetection();
								result = false;
							}
							else
							{
								this.messageType = MessageType.Journal;
								this.multipartMixed = this.RootPart;
								result = true;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00016790 File Offset: 0x00014990
		private bool DetectQuotaMessage(string contentType)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				Header header = this.rootPart.Headers.FindFirst("X-MS-Exchange-Organization-StorageQuota");
				if (header == null)
				{
					result = false;
				}
				else if (!this.DetectNormalBodyStructure(this.RootPart))
				{
					this.ResetDetection();
					result = false;
				}
				else
				{
					string headerValue = Utility.GetHeaderValue(header);
					if (string.IsNullOrEmpty(headerValue))
					{
						this.ResetDetection();
						result = false;
					}
					else if (headerValue.Equals("warning", StringComparison.OrdinalIgnoreCase))
					{
						this.messageClass = "IPM.Note.StorageQuotaWarning.Warning";
						this.messageType = MessageType.Quota;
						result = true;
					}
					else if (headerValue.Equals("send", StringComparison.OrdinalIgnoreCase))
					{
						this.messageClass = "IPM.Note.StorageQuotaWarning.Send";
						this.messageType = MessageType.Quota;
						result = true;
					}
					else if (headerValue.Equals("sendreceive", StringComparison.OrdinalIgnoreCase))
					{
						this.messageClass = "IPM.Note.StorageQuotaWarning.SendReceive";
						this.messageType = MessageType.Quota;
						result = true;
					}
					else
					{
						this.ResetDetection();
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00016894 File Offset: 0x00014A94
		private bool DetectVoiceMessage(string contentType)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				Header header = this.RootPart.Headers.FindFirst(HeaderId.ContentClass);
				if (header == null)
				{
					result = false;
				}
				else if (!this.DetectNormalMessage(this.RootPart, contentType))
				{
					this.ResetDetection();
					result = false;
				}
				else
				{
					string headerValue = Utility.GetHeaderValue(header);
					if (string.IsNullOrEmpty(headerValue))
					{
						this.ResetDetection();
						result = false;
					}
					else
					{
						foreach (MimeAttachmentData mimeAttachmentData in this.mimeAttachments.InternalList)
						{
							if (mimeAttachmentData != null)
							{
								string contentType2 = mimeAttachmentData.AttachmentPart.ContentType;
								if (!PureMimeMessage.IsVoiceContentType(contentType2))
								{
									this.ResetDetection();
									return false;
								}
							}
						}
						if (headerValue.Equals("voice", StringComparison.OrdinalIgnoreCase))
						{
							this.messageClass = "IPM.Note.Microsoft.Voicemail.UM";
							this.messageType = MessageType.Voice;
							result = true;
						}
						else if (headerValue.Equals("voice-ca", StringComparison.OrdinalIgnoreCase))
						{
							this.messageClass = "IPM.Note.Microsoft.Voicemail.UM.CA";
							this.messageType = MessageType.Voice;
							result = true;
						}
						else if (headerValue.Equals("voice-uc", StringComparison.OrdinalIgnoreCase))
						{
							this.messageClass = "IPM.Note.Microsoft.Conversation.Voice";
							this.messageType = MessageType.Voice;
							result = true;
						}
						else if (headerValue.Equals("missedcall", StringComparison.OrdinalIgnoreCase))
						{
							this.messageClass = "IPM.Note.Microsoft.Missed.Voice";
							this.messageType = MessageType.Voice;
							result = true;
						}
						else
						{
							this.ResetDetection();
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00016A48 File Offset: 0x00014C48
		private bool DetectUMPartnerMessage(string contentType)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				Header header = this.RootPart.Headers.FindFirst(HeaderId.ContentClass);
				if (header == null)
				{
					result = false;
				}
				else if (!this.DetectNormalMessage(this.RootPart, contentType))
				{
					this.ResetDetection();
					result = false;
				}
				else
				{
					string headerValue = Utility.GetHeaderValue(header);
					if (string.IsNullOrEmpty(headerValue))
					{
						this.ResetDetection();
						result = false;
					}
					else if (headerValue.Equals("MS-Exchange-UM-Partner", StringComparison.OrdinalIgnoreCase))
					{
						this.messageClass = "IPM.Note.Microsoft.Partner.UM";
						this.messageType = MessageType.UMPartner;
						result = true;
					}
					else
					{
						this.ResetDetection();
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00016AF8 File Offset: 0x00014CF8
		private bool DetectSharingMessage()
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				Header header = this.RootPart.Headers.FindFirst(HeaderId.ContentClass);
				if (header == null)
				{
					result = false;
				}
				else
				{
					string headerValue = Utility.GetHeaderValue(header);
					if (string.IsNullOrEmpty(headerValue))
					{
						result = false;
					}
					else if (headerValue.Equals("Sharing", StringComparison.OrdinalIgnoreCase))
					{
						this.messageClass = "IPM.Sharing";
						this.messageType = MessageType.Normal;
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

		// Token: 0x060006DD RID: 1757 RVA: 0x00016B84 File Offset: 0x00014D84
		private bool DetectCustomClassMessage()
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				Header header = this.rootPart.Headers.FindFirst(HeaderId.ContentClass);
				if (header == null)
				{
					result = false;
				}
				else
				{
					string headerValue = Utility.GetHeaderValue(header);
					if (string.IsNullOrEmpty(headerValue))
					{
						result = false;
					}
					else if (headerValue.Equals("RSS", StringComparison.OrdinalIgnoreCase))
					{
						this.messageClass = "IPM.Post.RSS";
						result = true;
					}
					else if (headerValue.Equals("MS-OMS-SMS", StringComparison.OrdinalIgnoreCase))
					{
						this.messageClass = "IPM.Note.Mobile.SMS";
						result = true;
					}
					else if (headerValue.Equals("MS-OMS-MMS", StringComparison.OrdinalIgnoreCase))
					{
						this.messageClass = "IPM.Note.Mobile.MMS";
						result = true;
					}
					else if (headerValue.StartsWith("urn:content-class:custom.", StringComparison.OrdinalIgnoreCase))
					{
						string str = headerValue.Substring("urn:content-class:custom.".Length);
						this.messageClass = "IPM.Note.Custom." + str;
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

		// Token: 0x060006DE RID: 1758 RVA: 0x00016C80 File Offset: 0x00014E80
		private bool DetectInfoPathFormMessage()
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				Header header = this.rootPart.Headers.FindFirst(HeaderId.ContentClass);
				if (header == null)
				{
					result = false;
				}
				else
				{
					string headerValue = Utility.GetHeaderValue(header);
					if (string.IsNullOrEmpty(headerValue))
					{
						result = false;
					}
					else
					{
						if (headerValue.StartsWith("InfoPathForm.", StringComparison.OrdinalIgnoreCase))
						{
							string text = headerValue.Substring("InfoPathForm.".Length);
							int num = text.IndexOf('.');
							if (num > 0)
							{
								try
								{
									string text2 = text.Substring(0, num);
									new Guid(text2);
									text.Substring(num + 1);
									string str = string.Empty;
									if (this.messageType == MessageType.SmimeEncrypted || this.messageType == MessageType.SmimeOpaqueSigned)
									{
										str = ".SMIME";
									}
									else if (this.messageType == MessageType.SmimeSignedEncrypted || this.messageType == MessageType.SmimeSignedNormal || this.messageType == MessageType.SmimeSignedUnknown)
									{
										str = ".SMIME.MultipartSigned";
									}
									this.messageClass = "IPM.InfoPathForm." + text2 + str;
									return true;
								}
								catch (FormatException)
								{
								}
								catch (OverflowException)
								{
								}
							}
						}
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00016DC0 File Offset: 0x00014FC0
		private bool DetectFaxMessage(string contentType)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				Header header = this.rootPart.Headers.FindFirst(HeaderId.ContentClass);
				if (header == null)
				{
					result = false;
				}
				else if ("multipart/mixed" != contentType)
				{
					result = false;
				}
				else
				{
					MimePart mimePart = this.RootPart.FirstChild as MimePart;
					if (mimePart == null)
					{
						result = false;
					}
					else
					{
						MimePart mimePart2 = mimePart.NextSibling as MimePart;
						if (mimePart2 == null)
						{
							result = false;
						}
						else if ("text/html" != mimePart.ContentType)
						{
							result = false;
						}
						else if ("image/tiff" != mimePart2.ContentType)
						{
							result = false;
						}
						else if (!this.AddBody(mimePart))
						{
							this.ResetDetection();
							result = false;
						}
						else if (!this.AddAttachment(mimePart2, InternalAttachmentType.Regular))
						{
							this.ResetDetection();
							result = false;
						}
						else
						{
							string headerValue = Utility.GetHeaderValue(header);
							if (string.IsNullOrEmpty(headerValue))
							{
								this.ResetDetection();
								result = false;
							}
							else if (headerValue.Equals("fax", StringComparison.OrdinalIgnoreCase))
							{
								this.messageClass = "IPM.Note.Microsoft.Fax";
								this.messageType = MessageType.Fax;
								this.multipartMixed = this.RootPart;
								result = true;
							}
							else if (headerValue.Equals("fax-ca", StringComparison.OrdinalIgnoreCase))
							{
								this.messageClass = "IPM.Note.Microsoft.Fax.CA";
								this.messageType = MessageType.Fax;
								this.multipartMixed = this.RootPart;
								result = true;
							}
							else
							{
								this.ResetDetection();
								result = false;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00016F58 File Offset: 0x00015158
		private bool DetectApprovalMessage(string contentType)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-Approval-Initiator") == null)
				{
					this.ResetDetection();
					result = false;
				}
				else if (!this.DetectNormalMessage(this.RootPart, contentType))
				{
					this.ResetDetection();
					result = false;
				}
				else
				{
					this.messageClass = "IPM.Microsoft.Approval.Initiation";
					this.messageType = MessageType.ApprovalInitiation;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00016FE0 File Offset: 0x000151E0
		private bool DetectNormalMessage(MimePart part, string contentType)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (string.IsNullOrEmpty(contentType))
				{
					contentType = part.ContentType;
				}
				if (this.AddBody(part))
				{
					this.messageType = MessageType.Normal;
					this.bodyStructure = BodyStructure.SingleBody;
					result = true;
				}
				else if (this.IsAttachmentCandidate(part))
				{
					this.AddAttachment(part, InternalAttachmentType.Regular);
					this.messageType = MessageType.SingleAttachment;
					this.bodyTypes = BodyTypes.None;
					this.bodyStructure = BodyStructure.None;
					result = true;
				}
				else if (contentType == "multipart/mixed")
				{
					this.multipartMixed = part;
					if (!this.DetectNormalMultipartMixed(part))
					{
						this.multipartMixed = null;
						result = false;
					}
					else
					{
						result = true;
					}
				}
				else if (contentType == "multipart/related" || contentType == "multipart/alternative")
				{
					this.messageType = MessageType.Normal;
					if (!this.DetectNormalBodyStructure(part))
					{
						result = false;
					}
					else
					{
						result = true;
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000170D0 File Offset: 0x000152D0
		private bool DetectNormalMultipartMixed(MimePart root)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				MimePart mimePart = root.FirstChild as MimePart;
				if (mimePart == null)
				{
					result = false;
				}
				else
				{
					if (mimePart.NextSibling == null)
					{
						this.rebuildStructureAtNextOpportunity = true;
					}
					this.messageType = MessageType.MultipleAttachments;
					this.bodyStructure = BodyStructure.None;
					bool flag = this.bodyList.Count == 0;
					while (mimePart != null)
					{
						string contentType = mimePart.ContentType;
						if (flag && this.AddBody(mimePart))
						{
							this.messageType = MessageType.NormalWithRegularAttachments;
							this.bodyStructure = BodyStructure.SingleBody;
							flag = false;
						}
						else if (flag && (contentType == "multipart/related" || contentType == "multipart/alternative"))
						{
							this.messageType = MessageType.NormalWithRegularAttachments;
							if (!this.DetectNormalBodyStructure(mimePart))
							{
								return false;
							}
							flag = false;
						}
						else
						{
							if (!this.IsAttachmentCandidate(mimePart))
							{
								return false;
							}
							this.AddAttachment(mimePart, InternalAttachmentType.Regular);
						}
						mimePart = (mimePart.NextSibling as MimePart);
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000171D4 File Offset: 0x000153D4
		private bool IsAttachmentCandidate(MimePart part)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				MimePart mimePart;
				MimePart mimePart2;
				if (!part.IsMultipart)
				{
					result = true;
				}
				else if (Utility.TryGetAppleDoubleParts(part, out mimePart, out mimePart2))
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00017228 File Offset: 0x00015428
		private bool DetectNormalBodyStructure(MimePart root)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				this.bodyStructure = BodyStructure.Undefined;
				this.bodyTypes = BodyTypes.None;
				if (!this.FindBodiesAndRelatedAttachments(root))
				{
					this.bodyStructure = BodyStructure.None;
					this.bodyTypes = BodyTypes.None;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001728C File Offset: 0x0001548C
		internal bool FindBodiesAndRelatedAttachments(MimePart root)
		{
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				string contentType = root.ContentType;
				if (this.AddBody(root))
				{
					this.bodyStructure = BodyStructure.SingleBody;
					result = true;
				}
				else if (contentType == "multipart/alternative")
				{
					if (root.FirstChild == null)
					{
						result = false;
					}
					else
					{
						if (root.FirstChild.NextSibling == null)
						{
							this.rebuildStructureAtNextOpportunity = true;
						}
						bool flag = false;
						foreach (MimePart bodyPart in root)
						{
							if (!this.AddAlternativeBody(bodyPart, ref flag))
							{
								return false;
							}
						}
						if (flag)
						{
							this.bodyStructure = BodyStructure.AlternativeBodiesWithMhtml;
						}
						else
						{
							this.bodyStructure = BodyStructure.AlternativeBodies;
						}
						this.multipartAlternative = root;
						result = true;
					}
				}
				else if (contentType == "multipart/related")
				{
					if (root.FirstChild == null)
					{
						result = false;
					}
					else
					{
						if (root.FirstChild.NextSibling == null)
						{
							this.rebuildStructureAtNextOpportunity = true;
						}
						MimePart startChild = Utility.GetStartChild(root);
						if (startChild == null)
						{
							result = false;
						}
						else
						{
							string contentType2 = startChild.ContentType;
							if (contentType2 == "text/html")
							{
								if (!this.AddBody(startChild))
								{
									result = false;
								}
								else if (!this.AddAttachments(root, startChild, InternalAttachmentType.Related))
								{
									result = false;
								}
								else
								{
									this.multipartRelated = root;
									this.bodyStructure = BodyStructure.SingleBodyWithRelatedAttachments;
									result = true;
								}
							}
							else if (contentType2 == "multipart/alternative")
							{
								if (startChild.FirstChild == null)
								{
									result = false;
								}
								else
								{
									if (startChild.FirstChild.NextSibling == null)
									{
										this.rebuildStructureAtNextOpportunity = true;
									}
									foreach (MimePart part in startChild)
									{
										if (!this.AddBody(part))
										{
											return false;
										}
									}
									if ((this.bodyTypes & BodyTypes.Html) == BodyTypes.None)
									{
										result = false;
									}
									else if (!this.AddAttachments(root, startChild, InternalAttachmentType.Related))
									{
										result = false;
									}
									else
									{
										this.bodyStructure = BodyStructure.AlternativeBodiesWithSharedAttachments;
										this.multipartRelated = root;
										this.multipartAlternative = startChild;
										result = true;
									}
								}
							}
							else
							{
								result = false;
							}
						}
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00017500 File Offset: 0x00015700
		private bool AddAlternativeBody(MimePart bodyPart, ref bool mhtml)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				BodyTypes bodyType = Utility.GetBodyType(bodyPart.ContentType);
				if (bodyType != BodyTypes.None)
				{
					bool flag = this.AddBody(bodyPart);
					result = flag;
				}
				else if (bodyPart.ContentType != "multipart/related")
				{
					result = false;
				}
				else
				{
					this.multipartRelated = bodyPart;
					MimePart startChild = Utility.GetStartChild(bodyPart);
					if (startChild == null)
					{
						result = false;
					}
					else
					{
						string contentType = startChild.ContentType;
						if (contentType != "text/html")
						{
							result = false;
						}
						else if (!this.AddBody(startChild))
						{
							result = false;
						}
						else if (!this.AddAttachments(bodyPart, startChild, InternalAttachmentType.Related))
						{
							result = false;
						}
						else
						{
							mhtml = true;
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x000175C0 File Offset: 0x000157C0
		private void DetectCalendarAttachment(MimePart part)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (part.Parent == this.multipartMixed && part.ContentType == "text/calendar")
				{
					ContentDispositionHeader header = part.Headers.FindFirst(HeaderId.ContentDisposition) as ContentDispositionHeader;
					string headerValue = Utility.GetHeaderValue(header);
					if (headerValue != "attachment" && this.calendarPart == null)
					{
						this.calendarPart = part;
					}
				}
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001764C File Offset: 0x0001584C
		private bool AddAttachments(MimePart multipart, MimePart exclude, InternalAttachmentType type)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				foreach (MimePart mimePart in multipart)
				{
					if (mimePart != exclude && !this.AddAttachment(mimePart, type))
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000176CC File Offset: 0x000158CC
		private bool AddAttachment(MimePart part, InternalAttachmentType type)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (part == this.tnefPart)
				{
					result = true;
				}
				else if (part.IsMultipart)
				{
					if (type != InternalAttachmentType.Regular)
					{
						result = false;
					}
					else
					{
						result = this.AddAppleDoubleAttachment(part);
					}
				}
				else
				{
					this.DetectCalendarAttachment(part);
					if (type == InternalAttachmentType.Regular)
					{
						type = Utility.CheckContentDisposition(part);
					}
					this.GetAttachmentData(part, type, this.Version);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001774C File Offset: 0x0001594C
		private bool AddAppleDoubleAttachment(MimePart part)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				MimePart dataPart;
				MimePart mimePart;
				if (Utility.TryGetAppleDoubleParts(part, out dataPart, out mimePart))
				{
					this.GetAttachmentData(part, dataPart, this.Version);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000177A4 File Offset: 0x000159A4
		private bool AddBody(MimePart part)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				BodyTypes bodyType = Utility.GetBodyType(part.ContentType);
				if (bodyType == BodyTypes.None)
				{
					result = false;
				}
				else if (this.bodyTypes >= bodyType)
				{
					result = false;
				}
				else
				{
					string headerValue = Utility.GetHeaderValue(part, HeaderId.ContentDisposition);
					if (string.Equals(headerValue, "attachment", StringComparison.OrdinalIgnoreCase))
					{
						result = false;
					}
					else
					{
						this.bodyTypes |= bodyType;
						if (bodyType == BodyTypes.Calendar)
						{
							this.calendarPart = part;
						}
						this.bodyList.Add(part);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001783C File Offset: 0x00015A3C
		private MimeAttachmentData GetAttachmentData(MimePart part, InternalAttachmentType attachmentType, int snapshotVersion)
		{
			MimeAttachmentData result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				int num;
				MimeAttachmentData attachmentData = this.GetAttachmentData(part, snapshotVersion, this, out num);
				attachmentData.InternalAttachmentType = attachmentType;
				result = attachmentData;
			}
			return result;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00017888 File Offset: 0x00015A88
		private MimeAttachmentData GetAttachmentData(MimePart part, InternalAttachmentType attachmentType, int snapshotVersion, out int index)
		{
			MimeAttachmentData result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				MimeAttachmentData attachmentData = this.GetAttachmentData(part, snapshotVersion, this, out index);
				attachmentData.InternalAttachmentType = attachmentType;
				result = attachmentData;
			}
			return result;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000178D4 File Offset: 0x00015AD4
		private MimeAttachmentData GetAttachmentData(MimePart part, MimePart dataPart, int snapshotVersion)
		{
			MimeAttachmentData result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				int num;
				MimeAttachmentData attachmentData = this.GetAttachmentData(part, snapshotVersion, this, out num);
				attachmentData.InternalAttachmentType = InternalAttachmentType.Regular;
				attachmentData.DataPart = dataPart;
				result = attachmentData;
			}
			return result;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00017928 File Offset: 0x00015B28
		private MimeAttachmentData GetAttachmentData(MimePart part, int snapshotVersion, MessageImplementation message, out int index)
		{
			MimeAttachmentData result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				for (index = 0; index < this.mimeAttachments.InternalList.Count; index++)
				{
					MimeAttachmentData mimeAttachmentData = this.mimeAttachments.InternalList[index];
					if (mimeAttachmentData != null && mimeAttachmentData.AttachmentPart == part)
					{
						mimeAttachmentData.Referenced = true;
						mimeAttachmentData.FlushCache();
						return mimeAttachmentData;
					}
				}
				MimeAttachmentData mimeAttachmentData2 = new MimeAttachmentData(part, message);
				index = this.mimeAttachments.Add(mimeAttachmentData2);
				result = mimeAttachmentData2;
			}
			return result;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000179CC File Offset: 0x00015BCC
		private bool NeedPlaceholderBody()
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.mimeAttachments.Count == 0)
				{
					result = true;
				}
				else
				{
					MimeAttachmentData dataAtPublicIndex = this.mimeAttachments.GetDataAtPublicIndex(0);
					MimePart mimePart = dataAtPublicIndex.DataPart ?? dataAtPublicIndex.AttachmentPart;
					string contentType = mimePart.ContentType;
					bool flag = Utility.IsBodyContentType(contentType);
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00017A48 File Offset: 0x00015C48
		internal void SetBodyPartCharset(MimePart part, Charset charset)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				bool isSynchronized = this.IsSynchronized;
				this.SetPartCharset(part, charset.Name);
				if (isSynchronized)
				{
					this.UpdateMimeVersion();
				}
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00017A9C File Offset: 0x00015C9C
		private void GetCharsetFromMimeDocument(MimeDocument mimeDocument)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (mimeDocument.EffectiveHeaderDecodingOptions.Charset != null)
				{
					this.defaultBodyCharset = mimeDocument.EffectiveHeaderDecodingOptions.Charset;
				}
				if (this.defaultBodyCharset == null)
				{
					this.defaultBodyCharset = Charset.DefaultMimeCharset;
				}
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00017B08 File Offset: 0x00015D08
		[Conditional("DEBUG")]
		private void AssertNothingDetectedYet()
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				int num = 0;
				foreach (MimeAttachmentData mimeAttachmentData in this.mimeAttachments)
				{
					if (mimeAttachmentData != null && mimeAttachmentData.Referenced)
					{
						num++;
					}
				}
			}
		}

		// Token: 0x040003D9 RID: 985
		internal EmailRecipientCollection ToRecipients;

		// Token: 0x040003DA RID: 986
		internal EmailRecipientCollection CcRecipients;

		// Token: 0x040003DB RID: 987
		internal EmailRecipientCollection BccRecipients;

		// Token: 0x040003DC RID: 988
		internal EmailRecipientCollection BccFromOrgHeaderRecipients;

		// Token: 0x040003DD RID: 989
		internal EmailRecipientCollection ReplyToRecipients;

		// Token: 0x040003DE RID: 990
		internal EmailRecipient FromRecipient;

		// Token: 0x040003DF RID: 991
		internal EmailRecipient SenderRecipient;

		// Token: 0x040003E0 RID: 992
		internal EmailRecipient DntRecipient;

		// Token: 0x040003E1 RID: 993
		private static string[] smimeExtensions = new string[]
		{
			"p7m",
			"p7s",
			"p7c"
		};

		// Token: 0x040003E2 RID: 994
		private static int maxMessageIdLength = 1953;

		// Token: 0x040003E3 RID: 995
		private static KeyValuePair<string, string>[] actionToClassSuffix = new KeyValuePair<string, string>[]
		{
			new KeyValuePair<string, string>("delivered", "DR"),
			new KeyValuePair<string, string>("expanded", "Expanded.DR"),
			new KeyValuePair<string, string>("relayed", "Relayed.DR"),
			new KeyValuePair<string, string>("delayed", "Delayed.DR"),
			new KeyValuePair<string, string>("failed", "NDR")
		};

		// Token: 0x040003E4 RID: 996
		private static KeyValuePair<string, string>[] dispositionToClassSuffix = new KeyValuePair<string, string>[]
		{
			new KeyValuePair<string, string>("displayed", "IPNRN"),
			new KeyValuePair<string, string>("dispatched", "IPNRN"),
			new KeyValuePair<string, string>("processed", "IPNRN"),
			new KeyValuePair<string, string>("deleted", "IPNNRN"),
			new KeyValuePair<string, string>("denied", "IPNNRN"),
			new KeyValuePair<string, string>("failed", "IPNNRN")
		};

		// Token: 0x040003E5 RID: 997
		private PureMimeMessage.PureMimeMessageThreadAccessToken accessToken;

		// Token: 0x040003E6 RID: 998
		private MimeDocument mimeDocument;

		// Token: 0x040003E7 RID: 999
		private MimePart rootPart;

		// Token: 0x040003E8 RID: 1000
		private MessageType messageType;

		// Token: 0x040003E9 RID: 1001
		private BodyStructure bodyStructure;

		// Token: 0x040003EA RID: 1002
		private BodyTypes bodyTypes;

		// Token: 0x040003EB RID: 1003
		private bool rebuildStructureAtNextOpportunity;

		// Token: 0x040003EC RID: 1004
		private int version = -1;

		// Token: 0x040003ED RID: 1005
		private MimePart placeholderBody;

		// Token: 0x040003EE RID: 1006
		private MimePart multipartRelated;

		// Token: 0x040003EF RID: 1007
		private MimePart multipartMixed;

		// Token: 0x040003F0 RID: 1008
		private MimePart multipartAlternative;

		// Token: 0x040003F1 RID: 1009
		private MimePart multipartSigned;

		// Token: 0x040003F2 RID: 1010
		private MimePart signaturePart;

		// Token: 0x040003F3 RID: 1011
		private string messageClass = "IPM.Note";

		// Token: 0x040003F4 RID: 1012
		private MimePart tnefPart;

		// Token: 0x040003F5 RID: 1013
		private PureCalendarMessage calendarMessage;

		// Token: 0x040003F6 RID: 1014
		private MimePart calendarPart;

		// Token: 0x040003F7 RID: 1015
		private RelayStorage calendarRelayStorage;

		// Token: 0x040003F8 RID: 1016
		private List<MimePart> bodyList;

		// Token: 0x040003F9 RID: 1017
		private MimePart bodyMimePart;

		// Token: 0x040003FA RID: 1018
		private MimePart bodyWriteStreamMimePart;

		// Token: 0x040003FB RID: 1019
		private BodyData bodyData = new BodyData();

		// Token: 0x040003FC RID: 1020
		private AttachmentDataCollection<MimeAttachmentData> mimeAttachments = new AttachmentDataCollection<MimeAttachmentData>();

		// Token: 0x040003FD RID: 1021
		private Charset defaultBodyCharset;

		// Token: 0x020000F5 RID: 245
		private class PureMimeMessageThreadAccessToken : ObjectThreadAccessToken
		{
			// Token: 0x060006F6 RID: 1782 RVA: 0x00017D03 File Offset: 0x00015F03
			internal PureMimeMessageThreadAccessToken(PureMimeMessage parent)
			{
			}
		}
	}
}

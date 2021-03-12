using System;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Approval;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000069 RID: 105
	internal class MessageItemApprovalRequest : IDisposable
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00011A68 File Offset: 0x0000FC68
		protected MessageItemApprovalRequest(MessageItem messageItem, OrganizationId organizationId)
		{
			this.messageItem = messageItem;
			this.organizationId = organizationId;
			this.creationTime = DateTime.UtcNow;
		}

		// Token: 0x17000157 RID: 343
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00011A89 File Offset: 0x0000FC89
		public RoutingAddress ApprovalRequestor
		{
			set
			{
				if (value.IsValid && value != RoutingAddress.NullReversePath)
				{
					this.approvalRequestor = new RoutingAddress?(value);
					this.messageItem[MessageItemSchema.ApprovalRequestor] = (string)value;
				}
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00011AC3 File Offset: 0x0000FCC3
		internal MessageItem MessageItem
		{
			get
			{
				return this.messageItem;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00011ACB File Offset: 0x0000FCCB
		private byte[] Buffer
		{
			get
			{
				if (this.buffer == null)
				{
					this.buffer = new byte[4096];
				}
				return this.buffer;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00011AEC File Offset: 0x0000FCEC
		public static MessageItemApprovalRequest Create(MbxTransportMailItem mbxTransportMailItem)
		{
			MessageItem messageItem = MessageItem.CreateInMemory(MessageItemApprovalRequest.PrefetchProperties);
			return new MessageItemApprovalRequest(messageItem, mbxTransportMailItem.OrganizationId);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00011B10 File Offset: 0x0000FD10
		public void SetSender(RoutingAddress address)
		{
			Participant sender = new Participant(string.Empty, (string)address, "smtp");
			this.messageItem.Sender = sender;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00011B40 File Offset: 0x0000FD40
		public void AddRecipient(RoutingAddress address, bool sendToThisRecipient)
		{
			Participant participant = new Participant(string.Empty, (string)address, "smtp");
			Recipient recipient = this.messageItem.Recipients.Add(participant, RecipientItemType.To);
			recipient[ItemSchema.Responsibility] = sendToThisRecipient;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00011B88 File Offset: 0x0000FD88
		public void AddVotingOption(string votingOption, bool allowComments)
		{
			VotingInfo.OptionData data = default(VotingInfo.OptionData);
			data.DisplayName = votingOption;
			data.SendPrompt = (allowComments ? VotingInfo.SendPrompt.VotingOption : VotingInfo.SendPrompt.Send);
			this.messageItem.VotingInfo.AddOption(data);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		public void AddAttachment(Attachment attachment, IRecipientSession adRecipientSession)
		{
			bool flag = false;
			MimePart mimePart = attachment.MimePart;
			Header header = null;
			if (mimePart != null)
			{
				header = mimePart.Headers.FindFirst("X-MS-Exchange-Organization-Approval-AttachToApprovalRequest");
			}
			string text;
			if (header != null && header.TryGetValue(out text))
			{
				if (text.Equals("Never"))
				{
					return;
				}
				if (text.Equals("AsMessage"))
				{
					flag = true;
				}
			}
			if (flag)
			{
				using (Stream contentReadStream = attachment.GetContentReadStream())
				{
					using (ItemAttachment itemAttachment = (ItemAttachment)this.messageItem.AttachmentCollection.Create(AttachmentType.EmbeddedMessage))
					{
						using (Item item = itemAttachment.GetItem())
						{
							ItemConversion.ConvertAnyMimeToItem(item, contentReadStream, new InboundConversionOptions(Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName)
							{
								UserADSession = adRecipientSession
							});
							item[MessageItemSchema.Flags] = MessageFlags.None;
							item.Save(SaveMode.NoConflictResolution);
							string valueOrDefault = item.GetValueOrDefault<string>(ItemSchema.Subject);
							if (!string.IsNullOrEmpty(valueOrDefault))
							{
								itemAttachment[AttachmentSchema.DisplayName] = valueOrDefault;
							}
							itemAttachment.Save();
						}
					}
					return;
				}
			}
			using (StreamAttachment streamAttachment = (StreamAttachment)this.messageItem.AttachmentCollection.Create(AttachmentType.Stream))
			{
				streamAttachment.FileName = attachment.FileName;
				using (Stream contentStream = streamAttachment.GetContentStream())
				{
					using (Stream contentReadStream2 = attachment.GetContentReadStream())
					{
						ApprovalProcessor.CopyStream(contentReadStream2, contentStream, this.Buffer);
					}
					contentStream.Flush();
				}
				streamAttachment.Save();
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00011DB0 File Offset: 0x0000FFB0
		public void SetBody(Body body)
		{
			BodyWriteConfiguration configuration = new BodyWriteConfiguration(this.GetXsoBodyFormat(body), body.CharsetName);
			Body body2 = this.messageItem.Body;
			using (Stream stream = body2.OpenWriteStream(configuration))
			{
				using (Stream contentReadStream = body.GetContentReadStream())
				{
					ApprovalProcessor.CopyStream(contentReadStream, stream, this.Buffer);
				}
				stream.Flush();
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00011E34 File Offset: 0x00010034
		public void SetBody(string body)
		{
			using (TextWriter textWriter = this.messageItem.Body.OpenTextWriter(Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml))
			{
				textWriter.Write(body);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00011E78 File Offset: 0x00010078
		public void Send(string messageId, byte[] corelationBlob, MbxTransportMailItem mbxTransportMailItem)
		{
			this.messageItem.VotingInfo.MessageCorrelationBlob = corelationBlob;
			this.messageItem.ClassName = "IPM.Note.Microsoft.Approval.Request";
			this.messageItem.InternetMessageId = messageId;
			this.messageItem.Save(SaveMode.NoConflictResolution);
			using (MemorySubmissionItem memorySubmissionItem = new MemorySubmissionItem(this.messageItem, this.organizationId))
			{
				memorySubmissionItem.Submit(MessageTrackingSource.APPROVAL, new MemorySubmissionItem.OnConvertedToTransportMailItemDelegate(this.TransportMailItemHandler), mbxTransportMailItem);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00011F04 File Offset: 0x00010104
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00011F14 File Offset: 0x00010114
		protected virtual bool TransportMailItemHandler(TransportMailItem mailItem, bool isValid)
		{
			if (!isValid)
			{
				return false;
			}
			if (this.approvalRequestor != null)
			{
				mailItem.Message.Sender = new EmailRecipient(null, this.messageItem.Sender.EmailAddress);
				mailItem.Message.From = new EmailRecipient(null, (string)this.approvalRequestor.Value);
				mailItem.Message.ReplyTo.Add(new EmailRecipient(null, this.messageItem.Sender.EmailAddress));
			}
			else
			{
				mailItem.Message.From = new EmailRecipient(null, this.messageItem.Sender.EmailAddress);
				mailItem.Message.ReplyTo.Add(new EmailRecipient(null, this.messageItem.Sender.EmailAddress));
			}
			mailItem.RootPart.Headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-Mapi-Admin-Submission", string.Empty));
			mailItem.RootPart.Headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-OriginalArrivalTime", Util.FormatOrganizationalMessageArrivalTime(this.creationTime)));
			Header newChild = Header.Create("X-MS-Exchange-Forest-RulesExecuted");
			mailItem.RootPart.Headers.AppendChild(newChild);
			return true;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00012048 File Offset: 0x00010248
		private void Dispose(bool disposing)
		{
			if (disposing && this.messageItem != null)
			{
				this.messageItem.Dispose();
				this.messageItem = null;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00012068 File Offset: 0x00010268
		private Microsoft.Exchange.Data.Storage.BodyFormat GetXsoBodyFormat(Body body)
		{
			switch (body.BodyFormat)
			{
			case Microsoft.Exchange.Data.Transport.Email.BodyFormat.Text:
				return Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain;
			case Microsoft.Exchange.Data.Transport.Email.BodyFormat.Html:
				return Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml;
			}
			return Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain;
		}

		// Token: 0x04000219 RID: 537
		private const string Smtp = "smtp";

		// Token: 0x0400021A RID: 538
		private const int BufferSize = 4096;

		// Token: 0x0400021B RID: 539
		internal static readonly StorePropertyDefinition[] PrefetchProperties = StoreObjectSchema.ContentConversionProperties;

		// Token: 0x0400021C RID: 540
		private readonly DateTime creationTime;

		// Token: 0x0400021D RID: 541
		private MessageItem messageItem;

		// Token: 0x0400021E RID: 542
		private RoutingAddress? approvalRequestor;

		// Token: 0x0400021F RID: 543
		private byte[] buffer;

		// Token: 0x04000220 RID: 544
		private OrganizationId organizationId;
	}
}

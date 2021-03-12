using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x0200002E RID: 46
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnchorEmailMessageItem : DisposeTrackableBase, IAnchorEmailMessageItem, IAnchorAttachmentMessage, IDisposable
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x0000787E File Offset: 0x00005A7E
		internal AnchorEmailMessageItem(AnchorContext context, IAnchorADProvider adProvider, MessageItem message)
		{
			this.context = context;
			this.ADProvider = adProvider;
			this.Message = message;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000789B File Offset: 0x00005A9B
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x000078A3 File Offset: 0x00005AA3
		private IAnchorADProvider ADProvider { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000078AC File Offset: 0x00005AAC
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x000078B4 File Offset: 0x00005AB4
		private MessageItem Message { get; set; }

		// Token: 0x060001F7 RID: 503 RVA: 0x000078C0 File Offset: 0x00005AC0
		public void Send(IEnumerable<SmtpAddress> recipientAddresses, string subject, string body)
		{
			AnchorUtil.ThrowOnCollectionEmptyArgument(recipientAddresses, "recipientAddresses");
			AnchorUtil.ThrowOnNullOrEmptyArgument(subject, "subject");
			AnchorUtil.ThrowOnNullOrEmptyArgument(body, "body");
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SmtpAddress smtpAddress in recipientAddresses)
			{
				this.Message.Recipients.Add(this.CreateRecipient(smtpAddress.ToString()), RecipientItemType.To);
				stringBuilder.Append(smtpAddress.ToString());
				stringBuilder.Append(';');
			}
			this.Message.AutoResponseSuppress = AutoResponseSuppress.All;
			this.Message.Subject = subject;
			using (TextWriter textWriter = this.Message.Body.OpenTextWriter(BodyFormat.TextHtml))
			{
				textWriter.Write(body);
			}
			this.context.Logger.Log(MigrationEventType.Information, "Sending report email to {0}, subject {1}", new object[]
			{
				stringBuilder,
				subject
			});
			this.Message.SendWithoutSavingMessage();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000079E8 File Offset: 0x00005BE8
		public AnchorAttachment CreateAttachment(string name)
		{
			base.CheckDisposed();
			return AnchorMessageHelper.CreateAttachment(this.context, this.Message, name);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007A02 File Offset: 0x00005C02
		public AnchorAttachment GetAttachment(string name, PropertyOpenMode openMode)
		{
			base.CheckDisposed();
			return AnchorMessageHelper.GetAttachment(this.context, this.Message, name, openMode);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007A1D File Offset: 0x00005C1D
		public void DeleteAttachment(string name)
		{
			base.CheckDisposed();
			AnchorMessageHelper.DeleteAttachment(this.Message, name);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007A31 File Offset: 0x00005C31
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.Message != null)
			{
				this.Message.Dispose();
				this.Message = null;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007A50 File Offset: 0x00005C50
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AnchorEmailMessageItem>(this);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007A58 File Offset: 0x00005C58
		private Participant CreateRecipient(string emailAddress)
		{
			ADRecipient adrecipientByProxyAddress = this.ADProvider.GetADRecipientByProxyAddress(emailAddress);
			if (adrecipientByProxyAddress != null)
			{
				return new Participant(adrecipientByProxyAddress);
			}
			return new Participant(emailAddress, emailAddress, "SMTP");
		}

		// Token: 0x0400008F RID: 143
		private AnchorContext context;
	}
}

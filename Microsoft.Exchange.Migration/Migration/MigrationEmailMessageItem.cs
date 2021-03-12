using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000C5 RID: 197
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationEmailMessageItem : DisposeTrackableBase, IMigrationEmailMessageItem, IMigrationAttachmentMessage, IDisposable
	{
		// Token: 0x06000A73 RID: 2675 RVA: 0x0002BD76 File Offset: 0x00029F76
		internal MigrationEmailMessageItem(MigrationDataProvider dataProvider, MessageItem message)
		{
			this.DataProvider = dataProvider;
			this.Message = message;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0002BD8C File Offset: 0x00029F8C
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x0002BD94 File Offset: 0x00029F94
		private MigrationDataProvider DataProvider { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0002BD9D File Offset: 0x00029F9D
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x0002BDA5 File Offset: 0x00029FA5
		private MessageItem Message { get; set; }

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002BDB0 File Offset: 0x00029FB0
		public void Send(IEnumerable<SmtpAddress> recipientAddresses, string subject, string body)
		{
			MigrationUtil.ThrowOnCollectionEmptyArgument(recipientAddresses, "recipientAddresses");
			MigrationUtil.ThrowOnNullOrEmptyArgument(subject, "subject");
			MigrationUtil.ThrowOnNullOrEmptyArgument(body, "body");
			Participant participant = new Participant(this.DataProvider.ADProvider.PrimaryExchangeRecipient);
			this.Message.Sender = participant;
			this.Message.From = participant;
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
			MigrationLogger.Log(MigrationEventType.Information, "Sending report email to {0}, subject {1}", new object[]
			{
				stringBuilder,
				subject
			});
			this.Message.SendWithoutSavingMessage();
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002BF00 File Offset: 0x0002A100
		public IMigrationAttachment CreateAttachment(string name)
		{
			base.CheckDisposed();
			return MigrationMessageHelper.CreateAttachment(this.Message, name);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002BF14 File Offset: 0x0002A114
		public bool TryGetAttachment(string name, PropertyOpenMode openMode, out IMigrationAttachment attachment)
		{
			base.CheckDisposed();
			return MigrationMessageHelper.TryGetAttachment(this.Message, name, openMode, out attachment);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002BF2A File Offset: 0x0002A12A
		public IMigrationAttachment GetAttachment(string name, PropertyOpenMode openMode)
		{
			base.CheckDisposed();
			return MigrationMessageHelper.GetAttachment(this.Message, name, openMode);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002BF3F File Offset: 0x0002A13F
		public void DeleteAttachment(string name)
		{
			base.CheckDisposed();
			MigrationMessageHelper.DeleteAttachment(this.Message, name);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002BF53 File Offset: 0x0002A153
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.Message != null)
			{
				this.Message.Dispose();
				this.Message = null;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002BF72 File Offset: 0x0002A172
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationEmailMessageItem>(this);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002BF7C File Offset: 0x0002A17C
		private Participant CreateRecipient(string emailAddress)
		{
			ADRecipient adrecipientByProxyAddress = this.DataProvider.ADProvider.GetADRecipientByProxyAddress(emailAddress);
			if (adrecipientByProxyAddress != null)
			{
				return new Participant(adrecipientByProxyAddress);
			}
			return new Participant(emailAddress, emailAddress, "SMTP");
		}
	}
}

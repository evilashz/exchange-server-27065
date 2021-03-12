using System;

namespace Microsoft.Exchange.Data.Transport.StoreDriver
{
	// Token: 0x020000A0 RID: 160
	internal abstract class SubmissionAgent : StoreDriverAgent
	{
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000399 RID: 921 RVA: 0x00008973 File Offset: 0x00006B73
		// (remove) Token: 0x0600039A RID: 922 RVA: 0x00008981 File Offset: 0x00006B81
		protected event DemotedMessageEventHandler OnDemotedMessage
		{
			add
			{
				base.AddHandler("OnDemotedMessage", value);
			}
			remove
			{
				base.RemoveHandler("OnDemotedMessage");
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000898E File Offset: 0x00006B8E
		internal override string SnapshotPrefix
		{
			get
			{
				return "Submission";
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00008995 File Offset: 0x00006B95
		internal override void AsyncComplete()
		{
			base.AsyncComplete();
			base.EnsureMimeWriteStreamClosed();
			base.MailItem = null;
			base.EventArgId = null;
			base.SnapshotWriter = null;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000089B8 File Offset: 0x00006BB8
		internal override void Invoke(string eventTopic, object source, object e)
		{
			Delegate @delegate = (Delegate)base.Handlers[eventTopic];
			if (@delegate == null)
			{
				return;
			}
			StoreDriverEventSource source2 = (StoreDriverEventSource)source;
			StoreDriverSubmissionEventArgs storeDriverSubmissionEventArgs = (StoreDriverSubmissionEventArgs)e;
			base.MailItem = storeDriverSubmissionEventArgs.MailItem;
			if (eventTopic != null && eventTopic == "OnDemotedMessage")
			{
				((SmtpServer)this.HostState).AddressBook.RecipientCache = storeDriverSubmissionEventArgs.MailItem.RecipientCache;
				((DemotedMessageEventHandler)@delegate)(source2, storeDriverSubmissionEventArgs);
				if (base.Synchronous)
				{
					base.EnsureMimeWriteStreamClosed();
					((SmtpServer)this.HostState).AddressBook.RecipientCache = null;
					base.MailItem = null;
				}
				return;
			}
			throw new InvalidOperationException("Internal error: unsupported event topic: " + (eventTopic ?? "null"));
		}
	}
}

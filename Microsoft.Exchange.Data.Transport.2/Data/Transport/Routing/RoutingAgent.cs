using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Transport.Routing
{
	// Token: 0x02000082 RID: 130
	public abstract class RoutingAgent : Agent
	{
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060002EB RID: 747 RVA: 0x000076AF File Offset: 0x000058AF
		// (remove) Token: 0x060002EC RID: 748 RVA: 0x000076BD File Offset: 0x000058BD
		protected event SubmittedMessageEventHandler OnSubmittedMessage
		{
			add
			{
				base.AddHandler("OnSubmittedMessage", value);
			}
			remove
			{
				base.RemoveHandler("OnSubmittedMessage");
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060002ED RID: 749 RVA: 0x000076CA File Offset: 0x000058CA
		// (remove) Token: 0x060002EE RID: 750 RVA: 0x000076D8 File Offset: 0x000058D8
		protected event ResolvedMessageEventHandler OnResolvedMessage
		{
			add
			{
				base.AddHandler("OnResolvedMessage", value);
			}
			remove
			{
				base.RemoveHandler("OnResolvedMessage");
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060002EF RID: 751 RVA: 0x000076E5 File Offset: 0x000058E5
		// (remove) Token: 0x060002F0 RID: 752 RVA: 0x000076F3 File Offset: 0x000058F3
		protected event RoutedMessageEventHandler OnRoutedMessage
		{
			add
			{
				base.AddHandler("OnRoutedMessage", value);
			}
			remove
			{
				base.RemoveHandler("OnRoutedMessage");
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060002F1 RID: 753 RVA: 0x00007700 File Offset: 0x00005900
		// (remove) Token: 0x060002F2 RID: 754 RVA: 0x0000770E File Offset: 0x0000590E
		protected event CategorizedMessageEventHandler OnCategorizedMessage
		{
			add
			{
				base.AddHandler("OnCategorizedMessage", value);
			}
			remove
			{
				base.RemoveHandler("OnCategorizedMessage");
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000771B File Offset: 0x0000591B
		internal override string SnapshotPrefix
		{
			get
			{
				return "Routing";
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000772A File Offset: 0x0000592A
		internal override object HostState
		{
			get
			{
				return base.HostStateInternal;
			}
			set
			{
				base.HostStateInternal = value;
				((SmtpServer)base.HostStateInternal).AssociatedAgent = this;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00007744 File Offset: 0x00005944
		internal override void AsyncComplete()
		{
			base.EnsureMimeWriteStreamClosed();
			base.MailItem = null;
			((SmtpServer)this.HostState).AddressBook.RecipientCache = null;
			base.EventArgId = null;
			base.SnapshotWriter = null;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00007778 File Offset: 0x00005978
		internal override void Invoke(string eventTopic, object source, object e)
		{
			QueuedMessageEventArgs queuedMessageEventArgs = (QueuedMessageEventArgs)e;
			base.MailItem = queuedMessageEventArgs.MailItem;
			if (queuedMessageEventArgs.MailItem.PipelineTracingEnabled && base.SnapshotEnabled)
			{
				base.SnapshotWriter = queuedMessageEventArgs.MailItem.SnapshotWriter;
				base.EventArgId = queuedMessageEventArgs.MailItem.InternalMessageId.ToString(CultureInfo.InvariantCulture);
				if (base.SnapshotWriter != null && eventTopic == "OnSubmittedMessage")
				{
					base.SnapshotWriter.WriteOriginalData(this.GetHashCode(), base.EventArgId, eventTopic, null, base.MailItem);
				}
			}
			Delegate @delegate = (Delegate)base.Handlers[eventTopic];
			if (@delegate == null)
			{
				base.EventArgId = null;
				base.SnapshotWriter = null;
				return;
			}
			if (base.SnapshotWriter != null)
			{
				base.SnapshotWriter.WritePreProcessedData(this.GetHashCode(), "Routing", base.EventArgId, eventTopic, base.MailItem);
			}
			if (eventTopic != null)
			{
				if (!(eventTopic == "OnSubmittedMessage"))
				{
					if (!(eventTopic == "OnResolvedMessage"))
					{
						if (!(eventTopic == "OnRoutedMessage"))
						{
							if (eventTopic == "OnCategorizedMessage")
							{
								this.SetRecipientCache(queuedMessageEventArgs);
								((CategorizedMessageEventHandler)@delegate)((CategorizedMessageEventSource)source, queuedMessageEventArgs);
							}
						}
						else
						{
							this.SetRecipientCache(queuedMessageEventArgs);
							((RoutedMessageEventHandler)@delegate)((RoutedMessageEventSource)source, queuedMessageEventArgs);
						}
					}
					else
					{
						this.SetRecipientCache(queuedMessageEventArgs);
						((ResolvedMessageEventHandler)@delegate)((ResolvedMessageEventSource)source, queuedMessageEventArgs);
					}
				}
				else
				{
					this.SetRecipientCache(queuedMessageEventArgs);
					((SubmittedMessageEventHandler)@delegate)((SubmittedMessageEventSource)source, queuedMessageEventArgs);
				}
			}
			if (base.Synchronous)
			{
				base.EnsureMimeWriteStreamClosed();
				if (base.SnapshotWriter != null)
				{
					base.SnapshotWriter.WriteProcessedData("Routing", base.EventArgId, eventTopic, base.Name, base.MailItem);
					base.EventArgId = null;
					base.SnapshotWriter = null;
				}
				((SmtpServer)this.HostState).AddressBook.RecipientCache = null;
				base.MailItem = null;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000796B File Offset: 0x00005B6B
		private void SetRecipientCache(QueuedMessageEventArgs eventArgs)
		{
			((SmtpServer)this.HostState).AddressBook.RecipientCache = eventArgs.MailItem.RecipientCache;
		}

		// Token: 0x040001E6 RID: 486
		private const string SnapshotFileNamePrefix = "Routing";
	}
}

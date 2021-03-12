using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001D3 RID: 467
	internal class Expansion
	{
		// Token: 0x06001535 RID: 5429 RVA: 0x0005563E File Offset: 0x0005383E
		public Expansion(Resolver resolver)
		{
			this.resolver = resolver;
			this.template = MessageTemplate.StripHistory;
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x00055658 File Offset: 0x00053858
		public TransportMailItem MailItem
		{
			get
			{
				return this.resolver.MailItem;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00055665 File Offset: 0x00053865
		public TaskContext TaskContext
		{
			get
			{
				return this.resolver.TaskContext;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00055672 File Offset: 0x00053872
		public ResolverMessage Message
		{
			get
			{
				return this.resolver.Message;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0005567F File Offset: 0x0005387F
		public Sender Sender
		{
			get
			{
				return this.resolver.Sender;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0005568C File Offset: 0x0005388C
		public ResolverConfiguration Configuration
		{
			get
			{
				return this.Resolver.Configuration;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x00055699 File Offset: 0x00053899
		public Resolver Resolver
		{
			get
			{
				return this.resolver;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x000556A1 File Offset: 0x000538A1
		public bool BypassChildModeration
		{
			get
			{
				return this.template.BypassChildModeration;
			}
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x000556B0 File Offset: 0x000538B0
		public static Expansion Resume(MailRecipient recipient, Resolver resolver)
		{
			return new Expansion(resolver)
			{
				template = MessageTemplate.ReadFrom(recipient),
				history = History.ReadFrom(recipient)
			};
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x000556E0 File Offset: 0x000538E0
		public ExpansionDisposition Expand(MessageTemplate template, HistoryType historyType, RoutingAddress historyAddress, RecipientP2Type recipientP2Type, out Expansion child)
		{
			child = null;
			bool flag = false;
			bool flag2 = true;
			if (this.history != null)
			{
				flag = this.history.Contains(historyAddress, out flag2);
				if (flag)
				{
					ExTraceGlobals.ResolverTracer.TraceDebug<bool>((long)this.GetHashCode(), "loop found in recipient history. reportable = {0}", flag2);
				}
			}
			if (!flag && this.Message.History != null)
			{
				bool flag3;
				flag = this.Message.History.Contains(historyAddress, out flag3);
				flag2 = (flag2 && flag3);
				if (flag)
				{
					ExTraceGlobals.ResolverTracer.TraceDebug<bool, bool>((long)this.GetHashCode(), "loop found in message history. reportable = {0}, message reportable = {1}", flag2, flag3);
				}
			}
			if (!flag)
			{
				child = new Expansion(this.resolver);
				child.template = this.template.Derive(template);
				child.history = History.Derive(this.history, historyType, historyAddress, recipientP2Type);
				return ExpansionDisposition.Expanded;
			}
			if (flag2)
			{
				return ExpansionDisposition.ReportableLoopDetected;
			}
			return ExpansionDisposition.NonreportableLoopDetected;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x000557C0 File Offset: 0x000539C0
		public void Add(RecipientItem recipient, bool processInOriginalMailItem)
		{
			ExTraceGlobals.ResolverTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "adding recipient {0}. ", recipient.Recipient.Email);
			string text;
			if (!recipient.Recipient.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Transport.MessageTemplate", out text))
			{
				this.template.WriteTo(recipient.Recipient);
			}
			if (this.history != null)
			{
				this.history.WriteTo(recipient.Recipient);
			}
			if (processInOriginalMailItem)
			{
				this.Resolver.RecipientStack.Push(recipient);
			}
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0005584B File Offset: 0x00053A4B
		public MailRecipient Add(TransportMiniRecipient entry, DsnRequestedFlags dsnRequested)
		{
			return this.Add(entry, dsnRequested, null);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00055858 File Offset: 0x00053A58
		public MailRecipient Add(TransportMiniRecipient entry, DsnRequestedFlags dsnRequested, string orcpt)
		{
			string primarySmtpAddress = DirectoryItem.GetPrimarySmtpAddress(entry);
			if (primarySmtpAddress == null)
			{
				return null;
			}
			bool processInOriginalMailItem;
			MailRecipient mailRecipient = this.resolver.AddRecipient(primarySmtpAddress, entry, dsnRequested, orcpt, true, out processInOriginalMailItem);
			RecipientItem recipientItem = RecipientItem.Create(mailRecipient);
			this.Add(recipientItem, processInOriginalMailItem);
			recipientItem.AddItemVisited(this);
			return mailRecipient;
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0005589C File Offset: 0x00053A9C
		public RecipientItem AddGroupExpansionItem(TransportMiniRecipient entry, DsnRequestedFlags dsnFlags)
		{
			string primarySmtpAddress = DirectoryItem.GetPrimarySmtpAddress(entry);
			if (primarySmtpAddress == null)
			{
				return null;
			}
			bool processInOriginalMailItem;
			MailRecipient recipient = this.resolver.AddRecipient(primarySmtpAddress, entry, dsnFlags, null, false, out processInOriginalMailItem);
			RecipientItem recipientItem = RecipientItem.Create(recipient);
			this.Add(recipientItem, processInOriginalMailItem);
			return recipientItem;
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x000558D8 File Offset: 0x00053AD8
		public void CacheSerializedHistory()
		{
			if (this.history != null)
			{
				this.history.CacheSerializedHistory();
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x000558F3 File Offset: 0x00053AF3
		public void ClearSerializedHistory()
		{
			if (this.history != null)
			{
				this.history.ClearSerializedHistory();
			}
		}

		// Token: 0x04000A9E RID: 2718
		private Resolver resolver;

		// Token: 0x04000A9F RID: 2719
		private MessageTemplate template;

		// Token: 0x04000AA0 RID: 2720
		private History history;
	}
}

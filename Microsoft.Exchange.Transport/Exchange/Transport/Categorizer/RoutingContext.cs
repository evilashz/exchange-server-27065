using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000254 RID: 596
	internal class RoutingContext
	{
		// Token: 0x060019CF RID: 6607 RVA: 0x00069BE0 File Offset: 0x00067DE0
		public RoutingContext(TransportMailItem mailItem, RoutingTables routingTables, RoutingContextCore contextCore, TaskContext taskContext)
		{
			RoutingUtils.ThrowIfNull(mailItem, "mailItem");
			RoutingUtils.ThrowIfNull(routingTables, "routingTables");
			RoutingUtils.ThrowIfNull(contextCore, "contextCore");
			this.mailItem = mailItem;
			this.contextCore = contextCore;
			this.routingTables = routingTables;
			this.messageSize = -1L;
			this.taskContext = taskContext;
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x00069C39 File Offset: 0x00067E39
		public TransportMailItem MailItem
		{
			get
			{
				return this.mailItem;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x00069C41 File Offset: 0x00067E41
		// (set) Token: 0x060019D2 RID: 6610 RVA: 0x00069C49 File Offset: 0x00067E49
		public MailRecipient CurrentRecipient
		{
			get
			{
				return this.currentRecipient;
			}
			set
			{
				this.currentRecipient = value;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x00069C52 File Offset: 0x00067E52
		public RoutingContextCore Core
		{
			get
			{
				return this.contextCore;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x00069C5A File Offset: 0x00067E5A
		public RoutingTables RoutingTables
		{
			get
			{
				return this.routingTables;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x00069C62 File Offset: 0x00067E62
		public TaskContext TaskContext
		{
			get
			{
				return this.taskContext;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x00069C6C File Offset: 0x00067E6C
		public long MessageSize
		{
			get
			{
				if (this.messageSize < 0L)
				{
					this.messageSize = this.GetOriginalMessageSize();
					if (this.messageSize < 0L)
					{
						throw new InvalidOperationException("GetOriginalMessageSize() must not return a negative value: " + this.messageSize);
					}
				}
				return this.messageSize;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x00069CBA File Offset: 0x00067EBA
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x00069CC2 File Offset: 0x00067EC2
		public bool? ShouldMakeMailItemShadowed
		{
			get
			{
				return this.shouldMakeMailItemShadowed;
			}
			set
			{
				this.shouldMakeMailItemShadowed = value;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x00069CCB File Offset: 0x00067ECB
		public DateTime Timestamp
		{
			get
			{
				return this.routingTables.WhenCreated;
			}
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00069CD8 File Offset: 0x00067ED8
		public void RegisterCurrentRecipientForAction(Guid actionId, RoutingContext.ActionOnRecipients action)
		{
			RoutingContext.RecipientSetWithAction recipientSetWithAction = null;
			if (this.actions == null)
			{
				this.actions = new Dictionary<Guid, RoutingContext.RecipientSetWithAction>();
			}
			if (this.actions.TryGetValue(actionId, out recipientSetWithAction))
			{
				recipientSetWithAction.AddRecipient(this.currentRecipient);
				return;
			}
			recipientSetWithAction = new RoutingContext.RecipientSetWithAction(this.currentRecipient, action);
			this.actions.Add(actionId, recipientSetWithAction);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00069D34 File Offset: 0x00067F34
		public void ExecuteActions()
		{
			if (this.actions == null)
			{
				return;
			}
			foreach (KeyValuePair<Guid, RoutingContext.RecipientSetWithAction> keyValuePair in this.actions)
			{
				keyValuePair.Value.ExecuteAction(keyValuePair.Key, this);
			}
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00069DA0 File Offset: 0x00067FA0
		private long GetOriginalMessageSize()
		{
			return ResolverMessage.GetOriginalMessageSize(this.mailItem.RootPart.Headers, this.mailItem.MimeSize);
		}

		// Token: 0x04000C53 RID: 3155
		private RoutingContextCore contextCore;

		// Token: 0x04000C54 RID: 3156
		private RoutingTables routingTables;

		// Token: 0x04000C55 RID: 3157
		private TransportMailItem mailItem;

		// Token: 0x04000C56 RID: 3158
		private MailRecipient currentRecipient;

		// Token: 0x04000C57 RID: 3159
		private long messageSize;

		// Token: 0x04000C58 RID: 3160
		private Dictionary<Guid, RoutingContext.RecipientSetWithAction> actions;

		// Token: 0x04000C59 RID: 3161
		private bool? shouldMakeMailItemShadowed;

		// Token: 0x04000C5A RID: 3162
		private TaskContext taskContext;

		// Token: 0x02000255 RID: 597
		// (Invoke) Token: 0x060019DE RID: 6622
		internal delegate void ActionOnRecipients(Guid actionId, ICollection<MailRecipient> recipients, RoutingContext context);

		// Token: 0x02000256 RID: 598
		private class RecipientSetWithAction
		{
			// Token: 0x060019E1 RID: 6625 RVA: 0x00069DC2 File Offset: 0x00067FC2
			public RecipientSetWithAction(MailRecipient recipient, RoutingContext.ActionOnRecipients action)
			{
				RoutingUtils.ThrowIfNull(recipient, "recipient");
				RoutingUtils.ThrowIfNull(action, "action");
				this.recipients = new List<MailRecipient>();
				this.recipients.Add(recipient);
				this.action = action;
			}

			// Token: 0x060019E2 RID: 6626 RVA: 0x00069DFE File Offset: 0x00067FFE
			public void AddRecipient(MailRecipient recipient)
			{
				RoutingUtils.ThrowIfNull(recipient, "recipient");
				this.recipients.Add(recipient);
			}

			// Token: 0x060019E3 RID: 6627 RVA: 0x00069E17 File Offset: 0x00068017
			public void ExecuteAction(Guid actionId, RoutingContext context)
			{
				this.action(actionId, this.recipients, context);
			}

			// Token: 0x04000C5B RID: 3163
			private List<MailRecipient> recipients;

			// Token: 0x04000C5C RID: 3164
			private RoutingContext.ActionOnRecipients action;
		}
	}
}

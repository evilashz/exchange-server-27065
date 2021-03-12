using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001CA RID: 458
	internal abstract class RecipientItem
	{
		// Token: 0x060014E2 RID: 5346 RVA: 0x00053BD6 File Offset: 0x00051DD6
		protected RecipientItem(MailRecipient recipient)
		{
			this.recipient = recipient;
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00053BE5 File Offset: 0x00051DE5
		public RoutingAddress Email
		{
			get
			{
				return this.recipient.Email;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00053BF2 File Offset: 0x00051DF2
		public bool TopLevelRecipient
		{
			get
			{
				return this.topLevelRecipient;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00053BFA File Offset: 0x00051DFA
		public MailRecipient Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x00053C02 File Offset: 0x00051E02
		public static RecipientItem Create(MailRecipient recipient)
		{
			return RecipientItem.Create(recipient, false);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x00053C0C File Offset: 0x00051E0C
		public static RecipientItem Create(MailRecipient recipient, bool isTopLevelRecipient)
		{
			if (!Resolver.IsResolved(recipient))
			{
				return null;
			}
			RecipientItem recipientItem = DirectoryItem.Create(recipient);
			if (recipientItem == null)
			{
				recipientItem = new OneOffItem(recipient);
			}
			recipientItem.topLevelRecipient = isTopLevelRecipient;
			return recipientItem;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00053C3C File Offset: 0x00051E3C
		public void Process(Expansion expansion)
		{
			this.PreProcess(expansion);
			if (this.Recipient.IsProcessed)
			{
				return;
			}
			this.PostProcess(expansion);
		}

		// Token: 0x060014E9 RID: 5353
		public abstract void PreProcess(Expansion expansion);

		// Token: 0x060014EA RID: 5354
		public abstract void PostProcess(Expansion expansion);

		// Token: 0x060014EB RID: 5355 RVA: 0x00053C5A File Offset: 0x00051E5A
		public virtual void AddItemVisited(Expansion expansion)
		{
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00053C5C File Offset: 0x00051E5C
		public override string ToString()
		{
			return this.Email.ToString();
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00053C80 File Offset: 0x00051E80
		protected void ApplyTemplate(MessageTemplate template)
		{
			MessageTemplate messageTemplate = MessageTemplate.ReadFrom(this.recipient);
			MessageTemplate messageTemplate2 = messageTemplate.Derive(template);
			messageTemplate2.WriteTo(this.recipient);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00053CAD File Offset: 0x00051EAD
		protected void FailRecipient(SmtpResponse response)
		{
			Resolver.FailRecipient(this.recipient, response);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00053CBB File Offset: 0x00051EBB
		protected Expansion Expand(Expansion parent, HistoryType historyType)
		{
			return this.Expand(parent, MessageTemplate.Default, historyType);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00053CCC File Offset: 0x00051ECC
		protected Expansion Expand(Expansion parent, MessageTemplate template, HistoryType historyType)
		{
			Expansion result = null;
			int num;
			RecipientP2Type recipientP2Type;
			if (!this.recipient.ExtendedProperties.TryGetValue<int>("Microsoft.Exchange.Transport.RecipientP2Type", out num))
			{
				recipientP2Type = RecipientP2Type.Unknown;
			}
			else
			{
				recipientP2Type = (RecipientP2Type)num;
			}
			switch (parent.Expand(template, historyType, this.recipient.Email, recipientP2Type, out result))
			{
			case ExpansionDisposition.Expanded:
				return result;
			case ExpansionDisposition.NonreportableLoopDetected:
				this.recipient.Ack(AckStatus.SuccessNoDsn, AckReason.SilentExpansionLoopDetected);
				return null;
			}
			this.FailRecipient(AckReason.ExpansionLoopDetected);
			if (Resolver.PerfCounters != null)
			{
				Resolver.PerfCounters.LoopRecipientsTotal.Increment();
			}
			return null;
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00053D64 File Offset: 0x00051F64
		protected virtual bool CheckDeliveryRestrictions(Expansion expansion)
		{
			long num;
			RestrictionCheckResult restrictionCheckResult = DeliveryRestriction.CheckRestriction(this as RestrictedItem, expansion.Sender, expansion.Resolver.IsAuthenticated, expansion.MailItem.IsJournalReport(), expansion.Message.OriginalMessageSize, expansion.Configuration.MaxReceiveSize, expansion.Resolver.Configuration.PrivilegedSenders, null, expansion.MailItem.OrganizationId, out num);
			ExTraceGlobals.ResolverTracer.TraceDebug((long)this.GetHashCode(), "Restriction Check returns {0}: recipient {1} sender {2} authenticated {3} stream size {4}", new object[]
			{
				(int)restrictionCheckResult,
				this.Recipient,
				expansion.Sender,
				expansion.Resolver.IsAuthenticated,
				expansion.Message.OriginalMessageSize
			});
			if (ADRecipientRestriction.Failed(restrictionCheckResult))
			{
				if (restrictionCheckResult == (RestrictionCheckResult)2147483649U)
				{
					this.recipient.AddDsnParameters("MaxRecipMessageSizeInKB", num);
					this.recipient.AddDsnParameters("CurrentMessageSizeInKB", expansion.Message.OriginalMessageSize >> 10);
				}
				this.FailRecipient(DeliveryRestriction.GetResponseForResult(restrictionCheckResult));
				return false;
			}
			return true;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00053E84 File Offset: 0x00052084
		protected T GetProperty<T>(string name)
		{
			T result;
			this.Recipient.ExtendedProperties.TryGetValue<T>(name, out result);
			return result;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00053EA6 File Offset: 0x000520A6
		protected T GetProperty<T>(string name, T defaultValue)
		{
			return this.Recipient.ExtendedProperties.GetValue<T>(name, defaultValue);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00053EBC File Offset: 0x000520BC
		protected IList<ItemT> GetListProperty<ItemT>(string name)
		{
			ReadOnlyCollection<ItemT> result;
			this.Recipient.ExtendedProperties.TryGetListValue<ItemT>(name, out result);
			return result;
		}

		// Token: 0x04000A88 RID: 2696
		private MailRecipient recipient;

		// Token: 0x04000A89 RID: 2697
		private bool topLevelRecipient;
	}
}

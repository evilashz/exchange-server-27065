using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020000F7 RID: 247
	internal class MailRecipientCollection : IDataExternalComponent, IDataObjectComponent, ICollection<MailRecipient>, IReadOnlyMailRecipientCollection, IEnumerable<MailRecipient>, IEnumerable, IMailRecipientCollectionFacade
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x00024076 File Offset: 0x00022276
		public MailRecipientCollection(TransportMailItem mailItem) : this(mailItem, new List<MailRecipient>())
		{
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00024084 File Offset: 0x00022284
		public MailRecipientCollection(TransportMailItem mailItem, List<MailRecipient> recipients)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			this.mailItem = mailItem;
			this.recipients = recipients;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x000240B6 File Offset: 0x000222B6
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x000240B9 File Offset: 0x000222B9
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x000240BC File Offset: 0x000222BC
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x000240BF File Offset: 0x000222BF
		public int Count
		{
			get
			{
				return this.recipients.Count;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x000240CC File Offset: 0x000222CC
		public IEnumerable<MailRecipient> All
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x000240DC File Offset: 0x000222DC
		public bool PendingDatabaseUpdates
		{
			get
			{
				return this.recipients.Any((MailRecipient recipient) => recipient.PendingDatabaseUpdates);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0002410E File Offset: 0x0002230E
		public int PendingDatabaseUpdateCount
		{
			get
			{
				return this.recipients.Count((MailRecipient recipient) => recipient.PendingDatabaseUpdates);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0002414D File Offset: 0x0002234D
		public IEnumerable<MailRecipient> AllUnprocessed
		{
			get
			{
				return from recipient in this.recipients
				where recipient.IsActive && !recipient.IsProcessed
				select recipient;
			}
		}

		// Token: 0x1700029A RID: 666
		public MailRecipient this[int index]
		{
			get
			{
				return this.recipients[index];
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00024185 File Offset: 0x00022385
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002418D File Offset: 0x0002238D
		public IEnumerator<MailRecipient> GetEnumerator()
		{
			return this.recipients.GetEnumerator();
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000241B4 File Offset: 0x000223B4
		public int CountUnprocessed()
		{
			return this.recipients.Count((MailRecipient recipient) => recipient.IsActive && !recipient.IsProcessed);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000241DE File Offset: 0x000223DE
		public void CopyTo(Array array, int index)
		{
			this.CopyTo(array as MailRecipient[], index);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000241ED File Offset: 0x000223ED
		public void CopyTo(MailRecipient[] array, int index)
		{
			this.recipients.CopyTo(array, index);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000241FC File Offset: 0x000223FC
		public void Clear()
		{
			foreach (MailRecipient mailRecipient in this.recipients)
			{
				mailRecipient.ReleaseFromActive();
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00024250 File Offset: 0x00022450
		public void CloneFrom(IDataObjectComponent other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00024258 File Offset: 0x00022458
		void IDataExternalComponent.MarkToDelete()
		{
			foreach (MailRecipient mailRecipient in this.recipients)
			{
				mailRecipient.MarkToDelete();
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000242AC File Offset: 0x000224AC
		public void SaveToExternalRow(Transaction transaction)
		{
			for (int i = this.recipients.Count - 1; i >= 0; i--)
			{
				MailRecipient mailRecipient = this.recipients[i];
				mailRecipient.Commit(transaction);
				if (mailRecipient.IsRowDeleted || !mailRecipient.IsActive)
				{
					this.recipients.RemoveAt(i);
				}
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00024304 File Offset: 0x00022504
		public void MinimizeMemory()
		{
			foreach (MailRecipient mailRecipient in this.recipients)
			{
				mailRecipient.MinimizeMemory();
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00024358 File Offset: 0x00022558
		internal void Sort(IComparer<MailRecipient> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			this.recipients.Sort(comparer);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00024374 File Offset: 0x00022574
		internal void RemoveInternal(MailRecipient item)
		{
			this.recipients.Remove(item);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00024383 File Offset: 0x00022583
		public bool Contains(MailRecipient item)
		{
			return this.recipients.Contains(item);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00024394 File Offset: 0x00022594
		public MailRecipient Add(string smtpAddress)
		{
			MailRecipient mailRecipient = MailRecipient.NewMessageRecipient(this.mailItem);
			mailRecipient.Email = new RoutingAddress(smtpAddress);
			return mailRecipient;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000243BA File Offset: 0x000225BA
		public void Add(MailRecipient recip)
		{
			if (recip == null)
			{
				throw new ArgumentNullException("recip");
			}
			recip.Attach(this.mailItem);
			this.recipients.Add(recip);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000243E2 File Offset: 0x000225E2
		public void Prepend(MailRecipient recip)
		{
			if (recip == null)
			{
				throw new ArgumentNullException("recip");
			}
			recip.Attach(this.mailItem);
			this.recipients.Insert(0, recip);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002440C File Offset: 0x0002260C
		public bool Remove(MailRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (!this.recipients.Contains(recipient) || !recipient.IsActive)
			{
				return false;
			}
			bool result = !recipient.IsProcessed;
			recipient.ReleaseFromActive();
			return result;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00024450 File Offset: 0x00022650
		public bool Remove(MailRecipient recipient, DsnType dsnType, SmtpResponse smtpResponse)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (!this.recipients.Contains(recipient) || !recipient.IsActive || recipient.IsProcessed)
			{
				return false;
			}
			AckStatus ackStatus = AckStatus.Success;
			if (dsnType == DsnType.Failure)
			{
				ackStatus = AckStatus.Fail;
			}
			else if (dsnType == DsnType.Expanded)
			{
				ackStatus = AckStatus.Expand;
			}
			recipient.Ack(ackStatus, smtpResponse);
			return true;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000244A4 File Offset: 0x000226A4
		public void RemoveDuplicates()
		{
			if (this.recipients.Count < 2)
			{
				return;
			}
			HashSet<RoutingAddress> hashSet = new HashSet<RoutingAddress>();
			foreach (MailRecipient mailRecipient in this.AllUnprocessed)
			{
				if (!hashSet.Add(mailRecipient.Email))
				{
					MailRecipientCollection.Tracer.TraceDebug<RoutingAddress>(0L, "Removing duplicate recipient {0}", mailRecipient.Email);
					mailRecipient.Ack(AckStatus.SuccessNoDsn, AckReason.DuplicateRecipient);
				}
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00024530 File Offset: 0x00022730
		public void RemoveDuplicatesToSameRoute()
		{
			if (this.recipients.Count < 2)
			{
				return;
			}
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (MailRecipient mailRecipient in this.AllUnprocessed)
			{
				string text = mailRecipient.Email.ToString();
				RoutingOverride routingOverride = mailRecipient.RoutingOverride;
				if (routingOverride != null)
				{
					text = text + "@" + routingOverride.ToString();
				}
				if (!hashSet.Add(text))
				{
					MailRecipientCollection.Tracer.TraceDebug<RoutingAddress>(0L, "Removing duplicate recipient {0}", mailRecipient.Email);
					mailRecipient.Ack(AckStatus.SuccessNoDsn, AckReason.DuplicateRecipient);
				}
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x000245F4 File Offset: 0x000227F4
		void IDataExternalComponent.ParentPrimaryKeyChanged()
		{
			foreach (MailRecipient mailRecipient in this.recipients)
			{
				if (!mailRecipient.IsRowDeleted)
				{
					mailRecipient.UpdateOwnerId();
				}
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00024650 File Offset: 0x00022850
		void IMailRecipientCollectionFacade.Add(string smtpAddress)
		{
			this.Add(smtpAddress);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002465C File Offset: 0x0002285C
		void IMailRecipientCollectionFacade.AddWithoutDsnRequested(string smtpAddress)
		{
			MailRecipient mailRecipient = this.Add(smtpAddress);
			mailRecipient.DsnRequested = DsnRequestedFlags.Never;
		}

		// Token: 0x0400044D RID: 1101
		private static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;

		// Token: 0x0400044E RID: 1102
		private readonly List<MailRecipient> recipients;

		// Token: 0x0400044F RID: 1103
		private readonly TransportMailItem mailItem;
	}
}

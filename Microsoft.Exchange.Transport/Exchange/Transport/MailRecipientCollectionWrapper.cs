using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000184 RID: 388
	internal class MailRecipientCollectionWrapper : EnvelopeRecipientCollection
	{
		// Token: 0x060010E0 RID: 4320 RVA: 0x00044CC3 File Offset: 0x00042EC3
		internal MailRecipientCollectionWrapper(TransportMailItem mailItem, IMExSession mexSession, bool canAdd)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			this.mailItem = mailItem;
			this.mexSession = mexSession;
			this.canAdd = canAdd;
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00044CEE File Offset: 0x00042EEE
		public override int Count
		{
			get
			{
				return this.mailItem.Recipients.CountUnprocessed();
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x00044D00 File Offset: 0x00042F00
		public override bool CanAdd
		{
			get
			{
				return this.canAdd;
			}
		}

		// Token: 0x17000473 RID: 1139
		public override EnvelopeRecipient this[int index]
		{
			get
			{
				MailRecipient nonDeletedItem = this.GetNonDeletedItem(index);
				if (nonDeletedItem == null)
				{
					throw new IndexOutOfRangeException(Strings.IndexOutOfBounds(index, this.Count));
				}
				return new MailRecipientWrapper(nonDeletedItem, this.mailItem);
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00044D44 File Offset: 0x00042F44
		public override void Clear()
		{
			foreach (MailRecipient mailRecipient in this.mailItem.Recipients.AllUnprocessed)
			{
				this.TrackRecipientFail(mailRecipient.Email, mailRecipient.SmtpResponse, null);
				mailRecipient.ReleaseFromActive();
			}
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00044DB0 File Offset: 0x00042FB0
		public override bool Contains(RoutingAddress address)
		{
			foreach (MailRecipient mailRecipient in this.mailItem.Recipients.AllUnprocessed)
			{
				if (mailRecipient.Email.Equals(address))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00044E18 File Offset: 0x00043018
		public override void Add(RoutingAddress address)
		{
			if (!this.canAdd)
			{
				throw new NotSupportedException();
			}
			if (!address.IsValid)
			{
				throw new ArgumentException("address", Strings.InvalidSmtpAddress(address.ToString()));
			}
			string agentName = null;
			if (this.mexSession != null)
			{
				agentName = (this.mexSession.ExecutingAgentName ?? "Agent");
			}
			this.mailItem.Recipients.Add((string)address);
			MessageTrackingLog.TrackRecipientAdd(MessageTrackingSource.AGENT, this.mailItem, address, null, agentName);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00044EAB File Offset: 0x000430AB
		public override bool Remove(EnvelopeRecipient recipient)
		{
			return this.Remove(recipient, true);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00044EB8 File Offset: 0x000430B8
		public override int Remove(RoutingAddress address)
		{
			int num = 0;
			for (int i = this.mailItem.Recipients.Count - 1; i >= 0; i--)
			{
				MailRecipient mailRecipient = this.mailItem.Recipients[i];
				if (mailRecipient.IsActive && !mailRecipient.IsProcessed && mailRecipient.Email.CompareTo(address) == 0)
				{
					this.TrackRecipientFail(mailRecipient.Email, mailRecipient.SmtpResponse, null);
					if (this.mailItem.Recipients.Remove(mailRecipient))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00044F42 File Offset: 0x00043142
		public override bool Remove(EnvelopeRecipient recipient, DsnType dsnType, SmtpResponse smtpResponse)
		{
			return this.Remove(recipient, dsnType, smtpResponse, true, null);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00044F4F File Offset: 0x0004314F
		public override bool Remove(EnvelopeRecipient recipient, DsnType dsnType, SmtpResponse smtpResponse, string sourceContext)
		{
			return this.Remove(recipient, dsnType, smtpResponse, true, sourceContext);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00044F70 File Offset: 0x00043170
		public override EnvelopeRecipientCollection.Enumerator GetEnumerator()
		{
			return new EnvelopeRecipientCollection.Enumerator(this.mailItem.Recipients.AllUnprocessed, (object a) => new MailRecipientWrapper((MailRecipient)a, this.mailItem));
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00044F94 File Offset: 0x00043194
		public bool Remove(EnvelopeRecipient recipient, bool trackRecipientFail)
		{
			MailRecipientWrapper mailRecipientWrapper = recipient as MailRecipientWrapper;
			if (mailRecipientWrapper == null)
			{
				throw new ArgumentException("Does not match any valid recipient type.", "recipient");
			}
			if (!this.mailItem.Recipients.Contains(mailRecipientWrapper.MailRecipient) || !mailRecipientWrapper.MailRecipient.IsActive || mailRecipientWrapper.MailRecipient.IsProcessed)
			{
				return false;
			}
			if (trackRecipientFail)
			{
				this.TrackRecipientFail(mailRecipientWrapper.MailRecipient.Email, mailRecipientWrapper.MailRecipient.SmtpResponse, null);
			}
			mailRecipientWrapper.MailRecipient.ReleaseFromActive();
			return true;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0004501C File Offset: 0x0004321C
		public bool Remove(EnvelopeRecipient recipient, DsnType dsnType, SmtpResponse smtpResponse, bool trackRecipientFail, string sourceContext)
		{
			MailRecipientWrapper mailRecipientWrapper = recipient as MailRecipientWrapper;
			if (mailRecipientWrapper == null)
			{
				throw new ArgumentException("Does not match any valid recipient type.", "recipient");
			}
			if (!this.mailItem.Recipients.Contains(mailRecipientWrapper.MailRecipient) || !mailRecipientWrapper.MailRecipient.IsActive || mailRecipientWrapper.MailRecipient.IsProcessed)
			{
				return false;
			}
			mailRecipientWrapper.MailRecipient.SmtpResponse = smtpResponse;
			if (trackRecipientFail)
			{
				this.TrackRecipientFail(mailRecipientWrapper.MailRecipient.Email, smtpResponse, sourceContext);
			}
			return this.mailItem.Recipients.Remove(mailRecipientWrapper.MailRecipient, dsnType, smtpResponse);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000450B4 File Offset: 0x000432B4
		private MailRecipient GetNonDeletedItem(int index)
		{
			int num = 0;
			foreach (MailRecipient result in this.mailItem.Recipients.AllUnprocessed)
			{
				if (num == index)
				{
					return result;
				}
				num++;
			}
			return null;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00045118 File Offset: 0x00043318
		private void TrackRecipientFail(RoutingAddress email, SmtpResponse smtpResponse, string sourceContext)
		{
			if (string.IsNullOrEmpty(sourceContext) && this.mexSession != null)
			{
				sourceContext = (this.mexSession.ExecutingAgentName ?? "Agent");
			}
			LatencyFormatter latencyFormatter = new LatencyFormatter(this.mailItem, Components.Configuration.LocalServer.TransportServer.Fqdn, true);
			MessageTrackingLog.TrackRecipientFail(MessageTrackingSource.AGENT, this.mailItem, email, smtpResponse.Equals(SmtpResponse.Empty) ? AckReason.MessageDeletedByTransportAgent : smtpResponse, sourceContext, latencyFormatter);
		}

		// Token: 0x04000919 RID: 2329
		private const string DefaultAgentName = "Agent";

		// Token: 0x0400091A RID: 2330
		private TransportMailItem mailItem;

		// Token: 0x0400091B RID: 2331
		private IMExSession mexSession;

		// Token: 0x0400091C RID: 2332
		private bool canAdd;
	}
}

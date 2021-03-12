using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Agent.AntiSpam.Common
{
	// Token: 0x02000008 RID: 8
	internal class BypassedRecipients
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000314C File Offset: 0x0000134C
		public BypassedRecipients(MultiValuedProperty<SmtpAddress> recipients, AddressBook addressBook)
		{
			if (recipients != null)
			{
				this.bypassedRecipients = new Dictionary<RoutingAddress, bool>();
				foreach (SmtpAddress smtpAddress in recipients)
				{
					this.bypassedRecipients.Add((RoutingAddress)smtpAddress.ToString(), true);
				}
			}
			this.addressBook = addressBook;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000031CC File Offset: 0x000013CC
		public AddressBook AddressBook
		{
			get
			{
				return this.addressBook;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000031D4 File Offset: 0x000013D4
		public int NumBypassedRecipients(EnvelopeRecipientCollection recipients)
		{
			int num = 0;
			ReadOnlyCollection<AddressBookEntry> readOnlyCollection = null;
			if (recipients == null || recipients.Count == 0)
			{
				return 0;
			}
			CommonUtils.TryAddressBookFind(this.addressBook, recipients, out readOnlyCollection);
			for (int i = 0; i < recipients.Count; i++)
			{
				EnvelopeRecipient envelopeRecipient = recipients[i];
				AddressBookEntry addressBookEntry = (readOnlyCollection != null) ? readOnlyCollection[i] : null;
				if (this.IsBypassed(envelopeRecipient.Address, addressBookEntry))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003240 File Offset: 0x00001440
		public bool IsBypassed(RoutingAddress recipient)
		{
			AddressBookEntry addressBookEntry = null;
			if (this.addressBook != null)
			{
				CommonUtils.TryAddressBookFind(this.addressBook, recipient, out addressBookEntry);
			}
			return this.IsBypassed(recipient, addressBookEntry);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000326E File Offset: 0x0000146E
		public bool IsBypassed(RoutingAddress recipient, AddressBookEntry addressBookEntry)
		{
			return (this.bypassedRecipients != null && this.bypassedRecipients.ContainsKey(recipient)) || (addressBookEntry != null && addressBookEntry.AntispamBypass);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003294 File Offset: 0x00001494
		public void RemoveNonBypassedRecipients(MailItem mailItem, bool reject, SmtpResponse response, string agentName, string eventTopic, EventArgs eventArgs, SmtpSession smtpSession, LogEntry logEntry)
		{
			List<EnvelopeRecipient> list = new List<EnvelopeRecipient>(mailItem.Recipients.Count);
			EnvelopeRecipientCollection recipients = mailItem.Recipients;
			foreach (EnvelopeRecipient envelopeRecipient in recipients)
			{
				if (!this.IsBypassed(envelopeRecipient.Address))
				{
					list.Add(envelopeRecipient);
				}
			}
			if (reject)
			{
				AgentLog.Instance.LogRejectRecipients(agentName, eventTopic, eventArgs, smtpSession, mailItem, list, response, logEntry);
				using (List<EnvelopeRecipient>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						EnvelopeRecipient recipient = enumerator2.Current;
						mailItem.Recipients.Remove(recipient, DsnType.Failure, response);
					}
					return;
				}
			}
			AgentLog.Instance.LogDeleteRecipients(agentName, eventTopic, eventArgs, smtpSession, mailItem, list, logEntry);
			foreach (EnvelopeRecipient recipient2 in list)
			{
				mailItem.Recipients.Remove(recipient2);
			}
		}

		// Token: 0x04000029 RID: 41
		private Dictionary<RoutingAddress, bool> bypassedRecipients;

		// Token: 0x0400002A RID: 42
		private AddressBook addressBook;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002F4 RID: 756
	internal class TrackedUser
	{
		// Token: 0x0600163F RID: 5695 RVA: 0x00067A08 File Offset: 0x00065C08
		private TrackedUser()
		{
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00067A10 File Offset: 0x00065C10
		private TrackedUser(string address)
		{
			this.smtpAddress = new SmtpAddress(address);
			this.proxyAddresses = new ProxyAddressCollection();
			this.proxyAddresses.Add(address);
			this.PopulateProxyHashSet();
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00067A50 File Offset: 0x00065C50
		private TrackedUser(ADRecipient mailbox)
		{
			if (mailbox.ExternalEmailAddress != null && mailbox.ExternalEmailAddress is SmtpProxyAddress)
			{
				this.smtpAddress = new SmtpAddress(mailbox.ExternalEmailAddress.AddressString);
			}
			else
			{
				SmtpAddress primarySmtpAddress = mailbox.PrimarySmtpAddress;
				if (!mailbox.PrimarySmtpAddress.IsValidAddress)
				{
					throw new TrackedUserCreationException("PrimarySmtpAddress is invalid: {0}", new object[]
					{
						mailbox.PrimarySmtpAddress
					});
				}
				this.smtpAddress = mailbox.PrimarySmtpAddress;
			}
			if (mailbox.EmailAddresses == null || mailbox.EmailAddresses.Count == 0)
			{
				throw new TrackedUserCreationException("No EmailAddresses", new object[0]);
			}
			ProxyAddressCollection proxyAddressCollection = mailbox.EmailAddresses;
			foreach (ProxyAddress proxyAddress in mailbox.EmailAddresses)
			{
				if (proxyAddress is InvalidProxyAddress)
				{
					TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Invalid proxies found, will use only valid ones", new object[0]);
					IEnumerable<ProxyAddress> source = from proxy in mailbox.EmailAddresses
					where !(proxy is InvalidProxyAddress)
					select proxy;
					proxyAddressCollection = new ProxyAddressCollection(source.ToArray<ProxyAddress>());
					break;
				}
			}
			if (proxyAddressCollection.Count == 0)
			{
				throw new TrackedUserCreationException("All EmailAddresses discarded as invalid", new object[0]);
			}
			this.proxyAddresses = proxyAddressCollection;
			this.adRecipient = mailbox;
			this.readStatusTrackingEnabled = !(bool)mailbox[ADRecipientSchema.MessageTrackingReadStatusDisabled];
			this.PopulateProxyHashSet();
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00067BEC File Offset: 0x00065DEC
		public static TrackedUser CreateUnresolved(string smtpAddress)
		{
			return new TrackedUser(smtpAddress);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00067BF4 File Offset: 0x00065DF4
		public static TrackedUser Create(ADUser user)
		{
			try
			{
				return new TrackedUser(user);
			}
			catch (TrackedUserCreationException arg)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<TrackedUserCreationException>(0, "TrackedUserCreationException initializing from ADRecipient: {0}", arg);
			}
			return null;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00067C34 File Offset: 0x00065E34
		public static TrackedUser Create(string smtpAddress, IRecipientSession galSession)
		{
			ProxyAddress proxyAddress = ProxyAddress.Parse(smtpAddress);
			ADRecipient adrecipient = null;
			try
			{
				adrecipient = galSession.FindByProxyAddress(proxyAddress);
			}
			catch (NonUniqueRecipientException arg)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ProxyAddress, NonUniqueRecipientException>(0, "Create from SmtpAddress: Caught NonUniqueRecipientException when attempting to look up user for address {0}, exception: {1}", proxyAddress, arg);
				return null;
			}
			if (adrecipient != null)
			{
				try
				{
					return new TrackedUser(adrecipient);
				}
				catch (TrackedUserCreationException arg2)
				{
					TraceWrapper.SearchLibraryTracer.TraceError<string, TrackedUserCreationException>(0, "Create from SmtpAddress: TrackedUserCreationException initializing from ADRecipient: {0}, {1}", smtpAddress, arg2);
					return null;
				}
			}
			return new TrackedUser(smtpAddress);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00067CB8 File Offset: 0x00065EB8
		public static TrackedUser Create(Guid guid, IRecipientSession galSession)
		{
			ADRecipient adrecipient = null;
			try
			{
				adrecipient = galSession.FindByExchangeGuid(guid);
			}
			catch (NonUniqueRecipientException arg)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<Guid, NonUniqueRecipientException>(0, "Create from GUID: Caught NonUniqueRecipientException when attempting to look up server for guid {0}, exception: {1}", guid, arg);
				return null;
			}
			if (adrecipient != null)
			{
				try
				{
					return new TrackedUser(adrecipient);
				}
				catch (TrackedUserCreationException arg2)
				{
					TraceWrapper.SearchLibraryTracer.TraceError<Guid, TrackedUserCreationException>(0, "Create from GUID: TrackedUserCreationException initializing from ADRecipient: {0}, {1}", guid, arg2);
				}
			}
			return null;
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x00067D28 File Offset: 0x00065F28
		public SmtpAddress SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x00067D30 File Offset: 0x00065F30
		public ProxyAddressCollection ProxyAddresses
		{
			get
			{
				return this.proxyAddresses;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x00067D38 File Offset: 0x00065F38
		public ADUser ADUser
		{
			get
			{
				return this.adRecipient as ADUser;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x00067D45 File Offset: 0x00065F45
		public ADRecipient ADRecipient
		{
			get
			{
				return this.adRecipient;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x00067D4D File Offset: 0x00065F4D
		public bool IsArbitrationMailbox
		{
			get
			{
				return this.adRecipient != null && this.adRecipient.RecipientTypeDetails == RecipientTypeDetails.ArbitrationMailbox;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x00067D6C File Offset: 0x00065F6C
		public bool IsMailbox
		{
			get
			{
				return this.adRecipient != null && this.adRecipient.RecipientType == RecipientType.UserMailbox;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x00067D86 File Offset: 0x00065F86
		public bool IsSupportedForTrackingAsSender
		{
			get
			{
				return this.adRecipient == null || this.adRecipient.RecipientType == RecipientType.MailUser || this.adRecipient.RecipientType == RecipientType.MailContact || this.adRecipient.RecipientType == RecipientType.UserMailbox;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x00067DBC File Offset: 0x00065FBC
		public string DisplayName
		{
			get
			{
				if (this.adRecipient != null && !string.IsNullOrEmpty(this.adRecipient.DisplayName))
				{
					return this.adRecipient.DisplayName;
				}
				return this.smtpAddress.ToString();
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00067DF5 File Offset: 0x00065FF5
		public bool ReadStatusTrackingEnabled
		{
			get
			{
				return this.readStatusTrackingEnabled;
			}
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00067DFD File Offset: 0x00065FFD
		public bool IsAddressOneOfProxies(string address)
		{
			return this.proxyAddressesHashset.Contains(address);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00067E0C File Offset: 0x0006600C
		private void PopulateProxyHashSet()
		{
			this.proxyAddressesHashset = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.proxyAddressesHashset.Add((string)this.smtpAddress);
			foreach (ProxyAddress proxyAddress in this.proxyAddresses)
			{
				this.proxyAddressesHashset.Add(proxyAddress.AddressString);
			}
		}

		// Token: 0x04000E70 RID: 3696
		private SmtpAddress smtpAddress;

		// Token: 0x04000E71 RID: 3697
		private ProxyAddressCollection proxyAddresses;

		// Token: 0x04000E72 RID: 3698
		private ADRecipient adRecipient;

		// Token: 0x04000E73 RID: 3699
		private bool readStatusTrackingEnabled;

		// Token: 0x04000E74 RID: 3700
		private HashSet<string> proxyAddressesHashset;
	}
}

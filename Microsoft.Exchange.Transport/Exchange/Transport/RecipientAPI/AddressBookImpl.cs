using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Transport.RecipientAPI
{
	// Token: 0x0200052E RID: 1326
	internal class AddressBookImpl : TransportAddressBook
	{
		// Token: 0x06003DC7 RID: 15815 RVA: 0x00103778 File Offset: 0x00101978
		internal AddressBookImpl() : this(null)
		{
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x00103784 File Offset: 0x00101984
		private OrganizationId OrganizationId
		{
			get
			{
				ADRecipientCache<TransportMiniRecipient> adrecipientCache = base.RecipientCache as ADRecipientCache<TransportMiniRecipient>;
				if (adrecipientCache == null)
				{
					return OrganizationId.ForestWideOrgId;
				}
				return adrecipientCache.OrganizationId ?? OrganizationId.ForestWideOrgId;
			}
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x001037B5 File Offset: 0x001019B5
		internal AddressBookImpl(IIsMemberOfResolver<RoutingAddress> memberOfResolver)
		{
			if (memberOfResolver != null)
			{
				this.memberOfResolver = memberOfResolver;
				return;
			}
			this.memberOfResolver = Components.TransportIsMemberOfResolverComponent.IsMemberOfResolver;
		}

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x001037D8 File Offset: 0x001019D8
		internal static ExEventLog EventLog
		{
			get
			{
				return AddressBookImpl.eventLog;
			}
		}

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06003DCB RID: 15819 RVA: 0x001037DF File Offset: 0x001019DF
		internal static bool UsingValidator
		{
			get
			{
				return null != AddressBookImpl.recipientValidator;
			}
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x001037EC File Offset: 0x001019EC
		public static bool UsingAdam()
		{
			if (AddressBookImpl.usingAdam == null)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole\\AdamSettings"))
				{
					AddressBookImpl.usingAdam = new bool?(null != registryKey);
				}
			}
			return AddressBookImpl.usingAdam.Value;
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x0010384C File Offset: 0x00101A4C
		public override bool Contains(RoutingAddress smtpAddress)
		{
			if (!Utility.IsValidAddress(smtpAddress))
			{
				return false;
			}
			if (AddressBookImpl.RecipientDoesNotExist(smtpAddress))
			{
				return false;
			}
			ADRecipientCache<TransportMiniRecipient> cache = this.GetCache();
			ProxyAddress proxyAddress = AddressBookImpl.CreateProxyAddress(smtpAddress);
			Result<TransportMiniRecipient> recipientEntry = cache.FindAndCacheRecipient(proxyAddress);
			AddressBookImpl.LogRecipientDataValidationExceptionIfNeeded(recipientEntry, smtpAddress);
			return null != recipientEntry.Data;
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x0010389C File Offset: 0x00101A9C
		public override AddressBookEntry Find(RoutingAddress smtpAddress)
		{
			if (!Utility.IsValidAddress(smtpAddress))
			{
				return null;
			}
			if (AddressBookImpl.RecipientDoesNotExist(smtpAddress))
			{
				return null;
			}
			ADRecipientCache<TransportMiniRecipient> cache = this.GetCache();
			ProxyAddress proxyAddress = AddressBookImpl.CreateProxyAddress(smtpAddress);
			Result<TransportMiniRecipient> recipientEntry = cache.FindAndCacheRecipient(proxyAddress);
			AddressBookImpl.LogRecipientDataValidationExceptionIfNeeded(recipientEntry, smtpAddress);
			if (recipientEntry.Data == null)
			{
				return null;
			}
			return AddressBookImpl.CreateAddressBookEntry(recipientEntry.Data, smtpAddress);
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x00103910 File Offset: 0x00101B10
		public override ReadOnlyCollection<AddressBookEntry> Find(params RoutingAddress[] smtpAddresses)
		{
			if (smtpAddresses == null)
			{
				throw new ArgumentNullException("smtpAddresses");
			}
			return this.Find(smtpAddresses.Length, (int index) => smtpAddresses[index]);
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x00103974 File Offset: 0x00101B74
		public override ReadOnlyCollection<AddressBookEntry> Find(EnvelopeRecipientCollection recipients)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			return this.Find(recipients.Count, (int index) => recipients[index].Address);
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x001039C0 File Offset: 0x00101BC0
		public override bool IsMemberOf(RoutingAddress recipientSmtpAddress, RoutingAddress groupSmtpAddress)
		{
			if (recipientSmtpAddress.IsValid && groupSmtpAddress.IsValid)
			{
				ADRecipientCache<TransportMiniRecipient> cache = this.GetCache();
				ProxyAddress proxyAddress = AddressBookImpl.CreateProxyAddress(recipientSmtpAddress);
				Result<TransportMiniRecipient> recipientEntry = cache.FindAndCacheRecipient(proxyAddress);
				AddressBookImpl.LogRecipientDataValidationExceptionIfNeeded(recipientEntry, recipientSmtpAddress);
				if (recipientEntry.Data != null)
				{
					return this.memberOfResolver.IsMemberOf(cache.ADSession, recipientEntry.Data.Id, groupSmtpAddress);
				}
			}
			return false;
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x00103A28 File Offset: 0x00101C28
		public override bool IsSameRecipient(RoutingAddress proxyAddressA, RoutingAddress proxyAddressB)
		{
			if (string.Equals((string)proxyAddressA, (string)proxyAddressB, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (!Utility.IsValidAddress(proxyAddressA) || !Utility.IsValidAddress(proxyAddressB))
			{
				return false;
			}
			if (AddressBookImpl.RecipientDoesNotExist(proxyAddressA) || AddressBookImpl.RecipientDoesNotExist(proxyAddressB))
			{
				return false;
			}
			ADRecipientCache<TransportMiniRecipient> cache = this.GetCache();
			ProxyAddress proxyAddress = AddressBookImpl.CreateProxyAddress(proxyAddressA);
			ProxyAddress proxyAddress2 = AddressBookImpl.CreateProxyAddress(proxyAddressB);
			bool flag = false;
			if (!cache.ContainsKey(proxyAddress) && cache.ContainsKey(proxyAddress2))
			{
				flag = true;
			}
			if (flag)
			{
				ProxyAddress proxyAddress3 = proxyAddress;
				proxyAddress = proxyAddress2;
				proxyAddress2 = proxyAddress3;
			}
			Result<TransportMiniRecipient> recipientEntry = cache.FindAndCacheRecipient(proxyAddress);
			AddressBookImpl.LogRecipientDataValidationExceptionIfNeeded(recipientEntry, flag ? proxyAddressB : proxyAddressA);
			if (recipientEntry.Data == null)
			{
				return false;
			}
			object obj = recipientEntry.Data[ADRecipientSchema.EmailAddresses];
			if (obj == null)
			{
				return false;
			}
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)obj;
			foreach (ProxyAddress proxyAddress4 in proxyAddressCollection)
			{
				if (proxyAddress4.Equals(proxyAddress2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x00103B3C File Offset: 0x00101D3C
		public override bool IsInternal(RoutingAddress address)
		{
			RoutingDomain domain;
			return RoutingDomain.TryParse(address.DomainPart, out domain) && this.IsInternal(domain);
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x00103B62 File Offset: 0x00101D62
		public override bool IsInternal(RoutingDomain domain)
		{
			return this.IsInternalTo(domain, this.OrganizationId, false);
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x00103B74 File Offset: 0x00101D74
		public override bool IsInternalTo(RoutingAddress address, RoutingAddress organizationAddress)
		{
			RoutingDomain organizationDomain;
			return RoutingDomain.TryParse(organizationAddress.DomainPart, out organizationDomain) && this.IsInternalTo(address, organizationDomain);
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x00103B9C File Offset: 0x00101D9C
		public override bool IsInternalTo(RoutingAddress address, RoutingDomain organizationDomain)
		{
			SmtpDomain domain;
			if (!SmtpDomain.TryParse(organizationDomain.Domain, out domain))
			{
				return false;
			}
			OrganizationId organizationId;
			if (Components.Configuration.FirstOrgAcceptedDomainTable.CheckAccepted(domain))
			{
				organizationId = OrganizationId.ForestWideOrgId;
			}
			else
			{
				organizationId = this.OrganizationId;
			}
			return this.IsInternalTo(address, organizationId, false);
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x00103BE8 File Offset: 0x00101DE8
		public bool IsInternalTo(RoutingAddress address, OrganizationId organizationId, bool acceptedDomainsOnly = false)
		{
			RoutingDomain domain;
			return RoutingDomain.TryParse(address.DomainPart, out domain) && this.IsInternalTo(domain, organizationId, acceptedDomainsOnly);
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x00103C10 File Offset: 0x00101E10
		public bool IsInternalTo(RoutingDomain domain, OrganizationId organizationId, bool acceptedDomainsOnly = false)
		{
			if (organizationId == null)
			{
				organizationId = this.OrganizationId;
			}
			return TransportIsInternalResolver.IsInternal(organizationId, domain, acceptedDomainsOnly);
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x00103C2B File Offset: 0x00101E2B
		public ADOperationResult TryGetIsInternal(RoutingAddress address, out bool isInternal)
		{
			return this.TryGetIsInternal(address, false, out isInternal);
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x00103C68 File Offset: 0x00101E68
		public override ADOperationResult TryGetIsInternal(RoutingAddress address, bool acceptedDomainsOnly, out bool isInternal)
		{
			isInternal = false;
			bool tempIsInternal = false;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				tempIsInternal = this.IsInternalTo(address, this.OrganizationId, acceptedDomainsOnly);
			}, 0);
			if (adoperationResult.Succeeded)
			{
				isInternal = tempIsInternal;
			}
			return adoperationResult;
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x00103CBE File Offset: 0x00101EBE
		internal static void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params string[] messageArgs)
		{
			AddressBookImpl.eventLog.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x00103CD0 File Offset: 0x00101ED0
		internal static ProxyAddress CreateProxyAddress(RoutingAddress smtpAddress)
		{
			ProxyAddress result;
			if (AddressBookImpl.UsingAdam())
			{
				string address = null;
				lock (AddressBookImpl.stringHasher)
				{
					address = ProxyAddressHasher.GetHashedFormWithoutPrefix(AddressBookImpl.stringHasher, (string)smtpAddress);
				}
				result = new CustomProxyAddress(AddressBookImpl.smtpHashPrefix, address, false);
			}
			else
			{
				result = new SmtpProxyAddress((string)smtpAddress, false);
			}
			return result;
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x00103D44 File Offset: 0x00101F44
		internal static AddressBookEntry CreateAddressBookEntry(TransportMiniRecipient entry, RoutingAddress address)
		{
			if (AddressBookImpl.UsingAdam())
			{
				return new AddressBookEntryImpl(entry, address);
			}
			return new AddressBookEntryImpl(entry);
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x00103D5C File Offset: 0x00101F5C
		private static RecipientValidator InitializeRecipientValidator(bool suppressDisposeTracker)
		{
			if (Components.Configuration.LocalServer.TransportServer.RecipientValidationCacheEnabled)
			{
				RecipientValidator recipientValidator = new RecipientValidator();
				if (recipientValidator.Initialize(suppressDisposeTracker))
				{
					return recipientValidator;
				}
			}
			return null;
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x00103D91 File Offset: 0x00101F91
		private static bool RecipientDoesNotExist(RoutingAddress smtpAddress)
		{
			return AddressBookImpl.recipientValidator != null && AddressBookImpl.recipientValidator.RecipientDoesNotExist(smtpAddress);
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x00103DA8 File Offset: 0x00101FA8
		private static void LogRecipientDataValidationExceptionIfNeeded(Result<TransportMiniRecipient> recipientEntry, RoutingAddress recipientRoutingAddress)
		{
			ValidationError validationError = recipientEntry.Error as ValidationError;
			if (validationError != null)
			{
				AddressBookImpl.eventLog.LogEvent(TransportEventLogConstants.Tuple_RecipientHasDataValidationException, recipientRoutingAddress.ToString(), new object[]
				{
					recipientRoutingAddress.ToString(),
					recipientEntry.Error
				});
			}
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x00103E04 File Offset: 0x00102004
		private ReadOnlyCollection<AddressBookEntry> Find(int count, GetAddress getAddress)
		{
			List<ProxyAddress> list = new List<ProxyAddress>(count);
			for (int i = 0; i < count; i++)
			{
				RoutingAddress routingAddress = getAddress(i);
				if (!Utility.IsValidAddress(routingAddress) || AddressBookImpl.RecipientDoesNotExist(routingAddress))
				{
					list.Add(null);
				}
				else
				{
					ProxyAddress item = AddressBookImpl.CreateProxyAddress(routingAddress);
					list.Add(item);
				}
			}
			ADRecipientCache<TransportMiniRecipient> cache = this.GetCache();
			IList<Result<TransportMiniRecipient>> list2 = cache.FindAndCacheRecipients(list);
			AddressBookEntry[] array = new AddressBookEntryImpl[list.Count];
			for (int j = 0; j < count; j++)
			{
				TransportMiniRecipient data = list2[j].Data;
				AddressBookImpl.LogRecipientDataValidationExceptionIfNeeded(list2[j], getAddress(j));
				if (data != null)
				{
					RoutingAddress address = getAddress(j);
					array[j] = AddressBookImpl.CreateAddressBookEntry(data, address);
				}
			}
			return new ReadOnlyCollection<AddressBookEntry>(array);
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x00103ECF File Offset: 0x001020CF
		private ADRecipientCache<TransportMiniRecipient> GetCache()
		{
			if (base.RecipientCache != null && base.RecipientCache is ADRecipientCache<TransportMiniRecipient>)
			{
				return (ADRecipientCache<TransportMiniRecipient>)base.RecipientCache;
			}
			return new ADRecipientCache<TransportMiniRecipient>(RecipientSchema.PropertyDefinitions, 5);
		}

		// Token: 0x04001F8E RID: 8078
		private static readonly ExEventLog eventLog = new ExEventLog(new Guid("8cd349b7-795a-47f7-b99e-6f6a7fb399e1"), TransportEventLog.GetEventSource());

		// Token: 0x04001F8F RID: 8079
		private static readonly RecipientValidator recipientValidator = AddressBookImpl.InitializeRecipientValidator(true);

		// Token: 0x04001F90 RID: 8080
		private static readonly CustomProxyAddressPrefix smtpHashPrefix = new CustomProxyAddressPrefix("sh", "SMTP hash");

		// Token: 0x04001F91 RID: 8081
		private static bool? usingAdam;

		// Token: 0x04001F92 RID: 8082
		private static readonly StringHasher stringHasher = new StringHasher();

		// Token: 0x04001F93 RID: 8083
		private readonly IIsMemberOfResolver<RoutingAddress> memberOfResolver;
	}
}

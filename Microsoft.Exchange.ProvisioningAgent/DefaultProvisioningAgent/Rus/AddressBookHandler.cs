﻿using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.ProvisioningAgent;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x0200003E RID: 62
	internal class AddressBookHandler : RusDataHandler
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x00009B50 File Offset: 0x00007D50
		public AddressBookHandler(string configurationDomainController, string recipientDomainController, string globalCatalog, NetworkCredential credential, PartitionId partitionId, UserScope userScope, ProvisioningCache provisioningCache, LogMessageDelegate logger) : base(configurationDomainController, recipientDomainController, globalCatalog, credential, partitionId, provisioningCache, logger)
		{
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00009B63 File Offset: 0x00007D63
		protected AddressBookDataProvider Provider
		{
			get
			{
				if (this.provider == null)
				{
					this.provider = new AddressBookDataProvider(base.ConfigurationSession, base.RootOrgConfigurationSession, base.ProvisioningCache);
				}
				return this.provider;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00009B90 File Offset: 0x00007D90
		protected LdapFilterProvider FilterProvider
		{
			get
			{
				if (this.filterProvider == null)
				{
					if (base.Credential != null)
					{
						this.filterProvider = LdapFilterProvider.GetLdapFilterProvider(base.ConfigurationDomainController, base.Credential);
					}
					else
					{
						this.filterProvider = LdapFilterProvider.GetLdapFilterProvider(base.CurrentOrganizationId.PartitionId);
					}
				}
				return this.filterProvider;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009BE4 File Offset: 0x00007DE4
		public ProvisioningValidationError[] Validate(AddressBookBase addressList)
		{
			if (addressList == null)
			{
				throw new ArgumentNullException("addressList");
			}
			ExTraceGlobals.RusTracer.TraceDebug<string>((long)this.GetHashCode(), "AddressBookHandler.Validate: addressList={0}", addressList.Identity.ToString());
			if (base.Logger != null)
			{
				base.Logger(Strings.VerboseToBeValidateObject(addressList.Identity.ToString(), addressList.GetType().Name));
			}
			if (addressList.IsModified(AddressBookBaseSchema.LdapRecipientFilter) && !string.IsNullOrEmpty(addressList.LdapRecipientFilter))
			{
				try
				{
					LdapFilter.Parse(addressList.LdapRecipientFilter, this.FilterProvider);
				}
				catch (LdapFilterException ex)
				{
					return new ProvisioningValidationError[]
					{
						new ProvisioningValidationError(Strings.ErrorInvalidLdapFilter(ex.Message), ExchangeErrorCategory.Client, ex)
					};
				}
			}
			return new ProvisioningValidationError[0];
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009CC0 File Offset: 0x00007EC0
		public bool UpdateRecipient(ADRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			ExTraceGlobals.RusTracer.TraceDebug<string>((long)this.GetHashCode(), "AddressBookHandler.UpdateRecipient: recipient={0}", recipient.Identity.ToString());
			if (string.IsNullOrEmpty(recipient.Alias) || recipient.IsSoftDeleted)
			{
				return false;
			}
			if (base.RecipientDomainController == null)
			{
				base.RecipientSession.DomainController = recipient.OriginatingServer;
			}
			base.CurrentOrganizationId = recipient.OrganizationId;
			List<ADRawEntry> list = new List<ADRawEntry>();
			IEnumerable<ADRawEntry> policies = this.Provider.GetPolicies(recipient.OrganizationId, base.Logger);
			foreach (ADRawEntry adrawEntry in policies)
			{
				if (base.Logger != null)
				{
					base.Logger(Strings.VerboseFoundAddressList(adrawEntry.Id.ToString()));
				}
				ADObjectId adobjectId = (ADObjectId)adrawEntry[AddressBookBaseSchema.RecipientContainer];
				if (adobjectId == null || recipient.DistinguishedName.EndsWith(adobjectId.DistinguishedName, StringComparison.InvariantCultureIgnoreCase))
				{
					string text = (string)adrawEntry[AddressBookBaseSchema.LdapRecipientFilter];
					bool flag = (bool)adrawEntry[AddressBookBaseSchema.IsSystemAddressList];
					if (!string.IsNullOrEmpty(text))
					{
						if (recipient.HiddenFromAddressListsEnabled)
						{
							if (!flag)
							{
								continue;
							}
						}
						try
						{
							this.FilterProvider.PrepareLdapFilter(text);
						}
						catch (LdapFilterException ex)
						{
							throw new RusException(Strings.ErrorAddressListInvalidLdapFilter(text, adrawEntry.Identity.ToString(), ex.Message), ex);
						}
						list.Add(adrawEntry);
					}
				}
			}
			if (list.Count != 0)
			{
				return this.UpdateAddressListMembership(list, recipient, this.FilterProvider);
			}
			if (recipient.AddressListMembership.Count != 0)
			{
				recipient.AddressListMembership.Clear();
				return true;
			}
			return false;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009E98 File Offset: 0x00008098
		private bool UpdateAddressListMembership(IEnumerable<ADRawEntry> addressBooks, ADRecipient recipient, LdapFilterProvider filterProvider)
		{
			string[] allAttributes = filterProvider.GetAllAttributes();
			object[] marshalledAttributes = RUSMarshal.MarshalWholeObject(recipient, base.RecipientSession, allAttributes);
			List<ADObjectId> list = new List<ADObjectId>();
			foreach (ADRawEntry adrawEntry in addressBooks)
			{
				string text = (string)adrawEntry[AddressBookBaseSchema.LdapRecipientFilter];
				ExTraceGlobals.RusTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "AddressBookHandler.UpdateAddressListMembership: evaluate recipient={0} against Address Book={1} with LdapRecipientFilter={2}", recipient.Identity.ToString(), adrawEntry.Identity.ToString(), text);
				if (!string.IsNullOrEmpty(text))
				{
					LdapFilter ldapFilter = filterProvider.PrepareLdapFilter(text);
					if (ldapFilter.Evaluate(marshalledAttributes))
					{
						ExTraceGlobals.RusTracer.TraceDebug<string, string>((long)this.GetHashCode(), "AddressBookHandler.UpdateAddressListMembership: add Address Book={0} to AddressListMembership of recipient={1}", adrawEntry.Identity.ToString(), recipient.Id.DistinguishedName);
						list.Add(adrawEntry.Id);
					}
				}
			}
			bool result = false;
			for (int i = recipient.AddressListMembership.Count - 1; i >= 0; i--)
			{
				bool flag = false;
				foreach (ADObjectId adobjectId in list)
				{
					if (adobjectId.ObjectGuid == recipient.AddressListMembership[i].ObjectGuid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					if (base.Logger != null)
					{
						base.Logger(Strings.VerboseRemoveAddressListMemberShip(recipient.AddressListMembership[i].ToString()));
					}
					recipient.AddressListMembership.RemoveAt(i);
					result = true;
				}
			}
			if (list.Count != recipient.AddressListMembership.Count)
			{
				result = true;
				foreach (ADObjectId adobjectId2 in list)
				{
					bool flag2 = false;
					foreach (ADObjectId adobjectId3 in recipient.AddressListMembership)
					{
						if (adobjectId2.ObjectGuid == adobjectId3.ObjectGuid)
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						if (base.Logger != null)
						{
							base.Logger(Strings.VerboseAddAddressListMemberShip(adobjectId2.ToString()));
						}
						recipient.AddressListMembership.Add(adobjectId2);
					}
				}
			}
			return result;
		}

		// Token: 0x040000CB RID: 203
		private AddressBookDataProvider provider;

		// Token: 0x040000CC RID: 204
		private LdapFilterProvider filterProvider;
	}
}

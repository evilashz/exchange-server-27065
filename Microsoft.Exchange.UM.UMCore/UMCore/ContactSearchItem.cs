using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000102 RID: 258
	internal class ContactSearchItem : IEquatable<ContactSearchItem>
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x0001BC90 File Offset: 0x00019E90
		private ContactSearchItem()
		{
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001BCA3 File Offset: 0x00019EA3
		internal static StorePropertyDefinition[] ContactSearchPropertyDefinitions
		{
			get
			{
				return ContactSearchItem.contactSearchPropertyDefinitions;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001BCAA File Offset: 0x00019EAA
		internal string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001BCB2 File Offset: 0x00019EB2
		internal string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001BCBA File Offset: 0x00019EBA
		internal PhoneNumber Phone
		{
			get
			{
				return this.phone;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001BCC2 File Offset: 0x00019EC2
		internal PhoneNumber HomePhone
		{
			get
			{
				return this.homePhone;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001BCCA File Offset: 0x00019ECA
		internal PhoneNumber BusinessPhone
		{
			get
			{
				return this.businessPhone;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001BCD2 File Offset: 0x00019ED2
		internal PhoneNumber BusinessPhoneForTts
		{
			get
			{
				return this.businessPhoneForTts;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001BCDA File Offset: 0x00019EDA
		internal PhoneNumber MobilePhone
		{
			get
			{
				return this.mobilePhone;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001BCE2 File Offset: 0x00019EE2
		internal string Alias
		{
			get
			{
				if (this.recipient == null)
				{
					return null;
				}
				return this.recipient.Alias;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0001BCF9 File Offset: 0x00019EF9
		internal bool HasAlias
		{
			get
			{
				return !string.IsNullOrEmpty(this.Alias);
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001BD09 File Offset: 0x00019F09
		internal bool HasBusinessAddress
		{
			get
			{
				return !this.businessAddress.IsEmpty;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001BD19 File Offset: 0x00019F19
		internal bool HasBusinessNumber
		{
			get
			{
				if (this.recipient != null)
				{
					return !PhoneNumber.IsNullOrEmpty(this.businessPhoneForTts);
				}
				return !PhoneNumber.IsNullOrEmpty(this.businessPhone);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001BD40 File Offset: 0x00019F40
		internal bool HasHomeAddress
		{
			get
			{
				return !string.IsNullOrEmpty(this.homeAddress);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001BD50 File Offset: 0x00019F50
		internal bool HasOtherAddress
		{
			get
			{
				return !string.IsNullOrEmpty(this.otherAddress);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001BD60 File Offset: 0x00019F60
		internal bool HasMobileNumber
		{
			get
			{
				return !PhoneNumber.IsNullOrEmpty(this.mobilePhone);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001BD70 File Offset: 0x00019F70
		internal bool HasHomeNumber
		{
			get
			{
				return !PhoneNumber.IsNullOrEmpty(this.homePhone);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001BD80 File Offset: 0x00019F80
		internal bool HasEmail1
		{
			get
			{
				return 0 < this.contactEmailAddresses.Count && !string.IsNullOrEmpty(this.contactEmailAddresses[0]);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001BDA6 File Offset: 0x00019FA6
		internal bool HasEmail2
		{
			get
			{
				return 1 < this.contactEmailAddresses.Count && !string.IsNullOrEmpty(this.contactEmailAddresses[1]);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001BDCC File Offset: 0x00019FCC
		internal bool HasEmail3
		{
			get
			{
				return 2 < this.contactEmailAddresses.Count && !string.IsNullOrEmpty(this.contactEmailAddresses[2]);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001BDF2 File Offset: 0x00019FF2
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0001BDFA File Offset: 0x00019FFA
		internal bool IsGroup { get; private set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001BE03 File Offset: 0x0001A003
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x0001BE0B File Offset: 0x0001A00B
		internal bool GroupHasEmail { get; private set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001BE14 File Offset: 0x0001A014
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0001BE1C File Offset: 0x0001A01C
		internal string PrimarySmtpAddress
		{
			get
			{
				return this.primarySmtpAddress;
			}
			set
			{
				this.primarySmtpAddress = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001BE25 File Offset: 0x0001A025
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x0001BE2D File Offset: 0x0001A02D
		internal StorePropertyDefinition[] PropertyDefinitions { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001BE36 File Offset: 0x0001A036
		internal List<string> ContactEmailAddresses
		{
			get
			{
				return this.contactEmailAddresses;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001BE3E File Offset: 0x0001A03E
		internal string Email1DisplayName
		{
			get
			{
				return this.email1DisplayName;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001BE46 File Offset: 0x0001A046
		internal ADRecipient Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001BE4E File Offset: 0x0001A04E
		internal string CompanyName
		{
			get
			{
				return this.companyName;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0001BE56 File Offset: 0x0001A056
		internal string LastName
		{
			get
			{
				return this.lastName;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001BE5E File Offset: 0x0001A05E
		internal string FirstName
		{
			get
			{
				return this.firstName;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001BE66 File Offset: 0x0001A066
		internal string OfficeLocation
		{
			get
			{
				return this.officeLocation;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001BE6E File Offset: 0x0001A06E
		internal string ContactHomeAddress
		{
			get
			{
				return this.homeAddress;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001BE76 File Offset: 0x0001A076
		internal LocalizedString BusinessAddress
		{
			get
			{
				return this.businessAddress;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001BE7E File Offset: 0x0001A07E
		internal string OtherAddress
		{
			get
			{
				return this.otherAddress;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001BE86 File Offset: 0x0001A086
		internal string HomeAddress
		{
			get
			{
				return this.homeAddress;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001BE8E File Offset: 0x0001A08E
		internal string PersonId
		{
			get
			{
				return this.personId;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001BE96 File Offset: 0x0001A096
		internal string GALLinkId
		{
			get
			{
				return this.galLinkId;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001BE9E File Offset: 0x0001A09E
		internal string ContactLastNameFirstNameDtmfMap
		{
			get
			{
				return this.lastNameFirstNameDtmfMap;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001BEA6 File Offset: 0x0001A0A6
		internal string DtmfEmailAlias
		{
			get
			{
				return this.emailAliasDtmfMap;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001BEAE File Offset: 0x0001A0AE
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x0001BEB6 File Offset: 0x0001A0B6
		internal UMDialPlan TargetDialPlan
		{
			get
			{
				return this.targetDialPlan;
			}
			set
			{
				this.targetDialPlan = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001BEBF File Offset: 0x0001A0BF
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0001BEC7 File Offset: 0x0001A0C7
		internal bool NeedDisambiguation
		{
			get
			{
				return this.needDisambiguation;
			}
			set
			{
				this.needDisambiguation = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001BED0 File Offset: 0x0001A0D0
		internal Participant EmailParticipant
		{
			get
			{
				return this.emailParticipant;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001BED8 File Offset: 0x0001A0D8
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0001BEE0 File Offset: 0x0001A0E0
		internal bool IsPersonalContact { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001BEE9 File Offset: 0x0001A0E9
		internal bool IsFromRecipientCache
		{
			get
			{
				return !string.IsNullOrEmpty(this.partnerNetworkId) && StringComparer.OrdinalIgnoreCase.Equals(this.partnerNetworkId, WellKnownNetworkNames.RecipientCache);
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001BF0F File Offset: 0x0001A10F
		public bool Equals(ContactSearchItem other)
		{
			return this.Id.Equals(other.Id, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001BF23 File Offset: 0x0001A123
		public int CompareTo(ContactSearchItem other)
		{
			return string.Compare(this.Id, other.Id, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001BF38 File Offset: 0x0001A138
		internal static ContactSearchItem CreateFromRecipient(ADRecipient r)
		{
			if (r == null)
			{
				throw new ArgumentNullException("recipient");
			}
			ContactSearchItem contactSearchItem = new ContactSearchItem();
			contactSearchItem.recipient = r;
			contactSearchItem.fullName = contactSearchItem.recipient.DisplayName.Trim();
			contactSearchItem.primarySmtpAddress = contactSearchItem.recipient.PrimarySmtpAddress.ToString().Trim();
			contactSearchItem.id = contactSearchItem.recipient.Id.ToString();
			foreach (string text in contactSearchItem.recipient.UMDtmfMap)
			{
				if (text.StartsWith("emailAddress:", StringComparison.OrdinalIgnoreCase))
				{
					contactSearchItem.emailAliasDtmfMap = text;
					break;
				}
			}
			if (!string.IsNullOrEmpty(contactSearchItem.recipient.LegacyExchangeDN))
			{
				contactSearchItem.emailParticipant = new Participant(contactSearchItem.recipient.DisplayName, contactSearchItem.recipient.LegacyExchangeDN, "EX");
			}
			else
			{
				SmtpAddress smtpAddress = contactSearchItem.recipient.PrimarySmtpAddress;
				if (SmtpAddress.IsValidSmtpAddress(contactSearchItem.recipient.PrimarySmtpAddress.ToString()))
				{
					contactSearchItem.emailParticipant = new Participant(contactSearchItem.recipient.DisplayName, contactSearchItem.recipient.PrimarySmtpAddress.ToString(), "SMTP");
				}
			}
			IADOrgPerson iadorgPerson = r as IADOrgPerson;
			if (iadorgPerson != null)
			{
				contactSearchItem.phone = ContactSearchItem.CreatePhoneNumber(iadorgPerson.Phone);
				contactSearchItem.businessPhone = contactSearchItem.phone;
				contactSearchItem.mobilePhone = ContactSearchItem.CreatePhoneNumber(iadorgPerson.MobilePhone);
				contactSearchItem.homePhone = ContactSearchItem.CreatePhoneNumber(iadorgPerson.HomePhone);
				string text2 = string.IsNullOrEmpty(iadorgPerson.Office) ? string.Empty : iadorgPerson.Office.Trim();
				string text3 = string.IsNullOrEmpty(iadorgPerson.StreetAddress) ? string.Empty : iadorgPerson.StreetAddress.Trim();
				string text4 = string.IsNullOrEmpty(iadorgPerson.City) ? string.Empty : iadorgPerson.City.Trim();
				string text5 = string.IsNullOrEmpty(iadorgPerson.StateOrProvince) ? string.Empty : iadorgPerson.StateOrProvince.Trim();
				string text6 = string.IsNullOrEmpty(iadorgPerson.PostalCode) ? string.Empty : iadorgPerson.PostalCode.Trim();
				string text7 = (string)iadorgPerson.CountryOrRegion;
				if (!string.IsNullOrEmpty(text2) || !string.IsNullOrEmpty(text3) || !string.IsNullOrEmpty(text4) || !string.IsNullOrEmpty(text5) || !string.IsNullOrEmpty(text6) || !string.IsNullOrEmpty(text7))
				{
					contactSearchItem.businessAddress = Strings.OfficeAddress(text2, text3, text4, text5, text6, text7);
				}
			}
			return contactSearchItem;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001C1F4 File Offset: 0x0001A3F4
		internal static ContactSearchItem CreateSearchItem(object[] propertyArray, StorePropertyDefinition[] propertyDefinitions)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, null, "Entering ContactSearchItem.CreateSearchItem", new object[0]);
			ContactSearchItem contactSearchItem = new ContactSearchItem();
			contactSearchItem.PropertyDefinitions = propertyDefinitions;
			contactSearchItem.fullName = contactSearchItem.GetProperty(propertyArray, StoreObjectSchema.DisplayName);
			contactSearchItem.itemClass = contactSearchItem.GetProperty(propertyArray, StoreObjectSchema.ItemClass);
			contactSearchItem.partnerNetworkId = contactSearchItem.GetProperty(propertyArray, ContactSchema.PartnerNetworkId);
			contactSearchItem.IsPersonalContact = true;
			int num = Array.IndexOf<StorePropertyDefinition>(propertyDefinitions, ItemSchema.Id);
			VersionedId versionedId = (VersionedId)propertyArray[num];
			contactSearchItem.id = versionedId.ToBase64String();
			if (ObjectClass.IsOfClass(contactSearchItem.itemClass, "IPM.DistList"))
			{
				if (string.IsNullOrEmpty(contactSearchItem.fullName))
				{
					CallIdTracer.TraceWarning(ExTraceGlobals.DirectorySearchTracer, null, "The contact search item not created because fullName is null or empty", new object[0]);
					return null;
				}
				contactSearchItem.lastNameFirstNameDtmfMap = DtmfString.DtmfEncode(contactSearchItem.fullName);
			}
			else if (string.IsNullOrEmpty(contactSearchItem.fullName))
			{
				contactSearchItem.companyName = contactSearchItem.GetProperty(propertyArray, ContactSchema.CompanyName);
				if (string.IsNullOrEmpty(contactSearchItem.companyName))
				{
					CallIdTracer.TraceWarning(ExTraceGlobals.DirectorySearchTracer, null, "The contact search item not created because fullName and company is null or empty", new object[0]);
					return null;
				}
				contactSearchItem.lastNameFirstNameDtmfMap = DtmfString.DtmfEncode(contactSearchItem.companyName);
				contactSearchItem.fullName = contactSearchItem.companyName;
			}
			else
			{
				contactSearchItem.lastName = contactSearchItem.GetProperty(propertyArray, ContactSchema.Surname);
				contactSearchItem.firstName = contactSearchItem.GetProperty(propertyArray, ContactSchema.GivenName);
				contactSearchItem.email1DisplayName = contactSearchItem.GetProperty(propertyArray, ContactSchema.Email1DisplayName);
				PersonId property = XsoUtil.GetProperty<PersonId>(propertyArray, ContactSchema.PersonId, propertyDefinitions);
				if (property != null)
				{
					contactSearchItem.personId = property.ToBase64String();
				}
				contactSearchItem.galLinkId = XsoUtil.GetProperty<Guid>(propertyArray, ContactSchema.GALLinkID, propertyDefinitions).ToString();
				string text = string.Empty;
				if (!string.IsNullOrEmpty(contactSearchItem.lastName))
				{
					text += contactSearchItem.lastName;
				}
				if (!string.IsNullOrEmpty(contactSearchItem.firstName))
				{
					text += contactSearchItem.firstName;
				}
				if (string.IsNullOrEmpty(text))
				{
					contactSearchItem = null;
				}
				else
				{
					contactSearchItem.lastNameFirstNameDtmfMap = DtmfString.DtmfEncode(text);
				}
			}
			return contactSearchItem;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001C404 File Offset: 0x0001A604
		internal static ContactSearchItem CreateSelectedResult(MailboxSession session, object[] propertyArray, StorePropertyDefinition[] propertyDefinitions)
		{
			ContactSearchItem contactSearchItem = new ContactSearchItem();
			contactSearchItem.PropertyDefinitions = propertyDefinitions;
			contactSearchItem.mailboxGuid = session.MailboxGuid;
			contactSearchItem.propertyArray = propertyArray;
			contactSearchItem.itemClass = contactSearchItem.GetProperty(propertyArray, StoreObjectSchema.ItemClass);
			contactSearchItem.fullName = contactSearchItem.GetProperty(propertyArray, StoreObjectSchema.DisplayName);
			int num = Array.IndexOf<StorePropertyDefinition>(propertyDefinitions, ItemSchema.Id);
			VersionedId versionedId = (VersionedId)propertyArray[num];
			contactSearchItem.id = versionedId.ToBase64String();
			contactSearchItem.IsPersonalContact = true;
			if (ObjectClass.IsOfClass(contactSearchItem.itemClass, "IPM.DistList"))
			{
				contactSearchItem.InitializeSelectedGroup(session, versionedId, propertyArray);
			}
			else
			{
				contactSearchItem.InitializeSelectedPersonalContact(propertyArray);
			}
			return contactSearchItem;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001C4A4 File Offset: 0x0001A6A4
		internal static void AddSearchItems(UMMailboxRecipient subscriber, IDictionary<PropertyDefinition, object> searchFilter, List<ContactSearchItem> list, int maxItemCount)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, null, "AddSearchItems maxItemCount = {0}", new object[]
			{
				maxItemCount
			});
			ExAssert.RetailAssert(maxItemCount > 0, "maxItemCount {0} <= 0", new object[]
			{
				maxItemCount
			});
			if (list.Count >= maxItemCount)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, null, "List already has {0} items, maxItemCount = {1}", new object[]
				{
					list.Count,
					maxItemCount
				});
				return;
			}
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = subscriber.CreateSessionLock())
			{
				using (ContactsFolder contactsFolder = ContactsFolder.Bind(mailboxSessionLock.Session, mailboxSessionLock.Session.GetDefaultFolderId(DefaultFolderType.Contacts)))
				{
					object[][] array = contactsFolder.FindNamesView(searchFilter, maxItemCount - list.Count, new SortBy[]
					{
						new SortBy(StoreObjectSchema.DisplayName, SortOrder.Ascending)
					}, ContactSearchItem.ContactSearchPropertyDefinitions);
					for (int i = 0; i < array.Length; i++)
					{
						ContactSearchItem contactSearchItem = ContactSearchItem.CreateSearchItem(array[i], ContactSearchItem.ContactSearchPropertyDefinitions);
						if (contactSearchItem != null && !list.Contains(contactSearchItem))
						{
							list.Add(contactSearchItem);
						}
					}
				}
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001C5E8 File Offset: 0x0001A7E8
		internal static void AddMOWASearchItems(UMMailboxRecipient subscriber, IDictionary<PropertyDefinition, object> searchFilter, List<ContactSearchItem> list, int maxItemCount)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, null, "Entering ContactSearchItem.AddMOWASearchItems maxItemCount = {0}", new object[]
			{
				maxItemCount
			});
			ExAssert.RetailAssert(maxItemCount > 0, "maxItemCount {0} <= 0", new object[]
			{
				maxItemCount
			});
			if (list.Count >= maxItemCount)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, null, "List already has {0} items, maxItemCount = {1}", new object[]
				{
					list.Count,
					maxItemCount
				});
				return;
			}
			DefaultFolderType defaultFolderType = DefaultFolderType.AllContacts;
			List<QueryFilter> list2 = new List<QueryFilter>(searchFilter.Count);
			foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in searchFilter)
			{
				QueryFilter item = new ComparisonFilter(ComparisonOperator.Equal, keyValuePair.Key, keyValuePair.Value);
				list2.Add(item);
			}
			QueryFilter queryFilter = QueryFilter.AndTogether(list2.ToArray());
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = subscriber.CreateSessionLock())
			{
				using (Folder folder = Folder.Bind(mailboxSessionLock.Session, defaultFolderType))
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, queryFilter, null, ContactSearchItem.ContactSearchPropertyDefinitions))
					{
						object[][] rows = queryResult.GetRows(100);
						Dictionary<Tuple<string, string, string>, ContactSearchItem> dictionary = new Dictionary<Tuple<string, string, string>, ContactSearchItem>(rows.Length);
						while (rows != null && rows.Length > 0)
						{
							int num = 0;
							while (num < rows.Length && dictionary.Count + list.Count < maxItemCount)
							{
								ContactSearchItem contactSearchItem = ContactSearchItem.CreateSearchItem(rows[num], ContactSearchItem.ContactSearchPropertyDefinitions);
								if (contactSearchItem == null)
								{
									CallIdTracer.TraceWarning(ExTraceGlobals.DirectorySearchTracer, null, "The contact search item is not added to grammar, because its null", new object[0]);
								}
								else
								{
									string item2 = (contactSearchItem.PersonId != null) ? contactSearchItem.PersonId : Guid.NewGuid().ToString();
									Tuple<string, string, string> key = Tuple.Create<string, string, string>(item2, contactSearchItem.FullName, contactSearchItem.GALLinkId);
									if (!dictionary.ContainsKey(key))
									{
										dictionary.Add(key, contactSearchItem);
									}
								}
								num++;
							}
							if (dictionary.Count + list.Count >= maxItemCount)
							{
								break;
							}
							rows = queryResult.GetRows(100);
						}
						foreach (ContactSearchItem item3 in dictionary.Values)
						{
							list.Add(item3);
						}
					}
				}
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
		internal static ContactSearchItem GetSelectedSearchItemFromId(UMMailboxRecipient subscriber, string id)
		{
			ContactSearchItem result = null;
			IDictionary<PropertyDefinition, object> dictionary = new SortedDictionary<PropertyDefinition, object>();
			dictionary.Add(ItemSchema.Id, VersionedId.Deserialize(id));
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = subscriber.CreateSessionLock())
			{
				using (ContactsFolder contactsFolder = ContactsFolder.Bind(mailboxSessionLock.Session, mailboxSessionLock.Session.GetDefaultFolderId(DefaultFolderType.Contacts)))
				{
					object[][] array = contactsFolder.FindNamesView(dictionary, 0, null, PersonalContactInfo.ContactPropertyDefinitions);
					if (array.Length == 1)
					{
						result = ContactSearchItem.CreateSelectedResult(mailboxSessionLock.Session, array[0], PersonalContactInfo.ContactPropertyDefinitions);
					}
				}
			}
			return result;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001C980 File Offset: 0x0001AB80
		internal void SetEmailAddresses(ActivityManager manager)
		{
			for (int i = 0; i < 3; i++)
			{
				string text = null;
				if (i < this.ContactEmailAddresses.Count)
				{
					text = this.ContactEmailAddresses[i];
				}
				manager.WriteVariable("email" + Convert.ToString(i + 1, CultureInfo.InvariantCulture), text);
				manager.WriteVariable("haveEmail" + Convert.ToString(i + 1, CultureInfo.InvariantCulture), !string.IsNullOrEmpty(text));
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001C9FF File Offset: 0x0001ABFF
		internal void SetVariablesForTts(ActivityManager manager, BaseUMCallSession vo)
		{
			this.SetEmailAddresses(manager);
			this.SetHomeNumber(manager, vo);
			this.SetBusinessNumber(manager, vo);
			this.SetMobileNumber(manager, vo);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001CA20 File Offset: 0x0001AC20
		internal void SetBusinessPhoneForDialPlan(UMDialPlan originatingDialPlan)
		{
			PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, this.recipient.DisplayName);
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, data, "SetBusinessPhoneForDialPlan() Recipient=_UserDisplayName DialPlan={0}.", new object[]
			{
				originatingDialPlan.Name
			});
			PhoneNumber phoneNumber = null;
			DialPermissions.GetBestOfficeNumber(this.recipient, originatingDialPlan, out phoneNumber);
			this.phone = phoneNumber;
			this.businessPhone = phoneNumber;
			this.businessPhoneForTts = phoneNumber;
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			string text = null;
			if (iadorgPerson != null && originatingDialPlan.URIType == UMUriType.SipName && DialPermissions.TryGetDialplanExtension(this.recipient, originatingDialPlan, out text))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "SetBusinessPhoneForDialPlan() This is a UMEnabled result in SipName dialplan. RTCSipLine = '{0}' GalPhone = '{1}'", new object[]
				{
					iadorgPerson.RtcSipLine,
					iadorgPerson.Phone
				});
				string text2 = Utils.TrimSpaces(iadorgPerson.RtcSipLine);
				string text3 = Utils.TrimSpaces(iadorgPerson.Phone);
				if (text2 != null)
				{
					int num = text2.IndexOf("tel:", StringComparison.OrdinalIgnoreCase);
					if (num != -1 && text2.Length > num + 4)
					{
						text2 = text2.Substring(num + 4);
					}
				}
				PhoneNumber phoneNumber2 = null;
				if (text2 != null && PhoneNumber.TryParse(originatingDialPlan, text2, out phoneNumber2))
				{
					this.businessPhoneForTts = phoneNumber2;
				}
				else if (text3 != null && PhoneNumber.TryParse(originatingDialPlan, text3, out phoneNumber2))
				{
					this.businessPhoneForTts = phoneNumber2;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "SetBusinessPhoneForDialPlan() This is a UMEnabled result in SipName dialplan. Setting businessPhoneForTTS  = '{0}'", new object[]
				{
					this.businessPhoneForTts
				});
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.DirectorySearchTracer, this, "SetBusinessPhoneForDialPlan() businessPhone = {0} businessPhoneForTts = {1}", new object[]
			{
				this.businessPhone,
				this.businessPhoneForTts
			});
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		internal ContactInfo ToContactInfo(FoundByType selectedPhoneType)
		{
			ContactInfo result = null;
			if (this.IsPersonalContact)
			{
				result = new PersonalContactInfo(this.mailboxGuid, this.propertyArray, selectedPhoneType, false);
			}
			else
			{
				IADOrgPerson iadorgPerson = this.Recipient as IADOrgPerson;
				if (iadorgPerson != null)
				{
					result = new ADContactInfo(iadorgPerson, selectedPhoneType);
				}
			}
			return result;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001CC08 File Offset: 0x0001AE08
		private static PhoneNumber CreatePhoneNumber(string dialString)
		{
			string text = string.IsNullOrEmpty(dialString) ? string.Empty : dialString.Trim();
			PhoneNumber result = null;
			if (PhoneNumber.TryParse(text, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001CC3C File Offset: 0x0001AE3C
		private void InitializeSelectedGroup(MailboxSession session, VersionedId vid, object[] propertyArray)
		{
			using (DistributionList distributionList = (DistributionList)Item.Bind(session, vid))
			{
				this.IsGroup = true;
				Participant[] array = DistributionList.ExpandDeep(session, distributionList.StoreObjectId);
				if (array.Length > 0)
				{
					this.GroupHasEmail = true;
					this.emailParticipant = distributionList.GetAsParticipant();
				}
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001CCA0 File Offset: 0x0001AEA0
		private void InitializeSelectedPersonalContact(object[] propertyArray)
		{
			this.lastName = this.GetProperty(propertyArray, ContactSchema.Surname);
			this.firstName = this.GetProperty(propertyArray, ContactSchema.GivenName);
			string property = this.GetProperty(propertyArray, ContactSchema.Email1EmailAddress);
			if (!string.IsNullOrEmpty(property))
			{
				this.InitializeEmailAddresses(property, ContactSchema.Email1AddrType);
			}
			this.email1DisplayName = this.GetProperty(propertyArray, ContactSchema.Email1DisplayName);
			string property2 = this.GetProperty(propertyArray, ContactSchema.Email2EmailAddress);
			if (!string.IsNullOrEmpty(property2))
			{
				this.InitializeEmailAddresses(property2, ContactSchema.Email2AddrType);
			}
			string property3 = this.GetProperty(propertyArray, ContactSchema.Email3EmailAddress);
			if (!string.IsNullOrEmpty(property3))
			{
				this.InitializeEmailAddresses(property3, ContactSchema.Email3AddrType);
			}
			this.companyName = this.GetProperty(propertyArray, ContactSchema.CompanyName);
			this.officeLocation = this.GetProperty(propertyArray, ContactSchema.OfficeLocation);
			this.homeAddress = this.GetProperty(propertyArray, ContactSchema.HomeAddress);
			string property4 = this.GetProperty(propertyArray, ContactSchema.BusinessAddress);
			if (!string.IsNullOrEmpty(property4))
			{
				this.businessAddress = new LocalizedString(property4);
			}
			this.otherAddress = this.GetProperty(propertyArray, ContactSchema.OtherAddress);
			this.businessPhone = ContactSearchItem.CreatePhoneNumber(this.GetProperty(propertyArray, ContactSchema.BusinessPhoneNumber) ?? this.GetProperty(propertyArray, ContactSchema.BusinessPhoneNumber2));
			this.homePhone = ContactSearchItem.CreatePhoneNumber(this.GetProperty(propertyArray, ContactSchema.HomePhone) ?? this.GetProperty(propertyArray, ContactSchema.HomePhone2));
			this.mobilePhone = ContactSearchItem.CreatePhoneNumber(this.GetProperty(propertyArray, ContactSchema.MobilePhone) ?? this.GetProperty(propertyArray, ContactSchema.MobilePhone2));
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001CE24 File Offset: 0x0001B024
		private void InitializeEmailAddresses(string emailAddress, StorePropertyDefinition addrTypePropertyId)
		{
			string property = this.GetProperty(this.propertyArray, addrTypePropertyId);
			if (!string.IsNullOrEmpty(property))
			{
				if (property.Equals("EX", StringComparison.OrdinalIgnoreCase))
				{
					Participant source = new Participant(this.fullName, emailAddress, "EX");
					Participant participant = Participant.TryConvertTo(source, "SMTP");
					if (participant != null)
					{
						this.contactEmailAddresses.Add(participant.EmailAddress);
						if (this.emailParticipant == null)
						{
							this.emailParticipant = participant;
							this.primarySmtpAddress = emailAddress;
							return;
						}
					}
				}
				else if (property.Equals("SMTP", StringComparison.OrdinalIgnoreCase))
				{
					if (this.emailParticipant == null)
					{
						this.emailParticipant = new Participant(this.fullName, emailAddress, "SMTP");
						this.primarySmtpAddress = emailAddress;
					}
					this.contactEmailAddresses.Add(emailAddress);
				}
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		private string GetProperty(object[] results, StorePropertyDefinition propertyId)
		{
			int num = Array.IndexOf<StorePropertyDefinition>(this.PropertyDefinitions, propertyId);
			object obj = results[num];
			string text = obj as string;
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
			}
			return text;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001CF29 File Offset: 0x0001B129
		private void SetHomeNumber(ActivityManager manager, BaseUMCallSession vo)
		{
			manager.WriteVariable("haveDialableHomeNumber", Util.IsDialableNumber(this.homePhone, vo, this.recipient));
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001CF4D File Offset: 0x0001B14D
		private void SetBusinessNumber(ActivityManager manager, BaseUMCallSession vo)
		{
			manager.WriteVariable("haveDialableBusinessNumber", Util.IsDialableNumber(this.businessPhone, vo, this.recipient));
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001CF71 File Offset: 0x0001B171
		private void SetMobileNumber(ActivityManager manager, BaseUMCallSession vo)
		{
			manager.WriteVariable("haveDialableMobileNumber", Util.IsDialableNumber(this.mobilePhone, vo, this.recipient));
		}

		// Token: 0x040007DB RID: 2011
		private static StorePropertyDefinition[] contactSearchPropertyDefinitions = new StorePropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			ContactSchema.GivenName,
			ContactSchema.Surname,
			StoreObjectSchema.DisplayName,
			ContactSchema.Email1DisplayName,
			ContactSchema.CompanyName,
			ContactSchema.PersonId,
			ContactSchema.GALLinkID,
			ContactSchema.PartnerNetworkId
		};

		// Token: 0x040007DC RID: 2012
		private string itemClass;

		// Token: 0x040007DD RID: 2013
		private string id;

		// Token: 0x040007DE RID: 2014
		private string fullName;

		// Token: 0x040007DF RID: 2015
		private PhoneNumber phone;

		// Token: 0x040007E0 RID: 2016
		private PhoneNumber homePhone;

		// Token: 0x040007E1 RID: 2017
		private PhoneNumber businessPhone;

		// Token: 0x040007E2 RID: 2018
		private PhoneNumber businessPhoneForTts;

		// Token: 0x040007E3 RID: 2019
		private PhoneNumber mobilePhone;

		// Token: 0x040007E4 RID: 2020
		private string primarySmtpAddress;

		// Token: 0x040007E5 RID: 2021
		private List<string> contactEmailAddresses = new List<string>();

		// Token: 0x040007E6 RID: 2022
		private string email1DisplayName;

		// Token: 0x040007E7 RID: 2023
		private ADRecipient recipient;

		// Token: 0x040007E8 RID: 2024
		private string companyName;

		// Token: 0x040007E9 RID: 2025
		private string lastName;

		// Token: 0x040007EA RID: 2026
		private string firstName;

		// Token: 0x040007EB RID: 2027
		private string officeLocation;

		// Token: 0x040007EC RID: 2028
		private string homeAddress;

		// Token: 0x040007ED RID: 2029
		private string personId;

		// Token: 0x040007EE RID: 2030
		private string galLinkId;

		// Token: 0x040007EF RID: 2031
		private string partnerNetworkId;

		// Token: 0x040007F0 RID: 2032
		private LocalizedString businessAddress;

		// Token: 0x040007F1 RID: 2033
		private string otherAddress;

		// Token: 0x040007F2 RID: 2034
		private string lastNameFirstNameDtmfMap;

		// Token: 0x040007F3 RID: 2035
		private string emailAliasDtmfMap;

		// Token: 0x040007F4 RID: 2036
		private UMDialPlan targetDialPlan;

		// Token: 0x040007F5 RID: 2037
		private bool needDisambiguation;

		// Token: 0x040007F6 RID: 2038
		private Participant emailParticipant;

		// Token: 0x040007F7 RID: 2039
		private object[] propertyArray;

		// Token: 0x040007F8 RID: 2040
		private Guid mailboxGuid;
	}
}

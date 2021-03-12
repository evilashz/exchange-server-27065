using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000494 RID: 1172
	[OwaEventNamespace("ABDA")]
	[OwaEventObjectId(typeof(ADObjectId))]
	internal sealed class DirectoryItemEventHandler : AddressBookItemEventHandler
	{
		// Token: 0x06002D4B RID: 11595 RVA: 0x000FE184 File Offset: 0x000FC384
		protected override void BindToData()
		{
			this.itemIds = (ADObjectId[])base.GetParameter("id");
			if (this.itemIds.Length > 50)
			{
				string description = string.Format(LocalizedStrings.GetNonEncoded(243772161), this.itemIds.Length);
				throw new OwaEventHandlerException("Could not add more than 50 contacts to item", description, OwaEventHandlerErrorCode.AddContactsToItemError);
			}
			IRecipientSession recipientSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, base.UserContext);
			this.results = recipientSession.ReadMultiple((ADObjectId[])this.itemIds);
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000FE204 File Offset: 0x000FC404
		[OwaEvent("AOOToCt")]
		[OwaEventSegmentation(Feature.Contacts)]
		[OwaEventParameter("dn", typeof(string), false, true)]
		[OwaEventParameter("em", typeof(string), false, true)]
		[OwaEventParameter("mp", typeof(string), false, true)]
		public void AddOneOffToContacts()
		{
			ExTraceGlobals.ContactsCallTracer.TraceDebug((long)this.GetHashCode(), "DirectoryItemEventHandler.AddOneOffToContacts");
			string displayName = (string)base.GetParameter("dn");
			string email = (string)base.GetParameter("em");
			string text = (string)base.GetParameter("mp");
			using (Contact contact = Contact.Create(base.UserContext.MailboxSession, base.UserContext.ContactsFolderId))
			{
				ContactUtilities.SetContactEmailAddress(contact, ContactUtilities.GetEmailPropertyIndex(ContactUtilities.Email1EmailAddress), email, displayName);
				if (!string.IsNullOrEmpty(text))
				{
					contact.SetOrDeleteProperty(ContactUtilities.MobilePhone.PropertyDefinition, text);
				}
				contact.Save(SaveMode.ResolveConflicts);
				contact.Load();
				this.RenderUrlToOpenContactAsDraft(contact.Id.ObjectId.ToBase64String(), "IPM.Contact");
			}
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000FE2E8 File Offset: 0x000FC4E8
		[OwaEventParameter("id", typeof(ADObjectId), true, true)]
		[OwaEvent("AddToContacts")]
		[OwaEventParameter("email", typeof(string), false, true)]
		[OwaEventSegmentation(Feature.Contacts)]
		public void AddADRecipientsToContacts()
		{
			ExTraceGlobals.ContactsCallTracer.TraceDebug((long)this.GetHashCode(), "DirectoryItemEventHandler.AddToContacts");
			string text = null;
			string contactType = "IPM.Contact";
			int num = 0;
			bool flag = false;
			string text2 = null;
			if (base.IsParameterSet("email"))
			{
				text2 = (base.GetParameter("email") as string);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				if (base.IsParameterSet("id"))
				{
					this.itemIds = (ADObjectId[])base.GetParameter("id");
				}
				IRecipientSession session = Utilities.CreateADRecipientSession(Culture.GetUserCulture().LCID, true, ConsistencyMode.IgnoreInvalid, true, base.UserContext);
				ADRecipient data = Utilities.CreateADRecipientFromProxyAddress((this.itemIds == null) ? null : ((ADObjectId)this.itemIds[0]), text2, session);
				Result<ADRecipient> result = new Result<ADRecipient>(data, null);
				this.results = new Result<ADRecipient>[1];
				this.results[0] = result;
			}
			else
			{
				this.BindToData();
			}
			for (int i = 0; i < this.results.Length; i++)
			{
				ADRecipient data2 = this.results[i].Data;
				if (data2 == null)
				{
					flag = true;
				}
				else
				{
					using (ContactBase contactBase = ContactUtilities.AddADRecipientToContacts(base.UserContext, data2))
					{
						if (contactBase != null)
						{
							num++;
							if (num == 1)
							{
								contactBase.Load();
								text = contactBase.Id.ObjectId.ToBase64String();
								if (contactBase is DistributionList)
								{
									contactType = "IPM.DistList";
								}
							}
						}
					}
				}
			}
			if (num == 1 && text != null)
			{
				this.RenderUrlToOpenContactAsDraft(text, contactType);
				return;
			}
			if (flag)
			{
				this.Writer.Write("<div id=\"sCntErr\">");
				this.Writer.Write(LocalizedStrings.GetHtmlEncoded(-552412224));
				this.Writer.Write("</div>");
			}
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000FE4B4 File Offset: 0x000FC6B4
		protected override void LoadRecipientsToItem(Item item, AddressBookItemEventHandler.ItemTypeToPeople itemType)
		{
			MessageItem messageItem = null;
			CalendarItemBase calendarItemBase = null;
			if (itemType == AddressBookItemEventHandler.ItemTypeToPeople.Meeting)
			{
				calendarItemBase = (item as CalendarItemBase);
				calendarItemBase.IsMeeting = true;
			}
			else
			{
				messageItem = (item as MessageItem);
			}
			for (int i = 0; i < this.results.Length; i++)
			{
				ADRecipient data = this.results[i].Data;
				if (data != null)
				{
					if (itemType == AddressBookItemEventHandler.ItemTypeToPeople.TextMessage)
					{
						string emailAddress;
						if (!string.IsNullOrEmpty(emailAddress = Utilities.NormalizePhoneNumber(data[ADOrgPersonSchema.MobilePhone] as string)))
						{
							Participant participant = new Participant(data.DisplayName, emailAddress, "MOBILE");
							messageItem.Recipients.Add(participant, RecipientItemType.To);
						}
					}
					else
					{
						Participant primaryLegacyExchangeParticipant = DirectoryItemEventHandler.GetPrimaryLegacyExchangeParticipant(data);
						if (primaryLegacyExchangeParticipant != null && itemType == AddressBookItemEventHandler.ItemTypeToPeople.Message)
						{
							messageItem.Recipients.Add(primaryLegacyExchangeParticipant, RecipientItemType.To);
						}
						else if (primaryLegacyExchangeParticipant != null)
						{
							calendarItemBase.AttendeeCollection.Add(primaryLegacyExchangeParticipant, AttendeeType.Required, null, null, false);
						}
					}
				}
			}
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000FE5B0 File Offset: 0x000FC7B0
		private static Participant GetPrimaryLegacyExchangeParticipant(ADRecipient adRecipient)
		{
			Participant result = null;
			if (adRecipient.RecipientType == RecipientType.UserMailbox || adRecipient.RecipientType == RecipientType.MailUniversalDistributionGroup || adRecipient.RecipientType == RecipientType.MailUniversalSecurityGroup || adRecipient.RecipientType == RecipientType.MailNonUniversalGroup || adRecipient.RecipientType == RecipientType.MailUser || adRecipient.RecipientType == RecipientType.MailContact || adRecipient.RecipientType == RecipientType.DynamicDistributionGroup || adRecipient.RecipientType == RecipientType.PublicFolder)
			{
				if (!string.IsNullOrEmpty(adRecipient.LegacyExchangeDN))
				{
					result = new Participant(adRecipient.DisplayName, adRecipient.LegacyExchangeDN, "EX");
				}
				else
				{
					result = new Participant(adRecipient.DisplayName, adRecipient.PrimarySmtpAddress.ToString(), "SMTP");
				}
			}
			return result;
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000FE658 File Offset: 0x000FC858
		private void RenderUrlToOpenContactAsDraft(string contactId, string contactType)
		{
			this.Writer.Write("<div id=\"sCntUrl\">?ae=Item&a=New&t=");
			this.Writer.Write(contactType);
			this.Writer.Write("&exdltdrft=1&id=</div><div id=\"sCntId\">");
			this.Writer.Write(contactId);
			this.Writer.Write("</div>");
		}

		// Token: 0x04001DEA RID: 7658
		public const string EventNamespace = "ABDA";

		// Token: 0x04001DEB RID: 7659
		public const string MethodAddToContacts = "AddToContacts";

		// Token: 0x04001DEC RID: 7660
		public const string MethodAddOneOffToContacts = "AOOToCt";

		// Token: 0x04001DED RID: 7661
		public const string OneOffDisplayName = "dn";

		// Token: 0x04001DEE RID: 7662
		public const string OneOffEmail = "em";

		// Token: 0x04001DEF RID: 7663
		public const string OneOffMobilePhone = "mp";

		// Token: 0x04001DF0 RID: 7664
		public const string DirectorySingleRecipientEmail = "email";

		// Token: 0x04001DF1 RID: 7665
		private Result<ADRecipient>[] results;
	}
}

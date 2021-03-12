using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200013F RID: 319
	internal class ContactDictionaryEntryProperty : SimpleProperty, IPregatherParticipants, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x0002A7BD File Offset: 0x000289BD
		protected ContactDictionaryEntryProperty(CommandContext commandContext, string[] xmlNestedLocalNames) : base(commandContext)
		{
			this.xmlLocalNames = xmlNestedLocalNames;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0002A7D0 File Offset: 0x000289D0
		public static ContactDictionaryEntryProperty CreateCommandForEmailAddresses(CommandContext commandContext)
		{
			return ContactDictionaryEntryProperty.CreateCommand(commandContext, new string[]
			{
				"EmailAddresses",
				"Entry"
			});
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0002A7FC File Offset: 0x000289FC
		public static ContactDictionaryEntryProperty CreateCommandForImAddresses(CommandContext commandContext)
		{
			return ContactDictionaryEntryProperty.CreateCommand(commandContext, new string[]
			{
				"ImAddresses",
				"Entry"
			});
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0002A828 File Offset: 0x00028A28
		public static ContactDictionaryEntryProperty CreateCommandForPhoneNumbers(CommandContext commandContext)
		{
			return ContactDictionaryEntryProperty.CreateCommand(commandContext, new string[]
			{
				"PhoneNumbers",
				"Entry"
			});
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0002A854 File Offset: 0x00028A54
		void IPregatherParticipants.Pregather(StoreObject storeObject, List<Participant> participants)
		{
			Contact contact = (Contact)storeObject;
			foreach (EmailAddressIndex key in new EmailAddressIndex[]
			{
				EmailAddressIndex.Email1,
				EmailAddressIndex.Email2,
				EmailAddressIndex.Email3
			})
			{
				Participant participant = contact.EmailAddresses[key];
				if (participant != null)
				{
					participants.Add(participant);
				}
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0002A8B4 File Offset: 0x00028AB4
		public new void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			Contact contact = (Contact)commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			if (this.StorePropertyExists(contact))
			{
				string text = (string)contact[this.propertyDefinition];
				DictionaryPropertyUri dictionaryPropertyUri = this.commandContext.PropertyInformation.PropertyPath as DictionaryPropertyUri;
				if (dictionaryPropertyUri != null && dictionaryPropertyUri.FieldUri == DictionaryUriEnum.EmailAddress)
				{
					EmailAddressKeyType key = ContactDictionaryEntryProperty.emailAddressPropInfoMap[this.commandContext.PropertyInformation];
					EmailAddressDictionaryEntryType emailAddressDictionaryEntryType = new EmailAddressDictionaryEntryType(key, text);
					serviceObject[this.commandContext.PropertyInformation] = emailAddressDictionaryEntryType;
					EmailAddressIndex key2 = this.ParseEmailAddressIndex(this.commandContext.PropertyInformation.PropertyPath, this.propertyDefinition.Name);
					Participant participant = contact.EmailAddresses[key2];
					if (participant != null)
					{
						ParticipantInformationDictionary participantInformation = EWSSettings.ParticipantInformation;
						ParticipantInformation participant2 = participantInformation.GetParticipant(participant);
						emailAddressDictionaryEntryType.Value = participant2.EmailAddress;
						if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
						{
							emailAddressDictionaryEntryType.MailboxType = participant2.MailboxType.ToString();
							if (!string.IsNullOrEmpty(participant2.DisplayName))
							{
								emailAddressDictionaryEntryType.Name = participant2.DisplayName;
							}
							if (!string.IsNullOrEmpty(participant2.RoutingType))
							{
								emailAddressDictionaryEntryType.RoutingType = participant2.RoutingType;
								return;
							}
						}
					}
				}
				else
				{
					serviceObject[this.commandContext.PropertyInformation] = text;
				}
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0002AA2C File Offset: 0x00028C2C
		public new void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			string text = null;
			if (PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, this.propertyDefinition, out text))
			{
				EmailAddressKeyType key;
				if (ContactDictionaryEntryProperty.emailAddressPropInfoMap.TryGetValue(this.commandContext.PropertyInformation, out key))
				{
					commandSettings.ServiceObject[this.commandContext.PropertyInformation] = new EmailAddressDictionaryEntryType(key, text);
					return;
				}
				commandSettings.ServiceObject[this.commandContext.PropertyInformation] = text;
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002AAA8 File Offset: 0x00028CA8
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			StoreObject storeObject = updateCommandSettings.StoreObject;
			ServiceObject serviceObject = setPropertyUpdate.ServiceObject;
			this.SetProperty(serviceObject, storeObject);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002AACC File Offset: 0x00028CCC
		protected override void SetProperty(ServiceObject serviceObject, StoreObject storeObject)
		{
			DictionaryPropertyUri dictionaryPropertyUri = this.commandContext.PropertyInformation.PropertyPath as DictionaryPropertyUri;
			if (dictionaryPropertyUri != null && dictionaryPropertyUri.FieldUri == DictionaryUriEnum.EmailAddress)
			{
				EmailAddressDictionaryEntryType valueOrDefault = serviceObject.GetValueOrDefault<EmailAddressDictionaryEntryType>(this.commandContext.PropertyInformation);
				if (valueOrDefault != null)
				{
					this.SetEmailAddressProperty(this.commandContext.PropertyInformation.PropertyPath, storeObject as Contact, dictionaryPropertyUri.Key, valueOrDefault);
					return;
				}
			}
			else
			{
				base.SetProperty(serviceObject, storeObject);
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002AB3C File Offset: 0x00028D3C
		private static ContactDictionaryEntryProperty CreateCommand(CommandContext commandContext, string[] xmlNestedLocalNames)
		{
			return new ContactDictionaryEntryProperty(commandContext, xmlNestedLocalNames);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002AB48 File Offset: 0x00028D48
		private void SetEmailAddressProperty(PropertyPath propertyPath, Contact contact, string key, EmailAddressDictionaryEntryType emailAddress)
		{
			if (key != null)
			{
				EmailAddressIndex key2;
				StorePropertyDefinition propertyDefinition;
				if (!(key == "EmailAddress1"))
				{
					if (!(key == "EmailAddress2"))
					{
						if (!(key == "EmailAddress3"))
						{
							goto IL_50;
						}
						key2 = EmailAddressIndex.Email3;
						propertyDefinition = ContactSchema.Email3OriginalDisplayName;
					}
					else
					{
						key2 = EmailAddressIndex.Email2;
						propertyDefinition = ContactSchema.Email2OriginalDisplayName;
					}
				}
				else
				{
					key2 = EmailAddressIndex.Email1;
					propertyDefinition = ContactSchema.Email1OriginalDisplayName;
				}
				Participant participant2;
				Participant participant = MailboxHelper.ParseMailbox(this.commandContext.PropertyInformation.PropertyPath, contact, emailAddress, this.commandContext.IdConverter, out participant2, true);
				Participant participant3 = null;
				Participant.Builder builder;
				if (!contact.EmailAddresses.TryGetValue(key2, out participant3))
				{
					builder = new Participant.Builder(participant.DisplayName, participant.EmailAddress, participant.RoutingType);
				}
				else
				{
					builder = new Participant.Builder(participant);
				}
				participant3 = builder.ToParticipant();
				if (participant3.ValidationStatus != ParticipantValidationStatus.NoError)
				{
					throw new ServiceArgumentException((CoreResources.IDs)3156759755U);
				}
				contact.EmailAddresses[key2] = participant3;
				if (string.Compare(participant2.RoutingType, "SMTP") == 0)
				{
					contact[propertyDefinition] = emailAddress.Value;
					return;
				}
				Participant participant4 = MailboxHelper.TryConvertTo(participant2, "SMTP");
				if (participant4 == null)
				{
					contact[propertyDefinition] = emailAddress.Value;
					return;
				}
				contact[propertyDefinition] = participant4.EmailAddress;
				return;
			}
			IL_50:
			throw new InvalidMailboxException(propertyPath, (CoreResources.IDs)2886480659U);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0002ACA0 File Offset: 0x00028EA0
		private EmailAddressIndex ParseEmailAddressIndex(PropertyPath propertyPath, string emailAddressIndex)
		{
			if (string.Compare(emailAddressIndex, ContactSchema.Email1EmailAddress.Name) == 0)
			{
				return EmailAddressIndex.Email1;
			}
			if (string.Compare(emailAddressIndex, ContactSchema.Email2EmailAddress.Name) == 0)
			{
				return EmailAddressIndex.Email2;
			}
			if (string.Compare(emailAddressIndex, ContactSchema.Email3EmailAddress.Name) == 0)
			{
				return EmailAddressIndex.Email3;
			}
			throw new InvalidMailboxException(propertyPath, (CoreResources.IDs)2886480659U);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0002ACF9 File Offset: 0x00028EF9
		public new void ToXml()
		{
			throw new InvalidOperationException("ContactDictionaryEntryProperty.ToXml should not be called");
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0002AD05 File Offset: 0x00028F05
		public new void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("ContactDictionaryEntryProperty.ToXmlForPropertyBag should not be called");
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0002AD14 File Offset: 0x00028F14
		private void SetEmailAddressProperty(PropertyPath propertyPath, Contact contact, string key, XmlElement emailAddress)
		{
			if (key != null)
			{
				EmailAddressIndex key2;
				StorePropertyDefinition propertyDefinition;
				if (!(key == "EmailAddress1"))
				{
					if (!(key == "EmailAddress2"))
					{
						if (!(key == "EmailAddress3"))
						{
							goto IL_50;
						}
						key2 = EmailAddressIndex.Email3;
						propertyDefinition = ContactSchema.Email3OriginalDisplayName;
					}
					else
					{
						key2 = EmailAddressIndex.Email2;
						propertyDefinition = ContactSchema.Email2OriginalDisplayName;
					}
				}
				else
				{
					key2 = EmailAddressIndex.Email1;
					propertyDefinition = ContactSchema.Email1OriginalDisplayName;
				}
				Participant participant2;
				Participant participant = MailboxHelper.ParseMailbox(this.commandContext.PropertyInformation.PropertyPath, contact, emailAddress, this.commandContext.IdConverter, out participant2, true);
				Participant participant3 = null;
				Participant.Builder builder;
				if (!contact.EmailAddresses.TryGetValue(key2, out participant3))
				{
					builder = new Participant.Builder(participant.DisplayName, participant.EmailAddress, participant.RoutingType);
				}
				else
				{
					builder = new Participant.Builder(participant);
				}
				participant3 = builder.ToParticipant();
				if (participant3.ValidationStatus != ParticipantValidationStatus.NoError)
				{
					throw new ServiceArgumentException((CoreResources.IDs)3156759755U);
				}
				contact.EmailAddresses[key2] = participant3;
				if (string.Compare(participant2.RoutingType, "SMTP") == 0)
				{
					contact[propertyDefinition] = emailAddress.InnerText;
					return;
				}
				Participant participant4 = MailboxHelper.TryConvertTo(participant2, "SMTP");
				if (participant4 == null)
				{
					contact[propertyDefinition] = emailAddress.InnerText;
					return;
				}
				contact[propertyDefinition] = participant4.EmailAddress;
				return;
			}
			IL_50:
			throw new InvalidMailboxException(propertyPath, (CoreResources.IDs)2886480659U);
		}

		// Token: 0x0400075D RID: 1885
		protected const string XmlAttributeNameKey = "Key";

		// Token: 0x0400075E RID: 1886
		protected const string XmlAttributeNameDisplayName = "Name";

		// Token: 0x0400075F RID: 1887
		protected const string XmlAttributeNameRoutingType = "RoutingType";

		// Token: 0x04000760 RID: 1888
		protected const string XmlAttributeNameMailboxType = "MailboxType";

		// Token: 0x04000761 RID: 1889
		protected const string XmlElementNameEntry = "Entry";

		// Token: 0x04000762 RID: 1890
		protected const string XmlElementNameEmailAddresses = "EmailAddresses";

		// Token: 0x04000763 RID: 1891
		protected const string XmlElementNameImAddresses = "ImAddresses";

		// Token: 0x04000764 RID: 1892
		protected const string XmlElementNamePhoneNumbers = "PhoneNumbers";

		// Token: 0x04000765 RID: 1893
		protected const string XmlElementNamePhysicalAddresses = "PhysicalAddresses";

		// Token: 0x04000766 RID: 1894
		protected const string XmlElementNameCity = "City";

		// Token: 0x04000767 RID: 1895
		protected const string XmlElementNameCountryOrRegion = "CountryOrRegion";

		// Token: 0x04000768 RID: 1896
		protected const string XmlElementNamePostalCode = "PostalCode";

		// Token: 0x04000769 RID: 1897
		protected const string XmlElementNameState = "State";

		// Token: 0x0400076A RID: 1898
		protected const string XmlElementNameStreet = "Street";

		// Token: 0x0400076B RID: 1899
		protected const string EmailAddress1Name = "EmailAddress1";

		// Token: 0x0400076C RID: 1900
		protected const string EmailAddress2Name = "EmailAddress2";

		// Token: 0x0400076D RID: 1901
		protected const string EmailAddress3Name = "EmailAddress3";

		// Token: 0x0400076E RID: 1902
		private const int XmlElementWithKeyAttributeLevel = 1;

		// Token: 0x0400076F RID: 1903
		protected string[] xmlLocalNames;

		// Token: 0x04000770 RID: 1904
		private static Dictionary<PropertyInformation, EmailAddressKeyType> emailAddressPropInfoMap = new Dictionary<PropertyInformation, EmailAddressKeyType>
		{
			{
				ContactSchema.EmailAddressEmailAddress1,
				EmailAddressKeyType.EmailAddress1
			},
			{
				ContactSchema.EmailAddressEmailAddress2,
				EmailAddressKeyType.EmailAddress2
			},
			{
				ContactSchema.EmailAddressEmailAddress3,
				EmailAddressKeyType.EmailAddress3
			}
		};
	}
}

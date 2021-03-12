using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200010B RID: 267
	public class MailboxHelper
	{
		// Token: 0x0600079C RID: 1948 RVA: 0x00025050 File Offset: 0x00023250
		internal static Participant ParsePDLMemberParticipant(EmailAddressWrapper member, IdConverter idConverter, PropertyPath propertyPath)
		{
			Participant result = null;
			if (member == null)
			{
				return result;
			}
			string name = member.Name;
			string emailAddress = member.EmailAddress;
			string routingType = member.RoutingType;
			MailboxHelper.MailboxTypeType? mailboxTypeType = new MailboxHelper.MailboxTypeType?(EnumUtilities.Parse<MailboxHelper.MailboxTypeType>(member.MailboxType));
			ItemId itemId = member.ItemId;
			if (itemId != null)
			{
				if (mailboxTypeType != MailboxHelper.MailboxTypeType.PrivateDL && mailboxTypeType != MailboxHelper.MailboxTypeType.Contact)
				{
					throw new InvalidMailboxException(propertyPath, CoreResources.IDs.MessageCannotUseItemAsRecipient);
				}
				StoreObjectType storeObjectType = (mailboxTypeType == MailboxHelper.MailboxTypeType.PrivateDL) ? StoreObjectType.DistributionList : StoreObjectType.Contact;
				StoreId originItemId = IdConverter.EwsIdToStoreObjectIdGivenStoreObjectType(itemId.Id, storeObjectType);
				EmailAddressIndex emailAddressIndex = MailboxHelper.ParseEmailAddressIndex(member.EmailAddressIndex);
				ParticipantOrigin origin = new StoreParticipantOrigin(originItemId, emailAddressIndex);
				result = new Participant(name, emailAddress, routingType, origin, new KeyValuePair<PropertyDefinition, object>[0]);
				return result;
			}
			else
			{
				if (emailAddress == null)
				{
					throw new InvalidMailboxException(propertyPath, CoreResources.IDs.ErrorMissingInformationEmailAddress);
				}
				if (mailboxTypeType != null)
				{
					return MailboxHelper.CreateParticipantBasedOnMailboxType(name, emailAddress, routingType, mailboxTypeType.Value, propertyPath, false, out result);
				}
				return MailboxHelper.CreateParticipantWhenNoMailboxType(name, emailAddress, routingType, false, out result);
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00025184 File Offset: 0x00023384
		internal static Participant ParseMailbox(PropertyPath propertyPath, StoreObject storeObject, EmailAddressWrapper mailbox, IdConverter itemIdConverter, out Participant unconvertedParticipant, bool isContactEmailAddress)
		{
			ItemId itemId = mailbox.ItemId;
			string name = mailbox.Name;
			string routingType = mailbox.RoutingType;
			string emailAddress = mailbox.EmailAddress;
			MailboxHelper.MailboxTypeType? mailboxType = null;
			if (!string.IsNullOrEmpty(mailbox.MailboxType))
			{
				mailboxType = new MailboxHelper.MailboxTypeType?(EnumUtilities.Parse<MailboxHelper.MailboxTypeType>(mailbox.MailboxType));
			}
			unconvertedParticipant = null;
			if (!string.IsNullOrEmpty(routingType))
			{
				try
				{
					new CustomProxyAddressPrefix(routingType, name);
				}
				catch (ArgumentException ex)
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceError<string, string>((long)storeObject.GetHashCode(), "Invalid routing type: '{0}'.  ArgumentException encountered: {1}", routingType, ex.Message);
					throw new MalformedRoutingTypeException(propertyPath, ex);
				}
			}
			if (itemId != null)
			{
				IdAndSession idAndSession = itemIdConverter.ConvertItemIdToIdAndSessionReadOnly(itemId);
				StoreId id = idAndSession.Id;
				StoreObjectType objectType = idAndSession.GetAsStoreObjectId().ObjectType;
				if (objectType == StoreObjectType.DistributionList)
				{
					return MailboxHelper.CreateParticipantWhenStoreObjectIsPDL(idAndSession, mailboxType, propertyPath);
				}
				if (objectType == StoreObjectType.Contact)
				{
					ParticipantOrigin origin = new StoreParticipantOrigin(id);
					unconvertedParticipant = new Participant(name, emailAddress, routingType, origin, new KeyValuePair<PropertyDefinition, object>[0]);
					Participant participant = null;
					bool flag = false;
					using (Contact contact = (Contact)idAndSession.GetRootXsoItem(null))
					{
						foreach (EmailAddressIndex key in MailboxHelper.emailAddressIndexes)
						{
							if (contact.EmailAddresses[key] != null)
							{
								if (MailboxHelper.CompareOptionalString(emailAddress, contact.EmailAddresses[key].EmailAddress) && MailboxHelper.CompareOptionalString(routingType, contact.EmailAddresses[key].RoutingType) && MailboxHelper.CompareOptionalString(name, contact.EmailAddresses[key].DisplayName))
								{
									return contact.EmailAddresses[key];
								}
								if (contact.EmailAddresses[key].RoutingType == "EX")
								{
									if (!flag)
									{
										participant = MailboxHelper.TryConvertTo(unconvertedParticipant, "EX");
										flag = true;
									}
									if (participant != null && MailboxHelper.CompareOptionalString(participant.EmailAddress, contact.EmailAddresses[key].EmailAddress))
									{
										return contact.EmailAddresses[key];
									}
								}
							}
						}
						throw new InvalidMailboxException(propertyPath, CoreResources.IDs.MessageInvalidMailboxContactAddressNotFound);
					}
				}
				throw new InvalidMailboxException(propertyPath, CoreResources.IDs.MessageCannotUseItemAsRecipient);
			}
			else
			{
				if (emailAddress == null)
				{
					throw new InvalidMailboxException(propertyPath, CoreResources.IDs.ErrorMissingInformationEmailAddress);
				}
				if (mailboxType == null)
				{
					return MailboxHelper.CreateParticipantWhenNoMailboxType(name, emailAddress, routingType, true, out unconvertedParticipant);
				}
				if (isContactEmailAddress && (mailboxType.Value == MailboxHelper.MailboxTypeType.Contact || mailboxType.Value == MailboxHelper.MailboxTypeType.PrivateDL))
				{
					throw new InvalidMailboxException(propertyPath, "MailboxType", mailboxType.Value.ToString());
				}
				return MailboxHelper.CreateParticipantBasedOnMailboxType(name, emailAddress, routingType, mailboxType.Value, propertyPath, true, out unconvertedParticipant);
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00025478 File Offset: 0x00023678
		internal static Participant TryConvertTo(Participant participant, string targetRoutingType)
		{
			Participant[] array = MailboxHelper.TryConvertTo(new Participant[]
			{
				participant
			}, targetRoutingType);
			return array[0];
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0002549B File Offset: 0x0002369B
		internal static Participant[] TryConvertTo(Participant[] participants, string targetRoutingType)
		{
			return MailboxHelper.TryConvertTo(participants, targetRoutingType, CallContext.Current);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000254AC File Offset: 0x000236AC
		internal static Participant[] TryConvertTo(Participant[] participants, string targetRoutingType, CallContext callContext)
		{
			return Participant.TryConvertTo(participants, targetRoutingType, callContext.ADRecipientSessionContext.GetADRecipientSession());
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000254CD File Offset: 0x000236CD
		internal static MailboxHelper.MailboxTypeType GetMailboxType(ParticipantOrigin participantOrigin, string participantRoutingType)
		{
			return MailboxHelper.GetMailboxType(participantOrigin, participantRoutingType, CallContext.Current != null && CallContext.Current.IsOwa);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000254EC File Offset: 0x000236EC
		internal static MailboxHelper.MailboxTypeType GetMailboxType(ParticipantOrigin participantOrigin, string participantRoutingType, bool isOwa)
		{
			if (participantOrigin is OneOffParticipantOrigin)
			{
				return MailboxHelper.MailboxTypeType.OneOff;
			}
			DirectoryParticipantOrigin directoryParticipantOrigin = participantOrigin as DirectoryParticipantOrigin;
			if (directoryParticipantOrigin != null)
			{
				if (directoryParticipantOrigin.ADEntry != null)
				{
					return MailboxHelper.ConvertToMailboxType((RecipientType)directoryParticipantOrigin.ADEntry[ADRecipientSchema.RecipientType], (RecipientTypeDetails)directoryParticipantOrigin.ADEntry[ADRecipientSchema.RecipientTypeDetails], isOwa);
				}
				if (directoryParticipantOrigin.ADContact != null)
				{
					PersonType valueOrDefault = directoryParticipantOrigin.ADContact.GetValueOrDefault<PersonType>(ContactSchema.PersonType, PersonType.Unknown);
					return MailboxHelper.ConvertToMailboxType(valueOrDefault);
				}
				return MailboxHelper.MailboxTypeType.OneOff;
			}
			else
			{
				StoreParticipantOrigin storeParticipantOrigin = participantOrigin as StoreParticipantOrigin;
				if (storeParticipantOrigin == null || storeParticipantOrigin.OriginItemId == null)
				{
					return MailboxHelper.MailboxTypeType.Unknown;
				}
				if (participantRoutingType == "MAPIPDL")
				{
					return MailboxHelper.MailboxTypeType.PrivateDL;
				}
				return MailboxHelper.MailboxTypeType.Contact;
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00025590 File Offset: 0x00023790
		internal static MailboxHelper.MailboxTypeType ConvertToMailboxType(PersonType personType)
		{
			if (personType == PersonType.DistributionList)
			{
				return MailboxHelper.MailboxTypeType.PublicDL;
			}
			if (personType != PersonType.ModernGroup)
			{
				return MailboxHelper.MailboxTypeType.Mailbox;
			}
			return MailboxHelper.MailboxTypeType.GroupMailbox;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000255AE File Offset: 0x000237AE
		internal static bool IsFullyResolvedMailboxType(MailboxHelper.MailboxTypeType mailboxType)
		{
			return mailboxType != MailboxHelper.MailboxTypeType.OneOff && mailboxType != MailboxHelper.MailboxTypeType.Unknown;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000255BC File Offset: 0x000237BC
		internal static MailboxHelper.MailboxTypeType GetMailboxType(IParticipant participant, bool isOwa)
		{
			MailboxHelper.MailboxTypeType mailboxType = MailboxHelper.GetMailboxType(participant.Origin, participant.RoutingType, isOwa);
			if (MailboxHelper.IsFullyResolvedMailboxType(mailboxType))
			{
				return mailboxType;
			}
			if (participant.GetValueOrDefault<bool>(ParticipantSchema.IsGroupMailbox))
			{
				if (isOwa)
				{
					return MailboxHelper.MailboxTypeType.GroupMailbox;
				}
				return MailboxHelper.MailboxTypeType.Mailbox;
			}
			else if (participant.GetValueOrDefault<bool>(ParticipantSchema.IsDistributionList))
			{
				if (participant.Origin is StoreParticipantOrigin)
				{
					return MailboxHelper.MailboxTypeType.PrivateDL;
				}
				return MailboxHelper.MailboxTypeType.PublicDL;
			}
			else
			{
				if (participant.GetValueOrDefault<bool>(ParticipantSchema.IsResource))
				{
					return MailboxHelper.MailboxTypeType.Mailbox;
				}
				if (participant.GetValueOrDefault<bool>(ParticipantSchema.IsRoom))
				{
					return MailboxHelper.MailboxTypeType.Mailbox;
				}
				if (participant.GetValueOrDefault<bool>(ParticipantSchema.IsMailboxUser))
				{
					return MailboxHelper.MailboxTypeType.Mailbox;
				}
				return mailboxType;
			}
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00025646 File Offset: 0x00023846
		internal static MailboxHelper.MailboxTypeType ConvertToMailboxType(RecipientType recipientType, RecipientTypeDetails recipientTypeDetails)
		{
			return MailboxHelper.ConvertToMailboxType(recipientType, recipientTypeDetails, CallContext.Current != null && CallContext.Current.IsOwa);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00025664 File Offset: 0x00023864
		internal static MailboxHelper.MailboxTypeType ConvertToMailboxType(RecipientType recipientType, RecipientTypeDetails recipientTypeDetails, bool isOwa)
		{
			switch (recipientType)
			{
			case RecipientType.UserMailbox:
			case RecipientType.MailUser:
				if (recipientTypeDetails != RecipientTypeDetails.GroupMailbox)
				{
					return MailboxHelper.MailboxTypeType.Mailbox;
				}
				if (isOwa)
				{
					return MailboxHelper.MailboxTypeType.GroupMailbox;
				}
				return MailboxHelper.MailboxTypeType.Mailbox;
			case RecipientType.MailContact:
				return MailboxHelper.MailboxTypeType.Contact;
			case RecipientType.MailUniversalDistributionGroup:
			case RecipientType.MailUniversalSecurityGroup:
			case RecipientType.MailNonUniversalGroup:
			case RecipientType.DynamicDistributionGroup:
				return MailboxHelper.MailboxTypeType.PublicDL;
			case RecipientType.PublicFolder:
				if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
				{
					return MailboxHelper.MailboxTypeType.Mailbox;
				}
				return MailboxHelper.MailboxTypeType.PublicFolder;
			case RecipientType.SystemAttendantMailbox:
			case RecipientType.SystemMailbox:
			case RecipientType.MicrosoftExchange:
				if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2013))
				{
					return MailboxHelper.MailboxTypeType.Unknown;
				}
				return MailboxHelper.MailboxTypeType.Mailbox;
			}
			return MailboxHelper.MailboxTypeType.Unknown;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000256FC File Offset: 0x000238FC
		internal static Participant GetParticipantFromAddress(EmailAddressWrapper address)
		{
			Participant participant = new Participant(address.Name, address.EmailAddress, address.RoutingType);
			Participant participant2 = MailboxHelper.TryConvertTo(participant, "EX");
			if (null != participant2)
			{
				return participant2;
			}
			return participant;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0002573C File Offset: 0x0002393C
		private static Participant CreateParticipantBasedOnMailboxType(string displayName, string emailAddress, string routingType, MailboxHelper.MailboxTypeType mailboxType, PropertyPath propertyPath, bool shouldValidate, out Participant unconvertedParticipant)
		{
			unconvertedParticipant = null;
			switch (mailboxType)
			{
			case MailboxHelper.MailboxTypeType.OneOff:
				unconvertedParticipant = new Participant(displayName, emailAddress, routingType, new OneOffParticipantOrigin(), new KeyValuePair<PropertyDefinition, object>[0]);
				goto IL_131;
			case MailboxHelper.MailboxTypeType.Mailbox:
			case MailboxHelper.MailboxTypeType.PublicDL:
			case MailboxHelper.MailboxTypeType.Contact:
			case MailboxHelper.MailboxTypeType.PublicFolder:
			case MailboxHelper.MailboxTypeType.GroupMailbox:
			{
				unconvertedParticipant = new Participant(displayName, emailAddress, routingType, new DirectoryParticipantOrigin(), new KeyValuePair<PropertyDefinition, object>[0]);
				Participant participant = MailboxHelper.TryConvertTo(unconvertedParticipant, "EX");
				if (shouldValidate)
				{
					if (null == participant)
					{
						throw new InvalidMailboxException(propertyPath, (CoreResources.IDs)2343198056U);
					}
					DirectoryParticipantOrigin directoryParticipantOrigin = participant.Origin as DirectoryParticipantOrigin;
					MailboxHelper.MailboxTypeType mailboxTypeType = MailboxHelper.ConvertToMailboxType((RecipientType)directoryParticipantOrigin.ADEntry[ADRecipientSchema.RecipientType], (RecipientTypeDetails)directoryParticipantOrigin.ADEntry[ADRecipientSchema.RecipientTypeDetails]);
					if (mailboxTypeType != mailboxType)
					{
						throw new InvalidMailboxException(propertyPath, "MailboxType", mailboxTypeType.ToString(), mailboxType.ToString(), CoreResources.IDs.MessageInvalidMailboxMailboxType);
					}
					return participant;
				}
				else
				{
					if (null != participant)
					{
						return participant;
					}
					goto IL_131;
				}
				break;
			}
			}
			if (shouldValidate)
			{
				throw new InvalidMailboxException(propertyPath, "MailboxType", mailboxType.ToString());
			}
			unconvertedParticipant = new Participant(displayName, emailAddress, routingType);
			IL_131:
			return unconvertedParticipant;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00025880 File Offset: 0x00023A80
		private static Participant CreateParticipantWhenNoMailboxType(string displayName, string emailAddress, string routingType, bool shouldValidate, out Participant unconvertedParticipant)
		{
			unconvertedParticipant = new Participant(displayName, emailAddress, routingType);
			if (shouldValidate)
			{
				Participant participant = MailboxHelper.TryConvertTo(unconvertedParticipant, "EX");
				if (null != participant)
				{
					return participant;
				}
			}
			return unconvertedParticipant;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000258B8 File Offset: 0x00023AB8
		private static Participant CreateParticipantWhenStoreObjectIsPDL(IdAndSession refIdAndSession, MailboxHelper.MailboxTypeType? mailboxType, PropertyPath propertyPath)
		{
			if (mailboxType != null && mailboxType.Value != MailboxHelper.MailboxTypeType.PrivateDL)
			{
				throw new InvalidMailboxException(propertyPath, "MailboxType", MailboxHelper.MailboxTypeType.PrivateDL.ToString(), mailboxType.Value.ToString(), CoreResources.IDs.MessageInvalidMailboxNotPrivateDL);
			}
			Participant asParticipant;
			using (DistributionList distributionList = (DistributionList)refIdAndSession.GetRootXsoItem(null))
			{
				asParticipant = distributionList.GetAsParticipant();
			}
			return asParticipant;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0002593C File Offset: 0x00023B3C
		private static MailboxHelper.MailboxTypeType? ParseMailboxTypeType(string mailboxTypeString)
		{
			if (string.IsNullOrEmpty(mailboxTypeString))
			{
				return null;
			}
			switch (mailboxTypeString)
			{
			case "OneOff":
				return new MailboxHelper.MailboxTypeType?(MailboxHelper.MailboxTypeType.OneOff);
			case "Mailbox":
				return new MailboxHelper.MailboxTypeType?(MailboxHelper.MailboxTypeType.Mailbox);
			case "PublicDL":
				return new MailboxHelper.MailboxTypeType?(MailboxHelper.MailboxTypeType.PublicDL);
			case "PrivateDL":
				return new MailboxHelper.MailboxTypeType?(MailboxHelper.MailboxTypeType.PrivateDL);
			case "Contact":
				return new MailboxHelper.MailboxTypeType?(MailboxHelper.MailboxTypeType.Contact);
			case "PublicFolder":
				return new MailboxHelper.MailboxTypeType?(MailboxHelper.MailboxTypeType.PublicFolder);
			case "GroupMailbox":
				return new MailboxHelper.MailboxTypeType?(MailboxHelper.MailboxTypeType.GroupMailbox);
			}
			return new MailboxHelper.MailboxTypeType?(MailboxHelper.MailboxTypeType.Unknown);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00025A3C File Offset: 0x00023C3C
		private static EmailAddressIndex ParseEmailAddressIndex(string emailAddressIndexString)
		{
			if (string.IsNullOrEmpty(emailAddressIndexString))
			{
				return EmailAddressIndex.None;
			}
			switch (emailAddressIndexString)
			{
			case "Email1":
				return EmailAddressIndex.Email1;
			case "Email2":
				return EmailAddressIndex.Email2;
			case "Email3":
				return EmailAddressIndex.Email3;
			case "HomeFax":
				return EmailAddressIndex.HomeFax;
			case "BusinessFax":
				return EmailAddressIndex.BusinessFax;
			case "OtherFax":
				return EmailAddressIndex.OtherFax;
			}
			return EmailAddressIndex.None;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00025AF7 File Offset: 0x00023CF7
		private static bool CompareOptionalString(string stringToCompare, string stringToCompareWith)
		{
			return stringToCompare == null || 0 == string.Compare(stringToCompare, stringToCompareWith);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00025B08 File Offset: 0x00023D08
		internal static Participant ParseMailbox(PropertyPath propertyPath, StoreObject storeObject, XmlElement parentElement, IdConverter itemIdConverter, out Participant unconvertedParticipant, bool isContactEmailAddress)
		{
			XmlElement xmlElement = parentElement["ItemId", "http://schemas.microsoft.com/exchange/services/2006/types"];
			XmlElement xmlElement2 = parentElement["MailboxType", "http://schemas.microsoft.com/exchange/services/2006/types"];
			string text = null;
			string text2 = null;
			string text3 = null;
			string mailboxTypeString = null;
			if (isContactEmailAddress)
			{
				text = ServiceXml.GetXmlElementAttributeValueOptional(parentElement, "Name");
				text2 = ServiceXml.GetXmlElementAttributeValueOptional(parentElement, "RoutingType");
				mailboxTypeString = ServiceXml.GetXmlElementAttributeValueOptional(parentElement, "MailboxType");
				text3 = parentElement.InnerText;
			}
			else
			{
				text = ServiceXml.GetXmlTextNodeValue(parentElement, "Name", "http://schemas.microsoft.com/exchange/services/2006/types");
				text2 = ServiceXml.GetXmlTextNodeValue(parentElement, "RoutingType", "http://schemas.microsoft.com/exchange/services/2006/types");
				text3 = ServiceXml.GetXmlTextNodeValue(parentElement, "EmailAddress", "http://schemas.microsoft.com/exchange/services/2006/types");
				if (xmlElement2 != null)
				{
					mailboxTypeString = xmlElement2.InnerText;
				}
			}
			MailboxHelper.MailboxTypeType? mailboxTypeType = MailboxHelper.ParseMailboxTypeType(mailboxTypeString);
			unconvertedParticipant = null;
			if (text2 != null)
			{
				try
				{
					new CustomProxyAddressPrefix(text2, text);
				}
				catch (ArgumentException ex)
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceError<string, string>((long)storeObject.GetHashCode(), "Invalid routing type: '{0}'.  ArgumentException encountered: {1}", text2, ex.Message);
					throw new MalformedRoutingTypeException(propertyPath, ex);
				}
			}
			if (xmlElement != null)
			{
				IdAndSession idAndSession = itemIdConverter.ConvertXmlToIdAndSessionReadOnly(xmlElement, BasicTypes.Item);
				StoreId id = idAndSession.Id;
				StoreObjectType objectType = idAndSession.GetAsStoreObjectId().ObjectType;
				if (objectType == StoreObjectType.DistributionList)
				{
					if (mailboxTypeType != null && mailboxTypeType.Value != MailboxHelper.MailboxTypeType.PrivateDL)
					{
						throw new InvalidMailboxException(propertyPath, "MailboxType", MailboxHelper.MailboxTypeType.PrivateDL.ToString(), mailboxTypeType.Value.ToString(), CoreResources.IDs.MessageInvalidMailboxNotPrivateDL);
					}
					using (DistributionList distributionList = (DistributionList)idAndSession.GetRootXsoItem(null))
					{
						return distributionList.GetAsParticipant();
					}
				}
				if (objectType == StoreObjectType.Contact)
				{
					ParticipantOrigin origin = new StoreParticipantOrigin(id);
					unconvertedParticipant = new Participant(text, text3, text2, origin, new KeyValuePair<PropertyDefinition, object>[0]);
					Participant participant = null;
					bool flag = false;
					using (Contact contact = (Contact)idAndSession.GetRootXsoItem(null))
					{
						foreach (EmailAddressIndex key in MailboxHelper.emailAddressIndexes)
						{
							if (contact.EmailAddresses[key] != null)
							{
								if (MailboxHelper.CompareOptionalString(text3, contact.EmailAddresses[key].EmailAddress) && MailboxHelper.CompareOptionalString(text2, contact.EmailAddresses[key].RoutingType) && MailboxHelper.CompareOptionalString(text, contact.EmailAddresses[key].DisplayName))
								{
									return contact.EmailAddresses[key];
								}
								if (contact.EmailAddresses[key].RoutingType == "EX")
								{
									if (!flag)
									{
										participant = MailboxHelper.TryConvertTo(unconvertedParticipant, "EX");
										flag = true;
									}
									if (participant != null && MailboxHelper.CompareOptionalString(participant.EmailAddress, contact.EmailAddresses[key].EmailAddress))
									{
										return contact.EmailAddresses[key];
									}
								}
							}
						}
						throw new InvalidMailboxException(propertyPath, CoreResources.IDs.MessageInvalidMailboxContactAddressNotFound);
					}
				}
				throw new InvalidMailboxException(propertyPath, CoreResources.IDs.MessageCannotUseItemAsRecipient);
			}
			if (text3 == null)
			{
				throw new InvalidMailboxException(propertyPath, CoreResources.IDs.ErrorMissingInformationEmailAddress);
			}
			if (mailboxTypeType != null)
			{
				if (isContactEmailAddress && (mailboxTypeType.Value == MailboxHelper.MailboxTypeType.Contact || mailboxTypeType.Value == MailboxHelper.MailboxTypeType.PrivateDL))
				{
					throw new InvalidMailboxException(propertyPath, "MailboxType", mailboxTypeType.Value.ToString());
				}
				switch (mailboxTypeType.Value)
				{
				case MailboxHelper.MailboxTypeType.OneOff:
					unconvertedParticipant = new Participant(text, text3, text2, new OneOffParticipantOrigin(), new KeyValuePair<PropertyDefinition, object>[0]);
					goto IL_4AE;
				case MailboxHelper.MailboxTypeType.Mailbox:
				case MailboxHelper.MailboxTypeType.PublicDL:
				case MailboxHelper.MailboxTypeType.Contact:
				case MailboxHelper.MailboxTypeType.PublicFolder:
				case MailboxHelper.MailboxTypeType.GroupMailbox:
				{
					unconvertedParticipant = new Participant(text, text3, text2, new DirectoryParticipantOrigin(), new KeyValuePair<PropertyDefinition, object>[0]);
					Participant participant2 = MailboxHelper.TryConvertTo(unconvertedParticipant, "EX");
					if (null == participant2)
					{
						throw new InvalidMailboxException(propertyPath, (CoreResources.IDs)2343198056U);
					}
					DirectoryParticipantOrigin directoryParticipantOrigin = participant2.Origin as DirectoryParticipantOrigin;
					MailboxHelper.MailboxTypeType mailboxTypeType2 = MailboxHelper.ConvertToMailboxType((RecipientType)directoryParticipantOrigin.ADEntry[ADRecipientSchema.RecipientType], (RecipientTypeDetails)directoryParticipantOrigin.ADEntry[ADRecipientSchema.RecipientTypeDetails]);
					if (mailboxTypeType2 != mailboxTypeType.Value)
					{
						throw new InvalidMailboxException(propertyPath, "MailboxType", mailboxTypeType2.ToString(), mailboxTypeType.Value.ToString(), CoreResources.IDs.MessageInvalidMailboxMailboxType);
					}
					return participant2;
				}
				}
				throw new InvalidMailboxException(propertyPath, "MailboxType", mailboxTypeType.Value.ToString());
			}
			else
			{
				unconvertedParticipant = new Participant(text, text3, text2);
				Participant participant3 = MailboxHelper.TryConvertTo(unconvertedParticipant, "EX");
				if (null != participant3)
				{
					return participant3;
				}
			}
			IL_4AE:
			return unconvertedParticipant;
		}

		// Token: 0x040006EF RID: 1775
		private const string MailboxTypeOneOff = "OneOff";

		// Token: 0x040006F0 RID: 1776
		private const string MailboxTypeMailbox = "Mailbox";

		// Token: 0x040006F1 RID: 1777
		private const string MailboxTypePublicDL = "PublicDL";

		// Token: 0x040006F2 RID: 1778
		private const string MailboxTypePrivateDL = "PrivateDL";

		// Token: 0x040006F3 RID: 1779
		private const string MailboxTypeContact = "Contact";

		// Token: 0x040006F4 RID: 1780
		private const string MailboxTypePublicFolder = "PublicFolder";

		// Token: 0x040006F5 RID: 1781
		private const string MailboxTypeGroupMailbox = "GroupMailbox";

		// Token: 0x040006F6 RID: 1782
		private static EmailAddressIndex[] emailAddressIndexes = new EmailAddressIndex[]
		{
			EmailAddressIndex.Email1,
			EmailAddressIndex.Email2,
			EmailAddressIndex.Email3,
			EmailAddressIndex.BusinessFax,
			EmailAddressIndex.HomeFax,
			EmailAddressIndex.OtherFax
		};

		// Token: 0x0200010C RID: 268
		public enum MailboxTypeType
		{
			// Token: 0x040006F8 RID: 1784
			Unknown,
			// Token: 0x040006F9 RID: 1785
			OneOff,
			// Token: 0x040006FA RID: 1786
			Mailbox,
			// Token: 0x040006FB RID: 1787
			PublicDL,
			// Token: 0x040006FC RID: 1788
			PrivateDL,
			// Token: 0x040006FD RID: 1789
			Contact,
			// Token: 0x040006FE RID: 1790
			PublicFolder,
			// Token: 0x040006FF RID: 1791
			GroupMailbox
		}
	}
}

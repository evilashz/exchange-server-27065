using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.NaturalLanguage;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200016A RID: 362
	internal sealed class EntityExtractionResultProperty : ComplexPropertyBase, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x00031D1F File Offset: 0x0002FF1F
		private EntityExtractionResultProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00031D28 File Offset: 0x0002FF28
		public static EntityExtractionResultProperty CreateCommand(CommandContext commandContext)
		{
			return new EntityExtractionResultProperty(commandContext);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00031D48 File Offset: 0x0002FF48
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			StoreObject storeObject = commandSettings.StoreObject;
			Func<StorePropertyDefinition, object> propGetterFunc = (StorePropertyDefinition propDef) => storeObject.GetValueOrDefault<object>(propDef, null);
			ExTimeZone exTimeZone = null;
			if (storeObject != null && storeObject.Session != null && storeObject.Session.MailboxOwner != null)
			{
				string smtpAddress = storeObject.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
				MailboxSession mailboxSessionBySmtpAddress = CallContext.Current.SessionCache.GetMailboxSessionBySmtpAddress(smtpAddress, storeObject.Session.MailboxOwner.MailboxInfo.IsArchive);
				if (mailboxSessionBySmtpAddress != null)
				{
					exTimeZone = mailboxSessionBySmtpAddress.ExTimeZone;
				}
			}
			serviceObject.PropertyBag[propertyInformation] = EntityExtractionResultProperty.CreateResults(propGetterFunc, exTimeZone);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00031ED0 File Offset: 0x000300D0
		private static EntityExtractionResultType CreateResults(Func<StorePropertyDefinition, object> propGetterFunc, ExTimeZone exTimeZone)
		{
			EntityExtractionResultType result = new EntityExtractionResultType();
			EntityExtractionResultProperty.RunEntityFetchAction(delegate
			{
				EntityExtractionResultProperty.AddAddressesToResults(propGetterFunc, result);
			}, "Addresses");
			EntityExtractionResultProperty.RunEntityFetchAction(delegate
			{
				EntityExtractionResultProperty.AddUrlsToResults(propGetterFunc, result);
			}, "Urls");
			EntityExtractionResultProperty.RunEntityFetchAction(delegate
			{
				EntityExtractionResultProperty.AddContactsToResults(propGetterFunc, result);
			}, "Contacts");
			EntityExtractionResultProperty.RunEntityFetchAction(delegate
			{
				EntityExtractionResultProperty.AddMeetingSuggestionsToResults(propGetterFunc, result, exTimeZone);
			}, "MeetingSuggestions");
			EntityExtractionResultProperty.RunEntityFetchAction(delegate
			{
				EntityExtractionResultProperty.AddTaskSuggestionsToResults(propGetterFunc, result);
			}, "TaskSuggestions");
			EntityExtractionResultProperty.RunEntityFetchAction(delegate
			{
				EntityExtractionResultProperty.AddEmailAddressesToResults(propGetterFunc, result);
			}, "EmailAddresses");
			EntityExtractionResultProperty.RunEntityFetchAction(delegate
			{
				EntityExtractionResultProperty.AddPhoneNumbersResults(propGetterFunc, result);
			}, "PhoneNumbers");
			return result;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00031F9C File Offset: 0x0003019C
		private static void RunEntityFetchAction(Action action, string propertyName)
		{
			try
			{
				action();
			}
			catch (CorruptDataException arg)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, CorruptDataException>(0L, "Cannot get NLG property '{0}' exception: {1}", propertyName, arg);
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00031FF4 File Offset: 0x000301F4
		internal static EntityExtractionResultType Render(ItemPart itemPart, ExTimeZone exTimeZone)
		{
			new EntityExtractionResultType();
			Func<StorePropertyDefinition, object> propGetterFunc = (StorePropertyDefinition propDef) => itemPart.StorePropertyBag.GetValueOrDefault<object>(propDef, null);
			return EntityExtractionResultProperty.CreateResults(propGetterFunc, exTimeZone);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00032028 File Offset: 0x00030228
		private static bool HasSupportedPosition(IPositionedExtraction positionedExtraction)
		{
			return positionedExtraction.Position == EmailPosition.LatestReply || positionedExtraction.Position == EmailPosition.Subject || positionedExtraction.Position == EmailPosition.Signature || positionedExtraction.Position == EmailPosition.Other;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00032060 File Offset: 0x00030260
		private static bool TryGetEntityArray<T, TEntity>(Func<StorePropertyDefinition, object> propGetterFunc, StorePropertyDefinition propertyDefinition, EntityExtractionResultProperty.TryExtractEntity<T, TEntity> tryExtractEntityCallback, out TEntity[] extractedEntities) where T : IPositionedExtraction
		{
			extractedEntities = null;
			T[] array = (T[])propGetterFunc(propertyDefinition);
			if (array != null && array.Length > 0)
			{
				List<TEntity> list = new List<TEntity>(Math.Min(100, array.Length));
				foreach (T t in array)
				{
					TEntity item;
					if (EntityExtractionResultProperty.HasSupportedPosition(t) && tryExtractEntityCallback(t, out item))
					{
						list.Add(item);
						if (list.Count >= 100)
						{
							break;
						}
					}
				}
				if (list.Count > 0)
				{
					extractedEntities = list.ToArray();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00032140 File Offset: 0x00030340
		private static void AddAddressesToResults(Func<StorePropertyDefinition, object> propGetterFunc, EntityExtractionResultType result)
		{
			AddressEntityType[] addresses;
			if (EntityExtractionResultProperty.TryGetEntityArray<Address, AddressEntityType>(propGetterFunc, ItemSchema.ExtractedAddresses, delegate(Address nlgObject, out AddressEntityType extractedEntity)
			{
				extractedEntity = null;
				int num = nlgObject.AddressString.Length + 1;
				if (num < 3072)
				{
					extractedEntity = new AddressEntityType
					{
						Address = nlgObject.AddressString,
						Position = (EmailPositionType)nlgObject.Position
					};
					return true;
				}
				return false;
			}, out addresses))
			{
				result.Addresses = addresses;
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000321CC File Offset: 0x000303CC
		private static void AddEmailAddressesToResults(Func<StorePropertyDefinition, object> propGetterFunc, EntityExtractionResultType result)
		{
			EmailAddressEntityType[] emailAddresses;
			if (EntityExtractionResultProperty.TryGetEntityArray<Email, EmailAddressEntityType>(propGetterFunc, ItemSchema.ExtractedEmails, delegate(Email nlgObject, out EmailAddressEntityType extractedEntity)
			{
				extractedEntity = null;
				int num = nlgObject.EmailString.Length + 1;
				if (num < 3072)
				{
					extractedEntity = new EmailAddressEntityType
					{
						EmailAddress = nlgObject.EmailString,
						Position = (EmailPositionType)nlgObject.Position
					};
					return true;
				}
				return false;
			}, out emailAddresses))
			{
				result.EmailAddresses = emailAddresses;
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00032258 File Offset: 0x00030458
		private static void AddUrlsToResults(Func<StorePropertyDefinition, object> propGetterFunc, EntityExtractionResultType result)
		{
			UrlEntityType[] urls;
			if (EntityExtractionResultProperty.TryGetEntityArray<Url, UrlEntityType>(propGetterFunc, ItemSchema.ExtractedUrls, delegate(Url nlgObject, out UrlEntityType extractedEntity)
			{
				extractedEntity = null;
				int num = nlgObject.UrlString.Length + 1;
				if (num < 3072)
				{
					extractedEntity = new UrlEntityType
					{
						Url = nlgObject.UrlString,
						Position = (EmailPositionType)nlgObject.Position
					};
					return true;
				}
				return false;
			}, out urls))
			{
				result.Urls = urls;
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0003232C File Offset: 0x0003052C
		private static void AddPhoneNumbersResults(Func<StorePropertyDefinition, object> propGetterFunc, EntityExtractionResultType result)
		{
			PhoneEntityType[] phoneNumbers;
			if (EntityExtractionResultProperty.TryGetEntityArray<Phone, PhoneEntityType>(propGetterFunc, ItemSchema.ExtractedPhones, delegate(Phone nlgObject, out PhoneEntityType extractedEntity)
			{
				extractedEntity = null;
				string text = EnumUtilities.ToString<Microsoft.Exchange.Data.NaturalLanguage.PhoneType>(nlgObject.Type);
				int num = ((nlgObject.PhoneString == null) ? 0 : nlgObject.PhoneString.Length) + ((nlgObject.OriginalPhoneString == null) ? 0 : nlgObject.OriginalPhoneString.Length) + text.Length + 1;
				if (num < 3072)
				{
					extractedEntity = new PhoneEntityType
					{
						PhoneString = nlgObject.PhoneString,
						OriginalPhoneString = nlgObject.OriginalPhoneString,
						Type = text,
						Position = (EmailPositionType)nlgObject.Position
					};
					return true;
				}
				return false;
			}, out phoneNumbers))
			{
				result.PhoneNumbers = phoneNumbers;
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0003236C File Offset: 0x0003056C
		private static bool TryGetPhoneNumbers(Phone[] phones, out int totalLength, out Microsoft.Exchange.Services.Core.Types.PhoneType[] extractedPhoneNumbers)
		{
			totalLength = 0;
			extractedPhoneNumbers = null;
			if (phones != null && phones.Length > 0)
			{
				List<Microsoft.Exchange.Services.Core.Types.PhoneType> list = new List<Microsoft.Exchange.Services.Core.Types.PhoneType>(Math.Min(100, phones.Length));
				foreach (Phone phone in phones)
				{
					string text = EnumUtilities.ToString<Microsoft.Exchange.Data.NaturalLanguage.PhoneType>(phone.Type);
					int num = ((phone.PhoneString == null) ? 0 : phone.PhoneString.Length) + ((phone.OriginalPhoneString == null) ? 0 : phone.OriginalPhoneString.Length) + text.Length;
					if (num < 3072)
					{
						Microsoft.Exchange.Services.Core.Types.PhoneType item = new Microsoft.Exchange.Services.Core.Types.PhoneType
						{
							PhoneString = phone.PhoneString,
							OriginalPhoneString = phone.OriginalPhoneString,
							Type = text
						};
						list.Add(item);
						if (list.Count >= 100)
						{
							break;
						}
					}
				}
				if (list.Count > 0)
				{
					extractedPhoneNumbers = list.ToArray();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0003245C File Offset: 0x0003065C
		private static void AddTaskSuggestionsToResults(Func<StorePropertyDefinition, object> propGetterFunc, EntityExtractionResultType result)
		{
			Task[] array = (Task[])propGetterFunc(ItemSchema.ExtractedTasks);
			if (array != null && array.Length > 0)
			{
				List<TaskSuggestionType> list = new List<TaskSuggestionType>(Math.Min(100, array.Length));
				foreach (Task task in array)
				{
					if (EntityExtractionResultProperty.HasSupportedPosition(task) && (task.TaskString == null || task.TaskString.Length < 3072))
					{
						TaskSuggestionType item = new TaskSuggestionType
						{
							TaskString = task.TaskString,
							Assignees = EntityExtractionResultProperty.CreateEmailUsers(task.Assignees),
							Position = (EmailPositionType)task.Position
						};
						list.Add(item);
						if (list.Count >= 100)
						{
							break;
						}
					}
				}
				if (list.Count > 0)
				{
					result.TaskSuggestions = list.ToArray();
				}
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00032534 File Offset: 0x00030734
		private static void AddMeetingSuggestionsToResults(Func<StorePropertyDefinition, object> propGetterFunc, EntityExtractionResultType result, ExTimeZone exTimeZone)
		{
			Meeting[] array = (Meeting[])propGetterFunc(ItemSchema.ExtractedMeetings);
			if (array != null && array.Length > 0)
			{
				List<MeetingSuggestionType> list = new List<MeetingSuggestionType>(Math.Min(100, array.Length));
				foreach (Meeting meeting in array)
				{
					if (EntityExtractionResultProperty.HasSupportedPosition(meeting) && (meeting.MeetingString == null || meeting.MeetingString.Length < 3072))
					{
						MeetingSuggestionType meetingSuggestionType = new MeetingSuggestionType
						{
							Location = meeting.Location,
							Subject = meeting.Subject,
							MeetingString = meeting.MeetingString,
							Attendees = EntityExtractionResultProperty.CreateEmailUsers(meeting.Attendees),
							Position = (EmailPositionType)meeting.Position
						};
						if (meeting.StartTime != null)
						{
							meetingSuggestionType.StartTime = EntityExtractionResultProperty.ConvertToUtc(meeting.StartTime.Value, exTimeZone);
						}
						if (meeting.EndTime != null)
						{
							meetingSuggestionType.EndTime = EntityExtractionResultProperty.ConvertToUtc(meeting.EndTime.Value, exTimeZone);
						}
						list.Add(meetingSuggestionType);
						if (list.Count >= 100)
						{
							break;
						}
					}
				}
				if (list.Count > 0)
				{
					result.MeetingSuggestions = list.ToArray();
				}
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0003268C File Offset: 0x0003088C
		private static DateTime ConvertToUtc(DateTime dateTime, ExTimeZone timeZone)
		{
			if (dateTime.Kind != DateTimeKind.Unspecified)
			{
				return (DateTime)((ExDateTime)dateTime).ToUtc();
			}
			if (timeZone == null)
			{
				throw new InvalidOperationException("[EntityExtractionResultProperty.ConvertToUtc] DateTimeKind is Unspecified but timeZone to use for conversion is null");
			}
			return (DateTime)timeZone.Assign((ExDateTime)dateTime).ToUtc();
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x000326F8 File Offset: 0x000308F8
		private static void AddContactsToResults(Func<StorePropertyDefinition, object> propGetterFunc, EntityExtractionResultType result)
		{
			Contact[] array = (Contact[])propGetterFunc(ItemSchema.ExtractedContacts);
			if (array != null && array.Length > 0)
			{
				List<ContactType> list = new List<ContactType>(Math.Min(100, array.Length));
				foreach (Contact contact in array)
				{
					if (EntityExtractionResultProperty.HasSupportedPosition(contact))
					{
						int num = 0;
						ContactType contactType = new ContactType();
						contactType.Position = (EmailPositionType)contact.Position;
						num++;
						if (contact.Person != null && !string.IsNullOrEmpty(contact.Person.PersonString))
						{
							contactType.PersonName = contact.Person.PersonString;
							num += contact.Person.PersonString.Length;
						}
						if (contact.Business != null && !string.IsNullOrEmpty(contact.Business.BusinessString))
						{
							contactType.BusinessName = contact.Business.BusinessString;
							num += contact.Business.BusinessString.Length;
						}
						if (num < 3072)
						{
							int num2;
							Microsoft.Exchange.Services.Core.Types.PhoneType[] phoneNumbers;
							if (EntityExtractionResultProperty.TryGetPhoneNumbers(contact.Phones, out num2, out phoneNumbers))
							{
								contactType.PhoneNumbers = phoneNumbers;
								num += num2;
							}
							if (num < 3072)
							{
								string[] array3;
								if (EntityExtractionResultProperty.TryGetStringArrayForType<Address>(contact.Addresses, (Address address) => address.AddressString, out num2, out array3))
								{
									contactType.Addresses = array3;
									num += num2;
								}
								if (num < 3072)
								{
									if (EntityExtractionResultProperty.TryGetStringArrayForType<Url>(contact.Urls, (Url url) => url.UrlString, out num2, out array3))
									{
										contactType.Urls = array3;
										num += num2;
									}
									if (num < 3072)
									{
										if (EntityExtractionResultProperty.TryGetStringArrayForType<Email>(contact.Emails, (Email email) => email.EmailString, out num2, out array3))
										{
											contactType.EmailAddresses = array3;
											num += num2;
										}
										if (num < 3072)
										{
											contactType.ContactString = contact.ContactString;
											list.Add(contactType);
											if (list.Count >= 100)
											{
												break;
											}
										}
									}
								}
							}
						}
					}
				}
				result.Contacts = list.ToArray();
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0003292C File Offset: 0x00030B2C
		private static bool TryGetStringArrayForType<T>(Func<StorePropertyDefinition, object> propGetterFunc, StorePropertyDefinition propertyDefinition, EntityExtractionResultProperty.EntityPropertyExtractor<T> extractor, out string[] extractedValues)
		{
			extractedValues = null;
			T[] nlgArray = (T[])propGetterFunc(propertyDefinition);
			int num;
			return EntityExtractionResultProperty.TryGetStringArrayForType<T>(nlgArray, extractor, out num, out extractedValues);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00032954 File Offset: 0x00030B54
		private static bool TryGetStringArrayForType<T>(T[] nlgArray, EntityExtractionResultProperty.EntityPropertyExtractor<T> extractor, out int totalLength, out string[] extractedValues)
		{
			totalLength = 0;
			extractedValues = null;
			if (nlgArray != null && nlgArray.Length > 0)
			{
				List<string> list = new List<string>(Math.Min(100, nlgArray.Length));
				foreach (T nlgObject in nlgArray)
				{
					string text = extractor(nlgObject);
					if (!string.IsNullOrEmpty(text) && text.Length < 3072)
					{
						list.Add(text);
						totalLength += text.Length;
						if (list.Count >= 100)
						{
							break;
						}
					}
				}
				if (list.Count > 0)
				{
					extractedValues = list.ToArray();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000329EC File Offset: 0x00030BEC
		private static EmailUserType[] CreateEmailUsers(EmailUser[] emailUsers)
		{
			if (emailUsers == null)
			{
				return null;
			}
			EmailUserType[] array = new EmailUserType[emailUsers.Length];
			for (int i = 0; i < emailUsers.Length; i++)
			{
				array[i] = new EmailUserType
				{
					Name = emailUsers[i].Name,
					UserId = emailUsers[i].UserId
				};
			}
			return array;
		}

		// Token: 0x040007B8 RID: 1976
		private const int MaxValues = 100;

		// Token: 0x040007B9 RID: 1977
		private const int MaxLengthPerEntity = 3072;

		// Token: 0x0200016B RID: 363
		// (Invoke) Token: 0x06000A5F RID: 2655
		private delegate string EntityPropertyExtractor<T>(T nlgObject);

		// Token: 0x0200016C RID: 364
		// (Invoke) Token: 0x06000A63 RID: 2659
		private delegate bool TryExtractEntity<T, TEntity>(T nlgObject, out TEntity extractedEntity);
	}
}

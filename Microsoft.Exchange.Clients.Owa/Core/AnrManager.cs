using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000BA RID: 186
	internal sealed class AnrManager
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x00036412 File Offset: 0x00034612
		private AnrManager()
		{
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0003641C File Offset: 0x0003461C
		public static RecipientAddress ResolveAnrStringToOneOffEmail(string name, AnrManager.Options options)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			RecipientAddress recipientAddress = null;
			Participant participant;
			if (AnrManager.TryParseParticipant(name, options, out participant) && (!options.OnlyAllowDefaultRoutingType || options.IsDefaultRoutingType(participant.RoutingType)))
			{
				recipientAddress = new RecipientAddress();
				recipientAddress.DisplayName = participant.DisplayName;
				recipientAddress.AddressOrigin = RecipientAddress.ToAddressOrigin(participant);
				recipientAddress.RoutingAddress = (AnrManager.IsMobileNumberInput(participant, options) ? participant.DisplayName : participant.EmailAddress);
				recipientAddress.RoutingType = participant.RoutingType;
				recipientAddress.SmtpAddress = ((participant.RoutingType == "SMTP") ? participant.EmailAddress : null);
				recipientAddress.MobilePhoneNumber = ((participant.RoutingType == "MOBILE") ? participant.EmailAddress : null);
				StoreParticipantOrigin storeParticipantOrigin = participant.Origin as StoreParticipantOrigin;
				if (storeParticipantOrigin != null)
				{
					recipientAddress.StoreObjectId = storeParticipantOrigin.OriginItemId;
					recipientAddress.EmailAddressIndex = storeParticipantOrigin.EmailAddressIndex;
				}
			}
			return recipientAddress;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00036511 File Offset: 0x00034711
		public static RecipientAddress ResolveAnrStringToOneOffEmail(string name)
		{
			return AnrManager.ResolveAnrStringToOneOffEmail(name, new AnrManager.Options());
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0003651E File Offset: 0x0003471E
		public static void ResolveOneRecipient(string name, UserContext userContext, List<RecipientAddress> addresses)
		{
			AnrManager.ResolveOneRecipient(name, userContext, addresses, new AnrManager.Options());
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00036530 File Offset: 0x00034730
		public static void ResolveOneRecipient(string name, UserContext userContext, List<RecipientAddress> addresses, AnrManager.Options options)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			AnrManager.NameParsingResult parsingResult = AnrManager.ParseNameBeforeAnr(name, options);
			if (!options.ResolveOnlyFromAddressBook && userContext.IsFeatureEnabled(Feature.Contacts))
			{
				AnrManager.GetNamesByAnrFromContacts(userContext, parsingResult, options, addresses);
			}
			AnrManager.GetNamesByAnrFromAD(userContext, parsingResult, options, addresses);
			if (AnrManager.IsMobileNumberInput(parsingResult, options) && addresses.Count > 0)
			{
				bool flag = false;
				foreach (RecipientAddress address in addresses)
				{
					if (AnrManager.IsMobileAddressExactMatch(parsingResult, address))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					RecipientAddress recipientAddress = AnrManager.ResolveAnrStringToOneOffEmail(name, options);
					if (recipientAddress != null)
					{
						addresses.Add(recipientAddress);
					}
				}
			}
			addresses.Sort();
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0003660C File Offset: 0x0003480C
		public static RecipientAddress ResolveAnrString(string name, bool resolveContactsFirst, UserContext userContext)
		{
			return AnrManager.ResolveAnrString(name, new AnrManager.Options
			{
				ResolveContactsFirst = resolveContactsFirst
			}, userContext);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x000366A8 File Offset: 0x000348A8
		public static RecipientAddress ResolveAnrString(string name, AnrManager.Options options, UserContext userContext)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.NamesChecked.Increment();
			}
			AnrManager.NameParsingResult parsingResult = AnrManager.ParseNameBeforeAnr(name, options);
			if (options.ResolveContactsFirst)
			{
				return AnrManager.ResolveAnrStringFromContacts(userContext, parsingResult, options, () => AnrManager.ResolveAnrStringFromAD(userContext, parsingResult, options, () => AnrManager.ResolveAnrStringToOneOffEmail(name, options)));
			}
			return AnrManager.ResolveAnrStringFromAD(userContext, parsingResult, options, () => AnrManager.ResolveAnrStringFromContacts(userContext, parsingResult, options, () => AnrManager.ResolveAnrStringToOneOffEmail(name, options)));
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00036788 File Offset: 0x00034988
		private static RecipientAddress ResolveAnrStringFromContacts(UserContext userContext, AnrManager.NameParsingResult parsingResult, AnrManager.Options options, AnrManager.NextOperation nextOperation)
		{
			List<RecipientAddress> list = new List<RecipientAddress>();
			if (!options.ResolveOnlyFromAddressBook && userContext.IsFeatureEnabled(Feature.Contacts))
			{
				AnrManager.GetNamesByAnrFromContacts(userContext, parsingResult, options, list);
			}
			if (list.Count == 1)
			{
				if (!AnrManager.IsMobileNumberInput(parsingResult, options) || AnrManager.IsMobileAddressExactMatch(parsingResult, list[0]))
				{
					return list[0];
				}
				return null;
			}
			else
			{
				if (list.Count > 1)
				{
					return null;
				}
				if (nextOperation != null)
				{
					return nextOperation();
				}
				return null;
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000367F8 File Offset: 0x000349F8
		private static RecipientAddress ResolveAnrStringFromAD(UserContext userContext, AnrManager.NameParsingResult parsingResult, AnrManager.Options options, AnrManager.NextOperation nextOperation)
		{
			List<RecipientAddress> list = new List<RecipientAddress>();
			AnrManager.GetNamesByAnrFromAD(userContext, parsingResult, options, list);
			if (list.Count == 1)
			{
				if (!AnrManager.IsMobileNumberInput(parsingResult, options) || AnrManager.IsMobileAddressExactMatch(parsingResult, list[0]))
				{
					return list[0];
				}
				return null;
			}
			else
			{
				if (list.Count > 1)
				{
					return null;
				}
				if (nextOperation != null)
				{
					return nextOperation();
				}
				return null;
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00036858 File Offset: 0x00034A58
		private static bool AreNameAndAddressTheSameNumber(AnrManager.Options options, string routingType, string displayName, string routingAddress)
		{
			if (!options.IsDefaultRoutingType("MOBILE"))
			{
				return false;
			}
			if (!Utilities.IsMobileRoutingType(routingType))
			{
				return false;
			}
			E164Number objA = null;
			E164Number objB = null;
			return E164Number.TryParse(displayName, out objA) && E164Number.TryParse(routingAddress, out objB) && object.Equals(objA, objB);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000368A1 File Offset: 0x00034AA1
		private static bool IsMobileNumberInput(AnrManager.NameParsingResult parsingResult, AnrManager.Options options)
		{
			return parsingResult.ParsedSuccessfully && AnrManager.AreNameAndAddressTheSameNumber(options, parsingResult.RoutingType, parsingResult.Name, parsingResult.RoutingAddress);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000368C9 File Offset: 0x00034AC9
		private static bool IsMobileNumberInput(Participant parsedParticipant, AnrManager.Options options)
		{
			return parsedParticipant.ValidationStatus == ParticipantValidationStatus.NoError && AnrManager.AreNameAndAddressTheSameNumber(options, parsedParticipant.RoutingType, parsedParticipant.DisplayName, parsedParticipant.EmailAddress);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000368F0 File Offset: 0x00034AF0
		private static bool IsMobileAddressExactMatch(AnrManager.NameParsingResult parsingResult, RecipientAddress address)
		{
			if (!parsingResult.ParsedSuccessfully)
			{
				return false;
			}
			if (!Utilities.IsMobileRoutingType(parsingResult.RoutingType))
			{
				return false;
			}
			if (!Utilities.IsMobileRoutingType(address.RoutingType))
			{
				return false;
			}
			E164Number objA = null;
			E164Number objB = null;
			return E164Number.TryParse(parsingResult.RoutingAddress, out objA) && E164Number.TryParse(address.RoutingAddress, out objB) && object.Equals(objA, objB);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00036958 File Offset: 0x00034B58
		private static object FindFromResultsMapping(PropertyDefinition property, PropertyDefinition[] properties, object[] results)
		{
			if (properties.Length != results.Length)
			{
				throw new InvalidOperationException("The lengths should be the same");
			}
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i] == property)
				{
					return results[i];
				}
			}
			return null;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00036994 File Offset: 0x00034B94
		private static void GetNamesByAnrFromContacts(UserContext userContext, AnrManager.NameParsingResult parsingResult, AnrManager.Options options, List<RecipientAddress> addresses)
		{
			if (string.IsNullOrEmpty(parsingResult.Name))
			{
				return;
			}
			if (userContext.TryGetMyDefaultFolderId(DefaultFolderType.Contacts) == null)
			{
				return;
			}
			string ambiguousName = parsingResult.ParsedSuccessfully ? parsingResult.RoutingAddress : parsingResult.Name;
			using (ContactsFolder contactsFolder = ContactsFolder.Bind(userContext.MailboxSession, DefaultFolderType.Contacts))
			{
				if (contactsFolder.IsValidAmbiguousName(ambiguousName))
				{
					PropertyDefinition[] array;
					object[][] results;
					if (AnrManager.IsMobileNumberInput(parsingResult, options))
					{
						array = AnrManager.AnrProperties.Get(AnrManager.AnrProperties.PropertiesType.ContactFindSomeone, options);
						results = contactsFolder.FindNamesView(new Dictionary<PropertyDefinition, object>
						{
							{
								ContactSchema.MobilePhone,
								parsingResult.Name
							}
						}, AnrManager.nameLimit, null, array);
					}
					else if (options.ResolveAgainstAllContacts || options.IsDefaultRoutingType("MOBILE"))
					{
						array = AnrManager.AnrProperties.Get(AnrManager.AnrProperties.PropertiesType.ContactFindSomeone, options);
						results = contactsFolder.FindSomeoneView(ambiguousName, AnrManager.nameLimit, null, array);
					}
					else
					{
						array = AnrManager.AnrProperties.Get(AnrManager.AnrProperties.PropertiesType.ContactAnr, options);
						results = contactsFolder.ResolveAmbiguousNameView(ambiguousName, AnrManager.nameLimit, null, array);
					}
					AnrManager.AddContacts(userContext, options, array, results, addresses);
				}
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00036A9C File Offset: 0x00034C9C
		private static void AddContacts(UserContext userContext, AnrManager.Options options, PropertyDefinition[] properties, object[][] results, List<RecipientAddress> addresses)
		{
			if (results != null && results.GetLength(0) > 0)
			{
				int i = 0;
				while (i < results.GetLength(0))
				{
					object[] results2 = results[i];
					Participant participant = null;
					string displayName = null;
					string text = Utilities.NormalizePhoneNumber(AnrManager.FindFromResultsMapping(ContactSchema.MobilePhone, properties, results2) as string);
					VersionedId versionedId = AnrManager.FindFromResultsMapping(ItemSchema.Id, properties, results2) as VersionedId;
					if (!options.ResolveAgainstAllContacts && !options.IsDefaultRoutingType("MOBILE"))
					{
						participant = (AnrManager.FindFromResultsMapping(ContactBaseSchema.AnrViewParticipant, properties, results2) as Participant);
						displayName = participant.DisplayName;
						goto IL_1AB;
					}
					Participant participant2 = AnrManager.FindFromResultsMapping(DistributionListSchema.AsParticipant, properties, results2) as Participant;
					if (participant2 != null)
					{
						participant = participant2;
						displayName = participant.DisplayName;
						goto IL_1AB;
					}
					if (options.IsDefaultRoutingType("MOBILE"))
					{
						if (!string.IsNullOrEmpty(text))
						{
							displayName = (AnrManager.FindFromResultsMapping(StoreObjectSchema.DisplayName, properties, results2) as string);
							participant = new Participant(displayName, text, "MOBILE", new StoreParticipantOrigin(versionedId), new KeyValuePair<PropertyDefinition, object>[0]);
						}
						else if (options.OnlyAllowDefaultRoutingType)
						{
							goto IL_339;
						}
					}
					if (!(participant == null))
					{
						goto IL_1AB;
					}
					Participant participant3 = AnrManager.FindFromResultsMapping(ContactSchema.Email1, properties, results2) as Participant;
					Participant participant4 = AnrManager.FindFromResultsMapping(ContactSchema.Email2, properties, results2) as Participant;
					Participant participant5 = AnrManager.FindFromResultsMapping(ContactSchema.Email3, properties, results2) as Participant;
					if (participant3 != null && !string.IsNullOrEmpty(participant3.EmailAddress))
					{
						participant = participant3;
						displayName = participant.DisplayName;
						goto IL_1AB;
					}
					if (participant4 != null && !string.IsNullOrEmpty(participant4.EmailAddress))
					{
						participant = participant4;
						displayName = participant.DisplayName;
						goto IL_1AB;
					}
					if (participant5 != null && !string.IsNullOrEmpty(participant5.EmailAddress))
					{
						participant = participant5;
						displayName = participant.DisplayName;
						goto IL_1AB;
					}
					goto IL_1AB;
					IL_339:
					i++;
					continue;
					IL_1AB:
					RecipientAddress recipientAddress = new RecipientAddress();
					recipientAddress.MobilePhoneNumber = text;
					recipientAddress.DisplayName = displayName;
					recipientAddress.AddressOrigin = AddressOrigin.Store;
					if (participant != null)
					{
						if (Utilities.IsMapiPDL(participant.RoutingType) && Utilities.IsFlagSet((int)options.RecipientBlockType, 2))
						{
							goto IL_339;
						}
						recipientAddress.RoutingType = participant.RoutingType;
						recipientAddress.EmailAddressIndex = ((StoreParticipantOrigin)participant.Origin).EmailAddressIndex;
						if (!string.IsNullOrEmpty(participant.EmailAddress))
						{
							recipientAddress.RoutingAddress = participant.EmailAddress;
							if (string.CompareOrdinal(recipientAddress.RoutingType, "EX") == 0)
							{
								string text2 = participant.TryGetProperty(ParticipantSchema.SmtpAddress) as string;
								if (string.IsNullOrEmpty(text2))
								{
									IRecipientSession recipientSession = Utilities.CreateADRecipientSession(Culture.GetUserCulture().LCID, true, ConsistencyMode.IgnoreInvalid, true, userContext);
									ADRecipient adrecipient = null;
									try
									{
										adrecipient = recipientSession.FindByLegacyExchangeDN(recipientAddress.RoutingAddress);
									}
									catch (NonUniqueRecipientException ex)
									{
										ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "AnrManager.GetNamesByAnrFromContacts: NonUniqueRecipientException was thrown by FindByLegacyExchangeDN: {0}", ex.Message);
									}
									if (adrecipient == null || adrecipient.HiddenFromAddressListsEnabled)
									{
										goto IL_339;
									}
									recipientAddress.SmtpAddress = adrecipient.PrimarySmtpAddress.ToString();
								}
								else
								{
									recipientAddress.SmtpAddress = text2;
								}
							}
							else if (string.CompareOrdinal(recipientAddress.RoutingType, "SMTP") == 0)
							{
								recipientAddress.SmtpAddress = recipientAddress.RoutingAddress;
							}
						}
					}
					if (Utilities.IsMapiPDL(recipientAddress.RoutingType))
					{
						recipientAddress.IsDistributionList = true;
					}
					if (versionedId != null)
					{
						recipientAddress.StoreObjectId = versionedId.ObjectId;
					}
					addresses.Add(recipientAddress);
					goto IL_339;
				}
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00036E04 File Offset: 0x00035004
		private static void GetNamesByAnrFromAD(UserContext userContext, AnrManager.NameParsingResult parsingResult, AnrManager.Options options, List<RecipientAddress> addresses)
		{
			IRecipientSession recipientSession = Utilities.CreateADRecipientSession(Culture.GetUserCulture().LCID, true, ConsistencyMode.IgnoreInvalid, true, userContext);
			ADRawEntry[] array = null;
			bool flag = parsingResult.ParsedSuccessfully && !Utilities.IsMobileRoutingType(parsingResult.RoutingType);
			string text = flag ? string.Format("{0}:{1}", parsingResult.RoutingType, parsingResult.RoutingAddress) : parsingResult.Name;
			if (flag)
			{
				ADRawEntry adrawEntry = recipientSession.FindByProxyAddress(ProxyAddress.Parse(text), AnrManager.AnrProperties.Get(AnrManager.AnrProperties.PropertiesType.AD, options));
				if (adrawEntry != null)
				{
					if ((bool)adrawEntry[ADRecipientSchema.HiddenFromAddressListsEnabled])
					{
						array = new ADRawEntry[0];
						ExTraceGlobals.CoreTracer.TraceDebug<ADObjectId>(0L, "AnrManager.GetNamesByAnrFromAD: Recipient ignored because it is hiddem from address lists: {0}", adrawEntry.Id);
					}
					else
					{
						array = new ADRawEntry[]
						{
							adrawEntry
						};
					}
				}
			}
			if (array == null)
			{
				QueryFilter filter = new AndFilter(new QueryFilter[]
				{
					new AmbiguousNameResolutionFilter(text),
					AnrManager.addressListMembershipExists
				});
				array = recipientSession.Find(null, QueryScope.SubTree, filter, null, AnrManager.nameLimit, AnrManager.AnrProperties.Get(AnrManager.AnrProperties.PropertiesType.AD, options));
			}
			AnrManager.AddADRecipients(array, options, addresses);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00036F0C File Offset: 0x0003510C
		private static void AddADRecipients(ADRawEntry[] adRecipients, AnrManager.Options options, List<RecipientAddress> addresses)
		{
			if (adRecipients != null)
			{
				foreach (ADRawEntry adrawEntry in adRecipients)
				{
					RecipientType recipientType = (RecipientType)adrawEntry[ADRecipientSchema.RecipientType];
					if (recipientType == RecipientType.UserMailbox || recipientType == RecipientType.MailUniversalDistributionGroup || recipientType == RecipientType.MailUniversalSecurityGroup || recipientType == RecipientType.MailNonUniversalGroup || recipientType == RecipientType.MailUser || recipientType == RecipientType.MailContact || recipientType == RecipientType.DynamicDistributionGroup || recipientType == RecipientType.PublicFolder)
					{
						bool flag = Utilities.IsADDistributionList((MultiValuedProperty<string>)adrawEntry[ADObjectSchema.ObjectClass]);
						string text = Utilities.NormalizePhoneNumber((string)adrawEntry[ADOrgPersonSchema.MobilePhone]);
						if (!flag || !Utilities.IsFlagSet((int)options.RecipientBlockType, 1))
						{
							bool isRoom = DirectoryAssistance.IsADRecipientRoom((RecipientDisplayType?)adrawEntry[ADRecipientSchema.RecipientDisplayType]);
							RecipientAddress recipientAddress = null;
							if (!flag && options.IsDefaultRoutingType("MOBILE"))
							{
								if (!string.IsNullOrEmpty(text))
								{
									recipientAddress = new RecipientAddress();
									recipientAddress.RoutingType = "MOBILE";
									recipientAddress.RoutingAddress = text;
								}
								else if (options.OnlyAllowDefaultRoutingType)
								{
									goto IL_1BD;
								}
							}
							if (recipientAddress == null)
							{
								recipientAddress = new RecipientAddress();
								recipientAddress.Alias = (string)adrawEntry[ADRecipientSchema.Alias];
								recipientAddress.RoutingAddress = (string)adrawEntry[ADRecipientSchema.LegacyExchangeDN];
								recipientAddress.RoutingType = "EX";
								recipientAddress.SmtpAddress = adrawEntry[ADRecipientSchema.PrimarySmtpAddress].ToString();
							}
							recipientAddress.AddressOrigin = AddressOrigin.Directory;
							recipientAddress.ADObjectId = (ADObjectId)adrawEntry[ADObjectSchema.Id];
							recipientAddress.IsRoom = isRoom;
							recipientAddress.DisplayName = (string)adrawEntry[ADRecipientSchema.DisplayName];
							recipientAddress.IsDistributionList = flag;
							recipientAddress.RecipientType = recipientType;
							recipientAddress.SipUri = InstantMessageUtilities.GetSipUri((ProxyAddressCollection)adrawEntry[ADRecipientSchema.EmailAddresses]);
							recipientAddress.MobilePhoneNumber = text;
							addresses.Add(recipientAddress);
						}
					}
					IL_1BD:;
				}
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000370E4 File Offset: 0x000352E4
		private static AnrManager.NameParsingResult ParseNameBeforeAnr(string name, AnrManager.Options options)
		{
			AnrManager.NameParsingResult result = new AnrManager.NameParsingResult(name);
			Participant participant;
			if (AnrManager.TryParseParticipant(name, options, out participant))
			{
				result.ParsedSuccessfully = true;
				result.RoutingType = participant.RoutingType;
				result.RoutingAddress = participant.EmailAddress;
			}
			return result;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00037127 File Offset: 0x00035327
		private static bool TryParseParticipant(string input, AnrManager.Options options, out Participant participant)
		{
			if (string.IsNullOrEmpty(input))
			{
				participant = null;
				return false;
			}
			return Participant.TryParse(input, out participant) && participant.ValidationStatus == ParticipantValidationStatus.NoError && participant.RoutingType != null;
		}

		// Token: 0x040004C3 RID: 1219
		private const string ADAnrLookupFormat = "{0}:{1}";

		// Token: 0x040004C4 RID: 1220
		private static readonly int nameLimit = 100;

		// Token: 0x040004C5 RID: 1221
		private static readonly AndFilter addressListMembershipExists = new AndFilter(new QueryFilter[]
		{
			new ExistsFilter(ADRecipientSchema.AddressListMembership),
			new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.HiddenFromAddressListsEnabled, false)
		});

		// Token: 0x020000BB RID: 187
		public sealed class Options
		{
			// Token: 0x170001FC RID: 508
			// (get) Token: 0x060006F6 RID: 1782 RVA: 0x000371A3 File Offset: 0x000353A3
			// (set) Token: 0x060006F7 RID: 1783 RVA: 0x000371AB File Offset: 0x000353AB
			public bool ResolveContactsFirst { get; set; }

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x060006F8 RID: 1784 RVA: 0x000371B4 File Offset: 0x000353B4
			// (set) Token: 0x060006F9 RID: 1785 RVA: 0x000371BC File Offset: 0x000353BC
			public bool ResolveOnlyFromAddressBook { get; set; }

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x060006FA RID: 1786 RVA: 0x000371C5 File Offset: 0x000353C5
			// (set) Token: 0x060006FB RID: 1787 RVA: 0x000371CD File Offset: 0x000353CD
			public bool ResolveAgainstAllContacts { get; set; }

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x060006FC RID: 1788 RVA: 0x000371D6 File Offset: 0x000353D6
			// (set) Token: 0x060006FD RID: 1789 RVA: 0x000371DE File Offset: 0x000353DE
			public bool OnlyAllowDefaultRoutingType { get; set; }

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x060006FE RID: 1790 RVA: 0x000371E7 File Offset: 0x000353E7
			// (set) Token: 0x060006FF RID: 1791 RVA: 0x000371EF File Offset: 0x000353EF
			public RecipientBlockType RecipientBlockType { get; set; }

			// Token: 0x17000201 RID: 513
			// (get) Token: 0x06000700 RID: 1792 RVA: 0x000371F8 File Offset: 0x000353F8
			// (set) Token: 0x06000701 RID: 1793 RVA: 0x00037200 File Offset: 0x00035400
			public string DefaultRoutingType { get; set; }

			// Token: 0x06000702 RID: 1794 RVA: 0x00037209 File Offset: 0x00035409
			public bool IsDefaultRoutingType(string routingType)
			{
				return !string.IsNullOrEmpty(this.DefaultRoutingType) && string.Equals(routingType, this.DefaultRoutingType, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x020000BC RID: 188
		private static class AnrProperties
		{
			// Token: 0x06000704 RID: 1796 RVA: 0x00037230 File Offset: 0x00035430
			public static PropertyDefinition[] Get(AnrManager.AnrProperties.PropertiesType type, AnrManager.Options options)
			{
				switch (type)
				{
				case AnrManager.AnrProperties.PropertiesType.AD:
					return AnrManager.AnrProperties.adWithMobilePhoneNumber;
				case AnrManager.AnrProperties.PropertiesType.ContactAnr:
					if (options.IsDefaultRoutingType("MOBILE"))
					{
						throw new InvalidOperationException("Should not call contact ANR for mobile numbers");
					}
					return AnrManager.AnrProperties.contactAnr;
				case AnrManager.AnrProperties.PropertiesType.ContactFindSomeone:
					return AnrManager.AnrProperties.contactEmailAndMobilePhoneNumber;
				default:
					throw new ArgumentOutOfRangeException("type");
				}
			}

			// Token: 0x040004CC RID: 1228
			private static PropertyDefinition[] contactAnr = new PropertyDefinition[]
			{
				ParticipantSchema.DisplayName,
				ContactBaseSchema.AnrViewParticipant,
				ItemSchema.Id
			};

			// Token: 0x040004CD RID: 1229
			private static PropertyDefinition[] contactEmailAndMobilePhoneNumber = new PropertyDefinition[]
			{
				StoreObjectSchema.DisplayName,
				DistributionListSchema.AsParticipant,
				ItemSchema.Id,
				ContactSchema.Email1,
				ContactSchema.Email2,
				ContactSchema.Email3,
				ContactSchema.MobilePhone
			};

			// Token: 0x040004CE RID: 1230
			private static PropertyDefinition[] adWithMobilePhoneNumber = new PropertyDefinition[]
			{
				ADRecipientSchema.RecipientType,
				ADRecipientSchema.Alias,
				ADRecipientSchema.DisplayName,
				ADRecipientSchema.LegacyExchangeDN,
				ADRecipientSchema.PrimarySmtpAddress,
				ADRecipientSchema.RecipientDisplayType,
				ADRecipientSchema.HiddenFromAddressListsEnabled,
				ADOrgPersonSchema.MobilePhone,
				ADObjectSchema.Id
			};

			// Token: 0x020000BD RID: 189
			public enum PropertiesType
			{
				// Token: 0x040004D0 RID: 1232
				None,
				// Token: 0x040004D1 RID: 1233
				AD,
				// Token: 0x040004D2 RID: 1234
				ContactAnr,
				// Token: 0x040004D3 RID: 1235
				ContactFindSomeone
			}
		}

		// Token: 0x020000BE RID: 190
		// (Invoke) Token: 0x06000707 RID: 1799
		private delegate RecipientAddress NextOperation();

		// Token: 0x020000BF RID: 191
		private struct NameParsingResult
		{
			// Token: 0x0600070A RID: 1802 RVA: 0x0003735C File Offset: 0x0003555C
			public NameParsingResult(string name)
			{
				this.Name = name;
				this.ParsedSuccessfully = false;
				this.RoutingType = (this.RoutingAddress = string.Empty);
			}

			// Token: 0x040004D4 RID: 1236
			public bool ParsedSuccessfully;

			// Token: 0x040004D5 RID: 1237
			public string Name;

			// Token: 0x040004D6 RID: 1238
			public string RoutingType;

			// Token: 0x040004D7 RID: 1239
			public string RoutingAddress;
		}
	}
}

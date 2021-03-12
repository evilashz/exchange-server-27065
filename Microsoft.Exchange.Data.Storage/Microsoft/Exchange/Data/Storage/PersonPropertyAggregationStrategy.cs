using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200052A RID: 1322
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class PersonPropertyAggregationStrategy
	{
		// Token: 0x060038D7 RID: 14551 RVA: 0x000E8F76 File Offset: 0x000E7176
		public static PropertyAggregationStrategy CreateNameProperty(StorePropertyDefinition sourceProperty)
		{
			return new PropertyAggregationStrategy.SingleValuePropertyAggregation(ContactSelectionStrategy.CreatePersonNameProperty(sourceProperty));
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000E8F83 File Offset: 0x000E7183
		public static PropertyAggregationStrategy CreatePriorityPropertyAggregation(StorePropertyDefinition sourceProperty)
		{
			return new PropertyAggregationStrategy.SingleValuePropertyAggregation(ContactSelectionStrategy.CreateSingleSourceProperty(sourceProperty));
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x000E8F90 File Offset: 0x000E7190
		public static PropertyAggregationStrategy CreateExtendedPropertiesAggregation(params StorePropertyDefinition[] extendedProperties)
		{
			return new PersonPropertyAggregationStrategy.ExtendedPropertiesAggregation(extendedProperties);
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000E8F98 File Offset: 0x000E7198
		private static bool TryGetArrayResult<T>(IList<T> list, out object value)
		{
			if (list.Count > 0)
			{
				value = list.ToArray<T>();
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000E8FB4 File Offset: 0x000E71B4
		private static bool IsValidEmailAddress(EmailAddress emailAddress)
		{
			if (emailAddress != null)
			{
				if (StringComparer.OrdinalIgnoreCase.Equals(emailAddress.RoutingType, "EX") && !string.IsNullOrEmpty(emailAddress.OriginalDisplayName) && SmtpAddress.IsValidSmtpAddress(emailAddress.OriginalDisplayName))
				{
					return true;
				}
				if (StringComparer.OrdinalIgnoreCase.Equals(emailAddress.RoutingType, "SMTP") && !string.IsNullOrEmpty(emailAddress.Address))
				{
					return true;
				}
				if (!string.IsNullOrEmpty(emailAddress.RoutingType))
				{
					return true;
				}
				if (StringComparer.OrdinalIgnoreCase.Equals(emailAddress.RoutingType, string.Empty) && !string.IsNullOrEmpty(emailAddress.OriginalDisplayName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001E31 RID: 7729
		private static readonly Trace Tracer = ExTraceGlobals.PersonTracer;

		// Token: 0x04001E32 RID: 7730
		public static readonly PropertyAggregationStrategy FileAsIdProperty = new PropertyAggregationStrategy.SingleValuePropertyAggregation(ContactSelectionStrategy.FileAsIdProperty);

		// Token: 0x04001E33 RID: 7731
		public static readonly PropertyAggregationStrategy EmailAddressProperty = new PersonPropertyAggregationStrategy.EmailAddressAggregation();

		// Token: 0x04001E34 RID: 7732
		public static readonly PropertyAggregationStrategy EmailAddressesProperty = new PersonPropertyAggregationStrategy.EmailAddressesAggregation();

		// Token: 0x04001E35 RID: 7733
		public static readonly PropertyAggregationStrategy IMAddressProperty = new PersonPropertyAggregationStrategy.IMAddressAggregation();

		// Token: 0x04001E36 RID: 7734
		public static readonly PropertyAggregationStrategy GALLinkIDProperty = new PersonPropertyAggregationStrategy.GALLinkIDAggregation();

		// Token: 0x04001E37 RID: 7735
		public static readonly PropertyAggregationStrategy PostalAddressesProperty = new PersonPropertyAggregationStrategy.PostalAddressesAggregation(PostalAddressProperties.AllPropertiesForConversationView, new Func<PostalAddressProperties, IStorePropertyBag, PostalAddress>(PersonPropertyAggregationStrategy.PostalAddressesAggregation.GetFromAllPropertiesForConversationView));

		// Token: 0x04001E38 RID: 7736
		public static readonly PropertyAggregationStrategy PostalAddressesWithDetailsProperty = new PersonPropertyAggregationStrategy.PostalAddressesAggregation(PostalAddressProperties.AllProperties, new Func<PostalAddressProperties, IStorePropertyBag, PostalAddress>(PersonPropertyAggregationStrategy.PostalAddressesAggregation.GetFromAllProperties));

		// Token: 0x04001E39 RID: 7737
		public static readonly PropertyAggregationStrategy PostalAddressProperty = new PersonPropertyAggregationStrategy.PostalAddressAggregation();

		// Token: 0x04001E3A RID: 7738
		public static readonly PropertyAggregationStrategy IsPersonalContactProperty = new PersonPropertyAggregationStrategy.IsPersonalContactAggregation();

		// Token: 0x04001E3B RID: 7739
		public static readonly PropertyAggregationStrategy CreationTimeProperty = new PropertyAggregationStrategy.CreationTimeAggregation();

		// Token: 0x04001E3C RID: 7740
		public static readonly PropertyAggregationStrategy IsFavoriteProperty = new PersonPropertyAggregationStrategy.IsFavoriteAggregation();

		// Token: 0x04001E3D RID: 7741
		public static readonly PropertyAggregationStrategy RelevanceScoreProperty = new PersonPropertyAggregationStrategy.RelevanceScoreAggregation();

		// Token: 0x04001E3E RID: 7742
		public static readonly PropertyAggregationStrategy PhotoContactEntryIdProperty = new PropertyAggregationStrategy.SingleValuePropertyAggregation(ContactSelectionStrategy.PhotoContactIdProperty);

		// Token: 0x04001E3F RID: 7743
		public static readonly PropertyAggregationStrategy AttributedThirdPartyPhotoUrlsProperty = new PersonPropertyAggregationStrategy.AttributedThirdPartyPhotoUrlPropertyAggregation();

		// Token: 0x04001E40 RID: 7744
		public static readonly PropertyAggregationStrategy PersonIdProperty = new PersonPropertyAggregationStrategy.PersonIdAggregation();

		// Token: 0x04001E41 RID: 7745
		public static readonly PropertyAggregationStrategy PersonTypeProperty = new PersonPropertyAggregationStrategy.PersonTypeAggregation();

		// Token: 0x04001E42 RID: 7746
		public static readonly PropertyAggregationStrategy FolderIdsProperty = new PersonPropertyAggregationStrategy.FolderIdsAggregation();

		// Token: 0x04001E43 RID: 7747
		public static readonly PropertyAggregationStrategy AttributionsProperty = new PersonPropertyAggregationStrategy.AttributionsAggregation();

		// Token: 0x04001E44 RID: 7748
		public static readonly PropertyAggregationStrategy AttributedDisplayNamesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(StoreObjectSchema.DisplayName);

		// Token: 0x04001E45 RID: 7749
		public static readonly PropertyAggregationStrategy AttributedFileAsesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactBaseSchema.FileAs);

		// Token: 0x04001E46 RID: 7750
		public static readonly PropertyAggregationStrategy AttributedFileAsIdsProperty = new PersonPropertyAggregationStrategy.AttributedAsStringPropertyAggregation<FileAsMapping>(ContactSchema.FileAsId);

		// Token: 0x04001E47 RID: 7751
		public static readonly PropertyAggregationStrategy AttributedChildrenProperty = new PersonPropertyAggregationStrategy.AttributedStringArrayPropertyAggregation(ContactSchema.Children);

		// Token: 0x04001E48 RID: 7752
		public static readonly PropertyAggregationStrategy AttributedDisplayNamePrefixesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.DisplayNamePrefix);

		// Token: 0x04001E49 RID: 7753
		public static readonly PropertyAggregationStrategy AttributedGivenNamesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.GivenName);

		// Token: 0x04001E4A RID: 7754
		public static readonly PropertyAggregationStrategy AttributedMiddleNamesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.MiddleName);

		// Token: 0x04001E4B RID: 7755
		public static readonly PropertyAggregationStrategy AttributedSurnamesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.Surname);

		// Token: 0x04001E4C RID: 7756
		public static readonly PropertyAggregationStrategy AttributedGenerationsProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.Generation);

		// Token: 0x04001E4D RID: 7757
		public static readonly PropertyAggregationStrategy AttributedNicknamesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.Nickname);

		// Token: 0x04001E4E RID: 7758
		public static readonly PropertyAggregationStrategy AttributedInitialsProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.Initials);

		// Token: 0x04001E4F RID: 7759
		public static readonly PropertyAggregationStrategy AttributedYomiCompanyNamesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.YomiCompany);

		// Token: 0x04001E50 RID: 7760
		public static readonly PropertyAggregationStrategy AttributedYomiFirstNamesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.YomiFirstName);

		// Token: 0x04001E51 RID: 7761
		public static readonly PropertyAggregationStrategy AttributedYomiLastNamesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.YomiLastName);

		// Token: 0x04001E52 RID: 7762
		public static readonly PropertyAggregationStrategy AttributedBusinessPhoneNumbersProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.BusinessPhoneNumber, PersonPhoneNumberType.Business);

		// Token: 0x04001E53 RID: 7763
		public static readonly PropertyAggregationStrategy AttributedBusinessPhoneNumbers2Property = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.BusinessPhoneNumber2, PersonPhoneNumberType.Business);

		// Token: 0x04001E54 RID: 7764
		public static readonly PropertyAggregationStrategy AttributedHomePhonesProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.HomePhone, PersonPhoneNumberType.Home);

		// Token: 0x04001E55 RID: 7765
		public static readonly PropertyAggregationStrategy AttributedHomePhones2Property = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.HomePhone2, PersonPhoneNumberType.Home);

		// Token: 0x04001E56 RID: 7766
		public static readonly PropertyAggregationStrategy AttributedMobilePhonesProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyPlusProtectedPropertyAggregation(ContactSchema.MobilePhone, PersonPhoneNumberType.Mobile, InternalSchema.ProtectedPhoneNumber);

		// Token: 0x04001E57 RID: 7767
		public static readonly PropertyAggregationStrategy AttributedMobilePhones2Property = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.MobilePhone2, PersonPhoneNumberType.Mobile);

		// Token: 0x04001E58 RID: 7768
		public static readonly PropertyAggregationStrategy AttributedAssistantPhoneNumbersProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.AssistantPhoneNumber, PersonPhoneNumberType.Assistant);

		// Token: 0x04001E59 RID: 7769
		public static readonly PropertyAggregationStrategy AttributedCallbackPhonesProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.CallbackPhone, PersonPhoneNumberType.Callback);

		// Token: 0x04001E5A RID: 7770
		public static readonly PropertyAggregationStrategy AttributedCarPhonesProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.CarPhone, PersonPhoneNumberType.Car);

		// Token: 0x04001E5B RID: 7771
		public static readonly PropertyAggregationStrategy AttributedHomeFaxesProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.HomeFax, PersonPhoneNumberType.HomeFax);

		// Token: 0x04001E5C RID: 7772
		public static readonly PropertyAggregationStrategy AttributedOrganizationMainPhonesProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.OrganizationMainPhone, PersonPhoneNumberType.OrganizationMain);

		// Token: 0x04001E5D RID: 7773
		public static readonly PropertyAggregationStrategy AttributedOtherFaxesProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.OtherFax, PersonPhoneNumberType.OtherFax);

		// Token: 0x04001E5E RID: 7774
		public static readonly PropertyAggregationStrategy AttributedOtherTelephonesProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.OtherTelephone, PersonPhoneNumberType.Other);

		// Token: 0x04001E5F RID: 7775
		public static readonly PropertyAggregationStrategy AttributedOtherPhones2Property = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.OtherPhone2, PersonPhoneNumberType.Other);

		// Token: 0x04001E60 RID: 7776
		public static readonly PropertyAggregationStrategy AttributedPagersProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.Pager, PersonPhoneNumberType.Pager);

		// Token: 0x04001E61 RID: 7777
		public static readonly PropertyAggregationStrategy AttributedRadioPhonesProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.RadioPhone, PersonPhoneNumberType.Radio);

		// Token: 0x04001E62 RID: 7778
		public static readonly PropertyAggregationStrategy AttributedTelexNumbersProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.TelexNumber, PersonPhoneNumberType.Telex);

		// Token: 0x04001E63 RID: 7779
		public static readonly PropertyAggregationStrategy AttributedTtyTddPhoneNumbersProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.TtyTddPhoneNumber, PersonPhoneNumberType.TTYTDD);

		// Token: 0x04001E64 RID: 7780
		public static readonly PropertyAggregationStrategy AttributedWorkFaxesProperty = new PersonPropertyAggregationStrategy.AttributedPhoneNumberPropertyAggregation(ContactSchema.WorkFax, PersonPhoneNumberType.BusinessFax);

		// Token: 0x04001E65 RID: 7781
		public static readonly PropertyAggregationStrategy AttributedEmails1Property = new PersonPropertyAggregationStrategy.AttributedEmailAddressPropertyPlusProtectedPropertyAggregation(EmailAddressProperties.Email1, InternalSchema.ProtectedEmailAddress);

		// Token: 0x04001E66 RID: 7782
		public static readonly PropertyAggregationStrategy AttributedEmails2Property = new PersonPropertyAggregationStrategy.AttributedEmailAddressPropertyAggregation(EmailAddressProperties.Email2);

		// Token: 0x04001E67 RID: 7783
		public static readonly PropertyAggregationStrategy AttributedEmails3Property = new PersonPropertyAggregationStrategy.AttributedEmailAddressPropertyAggregation(EmailAddressProperties.Email3);

		// Token: 0x04001E68 RID: 7784
		public static readonly PropertyAggregationStrategy AttributedBusinessHomePagesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.BusinessHomePage);

		// Token: 0x04001E69 RID: 7785
		public static readonly PropertyAggregationStrategy AttributedSchoolsProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.Schools);

		// Token: 0x04001E6A RID: 7786
		public static readonly PropertyAggregationStrategy AttributedPersonalHomePagesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.PersonalHomePage);

		// Token: 0x04001E6B RID: 7787
		public static readonly PropertyAggregationStrategy AttributedOfficeLocationsProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.OfficeLocation);

		// Token: 0x04001E6C RID: 7788
		public static readonly PropertyAggregationStrategy AttributedIMAddressesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.IMAddress);

		// Token: 0x04001E6D RID: 7789
		public static readonly PropertyAggregationStrategy AttributedIMAddresses2Property = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.IMAddress2);

		// Token: 0x04001E6E RID: 7790
		public static readonly PropertyAggregationStrategy AttributedIMAddresses3Property = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.IMAddress3);

		// Token: 0x04001E6F RID: 7791
		public static readonly PropertyAggregationStrategy AttributedWorkAddressesProperty = new PersonPropertyAggregationStrategy.AttributedPostalAddressPropertyAggregation(PostalAddressProperties.WorkAddress);

		// Token: 0x04001E70 RID: 7792
		public static readonly PropertyAggregationStrategy AttributedHomeAddressesProperty = new PersonPropertyAggregationStrategy.AttributedPostalAddressPropertyAggregation(PostalAddressProperties.HomeAddress);

		// Token: 0x04001E71 RID: 7793
		public static readonly PropertyAggregationStrategy AttributedOtherAddressesProperty = new PersonPropertyAggregationStrategy.AttributedPostalAddressPropertyAggregation(PostalAddressProperties.OtherAddress);

		// Token: 0x04001E72 RID: 7794
		public static readonly PropertyAggregationStrategy AttributedTitlesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.Title);

		// Token: 0x04001E73 RID: 7795
		public static readonly PropertyAggregationStrategy AttributedDepartmentsProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.Department);

		// Token: 0x04001E74 RID: 7796
		public static readonly PropertyAggregationStrategy AttributedCompanyNamesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.CompanyName);

		// Token: 0x04001E75 RID: 7797
		public static readonly PropertyAggregationStrategy AttributedManagersProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.Manager);

		// Token: 0x04001E76 RID: 7798
		public static readonly PropertyAggregationStrategy AttributedAssistantNamesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.AssistantName);

		// Token: 0x04001E77 RID: 7799
		public static readonly PropertyAggregationStrategy AttributedProfessionsProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.Profession);

		// Token: 0x04001E78 RID: 7800
		public static readonly PropertyAggregationStrategy AttributedSpouseNamesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.SpouseName);

		// Token: 0x04001E79 RID: 7801
		public static readonly PropertyAggregationStrategy AttributedHobbiesProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.Hobbies);

		// Token: 0x04001E7A RID: 7802
		public static readonly PropertyAggregationStrategy AttributedWeddingAnniversariesProperty = new PersonPropertyAggregationStrategy.AttributedDateTimePropertyAggregation(ContactSchema.WeddingAnniversary);

		// Token: 0x04001E7B RID: 7803
		public static readonly PropertyAggregationStrategy AttributedBirthdaysProperty = new PersonPropertyAggregationStrategy.AttributedDateTimePropertyAggregation(ContactSchema.Birthday);

		// Token: 0x04001E7C RID: 7804
		public static readonly PropertyAggregationStrategy AttributedWeddingAnniversariesLocalProperty = new PersonPropertyAggregationStrategy.AttributedDateTimePropertyAggregation(ContactSchema.WeddingAnniversaryLocal);

		// Token: 0x04001E7D RID: 7805
		public static readonly PropertyAggregationStrategy AttributedBirthdaysLocalProperty = new PersonPropertyAggregationStrategy.AttributedDateTimePropertyAggregation(ContactSchema.BirthdayLocal);

		// Token: 0x04001E7E RID: 7806
		public static readonly PropertyAggregationStrategy AttributedLocationsProperty = new PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation(ContactSchema.Location);

		// Token: 0x02000531 RID: 1329
		private sealed class IsFavoriteAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060038F0 RID: 14576 RVA: 0x000E9B00 File Offset: 0x000E7D00
			public IsFavoriteAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.IsFavorite
			})
			{
			}

			// Token: 0x060038F1 RID: 14577 RVA: 0x000E9B24 File Offset: 0x000E7D24
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				bool flag = false;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					bool valueOrDefault = storePropertyBag.GetValueOrDefault<bool>(InternalSchema.IsFavorite, false);
					if (valueOrDefault)
					{
						flag = true;
						break;
					}
				}
				value = flag;
				return true;
			}
		}

		// Token: 0x02000532 RID: 1330
		private sealed class RelevanceScoreAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060038F2 RID: 14578 RVA: 0x000E9B8C File Offset: 0x000E7D8C
			public RelevanceScoreAggregation() : base(new StorePropertyDefinition[]
			{
				InternalSchema.RelevanceScore
			})
			{
			}

			// Token: 0x060038F3 RID: 14579 RVA: 0x000E9BB0 File Offset: 0x000E7DB0
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				int num = int.MaxValue;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					int valueOrDefault = storePropertyBag.GetValueOrDefault<int>(InternalSchema.RelevanceScore, int.MaxValue);
					if (valueOrDefault < num)
					{
						num = valueOrDefault;
					}
				}
				value = num;
				return true;
			}
		}

		// Token: 0x02000533 RID: 1331
		private sealed class PersonIdAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060038F4 RID: 14580 RVA: 0x000E9C1C File Offset: 0x000E7E1C
			public PersonIdAggregation() : base(new StorePropertyDefinition[]
			{
				ContactSchema.PersonId
			})
			{
			}

			// Token: 0x060038F5 RID: 14581 RVA: 0x000E9C40 File Offset: 0x000E7E40
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				PersonId personId = null;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					PersonId valueOrDefault = storePropertyBag.GetValueOrDefault<PersonId>(ContactSchema.PersonId, null);
					if (personId != null && valueOrDefault != null && !valueOrDefault.Equals(personId))
					{
						throw new ArgumentException("sources", "Property bag collection should have same personId");
					}
					if (personId == null)
					{
						personId = valueOrDefault;
					}
				}
				value = personId;
				return true;
			}
		}

		// Token: 0x02000534 RID: 1332
		private sealed class EmailAddressAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060038F6 RID: 14582 RVA: 0x000E9CC0 File Offset: 0x000E7EC0
			public EmailAddressAggregation() : base(PropertyDefinitionCollection.Merge<StorePropertyDefinition>(ContactEmailAddressesEnumerator.Properties, new StorePropertyDefinition[]
			{
				ContactSchema.PartnerNetworkId,
				ContactSchema.RelevanceScore,
				ContactBaseSchema.DisplayNameFirstLast,
				StoreObjectSchema.ItemClass,
				StoreObjectSchema.EntryId,
				StoreObjectSchema.ChangeKey,
				ContactSchema.SmtpAddressCache
			}))
			{
			}

			// Token: 0x060038F7 RID: 14583 RVA: 0x000E9D20 File Offset: 0x000E7F20
			internal static int GetAdjustedRelevanceScore(IStorePropertyBag source, string partnerNetworkId)
			{
				int result;
				if (string.Equals(partnerNetworkId, WellKnownNetworkNames.GAL))
				{
					result = 2147483645;
				}
				else
				{
					result = source.GetValueOrDefault<int>(ContactSchema.RelevanceScore, int.MaxValue);
				}
				return result;
			}

			// Token: 0x060038F8 RID: 14584 RVA: 0x000E9D54 File Offset: 0x000E7F54
			internal static bool HasSMTPAddressCacheMatch(string[] smtpAddressCache, string stmpAddress)
			{
				string y = "SMTP:" + stmpAddress;
				foreach (string x in smtpAddressCache)
				{
					if (StringComparer.OrdinalIgnoreCase.Equals(x, y))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060038F9 RID: 14585 RVA: 0x000E9D9C File Offset: 0x000E7F9C
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				value = null;
				int num = int.MaxValue;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					if (context.Sources.Count == 1)
					{
						string valueOrDefault = storePropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
						if (ObjectClass.IsDistributionList(valueOrDefault))
						{
							Participant valueOrDefault2 = storePropertyBag.GetValueOrDefault<Participant>(InternalSchema.DistributionListParticipant, null);
							if (valueOrDefault2 != null)
							{
								value = valueOrDefault2;
								break;
							}
						}
					}
					string valueOrDefault3 = storePropertyBag.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
					string[] valueOrDefault4 = storePropertyBag.GetValueOrDefault<string[]>(ContactSchema.SmtpAddressCache, Array<string>.Empty);
					int adjustedRelevanceScore = PersonPropertyAggregationStrategy.EmailAddressAggregation.GetAdjustedRelevanceScore(storePropertyBag, valueOrDefault3);
					PersonPropertyAggregationContext personPropertyAggregationContext = (PersonPropertyAggregationContext)context;
					ContactEmailAddressesEnumerator contactEmailAddressesEnumerator = new ContactEmailAddressesEnumerator(storePropertyBag, personPropertyAggregationContext.ClientInfoString);
					foreach (Tuple<EmailAddress, EmailAddressIndex> tuple in contactEmailAddressesEnumerator)
					{
						EmailAddress item = tuple.Item1;
						EmailAddressIndex item2 = tuple.Item2;
						if (!string.IsNullOrEmpty(item.Address))
						{
							string text = null;
							if (string.Equals(item.RoutingType, "SMTP", StringComparison.OrdinalIgnoreCase))
							{
								if (SmtpAddress.IsValidSmtpAddress(item.Address))
								{
									text = item.Address;
								}
							}
							else if (string.Equals(item.RoutingType, "EX", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(item.OriginalDisplayName) && SmtpAddress.IsValidSmtpAddress(item.OriginalDisplayName))
							{
								text = item.OriginalDisplayName;
							}
							else if (!string.IsNullOrEmpty(item.RoutingType))
							{
								text = item.Address;
							}
							if (!string.IsNullOrEmpty(text) && (num > adjustedRelevanceScore || value == null))
							{
								ParticipantOrigin origin;
								if (valueOrDefault3.Equals(WellKnownNetworkNames.GAL) || PersonPropertyAggregationStrategy.EmailAddressAggregation.HasSMTPAddressCacheMatch(valueOrDefault4, text))
								{
									origin = new DirectoryParticipantOrigin(storePropertyBag);
								}
								else
								{
									origin = new StoreParticipantOrigin(storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null), item2);
								}
								value = new Participant(item.Name, item.Address, item.RoutingType, item.OriginalDisplayName, origin, new KeyValuePair<PropertyDefinition, object>[0]);
								num = adjustedRelevanceScore;
							}
						}
					}
				}
				return value != null;
			}

			// Token: 0x04001E85 RID: 7813
			private const int DefaultGalContactRelevanceScore = 2147483645;
		}

		// Token: 0x02000535 RID: 1333
		private sealed class EmailAddressesAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060038FA RID: 14586 RVA: 0x000EA000 File Offset: 0x000E8200
			public EmailAddressesAggregation() : base(PropertyDefinitionCollection.Merge<StorePropertyDefinition>(ContactEmailAddressesEnumerator.Properties, new StorePropertyDefinition[]
			{
				ContactSchema.PartnerNetworkId,
				ContactSchema.RelevanceScore,
				ContactBaseSchema.DisplayNameFirstLast,
				StoreObjectSchema.ItemClass,
				StoreObjectSchema.EntryId,
				StoreObjectSchema.ChangeKey,
				ContactSchema.SmtpAddressCache
			}))
			{
			}

			// Token: 0x060038FB RID: 14587 RVA: 0x000EA08C File Offset: 0x000E828C
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				Dictionary<string, Pair<Participant, int>> emailAddressDictionary = new Dictionary<string, Pair<Participant, int>>(context.Sources.Count * EmailAddressProperties.PropertySets.Length, StringComparer.OrdinalIgnoreCase);
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					if (context.Sources.Count == 1)
					{
						string valueOrDefault = storePropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
						if (ObjectClass.IsDistributionList(valueOrDefault))
						{
							Participant valueOrDefault2 = storePropertyBag.GetValueOrDefault<Participant>(InternalSchema.DistributionListParticipant, null);
							if (valueOrDefault2 != null)
							{
								value = new Participant[]
								{
									valueOrDefault2
								};
								return true;
							}
							value = null;
							return false;
						}
					}
					string[] valueOrDefault3 = storePropertyBag.GetValueOrDefault<string[]>(ContactSchema.SmtpAddressCache, Array<string>.Empty);
					string valueOrDefault4 = storePropertyBag.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
					PersonPropertyAggregationContext personPropertyAggregationContext = (PersonPropertyAggregationContext)context;
					ContactEmailAddressesEnumerator contactEmailAddressesEnumerator = new ContactEmailAddressesEnumerator(storePropertyBag, personPropertyAggregationContext.ClientInfoString);
					foreach (Tuple<EmailAddress, EmailAddressIndex> tuple in contactEmailAddressesEnumerator)
					{
						EmailAddress item = tuple.Item1;
						EmailAddressIndex item2 = tuple.Item2;
						if (PersonPropertyAggregationStrategy.IsValidEmailAddress(item))
						{
							ParticipantOrigin origin;
							if (valueOrDefault4.Equals(WellKnownNetworkNames.GAL) || PersonPropertyAggregationStrategy.EmailAddressAggregation.HasSMTPAddressCacheMatch(valueOrDefault3, item.Address))
							{
								origin = new DirectoryParticipantOrigin(storePropertyBag);
							}
							else
							{
								origin = new StoreParticipantOrigin(storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null), item2);
							}
							Participant participant = new Participant(item.Name, item.Address, item.RoutingType, item.OriginalDisplayName, origin, new KeyValuePair<PropertyDefinition, object>[0]);
							int adjustedRelevanceScore = PersonPropertyAggregationStrategy.EmailAddressAggregation.GetAdjustedRelevanceScore(storePropertyBag, valueOrDefault4);
							string text;
							if (string.Equals(item.RoutingType, string.Empty, StringComparison.OrdinalIgnoreCase) || (string.Equals(item.RoutingType, "EX", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(participant.OriginalDisplayName)))
							{
								text = participant.OriginalDisplayName;
							}
							else
							{
								text = participant.EmailAddress;
							}
							if (!string.IsNullOrEmpty(text))
							{
								if (emailAddressDictionary.ContainsKey(text))
								{
									if (adjustedRelevanceScore < emailAddressDictionary[text].Second)
									{
										emailAddressDictionary[text] = new Pair<Participant, int>(participant, adjustedRelevanceScore);
									}
								}
								else
								{
									emailAddressDictionary.Add(text, new Pair<Participant, int>(participant, adjustedRelevanceScore));
								}
							}
						}
					}
				}
				IEnumerable<Participant> source = from k in emailAddressDictionary.Keys
				orderby emailAddressDictionary[k].Second
				select emailAddressDictionary[k].First;
				if (source.Count<Participant>() > 0)
				{
					value = source.ToArray<Participant>();
					return true;
				}
				value = null;
				return false;
			}
		}

		// Token: 0x02000536 RID: 1334
		private sealed class IMAddressAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060038FC RID: 14588 RVA: 0x000EA388 File Offset: 0x000E8588
			public IMAddressAggregation() : base(PropertyDefinitionCollection.Merge<StorePropertyDefinition>(PersonPropertyAggregationStrategy.IMAddressAggregation.IMProperties, new StorePropertyDefinition[]
			{
				ContactSchema.PartnerNetworkId
			}))
			{
			}

			// Token: 0x060038FD RID: 14589 RVA: 0x000EA3B8 File Offset: 0x000E85B8
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				value = null;
				string text = string.Empty;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					string valueOrDefault = storePropertyBag.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
					bool flag = StringComparer.OrdinalIgnoreCase.Equals(valueOrDefault, WellKnownNetworkNames.GAL);
					bool flag2 = StringComparer.OrdinalIgnoreCase.Equals(valueOrDefault, WellKnownNetworkNames.RecipientCache);
					if (flag || flag2 || value == null)
					{
						StorePropertyDefinition[] improperties = PersonPropertyAggregationStrategy.IMAddressAggregation.IMProperties;
						int i = 0;
						while (i < improperties.Length)
						{
							PropertyDefinition propertyDefinition = improperties[i];
							string valueOrDefault2 = storePropertyBag.GetValueOrDefault<string>(propertyDefinition, string.Empty);
							if (!string.IsNullOrEmpty(valueOrDefault2))
							{
								value = valueOrDefault2;
								if (flag)
								{
									return true;
								}
								if (flag2)
								{
									text = valueOrDefault2;
									break;
								}
								break;
							}
							else
							{
								i++;
							}
						}
					}
				}
				if (!text.Equals(string.Empty))
				{
					value = text;
				}
				return value != null;
			}

			// Token: 0x04001E86 RID: 7814
			private static readonly StorePropertyDefinition[] IMProperties = new StorePropertyDefinition[]
			{
				ContactSchema.IMAddress,
				ContactSchema.IMAddress2,
				ContactSchema.IMAddress3
			};
		}

		// Token: 0x02000537 RID: 1335
		private sealed class GALLinkIDAggregation : PropertyAggregationStrategy
		{
			// Token: 0x060038FF RID: 14591 RVA: 0x000EA4EE File Offset: 0x000E86EE
			public GALLinkIDAggregation() : base(PersonPropertyAggregationStrategy.GALLinkIDAggregation.GALLinkIDProperties)
			{
			}

			// Token: 0x06003900 RID: 14592 RVA: 0x000EA4FC File Offset: 0x000E86FC
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				value = null;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					string valueOrDefault = storePropertyBag.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
					bool flag = StringComparer.OrdinalIgnoreCase.Equals(valueOrDefault, WellKnownNetworkNames.GAL);
					Guid? valueOrDefault2 = storePropertyBag.GetValueOrDefault<Guid?>(ContactSchema.GALLinkID, null);
					if (valueOrDefault2 != null)
					{
						value = valueOrDefault2;
						if (flag)
						{
							return true;
						}
					}
				}
				return value != null;
			}

			// Token: 0x04001E87 RID: 7815
			private static readonly StorePropertyDefinition[] GALLinkIDProperties = new StorePropertyDefinition[]
			{
				ContactSchema.GALLinkID,
				ContactSchema.PartnerNetworkId
			};
		}

		// Token: 0x02000538 RID: 1336
		private sealed class PostalAddressesAggregation : PropertyAggregationStrategy
		{
			// Token: 0x06003902 RID: 14594 RVA: 0x000EA5D2 File Offset: 0x000E87D2
			public static PostalAddress GetFromAllProperties(PostalAddressProperties properties, IStorePropertyBag propertyBag)
			{
				return properties.GetFromAllPropertiesForConversationView(propertyBag);
			}

			// Token: 0x06003903 RID: 14595 RVA: 0x000EA5DB File Offset: 0x000E87DB
			public static PostalAddress GetFromAllPropertiesForConversationView(PostalAddressProperties properties, IStorePropertyBag propertyBag)
			{
				return properties.GetFromAllPropertiesForConversationView(propertyBag);
			}

			// Token: 0x06003904 RID: 14596 RVA: 0x000EA5E4 File Offset: 0x000E87E4
			public PostalAddressesAggregation(NativeStorePropertyDefinition[] properties, Func<PostalAddressProperties, IStorePropertyBag, PostalAddress> postalAddressFactory) : base(properties)
			{
				this.postalAddressFactory = postalAddressFactory;
			}

			// Token: 0x06003905 RID: 14597 RVA: 0x000EA5F4 File Offset: 0x000E87F4
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				List<PostalAddress> list = new List<PostalAddress>(context.Sources.Count * PostalAddressProperties.PropertySets.Length);
				foreach (IStorePropertyBag arg in context.Sources)
				{
					foreach (PostalAddressProperties arg2 in PostalAddressProperties.PropertySets)
					{
						PostalAddress postalAddress = this.postalAddressFactory(arg2, arg);
						if (postalAddress != null)
						{
							list.Add(postalAddress);
						}
					}
				}
				return PersonPropertyAggregationStrategy.TryGetArrayResult<PostalAddress>(list, out value);
			}

			// Token: 0x04001E88 RID: 7816
			private Func<PostalAddressProperties, IStorePropertyBag, PostalAddress> postalAddressFactory;
		}

		// Token: 0x02000539 RID: 1337
		private sealed class IsPersonalContactAggregation : PropertyAggregationStrategy
		{
			// Token: 0x06003906 RID: 14598 RVA: 0x000EA698 File Offset: 0x000E8898
			public IsPersonalContactAggregation() : base(new StorePropertyDefinition[0])
			{
			}

			// Token: 0x06003907 RID: 14599 RVA: 0x000EA6A6 File Offset: 0x000E88A6
			protected sealed override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				value = true;
				return true;
			}
		}

		// Token: 0x0200053A RID: 1338
		private sealed class PostalAddressAggregation : PropertyAggregationStrategy
		{
			// Token: 0x06003908 RID: 14600 RVA: 0x000EA6B1 File Offset: 0x000E88B1
			public PostalAddressAggregation() : base(PostalAddressProperties.AllProperties)
			{
			}

			// Token: 0x06003909 RID: 14601 RVA: 0x000EA6C0 File Offset: 0x000E88C0
			protected sealed override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				foreach (IStorePropertyBag propertyBag in context.Sources)
				{
					PostalAddress fromAllProperties = PostalAddressProperties.HomeAddress.GetFromAllProperties(propertyBag);
					if (fromAllProperties == null)
					{
						fromAllProperties = PostalAddressProperties.WorkAddress.GetFromAllProperties(propertyBag);
						if (fromAllProperties == null)
						{
							fromAllProperties = PostalAddressProperties.OtherAddress.GetFromAllProperties(propertyBag);
						}
					}
					if (fromAllProperties != null)
					{
						value = fromAllProperties.ToString();
						return true;
					}
				}
				value = null;
				return false;
			}
		}

		// Token: 0x0200053B RID: 1339
		private sealed class PersonTypeAggregation : PropertyAggregationStrategy
		{
			// Token: 0x0600390A RID: 14602 RVA: 0x000EA748 File Offset: 0x000E8948
			public PersonTypeAggregation() : base(new StorePropertyDefinition[]
			{
				ContactSchema.PersonType
			})
			{
			}

			// Token: 0x0600390B RID: 14603 RVA: 0x000EA76C File Offset: 0x000E896C
			protected sealed override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				value = PersonType.Person;
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					object obj = storePropertyBag.TryGetProperty(ContactSchema.PersonType);
					if (obj is PersonType)
					{
						value = (PersonType)obj;
						break;
					}
				}
				return true;
			}
		}

		// Token: 0x0200053C RID: 1340
		private sealed class FolderIdsAggregation : PropertyAggregationStrategy
		{
			// Token: 0x0600390C RID: 14604 RVA: 0x000EA7E0 File Offset: 0x000E89E0
			public FolderIdsAggregation() : base(new StorePropertyDefinition[]
			{
				StoreObjectSchema.ParentItemId,
				ContactSchema.IsFavorite
			})
			{
			}

			// Token: 0x0600390D RID: 14605 RVA: 0x000EA80C File Offset: 0x000E8A0C
			protected sealed override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				List<StoreObjectId> list = new List<StoreObjectId>();
				foreach (IStorePropertyBag storePropertyBag in context.Sources)
				{
					StoreObjectId valueOrDefault = storePropertyBag.GetValueOrDefault<StoreObjectId>(StoreObjectSchema.ParentItemId, null);
					if (valueOrDefault != null && !list.Contains(valueOrDefault))
					{
						list.Add(valueOrDefault);
						this.CalculateFolderIdBasedOnMyContactsFolderInfo(context as PersonPropertyAggregationContext, list, valueOrDefault);
						this.CalculateFolderIdBasedOnFavoriteInfo(context as PersonPropertyAggregationContext, storePropertyBag, list, valueOrDefault);
					}
				}
				return PersonPropertyAggregationStrategy.TryGetArrayResult<StoreObjectId>(list, out value);
			}

			// Token: 0x0600390E RID: 14606 RVA: 0x000EA89C File Offset: 0x000E8A9C
			private void CalculateFolderIdBasedOnMyContactsFolderInfo(PersonPropertyAggregationContext context, List<StoreObjectId> folderIdList, StoreObjectId parentId)
			{
				if (context != null && context.ContactFolders != null && context.ContactFolders.MyContactFolders.Contains(parentId) && !folderIdList.Contains(context.ContactFolders.MyContactsSearchFolderId))
				{
					folderIdList.Add(context.ContactFolders.MyContactsSearchFolderId);
				}
			}

			// Token: 0x0600390F RID: 14607 RVA: 0x000EA8EC File Offset: 0x000E8AEC
			private void CalculateFolderIdBasedOnFavoriteInfo(PersonPropertyAggregationContext context, IStorePropertyBag propertyBag, List<StoreObjectId> folderIdList, StoreObjectId parentId)
			{
				if (context != null && context.ContactFolders != null && context.ContactFolders.QuickContactsFolderId != null && context.ContactFolders.FavoritesFolderId != null)
				{
					bool valueOrDefault = propertyBag.GetValueOrDefault<bool>(ContactSchema.IsFavorite, false);
					if (context.ContactFolders.QuickContactsFolderId.Equals(parentId) && valueOrDefault && !folderIdList.Contains(context.ContactFolders.FavoritesFolderId))
					{
						folderIdList.Add(context.ContactFolders.FavoritesFolderId);
					}
				}
			}
		}

		// Token: 0x0200053D RID: 1341
		private sealed class AttributionsAggregation : PropertyAggregationStrategy
		{
			// Token: 0x06003910 RID: 14608 RVA: 0x000EA968 File Offset: 0x000E8B68
			public AttributionsAggregation() : base(new StorePropertyDefinition[]
			{
				ItemSchema.Id,
				ContactSchema.PartnerNetworkId,
				StoreObjectSchema.ParentItemId,
				ItemSchema.ParentDisplayName,
				ContactSchema.GALLinkID
			})
			{
			}

			// Token: 0x06003911 RID: 14609 RVA: 0x000EA9AC File Offset: 0x000E8BAC
			protected sealed override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				List<Attribution> list = new List<Attribution>(4);
				PersonPropertyAggregationContext personPropertyAggregationContext = (PersonPropertyAggregationContext)context;
				int i = 0;
				while (i < context.Sources.Count)
				{
					Attribution attribution = new Attribution();
					IStorePropertyBag storePropertyBag = context.Sources[i];
					string valueOrDefault = storePropertyBag.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, null);
					attribution.Id = i.ToString();
					if (valueOrDefault != null && valueOrDefault.Equals(WellKnownNetworkNames.GAL, StringComparison.OrdinalIgnoreCase))
					{
						Guid valueOrDefault2 = storePropertyBag.GetValueOrDefault<Guid>(ContactSchema.GALLinkID, Guid.Empty);
						if (!(valueOrDefault2 == Guid.Empty))
						{
							attribution.SourceId = AttributionSourceId.CreateFrom(valueOrDefault2);
							goto IL_C9;
						}
						PersonPropertyAggregationStrategy.Tracer.TraceError(0L, "Invalid GAL Contact since it doesn't have GALLinkID set, skip aggregating it.");
					}
					else
					{
						VersionedId valueOrDefault3 = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
						if (valueOrDefault3 == null)
						{
							throw new CorruptDataException(ServerStrings.ExContactHasNoId);
						}
						attribution.SourceId = AttributionSourceId.CreateFrom(valueOrDefault3);
						goto IL_C9;
					}
					IL_17B:
					i++;
					continue;
					IL_C9:
					attribution.IsHidden = false;
					attribution.DisplayName = storePropertyBag.GetValueOrDefault<string>(ContactSchema.AttributionDisplayName, WellKnownNetworkNames.Outlook);
					attribution.IsWritable = storePropertyBag.GetValueOrDefault<bool>(ContactSchema.IsWritable, true);
					StoreObjectId valueOrDefault4 = storePropertyBag.GetValueOrDefault<StoreObjectId>(StoreObjectSchema.ParentItemId, null);
					attribution.IsQuickContact = (valueOrDefault4 != null && personPropertyAggregationContext.ContactFolders != null && personPropertyAggregationContext.ContactFolders.QuickContactsFolderId != null && valueOrDefault4.Equals(personPropertyAggregationContext.ContactFolders.QuickContactsFolderId));
					if (attribution.DisplayName.Equals(WellKnownNetworkNames.Outlook, StringComparison.OrdinalIgnoreCase))
					{
						attribution.FolderId = valueOrDefault4;
						attribution.FolderName = storePropertyBag.GetValueOrDefault<string>(ItemSchema.ParentDisplayName, null);
					}
					else
					{
						attribution.FolderId = null;
					}
					list.Add(attribution);
					goto IL_17B;
				}
				return PersonPropertyAggregationStrategy.TryGetArrayResult<Attribution>(list, out value);
			}
		}

		// Token: 0x0200053E RID: 1342
		private abstract class AttributedPropertyAggregation<T> : PropertyAggregationStrategy
		{
			// Token: 0x06003912 RID: 14610 RVA: 0x000EAB50 File Offset: 0x000E8D50
			protected AttributedPropertyAggregation(params StorePropertyDefinition[] propertyDefinitions) : base(propertyDefinitions)
			{
			}

			// Token: 0x06003913 RID: 14611 RVA: 0x000EAB5C File Offset: 0x000E8D5C
			protected sealed override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				List<AttributedValue<T>> list = new List<AttributedValue<T>>(context.Sources.Count);
				IList<IStorePropertyBag> list2 = this.SortSources(context.Sources);
				for (int i = 0; i < list2.Count; i++)
				{
					T value2;
					if (this.TryGetSingleValue(list2[i], out value2))
					{
						AttributedValue<T> attributedValue = new AttributedValue<T>(value2, new string[]
						{
							i.ToString(CultureInfo.InvariantCulture)
						});
						AttributedValue<T>.AddToList(list, attributedValue);
					}
				}
				return PersonPropertyAggregationStrategy.TryGetArrayResult<AttributedValue<T>>(list, out value);
			}

			// Token: 0x06003914 RID: 14612
			protected abstract bool TryGetSingleValue(IStorePropertyBag source, out T singleValue);

			// Token: 0x06003915 RID: 14613 RVA: 0x000EABDA File Offset: 0x000E8DDA
			protected virtual IList<IStorePropertyBag> SortSources(IList<IStorePropertyBag> sources)
			{
				return sources;
			}
		}

		// Token: 0x0200053F RID: 1343
		private abstract class AttributedSinglePropertyAggregation<T> : PersonPropertyAggregationStrategy.AttributedPropertyAggregation<T>
		{
			// Token: 0x06003916 RID: 14614 RVA: 0x000EABE0 File Offset: 0x000E8DE0
			protected AttributedSinglePropertyAggregation(StorePropertyDefinition property, StorePropertyDefinition[] additionalDependencies) : base(PropertyDefinitionCollection.Merge<StorePropertyDefinition>(additionalDependencies, new StorePropertyDefinition[]
			{
				property
			}))
			{
				this.property = property;
			}

			// Token: 0x06003917 RID: 14615 RVA: 0x000EAC0C File Offset: 0x000E8E0C
			protected AttributedSinglePropertyAggregation(StorePropertyDefinition property) : this(property, Array<StorePropertyDefinition>.Empty)
			{
			}

			// Token: 0x170011C7 RID: 4551
			// (get) Token: 0x06003918 RID: 14616 RVA: 0x000EAC1A File Offset: 0x000E8E1A
			public StorePropertyDefinition Property
			{
				get
				{
					return this.property;
				}
			}

			// Token: 0x04001E89 RID: 7817
			private readonly StorePropertyDefinition property;
		}

		// Token: 0x02000540 RID: 1344
		private class AttributedStringPropertyAggregation : PersonPropertyAggregationStrategy.AttributedSinglePropertyAggregation<string>
		{
			// Token: 0x06003919 RID: 14617 RVA: 0x000EAC24 File Offset: 0x000E8E24
			public AttributedStringPropertyAggregation(StorePropertyDefinition property, StorePropertyDefinition[] additionalDependencies) : base(property, PropertyDefinitionCollection.Merge<StorePropertyDefinition>(additionalDependencies, new StorePropertyDefinition[]
			{
				property
			}))
			{
			}

			// Token: 0x0600391A RID: 14618 RVA: 0x000EAC4A File Offset: 0x000E8E4A
			public AttributedStringPropertyAggregation(StorePropertyDefinition property) : this(property, Array<StorePropertyDefinition>.Empty)
			{
			}

			// Token: 0x0600391B RID: 14619 RVA: 0x000EAC58 File Offset: 0x000E8E58
			protected override bool TryGetSingleValue(IStorePropertyBag source, out string singleValue)
			{
				string text = source.TryGetProperty(base.Property) as string;
				if (!string.IsNullOrWhiteSpace(text))
				{
					singleValue = text;
					return true;
				}
				singleValue = null;
				return false;
			}
		}

		// Token: 0x02000541 RID: 1345
		private sealed class AttributedStringArrayPropertyAggregation : PersonPropertyAggregationStrategy.AttributedSinglePropertyAggregation<string[]>
		{
			// Token: 0x0600391C RID: 14620 RVA: 0x000EAC88 File Offset: 0x000E8E88
			public AttributedStringArrayPropertyAggregation(StorePropertyDefinition property) : base(property)
			{
			}

			// Token: 0x0600391D RID: 14621 RVA: 0x000EAC94 File Offset: 0x000E8E94
			protected override bool TryGetSingleValue(IStorePropertyBag source, out string[] singleValue)
			{
				string[] valueOrDefault = source.GetValueOrDefault<string[]>(base.Property, null);
				if (valueOrDefault != null && valueOrDefault.Length > 0)
				{
					singleValue = valueOrDefault;
					return true;
				}
				singleValue = null;
				return false;
			}
		}

		// Token: 0x02000542 RID: 1346
		private sealed class AttributedAsStringPropertyAggregation<T> : PersonPropertyAggregationStrategy.AttributedSinglePropertyAggregation<string>
		{
			// Token: 0x0600391E RID: 14622 RVA: 0x000EACC1 File Offset: 0x000E8EC1
			public AttributedAsStringPropertyAggregation(StorePropertyDefinition property) : base(property)
			{
			}

			// Token: 0x0600391F RID: 14623 RVA: 0x000EACCC File Offset: 0x000E8ECC
			protected override bool TryGetSingleValue(IStorePropertyBag source, out string singleValue)
			{
				object obj = source.TryGetProperty(base.Property);
				if (obj != null && !(obj is PropertyError))
				{
					T t = (T)((object)obj);
					if (t != null)
					{
						singleValue = t.ToString();
						return true;
					}
				}
				singleValue = null;
				return false;
			}
		}

		// Token: 0x02000543 RID: 1347
		private sealed class AttributedDateTimePropertyAggregation : PersonPropertyAggregationStrategy.AttributedSinglePropertyAggregation<ExDateTime>
		{
			// Token: 0x06003920 RID: 14624 RVA: 0x000EAD15 File Offset: 0x000E8F15
			public AttributedDateTimePropertyAggregation(StorePropertyDefinition property) : base(property)
			{
			}

			// Token: 0x06003921 RID: 14625 RVA: 0x000EAD20 File Offset: 0x000E8F20
			protected override bool TryGetSingleValue(IStorePropertyBag source, out ExDateTime singleValue)
			{
				ExDateTime valueOrDefault = source.GetValueOrDefault<ExDateTime>(base.Property, PersonPropertyAggregationStrategy.AttributedDateTimePropertyAggregation.nullTime);
				if (!valueOrDefault.Equals(PersonPropertyAggregationStrategy.AttributedDateTimePropertyAggregation.nullTime))
				{
					singleValue = valueOrDefault;
					return true;
				}
				singleValue = default(ExDateTime);
				return false;
			}

			// Token: 0x04001E8A RID: 7818
			private static readonly ExDateTime nullTime = default(ExDateTime);
		}

		// Token: 0x02000544 RID: 1348
		private sealed class AttributedPhoneNumberPropertyAggregation : PersonPropertyAggregationStrategy.AttributedSinglePropertyAggregation<PhoneNumber>
		{
			// Token: 0x06003923 RID: 14627 RVA: 0x000EAD6B File Offset: 0x000E8F6B
			public AttributedPhoneNumberPropertyAggregation(StorePropertyDefinition property, PersonPhoneNumberType type) : base(property)
			{
				this.type = type;
			}

			// Token: 0x06003924 RID: 14628 RVA: 0x000EAD7C File Offset: 0x000E8F7C
			protected override bool TryGetSingleValue(IStorePropertyBag source, out PhoneNumber singleValue)
			{
				string text = source.TryGetProperty(base.Property) as string;
				if (!string.IsNullOrWhiteSpace(text))
				{
					singleValue = new PhoneNumber
					{
						Number = text,
						Type = this.type
					};
					return true;
				}
				singleValue = null;
				return false;
			}

			// Token: 0x04001E8B RID: 7819
			private readonly PersonPhoneNumberType type;
		}

		// Token: 0x02000545 RID: 1349
		private abstract class AttributedProtectedPropertyAggregation<T> : PropertyAggregationStrategy
		{
			// Token: 0x06003925 RID: 14629 RVA: 0x000EADC8 File Offset: 0x000E8FC8
			public AttributedProtectedPropertyAggregation(params StorePropertyDefinition[] properties) : base(PropertyDefinitionCollection.Merge<StorePropertyDefinition>(properties, new StorePropertyDefinition[]
			{
				ContactSchema.PartnerNetworkId
			}))
			{
			}

			// Token: 0x06003926 RID: 14630 RVA: 0x000EADF4 File Offset: 0x000E8FF4
			protected sealed override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				PersonPropertyAggregationContext personPropertyAggregationContext = (PersonPropertyAggregationContext)context;
				List<AttributedValue<T>> list = new List<AttributedValue<T>>(context.Sources.Count);
				for (int i = 0; i < context.Sources.Count; i++)
				{
					IStorePropertyBag storePropertyBag = context.Sources[i];
					string valueOrDefault = storePropertyBag.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
					T value2;
					bool flag;
					if (StringComparer.OrdinalIgnoreCase.Equals(valueOrDefault, WellKnownNetworkNames.Facebook))
					{
						if (ClientInfo.OWA.IsMatch(personPropertyAggregationContext.ClientInfoString))
						{
							flag = this.TryGetProtectedValue(storePropertyBag, out value2);
						}
						else
						{
							flag = false;
							value2 = default(T);
						}
					}
					else
					{
						flag = this.TryGetRegularValue(storePropertyBag, out value2);
					}
					if (flag)
					{
						AttributedValue<T> attributedValue = new AttributedValue<T>(value2, new string[]
						{
							i.ToString()
						});
						AttributedValue<T>.AddToList(list, attributedValue);
					}
				}
				return PersonPropertyAggregationStrategy.TryGetArrayResult<AttributedValue<T>>(list, out value);
			}

			// Token: 0x06003927 RID: 14631
			protected abstract bool TryGetRegularValue(IStorePropertyBag source, out T value);

			// Token: 0x06003928 RID: 14632
			protected abstract bool TryGetProtectedValue(IStorePropertyBag source, out T value);
		}

		// Token: 0x02000546 RID: 1350
		private sealed class AttributedEmailAddressPropertyPlusProtectedPropertyAggregation : PersonPropertyAggregationStrategy.AttributedProtectedPropertyAggregation<Participant>
		{
			// Token: 0x06003929 RID: 14633 RVA: 0x000EAED0 File Offset: 0x000E90D0
			public AttributedEmailAddressPropertyPlusProtectedPropertyAggregation(EmailAddressProperties properties, StorePropertyDefinition protectedProperty) : base(PropertyDefinitionCollection.Merge<StorePropertyDefinition>(properties.Properties, new StorePropertyDefinition[]
			{
				protectedProperty
			}))
			{
				this.properties = properties;
				this.protectedProperty = protectedProperty;
			}

			// Token: 0x0600392A RID: 14634 RVA: 0x000EAF08 File Offset: 0x000E9108
			protected override bool TryGetRegularValue(IStorePropertyBag source, out Participant singleValue)
			{
				EmailAddress from = this.properties.GetFrom(source);
				singleValue = PersonPropertyAggregationStrategy.AttributedEmailAddressPropertyAggregation.GetParticipant(source, from);
				return singleValue != null;
			}

			// Token: 0x0600392B RID: 14635 RVA: 0x000EAF38 File Offset: 0x000E9138
			protected override bool TryGetProtectedValue(IStorePropertyBag source, out Participant singleValue)
			{
				string text = source.TryGetProperty(this.protectedProperty) as string;
				if (!string.IsNullOrWhiteSpace(text))
				{
					EmailAddress emailAddress = new EmailAddress
					{
						RoutingType = "smtp",
						Address = text,
						Name = text
					};
					singleValue = PersonPropertyAggregationStrategy.AttributedEmailAddressPropertyAggregation.GetParticipant(source, emailAddress);
					return singleValue != null;
				}
				singleValue = null;
				return false;
			}

			// Token: 0x04001E8C RID: 7820
			private readonly EmailAddressProperties properties;

			// Token: 0x04001E8D RID: 7821
			private readonly PropertyDefinition protectedProperty;
		}

		// Token: 0x02000547 RID: 1351
		private sealed class AttributedEmailAddressPropertyAggregation : PersonPropertyAggregationStrategy.AttributedPropertyAggregation<Participant>
		{
			// Token: 0x0600392C RID: 14636 RVA: 0x000EAF9F File Offset: 0x000E919F
			public AttributedEmailAddressPropertyAggregation(EmailAddressProperties properties) : base(properties.Properties)
			{
				this.properties = properties;
			}

			// Token: 0x0600392D RID: 14637 RVA: 0x000EAFB4 File Offset: 0x000E91B4
			internal static Participant GetParticipant(IStorePropertyBag source, EmailAddress emailAddress)
			{
				Participant result = null;
				if (PersonPropertyAggregationStrategy.IsValidEmailAddress(emailAddress))
				{
					string[] valueOrDefault = source.GetValueOrDefault<string[]>(ContactSchema.SmtpAddressCache, Array<string>.Empty);
					string valueOrDefault2 = source.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
					ParticipantOrigin origin;
					if (valueOrDefault2.Equals(WellKnownNetworkNames.GAL) || PersonPropertyAggregationStrategy.EmailAddressAggregation.HasSMTPAddressCacheMatch(valueOrDefault, emailAddress.Address))
					{
						origin = new DirectoryParticipantOrigin(source);
					}
					else
					{
						origin = new StoreParticipantOrigin(source.GetValueOrDefault<VersionedId>(ItemSchema.Id, null));
					}
					result = new Participant(emailAddress.Name, emailAddress.Address, emailAddress.RoutingType, emailAddress.OriginalDisplayName, origin, new KeyValuePair<PropertyDefinition, object>[0]);
				}
				return result;
			}

			// Token: 0x0600392E RID: 14638 RVA: 0x000EB04C File Offset: 0x000E924C
			protected override bool TryGetSingleValue(IStorePropertyBag source, out Participant singleValue)
			{
				EmailAddress from = this.properties.GetFrom(source);
				singleValue = PersonPropertyAggregationStrategy.AttributedEmailAddressPropertyAggregation.GetParticipant(source, from);
				return singleValue != null;
			}

			// Token: 0x04001E8E RID: 7822
			private readonly EmailAddressProperties properties;
		}

		// Token: 0x02000548 RID: 1352
		private sealed class AttributedPostalAddressPropertyAggregation : PersonPropertyAggregationStrategy.AttributedPropertyAggregation<PostalAddress>
		{
			// Token: 0x0600392F RID: 14639 RVA: 0x000EB07C File Offset: 0x000E927C
			public AttributedPostalAddressPropertyAggregation(PostalAddressProperties properties) : base(properties.Properties)
			{
				this.properties = properties;
			}

			// Token: 0x06003930 RID: 14640 RVA: 0x000EB094 File Offset: 0x000E9294
			protected override bool TryGetSingleValue(IStorePropertyBag source, out PostalAddress singleValue)
			{
				PostalAddress fromAllProperties = this.properties.GetFromAllProperties(source);
				if (fromAllProperties != null)
				{
					bool flag = fromAllProperties.Latitude != null && fromAllProperties.Longitude != null;
					if (!string.IsNullOrWhiteSpace(fromAllProperties.Street) || !string.IsNullOrWhiteSpace(fromAllProperties.City) || !string.IsNullOrWhiteSpace(fromAllProperties.State) || !string.IsNullOrWhiteSpace(fromAllProperties.Country) || !string.IsNullOrWhiteSpace(fromAllProperties.PostalCode) || !string.IsNullOrWhiteSpace(fromAllProperties.PostOfficeBox) || flag)
					{
						singleValue = fromAllProperties;
						return true;
					}
				}
				singleValue = null;
				return false;
			}

			// Token: 0x04001E8F RID: 7823
			private readonly PostalAddressProperties properties;
		}

		// Token: 0x02000549 RID: 1353
		private sealed class ExtendedPropertiesAggregation : PropertyAggregationStrategy
		{
			// Token: 0x06003931 RID: 14641 RVA: 0x000EB12D File Offset: 0x000E932D
			public ExtendedPropertiesAggregation(StorePropertyDefinition[] extendedProperties) : base(extendedProperties)
			{
				this.extendedProperties = extendedProperties;
			}

			// Token: 0x06003932 RID: 14642 RVA: 0x000EB140 File Offset: 0x000E9340
			protected override bool TryAggregate(PropertyAggregationContext context, out object value)
			{
				List<AttributedValue<ContactExtendedPropertyData>> list = new List<AttributedValue<ContactExtendedPropertyData>>(context.Sources.Count * this.extendedProperties.Length);
				for (int i = 0; i < context.Sources.Count; i++)
				{
					string[] attributions = new string[]
					{
						i.ToString()
					};
					IStorePropertyBag storePropertyBag = context.Sources[i];
					foreach (StorePropertyDefinition propertyDefinition in this.extendedProperties)
					{
						object obj = storePropertyBag.TryGetProperty(propertyDefinition);
						if (obj != null && !(obj is PropertyError))
						{
							AttributedValue<ContactExtendedPropertyData> attributedValue = new AttributedValue<ContactExtendedPropertyData>(new ContactExtendedPropertyData(propertyDefinition, obj), attributions);
							AttributedValue<ContactExtendedPropertyData>.AddToList(list, attributedValue);
						}
					}
				}
				return PersonPropertyAggregationStrategy.TryGetArrayResult<AttributedValue<ContactExtendedPropertyData>>(list, out value);
			}

			// Token: 0x04001E90 RID: 7824
			private StorePropertyDefinition[] extendedProperties;
		}

		// Token: 0x0200054A RID: 1354
		private sealed class AttributedPhoneNumberPropertyPlusProtectedPropertyAggregation : PersonPropertyAggregationStrategy.AttributedProtectedPropertyAggregation<PhoneNumber>
		{
			// Token: 0x06003933 RID: 14643 RVA: 0x000EB1FC File Offset: 0x000E93FC
			public AttributedPhoneNumberPropertyPlusProtectedPropertyAggregation(StorePropertyDefinition regularProperty, PersonPhoneNumberType type, StorePropertyDefinition protectedProperty) : base(new StorePropertyDefinition[]
			{
				regularProperty,
				protectedProperty,
				ContactSchema.PartnerNetworkId
			})
			{
				this.regularProperty = regularProperty;
				this.protectedProperty = protectedProperty;
				this.type = type;
			}

			// Token: 0x06003934 RID: 14644 RVA: 0x000EB23C File Offset: 0x000E943C
			protected override bool TryGetRegularValue(IStorePropertyBag source, out PhoneNumber value)
			{
				return this.TryGetPropertyValue(source, this.regularProperty, out value);
			}

			// Token: 0x06003935 RID: 14645 RVA: 0x000EB24C File Offset: 0x000E944C
			protected override bool TryGetProtectedValue(IStorePropertyBag source, out PhoneNumber value)
			{
				return this.TryGetPropertyValue(source, this.protectedProperty, out value);
			}

			// Token: 0x06003936 RID: 14646 RVA: 0x000EB25C File Offset: 0x000E945C
			private bool TryGetPropertyValue(IStorePropertyBag source, StorePropertyDefinition property, out PhoneNumber value)
			{
				string text = source.TryGetProperty(property) as string;
				if (!string.IsNullOrWhiteSpace(text))
				{
					value = new PhoneNumber
					{
						Number = text,
						Type = this.type
					};
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x04001E91 RID: 7825
			private readonly StorePropertyDefinition regularProperty;

			// Token: 0x04001E92 RID: 7826
			private readonly StorePropertyDefinition protectedProperty;

			// Token: 0x04001E93 RID: 7827
			private readonly PersonPhoneNumberType type;
		}

		// Token: 0x0200054B RID: 1355
		private sealed class AttributedThirdPartyPhotoUrlPropertyAggregation : PersonPropertyAggregationStrategy.AttributedStringPropertyAggregation
		{
			// Token: 0x06003937 RID: 14647 RVA: 0x000EB2A0 File Offset: 0x000E94A0
			public AttributedThirdPartyPhotoUrlPropertyAggregation() : base(ContactSchema.PartnerNetworkThumbnailPhotoUrl, PersonPropertyAggregationStrategy.AttributedThirdPartyPhotoUrlPropertyAggregation.DependantProperties)
			{
			}

			// Token: 0x06003938 RID: 14648 RVA: 0x000EB2EC File Offset: 0x000E94EC
			protected override IList<IStorePropertyBag> SortSources(IList<IStorePropertyBag> sources)
			{
				if (this.IsConsumerUser())
				{
					return (from source in sources
					orderby this.GetConsumerRank(source.GetValueOrDefault<string>(ContactSchema.PartnerNetworkThumbnailPhotoUrl, string.Empty), source.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty))
					select source).ToList<IStorePropertyBag>();
				}
				return (from b in sources
				orderby b.GetValueOrDefault<ExDateTime>(ContactSchema.PeopleConnectionCreationTime, ExDateTime.MaxValue)
				select b).ToList<IStorePropertyBag>();
			}

			// Token: 0x06003939 RID: 14649 RVA: 0x000EB348 File Offset: 0x000E9548
			private int GetConsumerRank(string thirdPartyUrl, string partnerNetworkId)
			{
				int result = int.MaxValue;
				if (!string.IsNullOrWhiteSpace(thirdPartyUrl))
				{
					if (StringComparer.OrdinalIgnoreCase.Equals(partnerNetworkId, WellKnownNetworkNames.GAL))
					{
						result = 0;
					}
					else if (StringComparer.OrdinalIgnoreCase.Equals(partnerNetworkId, WellKnownNetworkNames.Facebook))
					{
						result = 1;
					}
					else if (StringComparer.OrdinalIgnoreCase.Equals(partnerNetworkId, WellKnownNetworkNames.LinkedIn))
					{
						result = 2;
					}
					else
					{
						result = 3;
					}
				}
				return result;
			}

			// Token: 0x0600393A RID: 14650 RVA: 0x000EB3A8 File Offset: 0x000E95A8
			private bool IsConsumerUser()
			{
				return false;
			}

			// Token: 0x04001E94 RID: 7828
			private static readonly StorePropertyDefinition[] DependantProperties = new StorePropertyDefinition[]
			{
				ContactSchema.PeopleConnectionCreationTime,
				ContactSchema.PartnerNetworkId
			};
		}
	}
}

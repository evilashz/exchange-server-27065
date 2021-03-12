using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000247 RID: 583
	internal static class Filters
	{
		// Token: 0x06001C87 RID: 7303 RVA: 0x00076478 File Offset: 0x00074678
		private static QueryFilter CreateUserEnabledFilter(bool enabled)
		{
			QueryFilter queryFilter = new BitMaskAndFilter(ADUserSchema.UserAccountControl, 2UL);
			if (!enabled)
			{
				return queryFilter;
			}
			return new NotFilter(queryFilter);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000764A0 File Offset: 0x000746A0
		private static QueryFilter CreateMailEnabledFilter(bool exists)
		{
			QueryFilter queryFilter = new ExistsFilter(ADRecipientSchema.Alias);
			if (!exists)
			{
				return new NotFilter(queryFilter);
			}
			return queryFilter;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x000764C4 File Offset: 0x000746C4
		private static QueryFilter CreateUniversalGroupFilter(bool isUniversal)
		{
			QueryFilter queryFilter = new BitMaskOrFilter(ADGroupSchema.GroupType, 8UL);
			if (!isUniversal)
			{
				return new NotFilter(queryFilter);
			}
			return queryFilter;
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x000764EC File Offset: 0x000746EC
		private static QueryFilter CreateSecurityGroupFilter(bool securityEnabled)
		{
			QueryFilter queryFilter = new BitMaskOrFilter(ADGroupSchema.GroupType, (ulong)int.MinValue);
			if (!securityEnabled)
			{
				return new NotFilter(queryFilter);
			}
			return queryFilter;
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00076518 File Offset: 0x00074718
		private static QueryFilter CreateMailboxEnabledFilter(bool exists)
		{
			QueryFilter queryFilter = new ExistsFilter(IADMailStorageSchema.ServerLegacyDN);
			if (!exists)
			{
				return new NotFilter(queryFilter);
			}
			return queryFilter;
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0007653B File Offset: 0x0007473B
		private static QueryFilter CreateRecipientTypeFilter(RecipientType recipientType)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, recipientType);
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0007654E File Offset: 0x0007474E
		private static QueryFilter CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails recipientTypeDetails)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, recipientTypeDetails);
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00076564 File Offset: 0x00074764
		private static QueryFilter[] InitializeStaticRecipientTypeFilters()
		{
			QueryFilter[] array = new QueryFilter[Filters.RecipientTypeCount];
			array[1] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectClassFilter(ADUser.MostDerivedClass, true),
				ADObject.ObjectCategoryFilter(ADUser.ObjectCategoryNameInternal),
				Filters.CreateMailEnabledFilter(false)
			});
			array[2] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectClassFilter(ADUser.MostDerivedClass, true),
				ADObject.ObjectCategoryFilter(ADUser.ObjectCategoryNameInternal),
				Filters.CreateMailEnabledFilter(true),
				Filters.CreateMailboxEnabledFilter(true)
			});
			array[3] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectClassFilter(ADUser.MostDerivedClass, true),
				ADObject.ObjectCategoryFilter(ADUser.ObjectCategoryNameInternal),
				Filters.CreateMailEnabledFilter(true),
				Filters.CreateMailboxEnabledFilter(false)
			});
			array[4] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectClassFilter(ADContact.MostDerivedClass),
				Filters.CreateMailEnabledFilter(false)
			});
			array[5] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectClassFilter(ADContact.MostDerivedClass),
				Filters.CreateMailEnabledFilter(true)
			});
			array[6] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectCategoryFilter(ADGroup.MostDerivedClass),
				Filters.CreateMailEnabledFilter(false)
			});
			array[9] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectCategoryFilter(ADGroup.MostDerivedClass),
				Filters.NonUniversalGroupFilter,
				Filters.CreateMailEnabledFilter(true)
			});
			array[7] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectCategoryFilter(ADGroup.MostDerivedClass),
				Filters.UniversalDistributionGroupFilter,
				Filters.CreateMailEnabledFilter(true)
			});
			array[8] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectCategoryFilter(ADGroup.MostDerivedClass),
				Filters.UniversalSecurityGroupFilter,
				Filters.CreateMailEnabledFilter(true)
			});
			array[10] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectCategoryFilter(ADDynamicGroup.MostDerivedClass),
				Filters.CreateMailEnabledFilter(true)
			});
			array[11] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectCategoryFilter(ADPublicFolder.MostDerivedClass),
				Filters.CreateMailEnabledFilter(true)
			});
			array[12] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectCategoryFilter(ADPublicDatabase.MostDerivedClass),
				Filters.CreateMailEnabledFilter(true)
			});
			array[13] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectCategoryFilter(ADSystemAttendantMailbox.MostDerivedClass),
				Filters.CreateMailEnabledFilter(true)
			});
			array[15] = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectCategoryFilter(ADMicrosoftExchangeRecipient.MostDerivedClass),
				Filters.CreateMailEnabledFilter(true)
			});
			array[14] = new AndFilter(new QueryFilter[]
			{
				Filters.CreateMailEnabledFilter(true),
				Filters.CreateMailboxEnabledFilter(true),
				new OrFilter(new QueryFilter[]
				{
					ADObject.ObjectCategoryFilter(ADSystemMailbox.MostDerivedClass),
					new AndFilter(new QueryFilter[]
					{
						ADUser.ImplicitFilterInternal,
						new TextFilter(ADObjectSchema.Name, "SystemMailbox{", MatchOptions.Prefix, MatchFlags.Default)
					})
				})
			});
			array[16] = ADComputerRecipient.ImplicitFilterInternal;
			return array;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0007689C File Offset: 0x00074A9C
		internal static bool HasWellKnownRecipientTypeFilter(QueryFilter filter)
		{
			if (filter == null)
			{
				return false;
			}
			if (Filters.IsEqualityFilterOnPropertyDefinition(filter, new PropertyDefinition[]
			{
				ADRecipientSchema.RecipientType,
				ADRecipientSchema.RecipientTypeDetails
			}))
			{
				return true;
			}
			if (Filters.IsOrFilterOnPropertyDefinitionComparisons(filter, new PropertyDefinition[]
			{
				ADRecipientSchema.RecipientType,
				ADRecipientSchema.RecipientTypeDetails
			}))
			{
				return true;
			}
			AndFilter andFilter = filter as AndFilter;
			return andFilter != null && andFilter.FilterCount > 0 && (Filters.IsEqualityFilterOnPropertyDefinition(andFilter.Filters[0], new PropertyDefinition[]
			{
				ADRecipientSchema.RecipientType,
				ADRecipientSchema.RecipientTypeDetails
			}) || Filters.IsOrFilterOnPropertyDefinitionComparisons(andFilter.Filters[0], new PropertyDefinition[]
			{
				ADRecipientSchema.RecipientType,
				ADRecipientSchema.RecipientTypeDetails
			}));
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00076968 File Offset: 0x00074B68
		internal static bool IsOrFilterOnPropertyDefinitionComparisons(QueryFilter filterToCheck, params PropertyDefinition[] propertyDefinitions)
		{
			OrFilter orFilter = filterToCheck as OrFilter;
			if (orFilter == null || orFilter.FilterCount <= 0)
			{
				return false;
			}
			foreach (QueryFilter filterToCheck2 in orFilter.Filters)
			{
				if (!Filters.IsEqualityFilterOnPropertyDefinition(filterToCheck2, propertyDefinitions))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x000769DC File Offset: 0x00074BDC
		internal static bool IsEqualityFilterOnPropertyDefinition(QueryFilter filterToCheck, params PropertyDefinition[] propertyDefinitions)
		{
			ComparisonFilter comparisonFilter = filterToCheck as ComparisonFilter;
			if (comparisonFilter != null && comparisonFilter.ComparisonOperator == ComparisonOperator.Equal)
			{
				foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
				{
					if (comparisonFilter.Property == propertyDefinition)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00076A24 File Offset: 0x00074C24
		internal static QueryFilter OptimizeRecipientTypeFilter(OrFilter orFilter)
		{
			Filters.RecipientTypeBitVector32 recipientTypeBitVector = default(Filters.RecipientTypeBitVector32);
			List<QueryFilter> list = null;
			foreach (QueryFilter queryFilter in orFilter.Filters)
			{
				ComparisonFilter comparisonFilter = queryFilter as ComparisonFilter;
				if (comparisonFilter != null && comparisonFilter.ComparisonOperator == ComparisonOperator.Equal && comparisonFilter.Property == ADRecipientSchema.RecipientType)
				{
					RecipientType index = (RecipientType)ADObject.PropertyValueFromEqualityFilter(comparisonFilter);
					recipientTypeBitVector[index] = true;
				}
				else
				{
					if (list == null)
					{
						list = new List<QueryFilter>(orFilter.FilterCount);
					}
					list.Add(queryFilter);
				}
			}
			QueryFilter queryFilter2 = null;
			if (!Filters.RecipientTypeFilterOptimizations.TryGetValue(recipientTypeBitVector.Data, out queryFilter2))
			{
				return orFilter;
			}
			if (list == null)
			{
				return queryFilter2;
			}
			list.Add(queryFilter2);
			return new OrFilter(list.ToArray());
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00076AFC File Offset: 0x00074CFC
		internal static QueryFilter GetRecipientTypeDetailsFilterOptimization(RecipientTypeDetails recipientTypeDetails)
		{
			QueryFilter result = null;
			Filters.RecipientTypeDetailsFilterOptimizations.TryGetValue(recipientTypeDetails, out result);
			return result;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00076B1C File Offset: 0x00074D1C
		private static Dictionary<int, QueryFilter> InitializeStaticRecipientTypeFilterOptimizations()
		{
			Dictionary<int, QueryFilter> dictionary = new Dictionary<int, QueryFilter>(32);
			Filters.RecipientTypeBitVector32 recipientTypeBitVector = default(Filters.RecipientTypeBitVector32);
			recipientTypeBitVector.Reset();
			recipientTypeBitVector[RecipientType.DynamicDistributionGroup] = true;
			recipientTypeBitVector[RecipientType.UserMailbox] = true;
			recipientTypeBitVector[RecipientType.MailContact] = true;
			recipientTypeBitVector[RecipientType.MailUniversalDistributionGroup] = true;
			recipientTypeBitVector[RecipientType.MailUniversalSecurityGroup] = true;
			recipientTypeBitVector[RecipientType.MailUser] = true;
			QueryFilter value = Filters.AllMailableUsersContactsDDLsUniversalGroupsFilter;
			dictionary.Add(recipientTypeBitVector.Data, value);
			recipientTypeBitVector.Reset();
			recipientTypeBitVector[RecipientType.Contact] = true;
			recipientTypeBitVector[RecipientType.MailContact] = true;
			value = ADObject.ObjectClassFilter(ADContact.MostDerivedClass);
			dictionary.Add(recipientTypeBitVector.Data, value);
			recipientTypeBitVector.Reset();
			recipientTypeBitVector[RecipientType.Group] = true;
			recipientTypeBitVector[RecipientType.MailNonUniversalGroup] = true;
			recipientTypeBitVector[RecipientType.MailUniversalDistributionGroup] = true;
			recipientTypeBitVector[RecipientType.MailUniversalSecurityGroup] = true;
			value = ADObject.ObjectCategoryFilter(ADGroup.MostDerivedClass);
			dictionary.Add(recipientTypeBitVector.Data, value);
			recipientTypeBitVector.Reset();
			recipientTypeBitVector[RecipientType.MailNonUniversalGroup] = true;
			recipientTypeBitVector[RecipientType.MailUniversalDistributionGroup] = true;
			recipientTypeBitVector[RecipientType.MailUniversalSecurityGroup] = true;
			value = new AndFilter(new QueryFilter[]
			{
				Filters.CreateMailEnabledFilter(true),
				ADObject.ObjectCategoryFilter(ADGroup.MostDerivedClass)
			});
			dictionary.Add(recipientTypeBitVector.Data, value);
			recipientTypeBitVector.Reset();
			recipientTypeBitVector[RecipientType.User] = true;
			recipientTypeBitVector[RecipientType.MailUser] = true;
			recipientTypeBitVector[RecipientType.UserMailbox] = true;
			value = ADUser.ImplicitFilterInternal;
			dictionary.Add(recipientTypeBitVector.Data, value);
			recipientTypeBitVector.Reset();
			recipientTypeBitVector[RecipientType.DynamicDistributionGroup] = true;
			recipientTypeBitVector[RecipientType.UserMailbox] = true;
			recipientTypeBitVector[RecipientType.MailContact] = true;
			recipientTypeBitVector[RecipientType.MailUniversalDistributionGroup] = true;
			recipientTypeBitVector[RecipientType.MailUniversalSecurityGroup] = true;
			recipientTypeBitVector[RecipientType.MailUser] = true;
			recipientTypeBitVector[RecipientType.MailNonUniversalGroup] = true;
			recipientTypeBitVector[RecipientType.PublicFolder] = true;
			value = new AndFilter(new QueryFilter[]
			{
				Filters.CreateMailEnabledFilter(true),
				new OrFilter(new QueryFilter[]
				{
					ADObject.ObjectCategoryFilter(ADUser.ObjectCategoryNameInternal),
					ADObject.ObjectCategoryFilter(ADDynamicGroup.ObjectCategoryNameInternal),
					ADObject.ObjectCategoryFilter(ADGroup.MostDerivedClass),
					ADObject.ObjectCategoryFilter(ADPublicFolder.MostDerivedClass)
				})
			});
			dictionary.Add(recipientTypeBitVector.Data, value);
			recipientTypeBitVector.Reset();
			recipientTypeBitVector[RecipientType.DynamicDistributionGroup] = true;
			recipientTypeBitVector[RecipientType.UserMailbox] = true;
			recipientTypeBitVector[RecipientType.MailContact] = true;
			recipientTypeBitVector[RecipientType.MailUniversalDistributionGroup] = true;
			recipientTypeBitVector[RecipientType.MailUniversalSecurityGroup] = true;
			recipientTypeBitVector[RecipientType.MailUser] = true;
			recipientTypeBitVector[RecipientType.MailNonUniversalGroup] = true;
			recipientTypeBitVector[RecipientType.PublicFolder] = true;
			recipientTypeBitVector[RecipientType.SystemAttendantMailbox] = true;
			recipientTypeBitVector[RecipientType.SystemMailbox] = true;
			value = new AndFilter(new QueryFilter[]
			{
				Filters.CreateMailEnabledFilter(true),
				new OrFilter(new QueryFilter[]
				{
					ADObject.ObjectCategoryFilter(ADUser.ObjectCategoryNameInternal),
					ADObject.ObjectCategoryFilter(ADDynamicGroup.ObjectCategoryNameInternal),
					ADObject.ObjectCategoryFilter(ADGroup.MostDerivedClass),
					ADObject.ObjectCategoryFilter(ADPublicFolder.MostDerivedClass),
					ADObject.ObjectCategoryFilter(ADSystemAttendantMailbox.MostDerivedClass),
					ADObject.ObjectCategoryFilter(ADSystemMailbox.MostDerivedClass)
				})
			});
			dictionary.Add(recipientTypeBitVector.Data, value);
			recipientTypeBitVector.Reset();
			recipientTypeBitVector[RecipientType.DynamicDistributionGroup] = true;
			recipientTypeBitVector[RecipientType.MailUniversalDistributionGroup] = true;
			recipientTypeBitVector[RecipientType.MailUniversalSecurityGroup] = true;
			value = new AndFilter(new QueryFilter[]
			{
				Filters.CreateMailEnabledFilter(true),
				new OrFilter(new QueryFilter[]
				{
					ADObject.ObjectCategoryFilter(ADDynamicGroup.ObjectCategoryNameInternal),
					new AndFilter(new QueryFilter[]
					{
						ADObject.ObjectCategoryFilter(ADGroup.MostDerivedClass),
						Filters.CreateUniversalGroupFilter(true)
					})
				})
			});
			dictionary.Add(recipientTypeBitVector.Data, value);
			recipientTypeBitVector.Reset();
			recipientTypeBitVector[RecipientType.MailContact] = true;
			recipientTypeBitVector[RecipientType.MailUser] = true;
			value = new AndFilter(new QueryFilter[]
			{
				ADObject.ObjectCategoryFilter(ADUser.ObjectCategoryNameInternal),
				Filters.CreateMailEnabledFilter(true),
				new OrFilter(new QueryFilter[]
				{
					ADObject.ObjectClassFilter(ADContact.MostDerivedClass),
					Filters.CreateMailboxEnabledFilter(false)
				})
			});
			dictionary.Add(recipientTypeBitVector.Data, value);
			recipientTypeBitVector.Reset();
			return dictionary;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00076F74 File Offset: 0x00075174
		private static Dictionary<RecipientTypeDetails, QueryFilter> InitializeStaticRecipientTypeDetailsFilterOptimizations()
		{
			Dictionary<RecipientTypeDetails, QueryFilter> dictionary = new Dictionary<RecipientTypeDetails, QueryFilter>(32);
			RecipientTypeDetails key = RecipientTypeDetails.UserMailbox | RecipientTypeDetails.LinkedMailbox | RecipientTypeDetails.SharedMailbox | RecipientTypeDetails.LegacyMailbox | RecipientTypeDetails.RoomMailbox | RecipientTypeDetails.EquipmentMailbox | RecipientTypeDetails.MailContact | RecipientTypeDetails.MailUser | RecipientTypeDetails.MailUniversalDistributionGroup | RecipientTypeDetails.MailUniversalSecurityGroup | RecipientTypeDetails.DynamicDistributionGroup | RecipientTypeDetails.MailForestContact | RecipientTypeDetails.RoomList | RecipientTypeDetails.DiscoveryMailbox | RecipientTypeDetails.RemoteUserMailbox | RecipientTypeDetails.RemoteRoomMailbox | RecipientTypeDetails.RemoteEquipmentMailbox | RecipientTypeDetails.RemoteSharedMailbox | RecipientTypeDetails.TeamMailbox | RecipientTypeDetails.RemoteTeamMailbox | RecipientTypeDetails.GroupMailbox | RecipientTypeDetails.LinkedRoomMailbox | RecipientTypeDetails.RemoteGroupMailbox;
			dictionary.Add(key, new AndFilter(new QueryFilter[]
			{
				Filters.AllMailableUsersContactsDDLsUniversalGroupsFilter,
				new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.ArbitrationMailbox)),
				new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.AuditLogMailbox)),
				new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.MailboxPlan))
			}));
			dictionary.Add(RecipientTypeDetails.User | RecipientTypeDetails.DisabledUser, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.User),
				new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.LinkedUser))
			}));
			dictionary.Add(RecipientTypeDetails.UniversalDistributionGroup | RecipientTypeDetails.UniversalSecurityGroup, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.Group),
				new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.RoleGroup)),
				Filters.CreateUniversalGroupFilter(true)
			}));
			dictionary.Add(RecipientTypeDetails.MailContact | RecipientTypeDetails.MailForestContact, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.MailContact)
			}));
			key = (RecipientTypeDetails.UserMailbox | RecipientTypeDetails.LinkedMailbox | RecipientTypeDetails.SharedMailbox | RecipientTypeDetails.LegacyMailbox | RecipientTypeDetails.RoomMailbox | RecipientTypeDetails.EquipmentMailbox | RecipientTypeDetails.MailUser | RecipientTypeDetails.User | RecipientTypeDetails.DisabledUser | RecipientTypeDetails.RemoteUserMailbox | RecipientTypeDetails.RemoteRoomMailbox | RecipientTypeDetails.RemoteEquipmentMailbox | RecipientTypeDetails.RemoteSharedMailbox | RecipientTypeDetails.TeamMailbox | RecipientTypeDetails.RemoteTeamMailbox);
			dictionary.Add(key, new OrFilter(new QueryFilter[]
			{
				new AndFilter(new QueryFilter[]
				{
					ADUser.ImplicitFilterInternal,
					new NotFilter(new ExistsFilter(ADRecipientSchema.RecipientTypeDetailsValue))
				}),
				new AndFilter(new QueryFilter[]
				{
					ADUser.ImplicitFilterInternal,
					new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.DiscoveryMailbox)),
					new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.LinkedUser))
				})
			}));
			key = (RecipientTypeDetails.UserMailbox | RecipientTypeDetails.LinkedMailbox | RecipientTypeDetails.SharedMailbox | RecipientTypeDetails.LegacyMailbox | RecipientTypeDetails.RoomMailbox | RecipientTypeDetails.EquipmentMailbox | RecipientTypeDetails.MailContact | RecipientTypeDetails.MailUser | RecipientTypeDetails.MailUniversalDistributionGroup | RecipientTypeDetails.MailNonUniversalGroup | RecipientTypeDetails.MailUniversalSecurityGroup | RecipientTypeDetails.DynamicDistributionGroup | RecipientTypeDetails.PublicFolder | RecipientTypeDetails.MailForestContact | RecipientTypeDetails.RoomList | RecipientTypeDetails.DiscoveryMailbox | RecipientTypeDetails.RemoteUserMailbox | RecipientTypeDetails.RemoteRoomMailbox | RecipientTypeDetails.RemoteEquipmentMailbox | RecipientTypeDetails.RemoteSharedMailbox | RecipientTypeDetails.TeamMailbox | RecipientTypeDetails.RemoteTeamMailbox | RecipientTypeDetails.LinkedRoomMailbox);
			dictionary.Add(key, Filters.AllRecipientsForGetRecipientTask);
			key = (RecipientTypeDetails.UserMailbox | RecipientTypeDetails.LinkedMailbox | RecipientTypeDetails.SharedMailbox | RecipientTypeDetails.LegacyMailbox | RecipientTypeDetails.RoomMailbox | RecipientTypeDetails.EquipmentMailbox | RecipientTypeDetails.DiscoveryMailbox | RecipientTypeDetails.TeamMailbox | RecipientTypeDetails.GroupMailbox | RecipientTypeDetails.LinkedRoomMailbox);
			dictionary.Add(key, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.ArbitrationMailbox)),
				new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.AuditLogMailbox)),
				new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.MailboxPlan))
			}));
			key = (RecipientTypeDetails.MailUniversalDistributionGroup | RecipientTypeDetails.MailNonUniversalGroup | RecipientTypeDetails.MailUniversalSecurityGroup | RecipientTypeDetails.RoomList);
			dictionary.Add(key, new OrFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.MailNonUniversalGroup),
				Filters.CreateRecipientTypeFilter(RecipientType.MailUniversalDistributionGroup),
				Filters.CreateRecipientTypeFilter(RecipientType.MailUniversalSecurityGroup)
			}));
			key = (RecipientTypeDetails.MailUniversalDistributionGroup | RecipientTypeDetails.MailNonUniversalGroup | RecipientTypeDetails.MailUniversalSecurityGroup);
			dictionary.Add(key, new AndFilter(new QueryFilter[]
			{
				new OrFilter(new QueryFilter[]
				{
					Filters.CreateRecipientTypeFilter(RecipientType.MailNonUniversalGroup),
					Filters.CreateRecipientTypeFilter(RecipientType.MailUniversalDistributionGroup),
					Filters.CreateRecipientTypeFilter(RecipientType.MailUniversalSecurityGroup)
				}),
				new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.RoomList))
			}));
			dictionary.Add(RecipientTypeDetails.RoomMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.RoomMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.LinkedRoomMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.LinkedRoomMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.EquipmentMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.EquipmentMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.LinkedMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.LinkedMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.UserMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.UserMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.MailForestContact, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.MailContact),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.MailForestContact)
			}));
			dictionary.Add(RecipientTypeDetails.SharedMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.SharedMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.TeamMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.TeamMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.RemoteGroupMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.MailUniversalDistributionGroup),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.RemoteGroupMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.GroupMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.GroupMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.ArbitrationMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.ArbitrationMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.MailboxPlan, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.MailboxPlan)
			}));
			dictionary.Add(RecipientTypeDetails.LinkedUser, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.User),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.LinkedUser)
			}));
			dictionary.Add(RecipientTypeDetails.RoomList, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.MailUniversalDistributionGroup),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.RoomList)
			}));
			dictionary.Add(RecipientTypeDetails.DiscoveryMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.DiscoveryMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.AuditLogMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.AuditLogMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.LegacyMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.UserMailbox),
				new OrFilter(new QueryFilter[]
				{
					new NotFilter(new ExistsFilter(ADRecipientSchema.RecipientTypeDetailsValue)),
					Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.LegacyMailbox)
				})
			}));
			dictionary.Add(RecipientTypeDetails.MailContact, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.MailContact),
				new OrFilter(new QueryFilter[]
				{
					new NotFilter(new ExistsFilter(ADRecipientSchema.RecipientTypeDetailsValue)),
					Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.MailContact)
				})
			}));
			dictionary.Add(RecipientTypeDetails.User, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.User),
				Filters.UserEnabledFilter
			}));
			dictionary.Add(RecipientTypeDetails.Contact, Filters.CreateRecipientTypeFilter(RecipientType.Contact));
			dictionary.Add(RecipientTypeDetails.UniversalDistributionGroup, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.Group),
				Filters.UniversalDistributionGroupFilter
			}));
			dictionary.Add(RecipientTypeDetails.UniversalSecurityGroup, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.Group),
				new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.RoleGroup)),
				Filters.UniversalSecurityGroupFilter
			}));
			dictionary.Add(RecipientTypeDetails.RoleGroup, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.Group),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.RoleGroup),
				Filters.UniversalSecurityGroupFilter
			}));
			dictionary.Add(RecipientTypeDetails.NonUniversalGroup, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.Group),
				Filters.NonUniversalGroupFilter
			}));
			dictionary.Add(RecipientTypeDetails.DisabledUser, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.User),
				Filters.UserDisabledFilter,
				new OrFilter(new QueryFilter[]
				{
					new NotFilter(new ExistsFilter(ADRecipientSchema.RecipientTypeDetailsValue)),
					Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.DisabledUser)
				})
			}));
			key = (RecipientTypeDetails.UserMailbox | RecipientTypeDetails.LinkedMailbox | RecipientTypeDetails.SharedMailbox | RecipientTypeDetails.LegacyMailbox | RecipientTypeDetails.RoomMailbox | RecipientTypeDetails.EquipmentMailbox | RecipientTypeDetails.MailContact | RecipientTypeDetails.MailUser | RecipientTypeDetails.MailForestContact | RecipientTypeDetails.DiscoveryMailbox | RecipientTypeDetails.RemoteUserMailbox | RecipientTypeDetails.RemoteRoomMailbox | RecipientTypeDetails.RemoteEquipmentMailbox | RecipientTypeDetails.RemoteSharedMailbox | RecipientTypeDetails.TeamMailbox | RecipientTypeDetails.RemoteTeamMailbox | RecipientTypeDetails.GroupMailbox | RecipientTypeDetails.LinkedRoomMailbox | RecipientTypeDetails.RemoteGroupMailbox);
			dictionary.Add(key, new AndFilter(new QueryFilter[]
			{
				Filters.CreateMailEnabledFilter(true),
				ADObject.ObjectCategoryFilter(ADUser.ObjectCategoryNameInternal),
				new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.ArbitrationMailbox)),
				new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.AuditLogMailbox)),
				new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.MailboxPlan))
			}));
			dictionary.Add(RecipientTypeDetails.MailUser, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.MailUser),
				new OrFilter(new QueryFilter[]
				{
					Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.MailUser),
					new NotFilter(new ExistsFilter(ADRecipientSchema.RecipientTypeDetailsValue))
				})
			}));
			dictionary.Add(RecipientTypeDetails.MailUniversalDistributionGroup, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.MailUniversalDistributionGroup),
				new OrFilter(new QueryFilter[]
				{
					new NotFilter(new ExistsFilter(ADRecipientSchema.RecipientTypeDetailsValue)),
					Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.MailUniversalDistributionGroup)
				})
			}));
			dictionary.Add(RecipientTypeDetails.MailUniversalSecurityGroup, Filters.CreateRecipientTypeFilter(RecipientType.MailUniversalSecurityGroup));
			dictionary.Add(RecipientTypeDetails.MailNonUniversalGroup, Filters.CreateRecipientTypeFilter(RecipientType.MailNonUniversalGroup));
			dictionary.Add(RecipientTypeDetails.DynamicDistributionGroup, Filters.CreateRecipientTypeFilter(RecipientType.DynamicDistributionGroup));
			dictionary.Add(RecipientTypeDetails.PublicFolder, Filters.CreateRecipientTypeFilter(RecipientType.PublicFolder));
			dictionary.Add(RecipientTypeDetails.MicrosoftExchange, Filters.CreateRecipientTypeFilter(RecipientType.MicrosoftExchange));
			dictionary.Add(RecipientTypeDetails.SystemAttendantMailbox, Filters.CreateRecipientTypeFilter(RecipientType.SystemAttendantMailbox));
			dictionary.Add(RecipientTypeDetails.SystemMailbox, Filters.CreateRecipientTypeFilter(RecipientType.SystemMailbox));
			dictionary.Add((RecipientTypeDetails)((ulong)int.MinValue), new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.MailUser),
				Filters.CreateRecipientTypeDetailsValueFilter((RecipientTypeDetails)((ulong)int.MinValue))
			}));
			dictionary.Add(RecipientTypeDetails.Computer, Filters.CreateRecipientTypeFilter(RecipientType.Computer));
			dictionary.Add(RecipientTypeDetails.RemoteRoomMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.MailUser),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.RemoteRoomMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.RemoteEquipmentMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.MailUser),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.RemoteEquipmentMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.RemoteTeamMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.MailUser),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.RemoteTeamMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.RemoteSharedMailbox, new AndFilter(new QueryFilter[]
			{
				Filters.CreateRecipientTypeFilter(RecipientType.MailUser),
				Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.RemoteSharedMailbox)
			}));
			dictionary.Add(RecipientTypeDetails.PublicFolderMailbox, Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.PublicFolderMailbox));
			dictionary.Add(RecipientTypeDetails.MonitoringMailbox, Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.MonitoringMailbox));
			return dictionary;
		}

		// Token: 0x04000DA0 RID: 3488
		public static readonly QueryFilter DefaultRecipientFilter = new OrFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "person"),
			new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "msExchDynamicDistributionList"),
			new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "group"),
			new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "publicFolder"),
			new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "msExchPublicMDB"),
			new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "msExchSystemMailbox"),
			new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADMicrosoftExchangeRecipient.MostDerivedClass),
			new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "exchangeAdminService"),
			new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "computer")
		});

		// Token: 0x04000DA1 RID: 3489
		internal static readonly int RecipientTypeCount = Enum.GetValues(typeof(RecipientType)).Length;

		// Token: 0x04000DA2 RID: 3490
		private static readonly QueryFilter UserEnabledFilter = Filters.CreateUserEnabledFilter(true);

		// Token: 0x04000DA3 RID: 3491
		private static readonly QueryFilter UserDisabledFilter = Filters.CreateUserEnabledFilter(false);

		// Token: 0x04000DA4 RID: 3492
		private static readonly QueryFilter NonUniversalGroupFilter = Filters.CreateUniversalGroupFilter(false);

		// Token: 0x04000DA5 RID: 3493
		private static readonly QueryFilter UniversalSecurityGroupFilter = new BitMaskAndFilter(ADGroupSchema.GroupType, (ulong)-2147483640);

		// Token: 0x04000DA6 RID: 3494
		private static readonly QueryFilter UniversalDistributionGroupFilter = new AndFilter(new QueryFilter[]
		{
			Filters.CreateUniversalGroupFilter(true),
			Filters.CreateSecurityGroupFilter(false)
		});

		// Token: 0x04000DA7 RID: 3495
		private static readonly QueryFilter AllMailableUsersContactsDDLsUniversalGroupsFilter = new AndFilter(new QueryFilter[]
		{
			Filters.CreateMailEnabledFilter(true),
			new OrFilter(new QueryFilter[]
			{
				ADObject.ObjectCategoryFilter(ADUser.ObjectCategoryNameInternal),
				ADObject.ObjectCategoryFilter(ADDynamicGroup.ObjectCategoryNameInternal),
				new AndFilter(new QueryFilter[]
				{
					ADObject.ObjectCategoryFilter(ADGroup.MostDerivedClass),
					Filters.CreateUniversalGroupFilter(true)
				})
			})
		});

		// Token: 0x04000DA8 RID: 3496
		private static readonly QueryFilter AllRecipientsForGetRecipientTask = new AndFilter(new QueryFilter[]
		{
			Filters.CreateMailEnabledFilter(true),
			new OrFilter(new QueryFilter[]
			{
				ADObject.ObjectCategoryFilter(ADUser.ObjectCategoryNameInternal),
				ADObject.ObjectCategoryFilter(ADDynamicGroup.ObjectCategoryNameInternal),
				ADObject.ObjectCategoryFilter(ADGroup.MostDerivedClass),
				ADObject.ObjectCategoryFilter(ADPublicFolder.MostDerivedClass)
			}),
			new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.ArbitrationMailbox)),
			new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.MailboxPlan)),
			new NotFilter(Filters.CreateRecipientTypeDetailsValueFilter(RecipientTypeDetails.AuditLogMailbox))
		});

		// Token: 0x04000DA9 RID: 3497
		internal static readonly QueryFilter[] RecipientTypeFilters = Filters.InitializeStaticRecipientTypeFilters();

		// Token: 0x04000DAA RID: 3498
		private static readonly Dictionary<int, QueryFilter> RecipientTypeFilterOptimizations = Filters.InitializeStaticRecipientTypeFilterOptimizations();

		// Token: 0x04000DAB RID: 3499
		private static readonly Dictionary<RecipientTypeDetails, QueryFilter> RecipientTypeDetailsFilterOptimizations = Filters.InitializeStaticRecipientTypeDetailsFilterOptimizations();

		// Token: 0x02000248 RID: 584
		private struct RecipientTypeBitVector32
		{
			// Token: 0x17000687 RID: 1671
			// (get) Token: 0x06001C97 RID: 7319 RVA: 0x00077D27 File Offset: 0x00075F27
			internal int Data
			{
				get
				{
					return this.data;
				}
			}

			// Token: 0x06001C98 RID: 7320 RVA: 0x00077D2F File Offset: 0x00075F2F
			internal void Reset()
			{
				this.data = 0;
			}

			// Token: 0x17000688 RID: 1672
			internal bool this[RecipientType index]
			{
				set
				{
					if (value)
					{
						this.data |= 1 << (int)index;
						return;
					}
					this.data &= ~(1 << (int)index);
				}
			}

			// Token: 0x04000DAC RID: 3500
			private int data;
		}
	}
}

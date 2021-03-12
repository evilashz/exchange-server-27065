using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000384 RID: 900
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarCorrelationMatch : IComparable<CalendarCorrelationMatch>
	{
		// Token: 0x060027B0 RID: 10160 RVA: 0x0009E66C File Offset: 0x0009C86C
		private static SortBy[] GetRelatedItemsSortOrder(PropertyDefinition relationBase)
		{
			return new SortBy[]
			{
				new SortBy(relationBase, SortOrder.Ascending)
			};
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x0009E68C File Offset: 0x0009C88C
		private static SortBy[] GetRelatedItemsChronologicalSortOrder(PropertyDefinition relationBase)
		{
			return new SortBy[]
			{
				new SortBy(relationBase, SortOrder.Ascending),
				new SortBy(InternalSchema.ItemVersion, SortOrder.Descending),
				new SortBy(InternalSchema.OriginalLastModifiedTime, SortOrder.Descending)
			};
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x0009E6C8 File Offset: 0x0009C8C8
		private static SortBy[] GetRelatedItemsByClassSortOrder(PropertyDefinition relationBase)
		{
			return new SortBy[]
			{
				new SortBy(relationBase, SortOrder.Ascending),
				new SortBy(InternalSchema.ItemClass, SortOrder.Ascending)
			};
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x0009E6F8 File Offset: 0x0009C8F8
		private static SortBy[] GetRelatedItemsByClassChronologicalSortOrder(PropertyDefinition relationBase)
		{
			return new SortBy[]
			{
				new SortBy(relationBase, SortOrder.Ascending),
				new SortBy(InternalSchema.ItemClass, SortOrder.Ascending),
				new SortBy(InternalSchema.ItemVersion, SortOrder.Descending),
				new SortBy(InternalSchema.OriginalLastModifiedTime, SortOrder.Descending)
			};
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x060027B4 RID: 10164 RVA: 0x0009E741 File Offset: 0x0009C941
		private static ICollection<PropertyDefinition> DefaultPropertySet
		{
			get
			{
				return CalendarCorrelationMatch.GetPropertySet("{651EFB55-4E21-44c3-8338-81A2502FA65D}", null, true);
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x0009E74F File Offset: 0x0009C94F
		internal PropertyBag Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x0009E758 File Offset: 0x0009C958
		private static ICollection<PropertyDefinition> GetPropertySet(string schemaKey, ICollection<PropertyDefinition> additionalRequiredProperties, bool useCache)
		{
			ICollection<PropertyDefinition> collection = null;
			if (useCache)
			{
				if (schemaKey == null)
				{
					throw new ArgumentNullException("schemaKey");
				}
				lock (CalendarCorrelationMatch.threadSafetyLock)
				{
					if (!CalendarCorrelationMatch.requiredPropertiesCache.TryGetValue(schemaKey, out collection))
					{
						collection = CalendarCorrelationMatch.CreateNewPropertySet(additionalRequiredProperties);
						CalendarCorrelationMatch.requiredPropertiesCache.Add(schemaKey, collection);
					}
					return collection;
				}
			}
			collection = CalendarCorrelationMatch.CreateNewPropertySet(additionalRequiredProperties);
			return collection;
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x0009E7D0 File Offset: 0x0009C9D0
		private static ICollection<PropertyDefinition> CreateNewPropertySet(ICollection<PropertyDefinition> additionalProperties)
		{
			ICollection<PropertyDefinition> collection = (additionalProperties == null || additionalProperties.Count == 0) ? CalendarCorrelationMatch.CorrelatedItemViewProperties : CalendarCorrelationMatch.CorrelatedItemViewProperties.Union(additionalProperties);
			return CalendarCorrelationMatch.GetNativeProperties(collection);
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x0009E804 File Offset: 0x0009CA04
		private static ICollection<PropertyDefinition> GetNativeProperties(ICollection<PropertyDefinition> properties)
		{
			ICollection<NativeStorePropertyDefinition> nativePropertyDefinitions = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.NeedForRead, properties);
			ICollection<PropertyDefinition> collection = new List<PropertyDefinition>(nativePropertyDefinitions.Count);
			foreach (PropertyDefinition item in nativePropertyDefinitions)
			{
				collection.Add(item);
			}
			return collection;
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x0009E864 File Offset: 0x0009CA64
		private CalendarCorrelationMatch(PropertyBag propertyBag, GlobalObjectId globalObjectId)
		{
			this.Id = propertyBag.GetValueOrDefault<VersionedId>(InternalSchema.ItemId);
			this.documentId = propertyBag.GetValueOrDefault<int>(InternalSchema.DocumentId, int.MinValue);
			byte[] goidBytes = CalendarCorrelationMatch.GetGoidBytes(propertyBag);
			this.goid = ((goidBytes == null) ? null : new GlobalObjectId(goidBytes));
			object obj = propertyBag.TryGetProperty(InternalSchema.AppointmentRecurrenceBlob);
			this.isRecurringMaster = (obj is byte[] || PropertyError.IsPropertyValueTooBig(obj));
			this.appointmentSequenceNumber = propertyBag.GetValueAsNullable<int>(InternalSchema.AppointmentSequenceNumber);
			this.lastModifiedTime = propertyBag.GetValueAsNullable<ExDateTime>(InternalSchema.LastModifiedTime);
			this.ownerCriticalChangeTime = propertyBag.GetValueAsNullable<ExDateTime>(InternalSchema.OwnerCriticalChangeTime);
			this.IsCorrelated = this.CheckIsCorrelated(globalObjectId, out this.isMasterMatchingTheOccurrence);
			this.properties = propertyBag;
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x0009E927 File Offset: 0x0009CB27
		private static byte[] GetGoidBytes(PropertyBag propertyBag)
		{
			return propertyBag.GetValueOrDefault<byte[]>(InternalSchema.GlobalObjectId) ?? propertyBag.GetValueOrDefault<byte[]>(InternalSchema.CleanGlobalObjectId);
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x0009E944 File Offset: 0x0009CB44
		private static void ValidateAdditionalProperties(ICollection<PropertyDefinition> additionalProperties)
		{
			if (additionalProperties == null || additionalProperties.Count == 0)
			{
				return;
			}
			foreach (PropertyDefinition propertyDefinition in additionalProperties)
			{
				if (!RecurrenceManager.CanPropertyBeInExceptionData(propertyDefinition) && !RecurrenceManager.MasterOnlyProperties.Contains(propertyDefinition))
				{
					throw new ArgumentException(string.Format("[CalendarCorrelationMatch.ValidateAdditionalProperties] Property '{0}' cannot be requested because it is not in the O11 blob properties nor master property list", propertyDefinition.Name));
				}
			}
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x0009E9DC File Offset: 0x0009CBDC
		internal static List<CalendarCorrelationMatch> FindMatches(CalendarFolder folder, GlobalObjectId globalObjectId, ICollection<PropertyDefinition> additionalProperties = null)
		{
			CalendarCorrelationMatch.ValidateAdditionalProperties(additionalProperties);
			CalendarCorrelationMatch.CalendarCorrelationMatchCollection matchCollection = new CalendarCorrelationMatch.CalendarCorrelationMatchCollection();
			CalendarCorrelationMatch.QueryRelatedItems(folder, globalObjectId, CalendarCorrelationMatch.GetPropertySet(CalendarCorrelationMatch.BuildSchemaKey(additionalProperties), additionalProperties, true), delegate(PropertyBag match)
			{
				matchCollection.AddMatch(globalObjectId, match);
				return true;
			}, false, false, null, null, null);
			return matchCollection.FoundMatches;
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x0009EA4C File Offset: 0x0009CC4C
		private static string BuildSchemaKey(ICollection<PropertyDefinition> additionalProperties)
		{
			if (additionalProperties == null || additionalProperties.Count == 0)
			{
				return "{651EFB55-4E21-44c3-8338-81A2502FA65D}";
			}
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in additionalProperties)
			{
				num ^= propertyDefinition.Name.GetHashCode();
			}
			return string.Format("{0}_{1}", "{651EFB55-4E21-44c3-8338-81A2502FA65D}", num);
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x0009EB30 File Offset: 0x0009CD30
		internal static void QuerySubjectContains(Folder folder, string subject, string schemaKey, ICollection<PropertyDefinition> propertiesToFetch, bool useCachedPropertySetIfPresent, Action<PropertyBag> matchFoundAction, ExDateTime startDate, ExDateTime endDate)
		{
			string normalizedSubject;
			string text;
			SubjectProperty.ComputeSubjectPrefix(subject, out text, out normalizedSubject);
			CalendarCorrelationMatch.QueryItemsUsingView(folder, new SortBy[]
			{
				new SortBy(InternalSchema.OriginalLastModifiedTime, SortOrder.Descending)
			}, null, CalendarCorrelationMatch.GetPropertySet(schemaKey, propertiesToFetch, useCachedPropertySetIfPresent), delegate(PropertyBag propertyBag)
			{
				string text2 = propertyBag.GetValueOrDefault<string>(InternalSchema.NormalizedSubject);
				if (string.IsNullOrEmpty(text2))
				{
					text2 = string.Empty;
				}
				if (text2.IndexOf(normalizedSubject, StringComparison.CurrentCultureIgnoreCase) != -1 && CalendarCorrelationMatch.MatchOriginalLastModifiedTime(propertyBag, new ExDateTime?(startDate), new ExDateTime?(endDate)))
				{
					matchFoundAction(propertyBag);
				}
				return true;
			});
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x0009EB9C File Offset: 0x0009CD9C
		public static void QueryRelatedItems(Folder folder, GlobalObjectId globalObjectId, string schemaKey, ICollection<PropertyDefinition> propertiesToFetch, bool useCachedPropertySetIfPresent, Func<PropertyBag, bool> matchFoundAction, bool fetchResultsInReverseChronologicalOrder, bool sameGoidOnly, string[] itemClassFilter, ExDateTime? startDate, ExDateTime? endDate)
		{
			Util.ThrowOnNullArgument(matchFoundAction, "matchFoundAction");
			CalendarCorrelationMatch.QueryRelatedItems(folder, globalObjectId, CalendarCorrelationMatch.GetPropertySet(schemaKey, propertiesToFetch, useCachedPropertySetIfPresent), matchFoundAction, fetchResultsInReverseChronologicalOrder, sameGoidOnly, itemClassFilter, startDate, endDate);
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x0009ECA0 File Offset: 0x0009CEA0
		private static void QueryRelatedItems(Folder folder, GlobalObjectId globalObjectId, ICollection<PropertyDefinition> propertySet, Func<PropertyBag, bool> matchFoundAction, bool fetchResultsInReverseChronologicalOrder, bool sameGoidOnly, string[] itemClassFilter, ExDateTime? startDate, ExDateTime? endDate)
		{
			Func<PropertyBag, bool> readRow = delegate(PropertyBag propertyBag)
			{
				if (itemClassFilter != null)
				{
					string classFromPropertyBag = propertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass);
					if (!itemClassFilter.Any((string itemClass) => ObjectClass.IsOfClass(classFromPropertyBag, itemClass)))
					{
						return false;
					}
				}
				byte[] goidBytes = CalendarCorrelationMatch.GetGoidBytes(propertyBag);
				bool flag = goidBytes != null && (sameGoidOnly ? GlobalObjectId.Equals(globalObjectId, new GlobalObjectId(goidBytes)) : GlobalObjectId.CompareCleanGlobalObjectIds(globalObjectId.Bytes, goidBytes));
				if (flag)
				{
					flag = CalendarCorrelationMatch.MatchOriginalLastModifiedTime(propertyBag, startDate, endDate);
				}
				if (flag)
				{
					flag = matchFoundAction(propertyBag);
				}
				return flag;
			};
			QueryFilter queryFilter = sameGoidOnly ? new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.GlobalObjectId, globalObjectId.Bytes) : new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.CleanGlobalObjectId, globalObjectId.CleanGlobalObjectIdBytes);
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, QueryFilter>(0L, "CalendarCorrelationMatch.QueryRelatedItems: GOID={0}. QueryFilter = {1}.", globalObjectId, queryFilter);
			SortBy[] sortBy;
			if (itemClassFilter != null)
			{
				if (sameGoidOnly)
				{
					sortBy = (fetchResultsInReverseChronologicalOrder ? CalendarCorrelationMatch.goidRestrictedRelatedItemsByClassChronologicalSortOrder : CalendarCorrelationMatch.goidRestrictedRelatedItemsByClassSortOrder);
				}
				else
				{
					sortBy = (fetchResultsInReverseChronologicalOrder ? CalendarCorrelationMatch.relatedItemsByClassChronologicalSortOrder : CalendarCorrelationMatch.relatedItemsByClassSortOrder);
				}
				QueryFilter[] array = new QueryFilter[itemClassFilter.Length];
				for (int i = 0; i < itemClassFilter.Length; i++)
				{
					array[i] = new TextFilter(InternalSchema.ItemClass, itemClassFilter[i], MatchOptions.Prefix, MatchFlags.IgnoreCase);
				}
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					new OrFilter(array)
				});
			}
			else if (sameGoidOnly)
			{
				sortBy = (fetchResultsInReverseChronologicalOrder ? CalendarCorrelationMatch.goidRestrictedRelatedItemsChronologicalSortOrder : CalendarCorrelationMatch.goidRestrictedRelatedItemsSortOrder);
			}
			else
			{
				sortBy = (fetchResultsInReverseChronologicalOrder ? CalendarCorrelationMatch.relatedItemsChronologicalSortOrder : CalendarCorrelationMatch.relatedItemsSortOrder);
			}
			CalendarCorrelationMatch.QueryItemsUsingView(folder, sortBy, queryFilter, propertySet, readRow);
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x0009EE1C File Offset: 0x0009D01C
		private static void QueryItemsUsingView(Folder folder, SortBy[] sortBy, QueryFilter condition, ICollection<PropertyDefinition> propertySet, Func<PropertyBag, bool> readRow)
		{
			long num = 0L;
			using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, sortBy, propertySet))
			{
				if (condition != null)
				{
					queryResult.SeekToCondition(SeekReference.OriginBeginning, condition, (condition is ComparisonFilter) ? SeekToConditionFlags.None : SeekToConditionFlags.AllowExtendedFilters);
				}
				bool flag = false;
				while (!flag)
				{
					object[][] rows = queryResult.GetRows(50);
					flag = (rows.Length == 0);
					foreach (object[] queryResultRow in rows)
					{
						QueryResultPropertyBag queryResultPropertyBag = new QueryResultPropertyBag(folder.Session, propertySet);
						queryResultPropertyBag.SetQueryResultRow(queryResultRow);
						if (!readRow(queryResultPropertyBag) || (num += 1L) >= 1000L)
						{
							flag = true;
							break;
						}
					}
				}
			}
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x0009EED4 File Offset: 0x0009D0D4
		private static bool MatchOriginalLastModifiedTime(PropertyBag propertyBag, ExDateTime? startDate, ExDateTime? endDate)
		{
			if (startDate == null || endDate == null)
			{
				return true;
			}
			ExDateTime? valueAsNullable = propertyBag.GetValueAsNullable<ExDateTime>(InternalSchema.OriginalLastModifiedTime);
			return valueAsNullable == null || (valueAsNullable >= startDate && valueAsNullable <= endDate);
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x0009EF6C File Offset: 0x0009D16C
		private bool CheckIsCorrelated(GlobalObjectId globalObjectId, out bool isOccurrenceMatchedToMaster)
		{
			bool result = false;
			isOccurrenceMatchedToMaster = false;
			if (this.Id != null && this.Id.ObjectId.ObjectType == StoreObjectType.CalendarItem)
			{
				if (globalObjectId.Date == this.goid.Date)
				{
					result = true;
				}
				else if (!globalObjectId.IsCleanGlobalObjectId && this.goid.IsCleanGlobalObjectId && this.isRecurringMaster)
				{
					result = true;
					isOccurrenceMatchedToMaster = true;
				}
			}
			else if (this.Id != null && this.Id.ObjectId.ObjectType == StoreObjectType.CalendarItemSeries)
			{
				result = this.goid.Equals(globalObjectId);
			}
			return result;
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x0009F004 File Offset: 0x0009D204
		internal VersionedId GetCorrelatedId(GlobalObjectId globalObjectId)
		{
			VersionedId result;
			if (this.isMasterMatchingTheOccurrence)
			{
				OccurrenceStoreObjectId itemId = new OccurrenceStoreObjectId(this.Id.ObjectId.ProviderLevelItemId, globalObjectId.Date);
				result = new VersionedId(itemId, this.Id.ChangeKeyAsByteArray());
			}
			else
			{
				result = this.Id;
			}
			return result;
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x0009F054 File Offset: 0x0009D254
		public int CompareTo(CalendarCorrelationMatch other)
		{
			int num;
			if (this.Id == other.Id)
			{
				num = 0;
			}
			else
			{
				num = Nullable.Compare<int>(this.appointmentSequenceNumber, other.appointmentSequenceNumber);
				if (num == 0)
				{
					if (this.lastModifiedTime != null && other.lastModifiedTime != null)
					{
						num = this.lastModifiedTime.Value.CompareTo(other.lastModifiedTime.Value, CalendarCorrelationMatch.LastModifiedTimeTreshold);
					}
					if (num == 0)
					{
						num = Nullable.Compare<ExDateTime>(this.ownerCriticalChangeTime, other.ownerCriticalChangeTime);
						if (num == 0)
						{
							num = this.documentId.CompareTo(other.documentId);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x04001764 RID: 5988
		private const string DefaultPropertySetKey = "{651EFB55-4E21-44c3-8338-81A2502FA65D}";

		// Token: 0x04001765 RID: 5989
		private const long MaxReturnedItems = 1000L;

		// Token: 0x04001766 RID: 5990
		private static readonly TimeSpan LastModifiedTimeTreshold = TimeSpan.FromSeconds(5.0);

		// Token: 0x04001767 RID: 5991
		private static readonly SortBy[] relatedItemsSortOrder = CalendarCorrelationMatch.GetRelatedItemsSortOrder(InternalSchema.CleanGlobalObjectId);

		// Token: 0x04001768 RID: 5992
		private static readonly SortBy[] relatedItemsChronologicalSortOrder = CalendarCorrelationMatch.GetRelatedItemsChronologicalSortOrder(InternalSchema.CleanGlobalObjectId);

		// Token: 0x04001769 RID: 5993
		private static readonly SortBy[] relatedItemsByClassSortOrder = CalendarCorrelationMatch.GetRelatedItemsByClassSortOrder(InternalSchema.CleanGlobalObjectId);

		// Token: 0x0400176A RID: 5994
		private static readonly SortBy[] relatedItemsByClassChronologicalSortOrder = CalendarCorrelationMatch.GetRelatedItemsByClassChronologicalSortOrder(InternalSchema.CleanGlobalObjectId);

		// Token: 0x0400176B RID: 5995
		private static readonly SortBy[] goidRestrictedRelatedItemsSortOrder = CalendarCorrelationMatch.GetRelatedItemsSortOrder(InternalSchema.GlobalObjectId);

		// Token: 0x0400176C RID: 5996
		private static readonly SortBy[] goidRestrictedRelatedItemsChronologicalSortOrder = CalendarCorrelationMatch.GetRelatedItemsChronologicalSortOrder(InternalSchema.GlobalObjectId);

		// Token: 0x0400176D RID: 5997
		private static readonly SortBy[] goidRestrictedRelatedItemsByClassSortOrder = CalendarCorrelationMatch.GetRelatedItemsByClassSortOrder(InternalSchema.GlobalObjectId);

		// Token: 0x0400176E RID: 5998
		private static readonly SortBy[] goidRestrictedRelatedItemsByClassChronologicalSortOrder = CalendarCorrelationMatch.GetRelatedItemsByClassChronologicalSortOrder(InternalSchema.GlobalObjectId);

		// Token: 0x0400176F RID: 5999
		private readonly VersionedId Id;

		// Token: 0x04001770 RID: 6000
		private readonly PropertyBag properties;

		// Token: 0x04001771 RID: 6001
		private readonly int documentId;

		// Token: 0x04001772 RID: 6002
		private readonly GlobalObjectId goid;

		// Token: 0x04001773 RID: 6003
		private readonly bool isRecurringMaster;

		// Token: 0x04001774 RID: 6004
		private readonly int? appointmentSequenceNumber;

		// Token: 0x04001775 RID: 6005
		private readonly ExDateTime? lastModifiedTime;

		// Token: 0x04001776 RID: 6006
		private readonly ExDateTime? ownerCriticalChangeTime;

		// Token: 0x04001777 RID: 6007
		private readonly bool IsCorrelated;

		// Token: 0x04001778 RID: 6008
		private readonly bool isMasterMatchingTheOccurrence;

		// Token: 0x04001779 RID: 6009
		private static object threadSafetyLock = new object();

		// Token: 0x0400177A RID: 6010
		private static Dictionary<string, ICollection<PropertyDefinition>> requiredPropertiesCache = new Dictionary<string, ICollection<PropertyDefinition>>(1);

		// Token: 0x0400177B RID: 6011
		internal static readonly ICollection<PropertyDefinition> CorrelatedItemViewProperties = new PropertyDefinition[]
		{
			InternalSchema.ItemId,
			InternalSchema.DocumentId,
			InternalSchema.GlobalObjectId,
			InternalSchema.CleanGlobalObjectId,
			InternalSchema.AppointmentRecurrenceBlob,
			InternalSchema.AppointmentSequenceNumber,
			InternalSchema.LastModifiedTime,
			InternalSchema.OwnerCriticalChangeTime,
			InternalSchema.OriginalLastModifiedTime
		};

		// Token: 0x02000385 RID: 901
		private class CalendarCorrelationMatchCollection
		{
			// Token: 0x17000D20 RID: 3360
			// (get) Token: 0x060027C7 RID: 10183 RVA: 0x0009F20B File Offset: 0x0009D40B
			// (set) Token: 0x060027C8 RID: 10184 RVA: 0x0009F213 File Offset: 0x0009D413
			public List<CalendarCorrelationMatch> FoundMatches { get; private set; }

			// Token: 0x17000D21 RID: 3361
			// (get) Token: 0x060027C9 RID: 10185 RVA: 0x0009F21C File Offset: 0x0009D41C
			// (set) Token: 0x060027CA RID: 10186 RVA: 0x0009F224 File Offset: 0x0009D424
			public bool? IsMasterMatch { get; private set; }

			// Token: 0x060027CB RID: 10187 RVA: 0x0009F22D File Offset: 0x0009D42D
			public CalendarCorrelationMatchCollection()
			{
				this.FoundMatches = new List<CalendarCorrelationMatch>();
			}

			// Token: 0x060027CC RID: 10188 RVA: 0x0009F240 File Offset: 0x0009D440
			public void AddMatch(GlobalObjectId globalObjectId, PropertyBag propertyBag)
			{
				CalendarCorrelationMatch matchData = new CalendarCorrelationMatch(propertyBag, globalObjectId);
				this.UpdateMatchCollection(matchData);
			}

			// Token: 0x060027CD RID: 10189 RVA: 0x0009F25C File Offset: 0x0009D45C
			private void UpdateMatchCollection(CalendarCorrelationMatch matchData)
			{
				if (matchData.IsCorrelated)
				{
					bool flag = false;
					if (this.FoundMatches.Count == 0)
					{
						flag = true;
					}
					else
					{
						if (this.IsMasterMatch == null)
						{
							throw new ArgumentNullException("isPreviousMasterMatchingTheOccurrence");
						}
						if (this.IsMasterMatch.Value == matchData.isMasterMatchingTheOccurrence)
						{
							flag = true;
						}
						else if (this.IsMasterMatch.Value)
						{
							flag = true;
							this.FoundMatches.Clear();
						}
					}
					if (flag)
					{
						this.FoundMatches.Add(matchData);
						this.IsMasterMatch = new bool?(matchData.isMasterMatchingTheOccurrence);
					}
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x0200001C RID: 28
	internal abstract class RefinerDataProvider
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000154 RID: 340
		internal abstract string Name { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000072D2 File Offset: 0x000054D2
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000072DA File Offset: 0x000054DA
		internal Guid CorrelationId { get; private set; }

		// Token: 0x06000157 RID: 343 RVA: 0x000072E3 File Offset: 0x000054E3
		internal static RefinerDataProvider Create(IStorePropertyBag[] results)
		{
			return new RefinerDataProvider.ResultBasedProvider(results);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000072EB File Offset: 0x000054EB
		internal static RefinerDataProvider Create(ISearchServiceConfig config, PagingImsFlowExecutor flowExecutor, Guid mdbGuid, Guid mbxGuid, string originalQuery, RefinementFilter refinementFilter = null)
		{
			return new RefinerDataProvider.FastProvider(config, flowExecutor, mdbGuid, mbxGuid, originalQuery, refinementFilter);
		}

		// Token: 0x06000159 RID: 345
		internal abstract IReadOnlyCollection<RefinerData> GetRefiners(IReadOnlyCollection<PropertyDefinition> requestedRefiners, int maxResults);

		// Token: 0x0200001D RID: 29
		private sealed class ResultBasedProvider : RefinerDataProvider
		{
			// Token: 0x0600015B RID: 347 RVA: 0x00007302 File Offset: 0x00005502
			internal ResultBasedProvider(IStorePropertyBag[] results)
			{
				InstantSearch.ThrowOnNullArgument(results, "results");
				this.results = results;
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x0600015C RID: 348 RVA: 0x0000731C File Offset: 0x0000551C
			internal override string Name
			{
				get
				{
					return "ResultBasedProvider";
				}
			}

			// Token: 0x0600015D RID: 349 RVA: 0x00007340 File Offset: 0x00005540
			internal override IReadOnlyCollection<RefinerData> GetRefiners(IReadOnlyCollection<PropertyDefinition> requestedRefiners, int maxResults)
			{
				base.CorrelationId = Guid.NewGuid();
				if (this.results.Length == 0)
				{
					return RefinerDataProvider.ResultBasedProvider.EmptyRefinerData;
				}
				RefinerDataProvider.RefinerDataCollection refinerDataCollection = new RefinerDataProvider.RefinerDataCollection(requestedRefiners.Count);
				foreach (PropertyDefinition propertyDefinition in requestedRefiners)
				{
					if (!InstantSearchSchema.RefinablePropertiesMap.ContainsKey(propertyDefinition) && !InstantSearchSchema.RefinablePropertiesMap.Values.Contains(propertyDefinition))
					{
						ExAssert.RetailAssert(false, "Unsupported refiner: {0}", new object[]
						{
							propertyDefinition
						});
					}
					RefinerData refinerData = null;
					if (propertyDefinition == ItemSchema.From)
					{
						refinerData = this.CalculatePeopleRefiner(propertyDefinition, "from");
					}
					else if (propertyDefinition == ConversationItemSchema.ConversationMVFrom)
					{
						refinerData = this.CalculateUniqueRefiner<string>(propertyDefinition, (string x) => "(" + x + ")", "from");
					}
					else if (propertyDefinition == ItemSchema.HasAttachment || propertyDefinition == ConversationItemSchema.ConversationHasAttach)
					{
						refinerData = this.CalculateUniqueRefiner<bool>(propertyDefinition, (bool x) => x.ToString(), "hasattachment");
					}
					if (refinerData != null && refinerData.Entries.Count > 0)
					{
						refinerDataCollection.Add(refinerData);
					}
				}
				return refinerDataCollection;
			}

			// Token: 0x0600015E RID: 350 RVA: 0x00007488 File Offset: 0x00005688
			private static int CompareRefinerDataEntry(RefinerDataEntry x, RefinerDataEntry y)
			{
				return (int)(y.HitCount - x.HitCount);
			}

			// Token: 0x0600015F RID: 351 RVA: 0x00007498 File Offset: 0x00005698
			private RefinerData CalculatePeopleRefiner(PropertyDefinition prop, string keyword)
			{
				Dictionary<string, long> dictionary = new Dictionary<string, long>(this.results.Length);
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>(this.results.Length);
				foreach (IStorePropertyBag storePropertyBag in this.results)
				{
					Participant participant = storePropertyBag.TryGetProperty(prop) as Participant;
					if (participant != null)
					{
						long num;
						if (dictionary.TryGetValue(participant.EmailAddress, out num))
						{
							dictionary[participant.EmailAddress] = num + 1L;
						}
						else
						{
							dictionary.Add(participant.EmailAddress, 1L);
							dictionary2.Add(participant.EmailAddress, participant.DisplayName);
						}
					}
				}
				List<RefinerDataEntry> list = new List<RefinerDataEntry>(dictionary.Count);
				foreach (KeyValuePair<string, long> keyValuePair in dictionary)
				{
					string key = keyValuePair.Key;
					long value = keyValuePair.Value;
					list.Add(new RefinerDataEntry(key + "|" + dictionary2[key], value, keyword + ":" + key));
				}
				list.Sort(new Comparison<RefinerDataEntry>(RefinerDataProvider.ResultBasedProvider.CompareRefinerDataEntry));
				return new RefinerData(prop, list);
			}

			// Token: 0x06000160 RID: 352 RVA: 0x000075E0 File Offset: 0x000057E0
			private RefinerData CalculateUniqueRefiner<T>(PropertyDefinition prop, Func<T, string> converter, string keyword)
			{
				Dictionary<string, long> dictionary = new Dictionary<string, long>(this.results.Length);
				foreach (IStorePropertyBag storePropertyBag in this.results)
				{
					object obj = storePropertyBag.TryGetProperty(prop);
					if (obj != null && !PropertyError.IsPropertyError(obj))
					{
						IEnumerable<T> enumerable = obj as IEnumerable<T>;
						if (enumerable == null)
						{
							enumerable = new List<T>(1)
							{
								(T)((object)obj)
							};
						}
						foreach (T arg in enumerable)
						{
							string key = converter(arg);
							long num;
							if (dictionary.TryGetValue(key, out num))
							{
								dictionary[key] = num + 1L;
							}
							else
							{
								dictionary.Add(key, 1L);
							}
						}
					}
				}
				List<RefinerDataEntry> list = new List<RefinerDataEntry>(dictionary.Count);
				foreach (KeyValuePair<string, long> keyValuePair in dictionary)
				{
					string key2 = keyValuePair.Key;
					long value = keyValuePair.Value;
					list.Add(new RefinerDataEntry(key2, value, keyword + ":" + key2));
				}
				list.Sort(new Comparison<RefinerDataEntry>(RefinerDataProvider.ResultBasedProvider.CompareRefinerDataEntry));
				return new RefinerData(prop, list);
			}

			// Token: 0x040000C3 RID: 195
			private const int DefaultBuckets = 4;

			// Token: 0x040000C4 RID: 196
			private static readonly RefinerData[] EmptyRefinerData = new RefinerData[0];

			// Token: 0x040000C5 RID: 197
			private readonly IStorePropertyBag[] results;
		}

		// Token: 0x0200001E RID: 30
		private sealed class FastProvider : RefinerDataProvider
		{
			// Token: 0x06000164 RID: 356 RVA: 0x00007764 File Offset: 0x00005964
			internal FastProvider(ISearchServiceConfig config, PagingImsFlowExecutor flowExecutor, Guid mdbGuid, Guid mbxGuid, string originalQuery, RefinementFilter refinementFilter = null)
			{
				InstantSearch.ThrowOnNullArgument(flowExecutor, "flowExecutor");
				InstantSearch.ThrowOnNullOrEmptyArgument(originalQuery, "originalQuery");
				this.config = config;
				this.flowExecutor = flowExecutor;
				this.flowName = FlowDescriptor.GetImsFlowDescriptor(config, FastIndexVersion.GetIndexSystemName(mdbGuid)).DisplayName;
				this.mailboxGuid = mbxGuid;
				this.originalQuery = originalQuery;
				if (refinementFilter != null)
				{
					this.filters = refinementFilter.Filters;
				}
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x06000165 RID: 357 RVA: 0x000077D4 File Offset: 0x000059D4
			internal override string Name
			{
				get
				{
					return "FastProvider";
				}
			}

			// Token: 0x06000166 RID: 358 RVA: 0x000077DC File Offset: 0x000059DC
			internal override IReadOnlyCollection<RefinerData> GetRefiners(IReadOnlyCollection<PropertyDefinition> requestedRefiners, int maxResults)
			{
				base.CorrelationId = Guid.NewGuid();
				AdditionalParameters additionalParameters = new AdditionalParameters
				{
					Refiners = RefinerDataProvider.FastProvider.ConvertToFastRefiners(requestedRefiners, maxResults),
					RefinementFilters = this.filters
				};
				if (this.config.UseExecuteAndReadPage)
				{
					QueryParameters queryParameters = new QueryParameters(this.flowExecutor.GetLookupTimeout(), this.flowName, this.originalQuery, this.mailboxGuid, Guid.NewGuid(), additionalParameters);
					IReadOnlyCollection<RefinerResult> refinerResults = this.flowExecutor.ReadRefiners(queryParameters);
					return RefinerDataProvider.FastProvider.ConvertRefinerResults(refinerResults);
				}
				IReadOnlyCollection<RefinerData> result;
				using (PagingImsFlowExecutor.QueryExecutionContext queryExecutionContext = this.flowExecutor.ExecuteRefinerQuery(this.flowName, this.mailboxGuid, base.CorrelationId, this.originalQuery, CultureInfo.InvariantCulture, additionalParameters))
				{
					IReadOnlyCollection<RefinerResult> refinerResults2 = this.flowExecutor.ReadRefiners(queryExecutionContext);
					result = RefinerDataProvider.FastProvider.ConvertRefinerResults(refinerResults2);
				}
				return result;
			}

			// Token: 0x06000167 RID: 359 RVA: 0x000078C4 File Offset: 0x00005AC4
			private static RefinerDataProvider.RefinerDataCollection ConvertRefinerResults(IReadOnlyCollection<RefinerResult> refinerResults)
			{
				RefinerDataProvider.RefinerDataCollection refinerDataCollection = new RefinerDataProvider.RefinerDataCollection(refinerResults.Count);
				foreach (RefinerResult refinerResult in refinerResults)
				{
					PropertyDefinition propertyDefinition = RefinerDataProvider.FastProvider.ConvertToProperty(refinerResult.Name);
					if (propertyDefinition != null)
					{
						List<RefinerDataEntry> list = new List<RefinerDataEntry>(refinerResult.Entries.Count);
						foreach (RefinerEntry refinerEntry in refinerResult.Entries)
						{
							list.Add(new RefinerDataEntry(refinerEntry.Name, refinerEntry.Count, refinerEntry.Filter));
						}
						refinerDataCollection.Add(new RefinerData(propertyDefinition, list));
					}
				}
				return refinerDataCollection;
			}

			// Token: 0x06000168 RID: 360 RVA: 0x000079A8 File Offset: 0x00005BA8
			private static List<string> ConvertToFastRefiners(IReadOnlyCollection<PropertyDefinition> requestedRefiners, int maxResults)
			{
				List<string> list = new List<string>(requestedRefiners.Count);
				foreach (PropertyDefinition propertyDefinition in requestedRefiners)
				{
					string text;
					if (!InstantSearchSchema.PropertyToRefinersMap.TryGetValue(propertyDefinition, out text))
					{
						ExAssert.RetailAssert(false, "Unsupported refiner: {0}", new object[]
						{
							propertyDefinition
						});
					}
					if (maxResults > 0)
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							"(filter=",
							maxResults,
							")"
						});
					}
					list.Add(text);
				}
				return list;
			}

			// Token: 0x06000169 RID: 361 RVA: 0x00007A60 File Offset: 0x00005C60
			private static PropertyDefinition ConvertToProperty(string fastRefiner)
			{
				foreach (KeyValuePair<PropertyDefinition, string> keyValuePair in InstantSearchSchema.PropertyToRefinersMap)
				{
					if (StringComparer.OrdinalIgnoreCase.Equals(fastRefiner, keyValuePair.Value))
					{
						return keyValuePair.Key;
					}
				}
				ExAssert.RetailAssert(false, "Unknown refiner: {0}", new object[]
				{
					fastRefiner
				});
				return null;
			}

			// Token: 0x040000C8 RID: 200
			private readonly ISearchServiceConfig config;

			// Token: 0x040000C9 RID: 201
			private readonly PagingImsFlowExecutor flowExecutor;

			// Token: 0x040000CA RID: 202
			private readonly string flowName;

			// Token: 0x040000CB RID: 203
			private readonly Guid mailboxGuid;

			// Token: 0x040000CC RID: 204
			private readonly string originalQuery;

			// Token: 0x040000CD RID: 205
			private readonly IReadOnlyCollection<string> filters;
		}

		// Token: 0x0200001F RID: 31
		private class RefinerDataCollection : List<RefinerData>
		{
			// Token: 0x0600016A RID: 362 RVA: 0x00007AE0 File Offset: 0x00005CE0
			public RefinerDataCollection(int capacity) : base(capacity)
			{
			}

			// Token: 0x0600016B RID: 363 RVA: 0x00007AEC File Offset: 0x00005CEC
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(25 * base.Count);
				foreach (RefinerData value in this)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(';');
					}
					stringBuilder.Append(value);
				}
				return stringBuilder.ToString();
			}
		}
	}
}

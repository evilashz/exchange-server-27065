using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000834 RID: 2100
	internal static class DlpUtils
	{
		// Token: 0x060048DB RID: 18651 RVA: 0x0012B3A8 File Offset: 0x001295A8
		internal static IEnumerable<Tuple<TransportRule, XDocument>> AggregateOobAndCustomClassificationDefinitions(OrganizationId organizationId, IConfigurationSession openedDataSession = null, Func<TransportRule, bool> inclusiveFilter = null, QueryFilter additionalFilter = null, IClassificationDefinitionsDataReader dataReader = null, IClassificationDefinitionsDiagnosticsReporter classificationDefinitionsDiagnosticsReporter = null)
		{
			IClassificationDefinitionsDataReader dataReaderToUse = dataReader ?? ClassificationDefinitionsDataReader.DefaultInstance;
			IClassificationDefinitionsDiagnosticsReporter classificationDefinitionsDiagnosticsReporterToUse = classificationDefinitionsDiagnosticsReporter ?? ClassificationDefinitionsDiagnosticsReporter.Instance;
			foreach (TransportRule transportRule in dataReaderToUse.GetAllClassificationRuleCollection(organizationId, openedDataSession, additionalFilter))
			{
				if (inclusiveFilter != null)
				{
					if (!inclusiveFilter(transportRule))
					{
						continue;
					}
				}
				XDocument rulePackXDoc;
				try
				{
					rulePackXDoc = ClassificationDefinitionUtils.GetRuleCollectionDocumentFromTransportRule(transportRule);
				}
				catch (InvalidOperationException)
				{
					classificationDefinitionsDiagnosticsReporterToUse.WriteInvalidObjectInformation(0, organizationId, transportRule.DistinguishedName);
					continue;
				}
				catch (ArgumentException underlyingException)
				{
					classificationDefinitionsDiagnosticsReporterToUse.WriteCorruptRulePackageDiagnosticsInformation(0, organizationId, transportRule.DistinguishedName, underlyingException);
					continue;
				}
				catch (AggregateException ex)
				{
					classificationDefinitionsDiagnosticsReporterToUse.WriteCorruptRulePackageDiagnosticsInformation(0, organizationId, transportRule.DistinguishedName, ex.Flatten());
					continue;
				}
				catch (XmlException ex2)
				{
					classificationDefinitionsDiagnosticsReporterToUse.WriteCorruptRulePackageDiagnosticsInformation(0, organizationId, transportRule.DistinguishedName, new AggregateException(new Exception[]
					{
						ex2
					}).Flatten());
					continue;
				}
				yield return new Tuple<TransportRule, XDocument>(transportRule, rulePackXDoc);
			}
			yield break;
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x0012B3EC File Offset: 0x001295EC
		internal static Dictionary<string, HashSet<string>> GetAllClassificationIdentifiers(OrganizationId organizationId, IConfigurationSession openedDataSession = null, Func<TransportRule, bool> inclusiveFilter = null, QueryFilter additionalFilter = null, IClassificationDefinitionsDataReader dataReader = null, IClassificationDefinitionsDiagnosticsReporter classificationDefinitionsDiagnosticsReporter = null)
		{
			IClassificationDefinitionsDiagnosticsReporter classificationDefinitionsDiagnosticsReporter2 = classificationDefinitionsDiagnosticsReporter ?? ClassificationDefinitionsDiagnosticsReporter.Instance;
			Dictionary<string, HashSet<string>> dictionary = new Dictionary<string, HashSet<string>>(ClassificationDefinitionConstants.RuleCollectionIdComparer);
			foreach (Tuple<TransportRule, XDocument> tuple in DlpUtils.AggregateOobAndCustomClassificationDefinitions(organizationId, openedDataSession, inclusiveFilter, additionalFilter, dataReader, classificationDefinitionsDiagnosticsReporter2))
			{
				TransportRule item = tuple.Item1;
				XDocument item2 = tuple.Item2;
				ExAssert.RetailAssert(item2 != null, "XDocument instance returned from AggregateOobAndCustomClassificationDefinitions should never be null!");
				string rulePackId;
				HashSet<string> value;
				try
				{
					rulePackId = XmlProcessingUtils.GetRulePackId(item2);
					value = new HashSet<string>(XmlProcessingUtils.GetAllRuleIds(item2), ClassificationDefinitionConstants.RuleIdComparer);
				}
				catch (XmlException ex)
				{
					ClassificationDefinitionsDiagnosticsReporter.Instance.WriteCorruptRulePackageDiagnosticsInformation(0, organizationId, item.DistinguishedName, new AggregateException(new Exception[]
					{
						ex
					}).Flatten());
					continue;
				}
				ExAssert.RetailAssert(!dictionary.ContainsKey(rulePackId), "Duplicate classification rule collection ID '{0}' under tenant's organization '{1}' is a should never ever happen case", new object[]
				{
					rulePackId,
					organizationId.ToString()
				});
				dictionary.Add(rulePackId, value);
			}
			return dictionary;
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x0012B554 File Offset: 0x00129754
		internal static IEnumerable<DataClassificationPresentationObject> QueryDataClassification(IEnumerable<string> queriedIdentities, OrganizationId organizationId, IConfigurationSession openedDataSession = null, IClassificationDefinitionsDataReader dataReader = null, IClassificationDefinitionsDiagnosticsReporter diagnosticsReporter = null)
		{
			ArgumentValidator.ThrowIfNull("queriedIdentities", queriedIdentities);
			if (object.ReferenceEquals(null, organizationId))
			{
				throw new ArgumentNullException("organizationId");
			}
			List<string> list = new List<string>();
			Dictionary<string, DataClassificationPresentationObject> queriedNameDictionary = new Dictionary<string, DataClassificationPresentationObject>(StringComparer.Ordinal);
			Dictionary<string, DataClassificationPresentationObject> queriedGuidDictionary = new Dictionary<string, DataClassificationPresentationObject>(ClassificationDefinitionConstants.RuleIdComparer);
			Dictionary<string, DataClassificationPresentationObject> allQueryResultsDictionary = new Dictionary<string, DataClassificationPresentationObject>(ClassificationDefinitionConstants.RuleIdComparer);
			foreach (string text in queriedIdentities)
			{
				if (!string.IsNullOrEmpty(text))
				{
					list.Add(text);
					Guid guid;
					if (GuidHelper.TryParseGuid(text, out guid))
					{
						queriedGuidDictionary.Add(text, null);
					}
					else
					{
						queriedNameDictionary.Add(text, null);
					}
				}
			}
			if (!list.Any<string>())
			{
				return Enumerable.Empty<DataClassificationPresentationObject>();
			}
			DlpUtils.DataClassificationQueryContext dataClassificationQueryContext = new DlpUtils.DataClassificationQueryContext(organizationId, diagnosticsReporter ?? ClassificationDefinitionsDiagnosticsReporter.Instance);
			bool flag = queriedNameDictionary.Any<KeyValuePair<string, DataClassificationPresentationObject>>();
			bool flag2 = queriedGuidDictionary.Any<KeyValuePair<string, DataClassificationPresentationObject>>();
			foreach (Tuple<TransportRule, XDocument> tuple in DlpUtils.AggregateOobAndCustomClassificationDefinitions(organizationId, openedDataSession, null, null, dataReader, dataClassificationQueryContext.CurrentDiagnosticsReporter))
			{
				dataClassificationQueryContext.CurrentRuleCollectionTransportRuleObject = tuple.Item1;
				dataClassificationQueryContext.CurrentRuleCollectionXDoc = tuple.Item2;
				IEnumerable<QueryMatchResult> nameMatchResultsFromCurrentRulePack = Enumerable.Empty<QueryMatchResult>();
				if (!flag || DlpUtils.GetNameQueryMatchResult(dataClassificationQueryContext, queriedNameDictionary.Keys, out nameMatchResultsFromCurrentRulePack))
				{
					IEnumerable<QueryMatchResult> idMatchResultsFromCurrentRulePack = Enumerable.Empty<QueryMatchResult>();
					if (!flag2 || DlpUtils.GetIdQueryMatchResult(dataClassificationQueryContext, queriedGuidDictionary.Keys, out idMatchResultsFromCurrentRulePack))
					{
						DlpUtils.PopulateMatchResults(dataClassificationQueryContext, queriedNameDictionary, nameMatchResultsFromCurrentRulePack, queriedGuidDictionary, idMatchResultsFromCurrentRulePack, allQueryResultsDictionary);
					}
				}
			}
			return (from presentationObject in list.Select(delegate(string queriedIdentity)
			{
				DataClassificationPresentationObject result;
				if (queriedNameDictionary.TryGetValue(queriedIdentity, out result))
				{
					return result;
				}
				if (!queriedGuidDictionary.TryGetValue(queriedIdentity, out result))
				{
					return null;
				}
				return result;
			})
			where presentationObject != null
			select presentationObject).ToList<DataClassificationPresentationObject>();
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x0012B768 File Offset: 0x00129968
		internal static bool TryExecuteOperation<TExceptionToHandle, TArg1, TResult>(DlpUtils.DataClassificationQueryContext operationContext, Func<TArg1, TResult> protectedOperation, TArg1 operationArgument1, out TResult operationResult) where TExceptionToHandle : Exception
		{
			operationResult = default(TResult);
			try
			{
				operationResult = protectedOperation(operationArgument1);
			}
			catch (TExceptionToHandle texceptionToHandle)
			{
				TExceptionToHandle texceptionToHandle2 = (TExceptionToHandle)((object)texceptionToHandle);
				operationContext.CurrentDiagnosticsReporter.WriteCorruptRulePackageDiagnosticsInformation(0, operationContext.CurrentOrganizationId, operationContext.CurrentRuleCollectionTransportRuleObject.DistinguishedName, new AggregateException(new Exception[]
				{
					texceptionToHandle2
				}).Flatten());
				return false;
			}
			return true;
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x0012B7E0 File Offset: 0x001299E0
		internal static bool TryExecuteOperation<TExceptionToHandle, TArg1, TArg2, TResult>(DlpUtils.DataClassificationQueryContext operationContext, Func<TArg1, TArg2, TResult> protectedOperation, TArg1 operationArgument1, TArg2 operationArgument2, out TResult operationResult) where TExceptionToHandle : Exception
		{
			operationResult = default(TResult);
			try
			{
				operationResult = protectedOperation(operationArgument1, operationArgument2);
			}
			catch (TExceptionToHandle texceptionToHandle)
			{
				TExceptionToHandle texceptionToHandle2 = (TExceptionToHandle)((object)texceptionToHandle);
				operationContext.CurrentDiagnosticsReporter.WriteCorruptRulePackageDiagnosticsInformation(0, operationContext.CurrentOrganizationId, operationContext.CurrentRuleCollectionTransportRuleObject.DistinguishedName, new AggregateException(new Exception[]
				{
					texceptionToHandle2
				}).Flatten());
				return false;
			}
			return true;
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x0012B867 File Offset: 0x00129A67
		private static bool GetNameQueryMatchResult(DlpUtils.DataClassificationQueryContext queryContext, IEnumerable<string> queriedNames, out IEnumerable<QueryMatchResult> nameMatchResultsFromCurrentRulePack)
		{
			return DlpUtils.TryExecuteOperation<XmlException, XDocument, IEnumerable<string>, IEnumerable<QueryMatchResult>>(queryContext, (XDocument rulePackXDoc, IEnumerable<string> queriedRuleNames) => XmlProcessingUtils.GetMatchingRulesByName(rulePackXDoc, queriedRuleNames, NameMatchingOptions.InvariantNameOrLocalizedNameMatch, true), queryContext.CurrentRuleCollectionXDoc, queriedNames, out nameMatchResultsFromCurrentRulePack);
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x0012B894 File Offset: 0x00129A94
		private static bool GetIdQueryMatchResult(DlpUtils.DataClassificationQueryContext queryContext, IEnumerable<string> queriedIds, out IEnumerable<QueryMatchResult> idMatchResultsFromCurrentRulePack)
		{
			return DlpUtils.TryExecuteOperation<XmlException, XDocument, IEnumerable<string>, IEnumerable<QueryMatchResult>>(queryContext, new Func<XDocument, IEnumerable<string>, IEnumerable<QueryMatchResult>>(XmlProcessingUtils.GetMatchingRulesById), queryContext.CurrentRuleCollectionXDoc, queriedIds, out idMatchResultsFromCurrentRulePack);
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x0012B8B0 File Offset: 0x00129AB0
		private static bool CreateClassificationRuleCollectionPresentationObject(DlpUtils.DataClassificationQueryContext operationContext, out ClassificationRuleCollectionPresentationObject presentationObject)
		{
			return DlpUtils.TryExecuteOperation<XmlException, TransportRule, XDocument, ClassificationRuleCollectionPresentationObject>(operationContext, new Func<TransportRule, XDocument, ClassificationRuleCollectionPresentationObject>(ClassificationRuleCollectionPresentationObject.Create), operationContext.CurrentRuleCollectionTransportRuleObject, operationContext.CurrentRuleCollectionXDoc, out presentationObject);
		}

		// Token: 0x060048E3 RID: 18659 RVA: 0x0012B8F8 File Offset: 0x00129AF8
		private static bool CreateDataClassificationPresentationObject(DlpUtils.DataClassificationQueryContext operationContext, string ruleIdentifier, XElement ruleElement, XElement resourceElement, ClassificationRuleCollectionPresentationObject ruleCollectionPresentationObject, out DataClassificationPresentationObject dataClassificationPresentationObject)
		{
			return DlpUtils.TryExecuteOperation<XmlException, object, object, DataClassificationPresentationObject>(operationContext, (object unusedStub1, object unusedStub2) => DataClassificationPresentationObject.Create(ruleIdentifier, ruleElement, resourceElement, ruleCollectionPresentationObject), null, null, out dataClassificationPresentationObject);
		}

		// Token: 0x060048E4 RID: 18660 RVA: 0x0012B940 File Offset: 0x00129B40
		private static bool CreateDataClassificationPresentationObjects(DlpUtils.DataClassificationQueryContext queryContext, ClassificationRuleCollectionPresentationObject ruleCollectionPresentationObject, IEnumerable<QueryMatchResult> queryMatchResults, out List<Tuple<QueryMatchResult, DataClassificationPresentationObject>> presentationObjects)
		{
			presentationObjects = null;
			List<Tuple<QueryMatchResult, DataClassificationPresentationObject>> list = new List<Tuple<QueryMatchResult, DataClassificationPresentationObject>>();
			foreach (QueryMatchResult queryMatchResult in queryMatchResults)
			{
				DataClassificationPresentationObject item;
				if (!DlpUtils.CreateDataClassificationPresentationObject(queryContext, queryMatchResult.MatchingRuleId, queryMatchResult.MatchingRuleXElement, queryMatchResult.MatchingResourceXElement, ruleCollectionPresentationObject, out item))
				{
					return false;
				}
				list.Add(new Tuple<QueryMatchResult, DataClassificationPresentationObject>(queryMatchResult, item));
			}
			presentationObjects = list;
			return true;
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x0012B9C4 File Offset: 0x00129BC4
		private static void EnsureResultsIntegrity(DlpUtils.DataClassificationQueryContext queryContext, QueryMatchResult queryMatchResult, Dictionary<string, DataClassificationPresentationObject> allQueryResultsDictionary, ClassificationRuleCollectionPresentationObject currentRulePackagePresentationObject)
		{
			DataClassificationPresentationObject dataClassificationPresentationObject;
			if (!allQueryResultsDictionary.TryGetValue(queryMatchResult.MatchingRuleId, out dataClassificationPresentationObject))
			{
				return;
			}
			if (ClassificationDefinitionConstants.RuleCollectionIdComparer.Equals(dataClassificationPresentationObject.ClassificationRuleCollection.Name, currentRulePackagePresentationObject.Name))
			{
				throw new ArgumentException(Strings.DataClassificationNonUniqueQuery(dataClassificationPresentationObject.LocalizedName, dataClassificationPresentationObject.Identity.ToString(), dataClassificationPresentationObject.ClassificationRuleCollection.Name));
			}
			queryContext.CurrentDiagnosticsReporter.WriteDuplicateRuleIdAcrossRulePacksDiagnosticsInformation(0, queryContext.CurrentOrganizationId, dataClassificationPresentationObject.ClassificationRuleCollection.DistinguishedName, queryContext.CurrentRuleCollectionTransportRuleObject.DistinguishedName, queryMatchResult.MatchingRuleId);
			throw new ArgumentException(Strings.DataClassificationAmbiguousIdentity(queryMatchResult.QueryString, currentRulePackagePresentationObject.Name, dataClassificationPresentationObject.LocalizedName, dataClassificationPresentationObject.Identity.ToString(), dataClassificationPresentationObject.ClassificationRuleCollection.Name));
		}

		// Token: 0x060048E6 RID: 18662 RVA: 0x0012BA94 File Offset: 0x00129C94
		private static bool PopulateMatchResults(DlpUtils.DataClassificationQueryContext queryContext, Dictionary<string, DataClassificationPresentationObject> queriedNameDictionary, IEnumerable<QueryMatchResult> nameMatchResultsFromCurrentRulePack, Dictionary<string, DataClassificationPresentationObject> queriedGuidDictionary, IEnumerable<QueryMatchResult> idMatchResultsFromCurrentRulePack, Dictionary<string, DataClassificationPresentationObject> allQueryResultsDictionary)
		{
			ClassificationRuleCollectionPresentationObject classificationRuleCollectionPresentationObject;
			if (!DlpUtils.CreateClassificationRuleCollectionPresentationObject(queryContext, out classificationRuleCollectionPresentationObject))
			{
				return false;
			}
			List<Tuple<QueryMatchResult, DataClassificationPresentationObject>> list;
			if (!DlpUtils.CreateDataClassificationPresentationObjects(queryContext, classificationRuleCollectionPresentationObject, nameMatchResultsFromCurrentRulePack, out list))
			{
				return false;
			}
			List<Tuple<QueryMatchResult, DataClassificationPresentationObject>> list2;
			if (!DlpUtils.CreateDataClassificationPresentationObjects(queryContext, classificationRuleCollectionPresentationObject, idMatchResultsFromCurrentRulePack, out list2))
			{
				return false;
			}
			foreach (Tuple<QueryMatchResult, DataClassificationPresentationObject> tuple in list)
			{
				QueryMatchResult item = tuple.Item1;
				DataClassificationPresentationObject item2 = tuple.Item2;
				if (queriedNameDictionary[item.QueryString] != null)
				{
					throw new ArgumentException(Strings.DataClassificationAmbiguousName(item.QueryString));
				}
				DlpUtils.EnsureResultsIntegrity(queryContext, item, allQueryResultsDictionary, classificationRuleCollectionPresentationObject);
				queriedNameDictionary[item.QueryString] = item2;
				allQueryResultsDictionary.Add(item.MatchingRuleId, item2);
			}
			foreach (Tuple<QueryMatchResult, DataClassificationPresentationObject> tuple2 in list2)
			{
				QueryMatchResult item3 = tuple2.Item1;
				DataClassificationPresentationObject item4 = tuple2.Item2;
				DataClassificationPresentationObject dataClassificationPresentationObject = queriedGuidDictionary[item3.QueryString];
				if (dataClassificationPresentationObject != null)
				{
					queryContext.CurrentDiagnosticsReporter.WriteDuplicateRuleIdAcrossRulePacksDiagnosticsInformation(0, queryContext.CurrentOrganizationId, dataClassificationPresentationObject.ClassificationRuleCollection.DistinguishedName, queryContext.CurrentRuleCollectionTransportRuleObject.DistinguishedName, item3.MatchingRuleId);
					throw new ArgumentException(Strings.DataClassificationAmbiguousIdentifier(item3.QueryString));
				}
				DlpUtils.EnsureResultsIntegrity(queryContext, item3, allQueryResultsDictionary, classificationRuleCollectionPresentationObject);
				queriedGuidDictionary[item3.QueryString] = item4;
				allQueryResultsDictionary.Add(item3.MatchingRuleId, item4);
			}
			return true;
		}

		// Token: 0x02000835 RID: 2101
		internal class DataClassificationQueryContext
		{
			// Token: 0x170015FC RID: 5628
			// (get) Token: 0x060048E9 RID: 18665 RVA: 0x0012BC3C File Offset: 0x00129E3C
			// (set) Token: 0x060048EA RID: 18666 RVA: 0x0012BC44 File Offset: 0x00129E44
			internal OrganizationId CurrentOrganizationId { get; private set; }

			// Token: 0x170015FD RID: 5629
			// (get) Token: 0x060048EB RID: 18667 RVA: 0x0012BC4D File Offset: 0x00129E4D
			// (set) Token: 0x060048EC RID: 18668 RVA: 0x0012BC55 File Offset: 0x00129E55
			internal TransportRule CurrentRuleCollectionTransportRuleObject { get; set; }

			// Token: 0x170015FE RID: 5630
			// (get) Token: 0x060048ED RID: 18669 RVA: 0x0012BC5E File Offset: 0x00129E5E
			// (set) Token: 0x060048EE RID: 18670 RVA: 0x0012BC66 File Offset: 0x00129E66
			internal XDocument CurrentRuleCollectionXDoc { get; set; }

			// Token: 0x170015FF RID: 5631
			// (get) Token: 0x060048EF RID: 18671 RVA: 0x0012BC6F File Offset: 0x00129E6F
			// (set) Token: 0x060048F0 RID: 18672 RVA: 0x0012BC77 File Offset: 0x00129E77
			internal IClassificationDefinitionsDiagnosticsReporter CurrentDiagnosticsReporter { get; private set; }

			// Token: 0x060048F1 RID: 18673 RVA: 0x0012BC80 File Offset: 0x00129E80
			internal DataClassificationQueryContext(OrganizationId currentOrganizationIdQueryContext, IClassificationDefinitionsDiagnosticsReporter diagnosticsReporterToUse)
			{
				this.CurrentOrganizationId = currentOrganizationIdQueryContext;
				this.CurrentDiagnosticsReporter = diagnosticsReporterToUse;
			}
		}
	}
}

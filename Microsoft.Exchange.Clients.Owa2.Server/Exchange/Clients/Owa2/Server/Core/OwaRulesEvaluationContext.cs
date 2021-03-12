using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Filtering;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000229 RID: 553
	internal class OwaRulesEvaluationContext : BaseTransportRulesEvaluationContext
	{
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x0004A542 File Offset: 0x00048742
		internal string RuleEvalLatency
		{
			get
			{
				return this.ruleEvalLatency.ToString();
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0004A54F File Offset: 0x0004874F
		internal string RuleEvalLResult
		{
			get
			{
				return this.ruleEvalResult.ToString();
			}
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0004A560 File Offset: 0x00048760
		public OwaRulesEvaluationContext(RuleCollection rules, ScanResultStorageProvider scanResultStorageProvider, Item item, string fromAddress, ShortList<string> recipients, PolicyTipRequestLogger policyTipRequestLogger) : base(rules, new OwaRulesTracer())
		{
			if (rules == null)
			{
				throw new ArgumentNullException("rules");
			}
			if (scanResultStorageProvider == null)
			{
				throw new ArgumentNullException("scanResultStorageProvider");
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (fromAddress == null)
			{
				throw new ArgumentNullException("fromAddress");
			}
			if (policyTipRequestLogger == null)
			{
				throw new ArgumentNullException("policyTipRequestLogger");
			}
			this.Item = item;
			this.ScanResultStorageProvider = scanResultStorageProvider;
			this.FromAddress = fromAddress;
			this.Recipients = recipients;
			this.userComparer = Microsoft.Exchange.Clients.Owa2.Server.Core.UserComparer.CreateInstance();
			this.membershipChecker = new MembershipChecker(this.OrganizationId);
			base.SetConditionEvaluationMode(ConditionEvaluationMode.Full);
			this.policyTipRequestLogger = policyTipRequestLogger;
			this.RuleExecutionMonitor = new RuleHealthMonitor(RuleHealthMonitor.ActivityType.Execute, 1L, 0L, delegate(string eventMessageDetails)
			{
			});
			this.RuleExecutionMonitor.MtlLogWriter = new RuleHealthMonitor.MtlLogWriterDelegate(this.AppendPerRuleDiagnosticData);
			this.RuleExecutionMonitor.TenantId = this.OrganizationId.ToString();
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0004A688 File Offset: 0x00048888
		public void AppendPerRuleDiagnosticData(string agentName, string eventTopic, List<KeyValuePair<string, string>> data)
		{
			if (data != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in data)
				{
					this.ruleEvalLatency.Append(keyValuePair.Key);
					this.ruleEvalLatency.Append("=");
					this.ruleEvalLatency.Append(keyValuePair.Value);
					this.ruleEvalLatency.Append("|");
				}
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x0004A71C File Offset: 0x0004891C
		public List<DlpPolicyMatchDetail> DlpPolicyMatchDetails
		{
			get
			{
				return this.dlpPolicyMatchDetails;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0004A724 File Offset: 0x00048924
		public OrganizationId OrganizationId
		{
			get
			{
				return this.Item.Session.OrganizationId;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x0004A736 File Offset: 0x00048936
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x0004A73E File Offset: 0x0004893E
		public RuleHealthMonitor RuleExecutionMonitor { get; set; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x0004A747 File Offset: 0x00048947
		// (set) Token: 0x060014FA RID: 5370 RVA: 0x0004A74F File Offset: 0x0004894F
		public Item Item { get; private set; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x0004A758 File Offset: 0x00048958
		// (set) Token: 0x060014FC RID: 5372 RVA: 0x0004A760 File Offset: 0x00048960
		public ScanResultStorageProvider ScanResultStorageProvider { get; private set; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0004A769 File Offset: 0x00048969
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x0004A771 File Offset: 0x00048971
		public bool NoContentMatch { get; private set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0004A77A File Offset: 0x0004897A
		// (set) Token: 0x06001500 RID: 5376 RVA: 0x0004A782 File Offset: 0x00048982
		public ShortList<string> Recipients { get; private set; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0004A78B File Offset: 0x0004898B
		// (set) Token: 0x06001502 RID: 5378 RVA: 0x0004A793 File Offset: 0x00048993
		public string FromAddress { get; private set; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0004A79C File Offset: 0x0004899C
		public override IStringComparer UserComparer
		{
			get
			{
				return this.userComparer;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0004A7A4 File Offset: 0x000489A4
		public override IStringComparer MembershipChecker
		{
			get
			{
				return this.membershipChecker;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x0004A7AC File Offset: 0x000489AC
		protected override FilteringServiceInvokerRequest FilteringServiceInvokerRequest
		{
			get
			{
				return OwaFilteringServiceInvokerRequest.CreateInstance(this.Item, this.ScanResultStorageProvider);
			}
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0004A7C0 File Offset: 0x000489C0
		public override bool ShouldInvokeFips()
		{
			this.invokeFips = (this.Item != null && this.ScanResultStorageProvider.NeedsClassificationForBodyOrAnyAttachments());
			this.policyTipRequestLogger.AppendData("InvokeFips".ToString(), this.invokeFips ? "1" : "0");
			if (this.invokeFips)
			{
				this.fipsScanStartTime = DateTime.UtcNow;
			}
			return this.invokeFips;
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0004A82C File Offset: 0x00048A2C
		protected override void OnDataClassificationsRetrieved(FilteringResults filteringResults)
		{
			if (this.invokeFips)
			{
				TimeSpan timeSpan = DateTime.UtcNow - this.fipsScanStartTime;
				this.policyTipRequestLogger.AppendExtraData("FipsLatency", timeSpan.TotalMilliseconds.ToString());
				if (filteringResults != null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (filteringResults.Streams != null)
					{
						foreach (StreamIdentity streamIdentity in filteringResults.Streams)
						{
							stringBuilder.Append(string.Format("(StreamName:{0}/StreamId:{1}/ParentId:{2})", streamIdentity.Name, streamIdentity.Id, streamIdentity.ParentId));
						}
					}
					StringBuilder stringBuilder2 = new StringBuilder();
					if (filteringResults.ScanResults != null)
					{
						foreach (ScanResult scanResult in filteringResults.ScanResults)
						{
							stringBuilder2.Append(string.Format("(StreamName:{0}/StreamId:{1}/ElapsedTime:{2})", scanResult.Stream.Name, scanResult.Stream.Id, scanResult.ElapsedTime));
						}
					}
					this.policyTipRequestLogger.AppendExtraData("FipsParsedStreams", stringBuilder.ToString());
					this.policyTipRequestLogger.AppendExtraData("FipsPerScanResultLatency", stringBuilder2.ToString());
				}
			}
			IEnumerable<DiscoveredDataClassification> enumerable = FipsResultParser.ParseDataClassifications(filteringResults, base.Tracer);
			this.policyTipRequestLogger.AppendData("NewClassifications", DiscoveredDataClassification.ToString(enumerable));
			IEnumerable<DiscoveredDataClassification> dlpDetectedClassificationObjects = this.ScanResultStorageProvider.GetDlpDetectedClassificationObjects();
			this.policyTipRequestLogger.AppendData("OldClassifications", DiscoveredDataClassification.ToString(dlpDetectedClassificationObjects));
			IEnumerable<DiscoveredDataClassification> enumerable2 = FipsResultParser.UnionDiscoveredDataClassificationsFromDistinctStreams(dlpDetectedClassificationObjects, enumerable);
			this.policyTipRequestLogger.AppendData("UnionClassifications", DiscoveredDataClassification.ToString(enumerable2));
			base.SetDataClassifications(enumerable2);
			this.ScanResultStorageProvider.SetHasDlpDetectedClassifications();
			this.ScanResultStorageProvider.SetDlpDetectedClassificationObjects(enumerable2);
			StringBuilder stringBuilder3 = new StringBuilder();
			foreach (DiscoveredDataClassification discoveredDataClassification in enumerable2)
			{
				stringBuilder3.Append(discoveredDataClassification.Id);
				stringBuilder3.Append("|");
			}
			this.ScanResultStorageProvider.SetDlpDetectedClassifications(stringBuilder3.ToString());
			if (!enumerable2.Any<DiscoveredDataClassification>())
			{
				this.NoContentMatch = true;
			}
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0004AAAC File Offset: 0x00048CAC
		internal void CapturePerRuleData()
		{
			RuleEvaluationResult currentRuleResult = base.RulesEvaluationHistory.GetCurrentRuleResult(this);
			this.ruleEvalResult.Append(string.Format("RuleName:{0};Result:{1}.", base.CurrentRule.Name, (currentRuleResult == null) ? "null" : OwaRulesEvaluationContext.RuleEvalResultToString(currentRuleResult)));
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0004AAF8 File Offset: 0x00048CF8
		internal void CapturePerRuleMatchData()
		{
			DlpPolicyMatchDetail dlpPolicyMatchDetail = new DlpPolicyMatchDetail();
			dlpPolicyMatchDetail.Action = (DlpPolicyTipAction)Enum.Parse(typeof(DlpPolicyTipAction), base.ActionName);
			if (base.MatchedClassifications != null)
			{
				dlpPolicyMatchDetail.Classifications = new string[base.MatchedClassifications.Count];
				int num = 0;
				foreach (string key in base.MatchedClassifications.Keys)
				{
					dlpPolicyMatchDetail.Classifications[num++] = base.MatchedClassifications[key];
				}
			}
			EmailAddressWrapper[] recipients = null;
			string[] attachmentIds = null;
			OwaRulesEvaluationContext.TrackMatchingRecipientsAndAttachments(base.RulesEvaluationHistory.GetCurrentRuleResult(this), this.policyTipRequestLogger, out recipients, out attachmentIds);
			dlpPolicyMatchDetail.Recipients = recipients;
			dlpPolicyMatchDetail.AttachmentIds = OwaRulesEvaluationContext.ConvertAttachmentIdsToAttachmentIdTypes(attachmentIds, this.Item, this.policyTipRequestLogger);
			this.DlpPolicyMatchDetails.Add(dlpPolicyMatchDetail);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0004ABF4 File Offset: 0x00048DF4
		private static AttachmentIdType[] ConvertAttachmentIdsToAttachmentIdTypes(string[] attachmentIds, Item item, PolicyTipRequestLogger policyTipRequestLogger)
		{
			if (attachmentIds == null || attachmentIds.Length == 0)
			{
				return null;
			}
			List<AttachmentIdType> list = new List<AttachmentIdType>();
			foreach (string text in attachmentIds)
			{
				AttachmentId item2 = null;
				try
				{
					item2 = AttachmentId.Deserialize(text);
				}
				catch (CorruptDataException)
				{
					policyTipRequestLogger.AppendData("InvalidAttachment", text);
				}
				AttachmentIdType item3 = new AttachmentIdType(new IdAndSession(item.Id, item.Session, new List<AttachmentId>
				{
					item2
				}).GetConcatenatedId().Id);
				list.Add(item3);
			}
			return list.ToArray();
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0004ACA8 File Offset: 0x00048EA8
		internal static void TrackMatchingRecipientsAndAttachments(RuleEvaluationResult ruleEvaluationResult, PolicyTipRequestLogger policyTipRequestLogger, out EmailAddressWrapper[] recipientEmails, out string[] attachmentIds)
		{
			recipientEmails = null;
			attachmentIds = null;
			if (ruleEvaluationResult == null)
			{
				return;
			}
			if (ruleEvaluationResult.Predicates != null)
			{
				List<string> listA = null;
				PredicateEvaluationResult predicateEvaluationResult = RuleEvaluationResult.GetPredicateEvaluationResult(typeof(SentToPredicate), ruleEvaluationResult.Predicates).FirstOrDefault<PredicateEvaluationResult>();
				if (predicateEvaluationResult != null)
				{
					listA = predicateEvaluationResult.MatchResults;
				}
				List<string> listB = null;
				PredicateEvaluationResult predicateEvaluationResult2 = RuleEvaluationResult.GetPredicateEvaluationResult(typeof(SentToScopePredicate), ruleEvaluationResult.Predicates).FirstOrDefault<PredicateEvaluationResult>();
				if (predicateEvaluationResult2 != null)
				{
					listB = predicateEvaluationResult2.MatchResults;
				}
				List<string> locationList = null;
				PredicateEvaluationResult predicateEvaluationResult3 = (from mcdc in RuleEvaluationResult.GetPredicateEvaluationResult(typeof(ContainsDataClassificationPredicate), ruleEvaluationResult.Predicates)
				where mcdc.SupplementalInfo == 1
				select mcdc).FirstOrDefault<PredicateEvaluationResult>();
				if (predicateEvaluationResult3 != null)
				{
					locationList = predicateEvaluationResult3.MatchResults;
				}
				recipientEmails = OwaRulesEvaluationContext.IntersectAndReturnEmailAddressWrappers(listA, listB);
				attachmentIds = OwaRulesEvaluationContext.GetAttachmentTypeIds(locationList, policyTipRequestLogger);
			}
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0004AD7C File Offset: 0x00048F7C
		private static string[] GetAttachmentTypeIds(List<string> locationList, PolicyTipRequestLogger policyTipRequestLogger)
		{
			if (locationList == null)
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (string text in locationList)
			{
				if (!string.IsNullOrEmpty(text) && !text.Equals("Message Body", StringComparison.OrdinalIgnoreCase))
				{
					int num = text.LastIndexOf(':');
					if (num <= 0 || num == text.Length - 1)
					{
						policyTipRequestLogger.AppendData("InvalidAttachment", text);
					}
					else
					{
						string value = text.Substring(0, num);
						string text2 = text.Substring(num + 1);
						if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(text2))
						{
							policyTipRequestLogger.AppendData("InvalidAttachment", text);
						}
						else
						{
							list.Add(text2);
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0004AE50 File Offset: 0x00049050
		private static EmailAddressWrapper[] IntersectAndReturnEmailAddressWrappers(List<string> listA, List<string> listB)
		{
			EmailAddressWrapper[] array = null;
			List<string> list;
			if (listA == null)
			{
				list = listB;
			}
			else if (listB == null)
			{
				list = listA;
			}
			else
			{
				list = listA.Intersect(listB, StringComparer.OrdinalIgnoreCase).ToList<string>();
			}
			if (list != null)
			{
				array = new EmailAddressWrapper[list.Count];
				int num = 0;
				foreach (string emailAddress in list)
				{
					EmailAddressWrapper emailAddressWrapper = new EmailAddressWrapper();
					emailAddressWrapper.EmailAddress = emailAddress;
					array[num++] = emailAddressWrapper;
				}
			}
			return array;
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0004AEF0 File Offset: 0x000490F0
		internal static string RuleEvalResultToString(RuleEvaluationResult ruleEvalResult)
		{
			if (ruleEvalResult == null)
			{
				return string.Empty;
			}
			return string.Format("IsRuleMatch:{0}/Predicates:{1}/Actions:{2}.", ruleEvalResult.IsMatch ? "1" : "0", string.Join(";", from predicate in ruleEvalResult.Predicates
			select OwaRulesEvaluationContext.PredicateEvalResultToString(predicate)), string.Join(";", ruleEvalResult.Actions));
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0004AF68 File Offset: 0x00049168
		internal static string PredicateEvalResultToString(PredicateEvaluationResult predicateEvalResult)
		{
			if (predicateEvalResult == null)
			{
				return string.Empty;
			}
			List<string> values = (predicateEvalResult.Type == typeof(SentToPredicate) || predicateEvalResult.Type == typeof(SentToScopePredicate) || predicateEvalResult.Type == typeof(OwaIsSameUserPredicate)) ? PolicyTipRequestLogger.MarkAsPII(predicateEvalResult.MatchResults) : predicateEvalResult.MatchResults;
			return string.Format("IsPredicateMatch:{0}/MatchResults:{1}/SupplInfo:{2}/Type:{3}/PropertyName:{4}.", new object[]
			{
				predicateEvalResult.IsMatch ? "1" : "0",
				string.Join(";", values),
				predicateEvalResult.SupplementalInfo,
				predicateEvalResult.Type,
				predicateEvalResult.PropertyName ?? string.Empty
			});
		}

		// Token: 0x04000B56 RID: 2902
		private const string RuleEvalResultToStringFormat = "IsRuleMatch:{0}/Predicates:{1}/Actions:{2}.";

		// Token: 0x04000B57 RID: 2903
		private const string PredicateEvalResultToStringFormat = "IsPredicateMatch:{0}/MatchResults:{1}/SupplInfo:{2}/Type:{3}/PropertyName:{4}.";

		// Token: 0x04000B58 RID: 2904
		private const string FipsPerScanResultLatencyFormat = "(StreamName:{0}/StreamId:{1}/ElapsedTime:{2})";

		// Token: 0x04000B59 RID: 2905
		private const string FipsParsedStreamsFormat = "(StreamName:{0}/StreamId:{1}/ParentId:{2})";

		// Token: 0x04000B5A RID: 2906
		private IStringComparer userComparer;

		// Token: 0x04000B5B RID: 2907
		private IStringComparer membershipChecker;

		// Token: 0x04000B5C RID: 2908
		private PolicyTipRequestLogger policyTipRequestLogger;

		// Token: 0x04000B5D RID: 2909
		private StringBuilder ruleEvalLatency = new StringBuilder();

		// Token: 0x04000B5E RID: 2910
		private StringBuilder ruleEvalResult = new StringBuilder();

		// Token: 0x04000B5F RID: 2911
		private List<DlpPolicyMatchDetail> dlpPolicyMatchDetails = new List<DlpPolicyMatchDetail>();

		// Token: 0x04000B60 RID: 2912
		private bool invokeFips;

		// Token: 0x04000B61 RID: 2913
		private DateTime fipsScanStartTime;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.FfoReporting.Data;
using Microsoft.Exchange.Management.FfoReporting.Providers;
using Microsoft.Exchange.Management.ReportingTask;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003A1 RID: 929
	[OutputType(new Type[]
	{
		typeof(MailTrafficSummaryReport)
	})]
	[Cmdlet("Get", "MailTrafficSummaryReport")]
	public sealed class GetMailTrafficSummaryReport : FfoReportingDalTask<MailTrafficSummaryReport>
	{
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x00089515 File Offset: 0x00087715
		public override string ComponentName
		{
			get
			{
				return ExchangeComponent.FfoRws.Name;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002073 RID: 8307 RVA: 0x00089521 File Offset: 0x00087721
		public override string MonitorEventName
		{
			get
			{
				return "FFO Reporting Task Status Monitor";
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x00089528 File Offset: 0x00087728
		public override string DalMonitorEventName
		{
			get
			{
				return "FFO DAL Retrieval Status Monitor";
			}
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x00089554 File Offset: 0x00087754
		public GetMailTrafficSummaryReport()
		{
			this.Category = string.Empty;
			this.Domain = new MultiValuedProperty<Fqdn>();
			this.DlpPolicy = new MultiValuedProperty<string>();
			this.TransportRule = new MultiValuedProperty<string>();
			this.mappings.Add(GetMailTrafficSummaryReport.Categories.InboundDLPHits, Tuple.Create<string, GetMailTrafficSummaryReport.AggregateDelegate>("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.PolicyTrafficReport, Microsoft.Exchange.Hygiene.Data", new GetMailTrafficSummaryReport.AggregateDelegate(this.AggregateInboundDLPHits)));
			this.mappings.Add(GetMailTrafficSummaryReport.Categories.OutboundDLPHits, Tuple.Create<string, GetMailTrafficSummaryReport.AggregateDelegate>("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.PolicyTrafficReport, Microsoft.Exchange.Hygiene.Data", new GetMailTrafficSummaryReport.AggregateDelegate(this.AggregateOutboundDLPHits)));
			this.mappings.Add(GetMailTrafficSummaryReport.Categories.InboundTransportRuleHits, Tuple.Create<string, GetMailTrafficSummaryReport.AggregateDelegate>("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.PolicyTrafficReport, Microsoft.Exchange.Hygiene.Data", () => this.AggregateTransportRuleHits(Schema.DirectionValues.Inbound)));
			this.mappings.Add(GetMailTrafficSummaryReport.Categories.OutboundTransportRuleHits, Tuple.Create<string, GetMailTrafficSummaryReport.AggregateDelegate>("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.PolicyTrafficReport, Microsoft.Exchange.Hygiene.Data", () => this.AggregateTransportRuleHits(Schema.DirectionValues.Outbound)));
			this.mappings.Add(GetMailTrafficSummaryReport.Categories.InboundDLPPolicyRuleHits, Tuple.Create<string, GetMailTrafficSummaryReport.AggregateDelegate>("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.PolicyTrafficReport, Microsoft.Exchange.Hygiene.Data", () => this.AggregateDLPPolicyRuleHits(Schema.DirectionValues.Inbound)));
			this.mappings.Add(GetMailTrafficSummaryReport.Categories.OutboundDLPPolicyRuleHits, Tuple.Create<string, GetMailTrafficSummaryReport.AggregateDelegate>("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.PolicyTrafficReport, Microsoft.Exchange.Hygiene.Data", () => this.AggregateDLPPolicyRuleHits(Schema.DirectionValues.Outbound)));
			this.AddTopTraffic(GetMailTrafficSummaryReport.Categories.TopSpamRecipient, Schema.EventTypes.TopSpamUser.ToString(), new string[]
			{
				Schema.DirectionValues.Inbound.ToString()
			});
			this.AddTopTraffic(GetMailTrafficSummaryReport.Categories.TopMailSender, Schema.EventTypes.TopMailUser.ToString(), new string[]
			{
				Schema.DirectionValues.Outbound.ToString()
			});
			this.AddTopTraffic(GetMailTrafficSummaryReport.Categories.TopMailRecipient, Schema.EventTypes.TopMailUser.ToString(), new string[]
			{
				Schema.DirectionValues.Inbound.ToString()
			});
			this.AddTopTraffic(GetMailTrafficSummaryReport.Categories.TopMalwareRecipient, Schema.EventTypes.TopMalwareUser.ToString(), new string[]
			{
				Schema.DirectionValues.Inbound.ToString()
			});
			this.AddTopTraffic(GetMailTrafficSummaryReport.Categories.TopMalware, Schema.EventTypes.TopMalware.ToString(), new string[]
			{
				Schema.DirectionValues.Inbound.ToString(),
				Schema.DirectionValues.Outbound.ToString()
			});
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x00089782 File Offset: 0x00087982
		// (set) Token: 0x06002077 RID: 8311 RVA: 0x0008978A File Offset: 0x0008798A
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(GetMailTrafficSummaryReport.Categories)
		}, ErrorMessage = Strings.IDs.InvalidCategory)]
		[Parameter(Mandatory = false)]
		public string Category { get; set; }

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x00089793 File Offset: 0x00087993
		// (set) Token: 0x06002079 RID: 8313 RVA: 0x0008979B File Offset: 0x0008799B
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[QueryParameter("DomainListQueryDefinition", new string[]
		{

		})]
		[CmdletValidator("ValidateDomain", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidDomain, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		public MultiValuedProperty<Fqdn> Domain { get; set; }

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x000897A4 File Offset: 0x000879A4
		// (set) Token: 0x0600207B RID: 8315 RVA: 0x000897AC File Offset: 0x000879AC
		[Parameter(Mandatory = false)]
		[QueryParameter("StartDateKeyQueryDefinition", new string[]
		{
			"StartHourKeyQueryDefinition"
		}, MethodName = "AddDateFilter")]
		public DateTime? StartDate { get; set; }

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x000897B5 File Offset: 0x000879B5
		// (set) Token: 0x0600207D RID: 8317 RVA: 0x000897BD File Offset: 0x000879BD
		[Parameter(Mandatory = false)]
		[QueryParameter("EndDateKeyQueryDefinition", new string[]
		{
			"EndHourKeyQueryDefinition"
		}, MethodName = "AddDateFilter")]
		public DateTime? EndDate { get; set; }

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x000897C6 File Offset: 0x000879C6
		// (set) Token: 0x0600207F RID: 8319 RVA: 0x000897CE File Offset: 0x000879CE
		[CmdletValidator("ValidateDlpPolicy", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidDlpPolicyParameter, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		[QueryParameter("PolicyListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DlpPolicy { get; set; }

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x000897D7 File Offset: 0x000879D7
		// (set) Token: 0x06002081 RID: 8321 RVA: 0x000897DF File Offset: 0x000879DF
		[CmdletValidator("ValidateTransportRule", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidTransportRule, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		[QueryParameter("RuleListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> TransportRule { get; set; }

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x000897E8 File Offset: 0x000879E8
		// (set) Token: 0x06002083 RID: 8323 RVA: 0x000897F0 File Offset: 0x000879F0
		private GetMailTrafficSummaryReport.Categories CategoryEnum { get; set; }

		// Token: 0x06002084 RID: 8324 RVA: 0x000897FC File Offset: 0x000879FC
		protected override void CustomInternalValidate()
		{
			base.CustomInternalValidate();
			Schema.Utilities.CheckDates(this.StartDate, this.EndDate, new Schema.Utilities.NotifyNeedDefaultDatesDelegate(this.SetDefaultDates), new Schema.Utilities.ValidateDatesDelegate(Schema.Utilities.VerifyDateRange));
			GetMailTrafficSummaryReport.Categories categories;
			if (!Enum.TryParse<GetMailTrafficSummaryReport.Categories>(this.Category, true, out categories))
			{
				throw new InvalidExpressionException(Strings.InvalidCategory);
			}
			Tuple<string, GetMailTrafficSummaryReport.AggregateDelegate> tuple;
			if (!this.mappings.TryGetValue(categories, out tuple))
			{
				throw new InvalidOperationException(Strings.InvalidCategory);
			}
			this.CategoryEnum = categories;
			base.DalObjectTypeName = tuple.Item1;
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x00089888 File Offset: 0x00087A88
		protected override IReadOnlyList<MailTrafficSummaryReport> AggregateOutput()
		{
			Tuple<string, GetMailTrafficSummaryReport.AggregateDelegate> tuple;
			if (this.mappings.TryGetValue(this.CategoryEnum, out tuple))
			{
				GetMailTrafficSummaryReport.AggregateDelegate item = tuple.Item2;
				IReadOnlyList<MailTrafficSummaryReport> readOnlyList = item();
				if (base.NeedSuppressingPiiData)
				{
					DataProcessorDriver.Process<MailTrafficSummaryReport>(readOnlyList, RedactionProcessor.Create<MailTrafficSummaryReport>());
				}
				return readOnlyList;
			}
			throw new InvalidOperationException(Strings.InvalidCategory);
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00089900 File Offset: 0x00087B00
		private void AddTopTraffic(GetMailTrafficSummaryReport.Categories category, string eventType, params string[] directions)
		{
			this.mappings.Add(category, Tuple.Create<string, GetMailTrafficSummaryReport.AggregateDelegate>("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.TopTrafficReport, Microsoft.Exchange.Hygiene.Data", () => this.AggregateTopTraffic(eventType, directions)));
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x0008994C File Offset: 0x00087B4C
		private QueryFilter BuildQueryFilter(IEnumerable<ComparisonFilter> optionalFilters)
		{
			CompositeFilter compositeFilter = (CompositeFilter)base.BuildQueryFilter();
			List<ComparisonFilter> list = new List<ComparisonFilter>(compositeFilter.Filters.Cast<ComparisonFilter>());
			list.AddRange(optionalFilters);
			return new AndFilter(list.ToArray<QueryFilter>());
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00089988 File Offset: 0x00087B88
		private IReadOnlyList<TDataObject> GetData<TDataObject>(IEnumerable<ComparisonFilter> filters)
		{
			IEnumerable dalRecords = base.GetDalRecords(new FfoReportingDalTask<MailTrafficSummaryReport>.DalRetrievalDelegate(ServiceLocator.Current.GetService<IDalProvider>().GetAllDataPages), this.BuildQueryFilter(filters));
			return DataProcessorDriver.Process<TDataObject>(dalRecords, ConversionProcessor.Create<TDataObject>(this));
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x000899C5 File Offset: 0x00087BC5
		private ComparisonFilter CreateDataTableFilter(string definitionName, params object[] values)
		{
			values = ((values.Length == 0) ? new object[0] : values);
			return new ComparisonFilter(ComparisonOperator.Equal, Schema.Utilities.GetSchemaPropertyDefinition(definitionName), Schema.Utilities.CreateDataTable(values));
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000899E9 File Offset: 0x00087BE9
		private ComparisonFilter CreateFilter(string definitionName, object value)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, Schema.Utilities.GetSchemaPropertyDefinition(definitionName), value);
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00089A48 File Offset: 0x00087C48
		internal IReadOnlyList<MailTrafficSummaryReport> AggregateInboundDLPHits()
		{
			IReadOnlyList<MailTrafficPolicyReport> data = this.GetData<MailTrafficPolicyReport>(new List<ComparisonFilter>
			{
				this.CreateDataTableFilter("DirectionListQueryDefinition", new object[]
				{
					Schema.DirectionValues.Inbound.ToString()
				}),
				this.CreateFilter("AggregateByQueryDefinition", Schema.AggregateByValues.Summary.ToString()),
				this.CreateDataTableFilter("EventTypeListQueryDefinition", new object[]
				{
					Schema.EventTypes.DLPPolicyHits.ToString()
				}),
				this.CreateDataTableFilter("ActionListQueryDefinition", new object[0]),
				this.CreateDataTableFilter("SummarizeByQueryDefinition", new object[]
				{
					Schema.SummarizeByValues.Action.ToString(),
					Schema.SummarizeByValues.Domain.ToString(),
					Schema.SummarizeByValues.EventType.ToString(),
					Schema.SummarizeByValues.TransportRule.ToString()
				})
			});
			IEnumerable<IGrouping<string, MailTrafficPolicyReport>> enumerable = from trafficReport in data
			group trafficReport by trafficReport.DlpPolicy into policyGroup
			select policyGroup;
			List<Tuple<string, int>> list = new List<Tuple<string, int>>();
			foreach (IGrouping<string, MailTrafficPolicyReport> grouping in enumerable)
			{
				string key = grouping.Key;
				int item = grouping.Sum((MailTrafficPolicyReport report) => report.MessageCount);
				list.Add(Tuple.Create<string, int>(key, item));
			}
			int count = (base.Page - 1) * base.PageSize;
			return (from tuple in (from tuple in list
			orderby tuple.Item2 descending
			select tuple).Skip(count).Take(base.PageSize)
			select new MailTrafficSummaryReport
			{
				C1 = tuple.Item1,
				C2 = tuple.Item2.ToString()
			}).ToList<MailTrafficSummaryReport>();
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00089E50 File Offset: 0x00088050
		internal IReadOnlyList<MailTrafficSummaryReport> AggregateOutboundDLPHits()
		{
			IReadOnlyList<MailTrafficPolicyReport> data = this.GetData<MailTrafficPolicyReport>(new List<ComparisonFilter>
			{
				this.CreateDataTableFilter("DirectionListQueryDefinition", new object[]
				{
					Schema.DirectionValues.Outbound.ToString()
				}),
				this.CreateFilter("AggregateByQueryDefinition", Schema.AggregateByValues.Summary.ToString()),
				this.CreateDataTableFilter("EventTypeListQueryDefinition", new object[]
				{
					Schema.EventTypes.DLPPolicyHits.ToString(),
					Schema.EventTypes.DLPPolicyOverride.ToString(),
					Schema.EventTypes.DLPPolicyFalsePositive.ToString()
				}),
				this.CreateDataTableFilter("ActionListQueryDefinition", new object[0]),
				this.CreateDataTableFilter("SummarizeByQueryDefinition", new object[]
				{
					Schema.SummarizeByValues.Action,
					Schema.SummarizeByValues.Domain,
					Schema.SummarizeByValues.TransportRule
				})
			});
			IEnumerable<IGrouping<string, IGrouping<string, MailTrafficPolicyReport>>> enumerable = from trafficReport in data
			group trafficReport by trafficReport.DlpPolicy into policyGroup
			from eventTypeGroup in 
				from trafficReport in policyGroup
				group trafficReport by trafficReport.EventType
			group eventTypeGroup by policyGroup.Key;
			List<Tuple<string, int, int, int>> list = new List<Tuple<string, int, int, int>>();
			string key = Schema.EventTypes.DLPPolicyHits.ToString().ToLower();
			string key2 = Schema.EventTypes.DLPPolicyOverride.ToString().ToLower();
			string key3 = Schema.EventTypes.DLPPolicyFalsePositive.ToString().ToLower();
			Dictionary<string, int> dictionary = new Dictionary<string, int>
			{
				{
					key,
					0
				},
				{
					key2,
					0
				},
				{
					key3,
					0
				}
			};
			foreach (IGrouping<string, IGrouping<string, MailTrafficPolicyReport>> grouping in enumerable)
			{
				dictionary[key] = 0;
				dictionary[key2] = 0;
				dictionary[key3] = 0;
				foreach (IGrouping<string, MailTrafficPolicyReport> grouping2 in grouping)
				{
					dictionary[grouping2.Key.ToLower()] = grouping2.Sum((MailTrafficPolicyReport report) => report.MessageCount);
				}
				list.Add(Tuple.Create<string, int, int, int>(grouping.Key, dictionary[key], dictionary[key2], dictionary[key3]));
			}
			int count = (base.Page - 1) * base.PageSize;
			return (from tuple in (from tuple in list
			orderby tuple.Item2 descending
			select tuple).Skip(count).Take(base.PageSize)
			select new MailTrafficSummaryReport
			{
				C1 = tuple.Item1,
				C2 = tuple.Item2.ToString(),
				C3 = tuple.Item3.ToString(),
				C4 = tuple.Item4.ToString()
			}).ToList<MailTrafficSummaryReport>();
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x0008A37C File Offset: 0x0008857C
		internal IReadOnlyList<MailTrafficSummaryReport> AggregateTransportRuleHits(Schema.DirectionValues direction)
		{
			IReadOnlyList<MailTrafficPolicyReport> data = this.GetData<MailTrafficPolicyReport>(new List<ComparisonFilter>
			{
				this.CreateDataTableFilter("DirectionListQueryDefinition", new object[]
				{
					direction.ToString()
				}),
				this.CreateFilter("AggregateByQueryDefinition", Schema.AggregateByValues.Summary.ToString()),
				this.CreateDataTableFilter("EventTypeListQueryDefinition", new object[]
				{
					Schema.EventTypes.TransportRuleHits.ToString()
				}),
				this.CreateDataTableFilter("ActionListQueryDefinition", new object[]
				{
					Schema.Actions.SetAuditSeverityLow.ToString(),
					Schema.Actions.SetAuditSeverityMedium.ToString(),
					Schema.Actions.SetAuditSeverityHigh.ToString()
				}),
				this.CreateDataTableFilter("SummarizeByQueryDefinition", new object[]
				{
					Schema.SummarizeByValues.Domain.ToString(),
					Schema.SummarizeByValues.EventType.ToString()
				})
			});
			IEnumerable<IGrouping<string, IGrouping<string, MailTrafficPolicyReport>>> enumerable = from trafficReport in data
			group trafficReport by trafficReport.TransportRule into ruleGroup
			from actionGroup in 
				from trafficReport in ruleGroup
				group trafficReport by trafficReport.Action
			group actionGroup by ruleGroup.Key;
			List<Tuple<string, string, int>> list = new List<Tuple<string, string, int>>();
			foreach (IGrouping<string, IGrouping<string, MailTrafficPolicyReport>> grouping in enumerable)
			{
				foreach (IGrouping<string, MailTrafficPolicyReport> grouping2 in grouping)
				{
					list.Add(Tuple.Create<string, string, int>(grouping.Key, grouping2.Key, grouping2.Sum((MailTrafficPolicyReport report) => report.MessageCount)));
				}
			}
			int count = (base.Page - 1) * base.PageSize;
			return (from tuple in (from tuple in list
			orderby tuple.Item3 descending
			select tuple).Skip(count).Take(base.PageSize)
			select new MailTrafficSummaryReport
			{
				C1 = tuple.Item1,
				C2 = tuple.Item2,
				C3 = tuple.Item3.ToString()
			}).ToList<MailTrafficSummaryReport>();
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0008A948 File Offset: 0x00088B48
		internal IReadOnlyList<MailTrafficSummaryReport> AggregateDLPPolicyRuleHits(Schema.DirectionValues direction)
		{
			IReadOnlyList<MailTrafficPolicyReport> data = this.GetData<MailTrafficPolicyReport>(new List<ComparisonFilter>
			{
				this.CreateDataTableFilter("DirectionListQueryDefinition", new object[]
				{
					direction.ToString()
				}),
				this.CreateFilter("AggregateByQueryDefinition", Schema.AggregateByValues.Summary.ToString()),
				this.CreateDataTableFilter("EventTypeListQueryDefinition", new object[]
				{
					Schema.EventTypes.DLPRuleHits.ToString()
				}),
				this.CreateDataTableFilter("ActionListQueryDefinition", new object[]
				{
					Schema.Actions.SetAuditSeverityLow.ToString(),
					Schema.Actions.SetAuditSeverityMedium.ToString(),
					Schema.Actions.SetAuditSeverityHigh.ToString()
				}),
				this.CreateDataTableFilter("SummarizeByQueryDefinition", new object[]
				{
					Schema.SummarizeByValues.Domain.ToString(),
					Schema.SummarizeByValues.EventType.ToString()
				})
			});
			var enumerable = from trafficReport in data
			group trafficReport by new
			{
				trafficReport.DlpPolicy,
				trafficReport.TransportRule
			} into policyGroup
			from ruleGroup in 
				from trafficReport in policyGroup
				group trafficReport by trafficReport.Action
			group ruleGroup by policyGroup.Key;
			List<Tuple<string, string, string, int>> list = new List<Tuple<string, string, string, int>>();
			foreach (var grouping in enumerable)
			{
				foreach (IGrouping<string, MailTrafficPolicyReport> grouping2 in grouping)
				{
					list.Add(Tuple.Create<string, string, string, int>(grouping.Key.DlpPolicy, grouping.Key.TransportRule, grouping2.Key, grouping2.Sum((MailTrafficPolicyReport report) => report.MessageCount)));
				}
			}
			int count = (base.Page - 1) * base.PageSize;
			return (from tuple in (from tuple in list
			orderby tuple.Item4 descending
			select tuple).Skip(count).Take(base.PageSize)
			select new MailTrafficSummaryReport
			{
				C1 = tuple.Item1,
				C2 = tuple.Item2,
				C3 = tuple.Item3,
				C4 = tuple.Item4.ToString()
			}).ToList<MailTrafficSummaryReport>();
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x0008AC8C File Offset: 0x00088E8C
		internal IReadOnlyList<MailTrafficSummaryReport> AggregateTopTraffic(string eventType, params string[] directions)
		{
			IReadOnlyList<MailTrafficTopReport> data = this.GetData<MailTrafficTopReport>(new List<ComparisonFilter>
			{
				this.CreateDataTableFilter("DirectionListQueryDefinition", directions),
				this.CreateFilter("AggregateByQueryDefinition", Schema.AggregateByValues.Summary.ToString()),
				this.CreateDataTableFilter("EventTypeListQueryDefinition", new object[]
				{
					eventType
				}),
				this.CreateDataTableFilter("SummarizeByQueryDefinition", new object[]
				{
					Schema.SummarizeByValues.Action.ToString(),
					Schema.SummarizeByValues.DlpPolicy.ToString(),
					Schema.SummarizeByValues.Domain.ToString(),
					Schema.SummarizeByValues.EventType.ToString(),
					Schema.SummarizeByValues.TransportRule.ToString()
				})
			});
			IEnumerable<IGrouping<string, MailTrafficTopReport>> enumerable = from trafficReport in data
			group trafficReport by trafficReport.Name into userGroup
			select userGroup;
			List<Tuple<string, int>> list = new List<Tuple<string, int>>();
			foreach (IGrouping<string, MailTrafficTopReport> grouping in enumerable)
			{
				list.Add(Tuple.Create<string, int>(grouping.Key, grouping.Sum((MailTrafficTopReport report) => report.MessageCount)));
			}
			int count = (base.Page - 1) * base.PageSize;
			return (from tuple in (from tuple in list
			orderby tuple.Item2 descending
			select tuple).Skip(count).Take(base.PageSize)
			select new MailTrafficSummaryReport
			{
				C1 = tuple.Item1,
				C2 = tuple.Item2.ToString()
			}).ToList<MailTrafficSummaryReport>();
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x0008AE88 File Offset: 0x00089088
		private void SetDefaultDates()
		{
			DateTime value = (DateTime)ExDateTime.UtcNow;
			this.EndDate = new DateTime?(value);
			this.StartDate = new DateTime?(value.AddDays(-14.0));
		}

		// Token: 0x040019E9 RID: 6633
		private const int DefaultDateOffset = -14;

		// Token: 0x040019EA RID: 6634
		private Dictionary<GetMailTrafficSummaryReport.Categories, Tuple<string, GetMailTrafficSummaryReport.AggregateDelegate>> mappings = new Dictionary<GetMailTrafficSummaryReport.Categories, Tuple<string, GetMailTrafficSummaryReport.AggregateDelegate>>();

		// Token: 0x020003A2 RID: 930
		private enum Categories
		{
			// Token: 0x04001A18 RID: 6680
			InboundDLPHits,
			// Token: 0x04001A19 RID: 6681
			OutboundDLPHits,
			// Token: 0x04001A1A RID: 6682
			InboundTransportRuleHits,
			// Token: 0x04001A1B RID: 6683
			OutboundTransportRuleHits,
			// Token: 0x04001A1C RID: 6684
			InboundDLPPolicyRuleHits,
			// Token: 0x04001A1D RID: 6685
			OutboundDLPPolicyRuleHits,
			// Token: 0x04001A1E RID: 6686
			TopSpamRecipient,
			// Token: 0x04001A1F RID: 6687
			TopMailSender,
			// Token: 0x04001A20 RID: 6688
			TopMailRecipient,
			// Token: 0x04001A21 RID: 6689
			TopMalwareRecipient,
			// Token: 0x04001A22 RID: 6690
			TopMalware
		}

		// Token: 0x020003A3 RID: 931
		// (Invoke) Token: 0x060020BB RID: 8379
		private delegate IReadOnlyList<MailTrafficSummaryReport> AggregateDelegate();
	}
}

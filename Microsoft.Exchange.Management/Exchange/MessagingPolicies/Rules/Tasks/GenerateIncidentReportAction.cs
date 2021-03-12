using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B8F RID: 2959
	[Serializable]
	public class GenerateIncidentReportAction : TransportRuleAction, IEquatable<GenerateIncidentReportAction>
	{
		// Token: 0x06006F7C RID: 28540 RVA: 0x001C90B0 File Offset: 0x001C72B0
		public override int GetHashCode()
		{
			return this.ReportDestination.GetHashCode() ^ this.IncidentReportOriginalMail.GetHashCode() ^ Utils.GetHashCodeForArray<IncidentReportContent>(this.IncidentReportContent);
		}

		// Token: 0x06006F7D RID: 28541 RVA: 0x001C90EE File Offset: 0x001C72EE
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as GenerateIncidentReportAction)));
		}

		// Token: 0x06006F7E RID: 28542 RVA: 0x001C9128 File Offset: 0x001C7328
		public bool Equals(GenerateIncidentReportAction other)
		{
			return this.ReportDestination.Equals(other.ReportDestination) && this.IncidentReportOriginalMail.Equals(other.IncidentReportOriginalMail) && this.IncidentReportContent.Length == other.IncidentReportContent.Length && !this.IncidentReportContent.Except(other.IncidentReportContent).Any<IncidentReportContent>() && !other.IncidentReportContent.Except(this.IncidentReportContent).Any<IncidentReportContent>();
		}

		// Token: 0x170022C8 RID: 8904
		// (get) Token: 0x06006F7F RID: 28543 RVA: 0x001C91AD File Offset: 0x001C73AD
		// (set) Token: 0x06006F80 RID: 28544 RVA: 0x001C91B5 File Offset: 0x001C73B5
		[LocDisplayName(RulesTasksStrings.IDs.ReportDestinationDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.ReportDestinationDescription)]
		[ActionParameterName("GenerateIncidentReport")]
		public SmtpAddress ReportDestination { get; set; }

		// Token: 0x170022C9 RID: 8905
		// (get) Token: 0x06006F81 RID: 28545 RVA: 0x001C91BE File Offset: 0x001C73BE
		// (set) Token: 0x06006F82 RID: 28546 RVA: 0x001C91C6 File Offset: 0x001C73C6
		[ActionParameterName("IncidentReportOriginalMail")]
		[LocDisplayName(RulesTasksStrings.IDs.IncidentReportOriginalMailnDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.IncidentReportOriginalMailDescription)]
		public IncidentReportOriginalMail IncidentReportOriginalMail { get; set; }

		// Token: 0x170022CA RID: 8906
		// (get) Token: 0x06006F83 RID: 28547 RVA: 0x001C91CF File Offset: 0x001C73CF
		// (set) Token: 0x06006F84 RID: 28548 RVA: 0x001C91DC File Offset: 0x001C73DC
		[LocDescription(RulesTasksStrings.IDs.IncidentReportContentDescription)]
		[ActionParameterName("IncidentReportContent")]
		[LocDisplayName(RulesTasksStrings.IDs.IncidentReportContentDisplayName)]
		public IncidentReportContent[] IncidentReportContent
		{
			get
			{
				return this.incidentReportContent.ToArray();
			}
			set
			{
				this.incidentReportContent = value.ToList<IncidentReportContent>();
			}
		}

		// Token: 0x06006F85 RID: 28549 RVA: 0x001C91EA File Offset: 0x001C73EA
		private bool AddIncidentReportContentItem(IncidentReportContent contentItem)
		{
			if (this.incidentReportContent.Contains(contentItem))
			{
				return false;
			}
			this.incidentReportContent.Add(contentItem);
			return true;
		}

		// Token: 0x170022CB RID: 8907
		// (get) Token: 0x06006F86 RID: 28550 RVA: 0x001C9220 File Offset: 0x001C7420
		internal override string Description
		{
			get
			{
				string includeOriginalMail = string.Join(GenerateIncidentReportAction.IncidentReportContentDescriptionSeparator, (from item in this.IncidentReportContent
				select LocalizedDescriptionAttribute.FromEnum(typeof(IncidentReportContent), item)).ToArray<string>());
				return RulesTasksStrings.RuleDescriptionGenerateIncidentReport(this.ReportDestination.ToString(), includeOriginalMail);
			}
		}

		// Token: 0x06006F87 RID: 28551 RVA: 0x001C9284 File Offset: 0x001C7484
		public GenerateIncidentReportAction()
		{
			this.Reset();
		}

		// Token: 0x06006F88 RID: 28552 RVA: 0x001C92A0 File Offset: 0x001C74A0
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "GenerateIncidentReport" || action.Arguments.Count == 0)
			{
				return null;
			}
			GenerateIncidentReportAction generateIncidentReportAction = new GenerateIncidentReportAction();
			string stringValue = TransportRuleAction.GetStringValue(action.Arguments[0]);
			if (!SmtpAddress.IsValidSmtpAddress(stringValue))
			{
				return null;
			}
			generateIncidentReportAction.ReportDestination = new SmtpAddress(stringValue);
			IncidentReportOriginalMail legacyReportOriginalMail;
			if (action.Arguments.Count == 2 && GenerateIncidentReport.TryGetIncidentReportOriginalMailParameter(TransportRuleAction.GetStringValue(action.Arguments[1]), out legacyReportOriginalMail))
			{
				generateIncidentReportAction.IncidentReportContent = GenerateIncidentReport.GetLegacyContentItems(legacyReportOriginalMail).ToArray();
			}
			else
			{
				for (int i = 1; i < action.Arguments.Count; i++)
				{
					IncidentReportContent contentItem;
					if (Enum.TryParse<IncidentReportContent>(TransportRuleAction.GetStringValue(action.Arguments[i]), out contentItem))
					{
						generateIncidentReportAction.AddIncidentReportContentItem(contentItem);
					}
				}
				if (generateIncidentReportAction.IncidentReportContent.Length == 0)
				{
					generateIncidentReportAction.AddIncidentReportContentItem(Microsoft.Exchange.MessagingPolicies.Rules.IncidentReportContent.RuleDetections);
				}
			}
			generateIncidentReportAction.IncidentReportOriginalMail = (generateIncidentReportAction.IncidentReportContent.Contains(Microsoft.Exchange.MessagingPolicies.Rules.IncidentReportContent.AttachOriginalMail) ? IncidentReportOriginalMail.IncludeOriginalMail : IncidentReportOriginalMail.DoNotIncludeOriginalMail);
			return generateIncidentReportAction;
		}

		// Token: 0x06006F89 RID: 28553 RVA: 0x001C939C File Offset: 0x001C759C
		internal override void Reset()
		{
			this.ReportDestination = default(SmtpAddress);
			this.IncidentReportOriginalMail = IncidentReportOriginalMail.DoNotIncludeOriginalMail;
			this.IncidentReportContent = new IncidentReportContent[0];
			base.Reset();
		}

		// Token: 0x06006F8A RID: 28554 RVA: 0x001C93D8 File Offset: 0x001C75D8
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.ReportDestination == SmtpAddress.Empty)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			if (this.IncidentReportOriginalMail == IncidentReportOriginalMail.DoNotIncludeOriginalMail)
			{
				if (this.IncidentReportContent.Any((IncidentReportContent reportContent) => reportContent == Microsoft.Exchange.MessagingPolicies.Rules.IncidentReportContent.AttachOriginalMail))
				{
					errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.IncidentReportConflictingParameters("IncidentReportOriginalMail", "IncidentReportContent"), base.Name));
				}
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06006F8B RID: 28555 RVA: 0x001C9468 File Offset: 0x001C7668
		internal override Action ToInternalAction()
		{
			ShortList<Argument> shortList = new ShortList<Argument>
			{
				new Value(this.ReportDestination.ToString())
			};
			foreach (IncidentReportContent incidentReportContent in this.IncidentReportContent)
			{
				shortList.Add(new Value(Enum.GetName(typeof(IncidentReportContent), incidentReportContent)));
			}
			if (this.IncidentReportOriginalMail == IncidentReportOriginalMail.IncludeOriginalMail && !this.IncidentReportContent.Contains(Microsoft.Exchange.MessagingPolicies.Rules.IncidentReportContent.AttachOriginalMail))
			{
				this.AddIncidentReportContentItem(Microsoft.Exchange.MessagingPolicies.Rules.IncidentReportContent.AttachOriginalMail);
				shortList.Add(new Value(Enum.GetName(typeof(IncidentReportContent), Microsoft.Exchange.MessagingPolicies.Rules.IncidentReportContent.AttachOriginalMail)));
			}
			return TransportRuleParser.Instance.CreateAction("GenerateIncidentReport", shortList, null);
		}

		// Token: 0x06006F8C RID: 28556 RVA: 0x001C9548 File Offset: 0x001C7748
		internal override string ToCmdletParameter()
		{
			string text = string.Empty;
			if (this.incidentReportContent.Any<IncidentReportContent>())
			{
				text = string.Format("-{0} ", "IncidentReportContent");
				text += string.Join(",", (from item in this.IncidentReportContent
				select Enum.GetName(typeof(IncidentReportContent), item)).ToArray<string>());
			}
			return string.Format("-{0} {1} {2}", "GenerateIncidentReport", Utils.QuoteCmdletParameter(this.ReportDestination.ToString()), text);
		}

		// Token: 0x06006F8D RID: 28557 RVA: 0x001C95E0 File Offset: 0x001C77E0
		internal override void SuppressPiiData()
		{
			string text;
			string text2;
			this.ReportDestination = SuppressingPiiData.Redact(this.ReportDestination, out text, out text2);
		}

		// Token: 0x040039B6 RID: 14774
		private static readonly string IncidentReportContentDescriptionSeparator = ", ";

		// Token: 0x040039B7 RID: 14775
		private List<IncidentReportContent> incidentReportContent = new List<IncidentReportContent>();
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B83 RID: 2947
	[Serializable]
	public class ApplyClassificationAction : TransportRuleAction, IEquatable<ApplyClassificationAction>
	{
		// Token: 0x06006F22 RID: 28450 RVA: 0x001C8522 File Offset: 0x001C6722
		public ApplyClassificationAction(IConfigDataProvider session)
		{
			this.session = session;
		}

		// Token: 0x06006F23 RID: 28451 RVA: 0x001C8531 File Offset: 0x001C6731
		public override int GetHashCode()
		{
			if (this.Classification != null)
			{
				return this.Classification.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06006F24 RID: 28452 RVA: 0x001C8548 File Offset: 0x001C6748
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as ApplyClassificationAction)));
		}

		// Token: 0x06006F25 RID: 28453 RVA: 0x001C8581 File Offset: 0x001C6781
		public bool Equals(ApplyClassificationAction other)
		{
			if (this.Classification == null)
			{
				return null == other.Classification;
			}
			return this.Classification.Equals(other.Classification);
		}

		// Token: 0x170022B8 RID: 8888
		// (get) Token: 0x06006F26 RID: 28454 RVA: 0x001C85A6 File Offset: 0x001C67A6
		// (set) Token: 0x06006F27 RID: 28455 RVA: 0x001C85AE File Offset: 0x001C67AE
		[LocDescription(RulesTasksStrings.IDs.ClassificationDescription)]
		[ActionParameterName("ApplyClassification")]
		[LocDisplayName(RulesTasksStrings.IDs.ClassificationDisplayName)]
		public ADObjectId Classification
		{
			get
			{
				return this.classification;
			}
			set
			{
				this.classification = value;
			}
		}

		// Token: 0x170022B9 RID: 8889
		// (get) Token: 0x06006F28 RID: 28456 RVA: 0x001C85B7 File Offset: 0x001C67B7
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionApplyClassification(Utils.GetClassificationDisplayName(this.Classification, this.session));
			}
		}

		// Token: 0x06006F29 RID: 28457 RVA: 0x001C85D4 File Offset: 0x001C67D4
		internal static TransportRuleAction CreateFromInternalActionWithSession(Action action, IConfigDataProvider session)
		{
			if (action.Name != "SetHeaderUniqueValue" || TransportRuleAction.GetStringValue(action.Arguments[0]) != "X-MS-Exchange-Organization-Classification")
			{
				return null;
			}
			return new ApplyClassificationAction(session)
			{
				Classification = Utils.GetClassificationADObjectId(TransportRuleAction.GetStringValue(action.Arguments[1]), session)
			};
		}

		// Token: 0x06006F2A RID: 28458 RVA: 0x001C8637 File Offset: 0x001C6837
		internal override void Reset()
		{
			this.classification = null;
			base.Reset();
		}

		// Token: 0x06006F2B RID: 28459 RVA: 0x001C8646 File Offset: 0x001C6846
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.classification == null)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06006F2C RID: 28460 RVA: 0x001C8670 File Offset: 0x001C6870
		internal override Action ToInternalAction()
		{
			string classificationId = Utils.GetClassificationId(this.classification, this.session);
			if (string.IsNullOrEmpty(classificationId))
			{
				throw new ArgumentException(RulesTasksStrings.InvalidClassification, "Classification");
			}
			ShortList<Argument> arguments = new ShortList<Argument>
			{
				new Value("X-MS-Exchange-Organization-Classification"),
				new Value(classificationId)
			};
			return TransportRuleParser.Instance.CreateAction("SetHeaderUniqueValue", arguments, Utils.GetActionName(this));
		}

		// Token: 0x06006F2D RID: 28461 RVA: 0x001C86E6 File Offset: 0x001C68E6
		internal override string GetActionParameters()
		{
			return this.Classification.ToString();
		}

		// Token: 0x040039A6 RID: 14758
		private ADObjectId classification;

		// Token: 0x040039A7 RID: 14759
		[NonSerialized]
		private readonly IConfigDataProvider session;
	}
}

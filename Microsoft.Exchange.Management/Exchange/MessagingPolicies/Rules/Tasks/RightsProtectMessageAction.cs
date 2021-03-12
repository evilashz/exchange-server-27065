using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.RightsManagement;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B9B RID: 2971
	[Serializable]
	public sealed class RightsProtectMessageAction : TransportRuleAction, IEquatable<RightsProtectMessageAction>
	{
		// Token: 0x0600700C RID: 28684 RVA: 0x001CAC02 File Offset: 0x001C8E02
		public override int GetHashCode()
		{
			if (this.Template != null)
			{
				return this.Template.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600700D RID: 28685 RVA: 0x001CAC19 File Offset: 0x001C8E19
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as RightsProtectMessageAction)));
		}

		// Token: 0x0600700E RID: 28686 RVA: 0x001CAC52 File Offset: 0x001C8E52
		public bool Equals(RightsProtectMessageAction other)
		{
			if (this.Template == null)
			{
				return null == other.Template;
			}
			return this.Template.Equals(other.Template);
		}

		// Token: 0x170022E1 RID: 8929
		// (get) Token: 0x0600700F RID: 28687 RVA: 0x001CAC77 File Offset: 0x001C8E77
		// (set) Token: 0x06007010 RID: 28688 RVA: 0x001CAC7F File Offset: 0x001C8E7F
		[LocDisplayName(RulesTasksStrings.IDs.RmsTemplateDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.RmsTemplateDescription)]
		[ActionParameterName("ApplyRightsProtectionTemplate")]
		public RmsTemplateIdentity Template
		{
			get
			{
				return this.template;
			}
			set
			{
				this.template = value;
			}
		}

		// Token: 0x170022E2 RID: 8930
		// (get) Token: 0x06007011 RID: 28689 RVA: 0x001CAC88 File Offset: 0x001C8E88
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionRightsProtectMessage(this.template.TemplateName);
			}
		}

		// Token: 0x06007012 RID: 28690 RVA: 0x001CACA0 File Offset: 0x001C8EA0
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "RightsProtectMessage")
			{
				return null;
			}
			if (action.Arguments == null || action.Arguments.Count < 3)
			{
				return null;
			}
			TransportRuleAction result;
			try
			{
				RightsProtectMessageAction rightsProtectMessageAction = new RightsProtectMessageAction();
				Guid templateId = new Guid(TransportRuleAction.GetStringValue(action.Arguments[1]));
				string stringValue = TransportRuleAction.GetStringValue(action.Arguments[2]);
				rightsProtectMessageAction.Template = new RmsTemplateIdentity(templateId, stringValue);
				result = rightsProtectMessageAction;
			}
			catch (FormatException)
			{
				result = null;
			}
			catch (OverflowException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06007013 RID: 28691 RVA: 0x001CAD44 File Offset: 0x001C8F44
		internal override void Reset()
		{
			this.template = new RmsTemplateIdentity();
			base.Reset();
		}

		// Token: 0x06007014 RID: 28692 RVA: 0x001CAD57 File Offset: 0x001C8F57
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.template.TemplateId == Guid.Empty)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06007015 RID: 28693 RVA: 0x001CAD90 File Offset: 0x001C8F90
		internal override Action ToInternalAction()
		{
			if (this.template.TemplateId == Guid.Empty)
			{
				throw new ArgumentException(RulesTasksStrings.InvalidRmsTemplate, "TemplateId");
			}
			ShortList<Argument> arguments = new ShortList<Argument>
			{
				new Value("X-MS-Exchange-Organization-RightsProtectMessage"),
				new Value(this.template.TemplateId.ToString("D", CultureInfo.InvariantCulture)),
				new Value(this.template.TemplateName)
			};
			return TransportRuleParser.Instance.CreateAction("RightsProtectMessage", arguments, Utils.GetActionName(this));
		}

		// Token: 0x06007016 RID: 28694 RVA: 0x001CAE38 File Offset: 0x001C9038
		internal override string GetActionParameters()
		{
			return this.Template.TemplateId.ToString();
		}

		// Token: 0x06007017 RID: 28695 RVA: 0x001CAE5E File Offset: 0x001C905E
		internal override void SuppressPiiData()
		{
			this.template = SuppressingPiiProperty.TryRedactValue<RmsTemplateIdentity>(RuleSchema.ApplyRightsProtectionTemplate, this.template);
		}

		// Token: 0x040039D6 RID: 14806
		private RmsTemplateIdentity template;
	}
}

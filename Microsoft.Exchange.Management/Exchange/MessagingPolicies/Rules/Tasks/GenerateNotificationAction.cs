using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B90 RID: 2960
	[Serializable]
	public class GenerateNotificationAction : TransportRuleAction, IEquatable<GenerateNotificationAction>
	{
		// Token: 0x06006F92 RID: 28562 RVA: 0x001C960E File Offset: 0x001C780E
		public GenerateNotificationAction()
		{
		}

		// Token: 0x06006F93 RID: 28563 RVA: 0x001C9616 File Offset: 0x001C7816
		internal GenerateNotificationAction(DisclaimerText notificationContent)
		{
			this.NotificationContent = notificationContent;
		}

		// Token: 0x06006F94 RID: 28564 RVA: 0x001C9628 File Offset: 0x001C7828
		public override int GetHashCode()
		{
			return this.NotificationContent.GetHashCode();
		}

		// Token: 0x06006F95 RID: 28565 RVA: 0x001C9649 File Offset: 0x001C7849
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as GenerateNotificationAction)));
		}

		// Token: 0x06006F96 RID: 28566 RVA: 0x001C9684 File Offset: 0x001C7884
		public bool Equals(GenerateNotificationAction other)
		{
			return this.NotificationContent.Equals(other.NotificationContent);
		}

		// Token: 0x170022CC RID: 8908
		// (get) Token: 0x06006F97 RID: 28567 RVA: 0x001C96A5 File Offset: 0x001C78A5
		// (set) Token: 0x06006F98 RID: 28568 RVA: 0x001C96AD File Offset: 0x001C78AD
		[LocDisplayName(RulesTasksStrings.IDs.GenerateNotificationDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.GenerateNotificationDescription)]
		[ActionParameterName("GenerateNotification")]
		public DisclaimerText NotificationContent { get; set; }

		// Token: 0x170022CD RID: 8909
		// (get) Token: 0x06006F99 RID: 28569 RVA: 0x001C96B8 File Offset: 0x001C78B8
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionGenerateNotification(this.NotificationContent.Value.ToString());
			}
		}

		// Token: 0x06006F9A RID: 28570 RVA: 0x001C96E4 File Offset: 0x001C78E4
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "GenerateNotification" || !action.Arguments.Any<Argument>())
			{
				return null;
			}
			TransportRuleAction result;
			try
			{
				result = new GenerateNotificationAction(new DisclaimerText(TransportRuleAction.GetStringValue(action.Arguments[0])));
			}
			catch (ArgumentOutOfRangeException)
			{
				result = null;
			}
			catch (ArgumentException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06006F9B RID: 28571 RVA: 0x001C9758 File Offset: 0x001C7958
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (string.IsNullOrWhiteSpace(this.NotificationContent.ToString()))
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06006F9C RID: 28572 RVA: 0x001C97A0 File Offset: 0x001C79A0
		internal override Action ToInternalAction()
		{
			ShortList<Argument> arguments = new ShortList<Argument>
			{
				new Value(this.NotificationContent.ToString())
			};
			return TransportRuleParser.Instance.CreateAction("GenerateNotification", arguments, Utils.GetActionName(this));
		}

		// Token: 0x06006F9D RID: 28573 RVA: 0x001C97EC File Offset: 0x001C79EC
		internal override string ToCmdletParameter()
		{
			return string.Format("-{0} {1}", "GenerateNotification", Utils.QuoteCmdletParameter(this.NotificationContent.ToString()));
		}

		// Token: 0x06006F9E RID: 28574 RVA: 0x001C9821 File Offset: 0x001C7A21
		internal override void SuppressPiiData()
		{
			this.NotificationContent = SuppressingPiiProperty.TryRedactValue<DisclaimerText>(RuleSchema.GenerateNotification, this.NotificationContent);
		}
	}
}

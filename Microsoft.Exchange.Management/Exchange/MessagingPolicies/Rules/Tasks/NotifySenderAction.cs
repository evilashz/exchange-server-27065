using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B95 RID: 2965
	[Serializable]
	public class NotifySenderAction : TransportRuleAction, IEquatable<NotifySenderAction>
	{
		// Token: 0x06006FC0 RID: 28608 RVA: 0x001C9E12 File Offset: 0x001C8012
		public NotifySenderAction()
		{
			this.Reset();
		}

		// Token: 0x06006FC1 RID: 28609 RVA: 0x001C9E20 File Offset: 0x001C8020
		public override int GetHashCode()
		{
			return this.SenderNotificationType.GetHashCode() ^ this.RejectReason.GetHashCode() ^ this.EnhancedStatusCode.GetHashCode();
		}

		// Token: 0x06006FC2 RID: 28610 RVA: 0x001C9E67 File Offset: 0x001C8067
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as NotifySenderAction)));
		}

		// Token: 0x06006FC3 RID: 28611 RVA: 0x001C9EA0 File Offset: 0x001C80A0
		public bool Equals(NotifySenderAction other)
		{
			return this.SenderNotificationType.Equals(other.SenderNotificationType) && this.RejectReason.Equals(other.RejectReason) && this.EnhancedStatusCode.Equals(other.EnhancedStatusCode);
		}

		// Token: 0x170022D3 RID: 8915
		// (get) Token: 0x06006FC4 RID: 28612 RVA: 0x001C9EF6 File Offset: 0x001C80F6
		// (set) Token: 0x06006FC5 RID: 28613 RVA: 0x001C9EFE File Offset: 0x001C80FE
		[LocDescription(RulesTasksStrings.IDs.SenderNotificationTypeDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.SenderNotificationTypeDisplayName)]
		[ActionParameterName("NotifySender")]
		public NotifySenderType SenderNotificationType { get; set; }

		// Token: 0x170022D4 RID: 8916
		// (get) Token: 0x06006FC6 RID: 28614 RVA: 0x001C9F07 File Offset: 0x001C8107
		// (set) Token: 0x06006FC7 RID: 28615 RVA: 0x001C9F0F File Offset: 0x001C810F
		[LocDisplayName(RulesTasksStrings.IDs.RejectReasonDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.RejectReasonDescription)]
		[ActionParameterName("RejectMessageReasonText")]
		public DsnText RejectReason { get; set; }

		// Token: 0x170022D5 RID: 8917
		// (get) Token: 0x06006FC8 RID: 28616 RVA: 0x001C9F18 File Offset: 0x001C8118
		// (set) Token: 0x06006FC9 RID: 28617 RVA: 0x001C9F20 File Offset: 0x001C8120
		[ActionParameterName("RejectMessageEnhancedStatusCode")]
		[LocDescription(RulesTasksStrings.IDs.EnhancedStatusCodeDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.EnhancedStatusCodeDisplayName)]
		public RejectEnhancedStatus EnhancedStatusCode { get; set; }

		// Token: 0x170022D6 RID: 8918
		// (get) Token: 0x06006FCA RID: 28618 RVA: 0x001C9F2C File Offset: 0x001C812C
		internal override string Description
		{
			get
			{
				switch (this.SenderNotificationType)
				{
				case NotifySenderType.NotifyOnly:
					return RulesTasksStrings.RuleDescriptionNotifySenderNotifyOnly;
				case NotifySenderType.RejectMessage:
					return RulesTasksStrings.RuleDescriptionNotifySenderRejectMessage(this.RejectReason.Value, this.EnhancedStatusCode.ToString());
				case NotifySenderType.RejectUnlessFalsePositiveOverride:
					return RulesTasksStrings.RuleDescriptionNotifySenderRejectUnlessFalsePositiveOverride(this.RejectReason.Value, this.EnhancedStatusCode.ToString());
				case NotifySenderType.RejectUnlessSilentOverride:
					return RulesTasksStrings.RuleDescriptionNotifySenderRejectUnlessSilentOverride(this.RejectReason.Value, this.EnhancedStatusCode.ToString());
				case NotifySenderType.RejectUnlessExplicitOverride:
					return RulesTasksStrings.RuleDescriptionNotifySenderRejectUnlessExplicitOverride(this.RejectReason.Value, this.EnhancedStatusCode.ToString());
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x06006FCB RID: 28619 RVA: 0x001CA02C File Offset: 0x001C822C
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "SenderNotify" || !TransportRuleAction.GetStringValue(action.Arguments[1]).Equals("550"))
			{
				return null;
			}
			NotifySenderAction notifySenderAction = new NotifySenderAction();
			try
			{
				notifySenderAction.SenderNotificationType = (NotifySenderType)Enum.Parse(typeof(NotifySenderType), TransportRuleAction.GetStringValue(action.Arguments[0]));
				notifySenderAction.EnhancedStatusCode = new RejectEnhancedStatus(TransportRuleAction.GetStringValue(action.Arguments[2]));
				notifySenderAction.RejectReason = new DsnText(TransportRuleAction.GetStringValue(action.Arguments[3]));
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
			catch (ArgumentException)
			{
				return null;
			}
			catch (OverflowException)
			{
				return null;
			}
			return notifySenderAction;
		}

		// Token: 0x06006FCC RID: 28620 RVA: 0x001CA110 File Offset: 0x001C8310
		internal override void Reset()
		{
			this.RejectReason = new DsnText(NotifySenderAction.defaultRejectText.Value);
			this.EnhancedStatusCode = NotifySenderAction.defaultEnhancedStatusCode;
			this.SenderNotificationType = NotifySenderType.NotifyOnly;
			base.Reset();
		}

		// Token: 0x06006FCD RID: 28621 RVA: 0x001CA150 File Offset: 0x001C8350
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (string.IsNullOrWhiteSpace(this.RejectReason.Value) || this.EnhancedStatusCode == RejectEnhancedStatus.Empty)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06006FCE RID: 28622 RVA: 0x001CA1A4 File Offset: 0x001C83A4
		internal override Action ToInternalAction()
		{
			ShortList<Argument> arguments = new ShortList<Argument>
			{
				new Value(Enum.GetName(typeof(NotifySenderType), this.SenderNotificationType)),
				new Value("550"),
				new Value(this.EnhancedStatusCode.ToString()),
				new Value(this.RejectReason.ToString())
			};
			return TransportRuleParser.Instance.CreateAction("SenderNotify", arguments, Utils.GetActionName(this));
		}

		// Token: 0x06006FCF RID: 28623 RVA: 0x001CA244 File Offset: 0x001C8444
		internal override string ToCmdletParameter()
		{
			return string.Format("-{0} {1} -{2} {3} -{4} {5}", new object[]
			{
				"NotifySender",
				Enum.GetName(typeof(NotifySenderType), this.SenderNotificationType),
				"RejectMessageReasonText",
				Utils.QuoteCmdletParameter(this.RejectReason.ToString()),
				"RejectMessageEnhancedStatusCode",
				Utils.QuoteCmdletParameter(this.EnhancedStatusCode.ToString())
			});
		}

		// Token: 0x06006FD0 RID: 28624 RVA: 0x001CA2D0 File Offset: 0x001C84D0
		internal override void SuppressPiiData()
		{
			this.RejectReason = SuppressingPiiProperty.TryRedactValue<DsnText>(RuleSchema.RejectMessageReasonText, this.RejectReason);
		}

		// Token: 0x040039C8 RID: 14792
		private const string StatusCode = "550";

		// Token: 0x040039C9 RID: 14793
		private static readonly RejectText defaultRejectText = new RejectText("Delivery not authorized, message refused");

		// Token: 0x040039CA RID: 14794
		private static readonly RejectEnhancedStatus defaultEnhancedStatusCode = new RejectEnhancedStatus("5.7.1");
	}
}

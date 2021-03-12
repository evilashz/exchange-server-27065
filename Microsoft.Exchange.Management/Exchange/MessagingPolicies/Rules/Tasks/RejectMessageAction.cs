using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B99 RID: 2969
	[Serializable]
	public class RejectMessageAction : TransportRuleAction, IEquatable<RejectMessageAction>
	{
		// Token: 0x06006FEF RID: 28655 RVA: 0x001CA6B8 File Offset: 0x001C88B8
		public override int GetHashCode()
		{
			return this.RejectReason.GetHashCode() ^ this.EnhancedStatusCode.GetHashCode();
		}

		// Token: 0x06006FF0 RID: 28656 RVA: 0x001CA6EE File Offset: 0x001C88EE
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as RejectMessageAction)));
		}

		// Token: 0x06006FF1 RID: 28657 RVA: 0x001CA728 File Offset: 0x001C8928
		public bool Equals(RejectMessageAction other)
		{
			return this.RejectReason.Equals(other.RejectReason) && this.EnhancedStatusCode.Equals(other.EnhancedStatusCode);
		}

		// Token: 0x170022DC RID: 8924
		// (get) Token: 0x06006FF2 RID: 28658 RVA: 0x001CA761 File Offset: 0x001C8961
		// (set) Token: 0x06006FF3 RID: 28659 RVA: 0x001CA769 File Offset: 0x001C8969
		[LocDescription(RulesTasksStrings.IDs.RejectReasonDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.RejectReasonDisplayName)]
		[ActionParameterName("RejectMessageReasonText")]
		public DsnText RejectReason
		{
			get
			{
				return this.rejectReason;
			}
			set
			{
				this.rejectReason = value;
			}
		}

		// Token: 0x170022DD RID: 8925
		// (get) Token: 0x06006FF4 RID: 28660 RVA: 0x001CA772 File Offset: 0x001C8972
		// (set) Token: 0x06006FF5 RID: 28661 RVA: 0x001CA77A File Offset: 0x001C897A
		[ActionParameterName("RejectMessageEnhancedStatusCode")]
		[LocDisplayName(RulesTasksStrings.IDs.EnhancedStatusCodeDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.EnhancedStatusCodeDescription)]
		public RejectEnhancedStatus EnhancedStatusCode
		{
			get
			{
				return this.enhancedStatusCode;
			}
			set
			{
				this.enhancedStatusCode = value;
			}
		}

		// Token: 0x170022DE RID: 8926
		// (get) Token: 0x06006FF6 RID: 28662 RVA: 0x001CA784 File Offset: 0x001C8984
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionRejectMessage(this.RejectReason.ToString(), this.EnhancedStatusCode.ToString());
			}
		}

		// Token: 0x06006FF7 RID: 28663 RVA: 0x001CA7C4 File Offset: 0x001C89C4
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "RejectMessage" || !TransportRuleAction.GetStringValue(action.Arguments[0]).Equals("550"))
			{
				return null;
			}
			RejectMessageAction rejectMessageAction = new RejectMessageAction();
			try
			{
				rejectMessageAction.RejectReason = new DsnText(TransportRuleAction.GetStringValue(action.Arguments[2]));
				rejectMessageAction.EnhancedStatusCode = new RejectEnhancedStatus(TransportRuleAction.GetStringValue(action.Arguments[1]));
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
			catch (ArgumentException)
			{
				return null;
			}
			return rejectMessageAction;
		}

		// Token: 0x06006FF8 RID: 28664 RVA: 0x001CA86C File Offset: 0x001C8A6C
		internal override void Reset()
		{
			this.rejectReason = new DsnText(RejectMessageAction.defaultRejectText.Value);
			this.enhancedStatusCode = RejectMessageAction.defaultEnhancedStatusCode;
			base.Reset();
		}

		// Token: 0x06006FF9 RID: 28665 RVA: 0x001CA8A4 File Offset: 0x001C8AA4
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (string.IsNullOrEmpty(this.RejectReason.Value) || this.EnhancedStatusCode == RejectEnhancedStatus.Empty)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06006FFA RID: 28666 RVA: 0x001CA8F8 File Offset: 0x001C8AF8
		internal override Action ToInternalAction()
		{
			ShortList<Argument> arguments = new ShortList<Argument>
			{
				new Value("550"),
				new Value(this.EnhancedStatusCode.ToString()),
				new Value(this.RejectReason.Value)
			};
			return TransportRuleParser.Instance.CreateAction("RejectMessage", arguments, Utils.GetActionName(this));
		}

		// Token: 0x06006FFB RID: 28667 RVA: 0x001CA96C File Offset: 0x001C8B6C
		internal override string ToCmdletParameter()
		{
			return string.Format("-{0} {1} -{2} {3}", new object[]
			{
				"RejectMessageReasonText",
				Utils.QuoteCmdletParameter(this.RejectReason.Value),
				"RejectMessageEnhancedStatusCode",
				Utils.QuoteCmdletParameter(this.EnhancedStatusCode.ToString())
			});
		}

		// Token: 0x06006FFC RID: 28668 RVA: 0x001CA9CD File Offset: 0x001C8BCD
		internal override void SuppressPiiData()
		{
			this.RejectReason = SuppressingPiiProperty.TryRedactValue<DsnText>(RuleSchema.RejectMessageReasonText, this.RejectReason);
		}

		// Token: 0x040039D0 RID: 14800
		private const string StatusCode = "550";

		// Token: 0x040039D1 RID: 14801
		private static readonly DsnText defaultRejectText = new DsnText("Delivery not authorized, message refused");

		// Token: 0x040039D2 RID: 14802
		private static readonly RejectEnhancedStatus defaultEnhancedStatusCode = new RejectEnhancedStatus("5.7.1");

		// Token: 0x040039D3 RID: 14803
		private DsnText rejectReason;

		// Token: 0x040039D4 RID: 14804
		private RejectEnhancedStatus enhancedStatusCode;
	}
}

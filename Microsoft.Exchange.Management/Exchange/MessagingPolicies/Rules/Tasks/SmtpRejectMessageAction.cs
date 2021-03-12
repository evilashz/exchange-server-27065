using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BA1 RID: 2977
	[Serializable]
	public class SmtpRejectMessageAction : TransportRuleAction, IEquatable<SmtpRejectMessageAction>
	{
		// Token: 0x06007052 RID: 28754 RVA: 0x001CB7A8 File Offset: 0x001C99A8
		public override int GetHashCode()
		{
			return this.StatusCode.GetHashCode() ^ this.RejectReason.GetHashCode();
		}

		// Token: 0x06007053 RID: 28755 RVA: 0x001CB7DE File Offset: 0x001C99DE
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SmtpRejectMessageAction)));
		}

		// Token: 0x06007054 RID: 28756 RVA: 0x001CB818 File Offset: 0x001C9A18
		public bool Equals(SmtpRejectMessageAction other)
		{
			return this.StatusCode.Equals(other.StatusCode) && this.RejectReason.Equals(other.RejectReason);
		}

		// Token: 0x170022ED RID: 8941
		// (get) Token: 0x06007055 RID: 28757 RVA: 0x001CB851 File Offset: 0x001C9A51
		// (set) Token: 0x06007056 RID: 28758 RVA: 0x001CB859 File Offset: 0x001C9A59
		[ActionParameterName("SmtpRejectMessageRejectStatusCode")]
		[LocDescription(RulesTasksStrings.IDs.StatusCodeDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.StatusCodeDisplayName)]
		public RejectStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				this.statusCode = value;
			}
		}

		// Token: 0x170022EE RID: 8942
		// (get) Token: 0x06007057 RID: 28759 RVA: 0x001CB862 File Offset: 0x001C9A62
		// (set) Token: 0x06007058 RID: 28760 RVA: 0x001CB86A File Offset: 0x001C9A6A
		[LocDescription(RulesTasksStrings.IDs.RejectReasonDescription)]
		[ActionParameterName("SmtpRejectMessageRejectText")]
		[LocDisplayName(RulesTasksStrings.IDs.RejectReasonDisplayName)]
		public RejectText RejectReason
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

		// Token: 0x170022EF RID: 8943
		// (get) Token: 0x06007059 RID: 28761 RVA: 0x001CB874 File Offset: 0x001C9A74
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionRejectMessage(this.RejectReason.ToString(), this.StatusCode.ToString());
			}
		}

		// Token: 0x0600705A RID: 28762 RVA: 0x001CB8B4 File Offset: 0x001C9AB4
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "RejectMessage" || !TransportRuleAction.GetStringValue(action.Arguments[1]).Equals(SmtpRejectMessageAction.EnhancedStatusCode))
			{
				return null;
			}
			SmtpRejectMessageAction smtpRejectMessageAction = new SmtpRejectMessageAction();
			try
			{
				smtpRejectMessageAction.StatusCode = new RejectStatusCode(TransportRuleAction.GetStringValue(action.Arguments[0]));
				smtpRejectMessageAction.RejectReason = new RejectText(TransportRuleAction.GetStringValue(action.Arguments[2]));
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
			return smtpRejectMessageAction;
		}

		// Token: 0x0600705B RID: 28763 RVA: 0x001CB94C File Offset: 0x001C9B4C
		internal override void Reset()
		{
			this.statusCode = SmtpRejectMessageAction.defaultStatusCode;
			this.rejectReason = SmtpRejectMessageAction.defaultRejectText;
			base.Reset();
		}

		// Token: 0x0600705C RID: 28764 RVA: 0x001CB96C File Offset: 0x001C9B6C
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.RejectReason == RejectText.Empty || this.StatusCode == RejectStatusCode.Empty)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x0600705D RID: 28765 RVA: 0x001CB9BC File Offset: 0x001C9BBC
		internal override Action ToInternalAction()
		{
			ShortList<Argument> arguments = new ShortList<Argument>
			{
				new Value(this.StatusCode.ToString()),
				new Value(SmtpRejectMessageAction.EnhancedStatusCode),
				new Value(this.RejectReason.ToString())
			};
			return TransportRuleParser.Instance.CreateAction("RejectMessage", arguments, Utils.GetActionName(this));
		}

		// Token: 0x0600705E RID: 28766 RVA: 0x001CBA38 File Offset: 0x001C9C38
		internal override string ToCmdletParameter()
		{
			return string.Format("-{0} {1} -{2} {3}", new object[]
			{
				"SmtpRejectMessageRejectText",
				Utils.QuoteCmdletParameter(this.RejectReason.ToString()),
				"SmtpRejectMessageRejectStatusCode",
				this.StatusCode.ToString()
			});
		}

		// Token: 0x0600705F RID: 28767 RVA: 0x001CBA9A File Offset: 0x001C9C9A
		internal override void SuppressPiiData()
		{
			this.RejectReason = SuppressingPiiProperty.TryRedactValue<RejectText>(RuleSchema.SmtpRejectMessageRejectText, this.RejectReason);
		}

		// Token: 0x040039DC RID: 14812
		private static readonly string EnhancedStatusCode = string.Empty;

		// Token: 0x040039DD RID: 14813
		private static readonly RejectStatusCode defaultStatusCode = new RejectStatusCode("550");

		// Token: 0x040039DE RID: 14814
		private static readonly RejectText defaultRejectText = new RejectText("Delivery not authorized, message refused");

		// Token: 0x040039DF RID: 14815
		private RejectStatusCode statusCode;

		// Token: 0x040039E0 RID: 14816
		private RejectText rejectReason;
	}
}

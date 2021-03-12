using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B9F RID: 2975
	[Serializable]
	public class SetHeaderAction : TransportRuleAction, IEquatable<SetHeaderAction>
	{
		// Token: 0x06007038 RID: 28728 RVA: 0x001CB288 File Offset: 0x001C9488
		public override int GetHashCode()
		{
			return this.MessageHeader.GetHashCode() ^ this.HeaderValue.GetHashCode();
		}

		// Token: 0x06007039 RID: 28729 RVA: 0x001CB2BE File Offset: 0x001C94BE
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SetHeaderAction)));
		}

		// Token: 0x0600703A RID: 28730 RVA: 0x001CB2F8 File Offset: 0x001C94F8
		public bool Equals(SetHeaderAction other)
		{
			return this.MessageHeader.Equals(other.MessageHeader) && this.HeaderValue.Equals(other.HeaderValue);
		}

		// Token: 0x170022E8 RID: 8936
		// (get) Token: 0x0600703B RID: 28731 RVA: 0x001CB331 File Offset: 0x001C9531
		// (set) Token: 0x0600703C RID: 28732 RVA: 0x001CB339 File Offset: 0x001C9539
		[ActionParameterName("SetHeaderName")]
		[LocDescription(RulesTasksStrings.IDs.MessageHeaderDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.MessageHeaderDisplayName)]
		public HeaderName MessageHeader
		{
			get
			{
				return this.messageHeader;
			}
			set
			{
				this.messageHeader = value;
			}
		}

		// Token: 0x170022E9 RID: 8937
		// (get) Token: 0x0600703D RID: 28733 RVA: 0x001CB342 File Offset: 0x001C9542
		// (set) Token: 0x0600703E RID: 28734 RVA: 0x001CB34A File Offset: 0x001C954A
		[LocDisplayName(RulesTasksStrings.IDs.HeaderValueDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.HeaderValueDescription)]
		[ActionParameterName("SetHeaderValue")]
		public HeaderValue HeaderValue
		{
			get
			{
				return this.headerValue;
			}
			set
			{
				this.headerValue = value;
			}
		}

		// Token: 0x170022EA RID: 8938
		// (get) Token: 0x0600703F RID: 28735 RVA: 0x001CB354 File Offset: 0x001C9554
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionSetHeader(this.MessageHeader.ToString(), this.HeaderValue.ToString());
			}
		}

		// Token: 0x06007040 RID: 28736 RVA: 0x001CB394 File Offset: 0x001C9594
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "SetHeader")
			{
				return null;
			}
			SetHeaderAction setHeaderAction = new SetHeaderAction();
			try
			{
				setHeaderAction.MessageHeader = new HeaderName(TransportRuleAction.GetStringValue(action.Arguments[0]));
				setHeaderAction.HeaderValue = new HeaderValue(TransportRuleAction.GetStringValue(action.Arguments[1]));
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
			return setHeaderAction;
		}

		// Token: 0x06007041 RID: 28737 RVA: 0x001CB410 File Offset: 0x001C9610
		internal override void Reset()
		{
			this.messageHeader = HeaderName.Empty;
			this.headerValue = HeaderValue.Empty;
			base.Reset();
		}

		// Token: 0x06007042 RID: 28738 RVA: 0x001CB430 File Offset: 0x001C9630
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.MessageHeader == HeaderName.Empty || this.HeaderValue == HeaderValue.Empty)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			int index;
			if (!Utils.CheckIsUnicodeStringWellFormed(this.headerValue.Value, out index))
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.CommentsHaveInvalidChars((int)this.headerValue.Value[index]), base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06007043 RID: 28739 RVA: 0x001CB4BC File Offset: 0x001C96BC
		internal override Action ToInternalAction()
		{
			ShortList<Argument> arguments = new ShortList<Argument>
			{
				new Value(this.MessageHeader.ToString()),
				new Value(this.HeaderValue.ToString())
			};
			return TransportRuleParser.Instance.CreateAction("SetHeader", arguments, Utils.GetActionName(this));
		}

		// Token: 0x06007044 RID: 28740 RVA: 0x001CB528 File Offset: 0x001C9728
		internal override string ToCmdletParameter()
		{
			return string.Format("-{0} {1} -{2} {3}", new object[]
			{
				"SetHeaderName",
				Utils.QuoteCmdletParameter(this.MessageHeader.ToString()),
				"SetHeaderValue",
				Utils.QuoteCmdletParameter(this.HeaderValue.ToString())
			});
		}

		// Token: 0x06007045 RID: 28741 RVA: 0x001CB58F File Offset: 0x001C978F
		internal override void SuppressPiiData()
		{
			this.MessageHeader = SuppressingPiiProperty.TryRedactValue<HeaderName>(RuleSchema.SetHeaderName, this.MessageHeader);
			this.HeaderValue = SuppressingPiiProperty.TryRedactValue<HeaderValue>(RuleSchema.SetHeaderValue, this.HeaderValue);
		}

		// Token: 0x040039D9 RID: 14809
		private HeaderName messageHeader;

		// Token: 0x040039DA RID: 14810
		private HeaderValue headerValue;
	}
}

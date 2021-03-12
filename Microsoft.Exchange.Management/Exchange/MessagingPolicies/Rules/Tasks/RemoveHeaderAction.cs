using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B9A RID: 2970
	[Serializable]
	public class RemoveHeaderAction : TransportRuleAction, IEquatable<RemoveHeaderAction>
	{
		// Token: 0x06006FFF RID: 28671 RVA: 0x001CAA10 File Offset: 0x001C8C10
		public override int GetHashCode()
		{
			return this.MessageHeader.GetHashCode();
		}

		// Token: 0x06007000 RID: 28672 RVA: 0x001CAA31 File Offset: 0x001C8C31
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as RemoveHeaderAction)));
		}

		// Token: 0x06007001 RID: 28673 RVA: 0x001CAA6C File Offset: 0x001C8C6C
		public bool Equals(RemoveHeaderAction other)
		{
			return this.MessageHeader.Equals(other.MessageHeader);
		}

		// Token: 0x170022DF RID: 8927
		// (get) Token: 0x06007002 RID: 28674 RVA: 0x001CAA8D File Offset: 0x001C8C8D
		// (set) Token: 0x06007003 RID: 28675 RVA: 0x001CAA95 File Offset: 0x001C8C95
		[LocDescription(RulesTasksStrings.IDs.MessageHeaderDescription)]
		[ActionParameterName("RemoveHeader")]
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

		// Token: 0x170022E0 RID: 8928
		// (get) Token: 0x06007004 RID: 28676 RVA: 0x001CAAA0 File Offset: 0x001C8CA0
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionRemoveHeader(this.MessageHeader.ToString());
			}
		}

		// Token: 0x06007005 RID: 28677 RVA: 0x001CAACC File Offset: 0x001C8CCC
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "RemoveHeader")
			{
				return null;
			}
			RemoveHeaderAction removeHeaderAction = new RemoveHeaderAction();
			try
			{
				removeHeaderAction.MessageHeader = new HeaderName(TransportRuleAction.GetStringValue(action.Arguments[0]));
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
			return removeHeaderAction;
		}

		// Token: 0x06007006 RID: 28678 RVA: 0x001CAB2C File Offset: 0x001C8D2C
		internal override void Reset()
		{
			this.messageHeader = HeaderName.Empty;
			base.Reset();
		}

		// Token: 0x06007007 RID: 28679 RVA: 0x001CAB3F File Offset: 0x001C8D3F
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.MessageHeader == HeaderName.Empty)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06007008 RID: 28680 RVA: 0x001CAB74 File Offset: 0x001C8D74
		internal override Action ToInternalAction()
		{
			return TransportRuleParser.Instance.CreateAction("RemoveHeader", new ShortList<Argument>
			{
				new Value(this.MessageHeader.ToString())
			}, Utils.GetActionName(this));
		}

		// Token: 0x06007009 RID: 28681 RVA: 0x001CABBC File Offset: 0x001C8DBC
		internal override string GetActionParameters()
		{
			return Utils.QuoteCmdletParameter(this.MessageHeader.ToString());
		}

		// Token: 0x0600700A RID: 28682 RVA: 0x001CABE2 File Offset: 0x001C8DE2
		internal override void SuppressPiiData()
		{
			this.MessageHeader = SuppressingPiiProperty.TryRedactValue<HeaderName>(RuleSchema.RemoveHeader, this.MessageHeader);
		}

		// Token: 0x040039D5 RID: 14805
		private HeaderName messageHeader;
	}
}

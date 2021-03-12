using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000067 RID: 103
	[DataContract]
	internal abstract class RuleActionData
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x00009595 File Offset: 0x00007795
		public RuleActionData()
		{
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000959D File Offset: 0x0000779D
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x000095A5 File Offset: 0x000077A5
		[DataMember(EmitDefaultValue = false)]
		public uint UserFlags { get; set; }

		// Token: 0x060004F4 RID: 1268 RVA: 0x000095AE File Offset: 0x000077AE
		public RuleActionData(RuleAction ruleAction)
		{
			this.UserFlags = ruleAction.UserFlags;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000095C4 File Offset: 0x000077C4
		public static RuleActionData GetRuleActionData(RuleAction ruleAction)
		{
			RuleActionData ruleActionData;
			if (ruleAction is RuleAction.InMailboxMove)
			{
				ruleActionData = new RuleActionInMailboxMoveData((RuleAction.InMailboxMove)ruleAction);
			}
			else if (ruleAction is RuleAction.InMailboxCopy)
			{
				ruleActionData = new RuleActionInMailboxCopyData((RuleAction.InMailboxCopy)ruleAction);
			}
			else if (ruleAction is RuleAction.ExternalMove)
			{
				ruleActionData = new RuleActionExternalMoveData((RuleAction.ExternalMove)ruleAction);
			}
			else if (ruleAction is RuleAction.ExternalCopy)
			{
				ruleActionData = new RuleActionExternalCopyData((RuleAction.ExternalCopy)ruleAction);
			}
			else if (ruleAction is RuleAction.Reply)
			{
				ruleActionData = new RuleActionReplyData((RuleAction.Reply)ruleAction);
			}
			else if (ruleAction is RuleAction.OOFReply)
			{
				ruleActionData = new RuleActionOOFReplyData((RuleAction.OOFReply)ruleAction);
			}
			else if (ruleAction is RuleAction.Forward)
			{
				ruleActionData = new RuleActionForwardData((RuleAction.Forward)ruleAction);
			}
			else if (ruleAction is RuleAction.Delegate)
			{
				ruleActionData = new RuleActionDelegateData((RuleAction.Delegate)ruleAction);
			}
			else if (ruleAction is RuleAction.Bounce)
			{
				ruleActionData = new RuleActionBounceData((RuleAction.Bounce)ruleAction);
			}
			else if (ruleAction is RuleAction.Tag)
			{
				ruleActionData = new RuleActionTagData((RuleAction.Tag)ruleAction);
			}
			else if (ruleAction is RuleAction.Defer)
			{
				ruleActionData = new RuleActionDeferData((RuleAction.Defer)ruleAction);
			}
			else if (ruleAction is RuleAction.Delete)
			{
				ruleActionData = new RuleActionDeleteData((RuleAction.Delete)ruleAction);
			}
			else
			{
				if (!(ruleAction is RuleAction.MarkAsRead))
				{
					return null;
				}
				ruleActionData = new RuleActionMarkAsReadData((RuleAction.MarkAsRead)ruleAction);
			}
			ruleActionData.UserFlags = ruleAction.UserFlags;
			return ruleActionData;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00009714 File Offset: 0x00007914
		public RuleAction GetRuleAction()
		{
			RuleAction ruleActionInternal = this.GetRuleActionInternal();
			ruleActionInternal.UserFlags = this.UserFlags;
			return ruleActionInternal;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00009735 File Offset: 0x00007935
		public override string ToString()
		{
			if (this.UserFlags != 0U)
			{
				return string.Format("RuleAction: UserFlags=0x{0:X}, {1}", this.UserFlags, this.ToStringInternal());
			}
			return string.Format("RuleAction: {0}", this.ToStringInternal());
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000976B File Offset: 0x0000796B
		public void Enumerate(CommonUtils.EnumPropTagDelegate propTagEnumerator, CommonUtils.EnumPropValueDelegate propValueEnumerator, CommonUtils.EnumAdrEntryDelegate adrEntryEnumerator)
		{
			if (propTagEnumerator != null)
			{
				this.EnumPropTagsInternal(propTagEnumerator);
			}
			if (propValueEnumerator != null)
			{
				this.EnumPropValuesInternal(propValueEnumerator);
			}
			if (adrEntryEnumerator != null)
			{
				this.EnumAdrEntriesInternal(adrEntryEnumerator);
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000978B File Offset: 0x0000798B
		protected virtual void EnumPropTagsInternal(CommonUtils.EnumPropTagDelegate del)
		{
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000978D File Offset: 0x0000798D
		protected virtual void EnumPropValuesInternal(CommonUtils.EnumPropValueDelegate del)
		{
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000978F File Offset: 0x0000798F
		protected virtual void EnumAdrEntriesInternal(CommonUtils.EnumAdrEntryDelegate del)
		{
		}

		// Token: 0x060004FC RID: 1276
		protected abstract RuleAction GetRuleActionInternal();

		// Token: 0x060004FD RID: 1277
		protected abstract string ToStringInternal();
	}
}

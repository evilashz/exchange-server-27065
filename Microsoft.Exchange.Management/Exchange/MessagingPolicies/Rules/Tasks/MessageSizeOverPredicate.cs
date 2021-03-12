using System;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BDC RID: 3036
	[Serializable]
	public class MessageSizeOverPredicate : SizeOverPredicate, IEquatable<MessageSizeOverPredicate>
	{
		// Token: 0x06007264 RID: 29284 RVA: 0x001D1F01 File Offset: 0x001D0101
		public MessageSizeOverPredicate() : base("Message.Size")
		{
		}

		// Token: 0x06007265 RID: 29285 RVA: 0x001D1F10 File Offset: 0x001D0110
		public override int GetHashCode()
		{
			return this.Size.GetHashCode();
		}

		// Token: 0x06007266 RID: 29286 RVA: 0x001D1F31 File Offset: 0x001D0131
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as MessageSizeOverPredicate)));
		}

		// Token: 0x06007267 RID: 29287 RVA: 0x001D1F6C File Offset: 0x001D016C
		public bool Equals(MessageSizeOverPredicate other)
		{
			return this.Size.Equals(other.Size);
		}

		// Token: 0x17002355 RID: 9045
		// (get) Token: 0x06007268 RID: 29288 RVA: 0x001D1F8D File Offset: 0x001D018D
		// (set) Token: 0x06007269 RID: 29289 RVA: 0x001D1F95 File Offset: 0x001D0195
		[ExceptionParameterName("ExceptIfMessageSizeOver")]
		[LocDescription(RulesTasksStrings.IDs.MessageSizeDescription)]
		[ConditionParameterName("MessageSizeOver")]
		[LocDisplayName(RulesTasksStrings.IDs.MessageSizeDisplayName)]
		public override ByteQuantifiedSize Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		// Token: 0x17002356 RID: 9046
		// (get) Token: 0x0600726A RID: 29290 RVA: 0x001D1FA0 File Offset: 0x001D01A0
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionMessageSizeOver(this.Size.ToString());
			}
		}

		// Token: 0x0600726B RID: 29291 RVA: 0x001D1FCB File Offset: 0x001D01CB
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SizeOverPredicate.CreateFromInternalCondition<MessageSizeOverPredicate>(condition, "Message.Size");
		}

		// Token: 0x04003A6E RID: 14958
		private const string InternalPropertyName = "Message.Size";
	}
}

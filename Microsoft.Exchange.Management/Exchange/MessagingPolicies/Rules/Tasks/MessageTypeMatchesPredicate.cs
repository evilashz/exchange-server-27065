using System;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BDE RID: 3038
	[Serializable]
	public class MessageTypeMatchesPredicate : TransportRulePredicate, IEquatable<MessageTypeMatchesPredicate>
	{
		// Token: 0x0600726C RID: 29292 RVA: 0x001D1FD8 File Offset: 0x001D01D8
		public override int GetHashCode()
		{
			return this.MessageType.GetHashCode();
		}

		// Token: 0x0600726D RID: 29293 RVA: 0x001D1FEA File Offset: 0x001D01EA
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as MessageTypeMatchesPredicate)));
		}

		// Token: 0x0600726E RID: 29294 RVA: 0x001D2023 File Offset: 0x001D0223
		public bool Equals(MessageTypeMatchesPredicate other)
		{
			return this.MessageType.Equals(other.MessageType);
		}

		// Token: 0x17002357 RID: 9047
		// (get) Token: 0x0600726F RID: 29295 RVA: 0x001D2040 File Offset: 0x001D0240
		// (set) Token: 0x06007270 RID: 29296 RVA: 0x001D2048 File Offset: 0x001D0248
		[LocDescription(RulesTasksStrings.IDs.MessageTypeDescription)]
		[ConditionParameterName("MessageTypeMatches")]
		[ExceptionParameterName("ExceptIfMessageTypeMatches")]
		[LocDisplayName(RulesTasksStrings.IDs.MessageTypeDisplayName)]
		public MessageType MessageType
		{
			get
			{
				return this.messageType;
			}
			set
			{
				this.messageType = value;
			}
		}

		// Token: 0x17002358 RID: 9048
		// (get) Token: 0x06007271 RID: 29297 RVA: 0x001D2051 File Offset: 0x001D0251
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionMessageTypeMatches(LocalizedDescriptionAttribute.FromEnum(typeof(MessageType), this.MessageType));
			}
		}

		// Token: 0x06007272 RID: 29298 RVA: 0x001D2078 File Offset: 0x001D0278
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (predicateCondition.Value.RawValues.Count != 1 || !predicateCondition.Name.Equals("isMessageType"))
			{
				return null;
			}
			MessageType messageType;
			try
			{
				messageType = (MessageType)Enum.Parse(typeof(MessageType), predicateCondition.Value.RawValues[0]);
			}
			catch (ArgumentException)
			{
				return null;
			}
			return new MessageTypeMatchesPredicate
			{
				MessageType = messageType
			};
		}

		// Token: 0x06007273 RID: 29299 RVA: 0x001D210C File Offset: 0x001D030C
		internal override Condition ToInternalCondition()
		{
			return TransportRuleParser.Instance.CreatePredicate("isMessageType", null, new ShortList<string>
			{
				this.MessageType.ToString()
			});
		}

		// Token: 0x06007274 RID: 29300 RVA: 0x001D2146 File Offset: 0x001D0346
		internal override string GetPredicateParameters()
		{
			return Enum.GetName(typeof(MessageType), this.MessageType);
		}

		// Token: 0x04003A79 RID: 14969
		private MessageType messageType;
	}
}

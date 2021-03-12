using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B91 RID: 2961
	[Serializable]
	public class LogEventAction : TransportRuleAction, IEquatable<LogEventAction>
	{
		// Token: 0x06006F9F RID: 28575 RVA: 0x001C983C File Offset: 0x001C7A3C
		public override int GetHashCode()
		{
			return this.EventMessage.GetHashCode();
		}

		// Token: 0x06006FA0 RID: 28576 RVA: 0x001C985D File Offset: 0x001C7A5D
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as LogEventAction)));
		}

		// Token: 0x06006FA1 RID: 28577 RVA: 0x001C9898 File Offset: 0x001C7A98
		public bool Equals(LogEventAction other)
		{
			return this.EventMessage.Equals(other.EventMessage);
		}

		// Token: 0x170022CE RID: 8910
		// (get) Token: 0x06006FA2 RID: 28578 RVA: 0x001C98B9 File Offset: 0x001C7AB9
		// (set) Token: 0x06006FA3 RID: 28579 RVA: 0x001C98C1 File Offset: 0x001C7AC1
		[ActionParameterName("LogEventText")]
		[LocDescription(RulesTasksStrings.IDs.EventMessageDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.EventMessageDisplayName)]
		public EventLogText EventMessage
		{
			get
			{
				return this.eventMessage;
			}
			set
			{
				this.eventMessage = value;
			}
		}

		// Token: 0x170022CF RID: 8911
		// (get) Token: 0x06006FA4 RID: 28580 RVA: 0x001C98CC File Offset: 0x001C7ACC
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionLogEvent(this.EventMessage.ToString());
			}
		}

		// Token: 0x06006FA5 RID: 28581 RVA: 0x001C98F8 File Offset: 0x001C7AF8
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "LogEvent")
			{
				return null;
			}
			LogEventAction logEventAction = new LogEventAction();
			try
			{
				logEventAction.EventMessage = new EventLogText(TransportRuleAction.GetStringValue(action.Arguments[0]));
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
			return logEventAction;
		}

		// Token: 0x06006FA6 RID: 28582 RVA: 0x001C9958 File Offset: 0x001C7B58
		internal override void Reset()
		{
			this.eventMessage = EventLogText.Empty;
			base.Reset();
		}

		// Token: 0x06006FA7 RID: 28583 RVA: 0x001C996C File Offset: 0x001C7B6C
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.EventMessage == EventLogText.Empty)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			int index;
			if (!Utils.CheckIsUnicodeStringWellFormed(this.eventMessage.Value, out index))
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.CommentsHaveInvalidChars((int)this.eventMessage.Value[index]), base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06006FA8 RID: 28584 RVA: 0x001C99E8 File Offset: 0x001C7BE8
		internal override Action ToInternalAction()
		{
			return TransportRuleParser.Instance.CreateAction("LogEvent", new ShortList<Argument>
			{
				new Value(this.EventMessage.ToString())
			}, Utils.GetActionName(this));
		}

		// Token: 0x06006FA9 RID: 28585 RVA: 0x001C9A30 File Offset: 0x001C7C30
		internal override string GetActionParameters()
		{
			return Utils.QuoteCmdletParameter(this.EventMessage.ToString());
		}

		// Token: 0x06006FAA RID: 28586 RVA: 0x001C9A56 File Offset: 0x001C7C56
		internal override void SuppressPiiData()
		{
			this.EventMessage = SuppressingPiiProperty.TryRedactValue<EventLogText>(RuleSchema.LogEventText, this.EventMessage);
		}

		// Token: 0x040039BE RID: 14782
		private EventLogText eventMessage;
	}
}

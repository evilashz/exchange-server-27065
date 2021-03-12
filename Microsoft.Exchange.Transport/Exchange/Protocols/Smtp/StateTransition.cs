using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004A6 RID: 1190
	internal sealed class StateTransition<StateType, EventType> : IEquatable<StateTransition<StateType, EventType>>
	{
		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x060035D0 RID: 13776 RVA: 0x000DD8A2 File Offset: 0x000DBAA2
		// (set) Token: 0x060035D1 RID: 13777 RVA: 0x000DD8AA File Offset: 0x000DBAAA
		public StateType FromState { get; private set; }

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x060035D2 RID: 13778 RVA: 0x000DD8B3 File Offset: 0x000DBAB3
		// (set) Token: 0x060035D3 RID: 13779 RVA: 0x000DD8BB File Offset: 0x000DBABB
		public EventType EventOccurred { get; private set; }

		// Token: 0x060035D4 RID: 13780 RVA: 0x000DD8C4 File Offset: 0x000DBAC4
		public StateTransition(StateType fromState, EventType eventOccurred)
		{
			this.FromState = fromState;
			this.EventOccurred = eventOccurred;
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000DD8DC File Offset: 0x000DBADC
		public override int GetHashCode()
		{
			int num = 17;
			int num2 = 31;
			StateType fromState = this.FromState;
			int num3 = num + num2 * fromState.GetHashCode();
			int num4 = 31;
			EventType eventOccurred = this.EventOccurred;
			return num3 + num4 * eventOccurred.GetHashCode();
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000DD91B File Offset: 0x000DBB1B
		public override bool Equals(object other)
		{
			return this.Equals(other as StateTransition<StateType, EventType>);
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000DD92C File Offset: 0x000DBB2C
		public bool Equals(StateTransition<StateType, EventType> other)
		{
			if (object.ReferenceEquals(other, null))
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			if (base.GetType() != other.GetType())
			{
				return false;
			}
			StateType fromState = this.FromState;
			if (fromState.Equals(other.FromState))
			{
				EventType eventOccurred = this.EventOccurred;
				return eventOccurred.Equals(other.EventOccurred);
			}
			return false;
		}
	}
}

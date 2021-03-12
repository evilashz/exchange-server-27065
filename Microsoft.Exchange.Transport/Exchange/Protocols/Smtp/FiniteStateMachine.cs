using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004A5 RID: 1189
	internal abstract class FiniteStateMachine<TStateType, TEventType>
	{
		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x060035C4 RID: 13764 RVA: 0x000DD6AB File Offset: 0x000DB8AB
		// (set) Token: 0x060035C5 RID: 13765 RVA: 0x000DD6B3 File Offset: 0x000DB8B3
		public TStateType StartState { get; private set; }

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x060035C6 RID: 13766 RVA: 0x000DD6BC File Offset: 0x000DB8BC
		// (set) Token: 0x060035C7 RID: 13767 RVA: 0x000DD6C4 File Offset: 0x000DB8C4
		public TStateType CurrentState { get; private set; }

		// Token: 0x060035C8 RID: 13768 RVA: 0x000DD6CD File Offset: 0x000DB8CD
		protected FiniteStateMachine(TStateType startState, IReadOnlyDictionary<StateTransition<TStateType, TEventType>, TStateType> stateTransitions)
		{
			ArgumentValidator.ThrowIfNull("stateTransitions", stateTransitions);
			this.StartState = startState;
			this.CurrentState = startState;
			this.stateTransitions = stateTransitions;
			this.ValidateStateMachineConstruction(startState);
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000DD6FC File Offset: 0x000DB8FC
		public bool TryMoveToNextState(TEventType eventOccurred)
		{
			TStateType tstateType;
			if (!this.TryGetStateTransition(eventOccurred, out tstateType))
			{
				return false;
			}
			this.OnStateTransition(this.CurrentState, eventOccurred, tstateType);
			this.CurrentState = tstateType;
			return true;
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000DD72C File Offset: 0x000DB92C
		protected bool TryGetStateTransition(TEventType eventOccurred, out TStateType nextState)
		{
			return this.stateTransitions.TryGetValue(new StateTransition<TStateType, TEventType>(this.CurrentState, eventOccurred), out nextState);
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000DD748 File Offset: 0x000DB948
		protected bool IsValidStateTransition(TEventType eventOccurred)
		{
			TStateType tstateType;
			return this.stateTransitions.TryGetValue(new StateTransition<TStateType, TEventType>(this.CurrentState, eventOccurred), out tstateType);
		}

		// Token: 0x060035CC RID: 13772
		protected abstract void OnStateTransition(TStateType currentState, TEventType eventOccurred, TStateType nextState);

		// Token: 0x060035CD RID: 13773 RVA: 0x000DD7CC File Offset: 0x000DB9CC
		private void ValidateStateMachineConstruction(TStateType startState)
		{
			if (!this.stateTransitions.Any<KeyValuePair<StateTransition<TStateType, TEventType>, TStateType>>())
			{
				throw new ConfigurationErrorsException("State machine must contain at least one transition");
			}
			if (!this.stateTransitions.Any(delegate(KeyValuePair<StateTransition<TStateType, TEventType>, TStateType> stateTransition)
			{
				TStateType fromState = stateTransition.Key.FromState;
				return fromState.Equals(startState);
			}))
			{
				throw new ConfigurationErrorsException(string.Format("State machine must contain a transition from the starting state {0}", startState));
			}
			if ((from stateTransition in this.stateTransitions
			select stateTransition.Key.GetType()).Any((Type stateTransitionType) => stateTransitionType != typeof(StateTransition<TStateType, TEventType>)))
			{
				throw new ConfigurationErrorsException(string.Format("All state transitions in the state machine must be of type StateTransition<{0}, {1}>", typeof(TStateType), typeof(TEventType)));
			}
		}

		// Token: 0x04001B9F RID: 7071
		private readonly IReadOnlyDictionary<StateTransition<TStateType, TEventType>, TStateType> stateTransitions;
	}
}

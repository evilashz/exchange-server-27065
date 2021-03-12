using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000505 RID: 1285
	internal static class StateMachineUtils<TState> where TState : struct
	{
		// Token: 0x06003B41 RID: 15169 RVA: 0x000F819F File Offset: 0x000F639F
		public static void AddStateChangeTransition(TState fromState, SmtpInStateMachineEvents eventOccurred, TState toState, ICollection<KeyValuePair<StateTransition<TState, SmtpInStateMachineEvents>, TState>> stateTransitions)
		{
			stateTransitions.Add(StateMachineUtils<TState>.CreateStateChangeTransition(fromState, eventOccurred, toState));
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x000F81AF File Offset: 0x000F63AF
		public static void AddNoStateChangeTransition(TState fromAndToState, SmtpInStateMachineEvents eventOccurred, ICollection<KeyValuePair<StateTransition<TState, SmtpInStateMachineEvents>, TState>> stateTransitions)
		{
			stateTransitions.Add(StateMachineUtils<TState>.CreateStateChangeTransition(fromAndToState, eventOccurred, fromAndToState));
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x000F81BF File Offset: 0x000F63BF
		private static KeyValuePair<StateTransition<TState, SmtpInStateMachineEvents>, TState> CreateStateChangeTransition(TState fromState, SmtpInStateMachineEvents eventOccurred, TState toState)
		{
			return new KeyValuePair<StateTransition<TState, SmtpInStateMachineEvents>, TState>(StateMachineUtils<TState>.NewStateTransition(fromState, eventOccurred), toState);
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x000F81CE File Offset: 0x000F63CE
		private static StateTransition<TState, SmtpInStateMachineEvents> NewStateTransition(TState fromState, SmtpInStateMachineEvents eventOccurred)
		{
			return new StateTransition<TState, SmtpInStateMachineEvents>(fromState, eventOccurred);
		}
	}
}

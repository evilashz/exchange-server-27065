using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000502 RID: 1282
	internal class MailboxTransportSmtpInStateMachine : TransportSmtpInStateMachine<MailboxTransportSmtpState>
	{
		// Token: 0x06003B37 RID: 15159 RVA: 0x000F7D0F File Offset: 0x000F5F0F
		public MailboxTransportSmtpInStateMachine(SmtpInSessionState sessionState) : base(sessionState, MailboxTransportSmtpState.WaitingForGreeting, MailboxTransportSmtpInStateMachine.StateTransitions)
		{
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000F7D1E File Offset: 0x000F5F1E
		protected override ISmtpInCommandFactory<SmtpInStateMachineEvents> CreateCommandFactory()
		{
			return new MailboxTransportSmtpInCommandFactory(this.sessionState, new AwaitCompletedDelegate(this.OnAwaitCompleted));
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06003B39 RID: 15161 RVA: 0x000F7D38 File Offset: 0x000F5F38
		protected override bool ReachedEndState
		{
			get
			{
				return base.CurrentState == MailboxTransportSmtpState.End;
			}
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x000F7D44 File Offset: 0x000F5F44
		protected override SmtpInStateMachineEvents GetCompletedEventForCommand(SmtpInCommand commandType)
		{
			switch (commandType)
			{
			case SmtpInCommand.AUTH:
				return SmtpInStateMachineEvents.AuthProcessed;
			case SmtpInCommand.BDAT:
				return SmtpInStateMachineEvents.BdatProcessed;
			case SmtpInCommand.DATA:
				return SmtpInStateMachineEvents.DataProcessed;
			case SmtpInCommand.EHLO:
				return SmtpInStateMachineEvents.EhloProcessed;
			case SmtpInCommand.EXPN:
				return SmtpInStateMachineEvents.ExpnProcessed;
			case SmtpInCommand.HELO:
				return SmtpInStateMachineEvents.HeloProcessed;
			case SmtpInCommand.HELP:
				return SmtpInStateMachineEvents.HelpProcessed;
			case SmtpInCommand.MAIL:
				return SmtpInStateMachineEvents.MailProcessed;
			case SmtpInCommand.NOOP:
				return SmtpInStateMachineEvents.NoopProcessed;
			case SmtpInCommand.QUIT:
				return SmtpInStateMachineEvents.QuitProcessed;
			case SmtpInCommand.RCPT:
				return SmtpInStateMachineEvents.RcptProcessed;
			case SmtpInCommand.RSET:
				return SmtpInStateMachineEvents.RsetProcessed;
			case SmtpInCommand.STARTTLS:
				return SmtpInStateMachineEvents.StartTlsProcessed;
			case SmtpInCommand.VRFY:
				return SmtpInStateMachineEvents.VrfyProcessed;
			case SmtpInCommand.XANONYMOUSTLS:
				return SmtpInStateMachineEvents.XAnonymousTlsProcessed;
			case SmtpInCommand.XEXPS:
				return SmtpInStateMachineEvents.XExpsProcessed;
			case SmtpInCommand.XSESSIONPARAMS:
				return SmtpInStateMachineEvents.XSessionParamsProcessed;
			}
			return SmtpInStateMachineEvents.CommandFailed;
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x000F7DE8 File Offset: 0x000F5FE8
		private static Dictionary<StateTransition<MailboxTransportSmtpState, SmtpInStateMachineEvents>, MailboxTransportSmtpState> CreateStateTransitions()
		{
			Dictionary<StateTransition<MailboxTransportSmtpState, SmtpInStateMachineEvents>, MailboxTransportSmtpState> dictionary = new Dictionary<StateTransition<MailboxTransportSmtpState, SmtpInStateMachineEvents>, MailboxTransportSmtpState>();
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.WaitingForGreeting, SmtpInStateMachineEvents.HeloProcessed, MailboxTransportSmtpState.GreetingReceived, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.WaitingForGreeting, SmtpInStateMachineEvents.EhloProcessed, MailboxTransportSmtpState.GreetingReceived, dictionary);
			MailboxTransportSmtpInStateMachine.AddDefaultTransitions(MailboxTransportSmtpState.WaitingForGreeting, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(MailboxTransportSmtpState.GreetingReceived, SmtpInStateMachineEvents.HeloProcessed, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(MailboxTransportSmtpState.GreetingReceived, SmtpInStateMachineEvents.EhloProcessed, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(MailboxTransportSmtpState.GreetingReceived, SmtpInStateMachineEvents.AuthProcessed, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(MailboxTransportSmtpState.GreetingReceived, SmtpInStateMachineEvents.XExpsProcessed, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.GreetingReceived, SmtpInStateMachineEvents.StartTlsProcessed, MailboxTransportSmtpState.WaitingForSecureGreeting, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.GreetingReceived, SmtpInStateMachineEvents.XAnonymousTlsProcessed, MailboxTransportSmtpState.WaitingForSecureGreeting, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.GreetingReceived, SmtpInStateMachineEvents.MailProcessed, MailboxTransportSmtpState.MailTransactionStarted, dictionary);
			MailboxTransportSmtpInStateMachine.AddDefaultTransitions(MailboxTransportSmtpState.GreetingReceived, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.WaitingForSecureGreeting, SmtpInStateMachineEvents.HeloProcessed, MailboxTransportSmtpState.SecureGreetingReceived, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.WaitingForSecureGreeting, SmtpInStateMachineEvents.EhloProcessed, MailboxTransportSmtpState.SecureGreetingReceived, dictionary);
			MailboxTransportSmtpInStateMachine.AddDefaultTransitions(MailboxTransportSmtpState.WaitingForSecureGreeting, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(MailboxTransportSmtpState.SecureGreetingReceived, SmtpInStateMachineEvents.HeloProcessed, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(MailboxTransportSmtpState.SecureGreetingReceived, SmtpInStateMachineEvents.EhloProcessed, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(MailboxTransportSmtpState.SecureGreetingReceived, SmtpInStateMachineEvents.AuthProcessed, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(MailboxTransportSmtpState.SecureGreetingReceived, SmtpInStateMachineEvents.XExpsProcessed, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(MailboxTransportSmtpState.SecureGreetingReceived, SmtpInStateMachineEvents.XSessionParamsProcessed, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.SecureGreetingReceived, SmtpInStateMachineEvents.MailProcessed, MailboxTransportSmtpState.MailTransactionStarted, dictionary);
			MailboxTransportSmtpInStateMachine.AddDefaultTransitions(MailboxTransportSmtpState.SecureGreetingReceived, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.MailTransactionStarted, SmtpInStateMachineEvents.HeloProcessed, MailboxTransportSmtpState.GreetingReceived, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.MailTransactionStarted, SmtpInStateMachineEvents.EhloProcessed, MailboxTransportSmtpState.GreetingReceived, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.MailTransactionStarted, SmtpInStateMachineEvents.RcptProcessed, MailboxTransportSmtpState.WaitingForMoreRecipientsOrData, dictionary);
			MailboxTransportSmtpInStateMachine.AddDefaultTransitions(MailboxTransportSmtpState.MailTransactionStarted, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.WaitingForMoreRecipientsOrData, SmtpInStateMachineEvents.HeloProcessed, MailboxTransportSmtpState.GreetingReceived, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.WaitingForMoreRecipientsOrData, SmtpInStateMachineEvents.EhloProcessed, MailboxTransportSmtpState.GreetingReceived, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(MailboxTransportSmtpState.WaitingForMoreRecipientsOrData, SmtpInStateMachineEvents.RcptProcessed, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.WaitingForMoreRecipientsOrData, SmtpInStateMachineEvents.DataProcessed, MailboxTransportSmtpState.GreetingReceived, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.WaitingForMoreRecipientsOrData, SmtpInStateMachineEvents.BdatLastProcessed, MailboxTransportSmtpState.GreetingReceived, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.WaitingForMoreRecipientsOrData, SmtpInStateMachineEvents.BdatProcessed, MailboxTransportSmtpState.ReceivingBdatChunks, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.WaitingForMoreRecipientsOrData, SmtpInStateMachineEvents.DataFailed, MailboxTransportSmtpState.GreetingReceived, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.WaitingForMoreRecipientsOrData, SmtpInStateMachineEvents.BdatFailed, MailboxTransportSmtpState.ReceivingBdatChunks, dictionary);
			MailboxTransportSmtpInStateMachine.AddDefaultTransitions(MailboxTransportSmtpState.WaitingForMoreRecipientsOrData, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(MailboxTransportSmtpState.ReceivingBdatChunks, SmtpInStateMachineEvents.BdatProcessed, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(MailboxTransportSmtpState.ReceivingBdatChunks, SmtpInStateMachineEvents.BdatLastProcessed, MailboxTransportSmtpState.GreetingReceived, dictionary);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(MailboxTransportSmtpState.ReceivingBdatChunks, SmtpInStateMachineEvents.BdatFailed, dictionary);
			MailboxTransportSmtpInStateMachine.AddDefaultTransitions(MailboxTransportSmtpState.ReceivingBdatChunks, dictionary);
			return dictionary;
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x000F7F50 File Offset: 0x000F6150
		public static void AddDefaultTransitions(MailboxTransportSmtpState fromState, ICollection<KeyValuePair<StateTransition<MailboxTransportSmtpState, SmtpInStateMachineEvents>, MailboxTransportSmtpState>> stateTransitions)
		{
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(fromState, SmtpInStateMachineEvents.QuitProcessed, MailboxTransportSmtpState.End, stateTransitions);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(fromState, SmtpInStateMachineEvents.NetworkError, MailboxTransportSmtpState.End, stateTransitions);
			StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(fromState, SmtpInStateMachineEvents.SendResponseAndDisconnectClient, MailboxTransportSmtpState.End, stateTransitions);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(fromState, SmtpInStateMachineEvents.CommandFailed, stateTransitions);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(fromState, SmtpInStateMachineEvents.HelpProcessed, stateTransitions);
			StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(fromState, SmtpInStateMachineEvents.NoopProcessed, stateTransitions);
			if (fromState == MailboxTransportSmtpState.WaitingForGreeting)
			{
				StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(fromState, SmtpInStateMachineEvents.RsetProcessed, stateTransitions);
			}
			else
			{
				StateMachineUtils<MailboxTransportSmtpState>.AddStateChangeTransition(fromState, SmtpInStateMachineEvents.RsetProcessed, MailboxTransportSmtpState.GreetingReceived, stateTransitions);
			}
			if (fromState != MailboxTransportSmtpState.WaitingForGreeting)
			{
				StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(fromState, SmtpInStateMachineEvents.ExpnProcessed, stateTransitions);
				StateMachineUtils<MailboxTransportSmtpState>.AddNoStateChangeTransition(fromState, SmtpInStateMachineEvents.VrfyProcessed, stateTransitions);
			}
		}

		// Token: 0x04001DE1 RID: 7649
		private static readonly Dictionary<StateTransition<MailboxTransportSmtpState, SmtpInStateMachineEvents>, MailboxTransportSmtpState> StateTransitions = MailboxTransportSmtpInStateMachine.CreateStateTransitions();
	}
}

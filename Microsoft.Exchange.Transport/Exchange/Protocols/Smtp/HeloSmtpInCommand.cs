using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004E8 RID: 1256
	internal class HeloSmtpInCommand : HeloSmtpInCommandBase
	{
		// Token: 0x06003A3D RID: 14909 RVA: 0x000F12A0 File Offset: 0x000EF4A0
		public HeloSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x000F12AC File Offset: 0x000EF4AC
		protected override void OnParseComplete(out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			HeloCommandEventArgs heloCommandEventArgs = new HeloCommandEventArgs(this.sessionState);
			if (!string.IsNullOrEmpty(this.parseOutput.HeloDomain))
			{
				this.sessionState.HelloDomain = this.parseOutput.HeloDomain;
				heloCommandEventArgs.Domain = this.parseOutput.HeloDomain;
			}
			agentEventTopic = "OnHeloCommand";
			agentEventArgs = heloCommandEventArgs;
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x000F1308 File Offset: 0x000EF508
		protected override Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsyncInternal(CancellationToken cancellationToken)
		{
			return Task.FromResult<ParseAndProcessResult<SmtpInStateMachineEvents>>(new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.Helo(this.sessionState.AdvertisedEhloOptions.AdvertisedFQDN, this.networkConnection.RemoteEndPoint.Address), SmtpInStateMachineEvents.HeloProcessed, false));
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06003A40 RID: 14912 RVA: 0x000F133D File Offset: 0x000EF53D
		protected override HeloOrEhlo Command
		{
			get
			{
				return HeloOrEhlo.Helo;
			}
		}
	}
}

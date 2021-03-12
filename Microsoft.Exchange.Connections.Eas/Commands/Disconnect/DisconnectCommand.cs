using System;
using Microsoft.Exchange.Connections.Eas.Commands.Autodiscover;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Disconnect
{
	// Token: 0x0200002D RID: 45
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DisconnectCommand : EasPseudoCommand<DisconnectRequest, DisconnectResponse>
	{
		// Token: 0x0600010D RID: 269 RVA: 0x000045AC File Offset: 0x000027AC
		protected internal DisconnectCommand(EasConnectionSettings easConnectionSettings) : base(Command.Disconnect, easConnectionSettings)
		{
			base.InitializeExpectedHttpStatusCodes(typeof(HttpStatus));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000045C8 File Offset: 0x000027C8
		internal override DisconnectResponse Execute(DisconnectRequest disconnectRequest)
		{
			AutodiscoverEndpoint mostRecentEndpoint = base.EasConnectionSettings.EasEndpointSettings.MostRecentEndpoint;
			return new DisconnectResponse
			{
				HttpStatus = HttpStatus.OK,
				DisconnectStatus = DisconnectStatus.Success,
				LastResolvedEndpoint = mostRecentEndpoint
			};
		}
	}
}

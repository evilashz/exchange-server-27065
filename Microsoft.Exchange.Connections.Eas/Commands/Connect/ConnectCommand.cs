using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas.Commands.Autodiscover;
using Microsoft.Exchange.Connections.Eas.Commands.Options;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Connect
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConnectCommand : EasPseudoCommand<ConnectRequest, ConnectResponse>
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x0000431D File Offset: 0x0000251D
		protected internal ConnectCommand(EasConnectionSettings easConnectionSettings) : base(Command.Connect, easConnectionSettings)
		{
			base.InitializeExpectedHttpStatusCodes(typeof(HttpStatus));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004338 File Offset: 0x00002538
		internal override ConnectResponse Execute(ConnectRequest connectRequest)
		{
			AutodiscoverRequest autodiscoverRequest = new AutodiscoverRequest();
			autodiscoverRequest.AutodiscoverOption = connectRequest.AutodiscoverOption;
			autodiscoverRequest.Request.EMailAddress = base.EasConnectionSettings.EasEndpointSettings.UserSmtpAddressString;
			AutodiscoverCommand autodiscoverCommand = new AutodiscoverCommand(base.EasConnectionSettings);
			AutodiscoverResponse autodiscoverResponse = autodiscoverCommand.Execute(autodiscoverRequest);
			return new ConnectResponse(autodiscoverResponse, connectRequest.AutodiscoverOption);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004394 File Offset: 0x00002594
		internal ConnectResponse Execute(ConnectRequest connectRequest, IServerCapabilities requiredCapabilities)
		{
			ConnectResponse connectResponse = this.Execute(connectRequest);
			connectResponse.UserSmtpAddressString = base.EasConnectionSettings.EasEndpointSettings.UserSmtpAddressString;
			if (connectResponse.ConnectStatus != ConnectStatus.Success || requiredCapabilities == null)
			{
				return connectResponse;
			}
			OptionsCommand optionsCommand = new OptionsCommand(base.EasConnectionSettings);
			OptionsResponse optionsResponse = optionsCommand.Execute(new OptionsRequest());
			connectResponse.OptionsResponse = optionsResponse;
			EasServerCapabilities easServerCapabilities = optionsResponse.EasServerCapabilities;
			if (easServerCapabilities.Supports(requiredCapabilities))
			{
				base.EasConnectionSettings.ExtensionCapabilities = optionsResponse.EasExtensionCapabilities;
				return connectResponse;
			}
			IEnumerable<string> values = requiredCapabilities.NotIn(easServerCapabilities);
			string text = string.Join(", ", values);
			base.EasConnectionSettings.Log.Debug("ConnectCommand, missing capabilities: '{0}'.", new object[]
			{
				text
			});
			throw new MissingCapabilitiesException(text);
		}
	}
}

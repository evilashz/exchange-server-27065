using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Settings
{
	// Token: 0x02000065 RID: 101
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SettingsCommand : EasServerCommand<SettingsRequest, SettingsResponse, SettingsStatus>
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00005552 File Offset: 0x00003752
		internal SettingsCommand(EasConnectionSettings easConnectionSettings) : base(Command.Settings, easConnectionSettings)
		{
		}
	}
}

using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.Settings
{
	// Token: 0x0200006A RID: 106
	[Flags]
	public enum SettingsStatus
	{
		// Token: 0x040001A3 RID: 419
		Success = 1,
		// Token: 0x040001A4 RID: 420
		ProtocolError = 4098,
		// Token: 0x040001A5 RID: 421
		AccessDenied = 4099,
		// Token: 0x040001A6 RID: 422
		ServerUnavailable = 260,
		// Token: 0x040001A7 RID: 423
		InvalidArguments = 4101,
		// Token: 0x040001A8 RID: 424
		ConflictingArguments = 4102,
		// Token: 0x040001A9 RID: 425
		DeniedByPolicy = 4103
	}
}

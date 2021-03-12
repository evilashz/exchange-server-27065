using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000FF RID: 255
	[Flags]
	public enum UserOptionsMigrationState
	{
		// Token: 0x04000660 RID: 1632
		None = 0,
		// Token: 0x04000661 RID: 1633
		WorkingHoursTimeZoneFixUp = 1,
		// Token: 0x04000662 RID: 1634
		ShowInferenceUiElementsMigrated = 2
	}
}

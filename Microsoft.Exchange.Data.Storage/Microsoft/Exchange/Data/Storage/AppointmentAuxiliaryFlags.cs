using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200020A RID: 522
	[Flags]
	internal enum AppointmentAuxiliaryFlags
	{
		// Token: 0x04000EE7 RID: 3815
		Copied = 1,
		// Token: 0x04000EE8 RID: 3816
		ForceMeetingResponse = 2,
		// Token: 0x04000EE9 RID: 3817
		ForwardedAppointment = 4,
		// Token: 0x04000EEA RID: 3818
		Orphaned = 8,
		// Token: 0x04000EEB RID: 3819
		ExtractOrganizer = 16,
		// Token: 0x04000EEC RID: 3820
		RepairUpdateMessage = 32,
		// Token: 0x04000EED RID: 3821
		ExtractForceReceived = 64,
		// Token: 0x04000EEE RID: 3822
		ExtractedMeeting = 128,
		// Token: 0x04000EEF RID: 3823
		EventAddedFromGroupCalendar = 256
	}
}

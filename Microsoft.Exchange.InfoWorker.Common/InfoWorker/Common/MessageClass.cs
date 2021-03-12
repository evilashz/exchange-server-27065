using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000009 RID: 9
	internal static class MessageClass
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000025A6 File Offset: 0x000007A6
		public static bool IsExternalOofTemplate(string messageClass)
		{
			return ObjectClass.IsOfClass(messageClass, "IPM.Note.Rules.ExternalOofTemplate.Microsoft");
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025B3 File Offset: 0x000007B3
		public static bool IsInternalOofTemplate(string messageClass)
		{
			return !MessageClass.IsExternalOofTemplate(messageClass) && ObjectClass.IsOfClass(messageClass, "IPM.Note.Rules.OofTemplate.Microsoft");
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025CA File Offset: 0x000007CA
		public static bool IsAppointment(string messageClass)
		{
			return ObjectClass.IsOfClass(messageClass, "IPM.Appointment");
		}

		// Token: 0x0400001D RID: 29
		public const string ExternalOofTemplate = "IPM.Note.Rules.ExternalOofTemplate.Microsoft";

		// Token: 0x0400001E RID: 30
		public const string InternalOofTemplate = "IPM.Note.Rules.OofTemplate.Microsoft";

		// Token: 0x0400001F RID: 31
		public const string Appointment = "IPM.Appointment";

		// Token: 0x04000020 RID: 32
		public const string UserUserOofSettings = "IPM.Microsoft.OOF.UserOofSettings";

		// Token: 0x04000021 RID: 33
		public const string ContactsEmailAddresses = "IPM.Microsoft.ContactsEmailAddresses";

		// Token: 0x04000022 RID: 34
		public const string OofAssistantControl = "IPM.Microsoft.OOF.Control";

		// Token: 0x04000023 RID: 35
		public const string OofSchedule = "IPM.Microsoft.OOF.Schedule";

		// Token: 0x04000024 RID: 36
		public const string OofLog = "IPM.Microsoft.OOF.Log";

		// Token: 0x04000025 RID: 37
		public const string RbaLog = "IPM.Microsoft.RBA.Log";

		// Token: 0x04000026 RID: 38
		public const string MfnLog = "IPM.Microsoft.MFN.Log";

		// Token: 0x04000027 RID: 39
		public const string CalendarAssistantLog = "IPM.Microsoft.CA.Log";

		// Token: 0x04000028 RID: 40
		public const string MrmLog = "IPM.Microsoft.MRM.Log";
	}
}

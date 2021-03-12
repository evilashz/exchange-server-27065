using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000067 RID: 103
	public static class MessageClassHelper
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x00010058 File Offset: 0x0000E258
		public static bool IsValidMessageClass(string msgClass)
		{
			if (msgClass == null)
			{
				return false;
			}
			if (msgClass == string.Empty)
			{
				return true;
			}
			if (msgClass.StartsWith(".") || msgClass.EndsWith(".") || msgClass.Contains(".."))
			{
				return false;
			}
			foreach (char c in msgClass)
			{
				if (c < ' ' || c > '~')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000100CE File Offset: 0x0000E2CE
		public static bool MatchingMessageClass(string messageClassToCheck, string desiredMessageClass)
		{
			return !string.IsNullOrEmpty(messageClassToCheck) && messageClassToCheck.Length >= desiredMessageClass.Length && messageClassToCheck.StartsWith(desiredMessageClass, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000100F2 File Offset: 0x0000E2F2
		public static bool IsSchedulePlusMessage(string messageClass)
		{
			return MessageClassHelper.MatchingMessageClass(messageClass, "IPM.Schedule.") || MessageClassHelper.MatchingMessageClass(messageClass, "IPM.MeetingMessageSeries.") || MessageClassHelper.MatchingMessageClass(messageClass, "IPM.SchedulePlus.FreeBusy.BinaryData");
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001011E File Offset: 0x0000E31E
		public static bool IsCalendarFamilyMessage(string messageClass)
		{
			return MessageClassHelper.MatchingMessageClass(messageClass, "IPM.Appointment") || MessageClassHelper.MatchingMessageClass(messageClass, "IPM.Schedule.Meeting") || MessageClassHelper.MatchingMessageClass(messageClass, "IPM.OLE.CLASS.{00061055-0000-0000-C000-000000000046}");
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001014A File Offset: 0x0000E34A
		public static bool IsFreeDocumentMessage(string messageClass)
		{
			return MessageClassHelper.MatchingMessageClass(messageClass, "IPM.Document");
		}
	}
}

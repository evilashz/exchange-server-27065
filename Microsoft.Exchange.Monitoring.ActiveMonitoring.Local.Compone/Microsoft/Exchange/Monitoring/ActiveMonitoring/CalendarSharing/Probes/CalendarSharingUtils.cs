using System;
using System.Text;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.CalendarSharing.Probes
{
	// Token: 0x02000057 RID: 87
	public static class CalendarSharingUtils
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x000126D8 File Offset: 0x000108D8
		public static void GetSharedFolderAppointment(ExchangeService pubService, ExchangeService subService, string publisherEmail, Appointment appointment)
		{
			CalendarView calendarView = new CalendarView(appointment.Start.AddDays(-1.0), appointment.End.AddDays(1.0));
			FolderId folderId = new FolderId(0, publisherEmail);
			FindItemsResults<Appointment> findItemsResults = subService.FindAppointments(folderId, calendarView);
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\nSubscriber's shared calendar appt.s \n");
			foreach (Appointment appointment2 in findItemsResults)
			{
				stringBuilder.Append(string.Format("Subject: {0} Start Time {1} End Time {2}\n", appointment2.Subject, appointment2.Start, appointment2.End));
				if (string.Equals(appointment2.Subject, appointment.Subject, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				stringBuilder.Append("\nPublisher's calendar appt.s \n");
				new FolderId(0);
				pubService.FindAppointments(folderId, calendarView);
				foreach (Appointment appointment3 in findItemsResults)
				{
					stringBuilder.Append(string.Format("Subject: {0} Start Time {1} End Time {2}\n", appointment3.Subject, appointment3.Start, appointment3.End));
				}
				throw new CalendarSharingException(string.Format("The internal calendar sharing probe didn't succeed. New appointment on shared calendar with subject {0} could not be found. Appointments returned by FindAppointments: {1}", appointment.Subject, stringBuilder));
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00012870 File Offset: 0x00010A70
		public static Appointment CreateTestAppointment(ExchangeService exService)
		{
			ExDateTime now = ExDateTime.GetNow(ExTimeZone.CurrentTimeZone);
			string subject = "Test Appt. " + now.UtcTicks.ToString();
			return CalendarSharingUtils.CreateTestAppointment(exService, subject, now);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000128B0 File Offset: 0x00010AB0
		public static Appointment CreateTestAppointment(ExchangeService pubService, string subject, ExDateTime start)
		{
			Appointment appointment = new Appointment(pubService);
			appointment.Subject = subject;
			appointment.Start = (DateTime)start;
			appointment.End = appointment.Start.AddHours(2.0);
			appointment.Save(0);
			return appointment;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000128FC File Offset: 0x00010AFC
		public static void DeleteTestAppointment(ExchangeService pubService, ItemId appointmentId)
		{
			Appointment appointment = Appointment.Bind(pubService, appointmentId);
			appointment.Delete(2);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00012918 File Offset: 0x00010B18
		public static void AddFolderPermission(ExchangeService pubService, string subscriberSmtp, FolderPermissionLevel fldPermLevel)
		{
			CalendarSharingUtils.RemoveFolderPermission(pubService, subscriberSmtp);
			Folder folder = Folder.Bind(pubService, new FolderId(0));
			FolderPermission folderPermission = new FolderPermission(subscriberSmtp, fldPermLevel);
			folder.Permissions.Add(folderPermission);
			folder.Update();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00012954 File Offset: 0x00010B54
		public static string GetFolderPermission(ExchangeService pubService, string subscriberSmtp)
		{
			Folder folder = Folder.Bind(pubService, new FolderId(0));
			string result = string.Empty;
			foreach (FolderPermission folderPermission in folder.Permissions)
			{
				if (folderPermission.UserId != null && string.Equals(folderPermission.UserId.PrimarySmtpAddress, subscriberSmtp, StringComparison.OrdinalIgnoreCase))
				{
					result = folderPermission.PermissionLevel.ToString();
					break;
				}
			}
			return result;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000129E0 File Offset: 0x00010BE0
		public static void RemoveFolderPermission(ExchangeService pubService, string subscriberSmtp)
		{
			Folder folder = Folder.Bind(pubService, new FolderId(0));
			foreach (FolderPermission folderPermission in folder.Permissions)
			{
				if (folderPermission.UserId != null && string.Equals(folderPermission.UserId.PrimarySmtpAddress, subscriberSmtp, StringComparison.OrdinalIgnoreCase))
				{
					folder.Permissions.Remove(folderPermission);
					folder.Update();
					break;
				}
			}
		}
	}
}

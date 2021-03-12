using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EDB RID: 3803
	[XmlRoot(Namespace = "WorkingHours.xsd", ElementName = "Root")]
	[Serializable]
	public class WorkHoursInCalendar
	{
		// Token: 0x06008346 RID: 33606 RVA: 0x0023AFB9 File Offset: 0x002391B9
		private WorkHoursInCalendar(ExTimeZone timeZone, int workDays, int startTimeInMinutes, int endTimeInMinutes)
		{
			this.workHoursVersion1 = new WorkHoursVersion1(new WorkHoursTimeZone(timeZone), new TimeSlot(startTimeInMinutes, endTimeInMinutes), (DaysOfWeek)workDays);
		}

		// Token: 0x06008347 RID: 33607 RVA: 0x0023AFDB File Offset: 0x002391DB
		internal static WorkHoursInCalendar Create(ExTimeZone timeZone, int workDays, int startTimeInMinutes, int endTimeInMinutes)
		{
			return new WorkHoursInCalendar(timeZone, workDays, startTimeInMinutes, endTimeInMinutes);
		}

		// Token: 0x170022D9 RID: 8921
		// (get) Token: 0x06008348 RID: 33608 RVA: 0x0023AFE6 File Offset: 0x002391E6
		// (set) Token: 0x06008349 RID: 33609 RVA: 0x0023AFEE File Offset: 0x002391EE
		[XmlElement]
		public WorkHoursVersion1 WorkHoursVersion1
		{
			get
			{
				return this.workHoursVersion1;
			}
			set
			{
				this.workHoursVersion1 = value;
			}
		}

		// Token: 0x0600834A RID: 33610 RVA: 0x0023AFF8 File Offset: 0x002391F8
		private static WorkHoursInCalendar DeserializeObject(Stream streamToReadFrom)
		{
			WorkHoursInCalendar workHoursInCalendar = new WorkHoursInCalendar();
			XmlSerializer xmlSerializer = VersionedXmlTypeFactory.GetXmlSerializer(workHoursInCalendar.GetType());
			return (WorkHoursInCalendar)xmlSerializer.Deserialize(streamToReadFrom);
		}

		// Token: 0x0600834B RID: 33611 RVA: 0x0023B028 File Offset: 0x00239228
		private static UserConfiguration GetOrCreateWorkingHourConfiguration(MailboxSession session, StoreId folderId)
		{
			UserConfigurationManager userConfigurationManager = session.UserConfigurationManager;
			UserConfiguration userConfiguration = null;
			try
			{
				userConfiguration = userConfigurationManager.GetFolderConfiguration("WorkHours", UserConfigurationTypes.XML, folderId);
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.WorkHoursTracer.TraceDebug<string>(0L, "WorkHoursInCalendar::GetOrCreateWorkingHourConfiguration. GetOrCreateWorkingHourConfigurationStorageWorkingHours does not exist yet. I am going to create a new one. Mailbox = {0}.", session.MailboxOwner.MailboxInfo.DisplayName);
				userConfiguration = userConfigurationManager.CreateFolderConfiguration("WorkHours", UserConfigurationTypes.XML, folderId);
			}
			if (userConfiguration == null)
			{
				throw new WorkingHoursSaveFailedException(ServerStrings.CreateConfigurationItem(session.MailboxOwner.MailboxInfo.DisplayName));
			}
			return userConfiguration;
		}

		// Token: 0x0600834C RID: 33612 RVA: 0x0023B0B0 File Offset: 0x002392B0
		private void SerializeObject(Stream streamToWriteTo)
		{
			XmlSerializer xmlSerializer = VersionedXmlTypeFactory.GetXmlSerializer(base.GetType());
			xmlSerializer.Serialize(streamToWriteTo, this);
		}

		// Token: 0x0600834D RID: 33613 RVA: 0x0023B0D1 File Offset: 0x002392D1
		private WorkHoursInCalendar()
		{
		}

		// Token: 0x0600834E RID: 33614 RVA: 0x0023B0DC File Offset: 0x002392DC
		internal void SaveToCalendar(MailboxSession session, StoreId folderId)
		{
			try
			{
				using (UserConfiguration orCreateWorkingHourConfiguration = WorkHoursInCalendar.GetOrCreateWorkingHourConfiguration(session, folderId))
				{
					using (Stream xmlStream = orCreateWorkingHourConfiguration.GetXmlStream())
					{
						xmlStream.SetLength(0L);
						this.SerializeObject(xmlStream);
						orCreateWorkingHourConfiguration.Save();
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.WorkHoursTracer.TraceError<string, InvalidOperationException>((long)this.GetHashCode(), "{0}: Could not save work hours configuration item to mailbox. Exception information is {1}.", session.MailboxOwner.MailboxInfo.DisplayName, ex);
				throw new WorkingHoursXmlMalformedException(ServerStrings.SaveConfigurationItem(session.MailboxOwner.MailboxInfo.DisplayName, ex.ToString()));
			}
		}

		// Token: 0x0600834F RID: 33615 RVA: 0x0023B198 File Offset: 0x00239398
		internal static bool DeleteFromCalendar(MailboxSession session, StoreId folderId)
		{
			UserConfigurationManager userConfigurationManager = session.UserConfigurationManager;
			OperationResult operationResult = userConfigurationManager.DeleteFolderConfigurations(folderId, new string[]
			{
				"WorkHours"
			});
			ExTraceGlobals.WorkHoursTracer.TraceDebug<IExchangePrincipal, OperationResult>(0L, "DeleteFromCalendar User{0} - Result {1}.", session.MailboxOwner, operationResult);
			return operationResult == OperationResult.Succeeded;
		}

		// Token: 0x06008350 RID: 33616 RVA: 0x0023B1E0 File Offset: 0x002393E0
		internal static WorkHoursInCalendar GetFromCalendar(MailboxSession session, StoreId folderId)
		{
			WorkHoursInCalendar workHoursInCalendar = null;
			try
			{
				UserConfigurationManager userConfigurationManager = session.UserConfigurationManager;
				using (IReadableUserConfiguration readOnlyFolderConfiguration = userConfigurationManager.GetReadOnlyFolderConfiguration("WorkHours", UserConfigurationTypes.XML, folderId))
				{
					if (readOnlyFolderConfiguration == null)
					{
						ExTraceGlobals.WorkHoursTracer.TraceDebug<string>(-1L, "{0}: Work hours configuration item was not found", session.MailboxOwner.MailboxInfo.DisplayName);
					}
					else
					{
						using (Stream xmlStream = readOnlyFolderConfiguration.GetXmlStream())
						{
							workHoursInCalendar = WorkHoursInCalendar.DeserializeObject(xmlStream);
							if (workHoursInCalendar == null)
							{
								ExTraceGlobals.WorkHoursTracer.TraceDebug<string>(-1L, "{0}: Work hours configuration item was found, but no content was found", session.MailboxOwner.MailboxInfo.DisplayName);
							}
						}
					}
				}
			}
			catch (ObjectNotFoundException arg)
			{
				ExTraceGlobals.WorkHoursTracer.TraceError<string, ObjectNotFoundException>(-1L, "{0}: Could not retrieve working hours. Exception information is {1}", session.MailboxOwner.MailboxInfo.DisplayName, arg);
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.WorkHoursTracer.TraceError<string, InvalidOperationException>(-1L, "{0}: Malformed working hours XML in mailbox. Exception information is {1}", session.MailboxOwner.MailboxInfo.DisplayName, ex);
				throw new WorkingHoursXmlMalformedException(ServerStrings.MalformedWorkingHours(session.MailboxOwner.MailboxInfo.DisplayName, ex.ToString()), ex);
			}
			catch (XmlException ex2)
			{
				ExTraceGlobals.WorkHoursTracer.TraceError<string, XmlException>(-1L, "{0}: Hit an XmlException deserializing working hours XML in mailbox. Exception information is {1}", session.MailboxOwner.MailboxInfo.DisplayName, ex2);
				throw new WorkingHoursXmlMalformedException(ServerStrings.MalformedWorkingHours(session.MailboxOwner.MailboxInfo.DisplayName, ex2.ToString()), ex2);
			}
			return workHoursInCalendar;
		}

		// Token: 0x040057EF RID: 22511
		internal const string WorkingHoursConfigurationName = "WorkHours";

		// Token: 0x040057F0 RID: 22512
		private WorkHoursVersion1 workHoursVersion1;
	}
}

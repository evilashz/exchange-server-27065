using System;
using System.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000418 RID: 1048
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TimeZoneSettings
	{
		// Token: 0x06002F26 RID: 12070 RVA: 0x000C25B8 File Offset: 0x000C07B8
		public static bool TryFindOwaTimeZone(MailboxSession session, out ExTimeZone timeZone)
		{
			timeZone = null;
			try
			{
				using (UserConfiguration mailboxConfiguration = session.UserConfigurationManager.GetMailboxConfiguration("OWA.UserOptions", UserConfigurationTypes.Dictionary))
				{
					IDictionary dictionary = mailboxConfiguration.GetDictionary();
					string text = dictionary["timezone"] as string;
					if (text != null && ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(text, out timeZone))
					{
						return true;
					}
				}
			}
			catch (ObjectNotFoundException)
			{
			}
			catch (CorruptDataException)
			{
			}
			catch (AccessDeniedException)
			{
			}
			return false;
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x000C2658 File Offset: 0x000C0858
		public static bool TryFindOutlookTimeZone(MailboxSession session, out byte[] blob)
		{
			blob = null;
			try
			{
				SortBy[] sortColumns = new SortBy[]
				{
					new SortBy(InternalSchema.LastModifiedTime, SortOrder.Descending)
				};
				PropertyDefinition[] dataColumns = new PropertyDefinition[]
				{
					InternalSchema.AppointmentRecurring,
					InternalSchema.AppointmentStateInternal,
					InternalSchema.ResponseState,
					InternalSchema.TimeZoneDefinitionRecurring
				};
				using (Folder folder = Folder.Bind(session, DefaultFolderType.Calendar))
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, sortColumns, dataColumns))
					{
						object[][] rows = queryResult.GetRows(10000);
						foreach (object[] row in rows)
						{
							if (TimeZoneSettings.TryFindTimezoneRow(row, out blob))
							{
								return true;
							}
						}
					}
				}
			}
			catch (ObjectNotFoundException)
			{
			}
			catch (AccessDeniedException)
			{
			}
			return false;
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x000C2758 File Offset: 0x000C0958
		private static bool TryFindTimezoneRow(object[] row, out byte[] blob)
		{
			blob = null;
			if (!(row[0] is bool) || !(bool)row[0])
			{
				return false;
			}
			if (!(row[3] is byte[]))
			{
				return false;
			}
			if (row[2] is int)
			{
				if ((int)row[2] == 1)
				{
					blob = (byte[])row[3];
					return true;
				}
			}
			else if (row[1] is int)
			{
				AppointmentStateFlags appointmentStateFlags = (AppointmentStateFlags)((int)row[1]);
				if (((appointmentStateFlags & AppointmentStateFlags.Meeting) == AppointmentStateFlags.Meeting && (appointmentStateFlags & AppointmentStateFlags.Received) == AppointmentStateFlags.None && (appointmentStateFlags & AppointmentStateFlags.Forward) == AppointmentStateFlags.None) || appointmentStateFlags == AppointmentStateFlags.None)
				{
					blob = (byte[])row[3];
					return true;
				}
			}
			return false;
		}
	}
}

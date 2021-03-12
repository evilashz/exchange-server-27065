using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x02000016 RID: 22
	internal static class BulkUserProvisioningCounters
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00007D40 File Offset: 0x00005F40
		public static void GetPerfCounterInfo(XElement element)
		{
			if (BulkUserProvisioningCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in BulkUserProvisioningCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x04000081 RID: 129
		public const string CategoryName = "MSExchange Bulk User Provisioning";

		// Token: 0x04000082 RID: 130
		public static readonly ExPerformanceCounter NumberOfRecipientsAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Recipients Attempted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000083 RID: 131
		public static readonly ExPerformanceCounter RateOfRecipientsAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Recipients Attempted Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000084 RID: 132
		public static readonly ExPerformanceCounter NumberOfRecipientsCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Recipients Created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000085 RID: 133
		public static readonly ExPerformanceCounter RateOfRecipientsCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Recipients Created Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000086 RID: 134
		public static readonly ExPerformanceCounter NumberOfRecipientsFailed = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Recipients Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000087 RID: 135
		public static readonly ExPerformanceCounter NumberOfMailboxesAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Mailboxes Attempted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000088 RID: 136
		public static readonly ExPerformanceCounter RateOfMailboxesAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Mailboxes Attempted Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000089 RID: 137
		public static readonly ExPerformanceCounter NumberOfMailboxesCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Mailboxes Created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008A RID: 138
		public static readonly ExPerformanceCounter RateOfMailboxesCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Mailboxes Created Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008B RID: 139
		public static readonly ExPerformanceCounter NumberOfMailboxesFailed = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Mailboxes Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008C RID: 140
		public static readonly ExPerformanceCounter NumberOfContactsAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Contacts Attempted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008D RID: 141
		public static readonly ExPerformanceCounter RateOfContactsAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Contacts Attempted Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008E RID: 142
		public static readonly ExPerformanceCounter NumberOfContactsCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Contacts Created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008F RID: 143
		public static readonly ExPerformanceCounter RateOfContactsCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Contacts Created Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000090 RID: 144
		public static readonly ExPerformanceCounter NumberOfContactsFailed = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Contacts Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000091 RID: 145
		public static readonly ExPerformanceCounter NumberOfGroupsAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Groups Attempted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000092 RID: 146
		public static readonly ExPerformanceCounter RateOfGroupsAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Groups Attempted Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000093 RID: 147
		public static readonly ExPerformanceCounter NumberOfGroupsCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Groups Created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000094 RID: 148
		public static readonly ExPerformanceCounter RateOfGroupsCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Groups Created Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000095 RID: 149
		public static readonly ExPerformanceCounter NumberOfGroupsFailed = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Groups Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000096 RID: 150
		public static readonly ExPerformanceCounter NumberOfMembersAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Members Attempted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000097 RID: 151
		public static readonly ExPerformanceCounter RateOfMembersAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Members Attempted Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000098 RID: 152
		public static readonly ExPerformanceCounter NumberOfMembersCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Members Created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000099 RID: 153
		public static readonly ExPerformanceCounter RateOfMembersCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Members Created Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009A RID: 154
		public static readonly ExPerformanceCounter NumberOfMembersFailed = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Members Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009B RID: 155
		public static readonly ExPerformanceCounter NumberOfUpdateContactsAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total UpdateContacts Attempted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009C RID: 156
		public static readonly ExPerformanceCounter RateOfUpdateContactsAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "UpdateContacts Attempted Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009D RID: 157
		public static readonly ExPerformanceCounter NumberOfUpdateContactsCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total UpdateContacts Created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009E RID: 158
		public static readonly ExPerformanceCounter RateOfUpdateContactsCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "UpdateContacts Created Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009F RID: 159
		public static readonly ExPerformanceCounter NumberOfUpdateContactsFailed = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total UpdateContacts Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A0 RID: 160
		public static readonly ExPerformanceCounter NumberOfUpdateUserAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total UpdateUser Attempted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A1 RID: 161
		public static readonly ExPerformanceCounter RateOfUpdateUserAttempted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "UpdateUser Attempted Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A2 RID: 162
		public static readonly ExPerformanceCounter NumberOfUpdateUserCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total UpdateUser Created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A3 RID: 163
		public static readonly ExPerformanceCounter RateOfUpdateUserCreated = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "UpdateUser Created Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A4 RID: 164
		public static readonly ExPerformanceCounter NumberOfUpdateUserFailed = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total UpdateUser Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A5 RID: 165
		public static readonly ExPerformanceCounter NumberOfRequestsInQueue = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Requests Loaded For Processing", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A6 RID: 166
		public static readonly ExPerformanceCounter NumberOfRequestsCompleted = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Requests Completed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A7 RID: 167
		public static readonly ExPerformanceCounter NumberOfRequestsWithTransientError = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Total Requests With Transient Error", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A8 RID: 168
		public static readonly ExPerformanceCounter NumberOfRequestsWithoutProgressInThisRound = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Requests Without Progress", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000A9 RID: 169
		public static readonly ExPerformanceCounter NumberOfRequestsWithProgressInThisRound = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Requests With Progress", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000AA RID: 170
		public static readonly ExPerformanceCounter NumberOfRoundsWithRequestsWithoutProgress = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Rounds With Requests Without Progress", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000AB RID: 171
		public static readonly ExPerformanceCounter NumberOfRoundsWithoutProgress = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Rounds Without Progress", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000AC RID: 172
		public static readonly ExPerformanceCounter LastRecipientAttemptedTimestamp = new ExPerformanceCounter("MSExchange Bulk User Provisioning", "Last Attempted Recipient Timestamp", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040000AD RID: 173
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			BulkUserProvisioningCounters.NumberOfRecipientsAttempted,
			BulkUserProvisioningCounters.RateOfRecipientsAttempted,
			BulkUserProvisioningCounters.NumberOfRecipientsCreated,
			BulkUserProvisioningCounters.RateOfRecipientsCreated,
			BulkUserProvisioningCounters.NumberOfRecipientsFailed,
			BulkUserProvisioningCounters.NumberOfMailboxesAttempted,
			BulkUserProvisioningCounters.RateOfMailboxesAttempted,
			BulkUserProvisioningCounters.NumberOfMailboxesCreated,
			BulkUserProvisioningCounters.RateOfMailboxesCreated,
			BulkUserProvisioningCounters.NumberOfMailboxesFailed,
			BulkUserProvisioningCounters.NumberOfContactsAttempted,
			BulkUserProvisioningCounters.RateOfContactsAttempted,
			BulkUserProvisioningCounters.NumberOfContactsCreated,
			BulkUserProvisioningCounters.RateOfContactsCreated,
			BulkUserProvisioningCounters.NumberOfContactsFailed,
			BulkUserProvisioningCounters.NumberOfGroupsAttempted,
			BulkUserProvisioningCounters.RateOfGroupsAttempted,
			BulkUserProvisioningCounters.NumberOfGroupsCreated,
			BulkUserProvisioningCounters.RateOfGroupsCreated,
			BulkUserProvisioningCounters.NumberOfGroupsFailed,
			BulkUserProvisioningCounters.NumberOfMembersAttempted,
			BulkUserProvisioningCounters.RateOfMembersAttempted,
			BulkUserProvisioningCounters.NumberOfMembersCreated,
			BulkUserProvisioningCounters.RateOfMembersCreated,
			BulkUserProvisioningCounters.NumberOfMembersFailed,
			BulkUserProvisioningCounters.NumberOfUpdateContactsAttempted,
			BulkUserProvisioningCounters.RateOfUpdateContactsAttempted,
			BulkUserProvisioningCounters.NumberOfUpdateContactsCreated,
			BulkUserProvisioningCounters.RateOfUpdateContactsCreated,
			BulkUserProvisioningCounters.NumberOfUpdateContactsFailed,
			BulkUserProvisioningCounters.NumberOfUpdateUserAttempted,
			BulkUserProvisioningCounters.RateOfUpdateUserAttempted,
			BulkUserProvisioningCounters.NumberOfUpdateUserCreated,
			BulkUserProvisioningCounters.RateOfUpdateUserCreated,
			BulkUserProvisioningCounters.NumberOfUpdateUserFailed,
			BulkUserProvisioningCounters.NumberOfRequestsInQueue,
			BulkUserProvisioningCounters.NumberOfRequestsCompleted,
			BulkUserProvisioningCounters.NumberOfRequestsWithTransientError,
			BulkUserProvisioningCounters.NumberOfRequestsWithoutProgressInThisRound,
			BulkUserProvisioningCounters.NumberOfRequestsWithProgressInThisRound,
			BulkUserProvisioningCounters.NumberOfRoundsWithRequestsWithoutProgress,
			BulkUserProvisioningCounters.NumberOfRoundsWithoutProgress,
			BulkUserProvisioningCounters.LastRecipientAttemptedTimestamp
		};
	}
}

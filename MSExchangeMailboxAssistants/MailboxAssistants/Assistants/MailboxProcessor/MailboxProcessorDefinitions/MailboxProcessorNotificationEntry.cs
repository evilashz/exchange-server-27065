using System;
using System.Globalization;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor.MailboxProcessorDefinitions
{
	// Token: 0x02000238 RID: 568
	internal class MailboxProcessorNotificationEntry
	{
		// Token: 0x0600156C RID: 5484 RVA: 0x00079DA0 File Offset: 0x00077FA0
		public MailboxProcessorNotificationEntry(Guid mailboxGuid, Guid databaseGuid, Guid externalDirectoryOrganizationId, int issueDetectedCount)
		{
			this.mailboxGuid = mailboxGuid;
			this.databaseGuid = databaseGuid;
			this.externalDirectoryOrganizationId = externalDirectoryOrganizationId;
			this.issueDetectedCount = issueDetectedCount;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00079DC8 File Offset: 0x00077FC8
		public MailboxProcessorNotificationEntry(ProbeResult probeResult)
		{
			Guid guid;
			if (!Guid.TryParse(probeResult.StateAttribute1, out guid))
			{
				throw new FailedToReadProbeResultException(1, "mailboxGuid", probeResult.StateAttribute1);
			}
			Guid guid2;
			if (!Guid.TryParse(probeResult.StateAttribute2, out guid2))
			{
				throw new FailedToReadProbeResultException(2, "databaseGuid", probeResult.StateAttribute2);
			}
			int num;
			if (!int.TryParse(probeResult.StateAttribute4, out num))
			{
				throw new FailedToReadProbeResultException(3, "issueDetectedCount", probeResult.StateAttribute3);
			}
			this.issueDetectedCount = num;
			this.mailboxGuid = guid;
			this.databaseGuid = guid2;
			Guid guid3;
			Guid.TryParse(probeResult.StateAttribute3, out guid3);
			this.externalDirectoryOrganizationId = guid3;
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x00079E68 File Offset: 0x00078068
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x00079E70 File Offset: 0x00078070
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x00079E78 File Offset: 0x00078078
		public Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return this.externalDirectoryOrganizationId;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x00079E80 File Offset: 0x00078080
		public int IssueDetectedCount
		{
			get
			{
				return this.issueDetectedCount;
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x00079E88 File Offset: 0x00078088
		public EventNotificationItem CreateMailboxProcessorEventNotificationItem(string serviceName, string component, string notificationReason)
		{
			return new EventNotificationItem(serviceName, component, notificationReason, ResultSeverityLevel.Error)
			{
				StateAttribute1 = this.MailboxGuid.ToString(),
				StateAttribute2 = this.DatabaseGuid.ToString(),
				StateAttribute3 = this.ExternalDirectoryOrganizationId.ToString(),
				StateAttribute4 = this.issueDetectedCount.ToString(CultureInfo.InvariantCulture),
				StateAttribute5 = "Client=TBA"
			};
		}

		// Token: 0x04000CB4 RID: 3252
		private readonly Guid mailboxGuid;

		// Token: 0x04000CB5 RID: 3253
		private readonly Guid databaseGuid;

		// Token: 0x04000CB6 RID: 3254
		private readonly Guid externalDirectoryOrganizationId;

		// Token: 0x04000CB7 RID: 3255
		private readonly int issueDetectedCount;
	}
}

using System;
using System.Text;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PeopleRelevance
{
	// Token: 0x0200021E RID: 542
	public static class PeopleRelevanceAssistantLoggingHelper
	{
		// Token: 0x060014A6 RID: 5286 RVA: 0x000770B0 File Offset: 0x000752B0
		public static string BuildOperationSpecificString(string MailboxGuid, string ServerFullyQualifiedDomainName, string MailboxType, string DisplayAddress, string OrganizationId)
		{
			return PeopleRelevanceAssistantLoggingHelper.BuildOperationSpecificString(MailboxGuid, ServerFullyQualifiedDomainName, MailboxType, DisplayAddress, OrganizationId, null);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x000770C0 File Offset: 0x000752C0
		public static string BuildOperationSpecificString(string MailboxGuid, string ServerFullyQualifiedDomainName, string MailboxType, string DisplayAddress, string OrganizationId, string ExceptionString)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(MailboxGuid))
			{
				stringBuilder.Append("MbGuid:" + MailboxGuid + ";");
			}
			if (!string.IsNullOrEmpty(ServerFullyQualifiedDomainName))
			{
				stringBuilder.Append("Server:" + ServerFullyQualifiedDomainName + ";");
			}
			if (!string.IsNullOrEmpty(MailboxType))
			{
				stringBuilder.Append("MbType:" + MailboxType + ";");
			}
			if (!string.IsNullOrEmpty(DisplayAddress))
			{
				stringBuilder.Append("Email:" + DisplayAddress + ";");
			}
			if (!string.IsNullOrEmpty(OrganizationId))
			{
				stringBuilder.Append("Tenant:" + OrganizationId + ";");
			}
			if (!string.IsNullOrEmpty(ExceptionString))
			{
				stringBuilder.Append("Exception:" + ExceptionString + ";");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000C6F RID: 3183
		internal const string SkippedMailboxProcessing = "SkipMBP";

		// Token: 0x04000C70 RID: 3184
		internal const string CompletedMailboxProcessing = "CompleteMBP";

		// Token: 0x04000C71 RID: 3185
		internal const string FailedToProcessMailbox = "FailedMBP";

		// Token: 0x04000C72 RID: 3186
		internal const string AbortMailboxProcessingForShutdownSignal = "AbortMBPS";

		// Token: 0x04000C73 RID: 3187
		private const string MbGuidKey = "MbGuid";

		// Token: 0x04000C74 RID: 3188
		private const string ServerKey = "Server";

		// Token: 0x04000C75 RID: 3189
		private const string MbTypeKey = "MbType";

		// Token: 0x04000C76 RID: 3190
		private const string EmailKey = "Email";

		// Token: 0x04000C77 RID: 3191
		private const string TenantKey = "Tenant";

		// Token: 0x04000C78 RID: 3192
		private const string ExceptionKey = "Exception";
	}
}

using System;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000018 RID: 24
	internal class MigrationJobIssue : MigrationIssue
	{
		// Token: 0x0600008B RID: 139 RVA: 0x0000528D File Offset: 0x0000348D
		public MigrationJobIssue(MigrationJob job) : base("JobIssue", job.TenantName, job.JobName, job.StatusData.InternalError)
		{
		}

		// Token: 0x04000032 RID: 50
		public const string ErrorClass = "JobIssue";
	}
}

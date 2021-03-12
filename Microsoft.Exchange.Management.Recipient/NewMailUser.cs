using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000089 RID: 137
	[Cmdlet("New", "MailUser", SupportsShouldProcess = true, DefaultParameterSetName = "DisabledUser")]
	public sealed class NewMailUser : NewMailUserBase
	{
		// Token: 0x0600099C RID: 2460 RVA: 0x00028504 File Offset: 0x00026704
		public NewMailUser()
		{
			base.NumberofCalls = ProvisioningCounters.NumberOfNewMailuserCalls;
			base.NumberofSuccessfulCalls = ProvisioningCounters.NumberOfSuccessfulNewMailuserCalls;
			base.AverageTimeTaken = ProvisioningCounters.AverageNewMailuserResponseTime;
			base.AverageBaseTimeTaken = ProvisioningCounters.AverageNewMailuserResponseTimeBase;
			base.AverageTimeTakenWithCache = ProvisioningCounters.AverageNewMailuserResponseTimeWithCache;
			base.AverageBaseTimeTakenWithCache = ProvisioningCounters.AverageNewMailuserResponseTimeBaseWithCache;
			base.AverageTimeTakenWithoutCache = ProvisioningCounters.AverageNewMailuserResponseTimeWithoutCache;
			base.AverageBaseTimeTakenWithoutCache = ProvisioningCounters.AverageNewMailuserResponseTimeBaseWithoutCache;
			base.TotalResponseTime = ProvisioningCounters.TotalNewMailuserResponseTime;
			base.CacheActivePercentage = ProvisioningCounters.NewMailuserCacheActivePercentage;
			base.CacheActiveBasePercentage = ProvisioningCounters.NewMailuserCacheActivePercentageBase;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00028590 File Offset: 0x00026790
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			MailUser result2 = new MailUser((ADUser)result);
			base.WriteResult(result2);
			TaskLogger.LogExit();
		}
	}
}

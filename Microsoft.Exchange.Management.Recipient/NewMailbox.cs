using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000070 RID: 112
	[Cmdlet("New", "Mailbox", SupportsShouldProcess = true, DefaultParameterSetName = "User")]
	public sealed class NewMailbox : NewMailboxOrSyncMailbox
	{
		// Token: 0x06000848 RID: 2120 RVA: 0x000253E8 File Offset: 0x000235E8
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			ADUser aduser = (ADUser)result;
			if (null != aduser.MasterAccountSid)
			{
				aduser.LinkedMasterAccount = SecurityPrincipalIdParameter.GetFriendlyUserName(aduser.MasterAccountSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				aduser.ResetChangeTracking();
			}
			Mailbox result2 = new Mailbox(aduser);
			base.WriteResult(result2);
			TaskLogger.LogExit();
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00025458 File Offset: 0x00023658
		public NewMailbox()
		{
			base.NumberofCalls = ProvisioningCounters.NumberOfNewMailboxCalls;
			base.NumberofSuccessfulCalls = ProvisioningCounters.NumberOfSuccessfulNewMailboxCalls;
			base.AverageTimeTaken = ProvisioningCounters.AverageNewMailboxResponseTime;
			base.AverageBaseTimeTaken = ProvisioningCounters.AverageNewMailboxResponseTimeBase;
			base.AverageTimeTakenWithCache = ProvisioningCounters.AverageNewMailboxResponseTimeWithCache;
			base.AverageBaseTimeTakenWithCache = ProvisioningCounters.AverageNewMailboxResponseTimeBaseWithCache;
			base.AverageTimeTakenWithoutCache = ProvisioningCounters.AverageNewMailboxResponseTimeWithoutCache;
			base.AverageBaseTimeTakenWithoutCache = ProvisioningCounters.AverageNewMailboxResponseTimeBaseWithoutCache;
			base.TotalResponseTime = ProvisioningCounters.TotalNewMailboxResponseTime;
			base.CacheActivePercentage = ProvisioningCounters.NewMailboxCacheActivePercentage;
			base.CacheActiveBasePercentage = ProvisioningCounters.NewMailboxCacheActivePercentageBase;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x000254E4 File Offset: 0x000236E4
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x000254F1 File Offset: 0x000236F1
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedRoomMailbox")]
		public string Office
		{
			get
			{
				return this.DataObject.Office;
			}
			set
			{
				this.DataObject.Office = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x000254FF File Offset: 0x000236FF
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x0002550C File Offset: 0x0002370C
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedRoomMailbox")]
		public string Phone
		{
			get
			{
				return this.DataObject.Phone;
			}
			set
			{
				this.DataObject.Phone = value;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0002551A File Offset: 0x0002371A
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x00025527 File Offset: 0x00023727
		[Parameter(Mandatory = false)]
		public NetID OriginalNetID
		{
			get
			{
				return this.DataObject.OriginalNetID;
			}
			set
			{
				this.DataObject.OriginalNetID = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00025535 File Offset: 0x00023735
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x00025542 File Offset: 0x00023742
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedRoomMailbox")]
		public int? ResourceCapacity
		{
			get
			{
				return this.DataObject.ResourceCapacity;
			}
			set
			{
				this.DataObject.ResourceCapacity = value;
			}
		}
	}
}

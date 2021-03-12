using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000069 RID: 105
	[Cmdlet("Get", "MailboxPlan", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxPlan : GetMailboxBase<MailboxPlanIdParameter>
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001F830 File Offset: 0x0001DA30
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x0001F856 File Offset: 0x0001DA56
		[Parameter(Mandatory = false)]
		public SwitchParameter AllMailboxPlanReleases
		{
			get
			{
				return (SwitchParameter)(base.Fields["AllMailboxPlanReleases"] ?? false);
			}
			set
			{
				base.Fields["AllMailboxPlanReleases"] = value;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001F86E File Offset: 0x0001DA6E
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return GetMailboxPlan.AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001F878 File Offset: 0x0001DA78
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (this.Identity != null || this.AllMailboxPlanReleases)
				{
					return base.InternalFilter;
				}
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.InternalFilter,
					new NotFilter(new BitMaskAndFilter(ADRecipientSchema.ProvisioningFlags, 32UL))
				});
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001F8CC File Offset: 0x0001DACC
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<MailboxPlanSchema>();
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001F8D4 File Offset: 0x0001DAD4
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADUser aduser = (ADUser)dataObject;
			if (null != aduser.MasterAccountSid)
			{
				aduser.LinkedMasterAccount = SecurityPrincipalIdParameter.GetFriendlyUserName(aduser.MasterAccountSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				aduser.ResetChangeTracking();
			}
			MailboxPlan mailboxPlan = new MailboxPlan(aduser);
			mailboxPlan.Database = ADObjectIdResolutionHelper.ResolveDN(mailboxPlan.Database);
			return mailboxPlan;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001F937 File Offset: 0x0001DB37
		internal new string Anr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040001A2 RID: 418
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.MailboxPlan
		};
	}
}

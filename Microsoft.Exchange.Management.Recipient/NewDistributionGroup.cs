using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000027 RID: 39
	[Cmdlet("New", "DistributionGroup", SupportsShouldProcess = true)]
	public sealed class NewDistributionGroup : NewDistributionGroupBase
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00009E08 File Offset: 0x00008008
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (base.ManagedBy != null && base.ManagedBy.IsChangesOnlyCopy)
			{
				MultiValuedProperty<GeneralRecipientIdParameter> managedBy = base.ManagedBy;
				base.ManagedBy = new MultiValuedProperty<GeneralRecipientIdParameter>();
				base.ManagedBy.CopyChangesFrom(managedBy);
			}
			ADObjectId adobjectId = null;
			base.TryGetExecutingUserId(out adobjectId);
			if (!base.Fields.IsModified(ADGroupSchema.ManagedBy) && adobjectId != null)
			{
				if (!base.CurrentOrganizationId.Equals(base.ExecutingUserOrganizationId))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorManagedByMustBeSpecifiedWIthOrganization), ExchangeErrorCategory.Client, null);
				}
				else
				{
					base.ManagedBy = new MultiValuedProperty<GeneralRecipientIdParameter>(adobjectId);
				}
				MailboxTaskHelper.CheckAndResolveManagedBy<ADGroup>(this, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), ExchangeErrorCategory.ServerOperation, (base.ManagedBy == null) ? null : base.ManagedBy.ToArray(), out this.managedByRecipients);
			}
			else
			{
				MailboxTaskHelper.CheckAndResolveManagedBy<ADGroup>(this, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), ExchangeErrorCategory.Client, (base.ManagedBy == null) ? null : base.ManagedBy.ToArray(), out this.managedByRecipients);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00009F18 File Offset: 0x00008118
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			DistributionGroup result2 = new DistributionGroup((ADGroup)result);
			base.WriteResult(result2);
			TaskLogger.LogExit();
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00009F58 File Offset: 0x00008158
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(DistributionGroup).FullName;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009F69 File Offset: 0x00008169
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return DistributionGroup.FromDataObject((ADGroup)dataObject);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009F78 File Offset: 0x00008178
		protected override void PrepareRecipientObject(ADGroup group)
		{
			base.PrepareRecipientObject(group);
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.ReportToOriginator.Enabled)
			{
				group.ReportToOriginatorEnabled = false;
			}
		}
	}
}

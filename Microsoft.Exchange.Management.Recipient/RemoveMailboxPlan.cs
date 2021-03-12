using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000075 RID: 117
	[Cmdlet("Remove", "MailboxPlan", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMailboxPlan : RemoveMailboxBase<MailboxPlanIdParameter>
	{
		// Token: 0x06000887 RID: 2183 RVA: 0x00025EF8 File Offset: 0x000240F8
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (MailboxTaskHelper.ExcludeArbitrationMailbox(adrecipient, base.Arbitration) || MailboxTaskHelper.ExcludePublicFolderMailbox(adrecipient, base.PublicFolder) || MailboxTaskHelper.ExcludeMailboxPlan(adrecipient, true) || MailboxTaskHelper.ExcludeAuditLogMailbox(adrecipient, base.AuditLog))
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ExchangeErrorCategory.Client, this.Identity);
			}
			return adrecipient;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00025FA0 File Offset: 0x000241A0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.Identity != null)
			{
				base.InternalValidate();
				OrFilter filter = new OrFilter(new QueryFilter[]
				{
					new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.UserMailbox),
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MailboxPlan, base.DataObject.Id)
					}),
					new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.MailUser),
						new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.IntendedMailboxPlan, base.DataObject.Id)
					})
				});
				ADRecipient[] array = base.TenantGlobalCatalogSession.Find(null, QueryScope.SubTree, filter, null, 1);
				if (array.Length != 0)
				{
					base.WriteError(new TaskInvalidOperationException(Strings.ErrorRemoveMailboxPlanWithAssociatedRecipents(this.Identity.ToString())), ExchangeErrorCategory.Client, base.DataObject.Identity);
				}
			}
			TaskLogger.LogExit();
		}
	}
}

using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B16 RID: 2838
	[Cmdlet("Remove", "RemoteAccountPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveRemoteAccountPolicy : RemoveSystemConfigurationObjectTask<RemoteAccountPolicyIdParameter, RemoteAccountPolicy>
	{
		// Token: 0x17001EA4 RID: 7844
		// (get) Token: 0x060064CB RID: 25803 RVA: 0x001A4B39 File Offset: 0x001A2D39
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveRemoteAccountPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x060064CC RID: 25804 RVA: 0x001A4B4C File Offset: 0x001A2D4C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.RemoteAccountPolicy, base.DataObject.Id),
				new NotFilter(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.ArbitrationMailbox))
			});
			ADRecipient[] array = base.TenantGlobalCatalogSession.Find(null, QueryScope.SubTree, filter, null, 1);
			if (array != null && array.Length > 0)
			{
				base.WriteError(new InvalidOperationException(Strings.RemoveRemoteAccountPolicyFailedWithExistingMailboxes), ErrorCategory.InvalidOperation, this.Identity);
			}
		}
	}
}

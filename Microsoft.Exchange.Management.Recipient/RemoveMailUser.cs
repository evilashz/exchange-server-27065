using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200008C RID: 140
	[Cmdlet("Remove", "MailUser", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMailUser : RemoveMailUserOrRemoteMailboxBase<MailUserIdParameter>
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x000288AD File Offset: 0x00026AAD
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x000288B5 File Offset: 0x00026AB5
		[Parameter(Mandatory = false)]
		public new SwitchParameter ForReconciliation
		{
			get
			{
				return base.ForReconciliation;
			}
			set
			{
				base.ForReconciliation = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x000288BE File Offset: 0x00026ABE
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x000288DF File Offset: 0x00026ADF
		[Parameter]
		public bool Permanent
		{
			get
			{
				return (bool)(base.Fields["Permanent"] ?? false);
			}
			set
			{
				base.Fields["Permanent"] = value;
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000288F8 File Offset: 0x00026AF8
		protected override bool ShouldSoftDeleteObject()
		{
			ADRecipient dataObject = base.DataObject;
			return dataObject != null && !(dataObject.OrganizationId == null) && dataObject.OrganizationId.ConfigurationUnit != null && !this.Permanent && Globals.IsMicrosoftHostedOnly;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002893C File Offset: 0x00026B3C
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			if (this.Permanent || this.ForReconciliation)
			{
				recipientSession = SoftDeletedTaskHelper.GetSessionForSoftDeletedObjects(recipientSession, null);
			}
			return recipientSession;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00028974 File Offset: 0x00026B74
		protected override void InternalProcessRecord()
		{
			if (Globals.IsMicrosoftHostedOnly)
			{
				ADUser dataObject = base.DataObject;
				if (this.ShouldSoftDeleteObject())
				{
					SoftDeletedTaskHelper.UpdateRecipientForSoftDelete(base.DataSession as IRecipientSession, dataObject, this.ForReconciliation);
					SoftDeletedTaskHelper.UpdateExchangeGuidForMailEnabledUser(dataObject);
				}
				else
				{
					dataObject.RecipientSoftDeletedStatus = 0;
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x000289D0 File Offset: 0x00026BD0
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.DataObject != null && base.DataObject.CatchAllRecipientBL.Count > 0)
			{
				string domain = string.Join(", ", (from r in base.DataObject.CatchAllRecipientBL
				select r.Name).ToArray<string>());
				base.WriteError(new CannotRemoveMailUserCatchAllRecipientException(domain), ExchangeErrorCategory.Client, base.DataObject.Identity);
			}
		}

		// Token: 0x040001F4 RID: 500
		private const string ParameterPermanent = "Permanent";
	}
}

using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000EF RID: 239
	[Cmdlet("Get", "UMMailboxPlan", DefaultParameterSetName = "Identity")]
	public sealed class GetUMMailboxPlan : GetUMMailboxBase<MailboxPlanIdParameter>
	{
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00042096 File Offset: 0x00040296
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return GetUMMailboxPlan.AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0004209D File Offset: 0x0004029D
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return UMMailboxPlan.FromDataObject((ADRecipient)dataObject);
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x000420AF File Offset: 0x000402AF
		internal new string Anr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000379 RID: 889
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.MailboxPlan
		};
	}
}

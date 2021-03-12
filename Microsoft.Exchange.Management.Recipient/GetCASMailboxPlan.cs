using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000004 RID: 4
	[Cmdlet("Get", "CASMailboxPlan", DefaultParameterSetName = "Identity")]
	public sealed class GetCASMailboxPlan : GetCASMailboxBase<MailboxPlanIdParameter>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002C93 File Offset: 0x00000E93
		internal new string Anr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002C96 File Offset: 0x00000E96
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return GetCASMailboxPlan.AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C9D File Offset: 0x00000E9D
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return CASMailboxPlan.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x04000005 RID: 5
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.MailboxPlan
		};
	}
}

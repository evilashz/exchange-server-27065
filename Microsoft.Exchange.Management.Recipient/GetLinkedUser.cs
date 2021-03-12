using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000060 RID: 96
	[Cmdlet("Get", "LinkedUser", DefaultParameterSetName = "Identity")]
	public sealed class GetLinkedUser : GetRecipientBase<UserIdParameter, ADUser>
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0001B30C File Offset: 0x0001950C
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetLinkedUser.SortPropertiesArray;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001B313 File Offset: 0x00019513
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return GetLinkedUser.AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001B31C File Offset: 0x0001951C
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			ADUser aduser = (ADUser)dataObject;
			if (null != aduser.MasterAccountSid)
			{
				aduser.LinkedMasterAccount = SecurityPrincipalIdParameter.GetFriendlyUserName(aduser.MasterAccountSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				aduser.ResetChangeTracking();
			}
			return new LinkedUser(aduser);
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001B367 File Offset: 0x00019567
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<LinkedUserSchema>();
			}
		}

		// Token: 0x04000189 RID: 393
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.LinkedUser
		};

		// Token: 0x0400018A RID: 394
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			OrgPersonPresentationObjectSchema.DisplayName,
			OrgPersonPresentationObjectSchema.FirstName,
			OrgPersonPresentationObjectSchema.LastName,
			OrgPersonPresentationObjectSchema.Office,
			OrgPersonPresentationObjectSchema.City
		};
	}
}

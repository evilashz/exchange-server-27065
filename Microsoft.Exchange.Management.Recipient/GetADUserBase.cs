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
	// Token: 0x020000D6 RID: 214
	public abstract class GetADUserBase<TIdentity> : GetRecipientBase<TIdentity, ADUser> where TIdentity : RecipientIdParameter, new()
	{
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x0003C5EF File Offset: 0x0003A7EF
		// (set) Token: 0x060010AC RID: 4268 RVA: 0x0003C5F7 File Offset: 0x0003A7F7
		[Parameter(Mandatory = false)]
		public SwitchParameter SoftDeletedUser
		{
			get
			{
				return base.SoftDeletedObject;
			}
			set
			{
				base.SoftDeletedObject = value;
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0003C600 File Offset: 0x0003A800
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			ADObjectId searchRoot = recipientSession.SearchRoot;
			if (this.SoftDeletedUser.IsPresent && base.CurrentOrganizationId != null && base.CurrentOrganizationId.OrganizationalUnit != null)
			{
				searchRoot = new ADObjectId("OU=Soft Deleted Objects," + base.CurrentOrganizationId.OrganizationalUnit.DistinguishedName);
			}
			if (this.SoftDeletedUser.IsPresent)
			{
				recipientSession.SessionSettings.IncludeSoftDeletedObjects = true;
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(recipientSession.DomainController, searchRoot, recipientSession.Lcid, recipientSession.ReadOnly, recipientSession.ConsistencyMode, recipientSession.NetworkCredential, recipientSession.SessionSettings, 74, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\user\\GetADUserBase.cs");
				tenantOrRootOrgRecipientSession.EnforceDefaultScope = recipientSession.EnforceDefaultScope;
				tenantOrRootOrgRecipientSession.UseGlobalCatalog = recipientSession.UseGlobalCatalog;
				tenantOrRootOrgRecipientSession.LinkResolutionServer = recipientSession.LinkResolutionServer;
				recipientSession = tenantOrRootOrgRecipientSession;
			}
			return recipientSession;
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x0003C6EB File Offset: 0x0003A8EB
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetADUserBase<UserIdParameter>.SortPropertiesArray;
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0003C6F2 File Offset: 0x0003A8F2
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return User.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x0003C6FF File Offset: 0x0003A8FF
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<UserSchema>();
			}
		}

		// Token: 0x040002F9 RID: 761
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

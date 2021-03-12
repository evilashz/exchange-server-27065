using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000796 RID: 1942
	public abstract class SetTenantXsoObjectWithFolderIdentityTaskBase<TDataObject> : SetTenantADTaskBase<MailboxFolderIdParameter, TDataObject, TDataObject> where TDataObject : IConfigurable, new()
	{
		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x00118BC2 File Offset: 0x00116DC2
		// (set) Token: 0x06004470 RID: 17520 RVA: 0x00118BCA File Offset: 0x00116DCA
		internal MailboxFolderDataProviderBase InnerMailboxFolderDataProvider { get; set; }

		// Token: 0x06004471 RID: 17521 RVA: 0x00118BD3 File Offset: 0x00116DD3
		public SetTenantXsoObjectWithFolderIdentityTaskBase()
		{
		}

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x00118BDB File Offset: 0x00116DDB
		// (set) Token: 0x06004473 RID: 17523 RVA: 0x00118BE3 File Offset: 0x00116DE3
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override MailboxFolderIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x00118BEC File Offset: 0x00116DEC
		protected virtual ADUser PrepareMailboxUser()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 63, "PrepareMailboxUser", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\SetTenantXsoObjectWithFolderIdentityTaskBase.cs");
			MailboxIdParameter mailboxIdParameter;
			if (null == this.Identity.InternalMailboxFolderId)
			{
				if (this.Identity.RawOwner != null)
				{
					mailboxIdParameter = this.Identity.RawOwner;
				}
				else
				{
					ADObjectId adObjectId;
					if (!base.TryGetExecutingUserId(out adObjectId))
					{
						throw new ExecutingUserPropertyNotFoundException("executingUserid");
					}
					mailboxIdParameter = new MailboxIdParameter(adObjectId);
				}
			}
			else
			{
				mailboxIdParameter = new MailboxIdParameter(this.Identity.InternalMailboxFolderId.MailboxOwnerId);
			}
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(mailboxIdParameter.ToString())));
			IDirectorySession session = tenantOrRootOrgRecipientSession;
			if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(tenantOrRootOrgRecipientSession, aduser))
			{
				session = TaskHelper.UnderscopeSessionToOrganization(tenantOrRootOrgRecipientSession, aduser.OrganizationId, true);
			}
			base.VerifyIsWithinScopes(session, aduser, true, new DataAccessTask<TDataObject>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
			if (this.Identity.InternalMailboxFolderId == null)
			{
				this.Identity.InternalMailboxFolderId = new Microsoft.Exchange.Data.Storage.Management.MailboxFolderId(aduser.Id, this.Identity.RawFolderStoreId, this.Identity.RawFolderPath);
			}
			return aduser;
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x00118D25 File Offset: 0x00116F25
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x00118D38 File Offset: 0x00116F38
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.InnerMailboxFolderDataProvider != null)
			{
				this.InnerMailboxFolderDataProvider.Dispose();
				this.InnerMailboxFolderDataProvider = null;
			}
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x00118D5E File Offset: 0x00116F5E
		protected override void InternalStateReset()
		{
			if (this.InnerMailboxFolderDataProvider != null)
			{
				this.InnerMailboxFolderDataProvider.Dispose();
				this.InnerMailboxFolderDataProvider = null;
			}
			base.InternalStateReset();
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x00118D80 File Offset: 0x00116F80
		protected override void ResolveCurrentOrgIdBasedOnIdentity(IIdentityParameter identity)
		{
			MailboxFolderIdParameter identity2 = this.Identity;
			if (identity2 != null && identity2.RawOwner != null && base.CurrentOrganizationId != null && base.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				OrganizationId organizationId = identity2.RawOwner.ResolveOrganizationIdBasedOnIdentity(base.ExecutingUserOrganizationId);
				if (organizationId != null && !organizationId.Equals(base.CurrentOrganizationId))
				{
					base.SetCurrentOrganizationWithScopeSet(organizationId);
				}
			}
		}
	}
}

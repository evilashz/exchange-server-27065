using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007C3 RID: 1987
	public abstract class SetMailboxConfigurationTaskBase<TDataObject> : SetTenantADTaskBase<MailboxIdParameter, TDataObject, TDataObject> where TDataObject : IConfigurable, new()
	{
		// Token: 0x060045C3 RID: 17859 RVA: 0x0011EC3A File Offset: 0x0011CE3A
		public SetMailboxConfigurationTaskBase()
		{
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x0011EC44 File Offset: 0x0011CE44
		protected sealed override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 51, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\UserOptions\\SetMailboxConfigurationTaskBase.cs");
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.Identity, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(this.Identity.ToString())));
			StoreTasksHelper.CheckUserVersion(aduser, new Task.TaskErrorLoggingDelegate(base.WriteError));
			IDirectorySession directorySession = tenantOrRootOrgRecipientSession;
			if (this.ReadUserFromDC)
			{
				IRecipientSession tenantOrRootOrgRecipientSession2 = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 70, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\UserOptions\\SetMailboxConfigurationTaskBase.cs");
				tenantOrRootOrgRecipientSession2.UseGlobalCatalog = false;
				if (aduser.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					tenantOrRootOrgRecipientSession2.EnforceDefaultScope = false;
				}
				ADUser aduser2 = (ADUser)tenantOrRootOrgRecipientSession2.Read<ADUser>(aduser.Identity);
				if (aduser2 != null)
				{
					aduser = aduser2;
					directorySession = tenantOrRootOrgRecipientSession2;
				}
			}
			if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(directorySession, aduser))
			{
				directorySession = TaskHelper.UnderscopeSessionToOrganization(directorySession, aduser.OrganizationId, true);
			}
			base.VerifyIsWithinScopes(directorySession, aduser, true, new DataAccessTask<TDataObject>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
			this.mailboxStoreIdParameter = new MailboxStoreIdParameter(new MailboxStoreIdentity(aduser.Id));
			return this.CreateMailboxDataProvider(aduser);
		}

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x0011ED84 File Offset: 0x0011CF84
		protected virtual bool ReadUserFromDC
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x0011ED88 File Offset: 0x0011CF88
		protected virtual IConfigDataProvider CreateMailboxDataProvider(ADUser adUser)
		{
			return new MailboxStoreTypeProvider(adUser)
			{
				MailboxSession = StoreTasksHelper.OpenMailboxSession(ExchangePrincipal.FromADUser(base.SessionSettings, adUser, RemotingOptions.AllowCrossSite), "GetMailboxConfigurationTaskBase")
			};
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x0011EDBA File Offset: 0x0011CFBA
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x0011EDCD File Offset: 0x0011CFCD
		protected override IConfigurable ResolveDataObject()
		{
			return base.GetDataObject(this.mailboxStoreIdParameter);
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x0011EDDB File Offset: 0x0011CFDB
		protected override void InternalStateReset()
		{
			StoreTasksHelper.CleanupMailboxStoreTypeProvider(base.DataSession);
			base.InternalStateReset();
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x0011EDEE File Offset: 0x0011CFEE
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				StoreTasksHelper.CleanupMailboxStoreTypeProvider(base.DataSession);
			}
		}

		// Token: 0x04002AEB RID: 10987
		private MailboxStoreIdParameter mailboxStoreIdParameter;
	}
}

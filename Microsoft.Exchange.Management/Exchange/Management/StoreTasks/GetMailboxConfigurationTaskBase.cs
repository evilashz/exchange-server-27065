using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007C1 RID: 1985
	public abstract class GetMailboxConfigurationTaskBase<TDataObject> : GetTenantADObjectWithIdentityTaskBase<MailboxIdParameter, TDataObject> where TDataObject : IConfigurable, new()
	{
		// Token: 0x060045B8 RID: 17848 RVA: 0x0011E9C7 File Offset: 0x0011CBC7
		public GetMailboxConfigurationTaskBase()
		{
		}

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x060045B9 RID: 17849 RVA: 0x0011E9CF File Offset: 0x0011CBCF
		// (set) Token: 0x060045BA RID: 17850 RVA: 0x0011E9D7 File Offset: 0x0011CBD7
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override MailboxIdParameter Identity
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

		// Token: 0x060045BB RID: 17851 RVA: 0x0011E9E0 File Offset: 0x0011CBE0
		protected sealed override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 68, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\UserOptions\\GetMailboxConfigurationTaskBase.cs");
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.Identity, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(this.Identity.ToString())));
			StoreTasksHelper.CheckUserVersion(aduser, new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (this.ReadUserFromDC)
			{
				IRecipientSession tenantOrRootOrgRecipientSession2 = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 85, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\UserOptions\\GetMailboxConfigurationTaskBase.cs");
				tenantOrRootOrgRecipientSession2.UseGlobalCatalog = false;
				if (aduser.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					tenantOrRootOrgRecipientSession2.EnforceDefaultScope = false;
				}
				ADUser aduser2 = (ADUser)tenantOrRootOrgRecipientSession2.Read<ADUser>(aduser.Identity);
				if (aduser2 != null)
				{
					aduser = aduser2;
				}
			}
			this.mailboxStoreIdParameter = new MailboxStoreIdParameter(new MailboxStoreIdentity(aduser.Id));
			return this.CreateMailboxDataProvider(aduser);
		}

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x060045BC RID: 17852 RVA: 0x0011EAED File Offset: 0x0011CCED
		protected virtual bool ReadUserFromDC
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x0011EAF0 File Offset: 0x0011CCF0
		protected virtual IConfigDataProvider CreateMailboxDataProvider(ADUser adUser)
		{
			return new MailboxStoreTypeProvider(adUser)
			{
				MailboxSession = StoreTasksHelper.OpenMailboxSession(ExchangePrincipal.FromADUser(base.SessionSettings, adUser, RemotingOptions.AllowCrossSite), "GetMailboxConfigurationTaskBase")
			};
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x0011EB22 File Offset: 0x0011CD22
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x0011EB38 File Offset: 0x0011CD38
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			try
			{
				LocalizedString? localizedString;
				IEnumerable<TDataObject> dataObjects = base.GetDataObjects(this.mailboxStoreIdParameter, base.OptionalIdentityData, out localizedString);
				this.WriteResult<TDataObject>(dataObjects);
				if (!base.HasErrors && base.WriteObjectCount == 0U)
				{
					base.WriteError(new ManagementObjectNotFoundException(localizedString ?? base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(TDataObject).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ErrorCategory.InvalidData, null);
				}
			}
			catch (FormatException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, this.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x0011EC08 File Offset: 0x0011CE08
		protected override void InternalStateReset()
		{
			StoreTasksHelper.CleanupMailboxStoreTypeProvider(base.DataSession);
			base.InternalStateReset();
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x0011EC1B File Offset: 0x0011CE1B
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				StoreTasksHelper.CleanupMailboxStoreTypeProvider(base.DataSession);
			}
		}

		// Token: 0x04002AEA RID: 10986
		private MailboxStoreIdParameter mailboxStoreIdParameter;
	}
}

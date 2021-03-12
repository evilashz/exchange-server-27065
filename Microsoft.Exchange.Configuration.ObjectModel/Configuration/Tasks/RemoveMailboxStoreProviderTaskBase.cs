using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000088 RID: 136
	public abstract class RemoveMailboxStoreProviderTaskBase<TIdentity, TDataObject> : RemoveTenantADTaskBase<TIdentity, TDataObject> where TIdentity : MailboxStoreIdParameter where TDataObject : IConfigurable, new()
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00015368 File Offset: 0x00013568
		protected ADObjectId MailboxOwnerId
		{
			get
			{
				return this.mailboxOwnerId;
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00015370 File Offset: 0x00013570
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 47, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\RemoveMailboxStoreProviderTaskBase.cs");
			TIdentity identity = this.Identity;
			MailboxIdParameter mailboxIdParameter = new MailboxIdParameter(identity.GetADUserName());
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, tenantOrRootOrgRecipientSession, null, null, new LocalizedString?(Strings.ErrorUserNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(mailboxIdParameter.ToString())));
			this.mailboxOwnerId = aduser.Id;
			return this.CreateMailboxDataProvider(aduser);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00015404 File Offset: 0x00013604
		protected virtual IConfigDataProvider CreateMailboxDataProvider(ADUser adUser)
		{
			return new MailboxStoreTypeProvider(adUser);
		}

		// Token: 0x0400012E RID: 302
		private ADObjectId mailboxOwnerId;
	}
}

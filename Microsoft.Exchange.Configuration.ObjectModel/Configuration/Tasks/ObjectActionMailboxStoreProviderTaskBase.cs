using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200007C RID: 124
	public abstract class ObjectActionMailboxStoreProviderTaskBase<TIdentity, TDataObject> : ObjectActionTenantADTask<TIdentity, TDataObject> where TIdentity : MailboxStoreIdParameter, new() where TDataObject : IConfigurable, new()
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000116BD File Offset: 0x0000F8BD
		protected ADObjectId MailboxOwnerId
		{
			get
			{
				return this.mailboxOwnerId;
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000116C8 File Offset: 0x0000F8C8
		protected sealed override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 45, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\ObjectActionMailboxStoreProviderTaskBase.cs");
			TIdentity identity = this.Identity;
			MailboxIdParameter mailboxIdParameter = new MailboxIdParameter(identity.GetADUserName());
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, tenantOrRootOrgRecipientSession, null, null, new LocalizedString?(Strings.ErrorUserNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(mailboxIdParameter.ToString())));
			this.mailboxOwnerId = aduser.Id;
			return this.CreateMailboxDataProvider(aduser);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001175C File Offset: 0x0000F95C
		protected virtual IConfigDataProvider CreateMailboxDataProvider(ADUser adUser)
		{
			return new MailboxStoreTypeProvider(adUser);
		}

		// Token: 0x04000119 RID: 281
		private ADObjectId mailboxOwnerId;
	}
}

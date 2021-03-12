using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000060 RID: 96
	public abstract class GetMailboxStoreProviderTaskBase<TIdentity, TDataObject> : GetTenantADObjectWithIdentityTaskBase<TIdentity, TDataObject> where TIdentity : MailboxStoreIdParameter where TDataObject : IConfigurable, new()
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000EF25 File Offset: 0x0000D125
		protected ADObjectId MailboxOwnerId
		{
			get
			{
				return this.mailboxOwnerId;
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000EF30 File Offset: 0x0000D130
		protected sealed override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 45, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\GetMailboxStoreProviderTaskBase.cs");
			TIdentity identity = this.Identity;
			MailboxIdParameter mailboxIdParameter = new MailboxIdParameter(identity.GetADUserName());
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, tenantOrRootOrgRecipientSession, null, null, new LocalizedString?(Strings.ErrorUserNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(mailboxIdParameter.ToString())));
			this.mailboxOwnerId = aduser.Id;
			return this.CreateMailboxDataProvider(aduser);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000EFC5 File Offset: 0x0000D1C5
		protected virtual IConfigDataProvider CreateMailboxDataProvider(ADUser adUser)
		{
			return new MailboxStoreTypeProvider(adUser);
		}

		// Token: 0x040000FC RID: 252
		private ADObjectId mailboxOwnerId;
	}
}

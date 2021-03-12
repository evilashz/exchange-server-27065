using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000098 RID: 152
	public abstract class SetMailboxStoreProviderTaskBase<TIdentity, TPublicObject, TDataObject> : SetTenantADTaskBase<TIdentity, TPublicObject, TDataObject> where TIdentity : MailboxStoreIdParameter, new() where TPublicObject : IConfigurable, new() where TDataObject : IConfigurable, new()
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x000168AC File Offset: 0x00014AAC
		protected ADObjectId MailboxOwnerId
		{
			get
			{
				return this.mailboxOwnerId;
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000168B4 File Offset: 0x00014AB4
		protected sealed override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 47, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\SetMailboxStoreProviderTaskBase.cs");
			TIdentity identity = this.Identity;
			MailboxIdParameter mailboxIdParameter = new MailboxIdParameter(identity.GetADUserName());
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, tenantOrRootOrgRecipientSession, null, null, new LocalizedString?(Strings.ErrorUserNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(mailboxIdParameter.ToString())));
			this.mailboxOwnerId = aduser.Id;
			return this.CreateMailboxDataProvider(aduser);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00016948 File Offset: 0x00014B48
		protected virtual IConfigDataProvider CreateMailboxDataProvider(ADUser adUser)
		{
			return new MailboxStoreTypeProvider(adUser);
		}

		// Token: 0x04000137 RID: 311
		private ADObjectId mailboxOwnerId;
	}
}

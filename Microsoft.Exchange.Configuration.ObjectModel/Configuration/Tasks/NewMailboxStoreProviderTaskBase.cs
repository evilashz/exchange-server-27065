using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200007A RID: 122
	public abstract class NewMailboxStoreProviderTaskBase<TDataObject> : NewTenantADTaskBase<TDataObject> where TDataObject : IConfigurable, new()
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00011508 File Offset: 0x0000F708
		protected ADObjectId MailboxOwnerId
		{
			get
			{
				return this.mailboxOwnerId;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00011510 File Offset: 0x0000F710
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x00011527 File Offset: 0x0000F727
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
		public virtual MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001153C File Offset: 0x0000F73C
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 62, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\NewMailboxStoreProviderTaskBase.cs");
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.Mailbox, tenantOrRootOrgRecipientSession, null, null, new LocalizedString?(Strings.ErrorUserNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(this.Mailbox.ToString())));
			this.mailboxOwnerId = aduser.Id;
			return this.CreateMailboxDataProvider(aduser);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000115C5 File Offset: 0x0000F7C5
		protected virtual IConfigDataProvider CreateMailboxDataProvider(ADUser adUser)
		{
			return new MailboxStoreTypeProvider(adUser);
		}

		// Token: 0x04000118 RID: 280
		private ADObjectId mailboxOwnerId;
	}
}

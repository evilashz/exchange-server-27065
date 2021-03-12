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
	// Token: 0x02000793 RID: 1939
	public abstract class GetTenantXsoObjectWithFolderIdentityTaskBase<TDataObject> : GetTenantADObjectWithIdentityTaskBase<MailboxFolderIdParameter, TDataObject> where TDataObject : IConfigurable, new()
	{
		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x06004451 RID: 17489 RVA: 0x001184B3 File Offset: 0x001166B3
		// (set) Token: 0x06004452 RID: 17490 RVA: 0x001184BB File Offset: 0x001166BB
		internal MailboxFolderDataProviderBase InnerMailboxFolderDataProvider { get; set; }

		// Token: 0x06004453 RID: 17491 RVA: 0x001184C4 File Offset: 0x001166C4
		public GetTenantXsoObjectWithFolderIdentityTaskBase()
		{
		}

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x06004454 RID: 17492 RVA: 0x001184CC File Offset: 0x001166CC
		// (set) Token: 0x06004455 RID: 17493 RVA: 0x001184D4 File Offset: 0x001166D4
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

		// Token: 0x06004456 RID: 17494 RVA: 0x001184E0 File Offset: 0x001166E0
		protected virtual ADUser PrepareMailboxUser()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 63, "PrepareMailboxUser", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\GetTenantXsoObjectWithFolderIdentityTaskBase.cs");
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
			if (this.Identity.InternalMailboxFolderId == null)
			{
				this.Identity.InternalMailboxFolderId = new Microsoft.Exchange.Data.Storage.Management.MailboxFolderId(aduser.Id, this.Identity.RawFolderStoreId, this.Identity.RawFolderPath);
			}
			return aduser;
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x001185E8 File Offset: 0x001167E8
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x06004458 RID: 17496 RVA: 0x001185FB File Offset: 0x001167FB
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.InnerMailboxFolderDataProvider != null)
			{
				this.InnerMailboxFolderDataProvider.Dispose();
				this.InnerMailboxFolderDataProvider = null;
			}
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x00118621 File Offset: 0x00116821
		protected override void InternalStateReset()
		{
			if (this.InnerMailboxFolderDataProvider != null)
			{
				this.InnerMailboxFolderDataProvider.Dispose();
				this.InnerMailboxFolderDataProvider = null;
			}
			base.InternalStateReset();
		}
	}
}

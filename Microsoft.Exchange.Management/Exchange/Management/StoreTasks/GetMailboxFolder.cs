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
	// Token: 0x020007C7 RID: 1991
	[Cmdlet("Get", "MailboxFolder", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxFolder : GetTenantXsoObjectWithFolderIdentityTaskBase<MailboxFolder>
	{
		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x060045E5 RID: 17893 RVA: 0x0011F4C3 File Offset: 0x0011D6C3
		// (set) Token: 0x060045E6 RID: 17894 RVA: 0x0011F4CB File Offset: 0x0011D6CB
		[Parameter(ParameterSetName = "Recurse", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Parameter(ParameterSetName = "GetChildren", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[Parameter(ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
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

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x060045E7 RID: 17895 RVA: 0x0011F4D4 File Offset: 0x0011D6D4
		// (set) Token: 0x060045E8 RID: 17896 RVA: 0x0011F4FA File Offset: 0x0011D6FA
		[Parameter(Mandatory = true, ParameterSetName = "Recurse")]
		public SwitchParameter Recurse
		{
			get
			{
				return (SwitchParameter)(base.Fields["Recurse"] ?? false);
			}
			set
			{
				base.Fields["Recurse"] = value;
			}
		}

		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x060045E9 RID: 17897 RVA: 0x0011F512 File Offset: 0x0011D712
		// (set) Token: 0x060045EA RID: 17898 RVA: 0x0011F538 File Offset: 0x0011D738
		[Parameter(Mandatory = true, ParameterSetName = "GetChildren")]
		public SwitchParameter GetChildren
		{
			get
			{
				return (SwitchParameter)(base.Fields["GetChildren"] ?? false);
			}
			set
			{
				base.Fields["GetChildren"] = value;
			}
		}

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x060045EB RID: 17899 RVA: 0x0011F550 File Offset: 0x0011D750
		// (set) Token: 0x060045EC RID: 17900 RVA: 0x0011F576 File Offset: 0x0011D776
		[Parameter(ParameterSetName = "GetChildren")]
		[Parameter(ParameterSetName = "Recurse")]
		public SwitchParameter MailFolderOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["MailFolderOnly"] ?? false);
			}
			set
			{
				base.Fields["MailFolderOnly"] = value;
			}
		}

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x060045ED RID: 17901 RVA: 0x0011F58E File Offset: 0x0011D78E
		// (set) Token: 0x060045EE RID: 17902 RVA: 0x0011F596 File Offset: 0x0011D796
		[Parameter(ParameterSetName = "Recurse")]
		[Parameter(ParameterSetName = "GetChildren")]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x0011F5A0 File Offset: 0x0011D7A0
		protected sealed override IConfigDataProvider CreateSession()
		{
			this.rootId = null;
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 104, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\UserOptions\\MailboxFolder\\GetMailboxFolder.cs");
			ADObjectId adobjectId;
			bool flag = base.TryGetExecutingUserId(out adobjectId);
			MailboxIdParameter mailboxIdParameter;
			if (this.Identity == null)
			{
				if (!flag)
				{
					throw new ExecutingUserPropertyNotFoundException("executingUserid");
				}
				mailboxIdParameter = new MailboxIdParameter(adobjectId);
				if (!this.Recurse.IsPresent && !this.GetChildren.IsPresent)
				{
					this.Identity = new MailboxFolderIdParameter(adobjectId);
				}
			}
			else if (null == this.Identity.InternalMailboxFolderId)
			{
				if (this.Identity.RawOwner != null)
				{
					mailboxIdParameter = this.Identity.RawOwner;
				}
				else
				{
					if (!flag)
					{
						throw new ExecutingUserPropertyNotFoundException("executingUserid");
					}
					mailboxIdParameter = new MailboxIdParameter(adobjectId);
				}
			}
			else
			{
				mailboxIdParameter = new MailboxIdParameter(this.Identity.InternalMailboxFolderId.MailboxOwnerId);
			}
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(mailboxIdParameter.ToString())));
			if (this.Identity != null && null == this.Identity.InternalMailboxFolderId)
			{
				this.Identity.InternalMailboxFolderId = new Microsoft.Exchange.Data.Storage.Management.MailboxFolderId(aduser.Id, this.Identity.RawFolderStoreId, this.Identity.RawFolderPath);
			}
			StoreTasksHelper.CheckUserVersion(aduser, new Task.TaskErrorLoggingDelegate(base.WriteError));
			if ((this.Recurse.IsPresent || this.GetChildren.IsPresent) && this.Identity != null)
			{
				this.rootId = this.Identity.InternalMailboxFolderId;
				this.Identity = null;
			}
			base.InnerMailboxFolderDataProvider = new MailboxFolderDataProvider(base.SessionSettings, aduser, (base.ExchangeRunspaceConfig == null) ? null : base.ExchangeRunspaceConfig.SecurityAccessToken, "Get-MailboxFolder");
			return base.InnerMailboxFolderDataProvider;
		}

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x060045F0 RID: 17904 RVA: 0x0011F790 File Offset: 0x0011D990
		protected override ObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x060045F1 RID: 17905 RVA: 0x0011F798 File Offset: 0x0011D998
		protected override bool DeepSearch
		{
			get
			{
				return this.Recurse.IsPresent;
			}
		}

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x060045F2 RID: 17906 RVA: 0x0011F7B4 File Offset: 0x0011D9B4
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (this.MailFolderOnly.IsPresent)
				{
					return new OrFilter(new QueryFilter[]
					{
						new NotFilter(new ExistsFilter(StoreObjectSchema.ContainerClass)),
						new TextFilter(StoreObjectSchema.ContainerClass, "IPF.Note", MatchOptions.Prefix, MatchFlags.IgnoreCase)
					});
				}
				return null;
			}
		}

		// Token: 0x04002AF2 RID: 10994
		private const string ParameterRecurse = "Recurse";

		// Token: 0x04002AF3 RID: 10995
		private const string ParameterGetChildren = "GetChildren";

		// Token: 0x04002AF4 RID: 10996
		private const string ParameterMailFolderOnly = "MailFolderOnly";

		// Token: 0x04002AF5 RID: 10997
		private const string ParameterSetRecurse = "Recurse";

		// Token: 0x04002AF6 RID: 10998
		private const string ParameterSetGetChildren = "GetChildren";

		// Token: 0x04002AF7 RID: 10999
		private Microsoft.Exchange.Data.Storage.Management.MailboxFolderId rootId;
	}
}

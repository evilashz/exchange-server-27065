using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000087 RID: 135
	[Cmdlet("get", "MailUser", DefaultParameterSetName = "Identity")]
	public sealed class GetMailUser : GetMailUserBase<MailUserIdParameter>
	{
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00027E92 File Offset: 0x00026092
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x00027EA9 File Offset: 0x000260A9
		[ValidateNotNullOrEmpty]
		[Parameter(ParameterSetName = "Database", ValueFromPipeline = true)]
		public DatabaseIdParameter ArchiveDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields[ADUserSchema.ArchiveDatabase];
			}
			set
			{
				base.Fields[ADUserSchema.ArchiveDatabase] = value;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00027EBC File Offset: 0x000260BC
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x00027EC4 File Offset: 0x000260C4
		[Parameter(Mandatory = false)]
		public new long UsnForReconciliationSearch
		{
			get
			{
				return base.UsnForReconciliationSearch;
			}
			set
			{
				base.UsnForReconciliationSearch = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00027ECD File Offset: 0x000260CD
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x00027ED5 File Offset: 0x000260D5
		[Parameter(Mandatory = false)]
		public SwitchParameter SoftDeletedMailUser
		{
			get
			{
				return base.SoftDeletedObject;
			}
			set
			{
				base.SoftDeletedObject = value;
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00027EE0 File Offset: 0x000260E0
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			ADObjectId rootId = recipientSession.SearchRoot;
			if (this.SoftDeletedMailUser.IsPresent && base.CurrentOrganizationId != null && base.CurrentOrganizationId.OrganizationalUnit != null)
			{
				rootId = new ADObjectId("OU=Soft Deleted Objects," + base.CurrentOrganizationId.OrganizationalUnit.DistinguishedName);
			}
			if (this.SoftDeletedMailUser.IsPresent)
			{
				recipientSession = SoftDeletedTaskHelper.GetSessionForSoftDeletedObjects(recipientSession, rootId);
			}
			return recipientSession;
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00027F64 File Offset: 0x00026164
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = MailUserIdParameter.GetMailUserRecipientTypeDetailsFilter();
				if (this.ArchiveDatabase != null)
				{
					MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.ArchiveDatabase, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.ArchiveDatabase.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.ArchiveDatabase.ToString())));
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.ArchiveDatabaseRaw, mailboxDatabase.Id)
					});
				}
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.InternalFilter,
					queryFilter
				});
			}
		}
	}
}

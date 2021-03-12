using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay.Monitoring;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008A1 RID: 2209
	[OutputType(new Type[]
	{
		typeof(DatabaseRedundancy)
	})]
	[Cmdlet("Get", "MailboxDatabaseRedundancy", DefaultParameterSetName = "Identity")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class GetMailboxDatabaseRedundancy : GetRedundancyTaskBase<DatabaseIdParameter, Database>
	{
		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x06004DA7 RID: 19879 RVA: 0x00143A54 File Offset: 0x00141C54
		// (set) Token: 0x06004DA8 RID: 19880 RVA: 0x00143A5C File Offset: 0x00141C5C
		[ValidateNotNullOrEmpty]
		[Alias(new string[]
		{
			"Database"
		})]
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override DatabaseIdParameter Identity
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

		// Token: 0x06004DA9 RID: 19881 RVA: 0x00143A68 File Offset: 0x00141C68
		protected override ADObjectId LookupIdentityObjectAndGetDagId()
		{
			Database database = this.LookupDatabase(this.Identity);
			if (database.MasterType != MasterType.DatabaseAvailabilityGroup)
			{
				base.WriteError(new DatabaseMustBeInDagException(database.Name), ErrorCategory.InvalidOperation, this.Identity);
				return null;
			}
			ADObjectId masterServerOrAvailabilityGroup = database.MasterServerOrAvailabilityGroup;
			if (masterServerOrAvailabilityGroup == null)
			{
				base.WriteError(new DatabaseMustBeInDagException(database.Name), ErrorCategory.InvalidOperation, this.Identity);
				return null;
			}
			return masterServerOrAvailabilityGroup;
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x00143ACC File Offset: 0x00141CCC
		protected override void WriteResultsFromHealthInfo(HealthInfoPersisted hip, string serverContactedFqdn)
		{
			bool flag = false;
			foreach (DbHealthInfoPersisted dbHealthInfoPersisted in hip.Databases)
			{
				if (this.Identity == null || dbHealthInfoPersisted.DbName.Equals(this.Identity.RawIdentity, StringComparison.InvariantCultureIgnoreCase))
				{
					flag = true;
					DatabaseRedundancy dataObject = new DatabaseRedundancy(hip, dbHealthInfoPersisted, serverContactedFqdn);
					this.WriteResult(dataObject);
				}
			}
			if (!flag)
			{
				this.WriteWarning(Strings.GetDagHealthInfoNoResultsWarning);
			}
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x00143B5C File Offset: 0x00141D5C
		private Database LookupDatabase(DatabaseIdParameter dbParam)
		{
			ADObjectId id = new DatabasesContainer().Id;
			Database database = (Database)base.GetDataObject<Database>(dbParam, base.GlobalConfigSession, id, new LocalizedString?(Strings.ErrorDatabaseNotFound(dbParam.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(dbParam.ToString())));
			if (!database.IsExchange2009OrLater)
			{
				base.WriteError(new ErrorDatabaseWrongVersion(database.Name), ErrorCategory.InvalidOperation, dbParam);
				return null;
			}
			return database;
		}
	}
}

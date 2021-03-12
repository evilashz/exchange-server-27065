using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004F1 RID: 1265
	[Cmdlet("Get", "MigrationEndpoint", DefaultParameterSetName = "Identity", SupportsShouldProcess = false)]
	public sealed class GetMigrationEndpoint : MigrationGetTaskBase<MigrationEndpointIdParameter, MigrationEndpoint>
	{
		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x000B46FD File Offset: 0x000B28FD
		// (set) Token: 0x06002CFE RID: 11518 RVA: 0x000B471E File Offset: 0x000B291E
		[Parameter(Mandatory = true, ParameterSetName = "TypeFilter")]
		public MigrationType Type
		{
			get
			{
				return (MigrationType)(base.Fields["Type"] ?? MigrationType.None);
			}
			set
			{
				base.Fields["Type"] = value;
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x000B4736 File Offset: 0x000B2936
		// (set) Token: 0x06002D00 RID: 11520 RVA: 0x000B474D File Offset: 0x000B294D
		[Parameter(Mandatory = true, ParameterSetName = "ConnectionSettingsFilter")]
		[ValidateNotNull]
		public ExchangeConnectionSettings ConnectionSettings
		{
			get
			{
				return (ExchangeConnectionSettings)base.Fields["ConnectionSettings"];
			}
			set
			{
				base.Fields["ConnectionSettings"] = value;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x000B4760 File Offset: 0x000B2960
		public override string Action
		{
			get
			{
				return "GetMigrationEndpoint";
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06002D02 RID: 11522 RVA: 0x000B4768 File Offset: 0x000B2968
		protected override QueryFilter InternalFilter
		{
			get
			{
				string parameterSetName;
				if ((parameterSetName = base.ParameterSetName) != null)
				{
					if (parameterSetName == "Identity")
					{
						MigrationEndpointId migrationEndpointId = (this.Identity == null) ? MigrationEndpointId.Any : this.Identity.MigrationEndpointId;
						return migrationEndpointId.GetFilter();
					}
					if (parameterSetName == "ConnectionSettingsFilter")
					{
						return MigrationEndpointDataProvider.GetFilterFromConnectionSettings(this.ConnectionSettings);
					}
					if (parameterSetName == "TypeFilter")
					{
						return MigrationEndpointDataProvider.GetFilterFromEndpointType(this.Type);
					}
				}
				return null;
			}
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000B47E4 File Offset: 0x000B29E4
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = "Get-MigrationEndpoint";
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			MigrationEndpointDataProvider migrationEndpointDataProvider = MigrationEndpointDataProvider.CreateDataProvider(this.Action, base.TenantGlobalCatalogSession, this.partitionMailbox);
			if (base.Diagnostic || !string.IsNullOrEmpty(base.DiagnosticArgument))
			{
				migrationEndpointDataProvider.EnableDiagnostics(base.DiagnosticArgument);
			}
			return migrationEndpointDataProvider;
		}

		// Token: 0x04002075 RID: 8309
		private const string ParameterNameType = "Type";

		// Token: 0x04002076 RID: 8310
		private const string ParameterNameConnectionSettings = "ConnectionSettings";

		// Token: 0x04002077 RID: 8311
		private const string ParameterSetNameTypeFilter = "TypeFilter";

		// Token: 0x04002078 RID: 8312
		private const string ParameterSetNameConnectionSettingsFilter = "ConnectionSettingsFilter";
	}
}

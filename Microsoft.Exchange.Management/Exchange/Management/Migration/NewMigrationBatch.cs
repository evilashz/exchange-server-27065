using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004E7 RID: 1255
	[Cmdlet("New", "MigrationBatch", DefaultParameterSetName = "Onboarding", SupportsShouldProcess = true)]
	public sealed class NewMigrationBatch : NewMigrationObjectTaskBase<MigrationBatch>
	{
		// Token: 0x06002BD9 RID: 11225 RVA: 0x000AF114 File Offset: 0x000AD314
		public NewMigrationBatch()
		{
			this.userDataProvider = new Lazy<MigrationUserDataProvider>(() => MigrationUserDataProvider.CreateDataProvider((MigrationDataProvider)((MigrationBatchDataProvider)base.DataSession).MailboxProvider, base.ExecutingUserIdentityName), LazyThreadSafetyMode.PublicationOnly);
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06002BDA RID: 11226 RVA: 0x000AF146 File Offset: 0x000AD346
		// (set) Token: 0x06002BDB RID: 11227 RVA: 0x000AF15D File Offset: 0x000AD35D
		[Parameter(Mandatory = false, ParameterSetName = "Onboarding")]
		[Parameter(Mandatory = true, ParameterSetName = "LocalPublicFolder")]
		[Parameter(Mandatory = true, ParameterSetName = "XO1")]
		[Parameter(Mandatory = true, ParameterSetName = "Offboarding")]
		[Parameter(Mandatory = true, ParameterSetName = "Local")]
		public byte[] CSVData
		{
			get
			{
				return (byte[])base.Fields["dataBlob"];
			}
			set
			{
				base.Fields["dataBlob"] = value;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06002BDC RID: 11228 RVA: 0x000AF170 File Offset: 0x000AD370
		// (set) Token: 0x06002BDD RID: 11229 RVA: 0x000AF191 File Offset: 0x000AD391
		[Parameter(Mandatory = false)]
		public bool AllowUnknownColumnsInCsv
		{
			get
			{
				return (bool)(base.Fields["AllowUnknownColumnsInCsv"] ?? false);
			}
			set
			{
				base.Fields["AllowUnknownColumnsInCsv"] = value;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x000AF1A9 File Offset: 0x000AD3A9
		// (set) Token: 0x06002BDF RID: 11231 RVA: 0x000AF1C0 File Offset: 0x000AD3C0
		[Parameter(Mandatory = true)]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x000AF1D3 File Offset: 0x000AD3D3
		// (set) Token: 0x06002BE1 RID: 11233 RVA: 0x000AF1EA File Offset: 0x000AD3EA
		[Parameter(Mandatory = false)]
		public bool? AllowIncrementalSyncs
		{
			get
			{
				return (bool?)base.Fields["AllowIncrementalSyncs"];
			}
			set
			{
				base.Fields["AllowIncrementalSyncs"] = value;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06002BE2 RID: 11234 RVA: 0x000AF202 File Offset: 0x000AD402
		// (set) Token: 0x06002BE3 RID: 11235 RVA: 0x000AF219 File Offset: 0x000AD419
		[Parameter(Mandatory = false, ParameterSetName = "Onboarding")]
		public MultiValuedProperty<string> ExcludeFolders
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["ExcludeFolders"];
			}
			set
			{
				base.Fields["ExcludeFolders"] = value;
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x000AF22C File Offset: 0x000AD42C
		// (set) Token: 0x06002BE5 RID: 11237 RVA: 0x000AF243 File Offset: 0x000AD443
		[Parameter(Mandatory = false)]
		public ExTimeZoneValue TimeZone
		{
			get
			{
				return (ExTimeZoneValue)base.Fields["TimeZone"];
			}
			set
			{
				base.Fields["TimeZone"] = value;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000AF256 File Offset: 0x000AD456
		// (set) Token: 0x06002BE7 RID: 11239 RVA: 0x000AF277 File Offset: 0x000AD477
		[Parameter(Mandatory = false)]
		public SkippableMigrationSteps[] SkipSteps
		{
			get
			{
				return ((SkippableMigrationSteps[])base.Fields["SkipSteps"]) ?? new SkippableMigrationSteps[0];
			}
			set
			{
				base.Fields["SkipSteps"] = value;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x000AF28A File Offset: 0x000AD48A
		// (set) Token: 0x06002BE9 RID: 11241 RVA: 0x000AF2B0 File Offset: 0x000AD4B0
		[Parameter(Mandatory = false, ParameterSetName = "PreexistingUserIds")]
		[Parameter(Mandatory = false, ParameterSetName = "Preexisting")]
		public SwitchParameter DisableOnCopy
		{
			get
			{
				return (SwitchParameter)(base.Fields["DisableOnCopy"] ?? new SwitchParameter(true));
			}
			set
			{
				base.Fields["DisableOnCopy"] = value;
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000AF2C8 File Offset: 0x000AD4C8
		// (set) Token: 0x06002BEB RID: 11243 RVA: 0x000AF2EE File Offset: 0x000AD4EE
		[Parameter(Mandatory = false, ParameterSetName = "Local")]
		[Parameter(Mandatory = false, ParameterSetName = "Offboarding")]
		[Parameter(Mandatory = false, ParameterSetName = "Onboarding")]
		public SwitchParameter DisallowExistingUsers
		{
			get
			{
				return (SwitchParameter)(base.Fields["DisallowExistingUsers"] ?? new SwitchParameter(true));
			}
			set
			{
				base.Fields["DisallowExistingUsers"] = value;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x000AF306 File Offset: 0x000AD506
		// (set) Token: 0x06002BED RID: 11245 RVA: 0x000AF31D File Offset: 0x000AD51D
		[Parameter(Mandatory = false)]
		public CultureInfo Locale
		{
			get
			{
				return (CultureInfo)base.Fields["Locale"];
			}
			set
			{
				base.Fields["Locale"] = value;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x000AF330 File Offset: 0x000AD530
		// (set) Token: 0x06002BEF RID: 11247 RVA: 0x000AF347 File Offset: 0x000AD547
		[Parameter(Mandatory = false)]
		public int? AutoRetryCount
		{
			get
			{
				return (int?)base.Fields["AutoRetryCount"];
			}
			set
			{
				base.Fields["AutoRetryCount"] = value;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x000AF35F File Offset: 0x000AD55F
		// (set) Token: 0x06002BF1 RID: 11249 RVA: 0x000AF376 File Offset: 0x000AD576
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpAddress> NotificationEmails
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)base.Fields["NotificationEmails"];
			}
			set
			{
				base.Fields["NotificationEmails"] = value;
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x000AF389 File Offset: 0x000AD589
		// (set) Token: 0x06002BF3 RID: 11251 RVA: 0x000AF3AF File Offset: 0x000AD5AF
		[Parameter(Mandatory = false)]
		public SwitchParameter AutoStart
		{
			get
			{
				return (SwitchParameter)(base.Fields["AutoStart"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["AutoStart"] = value;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x000AF3C7 File Offset: 0x000AD5C7
		// (set) Token: 0x06002BF5 RID: 11253 RVA: 0x000AF3ED File Offset: 0x000AD5ED
		[Parameter(Mandatory = false)]
		public SwitchParameter AutoComplete
		{
			get
			{
				return (SwitchParameter)(base.Fields["AutoComplete"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["AutoComplete"] = value;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x000AF405 File Offset: 0x000AD605
		// (set) Token: 0x06002BF7 RID: 11255 RVA: 0x000AF42B File Offset: 0x000AD62B
		[Parameter(Mandatory = false, ParameterSetName = "Local")]
		[Parameter(Mandatory = false, ParameterSetName = "Offboarding")]
		[Parameter(Mandatory = false, ParameterSetName = "Onboarding")]
		public SwitchParameter ArchiveOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["ArchiveOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ArchiveOnly"] = value;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x000AF443 File Offset: 0x000AD643
		// (set) Token: 0x06002BF9 RID: 11257 RVA: 0x000AF469 File Offset: 0x000AD669
		[Parameter(Mandatory = true, ParameterSetName = "Local")]
		public SwitchParameter Local
		{
			get
			{
				return (SwitchParameter)(base.Fields["Local"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Local"] = value;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x000AF481 File Offset: 0x000AD681
		// (set) Token: 0x06002BFB RID: 11259 RVA: 0x000AF4A7 File Offset: 0x000AD6A7
		[Parameter(Mandatory = true, ParameterSetName = "XO1")]
		public SwitchParameter XO1
		{
			get
			{
				return (SwitchParameter)(base.Fields["XO1"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["XO1"] = value;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x000AF4BF File Offset: 0x000AD6BF
		// (set) Token: 0x06002BFD RID: 11261 RVA: 0x000AF4E5 File Offset: 0x000AD6E5
		[Parameter(Mandatory = false, ParameterSetName = "Onboarding")]
		[Parameter(Mandatory = false, ParameterSetName = "Offboarding")]
		[Parameter(Mandatory = false, ParameterSetName = "Local")]
		public SwitchParameter PrimaryOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["PrimaryOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["PrimaryOnly"] = value;
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000AF4FD File Offset: 0x000AD6FD
		// (set) Token: 0x06002BFF RID: 11263 RVA: 0x000AF514 File Offset: 0x000AD714
		[Parameter(Mandatory = false, ParameterSetName = "Onboarding")]
		public MigrationEndpointIdParameter SourceEndpoint
		{
			get
			{
				return (MigrationEndpointIdParameter)base.Fields["SourceEndpoint"];
			}
			set
			{
				base.Fields["SourceEndpoint"] = value;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x000AF527 File Offset: 0x000AD727
		// (set) Token: 0x06002C01 RID: 11265 RVA: 0x000AF53E File Offset: 0x000AD73E
		[Parameter(Mandatory = false, ParameterSetName = "Offboarding")]
		public MigrationEndpointIdParameter TargetEndpoint
		{
			get
			{
				return (MigrationEndpointIdParameter)base.Fields["TargetEndpoint"];
			}
			set
			{
				base.Fields["TargetEndpoint"] = value;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x000AF551 File Offset: 0x000AD751
		// (set) Token: 0x06002C03 RID: 11267 RVA: 0x000AF577 File Offset: 0x000AD777
		[Parameter(Mandatory = false, ParameterSetName = "LocalPublicFolder")]
		[Parameter(Mandatory = false, ParameterSetName = "Offboarding")]
		[Parameter(Mandatory = false, ParameterSetName = "Onboarding")]
		[Parameter(Mandatory = false, ParameterSetName = "Local")]
		public Unlimited<int> BadItemLimit
		{
			get
			{
				return (Unlimited<int>)(base.Fields["BadItemLimit"] ?? new Unlimited<int>(0));
			}
			set
			{
				base.Fields["BadItemLimit"] = value;
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06002C04 RID: 11268 RVA: 0x000AF58F File Offset: 0x000AD78F
		// (set) Token: 0x06002C05 RID: 11269 RVA: 0x000AF5B5 File Offset: 0x000AD7B5
		[Parameter(Mandatory = false, ParameterSetName = "LocalPublicFolder")]
		[Parameter(Mandatory = false, ParameterSetName = "Offboarding")]
		[Parameter(Mandatory = false, ParameterSetName = "Onboarding")]
		public Unlimited<int> LargeItemLimit
		{
			get
			{
				return (Unlimited<int>)(base.Fields["LargeItemLimit"] ?? new Unlimited<int>(0));
			}
			set
			{
				base.Fields["LargeItemLimit"] = value;
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06002C06 RID: 11270 RVA: 0x000AF5CD File Offset: 0x000AD7CD
		// (set) Token: 0x06002C07 RID: 11271 RVA: 0x000AF5E4 File Offset: 0x000AD7E4
		[Parameter(Mandatory = false, ParameterSetName = "Offboarding")]
		[Parameter(Mandatory = false, ParameterSetName = "Onboarding")]
		[Parameter(Mandatory = false, ParameterSetName = "Local")]
		public MultiValuedProperty<string> TargetArchiveDatabases
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["TargetArchiveDatabases"];
			}
			set
			{
				base.Fields["TargetArchiveDatabases"] = value;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06002C08 RID: 11272 RVA: 0x000AF5F7 File Offset: 0x000AD7F7
		// (set) Token: 0x06002C09 RID: 11273 RVA: 0x000AF60E File Offset: 0x000AD80E
		[Parameter(Mandatory = false, ParameterSetName = "Local")]
		[Parameter(Mandatory = false, ParameterSetName = "Onboarding")]
		[Parameter(Mandatory = false, ParameterSetName = "Offboarding")]
		public MultiValuedProperty<string> TargetDatabases
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["TargetDatabases"];
			}
			set
			{
				base.Fields["TargetDatabases"] = value;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x000AF621 File Offset: 0x000AD821
		// (set) Token: 0x06002C0B RID: 11275 RVA: 0x000AF638 File Offset: 0x000AD838
		[Parameter(Mandatory = true, ParameterSetName = "LocalPublicFolder")]
		public DatabaseIdParameter SourcePublicFolderDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["SourcePublicFolderDatabase"];
			}
			set
			{
				base.Fields["SourcePublicFolderDatabase"] = value;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000AF64B File Offset: 0x000AD84B
		// (set) Token: 0x06002C0D RID: 11277 RVA: 0x000AF662 File Offset: 0x000AD862
		[Parameter(Mandatory = false, ParameterSetName = "Offboarding")]
		[Parameter(Mandatory = false, ParameterSetName = "Onboarding")]
		public string TargetDeliveryDomain
		{
			get
			{
				return (string)base.Fields["TargetDeliveryDomain"];
			}
			set
			{
				base.Fields["TargetDeliveryDomain"] = value;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x000AF675 File Offset: 0x000AD875
		// (set) Token: 0x06002C0F RID: 11279 RVA: 0x000AF68C File Offset: 0x000AD88C
		[Parameter(Mandatory = false)]
		public DateTime? StartAfter
		{
			get
			{
				return (DateTime?)base.Fields["StartAfter"];
			}
			set
			{
				base.Fields["StartAfter"] = value;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000AF6A4 File Offset: 0x000AD8A4
		// (set) Token: 0x06002C11 RID: 11281 RVA: 0x000AF6BB File Offset: 0x000AD8BB
		[Parameter(Mandatory = false)]
		public DateTime? CompleteAfter
		{
			get
			{
				return (DateTime?)base.Fields["CompleteAfter"];
			}
			set
			{
				base.Fields["CompleteAfter"] = value;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x000AF6D3 File Offset: 0x000AD8D3
		// (set) Token: 0x06002C13 RID: 11283 RVA: 0x000AF6EA File Offset: 0x000AD8EA
		[Parameter(Mandatory = false)]
		public TimeSpan? ReportInterval
		{
			get
			{
				return (TimeSpan?)base.Fields["ReportInterval"];
			}
			set
			{
				base.Fields["ReportInterval"] = value;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000AF702 File Offset: 0x000AD902
		// (set) Token: 0x06002C15 RID: 11285 RVA: 0x000AF719 File Offset: 0x000AD919
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Preexisting")]
		[ValidateNotNullOrEmpty]
		public MultiValuedProperty<MigrationUser> Users
		{
			get
			{
				return (MultiValuedProperty<MigrationUser>)base.Fields["Users"];
			}
			set
			{
				base.Fields["Users"] = value;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000AF72C File Offset: 0x000AD92C
		// (set) Token: 0x06002C17 RID: 11287 RVA: 0x000AF743 File Offset: 0x000AD943
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "PreexistingUserIds")]
		public MultiValuedProperty<MigrationUserIdParameter> UserIds
		{
			get
			{
				return (MultiValuedProperty<MigrationUserIdParameter>)base.Fields["UserIds"];
			}
			set
			{
				base.Fields["UserIds"] = value;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000AF756 File Offset: 0x000AD956
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMigrationBatch(this.Name, this.TenantName);
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x000AF769 File Offset: 0x000AD969
		protected override bool SkipWriteResult
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x000AF76C File Offset: 0x000AD96C
		private string TenantName
		{
			get
			{
				if (!(base.CurrentOrganizationId != null) || base.CurrentOrganizationId.OrganizationalUnit == null)
				{
					return string.Empty;
				}
				return base.CurrentOrganizationId.OrganizationalUnit.Name;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x000AF79F File Offset: 0x000AD99F
		private bool IsNewCutoverBatch
		{
			get
			{
				return this.CSVData == null;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x000AF7AA File Offset: 0x000AD9AA
		private bool Onboarding
		{
			get
			{
				return string.Equals(base.ParameterSetName, "Onboarding");
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x000AF7BC File Offset: 0x000AD9BC
		private bool Offboarding
		{
			get
			{
				return string.Equals(base.ParameterSetName, "Offboarding");
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x000AF7CE File Offset: 0x000AD9CE
		private bool IsLocalPublicFolderMigration
		{
			get
			{
				return string.Equals(base.ParameterSetName, "LocalPublicFolder");
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x000AF7E0 File Offset: 0x000AD9E0
		private bool PreexistingCopy
		{
			get
			{
				return string.Equals(base.ParameterSetName, "Preexisting") || string.Equals(base.ParameterSetName, "PreexistingUserIds");
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06002C20 RID: 11296 RVA: 0x000AF808 File Offset: 0x000ADA08
		private bool IsTenantOnboarding
		{
			get
			{
				bool flag = !string.IsNullOrEmpty(this.TenantName) || (base.ExchangeRunspaceConfig != null && base.ExchangeRunspaceConfig.IsDedicatedTenantAdmin);
				return this.Onboarding && flag;
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x000AF847 File Offset: 0x000ADA47
		// (set) Token: 0x06002C22 RID: 11298 RVA: 0x000AF84F File Offset: 0x000ADA4F
		private bool FoundErrors { get; set; }

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x000AF858 File Offset: 0x000ADA58
		// (set) Token: 0x06002C24 RID: 11300 RVA: 0x000AF860 File Offset: 0x000ADA60
		private MigrationBatch PreexistingBatch { get; set; }

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06002C25 RID: 11301 RVA: 0x000AF869 File Offset: 0x000ADA69
		// (set) Token: 0x06002C26 RID: 11302 RVA: 0x000AF871 File Offset: 0x000ADA71
		private List<Guid> PreexistingUserIds { get; set; }

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x000AF87A File Offset: 0x000ADA7A
		// (set) Token: 0x06002C28 RID: 11304 RVA: 0x000AF882 File Offset: 0x000ADA82
		private MigrationBatch NewBatch { get; set; }

		// Token: 0x06002C29 RID: 11305 RVA: 0x000AF88C File Offset: 0x000ADA8C
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = "New-MigrationBatch";
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			return MigrationBatchDataProvider.CreateDataProvider("NewMigrationBatch", base.TenantGlobalCatalogSession, null, this.partitionMailbox);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000AF8E4 File Offset: 0x000ADAE4
		protected override void InternalStateReset()
		{
			MigrationBatchDataProvider migrationBatchDataProvider = base.DataSession as MigrationBatchDataProvider;
			if (migrationBatchDataProvider != null && migrationBatchDataProvider.MailboxProvider.OrganizationId == base.CurrentOrganizationId)
			{
				return;
			}
			this.DisposeSession();
			base.InternalStateReset();
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000AF928 File Offset: 0x000ADB28
		protected override void InternalProcessRecord()
		{
			bool flag = true;
			try
			{
				if (this.NewBatch == null)
				{
					this.NewBatch = this.DataObject;
					this.ValidateAndInitialize(this.NewBatch);
				}
				if (this.PreexistingCopy)
				{
					IEnumerable<MigrationUser> enumerable = new List<MigrationUser>();
					if (this.UserIds != null)
					{
						foreach (MigrationUserIdParameter migrationUserIdParameter in this.UserIds)
						{
							MigrationUser[] array = this.userDataProvider.Value.GetByUserId(migrationUserIdParameter.MigrationUserId, 10).ToArray<MigrationUser>();
							if (array.Length == 0)
							{
								base.WriteError(new MigrationUserNotFoundException(migrationUserIdParameter.ToString()));
							}
							enumerable = enumerable.Union(array);
						}
					}
					if (this.Users != null)
					{
						enumerable = enumerable.Union(this.Users);
					}
					this.InternalProcessPreexistingUsers(this.NewBatch, enumerable);
				}
				flag = false;
			}
			finally
			{
				if (flag)
				{
					this.FoundErrors = true;
				}
			}
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000AFA2C File Offset: 0x000ADC2C
		protected override void InternalEndProcessing()
		{
			if (!this.FoundErrors && this.NewBatch != null)
			{
				this.InternalCreateJob(this.NewBatch);
				this.DataObject = this.NewBatch;
				this.WriteResult();
			}
			base.InternalEndProcessing();
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000AFA64 File Offset: 0x000ADC64
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.disposed)
				{
					if (disposing)
					{
						this.DisposeSession();
					}
					this.disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000AFAA4 File Offset: 0x000ADCA4
		protected override bool IsKnownException(Exception exception)
		{
			return MigrationBatchDataProvider.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000AFAB8 File Offset: 0x000ADCB8
		private void ValidateAndInitialize(MigrationBatch batch)
		{
			if (this.ArchiveOnly && this.PrimaryOnly)
			{
				base.WriteError(new TaskException(Strings.ErrorIncompatibleParameters("PrimaryOnly", "ArchiveOnly")));
			}
			MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
			if (!migrationBatchDataProvider.MigrationSession.Config.IsSupported(MigrationFeature.MultiBatch))
			{
				base.WriteError(new RequiredMigrationFeatureNotEnabledException(MigrationFeature.MultiBatch));
			}
			MigrationJob andValidateMigrationJob = MigrationObjectTaskBase<MigrationBatchIdParameter>.GetAndValidateMigrationJob(this, migrationBatchDataProvider, new MigrationBatchIdParameter(this.Name), false, false);
			if (andValidateMigrationJob != null)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationJobAlreadyExists(this.Name)));
			}
			this.ValidateCommonParameters(migrationBatchDataProvider, batch);
			if (this.Onboarding)
			{
				this.ValidateOnboardingParameters(migrationBatchDataProvider.MailboxProvider, batch);
				this.ValidateOrDiscoverTargetDeliveryDomain(batch);
				if (batch.MigrationType == MigrationType.PublicFolder)
				{
					this.ValidateOnboardingPublicFolderMigrationParameters(batch);
				}
			}
			else if (this.Offboarding)
			{
				this.ValidateOffboardingParameters(batch);
				this.ValidateOrDiscoverTargetDeliveryDomain(batch);
			}
			else if (this.Local)
			{
				this.ValidateLocalParameters(batch);
			}
			else if (this.IsLocalPublicFolderMigration)
			{
				this.ValidateLocalPublicFolderMigrationParameters(batch);
			}
			else if (this.PreexistingCopy)
			{
				this.ValidatePreexistingCopy(migrationBatchDataProvider);
			}
			else if (this.XO1)
			{
				this.ValidateXO1Parameters(batch);
			}
			this.ValidateProtocolParameters(batch);
			this.ValidateSchedulingParameters(batch);
			LocalizedException ex;
			if (!this.PreexistingCopy && !migrationBatchDataProvider.MigrationSession.CanCreateNewJobOfType(batch.MigrationType, this.CSVData != null, out ex))
			{
				base.WriteError(ex ?? new MaximumNumberOfBatchesReachedException());
			}
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000AFC38 File Offset: 0x000ADE38
		private void ValidateOnboardingPublicFolderMigrationParameters(MigrationBatch batch)
		{
			if (base.IsFieldSet("TargetDatabases"))
			{
				base.WriteError(new ParameterNotSupportedForMigrationTypeException("TargetDatabases", batch.MigrationType.ToString()));
			}
			if (base.IsFieldSet("TargetArchiveDatabases"))
			{
				base.WriteError(new ParameterNotSupportedForMigrationTypeException("TargetArchiveDatabases", batch.MigrationType.ToString()));
			}
			if (base.IsFieldSet("DisallowExistingUsers"))
			{
				base.WriteError(new ParameterNotSupportedForMigrationTypeException("DisallowExistingUsers", batch.MigrationType.ToString()));
			}
			if (this.CSVData == null)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationCSVRequired));
			}
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000AFCE4 File Offset: 0x000ADEE4
		private void ValidateCommonParameters(MigrationBatchDataProvider batchProvider, MigrationBatch batch)
		{
			if (!this.Onboarding && !this.Local && !this.Offboarding && !this.PreexistingCopy && !this.XO1 && !this.IsLocalPublicFolderMigration)
			{
				throw new InvalidOperationException("No direction specified for processing new migration batch.");
			}
			bool flag = batchProvider.MigrationSession.Config.IsSupported(MigrationFeature.PAW);
			IMigrationADProvider adprovider = batchProvider.MailboxProvider.ADProvider;
			if (!adprovider.IsMigrationEnabled)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationNotEnabledForTenant(this.TenantName)));
			}
			if (this.Locale != null && this.Locale.IsNeutralCulture)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationNeutralCultureIsInvalid));
			}
			if (this.ReportInterval != null && !flag)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationReportIntervalParameterInvalid));
			}
			this.ResolveEndpoints(batch);
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000AFDC4 File Offset: 0x000ADFC4
		private void ValidateSchedulingParameters(MigrationBatch batch)
		{
			MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
			bool flag = migrationBatchDataProvider.MigrationSession.Config.IsSupported(MigrationFeature.PAW);
			bool flag2 = batch.MigrationType == MigrationType.ExchangeRemoteMove || batch.MigrationType == MigrationType.ExchangeLocalMove;
			if (this.StartAfter != null && !flag2 && batch.MigrationType != MigrationType.ExchangeOutlookAnywhere && (batch.MigrationType != MigrationType.IMAP || !flag))
			{
				base.WriteError(new LocalizedException(Strings.MigrationStartAfterIncorrectMigrationType));
			}
			if (this.CompleteAfter != null && !flag2 && (batch.MigrationType != MigrationType.IMAP || !flag))
			{
				base.WriteError(new LocalizedException(Strings.MigrationCompleteAfterIncorrectMigrationType));
			}
			if (this.StartAfter != null && base.IsFieldSet("AutoStart"))
			{
				base.WriteError(new LocalizedException(Strings.MigrationStartAfterAndAutoStartExclusive));
			}
			if (this.CompleteAfter != null && flag2 && base.IsFieldSet("AutoComplete"))
			{
				base.WriteError(new LocalizedException(Strings.MigrationCompleteAfterAndAutoCompleteExclusive));
			}
			if (this.StartAfter != null)
			{
				RequestTaskHelper.ValidateStartAfterTime(this.StartAfter.Value.ToUniversalTime(), new Task.TaskErrorLoggingDelegate(base.WriteError), DateTime.UtcNow);
				this.AutoStart = true;
			}
			if (this.CompleteAfter != null)
			{
				RequestTaskHelper.ValidateCompleteAfterTime(this.CompleteAfter.Value.ToUniversalTime(), new Task.TaskErrorLoggingDelegate(base.WriteError), DateTime.UtcNow);
				this.AutoComplete = true;
			}
			if (this.StartAfter != null && !this.AutoComplete && flag2)
			{
				base.WriteError(new LocalizedException(Strings.MigrationStartAfterRequiresAutoComplete));
			}
			if (this.StartAfter != null && this.CompleteAfter != null)
			{
				RequestTaskHelper.ValidateStartAfterComesBeforeCompleteAfter(new DateTime?(this.StartAfter.Value.ToUniversalTime()), new DateTime?(this.CompleteAfter.Value.ToUniversalTime()), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000B000C File Offset: 0x000AE20C
		private void ValidateProtocolParameters(MigrationBatch batch)
		{
			MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
			migrationBatchDataProvider.MigrationSession.Config.IsSupported(MigrationFeature.PAW);
			SkippableMigrationSteps skippableMigrationSteps = MigrationBatch.ConvertStepsArrayToFlags(this.SkipSteps);
			if (skippableMigrationSteps.HasFlag(SkippableMigrationSteps.SettingTargetAddress) && (batch.MigrationType != MigrationType.ExchangeOutlookAnywhere || this.IsNewCutoverBatch))
			{
				base.WriteError(new SkipStepNotSupportedException(SkippableMigrationSteps.SettingTargetAddress.ToString()));
			}
			if (batch.MigrationType != MigrationType.ExchangeLocalMove && batch.MigrationType != MigrationType.ExchangeRemoteMove)
			{
				if (this.PrimaryOnly)
				{
					base.WriteError(new ParameterNotSupportedForMigrationTypeException("PrimaryOnly", batch.MigrationType.ToString()));
				}
				if (this.ArchiveOnly)
				{
					base.WriteError(new ParameterNotSupportedForMigrationTypeException("ArchiveOnly", batch.MigrationType.ToString()));
				}
				if (this.AutoComplete)
				{
					base.WriteError(new ParameterNotSupportedForMigrationTypeException("AutoComplete", batch.MigrationType.ToString()));
				}
			}
			if (batch.MigrationType != MigrationType.IMAP && base.IsFieldSet("ExcludeFolders"))
			{
				base.WriteError(new ParameterNotSupportedForMigrationTypeException("ExcludeFolders", batch.MigrationType.ToString()));
			}
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000B0154 File Offset: 0x000AE354
		private void ValidateLocalParameters(MigrationBatch batch)
		{
			if (batch.SourceEndpoint != null && batch.SourceEndpoint.IsRemote)
			{
				base.WriteError(new RemoteEndpointsCannotBeUsedOnLocalBatchesException(this.SourceEndpoint.RawIdentity));
			}
			if (batch.TargetEndpoint != null && batch.TargetEndpoint.IsRemote)
			{
				base.WriteError(new RemoteEndpointsCannotBeUsedOnLocalBatchesException(this.TargetEndpoint.RawIdentity));
			}
			if (this.TargetDatabases != null)
			{
				this.ValidateDatabasesExistLocally(this.TargetDatabases, "TargetDatabases");
			}
			if (this.TargetArchiveDatabases != null)
			{
				this.ValidateDatabasesExistLocally(this.TargetArchiveDatabases, "TargetArchiveDatabases");
			}
			batch.MigrationType = MigrationType.ExchangeLocalMove;
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000B01F4 File Offset: 0x000AE3F4
		private void ValidateDatabasesExistLocally(IEnumerable<string> databases, string parameterName)
		{
			foreach (string text in databases)
			{
				if (string.IsNullOrEmpty(text))
				{
					base.WriteError(new MigrationPermanentException(Strings.ErrorParameterValueRequired(parameterName)));
				}
				MailboxDatabase database = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(DatabaseIdParameter.Parse(text), this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(text)), new LocalizedString?(Strings.ErrorDatabaseNotUnique(text)));
				MailboxTaskHelper.VerifyDatabaseIsWithinScopeForRecipientCmdlets(this.ConfigurationSession.SessionSettings, database, new Task.ErrorLoggerDelegate(base.WriteError));
			}
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000B029C File Offset: 0x000AE49C
		private void ValidateLocalPublicFolderMigrationParameters(MigrationBatch batch)
		{
			PublicFolderDatabase publicFolderDatabase = (PublicFolderDatabase)base.GetDataObject<PublicFolderDatabase>(this.SourcePublicFolderDatabase, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.SourcePublicFolderDatabase.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.SourcePublicFolderDatabase.ToString())));
			using (IMailbox mailbox = PublicFolderEndpoint.ConnectToLocalSourceDatabase(publicFolderDatabase.ExchangeObjectId))
			{
				mailbox.Disconnect();
			}
			batch.MigrationType = MigrationType.PublicFolder;
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000B0328 File Offset: 0x000AE528
		private void ValidateXO1Parameters(MigrationBatch batch)
		{
			if (batch.SourceEndpoint != null && batch.SourceEndpoint.IsRemote)
			{
				base.WriteError(new RemoteEndpointsCannotBeUsedOnLocalBatchesException(this.SourceEndpoint.RawIdentity));
			}
			if (batch.TargetEndpoint != null && batch.TargetEndpoint.IsRemote)
			{
				base.WriteError(new RemoteEndpointsCannotBeUsedOnLocalBatchesException(this.TargetEndpoint.RawIdentity));
			}
			batch.MigrationType = MigrationType.XO1;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000B0392 File Offset: 0x000AE592
		private void ValidatePreexistingCopy(MigrationBatchDataProvider batchProvider)
		{
			if (!batchProvider.MigrationSession.Config.IsSupported(MigrationFeature.PAW))
			{
				base.WriteError(new RequiredMigrationFeatureNotEnabledException(MigrationFeature.PAW));
			}
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000B03B4 File Offset: 0x000AE5B4
		private void ValidateOnboardingParameters(IMigrationDataProvider dataProvider, MigrationBatch batch)
		{
			if (this.IsTenantOnboarding && (this.TargetDatabases != null || this.TargetArchiveDatabases != null))
			{
				base.WriteError(new TargetDatabasesNotAllowedException());
			}
			if (batch.SourceEndpoint == null || !batch.SourceEndpoint.IsRemote)
			{
				base.WriteError(new SourceEndpointRequiredException());
			}
			batch.MigrationType = MigrationEndpointBase.GetMigrationType(batch.SourceEndpoint, batch.TargetEndpoint);
			if (this.IsNewCutoverBatch)
			{
				if (!MigrationSession.SupportsCutover(dataProvider))
				{
					base.WriteError(new CutoverMigrationNotAllowedException(this.TenantName));
				}
				else if (batch.MigrationType != MigrationType.ExchangeOutlookAnywhere)
				{
					base.WriteError(new CutoverMigrationNotSupportedForProtocolException(batch.MigrationType.ToString()));
				}
			}
			if (this.TargetDatabases != null)
			{
				this.ValidateDatabasesExistLocally(this.TargetDatabases, "TargetDatabases");
			}
			if (this.TargetArchiveDatabases != null)
			{
				this.ValidateDatabasesExistLocally(this.TargetArchiveDatabases, "TargetArchiveDatabases");
			}
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000B0494 File Offset: 0x000AE694
		private void ValidateOffboardingParameters(MigrationBatch batch)
		{
			if (this.CSVData == null)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationCsvParameterInvalid));
			}
			if (batch.TargetEndpoint == null || !batch.TargetEndpoint.IsRemote)
			{
				base.WriteError(new TargetEndpointRequiredException());
			}
			batch.MigrationType = MigrationEndpointBase.GetMigrationType(batch.SourceEndpoint, batch.TargetEndpoint);
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000B04F0 File Offset: 0x000AE6F0
		private void ValidateOrDiscoverTargetDeliveryDomain(MigrationBatch batch)
		{
			if (batch.MigrationType != MigrationType.ExchangeRemoteMove)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.TargetDeliveryDomain))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorParameterValueRequired("TargetDeliveryDomain")), ErrorCategory.InvalidArgument, this.Name);
			}
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000B0528 File Offset: 0x000AE728
		private void InternalCreateJob(MigrationBatch batch)
		{
			MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
			bool flag = migrationBatchDataProvider.MigrationSession.Config.IsSupported(MigrationFeature.PAW);
			batch.Identity = new MigrationBatchId(this.Name);
			batch.UserTimeZone = this.TimeZone;
			batch.SubscriptionSettingsModified = (DateTime)ExDateTime.UtcNow;
			batch.ExcludedFolders = this.ExcludeFolders;
			batch.SkipSteps = MigrationBatch.ConvertStepsArrayToFlags(this.SkipSteps);
			batch.Locale = this.Locale;
			batch.AutoRetryCount = this.AutoRetryCount;
			batch.AllowUnknownColumnsInCsv = this.AllowUnknownColumnsInCsv;
			if (flag)
			{
				if (!this.AutoStart && this.StartAfter == null)
				{
					batch.Flags |= MigrationFlags.Stop;
				}
				if (this.ReportInterval != null)
				{
					batch.ReportInterval = new TimeSpan?(this.ReportInterval.Value);
				}
				if (this.AutoStart)
				{
					batch.BatchFlags |= MigrationBatchFlags.ReportInitial;
				}
			}
			else
			{
				if (this.DisallowExistingUsers)
				{
					batch.BatchFlags |= MigrationBatchFlags.DisallowExistingUsers;
				}
				if (this.AutoStart && (batch.MigrationType == MigrationType.ExchangeLocalMove || batch.MigrationType == MigrationType.ExchangeRemoteMove))
				{
					batch.BatchFlags |= MigrationBatchFlags.UseAdvancedValidation;
				}
				if (this.AutoComplete)
				{
					batch.BatchFlags |= MigrationBatchFlags.AutoComplete;
				}
				if (this.AllowIncrementalSyncs != null && !this.AllowIncrementalSyncs.Value)
				{
					batch.BatchFlags |= MigrationBatchFlags.AutoStop;
				}
			}
			ADObjectId adobjectId;
			bool flag2 = base.TryGetExecutingUserId(out adobjectId);
			batch.OwnerId = adobjectId;
			batch.DelegatedAdminOwner = string.Empty;
			if (flag2)
			{
				ADUser aduser = (ADUser)base.TenantGlobalCatalogSession.Read(adobjectId);
				if (aduser != null)
				{
					batch.SubmittedByUser = aduser.WindowsEmailAddress.ToString();
					batch.OwnerExchangeObjectId = aduser.ExchangeObjectId;
				}
			}
			else if (base.ExchangeRunspaceConfig.DelegatedPrincipal != null)
			{
				batch.SubmittedByUser = base.ExchangeRunspaceConfig.DelegatedPrincipal.UserId;
				batch.DelegatedAdminOwner = base.ExchangeRunspaceConfig.DelegatedPrincipal.Identity.Name;
			}
			else if (base.ExecutingUserOrganizationId == OrganizationId.ForestWideOrgId)
			{
				batch.SubmittedByUser = base.ExchangeRunspaceConfig.IdentityName;
			}
			else
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationOperationFailed));
			}
			batch.SubmittedByUserAdminType = MigrationObjectTaskBase<MigrationBatchIdParameter>.GetUserType(base.ExchangeRunspaceConfig, base.ExecutingUserOrganizationId);
			IEnumerable<SmtpAddress> enumerable = this.NotificationEmails;
			if (batch.NotificationEmails != null)
			{
				if (enumerable == null)
				{
					enumerable = batch.NotificationEmails;
				}
				else
				{
					enumerable = enumerable.Concat(batch.NotificationEmails);
				}
			}
			batch.NotificationEmails = MigrationObjectTaskBase<MigrationBatchIdParameter>.GetUpdatedNotificationEmails(this, base.TenantGlobalCatalogSession, enumerable);
			try
			{
				if (this.PreexistingCopy)
				{
					this.InternalEndProcessingPreexistingCopy(migrationBatchDataProvider, batch);
				}
				else if (this.Local)
				{
					this.InternalEndProcessingLocalMoves(migrationBatchDataProvider, batch);
				}
				else if (this.IsLocalPublicFolderMigration)
				{
					this.InternalEndProcessingLocalPublicFolderMigration(migrationBatchDataProvider, batch);
				}
				else if (this.XO1)
				{
					this.InternalEndProcessingXO1(migrationBatchDataProvider, batch);
				}
				else
				{
					this.InternalEndProcessingRemote(migrationBatchDataProvider, batch);
				}
				MigrationJob migrationJob = migrationBatchDataProvider.CreateBatch(batch);
				ConnectivityRec connectivityRec = new ConnectivityRec(ServerKind.Cmdlet, VersionInformation.MRS);
				migrationJob.ReportData.Append(Strings.MigrationReportJobCreated(base.ExecutingUserIdentityName, migrationJob.MigrationType), connectivityRec);
				if (!flag && this.AutoStart)
				{
					if (batch.ValidationWarnings != null && batch.ValidationWarnings.Count > 0)
					{
						this.WriteWarning(Strings.BatchValidationWarningsAutoStart(this.Name));
					}
					else
					{
						LocalizedString? localizedString;
						if (!migrationJob.SupportsStarting(out localizedString))
						{
							if (localizedString == null)
							{
								localizedString = new LocalizedString?(Strings.MigrationJobAlreadyStarted);
							}
							base.WriteError(new MigrationPermanentException(localizedString.Value));
						}
						migrationJob.ReportData.Append(Strings.MigrationReportJobAutoStarted);
						MigrationObjectTaskBase<MigrationBatchIdParameter>.StartJob(this, migrationBatchDataProvider, migrationJob, batch.NotificationEmails, batch.BatchFlags);
					}
				}
				migrationBatchDataProvider.MailboxProvider.FlushReport(migrationJob.ReportData);
			}
			finally
			{
				if (batch.CsvStream != null)
				{
					batch.CsvStream.Dispose();
					batch.CsvStream = null;
				}
			}
			MigrationObjectTaskBase<MigrationBatchIdParameter>.RegisterMigrationBatch(this, migrationBatchDataProvider.MailboxSession, base.CurrentOrganizationId, false, false);
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000B0990 File Offset: 0x000AEB90
		private void InternalEndProcessingRemote(MigrationBatchDataProvider batchProvider, MigrationBatch batch)
		{
			if (this.Offboarding)
			{
				batch.BatchDirection = MigrationBatchDirection.Offboarding;
			}
			else
			{
				batch.BatchDirection = MigrationBatchDirection.Onboarding;
				batch.TargetDomainName = batchProvider.MailboxProvider.ADProvider.TenantOrganizationName;
			}
			this.ProcessOnboardingCsvParameters(batchProvider, batch);
			this.SetSubscriptionSettings(batch);
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000B09D0 File Offset: 0x000AEBD0
		private void InternalProcessPreexistingUsers(MigrationBatch batch, IEnumerable<MigrationUser> users)
		{
			foreach (MigrationUser migrationUser in users)
			{
				if (migrationUser.BatchId == null || migrationUser.BatchId.JobId.Equals(Guid.Empty))
				{
					base.WriteError(new MigrationPermanentException(Strings.MigrationInvalidBatchIdForUser(migrationUser.Identity.ToString())));
				}
				if (this.PreexistingBatch == null)
				{
					MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
					MigrationJob migrationJobByBatchId = MigrationObjectTaskBase<MigrationBatchIdParameter>.GetMigrationJobByBatchId(this, migrationBatchDataProvider, migrationUser.BatchId, false, true);
					this.PreexistingUserIds = new List<Guid>(migrationJobByBatchId.TotalCount);
					this.PreexistingBatch = MigrationJob.GetMigrationBatch(migrationBatchDataProvider.MailboxProvider, migrationBatchDataProvider.MigrationSession, migrationJobByBatchId);
					batch.MigrationType = this.PreexistingBatch.MigrationType;
					batch.NotificationEmails = this.PreexistingBatch.NotificationEmails;
					batch.BatchFlags = this.PreexistingBatch.BatchFlags;
					batch.AutoRetryCount = this.PreexistingBatch.AutoRetryCount;
					batch.SkipSteps = this.PreexistingBatch.SkipSteps;
					LocalizedException ex;
					if (!migrationBatchDataProvider.MigrationSession.CanCreateNewJobOfType(batch.MigrationType, true, out ex))
					{
						base.WriteError(ex ?? new MaximumNumberOfBatchesReachedException());
					}
				}
				else if (this.PreexistingBatch.Identity.JobId != migrationUser.BatchId.JobId)
				{
					base.WriteError(new MigrationNewBatchUsersShareBatchException());
				}
				this.PreexistingUserIds.Add(migrationUser.Identity.JobItemGuid);
			}
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000B0B78 File Offset: 0x000AED78
		private void InternalEndProcessingPreexistingCopy(MigrationBatchDataProvider batchProvider, MigrationBatch batch)
		{
			int config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MigrationMaximumJobItemsPerBatch");
			if (this.PreexistingUserIds.Count > config)
			{
				base.WriteError(new MigrationUserLimitExceededException(config));
			}
			batch.CsvStream = new MemoryStream((this.PreexistingUserIds.Count + 2) * 38);
			using (StreamWriter streamWriter = new StreamWriter(batch.CsvStream, Encoding.UTF8, 4096, true))
			{
				MigrationPreexistingBatchCsvSchema migrationPreexistingBatchCsvSchema = new MigrationPreexistingBatchCsvSchema();
				try
				{
					migrationPreexistingBatchCsvSchema.WriteHeader(streamWriter);
					foreach (Guid userId in this.PreexistingUserIds)
					{
						migrationPreexistingBatchCsvSchema.Write(streamWriter, userId);
					}
				}
				finally
				{
					streamWriter.Flush();
				}
			}
			batch.CsvStream.Seek(0L, SeekOrigin.Begin);
			batch.TotalCount = this.PreexistingUserIds.Count;
			batch.OriginalBatchId = new Guid?(this.PreexistingBatch.Identity.JobId);
			if (this.DisableOnCopy)
			{
				batch.BatchFlags |= MigrationBatchFlags.DisableOnCopy;
			}
			batch.ExcludedFolders = this.PreexistingBatch.ExcludedFolders;
			batch.BatchDirection = this.PreexistingBatch.BatchDirection;
			batch.SourceEndpoint = this.PreexistingBatch.SourceEndpoint;
			batch.TargetEndpoint = this.PreexistingBatch.TargetEndpoint;
			batch.TargetDatabases = this.PreexistingBatch.TargetDatabases;
			batch.TargetArchiveDatabases = this.PreexistingBatch.TargetArchiveDatabases;
			batch.PrimaryOnly = this.PreexistingBatch.PrimaryOnly;
			batch.ArchiveOnly = this.PreexistingBatch.ArchiveOnly;
			batch.TargetDeliveryDomain = this.PreexistingBatch.TargetDeliveryDomain;
			batch.TargetDomainName = this.PreexistingBatch.TargetDomainName;
			batch.BadItemLimit = this.PreexistingBatch.BadItemLimit;
			batch.LargeItemLimit = this.PreexistingBatch.LargeItemLimit;
			if (this.StartAfter != null)
			{
				batch.StartAfter = new DateTime?(this.StartAfter.Value);
				batch.StartAfterUTC = new DateTime?(this.StartAfter.Value.ToUniversalTime());
			}
			else if (this.AutoStart)
			{
				batch.StartAfter = new DateTime?(DateTime.MinValue);
				batch.StartAfterUTC = new DateTime?(DateTime.MinValue);
			}
			else
			{
				batch.StartAfter = this.PreexistingBatch.StartAfter;
				batch.StartAfterUTC = this.PreexistingBatch.StartAfterUTC;
			}
			if (this.CompleteAfter != null)
			{
				batch.CompleteAfter = new DateTime?(this.CompleteAfter.Value);
				batch.CompleteAfterUTC = new DateTime?(this.CompleteAfter.Value.ToUniversalTime());
				return;
			}
			if (this.AutoComplete)
			{
				batch.CompleteAfter = new DateTime?(DateTime.MinValue);
				batch.CompleteAfterUTC = new DateTime?(DateTime.MinValue);
				return;
			}
			batch.CompleteAfter = this.PreexistingBatch.CompleteAfter;
			batch.CompleteAfterUTC = this.PreexistingBatch.CompleteAfterUTC;
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000B0EC4 File Offset: 0x000AF0C4
		private void ProcessOnboardingCsvParameters(MigrationBatchDataProvider batchProvider, MigrationBatch batch)
		{
			if (this.CSVData == null)
			{
				this.ProcessNspiOnboardingInputParameters(batch);
				return;
			}
			MigrationCsvSchemaBase schema = MigrationCSVDataRowProvider.CreateCsvSchema(batch.MigrationType, true, this.IsTenantOnboarding);
			LocalizedException ex = MigrationObjectTaskBase<MigrationBatchIdParameter>.ProcessCsv(batchProvider.MailboxProvider, batch, schema, this.CSVData);
			if (ex != null)
			{
				base.WriteError(ex);
			}
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000B0F14 File Offset: 0x000AF114
		private void ProcessNspiOnboardingInputParameters(MigrationBatch batch)
		{
			MigrationEndpointBase migrationEndpointBase = batch.SourceEndpoint;
			MigrationObjectsCount migrationObjectsCount = new MigrationObjectsCount(null);
			NspiMigrationDataReader nspiDataReader = migrationEndpointBase.GetNspiDataReader(null);
			try
			{
				migrationObjectsCount = nspiDataReader.GetCounts();
			}
			catch (MigrationTransientException exception)
			{
				base.WriteError(exception);
			}
			int config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MigrationSourceExchangeRecipientMaximumCount");
			if (migrationObjectsCount.GetTotal() > config)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationMailboxLimitExceeded(config)));
			}
			int config2 = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MigrationSourceExchangeMailboxMaximumCount");
			if (migrationObjectsCount.Mailboxes != null && migrationObjectsCount.Mailboxes.Value > config2)
			{
				base.WriteError(new MigrationPermanentException(Strings.MigrationMailboxLimitExceeded(config2)));
			}
			batch.TotalCount = migrationObjectsCount.GetTotal();
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000B0FE0 File Offset: 0x000AF1E0
		private void InternalEndProcessingLocalMoves(MigrationBatchDataProvider batchProvider, MigrationBatch batch)
		{
			batch.BatchDirection = MigrationBatchDirection.Local;
			this.SetSubscriptionSettings(batch);
			LocalizedException ex = MigrationObjectTaskBase<MigrationBatchIdParameter>.ProcessCsv(batchProvider.MailboxProvider, batch, new MigrationLocalMoveCsvSchema(), this.CSVData);
			if (ex != null)
			{
				base.WriteError(ex);
			}
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000B1020 File Offset: 0x000AF220
		private void InternalEndProcessingLocalPublicFolderMigration(MigrationBatchDataProvider batchProvider, MigrationBatch batch)
		{
			batch.BatchDirection = MigrationBatchDirection.Local;
			batch.BadItemLimit = this.BadItemLimit;
			batch.LargeItemLimit = this.LargeItemLimit;
			batch.SourcePublicFolderDatabase = this.SourcePublicFolderDatabase.RawIdentity;
			LocalizedException ex = MigrationObjectTaskBase<MigrationBatchIdParameter>.ProcessCsv(batchProvider.MailboxProvider, batch, new PublicFolderMigrationCsvSchema(), this.CSVData);
			if (ex != null)
			{
				base.WriteError(ex);
			}
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000B1080 File Offset: 0x000AF280
		private void InternalEndProcessingXO1(MigrationBatchDataProvider batchProvider, MigrationBatch batch)
		{
			batch.BatchDirection = MigrationBatchDirection.Local;
			LocalizedException ex = MigrationObjectTaskBase<MigrationBatchIdParameter>.ProcessCsv(batchProvider.MailboxProvider, batch, new XO1CsvSchema(), this.CSVData);
			if (ex != null)
			{
				base.WriteError(ex);
			}
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000B10B8 File Offset: 0x000AF2B8
		private void ResolveEndpoints(MigrationBatch batch)
		{
			using (MigrationEndpointDataProvider migrationEndpointDataProvider = MigrationEndpointDataProvider.CreateDataProvider("NewMigrationBatch", base.TenantGlobalCatalogSession, this.partitionMailbox))
			{
				if (this.SourceEndpoint != null)
				{
					batch.SourceEndpoint = this.LoadEndpoint(this.SourceEndpoint, migrationEndpointDataProvider);
				}
				if (this.TargetEndpoint != null)
				{
					batch.TargetEndpoint = this.LoadEndpoint(this.TargetEndpoint, migrationEndpointDataProvider);
				}
			}
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000B1130 File Offset: 0x000AF330
		private MigrationEndpoint LoadEndpoint(MigrationEndpointIdParameter endpointId, MigrationEndpointDataProvider endpointProvider)
		{
			MigrationUtil.ThrowOnNullArgument(endpointId, "endpointId");
			List<MigrationEndpoint> list = endpointId.GetObjects<MigrationEndpoint>(null, endpointProvider).ToList<MigrationEndpoint>();
			if (list.Count > 1)
			{
				base.WriteError(new ManagementObjectAmbiguousException(Strings.MigrationEndpointIdentityAmbiguous(endpointId.RawIdentity)));
			}
			MigrationEndpoint migrationEndpoint = list.FirstOrDefault<MigrationEndpoint>();
			if (migrationEndpoint == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(Strings.MigrationEndpointNotFound(endpointId.RawIdentity)));
			}
			return migrationEndpoint;
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000B1198 File Offset: 0x000AF398
		private void SetSubscriptionSettings(MigrationBatch batch)
		{
			MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
			bool flag = migrationBatchDataProvider.MigrationSession.Config.IsSupported(MigrationFeature.PAW);
			batch.TargetArchiveDatabases = this.TargetArchiveDatabases;
			batch.TargetDatabases = this.TargetDatabases;
			batch.PrimaryOnly = new bool?(this.PrimaryOnly);
			batch.ArchiveOnly = new bool?(this.ArchiveOnly);
			batch.TargetDeliveryDomain = this.TargetDeliveryDomain;
			batch.BadItemLimit = this.BadItemLimit;
			if (!this.Local)
			{
				batch.LargeItemLimit = this.LargeItemLimit;
			}
			if (this.StartAfter != null)
			{
				batch.StartAfter = this.StartAfter;
				batch.StartAfterUTC = new DateTime?(this.StartAfter.Value.ToUniversalTime());
			}
			else if (flag && this.AutoStart)
			{
				batch.StartAfter = new DateTime?(DateTime.MinValue);
				batch.StartAfterUTC = new DateTime?(DateTime.MinValue);
			}
			if (this.CompleteAfter != null)
			{
				batch.CompleteAfter = this.CompleteAfter;
				batch.CompleteAfterUTC = new DateTime?(this.CompleteAfter.Value.ToUniversalTime());
				return;
			}
			if (flag)
			{
				if (this.AutoComplete)
				{
					batch.CompleteAfter = new DateTime?(DateTime.MinValue);
					batch.CompleteAfterUTC = new DateTime?(DateTime.MinValue);
					return;
				}
				batch.CompleteAfter = new DateTime?(DateTime.MaxValue);
				batch.CompleteAfterUTC = new DateTime?(DateTime.MaxValue);
			}
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000B133C File Offset: 0x000AF53C
		private void DisposeSession()
		{
			IDisposable disposable = base.DataSession as IDisposable;
			if (disposable != null)
			{
				MigrationLogger.Close();
				disposable.Dispose();
			}
		}

		// Token: 0x0400202C RID: 8236
		private const string ParameterSetLocal = "Local";

		// Token: 0x0400202D RID: 8237
		private const string ParameterSetLocalPublicFolder = "LocalPublicFolder";

		// Token: 0x0400202E RID: 8238
		private const string ParameterSetXO1 = "XO1";

		// Token: 0x0400202F RID: 8239
		private const string ParameterSetOffboarding = "Offboarding";

		// Token: 0x04002030 RID: 8240
		private const string ParameterSetOnboarding = "Onboarding";

		// Token: 0x04002031 RID: 8241
		private const string ParameterSetPreexisting = "Preexisting";

		// Token: 0x04002032 RID: 8242
		private const string ParameterSetPreexistingUserIds = "PreexistingUserIds";

		// Token: 0x04002033 RID: 8243
		private const string ParameterAllowIncrementalSyncs = "AllowIncrementalSyncs";

		// Token: 0x04002034 RID: 8244
		private const string ParameterNameTargetArchiveDatabases = "TargetArchiveDatabases";

		// Token: 0x04002035 RID: 8245
		private const string ParameterNameSourcePublicFolderDatabase = "SourcePublicFolderDatabase";

		// Token: 0x04002036 RID: 8246
		private const string ParameterNameTargetDatabases = "TargetDatabases";

		// Token: 0x04002037 RID: 8247
		private const string ParameterArchiveOnly = "ArchiveOnly";

		// Token: 0x04002038 RID: 8248
		private const string ParameterBadItemLimit = "BadItemLimit";

		// Token: 0x04002039 RID: 8249
		private const string ParameterLargeItemLimit = "LargeItemLimit";

		// Token: 0x0400203A RID: 8250
		private const string ParameterLocal = "Local";

		// Token: 0x0400203B RID: 8251
		private const string ParameterXO1 = "XO1";

		// Token: 0x0400203C RID: 8252
		private const string ParameterPrimaryOnly = "PrimaryOnly";

		// Token: 0x0400203D RID: 8253
		private const string ParameterSourceEndpoint = "SourceEndpoint";

		// Token: 0x0400203E RID: 8254
		private const string ParameterTargetEndpoint = "TargetEndpoint";

		// Token: 0x0400203F RID: 8255
		private const string ParameterTargetDeliveryDomain = "TargetDeliveryDomain";

		// Token: 0x04002040 RID: 8256
		private const string ParameterNameExcludeFolders = "ExcludeFolders";

		// Token: 0x04002041 RID: 8257
		private const string ParameterAutoComplete = "AutoComplete";

		// Token: 0x04002042 RID: 8258
		private const string ParameterAutoStart = "AutoStart";

		// Token: 0x04002043 RID: 8259
		private const string ParameterStartAfter = "StartAfter";

		// Token: 0x04002044 RID: 8260
		private const string ParameterCompleteAfter = "CompleteAfter";

		// Token: 0x04002045 RID: 8261
		private const string ParameterReportInterval = "ReportInterval";

		// Token: 0x04002046 RID: 8262
		private const string ParameterUsers = "Users";

		// Token: 0x04002047 RID: 8263
		private const string ParameterUserIds = "UserIds";

		// Token: 0x04002048 RID: 8264
		private const string ParameterCSVData = "dataBlob";

		// Token: 0x04002049 RID: 8265
		private const string ParameterDisallowExistingUsers = "DisallowExistingUsers";

		// Token: 0x0400204A RID: 8266
		private const string ParameterAllowUnknownColumnsInCsv = "AllowUnknownColumnsInCsv";

		// Token: 0x0400204B RID: 8267
		private bool disposed;

		// Token: 0x0400204C RID: 8268
		private readonly Lazy<MigrationUserDataProvider> userDataProvider;
	}
}

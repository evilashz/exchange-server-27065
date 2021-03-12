using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000F7 RID: 247
	[Serializable]
	public class MigrationBatch : ConfigurableObject
	{
		// Token: 0x06000C56 RID: 3158 RVA: 0x00035650 File Offset: 0x00033850
		public MigrationBatch() : base(new SimplePropertyBag(MigrationBatchSchema.Identity, MigrationBatchSchema.ObjectState, MigrationBatchSchema.ExchangeVersion))
		{
			base.ResetChangeTracking();
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x00035672 File Offset: 0x00033872
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x0003567F File Offset: 0x0003387F
		public new MigrationBatchId Identity
		{
			get
			{
				return (MigrationBatchId)base.Identity;
			}
			internal set
			{
				this[MigrationBatchSchema.Identity] = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0003568D File Offset: 0x0003388D
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x0003569F File Offset: 0x0003389F
		public MigrationBatchStatus Status
		{
			get
			{
				return (MigrationBatchStatus)this[MigrationBatchSchema.BatchStatus];
			}
			internal set
			{
				this[MigrationBatchSchema.BatchStatus] = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x000356B2 File Offset: 0x000338B2
		public Guid BatchGuid
		{
			get
			{
				if (this.Identity == null)
				{
					return Guid.Empty;
				}
				return this.Identity.JobId;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x000356CD File Offset: 0x000338CD
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x000356DF File Offset: 0x000338DF
		public int TotalCount
		{
			get
			{
				return (int)this[MigrationBatchSchema.TotalCount];
			}
			internal set
			{
				this[MigrationBatchSchema.TotalCount] = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x000356F2 File Offset: 0x000338F2
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x00035704 File Offset: 0x00033904
		public int ActiveCount
		{
			get
			{
				return (int)this[MigrationBatchSchema.ActiveCount];
			}
			internal set
			{
				this[MigrationBatchSchema.ActiveCount] = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00035717 File Offset: 0x00033917
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x00035729 File Offset: 0x00033929
		public int StoppedCount
		{
			get
			{
				return (int)this[MigrationBatchSchema.StoppedCount];
			}
			internal set
			{
				this[MigrationBatchSchema.StoppedCount] = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0003573C File Offset: 0x0003393C
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x0003574E File Offset: 0x0003394E
		public int SyncedCount
		{
			get
			{
				return (int)this[MigrationBatchSchema.SyncedCount];
			}
			internal set
			{
				this[MigrationBatchSchema.SyncedCount] = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00035761 File Offset: 0x00033961
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00035773 File Offset: 0x00033973
		public int FinalizedCount
		{
			get
			{
				return (int)this[MigrationBatchSchema.FinalizedCount];
			}
			internal set
			{
				this[MigrationBatchSchema.FinalizedCount] = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00035786 File Offset: 0x00033986
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00035798 File Offset: 0x00033998
		public int FailedCount
		{
			get
			{
				return (int)this[MigrationBatchSchema.FailedCount];
			}
			internal set
			{
				this[MigrationBatchSchema.FailedCount] = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x000357AB File Offset: 0x000339AB
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x000357BD File Offset: 0x000339BD
		public int FailedInitialSyncCount
		{
			get
			{
				return (int)this[MigrationBatchSchema.FailedInitialSyncCount];
			}
			internal set
			{
				this[MigrationBatchSchema.FailedInitialSyncCount] = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x000357D0 File Offset: 0x000339D0
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x000357E2 File Offset: 0x000339E2
		public int FailedIncrementalSyncCount
		{
			get
			{
				return (int)this[MigrationBatchSchema.FailedIncrementalSyncCount];
			}
			internal set
			{
				this[MigrationBatchSchema.FailedIncrementalSyncCount] = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x000357F5 File Offset: 0x000339F5
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x00035807 File Offset: 0x00033A07
		public int PendingCount
		{
			get
			{
				return (int)this[MigrationBatchSchema.PendingCount];
			}
			internal set
			{
				this[MigrationBatchSchema.PendingCount] = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0003581A File Offset: 0x00033A1A
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x0003582C File Offset: 0x00033A2C
		public int ProvisionedCount
		{
			get
			{
				return (int)this[MigrationBatchSchema.ProvisionedCount];
			}
			internal set
			{
				this[MigrationBatchSchema.ProvisionedCount] = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0003583F File Offset: 0x00033A3F
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x00035851 File Offset: 0x00033A51
		public int ValidationWarningCount
		{
			get
			{
				return (int)this[MigrationBatchSchema.NumValidationErrors];
			}
			internal set
			{
				this[MigrationBatchSchema.NumValidationErrors] = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00035864 File Offset: 0x00033A64
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x00035876 File Offset: 0x00033A76
		public MultiValuedProperty<MigrationBatchError> ValidationWarnings
		{
			get
			{
				return (MultiValuedProperty<MigrationBatchError>)this[MigrationBatchSchema.ValidationErrors];
			}
			internal set
			{
				this[MigrationBatchSchema.ValidationErrors] = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00035884 File Offset: 0x00033A84
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x00035896 File Offset: 0x00033A96
		public LocalizedString Message
		{
			get
			{
				return (LocalizedString)this[MigrationBatchSchema.Message];
			}
			internal set
			{
				this[MigrationBatchSchema.Message] = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x000358A9 File Offset: 0x00033AA9
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x000358BB File Offset: 0x00033ABB
		public DateTime CreationDateTime
		{
			get
			{
				return (DateTime)this[MigrationBatchSchema.CreationDateTime];
			}
			internal set
			{
				this[MigrationBatchSchema.CreationDateTime] = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x000358CE File Offset: 0x00033ACE
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x000358E0 File Offset: 0x00033AE0
		public DateTime CreationDateTimeUTC
		{
			get
			{
				return (DateTime)this[MigrationBatchSchema.CreationDateTimeUTC];
			}
			internal set
			{
				this[MigrationBatchSchema.CreationDateTimeUTC] = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x000358F3 File Offset: 0x00033AF3
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x00035905 File Offset: 0x00033B05
		public DateTime? StartDateTime
		{
			get
			{
				return (DateTime?)this[MigrationBatchSchema.StartDateTime];
			}
			internal set
			{
				this[MigrationBatchSchema.StartDateTime] = value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00035918 File Offset: 0x00033B18
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0003592A File Offset: 0x00033B2A
		public DateTime? StartDateTimeUTC
		{
			get
			{
				return (DateTime?)this[MigrationBatchSchema.StartDateTimeUTC];
			}
			internal set
			{
				this[MigrationBatchSchema.StartDateTimeUTC] = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0003593D File Offset: 0x00033B3D
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x0003594F File Offset: 0x00033B4F
		public DateTime? InitialSyncDateTime
		{
			get
			{
				return (DateTime?)this[MigrationBatchSchema.InitialSyncDateTime];
			}
			internal set
			{
				this[MigrationBatchSchema.InitialSyncDateTime] = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x00035962 File Offset: 0x00033B62
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x00035974 File Offset: 0x00033B74
		public DateTime? InitialSyncDateTimeUTC
		{
			get
			{
				return (DateTime?)this[MigrationBatchSchema.InitialSyncDateTimeUTC];
			}
			internal set
			{
				this[MigrationBatchSchema.InitialSyncDateTimeUTC] = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00035987 File Offset: 0x00033B87
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x0003599C File Offset: 0x00033B9C
		public TimeSpan? InitialSyncDuration
		{
			get
			{
				return (TimeSpan?)this[MigrationBatchSchema.InitialSyncDuration];
			}
			internal set
			{
				this[MigrationBatchSchema.InitialSyncDuration] = ((value != null) ? new TimeSpan?(TimeSpan.FromTicks(value.Value.Ticks - value.Value.Ticks % 10000000L)) : null);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x000359FD File Offset: 0x00033BFD
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x00035A0F File Offset: 0x00033C0F
		public DateTime? LastSyncedDateTime
		{
			get
			{
				return (DateTime?)this[MigrationBatchSchema.LastSyncedDateTime];
			}
			internal set
			{
				this[MigrationBatchSchema.LastSyncedDateTime] = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00035A22 File Offset: 0x00033C22
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x00035A34 File Offset: 0x00033C34
		public DateTime? LastSyncedDateTimeUTC
		{
			get
			{
				return (DateTime?)this[MigrationBatchSchema.LastSyncedDateTimeUTC];
			}
			internal set
			{
				this[MigrationBatchSchema.LastSyncedDateTimeUTC] = value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00035A47 File Offset: 0x00033C47
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x00035A59 File Offset: 0x00033C59
		public DateTime? FinalizedDateTime
		{
			get
			{
				return (DateTime?)this[MigrationBatchSchema.FinalizedDateTime];
			}
			internal set
			{
				this[MigrationBatchSchema.FinalizedDateTime] = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00035A6C File Offset: 0x00033C6C
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x00035A7E File Offset: 0x00033C7E
		public DateTime? FinalizedDateTimeUTC
		{
			get
			{
				return (DateTime?)this[MigrationBatchSchema.FinalizedDateTimeUTC];
			}
			internal set
			{
				this[MigrationBatchSchema.FinalizedDateTimeUTC] = value;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00035A91 File Offset: 0x00033C91
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x00035AA3 File Offset: 0x00033CA3
		public string SubmittedByUser
		{
			get
			{
				return (string)this[MigrationBatchSchema.SubmittedByUser];
			}
			internal set
			{
				this[MigrationBatchSchema.SubmittedByUser] = value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00035AB1 File Offset: 0x00033CB1
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x00035AC3 File Offset: 0x00033CC3
		public ADObjectId OwnerId
		{
			get
			{
				return (ADObjectId)this[MigrationBatchSchema.OwnerId];
			}
			internal set
			{
				this[MigrationBatchSchema.OwnerId] = value;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x00035AD1 File Offset: 0x00033CD1
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x00035AE3 File Offset: 0x00033CE3
		public Guid OwnerExchangeObjectId
		{
			get
			{
				return (Guid)this[MigrationBatchSchema.OwnerExchangeObjectId];
			}
			internal set
			{
				this[MigrationBatchSchema.OwnerExchangeObjectId] = value;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00035AF6 File Offset: 0x00033CF6
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x00035B08 File Offset: 0x00033D08
		public MultiValuedProperty<SmtpAddress> NotificationEmails
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[MigrationBatchSchema.NotificationEmails];
			}
			internal set
			{
				this[MigrationBatchSchema.NotificationEmails] = value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00035B16 File Offset: 0x00033D16
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x00035B28 File Offset: 0x00033D28
		public MultiValuedProperty<string> ExcludedFolders
		{
			get
			{
				return (MultiValuedProperty<string>)this[MigrationBatchSchema.ExcludedFolders];
			}
			internal set
			{
				this[MigrationBatchSchema.ExcludedFolders] = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x00035B36 File Offset: 0x00033D36
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x00035B48 File Offset: 0x00033D48
		public MigrationType MigrationType
		{
			get
			{
				return (MigrationType)this[MigrationBatchSchema.MigrationType];
			}
			internal set
			{
				this[MigrationBatchSchema.MigrationType] = (int)value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00035B5B File Offset: 0x00033D5B
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x00035B6D File Offset: 0x00033D6D
		public MigrationBatchDirection BatchDirection
		{
			get
			{
				return (MigrationBatchDirection)this[MigrationBatchSchema.BatchDirection];
			}
			internal set
			{
				this[MigrationBatchSchema.BatchDirection] = (int)value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00035B80 File Offset: 0x00033D80
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x00035B92 File Offset: 0x00033D92
		public CultureInfo Locale
		{
			get
			{
				return (CultureInfo)this[MigrationBatchSchema.Locale];
			}
			internal set
			{
				this[MigrationBatchSchema.Locale] = value;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00035BA0 File Offset: 0x00033DA0
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x00035BB2 File Offset: 0x00033DB2
		public MultiValuedProperty<MigrationReportSet> Reports
		{
			get
			{
				return (MultiValuedProperty<MigrationReportSet>)this[MigrationBatchSchema.Reports];
			}
			internal set
			{
				this[MigrationBatchSchema.Reports] = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00035BC0 File Offset: 0x00033DC0
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00035BD2 File Offset: 0x00033DD2
		public bool IsProvisioning
		{
			get
			{
				return (bool)this[MigrationBatchSchema.IsProvisioning];
			}
			set
			{
				this[MigrationBatchSchema.IsProvisioning] = value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00035BE5 File Offset: 0x00033DE5
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x00035BF7 File Offset: 0x00033DF7
		public MigrationBatchFlags BatchFlags
		{
			get
			{
				return (MigrationBatchFlags)this[MigrationBatchSchema.MigrationBatchFlags];
			}
			set
			{
				this[MigrationBatchSchema.MigrationBatchFlags] = (int)value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00035C0A File Offset: 0x00033E0A
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x00035C1C File Offset: 0x00033E1C
		public int? AutoRetryCount
		{
			get
			{
				return (int?)this[MigrationBatchSchema.AutoRetryCount];
			}
			internal set
			{
				this[MigrationBatchSchema.AutoRetryCount] = value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00035C2F File Offset: 0x00033E2F
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x00035C41 File Offset: 0x00033E41
		public int CurrentRetryCount
		{
			get
			{
				return (int)this[MigrationBatchSchema.CurrentRetryCount];
			}
			internal set
			{
				this[MigrationBatchSchema.CurrentRetryCount] = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00035C54 File Offset: 0x00033E54
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x00035C66 File Offset: 0x00033E66
		public bool AllowUnknownColumnsInCsv
		{
			get
			{
				return (bool)this[MigrationBatchSchema.AllowUnknownColumnsInCsv];
			}
			internal set
			{
				this[MigrationBatchSchema.AllowUnknownColumnsInCsv] = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00035C79 File Offset: 0x00033E79
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x00035C8B File Offset: 0x00033E8B
		public string DiagnosticInfo
		{
			get
			{
				return (string)this[MigrationBatchSchema.DiagnosticInfo];
			}
			internal set
			{
				this[MigrationBatchSchema.DiagnosticInfo] = value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00035C99 File Offset: 0x00033E99
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x00035CAB File Offset: 0x00033EAB
		public MigrationBatchSupportedActions SupportedActions
		{
			get
			{
				return (MigrationBatchSupportedActions)this[MigrationBatchSchema.SupportedActions];
			}
			internal set
			{
				this[MigrationBatchSchema.SupportedActions] = value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00035CBE File Offset: 0x00033EBE
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x00035CD0 File Offset: 0x00033ED0
		public MigrationEndpoint SourceEndpoint
		{
			get
			{
				return (MigrationEndpoint)this[MigrationBatchSchema.SourceEndpoint];
			}
			set
			{
				this[MigrationBatchSchema.SourceEndpoint] = value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00035CDE File Offset: 0x00033EDE
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x00035CF0 File Offset: 0x00033EF0
		public MigrationEndpoint TargetEndpoint
		{
			get
			{
				return (MigrationEndpoint)this[MigrationBatchSchema.TargetEndpoint];
			}
			set
			{
				this[MigrationBatchSchema.TargetEndpoint] = value;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00035CFE File Offset: 0x00033EFE
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x00035D10 File Offset: 0x00033F10
		public string SourcePublicFolderDatabase
		{
			get
			{
				return (string)this[MigrationBatchSchema.SourcePublicFolderDatabase];
			}
			set
			{
				this[MigrationBatchSchema.SourcePublicFolderDatabase] = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x00035D1E File Offset: 0x00033F1E
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x00035D30 File Offset: 0x00033F30
		public MultiValuedProperty<string> TargetDatabases
		{
			get
			{
				return (MultiValuedProperty<string>)this[MigrationBatchSchema.TargetDatabases];
			}
			set
			{
				this[MigrationBatchSchema.TargetDatabases] = value;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00035D3E File Offset: 0x00033F3E
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x00035D50 File Offset: 0x00033F50
		public MultiValuedProperty<string> TargetArchiveDatabases
		{
			get
			{
				return (MultiValuedProperty<string>)this[MigrationBatchSchema.TargetArchiveDatabases];
			}
			set
			{
				this[MigrationBatchSchema.TargetArchiveDatabases] = value;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00035D5E File Offset: 0x00033F5E
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x00035D7E File Offset: 0x00033F7E
		public Unlimited<int> BadItemLimit
		{
			get
			{
				return (Unlimited<int>)(this[MigrationBatchSchema.BadItemLimit] ?? Unlimited<int>.UnlimitedValue);
			}
			set
			{
				this[MigrationBatchSchema.BadItemLimit] = value;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00035D91 File Offset: 0x00033F91
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x00035DB1 File Offset: 0x00033FB1
		public Unlimited<int> LargeItemLimit
		{
			get
			{
				return (Unlimited<int>)(this[MigrationBatchSchema.LargeItemLimit] ?? Unlimited<int>.UnlimitedValue);
			}
			set
			{
				this[MigrationBatchSchema.LargeItemLimit] = value;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00035DC4 File Offset: 0x00033FC4
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x00035DE5 File Offset: 0x00033FE5
		public bool? PrimaryOnly
		{
			get
			{
				return new bool?((bool)(this[MigrationBatchSchema.PrimaryOnly] ?? false));
			}
			set
			{
				this[MigrationBatchSchema.PrimaryOnly] = value;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00035DF8 File Offset: 0x00033FF8
		// (set) Token: 0x06000CBD RID: 3261 RVA: 0x00035E19 File Offset: 0x00034019
		public bool? ArchiveOnly
		{
			get
			{
				return new bool?((bool)(this[MigrationBatchSchema.ArchiveOnly] ?? false));
			}
			set
			{
				this[MigrationBatchSchema.ArchiveOnly] = value;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00035E2C File Offset: 0x0003402C
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x00035E3E File Offset: 0x0003403E
		public string TargetDeliveryDomain
		{
			get
			{
				return (string)this[MigrationBatchSchema.TargetDeliveryDomain];
			}
			set
			{
				this[MigrationBatchSchema.TargetDeliveryDomain] = value;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00035E4C File Offset: 0x0003404C
		// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x00035E5E File Offset: 0x0003405E
		public SkippableMigrationSteps SkipSteps
		{
			get
			{
				return (SkippableMigrationSteps)this[MigrationBatchSchema.SkipSteps];
			}
			set
			{
				this[MigrationBatchSchema.SkipSteps] = value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00035E71 File Offset: 0x00034071
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x00035E79 File Offset: 0x00034079
		public Report Report { get; set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00035E82 File Offset: 0x00034082
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x00035E94 File Offset: 0x00034094
		public DateTime? StartAfter
		{
			get
			{
				return (DateTime?)this[MigrationBatchSchema.StartAfter];
			}
			set
			{
				this[MigrationBatchSchema.StartAfter] = value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00035EA7 File Offset: 0x000340A7
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x00035EB9 File Offset: 0x000340B9
		public DateTime? StartAfterUTC
		{
			get
			{
				return (DateTime?)this[MigrationBatchSchema.StartAfterUTC];
			}
			set
			{
				this[MigrationBatchSchema.StartAfterUTC] = value;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00035ECC File Offset: 0x000340CC
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x00035EDE File Offset: 0x000340DE
		public DateTime? CompleteAfter
		{
			get
			{
				return (DateTime?)this[MigrationBatchSchema.CompleteAfter];
			}
			set
			{
				this[MigrationBatchSchema.CompleteAfter] = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00035EF1 File Offset: 0x000340F1
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x00035F03 File Offset: 0x00034103
		public DateTime? CompleteAfterUTC
		{
			get
			{
				return (DateTime?)this[MigrationBatchSchema.CompleteAfterUTC];
			}
			set
			{
				this[MigrationBatchSchema.CompleteAfterUTC] = value;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00035F16 File Offset: 0x00034116
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x00035F1E File Offset: 0x0003411E
		internal MigrationFlags Flags { get; set; }

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00035F27 File Offset: 0x00034127
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x00035F2F File Offset: 0x0003412F
		internal Stream CsvStream
		{
			get
			{
				return this.csvStream;
			}
			set
			{
				this.csvStream = value;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00035F38 File Offset: 0x00034138
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x00035F40 File Offset: 0x00034140
		internal DateTime SubscriptionSettingsModified
		{
			get
			{
				return this.subscriptionSettingsModified;
			}
			set
			{
				this.subscriptionSettingsModified = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x00035F49 File Offset: 0x00034149
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x00035F51 File Offset: 0x00034151
		internal ExTimeZoneValue UserTimeZone
		{
			get
			{
				return this.userTimeZone;
			}
			set
			{
				this.userTimeZone = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x00035F5A File Offset: 0x0003415A
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x00035F6C File Offset: 0x0003416C
		internal Guid? OriginalBatchId
		{
			get
			{
				return (Guid?)this[MigrationBatchSchema.OriginalBatchId];
			}
			set
			{
				this[MigrationBatchSchema.OriginalBatchId] = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00035F7F File Offset: 0x0003417F
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x00035F87 File Offset: 0x00034187
		internal DateTime OriginalCreationTime
		{
			get
			{
				return this.originalCreationTime;
			}
			set
			{
				this.originalCreationTime = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00035F90 File Offset: 0x00034190
		// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x00035F98 File Offset: 0x00034198
		internal bool OriginalStatisticsEnabled
		{
			get
			{
				return this.originalStatisticsEnabled;
			}
			set
			{
				this.originalStatisticsEnabled = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00035FA1 File Offset: 0x000341A1
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x00035FA9 File Offset: 0x000341A9
		internal string TargetDomainName
		{
			get
			{
				return this.targetDomainName;
			}
			set
			{
				this.targetDomainName = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x00035FB2 File Offset: 0x000341B2
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x00035FBA File Offset: 0x000341BA
		internal SubmittedByUserAdminType SubmittedByUserAdminType
		{
			get
			{
				return this.submittedByUserAdminType;
			}
			set
			{
				this.submittedByUserAdminType = value;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00035FC3 File Offset: 0x000341C3
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x00035FD5 File Offset: 0x000341D5
		internal string DelegatedAdminOwner
		{
			get
			{
				return (string)this[MigrationBatchSchema.DelegatedAdminOwner];
			}
			set
			{
				this[MigrationBatchSchema.DelegatedAdminOwner] = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00035FE3 File Offset: 0x000341E3
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x00035FF5 File Offset: 0x000341F5
		internal TimeSpan? ReportInterval
		{
			get
			{
				return (TimeSpan?)this[MigrationBatchSchema.ReportInterval];
			}
			set
			{
				this[MigrationBatchSchema.ReportInterval] = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00036008 File Offset: 0x00034208
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<MigrationBatchSchema>();
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0003600F File Offset: 0x0003420F
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00036018 File Offset: 0x00034218
		internal static SkippableMigrationSteps ConvertStepsArrayToFlags(SkippableMigrationSteps[] steps)
		{
			SkippableMigrationSteps skippableMigrationSteps = SkippableMigrationSteps.None;
			if (steps != null)
			{
				foreach (SkippableMigrationSteps skippableMigrationSteps2 in steps)
				{
					skippableMigrationSteps |= skippableMigrationSteps2;
				}
			}
			return skippableMigrationSteps;
		}

		// Token: 0x040004B4 RID: 1204
		[NonSerialized]
		private Stream csvStream;

		// Token: 0x040004B5 RID: 1205
		[NonSerialized]
		private DateTime subscriptionSettingsModified;

		// Token: 0x040004B6 RID: 1206
		[NonSerialized]
		private ExTimeZoneValue userTimeZone;

		// Token: 0x040004B7 RID: 1207
		[NonSerialized]
		private DateTime originalCreationTime;

		// Token: 0x040004B8 RID: 1208
		[NonSerialized]
		private SubmittedByUserAdminType submittedByUserAdminType;

		// Token: 0x040004B9 RID: 1209
		[NonSerialized]
		private bool originalStatisticsEnabled;

		// Token: 0x040004BA RID: 1210
		[NonSerialized]
		private string targetDomainName;
	}
}

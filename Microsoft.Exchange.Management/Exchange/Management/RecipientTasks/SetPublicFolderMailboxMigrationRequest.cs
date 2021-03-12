using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CB7 RID: 3255
	[Cmdlet("Set", "PublicFolderMailboxMigrationRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetPublicFolderMailboxMigrationRequest : SetRequest<PublicFolderMailboxMigrationRequestIdParameter>
	{
		// Token: 0x170026B7 RID: 9911
		// (get) Token: 0x06007CDA RID: 31962 RVA: 0x001FF136 File Offset: 0x001FD336
		// (set) Token: 0x06007CDB RID: 31963 RVA: 0x001FF14D File Offset: 0x001FD34D
		[Parameter(Mandatory = false, ParameterSetName = "MailboxMigrationOutlookAnywherePublicFolder")]
		[ValidateNotNullOrEmpty]
		public string RemoteMailboxLegacyDN
		{
			get
			{
				return (string)base.Fields["RemoteMailboxLegacyDN"];
			}
			set
			{
				base.Fields["RemoteMailboxLegacyDN"] = value;
			}
		}

		// Token: 0x170026B8 RID: 9912
		// (get) Token: 0x06007CDC RID: 31964 RVA: 0x001FF160 File Offset: 0x001FD360
		// (set) Token: 0x06007CDD RID: 31965 RVA: 0x001FF177 File Offset: 0x001FD377
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "MailboxMigrationOutlookAnywherePublicFolder")]
		public string RemoteMailboxServerLegacyDN
		{
			get
			{
				return (string)base.Fields["RemoteMailboxServerLegacyDN"];
			}
			set
			{
				base.Fields["RemoteMailboxServerLegacyDN"] = value;
			}
		}

		// Token: 0x170026B9 RID: 9913
		// (get) Token: 0x06007CDE RID: 31966 RVA: 0x001FF18A File Offset: 0x001FD38A
		// (set) Token: 0x06007CDF RID: 31967 RVA: 0x001FF1A1 File Offset: 0x001FD3A1
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "MailboxMigrationOutlookAnywherePublicFolder")]
		public Fqdn OutlookAnywhereHostName
		{
			get
			{
				return (Fqdn)base.Fields["OutlookAnywhereHostName"];
			}
			set
			{
				base.Fields["OutlookAnywhereHostName"] = value;
			}
		}

		// Token: 0x170026BA RID: 9914
		// (get) Token: 0x06007CE0 RID: 31968 RVA: 0x001FF1B4 File Offset: 0x001FD3B4
		// (set) Token: 0x06007CE1 RID: 31969 RVA: 0x001FF1D5 File Offset: 0x001FD3D5
		[Parameter(Mandatory = false, ParameterSetName = "MailboxMigrationOutlookAnywherePublicFolder")]
		public AuthenticationMethod AuthenticationMethod
		{
			get
			{
				return (AuthenticationMethod)(base.Fields["AuthenticationMethod"] ?? AuthenticationMethod.Basic);
			}
			set
			{
				base.Fields["AuthenticationMethod"] = value;
			}
		}

		// Token: 0x170026BB RID: 9915
		// (get) Token: 0x06007CE2 RID: 31970 RVA: 0x001FF1ED File Offset: 0x001FD3ED
		// (set) Token: 0x06007CE3 RID: 31971 RVA: 0x001FF204 File Offset: 0x001FD404
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "MailboxMigrationLocalPublicFolder")]
		public DatabaseIdParameter SourceDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["SourceDatabase"];
			}
			set
			{
				base.Fields["SourceDatabase"] = value;
			}
		}

		// Token: 0x170026BC RID: 9916
		// (get) Token: 0x06007CE4 RID: 31972 RVA: 0x001FF217 File Offset: 0x001FD417
		// (set) Token: 0x06007CE5 RID: 31973 RVA: 0x001FF21F File Offset: 0x001FD41F
		[Parameter(Mandatory = false, ParameterSetName = "MailboxMigrationOutlookAnywherePublicFolder")]
		[ValidateNotNull]
		public new PSCredential RemoteCredential
		{
			get
			{
				return base.RemoteCredential;
			}
			set
			{
				base.RemoteCredential = value;
			}
		}

		// Token: 0x170026BD RID: 9917
		// (get) Token: 0x06007CE6 RID: 31974 RVA: 0x001FF228 File Offset: 0x001FD428
		// (set) Token: 0x06007CE7 RID: 31975 RVA: 0x001FF230 File Offset: 0x001FD430
		[Parameter(Mandatory = false)]
		public new Unlimited<int> BadItemLimit
		{
			get
			{
				return base.BadItemLimit;
			}
			set
			{
				base.BadItemLimit = value;
			}
		}

		// Token: 0x170026BE RID: 9918
		// (get) Token: 0x06007CE8 RID: 31976 RVA: 0x001FF239 File Offset: 0x001FD439
		// (set) Token: 0x06007CE9 RID: 31977 RVA: 0x001FF241 File Offset: 0x001FD441
		[Parameter(Mandatory = false)]
		public new Unlimited<int> LargeItemLimit
		{
			get
			{
				return base.LargeItemLimit;
			}
			set
			{
				base.LargeItemLimit = value;
			}
		}

		// Token: 0x170026BF RID: 9919
		// (get) Token: 0x06007CEA RID: 31978 RVA: 0x001FF24A File Offset: 0x001FD44A
		// (set) Token: 0x06007CEB RID: 31979 RVA: 0x001FF252 File Offset: 0x001FD452
		[Parameter(Mandatory = false)]
		public new string BatchName
		{
			get
			{
				return base.BatchName;
			}
			set
			{
				base.BatchName = value;
			}
		}

		// Token: 0x170026C0 RID: 9920
		// (get) Token: 0x06007CEC RID: 31980 RVA: 0x001FF25B File Offset: 0x001FD45B
		// (set) Token: 0x06007CED RID: 31981 RVA: 0x001FF263 File Offset: 0x001FD463
		[Parameter(Mandatory = false)]
		public new SwitchParameter AcceptLargeDataLoss
		{
			get
			{
				return base.AcceptLargeDataLoss;
			}
			set
			{
				base.AcceptLargeDataLoss = value;
			}
		}

		// Token: 0x06007CEE RID: 31982 RVA: 0x001FF26C File Offset: 0x001FD46C
		protected override void ValidateRequest(TransactionalRequestJob requestJob)
		{
			base.ValidateRequest(requestJob);
			if (base.ParameterSetName.Equals("MailboxMigrationLocalPublicFolder") && requestJob.Flags.HasFlag(RequestFlags.CrossOrg))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorInvalidParametersForOutlookAnywherePublicFolderMailboxMigration), ExchangeErrorCategory.Client, null);
				return;
			}
			if (base.ParameterSetName.Equals("MailboxMigrationOutlookAnywherePublicFolder") && requestJob.Flags.HasFlag(RequestFlags.IntraOrg))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorInvalidParametersForLocalPublicFolderMailboxMigration), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x06007CEF RID: 31983 RVA: 0x001FF304 File Offset: 0x001FD504
		protected override void ModifyRequest(TransactionalRequestJob requestJob)
		{
			base.ModifyRequest(requestJob);
			if (base.IsFieldSet("SourceDatabase"))
			{
				PublicFolderDatabase publicFolderDatabase = (PublicFolderDatabase)base.GetDataObject<PublicFolderDatabase>(this.SourceDatabase, base.ConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.SourceDatabase.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.SourceDatabase.ToString())));
				DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(publicFolderDatabase.Id.ObjectGuid, null, null, FindServerFlags.None);
				if (!this.IsSupportedDatabaseVersion(databaseInformation.ServerVersion))
				{
					base.WriteError(new DatabaseVersionUnsupportedPermanentException(publicFolderDatabase.Identity.ToString(), databaseInformation.ServerFqdn, new ServerVersion(databaseInformation.ServerVersion).ToString()), ErrorCategory.InvalidArgument, null);
				}
				requestJob.SourceDatabase = publicFolderDatabase.Id;
			}
			if (base.IsFieldSet("RemoteMailboxLegacyDN"))
			{
				requestJob.RemoteMailboxLegacyDN = this.RemoteMailboxLegacyDN;
			}
			if (base.IsFieldSet("RemoteMailboxServerLegacyDN"))
			{
				requestJob.RemoteMailboxServerLegacyDN = this.RemoteMailboxServerLegacyDN;
			}
			if (base.IsFieldSet("OutlookAnywhereHostName"))
			{
				requestJob.OutlookAnywhereHostName = this.OutlookAnywhereHostName;
			}
			if (base.IsFieldSet("AuthenticationMethod"))
			{
				requestJob.AuthenticationMethod = new AuthenticationMethod?(this.AuthenticationMethod);
			}
			if (base.IsFieldSet("RemoteCredential"))
			{
				requestJob.RemoteCredential = RequestTaskHelper.GetNetworkCredential(this.RemoteCredential, requestJob.AuthenticationMethod);
			}
		}

		// Token: 0x06007CF0 RID: 31984 RVA: 0x001FF45C File Offset: 0x001FD65C
		protected override void CheckIndexEntry()
		{
		}

		// Token: 0x06007CF1 RID: 31985 RVA: 0x001FF45E File Offset: 0x001FD65E
		private bool IsSupportedDatabaseVersion(int serverVersion)
		{
			return serverVersion >= Server.E15MinVersion || (serverVersion >= Server.E14MinVersion && serverVersion < Server.E15MinVersion) || (serverVersion >= Server.E2007SP2MinVersion && serverVersion < Server.E14MinVersion);
		}

		// Token: 0x04003DA1 RID: 15777
		private const string TaskNoun = "PublicFolderMailboxMigrationRequest";

		// Token: 0x04003DA2 RID: 15778
		private const string ParameterSetOutlookAnywherePublicFolderMailboxMigration = "MailboxMigrationOutlookAnywherePublicFolder";

		// Token: 0x04003DA3 RID: 15779
		private const string ParameterRemoteMailboxLegacyDN = "RemoteMailboxLegacyDN";

		// Token: 0x04003DA4 RID: 15780
		private const string ParameterRemoteMailboxServerLegacyDN = "RemoteMailboxServerLegacyDN";

		// Token: 0x04003DA5 RID: 15781
		private const string ParameterOutlookAnywhereHostName = "OutlookAnywhereHostName";

		// Token: 0x04003DA6 RID: 15782
		private const string ParameterAuthenticationMethod = "AuthenticationMethod";

		// Token: 0x04003DA7 RID: 15783
		private const string ParameterSetLocalPublicFolderMailboxMigration = "MailboxMigrationLocalPublicFolder";

		// Token: 0x04003DA8 RID: 15784
		private const string ParameterSourceDatabase = "SourceDatabase";
	}
}

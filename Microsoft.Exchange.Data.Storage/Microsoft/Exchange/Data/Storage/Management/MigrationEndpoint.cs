using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management.Migration;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A25 RID: 2597
	[Serializable]
	public class MigrationEndpoint : ConfigurableObject
	{
		// Token: 0x06005F59 RID: 24409 RVA: 0x0019309C File Offset: 0x0019129C
		public MigrationEndpoint() : base(new SimplePropertyBag(MigrationEndpointSchema.Identity, MigrationEndpointSchema.ObjectState, MigrationEndpointSchema.ExchangeVersion))
		{
		}

		// Token: 0x17001A34 RID: 6708
		// (get) Token: 0x06005F5A RID: 24410 RVA: 0x001930B8 File Offset: 0x001912B8
		// (set) Token: 0x06005F5B RID: 24411 RVA: 0x001930C5 File Offset: 0x001912C5
		public new MigrationEndpointId Identity
		{
			get
			{
				return (MigrationEndpointId)base.Identity;
			}
			set
			{
				this[MigrationEndpointSchema.Identity] = value;
			}
		}

		// Token: 0x17001A35 RID: 6709
		// (get) Token: 0x06005F5C RID: 24412 RVA: 0x001930D3 File Offset: 0x001912D3
		// (set) Token: 0x06005F5D RID: 24413 RVA: 0x001930E5 File Offset: 0x001912E5
		public MigrationType EndpointType
		{
			get
			{
				return (MigrationType)this[MigrationEndpointSchema.EndpointType];
			}
			set
			{
				this[MigrationEndpointSchema.EndpointType] = value;
			}
		}

		// Token: 0x17001A36 RID: 6710
		// (get) Token: 0x06005F5E RID: 24414 RVA: 0x001930F8 File Offset: 0x001912F8
		// (set) Token: 0x06005F5F RID: 24415 RVA: 0x0019310A File Offset: 0x0019130A
		public Unlimited<int> MaxConcurrentMigrations
		{
			get
			{
				return (Unlimited<int>)this[MigrationEndpointSchema.MaxConcurrentMigrations];
			}
			set
			{
				this[MigrationEndpointSchema.MaxConcurrentMigrations] = value;
			}
		}

		// Token: 0x17001A37 RID: 6711
		// (get) Token: 0x06005F60 RID: 24416 RVA: 0x0019311D File Offset: 0x0019131D
		// (set) Token: 0x06005F61 RID: 24417 RVA: 0x0019312F File Offset: 0x0019132F
		public Unlimited<int> MaxConcurrentIncrementalSyncs
		{
			get
			{
				return (Unlimited<int>)this[MigrationEndpointSchema.MaxConcurrentIncrementalSyncs];
			}
			set
			{
				this[MigrationEndpointSchema.MaxConcurrentIncrementalSyncs] = value;
			}
		}

		// Token: 0x17001A38 RID: 6712
		// (get) Token: 0x06005F62 RID: 24418 RVA: 0x00193142 File Offset: 0x00191342
		// (set) Token: 0x06005F63 RID: 24419 RVA: 0x0019314A File Offset: 0x0019134A
		public int? ActiveMigrationCount { get; set; }

		// Token: 0x17001A39 RID: 6713
		// (get) Token: 0x06005F64 RID: 24420 RVA: 0x00193153 File Offset: 0x00191353
		// (set) Token: 0x06005F65 RID: 24421 RVA: 0x0019315B File Offset: 0x0019135B
		public int? ActiveIncrementalSyncCount { get; set; }

		// Token: 0x17001A3A RID: 6714
		// (get) Token: 0x06005F66 RID: 24422 RVA: 0x00193164 File Offset: 0x00191364
		// (set) Token: 0x06005F67 RID: 24423 RVA: 0x00193176 File Offset: 0x00191376
		public Fqdn RemoteServer
		{
			get
			{
				return (Fqdn)this[MigrationEndpointSchema.RemoteServer];
			}
			set
			{
				this[MigrationEndpointSchema.RemoteServer] = value;
			}
		}

		// Token: 0x17001A3B RID: 6715
		// (get) Token: 0x06005F68 RID: 24424 RVA: 0x00193184 File Offset: 0x00191384
		// (set) Token: 0x06005F69 RID: 24425 RVA: 0x00193196 File Offset: 0x00191396
		public string Username
		{
			get
			{
				return (string)this[MigrationEndpointSchema.Username];
			}
			set
			{
				this[MigrationEndpointSchema.Username] = value;
			}
		}

		// Token: 0x17001A3C RID: 6716
		// (get) Token: 0x06005F6A RID: 24426 RVA: 0x001931A4 File Offset: 0x001913A4
		// (set) Token: 0x06005F6B RID: 24427 RVA: 0x001931B6 File Offset: 0x001913B6
		public int? Port
		{
			get
			{
				return (int?)this[MigrationEndpointSchema.Port];
			}
			set
			{
				this[MigrationEndpointSchema.Port] = value;
			}
		}

		// Token: 0x17001A3D RID: 6717
		// (get) Token: 0x06005F6C RID: 24428 RVA: 0x001931C9 File Offset: 0x001913C9
		// (set) Token: 0x06005F6D RID: 24429 RVA: 0x001931DB File Offset: 0x001913DB
		public AuthenticationMethod? Authentication
		{
			get
			{
				return (AuthenticationMethod?)this[MigrationEndpointSchema.AuthenticationMethod];
			}
			set
			{
				this[MigrationEndpointSchema.AuthenticationMethod] = value;
			}
		}

		// Token: 0x17001A3E RID: 6718
		// (get) Token: 0x06005F6E RID: 24430 RVA: 0x001931EE File Offset: 0x001913EE
		// (set) Token: 0x06005F6F RID: 24431 RVA: 0x00193200 File Offset: 0x00191400
		public IMAPSecurityMechanism? Security
		{
			get
			{
				return (IMAPSecurityMechanism?)this[MigrationEndpointSchema.Security];
			}
			set
			{
				this[MigrationEndpointSchema.Security] = value;
			}
		}

		// Token: 0x17001A3F RID: 6719
		// (get) Token: 0x06005F70 RID: 24432 RVA: 0x00193213 File Offset: 0x00191413
		// (set) Token: 0x06005F71 RID: 24433 RVA: 0x00193225 File Offset: 0x00191425
		public Fqdn RpcProxyServer
		{
			get
			{
				return (Fqdn)this[MigrationEndpointSchema.RPCProxyServer];
			}
			set
			{
				this[MigrationEndpointSchema.RPCProxyServer] = value;
			}
		}

		// Token: 0x17001A40 RID: 6720
		// (get) Token: 0x06005F72 RID: 24434 RVA: 0x00193233 File Offset: 0x00191433
		// (set) Token: 0x06005F73 RID: 24435 RVA: 0x00193245 File Offset: 0x00191445
		public string ExchangeServer
		{
			get
			{
				return (string)this[MigrationEndpointSchema.ExchangeServer];
			}
			set
			{
				this[MigrationEndpointSchema.ExchangeServer] = value;
			}
		}

		// Token: 0x17001A41 RID: 6721
		// (get) Token: 0x06005F74 RID: 24436 RVA: 0x00193253 File Offset: 0x00191453
		// (set) Token: 0x06005F75 RID: 24437 RVA: 0x00193265 File Offset: 0x00191465
		public string NspiServer
		{
			get
			{
				return (string)this[MigrationEndpointSchema.NspiServer];
			}
			set
			{
				this[MigrationEndpointSchema.NspiServer] = value;
			}
		}

		// Token: 0x17001A42 RID: 6722
		// (get) Token: 0x06005F76 RID: 24438 RVA: 0x00193273 File Offset: 0x00191473
		// (set) Token: 0x06005F77 RID: 24439 RVA: 0x00193285 File Offset: 0x00191485
		public bool? UseAutoDiscover
		{
			get
			{
				return (bool?)this[MigrationEndpointSchema.UseAutoDiscover];
			}
			set
			{
				this[MigrationEndpointSchema.UseAutoDiscover] = value;
			}
		}

		// Token: 0x17001A43 RID: 6723
		// (get) Token: 0x06005F78 RID: 24440 RVA: 0x00193298 File Offset: 0x00191498
		// (set) Token: 0x06005F79 RID: 24441 RVA: 0x001932AA File Offset: 0x001914AA
		public MigrationMailboxPermission MailboxPermission
		{
			get
			{
				return (MigrationMailboxPermission)this[MigrationEndpointSchema.MailboxPermission];
			}
			set
			{
				this[MigrationEndpointSchema.MailboxPermission] = value;
			}
		}

		// Token: 0x17001A44 RID: 6724
		// (get) Token: 0x06005F7A RID: 24442 RVA: 0x001932BD File Offset: 0x001914BD
		public Guid Guid
		{
			get
			{
				return this.Identity.Guid;
			}
		}

		// Token: 0x17001A45 RID: 6725
		// (get) Token: 0x06005F7B RID: 24443 RVA: 0x001932CC File Offset: 0x001914CC
		public bool IsRemote
		{
			get
			{
				MigrationType endpointType = this.EndpointType;
				if (endpointType <= MigrationType.ExchangeOutlookAnywhere)
				{
					if (endpointType != MigrationType.IMAP && endpointType != MigrationType.ExchangeOutlookAnywhere)
					{
						return false;
					}
				}
				else if (endpointType != MigrationType.ExchangeRemoteMove && endpointType != MigrationType.PSTImport && endpointType != MigrationType.PublicFolder)
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x17001A46 RID: 6726
		// (get) Token: 0x06005F7C RID: 24444 RVA: 0x00193303 File Offset: 0x00191503
		// (set) Token: 0x06005F7D RID: 24445 RVA: 0x00193315 File Offset: 0x00191515
		public string SourceMailboxLegacyDN
		{
			get
			{
				return (string)this[MigrationEndpointSchema.SourceMailboxLegacyDN];
			}
			set
			{
				this[MigrationEndpointSchema.SourceMailboxLegacyDN] = value;
			}
		}

		// Token: 0x17001A47 RID: 6727
		// (get) Token: 0x06005F7E RID: 24446 RVA: 0x00193323 File Offset: 0x00191523
		// (set) Token: 0x06005F7F RID: 24447 RVA: 0x00193335 File Offset: 0x00191535
		public string PublicFolderDatabaseServerLegacyDN
		{
			get
			{
				return (string)this[MigrationEndpointSchema.PublicFolderDatabaseServerLegacyDN];
			}
			set
			{
				this[MigrationEndpointSchema.PublicFolderDatabaseServerLegacyDN] = value;
			}
		}

		// Token: 0x17001A48 RID: 6728
		// (get) Token: 0x06005F80 RID: 24448 RVA: 0x00193343 File Offset: 0x00191543
		// (set) Token: 0x06005F81 RID: 24449 RVA: 0x00193355 File Offset: 0x00191555
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

		// Token: 0x17001A49 RID: 6729
		// (get) Token: 0x06005F82 RID: 24450 RVA: 0x00193363 File Offset: 0x00191563
		// (set) Token: 0x06005F83 RID: 24451 RVA: 0x0019336B File Offset: 0x0019156B
		internal SmtpAddress EmailAddress { get; set; }

		// Token: 0x17001A4A RID: 6730
		// (get) Token: 0x06005F84 RID: 24452 RVA: 0x00193374 File Offset: 0x00191574
		// (set) Token: 0x06005F85 RID: 24453 RVA: 0x0019337C File Offset: 0x0019157C
		internal PSCredential Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
				if (value == null)
				{
					this.Username = null;
					return;
				}
				this.Username = value.UserName;
			}
		}

		// Token: 0x17001A4B RID: 6731
		// (get) Token: 0x06005F86 RID: 24454 RVA: 0x0019339C File Offset: 0x0019159C
		// (set) Token: 0x06005F87 RID: 24455 RVA: 0x001933A4 File Offset: 0x001915A4
		internal DateTime LastModifiedTime { get; set; }

		// Token: 0x17001A4C RID: 6732
		// (get) Token: 0x06005F88 RID: 24456 RVA: 0x001933AD File Offset: 0x001915AD
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17001A4D RID: 6733
		// (get) Token: 0x06005F89 RID: 24457 RVA: 0x001933B4 File Offset: 0x001915B4
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<MigrationEndpointSchema>();
			}
		}

		// Token: 0x06005F8A RID: 24458 RVA: 0x001933BB File Offset: 0x001915BB
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return base.ToString();
		}

		// Token: 0x040035FA RID: 13818
		[NonSerialized]
		private PSCredential credentials;
	}
}

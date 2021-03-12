using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A11 RID: 2577
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class MRSRequest : UserConfigurationObject
	{
		// Token: 0x06005EA0 RID: 24224 RVA: 0x0018FD78 File Offset: 0x0018DF78
		public MRSRequest()
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Exchange2012);
		}

		// Token: 0x170019F4 RID: 6644
		// (get) Token: 0x06005EA1 RID: 24225 RVA: 0x0018FD8B File Offset: 0x0018DF8B
		internal override UserConfigurationObjectSchema Schema
		{
			get
			{
				return MRSRequest.schema;
			}
		}

		// Token: 0x170019F5 RID: 6645
		// (get) Token: 0x06005EA2 RID: 24226 RVA: 0x0018FD92 File Offset: 0x0018DF92
		// (set) Token: 0x06005EA3 RID: 24227 RVA: 0x0018FDA4 File Offset: 0x0018DFA4
		public Guid RequestGuid
		{
			get
			{
				return (Guid)this[MRSRequestSchema.RequestGuid];
			}
			set
			{
				this[MRSRequestSchema.RequestGuid] = value;
			}
		}

		// Token: 0x170019F6 RID: 6646
		// (get) Token: 0x06005EA4 RID: 24228 RVA: 0x0018FDB7 File Offset: 0x0018DFB7
		// (set) Token: 0x06005EA5 RID: 24229 RVA: 0x0018FDC9 File Offset: 0x0018DFC9
		public string Name
		{
			get
			{
				return (string)this[MRSRequestSchema.Name];
			}
			set
			{
				this[MRSRequestSchema.Name] = value;
			}
		}

		// Token: 0x170019F7 RID: 6647
		// (get) Token: 0x06005EA6 RID: 24230 RVA: 0x0018FDD7 File Offset: 0x0018DFD7
		// (set) Token: 0x06005EA7 RID: 24231 RVA: 0x0018FDE9 File Offset: 0x0018DFE9
		public RequestStatus Status
		{
			get
			{
				return (RequestStatus)this[MRSRequestSchema.Status];
			}
			set
			{
				this[MRSRequestSchema.Status] = value;
			}
		}

		// Token: 0x170019F8 RID: 6648
		// (get) Token: 0x06005EA8 RID: 24232 RVA: 0x0018FDFC File Offset: 0x0018DFFC
		// (set) Token: 0x06005EA9 RID: 24233 RVA: 0x0018FE0E File Offset: 0x0018E00E
		public RequestFlags Flags
		{
			get
			{
				return (RequestFlags)this[MRSRequestSchema.Flags];
			}
			set
			{
				this[MRSRequestSchema.Flags] = value;
			}
		}

		// Token: 0x170019F9 RID: 6649
		// (get) Token: 0x06005EAA RID: 24234 RVA: 0x0018FE21 File Offset: 0x0018E021
		// (set) Token: 0x06005EAB RID: 24235 RVA: 0x0018FE33 File Offset: 0x0018E033
		public string RemoteHostName
		{
			get
			{
				return (string)this[MRSRequestSchema.RemoteHostName];
			}
			set
			{
				this[MRSRequestSchema.RemoteHostName] = value;
			}
		}

		// Token: 0x170019FA RID: 6650
		// (get) Token: 0x06005EAC RID: 24236 RVA: 0x0018FE41 File Offset: 0x0018E041
		// (set) Token: 0x06005EAD RID: 24237 RVA: 0x0018FE53 File Offset: 0x0018E053
		public string BatchName
		{
			get
			{
				return (string)this[MRSRequestSchema.BatchName];
			}
			set
			{
				this[MRSRequestSchema.BatchName] = value;
			}
		}

		// Token: 0x170019FB RID: 6651
		// (get) Token: 0x06005EAE RID: 24238 RVA: 0x0018FE61 File Offset: 0x0018E061
		// (set) Token: 0x06005EAF RID: 24239 RVA: 0x0018FE73 File Offset: 0x0018E073
		public ADObjectId SourceMDB
		{
			get
			{
				return (ADObjectId)this[MRSRequestSchema.SourceMDB];
			}
			set
			{
				this[MRSRequestSchema.SourceMDB] = ADObjectIdResolutionHelper.ResolveDN(value);
			}
		}

		// Token: 0x170019FC RID: 6652
		// (get) Token: 0x06005EB0 RID: 24240 RVA: 0x0018FE86 File Offset: 0x0018E086
		// (set) Token: 0x06005EB1 RID: 24241 RVA: 0x0018FE98 File Offset: 0x0018E098
		public ADObjectId TargetMDB
		{
			get
			{
				return (ADObjectId)this[MRSRequestSchema.TargetMDB];
			}
			set
			{
				this[MRSRequestSchema.TargetMDB] = ADObjectIdResolutionHelper.ResolveDN(value);
			}
		}

		// Token: 0x170019FD RID: 6653
		// (get) Token: 0x06005EB2 RID: 24242 RVA: 0x0018FEAB File Offset: 0x0018E0AB
		// (set) Token: 0x06005EB3 RID: 24243 RVA: 0x0018FEBD File Offset: 0x0018E0BD
		public ADObjectId StorageMDB
		{
			get
			{
				return (ADObjectId)this[MRSRequestSchema.StorageMDB];
			}
			set
			{
				this[MRSRequestSchema.StorageMDB] = ADObjectIdResolutionHelper.ResolveDN(value);
			}
		}

		// Token: 0x170019FE RID: 6654
		// (get) Token: 0x06005EB4 RID: 24244 RVA: 0x0018FED0 File Offset: 0x0018E0D0
		// (set) Token: 0x06005EB5 RID: 24245 RVA: 0x0018FEE2 File Offset: 0x0018E0E2
		public string FilePath
		{
			get
			{
				return (string)this[MRSRequestSchema.FilePath];
			}
			set
			{
				this[MRSRequestSchema.FilePath] = value;
			}
		}

		// Token: 0x170019FF RID: 6655
		// (get) Token: 0x06005EB6 RID: 24246 RVA: 0x0018FEF0 File Offset: 0x0018E0F0
		// (set) Token: 0x06005EB7 RID: 24247 RVA: 0x0018FF02 File Offset: 0x0018E102
		public MRSRequestType Type
		{
			get
			{
				return (MRSRequestType)this[MRSRequestSchema.Type];
			}
			set
			{
				this[MRSRequestSchema.Type] = value;
			}
		}

		// Token: 0x17001A00 RID: 6656
		// (get) Token: 0x06005EB8 RID: 24248 RVA: 0x0018FF15 File Offset: 0x0018E115
		// (set) Token: 0x06005EB9 RID: 24249 RVA: 0x0018FF27 File Offset: 0x0018E127
		public ADObjectId TargetUserId
		{
			get
			{
				return (ADObjectId)this[MRSRequestSchema.TargetUserId];
			}
			set
			{
				this[MRSRequestSchema.TargetUserId] = value;
			}
		}

		// Token: 0x17001A01 RID: 6657
		// (get) Token: 0x06005EBA RID: 24250 RVA: 0x0018FF35 File Offset: 0x0018E135
		// (set) Token: 0x06005EBB RID: 24251 RVA: 0x0018FF47 File Offset: 0x0018E147
		public ADObjectId SourceUserId
		{
			get
			{
				return (ADObjectId)this[MRSRequestSchema.SourceUserId];
			}
			set
			{
				this[MRSRequestSchema.SourceUserId] = value;
			}
		}

		// Token: 0x17001A02 RID: 6658
		// (get) Token: 0x06005EBC RID: 24252 RVA: 0x0018FF55 File Offset: 0x0018E155
		// (set) Token: 0x06005EBD RID: 24253 RVA: 0x0018FF5D File Offset: 0x0018E15D
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x17001A03 RID: 6659
		// (get) Token: 0x06005EBE RID: 24254 RVA: 0x0018FF66 File Offset: 0x0018E166
		public DateTime? WhenChanged
		{
			get
			{
				return (DateTime?)this[MRSRequestSchema.WhenChanged];
			}
		}

		// Token: 0x17001A04 RID: 6660
		// (get) Token: 0x06005EBF RID: 24255 RVA: 0x0018FF78 File Offset: 0x0018E178
		public DateTime? WhenCreated
		{
			get
			{
				return (DateTime?)this[MRSRequestSchema.WhenCreated];
			}
		}

		// Token: 0x17001A05 RID: 6661
		// (get) Token: 0x06005EC0 RID: 24256 RVA: 0x0018FF8A File Offset: 0x0018E18A
		public DateTime? WhenChangedUTC
		{
			get
			{
				return (DateTime?)this[MRSRequestSchema.WhenChangedUTC];
			}
		}

		// Token: 0x17001A06 RID: 6662
		// (get) Token: 0x06005EC1 RID: 24257 RVA: 0x0018FF9C File Offset: 0x0018E19C
		public DateTime? WhenCreatedUTC
		{
			get
			{
				return (DateTime?)this[MRSRequestSchema.WhenCreatedUTC];
			}
		}

		// Token: 0x06005EC2 RID: 24258 RVA: 0x0018FFB0 File Offset: 0x0018E1B0
		public static T Read<T>(MailboxStoreTypeProvider session, Guid requestGuid) where T : MRSRequest, new()
		{
			ExchangePrincipal principal = ExchangePrincipal.FromADUser(session.ADUser, null);
			T result;
			using (UserConfigurationDictionaryAdapter<T> userConfigurationDictionaryAdapter = new UserConfigurationDictionaryAdapter<T>(session.MailboxSession, MRSRequest.GetName(requestGuid), new GetUserConfigurationDelegate(UserConfigurationHelper.GetMailboxConfiguration), MRSRequestSchema.PersistedProperties))
			{
				try
				{
					T t = userConfigurationDictionaryAdapter.Read(principal);
					if (t.RequestGuid != requestGuid)
					{
						throw new CannotFindRequestIndexEntryException(requestGuid);
					}
					result = t;
				}
				catch (FormatException innerException)
				{
					throw new CannotFindRequestIndexEntryException(requestGuid, innerException);
				}
			}
			return result;
		}

		// Token: 0x06005EC3 RID: 24259 RVA: 0x00190048 File Offset: 0x0018E248
		public override void Delete(MailboxStoreTypeProvider session)
		{
			using (UserConfiguration mailboxConfiguration = UserConfigurationHelper.GetMailboxConfiguration(session.MailboxSession, MRSRequest.GetName(this.RequestGuid), UserConfigurationTypes.Dictionary, false))
			{
				if (mailboxConfiguration == null)
				{
					return;
				}
			}
			UserConfigurationHelper.DeleteMailboxConfiguration(session.MailboxSession, MRSRequest.GetName(this.RequestGuid));
		}

		// Token: 0x06005EC4 RID: 24260 RVA: 0x001900A8 File Offset: 0x0018E2A8
		public override IConfigurable Read(MailboxStoreTypeProvider session, ObjectId identity)
		{
			Guid requestGuid = new Guid(identity.GetBytes());
			return MRSRequest.Read<MRSRequest>(session, requestGuid);
		}

		// Token: 0x06005EC5 RID: 24261 RVA: 0x001900CC File Offset: 0x0018E2CC
		public override void Save(MailboxStoreTypeProvider session)
		{
			using (UserConfigurationDictionaryAdapter<MRSRequest> userConfigurationDictionaryAdapter = new UserConfigurationDictionaryAdapter<MRSRequest>(session.MailboxSession, MRSRequest.GetName(this.RequestGuid), SaveMode.NoConflictResolutionForceSave, new GetUserConfigurationDelegate(UserConfigurationHelper.GetMailboxConfiguration), MRSRequestSchema.PersistedProperties))
			{
				userConfigurationDictionaryAdapter.Save(this);
			}
			base.ResetChangeTracking();
		}

		// Token: 0x06005EC6 RID: 24262 RVA: 0x0019012C File Offset: 0x0018E32C
		private static string GetName(Guid requestGuid)
		{
			return string.Format("{0}.{1}", "MRSRequest", requestGuid.ToString("N"));
		}

		// Token: 0x040034B0 RID: 13488
		public const string ConfigurationNamePrefix = "MRSRequest";

		// Token: 0x040034B1 RID: 13489
		protected static readonly MRSRequestSchema schema = ObjectSchema.GetInstance<MRSRequestSchema>();
	}
}

using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009F9 RID: 2553
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class AggregatedAccountConfiguration : UserConfigurationObject
	{
		// Token: 0x17001985 RID: 6533
		// (get) Token: 0x06005D49 RID: 23881 RVA: 0x0018B250 File Offset: 0x00189450
		internal override UserConfigurationObjectSchema Schema
		{
			get
			{
				return AggregatedAccountConfiguration.schema;
			}
		}

		// Token: 0x06005D4A RID: 23882 RVA: 0x0018B257 File Offset: 0x00189457
		public AggregatedAccountConfiguration()
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Exchange2012);
		}

		// Token: 0x17001986 RID: 6534
		// (get) Token: 0x06005D4B RID: 23883 RVA: 0x0018B26A File Offset: 0x0018946A
		// (set) Token: 0x06005D4C RID: 23884 RVA: 0x0018B27C File Offset: 0x0018947C
		public SmtpAddress? EmailAddress
		{
			get
			{
				return (SmtpAddress?)this[AggregatedAccountConfigurationSchema.EmailAddress];
			}
			set
			{
				this[AggregatedAccountConfigurationSchema.EmailAddress] = value;
			}
		}

		// Token: 0x17001987 RID: 6535
		// (get) Token: 0x06005D4D RID: 23885 RVA: 0x0018B28F File Offset: 0x0018948F
		// (set) Token: 0x06005D4E RID: 23886 RVA: 0x0018B2A1 File Offset: 0x001894A1
		public int? SyncFailureCode
		{
			get
			{
				return (int?)this[AggregatedAccountConfigurationSchema.SyncFailureCode];
			}
			set
			{
				this[AggregatedAccountConfigurationSchema.SyncFailureCode] = value;
			}
		}

		// Token: 0x17001988 RID: 6536
		// (get) Token: 0x06005D4F RID: 23887 RVA: 0x0018B2B4 File Offset: 0x001894B4
		// (set) Token: 0x06005D50 RID: 23888 RVA: 0x0018B2C6 File Offset: 0x001894C6
		public ExDateTime? SyncFailureTimestamp
		{
			get
			{
				return (ExDateTime?)this[AggregatedAccountConfigurationSchema.SyncFailureTimestamp];
			}
			set
			{
				this[AggregatedAccountConfigurationSchema.SyncFailureTimestamp] = value;
			}
		}

		// Token: 0x17001989 RID: 6537
		// (get) Token: 0x06005D51 RID: 23889 RVA: 0x0018B2D9 File Offset: 0x001894D9
		// (set) Token: 0x06005D52 RID: 23890 RVA: 0x0018B2EB File Offset: 0x001894EB
		public string SyncFailureType
		{
			get
			{
				return (string)this[AggregatedAccountConfigurationSchema.SyncFailureType];
			}
			set
			{
				this[AggregatedAccountConfigurationSchema.SyncFailureType] = value;
			}
		}

		// Token: 0x1700198A RID: 6538
		// (get) Token: 0x06005D53 RID: 23891 RVA: 0x0018B2F9 File Offset: 0x001894F9
		// (set) Token: 0x06005D54 RID: 23892 RVA: 0x0018B30B File Offset: 0x0018950B
		public ExDateTime? SyncLastUpdateTimestamp
		{
			get
			{
				return (ExDateTime?)this[AggregatedAccountConfigurationSchema.SyncLastUpdateTimestamp];
			}
			set
			{
				this[AggregatedAccountConfigurationSchema.SyncLastUpdateTimestamp] = value;
			}
		}

		// Token: 0x1700198B RID: 6539
		// (get) Token: 0x06005D55 RID: 23893 RVA: 0x0018B31E File Offset: 0x0018951E
		// (set) Token: 0x06005D56 RID: 23894 RVA: 0x0018B330 File Offset: 0x00189530
		public ExDateTime? SyncQueuedTimestamp
		{
			get
			{
				return (ExDateTime?)this[AggregatedAccountConfigurationSchema.SyncQueuedTimestamp];
			}
			set
			{
				this[AggregatedAccountConfigurationSchema.SyncQueuedTimestamp] = value;
			}
		}

		// Token: 0x1700198C RID: 6540
		// (get) Token: 0x06005D57 RID: 23895 RVA: 0x0018B343 File Offset: 0x00189543
		// (set) Token: 0x06005D58 RID: 23896 RVA: 0x0018B355 File Offset: 0x00189555
		public Guid? SyncRequestGuid
		{
			get
			{
				return (Guid?)this[AggregatedAccountConfigurationSchema.SyncRequestGuid];
			}
			set
			{
				this[AggregatedAccountConfigurationSchema.SyncRequestGuid] = value;
			}
		}

		// Token: 0x1700198D RID: 6541
		// (get) Token: 0x06005D59 RID: 23897 RVA: 0x0018B368 File Offset: 0x00189568
		// (set) Token: 0x06005D5A RID: 23898 RVA: 0x0018B37A File Offset: 0x0018957A
		public ExDateTime? SyncStartTimestamp
		{
			get
			{
				return (ExDateTime?)this[AggregatedAccountConfigurationSchema.SyncStartTimestamp];
			}
			set
			{
				this[AggregatedAccountConfigurationSchema.SyncStartTimestamp] = value;
			}
		}

		// Token: 0x1700198E RID: 6542
		// (get) Token: 0x06005D5B RID: 23899 RVA: 0x0018B38D File Offset: 0x0018958D
		// (set) Token: 0x06005D5C RID: 23900 RVA: 0x0018B39F File Offset: 0x0018959F
		public RequestStatus? SyncStatus
		{
			get
			{
				return (RequestStatus?)this[AggregatedAccountConfigurationSchema.SyncStatus];
			}
			set
			{
				this[AggregatedAccountConfigurationSchema.SyncStatus] = value;
			}
		}

		// Token: 0x1700198F RID: 6543
		// (get) Token: 0x06005D5D RID: 23901 RVA: 0x0018B3B2 File Offset: 0x001895B2
		// (set) Token: 0x06005D5E RID: 23902 RVA: 0x0018B3C4 File Offset: 0x001895C4
		public ExDateTime? SyncSuspendedTimestamp
		{
			get
			{
				return (ExDateTime?)this[AggregatedAccountConfigurationSchema.SyncSuspendedTimestamp];
			}
			set
			{
				this[AggregatedAccountConfigurationSchema.SyncSuspendedTimestamp] = value;
			}
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x0018B3D8 File Offset: 0x001895D8
		public override void Delete(MailboxStoreTypeProvider session)
		{
			using (UserConfiguration mailboxConfiguration = UserConfigurationHelper.GetMailboxConfiguration(session.MailboxSession, "AggregatedAccount", UserConfigurationTypes.Dictionary, false))
			{
				if (mailboxConfiguration == null)
				{
					return;
				}
			}
			UserConfigurationHelper.DeleteMailboxConfiguration(session.MailboxSession, "AggregatedAccount");
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x0018B42C File Offset: 0x0018962C
		public override IConfigurable Read(MailboxStoreTypeProvider session, ObjectId identity)
		{
			base.Principal = ExchangePrincipal.FromADUser(session.ADUser, null);
			IConfigurable result;
			using (UserConfigurationDictionaryAdapter<AggregatedAccountConfiguration> userConfigurationDictionaryAdapter = new UserConfigurationDictionaryAdapter<AggregatedAccountConfiguration>(session.MailboxSession, "AggregatedAccount", new GetUserConfigurationDelegate(UserConfigurationHelper.GetMailboxConfiguration), AggregatedAccountConfiguration.aggregatedAccountProperties))
			{
				result = userConfigurationDictionaryAdapter.Read(base.Principal);
			}
			return result;
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x0018B498 File Offset: 0x00189698
		public override void Save(MailboxStoreTypeProvider session)
		{
			using (UserConfigurationDictionaryAdapter<AggregatedAccountConfiguration> userConfigurationDictionaryAdapter = new UserConfigurationDictionaryAdapter<AggregatedAccountConfiguration>(session.MailboxSession, "AggregatedAccount", SaveMode.NoConflictResolutionForceSave, new GetUserConfigurationDelegate(UserConfigurationHelper.GetMailboxConfiguration), AggregatedAccountConfiguration.aggregatedAccountProperties))
			{
				userConfigurationDictionaryAdapter.Save(this);
			}
			base.ResetChangeTracking();
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x0018B4F4 File Offset: 0x001896F4
		internal static object SmtpAddressGetter(IPropertyBag propertyBag)
		{
			string text = propertyBag[AggregatedAccountConfigurationSchema.EmailAddressRaw] as string;
			if (text != null)
			{
				return new SmtpAddress(text);
			}
			return null;
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x0018B524 File Offset: 0x00189724
		internal static void SmtpAddressSetter(object value, IPropertyBag propertyBag)
		{
			SmtpAddress? smtpAddress = value as SmtpAddress?;
			propertyBag[AggregatedAccountConfigurationSchema.EmailAddressRaw] = ((smtpAddress != null) ? smtpAddress.Value.ToString() : null);
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x0018B56C File Offset: 0x0018976C
		internal static object SyncRequestGuidGetter(IPropertyBag propertyBag)
		{
			byte[] array = propertyBag[AggregatedAccountConfigurationSchema.SyncRequestGuidRaw] as byte[];
			if (array != null && 16 == array.Length)
			{
				return new Guid(array);
			}
			return null;
		}

		// Token: 0x06005D65 RID: 23909 RVA: 0x0018B5A4 File Offset: 0x001897A4
		internal static void SyncRequestGuidSetter(object value, IPropertyBag propertyBag)
		{
			Guid? guid = value as Guid?;
			propertyBag[AggregatedAccountConfigurationSchema.SyncRequestGuidRaw] = ((guid != null) ? guid.Value.ToByteArray() : null);
		}

		// Token: 0x04003423 RID: 13347
		private static AggregatedAccountConfigurationSchema schema = ObjectSchema.GetInstance<AggregatedAccountConfigurationSchema>();

		// Token: 0x04003424 RID: 13348
		private static SimplePropertyDefinition[] aggregatedAccountProperties = new SimplePropertyDefinition[]
		{
			AggregatedAccountConfigurationSchema.EmailAddressRaw,
			AggregatedAccountConfigurationSchema.SyncFailureCode,
			AggregatedAccountConfigurationSchema.SyncFailureTimestamp,
			AggregatedAccountConfigurationSchema.SyncFailureType,
			AggregatedAccountConfigurationSchema.SyncLastUpdateTimestamp,
			AggregatedAccountConfigurationSchema.SyncQueuedTimestamp,
			AggregatedAccountConfigurationSchema.SyncRequestGuidRaw,
			AggregatedAccountConfigurationSchema.SyncStartTimestamp,
			AggregatedAccountConfigurationSchema.SyncStatus,
			AggregatedAccountConfigurationSchema.SyncSuspendedTimestamp
		};
	}
}

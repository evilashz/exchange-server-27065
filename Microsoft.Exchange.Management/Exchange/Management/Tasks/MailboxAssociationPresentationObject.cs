using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000469 RID: 1129
	[Serializable]
	public sealed class MailboxAssociationPresentationObject : ConfigurableObject
	{
		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x060027CF RID: 10191 RVA: 0x0009D239 File Offset: 0x0009B439
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxAssociationPresentationObject.schema;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x0009D240 File Offset: 0x0009B440
		// (set) Token: 0x060027D1 RID: 10193 RVA: 0x0009D248 File Offset: 0x0009B448
		public new ObjectId Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				this[SimpleProviderObjectSchema.Identity] = value;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x060027D2 RID: 10194 RVA: 0x0009D256 File Offset: 0x0009B456
		// (set) Token: 0x060027D3 RID: 10195 RVA: 0x0009D268 File Offset: 0x0009B468
		[Parameter(Mandatory = false)]
		public string ExternalId
		{
			get
			{
				return (string)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.ExternalId];
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.ExternalId] = value;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060027D4 RID: 10196 RVA: 0x0009D276 File Offset: 0x0009B476
		// (set) Token: 0x060027D5 RID: 10197 RVA: 0x0009D288 File Offset: 0x0009B488
		[Parameter(Mandatory = false)]
		public string LegacyDn
		{
			get
			{
				return (string)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.LegacyDn];
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.LegacyDn] = value;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x0009D298 File Offset: 0x0009B498
		// (set) Token: 0x060027D7 RID: 10199 RVA: 0x0009D2BD File Offset: 0x0009B4BD
		[Parameter(Mandatory = false)]
		public bool IsMember
		{
			get
			{
				return ((bool?)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.IsMember]).GetValueOrDefault();
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.IsMember] = value;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x0009D2D0 File Offset: 0x0009B4D0
		// (set) Token: 0x060027D9 RID: 10201 RVA: 0x0009D2E2 File Offset: 0x0009B4E2
		[Parameter(Mandatory = false)]
		public string JoinedBy
		{
			get
			{
				return (string)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.JoinedBy];
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.JoinedBy] = value;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x0009D2F0 File Offset: 0x0009B4F0
		// (set) Token: 0x060027DB RID: 10203 RVA: 0x0009D302 File Offset: 0x0009B502
		[Parameter(Mandatory = false)]
		public SmtpAddress GroupSmtpAddress
		{
			get
			{
				return (SmtpAddress)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.GroupSmtpAddress];
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.GroupSmtpAddress] = value;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x0009D315 File Offset: 0x0009B515
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x0009D327 File Offset: 0x0009B527
		[Parameter(Mandatory = false)]
		public SmtpAddress UserSmtpAddress
		{
			get
			{
				return (SmtpAddress)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.UserSmtpAddress];
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.UserSmtpAddress] = value;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x0009D33C File Offset: 0x0009B53C
		// (set) Token: 0x060027DF RID: 10207 RVA: 0x0009D361 File Offset: 0x0009B561
		[Parameter(Mandatory = false)]
		public bool IsPin
		{
			get
			{
				return ((bool?)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.IsPin]).GetValueOrDefault();
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.IsPin] = value;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x0009D374 File Offset: 0x0009B574
		// (set) Token: 0x060027E1 RID: 10209 RVA: 0x0009D399 File Offset: 0x0009B599
		[Parameter(Mandatory = false)]
		public bool ShouldEscalate
		{
			get
			{
				return ((bool?)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.ShouldEscalate]).GetValueOrDefault();
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.ShouldEscalate] = value;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060027E2 RID: 10210 RVA: 0x0009D3AC File Offset: 0x0009B5AC
		// (set) Token: 0x060027E3 RID: 10211 RVA: 0x0009D3D1 File Offset: 0x0009B5D1
		[Parameter(Mandatory = false)]
		public bool IsAutoSubscribed
		{
			get
			{
				return ((bool?)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.IsAutoSubscribed]).GetValueOrDefault();
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.IsAutoSubscribed] = value;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x0009D3E4 File Offset: 0x0009B5E4
		// (set) Token: 0x060027E5 RID: 10213 RVA: 0x0009D409 File Offset: 0x0009B609
		[Parameter(Mandatory = false)]
		public ExDateTime JoinDate
		{
			get
			{
				return ((ExDateTime?)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.JoinDate]).GetValueOrDefault();
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.JoinDate] = value;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x0009D41C File Offset: 0x0009B61C
		// (set) Token: 0x060027E7 RID: 10215 RVA: 0x0009D441 File Offset: 0x0009B641
		[Parameter(Mandatory = false)]
		public ExDateTime LastVisitedDate
		{
			get
			{
				return ((ExDateTime?)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.LastVisitedDate]).GetValueOrDefault();
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.LastVisitedDate] = value;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x0009D454 File Offset: 0x0009B654
		// (set) Token: 0x060027E9 RID: 10217 RVA: 0x0009D479 File Offset: 0x0009B679
		[Parameter(Mandatory = false)]
		public ExDateTime PinDate
		{
			get
			{
				return ((ExDateTime?)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.PinDate]).GetValueOrDefault();
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.PinDate] = value;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x0009D48C File Offset: 0x0009B68C
		// (set) Token: 0x060027EB RID: 10219 RVA: 0x0009D4B1 File Offset: 0x0009B6B1
		public ExDateTime LastModified
		{
			get
			{
				return ((ExDateTime?)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.LastModified]).GetValueOrDefault();
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.LastModified] = value;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x0009D4C4 File Offset: 0x0009B6C4
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x0009D4E9 File Offset: 0x0009B6E9
		[Parameter(Mandatory = false)]
		public int CurrentVersion
		{
			get
			{
				return ((int?)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.CurrentVersion]).GetValueOrDefault();
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.CurrentVersion] = value;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x0009D4FC File Offset: 0x0009B6FC
		// (set) Token: 0x060027EF RID: 10223 RVA: 0x0009D521 File Offset: 0x0009B721
		[Parameter(Mandatory = false)]
		public int SyncedVersion
		{
			get
			{
				return ((int?)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.SyncedVersion]).GetValueOrDefault();
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.SyncedVersion] = value;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x0009D534 File Offset: 0x0009B734
		// (set) Token: 0x060027F1 RID: 10225 RVA: 0x0009D546 File Offset: 0x0009B746
		[Parameter(Mandatory = false)]
		public string LastSyncError
		{
			get
			{
				return (string)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.LastSyncError];
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.LastSyncError] = value;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x0009D554 File Offset: 0x0009B754
		// (set) Token: 0x060027F3 RID: 10227 RVA: 0x0009D579 File Offset: 0x0009B779
		[Parameter(Mandatory = false)]
		public int SyncAttempts
		{
			get
			{
				return ((int?)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.SyncAttempts]).GetValueOrDefault();
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.SyncAttempts] = value;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060027F4 RID: 10228 RVA: 0x0009D58C File Offset: 0x0009B78C
		// (set) Token: 0x060027F5 RID: 10229 RVA: 0x0009D59E File Offset: 0x0009B79E
		[Parameter(Mandatory = false)]
		public string SyncedSchemaVersion
		{
			get
			{
				return (string)this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.SyncedSchemaVersion];
			}
			set
			{
				this[MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.SyncedSchemaVersion] = value;
			}
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x0009D5AC File Offset: 0x0009B7AC
		public MailboxAssociationPresentationObject() : base(new SimpleProviderPropertyBag())
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Current);
			base.ResetChangeTracking();
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x0009D5CC File Offset: 0x0009B7CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("ExternalId=");
			stringBuilder.Append(this.ExternalId);
			stringBuilder.Append(", LegacyDn=");
			stringBuilder.Append(this.LegacyDn);
			stringBuilder.Append(", IsMember=");
			stringBuilder.Append(this.IsMember);
			stringBuilder.Append(", JoinedBy=");
			stringBuilder.Append(this.JoinedBy);
			stringBuilder.Append(", JoinDate=");
			stringBuilder.Append(this.JoinDate);
			stringBuilder.Append(", GroupSmtpAddress=");
			stringBuilder.Append(this.GroupSmtpAddress);
			stringBuilder.Append(", UserSmtpAddress=");
			stringBuilder.Append(this.UserSmtpAddress);
			stringBuilder.Append(", IsPin=");
			stringBuilder.Append(this.IsPin);
			stringBuilder.Append(", ShouldEscalate=");
			stringBuilder.Append(this.ShouldEscalate);
			stringBuilder.Append(", IsAutoSubscribed=");
			stringBuilder.Append(this.IsAutoSubscribed);
			stringBuilder.Append(", LastVisitedDate=");
			stringBuilder.Append(this.LastVisitedDate);
			stringBuilder.Append(", PinDate=");
			stringBuilder.Append(this.PinDate);
			stringBuilder.Append(", CurrentVersion=");
			stringBuilder.Append(this.CurrentVersion);
			stringBuilder.Append(", SyncedVersion=");
			stringBuilder.Append(this.SyncedVersion);
			stringBuilder.Append(", LastSyncError=");
			stringBuilder.Append(this.LastSyncError);
			stringBuilder.Append(", SyncAttempts =");
			stringBuilder.Append(this.SyncAttempts);
			stringBuilder.Append(", SyncedSchemaVersion=");
			stringBuilder.Append(this.SyncedSchemaVersion);
			stringBuilder.Append(", LastModified=");
			stringBuilder.Append(this.LastModified);
			return stringBuilder.ToString();
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x0009D7C8 File Offset: 0x0009B9C8
		internal bool UpdateAssociation(MailboxAssociationFromStore association, IAssociationAdaptor associationAdaptor)
		{
			bool result = false;
			MailboxLocator slaveMailboxLocator = associationAdaptor.GetSlaveMailboxLocator(association);
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.ExternalId))
			{
				slaveMailboxLocator.ExternalId = this.ExternalId;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.LegacyDn))
			{
				slaveMailboxLocator.LegacyDn = this.LegacyDn;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.IsMember))
			{
				association.IsMember = this.IsMember;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.JoinedBy))
			{
				association.JoinedBy = this.JoinedBy;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.GroupSmtpAddress))
			{
				association.GroupSmtpAddress = this.GroupSmtpAddress;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.UserSmtpAddress))
			{
				association.UserSmtpAddress = this.UserSmtpAddress;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.IsPin))
			{
				association.IsPin = this.IsPin;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.ShouldEscalate))
			{
				association.ShouldEscalate = this.ShouldEscalate;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.IsAutoSubscribed))
			{
				association.IsAutoSubscribed = this.IsAutoSubscribed;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.JoinDate))
			{
				association.JoinDate = this.JoinDate;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.LastVisitedDate))
			{
				association.LastVisitedDate = this.LastVisitedDate;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.PinDate))
			{
				association.PinDate = this.PinDate;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.CurrentVersion))
			{
				association.CurrentVersion = this.CurrentVersion;
				result = true;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.SyncedVersion))
			{
				association.SyncedVersion = this.SyncedVersion;
				result = true;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.LastSyncError))
			{
				association.LastSyncError = this.LastSyncError;
				result = true;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.SyncAttempts))
			{
				association.SyncAttempts = this.SyncAttempts;
				result = true;
			}
			if (base.IsChanged(MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema.SyncedSchemaVersion))
			{
				association.SyncedSchemaVersion = this.SyncedSchemaVersion;
				result = true;
			}
			return result;
		}

		// Token: 0x04001DBA RID: 7610
		private static MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema schema = ObjectSchema.GetInstance<MailboxAssociationPresentationObject.MailboxAssociationPresentationObjectSchema>();

		// Token: 0x0200046A RID: 1130
		internal class MailboxAssociationPresentationObjectSchema : SimpleProviderObjectSchema
		{
			// Token: 0x04001DBB RID: 7611
			public static readonly SimpleProviderPropertyDefinition ExternalId = new SimpleProviderPropertyDefinition("ExternalId", ExchangeObjectVersion.Current, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DBC RID: 7612
			public static readonly SimpleProviderPropertyDefinition LegacyDn = new SimpleProviderPropertyDefinition("LegacyDn", ExchangeObjectVersion.Current, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DBD RID: 7613
			public static readonly SimpleProviderPropertyDefinition IsMember = new SimpleProviderPropertyDefinition("IsMember", ExchangeObjectVersion.Current, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DBE RID: 7614
			public static readonly SimpleProviderPropertyDefinition JoinedBy = new SimpleProviderPropertyDefinition("JoinedBy", ExchangeObjectVersion.Current, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DBF RID: 7615
			public static readonly SimpleProviderPropertyDefinition IsPin = new SimpleProviderPropertyDefinition("IsPin", ExchangeObjectVersion.Current, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DC0 RID: 7616
			public static readonly SimpleProviderPropertyDefinition ShouldEscalate = new SimpleProviderPropertyDefinition("ShouldEscalate", ExchangeObjectVersion.Current, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DC1 RID: 7617
			public static readonly SimpleProviderPropertyDefinition IsAutoSubscribed = new SimpleProviderPropertyDefinition("IsAutoSubscribed", ExchangeObjectVersion.Current, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DC2 RID: 7618
			public static readonly SimpleProviderPropertyDefinition JoinDate = new SimpleProviderPropertyDefinition("JoinDate", ExchangeObjectVersion.Current, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DC3 RID: 7619
			public static readonly SimpleProviderPropertyDefinition GroupSmtpAddress = new SimpleProviderPropertyDefinition("GroupSmtpAddress", ExchangeObjectVersion.Current, typeof(SmtpAddress?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DC4 RID: 7620
			public static readonly SimpleProviderPropertyDefinition UserSmtpAddress = new SimpleProviderPropertyDefinition("UserSmtpAddress", ExchangeObjectVersion.Current, typeof(SmtpAddress?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DC5 RID: 7621
			public static readonly SimpleProviderPropertyDefinition LastVisitedDate = new SimpleProviderPropertyDefinition("LastVisitedDate", ExchangeObjectVersion.Current, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DC6 RID: 7622
			public static readonly SimpleProviderPropertyDefinition PinDate = new SimpleProviderPropertyDefinition("PinDate", ExchangeObjectVersion.Current, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DC7 RID: 7623
			public static readonly SimpleProviderPropertyDefinition LastModified = new SimpleProviderPropertyDefinition("LastModified", ExchangeObjectVersion.Current, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DC8 RID: 7624
			public static readonly SimpleProviderPropertyDefinition CurrentVersion = new SimpleProviderPropertyDefinition("CurrentVersion", ExchangeObjectVersion.Current, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DC9 RID: 7625
			public static readonly SimpleProviderPropertyDefinition SyncedVersion = new SimpleProviderPropertyDefinition("SyncedVersion", ExchangeObjectVersion.Current, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DCA RID: 7626
			public static readonly SimpleProviderPropertyDefinition LastSyncError = new SimpleProviderPropertyDefinition("LastSyncError", ExchangeObjectVersion.Current, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DCB RID: 7627
			public static readonly SimpleProviderPropertyDefinition SyncAttempts = new SimpleProviderPropertyDefinition("SyncAttempts", ExchangeObjectVersion.Current, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04001DCC RID: 7628
			public static readonly SimpleProviderPropertyDefinition SyncedSchemaVersion = new SimpleProviderPropertyDefinition("SyncedSchemaVersion", ExchangeObjectVersion.Current, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}

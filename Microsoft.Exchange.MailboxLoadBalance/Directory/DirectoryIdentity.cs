using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory.ExchangeDirectory;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x02000074 RID: 116
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DirectoryIdentity : IEquatable<DirectoryIdentity>
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x0000B284 File Offset: 0x00009484
		public DirectoryIdentity(DirectoryObjectType objectType, Guid objectGuid, string objectName, Guid organizationId)
		{
			this.ObjectType = objectType;
			this.Guid = objectGuid;
			this.Name = objectName;
			this.OrganizationId = organizationId;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000B2A9 File Offset: 0x000094A9
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000B2B1 File Offset: 0x000094B1
		[DataMember]
		public string Name { get; private set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000B2BA File Offset: 0x000094BA
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000B2C2 File Offset: 0x000094C2
		[DataMember]
		public Guid Guid { get; private set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000B2CB File Offset: 0x000094CB
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000B2D3 File Offset: 0x000094D3
		[DataMember]
		public Guid OrganizationId { get; private set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000B2DC File Offset: 0x000094DC
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x0000B2E4 File Offset: 0x000094E4
		public DirectoryObjectType ObjectType { get; private set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000B2ED File Offset: 0x000094ED
		public ADObjectId ADObjectId
		{
			get
			{
				if (this.adObjectId == null)
				{
					this.adObjectId = new ADObjectId(this.Guid);
				}
				return this.adObjectId;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000B30E File Offset: 0x0000950E
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000B316 File Offset: 0x00009516
		[DataMember]
		protected int ObjectTypeInt
		{
			get
			{
				return (int)this.ObjectType;
			}
			set
			{
				this.ObjectType = (DirectoryObjectType)value;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000B31F File Offset: 0x0000951F
		public static DirectoryIdentity CreateForestIdentity(string forestName)
		{
			return new DirectoryIdentity(DirectoryObjectType.Forest, Guid.Empty, forestName, Guid.Empty);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000B332 File Offset: 0x00009532
		public static DirectoryIdentity CreateFromADObjectId(ADObjectId adObjectId, DirectoryObjectType objectType = DirectoryObjectType.Unknown)
		{
			return new DirectoryIdentity(objectType, adObjectId.ObjectGuid, adObjectId.Name, Guid.Empty);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000B34B File Offset: 0x0000954B
		public static DirectoryIdentity CreateMailboxIdentity(Guid mailboxGuid, Guid organizationId, DirectoryObjectType objectType = DirectoryObjectType.Mailbox)
		{
			return new DirectoryIdentity(objectType, mailboxGuid, string.Empty, organizationId);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000B35A File Offset: 0x0000955A
		public static DirectoryIdentity CreateMailboxIdentity(Guid guid, TenantPartitionHint tph, DirectoryObjectType objectType = DirectoryObjectType.Mailbox)
		{
			return DirectoryIdentity.CreateMailboxIdentity(guid, TenantPartitionHintAdapter.FromPartitionHint(tph), objectType);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000B369 File Offset: 0x00009569
		public static DirectoryIdentity CreateMailboxIdentity(Guid guid, TenantPartitionHintAdapter tph, DirectoryObjectType objectType = DirectoryObjectType.Mailbox)
		{
			return DirectoryIdentity.CreateMailboxIdentity(guid, tph.ExternalDirectoryOrganizationId, objectType);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000B378 File Offset: 0x00009578
		public static DirectoryIdentity CreateNonConnectedMailboxIdentity(Guid mailboxGuid, Guid organizationId)
		{
			return new DirectoryIdentity(DirectoryObjectType.NonConnectedMailbox, mailboxGuid, string.Empty, organizationId);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000B387 File Offset: 0x00009587
		public static DirectoryIdentity CreateConsumerMailboxIdentity(Guid mailboxGuid, Guid databaseGuid, Guid organizationId)
		{
			return new DirectoryIdentity(DirectoryObjectType.ConsumerMailbox, mailboxGuid, string.Empty, organizationId);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000B398 File Offset: 0x00009598
		public bool Equals(DirectoryIdentity other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || (string.Equals(this.Name, other.Name) && this.Guid.Equals(other.Guid) && this.OrganizationId.Equals(other.OrganizationId) && this.ObjectType == other.ObjectType));
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000B40A File Offset: 0x0000960A
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != base.GetType()) && this.Equals((DirectoryIdentity)obj)));
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000B444 File Offset: 0x00009644
		public override int GetHashCode()
		{
			int num = (this.Name != null) ? this.Name.GetHashCode() : 0;
			num = (num * 397 ^ this.Guid.GetHashCode());
			num = (num * 397 ^ this.OrganizationId.GetHashCode());
			return num * 397 ^ (int)this.ObjectType;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000B4B4 File Offset: 0x000096B4
		public override string ToString()
		{
			if (object.ReferenceEquals(this, DirectoryIdentity.NullIdentity))
			{
				return "{Null Identity}";
			}
			if (this.ObjectType == DirectoryObjectType.ConstraintSet)
			{
				return string.Format("[CSET: {{{0}}}]", this.Name);
			}
			return string.Format("[{0} {1} ID: {2} OID: {3}]", new object[]
			{
				this.ObjectType,
				this.Name,
				this.Guid,
				this.OrganizationId
			});
		}

		// Token: 0x0400014C RID: 332
		public static readonly DirectoryIdentity NullIdentity = new DirectoryIdentity(DirectoryObjectType.Unknown, Guid.Empty, string.Empty, Guid.Empty);

		// Token: 0x0400014D RID: 333
		[IgnoreDataMember]
		private ADObjectId adObjectId;
	}
}

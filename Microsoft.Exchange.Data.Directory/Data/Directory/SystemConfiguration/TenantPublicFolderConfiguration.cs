using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200064B RID: 1611
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TenantPublicFolderConfiguration : TenantConfigurationCacheableItem<Organization>
	{
		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x06004BB2 RID: 19378 RVA: 0x00117780 File Offset: 0x00115980
		// (set) Token: 0x06004BB3 RID: 19379 RVA: 0x00117788 File Offset: 0x00115988
		public HeuristicsFlags HeuristicsFlags { get; private set; }

		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x06004BB4 RID: 19380 RVA: 0x00117791 File Offset: 0x00115991
		public override long ItemSize
		{
			get
			{
				return this.estimatedSize;
			}
		}

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x06004BB5 RID: 19381 RVA: 0x00117799 File Offset: 0x00115999
		public PublicFoldersDeployment PublicFoldersDeploymentType
		{
			get
			{
				return this.publicFoldersDeploymentType;
			}
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x001177A4 File Offset: 0x001159A4
		public override void ReadData(IConfigurationSession configurationSession)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.FullyConsistent, configurationSession.SessionSettings, 104, "ReadData", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\TenantPublicFolderConfiguration.cs");
			this.localPublicFolderRecipients = new Hashtable();
			this.remotePublicFolderRecipients = new Hashtable();
			this.estimatedSize = 0L;
			Organization orgContainer = configurationSession.GetOrgContainer();
			this.hierarchyMailboxInformation = orgContainer.DefaultPublicFolderMailbox;
			this.publicFoldersDeploymentType = orgContainer.PublicFoldersEnabled;
			this.estimatedSize += (long)this.hierarchyMailboxInformation.ItemSize;
			this.HeuristicsFlags = orgContainer.Heuristics;
			this.estimatedSize += 4L;
			this.estimatedSize += 4L;
			ADRawEntry[] array = Array<ADRawEntry>.Empty;
			if (this.hierarchyMailboxInformation.HierarchyMailboxGuid != Guid.Empty)
			{
				array = tenantOrRootOrgRecipientSession.FindPaged<ADRawEntry>(null, QueryScope.SubTree, Filters.GetRecipientTypeDetailsFilterOptimization(RecipientTypeDetails.PublicFolderMailbox), new SortBy(ADObjectSchema.WhenCreatedUTC, SortOrder.Ascending), 0, TenantPublicFolderConfiguration.PublicFolderRecipientProperties).ReadAllPages();
			}
			List<PublicFolderRecipient> list = new List<PublicFolderRecipient>();
			if (this.PublicFoldersDeploymentType == PublicFoldersDeployment.Remote && orgContainer.RemotePublicFolderMailboxes.Count > 0)
			{
				ADObjectId[] array2 = orgContainer.RemotePublicFolderMailboxes.ToArray();
				foreach (ADObjectId adobjectId in array2)
				{
					if (!adobjectId.IsDeleted)
					{
						MiniRecipient miniRecipient = tenantOrRootOrgRecipientSession.ReadMiniRecipient(adobjectId, null);
						if (miniRecipient != null)
						{
							PublicFolderRecipient publicFolderRecipient = new PublicFolderRecipient(miniRecipient.Name, Guid.Empty, null, miniRecipient.PrimarySmtpAddress, miniRecipient.Id, false);
							this.estimatedSize += publicFolderRecipient.ItemSize;
							this.remotePublicFolderRecipients.Add(publicFolderRecipient.ObjectId, publicFolderRecipient);
							list.Add(publicFolderRecipient);
						}
					}
				}
				if (list.Count > 0)
				{
					this.consistentHashSetForRemoteMailboxes = new ConsistentHashSet<PublicFolderRecipient, Guid>(list.ToArray(), 1, 64);
					this.estimatedSize += this.consistentHashSetForRemoteMailboxes.ItemSize;
				}
			}
			list.Clear();
			if (array.Length > 0)
			{
				for (int j = 0; j < array.Length; j++)
				{
					PublicFolderRecipient publicFolderRecipient = new PublicFolderRecipient((string)array[j][ADRecipientSchema.DisplayName], (Guid)array[j][ADMailboxRecipientSchema.ExchangeGuid], (ADObjectId)array[j][ADMailboxRecipientSchema.Database], (SmtpAddress)array[j][ADRecipientSchema.PrimarySmtpAddress], (ADObjectId)array[j][ADObjectSchema.Id], true);
					this.estimatedSize += publicFolderRecipient.ItemSize;
					this.localPublicFolderRecipients.Add(publicFolderRecipient.ObjectId, publicFolderRecipient);
					if (!(bool)array[j][ADRecipientSchema.IsExcludedFromServingHierarchy] && (bool)array[j][ADRecipientSchema.IsHierarchyReady])
					{
						list.Add(publicFolderRecipient);
					}
				}
				if (list.Count > 0)
				{
					this.consistentHashSetForLocalMailboxes = new ConsistentHashSet<PublicFolderRecipient, Guid>(list.ToArray(), 1, 64);
					this.estimatedSize += this.consistentHashSetForLocalMailboxes.ItemSize;
				}
			}
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x00117AAC File Offset: 0x00115CAC
		public PublicFolderRecipient GetLocalMailboxRecipient(Guid mailboxGuid)
		{
			if (mailboxGuid != Guid.Empty)
			{
				foreach (object obj in this.localPublicFolderRecipients)
				{
					PublicFolderRecipient publicFolderRecipient = (PublicFolderRecipient)((DictionaryEntry)obj).Value;
					if (publicFolderRecipient.MailboxGuid == mailboxGuid)
					{
						return publicFolderRecipient;
					}
				}
			}
			return null;
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x00117B30 File Offset: 0x00115D30
		public PublicFolderRecipient GetPublicFolderRecipient(Guid actAsUserMailboxGuid, ADObjectId publicFolderMailboxId)
		{
			if (this.localPublicFolderRecipients.Count == 0 && this.remotePublicFolderRecipients.Count == 0)
			{
				return null;
			}
			if (publicFolderMailboxId != null)
			{
				PublicFolderRecipient publicFolderRecipient = this.GetPublicFolderRecipient(publicFolderMailboxId);
				if (publicFolderRecipient != null)
				{
					return publicFolderRecipient;
				}
			}
			if (this.PublicFoldersDeploymentType == PublicFoldersDeployment.Local)
			{
				if (this.consistentHashSetForLocalMailboxes != null)
				{
					return this.GetPublicFolderRecipient(this.consistentHashSetForLocalMailboxes.GetNearestNeighborSlot(actAsUserMailboxGuid).ObjectId);
				}
			}
			else if (this.PublicFoldersDeploymentType == PublicFoldersDeployment.Remote && this.consistentHashSetForRemoteMailboxes != null)
			{
				return this.GetPublicFolderRecipient(this.consistentHashSetForRemoteMailboxes.GetNearestNeighborSlot(actAsUserMailboxGuid).ObjectId);
			}
			return null;
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x00117BBC File Offset: 0x00115DBC
		public PublicFolderRecipient[] GetAllMailboxRecipients()
		{
			PublicFolderRecipient[] array = new PublicFolderRecipient[this.localPublicFolderRecipients.Count];
			this.localPublicFolderRecipients.Values.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x00117BED File Offset: 0x00115DED
		public PublicFolderInformation GetHierarchyMailboxInformation()
		{
			return this.hierarchyMailboxInformation;
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x00117BF8 File Offset: 0x00115DF8
		public Guid GetHierarchyMailboxGuidForUser(Guid actAsUserMailboxGuid, ADObjectId publicFolderMailboxId)
		{
			if (this.hierarchyMailboxInformation.Type != PublicFolderInformation.HierarchyType.MailboxGuid || this.hierarchyMailboxInformation.HierarchyMailboxGuid == Guid.Empty)
			{
				return Guid.Empty;
			}
			PublicFolderRecipient publicFolderRecipient = this.GetPublicFolderRecipient(actAsUserMailboxGuid, publicFolderMailboxId);
			if (publicFolderRecipient != null && publicFolderRecipient.IsLocal)
			{
				return publicFolderRecipient.MailboxGuid;
			}
			return Guid.Empty;
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x00117C50 File Offset: 0x00115E50
		public Guid[] GetContentMailboxGuids()
		{
			List<Guid> list = new List<Guid>();
			if (this.localPublicFolderRecipients.Count > 1)
			{
				foreach (object obj in this.localPublicFolderRecipients)
				{
					PublicFolderRecipient publicFolderRecipient = (PublicFolderRecipient)((DictionaryEntry)obj).Value;
					if (publicFolderRecipient.MailboxGuid != this.hierarchyMailboxInformation.HierarchyMailboxGuid)
					{
						list.Add(publicFolderRecipient.MailboxGuid);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x00117CF0 File Offset: 0x00115EF0
		private PublicFolderRecipient GetPublicFolderRecipient(ADObjectId objectId)
		{
			return (PublicFolderRecipient)(this.localPublicFolderRecipients[objectId] ?? this.remotePublicFolderRecipients[objectId]);
		}

		// Token: 0x040033F3 RID: 13299
		private const int consistentHashingReplicaCount = 1;

		// Token: 0x040033F4 RID: 13300
		private const int numberOfNeighborVisit = 64;

		// Token: 0x040033F5 RID: 13301
		private static readonly PropertyDefinition[] PublicFolderRecipientProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.IsExcludedFromServingHierarchy,
			ADRecipientSchema.IsHierarchyReady,
			ADRecipientSchema.PrimarySmtpAddress,
			ADMailboxRecipientSchema.ExchangeGuid,
			ADMailboxRecipientSchema.Database
		};

		// Token: 0x040033F6 RID: 13302
		private PublicFolderInformation hierarchyMailboxInformation;

		// Token: 0x040033F7 RID: 13303
		private Hashtable localPublicFolderRecipients;

		// Token: 0x040033F8 RID: 13304
		private Hashtable remotePublicFolderRecipients;

		// Token: 0x040033F9 RID: 13305
		private ConsistentHashSet<PublicFolderRecipient, Guid> consistentHashSetForLocalMailboxes;

		// Token: 0x040033FA RID: 13306
		private ConsistentHashSet<PublicFolderRecipient, Guid> consistentHashSetForRemoteMailboxes;

		// Token: 0x040033FB RID: 13307
		private long estimatedSize;

		// Token: 0x040033FC RID: 13308
		private PublicFoldersDeployment publicFoldersDeploymentType;
	}
}

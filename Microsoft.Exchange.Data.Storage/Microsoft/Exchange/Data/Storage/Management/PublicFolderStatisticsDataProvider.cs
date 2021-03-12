using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A71 RID: 2673
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderStatisticsDataProvider : DisposeTrackableBase, IConfigDataProvider
	{
		// Token: 0x17001AF6 RID: 6902
		// (get) Token: 0x060061AD RID: 25005 RVA: 0x0019CEF4 File Offset: 0x0019B0F4
		public PublicFolderDataProvider PublicFolderDataProvider
		{
			get
			{
				return this.publicFolderDataProvider;
			}
		}

		// Token: 0x17001AF7 RID: 6903
		// (get) Token: 0x060061AE RID: 25006 RVA: 0x0019CEFC File Offset: 0x0019B0FC
		public OrganizationId CurrentOrganizationId
		{
			get
			{
				if (this.PublicFolderDataProvider == null)
				{
					return null;
				}
				return this.PublicFolderDataProvider.CurrentOrganizationId;
			}
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x0019CF14 File Offset: 0x0019B114
		public PublicFolderStatisticsDataProvider(IConfigurationSession configurationSession, string action, Guid mailboxGuid)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.publicFolderDataProvider = new PublicFolderDataProvider(configurationSession, action, mailboxGuid);
				this.mailboxGuid = mailboxGuid;
				disposeGuard.Success();
			}
		}

		// Token: 0x060061B0 RID: 25008 RVA: 0x0019CF78 File Offset: 0x0019B178
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PublicFolderStatisticsDataProvider>(this);
		}

		// Token: 0x060061B1 RID: 25009 RVA: 0x0019CF80 File Offset: 0x0019B180
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			base.CheckDisposed();
			IConfigurable[] array = this.Find<T>(new FalseFilter(), identity, true, null);
			if (array != null && array.Length != 0)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x0019CFAF File Offset: 0x0019B1AF
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			base.CheckDisposed();
			return (IConfigurable[])this.FindPaged<T>(filter, rootId, deepSearch, sortBy, 0).ToArray<T>();
		}

		// Token: 0x060061B3 RID: 25011 RVA: 0x0019D4B0 File Offset: 0x0019B6B0
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			base.CheckDisposed();
			if (!typeof(PublicFolderStatistics).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
			{
				throw new NotSupportedException("FindPaged: " + typeof(T).FullName);
			}
			IEnumerable<PublicFolder> publicFolders = this.publicFolderDataProvider.FindPaged<PublicFolder>(filter, rootId, deepSearch, sortBy, pageSize);
			foreach (PublicFolder publicFolder in publicFolders)
			{
				PublicFolderSession contentSession = null;
				if (this.mailboxGuid == Guid.Empty)
				{
					contentSession = this.PublicFolderDataProvider.PublicFolderSessionCache.GetPublicFolderSession(publicFolder.InternalFolderIdentity.ObjectId);
				}
				else
				{
					contentSession = this.PublicFolderDataProvider.PublicFolderSessionCache.GetPublicFolderSession(this.mailboxGuid);
				}
				using (Folder contentFolder = Folder.Bind(contentSession, publicFolder.InternalFolderIdentity, PublicFolderStatisticsDataProvider.contentFolderProperties))
				{
					PublicFolderStatistics publicFolderStatistics = new PublicFolderStatistics();
					publicFolderStatistics.LoadDataFromXso(contentSession.MailboxPrincipal.ObjectId, contentFolder);
					uint ownerCount = 0U;
					uint contactCount = 0U;
					PermissionSet folderPermissionSet = contentFolder.GetPermissionSet();
					foreach (Permission permission in folderPermissionSet)
					{
						if (permission.IsFolderContact)
						{
							contactCount += 1U;
						}
						if (permission.IsFolderOwner)
						{
							ownerCount += 1U;
						}
					}
					publicFolderStatistics.OwnerCount = ownerCount;
					publicFolderStatistics.ContactCount = contactCount;
					StoreObjectId dumpsterId = PublicFolderCOWSession.GetRecoverableItemsDeletionsFolderId((CoreFolder)contentFolder.CoreObject);
					checked
					{
						if (dumpsterId != null)
						{
							try
							{
								using (CoreFolder coreFolder = CoreFolder.Bind(contentSession, dumpsterId, PublicFolderStatisticsDataProvider.dumpsterProperties))
								{
									publicFolderStatistics.DeletedItemCount = (uint)((int)coreFolder.PropertyBag[FolderSchema.ItemCount]);
									publicFolderStatistics.TotalDeletedItemSize = ByteQuantifiedSize.FromBytes((ulong)((long)coreFolder.PropertyBag[FolderSchema.ExtendedSize]));
								}
							}
							catch (ObjectNotFoundException)
							{
							}
						}
						yield return (T)((object)publicFolderStatistics);
					}
				}
			}
			yield break;
		}

		// Token: 0x060061B4 RID: 25012 RVA: 0x0019D4F2 File Offset: 0x0019B6F2
		public void Save(IConfigurable instance)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060061B5 RID: 25013 RVA: 0x0019D4F9 File Offset: 0x0019B6F9
		public void Delete(IConfigurable instance)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17001AF8 RID: 6904
		// (get) Token: 0x060061B6 RID: 25014 RVA: 0x0019D500 File Offset: 0x0019B700
		public string Source
		{
			get
			{
				return ((IConfigDataProvider)this.publicFolderDataProvider).Source;
			}
		}

		// Token: 0x060061B7 RID: 25015 RVA: 0x0019D50D File Offset: 0x0019B70D
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.publicFolderDataProvider != null)
			{
				this.publicFolderDataProvider.Dispose();
				this.publicFolderDataProvider = null;
			}
		}

		// Token: 0x04003772 RID: 14194
		private readonly Guid mailboxGuid = Guid.Empty;

		// Token: 0x04003773 RID: 14195
		private static ICollection<PropertyDefinition> contentFolderProperties = InternalSchema.Combine<PropertyDefinition>(FolderSchema.Instance.AutoloadProperties, PublicFolderStatistics.InternalSchema.AllDependentXsoProperties);

		// Token: 0x04003774 RID: 14196
		private static StorePropertyDefinition[] dumpsterProperties = new StorePropertyDefinition[]
		{
			FolderSchema.ItemCount,
			FolderSchema.ExtendedSize
		};

		// Token: 0x04003775 RID: 14197
		private PublicFolderDataProvider publicFolderDataProvider;
	}
}

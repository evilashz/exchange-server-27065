using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A67 RID: 2663
	[Serializable]
	public class MailboxFolder : XsoMailboxConfigurationObject
	{
		// Token: 0x17001AB7 RID: 6839
		// (get) Token: 0x06006122 RID: 24866 RVA: 0x0019A84A File Offset: 0x00198A4A
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return MailboxFolder.schema;
			}
		}

		// Token: 0x06006123 RID: 24867 RVA: 0x0019A854 File Offset: 0x00198A54
		internal static object IdentityGetter(IPropertyBag propertyBag)
		{
			ADObjectId mailboxOwnerId = (ADObjectId)propertyBag[XsoMailboxConfigurationObjectSchema.MailboxOwnerId];
			VersionedId versionedId = (VersionedId)propertyBag[MailboxFolderSchema.InternalFolderIdentity];
			MapiFolderPath mapiFolderPath = (MapiFolderPath)propertyBag[MailboxFolderSchema.FolderPath];
			if (null != mapiFolderPath || versionedId != null)
			{
				return new MailboxFolderId(mailboxOwnerId, (versionedId == null) ? null : versionedId.ObjectId, mapiFolderPath);
			}
			return null;
		}

		// Token: 0x06006124 RID: 24868 RVA: 0x0019A8B8 File Offset: 0x00198AB8
		internal static object ParentFolderGetter(IPropertyBag propertyBag)
		{
			ADObjectId mailboxOwnerId = (ADObjectId)propertyBag[XsoMailboxConfigurationObjectSchema.MailboxOwnerId];
			VersionedId versionedId = (VersionedId)propertyBag[MailboxFolderSchema.InternalFolderIdentity];
			StoreObjectId storeObjectId = (StoreObjectId)propertyBag[MailboxFolderSchema.InternalParentFolderIdentity];
			MapiFolderPath mapiFolderPath = (MapiFolderPath)propertyBag[MailboxFolderSchema.FolderPath];
			if (versionedId != null && versionedId != null && object.Equals(versionedId.ObjectId, storeObjectId))
			{
				return null;
			}
			if ((null != mapiFolderPath && null != mapiFolderPath.Parent) || storeObjectId != null)
			{
				return new MailboxFolderId(mailboxOwnerId, storeObjectId, (null == mapiFolderPath) ? null : mapiFolderPath.Parent);
			}
			return null;
		}

		// Token: 0x06006125 RID: 24869 RVA: 0x0019A954 File Offset: 0x00198B54
		internal static object FolderStoreObjectIdGetter(IPropertyBag propertyBag)
		{
			VersionedId versionedId = (VersionedId)propertyBag[MailboxFolderSchema.InternalFolderIdentity];
			if (versionedId != null && versionedId.ObjectId != null)
			{
				return versionedId.ObjectId.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06006126 RID: 24870 RVA: 0x0019A98E File Offset: 0x00198B8E
		internal void SetDefaultFolderType(DefaultFolderType defaultFolderType)
		{
			this[MailboxFolderSchema.DefaultFolderType] = new DefaultFolderType?(defaultFolderType);
		}

		// Token: 0x17001AB8 RID: 6840
		// (get) Token: 0x06006127 RID: 24871 RVA: 0x0019A9A6 File Offset: 0x00198BA6
		// (set) Token: 0x06006128 RID: 24872 RVA: 0x0019A9B8 File Offset: 0x00198BB8
		[ValidateNotNullOrEmpty]
		public string Name
		{
			get
			{
				return (string)this[MailboxFolderSchema.Name];
			}
			set
			{
				this[MailboxFolderSchema.Name] = value;
			}
		}

		// Token: 0x17001AB9 RID: 6841
		// (get) Token: 0x06006129 RID: 24873 RVA: 0x0019A9C6 File Offset: 0x00198BC6
		public override ObjectId Identity
		{
			get
			{
				return (MailboxFolderId)this[MailboxFolderSchema.Identity];
			}
		}

		// Token: 0x17001ABA RID: 6842
		// (get) Token: 0x0600612A RID: 24874 RVA: 0x0019A9D8 File Offset: 0x00198BD8
		public MailboxFolderId ParentFolder
		{
			get
			{
				return (MailboxFolderId)this[MailboxFolderSchema.ParentFolder];
			}
		}

		// Token: 0x17001ABB RID: 6843
		// (get) Token: 0x0600612B RID: 24875 RVA: 0x0019A9EA File Offset: 0x00198BEA
		internal VersionedId InternalFolderIdentity
		{
			get
			{
				return (VersionedId)this[MailboxFolderSchema.InternalFolderIdentity];
			}
		}

		// Token: 0x17001ABC RID: 6844
		// (get) Token: 0x0600612C RID: 24876 RVA: 0x0019A9FC File Offset: 0x00198BFC
		public string FolderStoreObjectId
		{
			get
			{
				return (string)this[MailboxFolderSchema.FolderStoreObjectId];
			}
		}

		// Token: 0x17001ABD RID: 6845
		// (get) Token: 0x0600612D RID: 24877 RVA: 0x0019AA0E File Offset: 0x00198C0E
		internal StoreObjectId InternalParentFolderIdentity
		{
			get
			{
				return (StoreObjectId)this[MailboxFolderSchema.InternalParentFolderIdentity];
			}
		}

		// Token: 0x17001ABE RID: 6846
		// (get) Token: 0x0600612E RID: 24878 RVA: 0x0019AA20 File Offset: 0x00198C20
		public long? FolderSize
		{
			get
			{
				return (long?)this[MailboxFolderSchema.FolderSize];
			}
		}

		// Token: 0x17001ABF RID: 6847
		// (get) Token: 0x0600612F RID: 24879 RVA: 0x0019AA32 File Offset: 0x00198C32
		public bool? HasSubfolders
		{
			get
			{
				return (bool?)this[MailboxFolderSchema.HasSubfolders];
			}
		}

		// Token: 0x17001AC0 RID: 6848
		// (get) Token: 0x06006130 RID: 24880 RVA: 0x0019AA44 File Offset: 0x00198C44
		// (set) Token: 0x06006131 RID: 24881 RVA: 0x0019AA56 File Offset: 0x00198C56
		public string FolderClass
		{
			get
			{
				return (string)this[MailboxFolderSchema.FolderClass];
			}
			internal set
			{
				this[MailboxFolderSchema.FolderClass] = value;
			}
		}

		// Token: 0x17001AC1 RID: 6849
		// (get) Token: 0x06006132 RID: 24882 RVA: 0x0019AA64 File Offset: 0x00198C64
		// (set) Token: 0x06006133 RID: 24883 RVA: 0x0019AA76 File Offset: 0x00198C76
		public MapiFolderPath FolderPath
		{
			get
			{
				return (MapiFolderPath)this[MailboxFolderSchema.FolderPath];
			}
			internal set
			{
				this[MailboxFolderSchema.FolderPath] = value;
			}
		}

		// Token: 0x17001AC2 RID: 6850
		// (get) Token: 0x06006134 RID: 24884 RVA: 0x0019AA84 File Offset: 0x00198C84
		public DefaultFolderType? DefaultFolderType
		{
			get
			{
				return (DefaultFolderType?)this[MailboxFolderSchema.DefaultFolderType];
			}
		}

		// Token: 0x17001AC3 RID: 6851
		// (get) Token: 0x06006135 RID: 24885 RVA: 0x0019AA96 File Offset: 0x00198C96
		public ExtendedFolderFlags? ExtendedFolderFlags
		{
			get
			{
				return (ExtendedFolderFlags?)this[MailboxFolderSchema.ExtendedFolderFlags];
			}
		}

		// Token: 0x06006136 RID: 24886 RVA: 0x0019AAA8 File Offset: 0x00198CA8
		public override string ToString()
		{
			if (this.Name != null)
			{
				return this.Name;
			}
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return base.ToString();
		}

		// Token: 0x04003744 RID: 14148
		private static MailboxFolderSchema schema = ObjectSchema.GetInstance<MailboxFolderSchema>();
	}
}

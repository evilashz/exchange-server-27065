using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x02000CC9 RID: 3273
	[Serializable]
	public class PublicFolderClientPermissionEntry : IConfigurable
	{
		// Token: 0x06007E26 RID: 32294 RVA: 0x00203CA2 File Offset: 0x00201EA2
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17002739 RID: 10041
		// (get) Token: 0x06007E27 RID: 32295 RVA: 0x00203CA9 File Offset: 0x00201EA9
		public ObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x1700273A RID: 10042
		// (get) Token: 0x06007E28 RID: 32296 RVA: 0x00203CB1 File Offset: 0x00201EB1
		bool IConfigurable.IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700273B RID: 10043
		// (get) Token: 0x06007E29 RID: 32297 RVA: 0x00203CB4 File Offset: 0x00201EB4
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x06007E2A RID: 32298 RVA: 0x00203CB7 File Offset: 0x00201EB7
		void IConfigurable.ResetChangeTracking()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007E2B RID: 32299 RVA: 0x00203CBE File Offset: 0x00201EBE
		ValidationError[] IConfigurable.Validate()
		{
			return ValidationError.None;
		}

		// Token: 0x1700273C RID: 10044
		// (get) Token: 0x06007E2C RID: 32300 RVA: 0x00203CC5 File Offset: 0x00201EC5
		public PublicFolderUserId User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x06007E2D RID: 32301 RVA: 0x00203CCD File Offset: 0x00201ECD
		public PublicFolderClientPermissionEntry()
		{
		}

		// Token: 0x06007E2E RID: 32302 RVA: 0x00203CD5 File Offset: 0x00201ED5
		public PublicFolderClientPermissionEntry(PublicFolderId identity, PublicFolderUserId user)
		{
			if (null == identity)
			{
				throw new ArgumentNullException("Identity");
			}
			if (null == user)
			{
				throw new ArgumentNullException("user");
			}
			this.identity = identity;
			this.user = user;
		}

		// Token: 0x1700273D RID: 10045
		// (get) Token: 0x06007E2F RID: 32303 RVA: 0x00203D13 File Offset: 0x00201F13
		// (set) Token: 0x06007E30 RID: 32304 RVA: 0x00203D2E File Offset: 0x00201F2E
		public MultiValuedProperty<PublicFolderAccessRight> AccessRights
		{
			get
			{
				if (this.accessRights != null)
				{
					return new MultiValuedProperty<PublicFolderAccessRight>(this.accessRights);
				}
				return new MultiValuedProperty<PublicFolderAccessRight>();
			}
			set
			{
				PublicFolderClientPermissionEntry.ValidateAccessRights(value);
				this.accessRights = value;
			}
		}

		// Token: 0x06007E31 RID: 32305 RVA: 0x00203D40 File Offset: 0x00201F40
		internal static void ValidateAccessRights(MultiValuedProperty<PublicFolderAccessRight> accessRights)
		{
			if (accessRights != null)
			{
				bool flag = false;
				bool flag2 = false;
				foreach (PublicFolderAccessRight publicFolderAccessRight in accessRights)
				{
					if (publicFolderAccessRight.IsRole)
					{
						if (flag2)
						{
							throw new ArgumentException(Strings.ErrorPrecannedRoleAndSpecificPermission);
						}
						flag = true;
					}
					else
					{
						if (flag)
						{
							throw new ArgumentException(Strings.ErrorPrecannedRoleAndSpecificPermission);
						}
						flag2 = true;
					}
				}
			}
		}

		// Token: 0x04003E1A RID: 15898
		private PublicFolderId identity;

		// Token: 0x04003E1B RID: 15899
		private PublicFolderUserId user;

		// Token: 0x04003E1C RID: 15900
		private MultiValuedProperty<PublicFolderAccessRight> accessRights;
	}
}

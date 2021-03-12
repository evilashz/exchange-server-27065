using System;
using System.DirectoryServices;
using System.Security.AccessControl;

namespace Microsoft.Exchange.Data.Directory.Permission
{
	// Token: 0x020001D7 RID: 471
	[Serializable]
	public class RecipientPermission : ConfigurableObject
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0005B79E File Offset: 0x0005999E
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return RecipientPermission.schema;
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0005B7A5 File Offset: 0x000599A5
		public RecipientPermission() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0005B7B4 File Offset: 0x000599B4
		public RecipientPermission(ActiveDirectoryAccessRule ace, ADObjectId identity, string trustee) : this()
		{
			if (ace == null)
			{
				throw new ArgumentNullException("ace");
			}
			this.Identity = identity;
			this.Trustee = trustee;
			this.AccessControlType = ace.AccessControlType;
			this.IsInherited = ace.IsInherited;
			this.InheritanceType = ace.InheritanceType;
			this.AccessRights = new MultiValuedProperty<RecipientAccessRight>(RecipientPermissionHelper.GetRecipientAccessRight(ace).Value);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0005B828 File Offset: 0x00059A28
		public RecipientPermission(ActiveDirectoryAccessRule ace, ADObjectId identity, string trustee, RecipientAccessRight accessRight) : this()
		{
			if (ace == null)
			{
				throw new ArgumentNullException("ace");
			}
			this.Identity = identity;
			this.Trustee = trustee;
			this.AccessControlType = ace.AccessControlType;
			this.AccessRights = new MultiValuedProperty<RecipientAccessRight>(accessRight);
			this.IsInherited = ace.IsInherited;
			this.InheritanceType = ace.InheritanceType;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0005B88D File Offset: 0x00059A8D
		// (set) Token: 0x06001308 RID: 4872 RVA: 0x0005B895 File Offset: 0x00059A95
		public new ObjectId Identity
		{
			get
			{
				return base.Identity;
			}
			private set
			{
				this[SimpleProviderObjectSchema.Identity] = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x0005B8A3 File Offset: 0x00059AA3
		// (set) Token: 0x0600130A RID: 4874 RVA: 0x0005B8B5 File Offset: 0x00059AB5
		public string Trustee
		{
			get
			{
				return (string)this[RecipientPermissionSchema.Trustee];
			}
			private set
			{
				this[RecipientPermissionSchema.Trustee] = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x0005B8C3 File Offset: 0x00059AC3
		// (set) Token: 0x0600130C RID: 4876 RVA: 0x0005B8D5 File Offset: 0x00059AD5
		public AccessControlType AccessControlType
		{
			get
			{
				return (AccessControlType)this[RecipientPermissionSchema.AccessControlType];
			}
			private set
			{
				this[RecipientPermissionSchema.AccessControlType] = value;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x0005B8E8 File Offset: 0x00059AE8
		// (set) Token: 0x0600130E RID: 4878 RVA: 0x0005B8FA File Offset: 0x00059AFA
		public MultiValuedProperty<RecipientAccessRight> AccessRights
		{
			get
			{
				return (MultiValuedProperty<RecipientAccessRight>)this[RecipientPermissionSchema.AccessRights];
			}
			private set
			{
				this[RecipientPermissionSchema.AccessRights] = value;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x0005B908 File Offset: 0x00059B08
		// (set) Token: 0x06001310 RID: 4880 RVA: 0x0005B91A File Offset: 0x00059B1A
		public bool IsInherited
		{
			get
			{
				return (bool)this[RecipientPermissionSchema.IsInherited];
			}
			private set
			{
				this[RecipientPermissionSchema.IsInherited] = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x0005B92D File Offset: 0x00059B2D
		// (set) Token: 0x06001312 RID: 4882 RVA: 0x0005B93F File Offset: 0x00059B3F
		public ActiveDirectorySecurityInheritance InheritanceType
		{
			get
			{
				return (ActiveDirectorySecurityInheritance)this[RecipientPermissionSchema.InheritanceType];
			}
			private set
			{
				this[RecipientPermissionSchema.InheritanceType] = value;
			}
		}

		// Token: 0x04000AE5 RID: 2789
		private static readonly ObjectSchema schema = ObjectSchema.GetInstance<RecipientPermissionSchema>();
	}
}

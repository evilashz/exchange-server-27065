using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002A4 RID: 676
	[Serializable]
	public abstract class ADConfigurationObject : ADObject
	{
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x0008BB80 File Offset: 0x00089D80
		internal IConfigurationSession Session
		{
			get
			{
				return (IConfigurationSession)this.m_Session;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x0008BB8D File Offset: 0x00089D8D
		internal virtual ADObjectId ParentPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0008BB90 File Offset: 0x00089D90
		internal void SetId(IConfigurationSession session, ADObjectId parent, string cn)
		{
			if (string.IsNullOrEmpty(cn))
			{
				throw new ArgumentException(DirectoryStrings.ErrorEmptyString("cn"), "cn");
			}
			ADObjectId adobjectId = session.GetOrgContainerId();
			if (this.ParentPath != null && !string.IsNullOrEmpty(this.ParentPath.DistinguishedName))
			{
				adobjectId = adobjectId.GetDescendantId(this.ParentPath);
			}
			if (parent != null && !string.IsNullOrEmpty(parent.DistinguishedName))
			{
				adobjectId = adobjectId.GetDescendantId(parent);
			}
			base.SetId(adobjectId.GetChildId(cn));
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0008BC12 File Offset: 0x00089E12
		internal void SetId(IConfigurationSession session, string cn)
		{
			this.SetId(session, null, cn);
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x0008BC1D File Offset: 0x00089E1D
		// (set) Token: 0x06001F6E RID: 8046 RVA: 0x0008BC2F File Offset: 0x00089E2F
		public string AdminDisplayName
		{
			get
			{
				return (string)this[ADConfigurationObjectSchema.AdminDisplayName];
			}
			internal set
			{
				this[ADConfigurationObjectSchema.AdminDisplayName] = value;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x0008BC3D File Offset: 0x00089E3D
		internal virtual SystemFlagsEnum SystemFlags
		{
			get
			{
				return (SystemFlagsEnum)this[ADConfigurationObjectSchema.SystemFlags];
			}
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0008BC50 File Offset: 0x00089E50
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (base.IsChanged(ADObjectSchema.Id) || (base.DistinguishedName != null && base.OriginalId != null && !base.DistinguishedName.Equals(base.OriginalId.DistinguishedName)))
			{
				int systemFlags = (int)this.SystemFlags;
				bool flag = 0 != (systemFlags & 1073741824);
				bool flag2 = 0 != (systemFlags & 536870912);
				bool flag3 = 0 != (systemFlags & 268435456);
				bool flag4 = !base.Id.Parent.Equals(base.OriginalId.Parent);
				bool flag5 = !base.Id.Rdn.UnescapedName.Equals(base.OriginalId.Rdn.UnescapedName, StringComparison.OrdinalIgnoreCase);
				if (flag4 && !flag2 && !flag3)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.MoveNotAllowed, this.Identity, string.Empty));
					return;
				}
				if (flag5 && !flag)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.RenameNotAllowed, this.Identity, string.Empty));
					return;
				}
				if (flag4 && !flag2)
				{
					int depth = base.OriginalId.Depth;
					int depth2 = base.Id.Depth;
					if (depth != depth2 || depth < 2 || depth2 < 2)
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.LimitedMoveOnlyAllowed, this.Identity, string.Empty));
					}
					if (!base.Id.Parent.Parent.Equals(base.OriginalId.Parent.Parent))
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.LimitedMoveOnlyAllowed, this.Identity, string.Empty));
					}
				}
			}
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0008BDFB File Offset: 0x00089FFB
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(ADConfigurationObjectSchema.SystemFlags))
			{
				this[ADConfigurationObjectSchema.SystemFlags] = SystemFlagsEnum.Renamable;
			}
			base.StampPersistableDefaultValues();
		}
	}
}

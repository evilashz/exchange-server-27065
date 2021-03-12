using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000110 RID: 272
	internal sealed class PermissionInformation : PermissionInformationBase<PermissionType>
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x000264B8 File Offset: 0x000246B8
		public PermissionInformation()
		{
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x000264C0 File Offset: 0x000246C0
		public PermissionInformation(PermissionType permissionType) : base(permissionType)
		{
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x000264C9 File Offset: 0x000246C9
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x000264D6 File Offset: 0x000246D6
		internal PermissionLevel PermissionLevel
		{
			get
			{
				return (PermissionLevel)base.PermissionElement.PermissionLevel;
			}
			set
			{
				base.PermissionElement.PermissionLevel = (PermissionLevelType)value;
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000264E4 File Offset: 0x000246E4
		internal override bool DoAnyNonPermissionLevelFieldsHaveValue()
		{
			return base.DoAnyNonPermissionLevelFieldsHaveValue();
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000264EC File Offset: 0x000246EC
		protected override PermissionLevel GetPermissionLevelToSet()
		{
			return this.PermissionLevel;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000264F4 File Offset: 0x000246F4
		internal override bool IsNonCustomPermissionLevelSet()
		{
			return this.PermissionLevel != PermissionLevel.Custom;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00026503 File Offset: 0x00024703
		protected override void SetByTypePermissionFieldsOntoPermission(Permission permission)
		{
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00026505 File Offset: 0x00024705
		protected override PermissionType CreateDefaultBasePermissionType()
		{
			return new PermissionType();
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0002650C File Offset: 0x0002470C
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x0002655C File Offset: 0x0002475C
		internal override bool? CanReadItems
		{
			get
			{
				base.EnsurePermissionElementIsNotNull();
				if (base.PermissionElement.ReadItems != null)
				{
					return new bool?(base.PermissionElement.ReadItems.Value == PermissionReadAccess.FullDetails);
				}
				return null;
			}
			set
			{
				base.EnsurePermissionElementIsNotNull();
				if (value == null)
				{
					base.PermissionElement.ReadItems = null;
					return;
				}
				if (value.Value)
				{
					base.PermissionElement.ReadItems = new PermissionReadAccess?(PermissionReadAccess.FullDetails);
					return;
				}
				base.PermissionElement.ReadItems = new PermissionReadAccess?(PermissionReadAccess.None);
			}
		}
	}
}

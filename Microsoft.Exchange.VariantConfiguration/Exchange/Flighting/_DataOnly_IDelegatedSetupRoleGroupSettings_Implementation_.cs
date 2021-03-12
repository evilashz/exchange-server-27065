using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x02000044 RID: 68
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IDelegatedSetupRoleGroupSettings_Implementation_ : IDelegatedSetupRoleGroupSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000466A File Offset: 0x0000286A
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000466D File Offset: 0x0000286D
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00004670 File Offset: 0x00002870
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00004678 File Offset: 0x00002878
		public DelegatedSetupRoleGroupValueEnum DelegatedSetupRoleGroupValue
		{
			get
			{
				return this._DelegatedSetupRoleGroupValue_MaterializedValue_;
			}
		}

		// Token: 0x0400011C RID: 284
		internal string _Name_MaterializedValue_;

		// Token: 0x0400011D RID: 285
		internal DelegatedSetupRoleGroupValueEnum _DelegatedSetupRoleGroupValue_MaterializedValue_;
	}
}

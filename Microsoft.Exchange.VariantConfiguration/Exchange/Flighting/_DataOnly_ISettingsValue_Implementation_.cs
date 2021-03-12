using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x020000A0 RID: 160
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_ISettingsValue_Implementation_ : ISettingsValue, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00006C7B File Offset: 0x00004E7B
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00006C7E File Offset: 0x00004E7E
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00006C81 File Offset: 0x00004E81
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00006C89 File Offset: 0x00004E89
		public string Value
		{
			get
			{
				return this._Value_MaterializedValue_;
			}
		}

		// Token: 0x040002FB RID: 763
		internal string _Name_MaterializedValue_;

		// Token: 0x040002FC RID: 764
		internal string _Value_MaterializedValue_;
	}
}

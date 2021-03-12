using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x020000A3 RID: 163
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_ISettings_Implementation_ : ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00006CD6 File Offset: 0x00004ED6
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00006CD9 File Offset: 0x00004ED9
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00006CDC File Offset: 0x00004EDC
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x04000300 RID: 768
		internal string _Name_MaterializedValue_;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x020000B7 RID: 183
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IUrl_Implementation_ : IUrl, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000070B7 File Offset: 0x000052B7
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000070BA File Offset: 0x000052BA
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x000070BD File Offset: 0x000052BD
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x000070C5 File Offset: 0x000052C5
		public string Url
		{
			get
			{
				return this._Url_MaterializedValue_;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x000070CD File Offset: 0x000052CD
		public bool Enabled
		{
			get
			{
				return this._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x04000330 RID: 816
		internal string _Name_MaterializedValue_;

		// Token: 0x04000331 RID: 817
		internal string _Url_MaterializedValue_;

		// Token: 0x04000332 RID: 818
		internal bool _Enabled_MaterializedValue_;
	}
}

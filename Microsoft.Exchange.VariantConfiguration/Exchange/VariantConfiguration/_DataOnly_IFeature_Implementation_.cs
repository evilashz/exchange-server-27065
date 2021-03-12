using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000054 RID: 84
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IFeature_Implementation_ : IFeature, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000049DE File Offset: 0x00002BDE
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000049E1 File Offset: 0x00002BE1
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000049E4 File Offset: 0x00002BE4
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000049EC File Offset: 0x00002BEC
		public bool Enabled
		{
			get
			{
				return this._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x04000144 RID: 324
		internal string _Name_MaterializedValue_;

		// Token: 0x04000145 RID: 325
		internal bool _Enabled_MaterializedValue_;
	}
}

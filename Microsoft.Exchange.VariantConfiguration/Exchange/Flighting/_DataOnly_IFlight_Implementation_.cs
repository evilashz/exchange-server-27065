using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x0200005C RID: 92
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_IFlight_Implementation_ : IFlight, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00004B89 File Offset: 0x00002D89
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00004B8C File Offset: 0x00002D8C
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00004B8F File Offset: 0x00002D8F
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00004B97 File Offset: 0x00002D97
		public bool Enabled
		{
			get
			{
				return this._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00004B9F File Offset: 0x00002D9F
		public string Rotate
		{
			get
			{
				return this._Rotate_MaterializedValue_;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00004BA7 File Offset: 0x00002DA7
		public string Ramp
		{
			get
			{
				return this._Ramp_MaterializedValue_;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00004BAF File Offset: 0x00002DAF
		public string RampSeed
		{
			get
			{
				return this._RampSeed_MaterializedValue_;
			}
		}

		// Token: 0x04000158 RID: 344
		internal string _Name_MaterializedValue_;

		// Token: 0x04000159 RID: 345
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x0400015A RID: 346
		internal string _Rotate_MaterializedValue_;

		// Token: 0x0400015B RID: 347
		internal string _Ramp_MaterializedValue_;

		// Token: 0x0400015C RID: 348
		internal string _RampSeed_MaterializedValue_;
	}
}

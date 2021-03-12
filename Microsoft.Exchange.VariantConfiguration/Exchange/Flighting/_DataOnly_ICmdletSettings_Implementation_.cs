using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x0200003C RID: 60
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_ICmdletSettings_Implementation_ : ICmdletSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00004488 File Offset: 0x00002688
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000448B File Offset: 0x0000268B
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000448E File Offset: 0x0000268E
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00004496 File Offset: 0x00002696
		public bool Enabled
		{
			get
			{
				return this._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000449E File Offset: 0x0000269E
		public IList<string> AllFlightingParams
		{
			get
			{
				return this._AllFlightingParams_MaterializedValue_;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000044A6 File Offset: 0x000026A6
		public IList<string> Params0
		{
			get
			{
				return this._Params0_MaterializedValue_;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000044AE File Offset: 0x000026AE
		public IList<string> Params1
		{
			get
			{
				return this._Params1_MaterializedValue_;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000044B6 File Offset: 0x000026B6
		public IList<string> Params2
		{
			get
			{
				return this._Params2_MaterializedValue_;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000044BE File Offset: 0x000026BE
		public IList<string> Params3
		{
			get
			{
				return this._Params3_MaterializedValue_;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000044C6 File Offset: 0x000026C6
		public IList<string> Params4
		{
			get
			{
				return this._Params4_MaterializedValue_;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000044CE File Offset: 0x000026CE
		public IList<string> Params5
		{
			get
			{
				return this._Params5_MaterializedValue_;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000044D6 File Offset: 0x000026D6
		public IList<string> Params6
		{
			get
			{
				return this._Params6_MaterializedValue_;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000044DE File Offset: 0x000026DE
		public IList<string> Params7
		{
			get
			{
				return this._Params7_MaterializedValue_;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000044E6 File Offset: 0x000026E6
		public IList<string> Params8
		{
			get
			{
				return this._Params8_MaterializedValue_;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000044EE File Offset: 0x000026EE
		public IList<string> Params9
		{
			get
			{
				return this._Params9_MaterializedValue_;
			}
		}

		// Token: 0x040000FD RID: 253
		internal string _Name_MaterializedValue_;

		// Token: 0x040000FE RID: 254
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x040000FF RID: 255
		internal IList<string> _AllFlightingParams_MaterializedValue_;

		// Token: 0x04000100 RID: 256
		internal IList<string> _Params0_MaterializedValue_;

		// Token: 0x04000101 RID: 257
		internal IList<string> _Params1_MaterializedValue_;

		// Token: 0x04000102 RID: 258
		internal IList<string> _Params2_MaterializedValue_;

		// Token: 0x04000103 RID: 259
		internal IList<string> _Params3_MaterializedValue_;

		// Token: 0x04000104 RID: 260
		internal IList<string> _Params4_MaterializedValue_;

		// Token: 0x04000105 RID: 261
		internal IList<string> _Params5_MaterializedValue_;

		// Token: 0x04000106 RID: 262
		internal IList<string> _Params6_MaterializedValue_;

		// Token: 0x04000107 RID: 263
		internal IList<string> _Params7_MaterializedValue_;

		// Token: 0x04000108 RID: 264
		internal IList<string> _Params8_MaterializedValue_;

		// Token: 0x04000109 RID: 265
		internal IList<string> _Params9_MaterializedValue_;
	}
}

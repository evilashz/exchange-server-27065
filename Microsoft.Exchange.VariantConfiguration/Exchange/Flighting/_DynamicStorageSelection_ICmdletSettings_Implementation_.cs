using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x0200003B RID: 59
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_ICmdletSettings_Implementation_ : ICmdletSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_ICmdletSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004207 File Offset: 0x00002407
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000420F File Offset: 0x0000240F
		_DynamicStorageSelection_ICmdletSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_ICmdletSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004217 File Offset: 0x00002417
		void IDataAccessorBackedObject<_DynamicStorageSelection_ICmdletSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_ICmdletSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00004227 File Offset: 0x00002427
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004234 File Offset: 0x00002434
		public bool Enabled
		{
			get
			{
				if (this.dataAccessor._Enabled_ValueProvider_ != null)
				{
					return this.dataAccessor._Enabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00004265 File Offset: 0x00002465
		public IList<string> AllFlightingParams
		{
			get
			{
				if (this.dataAccessor._AllFlightingParams_ValueProvider_ != null)
				{
					return this.dataAccessor._AllFlightingParams_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._AllFlightingParams_MaterializedValue_;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00004296 File Offset: 0x00002496
		public IList<string> Params0
		{
			get
			{
				if (this.dataAccessor._Params0_ValueProvider_ != null)
				{
					return this.dataAccessor._Params0_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Params0_MaterializedValue_;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000042C7 File Offset: 0x000024C7
		public IList<string> Params1
		{
			get
			{
				if (this.dataAccessor._Params1_ValueProvider_ != null)
				{
					return this.dataAccessor._Params1_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Params1_MaterializedValue_;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000042F8 File Offset: 0x000024F8
		public IList<string> Params2
		{
			get
			{
				if (this.dataAccessor._Params2_ValueProvider_ != null)
				{
					return this.dataAccessor._Params2_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Params2_MaterializedValue_;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00004329 File Offset: 0x00002529
		public IList<string> Params3
		{
			get
			{
				if (this.dataAccessor._Params3_ValueProvider_ != null)
				{
					return this.dataAccessor._Params3_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Params3_MaterializedValue_;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000435A File Offset: 0x0000255A
		public IList<string> Params4
		{
			get
			{
				if (this.dataAccessor._Params4_ValueProvider_ != null)
				{
					return this.dataAccessor._Params4_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Params4_MaterializedValue_;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000438B File Offset: 0x0000258B
		public IList<string> Params5
		{
			get
			{
				if (this.dataAccessor._Params5_ValueProvider_ != null)
				{
					return this.dataAccessor._Params5_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Params5_MaterializedValue_;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000043BC File Offset: 0x000025BC
		public IList<string> Params6
		{
			get
			{
				if (this.dataAccessor._Params6_ValueProvider_ != null)
				{
					return this.dataAccessor._Params6_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Params6_MaterializedValue_;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000043ED File Offset: 0x000025ED
		public IList<string> Params7
		{
			get
			{
				if (this.dataAccessor._Params7_ValueProvider_ != null)
				{
					return this.dataAccessor._Params7_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Params7_MaterializedValue_;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000441E File Offset: 0x0000261E
		public IList<string> Params8
		{
			get
			{
				if (this.dataAccessor._Params8_ValueProvider_ != null)
				{
					return this.dataAccessor._Params8_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Params8_MaterializedValue_;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000444F File Offset: 0x0000264F
		public IList<string> Params9
		{
			get
			{
				if (this.dataAccessor._Params9_ValueProvider_ != null)
				{
					return this.dataAccessor._Params9_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Params9_MaterializedValue_;
			}
		}

		// Token: 0x040000FB RID: 251
		private _DynamicStorageSelection_ICmdletSettings_DataAccessor_ dataAccessor;

		// Token: 0x040000FC RID: 252
		private VariantContextSnapshot context;
	}
}

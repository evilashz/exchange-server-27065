using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x0200005B RID: 91
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IFlight_Implementation_ : IFlight, IDataAccessorBackedObject<_DynamicStorageSelection_IFlight_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00004A90 File Offset: 0x00002C90
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00004A98 File Offset: 0x00002C98
		_DynamicStorageSelection_IFlight_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IFlight_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00004AA0 File Offset: 0x00002CA0
		void IDataAccessorBackedObject<_DynamicStorageSelection_IFlight_DataAccessor_>.Initialize(_DynamicStorageSelection_IFlight_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00004AB0 File Offset: 0x00002CB0
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00004ABD File Offset: 0x00002CBD
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

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00004AEE File Offset: 0x00002CEE
		public string Rotate
		{
			get
			{
				if (this.dataAccessor._Rotate_ValueProvider_ != null)
				{
					return this.dataAccessor._Rotate_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Rotate_MaterializedValue_;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00004B1F File Offset: 0x00002D1F
		public string Ramp
		{
			get
			{
				if (this.dataAccessor._Ramp_ValueProvider_ != null)
				{
					return this.dataAccessor._Ramp_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Ramp_MaterializedValue_;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00004B50 File Offset: 0x00002D50
		public string RampSeed
		{
			get
			{
				if (this.dataAccessor._RampSeed_ValueProvider_ != null)
				{
					return this.dataAccessor._RampSeed_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._RampSeed_MaterializedValue_;
			}
		}

		// Token: 0x04000156 RID: 342
		private _DynamicStorageSelection_IFlight_DataAccessor_ dataAccessor;

		// Token: 0x04000157 RID: 343
		private VariantContextSnapshot context;
	}
}

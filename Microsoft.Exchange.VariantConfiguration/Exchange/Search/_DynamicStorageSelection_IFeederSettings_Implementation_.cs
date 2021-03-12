using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000057 RID: 87
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IFeederSettings_Implementation_ : IFeederSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IFeederSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00004A04 File Offset: 0x00002C04
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00004A0C File Offset: 0x00002C0C
		_DynamicStorageSelection_IFeederSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IFeederSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00004A14 File Offset: 0x00002C14
		void IDataAccessorBackedObject<_DynamicStorageSelection_IFeederSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IFeederSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00004A24 File Offset: 0x00002C24
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00004A31 File Offset: 0x00002C31
		public int QueueSize
		{
			get
			{
				if (this.dataAccessor._QueueSize_ValueProvider_ != null)
				{
					return this.dataAccessor._QueueSize_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._QueueSize_MaterializedValue_;
			}
		}

		// Token: 0x04000149 RID: 329
		private _DynamicStorageSelection_IFeederSettings_DataAccessor_ dataAccessor;

		// Token: 0x0400014A RID: 330
		private VariantContextSnapshot context;
	}
}

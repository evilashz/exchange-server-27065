using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x020000AE RID: 174
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_ITransportFlowSettings_Implementation_ : ITransportFlowSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_ITransportFlowSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00006E5D File Offset: 0x0000505D
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00006E65 File Offset: 0x00005065
		_DynamicStorageSelection_ITransportFlowSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_ITransportFlowSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00006E6D File Offset: 0x0000506D
		void IDataAccessorBackedObject<_DynamicStorageSelection_ITransportFlowSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_ITransportFlowSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00006E7D File Offset: 0x0000507D
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00006E8A File Offset: 0x0000508A
		public bool SkipTokenInfoGeneration
		{
			get
			{
				if (this.dataAccessor._SkipTokenInfoGeneration_ValueProvider_ != null)
				{
					return this.dataAccessor._SkipTokenInfoGeneration_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._SkipTokenInfoGeneration_MaterializedValue_;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00006EBB File Offset: 0x000050BB
		public bool SkipMdmGeneration
		{
			get
			{
				if (this.dataAccessor._SkipMdmGeneration_ValueProvider_ != null)
				{
					return this.dataAccessor._SkipMdmGeneration_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._SkipMdmGeneration_MaterializedValue_;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00006EEC File Offset: 0x000050EC
		public bool UseMdmFlow
		{
			get
			{
				if (this.dataAccessor._UseMdmFlow_ValueProvider_ != null)
				{
					return this.dataAccessor._UseMdmFlow_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._UseMdmFlow_MaterializedValue_;
			}
		}

		// Token: 0x04000319 RID: 793
		private _DynamicStorageSelection_ITransportFlowSettings_DataAccessor_ dataAccessor;

		// Token: 0x0400031A RID: 794
		private VariantContextSnapshot context;
	}
}

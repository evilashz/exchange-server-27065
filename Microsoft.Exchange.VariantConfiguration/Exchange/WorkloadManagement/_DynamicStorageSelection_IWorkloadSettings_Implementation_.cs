using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x020000BE RID: 190
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IWorkloadSettings_Implementation_ : IWorkloadSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IWorkloadSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00007171 File Offset: 0x00005371
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00007179 File Offset: 0x00005379
		_DynamicStorageSelection_IWorkloadSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IWorkloadSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00007181 File Offset: 0x00005381
		void IDataAccessorBackedObject<_DynamicStorageSelection_IWorkloadSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IWorkloadSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00007191 File Offset: 0x00005391
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000719E File Offset: 0x0000539E
		public WorkloadClassification Classification
		{
			get
			{
				if (this.dataAccessor._Classification_ValueProvider_ != null)
				{
					return this.dataAccessor._Classification_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Classification_MaterializedValue_;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x000071CF File Offset: 0x000053CF
		public int MaxConcurrency
		{
			get
			{
				if (this.dataAccessor._MaxConcurrency_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxConcurrency_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxConcurrency_MaterializedValue_;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00007200 File Offset: 0x00005400
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

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00007231 File Offset: 0x00005431
		public bool EnabledDuringBlackout
		{
			get
			{
				if (this.dataAccessor._EnabledDuringBlackout_ValueProvider_ != null)
				{
					return this.dataAccessor._EnabledDuringBlackout_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._EnabledDuringBlackout_MaterializedValue_;
			}
		}

		// Token: 0x04000343 RID: 835
		private _DynamicStorageSelection_IWorkloadSettings_DataAccessor_ dataAccessor;

		// Token: 0x04000344 RID: 836
		private VariantContextSnapshot context;
	}
}

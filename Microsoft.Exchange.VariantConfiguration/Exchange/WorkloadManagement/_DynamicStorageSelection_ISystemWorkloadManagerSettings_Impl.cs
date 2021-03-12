using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x020000A6 RID: 166
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_ISystemWorkloadManagerSettings_Implementation_ : ISystemWorkloadManagerSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_ISystemWorkloadManagerSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00006D00 File Offset: 0x00004F00
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00006D08 File Offset: 0x00004F08
		_DynamicStorageSelection_ISystemWorkloadManagerSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_ISystemWorkloadManagerSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00006D10 File Offset: 0x00004F10
		void IDataAccessorBackedObject<_DynamicStorageSelection_ISystemWorkloadManagerSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_ISystemWorkloadManagerSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00006D20 File Offset: 0x00004F20
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00006D2D File Offset: 0x00004F2D
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

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00006D5E File Offset: 0x00004F5E
		public TimeSpan RefreshCycle
		{
			get
			{
				if (this.dataAccessor._RefreshCycle_ValueProvider_ != null)
				{
					return this.dataAccessor._RefreshCycle_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._RefreshCycle_MaterializedValue_;
			}
		}

		// Token: 0x04000306 RID: 774
		private _DynamicStorageSelection_ISystemWorkloadManagerSettings_DataAccessor_ dataAccessor;

		// Token: 0x04000307 RID: 775
		private VariantContextSnapshot context;
	}
}

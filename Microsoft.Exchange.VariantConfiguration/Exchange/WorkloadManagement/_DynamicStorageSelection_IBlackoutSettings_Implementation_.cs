using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200001B RID: 27
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IBlackoutSettings_Implementation_ : IBlackoutSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IBlackoutSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000036EA File Offset: 0x000018EA
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000036F2 File Offset: 0x000018F2
		_DynamicStorageSelection_IBlackoutSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IBlackoutSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000036FA File Offset: 0x000018FA
		void IDataAccessorBackedObject<_DynamicStorageSelection_IBlackoutSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IBlackoutSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000370A File Offset: 0x0000190A
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003717 File Offset: 0x00001917
		public TimeSpan StartTime
		{
			get
			{
				if (this.dataAccessor._StartTime_ValueProvider_ != null)
				{
					return this.dataAccessor._StartTime_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._StartTime_MaterializedValue_;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003748 File Offset: 0x00001948
		public TimeSpan EndTime
		{
			get
			{
				if (this.dataAccessor._EndTime_ValueProvider_ != null)
				{
					return this.dataAccessor._EndTime_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._EndTime_MaterializedValue_;
			}
		}

		// Token: 0x04000058 RID: 88
		private _DynamicStorageSelection_IBlackoutSettings_DataAccessor_ dataAccessor;

		// Token: 0x04000059 RID: 89
		private VariantContextSnapshot context;
	}
}

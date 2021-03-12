using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Calendar
{
	// Token: 0x0200002F RID: 47
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_ICalendarUpgradeSettings_Implementation_ : ICalendarUpgradeSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_ICalendarUpgradeSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003C28 File Offset: 0x00001E28
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003C30 File Offset: 0x00001E30
		_DynamicStorageSelection_ICalendarUpgradeSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_ICalendarUpgradeSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003C38 File Offset: 0x00001E38
		void IDataAccessorBackedObject<_DynamicStorageSelection_ICalendarUpgradeSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_ICalendarUpgradeSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003C48 File Offset: 0x00001E48
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003C55 File Offset: 0x00001E55
		public int MinCalendarItemsForUpgrade
		{
			get
			{
				if (this.dataAccessor._MinCalendarItemsForUpgrade_ValueProvider_ != null)
				{
					return this.dataAccessor._MinCalendarItemsForUpgrade_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MinCalendarItemsForUpgrade_MaterializedValue_;
			}
		}

		// Token: 0x04000097 RID: 151
		private _DynamicStorageSelection_ICalendarUpgradeSettings_DataAccessor_ dataAccessor;

		// Token: 0x04000098 RID: 152
		private VariantContextSnapshot context;
	}
}

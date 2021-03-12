using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Calendar
{
	// Token: 0x0200002E RID: 46
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_ICalendarUpgradeSettings_DataAccessor_ : VariantObjectDataAccessorBase<ICalendarUpgradeSettings, _DynamicStorageSelection_ICalendarUpgradeSettings_Implementation_, _DynamicStorageSelection_ICalendarUpgradeSettings_DataAccessor_>
	{
		// Token: 0x04000094 RID: 148
		internal string _Name_MaterializedValue_;

		// Token: 0x04000095 RID: 149
		internal int _MinCalendarItemsForUpgrade_MaterializedValue_;

		// Token: 0x04000096 RID: 150
		internal ValueProvider<int> _MinCalendarItemsForUpgrade_ValueProvider_;
	}
}

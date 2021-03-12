using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.MessageDepot
{
	// Token: 0x0200008A RID: 138
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IMessageDepotSettings_DataAccessor_ : VariantObjectDataAccessorBase<IMessageDepotSettings, _DynamicStorageSelection_IMessageDepotSettings_Implementation_, _DynamicStorageSelection_IMessageDepotSettings_DataAccessor_>
	{
		// Token: 0x040002A0 RID: 672
		internal string _Name_MaterializedValue_;

		// Token: 0x040002A1 RID: 673
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x040002A2 RID: 674
		internal ValueProvider<bool> _Enabled_ValueProvider_;

		// Token: 0x040002A3 RID: 675
		internal IList<DayOfWeek> _EnabledOnDaysOfWeek_MaterializedValue_;

		// Token: 0x040002A4 RID: 676
		internal ValueProvider<IList<DayOfWeek>> _EnabledOnDaysOfWeek_ValueProvider_;
	}
}

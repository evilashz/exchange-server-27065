using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.TextProcessing.Boomerang
{
	// Token: 0x0200001E RID: 30
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IBoomerangSettings_DataAccessor_ : VariantObjectDataAccessorBase<IBoomerangSettings, _DynamicStorageSelection_IBoomerangSettings_Implementation_, _DynamicStorageSelection_IBoomerangSettings_DataAccessor_>
	{
		// Token: 0x0400005D RID: 93
		internal string _Name_MaterializedValue_;

		// Token: 0x0400005E RID: 94
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x0400005F RID: 95
		internal ValueProvider<bool> _Enabled_ValueProvider_;

		// Token: 0x04000060 RID: 96
		internal string _MasterKeyRegistryPath_MaterializedValue_;

		// Token: 0x04000061 RID: 97
		internal ValueProvider<string> _MasterKeyRegistryPath_ValueProvider_;

		// Token: 0x04000062 RID: 98
		internal string _MasterKeyRegistryKeyName_MaterializedValue_;

		// Token: 0x04000063 RID: 99
		internal ValueProvider<string> _MasterKeyRegistryKeyName_ValueProvider_;

		// Token: 0x04000064 RID: 100
		internal uint _NumberOfValidIntervalsInDays_MaterializedValue_;

		// Token: 0x04000065 RID: 101
		internal ValueProvider<uint> _NumberOfValidIntervalsInDays_ValueProvider_;
	}
}

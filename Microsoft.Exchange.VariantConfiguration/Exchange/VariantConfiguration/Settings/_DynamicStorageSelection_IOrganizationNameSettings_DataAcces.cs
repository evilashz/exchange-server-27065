using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.VariantConfiguration.Settings
{
	// Token: 0x0200008E RID: 142
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IOrganizationNameSettings_DataAccessor_ : VariantObjectDataAccessorBase<IOrganizationNameSettings, _DynamicStorageSelection_IOrganizationNameSettings_Implementation_, _DynamicStorageSelection_IOrganizationNameSettings_DataAccessor_>
	{
		// Token: 0x040002AA RID: 682
		internal string _Name_MaterializedValue_;

		// Token: 0x040002AB RID: 683
		internal IList<string> _OrgNames_MaterializedValue_;

		// Token: 0x040002AC RID: 684
		internal ValueProvider<IList<string>> _OrgNames_ValueProvider_;

		// Token: 0x040002AD RID: 685
		internal IList<string> _DogfoodOrgNames_MaterializedValue_;

		// Token: 0x040002AE RID: 686
		internal ValueProvider<IList<string>> _DogfoodOrgNames_ValueProvider_;
	}
}

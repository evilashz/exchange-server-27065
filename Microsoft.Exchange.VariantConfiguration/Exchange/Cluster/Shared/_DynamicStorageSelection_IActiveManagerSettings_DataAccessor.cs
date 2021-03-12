using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000016 RID: 22
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IActiveManagerSettings_DataAccessor_ : VariantObjectDataAccessorBase<IActiveManagerSettings, _DynamicStorageSelection_IActiveManagerSettings_Implementation_, _DynamicStorageSelection_IActiveManagerSettings_DataAccessor_>
	{
		// Token: 0x0400003D RID: 61
		internal string _Name_MaterializedValue_;

		// Token: 0x0400003E RID: 62
		internal DxStoreMode _DxStoreRunMode_MaterializedValue_;

		// Token: 0x0400003F RID: 63
		internal ValueProvider<DxStoreMode> _DxStoreRunMode_ValueProvider_;

		// Token: 0x04000040 RID: 64
		internal bool _DxStoreIsUseHttpForInstanceCommunication_MaterializedValue_;

		// Token: 0x04000041 RID: 65
		internal ValueProvider<bool> _DxStoreIsUseHttpForInstanceCommunication_ValueProvider_;

		// Token: 0x04000042 RID: 66
		internal bool _DxStoreIsUseHttpForClientCommunication_MaterializedValue_;

		// Token: 0x04000043 RID: 67
		internal ValueProvider<bool> _DxStoreIsUseHttpForClientCommunication_ValueProvider_;

		// Token: 0x04000044 RID: 68
		internal bool _DxStoreIsEncryptionEnabled_MaterializedValue_;

		// Token: 0x04000045 RID: 69
		internal ValueProvider<bool> _DxStoreIsEncryptionEnabled_ValueProvider_;

		// Token: 0x04000046 RID: 70
		internal bool _DxStoreIsPeriodicFixupEnabled_MaterializedValue_;

		// Token: 0x04000047 RID: 71
		internal ValueProvider<bool> _DxStoreIsPeriodicFixupEnabled_ValueProvider_;

		// Token: 0x04000048 RID: 72
		internal bool _DxStoreIsUseBinarySerializerForClientCommunication_MaterializedValue_;

		// Token: 0x04000049 RID: 73
		internal ValueProvider<bool> _DxStoreIsUseBinarySerializerForClientCommunication_ValueProvider_;
	}
}

using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D62 RID: 3426
	internal class UMPromptPreviewRpcTargetPicker : UMServerRpcTargetPickerBase<IVersionedRpcTarget>
	{
		// Token: 0x06008367 RID: 33639 RVA: 0x00218A40 File Offset: 0x00216C40
		protected override IVersionedRpcTarget CreateTarget(Server server)
		{
			return new UMPromptPreviewRpcTarget(server);
		}

		// Token: 0x04003FC9 RID: 16329
		public static readonly UMPromptPreviewRpcTargetPicker Instance = new UMPromptPreviewRpcTargetPicker();
	}
}

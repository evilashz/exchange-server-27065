using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000183 RID: 387
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IContentsSynchronizerClient : IDisposable
	{
		// Token: 0x06000798 RID: 1944
		void SetProgressInformation(ProgressInformation progressInformation);

		// Token: 0x06000799 RID: 1945
		IMessageChangeClient UploadMessageChange();

		// Token: 0x0600079A RID: 1946
		IPropertyBag LoadDeletionPropertyBag();

		// Token: 0x0600079B RID: 1947
		IPropertyBag LoadReadUnreadPropertyBag();

		// Token: 0x0600079C RID: 1948
		IIcsState LoadFinalState();
	}
}

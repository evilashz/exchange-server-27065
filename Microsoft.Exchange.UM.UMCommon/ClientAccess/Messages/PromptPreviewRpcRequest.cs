using System;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x02000136 RID: 310
	[Serializable]
	public class PromptPreviewRpcRequest : RequestBase
	{
		// Token: 0x060009F3 RID: 2547 RVA: 0x00026555 File Offset: 0x00024755
		internal override string GetFriendlyName()
		{
			return Strings.PromptPreviewRpcRequest;
		}
	}
}

using System;
using System.Collections;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000202 RID: 514
	internal class UMAABusinessLocationPromptRpcRequestHandler : UMPromptRpcRequestHandler
	{
		// Token: 0x06000F08 RID: 3848 RVA: 0x00044140 File Offset: 0x00042340
		protected override ArrayList GetPrompts(RequestBase requestBase)
		{
			UMAABusinessLocationPromptRpcRequest umaabusinessLocationPromptRpcRequest = requestBase as UMAABusinessLocationPromptRpcRequest;
			return VariablePrompt<AutoAttendantLocationContext>.GetPreview<AABusinessLocationPrompt>(new AABusinessLocationPrompt(), umaabusinessLocationPromptRpcRequest.AutoAttendant.Language.Culture, new AutoAttendantLocationContext(umaabusinessLocationPromptRpcRequest.AutoAttendant, Strings.BusinessLocationDefaultMenuName));
		}
	}
}

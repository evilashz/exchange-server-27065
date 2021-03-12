using System;
using System.Collections;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.ClientAccess.Messages;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000201 RID: 513
	internal class UMAABusinessHoursPromptRpcRequestHandler : UMPromptRpcRequestHandler
	{
		// Token: 0x06000F06 RID: 3846 RVA: 0x00044100 File Offset: 0x00042300
		protected override ArrayList GetPrompts(RequestBase requestBase)
		{
			UMAABusinessHoursPromptRpcRequest umaabusinessHoursPromptRpcRequest = requestBase as UMAABusinessHoursPromptRpcRequest;
			BusinessHoursPrompt prompt = new BusinessHoursPrompt();
			return VariablePrompt<UMAutoAttendant>.GetPreview<BusinessHoursPrompt>(prompt, umaabusinessHoursPromptRpcRequest.AutoAttendant.Language.Culture, umaabusinessHoursPromptRpcRequest.AutoAttendant);
		}
	}
}

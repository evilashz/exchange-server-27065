using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002A3 RID: 675
	internal interface IMessageTrackingRequestLogInformation
	{
		// Token: 0x060011FB RID: 4603
		void AddRequestDataForLogging(List<KeyValuePair<string, object>> requestData);
	}
}

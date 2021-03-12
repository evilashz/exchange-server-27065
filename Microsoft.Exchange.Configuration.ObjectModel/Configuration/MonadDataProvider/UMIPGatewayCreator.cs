using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001C0 RID: 448
	internal class UMIPGatewayCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FC5 RID: 4037 RVA: 0x000301D0 File Offset: 0x0002E3D0
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"WhenChanged",
				"Name",
				"OutcallsAllowed",
				"MessageWaitingIndicatorAllowed",
				"Address",
				"Status"
			};
		}
	}
}

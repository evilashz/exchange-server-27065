using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.Analysis.Features;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000019 RID: 25
	internal interface IManagedMethodProvider
	{
		// Token: 0x06000038 RID: 56
		Dictionary<string, object[]> CheckDNS(string ipAddress, string svrFQDN);

		// Token: 0x06000039 RID: 57
		Dictionary<string, object[]> PortAvailable(string svrName, Dictionary<string, List<string>> commands);

		// Token: 0x0600003A RID: 58
		string GetComputerNameEx(ValidationConstant.ComputerNameFormat computerNameFormat);

		// Token: 0x0600003B RID: 59
		string GetUserNameEx(ValidationConstant.ExtendedNameFormat extendedNameType);
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000017 RID: 23
	// (Invoke) Token: 0x0600006F RID: 111
	internal delegate IEntity CreateEntityDelegate(string name, TaskInvocationInfo taskInvocationInfo, Dictionary<string, List<string>> reportPropertyCmdletParamsMap, IReportAnnotation annotation);
}

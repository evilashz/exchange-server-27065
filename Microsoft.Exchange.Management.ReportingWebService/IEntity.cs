using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.ReportingWebService.PowerShell;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200001E RID: 30
	internal interface IEntity
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000092 RID: 146
		string Name { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000093 RID: 147
		TaskInvocationInfo TaskInvocationInfo { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000094 RID: 148
		Dictionary<string, List<string>> ReportPropertyCmdletParamsMap { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000095 RID: 149
		IReportAnnotation Annotation { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000096 RID: 150
		string[] KeyMembers { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000097 RID: 151
		Type ClrType { get; }

		// Token: 0x06000098 RID: 152
		void Initialize(IPSCommandResolver commandResolver);
	}
}

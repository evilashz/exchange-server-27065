using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x0200003A RID: 58
	internal interface IComplianceTaskCreator
	{
		// Token: 0x06000172 RID: 370
		IEnumerable<CompositeTask> CreateTasks(IConfigDataProvider dataProvider, ComplianceJob job, CreateTaskOptions createTaskOptions, Action<string, ComplianceBindingErrorType> invalidBindingHandler);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000024 RID: 36
	internal interface IReportingDataSource
	{
		// Token: 0x060000BC RID: 188
		IList<T> GetData<T>(IEntity entity, Expression expression);

		// Token: 0x060000BD RID: 189
		IList GetData(IEntity entity, Expression expression);
	}
}

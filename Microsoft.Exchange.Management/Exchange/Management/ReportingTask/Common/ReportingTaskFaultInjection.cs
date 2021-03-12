using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Exchange.Diagnostics.Components.ReportingTask;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006A9 RID: 1705
	public class ReportingTaskFaultInjection
	{
		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06003C69 RID: 15465 RVA: 0x0010143C File Offset: 0x000FF63C
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ReportingTaskFaultInjection.faultInjectionTracer == null)
				{
					lock (ReportingTaskFaultInjection.lockObject)
					{
						if (ReportingTaskFaultInjection.faultInjectionTracer == null)
						{
							FaultInjectionTrace faultInjectionTrace = ExTraceGlobals.FaultInjectionTracer;
							faultInjectionTrace.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(ReportingTaskFaultInjection.Callback));
							ReportingTaskFaultInjection.faultInjectionTracer = faultInjectionTrace;
						}
					}
				}
				return ReportingTaskFaultInjection.faultInjectionTracer;
			}
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x001014A8 File Offset: 0x000FF6A8
		public static Exception Callback(string exceptionType)
		{
			Exception result = null;
			if (exceptionType != null)
			{
				if (typeof(DatabaseException).FullName.Equals(exceptionType))
				{
					return new DatabaseException(0, "DatabaseException");
				}
				if (typeof(SqlTypeException).FullName.Equals(exceptionType))
				{
					return new SqlTypeException("SqlTypeException");
				}
				if (typeof(InvalidOperationException).FullName.Equals(exceptionType))
				{
					SqlCommand sqlCommand = new SqlCommand();
					sqlCommand.ExecuteScalar();
				}
			}
			return result;
		}

		// Token: 0x04002734 RID: 10036
		private static object lockObject = new object();

		// Token: 0x04002735 RID: 10037
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}

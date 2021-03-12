using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000068 RID: 104
	internal class LegacyErrorHandler : IErrorHandler
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x00013309 File Offset: 0x00011509
		public bool HandleError(Exception error)
		{
			LegacyErrorHandler.ReportException(error, this, HttpContext.Current);
			return true;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00013318 File Offset: 0x00011518
		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			LegacyErrorHandler.ReportException(error, this, HttpContext.Current);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00013328 File Offset: 0x00011528
		private static void ReportException(Exception exception, object responsibleObject, HttpContext httpContext)
		{
			bool flag = true;
			if (exception is FaultException)
			{
				FaultException ex = exception as FaultException;
				if (ex.Code.IsSenderFault)
				{
					flag = false;
				}
			}
			try
			{
				if (flag)
				{
					Common.ReportException(exception, responsibleObject, httpContext);
				}
			}
			catch (Exception)
			{
			}
		}
	}
}

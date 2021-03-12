using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Server.Wcf
{
	// Token: 0x0200002D RID: 45
	public class PushNotificationErrorHandler : IErrorHandler
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00004864 File Offset: 0x00002A64
		public bool HandleError(Exception error)
		{
			if (error is PushNotificationPermanentException || error is PushNotificationTransientException)
			{
				if (ExTraceGlobals.PushNotificationServiceTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.PushNotificationServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "PushNotificationErrorHandler.HandleError: Error reported is known Exception {0}.", error.ToTraceString());
				}
				return false;
			}
			PushNotificationsCrimsonEvents.ErrorHandlerException.LogPeriodic<string>(error.ToString(), CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, error.ToTraceString());
			if (ExTraceGlobals.PushNotificationServiceTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				ExTraceGlobals.PushNotificationServiceTracer.TraceError<string>((long)this.GetHashCode(), "PushNotificationErrorHandler.HandleError: Unknown error reported '{0}'.", error.ToTraceString());
			}
			if (this.IsGrayException(error))
			{
				ExWatson.SendReport(error, ReportOptions.DoNotFreezeThreads, null);
			}
			return false;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004904 File Offset: 0x00002B04
		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004906 File Offset: 0x00002B06
		private bool IsGrayException(Exception ex)
		{
			return GrayException.IsGrayException(ex);
		}
	}
}

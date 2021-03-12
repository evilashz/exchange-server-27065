using System;
using System.Net;
using System.ServiceModel.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Server.Core
{
	// Token: 0x02000008 RID: 8
	internal class ServiceCommandAsyncResult<TResult> : BasicAsyncResult<TResult>
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000028B5 File Offset: 0x00000AB5
		public ServiceCommandAsyncResult(AsyncCallback asyncCallback, object state) : base(asyncCallback, state)
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000028BF File Offset: 0x00000ABF
		protected override bool ShouldThrowCallbackException(Exception ex)
		{
			if (ExTraceGlobals.PushNotificationServiceTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				ExTraceGlobals.PushNotificationServiceTracer.TraceError<string>((long)this.GetHashCode(), "ServiceCommandAsyncResult.ShouldThrowCallbackException Exception processed.\n{0}.", ex.ToTraceString());
			}
			return base.ShouldThrowCallbackException(ex);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000028F1 File Offset: 0x00000AF1
		protected override Exception CreateEndException(Exception currentException)
		{
			this.LogException(currentException);
			return new WebFaultException<PushNotificationFault>(new PushNotificationFault(currentException, ServiceConfig.IsWriteStackTraceOnResponseEnabled, 0, true), HttpStatusCode.InternalServerError);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002914 File Offset: 0x00000B14
		private void LogException(Exception ex)
		{
			PushNotificationsCrimsonEvents.ServiceCommandException.LogPeriodic<string>(ex.ToString(), CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, ex.ToTraceString());
			if (ExTraceGlobals.PushNotificationServiceTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				ExTraceGlobals.PushNotificationServiceTracer.TraceError<string>((long)this.GetHashCode(), "A service command execution reported an error.\n{0}.", ex.ToTraceString());
			}
		}
	}
}

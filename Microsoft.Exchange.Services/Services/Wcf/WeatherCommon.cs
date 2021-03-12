using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000944 RID: 2372
	internal static class WeatherCommon
	{
		// Token: 0x06004497 RID: 17559 RVA: 0x000ECA24 File Offset: 0x000EAC24
		internal static string ExecuteActionAndHandleException(CallContext callContext, int traceId, string defaultExceptionMessage, Action operation)
		{
			Exception exception = null;
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						operation();
					}
					catch (Exception ex2)
					{
						if (ex2 is WebException)
						{
							callContext.ProtocolLog.Set(WeatherMetadata.WebExceptionStatusCode, ((WebException)ex2).Status);
							exception = ex2;
						}
						else
						{
							if (!(ex2 is ObjectDisposedException) && !(ex2 is WeatherException))
							{
								throw;
							}
							exception = ex2;
						}
					}
				});
			}
			catch (GrayException ex)
			{
				ExTraceGlobals.WeatherTracer.TraceError<GrayException>((long)traceId, "Request failed due to gray exception {0}", ex);
				exception = ex;
			}
			if (exception == null)
			{
				return null;
			}
			ExTraceGlobals.WeatherTracer.TraceError<Exception>((long)traceId, "Request failed due to exception {0}", exception);
			callContext.ProtocolLog.Set(WeatherMetadata.Failed, exception.Message);
			if (!string.IsNullOrEmpty(exception.Message))
			{
				return exception.Message;
			}
			return defaultExceptionMessage;
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x000ECAF4 File Offset: 0x000EACF4
		internal static string FormatWebFormField(string fieldName, string fieldValue)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[]
			{
				fieldName,
				fieldValue
			});
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x000ECB20 File Offset: 0x000EAD20
		internal static string FormatLatitude(double latitude)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}° {1}", new object[]
			{
				Math.Abs(latitude),
				(latitude < 0.0) ? "S" : "N"
			});
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x000ECB70 File Offset: 0x000EAD70
		internal static string FormatLongitude(double longitude)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}° {1}", new object[]
			{
				Math.Abs(longitude),
				(longitude < 0.0) ? "W" : "E"
			});
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x000ECBC0 File Offset: 0x000EADC0
		internal static string FormatEntityId(object entityId)
		{
			return string.Format(CultureInfo.InvariantCulture, "ei:{0}", new object[]
			{
				entityId
			});
		}

		// Token: 0x040027F9 RID: 10233
		internal const int MaxLocationsResponseLength = 70000;

		// Token: 0x040027FA RID: 10234
		internal const int MaxForecastResponseLength = 300000;

		// Token: 0x040027FB RID: 10235
		internal const string LocationSearchStringFieldName = "weasearchstr";

		// Token: 0x040027FC RID: 10236
		internal const string LocationStringFieldName = "wealocations";

		// Token: 0x040027FD RID: 10237
		internal const string SourceFieldName = "src";

		// Token: 0x040027FE RID: 10238
		internal const string CultureFieldName = "culture";

		// Token: 0x040027FF RID: 10239
		internal const string OutputViewFieldName = "outputview";

		// Token: 0x04002800 RID: 10240
		internal const string QueryStringFieldSeparator = "&";

		// Token: 0x04002801 RID: 10241
		internal const string QueryStringFieldFormat = "{0}={1}";
	}
}

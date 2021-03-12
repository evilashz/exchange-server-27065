using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000012 RID: 18
	internal abstract class MapiHttpHandler : IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00005A82 File Offset: 0x00003C82
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000032 RID: 50
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00005A85 File Offset: 0x00003C85
		internal static Action ShutdownHandlerDelegate
		{
			set
			{
				MapiHttpHandler.delegateShutdownHandler = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00005A8D File Offset: 0x00003C8D
		internal static Func<object, bool> IsValidContextHandleDelegate
		{
			set
			{
				MapiHttpHandler.delegateIsValidContextHandle = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00005A95 File Offset: 0x00003C95
		internal static Func<object, bool> TryContextHandleRundownDelegate
		{
			set
			{
				MapiHttpHandler.delegateTryContextHandleRundown = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00005A9D File Offset: 0x00003C9D
		internal static Action<object> QueueDroppedConnectionDelegate
		{
			set
			{
				MapiHttpHandler.delegateQueueDroppedConnection = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00005AA5 File Offset: 0x00003CA5
		internal static Func<string, bool> NeedTokenRehydrationDelegate
		{
			set
			{
				MapiHttpHandler.delegateNeedTokenRehydration = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00005AAD File Offset: 0x00003CAD
		internal static bool CanTrustEntireForwardedForHeader
		{
			get
			{
				if (MapiHttpHandler.canTrustEntireForwardedForHeader == null)
				{
					MapiHttpHandler.canTrustEntireForwardedForHeader = new bool?(MapiHttpHandler.ReadBoolAppSetting("TrustEntireForwardedFor", false));
				}
				return MapiHttpHandler.canTrustEntireForwardedForHeader.Value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00005ADA File Offset: 0x00003CDA
		internal static bool UseBufferedReadStream
		{
			get
			{
				if (MapiHttpHandler.useBufferedReadStream == null)
				{
					MapiHttpHandler.useBufferedReadStream = new bool?(MapiHttpHandler.ReadBoolAppSetting("UseBufferedReadStream", false));
				}
				return MapiHttpHandler.useBufferedReadStream.Value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005B10 File Offset: 0x00003D10
		internal static TimeSpan ClientTunnelExpirationTime
		{
			get
			{
				if (MapiHttpHandler.clientTunnelExpirationTime == null)
				{
					MapiHttpHandler.clientTunnelExpirationTime = new TimeSpan?(MapiHttpHandler.ReadTimeSpanAppSetting("ClientTunnelExpirationTime", (double x) => TimeSpan.FromMinutes(x), Constants.ClientTunnelExpirationTimeMin, Constants.ClientTunnelExpirationTimeMax, Constants.ClientTunnelExpirationTimeDefault));
				}
				return MapiHttpHandler.clientTunnelExpirationTime.Value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B6 RID: 182
		internal abstract string EndpointVdirPath { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B7 RID: 183
		internal abstract IAsyncOperationFactory OperationFactory { get; }

		// Token: 0x060000B8 RID: 184 RVA: 0x00005B73 File Offset: 0x00003D73
		public void ProcessRequest(HttpContext context)
		{
			throw new NotSupportedException("Handler not synchronously callable.");
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005B80 File Offset: 0x00003D80
		public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback asyncCallback, object asyncState)
		{
			MapiHttpRequestState mapiHttpRequestState = new MapiHttpRequestState(this, MapiHttpContextWrapper.GetWrapper(context), asyncCallback, asyncState);
			mapiHttpRequestState.Begin();
			return mapiHttpRequestState;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005BA4 File Offset: 0x00003DA4
		public void EndProcessRequest(IAsyncResult asyncResult)
		{
			MapiHttpRequestState mapiHttpRequestState = asyncResult as MapiHttpRequestState;
			if (mapiHttpRequestState == null)
			{
				throw new InvalidOperationException("IAsyncResult isn't a MapiHttpRequestState object.");
			}
			mapiHttpRequestState.End();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005E20 File Offset: 0x00004020
		internal static async Task<MapiHttpDispatchedCallResult> DispatchCallAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod)
		{
			uint statusCode = 0U;
			Exception thrownException = null;
			try
			{
				await Task.Factory.FromAsync((AsyncCallback asyncCallback, object asyncState) => beginMethod(delegate(IAsyncResult asyncResult)
				{
					if (!ThreadPool.QueueUserWorkItem(delegate(object state)
					{
						asyncCallback(asyncResult);
					}))
					{
						asyncCallback(asyncResult);
					}
				}, asyncState), endMethod, null);
			}
			catch (Exception ex)
			{
				thrownException = ex;
				if (!MapiHttpHandler.TryHandleException(ex, out statusCode))
				{
					throw;
				}
			}
			return new MapiHttpDispatchedCallResult(statusCode, thrownException);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005E70 File Offset: 0x00004070
		internal static uint DispatchCallSync(Action callMethod)
		{
			uint result = 0U;
			try
			{
				callMethod();
			}
			catch (Exception exception)
			{
				if (!MapiHttpHandler.TryHandleException(exception, out result))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005EA8 File Offset: 0x000040A8
		internal static void ShutdownHandler()
		{
			if (MapiHttpHandler.delegateShutdownHandler != null)
			{
				MapiHttpHandler.delegateShutdownHandler();
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005EBB File Offset: 0x000040BB
		internal static bool IsValidContextHandle(object contextHandle)
		{
			return MapiHttpHandler.delegateIsValidContextHandle != null && MapiHttpHandler.delegateIsValidContextHandle(contextHandle);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005ED1 File Offset: 0x000040D1
		internal static bool TryContextHandleRundown(object contextHandle)
		{
			return MapiHttpHandler.delegateTryContextHandleRundown != null && MapiHttpHandler.delegateTryContextHandleRundown(contextHandle);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005EE7 File Offset: 0x000040E7
		internal static void QueueDroppedConnection(object contextHandle)
		{
			if (contextHandle != null && MapiHttpHandler.delegateQueueDroppedConnection != null)
			{
				MapiHttpHandler.delegateQueueDroppedConnection(contextHandle);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005EFE File Offset: 0x000040FE
		internal static bool NeedTokenRehydration(string requestType)
		{
			return MapiHttpHandler.delegateNeedTokenRehydration == null || MapiHttpHandler.delegateNeedTokenRehydration(requestType);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005F14 File Offset: 0x00004114
		internal AsyncOperation BuildAsyncOperation(string requestType, HttpContextBase context)
		{
			return this.OperationFactory.Create(requestType, context);
		}

		// Token: 0x060000C3 RID: 195
		internal abstract bool TryEnsureHandlerIsInitialized();

		// Token: 0x060000C4 RID: 196
		internal abstract void LogFailure(IList<string> requestIds, IList<string> cookies, string message, string userName, string protocolSequence, string clientAddress, string organization, Exception exception, Trace trace);

		// Token: 0x060000C5 RID: 197 RVA: 0x00005F24 File Offset: 0x00004124
		private static bool TryHandleException(Exception exception, out uint statusCode)
		{
			statusCode = 0U;
			AggregateException ex = exception as AggregateException;
			if (ex != null)
			{
				foreach (Exception exception2 in ex.InnerExceptions)
				{
					if (MapiHttpHandler.TryHandleException(exception2, out statusCode))
					{
						return true;
					}
				}
			}
			RpcException ex2 = exception as RpcException;
			if (ex2 != null)
			{
				statusCode = (uint)ex2.ErrorCode;
				return true;
			}
			if (exception is ThreadAbortException)
			{
				statusCode = 1726U;
				return true;
			}
			if (exception is OutOfMemoryException)
			{
				statusCode = 14U;
				return true;
			}
			return false;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005FC0 File Offset: 0x000041C0
		private static bool ReadBoolAppSetting(string appSettingName, bool appSettingDefault)
		{
			string value = WebConfigurationManager.AppSettings[appSettingName];
			bool result;
			if (!string.IsNullOrEmpty(value) && bool.TryParse(value, out result))
			{
				return result;
			}
			return appSettingDefault;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005FF0 File Offset: 0x000041F0
		private static TimeSpan ReadTimeSpanAppSetting(string appSettingName, Func<double, TimeSpan> timeSpanConversion, TimeSpan timeSpanMin, TimeSpan timeSpanMax, TimeSpan timeSpanDefault)
		{
			string text = WebConfigurationManager.AppSettings[appSettingName];
			double num;
			if (!string.IsNullOrEmpty(text) && double.TryParse(text, out num) && num >= 0.0)
			{
				TimeSpan timeSpan = timeSpanConversion(num);
				if (timeSpan < timeSpanMin)
				{
					timeSpan = timeSpanMin;
				}
				else if (timeSpan > timeSpanMax)
				{
					timeSpan = timeSpanMax;
				}
				return timeSpan;
			}
			return timeSpanDefault;
		}

		// Token: 0x04000066 RID: 102
		private static Action delegateShutdownHandler = null;

		// Token: 0x04000067 RID: 103
		private static Func<object, bool> delegateIsValidContextHandle = null;

		// Token: 0x04000068 RID: 104
		private static Func<object, bool> delegateTryContextHandleRundown = null;

		// Token: 0x04000069 RID: 105
		private static Action<object> delegateQueueDroppedConnection = null;

		// Token: 0x0400006A RID: 106
		private static Func<string, bool> delegateNeedTokenRehydration = null;

		// Token: 0x0400006B RID: 107
		private static bool? canTrustEntireForwardedForHeader = null;

		// Token: 0x0400006C RID: 108
		private static bool? useBufferedReadStream = null;

		// Token: 0x0400006D RID: 109
		private static TimeSpan? clientTunnelExpirationTime = null;
	}
}

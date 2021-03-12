using System;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DxStore;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200008E RID: 142
	public class WcfStoreAccessClient : IDxStoreAccessClient
	{
		// Token: 0x06000574 RID: 1396 RVA: 0x000133A4 File Offset: 0x000115A4
		public WcfStoreAccessClient(CachedChannelFactory<IDxStoreAccess> channelFactory, TimeSpan? operationTimeout = null)
		{
			this.Initialize(channelFactory, operationTimeout);
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x000133B4 File Offset: 0x000115B4
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x000133BB File Offset: 0x000115BB
		public static DxStoreAccessExceptionTranslator Runner { get; set; } = new DxStoreAccessExceptionTranslator();

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x000133C3 File Offset: 0x000115C3
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x000133CB File Offset: 0x000115CB
		public TimeSpan? OperationTimeout { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x000133D4 File Offset: 0x000115D4
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x000133DC File Offset: 0x000115DC
		public CachedChannelFactory<IDxStoreAccess> ChannelFactory { get; set; }

		// Token: 0x0600057B RID: 1403 RVA: 0x000133E8 File Offset: 0x000115E8
		public void Initialize(CachedChannelFactory<IDxStoreAccess> channelFactory, TimeSpan? operationTimeout)
		{
			this.ChannelFactory = channelFactory;
			this.OperationTimeout = operationTimeout;
			if (ExTraceGlobals.AccessClientTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.AccessClientTracer.TraceDebug<string, string>(0L, "{0} Initialized (timeout: {1})", base.GetType().Name, (operationTimeout != null) ? operationTimeout.ToString() : "<null>");
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00013474 File Offset: 0x00011674
		public DxStoreAccessReply.CheckKey CheckKey(DxStoreAccessRequest.CheckKey request, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreAccess> runner = WcfStoreAccessClient.Runner;
			CachedChannelFactory<IDxStoreAccess> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			return runner.Execute<DxStoreAccessReply.CheckKey>(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, (IDxStoreAccess service) => this.TraceRequests<DxStoreAccessRequest.CheckKey, DxStoreAccessReply.CheckKey>(request, new Func<DxStoreAccessRequest.CheckKey, DxStoreAccessReply.CheckKey>(service.CheckKey)));
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000134F8 File Offset: 0x000116F8
		public DxStoreAccessReply.DeleteKey DeleteKey(DxStoreAccessRequest.DeleteKey request, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreAccess> runner = WcfStoreAccessClient.Runner;
			CachedChannelFactory<IDxStoreAccess> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			return runner.Execute<DxStoreAccessReply.DeleteKey>(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, (IDxStoreAccess service) => this.TraceRequests<DxStoreAccessRequest.DeleteKey, DxStoreAccessReply.DeleteKey>(request, new Func<DxStoreAccessRequest.DeleteKey, DxStoreAccessReply.DeleteKey>(service.DeleteKey)));
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001357C File Offset: 0x0001177C
		public DxStoreAccessReply.SetProperty SetProperty(DxStoreAccessRequest.SetProperty request, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreAccess> runner = WcfStoreAccessClient.Runner;
			CachedChannelFactory<IDxStoreAccess> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			return runner.Execute<DxStoreAccessReply.SetProperty>(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, (IDxStoreAccess service) => this.TraceRequests<DxStoreAccessRequest.SetProperty, DxStoreAccessReply.SetProperty>(request, new Func<DxStoreAccessRequest.SetProperty, DxStoreAccessReply.SetProperty>(service.SetProperty)));
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00013600 File Offset: 0x00011800
		public DxStoreAccessReply.DeleteProperty DeleteProperty(DxStoreAccessRequest.DeleteProperty request, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreAccess> runner = WcfStoreAccessClient.Runner;
			CachedChannelFactory<IDxStoreAccess> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			return runner.Execute<DxStoreAccessReply.DeleteProperty>(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, (IDxStoreAccess service) => this.TraceRequests<DxStoreAccessRequest.DeleteProperty, DxStoreAccessReply.DeleteProperty>(request, new Func<DxStoreAccessRequest.DeleteProperty, DxStoreAccessReply.DeleteProperty>(service.DeleteProperty)));
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00013684 File Offset: 0x00011884
		public DxStoreAccessReply.ExecuteBatch ExecuteBatch(DxStoreAccessRequest.ExecuteBatch request, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreAccess> runner = WcfStoreAccessClient.Runner;
			CachedChannelFactory<IDxStoreAccess> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			return runner.Execute<DxStoreAccessReply.ExecuteBatch>(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, (IDxStoreAccess service) => this.TraceRequests<DxStoreAccessRequest.ExecuteBatch, DxStoreAccessReply.ExecuteBatch>(request, new Func<DxStoreAccessRequest.ExecuteBatch, DxStoreAccessReply.ExecuteBatch>(service.ExecuteBatch)));
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00013708 File Offset: 0x00011908
		public DxStoreAccessReply.GetProperty GetProperty(DxStoreAccessRequest.GetProperty request, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreAccess> runner = WcfStoreAccessClient.Runner;
			CachedChannelFactory<IDxStoreAccess> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			return runner.Execute<DxStoreAccessReply.GetProperty>(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, (IDxStoreAccess service) => this.TraceRequests<DxStoreAccessRequest.GetProperty, DxStoreAccessReply.GetProperty>(request, new Func<DxStoreAccessRequest.GetProperty, DxStoreAccessReply.GetProperty>(service.GetProperty)));
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001378C File Offset: 0x0001198C
		public DxStoreAccessReply.GetAllProperties GetAllProperties(DxStoreAccessRequest.GetAllProperties request, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreAccess> runner = WcfStoreAccessClient.Runner;
			CachedChannelFactory<IDxStoreAccess> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			return runner.Execute<DxStoreAccessReply.GetAllProperties>(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, (IDxStoreAccess service) => this.TraceRequests<DxStoreAccessRequest.GetAllProperties, DxStoreAccessReply.GetAllProperties>(request, new Func<DxStoreAccessRequest.GetAllProperties, DxStoreAccessReply.GetAllProperties>(service.GetAllProperties)));
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00013810 File Offset: 0x00011A10
		public DxStoreAccessReply.GetPropertyNames GetPropertyNames(DxStoreAccessRequest.GetPropertyNames request, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreAccess> runner = WcfStoreAccessClient.Runner;
			CachedChannelFactory<IDxStoreAccess> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			return runner.Execute<DxStoreAccessReply.GetPropertyNames>(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, (IDxStoreAccess service) => this.TraceRequests<DxStoreAccessRequest.GetPropertyNames, DxStoreAccessReply.GetPropertyNames>(request, new Func<DxStoreAccessRequest.GetPropertyNames, DxStoreAccessReply.GetPropertyNames>(service.GetPropertyNames)));
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00013894 File Offset: 0x00011A94
		public DxStoreAccessReply.GetSubkeyNames GetSubkeyNames(DxStoreAccessRequest.GetSubkeyNames request, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreAccess> runner = WcfStoreAccessClient.Runner;
			CachedChannelFactory<IDxStoreAccess> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			return runner.Execute<DxStoreAccessReply.GetSubkeyNames>(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, (IDxStoreAccess service) => this.TraceRequests<DxStoreAccessRequest.GetSubkeyNames, DxStoreAccessReply.GetSubkeyNames>(request, new Func<DxStoreAccessRequest.GetSubkeyNames, DxStoreAccessReply.GetSubkeyNames>(service.GetSubkeyNames)));
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000138F0 File Offset: 0x00011AF0
		public TRep TraceRequests<TReq, TRep>(TReq request, Func<TReq, TRep> action) where TReq : DxStoreAccessRequest where TRep : DxStoreAccessReply
		{
			Trace accessClientTracer = ExTraceGlobals.AccessClientTracer;
			bool flag = accessClientTracer.IsTraceEnabled(TraceType.DebugTrace);
			bool flag2 = accessClientTracer.IsTraceEnabled(TraceType.ErrorTrace);
			if (!flag && !flag2)
			{
				return action(request);
			}
			TRep trep = default(TRep);
			try
			{
				if (flag)
				{
					string arg = Utils.SerializeObjectToJsonString<TReq>(request, false, true) ?? "<serialization error>";
					accessClientTracer.TraceDebug<string, string>(0L, "Sending Request: {0}{1}", typeof(TReq).Name, arg);
				}
				trep = action(request);
			}
			catch (Exception ex)
			{
				if (flag2)
				{
					string arg2 = "<none>";
					FaultException<DxStoreServerFault> faultException = ex as FaultException<DxStoreServerFault>;
					if (faultException != null)
					{
						DxStoreServerFault detail = faultException.Detail;
						arg2 = (Utils.SerializeObjectToJsonString<DxStoreServerFault>(detail, false, true) ?? "<serialization error>");
					}
					accessClientTracer.TraceDebug<string, string, Exception>(0L, "Send failed - Request: {0} - Fault: {1} - Exception: {2}", typeof(TReq).Name, arg2, ex);
				}
				throw;
			}
			if (flag)
			{
				string arg3 = (trep != null) ? (Utils.SerializeObjectToJsonString<TRep>(trep, false, true) ?? "<serialization error>") : "<null>";
				accessClientTracer.TraceDebug<string, string>(0L, "Received reply: {0}{1}", typeof(TRep).Name, arg3);
			}
			return trep;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.Globalization;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceModel;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DiagnosticsAggregation;
using Microsoft.Exchange.Net.DiagnosticsAggregation;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Servicelets.DiagnosticsAggregation.Messages;
using Microsoft.Exchange.Transport.DiagnosticsAggregationService;

namespace Microsoft.Exchange.Servicelets.DiagnosticsAggregation
{
	// Token: 0x02000006 RID: 6
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = false)]
	internal class DiagnosticsAggregationServiceImpl : IDiagnosticsAggregationService
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002448 File Offset: 0x00000648
		public DiagnosticsAggregationServiceImpl()
		{
			this.localQueuesDataProvider = DiagnosticsAggregationServicelet.GetLocalQueuesDataProvider();
			this.groupQueuesDataProvider = DiagnosticsAggregationServicelet.GetGroupQueuesDataProvider();
			this.log = DiagnosticsAggregationServicelet.Log;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002471 File Offset: 0x00000671
		public DiagnosticsAggregationServiceImpl(ILocalQueuesDataProvider localQueuesDataProvider, IGroupQueuesDataProvider groupQueuesDataProvider, DiagnosticsAggregationLog log)
		{
			this.localQueuesDataProvider = localQueuesDataProvider;
			this.groupQueuesDataProvider = groupQueuesDataProvider;
			this.log = log;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002664 File Offset: 0x00000864
		public LocalViewResponse GetLocalView(LocalViewRequest request)
		{
			LocalViewResponse response = null;
			this.ServiceRequest(delegate
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceFunction<string, string, string>(0L, "GetLocalView called. ClientMachineName={0}; ClientProcessName={1}; ClientProcessId={2}", (request.ClientInformation == null) ? string.Empty : request.ClientInformation.ClientMachineName, (request.ClientInformation == null) ? string.Empty : request.ClientInformation.ClientProcessName, (request.ClientInformation == null) ? string.Empty : request.ClientInformation.ClientProcessId.ToString());
				this.log.LogOperationFromClient(DiagnosticsAggregationEvent.LocalViewRequestReceived, request.ClientInformation, null, "");
				DiagnosticsAggregationServiceImpl.VerifyParameterIsNotNull(request, "request");
				DiagnosticsAggregationServiceImpl.VerifyParameterIsNotNullOrEmpty(request.RequestType, "request.RequestType");
				RequestType requestType;
				bool flag = Enum.TryParse<RequestType>(request.RequestType, out requestType);
				if (!flag || requestType != RequestType.Queues)
				{
					throw DiagnosticsAggregationServiceImpl.NewUnsupportedParameterFault(request.RequestType, "request.RequestType");
				}
				ServerQueuesSnapshot localServerQueues = this.localQueuesDataProvider.GetLocalServerQueues();
				if (localServerQueues.IsEmpty())
				{
					throw DiagnosticsAggregationServiceImpl.NewFault(ErrorCode.LocalQueueDataNotAvailable, localServerQueues.LastError);
				}
				string message;
				if (this.LocalQueueDataTooOld(localServerQueues.TimeStampOfQueues, out message))
				{
					throw DiagnosticsAggregationServiceImpl.NewFault(ErrorCode.LocalQueueDataTooOld, message);
				}
				response = new LocalViewResponse(localServerQueues.GetServerSnapshotStatus());
				response.QueueLocalViewResponse = new QueueLocalViewResponse(new List<LocalQueueInfo>(localServerQueues.Queues), localServerQueues.TimeStampOfQueues);
				stopwatch.Stop();
				this.log.LogOperationFromClient(DiagnosticsAggregationEvent.LocalViewResponseSent, request.ClientInformation, new TimeSpan?(stopwatch.Elapsed), "");
			}, "GetLocalView", request.ClientInformation);
			return response;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002A28 File Offset: 0x00000C28
		public AggregatedViewResponse GetAggregatedView(AggregatedViewRequest request)
		{
			ExTraceGlobals.DiagnosticsAggregationTracer.TraceFunction<string, string, string>(0L, "GetAggregatedView called. ClientMachineName={0}; ClientProcessName={1}; ClientProcessId={2}", (request.ClientInformation == null) ? string.Empty : request.ClientInformation.ClientMachineName, (request.ClientInformation == null) ? string.Empty : request.ClientInformation.ClientProcessName, (request.ClientInformation == null) ? string.Empty : request.ClientInformation.ClientProcessId.ToString());
			AggregatedViewResponse response = null;
			this.ServiceRequest(delegate
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				this.log.LogOperationFromClient(DiagnosticsAggregationEvent.AggregatedViewRequestReceived, request.ClientInformation, null, "");
				DiagnosticsAggregationServiceImpl.VerifyParameterIsNotNull(request, "request");
				DiagnosticsAggregationServiceImpl.VerifyParameterIsNotNullOrEmpty(request.RequestType, "request.RequestType");
				RequestType requestType;
				bool flag = Enum.TryParse<RequestType>(request.RequestType, out requestType);
				if (!flag || requestType != RequestType.Queues)
				{
					throw DiagnosticsAggregationServiceImpl.NewUnsupportedParameterFault(request.RequestType, "request.RequestType");
				}
				DiagnosticsAggregationServiceImpl.VerifyParameterIsNotNull(request.QueueAggregatedViewRequest, "request.QueueAggregatedViewRequest");
				IQueueFilter filter;
				if (!QueueFilter.TryParse(request.QueueAggregatedViewRequest.QueueFilter, out filter))
				{
					throw DiagnosticsAggregationServiceImpl.NewInvalidParameterFault(request.QueueAggregatedViewRequest.QueueFilter, "request.QueueAggregatedViewRequest.QueueFilter");
				}
				DiagnosticsAggregationServiceImpl.VerifyParameterIsNotNullOrEmpty(request.QueueAggregatedViewRequest.GroupByKey, "request.QueueAggregatedViewRequest.GroupByKey");
				QueueDigestGroupBy groupByKey;
				if (!Enum.TryParse<QueueDigestGroupBy>(request.QueueAggregatedViewRequest.GroupByKey, out groupByKey))
				{
					throw DiagnosticsAggregationServiceImpl.NewUnsupportedParameterFault(request.QueueAggregatedViewRequest.GroupByKey, "request.QueueAggregatedViewRequest.GroupByKey");
				}
				DiagnosticsAggregationServiceImpl.VerifyParameterIsNotNullOrEmpty(request.QueueAggregatedViewRequest.DetailsLevel, "request.QueueAggregatedViewRequest.DetailsLevel");
				DetailsLevel detailsLevel;
				if (!Enum.TryParse<DetailsLevel>(request.QueueAggregatedViewRequest.DetailsLevel, out detailsLevel))
				{
					throw DiagnosticsAggregationServiceImpl.NewUnsupportedParameterFault(request.QueueAggregatedViewRequest.DetailsLevel, "request.QueueAggregatedViewRequest.DetailsLevel");
				}
				QueueAggregator queueAggregator = new QueueAggregator(groupByKey, detailsLevel, filter, new TimeSpan?(this.GetTimeSpanForQueueDataBeingCurrent()));
				bool flag2 = request.ServersToInclude != null && request.ServersToInclude.Count > 0;
				HashSet<string> hashSet = flag2 ? new HashSet<string>(request.ServersToInclude, StringComparer.InvariantCultureIgnoreCase) : new HashSet<string>();
				IDictionary<ADObjectId, ServerQueuesSnapshot> currentGroupServerToQueuesMap = this.groupQueuesDataProvider.GetCurrentGroupServerToQueuesMap();
				currentGroupServerToQueuesMap.Add(this.localQueuesDataProvider.GetLocalServerId(), this.localQueuesDataProvider.GetLocalServerQueues());
				List<ServerSnapshotStatus> list = new List<ServerSnapshotStatus>();
				foreach (KeyValuePair<ADObjectId, ServerQueuesSnapshot> keyValuePair in currentGroupServerToQueuesMap)
				{
					ADObjectId key = keyValuePair.Key;
					ServerQueuesSnapshot value = keyValuePair.Value;
					if (!flag2 || hashSet.Contains(key.ToString()))
					{
						string message;
						if (value.IsEmpty())
						{
							value.SetAsFailed(DiagnosticsAggregationServiceImpl.NewFault(ErrorCode.LocalQueueDataNotAvailable, value.LastError).Detail.ToString());
						}
						else if (this.LocalQueueDataTooOld(value.TimeStampOfQueues, out message))
						{
							value.SetAsFailed(DiagnosticsAggregationServiceImpl.NewFault(ErrorCode.LocalQueueDataTooOld, message).Detail.ToString());
						}
						else
						{
							queueAggregator.AddLocalQueues(value.Queues, value.TimeStampOfQueues);
						}
						list.Add(value.GetServerSnapshotStatus());
					}
				}
				response = new AggregatedViewResponse(list);
				response.QueueAggregatedViewResponse = new QueueAggregatedViewResponse(queueAggregator.GetResultSortedByMessageCount(request.ResultSize));
				stopwatch.Stop();
				this.log.LogOperationFromClient(DiagnosticsAggregationEvent.AggregatedViewResponseSent, request.ClientInformation, new TimeSpan?(stopwatch.Elapsed), "");
			}, "GetAggregatedView", request.ClientInformation);
			return response;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002AFD File Offset: 0x00000CFD
		protected virtual TimeSpan GetTimeSpanForQueueDataBeingCurrent()
		{
			return DiagnosticsAggregationServicelet.Config.TimeSpanForQueueDataBeingCurrent;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002B0C File Offset: 0x00000D0C
		protected virtual bool LocalQueueDataTooOld(DateTime timeStampOfQueues, out string errorMessage)
		{
			DateTime utcNow = DateTime.UtcNow;
			TimeSpan timeSpanForQueueDataBeingCurrent = DiagnosticsAggregationServicelet.Config.TimeSpanForQueueDataBeingCurrent;
			TimeSpan timeSpanForQueueDataBeingStale = DiagnosticsAggregationServicelet.Config.TimeSpanForQueueDataBeingStale;
			bool result;
			if (timeStampOfQueues < utcNow - (timeSpanForQueueDataBeingCurrent + timeSpanForQueueDataBeingStale))
			{
				result = true;
				errorMessage = string.Format("timestamp of queues: {0}, current time utc: {1}, TimeSpanForQueueDataBeingCurrent: {2}, TimeSpanForQueueDataBeingStale: {3}", new object[]
				{
					timeStampOfQueues,
					utcNow,
					timeSpanForQueueDataBeingCurrent,
					timeSpanForQueueDataBeingStale
				});
			}
			else
			{
				result = false;
				errorMessage = string.Empty;
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002B98 File Offset: 0x00000D98
		protected virtual void HandleUnHandledException(string operationName, ClientInformation clientInfo, Exception e)
		{
			ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<string, Exception>(0L, "Operation {0} encountered exception {1}", operationName, e);
			DiagnosticsAggregationServicelet.EventLog.LogEvent(MSExchangeDiagnosticsAggregationEventLogConstants.Tuple_DiagnosticsAggregationServiceUnexpectedException, null, new object[]
			{
				operationName,
				e.ToString()
			});
			DiagnosticsAggregationEvent evt = (operationName == "GetLocalView") ? DiagnosticsAggregationEvent.LocalViewRequestReceivedFailed : DiagnosticsAggregationEvent.AggregatedViewRequestReceivedFailed;
			this.log.LogOperationFromClient(evt, clientInfo, null, e.ToString());
			ExWatson.SendReportAndCrashOnAnotherThread(e);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002C14 File Offset: 0x00000E14
		protected virtual void CheckClientAuthorization()
		{
			ServiceSecurityContext serviceSecurityContext = ServiceSecurityContext.Current;
			bool flag = false;
			if (serviceSecurityContext == null)
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError((long)this.GetHashCode(), "ServiceSecurityContext is null");
			}
			else if (serviceSecurityContext.WindowsIdentity == null)
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError((long)this.GetHashCode(), "ServiceSecurityContext WindowsIdentity is null");
			}
			else if (!this.HasReadAccessInAd(serviceSecurityContext))
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<string>((long)this.GetHashCode(), "User {0} does not have Read access in AD", ServiceSecurityContext.Current.WindowsIdentity.Name);
			}
			else
			{
				flag = true;
			}
			if (!flag)
			{
				string empty = string.Empty;
				if (serviceSecurityContext != null && serviceSecurityContext.WindowsIdentity != null)
				{
					string name = serviceSecurityContext.WindowsIdentity.Name;
				}
				throw DiagnosticsAggregationServiceImpl.NewFault(ErrorCode.AccessDenied, "Access is Denied");
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002CC4 File Offset: 0x00000EC4
		protected virtual void LogFaultException(string operationName, ClientInformation clientInfo, FaultException<DiagnosticsAggregationFault> faultException)
		{
			ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<string, FaultException<DiagnosticsAggregationFault>>(0L, "Operation {0} encountered exception {1}", operationName, faultException);
			DiagnosticsAggregationEvent evt = (operationName == "GetLocalView") ? DiagnosticsAggregationEvent.LocalViewRequestReceivedFailed : DiagnosticsAggregationEvent.AggregatedViewRequestReceivedFailed;
			this.log.LogOperationFromClient(evt, clientInfo, null, faultException.Detail.ToString());
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002D18 File Offset: 0x00000F18
		private static void VerifyParameterIsNotNull(object parameterValue, string parameterName)
		{
			if (parameterValue == null)
			{
				throw DiagnosticsAggregationServiceImpl.NewInvalidParameterFault(parameterValue, parameterName);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002D25 File Offset: 0x00000F25
		private static void VerifyParameterIsNotNullOrEmpty(string parameterValue, string parameterName)
		{
			if (string.IsNullOrEmpty(parameterValue))
			{
				throw DiagnosticsAggregationServiceImpl.NewInvalidParameterFault(parameterValue, parameterName);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002D38 File Offset: 0x00000F38
		private static FaultException<DiagnosticsAggregationFault> NewUnsupportedParameterFault(object parameterValue, string parameterName)
		{
			string message = string.Format(CultureInfo.InvariantCulture, "parameter [{0}] has an unsupported value [{1}]", new object[]
			{
				parameterName,
				parameterValue
			});
			return DiagnosticsAggregationServiceImpl.NewFault(ErrorCode.UnsupportedParameter, message);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002D6C File Offset: 0x00000F6C
		private static FaultException<DiagnosticsAggregationFault> NewInvalidParameterFault(object parameterValue, string parameterName)
		{
			if (parameterValue == null)
			{
				parameterValue = "<null>";
			}
			else if (object.Equals(parameterValue, string.Empty))
			{
				parameterValue = "<empty_string>";
			}
			string message = string.Format(CultureInfo.InvariantCulture, "parameter [{0}] has an invalid value [{1}]", new object[]
			{
				parameterName,
				parameterValue
			});
			return DiagnosticsAggregationServiceImpl.NewFault(ErrorCode.InvalidParameter, message);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002DC0 File Offset: 0x00000FC0
		private static FaultException<DiagnosticsAggregationFault> NewFault(ErrorCode errorCode, string message)
		{
			return new FaultException<DiagnosticsAggregationFault>(new DiagnosticsAggregationFault(errorCode, message));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002DD0 File Offset: 0x00000FD0
		private void ServiceRequest(DiagnosticsAggregationServiceImpl.ProcessRequestDelegate serviceCall, string operationName, ClientInformation clientInfo)
		{
			try
			{
				this.CheckClientAuthorization();
				serviceCall();
			}
			catch (FaultException<DiagnosticsAggregationFault> faultException)
			{
				this.LogFaultException(operationName, clientInfo, faultException);
				throw;
			}
			catch (Exception e)
			{
				this.HandleUnHandledException(operationName, clientInfo, e);
				throw;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002E20 File Offset: 0x00001020
		private bool HasReadAccessInAd(ServiceSecurityContext context)
		{
			SecurityIdentifier user = context.WindowsIdentity.User;
			bool result;
			using (ClientSecurityContext clientSecurityContext = new ClientSecurityContext(context.WindowsIdentity))
			{
				AccessMask accessMask = (AccessMask)131220;
				try
				{
					AccessMask grantedAccess = (AccessMask)clientSecurityContext.GetGrantedAccess(this.GetSecurityDescriptorToCheckAgainst(), user, accessMask);
					if ((grantedAccess & accessMask) == AccessMask.Open)
					{
						this.TraceAndLogError(ExTraceGlobals.DiagnosticsAggregationTracer, "Access check failed for {0}. Response={1}", new object[]
						{
							context.WindowsIdentity.Name,
							grantedAccess
						});
						result = false;
					}
					else
					{
						result = true;
					}
				}
				catch (ADTransientException ex)
				{
					this.TraceAndLogError(ExTraceGlobals.DiagnosticsAggregationTracer, "AD Transient Exception. Details {0}", new object[]
					{
						ex
					});
					result = false;
				}
				catch (AuthzException ex2)
				{
					this.TraceAndLogError(ExTraceGlobals.DiagnosticsAggregationTracer, "Authorization check failed. Details {0}", new object[]
					{
						ex2
					});
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002F20 File Offset: 0x00001120
		private SecurityDescriptor GetSecurityDescriptorToCheckAgainst()
		{
			if (DiagnosticsAggregationServiceImpl.transportServerSecurity == null)
			{
				Server localServer = DiagnosticsAggregationServicelet.LocalServer;
				RawSecurityDescriptor rawSecurityDescriptor = localServer.ReadSecurityDescriptor();
				if (rawSecurityDescriptor != null)
				{
					try
					{
						ActiveDirectorySecurity activeDirectorySecurity = TransportADUtils.SetupActiveDirectorySecurity(rawSecurityDescriptor);
						DiagnosticsAggregationServiceImpl.transportServerSecurity = new SecurityDescriptor(activeDirectorySecurity.GetSecurityDescriptorBinaryForm());
						return DiagnosticsAggregationServiceImpl.transportServerSecurity;
					}
					catch (OverflowException ex)
					{
						this.TraceAndLogError(ExTraceGlobals.DiagnosticsAggregationTracer, "Encountered exception while setting up Authorization setttings. Details {0}", new object[]
						{
							ex
						});
					}
				}
				DiagnosticsAggregationServiceImpl.transportServerSecurity = SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor);
			}
			return DiagnosticsAggregationServiceImpl.transportServerSecurity;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002FAC File Offset: 0x000011AC
		private void TraceAndLogError(Microsoft.Exchange.Diagnostics.Trace tracer, string format, params object[] parameters)
		{
			tracer.TraceError((long)this.GetHashCode(), format, parameters);
			this.log.Log(DiagnosticsAggregationEvent.ServiceletError, format, parameters);
		}

		// Token: 0x04000022 RID: 34
		private const string GetLocalViewOperationName = "GetLocalView";

		// Token: 0x04000023 RID: 35
		private const string GetAggregatedViewOperationName = "GetAggregatedView";

		// Token: 0x04000024 RID: 36
		private const bool IncludeExceptionDetailInFaults = false;

		// Token: 0x04000025 RID: 37
		private static SecurityDescriptor transportServerSecurity;

		// Token: 0x04000026 RID: 38
		private ILocalQueuesDataProvider localQueuesDataProvider;

		// Token: 0x04000027 RID: 39
		private IGroupQueuesDataProvider groupQueuesDataProvider;

		// Token: 0x04000028 RID: 40
		private DiagnosticsAggregationLog log;

		// Token: 0x02000007 RID: 7
		// (Invoke) Token: 0x0600001E RID: 30
		protected delegate void ProcessRequestDelegate();
	}
}

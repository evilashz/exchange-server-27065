using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation
{
	// Token: 0x020001F1 RID: 497
	internal sealed class ActivityScopeImpl : IActivityScope, IDisposable
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0003B5CE File Offset: 0x000397CE
		// (set) Token: 0x06000E65 RID: 3685 RVA: 0x0003B5D5 File Offset: 0x000397D5
		internal static int? MaxAppendableColumnLength { get; set; }

		// Token: 0x06000E66 RID: 3686 RVA: 0x0003B5E0 File Offset: 0x000397E0
		private ActivityScopeImpl(Guid localId)
		{
			this.LocalId = localId;
			this.ActivityId = Guid.NewGuid();
			int? initialMetadataCapacity = ActivityContext.InitialMetadataCapacity;
			if (initialMetadataCapacity != null)
			{
				this.metadata = new ConcurrentDictionary<Enum, object>(Environment.ProcessorCount * 4, initialMetadataCapacity.Value);
			}
			else
			{
				this.metadata = new ConcurrentDictionary<Enum, object>();
			}
			this.statistics = new ConcurrentDictionary<OperationKey, OperationStatistics>();
			this.status = 0;
			this.activityType = ActivityType.Request;
			this.startTime = DateTime.UtcNow;
			this.endTime = null;
			this.stopwatch = Stopwatch.StartNew();
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0003B685 File Offset: 0x00039885
		// (set) Token: 0x06000E68 RID: 3688 RVA: 0x0003B68D File Offset: 0x0003988D
		public Guid ActivityId
		{
			get
			{
				return this.activityId;
			}
			private set
			{
				DebugContext.SetActivityId(value);
				this.activityId = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0003B69C File Offset: 0x0003989C
		// (set) Token: 0x06000E6A RID: 3690 RVA: 0x0003B6A4 File Offset: 0x000398A4
		public Guid LocalId { get; private set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0003B6AD File Offset: 0x000398AD
		public ActivityContextStatus Status
		{
			get
			{
				return (ActivityContextStatus)this.status;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0003B6B5 File Offset: 0x000398B5
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x0003B6BD File Offset: 0x000398BD
		public ActivityType ActivityType
		{
			get
			{
				return this.activityType;
			}
			internal set
			{
				this.activityType = value;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0003B6C6 File Offset: 0x000398C6
		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0003B6CE File Offset: 0x000398CE
		public DateTime? EndTime
		{
			get
			{
				return this.endTime;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0003B6D6 File Offset: 0x000398D6
		public double TotalMilliseconds
		{
			get
			{
				return (double)this.stopwatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0003B6E4 File Offset: 0x000398E4
		// (set) Token: 0x06000E72 RID: 3698 RVA: 0x0003B6EC File Offset: 0x000398EC
		public object UserState { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0003B6F5 File Offset: 0x000398F5
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x0003B703 File Offset: 0x00039903
		public string UserId
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.UserId);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.UserId, value);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0003B70D File Offset: 0x0003990D
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x0003B71B File Offset: 0x0003991B
		public string Puid
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.Puid);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.Puid, value);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0003B725 File Offset: 0x00039925
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x0003B733 File Offset: 0x00039933
		public string UserEmail
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.UserEmail);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.UserEmail, value);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0003B73D File Offset: 0x0003993D
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x0003B74B File Offset: 0x0003994B
		public string AuthenticationType
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.AuthenticationType);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.AuthenticationType, value);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0003B755 File Offset: 0x00039955
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x0003B763 File Offset: 0x00039963
		public string AuthenticationToken
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.AuthenticationToken);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.AuthenticationToken, value);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0003B76D File Offset: 0x0003996D
		// (set) Token: 0x06000E7E RID: 3710 RVA: 0x0003B77B File Offset: 0x0003997B
		public string TenantId
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.TenantId);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.TenantId, value);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0003B785 File Offset: 0x00039985
		// (set) Token: 0x06000E80 RID: 3712 RVA: 0x0003B793 File Offset: 0x00039993
		public string TenantType
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.TenantType);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.TenantType, value);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0003B79D File Offset: 0x0003999D
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x0003B7AB File Offset: 0x000399AB
		public string Component
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.Component);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.Component, value);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0003B7B5 File Offset: 0x000399B5
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x0003B7C3 File Offset: 0x000399C3
		public string ComponentInstance
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.ComponentInstance);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.ComponentInstance, value);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0003B7CD File Offset: 0x000399CD
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x0003B7DC File Offset: 0x000399DC
		public string Feature
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.Feature);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.Feature, value);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0003B7E7 File Offset: 0x000399E7
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x0003B7F6 File Offset: 0x000399F6
		public string Protocol
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.Protocol);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.Protocol, value);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0003B801 File Offset: 0x00039A01
		// (set) Token: 0x06000E8A RID: 3722 RVA: 0x0003B810 File Offset: 0x00039A10
		public string ClientInfo
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.ClientInfo);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.ClientInfo, value);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0003B81B File Offset: 0x00039A1B
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x0003B82A File Offset: 0x00039A2A
		public string ClientRequestId
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.ClientRequestId);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.ClientRequestId, value);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0003B835 File Offset: 0x00039A35
		// (set) Token: 0x06000E8E RID: 3726 RVA: 0x0003B844 File Offset: 0x00039A44
		public string ReturnClientRequestId
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.ReturnClientRequestId);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.ReturnClientRequestId, value);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0003B84F File Offset: 0x00039A4F
		// (set) Token: 0x06000E90 RID: 3728 RVA: 0x0003B85E File Offset: 0x00039A5E
		public string Action
		{
			get
			{
				return (string)this.GetStandardProperty(ActivityStandardMetadata.Action);
			}
			set
			{
				this.SetStandardProperty(ActivityStandardMetadata.Action, value);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x0003B869 File Offset: 0x00039A69
		public IEnumerable<KeyValuePair<Enum, object>> Metadata
		{
			get
			{
				return this.metadata;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0003B871 File Offset: 0x00039A71
		public IEnumerable<KeyValuePair<OperationKey, OperationStatistics>> Statistics
		{
			get
			{
				return this.statistics;
			}
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003B884 File Offset: 0x00039A84
		public ActivityContextState Suspend()
		{
			Func<string, LocalizedString> func = (string debugInfo) => DiagnosticsResources.ExceptionMustStartBeforeSuspend(debugInfo);
			if (this.status == 2)
			{
				try
				{
					throw new ActivityContextException(func(DebugContext.GetDebugInfo()));
				}
				catch (ActivityContextException)
				{
				}
				return new ActivityContextState(this, this.metadata);
			}
			this.TransitionToFinalStatus(ActivityContextStatus.ActivitySuspended, ActivityContext.OnSuspendEventArgs);
			return new ActivityContextState(this, this.metadata);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003B908 File Offset: 0x00039B08
		public void End()
		{
			this.TransitionToFinalStatus(ActivityContextStatus.ActivityEnded, ActivityContext.OnEndEventArgs);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003B918 File Offset: 0x00039B18
		public bool AddOperation(ActivityOperationType activityOperationType, string instance, float value = 0f, int count = 1)
		{
			if (ExTraceGlobals.ActivityContextTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ActivityContextTracer.TraceDebug((long)this.LocalId.GetHashCode(), "ActivityScopeImpl.AddOperation - activityOperationType = {0}, instance = {1}, value = {2}. Key = {3}", new object[]
				{
					activityOperationType,
					instance ?? "<null>",
					value,
					this.LocalId
				});
			}
			if (!this.VerifyIsStarted())
			{
				return false;
			}
			OperationStatistics operationStatistics = this.GetOperationStatistics(activityOperationType, instance);
			if (this.Status == ActivityContextStatus.ActivityStarted)
			{
				operationStatistics.AddCall(value, count);
				return true;
			}
			return false;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003B9B4 File Offset: 0x00039BB4
		public void SetProperty(Enum property, string value)
		{
			this.SetPropertyObject(property, value);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003B9C0 File Offset: 0x00039BC0
		public bool AppendToProperty(Enum property, string value)
		{
			if (!this.IsRegisteredProperty(property))
			{
				return false;
			}
			if (string.IsNullOrEmpty(value))
			{
				return true;
			}
			object obj;
			StringBuilder stringBuilder;
			if (this.metadata.TryGetValue(property, out obj))
			{
				stringBuilder = new StringBuilder((string)obj);
			}
			else
			{
				stringBuilder = new StringBuilder();
			}
			if (ActivityScopeImpl.MaxAppendableColumnLength != null)
			{
				if (stringBuilder.Length > ActivityScopeImpl.MaxAppendableColumnLength)
				{
					return true;
				}
				if (stringBuilder.Length + value.Length > ActivityScopeImpl.MaxAppendableColumnLength.Value)
				{
					stringBuilder.Append(value.Substring(0, ActivityScopeImpl.MaxAppendableColumnLength.Value - stringBuilder.Length));
					stringBuilder.Append("...");
				}
				else
				{
					stringBuilder.Append(value);
				}
			}
			else
			{
				stringBuilder.Append(value);
			}
			this.metadata[property] = stringBuilder.ToString();
			return true;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0003BAB0 File Offset: 0x00039CB0
		public string GetProperty(Enum property)
		{
			object propertyObject = this.GetPropertyObject(property);
			if (propertyObject == null)
			{
				return null;
			}
			return LogRowFormatter.Format(propertyObject);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0003BAD0 File Offset: 0x00039CD0
		public List<KeyValuePair<string, object>> GetFormattableMetadata()
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			foreach (KeyValuePair<Enum, object> keyValuePair in this.Metadata)
			{
				string key = ActivityContext.LookupEnumName(keyValuePair.Key);
				list.Add(new KeyValuePair<string, object>(key, keyValuePair.Value));
			}
			string text = ActivityContext.LookupEnumName(WlmMetaData.TimeOnServer);
			if (this.Status != ActivityContextStatus.ActivityStarted && text != null)
			{
				list.Add(new KeyValuePair<string, object>(text, this.TotalMilliseconds));
			}
			return list;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003BD4C File Offset: 0x00039F4C
		public IEnumerable<KeyValuePair<string, object>> GetFormattableMetadata(IEnumerable<Enum> properties)
		{
			foreach (Enum property in properties)
			{
				if (this.metadata.ContainsKey(property))
				{
					string key = ActivityContext.LookupEnumName(property);
					yield return new KeyValuePair<string, object>(key, this.metadata[property]);
				}
			}
			yield break;
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0003BD70 File Offset: 0x00039F70
		public List<KeyValuePair<string, object>> GetFormattableStatistics()
		{
			return ActivityScopeImpl.GetFormattableStatistics(this.statistics);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0003BD80 File Offset: 0x00039F80
		public void UpdateFromMessage(OperationContext wcfOperationContext)
		{
			ActivityContextState activityContextState = ActivityContextState.DeserializeFrom(wcfOperationContext);
			if (activityContextState != null)
			{
				this.UpdateFromState(activityContextState);
			}
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0003BDA0 File Offset: 0x00039FA0
		public void UpdateFromMessage(HttpRequestMessageProperty wcfMessage)
		{
			ActivityContextState activityContextState = ActivityContextState.DeserializeFrom(wcfMessage);
			if (activityContextState != null)
			{
				this.UpdateFromState(activityContextState);
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003BDBE File Offset: 0x00039FBE
		public void UpdateFromMessage(HttpRequest httpRequest)
		{
			this.UpdateFromMessage(new HttpRequestWrapper(httpRequest));
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003BDCC File Offset: 0x00039FCC
		public void UpdateFromMessage(HttpRequestBase httpRequestBase)
		{
			ActivityContextState activityContextState = ActivityContextState.DeserializeFrom(httpRequestBase);
			if (activityContextState != null)
			{
				this.UpdateFromState(activityContextState);
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003BDEC File Offset: 0x00039FEC
		public void SerializeTo(HttpRequestMessageProperty wcfMessage)
		{
			if (wcfMessage == null)
			{
				throw new ArgumentNullException("wcfMessage");
			}
			string serializedScope = this.GetSerializedScope();
			if (wcfMessage.Headers["X-MSExchangeActivityCtx"] == null)
			{
				wcfMessage.Headers.Add("X-MSExchangeActivityCtx", serializedScope);
				return;
			}
			wcfMessage.Headers["X-MSExchangeActivityCtx"] = serializedScope;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003BE48 File Offset: 0x0003A048
		public void SerializeTo(OperationContext wcfOperationContext)
		{
			if (wcfOperationContext == null)
			{
				throw new ArgumentNullException("wcfOperationContext");
			}
			string serializedScope = this.GetSerializedScope();
			MessageHeader header = MessageHeader.CreateHeader("X-MSExchangeActivityCtx", string.Empty, serializedScope);
			wcfOperationContext.OutgoingMessageHeaders.Add(header);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003BE8C File Offset: 0x0003A08C
		public void SerializeTo(HttpWebRequest httpWebRequest)
		{
			if (httpWebRequest == null)
			{
				throw new ArgumentNullException("httpWebRequest");
			}
			string serializedScope = this.GetSerializedScope();
			if (httpWebRequest.Headers["X-MSExchangeActivityCtx"] == null)
			{
				httpWebRequest.Headers.Add("X-MSExchangeActivityCtx", serializedScope);
				return;
			}
			httpWebRequest.Headers["X-MSExchangeActivityCtx"] = serializedScope;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0003BEE8 File Offset: 0x0003A0E8
		public void SerializeTo(HttpResponse httpResponse)
		{
			if (httpResponse == null)
			{
				throw new ArgumentNullException("httpResponse");
			}
			httpResponse.Headers["request-id"] = this.activityId.ToString();
			if (!string.IsNullOrWhiteSpace(this.ClientRequestId) && string.Equals("true", this.ReturnClientRequestId, StringComparison.OrdinalIgnoreCase))
			{
				httpResponse.Headers["client-request-id"] = this.ClientRequestId;
			}
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0003BF5C File Offset: 0x0003A15C
		public void SerializeMinimalTo(HttpWebRequest httpWebRequest)
		{
			if (httpWebRequest == null)
			{
				throw new ArgumentNullException("httpWebRequest");
			}
			string minimalSerializedScope = this.GetMinimalSerializedScope();
			if (httpWebRequest.Headers["X-MSExchangeActivityCtx"] == null)
			{
				httpWebRequest.Headers.Add("X-MSExchangeActivityCtx", minimalSerializedScope);
				return;
			}
			httpWebRequest.Headers["X-MSExchangeActivityCtx"] = minimalSerializedScope;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003BFB8 File Offset: 0x0003A1B8
		public void SerializeMinimalTo(HttpRequestBase httpRequest)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			string minimalSerializedScope = this.GetMinimalSerializedScope();
			if (httpRequest.Headers["X-MSExchangeActivityCtx"] == null)
			{
				httpRequest.Headers.Add("X-MSExchangeActivityCtx", minimalSerializedScope);
				return;
			}
			httpRequest.Headers["X-MSExchangeActivityCtx"] = minimalSerializedScope;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003C014 File Offset: 0x0003A214
		public AggregatedOperationStatistics TakeStatisticsSnapshot(AggregatedOperationType type)
		{
			switch (type)
			{
			case AggregatedOperationType.ADCalls:
				return this.TakeADStatisticsSnapshot();
			case AggregatedOperationType.StoreRPCs:
				return this.TakeStoreRpcStatisticsSnapshot();
			case AggregatedOperationType.ADObjToExchObjLatency:
				return this.TakeADObjToExchObjStatisticsSnapshot();
			default:
				throw new NotSupportedException("Unknown type: " + type);
			}
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003C064 File Offset: 0x0003A264
		public void Dispose()
		{
			ActivityScopeImpl activityScopeImpl;
			try
			{
				ActivityScopeImpl.scopeCacheLock.EnterWriteLock();
				if (ActivityScopeImpl.scopeCache.TryGetValue(this.LocalId, out activityScopeImpl))
				{
					ActivityScopeImpl.scopeCache.Remove(this.LocalId);
				}
			}
			finally
			{
				try
				{
					ActivityScopeImpl.scopeCacheLock.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			ExTraceGlobals.ActivityContextTracer.TraceDebug<bool, Guid>((long)this.LocalId.GetHashCode(), "ActivityScopeImpl.Remove - TryRemove removed an object - {0}. Key = {1}", activityScopeImpl != null, this.LocalId);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003C100 File Offset: 0x0003A300
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Id");
			stringBuilder.Append(ActivityContextState.SerializedKeyValueDelimiter[0]);
			stringBuilder.Append(this.ActivityId.ToString("D"));
			foreach (KeyValuePair<Enum, object> keyValuePair in this.Metadata)
			{
				if (keyValuePair.Value != null)
				{
					stringBuilder.Append(ActivityContextState.SerializedElementDelimiter[0]);
					stringBuilder.Append(ActivityContext.LookupEnumName(keyValuePair.Key));
					stringBuilder.Append(ActivityContextState.SerializedKeyValueDelimiter[0]);
					stringBuilder.Append(LogRowFormatter.Format(keyValuePair.Value));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003C1D4 File Offset: 0x0003A3D4
		internal static ActivityScope AddActivityScope(ActivityContextState activityContextState)
		{
			Guid guid = Guid.NewGuid();
			ActivityScope result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				try
				{
					ActivityScopeImpl.scopeCacheLock.EnterWriteLock();
					ActivityScopeImpl activityScopeImpl = new ActivityScopeImpl(guid);
					disposeGuard.Add<ActivityScopeImpl>(activityScopeImpl);
					ActivityScopeImpl.scopeCache.Add(guid, activityScopeImpl);
					ActivityScope activityScope = new ActivityScope(activityScopeImpl);
					disposeGuard.Add<ActivityScope>(activityScope);
					activityScopeImpl.UpdateFromState(activityContextState);
					SingleContext.Singleton.LocalId = new Guid?(guid);
					SingleContext.Singleton.SetId();
					disposeGuard.Success();
					result = activityScope;
				}
				finally
				{
					try
					{
						ActivityScopeImpl.scopeCacheLock.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
			}
			return result;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003C2A0 File Offset: 0x0003A4A0
		internal static IActivityScope GetActivityScope(Guid key)
		{
			IActivityScope activityScope = null;
			ActivityScopeImpl activityScopeImpl = null;
			try
			{
				ActivityScopeImpl.scopeCacheLock.EnterReadLock();
				if (ActivityScopeImpl.scopeCache.TryGetValue(key, out activityScopeImpl))
				{
					activityScope = activityScopeImpl;
				}
			}
			finally
			{
				try
				{
					ActivityScopeImpl.scopeCacheLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			if (activityScope != null)
			{
				return activityScope;
			}
			return null;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003C300 File Offset: 0x0003A500
		internal static ActivityScopeImpl GetScopeImpl(Guid key)
		{
			ActivityScopeImpl activityScopeImpl;
			ActivityScopeImpl.scopeCache.TryGetValue(key, out activityScopeImpl);
			ExTraceGlobals.ActivityContextTracer.TraceDebug<bool>((long)key.GetHashCode(), "ActivityScopeImpl.GetScopeImpl - TryGetValue found ActivityScopeImpl object - {0}", activityScopeImpl != null);
			return activityScopeImpl;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003C340 File Offset: 0x0003A540
		internal static IEnumerable<KeyValuePair<Guid, ActivityScopeImpl>> GetScopeCacheForUnitTesting()
		{
			return ActivityScopeImpl.scopeCache;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003C348 File Offset: 0x0003A548
		internal static List<KeyValuePair<string, object>> GetFormattableStatistics(IEnumerable<KeyValuePair<OperationKey, OperationStatistics>> statistics)
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			foreach (KeyValuePair<OperationKey, OperationStatistics> keyValuePair in statistics)
			{
				keyValuePair.Value.AppendStatistics(keyValuePair.Key, list);
			}
			return list;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003C3A4 File Offset: 0x0003A5A4
		internal void RemoveInstrInstances()
		{
			foreach (KeyValuePair<OperationKey, OperationStatistics> keyValuePair in this.statistics)
			{
				if (object.ReferenceEquals(keyValuePair.Key.Instance, "INSTR"))
				{
					OperationStatistics operationStatistics;
					this.statistics.TryRemove(keyValuePair.Key, out operationStatistics);
				}
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003C418 File Offset: 0x0003A618
		internal void SetPropertyObject(Enum property, object value)
		{
			Enum key;
			if (!ActivityContext.TryGetPreboxedEnum(property, out key))
			{
				return;
			}
			if (value != null)
			{
				this.metadata[key] = value;
				return;
			}
			this.metadata.TryRemove(property, out value);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003C450 File Offset: 0x0003A650
		internal object GetPropertyObject(Enum property)
		{
			object result = null;
			if (property.GetType() == typeof(ActivityReadonlyMetadata))
			{
				switch ((ActivityReadonlyMetadata)property)
				{
				case ActivityReadonlyMetadata.ActivityId:
					result = this.ActivityId;
					break;
				case ActivityReadonlyMetadata.StartTime:
					result = this.StartTime;
					break;
				case ActivityReadonlyMetadata.EndTime:
					result = this.EndTime;
					break;
				case ActivityReadonlyMetadata.TotalMilliseconds:
					result = this.TotalMilliseconds;
					break;
				}
			}
			else
			{
				if (!this.IsRegisteredProperty(property))
				{
					return null;
				}
				this.metadata.TryGetValue(property, out result);
			}
			return result;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003C4EC File Offset: 0x0003A6EC
		internal string GetSerializedScope()
		{
			return string.Concat(new object[]
			{
				"V",
				ActivityContextState.SerializedKeyValueDelimiter[0],
				ActivityContextState.SerializationVersion,
				ActivityContextState.SerializedElementDelimiter[0],
				"Id",
				ActivityContextState.SerializedKeyValueDelimiter[0],
				this.ActivityId.ToString("D"),
				ActivityContextState.SerializedElementDelimiter[0],
				"C",
				ActivityContextState.SerializedKeyValueDelimiter[0],
				HttpUtility.UrlEncode(this.Component),
				ActivityContextState.SerializedElementDelimiter[0],
				"P",
				ActivityContextState.SerializedKeyValueDelimiter[0],
				this.GetSerializedPayload()
			});
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003C5CB File Offset: 0x0003A7CB
		internal string GetMinimalSerializedScope()
		{
			return ActivityContextState.GetMinimalSerializedScope(this.ActivityId);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003C5D8 File Offset: 0x0003A7D8
		internal void UpdateFromState(ActivityContextState state)
		{
			if (state != null)
			{
				if (state.ActivityId != null)
				{
					this.ActivityId = state.ActivityId.Value;
					this.ActivityType = state.ActivityType;
				}
				if (state.Properties != null)
				{
					foreach (KeyValuePair<Enum, object> keyValuePair in state.Properties)
					{
						this.SetPropertyObject(keyValuePair.Key, keyValuePair.Value);
					}
				}
				DebugContext.UpdateFrom(this);
			}
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003C674 File Offset: 0x0003A874
		private bool IsRegisteredProperty(Enum key)
		{
			return null != ActivityContext.LookupEnumName(key);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003C682 File Offset: 0x0003A882
		private bool VerifyIsStarted()
		{
			return this.Status == ActivityContextStatus.ActivityStarted;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003C68F File Offset: 0x0003A88F
		private void SetStandardProperty(ActivityStandardMetadata property, object value)
		{
			this.SetPropertyObject(property, value);
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0003C69E File Offset: 0x0003A89E
		private object GetStandardProperty(ActivityStandardMetadata property)
		{
			return this.GetPropertyObject(property);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0003C6B4 File Offset: 0x0003A8B4
		private OperationStatistics GetOperationStatistics(ActivityOperationType activityOperationType, string instance)
		{
			OperationKey key = new OperationKey(activityOperationType, instance);
			Func<OperationKey, OperationStatistics> valueFactory = (OperationKey operationKey) => OperationStatistics.GetInstance(operationKey);
			return this.statistics.GetOrAdd(key, valueFactory);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0003C6F8 File Offset: 0x0003A8F8
		private void TransitionToFinalStatus(ActivityContextStatus status, ActivityEventArgs eventArgs)
		{
			bool flag = false;
			try
			{
				int num;
				try
				{
					this.spinLock.Enter(ref flag);
					num = this.status;
					this.status = (int)status;
				}
				finally
				{
					if (flag)
					{
						this.spinLock.Exit();
					}
				}
				if (num == 0)
				{
					this.stopwatch.Stop();
					this.endTime = new DateTime?(DateTime.UtcNow);
					ExTraceGlobals.ActivityContextTracer.TraceDebug<Guid, ActivityContextStatus>((long)this.LocalId.GetHashCode(), "ActivityScopeImpl.TransitionToFinalStatus key {0}, Status={1}.", this.LocalId, status);
					ActivityContext.RaiseEvent(this, eventArgs);
				}
			}
			finally
			{
				this.Dispose();
				ActivityContext.ClearThreadScope();
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0003C7B0 File Offset: 0x0003A9B0
		private string GetSerializedPayload()
		{
			byte[] inArray = null;
			string text = string.Empty;
			MemoryStream memoryStream = null;
			BinaryWriter binaryWriter = null;
			int num = 0;
			string result;
			try
			{
				foreach (Enum @enum in ActivityContextState.PayloadAllowedMetadata)
				{
					string value = ActivityContext.LookupEnumName(@enum);
					string property = this.GetProperty(@enum);
					if (!string.IsNullOrEmpty(property))
					{
						if (memoryStream == null)
						{
							memoryStream = new MemoryStream();
							binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8);
						}
						binaryWriter.Write(value);
						binaryWriter.Write(property);
						if (memoryStream.Position >= 1024L)
						{
							ExTraceGlobals.ActivityContextTracer.TraceDebug(0L, "Reached MaximumPayloadBinaryLength limit, some metadata will not be serialized into payload.");
							break;
						}
						num = (int)memoryStream.Position;
					}
				}
				if (memoryStream != null)
				{
					binaryWriter.Flush();
					memoryStream.Flush();
					inArray = memoryStream.GetBuffer();
				}
				if (num > 0)
				{
					text = Convert.ToBase64String(inArray, 0, num, Base64FormattingOptions.None);
				}
				result = text;
			}
			finally
			{
				if (binaryWriter != null)
				{
					binaryWriter.Dispose();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0003C8C8 File Offset: 0x0003AAC8
		private AggregatedOperationStatistics TakeADStatisticsSnapshot()
		{
			AggregatedOperationStatistics result = new AggregatedOperationStatistics
			{
				Type = AggregatedOperationType.ADCalls
			};
			foreach (KeyValuePair<OperationKey, OperationStatistics> keyValuePair in this.statistics)
			{
				switch (keyValuePair.Key.ActivityOperationType)
				{
				case ActivityOperationType.ADRead:
				case ActivityOperationType.ADSearch:
				case ActivityOperationType.ADWrite:
				{
					AverageOperationStatistics averageOperationStatistics = (AverageOperationStatistics)keyValuePair.Value;
					result.Count += (long)averageOperationStatistics.Count;
					result.TotalMilliseconds += (double)((float)averageOperationStatistics.Count * averageOperationStatistics.CumulativeAverage);
					break;
				}
				}
			}
			return result;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0003C988 File Offset: 0x0003AB88
		private AggregatedOperationStatistics TakeStoreRpcStatisticsSnapshot()
		{
			AggregatedOperationStatistics result = new AggregatedOperationStatistics
			{
				Type = AggregatedOperationType.StoreRPCs
			};
			foreach (KeyValuePair<OperationKey, OperationStatistics> keyValuePair in this.statistics)
			{
				switch (keyValuePair.Key.ActivityOperationType)
				{
				case ActivityOperationType.RpcCount:
				{
					CountOperationStatistics countOperationStatistics = (CountOperationStatistics)keyValuePair.Value;
					result.Count += (long)countOperationStatistics.Count;
					break;
				}
				case ActivityOperationType.RpcLatency:
				{
					TotalOperationStatistics totalOperationStatistics = (TotalOperationStatistics)keyValuePair.Value;
					result.TotalMilliseconds += totalOperationStatistics.Total;
					break;
				}
				}
			}
			return result;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0003CA50 File Offset: 0x0003AC50
		private AggregatedOperationStatistics TakeADObjToExchObjStatisticsSnapshot()
		{
			AggregatedOperationStatistics result = new AggregatedOperationStatistics
			{
				Type = AggregatedOperationType.ADObjToExchObjLatency
			};
			foreach (KeyValuePair<OperationKey, OperationStatistics> keyValuePair in this.statistics)
			{
				ActivityOperationType activityOperationType = keyValuePair.Key.ActivityOperationType;
				if (activityOperationType == ActivityOperationType.ADObjToExchObjLatency)
				{
					AverageOperationStatistics averageOperationStatistics = (AverageOperationStatistics)keyValuePair.Value;
					result.Count += (long)averageOperationStatistics.Count;
					result.TotalMilliseconds += (double)((float)averageOperationStatistics.Count * averageOperationStatistics.CumulativeAverage);
				}
			}
			return result;
		}

		// Token: 0x04000A7C RID: 2684
		private static readonly Dictionary<Guid, ActivityScopeImpl> scopeCache = new Dictionary<Guid, ActivityScopeImpl>();

		// Token: 0x04000A7D RID: 2685
		private static readonly ReaderWriterLockSlim scopeCacheLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

		// Token: 0x04000A7E RID: 2686
		private readonly DateTime startTime;

		// Token: 0x04000A7F RID: 2687
		private readonly ConcurrentDictionary<OperationKey, OperationStatistics> statistics;

		// Token: 0x04000A80 RID: 2688
		private readonly ConcurrentDictionary<Enum, object> metadata;

		// Token: 0x04000A81 RID: 2689
		private DateTime? endTime;

		// Token: 0x04000A82 RID: 2690
		private readonly Stopwatch stopwatch;

		// Token: 0x04000A83 RID: 2691
		private Guid activityId;

		// Token: 0x04000A84 RID: 2692
		private int status;

		// Token: 0x04000A85 RID: 2693
		private ActivityType activityType;

		// Token: 0x04000A86 RID: 2694
		private SpinLock spinLock = new SpinLock(DebugContext.TestOnlyIsDebugBuild);
	}
}

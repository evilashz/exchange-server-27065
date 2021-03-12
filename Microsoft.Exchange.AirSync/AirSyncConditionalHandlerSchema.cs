using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000072 RID: 114
	internal class AirSyncConditionalHandlerSchema : ConditionalHandlerSchema
	{
		// Token: 0x0400044B RID: 1099
		public static readonly SimpleProviderPropertyDefinition FilterChangeSync = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("FilterChangeSync");

		// Token: 0x0400044C RID: 1100
		public static readonly SimpleProviderPropertyDefinition InitialSync = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("InitialSync");

		// Token: 0x0400044D RID: 1101
		public static readonly SimpleProviderPropertyDefinition IcsSync = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("IcsSync");

		// Token: 0x0400044E RID: 1102
		public static readonly SimpleProviderPropertyDefinition ItemQuerySync = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("ItemQuerySync");

		// Token: 0x0400044F RID: 1103
		public static readonly SimpleProviderPropertyDefinition AnyCollectionEmptyWithMoreAvailable = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("AnyCollectionEmptyWithMoreAvailable");

		// Token: 0x04000450 RID: 1104
		public static readonly SimpleProviderPropertyDefinition IsLogicallyEmptyResponse = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("IsLogicallyEmptyResponse");

		// Token: 0x04000451 RID: 1105
		public static readonly SimpleProviderPropertyDefinition PerCallTracing = ConditionalHandlerSchema.BuildStringPropDef("PerCallTracing");

		// Token: 0x04000452 RID: 1106
		public static readonly SimpleProviderPropertyDefinition EmptyRequest = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("EmptyRequest");

		// Token: 0x04000453 RID: 1107
		public static readonly SimpleProviderPropertyDefinition PartialRequest = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("PartialRequest");

		// Token: 0x04000454 RID: 1108
		public static readonly SimpleProviderPropertyDefinition LoadedCachedRequest = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("LoadedCachedRequest");

		// Token: 0x04000455 RID: 1109
		public static readonly SimpleProviderPropertyDefinition HangTimedOut = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("HangTimedOut");

		// Token: 0x04000456 RID: 1110
		public static readonly SimpleProviderPropertyDefinition StolenNM = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("StolenNM");

		// Token: 0x04000457 RID: 1111
		public static readonly SimpleProviderPropertyDefinition SyncHangingHint = ConditionalHandlerSchema.BuildStringPropDef("SyncHangingHint");

		// Token: 0x04000458 RID: 1112
		public static readonly SimpleProviderPropertyDefinition DeviceAccessState = ConditionalHandlerSchema.BuildValueTypePropDef<DeviceAccessState>("DeviceAccessState");

		// Token: 0x04000459 RID: 1113
		public static readonly SimpleProviderPropertyDefinition DeviceAccessStateReason = ConditionalHandlerSchema.BuildValueTypePropDef<DeviceAccessStateReason>("DeviceAccessStateReason");

		// Token: 0x0400045A RID: 1114
		public static readonly SimpleProviderPropertyDefinition DeviceId = ConditionalHandlerSchema.BuildStringPropDef("DeviceId");

		// Token: 0x0400045B RID: 1115
		public static readonly SimpleProviderPropertyDefinition DeviceType = ConditionalHandlerSchema.BuildStringPropDef("DeviceType");

		// Token: 0x0400045C RID: 1116
		public static readonly SimpleProviderPropertyDefinition ProtocolVersion = ConditionalHandlerSchema.BuildStringPropDef("ProtocolVersion");

		// Token: 0x0400045D RID: 1117
		public static readonly SimpleProviderPropertyDefinition ProxyToServer = ConditionalHandlerSchema.BuildStringPropDef("ProxyToServer");

		// Token: 0x0400045E RID: 1118
		public static readonly SimpleProviderPropertyDefinition ProxyFromServer = ConditionalHandlerSchema.BuildStringPropDef("ProxyFromServer");

		// Token: 0x0400045F RID: 1119
		public static readonly SimpleProviderPropertyDefinition WasProxied = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("WasProxied");

		// Token: 0x04000460 RID: 1120
		public static readonly SimpleProviderPropertyDefinition ProxyElapsed = ConditionalHandlerSchema.BuildValueTypePropDef<TimeSpan>("ProxyElapsed");

		// Token: 0x04000461 RID: 1121
		public static readonly SimpleProviderPropertyDefinition TimeTracker = ConditionalHandlerSchema.BuildStringPropDef("TimeTracker");

		// Token: 0x04000462 RID: 1122
		public static readonly SimpleProviderPropertyDefinition XmlRequest = ConditionalHandlerSchema.BuildStringPropDef("XmlRequest");

		// Token: 0x04000463 RID: 1123
		public static readonly SimpleProviderPropertyDefinition XmlResponse = ConditionalHandlerSchema.BuildStringPropDef("XmlResponse");

		// Token: 0x04000464 RID: 1124
		public static readonly SimpleProviderPropertyDefinition ProtocolLoggerData = ConditionalHandlerSchema.BuildStringPropDef("ProtocolLoggerData");

		// Token: 0x04000465 RID: 1125
		public static readonly SimpleProviderPropertyDefinition CompletedWithDelay = ConditionalHandlerSchema.BuildValueTypePropDef<TimeSpan>("CompletedWithDelay");

		// Token: 0x04000466 RID: 1126
		public static readonly SimpleProviderPropertyDefinition WbXmlRequestSize = ConditionalHandlerSchema.BuildValueTypePropDef<int>("WbXmlRequestSize");

		// Token: 0x04000467 RID: 1127
		public static readonly SimpleProviderPropertyDefinition HttpStatus = ConditionalHandlerSchema.BuildValueTypePropDef<int>("HttpStatus");

		// Token: 0x04000468 RID: 1128
		public static readonly SimpleProviderPropertyDefinition EasStatus = ConditionalHandlerSchema.BuildValueTypePropDef<int>("EasStatus");

		// Token: 0x04000469 RID: 1129
		public static readonly SimpleProviderPropertyDefinition ProtocolError = ConditionalHandlerSchema.BuildStringPropDef("ProtocolError");

		// Token: 0x0400046A RID: 1130
		public static readonly SimpleProviderPropertyDefinition EasMaxDevices = ConditionalHandlerSchema.BuildUnlimitedPropDef("EasMaxDevices");

		// Token: 0x0400046B RID: 1131
		public static readonly SimpleProviderPropertyDefinition EasMaxDeviceDeletesPerMonth = ConditionalHandlerSchema.BuildUnlimitedPropDef("EasMaxDeviceDeletesPerMonth");

		// Token: 0x0400046C RID: 1132
		public static readonly SimpleProviderPropertyDefinition EasMaxInactivityForDeviceCleanup = ConditionalHandlerSchema.BuildUnlimitedPropDef("EasMaxInactivityForDeviceCleanup");

		// Token: 0x0400046D RID: 1133
		public static readonly SimpleProviderPropertyDefinition Traces = ConditionalHandlerSchema.BuildStringPropDef("Traces");

		// Token: 0x0400046E RID: 1134
		public static readonly SimpleProviderPropertyDefinition RequestHeaders = ConditionalHandlerSchema.BuildStringPropDef("RequestHeaders");

		// Token: 0x0400046F RID: 1135
		public static readonly SimpleProviderPropertyDefinition ResponseHeaders = ConditionalHandlerSchema.BuildStringPropDef("ResponseHeaders");

		// Token: 0x04000470 RID: 1136
		public static readonly SimpleProviderPropertyDefinition UserWLMData = ConditionalHandlerSchema.BuildRefTypePropDef<UserWorkloadManagerResult>("UserWLMData");

		// Token: 0x04000471 RID: 1137
		public static readonly SimpleProviderPropertyDefinition IsConsumerOrganizationUser = ConditionalHandlerSchema.BuildRefTypePropDef<UserWorkloadManagerResult>("IsConsumerOrganizationUser");
	}
}

using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.RPCHTTP.Messages
{
	// Token: 0x0200000F RID: 15
	public static class MSExchangeRPCHTTPAutoconfigEventLogConstants
	{
		// Token: 0x04000050 RID: 80
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransientException = new ExEventLog.EventTuple(3221227473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000051 RID: 81
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PermanentException = new ExEventLog.EventTuple(3221227474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000052 RID: 82
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyNotInstalled = new ExEventLog.EventTuple(3221227475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000053 RID: 83
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WebSiteNotFound = new ExEventLog.EventTuple(3221227476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000054 RID: 84
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WebSiteNotUnique = new ExEventLog.EventTuple(3221227477U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000055 RID: 85
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AppCmdNotFound = new ExEventLog.EventTuple(3221227478U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000056 RID: 86
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AppCmdRunFailure = new ExEventLog.EventTuple(3221227479U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000057 RID: 87
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyNotEnabled = new ExEventLog.EventTuple(3221227480U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000058 RID: 88
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FileNotAccessible = new ExEventLog.EventTuple(3221489625U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000059 RID: 89
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WebSitesNotConfigured = new ExEventLog.EventTuple(3221227482U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005A RID: 90
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedValidPorts = new ExEventLog.EventTuple(1073744824U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005B RID: 91
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DisabledValidPorts = new ExEventLog.EventTuple(1073744825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005C RID: 92
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedNSPIKey = new ExEventLog.EventTuple(2147486650U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005D RID: 93
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Obsolete_UpdatedAuthFlags = new ExEventLog.EventTuple(1073744827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005E RID: 94
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedHTTPErrors = new ExEventLog.EventTuple(1073744828U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005F RID: 95
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedAllowAnon = new ExEventLog.EventTuple(1073744829U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000060 RID: 96
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EnabledValidPorts = new ExEventLog.EventTuple(1073744830U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000061 RID: 97
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedConcurrentRequestLimit = new ExEventLog.EventTuple(1073744831U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000062 RID: 98
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EnabledClientAccessServers = new ExEventLog.EventTuple(1073744832U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000063 RID: 99
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedClientAccessServers = new ExEventLog.EventTuple(1073744833U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000064 RID: 100
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DisabledClientAccessServers = new ExEventLog.EventTuple(1073744834U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000065 RID: 101
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EnabledClientAccessArray = new ExEventLog.EventTuple(1073744835U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000066 RID: 102
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedClientAccessArray = new ExEventLog.EventTuple(1073744836U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000067 RID: 103
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DisabledClientAccessArray = new ExEventLog.EventTuple(1073744837U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000068 RID: 104
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcHttpLBS_StartSuccess = new ExEventLog.EventTuple(1073744838U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000069 RID: 105
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcHttpLBS_StartFailure = new ExEventLog.EventTuple(3221228487U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006A RID: 106
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcHttpLBS_NotInstalled = new ExEventLog.EventTuple(3221228488U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006B RID: 107
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcHttpLBS_StopSuccess = new ExEventLog.EventTuple(1073744841U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006C RID: 108
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcHttpLBS_StopFailure = new ExEventLog.EventTuple(2147486666U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006D RID: 109
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EnabledValidPorts_AutoGCs = new ExEventLog.EventTuple(1073744843U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006E RID: 110
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedValidPorts_AutoGCs = new ExEventLog.EventTuple(1073744844U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400006F RID: 111
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DisabledValidPorts_AutoGCs = new ExEventLog.EventTuple(1073744845U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000070 RID: 112
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidRegistryValueType = new ExEventLog.EventTuple(3221228494U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000071 RID: 113
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedAuthenticationProviders = new ExEventLog.EventTuple(1073744847U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000072 RID: 114
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedLBSArbitrationMode = new ExEventLog.EventTuple(1073744848U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000073 RID: 115
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedRpcVirtualDirectory = new ExEventLog.EventTuple(1073744849U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000074 RID: 116
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedRpcHttpGeneralSettings = new ExEventLog.EventTuple(1073744850U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000075 RID: 117
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_RunningIn2010RtmFunctionalMode = new ExEventLog.EventTuple(2147486678U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000076 RID: 118
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_RunningIn2010SP1FunctionalMode = new ExEventLog.EventTuple(1073744855U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000077 RID: 119
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_RunningIn2010DatacenterFunctionalMode = new ExEventLog.EventTuple(2147486680U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000078 RID: 120
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EnabledE15ClientAccessServers = new ExEventLog.EventTuple(1073744857U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000079 RID: 121
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedE15ClientAccessServers = new ExEventLog.EventTuple(1073744858U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007A RID: 122
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DisabledE15ClientAccessServers = new ExEventLog.EventTuple(1073744859U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007B RID: 123
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdatedAuthenticationSettings = new ExEventLog.EventTuple(1073744860U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007C RID: 124
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcHttpServiceletSuccessfullyCheckedForUpdatedSettings = new ExEventLog.EventTuple(1073744861U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007D RID: 125
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceAccountNoWorkingCredentials = new ExEventLog.EventTuple(3221229472U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007E RID: 126
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceAccountKerberosError = new ExEventLog.EventTuple(3221229473U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007F RID: 127
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceAccountCredentialException = new ExEventLog.EventTuple(3221229474U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000080 RID: 128
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceAccountCredentialsAdded = new ExEventLog.EventTuple(1073745827U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000081 RID: 129
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceAccountCredentialsRemoved = new ExEventLog.EventTuple(1073745828U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000010 RID: 16
		private enum Category : short
		{
			// Token: 0x04000083 RID: 131
			General = 1,
			// Token: 0x04000084 RID: 132
			ServiceAccount
		}

		// Token: 0x02000011 RID: 17
		internal enum Message : uint
		{
			// Token: 0x04000086 RID: 134
			TransientException = 3221227473U,
			// Token: 0x04000087 RID: 135
			PermanentException,
			// Token: 0x04000088 RID: 136
			ProxyNotInstalled,
			// Token: 0x04000089 RID: 137
			WebSiteNotFound,
			// Token: 0x0400008A RID: 138
			WebSiteNotUnique,
			// Token: 0x0400008B RID: 139
			AppCmdNotFound,
			// Token: 0x0400008C RID: 140
			AppCmdRunFailure,
			// Token: 0x0400008D RID: 141
			ProxyNotEnabled,
			// Token: 0x0400008E RID: 142
			FileNotAccessible = 3221489625U,
			// Token: 0x0400008F RID: 143
			WebSitesNotConfigured = 3221227482U,
			// Token: 0x04000090 RID: 144
			UpdatedValidPorts = 1073744824U,
			// Token: 0x04000091 RID: 145
			DisabledValidPorts,
			// Token: 0x04000092 RID: 146
			UpdatedNSPIKey = 2147486650U,
			// Token: 0x04000093 RID: 147
			Obsolete_UpdatedAuthFlags = 1073744827U,
			// Token: 0x04000094 RID: 148
			UpdatedHTTPErrors,
			// Token: 0x04000095 RID: 149
			UpdatedAllowAnon,
			// Token: 0x04000096 RID: 150
			EnabledValidPorts,
			// Token: 0x04000097 RID: 151
			UpdatedConcurrentRequestLimit,
			// Token: 0x04000098 RID: 152
			EnabledClientAccessServers,
			// Token: 0x04000099 RID: 153
			UpdatedClientAccessServers,
			// Token: 0x0400009A RID: 154
			DisabledClientAccessServers,
			// Token: 0x0400009B RID: 155
			EnabledClientAccessArray,
			// Token: 0x0400009C RID: 156
			UpdatedClientAccessArray,
			// Token: 0x0400009D RID: 157
			DisabledClientAccessArray,
			// Token: 0x0400009E RID: 158
			RpcHttpLBS_StartSuccess,
			// Token: 0x0400009F RID: 159
			RpcHttpLBS_StartFailure = 3221228487U,
			// Token: 0x040000A0 RID: 160
			RpcHttpLBS_NotInstalled,
			// Token: 0x040000A1 RID: 161
			RpcHttpLBS_StopSuccess = 1073744841U,
			// Token: 0x040000A2 RID: 162
			RpcHttpLBS_StopFailure = 2147486666U,
			// Token: 0x040000A3 RID: 163
			EnabledValidPorts_AutoGCs = 1073744843U,
			// Token: 0x040000A4 RID: 164
			UpdatedValidPorts_AutoGCs,
			// Token: 0x040000A5 RID: 165
			DisabledValidPorts_AutoGCs,
			// Token: 0x040000A6 RID: 166
			InvalidRegistryValueType = 3221228494U,
			// Token: 0x040000A7 RID: 167
			UpdatedAuthenticationProviders = 1073744847U,
			// Token: 0x040000A8 RID: 168
			UpdatedLBSArbitrationMode,
			// Token: 0x040000A9 RID: 169
			UpdatedRpcVirtualDirectory,
			// Token: 0x040000AA RID: 170
			UpdatedRpcHttpGeneralSettings,
			// Token: 0x040000AB RID: 171
			RunningIn2010RtmFunctionalMode = 2147486678U,
			// Token: 0x040000AC RID: 172
			RunningIn2010SP1FunctionalMode = 1073744855U,
			// Token: 0x040000AD RID: 173
			RunningIn2010DatacenterFunctionalMode = 2147486680U,
			// Token: 0x040000AE RID: 174
			EnabledE15ClientAccessServers = 1073744857U,
			// Token: 0x040000AF RID: 175
			UpdatedE15ClientAccessServers,
			// Token: 0x040000B0 RID: 176
			DisabledE15ClientAccessServers,
			// Token: 0x040000B1 RID: 177
			UpdatedAuthenticationSettings,
			// Token: 0x040000B2 RID: 178
			RpcHttpServiceletSuccessfullyCheckedForUpdatedSettings,
			// Token: 0x040000B3 RID: 179
			ServiceAccountNoWorkingCredentials = 3221229472U,
			// Token: 0x040000B4 RID: 180
			ServiceAccountKerberosError,
			// Token: 0x040000B5 RID: 181
			ServiceAccountCredentialException,
			// Token: 0x040000B6 RID: 182
			ServiceAccountCredentialsAdded = 1073745827U,
			// Token: 0x040000B7 RID: 183
			ServiceAccountCredentialsRemoved
		}
	}
}

using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000015 RID: 21
	internal interface IAirSyncContext : IReadOnlyPropertyBag, INotificationManagerContext, ISyncLogger
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000101 RID: 257
		bool PerCallTracingEnabled { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000102 RID: 258
		// (set) Token: 0x06000103 RID: 259
		IAirSyncUser User { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000104 RID: 260
		string TaskDescription { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000105 RID: 261
		// (set) Token: 0x06000106 RID: 262
		TimeTracker Tracker { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000107 RID: 263
		// (set) Token: 0x06000108 RID: 264
		IPrincipal Principal { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000109 RID: 265
		string WindowsLiveId { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600010A RID: 266
		Dictionary<EasFeature, bool> FlightingOverrides { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600010B RID: 267
		IAirSyncRequest Request { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600010C RID: 268
		IAirSyncResponse Response { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600010D RID: 269
		ProtocolLogger ProtocolLogger { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600010E RID: 270
		// (set) Token: 0x0600010F RID: 271
		DeviceBehavior DeviceBehavior { get; set; }

		// Token: 0x06000110 RID: 272
		void PrepareToHang();

		// Token: 0x06000111 RID: 273
		void WriteActivityContextData();

		// Token: 0x06000112 RID: 274
		string GetActivityContextData();

		// Token: 0x06000113 RID: 275
		void SetDiagnosticValue(PropertyDefinition propDef, object value);

		// Token: 0x06000114 RID: 276
		void ClearDiagnosticValue(PropertyDefinition propDef);

		// Token: 0x06000115 RID: 277
		object GetThrottlingPolicyValue(Func<IThrottlingPolicy, object> func);

		// Token: 0x06000116 RID: 278
		bool TryGetElapsed(PropertyDefinition startTime, out TimeSpan elapsed);
	}
}

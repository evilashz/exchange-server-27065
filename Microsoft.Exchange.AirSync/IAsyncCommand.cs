using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000C7 RID: 199
	internal interface IAsyncCommand
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000BA6 RID: 2982
		// (set) Token: 0x06000BA7 RID: 2983
		bool ProcessingEventsEnabled { get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000BA8 RID: 2984
		bool PerUserTracingEnabled { get; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000BA9 RID: 2985
		INotificationManagerContext Context { get; }

		// Token: 0x06000BAA RID: 2986
		void Consume(Event evt);

		// Token: 0x06000BAB RID: 2987
		void HandleAccountTerminated(NotificationManager.AsyncEvent evt);

		// Token: 0x06000BAC RID: 2988
		void HandleException(Exception ex);

		// Token: 0x06000BAD RID: 2989
		void HeartbeatCallback();

		// Token: 0x06000BAE RID: 2990
		void ReleaseNotificationManager(bool wasStolen);

		// Token: 0x06000BAF RID: 2991
		void SetContextDataInTls();

		// Token: 0x06000BB0 RID: 2992
		uint GetHeartbeatInterval();
	}
}

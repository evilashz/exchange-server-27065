using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000006 RID: 6
	internal class SessionLockSerializer : ISessionSerializer
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000020D0 File Offset: 0x000002D0
		public void SerializeCallback<TState>(TState state, SerializableCallback<TState> callback, ISerializationGuard guard, bool forceCallback, string callbackName)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "=> EventSerializer queueing {0}", new object[]
			{
				callbackName
			});
			lock (this.myLock)
			{
				lock (guard.SerializationLocker)
				{
					if (forceCallback || this.ShouldProcessCallback(guard))
					{
						callback(state);
					}
				}
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002168 File Offset: 0x00000368
		public void SerializeEvent<TArgs>(object sender, TArgs args, SerializableEventHandler<TArgs> callback, ISerializationGuard guard, bool forceEvent, string eventName)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "=> EventSerializer queueing {0}", new object[]
			{
				eventName
			});
			lock (this.myLock)
			{
				lock (guard.SerializationLocker)
				{
					if (forceEvent || this.ShouldProcessCallback(guard))
					{
						callback(sender, args);
					}
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002204 File Offset: 0x00000404
		private bool ShouldProcessCallback(ISerializationGuard guard)
		{
			bool result = true;
			if (guard.StopSerializedEvents)
			{
				result = false;
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, this, "Ignoring callback because the serialization guard has been stopped.", new object[0]);
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		private object myLock = new object();
	}
}

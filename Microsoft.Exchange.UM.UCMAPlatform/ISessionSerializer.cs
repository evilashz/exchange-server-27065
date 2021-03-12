using System;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000005 RID: 5
	internal interface ISessionSerializer
	{
		// Token: 0x0600000B RID: 11
		void SerializeCallback<TState>(TState state, SerializableCallback<TState> callback, ISerializationGuard guard, bool forceCallback, string callbackName);

		// Token: 0x0600000C RID: 12
		void SerializeEvent<TArgs>(object sender, TArgs args, SerializableEventHandler<TArgs> callback, ISerializationGuard guard, bool forceEvent, string eventName);
	}
}

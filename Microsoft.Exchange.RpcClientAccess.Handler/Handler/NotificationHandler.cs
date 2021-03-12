using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NotificationHandler : INotificationHandler
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00006BFD File Offset: 0x00004DFD
		internal NotificationHandler(ConnectionHandler connectionHandler)
		{
			this.connectionHandler = connectionHandler;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006C27 File Offset: 0x00004E27
		public bool HasPendingNotifications()
		{
			return this.connectionHandler.ForAnyLogon((Logon logon) => !logon.NotificationQueue.IsEmpty);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00006CA8 File Offset: 0x00004EA8
		public void CollectNotifications(NotificationCollector collector)
		{
			if (this.connectionHandler.ForAnyLogon((Logon logon) => logon.HasActiveAsyncOperation))
			{
				return;
			}
			this.connectionHandler.ForAnyLogon(delegate(Logon logon)
			{
				ServerObjectHandle notificationHandle;
				byte logonId;
				Encoding string8Encoding;
				Notification notification;
				while (logon.NotificationQueue.Peek(out notificationHandle, out logonId, out string8Encoding, out notification))
				{
					if (!collector.TryAddNotification(notificationHandle, logonId, string8Encoding, notification))
					{
						return true;
					}
					logon.NotificationQueue.Dequeue();
				}
				return false;
			});
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006D08 File Offset: 0x00004F08
		public void RegisterCallback(Action callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback", "Callback cannot be null");
			}
			if (this.HasPendingNotifications())
			{
				ExTraceGlobals.AsyncRpcTracer.TraceDebug<IConnection, bool, Action>(Activity.TraceId, "RegisteredCallback. Invoke callback immediately. Connection = {0}, HasPendingNotifications = {1}, callback = {2}.", this.connectionHandler.Connection, true, callback);
				callback();
				return;
			}
			lock (this.callbackLock)
			{
				this.registeredCallback = callback;
				if (this.HasPendingNotifications())
				{
					ExTraceGlobals.AsyncRpcTracer.TraceDebug<IConnection, bool, Action>(Activity.TraceId, "RegisteredCallback. Invoke callback immediately after registering the callback. Connection = {0}, HasPendingNotifications = {1}, registeredCallback = {2}.", this.connectionHandler.Connection, true, this.registeredCallback);
					this.InvokeCallback();
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006DC0 File Offset: 0x00004FC0
		public void CancelCallback()
		{
			ExTraceGlobals.AsyncRpcTracer.TraceDebug<IConnection, bool>(Activity.TraceId, "CancelCallback. Connection = {0}, registeredCallback = {1}.", this.connectionHandler.Connection, this.registeredCallback != null);
			lock (this.callbackLock)
			{
				this.registeredCallback = null;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006E2C File Offset: 0x0000502C
		internal void InvokeCallback()
		{
			lock (this.callbackLock)
			{
				if (this.registeredCallback != null)
				{
					ExTraceGlobals.AsyncRpcTracer.TraceInformation<IConnection, Action>(0, Activity.TraceId, "InvokeCallback. Invoke callback. Connection = {0}, registeredCallback = {1}.", this.connectionHandler.Connection, this.registeredCallback);
					Action action = this.registeredCallback;
					this.registeredCallback = null;
					action();
				}
				else
				{
					ExTraceGlobals.AsyncRpcTracer.TraceDebug<IConnection>(0, Activity.TraceId, "InvokeCallback. Invoke callback but there is NO registered callback. Connection = {0}.", this.connectionHandler.Connection);
				}
			}
		}

		// Token: 0x04000053 RID: 83
		private readonly ConnectionHandler connectionHandler;

		// Token: 0x04000054 RID: 84
		private object callbackLock = new object();

		// Token: 0x04000055 RID: 85
		private Action registeredCallback;
	}
}

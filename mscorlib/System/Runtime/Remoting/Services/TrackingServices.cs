using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Services
{
	// Token: 0x020007DA RID: 2010
	[SecurityCritical]
	[ComVisible(true)]
	public class TrackingServices
	{
		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06005757 RID: 22359 RVA: 0x00133408 File Offset: 0x00131608
		private static object TrackingServicesSyncObject
		{
			get
			{
				if (TrackingServices.s_TrackingServicesSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref TrackingServices.s_TrackingServicesSyncObject, value, null);
				}
				return TrackingServices.s_TrackingServicesSyncObject;
			}
		}

		// Token: 0x06005758 RID: 22360 RVA: 0x00133434 File Offset: 0x00131634
		[SecurityCritical]
		public static void RegisterTrackingHandler(ITrackingHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			object trackingServicesSyncObject = TrackingServices.TrackingServicesSyncObject;
			lock (trackingServicesSyncObject)
			{
				if (-1 != TrackingServices.Match(handler))
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_TrackingHandlerAlreadyRegistered", new object[]
					{
						"handler"
					}));
				}
				if (TrackingServices._Handlers == null || TrackingServices._Size == TrackingServices._Handlers.Length)
				{
					ITrackingHandler[] array = new ITrackingHandler[TrackingServices._Size * 2 + 4];
					if (TrackingServices._Handlers != null)
					{
						Array.Copy(TrackingServices._Handlers, array, TrackingServices._Size);
					}
					TrackingServices._Handlers = array;
				}
				Volatile.Write<ITrackingHandler>(ref TrackingServices._Handlers[TrackingServices._Size++], handler);
			}
		}

		// Token: 0x06005759 RID: 22361 RVA: 0x00133518 File Offset: 0x00131718
		[SecurityCritical]
		public static void UnregisterTrackingHandler(ITrackingHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			object trackingServicesSyncObject = TrackingServices.TrackingServicesSyncObject;
			lock (trackingServicesSyncObject)
			{
				int num = TrackingServices.Match(handler);
				if (-1 == num)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_HandlerNotRegistered", new object[]
					{
						handler
					}));
				}
				Array.Copy(TrackingServices._Handlers, num + 1, TrackingServices._Handlers, num, TrackingServices._Size - num - 1);
				TrackingServices._Size--;
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x0600575A RID: 22362 RVA: 0x001335B8 File Offset: 0x001317B8
		public static ITrackingHandler[] RegisteredHandlers
		{
			[SecurityCritical]
			get
			{
				object trackingServicesSyncObject = TrackingServices.TrackingServicesSyncObject;
				ITrackingHandler[] result;
				lock (trackingServicesSyncObject)
				{
					if (TrackingServices._Size == 0)
					{
						result = new ITrackingHandler[0];
					}
					else
					{
						ITrackingHandler[] array = new ITrackingHandler[TrackingServices._Size];
						for (int i = 0; i < TrackingServices._Size; i++)
						{
							array[i] = TrackingServices._Handlers[i];
						}
						result = array;
					}
				}
				return result;
			}
		}

		// Token: 0x0600575B RID: 22363 RVA: 0x00133638 File Offset: 0x00131838
		[SecurityCritical]
		internal static void MarshaledObject(object obj, ObjRef or)
		{
			try
			{
				ITrackingHandler[] handlers = TrackingServices._Handlers;
				for (int i = 0; i < TrackingServices._Size; i++)
				{
					Volatile.Read<ITrackingHandler>(ref handlers[i]).MarshaledObject(obj, or);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x00133688 File Offset: 0x00131888
		[SecurityCritical]
		internal static void UnmarshaledObject(object obj, ObjRef or)
		{
			try
			{
				ITrackingHandler[] handlers = TrackingServices._Handlers;
				for (int i = 0; i < TrackingServices._Size; i++)
				{
					Volatile.Read<ITrackingHandler>(ref handlers[i]).UnmarshaledObject(obj, or);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600575D RID: 22365 RVA: 0x001336D8 File Offset: 0x001318D8
		[SecurityCritical]
		internal static void DisconnectedObject(object obj)
		{
			try
			{
				ITrackingHandler[] handlers = TrackingServices._Handlers;
				for (int i = 0; i < TrackingServices._Size; i++)
				{
					Volatile.Read<ITrackingHandler>(ref handlers[i]).DisconnectedObject(obj);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x00133728 File Offset: 0x00131928
		private static int Match(ITrackingHandler handler)
		{
			int result = -1;
			for (int i = 0; i < TrackingServices._Size; i++)
			{
				if (TrackingServices._Handlers[i] == handler)
				{
					result = i;
					break;
				}
			}
			return result;
		}

		// Token: 0x0400279B RID: 10139
		private static volatile ITrackingHandler[] _Handlers = new ITrackingHandler[0];

		// Token: 0x0400279C RID: 10140
		private static volatile int _Size = 0;

		// Token: 0x0400279D RID: 10141
		private static object s_TrackingServicesSyncObject = null;
	}
}

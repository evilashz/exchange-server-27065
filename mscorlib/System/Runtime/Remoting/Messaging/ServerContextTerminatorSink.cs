using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000858 RID: 2136
	[Serializable]
	internal class ServerContextTerminatorSink : InternalSink, IMessageSink
	{
		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06005B74 RID: 23412 RVA: 0x00140140 File Offset: 0x0013E340
		internal static IMessageSink MessageSink
		{
			get
			{
				if (ServerContextTerminatorSink.messageSink == null)
				{
					ServerContextTerminatorSink serverContextTerminatorSink = new ServerContextTerminatorSink();
					object obj = ServerContextTerminatorSink.staticSyncObject;
					lock (obj)
					{
						if (ServerContextTerminatorSink.messageSink == null)
						{
							ServerContextTerminatorSink.messageSink = serverContextTerminatorSink;
						}
					}
				}
				return ServerContextTerminatorSink.messageSink;
			}
		}

		// Token: 0x06005B75 RID: 23413 RVA: 0x001401A0 File Offset: 0x0013E3A0
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				return message;
			}
			Context currentContext = Thread.CurrentContext;
			IMessage message2;
			if (reqMsg is IConstructionCallMessage)
			{
				message = currentContext.NotifyActivatorProperties(reqMsg, true);
				if (message != null)
				{
					return message;
				}
				message2 = ((IConstructionCallMessage)reqMsg).Activator.Activate((IConstructionCallMessage)reqMsg);
				message = currentContext.NotifyActivatorProperties(message2, true);
				if (message != null)
				{
					return message;
				}
			}
			else
			{
				MarshalByRefObject marshalByRefObject = null;
				try
				{
					message2 = this.GetObjectChain(reqMsg, out marshalByRefObject).SyncProcessMessage(reqMsg);
				}
				finally
				{
					IDisposable disposable;
					if (marshalByRefObject != null && (disposable = (marshalByRefObject as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			return message2;
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x00140238 File Offset: 0x0013E438
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			IMessageCtrl result = null;
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message == null)
			{
				message = InternalSink.DisallowAsyncActivation(reqMsg);
			}
			if (message != null)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(message);
				}
			}
			else
			{
				MarshalByRefObject marshalByRefObject;
				IMessageSink objectChain = this.GetObjectChain(reqMsg, out marshalByRefObject);
				IDisposable iDis;
				if (marshalByRefObject != null && (iDis = (marshalByRefObject as IDisposable)) != null)
				{
					DisposeSink disposeSink = new DisposeSink(iDis, replySink);
					replySink = disposeSink;
				}
				result = objectChain.AsyncProcessMessage(reqMsg, replySink);
			}
			return result;
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06005B77 RID: 23415 RVA: 0x00140298 File Offset: 0x0013E498
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x0014029C File Offset: 0x0013E49C
		[SecurityCritical]
		internal virtual IMessageSink GetObjectChain(IMessage reqMsg, out MarshalByRefObject obj)
		{
			ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
			return serverIdentity.GetServerObjectChain(out obj);
		}

		// Token: 0x04002912 RID: 10514
		private static volatile ServerContextTerminatorSink messageSink;

		// Token: 0x04002913 RID: 10515
		private static object staticSyncObject = new object();
	}
}

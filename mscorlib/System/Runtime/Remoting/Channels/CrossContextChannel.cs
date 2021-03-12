using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000807 RID: 2055
	internal class CrossContextChannel : InternalSink, IMessageSink
	{
		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x060058A1 RID: 22689 RVA: 0x00137AFD File Offset: 0x00135CFD
		// (set) Token: 0x060058A2 RID: 22690 RVA: 0x00137B13 File Offset: 0x00135D13
		private static CrossContextChannel messageSink
		{
			get
			{
				return Thread.GetDomain().RemotingData.ChannelServicesData.xctxmessageSink;
			}
			set
			{
				Thread.GetDomain().RemotingData.ChannelServicesData.xctxmessageSink = value;
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x060058A3 RID: 22691 RVA: 0x00137B2C File Offset: 0x00135D2C
		internal static IMessageSink MessageSink
		{
			get
			{
				if (CrossContextChannel.messageSink == null)
				{
					CrossContextChannel messageSink = new CrossContextChannel();
					object obj = CrossContextChannel.staticSyncObject;
					lock (obj)
					{
						if (CrossContextChannel.messageSink == null)
						{
							CrossContextChannel.messageSink = messageSink;
						}
					}
				}
				return CrossContextChannel.messageSink;
			}
		}

		// Token: 0x060058A4 RID: 22692 RVA: 0x00137B84 File Offset: 0x00135D84
		[SecurityCritical]
		internal static object SyncProcessMessageCallback(object[] args)
		{
			IMessage message = args[0] as IMessage;
			Context context = args[1] as Context;
			if (RemotingServices.CORProfilerTrackRemoting())
			{
				Guid id = Guid.Empty;
				if (RemotingServices.CORProfilerTrackRemotingCookie())
				{
					object obj = message.Properties["CORProfilerCookie"];
					if (obj != null)
					{
						id = (Guid)obj;
					}
				}
				RemotingServices.CORProfilerRemotingServerReceivingMessage(id, false);
			}
			context.NotifyDynamicSinks(message, false, true, false, true);
			IMessage message2 = context.GetServerContextChain().SyncProcessMessage(message);
			context.NotifyDynamicSinks(message2, false, false, false, true);
			if (RemotingServices.CORProfilerTrackRemoting())
			{
				Guid guid;
				RemotingServices.CORProfilerRemotingServerSendingReply(out guid, false);
				if (RemotingServices.CORProfilerTrackRemotingCookie())
				{
					message2.Properties["CORProfilerCookie"] = guid;
				}
			}
			return message2;
		}

		// Token: 0x060058A5 RID: 22693 RVA: 0x00137C34 File Offset: 0x00135E34
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			object[] array = new object[2];
			IMessage message = null;
			try
			{
				IMessage message2 = InternalSink.ValidateMessage(reqMsg);
				if (message2 != null)
				{
					return message2;
				}
				ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
				array[0] = reqMsg;
				array[1] = serverIdentity.ServerContext;
				message = (IMessage)Thread.CurrentThread.InternalCrossContextCallback(serverIdentity.ServerContext, CrossContextChannel.s_xctxDel, array);
			}
			catch (Exception e)
			{
				message = new ReturnMessage(e, (IMethodCallMessage)reqMsg);
				if (reqMsg != null)
				{
					((ReturnMessage)message).SetLogicalCallContext((LogicalCallContext)reqMsg.Properties[Message.CallContextKey]);
				}
			}
			return message;
		}

		// Token: 0x060058A6 RID: 22694 RVA: 0x00137CD8 File Offset: 0x00135ED8
		[SecurityCritical]
		internal static object AsyncProcessMessageCallback(object[] args)
		{
			AsyncWorkItem replySink = null;
			IMessage msg = (IMessage)args[0];
			IMessageSink messageSink = (IMessageSink)args[1];
			Context oldCtx = (Context)args[2];
			Context context = (Context)args[3];
			if (messageSink != null)
			{
				replySink = new AsyncWorkItem(messageSink, oldCtx);
			}
			context.NotifyDynamicSinks(msg, false, true, true, true);
			return context.GetServerContextChain().AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x060058A7 RID: 22695 RVA: 0x00137D3C File Offset: 0x00135F3C
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			object[] array = new object[4];
			IMessageCtrl result = null;
			if (message != null)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(message);
				}
			}
			else
			{
				ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
				if (RemotingServices.CORProfilerTrackRemotingAsync())
				{
					Guid id = Guid.Empty;
					if (RemotingServices.CORProfilerTrackRemotingCookie())
					{
						object obj = reqMsg.Properties["CORProfilerCookie"];
						if (obj != null)
						{
							id = (Guid)obj;
						}
					}
					RemotingServices.CORProfilerRemotingServerReceivingMessage(id, true);
					if (replySink != null)
					{
						IMessageSink messageSink = new ServerAsyncReplyTerminatorSink(replySink);
						replySink = messageSink;
					}
				}
				Context serverContext = serverIdentity.ServerContext;
				if (serverContext.IsThreadPoolAware)
				{
					array[0] = reqMsg;
					array[1] = replySink;
					array[2] = Thread.CurrentContext;
					array[3] = serverContext;
					InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(CrossContextChannel.AsyncProcessMessageCallback);
					result = (IMessageCtrl)Thread.CurrentThread.InternalCrossContextCallback(serverContext, ftnToCall, array);
				}
				else
				{
					AsyncWorkItem @object = new AsyncWorkItem(reqMsg, replySink, Thread.CurrentContext, serverIdentity);
					WaitCallback callBack = new WaitCallback(@object.FinishAsyncWork);
					ThreadPool.QueueUserWorkItem(callBack);
				}
			}
			return result;
		}

		// Token: 0x060058A8 RID: 22696 RVA: 0x00137E38 File Offset: 0x00136038
		[SecurityCritical]
		internal static object DoAsyncDispatchCallback(object[] args)
		{
			AsyncWorkItem replySink = null;
			IMessage msg = (IMessage)args[0];
			IMessageSink messageSink = (IMessageSink)args[1];
			Context oldCtx = (Context)args[2];
			Context context = (Context)args[3];
			if (messageSink != null)
			{
				replySink = new AsyncWorkItem(messageSink, oldCtx);
			}
			return context.GetServerContextChain().AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x060058A9 RID: 22697 RVA: 0x00137E8C File Offset: 0x0013608C
		[SecurityCritical]
		internal static IMessageCtrl DoAsyncDispatch(IMessage reqMsg, IMessageSink replySink)
		{
			object[] array = new object[4];
			ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
			if (RemotingServices.CORProfilerTrackRemotingAsync())
			{
				Guid id = Guid.Empty;
				if (RemotingServices.CORProfilerTrackRemotingCookie())
				{
					object obj = reqMsg.Properties["CORProfilerCookie"];
					if (obj != null)
					{
						id = (Guid)obj;
					}
				}
				RemotingServices.CORProfilerRemotingServerReceivingMessage(id, true);
				if (replySink != null)
				{
					IMessageSink messageSink = new ServerAsyncReplyTerminatorSink(replySink);
					replySink = messageSink;
				}
			}
			Context serverContext = serverIdentity.ServerContext;
			array[0] = reqMsg;
			array[1] = replySink;
			array[2] = Thread.CurrentContext;
			array[3] = serverContext;
			InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(CrossContextChannel.DoAsyncDispatchCallback);
			return (IMessageCtrl)Thread.CurrentThread.InternalCrossContextCallback(serverContext, ftnToCall, array);
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x060058AA RID: 22698 RVA: 0x00137F32 File Offset: 0x00136132
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x04002820 RID: 10272
		private const string _channelName = "XCTX";

		// Token: 0x04002821 RID: 10273
		private const int _channelCapability = 0;

		// Token: 0x04002822 RID: 10274
		private const string _channelURI = "XCTX_URI";

		// Token: 0x04002823 RID: 10275
		private static object staticSyncObject = new object();

		// Token: 0x04002824 RID: 10276
		private static InternalCrossContextDelegate s_xctxDel = new InternalCrossContextDelegate(CrossContextChannel.SyncProcessMessageCallback);
	}
}

using System;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085A RID: 2138
	[Serializable]
	internal class ServerObjectTerminatorSink : InternalSink, IMessageSink
	{
		// Token: 0x06005B7F RID: 23423 RVA: 0x00140337 File Offset: 0x0013E537
		internal ServerObjectTerminatorSink(MarshalByRefObject srvObj)
		{
			this._stackBuilderSink = new StackBuilderSink(srvObj);
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x0014034C File Offset: 0x0013E54C
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				return message;
			}
			ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
			ArrayWithSize serverSideDynamicSinks = serverIdentity.ServerSideDynamicSinks;
			if (serverSideDynamicSinks != null)
			{
				DynamicPropertyHolder.NotifyDynamicSinks(reqMsg, serverSideDynamicSinks, false, true, false);
			}
			IMessageSink messageSink = this._stackBuilderSink.ServerObject as IMessageSink;
			IMessage message2;
			if (messageSink != null)
			{
				message2 = messageSink.SyncProcessMessage(reqMsg);
			}
			else
			{
				message2 = this._stackBuilderSink.SyncProcessMessage(reqMsg);
			}
			if (serverSideDynamicSinks != null)
			{
				DynamicPropertyHolder.NotifyDynamicSinks(message2, serverSideDynamicSinks, false, false, false);
			}
			return message2;
		}

		// Token: 0x06005B81 RID: 23425 RVA: 0x001403BC File Offset: 0x0013E5BC
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			IMessageCtrl result = null;
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(message);
				}
			}
			else
			{
				IMessageSink messageSink = this._stackBuilderSink.ServerObject as IMessageSink;
				if (messageSink != null)
				{
					result = messageSink.AsyncProcessMessage(reqMsg, replySink);
				}
				else
				{
					result = this._stackBuilderSink.AsyncProcessMessage(reqMsg, replySink);
				}
			}
			return result;
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06005B82 RID: 23426 RVA: 0x00140410 File Offset: 0x0013E610
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x04002916 RID: 10518
		internal StackBuilderSink _stackBuilderSink;
	}
}

using System;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000855 RID: 2133
	[Serializable]
	internal class EnvoyTerminatorSink : InternalSink, IMessageSink
	{
		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06005B60 RID: 23392 RVA: 0x0013FCFC File Offset: 0x0013DEFC
		internal static IMessageSink MessageSink
		{
			get
			{
				if (EnvoyTerminatorSink.messageSink == null)
				{
					EnvoyTerminatorSink envoyTerminatorSink = new EnvoyTerminatorSink();
					object obj = EnvoyTerminatorSink.staticSyncObject;
					lock (obj)
					{
						if (EnvoyTerminatorSink.messageSink == null)
						{
							EnvoyTerminatorSink.messageSink = envoyTerminatorSink;
						}
					}
				}
				return EnvoyTerminatorSink.messageSink;
			}
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x0013FD5C File Offset: 0x0013DF5C
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				return message;
			}
			return Thread.CurrentContext.GetClientContextChain().SyncProcessMessage(reqMsg);
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x0013FD88 File Offset: 0x0013DF88
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
				result = Thread.CurrentContext.GetClientContextChain().AsyncProcessMessage(reqMsg, replySink);
			}
			return result;
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06005B63 RID: 23395 RVA: 0x0013FDC1 File Offset: 0x0013DFC1
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x0400290C RID: 10508
		private static volatile EnvoyTerminatorSink messageSink;

		// Token: 0x0400290D RID: 10509
		private static object staticSyncObject = new object();
	}
}

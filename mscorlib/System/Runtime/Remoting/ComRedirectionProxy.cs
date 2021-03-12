using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x02000791 RID: 1937
	internal class ComRedirectionProxy : MarshalByRefObject, IMessageSink
	{
		// Token: 0x060054B0 RID: 21680 RVA: 0x0012C1B2 File Offset: 0x0012A3B2
		internal ComRedirectionProxy(MarshalByRefObject comObject, Type serverType)
		{
			this._comObject = comObject;
			this._serverType = serverType;
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x0012C1C8 File Offset: 0x0012A3C8
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			IMethodCallMessage reqMsg = (IMethodCallMessage)msg;
			IMethodReturnMessage methodReturnMessage = RemotingServices.ExecuteMessage(this._comObject, reqMsg);
			if (methodReturnMessage != null)
			{
				COMException ex = methodReturnMessage.Exception as COMException;
				if (ex != null && (ex._HResult == -2147023174 || ex._HResult == -2147023169))
				{
					this._comObject = (MarshalByRefObject)Activator.CreateInstance(this._serverType, true);
					methodReturnMessage = RemotingServices.ExecuteMessage(this._comObject, reqMsg);
				}
			}
			return methodReturnMessage;
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x0012C23C File Offset: 0x0012A43C
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			IMessage msg2 = this.SyncProcessMessage(msg);
			if (replySink != null)
			{
				replySink.SyncProcessMessage(msg2);
			}
			return null;
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x060054B3 RID: 21683 RVA: 0x0012C25F File Offset: 0x0012A45F
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x040026BF RID: 9919
		private MarshalByRefObject _comObject;

		// Token: 0x040026C0 RID: 9920
		private Type _serverType;
	}
}

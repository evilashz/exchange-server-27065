using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x02000790 RID: 1936
	internal class RedirectionProxy : MarshalByRefObject, IMessageSink
	{
		// Token: 0x060054AB RID: 21675 RVA: 0x0012C0C8 File Offset: 0x0012A2C8
		[SecurityCritical]
		internal RedirectionProxy(MarshalByRefObject proxy, Type serverType)
		{
			this._proxy = proxy;
			this._realProxy = RemotingServices.GetRealProxy(this._proxy);
			this._serverType = serverType;
			this._objectMode = WellKnownObjectMode.Singleton;
		}

		// Token: 0x17000E0B RID: 3595
		// (set) Token: 0x060054AC RID: 21676 RVA: 0x0012C0F6 File Offset: 0x0012A2F6
		public WellKnownObjectMode ObjectMode
		{
			set
			{
				this._objectMode = value;
			}
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x0012C100 File Offset: 0x0012A300
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage result = null;
			try
			{
				msg.Properties["__Uri"] = this._realProxy.IdentityObject.URI;
				if (this._objectMode == WellKnownObjectMode.Singleton)
				{
					result = this._realProxy.Invoke(msg);
				}
				else
				{
					MarshalByRefObject proxy = (MarshalByRefObject)Activator.CreateInstance(this._serverType, true);
					RealProxy realProxy = RemotingServices.GetRealProxy(proxy);
					result = realProxy.Invoke(msg);
				}
			}
			catch (Exception e)
			{
				result = new ReturnMessage(e, msg as IMethodCallMessage);
			}
			return result;
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x0012C18C File Offset: 0x0012A38C
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

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x060054AF RID: 21679 RVA: 0x0012C1AF File Offset: 0x0012A3AF
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x040026BB RID: 9915
		private MarshalByRefObject _proxy;

		// Token: 0x040026BC RID: 9916
		[SecurityCritical]
		private RealProxy _realProxy;

		// Token: 0x040026BD RID: 9917
		private Type _serverType;

		// Token: 0x040026BE RID: 9918
		private WellKnownObjectMode _objectMode;
	}
}

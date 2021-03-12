using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x0200079F RID: 1951
	internal class ServerIdentity : Identity
	{
		// Token: 0x06005570 RID: 21872 RVA: 0x0012E728 File Offset: 0x0012C928
		internal Type GetLastCalledType(string newTypeName)
		{
			ServerIdentity.LastCalledType lastCalledType = this._lastCalledType;
			if (lastCalledType == null)
			{
				return null;
			}
			string typeName = lastCalledType.typeName;
			Type type = lastCalledType.type;
			if (typeName == null || type == null)
			{
				return null;
			}
			if (typeName.Equals(newTypeName))
			{
				return type;
			}
			return null;
		}

		// Token: 0x06005571 RID: 21873 RVA: 0x0012E76C File Offset: 0x0012C96C
		internal void SetLastCalledType(string newTypeName, Type newType)
		{
			this._lastCalledType = new ServerIdentity.LastCalledType
			{
				typeName = newTypeName,
				type = newType
			};
		}

		// Token: 0x06005572 RID: 21874 RVA: 0x0012E794 File Offset: 0x0012C994
		[SecurityCritical]
		internal void SetHandle()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				if (!this._srvIdentityHandle.IsAllocated)
				{
					this._srvIdentityHandle = new GCHandle(this, GCHandleType.Normal);
				}
				else
				{
					this._srvIdentityHandle.Target = this;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x06005573 RID: 21875 RVA: 0x0012E7F4 File Offset: 0x0012C9F4
		[SecurityCritical]
		internal void ResetHandle()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				this._srvIdentityHandle.Target = null;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x06005574 RID: 21876 RVA: 0x0012E838 File Offset: 0x0012CA38
		internal GCHandle GetHandle()
		{
			return this._srvIdentityHandle;
		}

		// Token: 0x06005575 RID: 21877 RVA: 0x0012E840 File Offset: 0x0012CA40
		[SecurityCritical]
		internal ServerIdentity(MarshalByRefObject obj, Context serverCtx) : base(obj is ContextBoundObject)
		{
			if (obj != null)
			{
				if (!RemotingServices.IsTransparentProxy(obj))
				{
					this._srvType = obj.GetType();
				}
				else
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(obj);
					this._srvType = realProxy.GetProxiedType();
				}
			}
			this._srvCtx = serverCtx;
			this._serverObjectChain = null;
			this._stackBuilderSink = null;
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x0012E89D File Offset: 0x0012CA9D
		[SecurityCritical]
		internal ServerIdentity(MarshalByRefObject obj, Context serverCtx, string uri) : this(obj, serverCtx)
		{
			base.SetOrCreateURI(uri, true);
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06005577 RID: 21879 RVA: 0x0012E8AF File Offset: 0x0012CAAF
		internal Context ServerContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._srvCtx;
			}
		}

		// Token: 0x06005578 RID: 21880 RVA: 0x0012E8B7 File Offset: 0x0012CAB7
		internal void SetSingleCallObjectMode()
		{
			this._flags |= 512;
		}

		// Token: 0x06005579 RID: 21881 RVA: 0x0012E8CB File Offset: 0x0012CACB
		internal void SetSingletonObjectMode()
		{
			this._flags |= 1024;
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x0012E8DF File Offset: 0x0012CADF
		internal bool IsSingleCall()
		{
			return (this._flags & 512) != 0;
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x0012E8F0 File Offset: 0x0012CAF0
		internal bool IsSingleton()
		{
			return (this._flags & 1024) != 0;
		}

		// Token: 0x0600557C RID: 21884 RVA: 0x0012E904 File Offset: 0x0012CB04
		[SecurityCritical]
		internal IMessageSink GetServerObjectChain(out MarshalByRefObject obj)
		{
			obj = null;
			if (!this.IsSingleCall())
			{
				if (this._serverObjectChain == null)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(this, ref flag);
						if (this._serverObjectChain == null)
						{
							MarshalByRefObject tporObject = base.TPOrObject;
							this._serverObjectChain = this._srvCtx.CreateServerObjectChain(tporObject);
						}
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(this);
						}
					}
				}
				return this._serverObjectChain;
			}
			MarshalByRefObject marshalByRefObject;
			IMessageSink messageSink;
			if (this._tpOrObject != null && this._firstCallDispatched == 0 && Interlocked.CompareExchange(ref this._firstCallDispatched, 1, 0) == 0)
			{
				marshalByRefObject = (MarshalByRefObject)this._tpOrObject;
				messageSink = this._serverObjectChain;
				if (messageSink == null)
				{
					messageSink = this._srvCtx.CreateServerObjectChain(marshalByRefObject);
				}
			}
			else
			{
				marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._srvType, true);
				string objectUri = RemotingServices.GetObjectUri(marshalByRefObject);
				if (objectUri != null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_WellKnown_CtorCantMarshal"), base.URI));
				}
				if (!RemotingServices.IsTransparentProxy(marshalByRefObject))
				{
					marshalByRefObject.__RaceSetServerIdentity(this);
				}
				else
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(marshalByRefObject);
					realProxy.IdentityObject = this;
				}
				messageSink = this._srvCtx.CreateServerObjectChain(marshalByRefObject);
			}
			obj = marshalByRefObject;
			return messageSink;
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x0600557D RID: 21885 RVA: 0x0012EA34 File Offset: 0x0012CC34
		// (set) Token: 0x0600557E RID: 21886 RVA: 0x0012EA3C File Offset: 0x0012CC3C
		internal Type ServerType
		{
			get
			{
				return this._srvType;
			}
			set
			{
				this._srvType = value;
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x0600557F RID: 21887 RVA: 0x0012EA45 File Offset: 0x0012CC45
		// (set) Token: 0x06005580 RID: 21888 RVA: 0x0012EA4D File Offset: 0x0012CC4D
		internal bool MarshaledAsSpecificType
		{
			get
			{
				return this._bMarshaledAsSpecificType;
			}
			set
			{
				this._bMarshaledAsSpecificType = value;
			}
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x0012EA58 File Offset: 0x0012CC58
		[SecurityCritical]
		internal IMessageSink RaceSetServerObjectChain(IMessageSink serverObjectChain)
		{
			if (this._serverObjectChain == null)
			{
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(this, ref flag);
					if (this._serverObjectChain == null)
					{
						this._serverObjectChain = serverObjectChain;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
			}
			return this._serverObjectChain;
		}

		// Token: 0x06005582 RID: 21890 RVA: 0x0012EAB0 File Offset: 0x0012CCB0
		[SecurityCritical]
		internal bool AddServerSideDynamicProperty(IDynamicProperty prop)
		{
			if (this._dphSrv == null)
			{
				DynamicPropertyHolder dphSrv = new DynamicPropertyHolder();
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(this, ref flag);
					if (this._dphSrv == null)
					{
						this._dphSrv = dphSrv;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
			}
			return this._dphSrv.AddDynamicProperty(prop);
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x0012EB14 File Offset: 0x0012CD14
		[SecurityCritical]
		internal bool RemoveServerSideDynamicProperty(string name)
		{
			if (this._dphSrv == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_PropNotFound"));
			}
			return this._dphSrv.RemoveDynamicProperty(name);
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06005584 RID: 21892 RVA: 0x0012EB3A File Offset: 0x0012CD3A
		internal ArrayWithSize ServerSideDynamicSinks
		{
			[SecurityCritical]
			get
			{
				if (this._dphSrv == null)
				{
					return null;
				}
				return this._dphSrv.DynamicSinks;
			}
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x0012EB51 File Offset: 0x0012CD51
		[SecurityCritical]
		internal override void AssertValid()
		{
			if (base.TPOrObject != null)
			{
				RemotingServices.IsTransparentProxy(base.TPOrObject);
			}
		}

		// Token: 0x040026E5 RID: 9957
		internal Context _srvCtx;

		// Token: 0x040026E6 RID: 9958
		internal IMessageSink _serverObjectChain;

		// Token: 0x040026E7 RID: 9959
		internal StackBuilderSink _stackBuilderSink;

		// Token: 0x040026E8 RID: 9960
		internal DynamicPropertyHolder _dphSrv;

		// Token: 0x040026E9 RID: 9961
		internal Type _srvType;

		// Token: 0x040026EA RID: 9962
		private ServerIdentity.LastCalledType _lastCalledType;

		// Token: 0x040026EB RID: 9963
		internal bool _bMarshaledAsSpecificType;

		// Token: 0x040026EC RID: 9964
		internal int _firstCallDispatched;

		// Token: 0x040026ED RID: 9965
		internal GCHandle _srvIdentityHandle;

		// Token: 0x02000C36 RID: 3126
		private class LastCalledType
		{
			// Token: 0x040036F8 RID: 14072
			public string typeName;

			// Token: 0x040036F9 RID: 14073
			public Type type;
		}
	}
}

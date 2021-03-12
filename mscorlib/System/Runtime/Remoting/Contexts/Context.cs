using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007DC RID: 2012
	[ComVisible(true)]
	public class Context
	{
		// Token: 0x06005765 RID: 22373 RVA: 0x0013377F File Offset: 0x0013197F
		[SecurityCritical]
		public Context() : this(0)
		{
		}

		// Token: 0x06005766 RID: 22374 RVA: 0x00133788 File Offset: 0x00131988
		[SecurityCritical]
		private Context(int flags)
		{
			this._ctxFlags = flags;
			if ((this._ctxFlags & 1) != 0)
			{
				this._ctxID = 0;
			}
			else
			{
				this._ctxID = Interlocked.Increment(ref Context._ctxIDCounter);
			}
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			if (remotingData != null)
			{
				IContextProperty[] appDomainContextProperties = remotingData.AppDomainContextProperties;
				if (appDomainContextProperties != null)
				{
					for (int i = 0; i < appDomainContextProperties.Length; i++)
					{
						this.SetProperty(appDomainContextProperties[i]);
					}
				}
			}
			if ((this._ctxFlags & 1) != 0)
			{
				this.Freeze();
			}
			this.SetupInternalContext((this._ctxFlags & 1) == 1);
		}

		// Token: 0x06005767 RID: 22375
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetupInternalContext(bool bDefault);

		// Token: 0x06005768 RID: 22376 RVA: 0x00133818 File Offset: 0x00131A18
		[SecuritySafeCritical]
		~Context()
		{
			if (this._internalContext != IntPtr.Zero && (this._ctxFlags & 1) == 0)
			{
				this.CleanupInternalContext();
			}
		}

		// Token: 0x06005769 RID: 22377
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CleanupInternalContext();

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x0600576A RID: 22378 RVA: 0x00133860 File Offset: 0x00131A60
		public virtual int ContextID
		{
			[SecurityCritical]
			get
			{
				return this._ctxID;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x0600576B RID: 22379 RVA: 0x00133868 File Offset: 0x00131A68
		internal virtual IntPtr InternalContextID
		{
			get
			{
				return this._internalContext;
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x0600576C RID: 22380 RVA: 0x00133870 File Offset: 0x00131A70
		internal virtual AppDomain AppDomain
		{
			get
			{
				return this._appDomain;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x0600576D RID: 22381 RVA: 0x00133878 File Offset: 0x00131A78
		internal bool IsDefaultContext
		{
			get
			{
				return this._ctxID == 0;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x0600576E RID: 22382 RVA: 0x00133883 File Offset: 0x00131A83
		public static Context DefaultContext
		{
			[SecurityCritical]
			get
			{
				return Thread.GetDomain().GetDefaultContext();
			}
		}

		// Token: 0x0600576F RID: 22383 RVA: 0x0013388F File Offset: 0x00131A8F
		[SecurityCritical]
		internal static Context CreateDefaultContext()
		{
			return new Context(1);
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x00133898 File Offset: 0x00131A98
		[SecurityCritical]
		public virtual IContextProperty GetProperty(string name)
		{
			if (this._ctxProps == null || name == null)
			{
				return null;
			}
			IContextProperty result = null;
			for (int i = 0; i < this._numCtxProps; i++)
			{
				if (this._ctxProps[i].Name.Equals(name))
				{
					result = this._ctxProps[i];
					break;
				}
			}
			return result;
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x001338E8 File Offset: 0x00131AE8
		[SecurityCritical]
		public virtual void SetProperty(IContextProperty prop)
		{
			if (prop == null || prop.Name == null)
			{
				throw new ArgumentNullException((prop == null) ? "prop" : "property name");
			}
			if ((this._ctxFlags & 2) != 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AddContextFrozen"));
			}
			lock (this)
			{
				Context.CheckPropertyNameClash(prop.Name, this._ctxProps, this._numCtxProps);
				if (this._ctxProps == null || this._numCtxProps == this._ctxProps.Length)
				{
					this._ctxProps = Context.GrowPropertiesArray(this._ctxProps);
				}
				IContextProperty[] ctxProps = this._ctxProps;
				int numCtxProps = this._numCtxProps;
				this._numCtxProps = numCtxProps + 1;
				ctxProps[numCtxProps] = prop;
			}
		}

		// Token: 0x06005772 RID: 22386 RVA: 0x001339B0 File Offset: 0x00131BB0
		[SecurityCritical]
		internal virtual void InternalFreeze()
		{
			this._ctxFlags |= 2;
			for (int i = 0; i < this._numCtxProps; i++)
			{
				this._ctxProps[i].Freeze(this);
			}
		}

		// Token: 0x06005773 RID: 22387 RVA: 0x001339EC File Offset: 0x00131BEC
		[SecurityCritical]
		public virtual void Freeze()
		{
			lock (this)
			{
				if ((this._ctxFlags & 2) != 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ContextAlreadyFrozen"));
				}
				this.InternalFreeze();
			}
		}

		// Token: 0x06005774 RID: 22388 RVA: 0x00133A44 File Offset: 0x00131C44
		internal virtual void SetThreadPoolAware()
		{
			this._ctxFlags |= 4;
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06005775 RID: 22389 RVA: 0x00133A54 File Offset: 0x00131C54
		internal virtual bool IsThreadPoolAware
		{
			get
			{
				return (this._ctxFlags & 4) == 4;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06005776 RID: 22390 RVA: 0x00133A64 File Offset: 0x00131C64
		public virtual IContextProperty[] ContextProperties
		{
			[SecurityCritical]
			get
			{
				if (this._ctxProps == null)
				{
					return null;
				}
				IContextProperty[] result;
				lock (this)
				{
					IContextProperty[] array = new IContextProperty[this._numCtxProps];
					Array.Copy(this._ctxProps, array, this._numCtxProps);
					result = array;
				}
				return result;
			}
		}

		// Token: 0x06005777 RID: 22391 RVA: 0x00133AC4 File Offset: 0x00131CC4
		[SecurityCritical]
		internal static void CheckPropertyNameClash(string name, IContextProperty[] props, int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (props[i].Name.Equals(name))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DuplicatePropertyName"));
				}
			}
		}

		// Token: 0x06005778 RID: 22392 RVA: 0x00133B00 File Offset: 0x00131D00
		internal static IContextProperty[] GrowPropertiesArray(IContextProperty[] props)
		{
			int num = ((props != null) ? props.Length : 0) + 8;
			IContextProperty[] array = new IContextProperty[num];
			if (props != null)
			{
				Array.Copy(props, array, props.Length);
			}
			return array;
		}

		// Token: 0x06005779 RID: 22393 RVA: 0x00133B30 File Offset: 0x00131D30
		[SecurityCritical]
		internal virtual IMessageSink GetServerContextChain()
		{
			if (this._serverContextChain == null)
			{
				IMessageSink messageSink = ServerContextTerminatorSink.MessageSink;
				int numCtxProps = this._numCtxProps;
				while (numCtxProps-- > 0)
				{
					object obj = this._ctxProps[numCtxProps];
					IContributeServerContextSink contributeServerContextSink = obj as IContributeServerContextSink;
					if (contributeServerContextSink != null)
					{
						messageSink = contributeServerContextSink.GetServerContextSink(messageSink);
						if (messageSink == null)
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
						}
					}
				}
				lock (this)
				{
					if (this._serverContextChain == null)
					{
						this._serverContextChain = messageSink;
					}
				}
			}
			return this._serverContextChain;
		}

		// Token: 0x0600577A RID: 22394 RVA: 0x00133BD0 File Offset: 0x00131DD0
		[SecurityCritical]
		internal virtual IMessageSink GetClientContextChain()
		{
			if (this._clientContextChain == null)
			{
				IMessageSink messageSink = ClientContextTerminatorSink.MessageSink;
				for (int i = 0; i < this._numCtxProps; i++)
				{
					object obj = this._ctxProps[i];
					IContributeClientContextSink contributeClientContextSink = obj as IContributeClientContextSink;
					if (contributeClientContextSink != null)
					{
						messageSink = contributeClientContextSink.GetClientContextSink(messageSink);
						if (messageSink == null)
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
						}
					}
				}
				lock (this)
				{
					if (this._clientContextChain == null)
					{
						this._clientContextChain = messageSink;
					}
				}
			}
			return this._clientContextChain;
		}

		// Token: 0x0600577B RID: 22395 RVA: 0x00133C70 File Offset: 0x00131E70
		[SecurityCritical]
		internal virtual IMessageSink CreateServerObjectChain(MarshalByRefObject serverObj)
		{
			IMessageSink messageSink = new ServerObjectTerminatorSink(serverObj);
			int numCtxProps = this._numCtxProps;
			while (numCtxProps-- > 0)
			{
				object obj = this._ctxProps[numCtxProps];
				IContributeObjectSink contributeObjectSink = obj as IContributeObjectSink;
				if (contributeObjectSink != null)
				{
					messageSink = contributeObjectSink.GetObjectSink(serverObj, messageSink);
					if (messageSink == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
					}
				}
			}
			return messageSink;
		}

		// Token: 0x0600577C RID: 22396 RVA: 0x00133CC8 File Offset: 0x00131EC8
		[SecurityCritical]
		internal virtual IMessageSink CreateEnvoyChain(MarshalByRefObject objectOrProxy)
		{
			IMessageSink messageSink = EnvoyTerminatorSink.MessageSink;
			for (int i = 0; i < this._numCtxProps; i++)
			{
				object obj = this._ctxProps[i];
				IContributeEnvoySink contributeEnvoySink = obj as IContributeEnvoySink;
				if (contributeEnvoySink != null)
				{
					messageSink = contributeEnvoySink.GetEnvoySink(objectOrProxy, messageSink);
					if (messageSink == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
					}
				}
			}
			return messageSink;
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x00133D24 File Offset: 0x00131F24
		[SecurityCritical]
		internal IMessage NotifyActivatorProperties(IMessage msg, bool bServerSide)
		{
			IMessage message = null;
			try
			{
				int numCtxProps = this._numCtxProps;
				while (numCtxProps-- != 0)
				{
					object obj = this._ctxProps[numCtxProps];
					IContextPropertyActivator contextPropertyActivator = obj as IContextPropertyActivator;
					if (contextPropertyActivator != null)
					{
						IConstructionCallMessage constructionCallMessage = msg as IConstructionCallMessage;
						if (constructionCallMessage != null)
						{
							if (!bServerSide)
							{
								contextPropertyActivator.CollectFromClientContext(constructionCallMessage);
							}
							else
							{
								contextPropertyActivator.DeliverClientContextToServerContext(constructionCallMessage);
							}
						}
						else if (bServerSide)
						{
							contextPropertyActivator.CollectFromServerContext((IConstructionReturnMessage)msg);
						}
						else
						{
							contextPropertyActivator.DeliverServerContextToClientContext((IConstructionReturnMessage)msg);
						}
					}
				}
			}
			catch (Exception e)
			{
				IMethodCallMessage mcm;
				if (msg is IConstructionCallMessage)
				{
					mcm = (IMethodCallMessage)msg;
				}
				else
				{
					mcm = new ErrorMessage();
				}
				message = new ReturnMessage(e, mcm);
				if (msg != null)
				{
					((ReturnMessage)message).SetLogicalCallContext((LogicalCallContext)msg.Properties[Message.CallContextKey]);
				}
			}
			return message;
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x00133DFC File Offset: 0x00131FFC
		public override string ToString()
		{
			return "ContextID: " + this._ctxID;
		}

		// Token: 0x0600577F RID: 22399 RVA: 0x00133E14 File Offset: 0x00132014
		[SecurityCritical]
		public void DoCallBack(CrossContextDelegate deleg)
		{
			if (deleg == null)
			{
				throw new ArgumentNullException("deleg");
			}
			if ((this._ctxFlags & 2) == 0)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_ContextNotFrozenForCallBack"));
			}
			Context currentContext = Thread.CurrentContext;
			if (currentContext == this)
			{
				deleg();
				return;
			}
			currentContext.DoCallBackGeneric(this.InternalContextID, deleg);
			GC.KeepAlive(this);
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x00133E70 File Offset: 0x00132070
		[SecurityCritical]
		internal static void DoCallBackFromEE(IntPtr targetCtxID, IntPtr privateData, int targetDomainID)
		{
			if (targetDomainID == 0)
			{
				CallBackHelper @object = new CallBackHelper(privateData, true, targetDomainID);
				CrossContextDelegate deleg = new CrossContextDelegate(@object.Func);
				Thread.CurrentContext.DoCallBackGeneric(targetCtxID, deleg);
				return;
			}
			TransitionCall msg = new TransitionCall(targetCtxID, privateData, targetDomainID);
			Message.PropagateCallContextFromThreadToMessage(msg);
			IMessage message = Thread.CurrentContext.GetClientContextChain().SyncProcessMessage(msg);
			Message.PropagateCallContextFromMessageToThread(message);
			IMethodReturnMessage methodReturnMessage = message as IMethodReturnMessage;
			if (methodReturnMessage != null && methodReturnMessage.Exception != null)
			{
				throw methodReturnMessage.Exception;
			}
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x00133EE8 File Offset: 0x001320E8
		[SecurityCritical]
		internal void DoCallBackGeneric(IntPtr targetCtxID, CrossContextDelegate deleg)
		{
			TransitionCall msg = new TransitionCall(targetCtxID, deleg);
			Message.PropagateCallContextFromThreadToMessage(msg);
			IMessage message = this.GetClientContextChain().SyncProcessMessage(msg);
			if (message != null)
			{
				Message.PropagateCallContextFromMessageToThread(message);
			}
			IMethodReturnMessage methodReturnMessage = message as IMethodReturnMessage;
			if (methodReturnMessage != null && methodReturnMessage.Exception != null)
			{
				throw methodReturnMessage.Exception;
			}
		}

		// Token: 0x06005782 RID: 22402
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ExecuteCallBackInEE(IntPtr privateData);

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06005783 RID: 22403 RVA: 0x00133F34 File Offset: 0x00132134
		private LocalDataStore MyLocalStore
		{
			get
			{
				if (this._localDataStore == null)
				{
					LocalDataStoreMgr localDataStoreMgr = Context._localDataStoreMgr;
					lock (localDataStoreMgr)
					{
						if (this._localDataStore == null)
						{
							this._localDataStore = Context._localDataStoreMgr.CreateLocalDataStore();
						}
					}
				}
				return this._localDataStore.Store;
			}
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x00133FA0 File Offset: 0x001321A0
		[SecurityCritical]
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return Context._localDataStoreMgr.AllocateDataSlot();
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x00133FAC File Offset: 0x001321AC
		[SecurityCritical]
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			return Context._localDataStoreMgr.AllocateNamedDataSlot(name);
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x00133FB9 File Offset: 0x001321B9
		[SecurityCritical]
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			return Context._localDataStoreMgr.GetNamedDataSlot(name);
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x00133FC6 File Offset: 0x001321C6
		[SecurityCritical]
		public static void FreeNamedDataSlot(string name)
		{
			Context._localDataStoreMgr.FreeNamedDataSlot(name);
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x00133FD3 File Offset: 0x001321D3
		[SecurityCritical]
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			Thread.CurrentContext.MyLocalStore.SetData(slot, data);
		}

		// Token: 0x06005789 RID: 22409 RVA: 0x00133FE6 File Offset: 0x001321E6
		[SecurityCritical]
		public static object GetData(LocalDataStoreSlot slot)
		{
			return Thread.CurrentContext.MyLocalStore.GetData(slot);
		}

		// Token: 0x0600578A RID: 22410 RVA: 0x00133FF8 File Offset: 0x001321F8
		private int ReserveSlot()
		{
			if (this._ctxStatics == null)
			{
				this._ctxStatics = new object[8];
				this._ctxStatics[0] = null;
				this._ctxStaticsFreeIndex = 1;
				this._ctxStaticsCurrentBucket = 0;
			}
			if (this._ctxStaticsFreeIndex == 8)
			{
				object[] array = new object[8];
				object[] array2 = this._ctxStatics;
				while (array2[0] != null)
				{
					array2 = (object[])array2[0];
				}
				array2[0] = array;
				this._ctxStaticsFreeIndex = 1;
				this._ctxStaticsCurrentBucket++;
			}
			int ctxStaticsFreeIndex = this._ctxStaticsFreeIndex;
			this._ctxStaticsFreeIndex = ctxStaticsFreeIndex + 1;
			return ctxStaticsFreeIndex | this._ctxStaticsCurrentBucket << 16;
		}

		// Token: 0x0600578B RID: 22411 RVA: 0x0013408C File Offset: 0x0013228C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static bool RegisterDynamicProperty(IDynamicProperty prop, ContextBoundObject obj, Context ctx)
		{
			if (prop == null || prop.Name == null || !(prop is IContributeDynamicSink))
			{
				throw new ArgumentNullException("prop");
			}
			if (obj != null && ctx != null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NonNullObjAndCtx"));
			}
			bool result;
			if (obj != null)
			{
				result = IdentityHolder.AddDynamicProperty(obj, prop);
			}
			else
			{
				result = Context.AddDynamicProperty(ctx, prop);
			}
			return result;
		}

		// Token: 0x0600578C RID: 22412 RVA: 0x001340E8 File Offset: 0x001322E8
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static bool UnregisterDynamicProperty(string name, ContextBoundObject obj, Context ctx)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (obj != null && ctx != null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NonNullObjAndCtx"));
			}
			bool result;
			if (obj != null)
			{
				result = IdentityHolder.RemoveDynamicProperty(obj, name);
			}
			else
			{
				result = Context.RemoveDynamicProperty(ctx, name);
			}
			return result;
		}

		// Token: 0x0600578D RID: 22413 RVA: 0x00134131 File Offset: 0x00132331
		[SecurityCritical]
		internal static bool AddDynamicProperty(Context ctx, IDynamicProperty prop)
		{
			if (ctx != null)
			{
				return ctx.AddPerContextDynamicProperty(prop);
			}
			return Context.AddGlobalDynamicProperty(prop);
		}

		// Token: 0x0600578E RID: 22414 RVA: 0x00134144 File Offset: 0x00132344
		[SecurityCritical]
		private bool AddPerContextDynamicProperty(IDynamicProperty prop)
		{
			if (this._dphCtx == null)
			{
				DynamicPropertyHolder dphCtx = new DynamicPropertyHolder();
				lock (this)
				{
					if (this._dphCtx == null)
					{
						this._dphCtx = dphCtx;
					}
				}
			}
			return this._dphCtx.AddDynamicProperty(prop);
		}

		// Token: 0x0600578F RID: 22415 RVA: 0x001341A4 File Offset: 0x001323A4
		[SecurityCritical]
		private static bool AddGlobalDynamicProperty(IDynamicProperty prop)
		{
			return Context._dphGlobal.AddDynamicProperty(prop);
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x001341B1 File Offset: 0x001323B1
		[SecurityCritical]
		internal static bool RemoveDynamicProperty(Context ctx, string name)
		{
			if (ctx != null)
			{
				return ctx.RemovePerContextDynamicProperty(name);
			}
			return Context.RemoveGlobalDynamicProperty(name);
		}

		// Token: 0x06005791 RID: 22417 RVA: 0x001341C4 File Offset: 0x001323C4
		[SecurityCritical]
		private bool RemovePerContextDynamicProperty(string name)
		{
			if (this._dphCtx == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Contexts_NoProperty"), name));
			}
			return this._dphCtx.RemoveDynamicProperty(name);
		}

		// Token: 0x06005792 RID: 22418 RVA: 0x001341F5 File Offset: 0x001323F5
		[SecurityCritical]
		private static bool RemoveGlobalDynamicProperty(string name)
		{
			return Context._dphGlobal.RemoveDynamicProperty(name);
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06005793 RID: 22419 RVA: 0x00134202 File Offset: 0x00132402
		internal virtual IDynamicProperty[] PerContextDynamicProperties
		{
			get
			{
				if (this._dphCtx == null)
				{
					return null;
				}
				return this._dphCtx.DynamicProperties;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06005794 RID: 22420 RVA: 0x00134219 File Offset: 0x00132419
		internal static ArrayWithSize GlobalDynamicSinks
		{
			[SecurityCritical]
			get
			{
				return Context._dphGlobal.DynamicSinks;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06005795 RID: 22421 RVA: 0x00134225 File Offset: 0x00132425
		internal virtual ArrayWithSize DynamicSinks
		{
			[SecurityCritical]
			get
			{
				if (this._dphCtx == null)
				{
					return null;
				}
				return this._dphCtx.DynamicSinks;
			}
		}

		// Token: 0x06005796 RID: 22422 RVA: 0x0013423C File Offset: 0x0013243C
		[SecurityCritical]
		internal virtual bool NotifyDynamicSinks(IMessage msg, bool bCliSide, bool bStart, bool bAsync, bool bNotifyGlobals)
		{
			bool result = false;
			if (bNotifyGlobals && Context._dphGlobal.DynamicProperties != null)
			{
				ArrayWithSize globalDynamicSinks = Context.GlobalDynamicSinks;
				if (globalDynamicSinks != null)
				{
					DynamicPropertyHolder.NotifyDynamicSinks(msg, globalDynamicSinks, bCliSide, bStart, bAsync);
					result = true;
				}
			}
			ArrayWithSize dynamicSinks = this.DynamicSinks;
			if (dynamicSinks != null)
			{
				DynamicPropertyHolder.NotifyDynamicSinks(msg, dynamicSinks, bCliSide, bStart, bAsync);
				result = true;
			}
			return result;
		}

		// Token: 0x0400279E RID: 10142
		internal const int CTX_DEFAULT_CONTEXT = 1;

		// Token: 0x0400279F RID: 10143
		internal const int CTX_FROZEN = 2;

		// Token: 0x040027A0 RID: 10144
		internal const int CTX_THREADPOOL_AWARE = 4;

		// Token: 0x040027A1 RID: 10145
		private const int GROW_BY = 8;

		// Token: 0x040027A2 RID: 10146
		private const int STATICS_BUCKET_SIZE = 8;

		// Token: 0x040027A3 RID: 10147
		private IContextProperty[] _ctxProps;

		// Token: 0x040027A4 RID: 10148
		private DynamicPropertyHolder _dphCtx;

		// Token: 0x040027A5 RID: 10149
		private volatile LocalDataStoreHolder _localDataStore;

		// Token: 0x040027A6 RID: 10150
		private IMessageSink _serverContextChain;

		// Token: 0x040027A7 RID: 10151
		private IMessageSink _clientContextChain;

		// Token: 0x040027A8 RID: 10152
		private AppDomain _appDomain;

		// Token: 0x040027A9 RID: 10153
		private object[] _ctxStatics;

		// Token: 0x040027AA RID: 10154
		private IntPtr _internalContext;

		// Token: 0x040027AB RID: 10155
		private int _ctxID;

		// Token: 0x040027AC RID: 10156
		private int _ctxFlags;

		// Token: 0x040027AD RID: 10157
		private int _numCtxProps;

		// Token: 0x040027AE RID: 10158
		private int _ctxStaticsCurrentBucket;

		// Token: 0x040027AF RID: 10159
		private int _ctxStaticsFreeIndex;

		// Token: 0x040027B0 RID: 10160
		private static DynamicPropertyHolder _dphGlobal = new DynamicPropertyHolder();

		// Token: 0x040027B1 RID: 10161
		private static LocalDataStoreMgr _localDataStoreMgr = new LocalDataStoreMgr();

		// Token: 0x040027B2 RID: 10162
		private static int _ctxIDCounter = 0;
	}
}

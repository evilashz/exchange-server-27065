using System;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x020007D5 RID: 2005
	[SecurityCritical]
	internal class RemotingProxy : RealProxy, IRemotingTypeInfo
	{
		// Token: 0x06005739 RID: 22329 RVA: 0x00132BC6 File Offset: 0x00130DC6
		public RemotingProxy(Type serverType) : base(serverType)
		{
		}

		// Token: 0x0600573A RID: 22330 RVA: 0x00132BCF File Offset: 0x00130DCF
		private RemotingProxy()
		{
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x0600573B RID: 22331 RVA: 0x00132BD7 File Offset: 0x00130DD7
		// (set) Token: 0x0600573C RID: 22332 RVA: 0x00132BDF File Offset: 0x00130DDF
		internal int CtorThread
		{
			get
			{
				return this._ctorThread;
			}
			set
			{
				this._ctorThread = value;
			}
		}

		// Token: 0x0600573D RID: 22333 RVA: 0x00132BE8 File Offset: 0x00130DE8
		internal static IMessage CallProcessMessage(IMessageSink ms, IMessage reqMsg, ArrayWithSize proxySinks, Thread currentThread, Context currentContext, bool bSkippingContextChain)
		{
			if (proxySinks != null)
			{
				DynamicPropertyHolder.NotifyDynamicSinks(reqMsg, proxySinks, true, true, false);
			}
			bool flag = false;
			if (bSkippingContextChain)
			{
				flag = currentContext.NotifyDynamicSinks(reqMsg, true, true, false, true);
				ChannelServices.NotifyProfiler(reqMsg, RemotingProfilerEvent.ClientSend);
			}
			if (ms == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_NoChannelSink"));
			}
			IMessage message = ms.SyncProcessMessage(reqMsg);
			if (bSkippingContextChain)
			{
				ChannelServices.NotifyProfiler(message, RemotingProfilerEvent.ClientReceive);
				if (flag)
				{
					currentContext.NotifyDynamicSinks(message, true, false, false, true);
				}
			}
			IMethodReturnMessage methodReturnMessage = message as IMethodReturnMessage;
			if (message == null || methodReturnMessage == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			}
			if (proxySinks != null)
			{
				DynamicPropertyHolder.NotifyDynamicSinks(message, proxySinks, true, false, false);
			}
			return message;
		}

		// Token: 0x0600573E RID: 22334 RVA: 0x00132C80 File Offset: 0x00130E80
		[SecurityCritical]
		public override IMessage Invoke(IMessage reqMsg)
		{
			IConstructionCallMessage constructionCallMessage = reqMsg as IConstructionCallMessage;
			if (constructionCallMessage != null)
			{
				return this.InternalActivate(constructionCallMessage);
			}
			if (!base.Initialized)
			{
				if (this.CtorThread != Thread.CurrentThread.GetHashCode())
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_InvalidCall"));
				}
				ServerIdentity serverIdentity = this.IdentityObject as ServerIdentity;
				RemotingServices.Wrap((ContextBoundObject)base.UnwrappedServerObject);
			}
			int callType = 0;
			Message message = reqMsg as Message;
			if (message != null)
			{
				callType = message.GetCallType();
			}
			return this.InternalInvoke((IMethodCallMessage)reqMsg, false, callType);
		}

		// Token: 0x0600573F RID: 22335 RVA: 0x00132D0C File Offset: 0x00130F0C
		internal virtual IMessage InternalInvoke(IMethodCallMessage reqMcmMsg, bool useDispatchMessage, int callType)
		{
			Message message = reqMcmMsg as Message;
			if (message == null && callType != 0)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_InvalidCallType"));
			}
			IMessage result = null;
			Thread currentThread = Thread.CurrentThread;
			LogicalCallContext logicalCallContext = currentThread.GetMutableExecutionContext().LogicalCallContext;
			Identity identityObject = this.IdentityObject;
			ServerIdentity serverIdentity = identityObject as ServerIdentity;
			if (serverIdentity != null && identityObject.IsFullyDisconnected())
			{
				throw new ArgumentException(Environment.GetResourceString("Remoting_ServerObjectNotFound", new object[]
				{
					reqMcmMsg.Uri
				}));
			}
			MethodBase methodBase = reqMcmMsg.MethodBase;
			if (RemotingProxy._getTypeMethod == methodBase)
			{
				Type proxiedType = base.GetProxiedType();
				return new ReturnMessage(proxiedType, null, 0, logicalCallContext, reqMcmMsg);
			}
			if (RemotingProxy._getHashCodeMethod == methodBase)
			{
				int hashCode = identityObject.GetHashCode();
				return new ReturnMessage(hashCode, null, 0, logicalCallContext, reqMcmMsg);
			}
			if (identityObject.ChannelSink == null)
			{
				IMessageSink chnlSink = null;
				IMessageSink envoySink = null;
				if (!identityObject.ObjectRef.IsObjRefLite())
				{
					RemotingServices.CreateEnvoyAndChannelSinks(null, identityObject.ObjectRef, out chnlSink, out envoySink);
				}
				else
				{
					RemotingServices.CreateEnvoyAndChannelSinks(identityObject.ObjURI, null, out chnlSink, out envoySink);
				}
				RemotingServices.SetEnvoyAndChannelSinks(identityObject, chnlSink, envoySink);
				if (identityObject.ChannelSink == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_NoChannelSink"));
				}
			}
			IInternalMessage internalMessage = (IInternalMessage)reqMcmMsg;
			internalMessage.IdentityObject = identityObject;
			if (serverIdentity != null)
			{
				internalMessage.ServerIdentityObject = serverIdentity;
			}
			else
			{
				internalMessage.SetURI(identityObject.URI);
			}
			switch (callType)
			{
			case 0:
			{
				bool bSkippingContextChain = false;
				Context currentContextInternal = currentThread.GetCurrentContextInternal();
				IMessageSink messageSink = identityObject.EnvoyChain;
				if (currentContextInternal.IsDefaultContext && messageSink is EnvoyTerminatorSink)
				{
					bSkippingContextChain = true;
					messageSink = identityObject.ChannelSink;
				}
				result = RemotingProxy.CallProcessMessage(messageSink, reqMcmMsg, identityObject.ProxySideDynamicSinks, currentThread, currentContextInternal, bSkippingContextChain);
				break;
			}
			case 1:
			case 9:
			{
				logicalCallContext = (LogicalCallContext)logicalCallContext.Clone();
				internalMessage.SetCallContext(logicalCallContext);
				AsyncResult asyncResult = new AsyncResult(message);
				this.InternalInvokeAsync(asyncResult, message, useDispatchMessage, callType);
				result = new ReturnMessage(asyncResult, null, 0, null, message);
				break;
			}
			case 2:
				result = RealProxy.EndInvokeHelper(message, true);
				break;
			case 8:
				logicalCallContext = (LogicalCallContext)logicalCallContext.Clone();
				internalMessage.SetCallContext(logicalCallContext);
				this.InternalInvokeAsync(null, message, useDispatchMessage, callType);
				result = new ReturnMessage(null, null, 0, null, reqMcmMsg);
				break;
			case 10:
				result = new ReturnMessage(null, null, 0, null, reqMcmMsg);
				break;
			}
			return result;
		}

		// Token: 0x06005740 RID: 22336 RVA: 0x00132F68 File Offset: 0x00131168
		internal void InternalInvokeAsync(IMessageSink ar, Message reqMsg, bool useDispatchMessage, int callType)
		{
			Identity identityObject = this.IdentityObject;
			ServerIdentity serverIdentity = identityObject as ServerIdentity;
			MethodCall methodCall = new MethodCall(reqMsg);
			IInternalMessage internalMessage = methodCall;
			internalMessage.IdentityObject = identityObject;
			if (serverIdentity != null)
			{
				internalMessage.ServerIdentityObject = serverIdentity;
			}
			if (useDispatchMessage)
			{
				IMessageCtrl messageCtrl = ChannelServices.AsyncDispatchMessage(methodCall, ((callType & 8) != 0) ? null : ar);
			}
			else
			{
				if (identityObject.EnvoyChain == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Remoting_Proxy_InvalidState"));
				}
				IMessageCtrl messageCtrl = identityObject.EnvoyChain.AsyncProcessMessage(methodCall, ((callType & 8) != 0) ? null : ar);
			}
			if ((callType & 1) != 0 && (callType & 8) != 0)
			{
				ar.SyncProcessMessage(null);
			}
		}

		// Token: 0x06005741 RID: 22337 RVA: 0x00133000 File Offset: 0x00131200
		private IConstructionReturnMessage InternalActivate(IConstructionCallMessage ctorMsg)
		{
			this.CtorThread = Thread.CurrentThread.GetHashCode();
			IConstructionReturnMessage result = ActivationServices.Activate(this, ctorMsg);
			base.Initialized = true;
			return result;
		}

		// Token: 0x06005742 RID: 22338 RVA: 0x00133030 File Offset: 0x00131230
		private static void Invoke(object NotUsed, ref MessageData msgData)
		{
			Message message = new Message();
			message.InitFields(msgData);
			object thisPtr = message.GetThisPtr();
			Delegate @delegate;
			if ((@delegate = (thisPtr as Delegate)) == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
			RemotingProxy remotingProxy = (RemotingProxy)RemotingServices.GetRealProxy(@delegate.Target);
			if (remotingProxy != null)
			{
				remotingProxy.InternalInvoke(message, true, message.GetCallType());
				return;
			}
			int callType = message.GetCallType();
			if (callType <= 2)
			{
				if (callType != 1)
				{
					if (callType != 2)
					{
						return;
					}
					RealProxy.EndInvokeHelper(message, false);
					return;
				}
			}
			else if (callType != 9)
			{
				return;
			}
			message.Properties[Message.CallContextKey] = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Clone();
			AsyncResult asyncResult = new AsyncResult(message);
			AgileAsyncWorkerItem state = new AgileAsyncWorkerItem(message, ((callType & 8) != 0) ? null : asyncResult, @delegate.Target);
			ThreadPool.QueueUserWorkItem(new WaitCallback(AgileAsyncWorkerItem.ThreadPoolCallBack), state);
			if ((callType & 8) != 0)
			{
				asyncResult.SyncProcessMessage(null);
			}
			message.PropagateOutParameters(null, asyncResult);
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06005743 RID: 22339 RVA: 0x00133139 File Offset: 0x00131339
		// (set) Token: 0x06005744 RID: 22340 RVA: 0x00133141 File Offset: 0x00131341
		internal ConstructorCallMessage ConstructorMessage
		{
			get
			{
				return this._ccm;
			}
			set
			{
				this._ccm = value;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06005745 RID: 22341 RVA: 0x0013314A File Offset: 0x0013134A
		// (set) Token: 0x06005746 RID: 22342 RVA: 0x00133157 File Offset: 0x00131357
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				return base.GetProxiedType().FullName;
			}
			[SecurityCritical]
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x00133160 File Offset: 0x00131360
		[SecurityCritical]
		public override IntPtr GetCOMIUnknown(bool fIsBeingMarshalled)
		{
			IntPtr result = IntPtr.Zero;
			object transparentProxy = this.GetTransparentProxy();
			bool flag = RemotingServices.IsObjectOutOfProcess(transparentProxy);
			if (flag)
			{
				if (fIsBeingMarshalled)
				{
					result = MarshalByRefObject.GetComIUnknown((MarshalByRefObject)transparentProxy);
				}
				else
				{
					result = MarshalByRefObject.GetComIUnknown((MarshalByRefObject)transparentProxy);
				}
			}
			else
			{
				bool flag2 = RemotingServices.IsObjectOutOfAppDomain(transparentProxy);
				if (flag2)
				{
					result = ((MarshalByRefObject)transparentProxy).GetComIUnknown(fIsBeingMarshalled);
				}
				else
				{
					result = MarshalByRefObject.GetComIUnknown((MarshalByRefObject)transparentProxy);
				}
			}
			return result;
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x001331C9 File Offset: 0x001313C9
		[SecurityCritical]
		public override void SetCOMIUnknown(IntPtr i)
		{
		}

		// Token: 0x06005749 RID: 22345 RVA: 0x001331CC File Offset: 0x001313CC
		[SecurityCritical]
		public bool CanCastTo(Type castType, object o)
		{
			if (castType == null)
			{
				throw new ArgumentNullException("castType");
			}
			RuntimeType runtimeType = castType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			bool flag = false;
			if (runtimeType == RemotingProxy.s_typeofObject || runtimeType == RemotingProxy.s_typeofMarshalByRefObject)
			{
				return true;
			}
			ObjRef objectRef = this.IdentityObject.ObjectRef;
			if (objectRef != null)
			{
				object transparentProxy = this.GetTransparentProxy();
				IRemotingTypeInfo typeInfo = objectRef.TypeInfo;
				if (typeInfo != null)
				{
					flag = typeInfo.CanCastTo(runtimeType, transparentProxy);
					if (!flag && typeInfo.GetType() == typeof(TypeInfo) && objectRef.IsWellKnown())
					{
						flag = this.CanCastToWK(runtimeType);
					}
				}
				else if (objectRef.IsObjRefLite())
				{
					flag = MarshalByRefObject.CanCastToXmlTypeHelper(runtimeType, (MarshalByRefObject)o);
				}
			}
			else
			{
				flag = this.CanCastToWK(runtimeType);
			}
			return flag;
		}

		// Token: 0x0600574A RID: 22346 RVA: 0x001332A4 File Offset: 0x001314A4
		private bool CanCastToWK(Type castType)
		{
			bool result = false;
			if (castType.IsClass)
			{
				result = base.GetProxiedType().IsAssignableFrom(castType);
			}
			else if (!(this.IdentityObject is ServerIdentity))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0400278D RID: 10125
		private static MethodInfo _getTypeMethod = typeof(object).GetMethod("GetType");

		// Token: 0x0400278E RID: 10126
		private static MethodInfo _getHashCodeMethod = typeof(object).GetMethod("GetHashCode");

		// Token: 0x0400278F RID: 10127
		private static RuntimeType s_typeofObject = (RuntimeType)typeof(object);

		// Token: 0x04002790 RID: 10128
		private static RuntimeType s_typeofMarshalByRefObject = (RuntimeType)typeof(MarshalByRefObject);

		// Token: 0x04002791 RID: 10129
		private ConstructorCallMessage _ccm;

		// Token: 0x04002792 RID: 10130
		private int _ctorThread;
	}
}

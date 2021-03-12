using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x0200079D RID: 1949
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public static class RemotingServices
	{
		// Token: 0x06005503 RID: 21763
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsTransparentProxy(object proxy);

		// Token: 0x06005504 RID: 21764 RVA: 0x0012CB64 File Offset: 0x0012AD64
		[SecuritySafeCritical]
		public static bool IsObjectOutOfContext(object tp)
		{
			if (!RemotingServices.IsTransparentProxy(tp))
			{
				return false;
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			ServerIdentity serverIdentity = identityObject as ServerIdentity;
			return serverIdentity == null || !(realProxy is RemotingProxy) || Thread.CurrentContext != serverIdentity.ServerContext;
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x0012CBAD File Offset: 0x0012ADAD
		[__DynamicallyInvokable]
		public static bool IsObjectOutOfAppDomain(object tp)
		{
			return RemotingServices.IsClientProxy(tp);
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x0012CBB8 File Offset: 0x0012ADB8
		internal static bool IsClientProxy(object obj)
		{
			MarshalByRefObject marshalByRefObject = obj as MarshalByRefObject;
			if (marshalByRefObject == null)
			{
				return false;
			}
			bool result = false;
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(marshalByRefObject, out flag);
			if (identity != null && !(identity is ServerIdentity))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x0012CBEC File Offset: 0x0012ADEC
		[SecurityCritical]
		internal static bool IsObjectOutOfProcess(object tp)
		{
			if (!RemotingServices.IsTransparentProxy(tp))
			{
				return false;
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			if (identityObject is ServerIdentity)
			{
				return false;
			}
			if (identityObject != null)
			{
				ObjRef objectRef = identityObject.ObjectRef;
				return objectRef == null || !objectRef.IsFromThisProcess();
			}
			return true;
		}

		// Token: 0x06005508 RID: 21768
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern RealProxy GetRealProxy(object proxy);

		// Token: 0x06005509 RID: 21769
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateTransparentProxy(RealProxy rp, RuntimeType typeToProxy, IntPtr stub, object stubData);

		// Token: 0x0600550A RID: 21770 RVA: 0x0012CC38 File Offset: 0x0012AE38
		[SecurityCritical]
		internal static object CreateTransparentProxy(RealProxy rp, Type typeToProxy, IntPtr stub, object stubData)
		{
			RuntimeType runtimeType = typeToProxy as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), "typeToProxy"));
			}
			return RemotingServices.CreateTransparentProxy(rp, runtimeType, stub, stubData);
		}

		// Token: 0x0600550B RID: 21771
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MarshalByRefObject AllocateUninitializedObject(RuntimeType objectType);

		// Token: 0x0600550C RID: 21772
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CallDefaultCtor(object o);

		// Token: 0x0600550D RID: 21773 RVA: 0x0012CC80 File Offset: 0x0012AE80
		[SecurityCritical]
		internal static MarshalByRefObject AllocateUninitializedObject(Type objectType)
		{
			RuntimeType runtimeType = objectType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), "objectType"));
			}
			return RemotingServices.AllocateUninitializedObject(runtimeType);
		}

		// Token: 0x0600550E RID: 21774
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MarshalByRefObject AllocateInitializedObject(RuntimeType objectType);

		// Token: 0x0600550F RID: 21775 RVA: 0x0012CCC4 File Offset: 0x0012AEC4
		[SecurityCritical]
		internal static MarshalByRefObject AllocateInitializedObject(Type objectType)
		{
			RuntimeType runtimeType = objectType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), "objectType"));
			}
			return RemotingServices.AllocateInitializedObject(runtimeType);
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x0012CD08 File Offset: 0x0012AF08
		[SecurityCritical]
		internal static bool RegisterWellKnownChannels()
		{
			if (!RemotingServices.s_bRegisteredWellKnownChannels)
			{
				bool flag = false;
				object configLock = Thread.GetDomain().RemotingData.ConfigLock;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(configLock, ref flag);
					if (!RemotingServices.s_bRegisteredWellKnownChannels && !RemotingServices.s_bInProcessOfRegisteringWellKnownChannels)
					{
						RemotingServices.s_bInProcessOfRegisteringWellKnownChannels = true;
						CrossAppDomainChannel.RegisterChannel();
						RemotingServices.s_bRegisteredWellKnownChannels = true;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(configLock);
					}
				}
			}
			return true;
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x0012CD80 File Offset: 0x0012AF80
		[SecurityCritical]
		internal static void InternalSetRemoteActivationConfigured()
		{
			if (!RemotingServices.s_bRemoteActivationConfigured)
			{
				RemotingServices.nSetRemoteActivationConfigured();
				RemotingServices.s_bRemoteActivationConfigured = true;
			}
		}

		// Token: 0x06005512 RID: 21778
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nSetRemoteActivationConfigured();

		// Token: 0x06005513 RID: 21779 RVA: 0x0012CD98 File Offset: 0x0012AF98
		[SecurityCritical]
		public static string GetSessionIdForMethodMessage(IMethodMessage msg)
		{
			return msg.Uri;
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x0012CDA0 File Offset: 0x0012AFA0
		[SecuritySafeCritical]
		public static object GetLifetimeService(MarshalByRefObject obj)
		{
			if (obj != null)
			{
				return obj.GetLifetimeService();
			}
			return null;
		}

		// Token: 0x06005515 RID: 21781 RVA: 0x0012CDB0 File Offset: 0x0012AFB0
		[SecurityCritical]
		public static string GetObjectUri(MarshalByRefObject obj)
		{
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			if (identity != null)
			{
				return identity.URI;
			}
			return null;
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x0012CDD4 File Offset: 0x0012AFD4
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void SetObjectUriForMarshal(MarshalByRefObject obj, string uri)
		{
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			Identity identity2 = identity as ServerIdentity;
			if (identity != null && identity2 == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__ObjectNeedsToBeLocal"));
			}
			if (identity != null && identity.URI != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
			}
			if (identity == null)
			{
				Context defaultContext = Thread.GetDomain().GetDefaultContext();
				ServerIdentity serverIdentity = new ServerIdentity(obj, defaultContext, uri);
				identity = obj.__RaceSetServerIdentity(serverIdentity);
				if (identity != serverIdentity)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
				}
			}
			else
			{
				identity.SetOrCreateURI(uri, true);
			}
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x0012CE66 File Offset: 0x0012B066
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ObjRef Marshal(MarshalByRefObject Obj)
		{
			return RemotingServices.MarshalInternal(Obj, null, null);
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x0012CE70 File Offset: 0x0012B070
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ObjRef Marshal(MarshalByRefObject Obj, string URI)
		{
			return RemotingServices.MarshalInternal(Obj, URI, null);
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x0012CE7A File Offset: 0x0012B07A
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ObjRef Marshal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
		{
			return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType);
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x0012CE84 File Offset: 0x0012B084
		[SecurityCritical]
		internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
		{
			return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType, true);
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x0012CE8F File Offset: 0x0012B08F
		[SecurityCritical]
		internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType, bool updateChannelData)
		{
			return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType, updateChannelData, false);
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x0012CE9C File Offset: 0x0012B09C
		[SecurityCritical]
		internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType, bool updateChannelData, bool isInitializing)
		{
			if (Obj == null)
			{
				return null;
			}
			ObjRef objRef = null;
			Identity orCreateIdentity = RemotingServices.GetOrCreateIdentity(Obj, ObjURI, isInitializing);
			if (RequestedType != null)
			{
				ServerIdentity serverIdentity = orCreateIdentity as ServerIdentity;
				if (serverIdentity != null)
				{
					serverIdentity.ServerType = RequestedType;
					serverIdentity.MarshaledAsSpecificType = true;
				}
			}
			objRef = orCreateIdentity.ObjectRef;
			if (objRef == null)
			{
				if (RemotingServices.IsTransparentProxy(Obj))
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(Obj);
					objRef = realProxy.CreateObjRef(RequestedType);
				}
				else
				{
					objRef = Obj.CreateObjRef(RequestedType);
				}
				if (orCreateIdentity == null || objRef == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidMarshalByRefObject"), "Obj");
				}
				objRef = orCreateIdentity.RaceSetObjRef(objRef);
			}
			ServerIdentity serverIdentity2 = orCreateIdentity as ServerIdentity;
			if (serverIdentity2 != null)
			{
				MarshalByRefObject marshalByRefObject = null;
				serverIdentity2.GetServerObjectChain(out marshalByRefObject);
				Lease lease = orCreateIdentity.Lease;
				if (lease != null)
				{
					Lease obj = lease;
					lock (obj)
					{
						if (lease.CurrentState == LeaseState.Expired)
						{
							lease.ActivateLease();
						}
						else
						{
							lease.RenewInternal(orCreateIdentity.Lease.InitialLeaseTime);
						}
					}
				}
				if (updateChannelData && objRef.ChannelInfo != null)
				{
					object[] currentChannelData = ChannelServices.CurrentChannelData;
					if (!(Obj is AppDomain))
					{
						objRef.ChannelInfo.ChannelData = currentChannelData;
					}
					else
					{
						int num = currentChannelData.Length;
						object[] array = new object[num];
						Array.Copy(currentChannelData, array, num);
						for (int i = 0; i < num; i++)
						{
							if (!(array[i] is CrossAppDomainData))
							{
								array[i] = null;
							}
						}
						objRef.ChannelInfo.ChannelData = array;
					}
				}
			}
			TrackingServices.MarshaledObject(Obj, objRef);
			return objRef;
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x0012D024 File Offset: 0x0012B224
		[SecurityCritical]
		private static Identity GetOrCreateIdentity(MarshalByRefObject Obj, string ObjURI, bool isInitializing)
		{
			int num = 2;
			if (isInitializing)
			{
				num |= 4;
			}
			Identity identity;
			if (RemotingServices.IsTransparentProxy(Obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(Obj);
				identity = realProxy.IdentityObject;
				if (identity == null)
				{
					identity = IdentityHolder.FindOrCreateServerIdentity(Obj, ObjURI, num);
					identity.RaceSetTransparentProxy(Obj);
				}
				ServerIdentity serverIdentity = identity as ServerIdentity;
				if (serverIdentity != null)
				{
					identity = IdentityHolder.FindOrCreateServerIdentity(serverIdentity.TPOrObject, ObjURI, num);
					if (ObjURI != null && ObjURI != Identity.RemoveAppNameOrAppGuidIfNecessary(identity.ObjURI))
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_URIExists"));
					}
				}
				else if (ObjURI != null && ObjURI != identity.ObjURI)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_URIToProxy"));
				}
			}
			else
			{
				identity = IdentityHolder.FindOrCreateServerIdentity(Obj, ObjURI, num);
			}
			return identity;
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x0012D0D4 File Offset: 0x0012B2D4
		[SecurityCritical]
		public static void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			ObjRef objRef = RemotingServices.MarshalInternal((MarshalByRefObject)obj, null, null);
			objRef.GetObjectData(info, context);
		}

		// Token: 0x0600551F RID: 21791 RVA: 0x0012D113 File Offset: 0x0012B313
		[SecurityCritical]
		public static object Unmarshal(ObjRef objectRef)
		{
			return RemotingServices.InternalUnmarshal(objectRef, null, false);
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x0012D11D File Offset: 0x0012B31D
		[SecurityCritical]
		public static object Unmarshal(ObjRef objectRef, bool fRefine)
		{
			return RemotingServices.InternalUnmarshal(objectRef, null, fRefine);
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x0012D127 File Offset: 0x0012B327
		[SecurityCritical]
		[ComVisible(true)]
		public static object Connect(Type classToProxy, string url)
		{
			return RemotingServices.Unmarshal(classToProxy, url, null);
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x0012D131 File Offset: 0x0012B331
		[SecurityCritical]
		[ComVisible(true)]
		public static object Connect(Type classToProxy, string url, object data)
		{
			return RemotingServices.Unmarshal(classToProxy, url, data);
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x0012D13B File Offset: 0x0012B33B
		[SecurityCritical]
		public static bool Disconnect(MarshalByRefObject obj)
		{
			return RemotingServices.Disconnect(obj, true);
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x0012D144 File Offset: 0x0012B344
		[SecurityCritical]
		internal static bool Disconnect(MarshalByRefObject obj, bool bResetURI)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			bool result = false;
			if (identity != null)
			{
				if (!(identity is ServerIdentity))
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_CantDisconnectClientProxy"));
				}
				if (identity.IsInIDTable())
				{
					IdentityHolder.RemoveIdentity(identity.URI, bResetURI);
					result = true;
				}
				TrackingServices.DisconnectedObject(obj);
			}
			return result;
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x0012D1A4 File Offset: 0x0012B3A4
		[SecurityCritical]
		public static IMessageSink GetEnvoyChainForProxy(MarshalByRefObject obj)
		{
			IMessageSink result = null;
			if (RemotingServices.IsObjectOutOfContext(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				Identity identityObject = realProxy.IdentityObject;
				if (identityObject != null)
				{
					result = identityObject.EnvoyChain;
				}
			}
			return result;
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x0012D1D4 File Offset: 0x0012B3D4
		[SecurityCritical]
		public static ObjRef GetObjRefForProxy(MarshalByRefObject obj)
		{
			ObjRef result = null;
			if (!RemotingServices.IsTransparentProxy(obj))
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_BadType"));
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(obj);
			Identity identityObject = realProxy.IdentityObject;
			if (identityObject != null)
			{
				result = identityObject.ObjectRef;
			}
			return result;
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x0012D214 File Offset: 0x0012B414
		[SecurityCritical]
		internal static object Unmarshal(Type classToProxy, string url)
		{
			return RemotingServices.Unmarshal(classToProxy, url, null);
		}

		// Token: 0x06005528 RID: 21800 RVA: 0x0012D220 File Offset: 0x0012B420
		[SecurityCritical]
		internal static object Unmarshal(Type classToProxy, string url, object data)
		{
			if (null == classToProxy)
			{
				throw new ArgumentNullException("classToProxy");
			}
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (!classToProxy.IsMarshalByRef && !classToProxy.IsInterface)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_NotRemotableByReference"));
			}
			Identity identity = IdentityHolder.ResolveIdentity(url);
			if (identity == null || identity.ChannelSink == null || identity.EnvoyChain == null)
			{
				IMessageSink messageSink = null;
				IMessageSink envoySink = null;
				string text = RemotingServices.CreateEnvoyAndChannelSinks(url, data, out messageSink, out envoySink);
				if (messageSink == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Connect_CantCreateChannelSink"), url));
				}
				if (text == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
				}
				identity = IdentityHolder.FindOrCreateIdentity(text, url, null);
				RemotingServices.SetEnvoyAndChannelSinks(identity, messageSink, envoySink);
			}
			return RemotingServices.GetOrCreateProxy(classToProxy, identity);
		}

		// Token: 0x06005529 RID: 21801 RVA: 0x0012D2EA File Offset: 0x0012B4EA
		[SecurityCritical]
		internal static object Wrap(ContextBoundObject obj)
		{
			return RemotingServices.Wrap(obj, null, true);
		}

		// Token: 0x0600552A RID: 21802 RVA: 0x0012D2F4 File Offset: 0x0012B4F4
		[SecurityCritical]
		internal static object Wrap(ContextBoundObject obj, object proxy, bool fCreateSinks)
		{
			if (obj != null && !RemotingServices.IsTransparentProxy(obj))
			{
				Identity idObj;
				if (proxy != null)
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(proxy);
					if (realProxy.UnwrappedServerObject == null)
					{
						realProxy.AttachServerHelper(obj);
					}
					idObj = MarshalByRefObject.GetIdentity(obj);
				}
				else
				{
					idObj = IdentityHolder.FindOrCreateServerIdentity(obj, null, 0);
				}
				proxy = RemotingServices.GetOrCreateProxy(idObj, proxy, true);
				RemotingServices.GetRealProxy(proxy).Wrap();
				if (fCreateSinks)
				{
					IMessageSink chnlSink = null;
					IMessageSink envoySink = null;
					RemotingServices.CreateEnvoyAndChannelSinks((MarshalByRefObject)proxy, null, out chnlSink, out envoySink);
					RemotingServices.SetEnvoyAndChannelSinks(idObj, chnlSink, envoySink);
				}
				RealProxy realProxy2 = RemotingServices.GetRealProxy(proxy);
				if (realProxy2.UnwrappedServerObject == null)
				{
					realProxy2.AttachServerHelper(obj);
				}
				return proxy;
			}
			return obj;
		}

		// Token: 0x0600552B RID: 21803 RVA: 0x0012D38C File Offset: 0x0012B58C
		internal static string GetObjectUriFromFullUri(string fullUri)
		{
			if (fullUri == null)
			{
				return null;
			}
			int num = fullUri.LastIndexOf('/');
			if (num == -1)
			{
				return fullUri;
			}
			return fullUri.Substring(num + 1);
		}

		// Token: 0x0600552C RID: 21804
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object Unwrap(ContextBoundObject obj);

		// Token: 0x0600552D RID: 21805
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object AlwaysUnwrap(ContextBoundObject obj);

		// Token: 0x0600552E RID: 21806 RVA: 0x0012D3B8 File Offset: 0x0012B5B8
		[SecurityCritical]
		internal static object InternalUnmarshal(ObjRef objectRef, object proxy, bool fRefine)
		{
			Context currentContext = Thread.CurrentContext;
			if (!ObjRef.IsWellFormed(objectRef))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_BadObjRef"), "Unmarshal"));
			}
			object obj;
			Identity identity;
			if (objectRef.IsWellKnown())
			{
				obj = RemotingServices.Unmarshal(typeof(MarshalByRefObject), objectRef.URI);
				identity = IdentityHolder.ResolveIdentity(objectRef.URI);
				if (identity.ObjectRef == null)
				{
					identity.RaceSetObjRef(objectRef);
				}
				return obj;
			}
			identity = IdentityHolder.FindOrCreateIdentity(objectRef.URI, null, objectRef);
			currentContext = Thread.CurrentContext;
			ServerIdentity serverIdentity = identity as ServerIdentity;
			if (serverIdentity != null)
			{
				currentContext = Thread.CurrentContext;
				if (!serverIdentity.IsContextBound)
				{
					if (proxy != null)
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_ProxySameAppDomain"), Array.Empty<object>()));
					}
					obj = serverIdentity.TPOrObject;
				}
				else
				{
					IMessageSink chnlSink = null;
					IMessageSink envoySink = null;
					RemotingServices.CreateEnvoyAndChannelSinks(serverIdentity.TPOrObject, null, out chnlSink, out envoySink);
					RemotingServices.SetEnvoyAndChannelSinks(identity, chnlSink, envoySink);
					obj = RemotingServices.GetOrCreateProxy(identity, proxy, true);
				}
			}
			else
			{
				IMessageSink chnlSink2 = null;
				IMessageSink envoySink2 = null;
				if (!objectRef.IsObjRefLite())
				{
					RemotingServices.CreateEnvoyAndChannelSinks(null, objectRef, out chnlSink2, out envoySink2);
				}
				else
				{
					RemotingServices.CreateEnvoyAndChannelSinks(objectRef.URI, null, out chnlSink2, out envoySink2);
				}
				RemotingServices.SetEnvoyAndChannelSinks(identity, chnlSink2, envoySink2);
				if (objectRef.HasProxyAttribute())
				{
					fRefine = true;
				}
				obj = RemotingServices.GetOrCreateProxy(identity, proxy, fRefine);
			}
			TrackingServices.UnmarshaledObject(obj, objectRef);
			return obj;
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x0012D508 File Offset: 0x0012B708
		[SecurityCritical]
		private static object GetOrCreateProxy(Identity idObj, object proxy, bool fRefine)
		{
			if (proxy == null)
			{
				ServerIdentity serverIdentity = idObj as ServerIdentity;
				Type type;
				if (serverIdentity != null)
				{
					type = serverIdentity.ServerType;
				}
				else
				{
					IRemotingTypeInfo typeInfo = idObj.ObjectRef.TypeInfo;
					type = null;
					if ((typeInfo is TypeInfo && !fRefine) || typeInfo == null)
					{
						type = typeof(MarshalByRefObject);
					}
					else
					{
						string typeName = typeInfo.TypeName;
						if (typeName != null)
						{
							string name = null;
							string assemblyName = null;
							TypeInfo.ParseTypeAndAssembly(typeName, out name, out assemblyName);
							Assembly assembly = FormatterServices.LoadAssemblyFromStringNoThrow(assemblyName);
							if (assembly != null)
							{
								type = assembly.GetType(name, false, false);
							}
						}
					}
					if (null == type)
					{
						throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), typeInfo.TypeName));
					}
				}
				proxy = RemotingServices.SetOrCreateProxy(idObj, type, null);
			}
			else
			{
				proxy = RemotingServices.SetOrCreateProxy(idObj, null, proxy);
			}
			if (proxy == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_UnexpectedNullTP"));
			}
			return proxy;
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x0012D5E8 File Offset: 0x0012B7E8
		[SecurityCritical]
		private static object GetOrCreateProxy(Type classToProxy, Identity idObj)
		{
			object obj = idObj.TPOrObject;
			if (obj == null)
			{
				obj = RemotingServices.SetOrCreateProxy(idObj, classToProxy, null);
			}
			ServerIdentity serverIdentity = idObj as ServerIdentity;
			if (serverIdentity != null)
			{
				Type serverType = serverIdentity.ServerType;
				if (!classToProxy.IsAssignableFrom(serverType))
				{
					throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidCast_FromTo"), serverType.FullName, classToProxy.FullName));
				}
			}
			return obj;
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x0012D64C File Offset: 0x0012B84C
		[SecurityCritical]
		private static MarshalByRefObject SetOrCreateProxy(Identity idObj, Type classToProxy, object proxy)
		{
			RealProxy realProxy = null;
			if (proxy == null)
			{
				ServerIdentity serverIdentity = idObj as ServerIdentity;
				if (idObj.ObjectRef != null)
				{
					ProxyAttribute proxyAttribute = ActivationServices.GetProxyAttribute(classToProxy);
					realProxy = proxyAttribute.CreateProxy(idObj.ObjectRef, classToProxy, null, null);
				}
				if (realProxy == null)
				{
					ProxyAttribute defaultProxyAttribute = ActivationServices.DefaultProxyAttribute;
					realProxy = defaultProxyAttribute.CreateProxy(idObj.ObjectRef, classToProxy, null, (serverIdentity == null) ? null : serverIdentity.ServerContext);
				}
			}
			else
			{
				realProxy = RemotingServices.GetRealProxy(proxy);
			}
			realProxy.IdentityObject = idObj;
			proxy = realProxy.GetTransparentProxy();
			proxy = idObj.RaceSetTransparentProxy(proxy);
			return (MarshalByRefObject)proxy;
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x0012D6D0 File Offset: 0x0012B8D0
		private static bool AreChannelDataElementsNull(object[] channelData)
		{
			foreach (object obj in channelData)
			{
				if (obj != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x0012D6F8 File Offset: 0x0012B8F8
		[SecurityCritical]
		internal static void CreateEnvoyAndChannelSinks(MarshalByRefObject tpOrObject, ObjRef objectRef, out IMessageSink chnlSink, out IMessageSink envoySink)
		{
			chnlSink = null;
			envoySink = null;
			if (objectRef == null)
			{
				chnlSink = ChannelServices.GetCrossContextChannelSink();
				envoySink = Thread.CurrentContext.CreateEnvoyChain(tpOrObject);
				return;
			}
			object[] channelData = objectRef.ChannelInfo.ChannelData;
			if (channelData != null && !RemotingServices.AreChannelDataElementsNull(channelData))
			{
				for (int i = 0; i < channelData.Length; i++)
				{
					chnlSink = ChannelServices.CreateMessageSink(channelData[i]);
					if (chnlSink != null)
					{
						break;
					}
				}
				if (chnlSink == null)
				{
					object obj = RemotingServices.s_delayLoadChannelLock;
					lock (obj)
					{
						for (int j = 0; j < channelData.Length; j++)
						{
							chnlSink = ChannelServices.CreateMessageSink(channelData[j]);
							if (chnlSink != null)
							{
								break;
							}
						}
						if (chnlSink == null)
						{
							foreach (object data in channelData)
							{
								string text;
								chnlSink = RemotingConfigHandler.FindDelayLoadChannelForCreateMessageSink(null, data, out text);
								if (chnlSink != null)
								{
									break;
								}
							}
						}
					}
				}
			}
			if (objectRef.EnvoyInfo != null && objectRef.EnvoyInfo.EnvoySinks != null)
			{
				envoySink = objectRef.EnvoyInfo.EnvoySinks;
				return;
			}
			envoySink = EnvoyTerminatorSink.MessageSink;
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x0012D80C File Offset: 0x0012BA0C
		[SecurityCritical]
		internal static string CreateEnvoyAndChannelSinks(string url, object data, out IMessageSink chnlSink, out IMessageSink envoySink)
		{
			string result = RemotingServices.CreateChannelSink(url, data, out chnlSink);
			envoySink = EnvoyTerminatorSink.MessageSink;
			return result;
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x0012D82C File Offset: 0x0012BA2C
		[SecurityCritical]
		private static string CreateChannelSink(string url, object data, out IMessageSink chnlSink)
		{
			string result = null;
			chnlSink = ChannelServices.CreateMessageSink(url, data, out result);
			if (chnlSink == null)
			{
				object obj = RemotingServices.s_delayLoadChannelLock;
				lock (obj)
				{
					chnlSink = ChannelServices.CreateMessageSink(url, data, out result);
					if (chnlSink == null)
					{
						chnlSink = RemotingConfigHandler.FindDelayLoadChannelForCreateMessageSink(url, data, out result);
					}
				}
			}
			return result;
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x0012D894 File Offset: 0x0012BA94
		internal static void SetEnvoyAndChannelSinks(Identity idObj, IMessageSink chnlSink, IMessageSink envoySink)
		{
			if (idObj.ChannelSink == null && chnlSink != null)
			{
				idObj.RaceSetChannelSink(chnlSink);
			}
			if (idObj.EnvoyChain != null)
			{
				return;
			}
			if (envoySink != null)
			{
				idObj.RaceSetEnvoyChain(envoySink);
				return;
			}
			throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_FailEnvoySink"), Array.Empty<object>()));
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x0012D8E8 File Offset: 0x0012BAE8
		[SecurityCritical]
		private static bool CheckCast(RealProxy rp, RuntimeType castType)
		{
			bool result = false;
			if (castType == typeof(object))
			{
				return true;
			}
			if (!castType.IsInterface && !castType.IsMarshalByRef)
			{
				return false;
			}
			if (castType != typeof(IObjectReference))
			{
				IRemotingTypeInfo remotingTypeInfo = rp as IRemotingTypeInfo;
				if (remotingTypeInfo != null)
				{
					result = remotingTypeInfo.CanCastTo(castType, rp.GetTransparentProxy());
				}
				else
				{
					Identity identityObject = rp.IdentityObject;
					if (identityObject != null)
					{
						ObjRef objectRef = identityObject.ObjectRef;
						if (objectRef != null)
						{
							remotingTypeInfo = objectRef.TypeInfo;
							if (remotingTypeInfo != null)
							{
								result = remotingTypeInfo.CanCastTo(castType, rp.GetTransparentProxy());
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x0012D976 File Offset: 0x0012BB76
		[SecurityCritical]
		internal static bool ProxyCheckCast(RealProxy rp, RuntimeType castType)
		{
			return RemotingServices.CheckCast(rp, castType);
		}

		// Token: 0x06005539 RID: 21817
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CheckCast(object objToExpand, RuntimeType type);

		// Token: 0x0600553A RID: 21818 RVA: 0x0012D980 File Offset: 0x0012BB80
		[SecurityCritical]
		internal static GCHandle CreateDelegateInvocation(WaitCallback waitDelegate, object state)
		{
			return GCHandle.Alloc(new object[]
			{
				waitDelegate,
				state
			});
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x0012D9A2 File Offset: 0x0012BBA2
		[SecurityCritical]
		internal static void DisposeDelegateInvocation(GCHandle delegateCallToken)
		{
			delegateCallToken.Free();
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x0012D9AC File Offset: 0x0012BBAC
		[SecurityCritical]
		internal static object CreateProxyForDomain(int appDomainId, IntPtr defCtxID)
		{
			ObjRef objectRef = RemotingServices.CreateDataForDomain(appDomainId, defCtxID);
			return (AppDomain)RemotingServices.Unmarshal(objectRef);
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x0012D9D0 File Offset: 0x0012BBD0
		[SecurityCritical]
		internal static object CreateDataForDomainCallback(object[] args)
		{
			RemotingServices.RegisterWellKnownChannels();
			ObjRef objRef = RemotingServices.MarshalInternal(Thread.CurrentContext.AppDomain, null, null, false);
			ServerIdentity serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(Thread.CurrentContext.AppDomain);
			serverIdentity.SetHandle();
			objRef.SetServerIdentity(serverIdentity.GetHandle());
			objRef.SetDomainID(AppDomain.CurrentDomain.GetId());
			return objRef;
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x0012DA30 File Offset: 0x0012BC30
		[SecurityCritical]
		internal static ObjRef CreateDataForDomain(int appDomainId, IntPtr defCtxID)
		{
			RemotingServices.RegisterWellKnownChannels();
			InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(RemotingServices.CreateDataForDomainCallback);
			return (ObjRef)Thread.CurrentThread.InternalCrossContextCallback(null, defCtxID, appDomainId, ftnToCall, null);
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x0012DA64 File Offset: 0x0012BC64
		[SecurityCritical]
		public static MethodBase GetMethodBaseFromMethodMessage(IMethodMessage msg)
		{
			return RemotingServices.InternalGetMethodBaseFromMethodMessage(msg);
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x0012DA7C File Offset: 0x0012BC7C
		[SecurityCritical]
		internal static MethodBase InternalGetMethodBaseFromMethodMessage(IMethodMessage msg)
		{
			if (msg == null)
			{
				return null;
			}
			Type type = RemotingServices.InternalGetTypeFromQualifiedTypeName(msg.TypeName);
			if (type == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), msg.TypeName));
			}
			Type[] signature = (Type[])msg.MethodSignature;
			return RemotingServices.GetMethodBase(msg, type, signature);
		}

		// Token: 0x06005541 RID: 21825 RVA: 0x0012DAD8 File Offset: 0x0012BCD8
		[SecurityCritical]
		public static bool IsMethodOverloaded(IMethodMessage msg)
		{
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(msg.MethodBase);
			return reflectionCachedData.IsOverloaded();
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x0012DAF8 File Offset: 0x0012BCF8
		[SecurityCritical]
		private static MethodBase GetMethodBase(IMethodMessage msg, Type t, Type[] signature)
		{
			MethodBase result = null;
			if (msg is IConstructionCallMessage || msg is IConstructionReturnMessage)
			{
				if (signature == null)
				{
					RuntimeType runtimeType = t as RuntimeType;
					ConstructorInfo[] constructors;
					if (runtimeType == null)
					{
						constructors = t.GetConstructors();
					}
					else
					{
						constructors = runtimeType.GetConstructors();
					}
					if (1 != constructors.Length)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Remoting_AmbiguousCTOR"));
					}
					result = constructors[0];
				}
				else
				{
					RuntimeType runtimeType2 = t as RuntimeType;
					if (runtimeType2 == null)
					{
						result = t.GetConstructor(signature);
					}
					else
					{
						result = runtimeType2.GetConstructor(signature);
					}
				}
			}
			else if (msg is IMethodCallMessage || msg is IMethodReturnMessage)
			{
				if (signature == null)
				{
					RuntimeType runtimeType3 = t as RuntimeType;
					if (runtimeType3 == null)
					{
						result = t.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					}
					else
					{
						result = runtimeType3.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					}
				}
				else
				{
					RuntimeType runtimeType4 = t as RuntimeType;
					if (runtimeType4 == null)
					{
						result = t.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, signature, null);
					}
					else
					{
						result = runtimeType4.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, signature, null);
					}
				}
			}
			return result;
		}

		// Token: 0x06005543 RID: 21827 RVA: 0x0012DC08 File Offset: 0x0012BE08
		[SecurityCritical]
		internal static bool IsMethodAllowedRemotely(MethodBase method)
		{
			if (RemotingServices.s_FieldGetterMB == null || RemotingServices.s_FieldSetterMB == null || RemotingServices.s_IsInstanceOfTypeMB == null || RemotingServices.s_InvokeMemberMB == null || RemotingServices.s_CanCastToXmlTypeMB == null)
			{
				CodeAccessPermission.Assert(true);
				if (RemotingServices.s_FieldGetterMB == null)
				{
					RemotingServices.s_FieldGetterMB = typeof(object).GetMethod("FieldGetter", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_FieldSetterMB == null)
				{
					RemotingServices.s_FieldSetterMB = typeof(object).GetMethod("FieldSetter", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_IsInstanceOfTypeMB == null)
				{
					RemotingServices.s_IsInstanceOfTypeMB = typeof(MarshalByRefObject).GetMethod("IsInstanceOfType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_CanCastToXmlTypeMB == null)
				{
					RemotingServices.s_CanCastToXmlTypeMB = typeof(MarshalByRefObject).GetMethod("CanCastToXmlType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_InvokeMemberMB == null)
				{
					RemotingServices.s_InvokeMemberMB = typeof(MarshalByRefObject).GetMethod("InvokeMember", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
			}
			return method == RemotingServices.s_FieldGetterMB || method == RemotingServices.s_FieldSetterMB || method == RemotingServices.s_IsInstanceOfTypeMB || method == RemotingServices.s_InvokeMemberMB || method == RemotingServices.s_CanCastToXmlTypeMB;
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x0012DD90 File Offset: 0x0012BF90
		[SecurityCritical]
		public static bool IsOneWay(MethodBase method)
		{
			if (method == null)
			{
				return false;
			}
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(method);
			return reflectionCachedData.IsOneWayMethod();
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x0012DDB8 File Offset: 0x0012BFB8
		internal static bool FindAsyncMethodVersion(MethodInfo method, out MethodInfo beginMethod, out MethodInfo endMethod)
		{
			beginMethod = null;
			endMethod = null;
			string value = "Begin" + method.Name;
			string value2 = "End" + method.Name;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			Type typeFromHandle = typeof(IAsyncResult);
			Type returnType = method.ReturnType;
			ParameterInfo[] parameters = method.GetParameters();
			foreach (ParameterInfo parameterInfo in parameters)
			{
				if (parameterInfo.IsOut)
				{
					arrayList2.Add(parameterInfo);
				}
				else if (parameterInfo.ParameterType.IsByRef)
				{
					arrayList.Add(parameterInfo);
					arrayList2.Add(parameterInfo);
				}
				else
				{
					arrayList.Add(parameterInfo);
				}
			}
			arrayList.Add(typeof(AsyncCallback));
			arrayList.Add(typeof(object));
			arrayList2.Add(typeof(IAsyncResult));
			Type declaringType = method.DeclaringType;
			MethodInfo[] methods = declaringType.GetMethods();
			foreach (MethodInfo methodInfo in methods)
			{
				ParameterInfo[] parameters2 = methodInfo.GetParameters();
				if (methodInfo.Name.Equals(value) && methodInfo.ReturnType == typeFromHandle && RemotingServices.CompareParameterList(arrayList, parameters2))
				{
					beginMethod = methodInfo;
				}
				else if (methodInfo.Name.Equals(value2) && methodInfo.ReturnType == returnType && RemotingServices.CompareParameterList(arrayList2, parameters2))
				{
					endMethod = methodInfo;
				}
			}
			return beginMethod != null && endMethod != null;
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x0012DF50 File Offset: 0x0012C150
		private static bool CompareParameterList(ArrayList params1, ParameterInfo[] params2)
		{
			if (params1.Count != params2.Length)
			{
				return false;
			}
			int num = 0;
			foreach (object obj in params1)
			{
				ParameterInfo parameterInfo = params2[num];
				ParameterInfo parameterInfo2 = obj as ParameterInfo;
				if (parameterInfo2 != null)
				{
					if (parameterInfo2.ParameterType != parameterInfo.ParameterType || parameterInfo2.IsIn != parameterInfo.IsIn || parameterInfo2.IsOut != parameterInfo.IsOut)
					{
						return false;
					}
				}
				else if ((Type)obj != parameterInfo.ParameterType && parameterInfo.IsIn)
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x06005547 RID: 21831 RVA: 0x0012E01C File Offset: 0x0012C21C
		[SecurityCritical]
		public static Type GetServerTypeForUri(string URI)
		{
			Type result = null;
			if (URI != null)
			{
				ServerIdentity serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity(URI);
				if (serverIdentity == null)
				{
					result = RemotingConfigHandler.GetServerTypeForUri(URI);
				}
				else
				{
					result = serverIdentity.ServerType;
				}
			}
			return result;
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x0012E04E File Offset: 0x0012C24E
		[SecurityCritical]
		internal static void DomainUnloaded(int domainID)
		{
			IdentityHolder.FlushIdentityTable();
			CrossAppDomainSink.DomainUnloaded(domainID);
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x0012E05C File Offset: 0x0012C25C
		[SecurityCritical]
		internal static IntPtr GetServerContextForProxy(object tp)
		{
			ObjRef objRef = null;
			bool flag;
			int num;
			return RemotingServices.GetServerContextForProxy(tp, out objRef, out flag, out num);
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x0012E078 File Offset: 0x0012C278
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static int GetServerDomainIdForProxy(object tp)
		{
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			return identityObject.ObjectRef.GetServerDomainId();
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x0012E0A0 File Offset: 0x0012C2A0
		[SecurityCritical]
		internal static void GetServerContextAndDomainIdForProxy(object tp, out IntPtr contextId, out int domainId)
		{
			ObjRef objRef;
			bool flag;
			contextId = RemotingServices.GetServerContextForProxy(tp, out objRef, out flag, out domainId);
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x0012E0BC File Offset: 0x0012C2BC
		[SecurityCritical]
		private static IntPtr GetServerContextForProxy(object tp, out ObjRef objRef, out bool bSameDomain, out int domainId)
		{
			IntPtr result = IntPtr.Zero;
			objRef = null;
			bSameDomain = false;
			domainId = 0;
			if (RemotingServices.IsTransparentProxy(tp))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(tp);
				Identity identityObject = realProxy.IdentityObject;
				if (identityObject != null)
				{
					ServerIdentity serverIdentity = identityObject as ServerIdentity;
					if (serverIdentity != null)
					{
						bSameDomain = true;
						result = serverIdentity.ServerContext.InternalContextID;
						domainId = Thread.GetDomain().GetId();
					}
					else
					{
						objRef = identityObject.ObjectRef;
						if (objRef != null)
						{
							result = objRef.GetServerContext(out domainId);
						}
						else
						{
							result = IntPtr.Zero;
						}
					}
				}
				else
				{
					result = Context.DefaultContext.InternalContextID;
				}
			}
			return result;
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x0012E144 File Offset: 0x0012C344
		[SecurityCritical]
		internal static Context GetServerContext(MarshalByRefObject obj)
		{
			Context result = null;
			if (!RemotingServices.IsTransparentProxy(obj) && obj is ContextBoundObject)
			{
				result = Thread.CurrentContext;
			}
			else
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				Identity identityObject = realProxy.IdentityObject;
				ServerIdentity serverIdentity = identityObject as ServerIdentity;
				if (serverIdentity != null)
				{
					result = serverIdentity.ServerContext;
				}
			}
			return result;
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x0012E18C File Offset: 0x0012C38C
		[SecurityCritical]
		private static object GetType(object tp)
		{
			Type result = null;
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			if (identityObject != null && identityObject.ObjectRef != null && identityObject.ObjectRef.TypeInfo != null)
			{
				IRemotingTypeInfo typeInfo = identityObject.ObjectRef.TypeInfo;
				string typeName = typeInfo.TypeName;
				if (typeName != null)
				{
					result = RemotingServices.InternalGetTypeFromQualifiedTypeName(typeName);
				}
			}
			return result;
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x0012E1E4 File Offset: 0x0012C3E4
		[SecurityCritical]
		internal static byte[] MarshalToBuffer(object o, bool crossRuntime)
		{
			if (crossRuntime)
			{
				if (RemotingServices.IsTransparentProxy(o))
				{
					if (RemotingServices.GetRealProxy(o) is RemotingProxy && ChannelServices.RegisteredChannels.Length == 0)
					{
						return null;
					}
				}
				else
				{
					MarshalByRefObject marshalByRefObject = o as MarshalByRefObject;
					if (marshalByRefObject != null)
					{
						ProxyAttribute proxyAttribute = ActivationServices.GetProxyAttribute(marshalByRefObject.GetType());
						if (proxyAttribute == ActivationServices.DefaultProxyAttribute && ChannelServices.RegisteredChannels.Length == 0)
						{
							return null;
						}
					}
				}
			}
			MemoryStream memoryStream = new MemoryStream();
			RemotingSurrogateSelector surrogateSelector = new RemotingSurrogateSelector();
			new BinaryFormatter
			{
				SurrogateSelector = surrogateSelector,
				Context = new StreamingContext(StreamingContextStates.Other)
			}.Serialize(memoryStream, o, null, false);
			return memoryStream.GetBuffer();
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x0012E274 File Offset: 0x0012C474
		[SecurityCritical]
		internal static object UnmarshalFromBuffer(byte[] b, bool crossRuntime)
		{
			MemoryStream serializationStream = new MemoryStream(b);
			object obj = new BinaryFormatter
			{
				AssemblyFormat = FormatterAssemblyStyle.Simple,
				SurrogateSelector = null,
				Context = new StreamingContext(StreamingContextStates.Other)
			}.Deserialize(serializationStream, null, false);
			if (crossRuntime && RemotingServices.IsTransparentProxy(obj))
			{
				if (!(RemotingServices.GetRealProxy(obj) is RemotingProxy))
				{
					return obj;
				}
				if (ChannelServices.RegisteredChannels.Length == 0)
				{
					return null;
				}
				obj.GetHashCode();
			}
			return obj;
		}

		// Token: 0x06005551 RID: 21841 RVA: 0x0012E2E0 File Offset: 0x0012C4E0
		internal static object UnmarshalReturnMessageFromBuffer(byte[] b, IMethodCallMessage msg)
		{
			MemoryStream serializationStream = new MemoryStream(b);
			return new BinaryFormatter
			{
				SurrogateSelector = null,
				Context = new StreamingContext(StreamingContextStates.Other)
			}.DeserializeMethodResponse(serializationStream, null, msg);
		}

		// Token: 0x06005552 RID: 21842 RVA: 0x0012E31C File Offset: 0x0012C51C
		[SecurityCritical]
		public static IMethodReturnMessage ExecuteMessage(MarshalByRefObject target, IMethodCallMessage reqMsg)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(target);
			if (realProxy is RemotingProxy && !realProxy.DoContextsMatch())
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_WrongContext"));
			}
			StackBuilderSink stackBuilderSink = new StackBuilderSink(target);
			return (IMethodReturnMessage)stackBuilderSink.SyncProcessMessage(reqMsg);
		}

		// Token: 0x06005553 RID: 21843 RVA: 0x0012E374 File Offset: 0x0012C574
		[SecurityCritical]
		internal static string DetermineDefaultQualifiedTypeName(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string str = null;
			string str2 = null;
			if (SoapServices.GetXmlTypeForInteropType(type, out str, out str2))
			{
				return "soap:" + str + ", " + str2;
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x0012E3C0 File Offset: 0x0012C5C0
		[SecurityCritical]
		internal static string GetDefaultQualifiedTypeName(RuntimeType type)
		{
			RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(type);
			return reflectionCachedData.QualifiedTypeName;
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x0012E3DC File Offset: 0x0012C5DC
		internal static string InternalGetClrTypeNameFromQualifiedTypeName(string qualifiedTypeName)
		{
			if (qualifiedTypeName.Length > 4 && string.CompareOrdinal(qualifiedTypeName, 0, "clr:", 0, 4) == 0)
			{
				return qualifiedTypeName.Substring(4);
			}
			return null;
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x0012E40D File Offset: 0x0012C60D
		private static int IsSoapType(string qualifiedTypeName)
		{
			if (qualifiedTypeName.Length > 5 && string.CompareOrdinal(qualifiedTypeName, 0, "soap:", 0, 5) == 0)
			{
				return qualifiedTypeName.IndexOf(',', 5);
			}
			return -1;
		}

		// Token: 0x06005557 RID: 21847 RVA: 0x0012E434 File Offset: 0x0012C634
		[SecurityCritical]
		internal static string InternalGetSoapTypeNameFromQualifiedTypeName(string xmlTypeName, string xmlTypeNamespace)
		{
			string text;
			string str;
			if (!SoapServices.DecodeXmlNamespaceForClrTypeNamespace(xmlTypeNamespace, out text, out str))
			{
				return null;
			}
			string str2;
			if (text != null && text.Length > 0)
			{
				str2 = text + "." + xmlTypeName;
			}
			else
			{
				str2 = xmlTypeName;
			}
			try
			{
				return str2 + ", " + str;
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x0012E498 File Offset: 0x0012C698
		[SecurityCritical]
		internal static string InternalGetTypeNameFromQualifiedTypeName(string qualifiedTypeName)
		{
			if (qualifiedTypeName == null)
			{
				throw new ArgumentNullException("qualifiedTypeName");
			}
			string text = RemotingServices.InternalGetClrTypeNameFromQualifiedTypeName(qualifiedTypeName);
			if (text != null)
			{
				return text;
			}
			int num = RemotingServices.IsSoapType(qualifiedTypeName);
			if (num != -1)
			{
				string xmlTypeName = qualifiedTypeName.Substring(5, num - 5);
				string xmlTypeNamespace = qualifiedTypeName.Substring(num + 2, qualifiedTypeName.Length - (num + 2));
				text = RemotingServices.InternalGetSoapTypeNameFromQualifiedTypeName(xmlTypeName, xmlTypeNamespace);
				if (text != null)
				{
					return text;
				}
			}
			return qualifiedTypeName;
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x0012E4F8 File Offset: 0x0012C6F8
		[SecurityCritical]
		internal static RuntimeType InternalGetTypeFromQualifiedTypeName(string qualifiedTypeName, bool partialFallback)
		{
			if (qualifiedTypeName == null)
			{
				throw new ArgumentNullException("qualifiedTypeName");
			}
			string text = RemotingServices.InternalGetClrTypeNameFromQualifiedTypeName(qualifiedTypeName);
			if (text != null)
			{
				return RemotingServices.LoadClrTypeWithPartialBindFallback(text, partialFallback);
			}
			int num = RemotingServices.IsSoapType(qualifiedTypeName);
			if (num != -1)
			{
				string text2 = qualifiedTypeName.Substring(5, num - 5);
				string xmlTypeNamespace = qualifiedTypeName.Substring(num + 2, qualifiedTypeName.Length - (num + 2));
				RuntimeType runtimeType = (RuntimeType)SoapServices.GetInteropTypeFromXmlType(text2, xmlTypeNamespace);
				if (runtimeType != null)
				{
					return runtimeType;
				}
				text = RemotingServices.InternalGetSoapTypeNameFromQualifiedTypeName(text2, xmlTypeNamespace);
				if (text != null)
				{
					return RemotingServices.LoadClrTypeWithPartialBindFallback(text, true);
				}
			}
			return RemotingServices.LoadClrTypeWithPartialBindFallback(qualifiedTypeName, partialFallback);
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x0012E584 File Offset: 0x0012C784
		[SecurityCritical]
		internal static Type InternalGetTypeFromQualifiedTypeName(string qualifiedTypeName)
		{
			return RemotingServices.InternalGetTypeFromQualifiedTypeName(qualifiedTypeName, true);
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x0012E590 File Offset: 0x0012C790
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static RuntimeType LoadClrTypeWithPartialBindFallback(string typeName, bool partialFallback)
		{
			if (!partialFallback)
			{
				return (RuntimeType)Type.GetType(typeName, false);
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeTypeHandle.GetTypeByName(typeName, false, false, false, ref stackCrawlMark, true);
		}

		// Token: 0x0600555C RID: 21852
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CORProfilerTrackRemoting();

		// Token: 0x0600555D RID: 21853
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CORProfilerTrackRemotingCookie();

		// Token: 0x0600555E RID: 21854
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CORProfilerTrackRemotingAsync();

		// Token: 0x0600555F RID: 21855
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingClientSendingMessage(out Guid id, bool fIsAsync);

		// Token: 0x06005560 RID: 21856
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingClientReceivingReply(Guid id, bool fIsAsync);

		// Token: 0x06005561 RID: 21857
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingServerReceivingMessage(Guid id, bool fIsAsync);

		// Token: 0x06005562 RID: 21858
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingServerSendingReply(out Guid id, bool fIsAsync);

		// Token: 0x06005563 RID: 21859 RVA: 0x0012E5BB File Offset: 0x0012C7BB
		[SecurityCritical]
		[Conditional("REMOTING_PERF")]
		[Obsolete("Use of this method is not recommended. The LogRemotingStage existed for internal diagnostic purposes only.")]
		public static void LogRemotingStage(int stage)
		{
		}

		// Token: 0x06005564 RID: 21860
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ResetInterfaceCache(object proxy);

		// Token: 0x040026D6 RID: 9942
		private const BindingFlags LookupAll = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x040026D7 RID: 9943
		private const string FieldGetterName = "FieldGetter";

		// Token: 0x040026D8 RID: 9944
		private const string FieldSetterName = "FieldSetter";

		// Token: 0x040026D9 RID: 9945
		private const string IsInstanceOfTypeName = "IsInstanceOfType";

		// Token: 0x040026DA RID: 9946
		private const string CanCastToXmlTypeName = "CanCastToXmlType";

		// Token: 0x040026DB RID: 9947
		private const string InvokeMemberName = "InvokeMember";

		// Token: 0x040026DC RID: 9948
		private static volatile MethodBase s_FieldGetterMB;

		// Token: 0x040026DD RID: 9949
		private static volatile MethodBase s_FieldSetterMB;

		// Token: 0x040026DE RID: 9950
		private static volatile MethodBase s_IsInstanceOfTypeMB;

		// Token: 0x040026DF RID: 9951
		private static volatile MethodBase s_CanCastToXmlTypeMB;

		// Token: 0x040026E0 RID: 9952
		private static volatile MethodBase s_InvokeMemberMB;

		// Token: 0x040026E1 RID: 9953
		private static volatile bool s_bRemoteActivationConfigured;

		// Token: 0x040026E2 RID: 9954
		private static volatile bool s_bRegisteredWellKnownChannels;

		// Token: 0x040026E3 RID: 9955
		private static bool s_bInProcessOfRegisteringWellKnownChannels;

		// Token: 0x040026E4 RID: 9956
		private static readonly object s_delayLoadChannelLock = new object();
	}
}

using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x020007D0 RID: 2000
	[SecurityCritical]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ProxyAttribute : Attribute, IContextAttribute
	{
		// Token: 0x0600570D RID: 22285 RVA: 0x00131FBC File Offset: 0x001301BC
		[SecurityCritical]
		public virtual MarshalByRefObject CreateInstance(Type serverType)
		{
			if (serverType == null)
			{
				throw new ArgumentNullException("serverType");
			}
			RuntimeType runtimeType = serverType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			if (!serverType.IsContextful)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Activation_MBR_ProxyAttribute"));
			}
			if (serverType.IsAbstract)
			{
				throw new RemotingException(Environment.GetResourceString("Acc_CreateAbst"));
			}
			return this.CreateInstanceInternal(runtimeType);
		}

		// Token: 0x0600570E RID: 22286 RVA: 0x00132034 File Offset: 0x00130234
		internal MarshalByRefObject CreateInstanceInternal(RuntimeType serverType)
		{
			return ActivationServices.CreateInstance(serverType);
		}

		// Token: 0x0600570F RID: 22287 RVA: 0x0013203C File Offset: 0x0013023C
		[SecurityCritical]
		public virtual RealProxy CreateProxy(ObjRef objRef, Type serverType, object serverObject, Context serverContext)
		{
			RemotingProxy remotingProxy = new RemotingProxy(serverType);
			if (serverContext != null)
			{
				RealProxy.SetStubData(remotingProxy, serverContext.InternalContextID);
			}
			if (objRef != null && objRef.GetServerIdentity().IsAllocated)
			{
				remotingProxy.SetSrvInfo(objRef.GetServerIdentity(), objRef.GetDomainID());
			}
			remotingProxy.Initialized = true;
			if (!serverType.IsContextful && !serverType.IsMarshalByRef && serverContext != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Activation_MBR_ProxyAttribute"));
			}
			return remotingProxy;
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x001320B9 File Offset: 0x001302B9
		[SecurityCritical]
		[ComVisible(true)]
		public bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return true;
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x001320BC File Offset: 0x001302BC
		[SecurityCritical]
		[ComVisible(true)]
		public void GetPropertiesForNewContext(IConstructionCallMessage msg)
		{
		}
	}
}

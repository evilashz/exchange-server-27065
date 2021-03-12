using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Remoting.Services
{
	// Token: 0x020007D8 RID: 2008
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class EnterpriseServicesHelper
	{
		// Token: 0x06005750 RID: 22352 RVA: 0x001333A8 File Offset: 0x001315A8
		[SecurityCritical]
		public static object WrapIUnknownWithComObject(IntPtr punk)
		{
			return Marshal.InternalWrapIUnknownWithComObject(punk);
		}

		// Token: 0x06005751 RID: 22353 RVA: 0x001333B0 File Offset: 0x001315B0
		[ComVisible(true)]
		public static IConstructionReturnMessage CreateConstructionReturnMessage(IConstructionCallMessage ctorMsg, MarshalByRefObject retObj)
		{
			return new ConstructorReturnMessage(retObj, null, 0, null, ctorMsg);
		}

		// Token: 0x06005752 RID: 22354 RVA: 0x001333CC File Offset: 0x001315CC
		[SecurityCritical]
		public static void SwitchWrappers(RealProxy oldcp, RealProxy newcp)
		{
			object transparentProxy = oldcp.GetTransparentProxy();
			object transparentProxy2 = newcp.GetTransparentProxy();
			IntPtr serverContextForProxy = RemotingServices.GetServerContextForProxy(transparentProxy);
			IntPtr serverContextForProxy2 = RemotingServices.GetServerContextForProxy(transparentProxy2);
			Marshal.InternalSwitchCCW(transparentProxy, transparentProxy2);
		}
	}
}

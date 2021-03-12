using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007A2 RID: 1954
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	public class ObjectHandle : MarshalByRefObject, IObjectHandle
	{
		// Token: 0x060055A7 RID: 21927 RVA: 0x0012F823 File Offset: 0x0012DA23
		private ObjectHandle()
		{
		}

		// Token: 0x060055A8 RID: 21928 RVA: 0x0012F82B File Offset: 0x0012DA2B
		public ObjectHandle(object o)
		{
			this.WrappedObject = o;
		}

		// Token: 0x060055A9 RID: 21929 RVA: 0x0012F83A File Offset: 0x0012DA3A
		public object Unwrap()
		{
			return this.WrappedObject;
		}

		// Token: 0x060055AA RID: 21930 RVA: 0x0012F844 File Offset: 0x0012DA44
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			MarshalByRefObject marshalByRefObject = this.WrappedObject as MarshalByRefObject;
			if (marshalByRefObject != null && marshalByRefObject.InitializeLifetimeService() == null)
			{
				return null;
			}
			return (ILease)base.InitializeLifetimeService();
		}

		// Token: 0x040026F9 RID: 9977
		private object WrappedObject;
	}
}

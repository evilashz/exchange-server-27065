using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000869 RID: 2153
	[Serializable]
	internal class ConstructionLevelActivator : IActivator
	{
		// Token: 0x06005BFF RID: 23551 RVA: 0x00141E4B File Offset: 0x0014004B
		internal ConstructionLevelActivator()
		{
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06005C00 RID: 23552 RVA: 0x00141E53 File Offset: 0x00140053
		// (set) Token: 0x06005C01 RID: 23553 RVA: 0x00141E56 File Offset: 0x00140056
		public virtual IActivator NextActivator
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06005C02 RID: 23554 RVA: 0x00141E5D File Offset: 0x0014005D
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.Construction;
			}
		}

		// Token: 0x06005C03 RID: 23555 RVA: 0x00141E60 File Offset: 0x00140060
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			ctorMsg.Activator = ctorMsg.Activator.NextActivator;
			return ActivationServices.DoServerContextActivation(ctorMsg);
		}
	}
}

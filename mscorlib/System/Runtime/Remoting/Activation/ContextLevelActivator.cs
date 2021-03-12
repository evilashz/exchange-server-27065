using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000868 RID: 2152
	[Serializable]
	internal class ContextLevelActivator : IActivator
	{
		// Token: 0x06005BF9 RID: 23545 RVA: 0x00141DD9 File Offset: 0x0013FFD9
		internal ContextLevelActivator()
		{
			this.m_NextActivator = null;
		}

		// Token: 0x06005BFA RID: 23546 RVA: 0x00141DE8 File Offset: 0x0013FFE8
		internal ContextLevelActivator(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_NextActivator = (IActivator)info.GetValue("m_NextActivator", typeof(IActivator));
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06005BFB RID: 23547 RVA: 0x00141E1E File Offset: 0x0014001E
		// (set) Token: 0x06005BFC RID: 23548 RVA: 0x00141E26 File Offset: 0x00140026
		public virtual IActivator NextActivator
		{
			[SecurityCritical]
			get
			{
				return this.m_NextActivator;
			}
			[SecurityCritical]
			set
			{
				this.m_NextActivator = value;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06005BFD RID: 23549 RVA: 0x00141E2F File Offset: 0x0014002F
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.Context;
			}
		}

		// Token: 0x06005BFE RID: 23550 RVA: 0x00141E32 File Offset: 0x00140032
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			ctorMsg.Activator = ctorMsg.Activator.NextActivator;
			return ActivationServices.DoCrossContextActivation(ctorMsg);
		}

		// Token: 0x04002935 RID: 10549
		private IActivator m_NextActivator;
	}
}

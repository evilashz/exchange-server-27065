using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000867 RID: 2151
	[Serializable]
	internal class AppDomainLevelActivator : IActivator
	{
		// Token: 0x06005BF3 RID: 23539 RVA: 0x00141D66 File Offset: 0x0013FF66
		internal AppDomainLevelActivator(string remActivatorURL)
		{
			this.m_RemActivatorURL = remActivatorURL;
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x00141D75 File Offset: 0x0013FF75
		internal AppDomainLevelActivator(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_NextActivator = (IActivator)info.GetValue("m_NextActivator", typeof(IActivator));
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06005BF5 RID: 23541 RVA: 0x00141DAB File Offset: 0x0013FFAB
		// (set) Token: 0x06005BF6 RID: 23542 RVA: 0x00141DB3 File Offset: 0x0013FFB3
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

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06005BF7 RID: 23543 RVA: 0x00141DBC File Offset: 0x0013FFBC
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x06005BF8 RID: 23544 RVA: 0x00141DC0 File Offset: 0x0013FFC0
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			ctorMsg.Activator = this.m_NextActivator;
			return ActivationServices.GetActivator().Activate(ctorMsg);
		}

		// Token: 0x04002933 RID: 10547
		private IActivator m_NextActivator;

		// Token: 0x04002934 RID: 10548
		private string m_RemActivatorURL;
	}
}

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200086A RID: 2154
	internal class RemotePropertyHolderAttribute : IContextAttribute
	{
		// Token: 0x06005C04 RID: 23556 RVA: 0x00141E79 File Offset: 0x00140079
		internal RemotePropertyHolderAttribute(IList cp)
		{
			this._cp = cp;
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x00141E88 File Offset: 0x00140088
		[SecurityCritical]
		[ComVisible(true)]
		public virtual bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return false;
		}

		// Token: 0x06005C06 RID: 23558 RVA: 0x00141E8C File Offset: 0x0014008C
		[SecurityCritical]
		[ComVisible(true)]
		public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			for (int i = 0; i < this._cp.Count; i++)
			{
				ctorMsg.ContextProperties.Add(this._cp[i]);
			}
		}

		// Token: 0x04002936 RID: 10550
		private IList _cp;
	}
}

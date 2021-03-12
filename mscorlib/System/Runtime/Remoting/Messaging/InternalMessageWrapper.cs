using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000845 RID: 2117
	[SecurityCritical]
	[ComVisible(true)]
	public class InternalMessageWrapper
	{
		// Token: 0x06005ADE RID: 23262 RVA: 0x0013E080 File Offset: 0x0013C280
		public InternalMessageWrapper(IMessage msg)
		{
			this.WrappedMessage = msg;
		}

		// Token: 0x06005ADF RID: 23263 RVA: 0x0013E090 File Offset: 0x0013C290
		[SecurityCritical]
		internal object GetIdentityObject()
		{
			IInternalMessage internalMessage = this.WrappedMessage as IInternalMessage;
			if (internalMessage != null)
			{
				return internalMessage.IdentityObject;
			}
			InternalMessageWrapper internalMessageWrapper = this.WrappedMessage as InternalMessageWrapper;
			if (internalMessageWrapper != null)
			{
				return internalMessageWrapper.GetIdentityObject();
			}
			return null;
		}

		// Token: 0x06005AE0 RID: 23264 RVA: 0x0013E0CC File Offset: 0x0013C2CC
		[SecurityCritical]
		internal object GetServerIdentityObject()
		{
			IInternalMessage internalMessage = this.WrappedMessage as IInternalMessage;
			if (internalMessage != null)
			{
				return internalMessage.ServerIdentityObject;
			}
			InternalMessageWrapper internalMessageWrapper = this.WrappedMessage as InternalMessageWrapper;
			if (internalMessageWrapper != null)
			{
				return internalMessageWrapper.GetServerIdentityObject();
			}
			return null;
		}

		// Token: 0x040028D8 RID: 10456
		protected IMessage WrappedMessage;
	}
}

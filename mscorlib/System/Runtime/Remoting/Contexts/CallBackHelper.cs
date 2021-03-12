using System;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007DD RID: 2013
	[Serializable]
	internal class CallBackHelper
	{
		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06005798 RID: 22424 RVA: 0x001342A5 File Offset: 0x001324A5
		// (set) Token: 0x06005799 RID: 22425 RVA: 0x001342B2 File Offset: 0x001324B2
		internal bool IsEERequested
		{
			get
			{
				return (this._flags & 1) == 1;
			}
			set
			{
				if (value)
				{
					this._flags |= 1;
				}
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (set) Token: 0x0600579A RID: 22426 RVA: 0x001342C5 File Offset: 0x001324C5
		internal bool IsCrossDomain
		{
			set
			{
				if (value)
				{
					this._flags |= 256;
				}
			}
		}

		// Token: 0x0600579B RID: 22427 RVA: 0x001342DC File Offset: 0x001324DC
		internal CallBackHelper(IntPtr privateData, bool bFromEE, int targetDomainID)
		{
			this.IsEERequested = bFromEE;
			this.IsCrossDomain = (targetDomainID != 0);
			this._privateData = privateData;
		}

		// Token: 0x0600579C RID: 22428 RVA: 0x001342FC File Offset: 0x001324FC
		[SecurityCritical]
		internal void Func()
		{
			if (this.IsEERequested)
			{
				Context.ExecuteCallBackInEE(this._privateData);
			}
		}

		// Token: 0x040027B3 RID: 10163
		internal const int RequestedFromEE = 1;

		// Token: 0x040027B4 RID: 10164
		internal const int XDomainTransition = 256;

		// Token: 0x040027B5 RID: 10165
		private int _flags;

		// Token: 0x040027B6 RID: 10166
		private IntPtr _privateData;
	}
}

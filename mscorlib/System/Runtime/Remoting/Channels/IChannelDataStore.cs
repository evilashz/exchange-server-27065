using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200081D RID: 2077
	[ComVisible(true)]
	public interface IChannelDataStore
	{
		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06005906 RID: 22790
		string[] ChannelUris { [SecurityCritical] get; }

		// Token: 0x17000EE5 RID: 3813
		object this[object key]
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}
	}
}

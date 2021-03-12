using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x0200057E RID: 1406
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	[SecurityCritical]
	internal sealed class CleanupWorkList
	{
		// Token: 0x0600420F RID: 16911 RVA: 0x000F5934 File Offset: 0x000F3B34
		public void Add(CleanupWorkListElement elem)
		{
			this.m_list.Add(elem);
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x000F5944 File Offset: 0x000F3B44
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Destroy()
		{
			for (int i = this.m_list.Count - 1; i >= 0; i--)
			{
				if (this.m_list[i].m_owned)
				{
					StubHelpers.SafeHandleRelease(this.m_list[i].m_handle);
				}
			}
		}

		// Token: 0x04001B32 RID: 6962
		private List<CleanupWorkListElement> m_list = new List<CleanupWorkListElement>();
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002AA RID: 682
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SeederInstances : SafeInstanceTable<SeederInstanceContainer>
	{
		// Token: 0x06001AAD RID: 6829 RVA: 0x00072390 File Offset: 0x00070590
		internal override void RemoveInstance(SeederInstanceContainer instance)
		{
			bool flag = false;
			this.m_rwLock.AcquireWriterLock(-1);
			try
			{
				if (this.m_instances.ContainsKey(instance.Identity))
				{
					flag = true;
					this.m_instances.Remove(instance.Identity);
				}
			}
			finally
			{
				this.m_rwLock.ReleaseWriterLock();
			}
			if (flag)
			{
				instance.WaitUntilStopped();
			}
		}
	}
}

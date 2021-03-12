using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200056B RID: 1387
	internal class StandaloneMultiChecks : MultiReplicationCheck
	{
		// Token: 0x06003105 RID: 12549 RVA: 0x000C7115 File Offset: 0x000C5315
		public StandaloneMultiChecks(string serverName, IEventManager eventManager, string momEventSource, uint ignoreTransientErrorsThreshold) : base(serverName, eventManager, momEventSource, null, null, null, ignoreTransientErrorsThreshold)
		{
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000C7128 File Offset: 0x000C5328
		protected override void Initialize()
		{
			this.m_Checks = new IReplicationCheck[]
			{
				new ReplayServiceCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, new uint?(this.m_IgnoreTransientErrorsThreshold)),
				new ActiveManagerCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, new uint?(this.m_IgnoreTransientErrorsThreshold)),
				new ServerLocatorServiceCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, new uint?(this.m_IgnoreTransientErrorsThreshold))
			};
		}
	}
}

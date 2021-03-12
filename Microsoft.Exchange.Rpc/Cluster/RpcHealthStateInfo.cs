using System;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x0200012C RID: 300
	[Serializable]
	internal sealed class RpcHealthStateInfo
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x00005AA8 File Offset: 0x00004EA8
		public RpcHealthStateInfo(string componentName, int priority, string resultName, string dbName, int healthStatus, DateTime lastReportedTime)
		{
			this.m_componentName = componentName;
			this.m_priority = priority;
			this.m_resultName = resultName;
			this.m_databaseName = dbName;
			this.m_healthStatus = healthStatus;
			this.m_lastReportedTime = lastReportedTime;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00005A94 File Offset: 0x00004E94
		public RpcHealthStateInfo()
		{
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00005AE8 File Offset: 0x00004EE8
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x00005AFC File Offset: 0x00004EFC
		public string ComponentName
		{
			get
			{
				return this.m_componentName;
			}
			set
			{
				this.m_componentName = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00005B10 File Offset: 0x00004F10
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x00005B24 File Offset: 0x00004F24
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
			set
			{
				this.m_databaseName = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00005B38 File Offset: 0x00004F38
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00005B4C File Offset: 0x00004F4C
		public string ResultName
		{
			get
			{
				return this.m_resultName;
			}
			set
			{
				this.m_resultName = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00005B60 File Offset: 0x00004F60
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00005B74 File Offset: 0x00004F74
		public int Priority
		{
			get
			{
				return this.m_priority;
			}
			set
			{
				this.m_priority = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00005B88 File Offset: 0x00004F88
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x00005B9C File Offset: 0x00004F9C
		public int HealthStatus
		{
			get
			{
				return this.m_healthStatus;
			}
			set
			{
				this.m_healthStatus = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00005BB0 File Offset: 0x00004FB0
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x00005BC8 File Offset: 0x00004FC8
		public DateTime LastReportedTime
		{
			get
			{
				return this.m_lastReportedTime;
			}
			set
			{
				this.m_lastReportedTime = value;
			}
		}

		// Token: 0x040009AD RID: 2477
		private string m_componentName;

		// Token: 0x040009AE RID: 2478
		private string m_resultName;

		// Token: 0x040009AF RID: 2479
		private int m_healthStatus;

		// Token: 0x040009B0 RID: 2480
		private int m_priority;

		// Token: 0x040009B1 RID: 2481
		private DateTime m_lastReportedTime;

		// Token: 0x040009B2 RID: 2482
		private string m_databaseName;
	}
}

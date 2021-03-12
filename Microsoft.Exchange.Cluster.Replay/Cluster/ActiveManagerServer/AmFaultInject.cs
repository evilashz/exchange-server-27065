using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000C5 RID: 197
	internal static class AmFaultInject
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x00026FEC File Offset: 0x000251EC
		internal static void Init()
		{
			AmFaultInject.m_faultInjectHelper.Init();
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00026FF8 File Offset: 0x000251F8
		internal static void SleepIfRequired(Guid dbGuid, string propertyName)
		{
			AmFaultInject.m_faultInjectHelper.SleepIfRequired(dbGuid, propertyName);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00027006 File Offset: 0x00025206
		internal static void SleepIfRequired(string propertyName)
		{
			AmFaultInject.m_faultInjectHelper.SleepIfRequired(propertyName);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00027013 File Offset: 0x00025213
		internal static void SleepIfRequired(Guid dbGuid, AmSleepTag sleepTag)
		{
			AmFaultInject.m_faultInjectHelper.SleepIfRequired(dbGuid, sleepTag.ToString());
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0002702B File Offset: 0x0002522B
		internal static void SleepIfRequired(AmSleepTag sleepTag)
		{
			AmFaultInject.m_faultInjectHelper.SleepIfRequired(sleepTag.ToString());
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00027042 File Offset: 0x00025242
		internal static void SleepIfRequired(string subKeyName, string propertyName)
		{
			AmFaultInject.m_faultInjectHelper.SleepIfRequired(subKeyName, propertyName);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00027050 File Offset: 0x00025250
		internal static void GenerateMapiExceptionIfRequired(Guid dbGuid, AmServerName serverName)
		{
			AmFaultInject.m_faultInjectHelper.GenerateMapiExceptionIfRequired(dbGuid, serverName);
		}

		// Token: 0x04000389 RID: 905
		private static AmFaultInjectHelper m_faultInjectHelper = new AmFaultInjectHelper();
	}
}

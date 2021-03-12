using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000019 RID: 25
	internal sealed class CiHealthMonitorConfigurationSetting : ResourceHealthMonitorConfigurationSetting
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004B51 File Offset: 0x00002D51
		internal override string DisabledRegistryValueName
		{
			get
			{
				return "DisableCiHealthCollection";
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004B58 File Offset: 0x00002D58
		internal override string RefreshIntervalRegistryValueName
		{
			get
			{
				return "CiHealthRefreshInterval";
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004B5F File Offset: 0x00002D5F
		internal override string OverrideMetricValueRegistryValueName
		{
			get
			{
				return "CiMetricValue";
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004B66 File Offset: 0x00002D66
		internal string NumberOfHealthyCopiesRequiredRegistryValueName
		{
			get
			{
				return "CiNumberOfHealthyCopiesRequired";
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004B6D File Offset: 0x00002D6D
		internal string FailedCatalogStatusThresholdRegistryValueName
		{
			get
			{
				return "CiFailedCatalogStatusThreshold";
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004B74 File Offset: 0x00002D74
		internal string RpcTimeoutRegistryValueName
		{
			get
			{
				return "CiRpcTimeoutInterval";
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004B7B File Offset: 0x00002D7B
		internal string MdbCopyUpdateIntervalRegistryValueName
		{
			get
			{
				return "CiMdbCopyUpdateInterval";
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004B82 File Offset: 0x00002D82
		internal string MdbCopyUpdateDelayRegistryValueName
		{
			get
			{
				return "CiMdbCopyUpdateDelay";
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004B89 File Offset: 0x00002D89
		internal override TimeSpan DefaultRefreshInterval
		{
			get
			{
				return TimeSpan.FromSeconds(10.0);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004B99 File Offset: 0x00002D99
		internal TimeSpan DefaultRpcTimeout
		{
			get
			{
				return TimeSpan.FromSeconds(10.0);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004BA9 File Offset: 0x00002DA9
		internal TimeSpan DefaultMdbCopyUpdateInterval
		{
			get
			{
				return TimeSpan.FromMinutes(15.0);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004BB9 File Offset: 0x00002DB9
		internal TimeSpan DefaultMdbCopyUpdateDelay
		{
			get
			{
				return TimeSpan.FromSeconds(5.0);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004BC9 File Offset: 0x00002DC9
		internal int DefaultNumberOfHealthyCopiesRequired
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004BCC File Offset: 0x00002DCC
		internal int DefaultFailedCatalogStatusThreshold
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x04000067 RID: 103
		private const string DisabledValueName = "DisableCiHealthCollection";

		// Token: 0x04000068 RID: 104
		private const string RefreshIntervalValueName = "CiHealthRefreshInterval";

		// Token: 0x04000069 RID: 105
		private const string OverrideMetricValueName = "CiMetricValue";

		// Token: 0x0400006A RID: 106
		private const string NumberOfHealthyCopiesRequiredValueName = "CiNumberOfHealthyCopiesRequired";

		// Token: 0x0400006B RID: 107
		private const string FailedCatalogStatusThresholdValueName = "CiFailedCatalogStatusThreshold";

		// Token: 0x0400006C RID: 108
		private const string RpcTimeoutValueName = "CiRpcTimeoutInterval";

		// Token: 0x0400006D RID: 109
		private const string MdbCopyUpdateIntervalValueName = "CiMdbCopyUpdateInterval";

		// Token: 0x0400006E RID: 110
		private const string MdbCopyUpdateDelayValueName = "CiMdbCopyUpdateDelay";
	}
}

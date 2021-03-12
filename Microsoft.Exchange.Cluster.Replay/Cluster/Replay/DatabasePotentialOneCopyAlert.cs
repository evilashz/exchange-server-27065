using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001F6 RID: 502
	internal class DatabasePotentialOneCopyAlert : MonitoringAlert
	{
		// Token: 0x060013F0 RID: 5104 RVA: 0x00050B8B File Offset: 0x0004ED8B
		public DatabasePotentialOneCopyAlert(string databaseName, Guid dbGuid, string targetServer) : base(databaseName, dbGuid)
		{
			this.TargetServer = targetServer;
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00050B9C File Offset: 0x0004ED9C
		// (set) Token: 0x060013F2 RID: 5106 RVA: 0x00050BA4 File Offset: 0x0004EDA4
		public string TargetServer { get; private set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x00050BAD File Offset: 0x0004EDAD
		protected override TimeSpan DatabaseHealthCheckRedTransitionSuppression
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckPotentialOneCopyRedTransitionSuppressionInSec);
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00050BBA File Offset: 0x0004EDBA
		protected override TimeSpan DatabaseHealthCheckGreenTransitionSuppression
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckPotentialOneCopyGreenTransitionSuppressionInSec);
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x00050BC7 File Offset: 0x0004EDC7
		protected override TimeSpan DatabaseHealthCheckRedPeriodicInterval
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckPotentialOneCopyRedPeriodicIntervalInSec);
			}
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00050BD4 File Offset: 0x0004EDD4
		protected override void RaiseGreenEvent(IHealthValidationResultMinimal result)
		{
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00050BD6 File Offset: 0x0004EDD6
		protected override void RaiseRedEvent(IHealthValidationResultMinimal result)
		{
		}
	}
}

using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001C6 RID: 454
	internal class ServerHealthInfo
	{
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0004A9B3 File Offset: 0x00048BB3
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x0004A9BB File Offset: 0x00048BBB
		public AmServerName ServerName { get; private set; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0004A9C4 File Offset: 0x00048BC4
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x0004A9CC File Offset: 0x00048BCC
		public StateTransitionInfo ServerFoundInAD { get; private set; }

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0004A9D5 File Offset: 0x00048BD5
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x0004A9DD File Offset: 0x00048BDD
		public StateTransitionInfo CriticalForMaintainingAvailability { get; private set; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0004A9E6 File Offset: 0x00048BE6
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x0004A9EE File Offset: 0x00048BEE
		public StateTransitionInfo CriticalForMaintainingRedundancy { get; private set; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0004A9F7 File Offset: 0x00048BF7
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x0004A9FF File Offset: 0x00048BFF
		public StateTransitionInfo CriticalForRestoringAvailability { get; private set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0004AA08 File Offset: 0x00048C08
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x0004AA10 File Offset: 0x00048C10
		public StateTransitionInfo CriticalForRestoringRedundancy { get; private set; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0004AA19 File Offset: 0x00048C19
		// (set) Token: 0x060011E9 RID: 4585 RVA: 0x0004AA21 File Offset: 0x00048C21
		public StateTransitionInfo HighForRestoringAvailability { get; private set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0004AA2A File Offset: 0x00048C2A
		// (set) Token: 0x060011EB RID: 4587 RVA: 0x0004AA32 File Offset: 0x00048C32
		public StateTransitionInfo HighForRestoringRedundancy { get; private set; }

		// Token: 0x060011EC RID: 4588 RVA: 0x0004AA3C File Offset: 0x00048C3C
		public ServerHealthInfo(AmServerName serverName)
		{
			this.ServerName = serverName;
			this.ServerFoundInAD = new StateTransitionInfo();
			this.CriticalForMaintainingAvailability = new StateTransitionInfo();
			this.CriticalForMaintainingRedundancy = new StateTransitionInfo();
			this.CriticalForRestoringAvailability = new StateTransitionInfo();
			this.CriticalForRestoringRedundancy = new StateTransitionInfo();
			this.HighForRestoringAvailability = new StateTransitionInfo();
			this.HighForRestoringRedundancy = new StateTransitionInfo();
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0004AAA4 File Offset: 0x00048CA4
		public ServerHealthInfoPersisted ConvertToSerializable()
		{
			return new ServerHealthInfoPersisted(this.ServerName.Fqdn)
			{
				ServerFoundInAD = this.ServerFoundInAD.ConvertToSerializable(),
				CriticalForMaintainingAvailability = this.CriticalForMaintainingAvailability.ConvertToSerializable(),
				CriticalForMaintainingRedundancy = this.CriticalForMaintainingRedundancy.ConvertToSerializable(),
				CriticalForRestoringAvailability = this.CriticalForRestoringAvailability.ConvertToSerializable(),
				CriticalForRestoringRedundancy = this.CriticalForRestoringRedundancy.ConvertToSerializable(),
				HighForRestoringAvailability = this.HighForRestoringAvailability.ConvertToSerializable(),
				HighForRestoringRedundancy = this.HighForRestoringRedundancy.ConvertToSerializable()
			};
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0004AB3C File Offset: 0x00048D3C
		public void InitializeFromSerializable(ServerHealthInfoPersisted ship)
		{
			this.ServerFoundInAD = StateTransitionInfo.ConstructFromPersisted(ship.ServerFoundInAD);
			this.CriticalForMaintainingAvailability = StateTransitionInfo.ConstructFromPersisted(ship.CriticalForMaintainingAvailability);
			this.CriticalForMaintainingRedundancy = StateTransitionInfo.ConstructFromPersisted(ship.CriticalForMaintainingRedundancy);
			this.CriticalForRestoringAvailability = StateTransitionInfo.ConstructFromPersisted(ship.CriticalForRestoringAvailability);
			this.CriticalForRestoringRedundancy = StateTransitionInfo.ConstructFromPersisted(ship.CriticalForRestoringRedundancy);
			this.HighForRestoringAvailability = StateTransitionInfo.ConstructFromPersisted(ship.HighForRestoringAvailability);
			this.HighForRestoringRedundancy = StateTransitionInfo.ConstructFromPersisted(ship.HighForRestoringRedundancy);
		}
	}
}

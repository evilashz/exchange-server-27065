using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x02000069 RID: 105
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class DeltaSyncObject
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x000172A8 File Offset: 0x000154A8
		internal DeltaSyncObject(Guid serverId)
		{
			this.serverId = serverId;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000172B7 File Offset: 0x000154B7
		internal DeltaSyncObject(string clientId)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("clientId", clientId);
			this.clientId = clientId;
			this.isClientObject = true;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x000172D8 File Offset: 0x000154D8
		internal bool IsClientObject
		{
			get
			{
				return this.isClientObject;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000172E0 File Offset: 0x000154E0
		internal string Id
		{
			get
			{
				if (!this.isClientObject)
				{
					return this.serverId.ToString();
				}
				return this.clientId;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00017302 File Offset: 0x00015502
		internal string ClientId
		{
			get
			{
				return this.clientId;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0001730A File Offset: 0x0001550A
		internal Guid ServerId
		{
			get
			{
				return this.serverId;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00017312 File Offset: 0x00015512
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0001731A File Offset: 0x0001551A
		internal DeltaSyncFolder Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x04000298 RID: 664
		private Guid serverId;

		// Token: 0x04000299 RID: 665
		private string clientId;

		// Token: 0x0400029A RID: 666
		private bool isClientObject;

		// Token: 0x0400029B RID: 667
		private DeltaSyncFolder parent;
	}
}

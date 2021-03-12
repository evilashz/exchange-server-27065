using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000032 RID: 50
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ProvisioningId : ObjectId, ISnapshotId
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x000097BB File Offset: 0x000079BB
		public ProvisioningId(Guid jobItemGuid, Guid jobGuid)
		{
			this.JobItemGuid = jobItemGuid;
			this.JobGuid = jobGuid;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000097D1 File Offset: 0x000079D1
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x000097D9 File Offset: 0x000079D9
		public Guid JobItemGuid { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000097E2 File Offset: 0x000079E2
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x000097EA File Offset: 0x000079EA
		public Guid JobGuid { get; private set; }

		// Token: 0x060001F6 RID: 502 RVA: 0x000097F4 File Offset: 0x000079F4
		public override byte[] GetBytes()
		{
			return this.JobItemGuid.ToByteArray().Concat(this.JobGuid.ToByteArray()).ToArray<byte>();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009828 File Offset: 0x00007A28
		public override bool Equals(object obj)
		{
			ProvisioningId provisioningId = obj as ProvisioningId;
			return provisioningId != null && this.JobItemGuid == provisioningId.JobItemGuid && this.JobGuid == provisioningId.JobGuid;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009868 File Offset: 0x00007A68
		public override int GetHashCode()
		{
			return this.JobItemGuid.GetHashCode() / 2 + this.JobGuid.GetHashCode() / 2;
		}
	}
}

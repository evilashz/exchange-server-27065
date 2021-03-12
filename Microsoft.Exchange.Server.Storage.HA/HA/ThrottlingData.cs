using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x0200001E RID: 30
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class ThrottlingData
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000066E9 File Offset: 0x000048E9
		[CLSCompliant(false)]
		public ulong DataProtectionHealth
		{
			get
			{
				return this.dataProtectionHealth;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000066F1 File Offset: 0x000048F1
		[CLSCompliant(false)]
		public ulong DataAvailabilityHealth
		{
			get
			{
				return this.dataAvailabilityHealth;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000066F9 File Offset: 0x000048F9
		[CLSCompliant(false)]
		public void Update(ulong dataProtectionHealth, ulong dataAvailabilityHealth)
		{
			this.dataProtectionHealth = ((dataProtectionHealth < (ulong)-1) ? dataProtectionHealth : ((ulong)-1));
			this.dataAvailabilityHealth = ((dataAvailabilityHealth < (ulong)-1) ? dataAvailabilityHealth : ((ulong)-1));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000671B File Offset: 0x0000491B
		internal void MarkFailed()
		{
			this.Update(52428800UL, 1048576000UL);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000672F File Offset: 0x0000492F
		internal void MarkHealthy()
		{
			this.Update(0UL, 0UL);
		}

		// Token: 0x0400008B RID: 139
		internal const long MiB = 1048576L;

		// Token: 0x0400008C RID: 140
		internal const uint MaxCopyQueue = 50U;

		// Token: 0x0400008D RID: 141
		internal const uint MaxReplayQueue = 1000U;

		// Token: 0x0400008E RID: 142
		private ulong dataProtectionHealth;

		// Token: 0x0400008F RID: 143
		private ulong dataAvailabilityHealth;
	}
}

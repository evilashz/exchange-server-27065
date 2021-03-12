using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E91 RID: 3729
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class SyncNotificationResult
	{
		// Token: 0x060081BD RID: 33213 RVA: 0x00237507 File Offset: 0x00235707
		public SyncNotificationResult(object resultObject = null)
		{
			this.ResultObject = resultObject;
		}

		// Token: 0x060081BE RID: 33214 RVA: 0x00237516 File Offset: 0x00235716
		public SyncNotificationResult(SyncAgentExceptionBase error)
		{
			this.Error = error;
		}

		// Token: 0x1700226A RID: 8810
		// (get) Token: 0x060081BF RID: 33215 RVA: 0x00237525 File Offset: 0x00235725
		// (set) Token: 0x060081C0 RID: 33216 RVA: 0x0023752D File Offset: 0x0023572D
		public SyncAgentExceptionBase Error { get; set; }

		// Token: 0x1700226B RID: 8811
		// (get) Token: 0x060081C1 RID: 33217 RVA: 0x00237536 File Offset: 0x00235736
		// (set) Token: 0x060081C2 RID: 33218 RVA: 0x0023753E File Offset: 0x0023573E
		public object ResultObject { get; set; }

		// Token: 0x1700226C RID: 8812
		// (get) Token: 0x060081C3 RID: 33219 RVA: 0x00237547 File Offset: 0x00235747
		// (set) Token: 0x060081C4 RID: 33220 RVA: 0x0023754F File Offset: 0x0023574F
		public string AdditionalInformation { get; set; }

		// Token: 0x1700226D RID: 8813
		// (get) Token: 0x060081C5 RID: 33221 RVA: 0x00237558 File Offset: 0x00235758
		public bool Success
		{
			get
			{
				return this.Error == null;
			}
		}
	}
}

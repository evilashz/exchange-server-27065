using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x0200094E RID: 2382
	internal class CmdletImplementation
	{
		// Token: 0x1700196F RID: 6511
		// (get) Token: 0x060054F8 RID: 21752 RVA: 0x0015E9C9 File Offset: 0x0015CBC9
		// (set) Token: 0x060054F9 RID: 21753 RVA: 0x0015E9D1 File Offset: 0x0015CBD1
		public CmdletImplementation.ShouldContinueMethod ShouldContinue { get; set; }

		// Token: 0x17001970 RID: 6512
		// (get) Token: 0x060054FA RID: 21754 RVA: 0x0015E9DA File Offset: 0x0015CBDA
		// (set) Token: 0x060054FB RID: 21755 RVA: 0x0015E9E2 File Offset: 0x0015CBE2
		public IConfigDataProvider DataSession { get; set; }

		// Token: 0x060054FC RID: 21756 RVA: 0x0015E9EB File Offset: 0x0015CBEB
		public virtual void ProcessRecord()
		{
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x0015E9ED File Offset: 0x0015CBED
		public virtual void Validate()
		{
		}

		// Token: 0x0200094F RID: 2383
		// (Invoke) Token: 0x06005500 RID: 21760
		public delegate bool ShouldContinueMethod(LocalizedString prompt);
	}
}

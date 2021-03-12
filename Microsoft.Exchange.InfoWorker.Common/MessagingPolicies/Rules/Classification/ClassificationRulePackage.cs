using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Classification
{
	// Token: 0x02000162 RID: 354
	internal sealed class ClassificationRulePackage : IVersionedItem
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x000290C4 File Offset: 0x000272C4
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x000290CC File Offset: 0x000272CC
		public string RuleXml { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x000290D5 File Offset: 0x000272D5
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x000290DD File Offset: 0x000272DD
		public string ID { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000290E6 File Offset: 0x000272E6
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x000290EE File Offset: 0x000272EE
		public DateTime Version { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x000290F7 File Offset: 0x000272F7
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x000290FF File Offset: 0x000272FF
		public HashSet<string> VersionedDataClassificationIds { get; set; }
	}
}

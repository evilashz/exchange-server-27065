using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002F1 RID: 753
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FindPeopleParameters
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x0006CAA9 File Offset: 0x0006ACA9
		// (set) Token: 0x06001535 RID: 5429 RVA: 0x0006CAB1 File Offset: 0x0006ACB1
		public string QueryString { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x0006CABA File Offset: 0x0006ACBA
		// (set) Token: 0x06001537 RID: 5431 RVA: 0x0006CAC2 File Offset: 0x0006ACC2
		public SortResults[] SortResults { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0006CACB File Offset: 0x0006ACCB
		// (set) Token: 0x06001539 RID: 5433 RVA: 0x0006CAD3 File Offset: 0x0006ACD3
		public BasePagingType Paging { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0006CADC File Offset: 0x0006ACDC
		// (set) Token: 0x0600153B RID: 5435 RVA: 0x0006CAE4 File Offset: 0x0006ACE4
		public RestrictionType Restriction { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x0006CAED File Offset: 0x0006ACED
		// (set) Token: 0x0600153D RID: 5437 RVA: 0x0006CAF5 File Offset: 0x0006ACF5
		public RestrictionType AggregationRestriction { get; set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0006CAFE File Offset: 0x0006ACFE
		// (set) Token: 0x0600153F RID: 5439 RVA: 0x0006CB06 File Offset: 0x0006AD06
		public PersonaResponseShape PersonaShape { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x0006CB0F File Offset: 0x0006AD0F
		// (set) Token: 0x06001541 RID: 5441 RVA: 0x0006CB17 File Offset: 0x0006AD17
		public CultureInfo CultureInfo { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x0006CB20 File Offset: 0x0006AD20
		// (set) Token: 0x06001543 RID: 5443 RVA: 0x0006CB28 File Offset: 0x0006AD28
		public RequestDetailsLogger Logger { get; set; }
	}
}

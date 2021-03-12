using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200048A RID: 1162
	internal sealed class RcptParseOutput
	{
		// Token: 0x06003512 RID: 13586 RVA: 0x000D83D7 File Offset: 0x000D65D7
		public RcptParseOutput()
		{
			this.Notify = DsnRequestedFlags.Default;
			this.LowAuthenticationLevelTarpitOverride = TarpitAction.None;
			this.ConsumerMailOptionalArguments = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000D83FD File Offset: 0x000D65FD
		// (set) Token: 0x06003514 RID: 13588 RVA: 0x000D8405 File Offset: 0x000D6605
		public RoutingAddress RecipientAddress { get; set; }

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x000D840E File Offset: 0x000D660E
		// (set) Token: 0x06003516 RID: 13590 RVA: 0x000D8416 File Offset: 0x000D6616
		public RoutingAddress Orar { get; set; }

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x000D841F File Offset: 0x000D661F
		// (set) Token: 0x06003518 RID: 13592 RVA: 0x000D8427 File Offset: 0x000D6627
		public DsnRequestedFlags Notify { get; set; }

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000D8430 File Offset: 0x000D6630
		// (set) Token: 0x0600351A RID: 13594 RVA: 0x000D8438 File Offset: 0x000D6638
		public string ORcpt { get; set; }

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x000D8441 File Offset: 0x000D6641
		// (set) Token: 0x0600351C RID: 13596 RVA: 0x000D8449 File Offset: 0x000D6649
		public string RDst { get; set; }

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x000D8452 File Offset: 0x000D6652
		// (set) Token: 0x0600351E RID: 13598 RVA: 0x000D845A File Offset: 0x000D665A
		public TarpitAction LowAuthenticationLevelTarpitOverride { get; set; }

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000D8463 File Offset: 0x000D6663
		// (set) Token: 0x06003520 RID: 13600 RVA: 0x000D846B File Offset: 0x000D666B
		public int TooManyRecipientsResponseCount { get; set; }

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06003521 RID: 13601 RVA: 0x000D8474 File Offset: 0x000D6674
		// (set) Token: 0x06003522 RID: 13602 RVA: 0x000D847C File Offset: 0x000D667C
		public Dictionary<string, string> ConsumerMailOptionalArguments { get; set; }
	}
}

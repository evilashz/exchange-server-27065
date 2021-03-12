using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200048B RID: 1163
	internal sealed class Rcpt2ParseOutput
	{
		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06003523 RID: 13603 RVA: 0x000D8485 File Offset: 0x000D6685
		// (set) Token: 0x06003524 RID: 13604 RVA: 0x000D848D File Offset: 0x000D668D
		public Dictionary<string, string> ConsumerMailOptionalArguments { get; set; }

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06003525 RID: 13605 RVA: 0x000D8496 File Offset: 0x000D6696
		// (set) Token: 0x06003526 RID: 13606 RVA: 0x000D849E File Offset: 0x000D669E
		public RoutingAddress RecipientAddress { get; set; }
	}
}

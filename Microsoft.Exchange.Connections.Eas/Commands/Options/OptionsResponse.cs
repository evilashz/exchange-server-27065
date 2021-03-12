using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Options
{
	// Token: 0x0200005D RID: 93
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class OptionsResponse : IHaveAnHttpStatus
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00005384 File Offset: 0x00003584
		public OptionsResponse()
		{
			this.EasServerCapabilities = new EasServerCapabilities();
			this.EasExtensionCapabilities = new EasExtensionCapabilities();
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000053A2 File Offset: 0x000035A2
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x000053AA File Offset: 0x000035AA
		public HttpStatus HttpStatus { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000053B3 File Offset: 0x000035B3
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x000053BB File Offset: 0x000035BB
		internal OptionsStatus OptionsStatus { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000053C4 File Offset: 0x000035C4
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000053CC File Offset: 0x000035CC
		internal EasServerCapabilities EasServerCapabilities { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000053D5 File Offset: 0x000035D5
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000053DD File Offset: 0x000035DD
		internal EasExtensionCapabilities EasExtensionCapabilities { get; set; }
	}
}

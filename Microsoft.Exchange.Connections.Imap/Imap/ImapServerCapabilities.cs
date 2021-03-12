using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ImapServerCapabilities : ServerCapabilities
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0000A169 File Offset: 0x00008369
		internal ImapServerCapabilities()
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000A171 File Offset: 0x00008371
		internal ImapServerCapabilities(IEnumerable<string> capabilities) : base(capabilities)
		{
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000A17A File Offset: 0x0000837A
		public bool SupportsImap4Rev1
		{
			get
			{
				return base.Capabilities.Contains("IMAP4REV1", StringComparer.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000A191 File Offset: 0x00008391
		public bool SupportsIdle
		{
			get
			{
				return base.Capabilities.Contains("IDLE", StringComparer.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000A1A8 File Offset: 0x000083A8
		public bool SupportsQuota
		{
			get
			{
				return base.Capabilities.Contains("QUOTA", StringComparer.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000A1BF File Offset: 0x000083BF
		public bool SupportsEnable
		{
			get
			{
				return base.Capabilities.Contains("ENABLE", StringComparer.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000A1D6 File Offset: 0x000083D6
		public bool SupportsUidPlus
		{
			get
			{
				return base.Capabilities.Contains("UIDPLUS", StringComparer.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000A1ED File Offset: 0x000083ED
		public bool SupportsChildren
		{
			get
			{
				return base.Capabilities.Contains("CHILDREN", StringComparer.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000A204 File Offset: 0x00008404
		public bool SupportsMove
		{
			get
			{
				return base.Capabilities.Contains("MOVE", StringComparer.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000A21B File Offset: 0x0000841B
		public bool SupportsCompressEqualsDeflate
		{
			get
			{
				return base.Capabilities.Contains("COMPRESS=DEFLATE", StringComparer.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000A232 File Offset: 0x00008432
		public bool SupportsXlist
		{
			get
			{
				return base.Capabilities.Contains("XLIST", StringComparer.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000A249 File Offset: 0x00008449
		public bool SupportsId
		{
			get
			{
				return base.Capabilities.Contains("ID", StringComparer.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000A260 File Offset: 0x00008460
		public bool SupportsUnselect
		{
			get
			{
				return base.Capabilities.Contains("UNSELECT", StringComparer.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000A277 File Offset: 0x00008477
		public bool SupportsNamespace
		{
			get
			{
				return base.Capabilities.Contains("UIDPLUS", StringComparer.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000A28E File Offset: 0x0000848E
		public bool SupportsXGmExt1
		{
			get
			{
				return base.Capabilities.Contains("X-GM-EXT-1", StringComparer.CurrentCultureIgnoreCase);
			}
		}

		// Token: 0x040000FC RID: 252
		internal const string Imap4Rev1Capability = "IMAP4REV1";

		// Token: 0x040000FD RID: 253
		internal const string IdleCapability = "IDLE";

		// Token: 0x040000FE RID: 254
		internal const string QuotaCapability = "QUOTA";

		// Token: 0x040000FF RID: 255
		internal const string EnableCapability = "ENABLE";

		// Token: 0x04000100 RID: 256
		internal const string UidPlusCapability = "UIDPLUS";

		// Token: 0x04000101 RID: 257
		internal const string ChildrenCapability = "CHILDREN";

		// Token: 0x04000102 RID: 258
		internal const string MoveCapability = "MOVE";

		// Token: 0x04000103 RID: 259
		internal const string CompressEqualsDeflateCapability = "COMPRESS=DEFLATE";

		// Token: 0x04000104 RID: 260
		internal const string XlistCapability = "XLIST";

		// Token: 0x04000105 RID: 261
		internal const string IdCapability = "ID";

		// Token: 0x04000106 RID: 262
		internal const string UnselectCapability = "UNSELECT";

		// Token: 0x04000107 RID: 263
		internal const string NamespaceCapability = "NAMESPACE";

		// Token: 0x04000108 RID: 264
		internal const string XGmExt1Capability = "X-GM-EXT-1";
	}
}

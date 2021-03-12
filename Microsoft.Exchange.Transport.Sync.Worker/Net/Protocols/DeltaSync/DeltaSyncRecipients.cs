using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x02000071 RID: 113
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeltaSyncRecipients
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0001758D File Offset: 0x0001578D
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x000175A8 File Offset: 0x000157A8
		internal IList<string> To
		{
			get
			{
				if (this.to == null)
				{
					this.to = DeltaSyncRecipients.ReadOnlyEmptyList;
				}
				return this.to;
			}
			set
			{
				this.to = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x000175B1 File Offset: 0x000157B1
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x000175CC File Offset: 0x000157CC
		internal IList<string> Cc
		{
			get
			{
				if (this.cc == null)
				{
					this.cc = DeltaSyncRecipients.ReadOnlyEmptyList;
				}
				return this.cc;
			}
			set
			{
				this.cc = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x000175D5 File Offset: 0x000157D5
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x000175F0 File Offset: 0x000157F0
		internal IList<string> Bcc
		{
			get
			{
				if (this.bcc == null)
				{
					this.bcc = DeltaSyncRecipients.ReadOnlyEmptyList;
				}
				return this.bcc;
			}
			set
			{
				this.bcc = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x000175F9 File Offset: 0x000157F9
		internal int Count
		{
			get
			{
				return this.To.Count + this.Cc.Count + this.Bcc.Count;
			}
		}

		// Token: 0x040002C6 RID: 710
		private static readonly ReadOnlyCollection<string> ReadOnlyEmptyList = new List<string>(0).AsReadOnly();

		// Token: 0x040002C7 RID: 711
		private IList<string> to;

		// Token: 0x040002C8 RID: 712
		private IList<string> cc;

		// Token: 0x040002C9 RID: 713
		private IList<string> bcc;
	}
}

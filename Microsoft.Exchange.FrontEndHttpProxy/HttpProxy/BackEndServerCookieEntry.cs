using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200003D RID: 61
	internal class BackEndServerCookieEntry : BackEndCookieEntryBase
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000A4C0 File Offset: 0x000086C0
		public BackEndServerCookieEntry(string fqdn, int version, ExDateTime expiryTime) : base(BackEndCookieEntryType.Server, expiryTime)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			this.Fqdn = fqdn;
			this.Version = version;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000A4EB File Offset: 0x000086EB
		public BackEndServerCookieEntry(string fqdn, int version) : this(fqdn, version, ExDateTime.UtcNow + BackEndCookieEntryBase.BackEndServerCookieLifeTime)
		{
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000A504 File Offset: 0x00008704
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000A50C File Offset: 0x0000870C
		public string Fqdn { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000A515 File Offset: 0x00008715
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x0000A51D File Offset: 0x0000871D
		public int Version { get; private set; }

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A526 File Offset: 0x00008726
		public override bool ShouldInvalidate(BackEndServer badTarget)
		{
			if (badTarget == null)
			{
				throw new ArgumentNullException("badTarget");
			}
			return string.Equals(this.Fqdn, badTarget.Fqdn, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A548 File Offset: 0x00008748
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				BackEndCookieEntryBase.ConvertBackEndCookieEntryTypeToString(base.EntryType),
				"~",
				this.Fqdn,
				"~",
				this.Version.ToString(),
				"~",
				base.ExpiryTime.ToString("s")
			});
		}
	}
}

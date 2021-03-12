using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000032 RID: 50
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SyncPoisonItem
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000BE49 File Offset: 0x0000A049
		public SyncPoisonItem(string id, SyncPoisonEntitySource source, SyncPoisonEntityType type)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("id", id);
			this.id = id;
			this.source = source;
			this.type = type;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000BE74 File Offset: 0x0000A074
		public string Key
		{
			get
			{
				if (this.key == null)
				{
					this.key = string.Concat(new object[]
					{
						(int)this.Source,
						":",
						(int)this.Type,
						":",
						SyncPoisonItem.Encode(this.id)
					});
				}
				return this.key;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		public SyncPoisonEntitySource Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000BEEC File Offset: 0x0000A0EC
		public SyncPoisonEntityType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		public override string ToString()
		{
			if (this.cachedToString == null)
			{
				this.cachedToString = string.Format(CultureInfo.InvariantCulture, "ItemId: {0}, Source: {1}, Type: {2}", new object[]
				{
					this.id,
					this.source,
					this.type
				});
			}
			return this.cachedToString;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000BF51 File Offset: 0x0000A151
		private static string Encode(string inputString)
		{
			return Convert.ToBase64String(Encoding.Unicode.GetBytes(inputString));
		}

		// Token: 0x0400015B RID: 347
		private const string FormatString = "ItemId: {0}, Source: {1}, Type: {2}";

		// Token: 0x0400015C RID: 348
		private const string Seperator = ":";

		// Token: 0x0400015D RID: 349
		private readonly string id;

		// Token: 0x0400015E RID: 350
		private readonly SyncPoisonEntitySource source;

		// Token: 0x0400015F RID: 351
		private readonly SyncPoisonEntityType type;

		// Token: 0x04000160 RID: 352
		private string key;

		// Token: 0x04000161 RID: 353
		private string cachedToString;
	}
}

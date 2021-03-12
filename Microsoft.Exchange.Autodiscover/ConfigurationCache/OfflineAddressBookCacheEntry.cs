using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Autodiscover.ConfigurationCache
{
	// Token: 0x02000024 RID: 36
	public class OfflineAddressBookCacheEntry
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00007228 File Offset: 0x00005428
		private OfflineAddressBookCacheEntry(OfflineAddressBook oab)
		{
			this.hasValue = (oab != null);
			if (this.hasValue)
			{
				this.webDistributionEnabled = oab.WebDistributionEnabled;
				this.globalWebDistributionEnabled = oab.GlobalWebDistributionEnabled;
				this.id = oab.Id;
				this.distinguishedName = oab.Id.DistinguishedName;
				this.exchangeObjectId = oab.ExchangeObjectId;
				this.exchangeVersion = oab.ExchangeVersion;
				this.isDefault = oab.IsDefault;
				if (this.isDefault)
				{
					this.configurationUnitId = oab.ConfigurationUnit;
				}
			}
			this.createdTime = DateTime.UtcNow;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000072C8 File Offset: 0x000054C8
		[CLSCompliant(false)]
		public static OfflineAddressBookCacheEntry Create(OfflineAddressBook oab)
		{
			return new OfflineAddressBookCacheEntry(oab);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000072D0 File Offset: 0x000054D0
		public bool HasValue
		{
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000072D8 File Offset: 0x000054D8
		public bool WebDistributionEnabled
		{
			get
			{
				return this.webDistributionEnabled;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000072E0 File Offset: 0x000054E0
		public bool GlobalWebDistributionEnabled
		{
			get
			{
				return this.globalWebDistributionEnabled;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000072E8 File Offset: 0x000054E8
		[CLSCompliant(false)]
		public ADObjectId Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000072F0 File Offset: 0x000054F0
		[CLSCompliant(false)]
		public ADObjectId ConfigurationUnitId
		{
			get
			{
				return this.configurationUnitId;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000072F8 File Offset: 0x000054F8
		public string DistinguishedName
		{
			get
			{
				return this.distinguishedName;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00007300 File Offset: 0x00005500
		public Guid Guid
		{
			get
			{
				return this.exchangeObjectId;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00007308 File Offset: 0x00005508
		public bool IsDefault
		{
			get
			{
				return this.isDefault;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00007310 File Offset: 0x00005510
		[CLSCompliant(false)]
		public ExchangeObjectVersion ExchangeVersion
		{
			get
			{
				return this.exchangeVersion;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00007318 File Offset: 0x00005518
		public TimeSpan ElapsedTimeSinceCreated
		{
			get
			{
				return DateTime.UtcNow - this.createdTime;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000732A File Offset: 0x0000552A
		public void NullConfigurationUnit()
		{
			this.configurationUnitId = null;
		}

		// Token: 0x04000147 RID: 327
		private readonly bool hasValue;

		// Token: 0x04000148 RID: 328
		private readonly bool webDistributionEnabled;

		// Token: 0x04000149 RID: 329
		private readonly bool globalWebDistributionEnabled;

		// Token: 0x0400014A RID: 330
		private readonly ADObjectId id;

		// Token: 0x0400014B RID: 331
		private readonly string distinguishedName;

		// Token: 0x0400014C RID: 332
		private readonly Guid exchangeObjectId;

		// Token: 0x0400014D RID: 333
		private readonly bool isDefault;

		// Token: 0x0400014E RID: 334
		private readonly ExchangeObjectVersion exchangeVersion;

		// Token: 0x0400014F RID: 335
		private readonly DateTime createdTime;

		// Token: 0x04000150 RID: 336
		private ADObjectId configurationUnitId;
	}
}

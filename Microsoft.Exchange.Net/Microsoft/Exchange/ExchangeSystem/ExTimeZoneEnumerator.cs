using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000068 RID: 104
	public class ExTimeZoneEnumerator : IEnumerable<ExTimeZone>, IEnumerable
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000F308 File Offset: 0x0000D508
		public static ExTimeZoneEnumerator Instance
		{
			get
			{
				if (ExTimeZoneEnumerator.instance == null)
				{
					lock (ExTimeZoneEnumerator.instanceLock)
					{
						if (ExTimeZoneEnumerator.instance == null)
						{
							ExTimeZoneEnumerator.instance = new ExTimeZoneEnumerator(ExRegistryTimeZoneProvider.Instance);
						}
					}
				}
				return ExTimeZoneEnumerator.instance;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000F364 File Offset: 0x0000D564
		public static ExTimeZoneEnumerator InstanceWithReload
		{
			get
			{
				lock (ExTimeZoneEnumerator.instanceLock)
				{
					ExTimeZoneEnumerator.instance = null;
					ExTimeZoneEnumerator.instance = new ExTimeZoneEnumerator(ExRegistryTimeZoneProvider.InstanceWithReload);
				}
				return ExTimeZoneEnumerator.instance;
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		private ExTimeZoneEnumerator(ExRegistryTimeZoneProvider provider)
		{
			this.provider = provider;
			this.gmtOrderTimeZones = new List<ExTimeZone>(provider.GetTimeZones());
			this.gmtOrderTimeZones.Sort(new ExTimeZoneComparator());
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		public bool TryGetTimeZoneByName(string timeZoneName, out ExTimeZone timeZone)
		{
			return this.provider.TryGetTimeZoneById(timeZoneName, out timeZone);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000F3F7 File Offset: 0x0000D5F7
		public IEnumerator<ExTimeZone> GetEnumerator()
		{
			return this.gmtOrderTimeZones.GetEnumerator();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000F409 File Offset: 0x0000D609
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040001C7 RID: 455
		private static object instanceLock = new object();

		// Token: 0x040001C8 RID: 456
		private static ExTimeZoneEnumerator instance = null;

		// Token: 0x040001C9 RID: 457
		private readonly ExTimeZoneProviderBase provider;

		// Token: 0x040001CA RID: 458
		private readonly List<ExTimeZone> gmtOrderTimeZones;
	}
}

using System;
using System.ComponentModel;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005F0 RID: 1520
	[ImmutableObject(true)]
	public class UMTimeZone
	{
		// Token: 0x060047FC RID: 18428 RVA: 0x00109555 File Offset: 0x00107755
		internal UMTimeZone(string id)
		{
			ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(id, out this.TimeZone);
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x0010956F File Offset: 0x0010776F
		internal UMTimeZone(ExTimeZone etz)
		{
			this.TimeZone = etz;
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x00109580 File Offset: 0x00107780
		public static UMTimeZone Parse(string timeZoneName)
		{
			if (string.IsNullOrEmpty(timeZoneName))
			{
				throw new ArgumentNullException(timeZoneName);
			}
			ExTimeZone exTimeZone = null;
			foreach (ExTimeZone exTimeZone2 in ExTimeZoneEnumerator.Instance)
			{
				string text = exTimeZone2.LocalizableDisplayName.ToString(CultureInfo.CurrentCulture);
				if (string.Compare(timeZoneName, text, CultureInfo.CurrentCulture, CompareOptions.OrdinalIgnoreCase) == 0)
				{
					exTimeZone = exTimeZone2;
					break;
				}
				if (text.IndexOf(timeZoneName, StringComparison.OrdinalIgnoreCase) != -1)
				{
					if (exTimeZone != null)
					{
						throw new InvalidTimeZoneNameException(DirectoryStrings.AmbiguousTimeZoneNameError(timeZoneName));
					}
					exTimeZone = exTimeZone2;
				}
			}
			if (exTimeZone == null)
			{
				throw new InvalidTimeZoneNameException(DirectoryStrings.NonexistentTimeZoneError(timeZoneName));
			}
			return new UMTimeZone(exTimeZone);
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x00109634 File Offset: 0x00107834
		public override string ToString()
		{
			if (this.TimeZone == null)
			{
				return string.Empty;
			}
			return this.TimeZone.LocalizableDisplayName.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x06004800 RID: 18432 RVA: 0x00109667 File Offset: 0x00107867
		public string Name
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x06004801 RID: 18433 RVA: 0x0010966F File Offset: 0x0010786F
		internal string Id
		{
			get
			{
				if (this.TimeZone == null)
				{
					return string.Empty;
				}
				return this.TimeZone.Id;
			}
		}

		// Token: 0x0400319F RID: 12703
		internal ExTimeZone TimeZone;
	}
}

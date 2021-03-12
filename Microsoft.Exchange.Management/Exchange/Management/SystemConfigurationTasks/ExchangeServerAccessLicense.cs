using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009A6 RID: 2470
	[Serializable]
	public sealed class ExchangeServerAccessLicense : ExchangeServerAccessLicenseBase
	{
		// Token: 0x0600584D RID: 22605 RVA: 0x00170576 File Offset: 0x0016E776
		public ExchangeServerAccessLicense(ExchangeServerAccessLicense.ServerVersionMajor versionMajor, ExchangeServerAccessLicense.AccessLicenseType accessLicense, ExchangeServerAccessLicense.UnitLabelType unitLabel) : this(versionMajor, accessLicense, unitLabel, ExchangeServerAccessLicense.TabulationMethodType.Net)
		{
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x00170582 File Offset: 0x0016E782
		public ExchangeServerAccessLicense(ExchangeServerAccessLicense.ServerVersionMajor versionMajor, ExchangeServerAccessLicense.AccessLicenseType accessLicense, ExchangeServerAccessLicense.UnitLabelType unitLabel, ExchangeServerAccessLicense.TabulationMethodType tabulationMethod)
		{
			this.VersionMajor = versionMajor;
			this.AccessLicense = accessLicense;
			this.UnitLabel = unitLabel;
			this.TabulationMethod = tabulationMethod;
			this.InternalInitialize();
		}

		// Token: 0x17001A56 RID: 6742
		// (get) Token: 0x0600584F RID: 22607 RVA: 0x001705AD File Offset: 0x0016E7AD
		// (set) Token: 0x06005850 RID: 22608 RVA: 0x001705B5 File Offset: 0x0016E7B5
		public string ProductName { get; private set; }

		// Token: 0x17001A57 RID: 6743
		// (get) Token: 0x06005851 RID: 22609 RVA: 0x001705BE File Offset: 0x0016E7BE
		// (set) Token: 0x06005852 RID: 22610 RVA: 0x001705C6 File Offset: 0x0016E7C6
		public ExchangeServerAccessLicense.UnitLabelType UnitLabel { get; private set; }

		// Token: 0x17001A58 RID: 6744
		// (get) Token: 0x06005853 RID: 22611 RVA: 0x001705CF File Offset: 0x0016E7CF
		// (set) Token: 0x06005854 RID: 22612 RVA: 0x001705D7 File Offset: 0x0016E7D7
		public ExchangeServerAccessLicense.TabulationMethodType TabulationMethod { get; private set; }

		// Token: 0x17001A59 RID: 6745
		// (get) Token: 0x06005855 RID: 22613 RVA: 0x001705E0 File Offset: 0x0016E7E0
		// (set) Token: 0x06005856 RID: 22614 RVA: 0x001705E8 File Offset: 0x0016E7E8
		internal ExchangeServerAccessLicense.ServerVersionMajor VersionMajor { get; private set; }

		// Token: 0x17001A5A RID: 6746
		// (get) Token: 0x06005857 RID: 22615 RVA: 0x001705F1 File Offset: 0x0016E7F1
		// (set) Token: 0x06005858 RID: 22616 RVA: 0x001705F9 File Offset: 0x0016E7F9
		internal ExchangeServerAccessLicense.AccessLicenseType AccessLicense { get; private set; }

		// Token: 0x06005859 RID: 22617 RVA: 0x00170620 File Offset: 0x0016E820
		public static bool TryParse(string licenseName, out ExchangeServerAccessLicense license)
		{
			license = null;
			if (string.IsNullOrEmpty(licenseName))
			{
				return false;
			}
			string[] array = licenseName.Split(new char[]
			{
				' '
			});
			if (array.Length != 5)
			{
				return false;
			}
			int num = 0;
			string productName = string.Format("{0} {1} {2}", array[num++], array[num++], array[num++]);
			KeyValuePair<ExchangeServerAccessLicense.ServerVersionMajor, string> keyValuePair = ExchangeServerAccessLicense.VersionMajorProductNameMap.SingleOrDefault((KeyValuePair<ExchangeServerAccessLicense.ServerVersionMajor, string> x) => x.Value.Equals(productName, StringComparison.InvariantCultureIgnoreCase));
			if (keyValuePair.Equals(default(KeyValuePair<ExchangeServerAccessLicense.ServerVersionMajor, string>)))
			{
				return false;
			}
			ExchangeServerAccessLicense.ServerVersionMajor key = keyValuePair.Key;
			ExchangeServerAccessLicense.AccessLicenseType accessLicense;
			if (!ExchangeServerAccessLicense.TryStringToEnum<ExchangeServerAccessLicense.AccessLicenseType>(array[num++], false, out accessLicense))
			{
				return false;
			}
			string text = array[num++];
			ExchangeServerAccessLicense.UnitLabelType unitLabelType;
			if (text.Equals("Edition", StringComparison.InvariantCultureIgnoreCase))
			{
				unitLabelType = ExchangeServerAccessLicense.UnitLabelType.Server;
			}
			else
			{
				if (!ExchangeServerAccessLicense.TryStringToEnum<ExchangeServerAccessLicense.UnitLabelType>(text, false, out unitLabelType))
				{
					return false;
				}
				if (unitLabelType == ExchangeServerAccessLicense.UnitLabelType.Server)
				{
					return false;
				}
			}
			license = new ExchangeServerAccessLicense(key, accessLicense, unitLabelType);
			return true;
		}

		// Token: 0x0600585A RID: 22618 RVA: 0x0017071C File Offset: 0x0016E91C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ProductName: ");
			stringBuilder.AppendLine(this.ProductName);
			stringBuilder.Append("VersionMajor: ");
			stringBuilder.AppendLine(this.VersionMajor.ToString());
			stringBuilder.Append("AccessLicense: ");
			stringBuilder.AppendLine(this.AccessLicense.ToString());
			stringBuilder.Append("UnitLabel: ");
			stringBuilder.AppendLine(this.UnitLabel.ToString());
			stringBuilder.Append("TabulationMethod: ");
			stringBuilder.AppendLine(this.TabulationMethod.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x0600585B RID: 22619 RVA: 0x001707DC File Offset: 0x0016E9DC
		internal static bool TryStringToEnum<TEnum>(string input, bool isUnderlyingNumber, out TEnum output) where TEnum : struct
		{
			output = default(TEnum);
			if (string.IsNullOrEmpty(input))
			{
				return false;
			}
			try
			{
				if (!Enum.TryParse<TEnum>(input, true, out output))
				{
					return false;
				}
				int num;
				bool flag = int.TryParse(input, out num);
				if (isUnderlyingNumber)
				{
					if (!flag)
					{
						return false;
					}
					if (!Enum.IsDefined(typeof(TEnum), output))
					{
						return false;
					}
				}
				else if (flag)
				{
					return false;
				}
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600585C RID: 22620 RVA: 0x0017085C File Offset: 0x0016EA5C
		private void InternalInitialize()
		{
			this.ProductName = ExchangeServerAccessLicense.VersionMajorProductNameMap[this.VersionMajor];
			base.LicenseName = string.Format("{0} {1} {2}", this.ProductName, this.AccessLicense, (this.UnitLabel == ExchangeServerAccessLicense.UnitLabelType.Server) ? "Edition" : this.UnitLabel.ToString());
		}

		// Token: 0x040032BB RID: 12987
		private const string Edition = "Edition";

		// Token: 0x040032BC RID: 12988
		private const int TokenLength = 5;

		// Token: 0x040032BD RID: 12989
		private static readonly Dictionary<ExchangeServerAccessLicense.ServerVersionMajor, string> VersionMajorProductNameMap = new Dictionary<ExchangeServerAccessLicense.ServerVersionMajor, string>
		{
			{
				ExchangeServerAccessLicense.ServerVersionMajor.E15,
				"Exchange Server 2013"
			}
		};

		// Token: 0x020009A7 RID: 2471
		public enum ServerVersionMajor
		{
			// Token: 0x040032C4 RID: 12996
			E15 = 15
		}

		// Token: 0x020009A8 RID: 2472
		public enum AccessLicenseType
		{
			// Token: 0x040032C6 RID: 12998
			Standard,
			// Token: 0x040032C7 RID: 12999
			Enterprise
		}

		// Token: 0x020009A9 RID: 2473
		public enum UnitLabelType
		{
			// Token: 0x040032C9 RID: 13001
			Server,
			// Token: 0x040032CA RID: 13002
			CAL
		}

		// Token: 0x020009AA RID: 2474
		public enum TabulationMethodType
		{
			// Token: 0x040032CC RID: 13004
			Net
		}
	}
}

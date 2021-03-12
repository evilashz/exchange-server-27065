using System;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Xml.Serialization;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000670 RID: 1648
	[XmlRoot("S")]
	[Serializable]
	public sealed class SettingOverrideXml : XMLSerializableBase
	{
		// Token: 0x06004CEF RID: 19695 RVA: 0x0011C9AC File Offset: 0x0011ABAC
		private static Version GetVersion()
		{
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(typeof(VariantConfiguration).Assembly.Location);
			return Version.Parse(versionInfo.ProductVersion);
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x0011C9DE File Offset: 0x0011ABDE
		public SettingOverrideXml()
		{
			this.Parameters = new MultiValuedProperty<string>();
		}

		// Token: 0x1700194E RID: 6478
		// (get) Token: 0x06004CF1 RID: 19697 RVA: 0x0011C9F1 File Offset: 0x0011ABF1
		// (set) Token: 0x06004CF2 RID: 19698 RVA: 0x0011C9F9 File Offset: 0x0011ABF9
		[XmlAttribute("CN")]
		public string ComponentName { get; set; }

		// Token: 0x1700194F RID: 6479
		// (get) Token: 0x06004CF3 RID: 19699 RVA: 0x0011CA02 File Offset: 0x0011AC02
		// (set) Token: 0x06004CF4 RID: 19700 RVA: 0x0011CA0A File Offset: 0x0011AC0A
		[XmlAttribute("SN")]
		public string SectionName { get; set; }

		// Token: 0x17001950 RID: 6480
		// (get) Token: 0x06004CF5 RID: 19701 RVA: 0x0011CA13 File Offset: 0x0011AC13
		// (set) Token: 0x06004CF6 RID: 19702 RVA: 0x0011CA1B File Offset: 0x0011AC1B
		[XmlAttribute("FN")]
		public string FlightName { get; set; }

		// Token: 0x17001951 RID: 6481
		// (get) Token: 0x06004CF7 RID: 19703 RVA: 0x0011CA24 File Offset: 0x0011AC24
		// (set) Token: 0x06004CF8 RID: 19704 RVA: 0x0011CA2C File Offset: 0x0011AC2C
		[XmlAttribute("MB")]
		public string ModifiedBy { get; set; }

		// Token: 0x17001952 RID: 6482
		// (get) Token: 0x06004CF9 RID: 19705 RVA: 0x0011CA35 File Offset: 0x0011AC35
		// (set) Token: 0x06004CFA RID: 19706 RVA: 0x0011CA3D File Offset: 0x0011AC3D
		[XmlAttribute("R")]
		public string Reason { get; set; }

		// Token: 0x17001953 RID: 6483
		// (get) Token: 0x06004CFB RID: 19707 RVA: 0x0011CA46 File Offset: 0x0011AC46
		// (set) Token: 0x06004CFC RID: 19708 RVA: 0x0011CA4E File Offset: 0x0011AC4E
		[XmlIgnore]
		public Version MinVersion
		{
			get
			{
				return this.minVersion;
			}
			set
			{
				this.minVersion = this.Normalize(value);
			}
		}

		// Token: 0x17001954 RID: 6484
		// (get) Token: 0x06004CFD RID: 19709 RVA: 0x0011CA5D File Offset: 0x0011AC5D
		// (set) Token: 0x06004CFE RID: 19710 RVA: 0x0011CA65 File Offset: 0x0011AC65
		[XmlIgnore]
		public Version MaxVersion
		{
			get
			{
				return this.maxVersion;
			}
			set
			{
				this.maxVersion = this.Normalize(value);
			}
		}

		// Token: 0x17001955 RID: 6485
		// (get) Token: 0x06004CFF RID: 19711 RVA: 0x0011CA74 File Offset: 0x0011AC74
		// (set) Token: 0x06004D00 RID: 19712 RVA: 0x0011CA7C File Offset: 0x0011AC7C
		[XmlIgnore]
		public Version FixVersion
		{
			get
			{
				return this.fixVersion;
			}
			set
			{
				this.fixVersion = this.Normalize(value);
			}
		}

		// Token: 0x17001956 RID: 6486
		// (get) Token: 0x06004D01 RID: 19713 RVA: 0x0011CA8B File Offset: 0x0011AC8B
		// (set) Token: 0x06004D02 RID: 19714 RVA: 0x0011CA93 File Offset: 0x0011AC93
		[XmlArray("Ss")]
		[XmlArrayItem("S", typeof(string))]
		public string[] Server { get; set; }

		// Token: 0x17001957 RID: 6487
		// (get) Token: 0x06004D03 RID: 19715 RVA: 0x0011CA9C File Offset: 0x0011AC9C
		// (set) Token: 0x06004D04 RID: 19716 RVA: 0x0011CAA4 File Offset: 0x0011ACA4
		[XmlArray("Ps")]
		[XmlArrayItem("P", typeof(string))]
		public MultiValuedProperty<string> Parameters { get; set; }

		// Token: 0x17001958 RID: 6488
		// (get) Token: 0x06004D05 RID: 19717 RVA: 0x0011CAAD File Offset: 0x0011ACAD
		// (set) Token: 0x06004D06 RID: 19718 RVA: 0x0011CACA File Offset: 0x0011ACCA
		[XmlAttribute("LV")]
		public string MinVersionString
		{
			get
			{
				if (!(this.MinVersion != null))
				{
					return null;
				}
				return this.MinVersion.ToString();
			}
			set
			{
				this.MinVersion = (string.IsNullOrEmpty(value) ? null : Version.Parse(value));
			}
		}

		// Token: 0x17001959 RID: 6489
		// (get) Token: 0x06004D07 RID: 19719 RVA: 0x0011CAE3 File Offset: 0x0011ACE3
		// (set) Token: 0x06004D08 RID: 19720 RVA: 0x0011CB00 File Offset: 0x0011AD00
		[XmlAttribute("HV")]
		public string MaxVersionString
		{
			get
			{
				if (!(this.MaxVersion != null))
				{
					return null;
				}
				return this.MaxVersion.ToString();
			}
			set
			{
				this.MaxVersion = (string.IsNullOrEmpty(value) ? null : Version.Parse(value));
			}
		}

		// Token: 0x1700195A RID: 6490
		// (get) Token: 0x06004D09 RID: 19721 RVA: 0x0011CB19 File Offset: 0x0011AD19
		// (set) Token: 0x06004D0A RID: 19722 RVA: 0x0011CB36 File Offset: 0x0011AD36
		[XmlAttribute("FV")]
		public string FixVersionString
		{
			get
			{
				if (!(this.FixVersion != null))
				{
					return null;
				}
				return this.FixVersion.ToString();
			}
			set
			{
				this.FixVersion = (string.IsNullOrEmpty(value) ? null : Version.Parse(value));
			}
		}

		// Token: 0x1700195B RID: 6491
		// (get) Token: 0x06004D0B RID: 19723 RVA: 0x0011CB64 File Offset: 0x0011AD64
		[XmlIgnore]
		internal bool Applies
		{
			get
			{
				if (this.MinVersion != null && SettingOverrideXml.CurrentVersion < this.MinVersion)
				{
					return false;
				}
				if (this.MaxVersion != null && SettingOverrideXml.CurrentVersion > this.MaxVersion)
				{
					return false;
				}
				if (this.FixVersion != null && SettingOverrideXml.CurrentVersion >= this.FixVersion)
				{
					return false;
				}
				if (this.Server != null && this.Server.Length > 0)
				{
					if (!this.Server.Any((string wildcard) => new WildcardPattern(wildcard, WildcardOptions.IgnoreCase).IsMatch(Environment.MachineName)))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x0011CC18 File Offset: 0x0011AE18
		private Version Normalize(Version version)
		{
			if (version == null)
			{
				return null;
			}
			return new Version(version.Major, version.Minor, (version.Build < 0) ? 0 : version.Build, (version.Revision < 0) ? 0 : version.Revision);
		}

		// Token: 0x0400348F RID: 13455
		internal static readonly Version CurrentVersion = SettingOverrideXml.GetVersion();

		// Token: 0x04003490 RID: 13456
		private Version minVersion;

		// Token: 0x04003491 RID: 13457
		private Version maxVersion;

		// Token: 0x04003492 RID: 13458
		private Version fixVersion;
	}
}

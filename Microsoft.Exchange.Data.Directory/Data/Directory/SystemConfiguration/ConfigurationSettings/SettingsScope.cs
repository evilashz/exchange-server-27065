using System;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000671 RID: 1649
	[XmlInclude(typeof(SettingsDagScope))]
	[XmlInclude(typeof(SettingsProcessScope))]
	[XmlInclude(typeof(SettingsServerScope))]
	[XmlInclude(typeof(SettingsUserScope))]
	[XmlInclude(typeof(SettingsDatabaseScope))]
	[XmlInclude(typeof(SettingsForestScope))]
	[XmlInclude(typeof(SettingsGenericScope))]
	[XmlInclude(typeof(SettingsOrganizationScope))]
	[Serializable]
	public abstract class SettingsScope : XMLSerializableBase
	{
		// Token: 0x06004D0F RID: 19727 RVA: 0x0011CC71 File Offset: 0x0011AE71
		public SettingsScope()
		{
			this.ScopeId = Guid.NewGuid();
		}

		// Token: 0x06004D10 RID: 19728 RVA: 0x0011CC84 File Offset: 0x0011AE84
		public SettingsScope(string subType, string nameMatch) : this()
		{
			this.Restriction = new SettingsScopeRestriction(subType, nameMatch);
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x0011CC99 File Offset: 0x0011AE99
		public SettingsScope(Guid? guidMatch) : this()
		{
			this.Restriction = new SettingsScopeRestriction(guidMatch.Value);
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x0011CCB3 File Offset: 0x0011AEB3
		public SettingsScope(string nameMatch, string minVersion, string maxVersion) : this()
		{
			this.Restriction = new SettingsScopeRestriction(nameMatch, minVersion, maxVersion);
		}

		// Token: 0x1700195C RID: 6492
		// (get) Token: 0x06004D13 RID: 19731 RVA: 0x0011CCC9 File Offset: 0x0011AEC9
		// (set) Token: 0x06004D14 RID: 19732 RVA: 0x0011CCD1 File Offset: 0x0011AED1
		[XmlElement(ElementName = "Id")]
		public Guid ScopeId { get; set; }

		// Token: 0x1700195D RID: 6493
		// (get) Token: 0x06004D15 RID: 19733 RVA: 0x0011CCDA File Offset: 0x0011AEDA
		// (set) Token: 0x06004D16 RID: 19734 RVA: 0x0011CCE2 File Offset: 0x0011AEE2
		[XmlElement(ElementName = "Rs")]
		public SettingsScopeRestriction Restriction { get; set; }

		// Token: 0x1700195E RID: 6494
		// (get) Token: 0x06004D17 RID: 19735
		internal abstract int DefaultPriority { get; }

		// Token: 0x06004D18 RID: 19736 RVA: 0x0011CCEC File Offset: 0x0011AEEC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[{0}", base.GetType().Name);
			if (this.Restriction != null)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(this.Restriction.ToString());
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06004D19 RID: 19737
		internal abstract QueryFilter ConstructScopeFilter(IConfigSchema schema);

		// Token: 0x06004D1A RID: 19738
		internal abstract void Validate(IConfigSchema schema);
	}
}

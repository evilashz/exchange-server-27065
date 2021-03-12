using System;
using System.IO;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000138 RID: 312
	public struct VariantConfigurationSection
	{
		// Token: 0x06000E96 RID: 3734 RVA: 0x00023890 File Offset: 0x00021A90
		internal VariantConfigurationSection(string settingsFile, string name, Type type, bool isPublic)
		{
			this.FileName = settingsFile;
			this.SectionName = name;
			this.Type = type;
			this.IsPublic = isPublic;
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x000238AF File Offset: 0x00021AAF
		public string Name
		{
			get
			{
				return Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(this.FileName));
			}
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x000238C1 File Offset: 0x00021AC1
		public VariantConfigurationOverride CreateOverride(params string[] parameters)
		{
			if (this.FileName.EndsWith(".flight.ini", StringComparison.InvariantCultureIgnoreCase))
			{
				return new VariantConfigurationFlightOverride(this.SectionName, parameters);
			}
			return new VariantConfigurationSettingOverride(this.Name, this.SectionName, parameters);
		}

		// Token: 0x040004A7 RID: 1191
		public readonly string FileName;

		// Token: 0x040004A8 RID: 1192
		public readonly string SectionName;

		// Token: 0x040004A9 RID: 1193
		public readonly Type Type;

		// Token: 0x040004AA RID: 1194
		public readonly bool IsPublic;
	}
}

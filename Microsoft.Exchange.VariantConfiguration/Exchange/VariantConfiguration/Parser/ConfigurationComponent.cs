using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Search.Platform.Parallax;
using Microsoft.Search.Platform.Parallax.Util.IniFormat;
using Microsoft.Search.Platform.Parallax.Util.IniFormat.FileModel;

namespace Microsoft.Exchange.VariantConfiguration.Parser
{
	// Token: 0x02000131 RID: 305
	internal class ConfigurationComponent
	{
		// Token: 0x06000E5C RID: 3676 RVA: 0x00022C0C File Offset: 0x00020E0C
		internal ConfigurationComponent(string dataSourceName, IDictionary<string, ConfigurationSection> sections)
		{
			this.DataSourceName = dataSourceName;
			this.sections = sections;
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00022C22 File Offset: 0x00020E22
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x00022C2A File Offset: 0x00020E2A
		public string DataSourceName { get; private set; }

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00022C33 File Offset: 0x00020E33
		public ConfigurationComponentType ComponentType
		{
			get
			{
				if (string.Equals(this.DataSourceName, "Flighting.settings.ini", StringComparison.OrdinalIgnoreCase))
				{
					return ConfigurationComponentType.Team;
				}
				if (this.DataSourceName.EndsWith(".flight.ini", StringComparison.OrdinalIgnoreCase))
				{
					return ConfigurationComponentType.Flight;
				}
				return ConfigurationComponentType.Settings;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x00022C60 File Offset: 0x00020E60
		public IEnumerable<ConfigurationSection> Sections
		{
			get
			{
				return this.sections.Values;
			}
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00022C70 File Offset: 0x00020E70
		public static ConfigurationComponent Create(string dataSourcePath)
		{
			if (!File.Exists(dataSourcePath))
			{
				throw new ArgumentException(string.Format("{0} does not exist", dataSourcePath));
			}
			string input = File.ReadAllText(dataSourcePath);
			string fileName = Path.GetFileName(dataSourcePath);
			return ConfigurationComponent.Create(input, fileName);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00022CAC File Offset: 0x00020EAC
		internal static ConfigurationComponent Create(string input, string dataSourceName)
		{
			ConfigurationComponent result;
			try
			{
				IniFileModel ini = IniFileModel.CreateFromString(dataSourceName, input);
				IDictionary<string, ConfigurationSection> dictionary = ConfigurationComponent.ParseSections(ini);
				result = new ConfigurationComponent(dataSourceName, dictionary);
			}
			catch (IniParseException ex)
			{
				throw new VariantConfigurationIniParseException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00022CF4 File Offset: 0x00020EF4
		internal static bool TryCreate(string input, string dataSourceName, out ConfigurationComponent component)
		{
			try
			{
				component = ConfigurationComponent.Create(input, dataSourceName);
				return true;
			}
			catch (VariantConfigurationIniParseException)
			{
			}
			catch (VariantConfigurationConventionException)
			{
			}
			catch (VariantConfigurationSyntaxException)
			{
			}
			component = null;
			return false;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00022D48 File Offset: 0x00020F48
		internal bool ContainsSection(string section)
		{
			return this.sections.ContainsKey(section);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00022D56 File Offset: 0x00020F56
		internal ConfigurationSection GetSection(string section)
		{
			return this.sections[section];
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00022D64 File Offset: 0x00020F64
		private static IDictionary<string, ConfigurationSection> ParseSections(IniFileModel ini)
		{
			Dictionary<string, ConfigurationSection> dictionary = new Dictionary<string, ConfigurationSection>();
			foreach (Section section in ini.Sections.Values)
			{
				try
				{
					ConfigurationSection configurationSection = ConfigurationSection.Create(section);
					dictionary.Add(configurationSection.SectionName, configurationSection);
				}
				catch (VariantConfigurationSyntaxException ex)
				{
					throw new VariantConfigurationSyntaxException(string.Format("[Section: '{0}']: {1}", section.Name, ex.Message));
				}
				catch (TypeNotFoundException ex2)
				{
					throw new TypeNotFoundException(string.Format("[Section: '{0}']: {1}", section.Name, ex2.Message));
				}
			}
			return dictionary;
		}

		// Token: 0x04000496 RID: 1174
		private IDictionary<string, ConfigurationSection> sections;
	}
}

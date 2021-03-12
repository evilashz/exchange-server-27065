using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Search.Platform.Parallax;

namespace Microsoft.Exchange.VariantConfiguration.Parser
{
	// Token: 0x02000133 RID: 307
	public class ConfigurationParser
	{
		// Token: 0x06000E75 RID: 3701 RVA: 0x0002317B File Offset: 0x0002137B
		internal ConfigurationParser(IEnumerable<ConfigurationComponent> teamComponents, IEnumerable<ConfigurationComponent> flightComponents, IEnumerable<ConfigurationComponent> settingsComponents)
		{
			this.flightComponentMap = ConfigurationParser.CreateFlightComponentMap(flightComponents);
			this.SettingsComponents = settingsComponents;
			this.TeamComponents = teamComponents;
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0002319D File Offset: 0x0002139D
		// (set) Token: 0x06000E77 RID: 3703 RVA: 0x000231A5 File Offset: 0x000213A5
		internal IEnumerable<ConfigurationComponent> TeamComponents { get; private set; }

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x000231AE File Offset: 0x000213AE
		internal IEnumerable<ConfigurationComponent> FlightComponents
		{
			get
			{
				return this.flightComponentMap.Values.Distinct<ConfigurationComponent>();
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x000231C0 File Offset: 0x000213C0
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x000231C8 File Offset: 0x000213C8
		internal IEnumerable<ConfigurationComponent> SettingsComponents { get; private set; }

		// Token: 0x06000E7B RID: 3707 RVA: 0x000231D4 File Offset: 0x000213D4
		public static ConfigurationParser Create(IEnumerable<string> dataSourcePaths)
		{
			List<ConfigurationComponent> list = new List<ConfigurationComponent>();
			List<ConfigurationComponent> list2 = new List<ConfigurationComponent>();
			List<ConfigurationComponent> list3 = new List<ConfigurationComponent>();
			foreach (string text in dataSourcePaths)
			{
				try
				{
					ConfigurationComponent configurationComponent = ConfigurationComponent.Create(text);
					switch (configurationComponent.ComponentType)
					{
					case ConfigurationComponentType.Flight:
						list2.Add(configurationComponent);
						break;
					case ConfigurationComponentType.Team:
						list.Add(configurationComponent);
						break;
					default:
						list3.Add(configurationComponent);
						break;
					}
				}
				catch (VariantConfigurationSyntaxException ex)
				{
					throw new VariantConfigurationSyntaxException(string.Format("[Component: '{0}']: {1}", text, ex.Message));
				}
				catch (TypeNotFoundException ex2)
				{
					throw new TypeNotFoundException(string.Format("[Component: '{0}']: {1}", text, ex2.Message));
				}
			}
			return new ConfigurationParser(list, list2, list3);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x000232C8 File Offset: 0x000214C8
		public bool DoesFlightUseRotate(string flightName)
		{
			return this.DoesFlightUseProperty(flightName, "Rotate");
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x000232D6 File Offset: 0x000214D6
		public bool DoesFlightUseRamp(string flightName)
		{
			return this.DoesFlightUseProperty(flightName, "Ramp");
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00023300 File Offset: 0x00021500
		public IEnumerable<string> GetFlightDependencies(string dataSourceName, string sectionName)
		{
			if (string.IsNullOrEmpty(dataSourceName))
			{
				throw new ArgumentNullException("dataSourceName");
			}
			if (string.IsNullOrEmpty(sectionName))
			{
				throw new ArgumentNullException("sectionName");
			}
			ConfigurationComponent configurationComponent = this.SettingsComponents.FirstOrDefault((ConfigurationComponent comp) => string.Equals(comp.DataSourceName, dataSourceName, StringComparison.OrdinalIgnoreCase));
			if (configurationComponent == null)
			{
				throw new ArgumentException(string.Format("Could not find data source with name {0}", dataSourceName));
			}
			if (!configurationComponent.ContainsSection(sectionName))
			{
				throw new ArgumentException(string.Format("Could not find section {0} in data source {1}", sectionName, dataSourceName));
			}
			ConfigurationSection section = configurationComponent.GetSection(sectionName);
			return section.GetFlightDependencies();
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x000233A4 File Offset: 0x000215A4
		internal bool DoesFlightUseProperty(string flightName, string propertyName)
		{
			if (string.IsNullOrEmpty(flightName))
			{
				throw new ArgumentNullException(flightName);
			}
			if (!this.flightComponentMap.ContainsKey(flightName))
			{
				throw new ArgumentException(string.Format("Flight {0} does not exist in the current context", flightName));
			}
			ConfigurationSection section = this.flightComponentMap[flightName].GetSection(flightName);
			foreach (ConfigurationParameter configurationParameter in section.Parameters)
			{
				if (string.Equals(configurationParameter.Name, propertyName, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00023444 File Offset: 0x00021644
		private static IDictionary<string, ConfigurationComponent> CreateFlightComponentMap(IEnumerable<ConfigurationComponent> flightComponents)
		{
			Dictionary<string, ConfigurationComponent> dictionary = new Dictionary<string, ConfigurationComponent>(StringComparer.OrdinalIgnoreCase);
			foreach (ConfigurationComponent configurationComponent in flightComponents)
			{
				foreach (ConfigurationSection configurationSection in configurationComponent.Sections)
				{
					dictionary[configurationSection.SectionName] = configurationComponent;
				}
			}
			return dictionary;
		}

		// Token: 0x0400049F RID: 1183
		internal const string FlightPrefix = "flt.";

		// Token: 0x040004A0 RID: 1184
		private IDictionary<string, ConfigurationComponent> flightComponentMap;
	}
}

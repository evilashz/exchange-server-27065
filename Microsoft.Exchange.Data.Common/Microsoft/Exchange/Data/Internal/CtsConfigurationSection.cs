using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000148 RID: 328
	internal sealed class CtsConfigurationSection : ConfigurationSection
	{
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0006EC6B File Offset: 0x0006CE6B
		public Dictionary<string, IList<CtsConfigurationSetting>> SubSectionsDictionary
		{
			get
			{
				return this.subSections;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0006EC73 File Offset: 0x0006CE73
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (CtsConfigurationSection.properties == null)
				{
					CtsConfigurationSection.properties = new ConfigurationPropertyCollection();
				}
				return CtsConfigurationSection.properties;
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0006EC8C File Offset: 0x0006CE8C
		protected override void DeserializeSection(XmlReader reader)
		{
			IList<CtsConfigurationSetting> list = new List<CtsConfigurationSetting>();
			this.subSections.Add(string.Empty, list);
			if (!reader.Read() || reader.NodeType != XmlNodeType.Element)
			{
				throw new ConfigurationErrorsException("error", reader);
			}
			if (!reader.IsEmptyElement)
			{
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						if (reader.IsEmptyElement)
						{
							CtsConfigurationSetting item = this.DeserializeSetting(reader);
							list.Add(item);
						}
						else
						{
							string name = reader.Name;
							IList<CtsConfigurationSetting> list2;
							if (!this.subSections.TryGetValue(name, out list2))
							{
								list2 = new List<CtsConfigurationSetting>();
								this.subSections.Add(name, list2);
							}
							while (reader.Read())
							{
								if (reader.NodeType == XmlNodeType.Element)
								{
									if (!reader.IsEmptyElement)
									{
										throw new ConfigurationErrorsException("error", reader);
									}
									CtsConfigurationSetting item2 = this.DeserializeSetting(reader);
									list2.Add(item2);
								}
								else
								{
									if (reader.NodeType == XmlNodeType.EndElement)
									{
										break;
									}
									if (reader.NodeType == XmlNodeType.CDATA || reader.NodeType == XmlNodeType.Text)
									{
										throw new ConfigurationErrorsException("error", reader);
									}
								}
							}
						}
					}
					else
					{
						if (reader.NodeType == XmlNodeType.EndElement)
						{
							return;
						}
						if (reader.NodeType == XmlNodeType.CDATA || reader.NodeType == XmlNodeType.Text)
						{
							throw new ConfigurationErrorsException("error", reader);
						}
					}
				}
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0006EDC8 File Offset: 0x0006CFC8
		private CtsConfigurationSetting DeserializeSetting(XmlReader reader)
		{
			string name = reader.Name;
			CtsConfigurationSetting ctsConfigurationSetting = new CtsConfigurationSetting(name);
			if (reader.AttributeCount > 0)
			{
				while (reader.MoveToNextAttribute())
				{
					string name2 = reader.Name;
					string value = reader.Value;
					ctsConfigurationSetting.AddArgument(name2, value);
				}
			}
			return ctsConfigurationSetting;
		}

		// Token: 0x04000F1F RID: 3871
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000F20 RID: 3872
		private Dictionary<string, IList<CtsConfigurationSetting>> subSections = new Dictionary<string, IList<CtsConfigurationSetting>>();
	}
}

using System;
using System.Configuration;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Common.Probes
{
	// Token: 0x020000C9 RID: 201
	public class FilteredEventSection : ConfigurationSection
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x000278F9 File Offset: 0x00025AF9
		[ConfigurationProperty("controlPanelRedEvent4")]
		public KeyValueConfigurationCollection ControlPanelRedEvent4
		{
			get
			{
				return (KeyValueConfigurationCollection)base["controlPanelRedEvent4"];
			}
		}
	}
}

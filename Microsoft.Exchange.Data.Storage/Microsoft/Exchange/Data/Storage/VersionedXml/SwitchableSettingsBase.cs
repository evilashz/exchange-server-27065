using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000ECA RID: 3786
	[Serializable]
	public abstract class SwitchableSettingsBase
	{
		// Token: 0x06008297 RID: 33431 RVA: 0x00239D3B File Offset: 0x00237F3B
		protected SwitchableSettingsBase()
		{
		}

		// Token: 0x06008298 RID: 33432 RVA: 0x00239D43 File Offset: 0x00237F43
		protected SwitchableSettingsBase(bool enabled)
		{
			this.Enabled = enabled;
		}

		// Token: 0x17002299 RID: 8857
		// (get) Token: 0x06008299 RID: 33433 RVA: 0x00239D52 File Offset: 0x00237F52
		// (set) Token: 0x0600829A RID: 33434 RVA: 0x00239D5A File Offset: 0x00237F5A
		[XmlElement("Enabled")]
		public bool Enabled { get; set; }
	}
}

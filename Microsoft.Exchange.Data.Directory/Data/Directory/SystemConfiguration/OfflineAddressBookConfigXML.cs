using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000516 RID: 1302
	[XmlType(TypeName = "OfflineAddressBookConfigXML")]
	public class OfflineAddressBookConfigXML : XMLSerializableBase
	{
		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x0600399C RID: 14748 RVA: 0x000DE92B File Offset: 0x000DCB2B
		// (set) Token: 0x0600399D RID: 14749 RVA: 0x000DE933 File Offset: 0x000DCB33
		[XmlElement(ElementName = "ManifestVersion")]
		public OfflineAddressBookManifestVersion ManifestVersion
		{
			get
			{
				return this.manifestVersion;
			}
			set
			{
				this.manifestVersion = value;
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000DE93C File Offset: 0x000DCB3C
		// (set) Token: 0x0600399F RID: 14751 RVA: 0x000DE944 File Offset: 0x000DCB44
		[XmlElement(ElementName = "LastFailedTime")]
		public DateTime? LastFailedTime
		{
			get
			{
				return this.lastFailedTime;
			}
			set
			{
				this.lastFailedTime = value;
			}
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x060039A0 RID: 14752 RVA: 0x000DE94D File Offset: 0x000DCB4D
		// (set) Token: 0x060039A1 RID: 14753 RVA: 0x000DE955 File Offset: 0x000DCB55
		[XmlElement(ElementName = "LastGeneratingData")]
		public OfflineAddressBookLastGeneratingData LastGeneratingData
		{
			get
			{
				return this.lastGeneratingData;
			}
			set
			{
				this.lastGeneratingData = value;
			}
		}

		// Token: 0x04002765 RID: 10085
		private OfflineAddressBookManifestVersion manifestVersion;

		// Token: 0x04002766 RID: 10086
		private DateTime? lastFailedTime;

		// Token: 0x04002767 RID: 10087
		private OfflineAddressBookLastGeneratingData lastGeneratingData;
	}
}

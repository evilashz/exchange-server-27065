using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000126 RID: 294
	[XmlRoot(ElementName = "Settings", Namespace = "HMSETTINGS:", IsNullable = false)]
	[Serializable]
	public class Settings
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0001BFF1 File Offset: 0x0001A1F1
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x0001C00C File Offset: 0x0001A20C
		[XmlIgnore]
		public ServiceSettingsType ServiceSettings
		{
			get
			{
				if (this.internalServiceSettings == null)
				{
					this.internalServiceSettings = new ServiceSettingsType();
				}
				return this.internalServiceSettings;
			}
			set
			{
				this.internalServiceSettings = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0001C015 File Offset: 0x0001A215
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x0001C030 File Offset: 0x0001A230
		[XmlIgnore]
		public AccountSettingsType AccountSettings
		{
			get
			{
				if (this.internalAccountSettings == null)
				{
					this.internalAccountSettings = new AccountSettingsType();
				}
				return this.internalAccountSettings;
			}
			set
			{
				this.internalAccountSettings = value;
			}
		}

		// Token: 0x040004D5 RID: 1237
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ServiceSettingsType), ElementName = "ServiceSettings", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public ServiceSettingsType internalServiceSettings;

		// Token: 0x040004D6 RID: 1238
		[XmlElement(Type = typeof(AccountSettingsType), ElementName = "AccountSettings", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AccountSettingsType internalAccountSettings;
	}
}

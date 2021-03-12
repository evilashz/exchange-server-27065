using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200015D RID: 349
	[XmlRoot(ElementName = "Settings", Namespace = "HMSETTINGS:", IsNullable = false)]
	[Serializable]
	public class Settings
	{
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0001D259 File Offset: 0x0001B459
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x0001D261 File Offset: 0x0001B461
		[XmlIgnore]
		public int Status
		{
			get
			{
				return this.internalStatus;
			}
			set
			{
				this.internalStatus = value;
				this.internalStatusSpecified = true;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0001D271 File Offset: 0x0001B471
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x0001D28C File Offset: 0x0001B48C
		[XmlIgnore]
		public SettingsFault Fault
		{
			get
			{
				if (this.internalFault == null)
				{
					this.internalFault = new SettingsFault();
				}
				return this.internalFault;
			}
			set
			{
				this.internalFault = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0001D295 File Offset: 0x0001B495
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x0001D2B0 File Offset: 0x0001B4B0
		[XmlIgnore]
		public SettingsAuthPolicy AuthPolicy
		{
			get
			{
				if (this.internalAuthPolicy == null)
				{
					this.internalAuthPolicy = new SettingsAuthPolicy();
				}
				return this.internalAuthPolicy;
			}
			set
			{
				this.internalAuthPolicy = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0001D2B9 File Offset: 0x0001B4B9
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x0001D2D4 File Offset: 0x0001B4D4
		[XmlIgnore]
		public ServiceSettings ServiceSettings
		{
			get
			{
				if (this.internalServiceSettings == null)
				{
					this.internalServiceSettings = new ServiceSettings();
				}
				return this.internalServiceSettings;
			}
			set
			{
				this.internalServiceSettings = value;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0001D2DD File Offset: 0x0001B4DD
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x0001D2F8 File Offset: 0x0001B4F8
		[XmlIgnore]
		public AccountSettings AccountSettings
		{
			get
			{
				if (this.internalAccountSettings == null)
				{
					this.internalAccountSettings = new AccountSettings();
				}
				return this.internalAccountSettings;
			}
			set
			{
				this.internalAccountSettings = value;
			}
		}

		// Token: 0x040005B1 RID: 1457
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x040005B2 RID: 1458
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x040005B3 RID: 1459
		[XmlElement(Type = typeof(SettingsFault), ElementName = "Fault", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SettingsFault internalFault;

		// Token: 0x040005B4 RID: 1460
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(SettingsAuthPolicy), ElementName = "AuthPolicy", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public SettingsAuthPolicy internalAuthPolicy;

		// Token: 0x040005B5 RID: 1461
		[XmlElement(Type = typeof(ServiceSettings), ElementName = "ServiceSettings", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ServiceSettings internalServiceSettings;

		// Token: 0x040005B6 RID: 1462
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AccountSettings), ElementName = "AccountSettings", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public AccountSettings internalAccountSettings;
	}
}

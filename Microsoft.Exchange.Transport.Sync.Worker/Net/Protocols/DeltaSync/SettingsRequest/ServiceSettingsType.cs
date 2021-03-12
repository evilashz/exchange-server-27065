using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000F6 RID: 246
	[XmlType(TypeName = "ServiceSettingsType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ServiceSettingsType
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0001AD0C File Offset: 0x00018F0C
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x0001AD14 File Offset: 0x00018F14
		[XmlIgnore]
		public string SafetySchemaVersion
		{
			get
			{
				return this.internalSafetySchemaVersion;
			}
			set
			{
				this.internalSafetySchemaVersion = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0001AD1D File Offset: 0x00018F1D
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x0001AD38 File Offset: 0x00018F38
		[XmlIgnore]
		public SafetyLevelRules SafetyLevelRules
		{
			get
			{
				if (this.internalSafetyLevelRules == null)
				{
					this.internalSafetyLevelRules = new SafetyLevelRules();
				}
				return this.internalSafetyLevelRules;
			}
			set
			{
				this.internalSafetyLevelRules = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0001AD41 File Offset: 0x00018F41
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x0001AD5C File Offset: 0x00018F5C
		[XmlIgnore]
		public SafetyActions SafetyActions
		{
			get
			{
				if (this.internalSafetyActions == null)
				{
					this.internalSafetyActions = new SafetyActions();
				}
				return this.internalSafetyActions;
			}
			set
			{
				this.internalSafetyActions = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0001AD65 File Offset: 0x00018F65
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x0001AD80 File Offset: 0x00018F80
		[XmlIgnore]
		public Properties Properties
		{
			get
			{
				if (this.internalProperties == null)
				{
					this.internalProperties = new Properties();
				}
				return this.internalProperties;
			}
			set
			{
				this.internalProperties = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0001AD89 File Offset: 0x00018F89
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x0001ADA4 File Offset: 0x00018FA4
		[XmlIgnore]
		public Lists Lists
		{
			get
			{
				if (this.internalLists == null)
				{
					this.internalLists = new Lists();
				}
				return this.internalLists;
			}
			set
			{
				this.internalLists = value;
			}
		}

		// Token: 0x04000426 RID: 1062
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "SafetySchemaVersion", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public string internalSafetySchemaVersion;

		// Token: 0x04000427 RID: 1063
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(SafetyLevelRules), ElementName = "SafetyLevelRules", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public SafetyLevelRules internalSafetyLevelRules;

		// Token: 0x04000428 RID: 1064
		[XmlElement(Type = typeof(SafetyActions), ElementName = "SafetyActions", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SafetyActions internalSafetyActions;

		// Token: 0x04000429 RID: 1065
		[XmlElement(Type = typeof(Properties), ElementName = "Properties", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Properties internalProperties;

		// Token: 0x0400042A RID: 1066
		[XmlElement(Type = typeof(Lists), ElementName = "Lists", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Lists internalLists;
	}
}

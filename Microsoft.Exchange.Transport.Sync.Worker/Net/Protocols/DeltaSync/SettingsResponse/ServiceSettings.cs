using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000160 RID: 352
	[XmlType(TypeName = "ServiceSettings", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class ServiceSettings
	{
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0001D36E File Offset: 0x0001B56E
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x0001D376 File Offset: 0x0001B576
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

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0001D386 File Offset: 0x0001B586
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x0001D3A1 File Offset: 0x0001B5A1
		[XmlIgnore]
		public RulesResponseType SafetyLevelRules
		{
			get
			{
				if (this.internalSafetyLevelRules == null)
				{
					this.internalSafetyLevelRules = new RulesResponseType();
				}
				return this.internalSafetyLevelRules;
			}
			set
			{
				this.internalSafetyLevelRules = value;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0001D3AA File Offset: 0x0001B5AA
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x0001D3C5 File Offset: 0x0001B5C5
		[XmlIgnore]
		public RulesResponseType SafetyActions
		{
			get
			{
				if (this.internalSafetyActions == null)
				{
					this.internalSafetyActions = new RulesResponseType();
				}
				return this.internalSafetyActions;
			}
			set
			{
				this.internalSafetyActions = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0001D3CE File Offset: 0x0001B5CE
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x0001D3E9 File Offset: 0x0001B5E9
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

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0001D3F2 File Offset: 0x0001B5F2
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x0001D40D File Offset: 0x0001B60D
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

		// Token: 0x040005BC RID: 1468
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x040005BD RID: 1469
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x040005BE RID: 1470
		[XmlElement(Type = typeof(RulesResponseType), ElementName = "SafetyLevelRules", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public RulesResponseType internalSafetyLevelRules;

		// Token: 0x040005BF RID: 1471
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(RulesResponseType), ElementName = "SafetyActions", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public RulesResponseType internalSafetyActions;

		// Token: 0x040005C0 RID: 1472
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Properties), ElementName = "Properties", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public Properties internalProperties;

		// Token: 0x040005C1 RID: 1473
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Lists), ElementName = "Lists", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public Lists internalLists;
	}
}

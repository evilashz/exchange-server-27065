using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000FA RID: 250
	[XmlType(TypeName = "SafetyActions", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class SafetyActions
	{
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0001AE15 File Offset: 0x00019015
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x0001AE30 File Offset: 0x00019030
		[XmlIgnore]
		public SafetyActionsGetVersion GetVersion
		{
			get
			{
				if (this.internalGetVersion == null)
				{
					this.internalGetVersion = new SafetyActionsGetVersion();
				}
				return this.internalGetVersion;
			}
			set
			{
				this.internalGetVersion = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x0001AE39 File Offset: 0x00019039
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x0001AE54 File Offset: 0x00019054
		[XmlIgnore]
		public SafetyActionsGet Get
		{
			get
			{
				if (this.internalGet == null)
				{
					this.internalGet = new SafetyActionsGet();
				}
				return this.internalGet;
			}
			set
			{
				this.internalGet = value;
			}
		}

		// Token: 0x0400042D RID: 1069
		[XmlElement(Type = typeof(SafetyActionsGetVersion), ElementName = "GetVersion", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SafetyActionsGetVersion internalGetVersion;

		// Token: 0x0400042E RID: 1070
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(SafetyActionsGet), ElementName = "Get", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public SafetyActionsGet internalGet;
	}
}

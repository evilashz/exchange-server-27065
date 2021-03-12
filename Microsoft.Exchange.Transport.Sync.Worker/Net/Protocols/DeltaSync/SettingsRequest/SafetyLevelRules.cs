using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000F7 RID: 247
	[XmlType(TypeName = "SafetyLevelRules", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class SafetyLevelRules
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0001ADB5 File Offset: 0x00018FB5
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x0001ADD0 File Offset: 0x00018FD0
		[XmlIgnore]
		public GetVersion GetVersion
		{
			get
			{
				if (this.internalGetVersion == null)
				{
					this.internalGetVersion = new GetVersion();
				}
				return this.internalGetVersion;
			}
			set
			{
				this.internalGetVersion = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0001ADD9 File Offset: 0x00018FD9
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x0001ADF4 File Offset: 0x00018FF4
		[XmlIgnore]
		public Get Get
		{
			get
			{
				if (this.internalGet == null)
				{
					this.internalGet = new Get();
				}
				return this.internalGet;
			}
			set
			{
				this.internalGet = value;
			}
		}

		// Token: 0x0400042B RID: 1067
		[XmlElement(Type = typeof(GetVersion), ElementName = "GetVersion", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public GetVersion internalGetVersion;

		// Token: 0x0400042C RID: 1068
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Get), ElementName = "Get", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public Get internalGet;
	}
}

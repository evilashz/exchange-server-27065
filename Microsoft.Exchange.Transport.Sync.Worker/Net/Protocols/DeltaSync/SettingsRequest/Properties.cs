using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x020000FD RID: 253
	[XmlType(TypeName = "Properties", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Properties
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0001AE75 File Offset: 0x00019075
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x0001AE90 File Offset: 0x00019090
		[XmlIgnore]
		public PropertiesGet Get
		{
			get
			{
				if (this.internalGet == null)
				{
					this.internalGet = new PropertiesGet();
				}
				return this.internalGet;
			}
			set
			{
				this.internalGet = value;
			}
		}

		// Token: 0x0400042F RID: 1071
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(PropertiesGet), ElementName = "Get", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public PropertiesGet internalGet;
	}
}

using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000148 RID: 328
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "RulesResponseType", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class RulesResponseType
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0001C6B6 File Offset: 0x0001A8B6
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x0001C6BE File Offset: 0x0001A8BE
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

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001C6CE File Offset: 0x0001A8CE
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x0001C6E9 File Offset: 0x0001A8E9
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

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001C6F2 File Offset: 0x0001A8F2
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x0001C6FA File Offset: 0x0001A8FA
		[XmlIgnore]
		public string Version
		{
			get
			{
				return this.internalVersion;
			}
			set
			{
				this.internalVersion = value;
			}
		}

		// Token: 0x04000530 RID: 1328
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x04000531 RID: 1329
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x04000532 RID: 1330
		[XmlElement(Type = typeof(Get), ElementName = "Get", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Get internalGet;

		// Token: 0x04000533 RID: 1331
		[XmlElement(ElementName = "Version", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalVersion;
	}
}

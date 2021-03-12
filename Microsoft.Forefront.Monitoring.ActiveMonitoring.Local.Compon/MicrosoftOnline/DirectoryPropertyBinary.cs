using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000177 RID: 375
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To4000))]
	[DesignerCategory("code")]
	[XmlInclude(typeof(DirectoryPropertyBinarySingle))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To102400))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To32000))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To12000))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To8000))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To256))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To195))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To128))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To28))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength8))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public abstract class DirectoryPropertyBinary : DirectoryProperty
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0002082E File Offset: 0x0001EA2E
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x00020836 File Offset: 0x0001EA36
		[XmlElement("Value", DataType = "hexBinary")]
		public byte[][] Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x0400046C RID: 1132
		private byte[][] valueField;
	}
}

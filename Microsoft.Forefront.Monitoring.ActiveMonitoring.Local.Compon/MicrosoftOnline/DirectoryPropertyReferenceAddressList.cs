using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000163 RID: 355
	[XmlInclude(typeof(DirectoryPropertyReferenceAddressListSingle))]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyReferenceAddressList : DirectoryPropertyReference
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00020717 File Offset: 0x0001E917
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x0002071F File Offset: 0x0001E91F
		[XmlElement("Value")]
		public DirectoryReferenceAddressList[] Value
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

		// Token: 0x04000465 RID: 1125
		private DirectoryReferenceAddressList[] valueField;
	}
}

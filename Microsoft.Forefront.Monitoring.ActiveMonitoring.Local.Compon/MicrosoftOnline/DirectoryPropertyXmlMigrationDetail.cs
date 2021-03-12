using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200012F RID: 303
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlMigrationDetail : DirectoryPropertyXml
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00020456 File Offset: 0x0001E656
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x0002045E File Offset: 0x0001E65E
		public XmlValueMigrationDetail Value
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

		// Token: 0x04000454 RID: 1108
		private XmlValueMigrationDetail valueField;
	}
}

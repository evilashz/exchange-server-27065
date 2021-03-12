using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000175 RID: 373
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[XmlInclude(typeof(DirectoryPropertyBooleanSingle))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyBoolean : DirectoryProperty
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0002080D File Offset: 0x0001EA0D
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x00020815 File Offset: 0x0001EA15
		[XmlElement("Value")]
		public bool[] Value
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

		// Token: 0x0400046B RID: 1131
		private bool[] valueField;
	}
}

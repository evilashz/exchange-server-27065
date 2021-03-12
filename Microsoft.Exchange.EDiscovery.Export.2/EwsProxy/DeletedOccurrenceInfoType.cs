using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000145 RID: 325
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class DeletedOccurrenceInfoType
	{
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x000227C4 File Offset: 0x000209C4
		// (set) Token: 0x06000E3A RID: 3642 RVA: 0x000227CC File Offset: 0x000209CC
		public DateTime Start
		{
			get
			{
				return this.startField;
			}
			set
			{
				this.startField = value;
			}
		}

		// Token: 0x040009D1 RID: 2513
		private DateTime startField;
	}
}

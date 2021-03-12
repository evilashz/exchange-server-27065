using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x0200007A RID: 122
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	[DebuggerStepThrough]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[Serializable]
	public class TextMessagingHostingData_locDefinition
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00015B1A File Offset: 0x00013D1A
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x00015B22 File Offset: 0x00013D22
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string _locDefault_loc
		{
			get
			{
				return this._locDefault_locField;
			}
			set
			{
				this._locDefault_locField = value;
			}
		}

		// Token: 0x04000263 RID: 611
		private string _locDefault_locField;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000C9 RID: 201
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class ScopeReferencesScopeReference
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0001F340 File Offset: 0x0001D540
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x0001F348 File Offset: 0x0001D548
		[XmlAttribute]
		public string ScopeId
		{
			get
			{
				return this.scopeIdField;
			}
			set
			{
				this.scopeIdField = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001F351 File Offset: 0x0001D551
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x0001F359 File Offset: 0x0001D559
		[XmlAttribute]
		public bool BuiltIn
		{
			get
			{
				return this.builtInField;
			}
			set
			{
				this.builtInField = value;
			}
		}

		// Token: 0x0400034C RID: 844
		private string scopeIdField;

		// Token: 0x0400034D RID: 845
		private bool builtInField;
	}
}

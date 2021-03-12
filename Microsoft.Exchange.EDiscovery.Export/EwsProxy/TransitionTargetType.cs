using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002FA RID: 762
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class TransitionTargetType
	{
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x000286AD File Offset: 0x000268AD
		// (set) Token: 0x0600197A RID: 6522 RVA: 0x000286B5 File Offset: 0x000268B5
		[XmlAttribute]
		public TransitionTargetKindType Kind
		{
			get
			{
				return this.kindField;
			}
			set
			{
				this.kindField = value;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x000286BE File Offset: 0x000268BE
		// (set) Token: 0x0600197C RID: 6524 RVA: 0x000286C6 File Offset: 0x000268C6
		[XmlText]
		public string Value
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

		// Token: 0x04001131 RID: 4401
		private TransitionTargetKindType kindField;

		// Token: 0x04001132 RID: 4402
		private string valueField;
	}
}

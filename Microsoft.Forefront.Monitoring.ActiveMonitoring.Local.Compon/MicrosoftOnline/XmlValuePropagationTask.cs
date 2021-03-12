using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000EC RID: 236
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValuePropagationTask
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0001FC2F File Offset: 0x0001DE2F
		// (set) Token: 0x06000767 RID: 1895 RVA: 0x0001FC37 File Offset: 0x0001DE37
		public PropagationTaskValue PropagationTask
		{
			get
			{
				return this.propagationTaskField;
			}
			set
			{
				this.propagationTaskField = value;
			}
		}

		// Token: 0x040003CC RID: 972
		private PropagationTaskValue propagationTaskField;
	}
}

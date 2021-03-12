using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000F6 RID: 246
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[Serializable]
	public class DatacenterRedirectionValue
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x0001FD87 File Offset: 0x0001DF87
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x0001FD8F File Offset: 0x0001DF8F
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0001FD98 File Offset: 0x0001DF98
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x0001FDA0 File Offset: 0x0001DFA0
		[XmlAttribute]
		public int Priority
		{
			get
			{
				return this.priorityField;
			}
			set
			{
				this.priorityField = value;
			}
		}

		// Token: 0x040003E2 RID: 994
		private string nameField;

		// Token: 0x040003E3 RID: 995
		private int priorityField;
	}
}

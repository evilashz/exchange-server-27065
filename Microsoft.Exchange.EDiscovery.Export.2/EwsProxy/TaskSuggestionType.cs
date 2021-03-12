using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200017E RID: 382
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class TaskSuggestionType : EntityType
	{
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x00023BC6 File Offset: 0x00021DC6
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x00023BCE File Offset: 0x00021DCE
		public string TaskString
		{
			get
			{
				return this.taskStringField;
			}
			set
			{
				this.taskStringField = value;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x00023BD7 File Offset: 0x00021DD7
		// (set) Token: 0x06001099 RID: 4249 RVA: 0x00023BDF File Offset: 0x00021DDF
		[XmlArrayItem("EmailUser", IsNullable = false)]
		public EmailUserType[] Assignees
		{
			get
			{
				return this.assigneesField;
			}
			set
			{
				this.assigneesField = value;
			}
		}

		// Token: 0x04000B54 RID: 2900
		private string taskStringField;

		// Token: 0x04000B55 RID: 2901
		private EmailUserType[] assigneesField;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000370 RID: 880
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetUMPromptType : BaseRequestType
	{
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x00029C10 File Offset: 0x00027E10
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x00029C18 File Offset: 0x00027E18
		public string ConfigurationObject
		{
			get
			{
				return this.configurationObjectField;
			}
			set
			{
				this.configurationObjectField = value;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x00029C21 File Offset: 0x00027E21
		// (set) Token: 0x06001C06 RID: 7174 RVA: 0x00029C29 File Offset: 0x00027E29
		public string PromptName
		{
			get
			{
				return this.promptNameField;
			}
			set
			{
				this.promptNameField = value;
			}
		}

		// Token: 0x0400129A RID: 4762
		private string configurationObjectField;

		// Token: 0x0400129B RID: 4763
		private string promptNameField;
	}
}

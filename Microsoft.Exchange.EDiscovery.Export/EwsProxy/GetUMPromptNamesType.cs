using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000371 RID: 881
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetUMPromptNamesType : BaseRequestType
	{
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00029C3A File Offset: 0x00027E3A
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x00029C42 File Offset: 0x00027E42
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

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x00029C4B File Offset: 0x00027E4B
		// (set) Token: 0x06001C0B RID: 7179 RVA: 0x00029C53 File Offset: 0x00027E53
		public int HoursElapsedSinceLastModified
		{
			get
			{
				return this.hoursElapsedSinceLastModifiedField;
			}
			set
			{
				this.hoursElapsedSinceLastModifiedField = value;
			}
		}

		// Token: 0x0400129C RID: 4764
		private string configurationObjectField;

		// Token: 0x0400129D RID: 4765
		private int hoursElapsedSinceLastModifiedField;
	}
}

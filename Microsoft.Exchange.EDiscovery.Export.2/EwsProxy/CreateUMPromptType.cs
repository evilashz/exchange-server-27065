using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000372 RID: 882
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CreateUMPromptType : BaseRequestType
	{
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x00029C64 File Offset: 0x00027E64
		// (set) Token: 0x06001C0E RID: 7182 RVA: 0x00029C6C File Offset: 0x00027E6C
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

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x00029C75 File Offset: 0x00027E75
		// (set) Token: 0x06001C10 RID: 7184 RVA: 0x00029C7D File Offset: 0x00027E7D
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

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x00029C86 File Offset: 0x00027E86
		// (set) Token: 0x06001C12 RID: 7186 RVA: 0x00029C8E File Offset: 0x00027E8E
		[XmlElement(DataType = "base64Binary")]
		public byte[] AudioData
		{
			get
			{
				return this.audioDataField;
			}
			set
			{
				this.audioDataField = value;
			}
		}

		// Token: 0x0400129E RID: 4766
		private string configurationObjectField;

		// Token: 0x0400129F RID: 4767
		private string promptNameField;

		// Token: 0x040012A0 RID: 4768
		private byte[] audioDataField;
	}
}

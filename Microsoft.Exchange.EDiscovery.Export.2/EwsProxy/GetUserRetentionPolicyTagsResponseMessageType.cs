using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000CF RID: 207
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetUserRetentionPolicyTagsResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00020407 File Offset: 0x0001E607
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0002040F File Offset: 0x0001E60F
		[XmlArrayItem("RetentionPolicyTag", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public RetentionPolicyTagType[] RetentionPolicyTags
		{
			get
			{
				return this.retentionPolicyTagsField;
			}
			set
			{
				this.retentionPolicyTagsField = value;
			}
		}

		// Token: 0x040005B5 RID: 1461
		private RetentionPolicyTagType[] retentionPolicyTagsField;
	}
}

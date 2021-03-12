using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000891 RID: 2193
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlStringKeyValuePair : DirectoryPropertyXml
	{
		// Token: 0x06006D99 RID: 28057 RVA: 0x00175AB5 File Offset: 0x00173CB5
		public override IList GetValues()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006D9A RID: 28058 RVA: 0x00175ABC File Offset: 0x00173CBC
		public sealed override void SetValues(IList values)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17002711 RID: 10001
		// (get) Token: 0x06006D9B RID: 28059 RVA: 0x00175AC3 File Offset: 0x00173CC3
		// (set) Token: 0x06006D9C RID: 28060 RVA: 0x00175ACB File Offset: 0x00173CCB
		[XmlElement("Value", Order = 0)]
		public XmlValueStringKeyValuePair[] Value
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

		// Token: 0x04004783 RID: 18307
		private XmlValueStringKeyValuePair[] valueField;
	}
}

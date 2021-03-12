using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002BF RID: 703
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SetClientExtensionActionType
	{
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00027AB6 File Offset: 0x00025CB6
		// (set) Token: 0x0600180F RID: 6159 RVA: 0x00027ABE File Offset: 0x00025CBE
		public ClientExtensionType ClientExtension
		{
			get
			{
				return this.clientExtensionField;
			}
			set
			{
				this.clientExtensionField = value;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x00027AC7 File Offset: 0x00025CC7
		// (set) Token: 0x06001811 RID: 6161 RVA: 0x00027ACF File Offset: 0x00025CCF
		[XmlAttribute]
		public SetClientExtensionActionIdType ActionId
		{
			get
			{
				return this.actionIdField;
			}
			set
			{
				this.actionIdField = value;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x00027AD8 File Offset: 0x00025CD8
		// (set) Token: 0x06001813 RID: 6163 RVA: 0x00027AE0 File Offset: 0x00025CE0
		[XmlAttribute]
		public string ExtensionId
		{
			get
			{
				return this.extensionIdField;
			}
			set
			{
				this.extensionIdField = value;
			}
		}

		// Token: 0x04001054 RID: 4180
		private ClientExtensionType clientExtensionField;

		// Token: 0x04001055 RID: 4181
		private SetClientExtensionActionIdType actionIdField;

		// Token: 0x04001056 RID: 4182
		private string extensionIdField;
	}
}

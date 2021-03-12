using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200030F RID: 783
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetClientIntentType : BaseRequestType
	{
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x0002897F File Offset: 0x00026B7F
		// (set) Token: 0x060019D0 RID: 6608 RVA: 0x00028987 File Offset: 0x00026B87
		public string GlobalObjectId
		{
			get
			{
				return this.globalObjectIdField;
			}
			set
			{
				this.globalObjectIdField = value;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x00028990 File Offset: 0x00026B90
		// (set) Token: 0x060019D2 RID: 6610 RVA: 0x00028998 File Offset: 0x00026B98
		public NonEmptyStateDefinitionType StateDefinition
		{
			get
			{
				return this.stateDefinitionField;
			}
			set
			{
				this.stateDefinitionField = value;
			}
		}

		// Token: 0x0400115D RID: 4445
		private string globalObjectIdField;

		// Token: 0x0400115E RID: 4446
		private NonEmptyStateDefinitionType stateDefinitionField;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200031F RID: 799
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SetImListMigrationCompletedType : BaseRequestType
	{
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x00028BD4 File Offset: 0x00026DD4
		// (set) Token: 0x06001A17 RID: 6679 RVA: 0x00028BDC File Offset: 0x00026DDC
		public bool ImListMigrationCompleted
		{
			get
			{
				return this.imListMigrationCompletedField;
			}
			set
			{
				this.imListMigrationCompletedField = value;
			}
		}

		// Token: 0x0400118B RID: 4491
		private bool imListMigrationCompletedField;
	}
}

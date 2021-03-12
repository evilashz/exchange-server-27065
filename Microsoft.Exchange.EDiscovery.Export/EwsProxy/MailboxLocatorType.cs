using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002DB RID: 731
	[XmlInclude(typeof(UserLocatorType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(GroupLocatorType))]
	[DebuggerStepThrough]
	[Serializable]
	public class MailboxLocatorType
	{
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x000280AE File Offset: 0x000262AE
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x000280B6 File Offset: 0x000262B6
		public string ExternalDirectoryObjectId
		{
			get
			{
				return this.externalDirectoryObjectIdField;
			}
			set
			{
				this.externalDirectoryObjectIdField = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x000280BF File Offset: 0x000262BF
		// (set) Token: 0x060018C6 RID: 6342 RVA: 0x000280C7 File Offset: 0x000262C7
		public string LegacyDn
		{
			get
			{
				return this.legacyDnField;
			}
			set
			{
				this.legacyDnField = value;
			}
		}

		// Token: 0x040010C5 RID: 4293
		private string externalDirectoryObjectIdField;

		// Token: 0x040010C6 RID: 4294
		private string legacyDnField;
	}
}

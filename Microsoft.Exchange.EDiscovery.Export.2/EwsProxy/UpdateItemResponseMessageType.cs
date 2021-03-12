using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000266 RID: 614
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class UpdateItemResponseMessageType : ItemInfoResponseMessageType
	{
		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x00027293 File Offset: 0x00025493
		// (set) Token: 0x06001715 RID: 5909 RVA: 0x0002729B File Offset: 0x0002549B
		public ConflictResultsType ConflictResults
		{
			get
			{
				return this.conflictResultsField;
			}
			set
			{
				this.conflictResultsField = value;
			}
		}

		// Token: 0x04000FA3 RID: 4003
		private ConflictResultsType conflictResultsField;
	}
}

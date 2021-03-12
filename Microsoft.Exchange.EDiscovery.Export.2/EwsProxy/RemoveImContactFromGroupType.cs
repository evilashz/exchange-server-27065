using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000328 RID: 808
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class RemoveImContactFromGroupType : BaseRequestType
	{
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x00028CF9 File Offset: 0x00026EF9
		// (set) Token: 0x06001A3A RID: 6714 RVA: 0x00028D01 File Offset: 0x00026F01
		public ItemIdType ContactId
		{
			get
			{
				return this.contactIdField;
			}
			set
			{
				this.contactIdField = value;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x00028D0A File Offset: 0x00026F0A
		// (set) Token: 0x06001A3C RID: 6716 RVA: 0x00028D12 File Offset: 0x00026F12
		public ItemIdType GroupId
		{
			get
			{
				return this.groupIdField;
			}
			set
			{
				this.groupIdField = value;
			}
		}

		// Token: 0x04001198 RID: 4504
		private ItemIdType contactIdField;

		// Token: 0x04001199 RID: 4505
		private ItemIdType groupIdField;
	}
}

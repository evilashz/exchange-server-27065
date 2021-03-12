using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200017B RID: 379
	[DesignerCategory("code")]
	[XmlInclude(typeof(ContactType))]
	[XmlInclude(typeof(MeetingSuggestionType))]
	[XmlInclude(typeof(UrlEntityType))]
	[XmlInclude(typeof(EmailAddressEntityType))]
	[XmlInclude(typeof(PhoneEntityType))]
	[XmlInclude(typeof(AddressEntityType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(TaskSuggestionType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class EntityType
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00023B94 File Offset: 0x00021D94
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x00023B9C File Offset: 0x00021D9C
		[XmlElement("Position")]
		public EmailPositionType[] Position
		{
			get
			{
				return this.positionField;
			}
			set
			{
				this.positionField = value;
			}
		}

		// Token: 0x04000B4D RID: 2893
		private EmailPositionType[] positionField;
	}
}

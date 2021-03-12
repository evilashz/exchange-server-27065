using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200018D RID: 397
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetNonIndexableItemDetailsResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00024077 File Offset: 0x00022277
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x0002407F File Offset: 0x0002227F
		public NonIndexableItemDetailResultType NonIndexableItemDetailsResult
		{
			get
			{
				return this.nonIndexableItemDetailsResultField;
			}
			set
			{
				this.nonIndexableItemDetailsResultField = value;
			}
		}

		// Token: 0x04000BD0 RID: 3024
		private NonIndexableItemDetailResultType nonIndexableItemDetailsResultField;
	}
}

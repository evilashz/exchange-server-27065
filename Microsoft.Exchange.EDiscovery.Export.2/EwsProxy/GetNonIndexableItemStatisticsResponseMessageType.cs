using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000192 RID: 402
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetNonIndexableItemStatisticsResponseMessageType : ResponseMessageType
	{
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x000241B8 File Offset: 0x000223B8
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x000241C0 File Offset: 0x000223C0
		[XmlArrayItem("NonIndexableItemStatistic", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public NonIndexableItemStatisticType[] NonIndexableItemStatistics
		{
			get
			{
				return this.nonIndexableItemStatisticsField;
			}
			set
			{
				this.nonIndexableItemStatisticsField = value;
			}
		}

		// Token: 0x04000BEA RID: 3050
		private NonIndexableItemStatisticType[] nonIndexableItemStatisticsField;
	}
}

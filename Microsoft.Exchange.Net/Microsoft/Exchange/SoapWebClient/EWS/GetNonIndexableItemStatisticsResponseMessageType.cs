﻿using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000273 RID: 627
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetNonIndexableItemStatisticsResponseMessageType : ResponseMessageType
	{
		// Token: 0x0400103C RID: 4156
		[XmlArrayItem("NonIndexableItemStatistic", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public NonIndexableItemStatisticType[] NonIndexableItemStatistics;
	}
}

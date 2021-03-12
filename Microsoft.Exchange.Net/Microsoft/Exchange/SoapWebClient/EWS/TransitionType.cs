﻿using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003DD RID: 989
	[XmlInclude(typeof(RecurringDayTransitionType))]
	[XmlInclude(typeof(RecurringDateTransitionType))]
	[XmlInclude(typeof(AbsoluteDateTransitionType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(RecurringTimeTransitionType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class TransitionType
	{
		// Token: 0x04001588 RID: 5512
		public TransitionTargetType To;
	}
}

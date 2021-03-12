﻿using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002C9 RID: 713
	[DebuggerStepThrough]
	[XmlInclude(typeof(UpdateDelegateResponseMessageType))]
	[XmlInclude(typeof(RemoveDelegateResponseMessageType))]
	[XmlInclude(typeof(AddDelegateResponseMessageType))]
	[XmlInclude(typeof(GetDelegateResponseMessageType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public abstract class BaseDelegateResponseMessageType : ResponseMessageType
	{
		// Token: 0x04001220 RID: 4640
		[XmlArrayItem(IsNullable = false)]
		public DelegateUserResponseMessageType[] ResponseMessages;
	}
}

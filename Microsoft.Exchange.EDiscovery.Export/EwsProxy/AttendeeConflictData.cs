﻿using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000270 RID: 624
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[XmlInclude(typeof(GroupAttendeeConflictData))]
	[XmlInclude(typeof(IndividualAttendeeConflictData))]
	[XmlInclude(typeof(TooBigGroupAttendeeConflictData))]
	[XmlInclude(typeof(UnknownAttendeeConflictData))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public abstract class AttendeeConflictData
	{
	}
}

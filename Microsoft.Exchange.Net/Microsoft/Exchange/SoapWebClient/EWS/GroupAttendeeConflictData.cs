using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000352 RID: 850
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GroupAttendeeConflictData : AttendeeConflictData
	{
		// Token: 0x04001417 RID: 5143
		public int NumberOfMembers;

		// Token: 0x04001418 RID: 5144
		public int NumberOfMembersAvailable;

		// Token: 0x04001419 RID: 5145
		public int NumberOfMembersWithConflict;

		// Token: 0x0400141A RID: 5146
		public int NumberOfMembersWithNoData;
	}
}

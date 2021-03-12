using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000272 RID: 626
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class IndividualAttendeeConflictData : AttendeeConflictData
	{
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x000274E1 File Offset: 0x000256E1
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x000274E9 File Offset: 0x000256E9
		public LegacyFreeBusyType BusyType
		{
			get
			{
				return this.busyTypeField;
			}
			set
			{
				this.busyTypeField = value;
			}
		}

		// Token: 0x04000FC9 RID: 4041
		private LegacyFreeBusyType busyTypeField;
	}
}

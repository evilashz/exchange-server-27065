using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000B8 RID: 184
	[XmlType("AutoPromote")]
	[DataContract(Name = "AutoPromote")]
	[KnownType(typeof(AutoPromote))]
	public class AutoPromote
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000ABA5 File Offset: 0x00008DA5
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000ABAD File Offset: 0x00008DAD
		[XmlAttribute("OrganizerOnly")]
		[DataMember(Name = "OrganizerOnly", EmitDefaultValue = true)]
		public bool OrganizerOnly { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000ABB6 File Offset: 0x00008DB6
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000ABBE File Offset: 0x00008DBE
		[XmlAttribute("Value")]
		[DataMember(Name = "Value", EmitDefaultValue = true)]
		public AutoPromoteEnum Value { get; set; }

		// Token: 0x06000438 RID: 1080 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		internal static AutoPromote ConvertFrom(AutomaticLeaderAssignment leaderAssignment)
		{
			AutoPromote autoPromote = new AutoPromote();
			switch (leaderAssignment)
			{
			case AutomaticLeaderAssignment.Disabled:
				autoPromote.OrganizerOnly = true;
				autoPromote.Value = AutoPromoteEnum.None;
				break;
			case AutomaticLeaderAssignment.SameEnterprise:
				autoPromote.OrganizerOnly = false;
				autoPromote.Value = AutoPromoteEnum.Company;
				break;
			case AutomaticLeaderAssignment.Everyone:
				autoPromote.OrganizerOnly = false;
				autoPromote.Value = AutoPromoteEnum.Everyone;
				break;
			default:
				throw new InvalidEnumArgumentException("Invalid value for AutomaticLeaderAssignment");
			}
			return autoPromote;
		}
	}
}

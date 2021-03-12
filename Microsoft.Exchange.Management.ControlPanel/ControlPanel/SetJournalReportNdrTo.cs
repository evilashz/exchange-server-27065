using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200040B RID: 1035
	[DataContract]
	public class SetJournalReportNdrTo : SetObjectProperties
	{
		// Token: 0x170020C4 RID: 8388
		// (get) Token: 0x060034E9 RID: 13545 RVA: 0x000A5043 File Offset: 0x000A3243
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-TransportConfig";
			}
		}

		// Token: 0x170020C5 RID: 8389
		// (get) Token: 0x060034EA RID: 13546 RVA: 0x000A504A File Offset: 0x000A324A
		public override string RbacScope
		{
			get
			{
				return "@C:OrganizationConfig";
			}
		}

		// Token: 0x170020C6 RID: 8390
		// (get) Token: 0x060034EB RID: 13547 RVA: 0x000A5054 File Offset: 0x000A3254
		// (set) Token: 0x060034EC RID: 13548 RVA: 0x000A5090 File Offset: 0x000A3290
		[DataMember]
		public string JournalingReportNdrTo
		{
			get
			{
				string text = (string)base["JournalingReportNdrTo"];
				if (string.IsNullOrEmpty(text))
				{
					text = SmtpAddress.NullReversePath.ToString();
				}
				return text;
			}
			set
			{
				string value2 = value;
				if (string.IsNullOrEmpty(value2))
				{
					value2 = SmtpAddress.NullReversePath.ToString();
				}
				base["JournalingReportNdrTo"] = value2;
			}
		}
	}
}

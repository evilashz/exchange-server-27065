using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200006E RID: 110
	[DataContract]
	public class CalendarDiagnosticLog : WebServiceParameters
	{
		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x000563A6 File Offset: 0x000545A6
		// (set) Token: 0x06001B01 RID: 6913 RVA: 0x000563B8 File Offset: 0x000545B8
		[DataMember]
		public string Subject
		{
			get
			{
				return (string)base["Subject"];
			}
			set
			{
				base["Subject"] = value;
			}
		}

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x000563C6 File Offset: 0x000545C6
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-CalendarDiagnosticLog";
			}
		}

		// Token: 0x17001879 RID: 6265
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x000563CD File Offset: 0x000545CD
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}

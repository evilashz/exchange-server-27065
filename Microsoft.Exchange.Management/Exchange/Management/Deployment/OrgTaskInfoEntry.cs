using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200018B RID: 395
	[ClassAccessLevel(AccessLevel.Consumer)]
	public class OrgTaskInfoEntry : TaskInfoEntry
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x00041C96 File Offset: 0x0003FE96
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x00041C9E File Offset: 0x0003FE9E
		[XmlAttribute]
		public bool UseGlobalTask
		{
			get
			{
				return base.UsePrimaryTask;
			}
			set
			{
				base.UsePrimaryTask = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00041CA7 File Offset: 0x0003FEA7
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x00041CAF File Offset: 0x0003FEAF
		[XmlAttribute]
		public bool UseForReconciliation { get; set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00041CB8 File Offset: 0x0003FEB8
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x00041CC0 File Offset: 0x0003FEC0
		[XmlAttribute]
		public bool RecipientOperation { get; set; }
	}
}

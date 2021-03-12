using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000188 RID: 392
	[ClassAccessLevel(AccessLevel.Consumer)]
	public class ServerTaskInfoEntry : TaskInfoEntry
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00041BAC File Offset: 0x0003FDAC
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x00041BB4 File Offset: 0x0003FDB4
		[XmlAttribute]
		public bool UseStandaloneTask
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
	}
}

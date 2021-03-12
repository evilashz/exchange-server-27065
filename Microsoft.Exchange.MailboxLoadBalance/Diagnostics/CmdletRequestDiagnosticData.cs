using System;
using System.Management.Automation;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000056 RID: 86
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CmdletRequestDiagnosticData : RequestDiagnosticData
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000097FF File Offset: 0x000079FF
		// (set) Token: 0x060002FD RID: 765 RVA: 0x00009807 File Offset: 0x00007A07
		[DataMember]
		public ErrorRecord ErrorRecord { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00009810 File Offset: 0x00007A10
		// (set) Token: 0x060002FF RID: 767 RVA: 0x00009818 File Offset: 0x00007A18
		[DataMember]
		public PowershellCommandDiagnosticData[] Command { get; set; }
	}
}

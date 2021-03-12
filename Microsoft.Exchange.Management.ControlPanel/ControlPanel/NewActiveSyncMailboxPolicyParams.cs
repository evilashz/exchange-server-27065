using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002F9 RID: 761
	[DataContract]
	public class NewActiveSyncMailboxPolicyParams : BaseActiveSyncMailboxPolicyParams
	{
		// Token: 0x17001E85 RID: 7813
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x0008C387 File Offset: 0x0008A587
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-MobileMailboxPolicy";
			}
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x0008C38E File Offset: 0x0008A58E
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.ProcessPolicyParams(null);
		}
	}
}

using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000230 RID: 560
	[DataContract]
	public class MemberOfGroupFilter : GroupFilter
	{
		// Token: 0x17001C29 RID: 7209
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x0007D0F0 File Offset: 0x0007B2F0
		protected override string AdditionalFilterText
		{
			get
			{
				string text = (RbacPrincipal.Current.ExecutingUserId != null) ? RbacPrincipal.Current.ExecutingUserId.DistinguishedName : string.Empty;
				return string.Format("Members -eq '{0}'", text.Replace("'", "''"));
			}
		}

		// Token: 0x17001C2A RID: 7210
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x0007D13A File Offset: 0x0007B33A
		public override string RbacScope
		{
			get
			{
				return "@R:MyGAL";
			}
		}
	}
}

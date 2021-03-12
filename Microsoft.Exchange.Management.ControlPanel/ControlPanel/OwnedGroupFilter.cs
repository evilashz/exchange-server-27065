using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000233 RID: 563
	[DataContract]
	public class OwnedGroupFilter : GroupFilter
	{
		// Token: 0x17001C2B RID: 7211
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x0007D1C8 File Offset: 0x0007B3C8
		protected override string AdditionalFilterText
		{
			get
			{
				string text = (RbacPrincipal.Current.ExecutingUserId != null) ? RbacPrincipal.Current.ExecutingUserId.DistinguishedName : string.Empty;
				return string.Format("ManagedBy -eq '{0}'", text.Replace("'", "''"));
			}
		}

		// Token: 0x17001C2C RID: 7212
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x0007D212 File Offset: 0x0007B412
		public override string RbacScope
		{
			get
			{
				return "@R:MyGAL";
			}
		}
	}
}

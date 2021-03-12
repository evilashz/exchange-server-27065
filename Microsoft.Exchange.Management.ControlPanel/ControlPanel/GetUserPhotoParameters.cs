using System;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002A9 RID: 681
	[DataContract]
	public class GetUserPhotoParameters : WebServiceParameters
	{
		// Token: 0x17001D85 RID: 7557
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x00088258 File Offset: 0x00086458
		// (set) Token: 0x06002BAA RID: 11178 RVA: 0x00088281 File Offset: 0x00086481
		public SwitchParameter Preview
		{
			get
			{
				object obj = base["Preview"];
				if (obj != null)
				{
					return (SwitchParameter)obj;
				}
				return new SwitchParameter(false);
			}
			set
			{
				base["Preview"] = value;
			}
		}

		// Token: 0x17001D86 RID: 7558
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x00088294 File Offset: 0x00086494
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-UserPhoto";
			}
		}

		// Token: 0x17001D87 RID: 7559
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x0008829B File Offset: 0x0008649B
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}

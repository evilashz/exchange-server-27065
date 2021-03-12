using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000139 RID: 313
	public class EacEnvironment : IEacEnvironment
	{
		// Token: 0x060020F4 RID: 8436 RVA: 0x00063FA0 File Offset: 0x000621A0
		private EacEnvironment()
		{
		}

		// Token: 0x17001A47 RID: 6727
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x00063FA8 File Offset: 0x000621A8
		// (set) Token: 0x060020F6 RID: 8438 RVA: 0x00063FAF File Offset: 0x000621AF
		public static IEacEnvironment Instance { get; internal set; } = new EacEnvironment();

		// Token: 0x17001A48 RID: 6728
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x00063FB7 File Offset: 0x000621B7
		public bool IsForefrontForOffice
		{
			get
			{
				return DatacenterRegistry.IsForefrontForOffice();
			}
		}

		// Token: 0x17001A49 RID: 6729
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x00063FBE File Offset: 0x000621BE
		public bool IsDataCenter
		{
			get
			{
				return Util.IsDataCenter;
			}
		}
	}
}

using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200022A RID: 554
	[Cmdlet("remove", "umlanguagepack", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class RemoveUmLanguagePack : ManageUmLanguagePack
	{
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x000527FC File Offset: 0x000509FC
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x00052813 File Offset: 0x00050A13
		[Parameter(Mandatory = true)]
		public Guid ProductCode
		{
			get
			{
				return (Guid)base.Fields["ProductCode"];
			}
			set
			{
				base.Fields["ProductCode"] = value;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0005282B File Offset: 0x00050A2B
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x00052842 File Offset: 0x00050A42
		[Parameter(Mandatory = false)]
		public Guid TeleProductCode
		{
			get
			{
				return (Guid)base.Fields["TeleProductCode"];
			}
			set
			{
				base.Fields["TeleProductCode"] = value;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0005285A File Offset: 0x00050A5A
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x00052871 File Offset: 0x00050A71
		[Parameter(Mandatory = false)]
		public Guid TransProductCode
		{
			get
			{
				return (Guid)base.Fields["TransProductCode"];
			}
			set
			{
				base.Fields["TransProductCode"] = value;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00052889 File Offset: 0x00050A89
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x000528A0 File Offset: 0x00050AA0
		[Parameter(Mandatory = false)]
		public Guid TtsProductCode
		{
			get
			{
				return (Guid)base.Fields["TtsProductCode"];
			}
			set
			{
				base.Fields["TtsProductCode"] = value;
			}
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x000528B8 File Offset: 0x00050AB8
		public RemoveUmLanguagePack()
		{
			base.Fields["InstallationMode"] = InstallationModes.Uninstall;
		}
	}
}

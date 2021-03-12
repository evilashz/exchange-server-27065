using System;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200049C RID: 1180
	[DataContract]
	public class MobileDeviceFilter : SelfMailboxParameters
	{
		// Token: 0x06003AC9 RID: 15049 RVA: 0x000B2118 File Offset: 0x000B0318
		public MobileDeviceFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x000B213A File Offset: 0x000B033A
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			base["ShowRecoveryPassword"] = new SwitchParameter(true);
			base["ActiveSync"] = new SwitchParameter(true);
		}

		// Token: 0x1700233E RID: 9022
		// (get) Token: 0x06003ACB RID: 15051 RVA: 0x000B2168 File Offset: 0x000B0368
		// (set) Token: 0x06003ACC RID: 15052 RVA: 0x000B217A File Offset: 0x000B037A
		[DataMember]
		public Identity Mailbox
		{
			get
			{
				return (Identity)base["Mailbox"];
			}
			set
			{
				base["Mailbox"] = value;
			}
		}

		// Token: 0x1700233F RID: 9023
		// (get) Token: 0x06003ACD RID: 15053 RVA: 0x000B2188 File Offset: 0x000B0388
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-MobileDeviceStatistics";
			}
		}

		// Token: 0x17002340 RID: 9024
		// (get) Token: 0x06003ACE RID: 15054 RVA: 0x000B218F File Offset: 0x000B038F
		public override string RbacScope
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x04002724 RID: 10020
		public new const string RbacParameters = "?Mailbox&ShowRecoveryPassword&ActiveSync";

		// Token: 0x04002725 RID: 10021
		public new const string RbacParametersWithIdentity = "?Mailbox&ShowRecoveryPassword&ActiveSync&Identity";
	}
}

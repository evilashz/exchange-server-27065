using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004CA RID: 1226
	[DataContract]
	public class NewUMMailboxParameters : UMBasePinSetParameteres
	{
		// Token: 0x170023B4 RID: 9140
		// (get) Token: 0x06003C18 RID: 15384 RVA: 0x000B4E2A File Offset: 0x000B302A
		public override string AssociatedCmdlet
		{
			get
			{
				return "Enable-UMMailbox";
			}
		}

		// Token: 0x170023B5 RID: 9141
		// (get) Token: 0x06003C19 RID: 15385 RVA: 0x000B4E31 File Offset: 0x000B3031
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x170023B6 RID: 9142
		// (get) Token: 0x06003C1A RID: 15386 RVA: 0x000B4E38 File Offset: 0x000B3038
		// (set) Token: 0x06003C1B RID: 15387 RVA: 0x000B4E4A File Offset: 0x000B304A
		[DataMember]
		public Identity Identity
		{
			get
			{
				return (Identity)base["Identity"];
			}
			set
			{
				base["Identity"] = value;
			}
		}

		// Token: 0x170023B7 RID: 9143
		// (get) Token: 0x06003C1C RID: 15388 RVA: 0x000B4E58 File Offset: 0x000B3058
		// (set) Token: 0x06003C1D RID: 15389 RVA: 0x000B4E6A File Offset: 0x000B306A
		[DataMember]
		public Identity UMMailboxPolicy
		{
			get
			{
				return (Identity)base["UMMailboxPolicy"];
			}
			set
			{
				base["UMMailboxPolicy"] = value;
			}
		}

		// Token: 0x170023B8 RID: 9144
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x000B4E78 File Offset: 0x000B3078
		// (set) Token: 0x06003C1F RID: 15391 RVA: 0x000B4E8A File Offset: 0x000B308A
		[DataMember]
		public string Extension
		{
			get
			{
				return (string)base["Extensions"];
			}
			set
			{
				base["Extensions"] = value;
			}
		}

		// Token: 0x170023B9 RID: 9145
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x000B4E98 File Offset: 0x000B3098
		// (set) Token: 0x06003C21 RID: 15393 RVA: 0x000B4EA0 File Offset: 0x000B30A0
		[DataMember]
		public string SipResourceIdentifier { get; set; }

		// Token: 0x170023BA RID: 9146
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x000B4EA9 File Offset: 0x000B30A9
		// (set) Token: 0x06003C23 RID: 15395 RVA: 0x000B4EB1 File Offset: 0x000B30B1
		[DataMember]
		public string E164ResourceIdentifier { get; set; }

		// Token: 0x06003C24 RID: 15396 RVA: 0x000B4EBA File Offset: 0x000B30BA
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (!string.IsNullOrEmpty(this.SipResourceIdentifier))
			{
				base["SipResourceIdentifier"] = this.SipResourceIdentifier;
				return;
			}
			if (!string.IsNullOrEmpty(this.E164ResourceIdentifier))
			{
				base["SipResourceIdentifier"] = this.E164ResourceIdentifier;
			}
		}
	}
}

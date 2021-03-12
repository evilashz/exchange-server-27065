using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200004A RID: 74
	internal class MemberProvisioningData : ProvisioningData
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000BF98 File Offset: 0x0000A198
		internal MemberProvisioningData()
		{
			base.Action = ProvisioningAction.UpdateExisting;
			base.ProvisioningType = ProvisioningType.GroupMember;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000BFAE File Offset: 0x0000A1AE
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
		public string[] Members
		{
			get
			{
				return (string[])base[ADGroupSchema.Members];
			}
			set
			{
				base[ADGroupSchema.Members] = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000BFCE File Offset: 0x0000A1CE
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
		public string[] GrantSendOnBehalfTo
		{
			get
			{
				return (string[])base[ADRecipientSchema.GrantSendOnBehalfTo];
			}
			set
			{
				base[ADRecipientSchema.GrantSendOnBehalfTo] = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000BFEE File Offset: 0x0000A1EE
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000C000 File Offset: 0x0000A200
		public string ManagedBy
		{
			get
			{
				return (string)base[ADGroupSchema.ManagedBy];
			}
			set
			{
				base[ADGroupSchema.ManagedBy] = value;
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000C010 File Offset: 0x0000A210
		public static MemberProvisioningData Create(string groupId)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(groupId, "groupName");
			return new MemberProvisioningData
			{
				Identity = groupId
			};
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000C038 File Offset: 0x0000A238
		public bool IsEmpty()
		{
			return string.IsNullOrEmpty(base.Identity) || ((this.Members == null || this.Members.Length <= 0) && (this.GrantSendOnBehalfTo == null || this.GrantSendOnBehalfTo.Length <= 0) && (this.ManagedBy == null || this.ManagedBy.Length <= 0));
		}
	}
}

using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000041 RID: 65
	internal class GroupProvisioningData : RecipientProvisioningData
	{
		// Token: 0x060002CB RID: 715 RVA: 0x0000B3F4 File Offset: 0x000095F4
		internal GroupProvisioningData()
		{
			base.Action = ProvisioningAction.CreateNew;
			base.ProvisioningType = ProvisioningType.Group;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000B40A File Offset: 0x0000960A
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000B41C File Offset: 0x0000961C
		public string[] ManagedBy
		{
			get
			{
				return (string[])base[ADGroupSchema.ManagedBy];
			}
			set
			{
				base[ADGroupSchema.ManagedBy] = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000B42A File Offset: 0x0000962A
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000B43C File Offset: 0x0000963C
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000B44A File Offset: 0x0000964A
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000B45C File Offset: 0x0000965C
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

		// Token: 0x060002D2 RID: 722 RVA: 0x0000B46C File Offset: 0x0000966C
		public static GroupProvisioningData Create(string name)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(name, "name");
			return new GroupProvisioningData
			{
				Name = name
			};
		}
	}
}

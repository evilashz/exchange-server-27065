using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000023 RID: 35
	[Cmdlet("Get", "Group", DefaultParameterSetName = "Identity")]
	public sealed class GetGroup : GetRecipientBase<GroupIdParameter, ADGroup>
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000948C File Offset: 0x0000768C
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetGroup.SortPropertiesArray;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00009493 File Offset: 0x00007693
		protected override RecipientType[] RecipientTypes
		{
			get
			{
				return GetGroup.RecipientTypesArray;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000949A File Offset: 0x0000769A
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return this.RecipientTypeDetails;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000094A2 File Offset: 0x000076A2
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<WindowsGroupSchema>();
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000094A9 File Offset: 0x000076A9
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000094C0 File Offset: 0x000076C0
		[ValidateNotNullOrEmpty]
		[Parameter]
		public RecipientTypeDetails[] RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails[])base.Fields["RecipientTypeDetails"];
			}
			set
			{
				base.VerifyValues<RecipientTypeDetails>(GetGroup.AllowedRecipientTypeDetails, value);
				base.Fields["RecipientTypeDetails"] = value;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000094DF File Offset: 0x000076DF
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return WindowsGroup.FromDataObject((ADGroup)dataObject);
		}

		// Token: 0x0400003F RID: 63
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.MailNonUniversalGroup,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.MailUniversalDistributionGroup,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.MailUniversalSecurityGroup,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.NonUniversalGroup,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.UniversalDistributionGroup,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.UniversalSecurityGroup,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.RoomList
		};

		// Token: 0x04000040 RID: 64
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			ADRecipientSchema.DisplayName
		};

		// Token: 0x04000041 RID: 65
		private static readonly RecipientType[] RecipientTypesArray = new RecipientType[]
		{
			RecipientType.Group,
			RecipientType.MailUniversalDistributionGroup,
			RecipientType.MailUniversalSecurityGroup,
			RecipientType.MailNonUniversalGroup
		};
	}
}

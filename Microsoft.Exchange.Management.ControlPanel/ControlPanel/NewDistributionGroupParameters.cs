using System;
using System.Management.Automation;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004F1 RID: 1265
	[DataContract]
	public class NewDistributionGroupParameters : WebServiceParameters
	{
		// Token: 0x1700240E RID: 9230
		// (get) Token: 0x06003D35 RID: 15669 RVA: 0x000B839C File Offset: 0x000B659C
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-DistributionGroup";
			}
		}

		// Token: 0x1700240F RID: 9231
		// (get) Token: 0x06003D36 RID: 15670 RVA: 0x000B83A3 File Offset: 0x000B65A3
		public override string RbacScope
		{
			get
			{
				return "@W:MyDistributionGroups|Organization";
			}
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x000B83AC File Offset: 0x000B65AC
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (RbacPrincipal.Current.IsInRole("MultiTenant") && (this.PrimaryEAAlias != null || this.DomainName != null))
			{
				this.PrimaryEAAlias.FaultIfNullOrEmpty(Strings.AliasForDataCenterNotSetError);
				this.DomainName.FaultIfNullOrEmpty(Strings.GroupEmailDomainNameNotSetError);
				this.PrimarySmtpAddress = this.PrimaryEAAlias + "@" + this.DomainName;
			}
		}

		// Token: 0x17002410 RID: 9232
		// (get) Token: 0x06003D38 RID: 15672 RVA: 0x000B8420 File Offset: 0x000B6620
		// (set) Token: 0x06003D39 RID: 15673 RVA: 0x000B8432 File Offset: 0x000B6632
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base[ADObjectSchema.Name];
			}
			set
			{
				base[ADObjectSchema.Name] = value;
			}
		}

		// Token: 0x17002411 RID: 9233
		// (get) Token: 0x06003D3A RID: 15674 RVA: 0x000B8440 File Offset: 0x000B6640
		// (set) Token: 0x06003D3B RID: 15675 RVA: 0x000B8452 File Offset: 0x000B6652
		[DataMember]
		public string Alias
		{
			get
			{
				return (string)base[MailEnabledRecipientSchema.Alias];
			}
			set
			{
				base[MailEnabledRecipientSchema.Alias] = value;
			}
		}

		// Token: 0x17002412 RID: 9234
		// (get) Token: 0x06003D3C RID: 15676 RVA: 0x000B8460 File Offset: 0x000B6660
		// (set) Token: 0x06003D3D RID: 15677 RVA: 0x000B8477 File Offset: 0x000B6677
		[DataMember]
		public Identity OrganizationalUnit
		{
			get
			{
				return Identity.ParseIdentity((string)base[MailEnabledRecipientSchema.OrganizationalUnit]);
			}
			set
			{
				base[MailEnabledRecipientSchema.OrganizationalUnit] = value.ToIdParameter();
			}
		}

		// Token: 0x17002413 RID: 9235
		// (get) Token: 0x06003D3E RID: 15678 RVA: 0x000B848A File Offset: 0x000B668A
		// (set) Token: 0x06003D3F RID: 15679 RVA: 0x000B8492 File Offset: 0x000B6692
		[DataMember]
		public string PrimaryEAAlias { get; set; }

		// Token: 0x17002414 RID: 9236
		// (get) Token: 0x06003D40 RID: 15680 RVA: 0x000B849B File Offset: 0x000B669B
		// (set) Token: 0x06003D41 RID: 15681 RVA: 0x000B84A3 File Offset: 0x000B66A3
		[DataMember]
		public string DomainName { get; set; }

		// Token: 0x17002415 RID: 9237
		// (get) Token: 0x06003D42 RID: 15682 RVA: 0x000B84AC File Offset: 0x000B66AC
		// (set) Token: 0x06003D43 RID: 15683 RVA: 0x000B84BE File Offset: 0x000B66BE
		public string PrimarySmtpAddress
		{
			get
			{
				return (string)base[MailEnabledRecipientSchema.PrimarySmtpAddress];
			}
			set
			{
				base[MailEnabledRecipientSchema.PrimarySmtpAddress] = value;
			}
		}

		// Token: 0x17002416 RID: 9238
		// (get) Token: 0x06003D44 RID: 15684 RVA: 0x000B84CC File Offset: 0x000B66CC
		// (set) Token: 0x06003D45 RID: 15685 RVA: 0x000B8500 File Offset: 0x000B6700
		public bool IgnoreNamingPolicy
		{
			get
			{
				return base.ParameterIsSpecified("IgnoreNamingPolicy") && ((SwitchParameter)base["IgnoreNamingPolicy"]).ToBool();
			}
			set
			{
				base["IgnoreNamingPolicy"] = new SwitchParameter(value);
			}
		}

		// Token: 0x17002417 RID: 9239
		// (get) Token: 0x06003D46 RID: 15686 RVA: 0x000B8518 File Offset: 0x000B6718
		// (set) Token: 0x06003D47 RID: 15687 RVA: 0x000B852A File Offset: 0x000B672A
		[DataMember]
		public bool IsSecurityGroupMemberJoinApprovalRequired
		{
			get
			{
				return this.MemberJoinRestriction == "ApprovalRequired";
			}
			set
			{
				this.MemberJoinRestriction = (value ? "ApprovalRequired" : "Closed");
			}
		}

		// Token: 0x17002418 RID: 9240
		// (get) Token: 0x06003D48 RID: 15688 RVA: 0x000B8541 File Offset: 0x000B6741
		// (set) Token: 0x06003D49 RID: 15689 RVA: 0x000B8553 File Offset: 0x000B6753
		[DataMember]
		public string MemberJoinRestriction
		{
			get
			{
				return (string)base[DistributionGroupSchema.MemberJoinRestriction];
			}
			set
			{
				base[DistributionGroupSchema.MemberJoinRestriction] = value;
			}
		}

		// Token: 0x17002419 RID: 9241
		// (get) Token: 0x06003D4A RID: 15690 RVA: 0x000B8561 File Offset: 0x000B6761
		// (set) Token: 0x06003D4B RID: 15691 RVA: 0x000B8573 File Offset: 0x000B6773
		[DataMember]
		public string MemberDepartRestriction
		{
			get
			{
				return (string)base[DistributionGroupSchema.MemberDepartRestriction];
			}
			set
			{
				base[DistributionGroupSchema.MemberDepartRestriction] = value;
			}
		}

		// Token: 0x1700241A RID: 9242
		// (get) Token: 0x06003D4C RID: 15692 RVA: 0x000B8581 File Offset: 0x000B6781
		// (set) Token: 0x06003D4D RID: 15693 RVA: 0x000B8593 File Offset: 0x000B6793
		[DataMember]
		public Identity[] ManagedBy
		{
			get
			{
				return Identity.FromIdParameters(base[DistributionGroupSchema.ManagedBy]);
			}
			set
			{
				base[DistributionGroupSchema.ManagedBy] = value.ToIdParameters();
			}
		}

		// Token: 0x1700241B RID: 9243
		// (get) Token: 0x06003D4E RID: 15694 RVA: 0x000B85A6 File Offset: 0x000B67A6
		// (set) Token: 0x06003D4F RID: 15695 RVA: 0x000B85C2 File Offset: 0x000B67C2
		[DataMember]
		public bool CopyOwnerToMember
		{
			get
			{
				return (bool)(base["CopyOwnerToMember"] ?? false);
			}
			set
			{
				base["CopyOwnerToMember"] = value;
			}
		}

		// Token: 0x1700241C RID: 9244
		// (get) Token: 0x06003D50 RID: 15696 RVA: 0x000B85D5 File Offset: 0x000B67D5
		// (set) Token: 0x06003D51 RID: 15697 RVA: 0x000B85F8 File Offset: 0x000B67F8
		[DataMember]
		public bool IsSecurityGroupType
		{
			get
			{
				return (GroupType)(base["Type"] ?? GroupType.Distribution) == GroupType.Security;
			}
			set
			{
				if (value)
				{
					base["Type"] = GroupType.Security;
				}
			}
		}

		// Token: 0x1700241D RID: 9245
		// (get) Token: 0x06003D52 RID: 15698 RVA: 0x000B8612 File Offset: 0x000B6812
		// (set) Token: 0x06003D53 RID: 15699 RVA: 0x000B8624 File Offset: 0x000B6824
		[DataMember]
		public string Notes
		{
			get
			{
				return (string)base[WindowsGroupSchema.Notes];
			}
			set
			{
				base[WindowsGroupSchema.Notes] = value;
			}
		}

		// Token: 0x1700241E RID: 9246
		// (get) Token: 0x06003D54 RID: 15700 RVA: 0x000B8632 File Offset: 0x000B6832
		// (set) Token: 0x06003D55 RID: 15701 RVA: 0x000B8644 File Offset: 0x000B6844
		[DataMember]
		public Identity[] Members
		{
			get
			{
				return Identity.FromIdParameters(base[WindowsGroupSchema.Members]);
			}
			set
			{
				base[WindowsGroupSchema.Members] = value.ToIdParameters();
			}
		}

		// Token: 0x040027EC RID: 10220
		public const string RbacParameters_Enterprise = "?Name&Alias";

		// Token: 0x040027ED RID: 10221
		public const string RbacParameters_MultiTenant = "?Name&Alias&PrimarySmtpAddress";
	}
}

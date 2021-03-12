using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.RbacTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004DC RID: 1244
	[DataContract]
	public class NewAdminRoleGroupParameter : BaseRoleGroupParameters
	{
		// Token: 0x170023F3 RID: 9203
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x000B647E File Offset: 0x000B467E
		// (set) Token: 0x06003CC2 RID: 15554 RVA: 0x000B6490 File Offset: 0x000B4690
		[DataMember]
		public Identity[] Members
		{
			get
			{
				return (Identity[])base[ADGroupSchema.Members];
			}
			set
			{
				base[ADGroupSchema.Members] = value.ToIdParameters();
			}
		}

		// Token: 0x170023F4 RID: 9204
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x000B64A3 File Offset: 0x000B46A3
		// (set) Token: 0x06003CC4 RID: 15556 RVA: 0x000B64B5 File Offset: 0x000B46B5
		[DataMember]
		public Identity[] Roles
		{
			get
			{
				return (Identity[])base[ADGroupSchema.Roles];
			}
			set
			{
				base[ADGroupSchema.Roles] = value.ToIdParameters();
			}
		}

		// Token: 0x170023F5 RID: 9205
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x000B64C8 File Offset: 0x000B46C8
		// (set) Token: 0x06003CC6 RID: 15558 RVA: 0x000B64DA File Offset: 0x000B46DA
		[DataMember]
		internal Identity RecipientWriteScope
		{
			get
			{
				return (Identity)base[RbacCommonParameters.ParameterCustomRecipientWriteScope];
			}
			set
			{
				base[RbacCommonParameters.ParameterCustomRecipientWriteScope] = value.ToIdParameter();
			}
		}

		// Token: 0x170023F6 RID: 9206
		// (get) Token: 0x06003CC7 RID: 15559 RVA: 0x000B64ED File Offset: 0x000B46ED
		// (set) Token: 0x06003CC8 RID: 15560 RVA: 0x000B64FF File Offset: 0x000B46FF
		[DataMember]
		internal Identity ConfigWriteScope
		{
			get
			{
				return (Identity)base[RbacCommonParameters.ParameterCustomConfigWriteScope];
			}
			set
			{
				base[RbacCommonParameters.ParameterCustomConfigWriteScope] = value.ToIdParameter();
			}
		}

		// Token: 0x170023F7 RID: 9207
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000B6512 File Offset: 0x000B4712
		// (set) Token: 0x06003CCA RID: 15562 RVA: 0x000B6524 File Offset: 0x000B4724
		[DataMember]
		internal Identity RecipientOrganizationalUnitScope
		{
			get
			{
				return (Identity)base[RbacCommonParameters.ParameterRecipientOrganizationalUnitScope];
			}
			set
			{
				base[RbacCommonParameters.ParameterRecipientOrganizationalUnitScope] = value.ToIdParameter();
			}
		}

		// Token: 0x170023F8 RID: 9208
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x000B6537 File Offset: 0x000B4737
		public bool IsRolesModified
		{
			get
			{
				return this.Roles != null;
			}
		}

		// Token: 0x170023F9 RID: 9209
		// (get) Token: 0x06003CCC RID: 15564 RVA: 0x000B6545 File Offset: 0x000B4745
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-RoleGroup";
			}
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x000B654C File Offset: 0x000B474C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.Roles != null && this.Roles.Length < 1)
			{
				base[ADGroupSchema.Roles] = null;
			}
			if (this.Members != null && this.Members.Length < 1)
			{
				base[ADGroupSchema.Members] = null;
			}
		}
	}
}

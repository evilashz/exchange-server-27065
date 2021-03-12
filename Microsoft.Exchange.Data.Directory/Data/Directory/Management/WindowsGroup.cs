using System;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200076A RID: 1898
	[Serializable]
	public class WindowsGroup : ADPresentationObject
	{
		// Token: 0x1700209F RID: 8351
		// (get) Token: 0x06005D33 RID: 23859 RVA: 0x00142123 File Offset: 0x00140323
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return WindowsGroup.schema;
			}
		}

		// Token: 0x170020A0 RID: 8352
		// (get) Token: 0x06005D34 RID: 23860 RVA: 0x0014212A File Offset: 0x0014032A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x00142131 File Offset: 0x00140331
		public WindowsGroup()
		{
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x00142139 File Offset: 0x00140339
		public WindowsGroup(ADGroup dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x00142142 File Offset: 0x00140342
		internal static WindowsGroup FromDataObject(ADGroup dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new WindowsGroup(dataObject);
		}

		// Token: 0x170020A1 RID: 8353
		// (get) Token: 0x06005D38 RID: 23864 RVA: 0x0014214F File Offset: 0x0014034F
		// (set) Token: 0x06005D39 RID: 23865 RVA: 0x00142161 File Offset: 0x00140361
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return (string)this[WindowsGroupSchema.DisplayName];
			}
			set
			{
				this[WindowsGroupSchema.DisplayName] = value;
			}
		}

		// Token: 0x170020A2 RID: 8354
		// (get) Token: 0x06005D3A RID: 23866 RVA: 0x0014216F File Offset: 0x0014036F
		public GroupTypeFlags GroupType
		{
			get
			{
				return (GroupTypeFlags)this[WindowsGroupSchema.GroupType];
			}
		}

		// Token: 0x170020A3 RID: 8355
		// (get) Token: 0x06005D3B RID: 23867 RVA: 0x00142181 File Offset: 0x00140381
		// (set) Token: 0x06005D3C RID: 23868 RVA: 0x00142193 File Offset: 0x00140393
		public MultiValuedProperty<ADObjectId> ManagedBy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[WindowsGroupSchema.ManagedBy];
			}
			set
			{
				this[WindowsGroupSchema.ManagedBy] = value;
			}
		}

		// Token: 0x170020A4 RID: 8356
		// (get) Token: 0x06005D3D RID: 23869 RVA: 0x001421A1 File Offset: 0x001403A1
		// (set) Token: 0x06005D3E RID: 23870 RVA: 0x001421B3 File Offset: 0x001403B3
		public string SamAccountName
		{
			get
			{
				return (string)this[WindowsGroupSchema.SamAccountName];
			}
			set
			{
				this[WindowsGroupSchema.SamAccountName] = value;
			}
		}

		// Token: 0x170020A5 RID: 8357
		// (get) Token: 0x06005D3F RID: 23871 RVA: 0x001421C1 File Offset: 0x001403C1
		public SecurityIdentifier Sid
		{
			get
			{
				return (SecurityIdentifier)this[WindowsGroupSchema.Sid];
			}
		}

		// Token: 0x170020A6 RID: 8358
		// (get) Token: 0x06005D40 RID: 23872 RVA: 0x001421D3 File Offset: 0x001403D3
		public MultiValuedProperty<SecurityIdentifier> SidHistory
		{
			get
			{
				return (MultiValuedProperty<SecurityIdentifier>)this[WindowsGroupSchema.SidHistory];
			}
		}

		// Token: 0x170020A7 RID: 8359
		// (get) Token: 0x06005D41 RID: 23873 RVA: 0x001421E5 File Offset: 0x001403E5
		// (set) Token: 0x06005D42 RID: 23874 RVA: 0x001421F7 File Offset: 0x001403F7
		[Parameter(Mandatory = false)]
		public string SimpleDisplayName
		{
			get
			{
				return (string)this[WindowsGroupSchema.SimpleDisplayName];
			}
			set
			{
				this[WindowsGroupSchema.SimpleDisplayName] = value;
			}
		}

		// Token: 0x170020A8 RID: 8360
		// (get) Token: 0x06005D43 RID: 23875 RVA: 0x00142205 File Offset: 0x00140405
		public RecipientType RecipientType
		{
			get
			{
				return (RecipientType)this[WindowsGroupSchema.RecipientType];
			}
		}

		// Token: 0x170020A9 RID: 8361
		// (get) Token: 0x06005D44 RID: 23876 RVA: 0x00142217 File Offset: 0x00140417
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)this[WindowsGroupSchema.RecipientTypeDetails];
			}
		}

		// Token: 0x170020AA RID: 8362
		// (get) Token: 0x06005D45 RID: 23877 RVA: 0x00142229 File Offset: 0x00140429
		// (set) Token: 0x06005D46 RID: 23878 RVA: 0x0014223B File Offset: 0x0014043B
		[Parameter(Mandatory = false)]
		public SmtpAddress WindowsEmailAddress
		{
			get
			{
				return (SmtpAddress)this[WindowsGroupSchema.WindowsEmailAddress];
			}
			set
			{
				this[WindowsGroupSchema.WindowsEmailAddress] = value;
			}
		}

		// Token: 0x170020AB RID: 8363
		// (get) Token: 0x06005D47 RID: 23879 RVA: 0x0014224E File Offset: 0x0014044E
		// (set) Token: 0x06005D48 RID: 23880 RVA: 0x00142260 File Offset: 0x00140460
		[Parameter(Mandatory = false)]
		public string Notes
		{
			get
			{
				return (string)this[WindowsGroupSchema.Notes];
			}
			set
			{
				this[WindowsGroupSchema.Notes] = value;
			}
		}

		// Token: 0x170020AC RID: 8364
		// (get) Token: 0x06005D49 RID: 23881 RVA: 0x0014226E File Offset: 0x0014046E
		// (set) Token: 0x06005D4A RID: 23882 RVA: 0x00142280 File Offset: 0x00140480
		public MultiValuedProperty<ADObjectId> Members
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[WindowsGroupSchema.Members];
			}
			set
			{
				this[WindowsGroupSchema.Members] = value;
			}
		}

		// Token: 0x170020AD RID: 8365
		// (get) Token: 0x06005D4B RID: 23883 RVA: 0x0014228E File Offset: 0x0014048E
		// (set) Token: 0x06005D4C RID: 23884 RVA: 0x001422A0 File Offset: 0x001404A0
		[Parameter(Mandatory = false)]
		public string PhoneticDisplayName
		{
			get
			{
				return (string)this[WindowsGroupSchema.PhoneticDisplayName];
			}
			set
			{
				this[WindowsGroupSchema.PhoneticDisplayName] = value;
			}
		}

		// Token: 0x170020AE RID: 8366
		// (get) Token: 0x06005D4D RID: 23885 RVA: 0x001422AE File Offset: 0x001404AE
		public string OrganizationalUnit
		{
			get
			{
				return (string)this[WindowsGroupSchema.OrganizationalUnit];
			}
		}

		// Token: 0x170020AF RID: 8367
		// (get) Token: 0x06005D4E RID: 23886 RVA: 0x001422C0 File Offset: 0x001404C0
		// (set) Token: 0x06005D4F RID: 23887 RVA: 0x001422D2 File Offset: 0x001404D2
		[Parameter(Mandatory = false)]
		public int? SeniorityIndex
		{
			get
			{
				return (int?)this[WindowsGroupSchema.SeniorityIndex];
			}
			set
			{
				this[WindowsGroupSchema.SeniorityIndex] = value;
			}
		}

		// Token: 0x170020B0 RID: 8368
		// (get) Token: 0x06005D50 RID: 23888 RVA: 0x001422E5 File Offset: 0x001404E5
		// (set) Token: 0x06005D51 RID: 23889 RVA: 0x001422F7 File Offset: 0x001404F7
		[Parameter(Mandatory = false)]
		public bool IsHierarchicalGroup
		{
			get
			{
				return (bool)this[WindowsGroupSchema.IsHierarchicalGroup];
			}
			set
			{
				this[WindowsGroupSchema.IsHierarchicalGroup] = value;
			}
		}

		// Token: 0x04003F1A RID: 16154
		private static WindowsGroupSchema schema = ObjectSchema.GetInstance<WindowsGroupSchema>();
	}
}

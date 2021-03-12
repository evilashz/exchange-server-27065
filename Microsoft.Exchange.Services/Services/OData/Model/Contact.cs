using System;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E60 RID: 3680
	internal class Contact : Item
	{
		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x06005F38 RID: 24376 RVA: 0x001290E5 File Offset: 0x001272E5
		// (set) Token: 0x06005F39 RID: 24377 RVA: 0x001290F7 File Offset: 0x001272F7
		public string ParentFolderId
		{
			get
			{
				return (string)base[ContactSchema.ParentFolderId];
			}
			set
			{
				base[ContactSchema.ParentFolderId] = value;
			}
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x06005F3A RID: 24378 RVA: 0x00129105 File Offset: 0x00127305
		// (set) Token: 0x06005F3B RID: 24379 RVA: 0x00129117 File Offset: 0x00127317
		public DateTimeOffset Birthday
		{
			get
			{
				return (DateTimeOffset)base[ContactSchema.Birthday];
			}
			set
			{
				base[ContactSchema.Birthday] = value;
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x06005F3C RID: 24380 RVA: 0x0012912A File Offset: 0x0012732A
		// (set) Token: 0x06005F3D RID: 24381 RVA: 0x0012913C File Offset: 0x0012733C
		public string FileAs
		{
			get
			{
				return (string)base[ContactSchema.FileAs];
			}
			set
			{
				base[ContactSchema.FileAs] = value;
			}
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x06005F3E RID: 24382 RVA: 0x0012914A File Offset: 0x0012734A
		// (set) Token: 0x06005F3F RID: 24383 RVA: 0x0012915C File Offset: 0x0012735C
		public string DisplayName
		{
			get
			{
				return (string)base[ContactSchema.DisplayName];
			}
			set
			{
				base[ContactSchema.DisplayName] = value;
			}
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x06005F40 RID: 24384 RVA: 0x0012916A File Offset: 0x0012736A
		// (set) Token: 0x06005F41 RID: 24385 RVA: 0x0012917C File Offset: 0x0012737C
		public string GivenName
		{
			get
			{
				return (string)base[ContactSchema.GivenName];
			}
			set
			{
				base[ContactSchema.GivenName] = value;
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x06005F42 RID: 24386 RVA: 0x0012918A File Offset: 0x0012738A
		// (set) Token: 0x06005F43 RID: 24387 RVA: 0x0012919C File Offset: 0x0012739C
		public string Initials
		{
			get
			{
				return (string)base[ContactSchema.Initials];
			}
			set
			{
				base[ContactSchema.Initials] = value;
			}
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06005F44 RID: 24388 RVA: 0x001291AA File Offset: 0x001273AA
		// (set) Token: 0x06005F45 RID: 24389 RVA: 0x001291BC File Offset: 0x001273BC
		public string MiddleName
		{
			get
			{
				return (string)base[ContactSchema.MiddleName];
			}
			set
			{
				base[ContactSchema.MiddleName] = value;
			}
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06005F46 RID: 24390 RVA: 0x001291CA File Offset: 0x001273CA
		// (set) Token: 0x06005F47 RID: 24391 RVA: 0x001291DC File Offset: 0x001273DC
		public string NickName
		{
			get
			{
				return (string)base[ContactSchema.NickName];
			}
			set
			{
				base[ContactSchema.NickName] = value;
			}
		}

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x06005F48 RID: 24392 RVA: 0x001291EA File Offset: 0x001273EA
		// (set) Token: 0x06005F49 RID: 24393 RVA: 0x001291FC File Offset: 0x001273FC
		public string Surname
		{
			get
			{
				return (string)base[ContactSchema.Surname];
			}
			set
			{
				base[ContactSchema.Surname] = value;
			}
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06005F4A RID: 24394 RVA: 0x0012920A File Offset: 0x0012740A
		// (set) Token: 0x06005F4B RID: 24395 RVA: 0x0012921C File Offset: 0x0012741C
		public string Title
		{
			get
			{
				return (string)base[ContactSchema.Title];
			}
			set
			{
				base[ContactSchema.Title] = value;
			}
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x06005F4C RID: 24396 RVA: 0x0012922A File Offset: 0x0012742A
		// (set) Token: 0x06005F4D RID: 24397 RVA: 0x0012923C File Offset: 0x0012743C
		public string Generation
		{
			get
			{
				return (string)base[ContactSchema.Generation];
			}
			set
			{
				base[ContactSchema.Generation] = value;
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x06005F4E RID: 24398 RVA: 0x0012924A File Offset: 0x0012744A
		// (set) Token: 0x06005F4F RID: 24399 RVA: 0x0012925C File Offset: 0x0012745C
		public string EmailAddress1
		{
			get
			{
				return (string)base[ContactSchema.EmailAddress1];
			}
			set
			{
				base[ContactSchema.EmailAddress1] = value;
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x06005F50 RID: 24400 RVA: 0x0012926A File Offset: 0x0012746A
		// (set) Token: 0x06005F51 RID: 24401 RVA: 0x0012927C File Offset: 0x0012747C
		public string EmailAddress2
		{
			get
			{
				return (string)base[ContactSchema.EmailAddress2];
			}
			set
			{
				base[ContactSchema.EmailAddress2] = value;
			}
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x06005F52 RID: 24402 RVA: 0x0012928A File Offset: 0x0012748A
		// (set) Token: 0x06005F53 RID: 24403 RVA: 0x0012929C File Offset: 0x0012749C
		public string EmailAddress3
		{
			get
			{
				return (string)base[ContactSchema.EmailAddress3];
			}
			set
			{
				base[ContactSchema.EmailAddress3] = value;
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06005F54 RID: 24404 RVA: 0x001292AA File Offset: 0x001274AA
		// (set) Token: 0x06005F55 RID: 24405 RVA: 0x001292BC File Offset: 0x001274BC
		public string ImAddress1
		{
			get
			{
				return (string)base[ContactSchema.ImAddress1];
			}
			set
			{
				base[ContactSchema.ImAddress1] = value;
			}
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06005F56 RID: 24406 RVA: 0x001292CA File Offset: 0x001274CA
		// (set) Token: 0x06005F57 RID: 24407 RVA: 0x001292DC File Offset: 0x001274DC
		public string ImAddress2
		{
			get
			{
				return (string)base[ContactSchema.ImAddress2];
			}
			set
			{
				base[ContactSchema.ImAddress2] = value;
			}
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06005F58 RID: 24408 RVA: 0x001292EA File Offset: 0x001274EA
		// (set) Token: 0x06005F59 RID: 24409 RVA: 0x001292FC File Offset: 0x001274FC
		public string ImAddress3
		{
			get
			{
				return (string)base[ContactSchema.ImAddress3];
			}
			set
			{
				base[ContactSchema.ImAddress3] = value;
			}
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x06005F5A RID: 24410 RVA: 0x0012930A File Offset: 0x0012750A
		// (set) Token: 0x06005F5B RID: 24411 RVA: 0x0012931C File Offset: 0x0012751C
		public string JobTitle
		{
			get
			{
				return (string)base[ContactSchema.JobTitle];
			}
			set
			{
				base[ContactSchema.JobTitle] = value;
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x06005F5C RID: 24412 RVA: 0x0012932A File Offset: 0x0012752A
		// (set) Token: 0x06005F5D RID: 24413 RVA: 0x0012933C File Offset: 0x0012753C
		public string CompanyName
		{
			get
			{
				return (string)base[ContactSchema.CompanyName];
			}
			set
			{
				base[ContactSchema.CompanyName] = value;
			}
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x06005F5E RID: 24414 RVA: 0x0012934A File Offset: 0x0012754A
		// (set) Token: 0x06005F5F RID: 24415 RVA: 0x0012935C File Offset: 0x0012755C
		public string Department
		{
			get
			{
				return (string)base[ContactSchema.Department];
			}
			set
			{
				base[ContactSchema.Department] = value;
			}
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x06005F60 RID: 24416 RVA: 0x0012936A File Offset: 0x0012756A
		// (set) Token: 0x06005F61 RID: 24417 RVA: 0x0012937C File Offset: 0x0012757C
		public string OfficeLocation
		{
			get
			{
				return (string)base[ContactSchema.OfficeLocation];
			}
			set
			{
				base[ContactSchema.OfficeLocation] = value;
			}
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x06005F62 RID: 24418 RVA: 0x0012938A File Offset: 0x0012758A
		// (set) Token: 0x06005F63 RID: 24419 RVA: 0x0012939C File Offset: 0x0012759C
		public string Profession
		{
			get
			{
				return (string)base[ContactSchema.Profession];
			}
			set
			{
				base[ContactSchema.Profession] = value;
			}
		}

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x06005F64 RID: 24420 RVA: 0x001293AA File Offset: 0x001275AA
		// (set) Token: 0x06005F65 RID: 24421 RVA: 0x001293BC File Offset: 0x001275BC
		public string BusinessHomePage
		{
			get
			{
				return (string)base[ContactSchema.BusinessHomePage];
			}
			set
			{
				base[ContactSchema.BusinessHomePage] = value;
			}
		}

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x06005F66 RID: 24422 RVA: 0x001293CA File Offset: 0x001275CA
		// (set) Token: 0x06005F67 RID: 24423 RVA: 0x001293DC File Offset: 0x001275DC
		public string AssistantName
		{
			get
			{
				return (string)base[ContactSchema.AssistantName];
			}
			set
			{
				base[ContactSchema.AssistantName] = value;
			}
		}

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06005F68 RID: 24424 RVA: 0x001293EA File Offset: 0x001275EA
		// (set) Token: 0x06005F69 RID: 24425 RVA: 0x001293FC File Offset: 0x001275FC
		public string Manager
		{
			get
			{
				return (string)base[ContactSchema.Manager];
			}
			set
			{
				base[ContactSchema.Manager] = value;
			}
		}

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x06005F6A RID: 24426 RVA: 0x0012940A File Offset: 0x0012760A
		// (set) Token: 0x06005F6B RID: 24427 RVA: 0x0012941C File Offset: 0x0012761C
		public string HomePhone1
		{
			get
			{
				return (string)base[ContactSchema.HomePhone1];
			}
			set
			{
				base[ContactSchema.HomePhone1] = value;
			}
		}

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x06005F6C RID: 24428 RVA: 0x0012942A File Offset: 0x0012762A
		// (set) Token: 0x06005F6D RID: 24429 RVA: 0x0012943C File Offset: 0x0012763C
		public string HomePhone2
		{
			get
			{
				return (string)base[ContactSchema.HomePhone2];
			}
			set
			{
				base[ContactSchema.HomePhone2] = value;
			}
		}

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x06005F6E RID: 24430 RVA: 0x0012944A File Offset: 0x0012764A
		// (set) Token: 0x06005F6F RID: 24431 RVA: 0x0012945C File Offset: 0x0012765C
		public string BusinessPhone1
		{
			get
			{
				return (string)base[ContactSchema.BusinessPhone1];
			}
			set
			{
				base[ContactSchema.BusinessPhone1] = value;
			}
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06005F70 RID: 24432 RVA: 0x0012946A File Offset: 0x0012766A
		// (set) Token: 0x06005F71 RID: 24433 RVA: 0x0012947C File Offset: 0x0012767C
		public string BusinessPhone2
		{
			get
			{
				return (string)base[ContactSchema.BusinessPhone2];
			}
			set
			{
				base[ContactSchema.BusinessPhone2] = value;
			}
		}

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06005F72 RID: 24434 RVA: 0x0012948A File Offset: 0x0012768A
		// (set) Token: 0x06005F73 RID: 24435 RVA: 0x0012949C File Offset: 0x0012769C
		public string MobilePhone1
		{
			get
			{
				return (string)base[ContactSchema.MobilePhone1];
			}
			set
			{
				base[ContactSchema.MobilePhone1] = value;
			}
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06005F74 RID: 24436 RVA: 0x001294AA File Offset: 0x001276AA
		// (set) Token: 0x06005F75 RID: 24437 RVA: 0x001294BC File Offset: 0x001276BC
		public string OtherPhone
		{
			get
			{
				return (string)base[ContactSchema.OtherPhone];
			}
			set
			{
				base[ContactSchema.OtherPhone] = value;
			}
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06005F76 RID: 24438 RVA: 0x001294CA File Offset: 0x001276CA
		// (set) Token: 0x06005F77 RID: 24439 RVA: 0x001294DC File Offset: 0x001276DC
		public PhysicalAddress HomeAddress
		{
			get
			{
				return (PhysicalAddress)base[ContactSchema.HomeAddress];
			}
			set
			{
				base[ContactSchema.HomeAddress] = value;
			}
		}

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06005F78 RID: 24440 RVA: 0x001294EA File Offset: 0x001276EA
		// (set) Token: 0x06005F79 RID: 24441 RVA: 0x001294FC File Offset: 0x001276FC
		public PhysicalAddress BusinessAddress
		{
			get
			{
				return (PhysicalAddress)base[ContactSchema.BusinessAddress];
			}
			set
			{
				base[ContactSchema.BusinessAddress] = value;
			}
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x06005F7A RID: 24442 RVA: 0x0012950A File Offset: 0x0012770A
		// (set) Token: 0x06005F7B RID: 24443 RVA: 0x0012951C File Offset: 0x0012771C
		public PhysicalAddress OtherAddress
		{
			get
			{
				return (PhysicalAddress)base[ContactSchema.OtherAddress];
			}
			set
			{
				base[ContactSchema.OtherAddress] = value;
			}
		}

		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x06005F7C RID: 24444 RVA: 0x0012952A File Offset: 0x0012772A
		internal override EntitySchema Schema
		{
			get
			{
				return ContactSchema.SchemaInstance;
			}
		}

		// Token: 0x040033A0 RID: 13216
		internal new static readonly EdmEntityType EdmEntityType = new EdmEntityType(typeof(Contact).Namespace, typeof(Contact).Name, Microsoft.Exchange.Services.OData.Model.Item.EdmEntityType);
	}
}

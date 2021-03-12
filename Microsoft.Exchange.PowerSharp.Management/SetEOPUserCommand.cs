using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C2F RID: 3119
	public class SetEOPUserCommand : SyntheticCommand<object>
	{
		// Token: 0x0600981D RID: 38941 RVA: 0x000DD261 File Offset: 0x000DB461
		private SetEOPUserCommand() : base("Set-EOPUser")
		{
		}

		// Token: 0x0600981E RID: 38942 RVA: 0x000DD26E File Offset: 0x000DB46E
		public SetEOPUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600981F RID: 38943 RVA: 0x000DD27D File Offset: 0x000DB47D
		public virtual SetEOPUserCommand SetParameters(SetEOPUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C30 RID: 3120
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006B78 RID: 27512
			// (set) Token: 0x06009820 RID: 38944 RVA: 0x000DD287 File Offset: 0x000DB487
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006B79 RID: 27513
			// (set) Token: 0x06009821 RID: 38945 RVA: 0x000DD2A5 File Offset: 0x000DB4A5
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17006B7A RID: 27514
			// (set) Token: 0x06009822 RID: 38946 RVA: 0x000DD2B8 File Offset: 0x000DB4B8
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x17006B7B RID: 27515
			// (set) Token: 0x06009823 RID: 38947 RVA: 0x000DD2CB File Offset: 0x000DB4CB
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x17006B7C RID: 27516
			// (set) Token: 0x06009824 RID: 38948 RVA: 0x000DD2DE File Offset: 0x000DB4DE
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x17006B7D RID: 27517
			// (set) Token: 0x06009825 RID: 38949 RVA: 0x000DD2F1 File Offset: 0x000DB4F1
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x17006B7E RID: 27518
			// (set) Token: 0x06009826 RID: 38950 RVA: 0x000DD304 File Offset: 0x000DB504
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006B7F RID: 27519
			// (set) Token: 0x06009827 RID: 38951 RVA: 0x000DD317 File Offset: 0x000DB517
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x17006B80 RID: 27520
			// (set) Token: 0x06009828 RID: 38952 RVA: 0x000DD32A File Offset: 0x000DB52A
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17006B81 RID: 27521
			// (set) Token: 0x06009829 RID: 38953 RVA: 0x000DD33D File Offset: 0x000DB53D
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x17006B82 RID: 27522
			// (set) Token: 0x0600982A RID: 38954 RVA: 0x000DD350 File Offset: 0x000DB550
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17006B83 RID: 27523
			// (set) Token: 0x0600982B RID: 38955 RVA: 0x000DD363 File Offset: 0x000DB563
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17006B84 RID: 27524
			// (set) Token: 0x0600982C RID: 38956 RVA: 0x000DD376 File Offset: 0x000DB576
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x17006B85 RID: 27525
			// (set) Token: 0x0600982D RID: 38957 RVA: 0x000DD389 File Offset: 0x000DB589
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x17006B86 RID: 27526
			// (set) Token: 0x0600982E RID: 38958 RVA: 0x000DD39C File Offset: 0x000DB59C
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x17006B87 RID: 27527
			// (set) Token: 0x0600982F RID: 38959 RVA: 0x000DD3AF File Offset: 0x000DB5AF
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17006B88 RID: 27528
			// (set) Token: 0x06009830 RID: 38960 RVA: 0x000DD3C2 File Offset: 0x000DB5C2
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x17006B89 RID: 27529
			// (set) Token: 0x06009831 RID: 38961 RVA: 0x000DD3D5 File Offset: 0x000DB5D5
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x17006B8A RID: 27530
			// (set) Token: 0x06009832 RID: 38962 RVA: 0x000DD3E8 File Offset: 0x000DB5E8
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x17006B8B RID: 27531
			// (set) Token: 0x06009833 RID: 38963 RVA: 0x000DD3FB File Offset: 0x000DB5FB
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x17006B8C RID: 27532
			// (set) Token: 0x06009834 RID: 38964 RVA: 0x000DD40E File Offset: 0x000DB60E
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x17006B8D RID: 27533
			// (set) Token: 0x06009835 RID: 38965 RVA: 0x000DD421 File Offset: 0x000DB621
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006B8E RID: 27534
			// (set) Token: 0x06009836 RID: 38966 RVA: 0x000DD43F File Offset: 0x000DB63F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006B8F RID: 27535
			// (set) Token: 0x06009837 RID: 38967 RVA: 0x000DD457 File Offset: 0x000DB657
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006B90 RID: 27536
			// (set) Token: 0x06009838 RID: 38968 RVA: 0x000DD46F File Offset: 0x000DB66F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006B91 RID: 27537
			// (set) Token: 0x06009839 RID: 38969 RVA: 0x000DD487 File Offset: 0x000DB687
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}

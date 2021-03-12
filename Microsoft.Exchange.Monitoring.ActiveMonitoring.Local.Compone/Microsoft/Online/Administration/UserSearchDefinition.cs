using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003BB RID: 955
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "UserSearchDefinition", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class UserSearchDefinition : SearchDefinition
	{
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0008C49B File Offset: 0x0008A69B
		// (set) Token: 0x060016F5 RID: 5877 RVA: 0x0008C4A3 File Offset: 0x0008A6A3
		[DataMember]
		public AccountSkuIdentifier AccountSku
		{
			get
			{
				return this.AccountSkuField;
			}
			set
			{
				this.AccountSkuField = value;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x0008C4AC File Offset: 0x0008A6AC
		// (set) Token: 0x060016F7 RID: 5879 RVA: 0x0008C4B4 File Offset: 0x0008A6B4
		[DataMember]
		public bool? BlackberryUsersOnly
		{
			get
			{
				return this.BlackberryUsersOnlyField;
			}
			set
			{
				this.BlackberryUsersOnlyField = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x0008C4BD File Offset: 0x0008A6BD
		// (set) Token: 0x060016F9 RID: 5881 RVA: 0x0008C4C5 File Offset: 0x0008A6C5
		[DataMember]
		public string City
		{
			get
			{
				return this.CityField;
			}
			set
			{
				this.CityField = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x0008C4CE File Offset: 0x0008A6CE
		// (set) Token: 0x060016FB RID: 5883 RVA: 0x0008C4D6 File Offset: 0x0008A6D6
		[DataMember]
		public string Country
		{
			get
			{
				return this.CountryField;
			}
			set
			{
				this.CountryField = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x0008C4DF File Offset: 0x0008A6DF
		// (set) Token: 0x060016FD RID: 5885 RVA: 0x0008C4E7 File Offset: 0x0008A6E7
		[DataMember]
		public string Department
		{
			get
			{
				return this.DepartmentField;
			}
			set
			{
				this.DepartmentField = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x0008C4F0 File Offset: 0x0008A6F0
		// (set) Token: 0x060016FF RID: 5887 RVA: 0x0008C4F8 File Offset: 0x0008A6F8
		[DataMember]
		public string DomainName
		{
			get
			{
				return this.DomainNameField;
			}
			set
			{
				this.DomainNameField = value;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0008C501 File Offset: 0x0008A701
		// (set) Token: 0x06001701 RID: 5889 RVA: 0x0008C509 File Offset: 0x0008A709
		[DataMember]
		public UserEnabledFilter? EnabledFilter
		{
			get
			{
				return this.EnabledFilterField;
			}
			set
			{
				this.EnabledFilterField = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x0008C512 File Offset: 0x0008A712
		// (set) Token: 0x06001703 RID: 5891 RVA: 0x0008C51A File Offset: 0x0008A71A
		[DataMember]
		public bool? HasErrorsOnly
		{
			get
			{
				return this.HasErrorsOnlyField;
			}
			set
			{
				this.HasErrorsOnlyField = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x0008C523 File Offset: 0x0008A723
		// (set) Token: 0x06001705 RID: 5893 RVA: 0x0008C52B File Offset: 0x0008A72B
		[DataMember]
		public string[] IncludedProperties
		{
			get
			{
				return this.IncludedPropertiesField;
			}
			set
			{
				this.IncludedPropertiesField = value;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x0008C534 File Offset: 0x0008A734
		// (set) Token: 0x06001707 RID: 5895 RVA: 0x0008C53C File Offset: 0x0008A73C
		[DataMember]
		public bool? LicenseReconciliationNeededOnly
		{
			get
			{
				return this.LicenseReconciliationNeededOnlyField;
			}
			set
			{
				this.LicenseReconciliationNeededOnlyField = value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x0008C545 File Offset: 0x0008A745
		// (set) Token: 0x06001709 RID: 5897 RVA: 0x0008C54D File Offset: 0x0008A74D
		[DataMember]
		public bool? ReturnDeletedUsers
		{
			get
			{
				return this.ReturnDeletedUsersField;
			}
			set
			{
				this.ReturnDeletedUsersField = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x0008C556 File Offset: 0x0008A756
		// (set) Token: 0x0600170B RID: 5899 RVA: 0x0008C55E File Offset: 0x0008A75E
		[DataMember]
		public string State
		{
			get
			{
				return this.StateField;
			}
			set
			{
				this.StateField = value;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x0008C567 File Offset: 0x0008A767
		// (set) Token: 0x0600170D RID: 5901 RVA: 0x0008C56F File Offset: 0x0008A76F
		[DataMember]
		public bool? Synchronized
		{
			get
			{
				return this.SynchronizedField;
			}
			set
			{
				this.SynchronizedField = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x0008C578 File Offset: 0x0008A778
		// (set) Token: 0x0600170F RID: 5903 RVA: 0x0008C580 File Offset: 0x0008A780
		[DataMember]
		public string Title
		{
			get
			{
				return this.TitleField;
			}
			set
			{
				this.TitleField = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x0008C589 File Offset: 0x0008A789
		// (set) Token: 0x06001711 RID: 5905 RVA: 0x0008C591 File Offset: 0x0008A791
		[DataMember]
		public bool? UnlicensedUsersOnly
		{
			get
			{
				return this.UnlicensedUsersOnlyField;
			}
			set
			{
				this.UnlicensedUsersOnlyField = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x0008C59A File Offset: 0x0008A79A
		// (set) Token: 0x06001713 RID: 5907 RVA: 0x0008C5A2 File Offset: 0x0008A7A2
		[DataMember]
		public string UsageLocation
		{
			get
			{
				return this.UsageLocationField;
			}
			set
			{
				this.UsageLocationField = value;
			}
		}

		// Token: 0x04001045 RID: 4165
		private AccountSkuIdentifier AccountSkuField;

		// Token: 0x04001046 RID: 4166
		private bool? BlackberryUsersOnlyField;

		// Token: 0x04001047 RID: 4167
		private string CityField;

		// Token: 0x04001048 RID: 4168
		private string CountryField;

		// Token: 0x04001049 RID: 4169
		private string DepartmentField;

		// Token: 0x0400104A RID: 4170
		private string DomainNameField;

		// Token: 0x0400104B RID: 4171
		private UserEnabledFilter? EnabledFilterField;

		// Token: 0x0400104C RID: 4172
		private bool? HasErrorsOnlyField;

		// Token: 0x0400104D RID: 4173
		private string[] IncludedPropertiesField;

		// Token: 0x0400104E RID: 4174
		private bool? LicenseReconciliationNeededOnlyField;

		// Token: 0x0400104F RID: 4175
		private bool? ReturnDeletedUsersField;

		// Token: 0x04001050 RID: 4176
		private string StateField;

		// Token: 0x04001051 RID: 4177
		private bool? SynchronizedField;

		// Token: 0x04001052 RID: 4178
		private string TitleField;

		// Token: 0x04001053 RID: 4179
		private bool? UnlicensedUsersOnlyField;

		// Token: 0x04001054 RID: 4180
		private string UsageLocationField;
	}
}

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x02000070 RID: 112
	[DataContract(Name = "ThemeData", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class ThemeData : IExtensibleDataObject
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000DEC1 File Offset: 0x0000C0C1
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000DEC9 File Offset: 0x0000C0C9
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000DED2 File Offset: 0x0000C0D2
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000DEDA File Offset: 0x0000C0DA
		[DataMember]
		public bool IsDarkTheme
		{
			get
			{
				return this.IsDarkThemeField;
			}
			set
			{
				this.IsDarkThemeField = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000DEE3 File Offset: 0x0000C0E3
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000DEEB File Offset: 0x0000C0EB
		[DataMember]
		public string[] NeutralColors
		{
			get
			{
				return this.NeutralColorsField;
			}
			set
			{
				this.NeutralColorsField = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		[DataMember]
		public string TenantPrimaryColor
		{
			get
			{
				return this.TenantPrimaryColorField;
			}
			set
			{
				this.TenantPrimaryColorField = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000DF05 File Offset: 0x0000C105
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0000DF0D File Offset: 0x0000C10D
		[DataMember]
		public string[] TenantPrimaryColorShades
		{
			get
			{
				return this.TenantPrimaryColorShadesField;
			}
			set
			{
				this.TenantPrimaryColorShadesField = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000DF16 File Offset: 0x0000C116
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0000DF1E File Offset: 0x0000C11E
		[DataMember]
		public string[] TenantThemeColors
		{
			get
			{
				return this.TenantThemeColorsField;
			}
			set
			{
				this.TenantThemeColorsField = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000DF27 File Offset: 0x0000C127
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0000DF2F File Offset: 0x0000C12F
		[DataMember]
		public string ThemeVersion
		{
			get
			{
				return this.ThemeVersionField;
			}
			set
			{
				this.ThemeVersionField = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000DF38 File Offset: 0x0000C138
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0000DF40 File Offset: 0x0000C140
		[DataMember]
		public bool UserPersonalizationAllowed
		{
			get
			{
				return this.UserPersonalizationAllowedField;
			}
			set
			{
				this.UserPersonalizationAllowedField = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000DF49 File Offset: 0x0000C149
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0000DF51 File Offset: 0x0000C151
		[DataMember]
		public string[] UserThemeColors
		{
			get
			{
				return this.UserThemeColorsField;
			}
			set
			{
				this.UserThemeColorsField = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000DF5A File Offset: 0x0000C15A
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000DF62 File Offset: 0x0000C162
		[DataMember]
		public string UserThemeId
		{
			get
			{
				return this.UserThemeIdField;
			}
			set
			{
				this.UserThemeIdField = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000DF6B File Offset: 0x0000C16B
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000DF73 File Offset: 0x0000C173
		[DataMember]
		public string[] UserThemePrimaryColorShades
		{
			get
			{
				return this.UserThemePrimaryColorShadesField;
			}
			set
			{
				this.UserThemePrimaryColorShadesField = value;
			}
		}

		// Token: 0x040001ED RID: 493
		private ExtensionDataObject extensionDataField;

		// Token: 0x040001EE RID: 494
		private bool IsDarkThemeField;

		// Token: 0x040001EF RID: 495
		private string[] NeutralColorsField;

		// Token: 0x040001F0 RID: 496
		private string TenantPrimaryColorField;

		// Token: 0x040001F1 RID: 497
		private string[] TenantPrimaryColorShadesField;

		// Token: 0x040001F2 RID: 498
		private string[] TenantThemeColorsField;

		// Token: 0x040001F3 RID: 499
		private string ThemeVersionField;

		// Token: 0x040001F4 RID: 500
		private bool UserPersonalizationAllowedField;

		// Token: 0x040001F5 RID: 501
		private string[] UserThemeColorsField;

		// Token: 0x040001F6 RID: 502
		private string UserThemeIdField;

		// Token: 0x040001F7 RID: 503
		private string[] UserThemePrimaryColorShadesField;
	}
}

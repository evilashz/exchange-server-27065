using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x0200006F RID: 111
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ShellInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	public class ShellInfo : NavBarInfo
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000DE0F File Offset: 0x0000C00F
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000DE17 File Offset: 0x0000C017
		[DataMember]
		public string SharedCSSTouchDeviceUrl
		{
			get
			{
				return this.SharedCSSTouchDeviceUrlField;
			}
			set
			{
				this.SharedCSSTouchDeviceUrlField = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000DE20 File Offset: 0x0000C020
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000DE28 File Offset: 0x0000C028
		[DataMember]
		public string SharedCSSTouchNarrowUrl
		{
			get
			{
				return this.SharedCSSTouchNarrowUrlField;
			}
			set
			{
				this.SharedCSSTouchNarrowUrlField = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000DE31 File Offset: 0x0000C031
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000DE39 File Offset: 0x0000C039
		[DataMember]
		public string SharedCSSTouchWideUrl
		{
			get
			{
				return this.SharedCSSTouchWideUrlField;
			}
			set
			{
				this.SharedCSSTouchWideUrlField = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000DE42 File Offset: 0x0000C042
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000DE4A File Offset: 0x0000C04A
		[DataMember]
		public string SharedJSTouchDeviceUrl
		{
			get
			{
				return this.SharedJSTouchDeviceUrlField;
			}
			set
			{
				this.SharedJSTouchDeviceUrlField = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000DE53 File Offset: 0x0000C053
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000DE5B File Offset: 0x0000C05B
		[DataMember]
		public string SharedJSTouchNarrowUrl
		{
			get
			{
				return this.SharedJSTouchNarrowUrlField;
			}
			set
			{
				this.SharedJSTouchNarrowUrlField = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000DE64 File Offset: 0x0000C064
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000DE6C File Offset: 0x0000C06C
		[DataMember]
		public string SharedJSTouchWideUrl
		{
			get
			{
				return this.SharedJSTouchWideUrlField;
			}
			set
			{
				this.SharedJSTouchWideUrlField = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000DE75 File Offset: 0x0000C075
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000DE7D File Offset: 0x0000C07D
		[DataMember]
		public string SuiteServiceProxyOriginAllowedList
		{
			get
			{
				return this.SuiteServiceProxyOriginAllowedListField;
			}
			set
			{
				this.SuiteServiceProxyOriginAllowedListField = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000DE86 File Offset: 0x0000C086
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000DE8E File Offset: 0x0000C08E
		[DataMember]
		public string SuiteServiceProxyScriptUrl
		{
			get
			{
				return this.SuiteServiceProxyScriptUrlField;
			}
			set
			{
				this.SuiteServiceProxyScriptUrlField = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000DE97 File Offset: 0x0000C097
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0000DE9F File Offset: 0x0000C09F
		[DataMember]
		public string ThemeCSSUrl
		{
			get
			{
				return this.ThemeCSSUrlField;
			}
			set
			{
				this.ThemeCSSUrlField = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		[DataMember]
		public ThemeData ThemeData
		{
			get
			{
				return this.ThemeDataField;
			}
			set
			{
				this.ThemeDataField = value;
			}
		}

		// Token: 0x040001E3 RID: 483
		private string SharedCSSTouchDeviceUrlField;

		// Token: 0x040001E4 RID: 484
		private string SharedCSSTouchNarrowUrlField;

		// Token: 0x040001E5 RID: 485
		private string SharedCSSTouchWideUrlField;

		// Token: 0x040001E6 RID: 486
		private string SharedJSTouchDeviceUrlField;

		// Token: 0x040001E7 RID: 487
		private string SharedJSTouchNarrowUrlField;

		// Token: 0x040001E8 RID: 488
		private string SharedJSTouchWideUrlField;

		// Token: 0x040001E9 RID: 489
		private string SuiteServiceProxyOriginAllowedListField;

		// Token: 0x040001EA RID: 490
		private string SuiteServiceProxyScriptUrlField;

		// Token: 0x040001EB RID: 491
		private string ThemeCSSUrlField;

		// Token: 0x040001EC RID: 492
		private ThemeData ThemeDataField;
	}
}

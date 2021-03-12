using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001C0 RID: 448
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class ManageUnifiedMessagingRole : ManageRole
	{
		// Token: 0x06000F75 RID: 3957 RVA: 0x000443FC File Offset: 0x000425FC
		protected override void PopulateContextVariables()
		{
			base.PopulateContextVariables();
			this.Language = CultureInfo.CreateSpecificCulture("en-us");
			try
			{
				UmLanguagePack umLanguagePack = UmLanguagePackUtils.GetUmLanguagePack(this.Language);
				this.TeleProductCode = umLanguagePack.TeleProductCode;
				this.TransProductCode = umLanguagePack.TransProductCode;
				this.TtsProductCode = umLanguagePack.TtsProductCode;
				this.ProductCode = umLanguagePack.ProductCode;
			}
			catch (UnSupportedUMLanguageException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
			}
			base.WriteVerbose(Strings.UmLanguagePackProductCode(this.ProductCode));
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0004448C File Offset: 0x0004268C
		// (set) Token: 0x06000F77 RID: 3959 RVA: 0x000444A3 File Offset: 0x000426A3
		[Parameter(Mandatory = false)]
		public string LogFilePath
		{
			get
			{
				return (string)base.Fields["LogFilePath"];
			}
			set
			{
				base.Fields["LogFilePath"] = value;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x000444B6 File Offset: 0x000426B6
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x000444CD File Offset: 0x000426CD
		[Parameter(Mandatory = false)]
		public CultureInfo Language
		{
			get
			{
				return (CultureInfo)base.Fields["Language"];
			}
			set
			{
				base.Fields["Language"] = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x000444E0 File Offset: 0x000426E0
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x000444F7 File Offset: 0x000426F7
		[Parameter(Mandatory = false)]
		public Guid ProductCode
		{
			get
			{
				return (Guid)base.Fields["ProductCode"];
			}
			set
			{
				base.Fields["ProductCode"] = value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0004450F File Offset: 0x0004270F
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x00044526 File Offset: 0x00042726
		[Parameter(Mandatory = false)]
		public Guid TeleProductCode
		{
			get
			{
				return (Guid)base.Fields["TeleProductCode"];
			}
			set
			{
				base.Fields["TeleProductCode"] = value;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0004453E File Offset: 0x0004273E
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x00044555 File Offset: 0x00042755
		[Parameter(Mandatory = false)]
		public Guid TransProductCode
		{
			get
			{
				return (Guid)base.Fields["TransProductCode"];
			}
			set
			{
				base.Fields["TransProductCode"] = value;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0004456D File Offset: 0x0004276D
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x00044584 File Offset: 0x00042784
		[Parameter(Mandatory = false)]
		public Guid TtsProductCode
		{
			get
			{
				return (Guid)base.Fields["TtsProductCode"];
			}
			set
			{
				base.Fields["TtsProductCode"] = value;
			}
		}
	}
}

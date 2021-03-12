using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B63 RID: 2915
	public class ImportUMPromptCommand : SyntheticCommandWithPipelineInput<UMDialPlanIdParameter, UMDialPlanIdParameter>
	{
		// Token: 0x06008D66 RID: 36198 RVA: 0x000CF3DC File Offset: 0x000CD5DC
		private ImportUMPromptCommand() : base("Import-UMPrompt")
		{
		}

		// Token: 0x06008D67 RID: 36199 RVA: 0x000CF3E9 File Offset: 0x000CD5E9
		public ImportUMPromptCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008D68 RID: 36200 RVA: 0x000CF3F8 File Offset: 0x000CD5F8
		public virtual ImportUMPromptCommand SetParameters(ImportUMPromptCommand.UploadAutoAttendantPromptsStreamParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008D69 RID: 36201 RVA: 0x000CF402 File Offset: 0x000CD602
		public virtual ImportUMPromptCommand SetParameters(ImportUMPromptCommand.UploadAutoAttendantPromptsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008D6A RID: 36202 RVA: 0x000CF40C File Offset: 0x000CD60C
		public virtual ImportUMPromptCommand SetParameters(ImportUMPromptCommand.UploadDialPlanPromptsStreamParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008D6B RID: 36203 RVA: 0x000CF416 File Offset: 0x000CD616
		public virtual ImportUMPromptCommand SetParameters(ImportUMPromptCommand.UploadDialPlanPromptsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008D6C RID: 36204 RVA: 0x000CF420 File Offset: 0x000CD620
		public virtual ImportUMPromptCommand SetParameters(ImportUMPromptCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B64 RID: 2916
		public class UploadAutoAttendantPromptsStreamParameters : ParametersBase
		{
			// Token: 0x17006259 RID: 25177
			// (set) Token: 0x06008D6D RID: 36205 RVA: 0x000CF42A File Offset: 0x000CD62A
			public virtual string UMAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["UMAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x1700625A RID: 25178
			// (set) Token: 0x06008D6E RID: 36206 RVA: 0x000CF448 File Offset: 0x000CD648
			public virtual string PromptFileName
			{
				set
				{
					base.PowerSharpParameters["PromptFileName"] = value;
				}
			}

			// Token: 0x1700625B RID: 25179
			// (set) Token: 0x06008D6F RID: 36207 RVA: 0x000CF45B File Offset: 0x000CD65B
			public virtual Stream PromptFileStream
			{
				set
				{
					base.PowerSharpParameters["PromptFileStream"] = value;
				}
			}

			// Token: 0x1700625C RID: 25180
			// (set) Token: 0x06008D70 RID: 36208 RVA: 0x000CF46E File Offset: 0x000CD66E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700625D RID: 25181
			// (set) Token: 0x06008D71 RID: 36209 RVA: 0x000CF481 File Offset: 0x000CD681
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700625E RID: 25182
			// (set) Token: 0x06008D72 RID: 36210 RVA: 0x000CF499 File Offset: 0x000CD699
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700625F RID: 25183
			// (set) Token: 0x06008D73 RID: 36211 RVA: 0x000CF4B1 File Offset: 0x000CD6B1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006260 RID: 25184
			// (set) Token: 0x06008D74 RID: 36212 RVA: 0x000CF4C9 File Offset: 0x000CD6C9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006261 RID: 25185
			// (set) Token: 0x06008D75 RID: 36213 RVA: 0x000CF4E1 File Offset: 0x000CD6E1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B65 RID: 2917
		public class UploadAutoAttendantPromptsParameters : ParametersBase
		{
			// Token: 0x17006262 RID: 25186
			// (set) Token: 0x06008D77 RID: 36215 RVA: 0x000CF501 File Offset: 0x000CD701
			public virtual string UMAutoAttendant
			{
				set
				{
					base.PowerSharpParameters["UMAutoAttendant"] = ((value != null) ? new UMAutoAttendantIdParameter(value) : null);
				}
			}

			// Token: 0x17006263 RID: 25187
			// (set) Token: 0x06008D78 RID: 36216 RVA: 0x000CF51F File Offset: 0x000CD71F
			public virtual string PromptFileName
			{
				set
				{
					base.PowerSharpParameters["PromptFileName"] = value;
				}
			}

			// Token: 0x17006264 RID: 25188
			// (set) Token: 0x06008D79 RID: 36217 RVA: 0x000CF532 File Offset: 0x000CD732
			public virtual byte PromptFileData
			{
				set
				{
					base.PowerSharpParameters["PromptFileData"] = value;
				}
			}

			// Token: 0x17006265 RID: 25189
			// (set) Token: 0x06008D7A RID: 36218 RVA: 0x000CF54A File Offset: 0x000CD74A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006266 RID: 25190
			// (set) Token: 0x06008D7B RID: 36219 RVA: 0x000CF55D File Offset: 0x000CD75D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006267 RID: 25191
			// (set) Token: 0x06008D7C RID: 36220 RVA: 0x000CF575 File Offset: 0x000CD775
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006268 RID: 25192
			// (set) Token: 0x06008D7D RID: 36221 RVA: 0x000CF58D File Offset: 0x000CD78D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006269 RID: 25193
			// (set) Token: 0x06008D7E RID: 36222 RVA: 0x000CF5A5 File Offset: 0x000CD7A5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700626A RID: 25194
			// (set) Token: 0x06008D7F RID: 36223 RVA: 0x000CF5BD File Offset: 0x000CD7BD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B66 RID: 2918
		public class UploadDialPlanPromptsStreamParameters : ParametersBase
		{
			// Token: 0x1700626B RID: 25195
			// (set) Token: 0x06008D81 RID: 36225 RVA: 0x000CF5DD File Offset: 0x000CD7DD
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700626C RID: 25196
			// (set) Token: 0x06008D82 RID: 36226 RVA: 0x000CF5FB File Offset: 0x000CD7FB
			public virtual string PromptFileName
			{
				set
				{
					base.PowerSharpParameters["PromptFileName"] = value;
				}
			}

			// Token: 0x1700626D RID: 25197
			// (set) Token: 0x06008D83 RID: 36227 RVA: 0x000CF60E File Offset: 0x000CD80E
			public virtual Stream PromptFileStream
			{
				set
				{
					base.PowerSharpParameters["PromptFileStream"] = value;
				}
			}

			// Token: 0x1700626E RID: 25198
			// (set) Token: 0x06008D84 RID: 36228 RVA: 0x000CF621 File Offset: 0x000CD821
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700626F RID: 25199
			// (set) Token: 0x06008D85 RID: 36229 RVA: 0x000CF634 File Offset: 0x000CD834
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006270 RID: 25200
			// (set) Token: 0x06008D86 RID: 36230 RVA: 0x000CF64C File Offset: 0x000CD84C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006271 RID: 25201
			// (set) Token: 0x06008D87 RID: 36231 RVA: 0x000CF664 File Offset: 0x000CD864
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006272 RID: 25202
			// (set) Token: 0x06008D88 RID: 36232 RVA: 0x000CF67C File Offset: 0x000CD87C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006273 RID: 25203
			// (set) Token: 0x06008D89 RID: 36233 RVA: 0x000CF694 File Offset: 0x000CD894
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B67 RID: 2919
		public class UploadDialPlanPromptsParameters : ParametersBase
		{
			// Token: 0x17006274 RID: 25204
			// (set) Token: 0x06008D8B RID: 36235 RVA: 0x000CF6B4 File Offset: 0x000CD8B4
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17006275 RID: 25205
			// (set) Token: 0x06008D8C RID: 36236 RVA: 0x000CF6D2 File Offset: 0x000CD8D2
			public virtual string PromptFileName
			{
				set
				{
					base.PowerSharpParameters["PromptFileName"] = value;
				}
			}

			// Token: 0x17006276 RID: 25206
			// (set) Token: 0x06008D8D RID: 36237 RVA: 0x000CF6E5 File Offset: 0x000CD8E5
			public virtual byte PromptFileData
			{
				set
				{
					base.PowerSharpParameters["PromptFileData"] = value;
				}
			}

			// Token: 0x17006277 RID: 25207
			// (set) Token: 0x06008D8E RID: 36238 RVA: 0x000CF6FD File Offset: 0x000CD8FD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006278 RID: 25208
			// (set) Token: 0x06008D8F RID: 36239 RVA: 0x000CF710 File Offset: 0x000CD910
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006279 RID: 25209
			// (set) Token: 0x06008D90 RID: 36240 RVA: 0x000CF728 File Offset: 0x000CD928
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700627A RID: 25210
			// (set) Token: 0x06008D91 RID: 36241 RVA: 0x000CF740 File Offset: 0x000CD940
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700627B RID: 25211
			// (set) Token: 0x06008D92 RID: 36242 RVA: 0x000CF758 File Offset: 0x000CD958
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700627C RID: 25212
			// (set) Token: 0x06008D93 RID: 36243 RVA: 0x000CF770 File Offset: 0x000CD970
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B68 RID: 2920
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700627D RID: 25213
			// (set) Token: 0x06008D95 RID: 36245 RVA: 0x000CF790 File Offset: 0x000CD990
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700627E RID: 25214
			// (set) Token: 0x06008D96 RID: 36246 RVA: 0x000CF7A3 File Offset: 0x000CD9A3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700627F RID: 25215
			// (set) Token: 0x06008D97 RID: 36247 RVA: 0x000CF7BB File Offset: 0x000CD9BB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006280 RID: 25216
			// (set) Token: 0x06008D98 RID: 36248 RVA: 0x000CF7D3 File Offset: 0x000CD9D3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006281 RID: 25217
			// (set) Token: 0x06008D99 RID: 36249 RVA: 0x000CF7EB File Offset: 0x000CD9EB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006282 RID: 25218
			// (set) Token: 0x06008D9A RID: 36250 RVA: 0x000CF803 File Offset: 0x000CDA03
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}

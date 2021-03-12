using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000005 RID: 5
	internal class AACustomMenuPrompt : VariablePrompt<AutoAttendantContext>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000025FD File Offset: 0x000007FD
		protected override PromptConfigBase PreviewConfig
		{
			get
			{
				return GlobCfg.DefaultPromptsForPreview.AACustomMenu;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000260C File Offset: 0x0000080C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"aaCustomMenu",
				base.Config.PromptName,
				(this.aa == null) ? string.Empty : this.aa.ToString(),
				base.SbLog.ToString()
			});
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002674 File Offset: 0x00000874
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Business hours prompt returning ssmlstring: {0}.", new object[]
			{
				base.SbSsml.ToString()
			});
			return base.SbSsml.ToString();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026B4 File Offset: 0x000008B4
		internal string[] GetMenu(IEnumerable<CustomMenuKeyMapping> customExtensions)
		{
			string[] array = new string[]
			{
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty
			};
			foreach (CustomMenuKeyMapping customMenuKeyMapping in customExtensions)
			{
				array[(int)customMenuKeyMapping.MappedKey] = customMenuKeyMapping.Description;
			}
			return array;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002774 File Offset: 0x00000974
		protected override void InternalInitialize()
		{
			if (base.InitVal == null)
			{
				return;
			}
			this.aa = base.InitVal.AutoAttendant;
			base.Culture = this.aa.Language.Culture;
			this.menu = this.GetMenu(base.InitVal.IsBusinessHours ? base.InitVal.AutoAttendant.BusinessHoursKeyMapping : base.InitVal.AutoAttendant.AfterHoursKeyMapping);
			if (this.aa.SpeechEnabled)
			{
				this.InitializeSpeechCustomMenu();
			}
			else
			{
				this.InitializeDtmfCustomMenu();
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "AACustomMenuPrompt successfully intialized with ssml '{0}'.", new object[]
			{
				base.SbSsml.ToString()
			});
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000282D File Offset: 0x00000A2D
		private void InitializeDtmfCustomMenu()
		{
			this.AddKeyPrompts();
			if (this.aa.CallSomeoneEnabled || this.aa.SendVoiceMsgEnabled)
			{
				this.AddCallSomeonePrompt();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002858 File Offset: 0x00000A58
		private void InitializeSpeechCustomMenu()
		{
			if (this.aa.NameLookupEnabled && (this.aa.CallSomeoneEnabled || this.aa.SendVoiceMsgEnabled))
			{
				base.AddPrompts(GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
				{
					GlobCfg.DefaultPromptsForAA.PleaseSayTheName
				}));
			}
			else
			{
				base.AddPrompts(GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
				{
					GlobCfg.DefaultPromptsForAA.PleaseChooseFrom
				}));
			}
			this.AddDepartmentListPrompt();
			this.AddTimeoutOptionPrompt();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028E8 File Offset: 0x00000AE8
		private void AddKeyPrompts()
		{
			for (int i = 1; i <= 11; i++)
			{
				if (!string.IsNullOrEmpty(this.menu[i]))
				{
					this.AddKeyPrompt(i);
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002918 File Offset: 0x00000B18
		private void AddDepartmentListPrompt()
		{
			List<string> list = new List<string>();
			for (int i = 1; i < 11; i++)
			{
				if (!string.IsNullOrEmpty(this.menu[i]))
				{
					list.Add(this.menu[i]);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			TextListPrompt textListPrompt = new TextListPrompt();
			textListPrompt.Initialize(base.Config, base.Culture, list);
			base.AddPrompt(textListPrompt);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002980 File Offset: 0x00000B80
		private void AddCallSomeonePrompt()
		{
			base.AddPrompts(GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
			{
				GlobCfg.DefaultPromptsForAA.CallSomeone
			}));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029B4 File Offset: 0x00000BB4
		private void AddKeyPrompt(int i)
		{
			ArrayList prompts = GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
			{
				GlobCfg.DefaultPromptsForAA.CustomMenuConfig[i]
			});
			VariablePrompt<string>.SetActualPromptValues(prompts, "departmentName", this.menu[i]);
			base.AddPrompts(prompts);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029FF File Offset: 0x00000BFF
		private void AddTimeoutOptionPrompt()
		{
			if (string.IsNullOrEmpty(this.menu[11]))
			{
				return;
			}
			this.AddKeyPrompt(11);
		}

		// Token: 0x0400000C RID: 12
		private UMAutoAttendant aa;

		// Token: 0x0400000D RID: 13
		private string[] menu;
	}
}

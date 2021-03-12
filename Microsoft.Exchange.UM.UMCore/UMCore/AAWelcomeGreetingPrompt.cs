using System;
using System.Collections;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000006 RID: 6
	internal class AAWelcomeGreetingPrompt : VariablePrompt<AutoAttendantContext>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002A22 File Offset: 0x00000C22
		protected override PromptConfigBase PreviewConfig
		{
			get
			{
				return GlobCfg.DefaultPromptsForPreview.AAWelcomeGreeting;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A30 File Offset: 0x00000C30
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"aaWelcomeGreeting",
				base.Config.PromptName,
				(this.aa == null) ? string.Empty : this.aa.ToString(),
				base.SbLog.ToString()
			});
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A98 File Offset: 0x00000C98
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Business hours prompt returning ssmlstring: {0}.", new object[]
			{
				base.SbSsml.ToString()
			});
			return base.SbSsml.ToString();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002AD8 File Offset: 0x00000CD8
		protected override void InternalInitialize()
		{
			if (base.InitVal == null)
			{
				return;
			}
			this.aa = base.InitVal.AutoAttendant;
			base.Culture = this.aa.Language.Culture;
			bool isBusinessHours = base.InitVal.IsBusinessHours;
			if (this.aa.BusinessHoursWelcomeGreetingEnabled && isBusinessHours)
			{
				this.AddAAWelcomeCustomPrompt(this.aa.BusinessHoursWelcomeGreetingFilename);
			}
			else if (this.aa.AfterHoursWelcomeGreetingEnabled && !isBusinessHours)
			{
				this.AddAAWelcomeCustomPrompt(this.aa.AfterHoursWelcomeGreetingFilename);
			}
			else if (string.IsNullOrEmpty(this.aa.BusinessName))
			{
				if (isBusinessHours)
				{
					base.AddPrompts(GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
					{
						GlobCfg.DefaultPromptsForAA.AABusinessHoursWelcome
					}));
				}
				else
				{
					base.AddPrompts(GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
					{
						GlobCfg.DefaultPromptsForAA.AAAfterHoursWelcome
					}));
				}
			}
			else
			{
				base.AddPrompts(this.GetAAWelcomeWithBusinessNamePrompt());
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "AAWelcomeGreetingPrompt successfully intialized with ssml '{0}'.", new object[]
			{
				base.SbSsml.ToString()
			});
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C04 File Offset: 0x00000E04
		private void AddAAWelcomeCustomPrompt(string promptFileName)
		{
			UMConfigCache umconfigCache = new UMConfigCache();
			string prompt = umconfigCache.GetPrompt<UMAutoAttendant>(this.aa, promptFileName);
			PromptConfigBase promptConfigBase = PromptConfigBase.Create(prompt, "wave", string.Empty);
			base.AddPrompts(GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
			{
				promptConfigBase
			}));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C54 File Offset: 0x00000E54
		private ArrayList GetAAWelcomeWithBusinessNamePrompt()
		{
			ArrayList arrayList = GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
			{
				GlobCfg.DefaultPromptsForAA.AAWelcomeWithBusinessName
			});
			VariablePrompt<object>.SetActualPromptValues(arrayList, "businessName", this.aa.BusinessName);
			return arrayList;
		}

		// Token: 0x0400000E RID: 14
		private UMAutoAttendant aa;
	}
}

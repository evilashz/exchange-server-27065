using System;
using System.Collections;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000004 RID: 4
	internal class AABusinessLocationPrompt : VariablePrompt<AutoAttendantLocationContext>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002416 File Offset: 0x00000616
		protected override PromptConfigBase PreviewConfig
		{
			get
			{
				return GlobCfg.DefaultPromptsForPreview.AABusinessLocation;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002424 File Offset: 0x00000624
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"aaBusinessLocation",
				base.Config.PromptName,
				(this.aa == null) ? string.Empty : this.aa.ToString(),
				base.SbLog.ToString()
			});
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000248C File Offset: 0x0000068C
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Business hours prompt returning ssmlstring: {0}.", new object[]
			{
				base.SbSsml.ToString()
			});
			return base.SbSsml.ToString();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024CC File Offset: 0x000006CC
		protected override void InternalInitialize()
		{
			if (base.InitVal == null)
			{
				return;
			}
			this.aa = base.InitVal.AutoAttendant;
			this.selectedMenuDescription = base.InitVal.LocationMenuDescription;
			base.Culture = this.aa.Language.Culture;
			if (string.IsNullOrEmpty(this.aa.BusinessLocation))
			{
				base.AddPrompts(this.GetBusinessAddressNotSetPrompt());
			}
			else
			{
				base.AddPrompts(this.GetBusinessAddressPrompt());
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "AABusinessLocationPrompt successfully intialized with ssml '{0}'.", new object[]
			{
				base.SbSsml.ToString()
			});
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000256C File Offset: 0x0000076C
		private ArrayList GetBusinessAddressPrompt()
		{
			ArrayList arrayList = GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
			{
				GlobCfg.DefaultPromptsForAA.BusinessAddress
			});
			VariablePrompt<string>.SetActualPromptValues(arrayList, "businessAddress", this.aa.BusinessLocation);
			return arrayList;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025B4 File Offset: 0x000007B4
		private ArrayList GetBusinessAddressNotSetPrompt()
		{
			ArrayList arrayList = GlobCfg.DefaultPromptForAAHelper.Build(null, base.Culture, new PromptConfigBase[]
			{
				GlobCfg.DefaultPromptsForAA.BusinessAddressNotSet
			});
			VariablePrompt<string>.SetActualPromptValues(arrayList, "selectedMenu", this.selectedMenuDescription);
			return arrayList;
		}

		// Token: 0x0400000A RID: 10
		private UMAutoAttendant aa;

		// Token: 0x0400000B RID: 11
		private string selectedMenuDescription;
	}
}

using System;
using System.Collections;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000170 RID: 368
	internal abstract class MailboxGreetingPrompt : VariablePrompt<object>
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000AD1 RID: 2769
		protected abstract string PromptType { get; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000AD2 RID: 2770
		protected abstract PromptConfigBase PromptConfig { get; }

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002DDB8 File Offset: 0x0002BFB8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				this.PromptType,
				base.Config.PromptName,
				string.Empty,
				base.SbLog.ToString()
			});
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002DE0C File Offset: 0x0002C00C
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Business hours prompt returning ssmlstring: {0}.", new object[]
			{
				base.SbSsml.ToString()
			});
			return base.SbSsml.ToString();
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002DE4C File Offset: 0x0002C04C
		protected override void InternalInitialize()
		{
			if (base.InitVal == null)
			{
				return;
			}
			base.AddPrompts(this.GetGreetingPrompt());
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "MailboxGreetingPrompt successfully intialized with ssml '{0}'.", new object[]
			{
				base.SbSsml.ToString()
			});
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002DE94 File Offset: 0x0002C094
		private ArrayList GetGreetingPrompt()
		{
			ArrayList arrayList = GlobCfg.DefaultPromptHelper.Build(null, base.Culture, new PromptConfigBase[]
			{
				this.PromptConfig
			});
			VariablePrompt<object>.SetActualPromptValues(arrayList, "userName", base.InitVal);
			return arrayList;
		}
	}
}

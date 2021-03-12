using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F1 RID: 497
	internal class TempFilePrompt : VariablePrompt<ITempWavFile>
	{
		// Token: 0x06000E97 RID: 3735 RVA: 0x00041658 File Offset: 0x0003F858
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3} Extra={4}", new object[]
			{
				"tempwave",
				base.Config.PromptName,
				this.filename,
				base.Config.PromptName,
				(this.tempWav.ExtraInfo == null) ? "<null>" : this.tempWav.ExtraInfo
			});
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x000416CB File Offset: 0x0003F8CB
		internal string FileName
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x000416D3 File Offset: 0x0003F8D3
		internal override string ToSsml()
		{
			return FilePrompt.BuildAudioSsml(this.filename, base.ProsodyRate);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x000416E8 File Offset: 0x0003F8E8
		protected override void InternalInitialize()
		{
			if (base.InitVal == null)
			{
				return;
			}
			this.tempWav = base.InitVal;
			this.filename = this.tempWav.FilePath;
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "TempFilePrompt successfully intialized with filename {0}.", new object[]
			{
				this.tempWav.FilePath
			});
		}

		// Token: 0x04000AFC RID: 2812
		private string filename;

		// Token: 0x04000AFD RID: 2813
		private ITempWavFile tempWav;
	}
}

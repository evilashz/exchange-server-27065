using System;
using System.Globalization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001E3 RID: 483
	internal class SpokenNamePrompt : VariablePrompt<object>
	{
		// Token: 0x06000E39 RID: 3641 RVA: 0x000403C3 File Offset: 0x0003E5C3
		public SpokenNamePrompt()
		{
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x000403CB File Offset: 0x0003E5CB
		internal SpokenNamePrompt(string promptName, CultureInfo culture, object value) : base(promptName, culture, value)
		{
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x000403D6 File Offset: 0x0003E5D6
		public override string ToString()
		{
			return this.compositePrompt.ToString();
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x000403E3 File Offset: 0x0003E5E3
		internal override string ToSsml()
		{
			return this.compositePrompt.ToSsml();
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x000403F0 File Offset: 0x0003E5F0
		protected override void InternalInitialize()
		{
			if (base.InitVal is string)
			{
				this.compositePrompt = new NamePrompt();
				((VariablePrompt<string>)this.compositePrompt).Initialize(base.Config, base.Culture, (string)base.InitVal);
				return;
			}
			if (base.InitVal is ITempWavFile)
			{
				this.compositePrompt = new TempFilePrompt();
				((VariablePrompt<ITempWavFile>)this.compositePrompt).Initialize(base.Config, base.Culture, (ITempWavFile)base.InitVal);
				return;
			}
			if (base.InitVal is DisambiguatedName)
			{
				this.compositePrompt = new DisambiguatedNamePrompt();
				((VariablePrompt<DisambiguatedName>)this.compositePrompt).Initialize(base.Config, base.Culture, (DisambiguatedName)base.InitVal);
				return;
			}
			if (base.InitVal == null)
			{
				this.compositePrompt = new TextPrompt();
				((VariablePrompt<string>)this.compositePrompt).Initialize(base.Config, base.Culture, string.Empty);
				return;
			}
			throw new ArgumentException(base.InitVal.GetType().ToString());
		}

		// Token: 0x04000AD8 RID: 2776
		private Prompt compositePrompt;
	}
}

using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000084 RID: 132
	internal class CardinalPrompt : VariablePrompt<int>
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x00019B94 File Offset: 0x00017D94
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"cardinal",
				base.Config.PromptName,
				string.Empty,
				this.digits.ToString(CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00019BE9 File Offset: 0x00017DE9
		internal override string ToSsml()
		{
			return this.ssmlString;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00019BF4 File Offset: 0x00017DF4
		protected override void InternalInitialize()
		{
			this.digits = base.InitVal;
			string text = Path.Combine(Util.WavPathFromCulture(base.Culture), string.Format(CultureInfo.InvariantCulture, "Cardinal-{0}.wav", new object[]
			{
				this.digits
			}));
			if (File.Exists(text))
			{
				this.ssmlString = string.Format(CultureInfo.InvariantCulture, "<audio src=\"{0}\" />", new object[]
				{
					text
				});
			}
			else
			{
				this.ssmlString = this.AddProsodyWithVolume(string.Format(CultureInfo.InvariantCulture, "<say-as type=\"number:cardinal\">{0}</say-as>", new object[]
				{
					this.digits
				}));
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "CardinalPrompt successfully intialized text {0}.", new object[]
			{
				this.digits
			});
		}

		// Token: 0x04000260 RID: 608
		internal const string SSMLTemplate = "<say-as type=\"number:cardinal\">{0}</say-as>";

		// Token: 0x04000261 RID: 609
		internal const string RecordedFileTemplate = "Cardinal-{0}.wav";

		// Token: 0x04000262 RID: 610
		private int digits;

		// Token: 0x04000263 RID: 611
		private string ssmlString;
	}
}

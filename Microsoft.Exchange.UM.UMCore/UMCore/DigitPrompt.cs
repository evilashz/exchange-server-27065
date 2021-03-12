using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000117 RID: 279
	internal class DigitPrompt : VariablePrompt<string>
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0001FCAC File Offset: 0x0001DEAC
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x0001FCB4 File Offset: 0x0001DEB4
		protected string Digits
		{
			get
			{
				return this.digits;
			}
			set
			{
				this.digits = value;
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"digit",
				base.Config.PromptName,
				string.Empty,
				this.digits.ToString()
			});
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001FD10 File Offset: 0x0001DF10
		internal override string ToSsml()
		{
			return this.ssmlString;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001FD18 File Offset: 0x0001DF18
		protected override void InternalInitialize()
		{
			this.digits = base.InitVal;
			StringBuilder stringBuilder = new StringBuilder();
			string text = this.digits;
			int i = 0;
			while (i < text.Length)
			{
				char c = text[i];
				string path = string.Empty;
				if (char.IsDigit(c) || c == 'A' || c == 'B' || c == 'C' || c == 'D')
				{
					path = string.Format(CultureInfo.InvariantCulture, "Digit-{0}.wav", new object[]
					{
						c
					});
					goto IL_C9;
				}
				if ('*' == c)
				{
					path = string.Format(CultureInfo.InvariantCulture, "Digit-{0}.wav", new object[]
					{
						"Star"
					});
					goto IL_C9;
				}
				if ('#' == c)
				{
					path = string.Format(CultureInfo.InvariantCulture, "Digit-{0}.wav", new object[]
					{
						"Pound"
					});
					goto IL_C9;
				}
				IL_12C:
				i++;
				continue;
				IL_C9:
				string text2 = Path.Combine(Util.WavPathFromCulture(base.Culture), path);
				if (File.Exists(text2))
				{
					stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "<audio src=\"{0}\" />", new object[]
					{
						text2
					}));
					goto IL_12C;
				}
				stringBuilder.Append(this.AddProsodyWithVolume(" " + c + " "));
				goto IL_12C;
			}
			this.ssmlString = stringBuilder.ToString();
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "DigitPrompt successfully intialized text {0}.", new object[]
			{
				this.digits
			});
		}

		// Token: 0x04000852 RID: 2130
		internal const string RecordedFileTemplate = "Digit-{0}.wav";

		// Token: 0x04000853 RID: 2131
		private string digits;

		// Token: 0x04000854 RID: 2132
		private string ssmlString;
	}
}

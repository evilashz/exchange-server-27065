using System;
using System.Text;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002D3 RID: 723
	internal class Options
	{
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0006014D File Offset: 0x0005E34D
		// (set) Token: 0x060014A4 RID: 5284 RVA: 0x00060155 File Offset: 0x0005E355
		internal bool DiagnosticsEnabled { get; private set; }

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0006015E File Offset: 0x0005E35E
		// (set) Token: 0x060014A6 RID: 5286 RVA: 0x00060166 File Offset: 0x0005E366
		internal bool ExpandTree { get; private set; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0006016F File Offset: 0x0005E36F
		// (set) Token: 0x060014A8 RID: 5288 RVA: 0x00060177 File Offset: 0x0005E377
		internal bool SearchAsRecip { get; private set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x00060180 File Offset: 0x0005E380
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x00060188 File Offset: 0x0005E388
		internal string ServerHint { get; private set; }

		// Token: 0x060014AB RID: 5291 RVA: 0x00060191 File Offset: 0x0005E391
		internal Options(bool diagnosticsEnabled, bool expandTree, bool searchAsRecip, string serverHint)
		{
			this.DiagnosticsEnabled = diagnosticsEnabled;
			this.ExpandTree = expandTree;
			this.SearchAsRecip = searchAsRecip;
			this.ServerHint = serverHint;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x000601B8 File Offset: 0x0005E3B8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(20);
			if (this.DiagnosticsEnabled)
			{
				stringBuilder.Append("GetPerfDiagnostics");
			}
			if (this.ExpandTree)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(';');
				}
				stringBuilder.Append("ExpandTree");
			}
			if (this.SearchAsRecip)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(';');
				}
				stringBuilder.Append("SearchAsRecip");
			}
			if (!string.IsNullOrEmpty(this.ServerHint))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(';');
				}
				stringBuilder.Append("ServerHint=");
				stringBuilder.Append(this.ServerHint);
			}
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00060278 File Offset: 0x0005E478
		internal static Options GetOptions(string optionsString)
		{
			if (string.IsNullOrEmpty(optionsString))
			{
				return Options.noOptions;
			}
			string[] array = optionsString.Split(new char[]
			{
				';'
			});
			bool diagnosticsEnabled = false;
			bool expandTree = false;
			bool searchAsRecip = false;
			string text = null;
			foreach (string text2 in array)
			{
				if (string.Equals(text2, "GetPerfDiagnostics"))
				{
					diagnosticsEnabled = true;
				}
				else if (string.Equals(text2, "ExpandTree"))
				{
					expandTree = true;
				}
				else if (string.Equals(text2, "SearchAsRecip"))
				{
					searchAsRecip = true;
				}
				else if (text2.StartsWith("ServerHint="))
				{
					text = text2.Substring("ServerHint=".Length, text2.Length - "ServerHint=".Length);
					if (string.IsNullOrEmpty(text))
					{
						text = null;
					}
				}
			}
			return new Options(diagnosticsEnabled, expandTree, searchAsRecip, text);
		}

		// Token: 0x04000D78 RID: 3448
		public const string GetPerfDiagnosticsOption = "GetPerfDiagnostics";

		// Token: 0x04000D79 RID: 3449
		public const string ExpandTreeOption = "ExpandTree";

		// Token: 0x04000D7A RID: 3450
		public const string SearchAsRecipOption = "SearchAsRecip";

		// Token: 0x04000D7B RID: 3451
		private const char Separator = ';';

		// Token: 0x04000D7C RID: 3452
		private const string ServerHintOption = "ServerHint=";

		// Token: 0x04000D7D RID: 3453
		private static Options noOptions = new Options(false, false, false, null);
	}
}

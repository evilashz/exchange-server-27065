using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.ExSetup
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConsoleInteractionHandler : SetupInteractionHandler
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002947 File Offset: 0x00000B47
		internal ConsoleInteractionHandler()
		{
			this.leftStatusFieldStart = this.GetConsoleWidth() - this.StatusFieldLength - 1;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002964 File Offset: 0x00000B64
		private int GetConsoleWidth()
		{
			int result = 80;
			try
			{
				result = Console.WindowWidth;
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002990 File Offset: 0x00000B90
		internal int CompletedStringLength
		{
			get
			{
				return Strings.ExecutionCompleted.ToString().Length;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000029B5 File Offset: 0x00000BB5
		internal int StatusFieldLength
		{
			get
			{
				return this.CompletedStringLength + 4 + 4;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000029C1 File Offset: 0x00000BC1
		internal int ActivityLength
		{
			get
			{
				return this.GetConsoleWidth() - 4 - this.StatusFieldLength;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000029D4 File Offset: 0x00000BD4
		public override void ReportWarning(WarningReportEventArgs e)
		{
			base.ReportWarning(e);
			int num = 0;
			Console.WriteLine();
			num++;
			int cursorTop = Console.CursorTop;
			string text = string.Format("{0}{1}", " ", e.WarningMessage);
			Console.WriteLine(text);
			num++;
			if (!string.IsNullOrEmpty(e.HelpUrl))
			{
				string value = string.Format("{0}{1} {2}", " ", Strings.HelpUrlHeaderText, e.HelpUrl);
				Console.WriteLine(value);
				num++;
			}
			this.AdjustTopProgressBar(this.CalculateLines(text) + num, cursorTop);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002A60 File Offset: 0x00000C60
		public override void ReportProgress(ProgressReportEventArgs e)
		{
			base.ReportProgress(e);
			if (!this.inProgress)
			{
				this.topProgressBar = Console.CursorTop;
				this.inProgress = true;
			}
			if (e.ProgressRecord.RecordType == ProgressRecordType.Processing)
			{
				if (base.IsNewActivity)
				{
					string activity = e.ProgressRecord.Activity;
					this.ShowWorkUnit(activity);
				}
				if (e.ProgressRecord.Activity.CompareTo(" ") != 0)
				{
					this.ShowProgress(e.ProgressRecord.PercentComplete);
					return;
				}
			}
			else
			{
				this.CompleteProgress();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public override void ReportErrors(ErrorReportEventArgs e)
		{
			base.ReportErrors(e);
			this.hasErrors = true;
			int num = 0;
			Console.Error.WriteLine();
			num++;
			int cursorTop = Console.CursorTop;
			string text = string.Format("{0}{1}", "     ", e.ErrorRecord);
			Console.Error.WriteLine(text);
			num++;
			if (e.ErrorRecord.ErrorDetails != null && !string.IsNullOrEmpty(e.ErrorRecord.ErrorDetails.RecommendedAction))
			{
				string value = string.Format("{0}{1} {2}", "     ", Strings.HelpUrlHeaderText, e.ErrorRecord.ErrorDetails.RecommendedAction);
				Console.Error.WriteLine(value);
				num++;
			}
			this.AdjustTopProgressBar(this.CalculateLines(text) + num, cursorTop);
			e.Handled = true;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public override void ReportException(Exception e)
		{
			base.ReportException(e);
			this.hasErrors = true;
			this.CompleteProgress();
			int cursorTop = Console.CursorTop;
			string text = "     " + ((e.InnerException != null) ? e.InnerException.Message : string.Empty);
			Console.Error.WriteLine(text);
			this.AdjustTopProgressBar(this.CalculateLines(text) + 2, cursorTop);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002C1B File Offset: 0x00000E1B
		private void CompleteProgress()
		{
			this.inProgress = false;
			this.ShowProgress(100);
			Console.WriteLine();
			this.hasErrors = false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C38 File Offset: 0x00000E38
		private void ShowWorkUnit(string workUnit)
		{
			int cursorLeft = Console.CursorLeft;
			int cursorTop = Console.CursorTop;
			if (this.topProgressBar < 0)
			{
				return;
			}
			Console.SetCursorPosition(4, this.topProgressBar);
			int cursorTop2 = Console.CursorTop;
			Console.Write(this.WrapText(workUnit, this.ActivityLength));
			if (Console.CursorTop != cursorTop2)
			{
				Console.WriteLine();
				Console.SetCursorPosition(4, Console.CursorTop);
				Console.Write(Strings.ProgressSummary);
				this.topProgressBar = Console.CursorTop;
				Console.SetCursorPosition(cursorLeft, this.topProgressBar);
				return;
			}
			Console.SetCursorPosition(cursorLeft, cursorTop);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002CC8 File Offset: 0x00000EC8
		private void ShowProgress(int percent)
		{
			int cursorLeft = Console.CursorLeft;
			int cursorTop = Console.CursorTop;
			if (this.topProgressBar < 0)
			{
				return;
			}
			Console.SetCursorPosition(this.leftStatusFieldStart, this.topProgressBar);
			string text = "";
			if (this.inProgress)
			{
				text = text + percent + "%";
			}
			else if (this.hasErrors)
			{
				text = Strings.ExecutionFailed;
			}
			else
			{
				text = Strings.ExecutionCompleted;
			}
			this.PaintProgress(percent, text);
			Console.SetCursorPosition(cursorLeft, cursorTop);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002D4D File Offset: 0x00000F4D
		public void PaintProgress(int percent, string status)
		{
			Console.SetCursorPosition(this.leftStatusFieldStart, Console.CursorTop);
			status = status.PadRight(this.StatusFieldLength, ' ').Substring(0, this.StatusFieldLength);
			Console.Write(status);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002D84 File Offset: 0x00000F84
		private int CalculateLines(string output)
		{
			string[] array = output.Split(new char[]
			{
				'\n'
			});
			int num = 0;
			foreach (string text in array)
			{
				num += (int)Math.Ceiling((double)((float)(text.Length + 1) / (float)Console.BufferWidth));
			}
			return num;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002DDE File Offset: 0x00000FDE
		private void AdjustTopProgressBar(int lines, int startingPosition)
		{
			if (Console.CursorTop == Console.BufferHeight - 1)
			{
				this.topProgressBar = this.topProgressBar - lines + Console.BufferHeight - startingPosition;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002E04 File Offset: 0x00001004
		public override string WrapText(string text)
		{
			int consoleWidth = this.GetConsoleWidth();
			return this.WrapText(text, consoleWidth);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002E28 File Offset: 0x00001028
		private string WrapText(string text, int maxLength)
		{
			if (text.Length == 0)
			{
				return text;
			}
			if (text.Trim().Length == 0)
			{
				return text;
			}
			string value = "".PadLeft(Console.CursorLeft + text.Length - text.TrimStart(new char[]
			{
				' '
			}).Length, ' ');
			StringBuilder stringBuilder = new StringBuilder(64);
			string[] array = text.Split(new char[]
			{
				' ',
				'\r',
				'\t'
			}, StringSplitOptions.RemoveEmptyEntries);
			StringBuilder stringBuilder2 = new StringBuilder(64);
			foreach (string text2 in array)
			{
				if (stringBuilder2.Length + text2.Length + 1 > maxLength)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(value);
					}
					stringBuilder.Append(stringBuilder2.ToString());
					stringBuilder2.Clear();
				}
				if (stringBuilder2.Length > 0)
				{
					stringBuilder2.Append(" ");
				}
				stringBuilder2.Append(text2);
			}
			if (stringBuilder2.Length > 0)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("\n");
					stringBuilder.Append(value);
				}
				stringBuilder.Append(stringBuilder2.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000008 RID: 8
		private const int leftActivity = 4;

		// Token: 0x04000009 RID: 9
		private const int statusFieldRightPad = 4;

		// Token: 0x0400000A RID: 10
		private const int statusFieldLeftPad = 4;

		// Token: 0x0400000B RID: 11
		private const string indent = " ";

		// Token: 0x0400000C RID: 12
		private const string messageHeaderIndent = "     ";

		// Token: 0x0400000D RID: 13
		private int leftStatusFieldStart;

		// Token: 0x0400000E RID: 14
		private int topProgressBar;

		// Token: 0x0400000F RID: 15
		private bool inProgress;

		// Token: 0x04000010 RID: 16
		private bool hasErrors;
	}
}

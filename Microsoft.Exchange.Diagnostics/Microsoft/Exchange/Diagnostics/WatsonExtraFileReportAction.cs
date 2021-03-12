using System;
using System.IO;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000D3 RID: 211
	internal class WatsonExtraFileReportAction : WatsonReportAction
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x00018FC1 File Offset: 0x000171C1
		public WatsonExtraFileReportAction(string filename) : base(filename, false)
		{
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00018FCB File Offset: 0x000171CB
		public override string ActionName
		{
			get
			{
				return "Attached File";
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00018FE4 File Offset: 0x000171E4
		public override string Evaluate(WatsonReport watsonReport)
		{
			string result;
			try
			{
				watsonReport.PerformWerOperation(delegate(WerSafeHandle reportHandle)
				{
					DiagnosticsNativeMethods.WerReportAddFile(reportHandle, base.Expression, DiagnosticsNativeMethods.WER_FILE_TYPE.WerFileTypeOther, (DiagnosticsNativeMethods.WER_FILE_FLAGS)0U);
				});
				watsonReport.LogExtraFile(base.Expression);
				result = "Attached file \"" + Path.GetFileName(base.Expression) + "\" to report.";
			}
			catch (Exception ex)
			{
				watsonReport.LogExtraFile(this.FormatError(base.Expression, ex));
				result = this.FormatError(base.Expression, ex);
			}
			return result;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00019068 File Offset: 0x00017268
		private string FormatError(string filename, Exception ex)
		{
			return string.Concat(new string[]
			{
				"Error attaching \"",
				base.Expression,
				"\" to report: ",
				ex.GetType().Name,
				" (",
				ex.Message,
				")"
			});
		}
	}
}

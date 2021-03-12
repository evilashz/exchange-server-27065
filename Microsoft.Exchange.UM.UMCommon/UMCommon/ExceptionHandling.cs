using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000072 RID: 114
	internal class ExceptionHandling
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x0000EC7D File Offset: 0x0000CE7D
		public static void MapAndReportGrayExceptions(GrayException.UserCodeDelegate tryCode)
		{
			ExceptionHandling.MapAndReportGrayExceptions(tryCode, null);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000ED10 File Offset: 0x0000CF10
		public static void MapAndReportGrayExceptions(GrayException.UserCodeDelegate tryCode, GrayException.IsGrayExceptionDelegate isGrayExceptionDelegate)
		{
			ExceptionHandling.<>c__DisplayClass4 CS$<>8__locals1 = new ExceptionHandling.<>c__DisplayClass4();
			CS$<>8__locals1.tryCode = tryCode;
			CS$<>8__locals1.isGrayExceptionDelegate = isGrayExceptionDelegate;
			ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<MapAndReportGrayExceptions>b__0)), new FilterDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<MapAndReportGrayExceptions>b__1)), new CatchDelegate(null, (UIntPtr)ldftn(ExceptionCatcher)));
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000ED5A File Offset: 0x0000CF5A
		internal static void SendWatsonWithExtraData(Exception e, bool fatal)
		{
			ExceptionHandling.SendWatsonWithExtraData(e, null, fatal);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000ED64 File Offset: 0x0000CF64
		internal static void SendWatsonWithExtraData(Exception e, string fileName, bool fatal)
		{
			using (ITempFile tempFile = Breadcrumbs.GenerateDump())
			{
				using (ITempFile tempFile2 = ProcessLog.GenerateDump())
				{
					if (fileName != null)
					{
						ExWatson.TryAddExtraFile(fileName);
					}
					ExWatson.TryAddExtraFile(tempFile.FilePath);
					ExWatson.TryAddExtraFile(tempFile2.FilePath);
					ExWatson.SendReport(e, fatal ? ReportOptions.ReportTerminateAfterSend : ReportOptions.None, null);
				}
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
		internal static void SendWatsonWithoutDump(Exception e)
		{
			ExWatson.SendReport(e, ReportOptions.DoNotCollectDumps, null);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000EDF0 File Offset: 0x0000CFF0
		internal static void SendWatsonWithoutDumpWithExtraData(Exception e)
		{
			using (ITempFile tempFile = Breadcrumbs.GenerateDump())
			{
				using (ITempFile tempFile2 = ProcessLog.GenerateDump())
				{
					ExWatson.TryAddExtraFile(tempFile.FilePath);
					ExWatson.TryAddExtraFile(tempFile2.FilePath);
					ExWatson.SendReport(e, ReportOptions.DoNotCollectDumps, null);
				}
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000EE60 File Offset: 0x0000D060
		private static bool ExceptionFilter(object exception, GrayException.IsGrayExceptionDelegate isGrayExceptionDelegate)
		{
			Exception ex = exception as Exception;
			if (ex == null)
			{
				return false;
			}
			if (!isGrayExceptionDelegate(ex))
			{
				return false;
			}
			ExceptionHandling.SendWatsonWithExtraData(ex, false);
			return true;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000EE8E File Offset: 0x0000D08E
		private static void ExceptionCatcher(object exception)
		{
			throw new UMGrayException((Exception)exception);
		}
	}
}

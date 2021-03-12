using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000D1 RID: 209
	internal class WatsonExternalProcessReport : WatsonExceptionReport
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x00018DDF File Offset: 0x00016FDF
		public WatsonExternalProcessReport(Process process, string eventType, Exception exception, string detailedExceptionInformation, ReportOptions reportOptions) : base(eventType, process, exception, reportOptions)
		{
			this.callstack = exception.StackTrace;
			this.detailedExceptionInformation = detailedExceptionInformation;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00018E00 File Offset: 0x00017000
		protected override ProcSafeHandle GetProcessHandle()
		{
			if (base.IsProcessValid)
			{
				return base.GetProcessHandle();
			}
			return new ProcSafeHandle();
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00018E18 File Offset: 0x00017018
		protected override void WriteSpecializedPartOfTextReport(TextWriter reportFile)
		{
			base.WriteReportFileHeader(reportFile, "Manifest Report");
			reportFile.WriteLine("P0(appVersion)={0}", WatsonReport.GetValidString(base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AppVersion)));
			reportFile.WriteLine("P1(appName)={0}", WatsonReport.GetValidString(base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AppName)));
			reportFile.WriteLine("P2(exMethodName)={0}", WatsonReport.GetValidString(base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.ExMethodName)));
			reportFile.WriteLine("P3(exceptionType)={0}", WatsonReport.GetValidString(base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.ExceptionType)));
			reportFile.WriteLine("P4(callstackHash)={0}", base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.CallstackHash));
			WatsonExternalProcessReport.WriteReportFileCallStack(reportFile, this.callstack);
			WatsonExternalProcessReport.WriteReportFileDetailedExceptionInformation(reportFile, this.detailedExceptionInformation);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00018EB7 File Offset: 0x000170B7
		protected override void PrepareBucketingParameters()
		{
			base.PrepareBucketingParameters();
			base.SetBucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AssemblyVer, "unknown");
			base.SetBucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AssemblyName, "unknown");
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00018ED7 File Offset: 0x000170D7
		private static void WriteReportFileCallStack(TextWriter reportFile, string callStack)
		{
			if (!string.IsNullOrEmpty(callStack))
			{
				reportFile.WriteLine(reportFile.NewLine);
				reportFile.WriteLine("----------------------------------------------------");
				reportFile.WriteLine("-------------------- Call Stack --------------------");
				reportFile.WriteLine("----------------------------------------------------");
				reportFile.WriteLine(callStack);
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00018F15 File Offset: 0x00017115
		private static void WriteReportFileDetailedExceptionInformation(TextWriter reportFile, string detailedExceptionInformation)
		{
			if (!string.IsNullOrEmpty(detailedExceptionInformation))
			{
				reportFile.WriteLine(reportFile.NewLine);
				reportFile.WriteLine("----------------------------------------------------");
				reportFile.WriteLine("---------------- Detailed Information --------------");
				reportFile.WriteLine("----------------------------------------------------");
				reportFile.WriteLine(detailedExceptionInformation);
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00018F54 File Offset: 0x00017154
		protected override void WriteReportTypeSpecificSection(XmlWriter reportFile)
		{
			base.WriteReportTypeSpecificSection(reportFile);
			using (SafeXmlTag safeXmlTag = new SafeXmlTag(reportFile, "detailed-info"))
			{
				safeXmlTag.SetContent(this.detailedExceptionInformation);
			}
		}

		// Token: 0x04000441 RID: 1089
		private readonly string callstack;

		// Token: 0x04000442 RID: 1090
		private readonly string detailedExceptionInformation;
	}
}

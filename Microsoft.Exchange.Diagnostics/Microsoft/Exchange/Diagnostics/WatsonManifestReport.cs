using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000C6 RID: 198
	internal class WatsonManifestReport : WatsonReport
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x000171D2 File Offset: 0x000153D2
		public WatsonManifestReport(string eventType, Process process) : base(eventType, process)
		{
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000171DC File Offset: 0x000153DC
		protected override WatsonIssueType GetIssueTypeCode()
		{
			return WatsonIssueType.GenericReport;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000171DF File Offset: 0x000153DF
		protected override string GetIssueDetails()
		{
			return base.BucketingParameter<WatsonReport.BucketParamId>(WatsonReport.BucketParamId.ExceptionType);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000171E8 File Offset: 0x000153E8
		protected override void WriteReportTypeSpecificSection(XmlWriter reportFile)
		{
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000171EC File Offset: 0x000153EC
		protected override void WriteSpecializedPartOfTextReport(TextWriter reportFile)
		{
			reportFile.WriteLine("P1(flavor)={0}", base.BucketingParameter<WatsonReport.BucketParamId>(WatsonReport.BucketParamId.Flavor));
			reportFile.WriteLine("P2(appVersion)={0}", base.BucketingParameter<WatsonReport.BucketParamId>(WatsonReport.BucketParamId.AppVersion));
			reportFile.WriteLine("P3(appName)={0}", base.BucketingParameter<WatsonReport.BucketParamId>(WatsonReport.BucketParamId.AppName));
			reportFile.WriteLine("P4(assemblyName)={0}", base.BucketingParameter<WatsonReport.BucketParamId>(WatsonReport.BucketParamId.AssemblyName));
			reportFile.WriteLine("P5(exMethodName)={0}", base.BucketingParameter<WatsonReport.BucketParamId>(WatsonReport.BucketParamId.ExMethodName));
			reportFile.WriteLine("P6(exceptionType)={0}", base.BucketingParameter<WatsonReport.BucketParamId>(WatsonReport.BucketParamId.ExceptionType));
			reportFile.WriteLine("P7(callstackHash)={0}", base.BucketingParameter<WatsonReport.BucketParamId>(WatsonReport.BucketParamId.CallstackHash));
			reportFile.WriteLine("P8(assemblyVer)={0}", base.BucketingParameter<WatsonReport.BucketParamId>(WatsonReport.BucketParamId.AssemblyVer));
			reportFile.WriteLine();
		}
	}
}

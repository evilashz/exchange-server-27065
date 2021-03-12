using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000D6 RID: 214
	internal class WatsonHangReport : WatsonExceptionReport
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x000196D8 File Offset: 0x000178D8
		public WatsonHangReport(string eventType, Process hungProcess, Exception exception) : base(eventType, hungProcess, exception, ReportOptions.ReportTerminateAfterSend | ReportOptions.DoNotFreezeThreads)
		{
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000196E5 File Offset: 0x000178E5
		protected override ProcSafeHandle GetProcessHandle()
		{
			if (base.IsProcessValid)
			{
				return base.GetProcessHandle();
			}
			return new ProcSafeHandle();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000196FC File Offset: 0x000178FC
		protected override void PrepareBucketingParameters()
		{
			string s = "unknown";
			if (base.Exception != null)
			{
				s = base.Exception.GetType().ToString();
			}
			base.SetBucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.ExceptionType, WatsonReport.GetValidString(s));
			base.SetBucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AssemblyName, "unknown");
			base.SetBucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AssemblyVer, "unknown");
			base.SetBucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.ExMethodName, "unknown");
			base.SetBucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.CallstackHash, "0");
		}
	}
}

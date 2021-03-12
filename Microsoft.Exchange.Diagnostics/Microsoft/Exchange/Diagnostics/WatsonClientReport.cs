using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000C7 RID: 199
	internal class WatsonClientReport : WatsonManifestReport
	{
		// Token: 0x060005B0 RID: 1456 RVA: 0x00017290 File Offset: 0x00015490
		static WatsonClientReport()
		{
			WatsonClientReport.bucketingParamNames[0] = "serverFlavor";
			WatsonClientReport.bucketingParamNames[1] = "exVersion";
			WatsonClientReport.bucketingParamNames[2] = "appName";
			WatsonClientReport.bucketingParamNames[3] = "traceComponent";
			WatsonClientReport.bucketingParamNames[4] = "function";
			WatsonClientReport.bucketingParamNames[5] = "exceptionType";
			WatsonClientReport.bucketingParamNames[6] = "callstackHash";
			WatsonClientReport.bucketingParamNames[7] = "filename";
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00017308 File Offset: 0x00015508
		public WatsonClientReport(string traceComponent, string function, string exceptionMessage, string exceptionType, string originalCallStack, string callStack, int callStackHash, string fileName) : base("E12IE", null)
		{
			this.traceComponent = traceComponent;
			this.function = function;
			this.exceptionType = exceptionType;
			this.fileName = fileName;
			StringBuilder stringBuilder = new StringBuilder(exceptionType.Length + callStack.Length + 2);
			stringBuilder.AppendLine(exceptionType);
			stringBuilder.Append(callStack);
			this.reportCallStack = stringBuilder.ToString();
			this.callStackHash = callStackHash;
			StringBuilder stringBuilder2 = new StringBuilder(exceptionMessage.Length + exceptionType.Length + originalCallStack.Length + 4);
			stringBuilder2.AppendLine(exceptionMessage);
			stringBuilder2.AppendLine(exceptionType);
			stringBuilder2.Append(originalCallStack);
			this.detailedExceptionInformation = stringBuilder2.ToString();
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x000173D0 File Offset: 0x000155D0
		public string DetailedExceptionInformation
		{
			get
			{
				return this.detailedExceptionInformation;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000173D8 File Offset: 0x000155D8
		internal string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x000173E0 File Offset: 0x000155E0
		internal string ReportCallStack
		{
			get
			{
				return this.reportCallStack;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000173E8 File Offset: 0x000155E8
		internal static string[] BuildWatsonParameters(string flavor, string version, string traceComponent, string functionName, string exceptionType, string callstack, int callStackHash)
		{
			return new string[]
			{
				WatsonReport.GetValidString(flavor),
				WatsonReport.GetValidString(version),
				"OWAClient",
				WatsonReport.GetValidString(traceComponent),
				WatsonReport.GetValidString(functionName),
				WatsonReport.GetValidString(exceptionType),
				WatsonClientReport.GetStringHashFromString(callStackHash)
			};
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00017440 File Offset: 0x00015640
		internal string[] GetWatsonParameters()
		{
			this.PrepareBucketingParameters();
			return new string[]
			{
				base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.Flavor),
				base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.ExVersion),
				"OWAClient",
				base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.TraceComponent),
				base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.Function),
				base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.ExceptionType),
				base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.CallstackHash)
			};
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001749F File Offset: 0x0001569F
		protected override string GetShortParameter(uint bucketParamId, string longParameter)
		{
			if (bucketParamId == 4U && Uri.IsWellFormedUriString(longParameter, UriKind.RelativeOrAbsolute))
			{
				return longParameter.Trim();
			}
			return WatsonReport.GetShortParameter(longParameter.Trim());
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000174C0 File Offset: 0x000156C0
		protected override string[] GetBucketingParamNames()
		{
			return WatsonClientReport.bucketingParamNames;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000174C8 File Offset: 0x000156C8
		protected override void PrepareBucketingParameters()
		{
			if (this.bucketingParametersPrepared)
			{
				return;
			}
			base.PrepareBucketingParameters();
			base.SetBucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.ExVersion, this.version);
			base.SetBucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.AppName, "OWAClient");
			base.SetBucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.TraceComponent, this.traceComponent);
			base.SetBucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.Function, this.function);
			base.SetBucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.ExceptionType, this.exceptionType);
			base.SetBucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.CallstackHash, WatsonClientReport.GetStringHashFromString(this.callStackHash));
			base.SetBucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.FileName, string.Empty);
			this.bucketingParametersPrepared = true;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00017549 File Offset: 0x00015749
		protected override WatsonIssueType GetIssueTypeCode()
		{
			return WatsonIssueType.ScriptError;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001754C File Offset: 0x0001574C
		protected override void WriteReportTypeSpecificSection(XmlWriter reportFile)
		{
			using (new SafeXmlTag(reportFile, "client-report"))
			{
				using (SafeXmlTag safeXmlTag2 = new SafeXmlTag(reportFile, "callstack"))
				{
					safeXmlTag2.SetContent(this.ReportCallStack);
				}
				using (SafeXmlTag safeXmlTag3 = new SafeXmlTag(reportFile, "detailed-info"))
				{
					safeXmlTag3.SetContent(this.DetailedExceptionInformation);
				}
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000175E4 File Offset: 0x000157E4
		protected override void WriteSpecializedPartOfTextReport(TextWriter reportFile)
		{
			base.WriteReportFileHeader(reportFile, "Manifest Report: Non Fatal Error for OWA");
			reportFile.WriteLine("P1(flavor)={0}", WatsonReport.GetValidString(base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.Flavor)));
			reportFile.WriteLine("P2(exVersion)={0}", WatsonReport.GetValidString(base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.ExVersion)));
			reportFile.WriteLine("P3(appName)={0}", "OWAClient");
			reportFile.WriteLine("P4(traceComponent)={0}", WatsonReport.GetValidString(base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.TraceComponent)));
			reportFile.WriteLine("P5(function)={0}", WatsonReport.GetValidString(base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.Function)));
			reportFile.WriteLine("P6(exceptionType)={0}", WatsonReport.GetValidString(base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.ExceptionType)));
			reportFile.WriteLine("P7(callstackHash)={0}", base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.CallstackHash));
			reportFile.WriteLine("filename={0}", WatsonReport.GetValidString(this.fileName));
			WatsonClientReport.WriteReportFileClientCallStack(reportFile, this.ReportCallStack);
			WatsonClientReport.WriteReportFileClientDetailedExceptionInformation(reportFile, this.DetailedExceptionInformation);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000176C0 File Offset: 0x000158C0
		protected override StringBuilder GetArchivedReportName()
		{
			string text = base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.Function);
			string text2 = base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.TraceComponent);
			string text3 = base.BucketingParameter<WatsonClientReport.BucketParamId>(WatsonClientReport.BucketParamId.CallstackHash);
			StringBuilder stringBuilder = new StringBuilder(text.Length + text2.Length + text3.Length + "ExWatsonReport.xml".Length + 3);
			stringBuilder.Append(text);
			stringBuilder.Append('-');
			stringBuilder.Append(text2);
			stringBuilder.Append('-');
			stringBuilder.Append(text3);
			stringBuilder.Append('-');
			stringBuilder.Append("ExWatsonReport.xml");
			return stringBuilder;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001774C File Offset: 0x0001594C
		private static string GetStringHashFromString(int hash)
		{
			return Convert.ToString(hash & 65535, 16);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001775C File Offset: 0x0001595C
		private static void WriteReportFileClientCallStack(TextWriter reportFile, string callStack)
		{
			if (callStack != null)
			{
				reportFile.WriteLine(reportFile.NewLine);
				reportFile.WriteLine("----------------------------------------------------");
				reportFile.WriteLine("-------------------- Call Stack --------------------");
				reportFile.WriteLine("----------------------------------------------------");
				reportFile.WriteLine(callStack);
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00017795 File Offset: 0x00015995
		private static void WriteReportFileClientDetailedExceptionInformation(TextWriter reportFile, string detailedExceptionInformation)
		{
			if (detailedExceptionInformation != null)
			{
				reportFile.WriteLine(reportFile.NewLine);
				reportFile.WriteLine("----------------------------------------------------");
				reportFile.WriteLine("---------------- Detailed Information --------------");
				reportFile.WriteLine("----------------------------------------------------");
				reportFile.WriteLine(detailedExceptionInformation);
			}
		}

		// Token: 0x040003F7 RID: 1015
		private const string OwaClientAppName = "OWAClient";

		// Token: 0x040003F8 RID: 1016
		private static readonly string[] bucketingParamNames = new string[8];

		// Token: 0x040003F9 RID: 1017
		private readonly string version = WatsonReport.ExchangeFormattedVersion(ExWatson.ApplicationVersion);

		// Token: 0x040003FA RID: 1018
		private readonly string fileName;

		// Token: 0x040003FB RID: 1019
		private readonly string exceptionType;

		// Token: 0x040003FC RID: 1020
		private readonly string reportCallStack;

		// Token: 0x040003FD RID: 1021
		private readonly int callStackHash;

		// Token: 0x040003FE RID: 1022
		private readonly string function;

		// Token: 0x040003FF RID: 1023
		private readonly string traceComponent;

		// Token: 0x04000400 RID: 1024
		private readonly string detailedExceptionInformation;

		// Token: 0x04000401 RID: 1025
		private bool bucketingParametersPrepared;

		// Token: 0x020000C8 RID: 200
		internal new enum BucketParamId
		{
			// Token: 0x04000403 RID: 1027
			Flavor,
			// Token: 0x04000404 RID: 1028
			ExVersion,
			// Token: 0x04000405 RID: 1029
			AppName,
			// Token: 0x04000406 RID: 1030
			TraceComponent,
			// Token: 0x04000407 RID: 1031
			Function,
			// Token: 0x04000408 RID: 1032
			ExceptionType,
			// Token: 0x04000409 RID: 1033
			CallstackHash,
			// Token: 0x0400040A RID: 1034
			FileName,
			// Token: 0x0400040B RID: 1035
			_Count
		}
	}
}

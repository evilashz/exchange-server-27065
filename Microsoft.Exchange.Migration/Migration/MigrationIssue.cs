using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000015 RID: 21
	internal abstract class MigrationIssue : ServiceIssue
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000036D7 File Offset: 0x000018D7
		public MigrationIssue(string errorClass, string organization, string jobName, string error) : base(errorClass)
		{
			this.Organization = organization;
			this.JobName = jobName;
			this.MigrationError = error;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000036F6 File Offset: 0x000018F6
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000036FE File Offset: 0x000018FE
		public string Organization { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003707 File Offset: 0x00001907
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000370F File Offset: 0x0000190F
		public string JobName { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003718 File Offset: 0x00001918
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003720 File Offset: 0x00001920
		public string MigrationError { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003729 File Offset: 0x00001929
		public override string IdentifierString
		{
			get
			{
				return string.Format("{0}-{1}-{2}", base.Error, this.Organization, this.JobName);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003748 File Offset: 0x00001948
		public override XElement GetDiagnosticInfo(SICDiagnosticArgument arguments)
		{
			XElement diagnosticInfo = base.GetDiagnosticInfo(arguments);
			diagnosticInfo.Add(new object[]
			{
				new XElement("Organization", this.Organization),
				new XElement("JobName", this.JobName),
				new XElement("MigrationError", this.MigrationError)
			});
			return diagnosticInfo;
		}
	}
}

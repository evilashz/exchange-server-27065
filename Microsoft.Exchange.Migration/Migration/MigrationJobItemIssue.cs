using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000019 RID: 25
	internal class MigrationJobItemIssue : MigrationIssue
	{
		// Token: 0x0600008C RID: 140 RVA: 0x000052B1 File Offset: 0x000034B1
		public MigrationJobItemIssue(MigrationJobItem jobItem) : base("JobItemIssue", jobItem.TenantName, jobItem.JobName, jobItem.StatusData.InternalError)
		{
			this.Identity = jobItem.Identifier;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000052E1 File Offset: 0x000034E1
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000052E9 File Offset: 0x000034E9
		public string Identity { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000052F2 File Offset: 0x000034F2
		public override string IdentifierString
		{
			get
			{
				return string.Format("{0}-{1}", base.IdentifierString, this.Identity);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000530C File Offset: 0x0000350C
		public override XElement GetDiagnosticInfo(SICDiagnosticArgument arguments)
		{
			XElement diagnosticInfo = base.GetDiagnosticInfo(arguments);
			diagnosticInfo.Add(new XElement("Identity", this.Identity));
			return diagnosticInfo;
		}

		// Token: 0x04000033 RID: 51
		public const string ErrorClass = "JobItemIssue";
	}
}

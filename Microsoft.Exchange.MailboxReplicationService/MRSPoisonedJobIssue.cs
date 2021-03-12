using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000045 RID: 69
	internal class MRSPoisonedJobIssue : ServiceIssue
	{
		// Token: 0x060003AA RID: 938 RVA: 0x000174B8 File Offset: 0x000156B8
		public MRSPoisonedJobIssue(MoveJob job) : base("PoisonedJob")
		{
			this.MoveJob = job;
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003AB RID: 939 RVA: 0x000174CC File Offset: 0x000156CC
		// (set) Token: 0x060003AC RID: 940 RVA: 0x000174D4 File Offset: 0x000156D4
		public MoveJob MoveJob { get; private set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003AD RID: 941 RVA: 0x000174DD File Offset: 0x000156DD
		public override string IdentifierString
		{
			get
			{
				return string.Format("{0}-{1}", "PoisonedJob", this.MoveJob.RequestGuid);
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00017500 File Offset: 0x00015700
		public override XElement GetDiagnosticInfo(SICDiagnosticArgument arguments)
		{
			XElement diagnosticInfo = base.GetDiagnosticInfo(arguments);
			diagnosticInfo.Add(new object[]
			{
				new XElement("ExchangeGuid", this.MoveJob.ExchangeGuid),
				new XElement("RequestGuid", this.MoveJob.RequestGuid),
				new XElement("TargetDatabaseGuid", this.MoveJob.TargetDatabaseGuid),
				new XElement("RequestType", this.MoveJob.RequestType),
				new XElement("PoisonCount", this.MoveJob.PoisonCount),
				new XElement("FailureType", this.MoveJob.FailureType),
				new XElement("Status", this.MoveJob.Status)
			});
			return diagnosticInfo;
		}

		// Token: 0x04000177 RID: 375
		private const string ErrorClass = "PoisonedJob";
	}
}

using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000159 RID: 345
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DiagnosableServiceIssue : ServiceIssue
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x00024F94 File Offset: 0x00023194
		public DiagnosableServiceIssue(IDiagnosableObject diagnosableObject, string error) : base(error)
		{
			this.DiagnosableObject = diagnosableObject;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00024FA4 File Offset: 0x000231A4
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x00024FAC File Offset: 0x000231AC
		private IDiagnosableObject DiagnosableObject { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00024FB5 File Offset: 0x000231B5
		public override string IdentifierString
		{
			get
			{
				return string.Format("{0} : {1}", this.DiagnosableObject.GetType().ToString(), this.DiagnosableObject.HashableIdentity);
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00024FDC File Offset: 0x000231DC
		public override void DeriveFromIssue(ServiceIssue issue)
		{
			base.DeriveFromIssue(issue);
			DiagnosableServiceIssue diagnosableServiceIssue = issue as DiagnosableServiceIssue;
			this.DiagnosableObject = diagnosableServiceIssue.DiagnosableObject;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00025004 File Offset: 0x00023204
		public override XElement GetDiagnosticInfo(SICDiagnosticArgument arguments)
		{
			XElement diagnosticInfo = base.GetDiagnosticInfo(arguments);
			diagnosticInfo.Add(this.DiagnosableObject.GetDiagnosticInfo(null));
			return diagnosticInfo;
		}
	}
}

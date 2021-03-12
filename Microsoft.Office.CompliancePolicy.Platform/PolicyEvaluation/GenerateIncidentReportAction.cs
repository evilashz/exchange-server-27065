using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000A8 RID: 168
	public class GenerateIncidentReportAction : NotifyActionBase
	{
		// Token: 0x0600044E RID: 1102 RVA: 0x0000D764 File Offset: 0x0000B964
		public GenerateIncidentReportAction(List<Argument> arguments, string externalName = null) : base(arguments, externalName)
		{
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000D76E File Offset: 0x0000B96E
		public override string Name
		{
			get
			{
				return "GenerateIncidentReport";
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000D775 File Offset: 0x0000B975
		public override Version MinimumVersion
		{
			get
			{
				return GenerateIncidentReportAction.minVersion;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000D77C File Offset: 0x0000B97C
		protected ReadOnlyDictionary<string, KeyValuePair<string, string>> DefaultSubjectBodyTable
		{
			get
			{
				return GenerateIncidentReportAction.defaultSubjectBodyTable;
			}
		}

		// Token: 0x040002B5 RID: 693
		private static readonly Version minVersion = new Version("1.00.0002.000");

		// Token: 0x040002B6 RID: 694
		private static ReadOnlyDictionary<string, KeyValuePair<string, string>> defaultSubjectBodyTable = new ReadOnlyDictionary<string, KeyValuePair<string, string>>(new Dictionary<string, KeyValuePair<string, string>>
		{
			{
				"en",
				new KeyValuePair<string, string>("toBeDefinedSubject-GenerateIncidentReport", "toBeDefinedBody-GenerateIncidentReport")
			}
		});
	}
}

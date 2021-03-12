using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000FC RID: 252
	[Serializable]
	public sealed class AdditionalDiagnostics
	{
		// Token: 0x060006C8 RID: 1736 RVA: 0x00014AEB File Offset: 0x00012CEB
		public AdditionalDiagnostics()
		{
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00014AF4 File Offset: 0x00012CF4
		public AdditionalDiagnostics(string server, Exception exception)
		{
			this.Server = server;
			int num = 0;
			List<ExceptionRecord> list = null;
			while (exception != null && num < 10)
			{
				num++;
				if (list == null)
				{
					list = new List<ExceptionRecord>();
				}
				list.Add(new ExceptionRecord(exception));
				exception = exception.InnerException;
			}
			if (list != null)
			{
				this.ExceptionRecords = list.ToArray();
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00014B4C File Offset: 0x00012D4C
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x00014B54 File Offset: 0x00012D54
		public string Server { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00014B5D File Offset: 0x00012D5D
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x00014B65 File Offset: 0x00012D65
		public ExceptionRecord[] ExceptionRecords { get; set; }

		// Token: 0x060006CE RID: 1742 RVA: 0x00014B70 File Offset: 0x00012D70
		public string Serialize()
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(AdditionalDiagnostics));
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				xmlSerializer.Serialize(stringWriter, this);
				stringWriter.Flush();
				result = stringWriter.ToString();
			}
			return result;
		}
	}
}

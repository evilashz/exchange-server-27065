using System;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x0200038A RID: 906
	[Serializable]
	public class DlpDetailReport : DetailObject
	{
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x00087A85 File Offset: 0x00085C85
		// (set) Token: 0x06001F6E RID: 8046 RVA: 0x00087A8D File Offset: 0x00085C8D
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		public string Organization { get; private set; }

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x00087A96 File Offset: 0x00085C96
		// (set) Token: 0x06001F70 RID: 8048 RVA: 0x00087A9E File Offset: 0x00085C9E
		[DalConversion("DefaultSerializer", "PolicyMatchTime", new string[]
		{

		})]
		public DateTime Date { get; private set; }

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x00087AA7 File Offset: 0x00085CA7
		// (set) Token: 0x06001F72 RID: 8050 RVA: 0x00087AAF File Offset: 0x00085CAF
		[DalConversion("DefaultSerializer", "Title", new string[]
		{

		})]
		[Redact]
		public string Title { get; private set; }

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x00087AB8 File Offset: 0x00085CB8
		// (set) Token: 0x06001F74 RID: 8052 RVA: 0x00087AC0 File Offset: 0x00085CC0
		[DalConversion("DefaultSerializer", "Location", new string[]
		{

		})]
		public string Location { get; private set; }

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x00087AC9 File Offset: 0x00085CC9
		// (set) Token: 0x06001F76 RID: 8054 RVA: 0x00087AD1 File Offset: 0x00085CD1
		[DalConversion("DefaultSerializer", "Severity", new string[]
		{

		})]
		public string Severity { get; private set; }

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x00087ADA File Offset: 0x00085CDA
		// (set) Token: 0x06001F78 RID: 8056 RVA: 0x00087AE2 File Offset: 0x00085CE2
		[DalConversion("DefaultSerializer", "Size", new string[]
		{

		})]
		public long Size { get; private set; }

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x00087AEB File Offset: 0x00085CEB
		// (set) Token: 0x06001F7A RID: 8058 RVA: 0x00087AF3 File Offset: 0x00085CF3
		[ODataInput("Source")]
		[DalConversion("DefaultSerializer", "DataSource", new string[]
		{

		})]
		public string Source { get; private set; }

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x00087AFC File Offset: 0x00085CFC
		// (set) Token: 0x06001F7C RID: 8060 RVA: 0x00087B04 File Offset: 0x00085D04
		[ODataInput("Actor")]
		[Redact]
		[DalConversion("DefaultSerializer", "Actor", new string[]
		{

		})]
		public string Actor { get; private set; }

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x00087B0D File Offset: 0x00085D0D
		// (set) Token: 0x06001F7E RID: 8062 RVA: 0x00087B15 File Offset: 0x00085D15
		[DalConversion("DefaultSerializer", "PolicyName", new string[]
		{

		})]
		[ODataInput("DlpPolicy")]
		public string DlpPolicy { get; private set; }

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x00087B1E File Offset: 0x00085D1E
		// (set) Token: 0x06001F80 RID: 8064 RVA: 0x00087B26 File Offset: 0x00085D26
		[ODataInput("TransportRule")]
		[DalConversion("DefaultSerializer", "TransportRuleName", new string[]
		{

		})]
		public string TransportRule { get; private set; }

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x00087B2F File Offset: 0x00085D2F
		// (set) Token: 0x06001F82 RID: 8066 RVA: 0x00087B37 File Offset: 0x00085D37
		[DalConversion("DefaultSerializer", "OverrideType", new string[]
		{

		})]
		[Redact]
		public string UserAction { get; private set; }

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x00087B40 File Offset: 0x00085D40
		// (set) Token: 0x06001F84 RID: 8068 RVA: 0x00087B48 File Offset: 0x00085D48
		[Redact]
		[DalConversion("DefaultSerializer", "OverrideJustification", new string[]
		{

		})]
		public string Justification { get; private set; }

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x00087B51 File Offset: 0x00085D51
		// (set) Token: 0x06001F86 RID: 8070 RVA: 0x00087B59 File Offset: 0x00085D59
		[DalConversion("DefaultSerializer", "DataClassification", new string[]
		{

		})]
		[Redact]
		public string SensitiveInformationType { get; private set; }

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x00087B62 File Offset: 0x00085D62
		// (set) Token: 0x06001F88 RID: 8072 RVA: 0x00087B6A File Offset: 0x00085D6A
		[DalConversion("DefaultSerializer", "ClassificationCount", new string[]
		{

		})]
		public int SensitiveInformationCount { get; private set; }

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x00087B73 File Offset: 0x00085D73
		// (set) Token: 0x06001F8A RID: 8074 RVA: 0x00087B7B File Offset: 0x00085D7B
		[DalConversion("DefaultSerializer", "ClassificationConfidence", new string[]
		{

		})]
		public int SensitiveInformationConfidence { get; private set; }

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x00087B84 File Offset: 0x00085D84
		// (set) Token: 0x06001F8C RID: 8076 RVA: 0x00087B8C File Offset: 0x00085D8C
		[ODataInput("EventType")]
		[DalConversion("DefaultSerializer", "EventType", new string[]
		{

		})]
		public string EventType { get; private set; }

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001F8D RID: 8077 RVA: 0x00087B95 File Offset: 0x00085D95
		// (set) Token: 0x06001F8E RID: 8078 RVA: 0x00087B9D File Offset: 0x00085D9D
		[DalConversion("DefaultSerializer", "Action", new string[]
		{

		})]
		[ODataInput("Action")]
		public string Action { get; private set; }

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001F8F RID: 8079 RVA: 0x00087BA6 File Offset: 0x00085DA6
		// (set) Token: 0x06001F90 RID: 8080 RVA: 0x00087BAE File Offset: 0x00085DAE
		[DalConversion("DefaultSerializer", "ObjectId", new string[]
		{

		})]
		public Guid ObjectId { get; private set; }
	}
}

using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000A1 RID: 161
	[Serializable]
	public class QueryResponse
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0001962C File Offset: 0x0001782C
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x00019634 File Offset: 0x00017834
		public string ObjectClass { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0001963D File Offset: 0x0001783D
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00019645 File Offset: 0x00017845
		public string Result { get; set; }

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001964E File Offset: 0x0001784E
		public QueryResponse()
		{
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00019656 File Offset: 0x00017856
		public QueryResponse(string objectClass, string result)
		{
			this.ObjectClass = objectClass;
			this.Result = result;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001966C File Offset: 0x0001786C
		public static QueryResponse CreateError(string message)
		{
			return new QueryResponse("Error", message);
		}
	}
}

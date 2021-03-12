using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000011 RID: 17
	public class LiveOperationException : LocalizedException
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003060 File Offset: 0x00001260
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003068 File Offset: 0x00001268
		public string AdditionalWatsonData { get; set; }

		// Token: 0x06000059 RID: 89 RVA: 0x00003071 File Offset: 0x00001271
		public LiveOperationException(COMException e, uint errorCode) : base(Strings.LiveOperationExceptionMessage(errorCode, Enum.GetName(typeof(RPSErrorCode), errorCode) ?? string.Empty), e)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000030A0 File Offset: 0x000012A0
		public override string ToString()
		{
			if (string.IsNullOrWhiteSpace(this.AdditionalWatsonData))
			{
				return base.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString());
			stringBuilder.AppendLine().AppendLine();
			stringBuilder.AppendLine("AdditionalWatsonData: ");
			stringBuilder.Append("\t").AppendLine(this.AdditionalWatsonData);
			return stringBuilder.ToString();
		}
	}
}

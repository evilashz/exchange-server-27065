using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000014 RID: 20
	public class LiveExternalHRESULTException : LiveExternalException
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003163 File Offset: 0x00001363
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000316B File Offset: 0x0000136B
		public string AdditionalWatsonData { get; set; }

		// Token: 0x0600005F RID: 95 RVA: 0x00003174 File Offset: 0x00001374
		public LiveExternalHRESULTException(COMException e, uint errorCode) : base(Strings.LiveExternalHRESULTExceptionMessage(errorCode, Enum.GetName(typeof(RPSErrorCode), errorCode) ?? string.Empty), e)
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000031A4 File Offset: 0x000013A4
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

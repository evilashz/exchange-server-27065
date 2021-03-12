using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001D3 RID: 467
	internal class UMPartnerFaxStatus
	{
		// Token: 0x0600104C RID: 4172 RVA: 0x00031992 File Offset: 0x0002FB92
		protected UMPartnerFaxStatus(string status)
		{
			this.Status = status;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x000319A1 File Offset: 0x0002FBA1
		// (set) Token: 0x0600104E RID: 4174 RVA: 0x000319A9 File Offset: 0x0002FBA9
		public string Status { get; set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000319B2 File Offset: 0x0002FBB2
		// (set) Token: 0x06001050 RID: 4176 RVA: 0x000319BA File Offset: 0x0002FBBA
		public bool MissedCall { get; set; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x000319C3 File Offset: 0x0002FBC3
		// (set) Token: 0x06001052 RID: 4178 RVA: 0x000319CB File Offset: 0x0002FBCB
		public bool IsCompleteFax { get; set; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x000319D4 File Offset: 0x0002FBD4
		// (set) Token: 0x06001054 RID: 4180 RVA: 0x000319DC File Offset: 0x0002FBDC
		public FaxResultType Type { get; set; }

		// Token: 0x06001055 RID: 4181 RVA: 0x000319E8 File Offset: 0x0002FBE8
		public static bool TryParse(string status, out UMPartnerFaxStatus faxStatus)
		{
			faxStatus = null;
			if (string.IsNullOrEmpty(status))
			{
				return false;
			}
			Regex regex = new Regex(UMPartnerFaxStatus.CompleteFaxStatusFormat, RegexOptions.Compiled);
			Regex regex2 = new Regex(UMPartnerFaxStatus.IncompleteFaxStatusFormat, RegexOptions.Compiled);
			Regex regex3 = new Regex(UMPartnerFaxStatus.CancelledFaxStatusFormat, RegexOptions.Compiled);
			Regex regex4 = new Regex(UMPartnerFaxStatus.ServerErrorFaxStatusFormat, RegexOptions.Compiled);
			faxStatus = new UMPartnerFaxStatus(status);
			bool result = true;
			if (regex.Match(status).Success)
			{
				faxStatus.Type = FaxResultType.CompleteFax;
				faxStatus.MissedCall = false;
				faxStatus.IsCompleteFax = true;
			}
			else if (regex2.Match(status).Success)
			{
				faxStatus.Type = FaxResultType.IncompleteFax;
				faxStatus.MissedCall = false;
				faxStatus.IsCompleteFax = false;
			}
			else if (regex3.Match(status).Success)
			{
				faxStatus.Type = FaxResultType.CancelledFax;
				faxStatus.MissedCall = true;
			}
			else if (regex4.Match(status).Success)
			{
				faxStatus.Type = FaxResultType.ServerErrorFax;
				faxStatus.MissedCall = true;
			}
			else
			{
				faxStatus.Type = FaxResultType.None;
				result = false;
			}
			return result;
		}

		// Token: 0x040009B2 RID: 2482
		private static readonly string CompleteFaxStatusFormat = "\\b2.0";

		// Token: 0x040009B3 RID: 2483
		private static readonly string IncompleteFaxStatusFormat = "\\b2.6";

		// Token: 0x040009B4 RID: 2484
		private static readonly string CancelledFaxStatusFormat = "\\b2.4";

		// Token: 0x040009B5 RID: 2485
		private static readonly string ServerErrorFaxStatusFormat = "\\b5.0";
	}
}

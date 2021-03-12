using System;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200001D RID: 29
	public class MigratedUserLiveIdLogonException : OrgIdLogonException
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003461 File Offset: 0x00001661
		protected override string ErrorMessageFormatString
		{
			get
			{
				return Strings.MigratedUserLiveIdLogonErrorMessage;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003468 File Offset: 0x00001668
		public override Strings.IDs ErrorMessageStringId
		{
			get
			{
				return 1799660809;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000346F File Offset: 0x0000166F
		public MigratedUserLiveIdLogonException(string userName, string logoutUrl) : base(userName, logoutUrl)
		{
		}
	}
}

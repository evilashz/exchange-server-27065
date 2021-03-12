using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200045C RID: 1116
	public class MessageOptionsDialog : OwaPage
	{
		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06002956 RID: 10582 RVA: 0x000E9770 File Offset: 0x000E7970
		protected static int ImportanceLow
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x000E9773 File Offset: 0x000E7973
		protected static int ImportanceNormal
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06002958 RID: 10584 RVA: 0x000E9776 File Offset: 0x000E7976
		protected static int ImportanceHigh
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x000E9779 File Offset: 0x000E7979
		protected static int SensitivityNormal
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x0600295A RID: 10586 RVA: 0x000E977C File Offset: 0x000E797C
		protected static int SensitivityPersonal
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x000E977F File Offset: 0x000E797F
		protected static int SensitivityPrivate
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x0600295C RID: 10588 RVA: 0x000E9782 File Offset: 0x000E7982
		protected static int SensitivityCompanyConfidential
		{
			get
			{
				return 3;
			}
		}
	}
}

using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200013D RID: 317
	public class EacRbacPrincipal : IEacRbacPrincipal
	{
		// Token: 0x0600210A RID: 8458 RVA: 0x00064067 File Offset: 0x00062267
		private EacRbacPrincipal()
		{
		}

		// Token: 0x17001A55 RID: 6741
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x0006406F File Offset: 0x0006226F
		// (set) Token: 0x0600210C RID: 8460 RVA: 0x00064076 File Offset: 0x00062276
		public static IEacRbacPrincipal Instance { get; internal set; } = new EacRbacPrincipal();

		// Token: 0x17001A56 RID: 6742
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x0006407E File Offset: 0x0006227E
		public ADObjectId ExecutingUserId
		{
			get
			{
				return RbacPrincipal.Current.ExecutingUserId;
			}
		}

		// Token: 0x17001A57 RID: 6743
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x0006408A File Offset: 0x0006228A
		public string Name
		{
			get
			{
				return RbacPrincipal.Current.Name;
			}
		}

		// Token: 0x17001A58 RID: 6744
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x00064096 File Offset: 0x00062296
		public SmtpAddress ExecutingUserPrimarySmtpAddress
		{
			get
			{
				return LocalSession.Current.ExecutingUserPrimarySmtpAddress;
			}
		}

		// Token: 0x17001A59 RID: 6745
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x000640A2 File Offset: 0x000622A2
		public ExTimeZone UserTimeZone
		{
			get
			{
				return RbacPrincipal.Current.UserTimeZone;
			}
		}

		// Token: 0x17001A5A RID: 6746
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x000640AE File Offset: 0x000622AE
		public string DateFormat
		{
			get
			{
				return RbacPrincipal.Current.DateFormat;
			}
		}

		// Token: 0x17001A5B RID: 6747
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x000640BA File Offset: 0x000622BA
		public string TimeFormat
		{
			get
			{
				return RbacPrincipal.Current.TimeFormat;
			}
		}
	}
}

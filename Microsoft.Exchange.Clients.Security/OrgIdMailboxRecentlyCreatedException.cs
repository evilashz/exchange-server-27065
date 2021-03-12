using System;
using System.Globalization;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200001C RID: 28
	public class OrgIdMailboxRecentlyCreatedException : OrgIdMailboxNotFoundException
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003360 File Offset: 0x00001560
		protected override string ErrorMessageFormatString
		{
			get
			{
				int hoursBetweenAccountCreationAndNow = this.HoursBetweenAccountCreationAndNow;
				if (hoursBetweenAccountCreationAndNow != 1)
				{
					return Strings.MailboxRecentlyCreatedErrorMessageMoreThanOneHour;
				}
				return Strings.MailboxRecentlyCreatedErrorMessageOneHour;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003383 File Offset: 0x00001583
		public override ErrorMode? ErrorMode
		{
			get
			{
				return new ErrorMode?(Microsoft.Exchange.Clients.Common.ErrorMode.MailboxNotReady);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000338C File Offset: 0x0000158C
		public override Strings.IDs ErrorMessageStringId
		{
			get
			{
				int hoursBetweenAccountCreationAndNow = this.HoursBetweenAccountCreationAndNow;
				if (hoursBetweenAccountCreationAndNow != 1)
				{
					return -870357301;
				}
				return -1420330575;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000033B0 File Offset: 0x000015B0
		public override string Message
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, this.ErrorMessageFormatString, new object[]
				{
					base.UserName,
					base.LogoutLink,
					this.HoursBetweenAccountCreationAndNow.ToString()
				});
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000033F8 File Offset: 0x000015F8
		public int HoursBetweenAccountCreationAndNow
		{
			get
			{
				if (this.creationTimeSpanToNow.TotalHours < 1.0)
				{
					return 1;
				}
				if (this.creationTimeSpanToNow.TotalHours > 23.0)
				{
					return 23;
				}
				return (int)Math.Round(this.creationTimeSpanToNow.TotalHours);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003450 File Offset: 0x00001650
		public OrgIdMailboxRecentlyCreatedException(string userName, string logoutUrl, TimeSpan creationTimeSpanToNow) : base(userName, logoutUrl)
		{
			this.creationTimeSpanToNow = creationTimeSpanToNow;
		}

		// Token: 0x04000040 RID: 64
		private readonly TimeSpan creationTimeSpanToNow;
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B6B RID: 2923
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Offer
	{
		// Token: 0x06003E88 RID: 16008 RVA: 0x000A32FC File Offset: 0x000A14FC
		public static Offer Find(string claim)
		{
			foreach (Offer offer in Offer.offers)
			{
				if (StringComparer.OrdinalIgnoreCase.Equals(claim, offer.Name))
				{
					return offer;
				}
			}
			return null;
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06003E89 RID: 16009 RVA: 0x000A333B File Offset: 0x000A153B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x000A3343 File Offset: 0x000A1543
		public TimeSpan Duration
		{
			get
			{
				return this.duration;
			}
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x000A334B File Offset: 0x000A154B
		public static bool Equals(Offer a, Offer b)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(a.Name, b.Name);
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x000A3364 File Offset: 0x000A1564
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Name=",
				this.name,
				",Duration=",
				this.duration.TotalSeconds,
				"(secs)"
			});
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x000A33B2 File Offset: 0x000A15B2
		private Offer(string name, TimeSpan duration)
		{
			this.name = name;
			this.duration = duration;
		}

		// Token: 0x04003670 RID: 13936
		public static readonly Offer SharingInviteMessage = new Offer("MSExchange.SharingInviteMessage", TimeSpan.FromDays(15.0));

		// Token: 0x04003671 RID: 13937
		public static readonly Offer SharingCalendarFreeBusy = new Offer("MSExchange.SharingCalendarFreeBusy", TimeSpan.FromMinutes(5.0));

		// Token: 0x04003672 RID: 13938
		public static readonly Offer SharingRead = new Offer("MSExchange.SharingRead", TimeSpan.FromMinutes(60.0));

		// Token: 0x04003673 RID: 13939
		public static readonly Offer MailboxMove = new Offer("MSExchange.MailboxMove", TimeSpan.FromHours(1.0));

		// Token: 0x04003674 RID: 13940
		public static readonly Offer Autodiscover = new Offer("MSExchange.Autodiscover", TimeSpan.FromHours(8.0));

		// Token: 0x04003675 RID: 13941
		public static readonly Offer XropLogon = new Offer("MSExchange.XropLogon", TimeSpan.FromHours(8.0));

		// Token: 0x04003676 RID: 13942
		public static readonly Offer Messenger = new Offer("Messenger.SignIn", TimeSpan.FromHours(8.0));

		// Token: 0x04003677 RID: 13943
		public static readonly Offer IPCCertificationSTS = new Offer("MSRMS.CertificationWS", TimeSpan.FromHours(48.0));

		// Token: 0x04003678 RID: 13944
		public static readonly Offer IPCServerLicensingSTS = new Offer("MSRMS.ServerLicensingWS", TimeSpan.FromHours(48.0));

		// Token: 0x04003679 RID: 13945
		public static readonly Offer MailTips = new Offer("MSExchange.MailTips", TimeSpan.FromMinutes(5.0));

		// Token: 0x0400367A RID: 13946
		public static readonly Offer MailboxSearch = new Offer("MSExchange.MailboxSearch", TimeSpan.FromHours(8.0));

		// Token: 0x0400367B RID: 13947
		public static readonly Offer UserPhotoRetrieval = new Offer("MSExchange.UserPhotoRetrieval", TimeSpan.FromMinutes(10.0));

		// Token: 0x0400367C RID: 13948
		private string name;

		// Token: 0x0400367D RID: 13949
		private TimeSpan duration;

		// Token: 0x0400367E RID: 13950
		internal static readonly Offer[] offers = new Offer[]
		{
			Offer.SharingInviteMessage,
			Offer.SharingCalendarFreeBusy,
			Offer.SharingRead,
			Offer.MailboxMove,
			Offer.Autodiscover,
			Offer.MailTips,
			Offer.XropLogon,
			Offer.MailboxSearch,
			Offer.UserPhotoRetrieval
		};
	}
}

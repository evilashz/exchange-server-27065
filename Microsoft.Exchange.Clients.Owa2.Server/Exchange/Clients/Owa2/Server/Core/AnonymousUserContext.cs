using System;
using System.Globalization;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000092 RID: 146
	internal class AnonymousUserContext
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x00010580 File Offset: 0x0000E780
		private AnonymousUserContext(HttpContext context)
		{
			this.PublishingUrl = (PublishingUrl)context.Items["AnonymousUserContextPublishedUrl"];
			ExAssert.RetailAssert(this.PublishingUrl != null, "Missing Published Url");
			this.ExchangePrincipal = (ExchangePrincipal)context.Items["AnonymousUserContextExchangePrincipalKey"];
			ExAssert.RetailAssert(this.ExchangePrincipal != null, "Missing Exchange Principal");
			this.TimeZone = (ExTimeZone)context.Items["AnonymousUserContextTimeZoneKey"];
			ExAssert.RetailAssert(this.TimeZone != null, "Missing Timezone");
			this.PublishedCalendarName = (string)context.Items["AnonymousUserContextPublishedCalendarNameKey"];
			ExAssert.RetailAssert(this.PublishedCalendarName != null, "Missing Published Calendar Name");
			this.SharingDetail = (DetailLevelEnumType)context.Items["AnonymousUserContextSharingDetailsKey"];
			ExAssert.RetailAssert(this.SharingDetail.ToString().Length > 0, "Missing SharingDetail");
			this.PublishedCalendarId = (StoreObjectId)context.Items["AnonymousUserContextPublishedCalendarIdKey"];
			ExAssert.RetailAssert(this.PublishedCalendarId != null, "Missing PublishedCalendarId");
			this.UserAgent = OwaUserAgentUtilities.CreateUserAgentAnonymous(context);
			ExAssert.RetailAssert(this.PublishedCalendarId != null, "Missing UserAgent");
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x000106E8 File Offset: 0x0000E8E8
		internal static AnonymousUserContext Current
		{
			get
			{
				AnonymousUserContext anonymousUserContext = (AnonymousUserContext)HttpContext.Current.Items["AnonymousUserContext"];
				if (anonymousUserContext != null)
				{
					return anonymousUserContext;
				}
				lock (AnonymousUserContext.syncObject)
				{
					anonymousUserContext = (((AnonymousUserContext)HttpContext.Current.Items["AnonymousUserContext"]) ?? new AnonymousUserContext(HttpContext.Current));
					HttpContext.Current.Items["AnonymousUserContext"] = anonymousUserContext;
				}
				return anonymousUserContext;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00010780 File Offset: 0x0000E980
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x00010788 File Offset: 0x0000E988
		internal PublishingUrl PublishingUrl { get; private set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00010791 File Offset: 0x0000E991
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00010799 File Offset: 0x0000E999
		internal StoreObjectId PublishedCalendarId { get; private set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x000107A2 File Offset: 0x0000E9A2
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x000107AA File Offset: 0x0000E9AA
		internal DetailLevelEnumType SharingDetail { get; private set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x000107B3 File Offset: 0x0000E9B3
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x000107BB File Offset: 0x0000E9BB
		internal string PublishedCalendarName { get; private set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x000107C4 File Offset: 0x0000E9C4
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x000107CC File Offset: 0x0000E9CC
		internal ExchangePrincipal ExchangePrincipal { get; private set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000107D5 File Offset: 0x0000E9D5
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x000107DD File Offset: 0x0000E9DD
		internal ExTimeZone TimeZone { get; private set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x000107E6 File Offset: 0x0000E9E6
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x000107EE File Offset: 0x0000E9EE
		internal UserAgent UserAgent { get; private set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x000107F7 File Offset: 0x0000E9F7
		internal CultureInfo Culture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture;
			}
		}

		// Token: 0x04000316 RID: 790
		private const string AnonymousUserContextKey = "AnonymousUserContext";

		// Token: 0x04000317 RID: 791
		private const string AnonymousUserContextExchangePrincipalKey = "AnonymousUserContextExchangePrincipalKey";

		// Token: 0x04000318 RID: 792
		private const string AnonymousUserContextSharingDetailsKey = "AnonymousUserContextSharingDetailsKey";

		// Token: 0x04000319 RID: 793
		private const string AnonymousUserContextPublishedUrl = "AnonymousUserContextPublishedUrl";

		// Token: 0x0400031A RID: 794
		private const string AnonymousUserContextTimeZoneKey = "AnonymousUserContextTimeZoneKey";

		// Token: 0x0400031B RID: 795
		private const string AnonymousUserContextPublishedCalendarNameKey = "AnonymousUserContextPublishedCalendarNameKey";

		// Token: 0x0400031C RID: 796
		private const string AnonymousUserContextPublishedCalendarIdKey = "AnonymousUserContextPublishedCalendarIdKey";

		// Token: 0x0400031D RID: 797
		private static object syncObject = new object();
	}
}

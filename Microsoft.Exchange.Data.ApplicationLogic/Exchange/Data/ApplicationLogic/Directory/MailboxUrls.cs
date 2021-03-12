using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Data.ApplicationLogic.Directory
{
	// Token: 0x020000E6 RID: 230
	internal sealed class MailboxUrls : IMailboxUrls
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00025904 File Offset: 0x00023B04
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x0002590C File Offset: 0x00023B0C
		public string CalendarUrl { get; private set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00025915 File Offset: 0x00023B15
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x0002591D File Offset: 0x00023B1D
		public string EditGroupUrl { get; private set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00025926 File Offset: 0x00023B26
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x0002592E File Offset: 0x00023B2E
		public string EwsUrl { get; private set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00025937 File Offset: 0x00023B37
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x0002593F File Offset: 0x00023B3F
		public string InboxUrl { get; private set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00025948 File Offset: 0x00023B48
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x00025950 File Offset: 0x00023B50
		public string OwaUrl { get; private set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00025959 File Offset: 0x00023B59
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x00025961 File Offset: 0x00023B61
		public string PeopleUrl { get; private set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0002596A File Offset: 0x00023B6A
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x00025972 File Offset: 0x00023B72
		public string PhotoUrl { get; private set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0002597C File Offset: 0x00023B7C
		public bool IsFullyInitialized
		{
			get
			{
				return !string.IsNullOrEmpty(this.EditGroupUrl) && !string.IsNullOrEmpty(this.InboxUrl) && !string.IsNullOrEmpty(this.CalendarUrl) && !string.IsNullOrEmpty(this.PeopleUrl) && !string.IsNullOrEmpty(this.PhotoUrl);
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000259D0 File Offset: 0x00023BD0
		public MailboxUrls(IExchangePrincipal exchangePrincipal, bool failOnError = false)
		{
			string smtpAddress = exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			string owaUrl = MailboxUrls.GetOwaUrl(exchangePrincipal, failOnError);
			string ewsUrl = MailboxUrls.GetEwsUrl(exchangePrincipal, failOnError);
			this.InitializeUrls(smtpAddress, owaUrl, ewsUrl);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00025A16 File Offset: 0x00023C16
		public MailboxUrls(string smtpAddress, string owaUrl, string ewsUrl)
		{
			this.InitializeUrls(smtpAddress, owaUrl, ewsUrl);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00025A28 File Offset: 0x00023C28
		public static MailboxUrls GetOwaMailboxUrls(IExchangePrincipal exchangePrincipal)
		{
			string smtpAddress = exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			string owaUrl = MailboxUrls.GetOwaUrl(exchangePrincipal, true);
			return new MailboxUrls(smtpAddress, owaUrl, null);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00025A60 File Offset: 0x00023C60
		internal string[] ToExchangeResources()
		{
			return new string[]
			{
				"InboxUrl=" + this.InboxUrl,
				"CalendarUrl=" + this.CalendarUrl,
				"PeopleUrl=" + this.PeopleUrl,
				"PhotoUrl=" + this.PhotoUrl,
				"EditGroupUrl=" + this.EditGroupUrl
			};
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00025AD4 File Offset: 0x00023CD4
		internal Dictionary<string, string> ToExchangeResourcesDictionary()
		{
			return new Dictionary<string, string>
			{
				{
					"InboxUrl",
					this.InboxUrl
				},
				{
					"CalendarUrl",
					this.CalendarUrl
				},
				{
					"PeopleUrl",
					this.PeopleUrl
				},
				{
					"PhotoUrl",
					this.PhotoUrl
				},
				{
					"EditGroupUrl",
					this.EditGroupUrl
				}
			};
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00025B40 File Offset: 0x00023D40
		private void InitializeUrls(string smtpAddress, string owaUrl, string ewsUrl)
		{
			if (!string.IsNullOrEmpty(owaUrl))
			{
				this.OwaUrl = owaUrl;
				this.CalendarUrl = owaUrl + "?path=/group/" + smtpAddress + "/calendar";
				this.InboxUrl = owaUrl + "?path=/group/" + smtpAddress + "/mail";
				this.PeopleUrl = owaUrl + "?path=/group/" + smtpAddress + "/people";
				this.EditGroupUrl = owaUrl + "?path=/group/" + smtpAddress + "/action/edit";
			}
			if (!string.IsNullOrEmpty(ewsUrl))
			{
				this.EwsUrl = ewsUrl;
				HttpPhotoRequestBuilder httpPhotoRequestBuilder = new HttpPhotoRequestBuilder(MailboxUrls.PhotosConfiguration, MailboxUrls.Tracer);
				this.PhotoUrl = httpPhotoRequestBuilder.CreateUri(new Uri(ewsUrl), smtpAddress).AbsoluteUri;
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00025BF0 File Offset: 0x00023DF0
		private static string GetOwaUrl(IExchangePrincipal exchangePrincipal, bool failOnError)
		{
			try
			{
				Uri frontEndOwaUrl = FrontEndLocator.GetFrontEndOwaUrl(exchangePrincipal);
				return frontEndOwaUrl.ToString();
			}
			catch (LocalizedException ex)
			{
				MailboxUrls.Tracer.TraceWarning<string>(0L, "Not able to get the owa url by FrontEndLocator. Exception: {0}", ex.ToString());
				if (failOnError)
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00025C40 File Offset: 0x00023E40
		private static string GetEwsUrl(IExchangePrincipal exchangePrincipal, bool failOnError)
		{
			try
			{
				Uri frontEndWebServicesUrl = FrontEndLocator.GetFrontEndWebServicesUrl(exchangePrincipal);
				return frontEndWebServicesUrl.ToString();
			}
			catch (LocalizedException ex)
			{
				MailboxUrls.Tracer.TraceWarning<string>(0L, "Not able to get the ews url by FrontEndLocator. Exception: {0}", ex.ToString());
				if (failOnError)
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x04000483 RID: 1155
		private static readonly Trace Tracer = ExTraceGlobals.ModernGroupsTracer;

		// Token: 0x04000484 RID: 1156
		private static readonly PhotosConfiguration PhotosConfiguration = new PhotosConfiguration(ExchangeSetupContext.InstallPath);
	}
}

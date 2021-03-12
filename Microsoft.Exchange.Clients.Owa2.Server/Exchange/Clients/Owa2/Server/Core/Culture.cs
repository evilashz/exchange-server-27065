using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000103 RID: 259
	public static class Culture
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00020928 File Offset: 0x0001EB28
		public static List<CultureInfo> SupportedUMCultures
		{
			get
			{
				if (Culture.cacheUMCultures)
				{
					if (Culture.supportedUMDatacenterCultures == null)
					{
						lock (Culture.lockObject)
						{
							if (Culture.supportedUMDatacenterCultures == null)
							{
								Culture.supportedUMDatacenterCultures = new List<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackCultures(LanguagePackType.UnifiedMessaging));
							}
						}
					}
					return Culture.supportedUMDatacenterCultures;
				}
				return new List<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackCultures(LanguagePackType.UnifiedMessaging));
			}
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00020998 File Offset: 0x0001EB98
		public static bool IsCultureSpeechEnabled(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("Culture cannot be null");
			}
			return Culture.SupportedUMCultures.Contains(culture) && LocConfig.Instance[culture].MowaSpeech.EnableMowaVoice;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x000209CC File Offset: 0x0001EBCC
		public static CultureInfo GetUserCulture()
		{
			return CultureInfo.CurrentUICulture;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x000209D3 File Offset: 0x0001EBD3
		internal static void InternalSetThreadPreferredCulture()
		{
			Culture.InternalSetThreadPreferredCulture(CultureInfo.GetCultureInfo("en-US"));
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x000209E4 File Offset: 0x0001EBE4
		internal static void InternalSetThreadPreferredCulture(IExchangePrincipal exchangePrincipal)
		{
			Culture.InternalSetThreadPreferredCulture(Culture.GetPreferredCultureInfo(exchangePrincipal));
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000209F1 File Offset: 0x0001EBF1
		internal static void InternalSetThreadPreferredCulture(CultureInfo culture)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug<int>(0L, "Culture.InternalSetThreadCulture, LCID={0}", culture.LCID);
			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00020A20 File Offset: 0x0001EC20
		internal static void InternalSetAsyncThreadCulture(CultureInfo culture)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug<int>(0L, "Culture.InternalSetAsyncThreadCulture, LCID={0}", culture.LCID);
			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00020A4F File Offset: 0x0001EC4F
		internal static CultureInfo GetPreferredCultureInfo(IExchangePrincipal exchangePrincipal)
		{
			return ClientCultures.GetPreferredCultureInfo(exchangePrincipal.PreferredCultures);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00020A5C File Offset: 0x0001EC5C
		internal static CultureInfo GetPreferredCultureInfo(ADUser adUser)
		{
			return ClientCultures.GetPreferredCultureInfo(adUser.Languages);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00020A6C File Offset: 0x0001EC6C
		internal static void SetPreferredCulture(IExchangePrincipal exchangePrincipal, IEnumerable<CultureInfo> preferredCultures, IRecipientSession recipientSession)
		{
			ADUser aduser = recipientSession.Read(exchangePrincipal.ObjectId) as ADUser;
			if (aduser != null)
			{
				aduser.Languages.Clear();
				foreach (CultureInfo item in preferredCultures)
				{
					aduser.Languages.Add(item);
				}
				recipientSession.Save(aduser);
			}
		}

		// Token: 0x04000681 RID: 1665
		private static List<CultureInfo> supportedUMDatacenterCultures = null;

		// Token: 0x04000682 RID: 1666
		private static bool cacheUMCultures = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.CacheUMCultures.Enabled;

		// Token: 0x04000683 RID: 1667
		private static object lockObject = new object();
	}
}

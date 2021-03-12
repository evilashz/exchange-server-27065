using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200020A RID: 522
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PreferredCultureLocalizer
	{
		// Token: 0x060011B4 RID: 4532 RVA: 0x00039D32 File Offset: 0x00037F32
		internal PreferredCultureLocalizer(IExchangePrincipal userExchangePrincipal)
		{
			SyncUtilities.ThrowIfArgumentNull("userExchangePrincipal", userExchangePrincipal);
			this.InitializeState(userExchangePrincipal.PreferredCultures, PreferredCultureLocalizer.supportedCultures);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00039D56 File Offset: 0x00037F56
		internal PreferredCultureLocalizer(PreferredCultures preferredCultures, List<CultureInfo> supportedCultures)
		{
			SyncUtilities.ThrowIfArgumentNull("preferredCultures", preferredCultures);
			SyncUtilities.ThrowIfArgumentNull("supportedCultures", supportedCultures);
			this.InitializeState(preferredCultures, supportedCultures);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00039D7C File Offset: 0x00037F7C
		private PreferredCultureLocalizer()
		{
			this.cultureInfoToApply = null;
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00039D8B File Offset: 0x00037F8B
		public static PreferredCultureLocalizer DefaultThreadCulture
		{
			get
			{
				return PreferredCultureLocalizer.defaultThreadCulture;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00039D92 File Offset: 0x00037F92
		protected CultureInfo CultureInfoToApply
		{
			get
			{
				return this.cultureInfoToApply;
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00039D9A File Offset: 0x00037F9A
		public string Apply(LocalizedString localizedString)
		{
			if (this.CultureInfoToApply == null)
			{
				return localizedString.ToString();
			}
			return localizedString.ToString(this.CultureInfoToApply);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00039DC0 File Offset: 0x00037FC0
		private void InitializeState(IEnumerable<CultureInfo> preferredCultures, List<CultureInfo> supportedCultures)
		{
			if (!preferredCultures.Any<CultureInfo>())
			{
				this.cultureInfoToApply = null;
				return;
			}
			foreach (CultureInfo item in preferredCultures)
			{
				if (supportedCultures.Contains(item))
				{
					this.cultureInfoToApply = item;
					break;
				}
			}
		}

		// Token: 0x040009A5 RID: 2469
		private static readonly List<CultureInfo> supportedCultures = new List<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackSpecificCultures(LanguagePackType.Client));

		// Token: 0x040009A6 RID: 2470
		private static readonly PreferredCultureLocalizer defaultThreadCulture = new PreferredCultureLocalizer();

		// Token: 0x040009A7 RID: 2471
		private CultureInfo cultureInfoToApply;
	}
}

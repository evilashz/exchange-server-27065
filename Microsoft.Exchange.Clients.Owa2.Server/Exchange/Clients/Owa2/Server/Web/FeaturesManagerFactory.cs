using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000476 RID: 1142
	public class FeaturesManagerFactory
	{
		// Token: 0x060026C2 RID: 9922 RVA: 0x0008C76A File Offset: 0x0008A96A
		public FeaturesManagerFactory(MiniRecipient recipient, IConfigurationContext configurationContext, ScopeFlightsSettingsProvider scopeFlightsSettingsProvider) : this(recipient, configurationContext, scopeFlightsSettingsProvider, null, string.Empty, false)
		{
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x0008C77C File Offset: 0x0008A97C
		public FeaturesManagerFactory(MiniRecipient recipient, IConfigurationContext configurationContext, ScopeFlightsSettingsProvider scopeFlightsSettingsProvider, Func<VariantConfigurationSnapshot, IFeaturesStateOverride> featureStateOverrideFactory, string rampId, bool isFirstRelease)
		{
			this.recipient = recipient;
			this.configurationContext = configurationContext;
			this.scopeFlightsSettingsProvider = scopeFlightsSettingsProvider;
			this.featureStateOverrideFactory = featureStateOverrideFactory;
			this.rampId = rampId;
			this.isFirstRelease = isFirstRelease;
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x0008C7B4 File Offset: 0x0008A9B4
		public FeaturesManager GetFeaturesManager(HttpContext httpContext)
		{
			string flightsOverride = (httpContext == null) ? null : RequestDispatcherUtilities.GetStringUrlParameter(httpContext, "flights");
			HttpCookieCollection httpCookieCollection = (httpContext == null) ? null : httpContext.Request.Cookies;
			HttpCookie httpCookie = (httpCookieCollection == null) ? null : httpCookieCollection.Get("flights");
			if (httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value) && httpCookie.Value.Equals("none", StringComparison.InvariantCultureIgnoreCase))
			{
				flightsOverride = "none";
			}
			return this.GetFeaturesManagerInternal(flightsOverride);
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x0008C828 File Offset: 0x0008AA28
		protected virtual FeaturesManager CreateFeaturesManager()
		{
			IList<string> flightsFromQueryString = this.GetFlightsFromQueryString(this.currentFlightsOverride);
			VariantConfigurationSnapshot configurationSnapshot = string.IsNullOrWhiteSpace(this.rampId) ? VariantConfiguration.GetSnapshot(this.recipient.GetContext(null), null, flightsFromQueryString) : VariantConfiguration.GetSnapshot(this.recipient.GetContext(this.rampId, this.isFirstRelease), null, flightsFromQueryString);
			return FeaturesManager.Create(configurationSnapshot, this.configurationContext, this.featureStateOverrideFactory);
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x0008C8A0 File Offset: 0x0008AAA0
		private IList<string> GetFlightsFromQueryString(string flightsOverride)
		{
			if (flightsOverride == null)
			{
				return null;
			}
			List<string> list = new List<string>();
			string[] array = flightsOverride.Split(new char[]
			{
				',',
				' ',
				';'
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				if (ScopeFlightsSettingsProvider.IsLogicalScope(text))
				{
					list.AddRange(this.scopeFlightsSettingsProvider.GetFlightsForScope(text));
				}
				else
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x0008C90E File Offset: 0x0008AB0E
		private FeaturesManager GetFeaturesManagerInternal(string flightsOverride)
		{
			if (this.currentValue == null || flightsOverride != this.currentFlightsOverride)
			{
				this.currentFlightsOverride = flightsOverride;
				this.currentValue = this.CreateFeaturesManager();
			}
			return this.currentValue;
		}

		// Token: 0x04001699 RID: 5785
		public const string FlightCookieParameterName = "flights";

		// Token: 0x0400169A RID: 5786
		public const string FlightDisabledOverrideValue = "none";

		// Token: 0x0400169B RID: 5787
		private const string FlightQueryStringParameterName = "flights";

		// Token: 0x0400169C RID: 5788
		private readonly MiniRecipient recipient;

		// Token: 0x0400169D RID: 5789
		private readonly IConfigurationContext configurationContext;

		// Token: 0x0400169E RID: 5790
		private readonly ScopeFlightsSettingsProvider scopeFlightsSettingsProvider;

		// Token: 0x0400169F RID: 5791
		private readonly Func<VariantConfigurationSnapshot, IFeaturesStateOverride> featureStateOverrideFactory;

		// Token: 0x040016A0 RID: 5792
		private readonly string rampId;

		// Token: 0x040016A1 RID: 5793
		private readonly bool isFirstRelease;

		// Token: 0x040016A2 RID: 5794
		private FeaturesManager currentValue;

		// Token: 0x040016A3 RID: 5795
		private string currentFlightsOverride;
	}
}

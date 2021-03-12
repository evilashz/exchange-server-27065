using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Clients.Owa2.Server.Web;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000313 RID: 787
	internal class GetFlightsSettings : ServiceCommand<ScopeFlightsSetting[]>
	{
		// Token: 0x06001A2A RID: 6698 RVA: 0x0005F5F1 File Offset: 0x0005D7F1
		public GetFlightsSettings(CallContext callContext, ScopeFlightsSettingsProvider scopeFlightsSettingsProvider) : base(callContext)
		{
			this.scopeFlightsSettingsProvider = scopeFlightsSettingsProvider;
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0005F604 File Offset: 0x0005D804
		protected override ScopeFlightsSetting[] InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext);
			if (!userContext.FeaturesManager.ClientServerSettings.FlightsView.Enabled)
			{
				throw new OwaNotSupportedException("This method is not supported.");
			}
			IList<ScopeFlightsSetting> flightsForScope = this.scopeFlightsSettingsProvider.GetFlightsForScope();
			ScopeFlightsSetting item = new ScopeFlightsSetting(userContext.PrimarySmtpAddress.ToString(), userContext.FeaturesManager.ConfigurationSnapshot.Flights);
			flightsForScope.Add(item);
			return flightsForScope.ToArray<ScopeFlightsSetting>();
		}

		// Token: 0x04000E83 RID: 3715
		private readonly ScopeFlightsSettingsProvider scopeFlightsSettingsProvider;
	}
}

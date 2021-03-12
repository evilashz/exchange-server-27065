using System;
using System.Net;
using Microsoft.Exchange.Connections.Eas.Commands.Settings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TryExistingEndpoint : AutodiscoverStep
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x0000408B File Offset: 0x0000228B
		internal TryExistingEndpoint(EasConnectionSettings easConnectionSettings) : base(easConnectionSettings, Step.TrySmtpAddress)
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004098 File Offset: 0x00002298
		public override Step ExecuteStep(StepContext stepContext)
		{
			AutodiscoverEndpoint mostRecentEndpoint = stepContext.EasConnectionSettings.EasEndpointSettings.MostRecentEndpoint;
			if (mostRecentEndpoint == null)
			{
				return base.NextStepOnFailure;
			}
			if (mostRecentEndpoint.IsPotentiallyReusable() && this.CheckVitality(stepContext))
			{
				stepContext.EasConnectionSettings.Log.Debug("Use the existing endpoint: {0}", new object[]
				{
					mostRecentEndpoint.Url
				});
				stepContext.HttpStatusCode = HttpStatusCode.OK;
				stepContext.Response = new AutodiscoverResponse
				{
					HttpStatus = HttpStatus.OK,
					AutodiscoverStatus = AutodiscoverStatus.Success
				};
				return Step.Succeeded;
			}
			stepContext.EasConnectionSettings.EasEndpointSettings.MostRecentDomain = null;
			return base.NextStepOnFailure;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000413B File Offset: 0x0000233B
		protected override bool IsStepAllowable(StepContext stepContext)
		{
			return stepContext.Request.AutodiscoverOption != AutodiscoverOption.Probes;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004150 File Offset: 0x00002350
		private bool CheckVitality(StepContext stepContext)
		{
			SettingsCommand settingsCommand = new SettingsCommand(stepContext.EasConnectionSettings);
			bool result;
			try
			{
				settingsCommand.Execute(SettingsRequest.Default);
				result = true;
			}
			catch (LocalizedException ex)
			{
				stepContext.EasConnectionSettings.Log.Error("Failed to check vitality: {0}", new object[]
				{
					ex
				});
				result = false;
			}
			return result;
		}
	}
}

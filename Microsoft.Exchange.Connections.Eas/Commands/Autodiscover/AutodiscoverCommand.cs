using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x02000018 RID: 24
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AutodiscoverCommand : EasPseudoCommand<AutodiscoverRequest, AutodiscoverResponse>
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000034E0 File Offset: 0x000016E0
		protected internal AutodiscoverCommand(EasConnectionSettings easConnectionSettings) : base(Command.Autodiscover, easConnectionSettings)
		{
			this.steps = new Dictionary<Step, IExecuteStep>
			{
				{
					Step.TryExistingEndpoint,
					new TryExistingEndpoint(easConnectionSettings)
				},
				{
					Step.TrySmtpAddress,
					new TrySmtpAddress(easConnectionSettings)
				},
				{
					Step.TryRemovingDomainPrefix,
					new TryRemovingDomainPrefix(easConnectionSettings)
				},
				{
					Step.TryAddingAutodiscoverPrefix,
					new TryAddingAutodiscoverPrefix(easConnectionSettings)
				},
				{
					Step.TryUnauthenticatedGet,
					new TryUnauthenticatedGet(easConnectionSettings)
				},
				{
					Step.TryDnsLookupOfSrvRecord,
					new TryDnsLookupOfSrvRecord(easConnectionSettings)
				}
			};
			this.results = new Dictionary<Step, string>(this.steps.Count);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003574 File Offset: 0x00001774
		internal override AutodiscoverResponse Execute(AutodiscoverRequest autodiscoverRequest)
		{
			AutodiscoverResponse result;
			lock (this.syncLock)
			{
				this.results.Clear();
				StepContext stepContext = new StepContext(autodiscoverRequest, base.EasConnectionSettings);
				Step step = Step.TryExistingEndpoint;
				while ((step & Step.Done) == Step.None)
				{
					base.EasConnectionSettings.Log.Debug("AutoDiscover Step: {0}", new object[]
					{
						step
					});
					Step step2 = this.steps[step].PrepareAndExecuteStep(stepContext);
					this.results[step] = string.Format("HttpStatus={0}{1}.", stepContext.HttpStatusCode, (stepContext.Error != null) ? (",Error=" + stepContext.Error.Message) : string.Empty);
					step = step2;
				}
				result = this.ProcessResponse(stepContext.Response);
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003670 File Offset: 0x00001870
		private AutodiscoverResponse ProcessResponse(AutodiscoverResponse autodiscoverResponse)
		{
			if (autodiscoverResponse == null)
			{
				return this.CreateFailedAutodiscoverResponse();
			}
			if (autodiscoverResponse.Response != null && autodiscoverResponse.Response.Error == null)
			{
				string autodiscoveredDomain = autodiscoverResponse.AutodiscoveredDomain;
				base.EasConnectionSettings.Log.Info("Discovered Endpoint: {0}", new object[]
				{
					autodiscoveredDomain
				});
				if (!string.IsNullOrEmpty(autodiscoveredDomain))
				{
					base.EasConnectionSettings.EasEndpointSettings.MostRecentDomain = autodiscoveredDomain;
				}
			}
			autodiscoverResponse.ConvertStatusToEnum();
			return autodiscoverResponse;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000036E4 File Offset: 0x000018E4
		private AutodiscoverResponse CreateFailedAutodiscoverResponse()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<Step, string> keyValuePair in this.results)
			{
				stringBuilder.AppendFormat("[{0}]{1}", keyValuePair.Key, keyValuePair.Value);
			}
			return new AutodiscoverResponse
			{
				AutodiscoverStatus = AutodiscoverStatus.EveryStepFailed,
				AutodiscoverSteps = stringBuilder.ToString()
			};
		}

		// Token: 0x040000C1 RID: 193
		private readonly object syncLock = new object();

		// Token: 0x040000C2 RID: 194
		private readonly Dictionary<Step, IExecuteStep> steps;

		// Token: 0x040000C3 RID: 195
		private readonly Dictionary<Step, string> results;
	}
}

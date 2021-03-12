using System;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.UM.ClientAccess
{
	// Token: 0x02000065 RID: 101
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	[MessageInspectorBehavior]
	[ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
	internal class UMWebService : IUM12LegacyContract
	{
		// Token: 0x0600023A RID: 570 RVA: 0x0000BEAC File Offset: 0x0000A0AC
		private IBudget GetBudgetFromContext(ExchangePrincipal exchangePrincipal)
		{
			WindowsPrincipal windowsPrincipal = HttpContext.Current.User as WindowsPrincipal;
			IStandardBudget standardBudget;
			if (windowsPrincipal != null)
			{
				standardBudget = StandardBudget.Acquire((windowsPrincipal.Identity as WindowsIdentity).User, Global.BudgetType, exchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings());
			}
			else
			{
				standardBudget = StandardBudget.AcquireFallback(HttpContext.Current.User.Identity.Name, Global.BudgetType);
			}
			try
			{
				string callerInfo = "UMWebService.GetBudgetFromContext";
				ResourceLoadDelayInfo.CheckResourceHealth(standardBudget, this.workloadSettings, this.resources);
				standardBudget.StartConnection(callerInfo);
				standardBudget.StartLocal(callerInfo, default(TimeSpan));
			}
			catch (Exception)
			{
				standardBudget.Dispose();
				throw;
			}
			return standardBudget;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000BF7C File Offset: 0x0000A17C
		public bool IsUMEnabled()
		{
			bool retVal = false;
			this.DoOperation(delegate(UMClientCommon umClient)
			{
				retVal = umClient.IsUMEnabled();
			});
			return retVal;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
		public UMProperties GetUMProperties()
		{
			UMProperties umProps = null;
			this.DoOperation(delegate(UMClientCommon umClient)
			{
				UMPropertiesEx umproperties = umClient.GetUMProperties();
				umProps = new UMProperties(umproperties);
			});
			return umProps;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000C020 File Offset: 0x0000A220
		public void SetOofStatus(bool status)
		{
			this.DoOperation(delegate(UMClientCommon umClient)
			{
				umClient.SetOofStatus(status);
			});
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000C064 File Offset: 0x0000A264
		public void SetPlayOnPhoneDialString(string dialString)
		{
			this.DoOperation(delegate(UMClientCommon umClient)
			{
				umClient.SetPlayOnPhoneDialString(dialString);
			});
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		public void SetTelephoneAccessFolderEmail(string base64FolderId)
		{
			this.DoOperation(delegate(UMClientCommon umClient)
			{
				umClient.SetTelephoneAccessFolderEmail(base64FolderId);
			});
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		public void SetMissedCallNotificationEnabled(bool status)
		{
			this.DoOperation(delegate(UMClientCommon umClient)
			{
				umClient.SetMissedCallNotificationEnabled(status);
			});
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000C120 File Offset: 0x0000A320
		public void ResetPIN()
		{
			this.DoOperation(delegate(UMClientCommon umClient)
			{
				umClient.ResetPIN();
			});
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000C168 File Offset: 0x0000A368
		public string PlayOnPhone(string base64ObjectId, string dialString)
		{
			string callId = null;
			this.DoOperation(delegate(UMClientCommon umClient)
			{
				callId = umClient.PlayOnPhone(base64ObjectId, dialString);
			});
			return callId;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		public UMCallInfo GetCallInfo(string callId)
		{
			UMCallInfo callInfo = null;
			this.DoOperation(delegate(UMClientCommon umClient)
			{
				UMCallInfoEx callInfo = umClient.GetCallInfo(callId);
				callInfo = new UMCallInfo(callInfo);
			});
			return callInfo;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000C228 File Offset: 0x0000A428
		public void Disconnect([XmlElement("CallId")] string callId)
		{
			this.DoOperation(delegate(UMClientCommon umClient)
			{
				umClient.Disconnect(callId);
			});
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000C278 File Offset: 0x0000A478
		public string PlayOnPhoneGreeting([XmlElement("GreetingType")] UMGreetingType greetingType, [XmlElement("DialString")] string dialString)
		{
			string callId = null;
			this.DoOperation(delegate(UMClientCommon umClient)
			{
				callId = umClient.PlayOnPhoneGreeting(greetingType, dialString);
			});
			return callId;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		private ExchangePrincipal GetExchangePrincipal()
		{
			ExchangePrincipal result = null;
			using (AuthZClientInfo callerClientInfo = CallContext.GetCallerClientInfo())
			{
				if (callerClientInfo == null || !ExchangePrincipalCache.TryGetFromCache(callerClientInfo.ClientSecurityContext.UserSid, callerClientInfo.GetADRecipientSessionContext(), out result))
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceWarning(0L, "[UMWebService::GetExchangePrincipal] Could not get exchange principal.");
					throw new InvalidPrincipalException();
				}
			}
			return result;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000C320 File Offset: 0x0000A520
		private void DoOperation(Action<UMClientCommon> operation)
		{
			try
			{
				ExchangePrincipal exchangePrincipal = this.GetExchangePrincipal();
				using (IBudget budgetFromContext = this.GetBudgetFromContext(exchangePrincipal))
				{
					try
					{
						using (UMClientCommon umclientCommon = new UMClientCommon(exchangePrincipal))
						{
							operation(umclientCommon);
						}
					}
					finally
					{
						ResourceLoadDelayInfo.EnforceDelay(budgetFromContext, this.workloadSettings, this.resources, TimeSpan.MaxValue, null);
					}
				}
			}
			catch (LocalizedException exception)
			{
				throw FaultExceptionUtilities.CreateUMFault(exception, FaultParty.Receiver);
			}
		}

		// Token: 0x0400054D RID: 1357
		private WorkloadSettings workloadSettings = new WorkloadSettings(WorkloadType.Ews);

		// Token: 0x0400054E RID: 1358
		private ResourceKey[] resources = new ResourceKey[]
		{
			ProcessorResourceKey.Local
		};

		// Token: 0x02000066 RID: 102
		internal abstract class Constants
		{
			// Token: 0x04000550 RID: 1360
			internal const string WebServiceDescription = "Provides the server support for UM client features in OWA and Outlook";

			// Token: 0x04000551 RID: 1361
			internal const string IsUMEnabledDescription = "Returns true if the user defined by the http context is um-enabled, false otherwise";

			// Token: 0x04000552 RID: 1362
			internal const string GetUMPropertiesDescription = "Returns all UM properties";

			// Token: 0x04000553 RID: 1363
			internal const string SetOofStatusDescription = "Sets the OofStatus property";

			// Token: 0x04000554 RID: 1364
			internal const string SetPlayOnPhoneDialStringDescription = "Sets the PlayOnPhoneDialString property";

			// Token: 0x04000555 RID: 1365
			internal const string SetTelephoneAccessFolderEmailDescription = "Sets the TelephoneAccessFolderEmail property";

			// Token: 0x04000556 RID: 1366
			internal const string SetMissedCallNotificationEnabledDescription = "Sets the MissedCallNotificationEnabled property";

			// Token: 0x04000557 RID: 1367
			internal const string SetTelephoneAccessNumbersDescription = "Sets the TelephoneAccessNumbers property";

			// Token: 0x04000558 RID: 1368
			internal const string ResetPINDescription = "Changes the PIN (TUI Password) to a new random value";

			// Token: 0x04000559 RID: 1369
			internal const string PlayOnPhoneDescription = "Makes an outbound call and plays a voice message over the telephone";

			// Token: 0x0400055A RID: 1370
			internal const string GetCallInfoDescription = "Returns information about a call";

			// Token: 0x0400055B RID: 1371
			internal const string DisconnectDescription = "Disconnects a call";

			// Token: 0x0400055C RID: 1372
			internal const string PlayOnPhoneGreetingDescription = "Makes an outbound call and plays/records a greeting over the telephone";
		}
	}
}

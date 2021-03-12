using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000333 RID: 819
	internal class InstantMessageSignIn : InstantMessageCommandBase<int>
	{
		// Token: 0x06001B19 RID: 6937 RVA: 0x00066CD8 File Offset: 0x00064ED8
		static InstantMessageSignIn()
		{
			OwsLogRegistry.Register(OwaApplication.GetRequestDetailsLogger.Get(ExtensibleLoggerMetadata.EventId), typeof(InstantMessageSignIn.LogMetadata), new Type[0]);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00066CFF File Offset: 0x00064EFF
		public InstantMessageSignIn(CallContext callContext, bool signedInManually) : base(callContext)
		{
			this.signedInManually = signedInManually;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00066D10 File Offset: 0x00064F10
		protected override int InternalExecute()
		{
			InstantMessageOperationError instantMessageOperationError = this.SignIn();
			OwaApplication.GetRequestDetailsLogger.Set(InstantMessagingLogMetadata.OperationErrorCode, instantMessageOperationError);
			return (int)instantMessageOperationError;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00066D3C File Offset: 0x00064F3C
		private static InstantMessageOperationError InitializeProvider()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			InstantMessageOperationError result;
			try
			{
				result = InstantMessageProvider.Initialize();
			}
			finally
			{
				stopwatch.Stop();
				OwaApplication.GetRequestDetailsLogger.Set(InstantMessageSignIn.LogMetadata.InitializeProvider, stopwatch.ElapsedMilliseconds);
			}
			return result;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00066D8C File Offset: 0x00064F8C
		private static UserContext GetUserContext()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			UserContext userContext;
			try
			{
				userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			}
			finally
			{
				stopwatch.Stop();
				OwaApplication.GetRequestDetailsLogger.Set(InstantMessageSignIn.LogMetadata.GetUserContext, stopwatch.ElapsedMilliseconds);
			}
			return userContext;
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00066DF0 File Offset: 0x00064FF0
		private InstantMessageOperationError SignIn()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			InstantMessageOperationError result;
			try
			{
				InstantMessageOperationError instantMessageOperationError = InstantMessageSignIn.InitializeProvider();
				if (instantMessageOperationError != InstantMessageOperationError.Success)
				{
					result = instantMessageOperationError;
				}
				else
				{
					UserContext userContext = InstantMessageSignIn.GetUserContext();
					if (!userContext.IsInstantMessageEnabled)
					{
						result = InstantMessageOperationError.NotEnabled;
					}
					else if (userContext.InstantMessageManager == null)
					{
						result = InstantMessageOperationError.NotConfigured;
					}
					else if (!this.ShouldSignIn(userContext))
					{
						result = InstantMessageOperationError.NotSignedIn;
					}
					else
					{
						InstantMessageOperationError instantMessageOperationError2 = userContext.InstantMessageManager.StartProvider(base.MailboxIdentityMailboxSession);
						result = instantMessageOperationError2;
					}
				}
			}
			finally
			{
				stopwatch.Stop();
				OwaApplication.GetRequestDetailsLogger.Set(InstantMessageSignIn.LogMetadata.Total, stopwatch.ElapsedMilliseconds);
			}
			return result;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x00066E90 File Offset: 0x00065090
		private bool ShouldSignIn(UserContext userContext)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			bool result;
			try
			{
				if (this.signedInManually)
				{
					InstantMessageUtilities.SetSignedOutFlag(base.MailboxIdentityMailboxSession, false);
				}
				else if (InstantMessageUtilities.IsSignedOut(base.MailboxIdentityMailboxSession))
				{
					InstantMessageNotifier notifier = userContext.InstantMessageManager.Notifier;
					if (notifier != null)
					{
						InstantMessagePayloadUtilities.GenerateUnavailablePayload(notifier, null, "Not signed in because IsSignedOutOfIM was true.", InstantMessageServiceError.ClientSignOut, false);
					}
					return false;
				}
				result = true;
			}
			finally
			{
				stopwatch.Stop();
				OwaApplication.GetRequestDetailsLogger.Set(InstantMessageSignIn.LogMetadata.HandleSignOutFlag, stopwatch.ElapsedMilliseconds);
			}
			return result;
		}

		// Token: 0x04000F09 RID: 3849
		private readonly bool signedInManually;

		// Token: 0x02000334 RID: 820
		public enum LogMetadata
		{
			// Token: 0x04000F0B RID: 3851
			[DisplayName("IM.SI.UC")]
			GetUserContext,
			// Token: 0x04000F0C RID: 3852
			[DisplayName("IM.SI.IP")]
			InitializeProvider,
			// Token: 0x04000F0D RID: 3853
			[DisplayName("IM.SI.SOF")]
			HandleSignOutFlag,
			// Token: 0x04000F0E RID: 3854
			[DisplayName("IM.SI.CC")]
			CheckConfiguration,
			// Token: 0x04000F0F RID: 3855
			[DisplayName("IM.SI.FP")]
			FindProvider,
			// Token: 0x04000F10 RID: 3856
			[DisplayName("IM.SI.DLL")]
			LoadDll,
			// Token: 0x04000F11 RID: 3857
			[DisplayName("IM.SI.GC")]
			GetCertificate,
			// Token: 0x04000F12 RID: 3858
			[DisplayName("IM.SI.CEM")]
			CreateEndpointManager,
			// Token: 0x04000F13 RID: 3859
			[DisplayName("IM.SI.IEM")]
			InitializeEndpointManager,
			// Token: 0x04000F14 RID: 3860
			[DisplayName("IM.SI.CP")]
			CreateProvider,
			// Token: 0x04000F15 RID: 3861
			[DisplayName("IM.SI.ES")]
			EstablishSession,
			// Token: 0x04000F16 RID: 3862
			[DisplayName("IM.SI.GEG")]
			GetExpandedGroups,
			// Token: 0x04000F17 RID: 3863
			[DisplayName("IM.SI.EG")]
			ExpandGroups,
			// Token: 0x04000F18 RID: 3864
			[DisplayName("IM.SI.RP")]
			ResetPresence,
			// Token: 0x04000F19 RID: 3865
			[DisplayName("IM.SI.T")]
			Total
		}
	}
}

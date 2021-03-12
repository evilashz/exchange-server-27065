using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CrossServerConnectionPolicy : ICrossServerConnectionPolicy
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003130 File Offset: 0x00001330
		public CrossServerConnectionPolicy(ICrossServerDiagnostics crossServerDiagnostics, IClientBehaviorOverrides clientBehaviorOverrides)
		{
			if (crossServerDiagnostics == null)
			{
				throw new ArgumentNullException("crossServerDiagnostics");
			}
			if (clientBehaviorOverrides == null)
			{
				throw new ArgumentNullException("clientBehaviorOverrides");
			}
			this.crossServerDiagnostics = crossServerDiagnostics;
			this.clientBehaviorOverrides = clientBehaviorOverrides;
			this.InitializeCrossServerBehaviorDictionary();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003180 File Offset: 0x00001380
		private void CreateAndAddCrossServerBehaviorToDictionary(string clientId)
		{
			CrossServerBehavior crossServerBehavior = new CrossServerBehavior(clientId, true, false, false, false);
			this.crossServerBehaviors[new Tuple<string, bool>(crossServerBehavior.ClientId.ToLower(), crossServerBehavior.PreExchange15)] = crossServerBehavior;
			crossServerBehavior = new CrossServerBehavior(clientId, false, true, false, false);
			this.crossServerBehaviors[new Tuple<string, bool>(crossServerBehavior.ClientId.ToLower(), crossServerBehavior.PreExchange15)] = crossServerBehavior;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000031E8 File Offset: 0x000013E8
		private void InitializeCrossServerBehaviorDictionary()
		{
			this.CreateAndAddCrossServerBehaviorToDictionary("activesync");
			this.CreateAndAddCrossServerBehaviorToDictionary("approvalapi");
			this.CreateAndAddCrossServerBehaviorToDictionary("as");
			this.CreateAndAddCrossServerBehaviorToDictionary("ci");
			this.CreateAndAddCrossServerBehaviorToDictionary("eba");
			this.CreateAndAddCrossServerBehaviorToDictionary("ediscoverysearch");
			this.CreateAndAddCrossServerBehaviorToDictionary("elc");
			this.CreateAndAddCrossServerBehaviorToDictionary("hub");
			this.CreateAndAddCrossServerBehaviorToDictionary("hub transport");
			this.CreateAndAddCrossServerBehaviorToDictionary("management");
			this.CreateAndAddCrossServerBehaviorToDictionary("monitoring");
			this.CreateAndAddCrossServerBehaviorToDictionary("msexchangemigration");
			this.CreateAndAddCrossServerBehaviorToDictionary("msexchangerpc");
			this.CreateAndAddCrossServerBehaviorToDictionary("msexchangesimplemigration");
			this.CreateAndAddCrossServerBehaviorToDictionary("outlookservice");
			this.CreateAndAddCrossServerBehaviorToDictionary("owa");
			this.CreateAndAddCrossServerBehaviorToDictionary("pop3/imap4");
			this.CreateAndAddCrossServerBehaviorToDictionary("publicfoldersystem");
			this.CreateAndAddCrossServerBehaviorToDictionary("tba");
			this.CreateAndAddCrossServerBehaviorToDictionary("teammailbox");
			this.CreateAndAddCrossServerBehaviorToDictionary("transportsync");
			this.CreateAndAddCrossServerBehaviorToDictionary("um");
			this.CreateAndAddCrossServerBehaviorToDictionary("unifiedpolicy");
			this.CreateAndAddCrossServerBehaviorToDictionary("webservices");
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003300 File Offset: 0x00001500
		public void Apply(ExRpcConnectionInfo connectionInfo)
		{
			CrossServerBehavior crossServerBehavior = null;
			CrossServerBehavior crossServerBehavior2 = null;
			if (connectionInfo == null)
			{
				throw new ArgumentNullException("connectionInfo");
			}
			if ((connectionInfo.ConnectFlags & ConnectFlag.ConnectToExchangeRpcServerOnly) == ConnectFlag.ConnectToExchangeRpcServerOnly)
			{
				return;
			}
			if (ExEnvironment.IsTestProcess)
			{
				return;
			}
			if (!connectionInfo.IsCrossServer)
			{
				return;
			}
			bool flag = (connectionInfo.ConnectFlags & ConnectFlag.IsPreExchange15) == ConnectFlag.IsPreExchange15;
			string normalizedClientInfoWithoutPrefix = connectionInfo.ApplicationId.GetNormalizedClientInfoWithoutPrefix();
			if (this.CheckAndBlockMonitoringMailboxes(connectionInfo))
			{
				return;
			}
			CrossServerBehavior crossServerBehavior3 = flag ? CrossServerConnectionPolicy.defaultE14CrossServerConnectionBehavior : CrossServerConnectionPolicy.defaultE15CrossServerConnectionBehavior;
			if (!this.crossServerBehaviors.TryGetValue(new Tuple<string, bool>(normalizedClientInfoWithoutPrefix, flag), out crossServerBehavior))
			{
				crossServerBehavior = crossServerBehavior3;
			}
			if (this.clientBehaviorOverrides.TryGetClientSpecificOverrides(normalizedClientInfoWithoutPrefix, crossServerBehavior3, out crossServerBehavior2))
			{
				crossServerBehavior = crossServerBehavior2;
			}
			if (crossServerBehavior.ShouldTrace)
			{
				this.crossServerDiagnostics.TraceCrossServerCall(connectionInfo.ServerDn);
			}
			if (crossServerBehavior.ShouldLogInfoWatson)
			{
				this.crossServerDiagnostics.LogInfoWatson(connectionInfo);
			}
			if (crossServerBehavior.ShouldBlock)
			{
				this.crossServerDiagnostics.BlockCrossServerCall(connectionInfo);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000033EF File Offset: 0x000015EF
		private bool CheckAndBlockMonitoringMailboxes(ExRpcConnectionInfo connectionInfo)
		{
			if (connectionInfo.IsCrossServer && (connectionInfo.ConnectFlags & ConnectFlag.MonitoringMailbox) == ConnectFlag.MonitoringMailbox && connectionInfo.ApplicationId.ClientType != MapiClientType.ContentIndexing)
			{
				this.crossServerDiagnostics.BlockMonitoringCrossServerCall(connectionInfo);
				return true;
			}
			return false;
		}

		// Token: 0x0400007C RID: 124
		private ICrossServerDiagnostics crossServerDiagnostics;

		// Token: 0x0400007D RID: 125
		private IClientBehaviorOverrides clientBehaviorOverrides;

		// Token: 0x0400007E RID: 126
		private static CrossServerBehavior defaultE14CrossServerConnectionBehavior = new CrossServerBehavior(string.Empty, true, false, false, false);

		// Token: 0x0400007F RID: 127
		private static CrossServerBehavior defaultE15CrossServerConnectionBehavior = new CrossServerBehavior(string.Empty, false, true, true, true);

		// Token: 0x04000080 RID: 128
		private Dictionary<Tuple<string, bool>, CrossServerBehavior> crossServerBehaviors = new Dictionary<Tuple<string, bool>, CrossServerBehavior>();
	}
}

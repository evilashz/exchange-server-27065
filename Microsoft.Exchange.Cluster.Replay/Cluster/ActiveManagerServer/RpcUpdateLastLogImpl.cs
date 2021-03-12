using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.Common;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000B9 RID: 185
	internal static class RpcUpdateLastLogImpl
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x00024D10 File Offset: 0x00022F10
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcGenericReplyInfo tmpReplyInfo = null;
			RpcUpdateLastLogImpl.Request req = null;
			RpcUpdateLastLogImpl.Reply rep = new RpcUpdateLastLogImpl.Reply();
			Exception ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				req = ActiveManagerGenericRpcHelper.ValidateAndGetAttachedRequest<RpcUpdateLastLogImpl.Request>(requestInfo, 1, 0);
				ExDateTime exDateTime = ExDateTime.MinValue;
				AmCachedLastLogUpdater pamCachedLastLogUpdater = AmSystemManager.Instance.PamCachedLastLogUpdater;
				if (pamCachedLastLogUpdater != null)
				{
					exDateTime = pamCachedLastLogUpdater.AddEntries(req.ServerName, req.InitiatedTimeUtc, req.LastLogEntries);
				}
				rep.LastSuccessfulUpdateTimeUtc = exDateTime.UniversalTime;
				tmpReplyInfo = ActiveManagerGenericRpcHelper.PrepareServerReply(requestInfo, rep, 1, 0);
			});
			if (tmpReplyInfo != null)
			{
				replyInfo = tmpReplyInfo;
			}
			if (ex != null)
			{
				throw new AmServerException(ex.Message, ex);
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00024D78 File Offset: 0x00022F78
		internal static DateTime Send(AmServerName originatingServer, AmServerName targetServer, Dictionary<string, string> dbLastLogMap)
		{
			RpcUpdateLastLogImpl.Request attachedRequest = new RpcUpdateLastLogImpl.Request(originatingServer.Fqdn, dbLastLogMap);
			RpcGenericRequestInfo requestInfo = ActiveManagerGenericRpcHelper.PrepareClientRequest(attachedRequest, 2, 1, 0);
			RpcUpdateLastLogImpl.Reply reply = ActiveManagerGenericRpcHelper.RunRpcAndGetReply<RpcUpdateLastLogImpl.Reply>(requestInfo, targetServer.Fqdn, RegistryParameters.PamLastLogRpcTimeoutInMsec);
			return reply.LastSuccessfulUpdateTimeUtc;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00024DB4 File Offset: 0x00022FB4
		internal static KeyValuePair<Guid, long>[] ConvertLastLogDictionaryToKeyValuePairArray(Dictionary<string, string> dbLastLogMap)
		{
			List<KeyValuePair<Guid, long>> list = new List<KeyValuePair<Guid, long>>();
			foreach (KeyValuePair<string, string> keyValuePair in dbLastLogMap)
			{
				Guid key;
				long value;
				if (Guid.TryParse(keyValuePair.Key, out key) && long.TryParse(keyValuePair.Value, out value))
				{
					list.Add(new KeyValuePair<Guid, long>(key, value));
				}
			}
			return list.ToArray();
		}

		// Token: 0x0400035D RID: 861
		public const int MajorVersion = 1;

		// Token: 0x0400035E RID: 862
		public const int MinorVersion = 0;

		// Token: 0x0400035F RID: 863
		public const int CommandCode = 2;

		// Token: 0x020000BA RID: 186
		[Serializable]
		internal class Request
		{
			// Token: 0x060007A0 RID: 1952 RVA: 0x00024E38 File Offset: 0x00023038
			public Request(string serverFqdn, Dictionary<string, string> dbLastLogMap)
			{
				this.ServerName = serverFqdn;
				this.LastLogEntries = RpcUpdateLastLogImpl.ConvertLastLogDictionaryToKeyValuePairArray(dbLastLogMap);
				this.InitiatedTimeUtc = DateTime.UtcNow;
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00024E5E File Offset: 0x0002305E
			// (set) Token: 0x060007A2 RID: 1954 RVA: 0x00024E66 File Offset: 0x00023066
			public string ServerName { get; private set; }

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00024E6F File Offset: 0x0002306F
			// (set) Token: 0x060007A4 RID: 1956 RVA: 0x00024E77 File Offset: 0x00023077
			public DateTime InitiatedTimeUtc { get; set; }

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00024E80 File Offset: 0x00023080
			// (set) Token: 0x060007A6 RID: 1958 RVA: 0x00024E88 File Offset: 0x00023088
			public KeyValuePair<Guid, long>[] LastLogEntries { get; private set; }

			// Token: 0x060007A7 RID: 1959 RVA: 0x00024E91 File Offset: 0x00023091
			public override string ToString()
			{
				return string.Format("ServerName: '{0}' Count: '{1}'", this.ServerName, this.LastLogEntries.Length);
			}
		}

		// Token: 0x020000BB RID: 187
		[Serializable]
		internal class Reply
		{
			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00024EB0 File Offset: 0x000230B0
			// (set) Token: 0x060007A9 RID: 1961 RVA: 0x00024EB8 File Offset: 0x000230B8
			public DateTime LastSuccessfulUpdateTimeUtc { get; set; }

			// Token: 0x060007AA RID: 1962 RVA: 0x00024EC1 File Offset: 0x000230C1
			public override string ToString()
			{
				return string.Format("LastSuccessfulUpdateTimeUtc: '{0}'", this.LastSuccessfulUpdateTimeUtc);
			}
		}
	}
}

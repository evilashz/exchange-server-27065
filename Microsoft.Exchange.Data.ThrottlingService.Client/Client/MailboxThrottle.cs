using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ThrottlingService;

namespace Microsoft.Exchange.Data.ThrottlingService.Client
{
	// Token: 0x02000003 RID: 3
	internal sealed class MailboxThrottle : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020D0 File Offset: 0x000002D0
		public MailboxThrottle()
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.rpcClients = new Dictionary<string, ThrottlingRpcClientImpl>(16, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public static MailboxThrottle Instance
		{
			get
			{
				if (MailboxThrottle.instance == null)
				{
					lock (MailboxThrottle.instanceCreationSyncRoot)
					{
						if (MailboxThrottle.instance == null)
						{
							MailboxThrottle.instance = new MailboxThrottle();
							MailboxThrottle.instance.SuppressDisposeTracker();
						}
					}
				}
				return MailboxThrottle.instance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002158 File Offset: 0x00000358
		internal int TestabilityVersionForNewRpc
		{
			get
			{
				return MailboxThrottle.NewRpcVersion;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000215F File Offset: 0x0000035F
		internal bool TestabilityLastCallUsedNewRpc
		{
			get
			{
				return this.testabilityLastCallUsedNewRpc;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002168 File Offset: 0x00000368
		public static Unlimited<uint> GetUserSubmissionQuota(ADObjectId throttlingPolicyId, OrganizationId organizationId)
		{
			IThrottlingPolicy throttlingPolicy = ThrottlingPolicyCache.Singleton.Get(organizationId, throttlingPolicyId);
			return throttlingPolicy.RecipientRateLimit;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002188 File Offset: 0x00000388
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.rpcClients == null)
			{
				return;
			}
			lock (this.rpcClients)
			{
				foreach (KeyValuePair<string, ThrottlingRpcClientImpl> keyValuePair in this.rpcClients)
				{
					MailboxThrottle.tracer.TraceDebug<string>(0L, "Disposing RPC client for mailbox server {0}", keyValuePair.Key);
					keyValuePair.Value.Dispose();
				}
				this.rpcClients = null;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000224C File Offset: 0x0000044C
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxThrottle>(this);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002254 File Offset: 0x00000454
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000226C File Offset: 0x0000046C
		public bool ObtainUserSubmissionTokens(string mailboxServer, int mailboxServerVersion, Guid mailboxGuid, int recipientCount, ADObjectId throttlingPolicyId, OrganizationId organizationId, string clientInfo)
		{
			Unlimited<uint> userSubmissionQuota = MailboxThrottle.GetUserSubmissionQuota(throttlingPolicyId, organizationId);
			MailboxThrottle.tracer.TraceDebug<Guid, Unlimited<uint>, int>(0L, "Submission quota for mailbox <{0}> is {1}, requesting {2} tokens.", mailboxGuid, userSubmissionQuota, recipientCount);
			return this.ObtainTokens(mailboxServer, mailboxServerVersion, mailboxGuid, RequestedAction.UserMailSubmission, recipientCount, userSubmissionQuota, clientInfo);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022A7 File Offset: 0x000004A7
		public bool ObtainUserSubmissionTokens(string mailboxServer, int mailboxServerVersion, Guid mailboxGuid, int recipientCount, Unlimited<uint> totalTokenCount, string clientInfo)
		{
			MailboxThrottle.tracer.TraceDebug<Guid, Unlimited<uint>, int>(0L, "Rule submission quota for mailbox <{0}> is {1}, requesting {2} tokens.", mailboxGuid, totalTokenCount, recipientCount);
			return this.ObtainTokens(mailboxServer, mailboxServerVersion, mailboxGuid, RequestedAction.UserMailSubmission, recipientCount, totalTokenCount, clientInfo);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022CF File Offset: 0x000004CF
		public bool ObtainRuleSubmissionTokens(string mailboxServer, int mailboxServerVersion, Guid mailboxGuid, int recipientCount, Unlimited<uint> totalTokenCount, string clientInfo)
		{
			MailboxThrottle.tracer.TraceDebug<Guid, Unlimited<uint>, int>(0L, "Rule submission quota for mailbox <{0}> is {1}, requesting {2} tokens.", mailboxGuid, totalTokenCount, recipientCount);
			return this.ObtainTokens(mailboxServer, mailboxServerVersion, mailboxGuid, RequestedAction.MailboxRuleMailSubmission, recipientCount, totalTokenCount, clientInfo);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022F8 File Offset: 0x000004F8
		private static bool TryAnswerWithoutRpc(Guid mailboxGuid, int requestedTokenCount, Unlimited<uint> totalTokenCount, out int quota, out bool allow)
		{
			if (totalTokenCount.IsUnlimited)
			{
				MailboxThrottle.tracer.TraceDebug<Guid, int>(0L, "Automatically allow submission for mailbox GUID <{0}> and requestedTokenCount {1} because the quota is unlimited", mailboxGuid, requestedTokenCount);
				allow = true;
				quota = int.MaxValue;
				return true;
			}
			quota = (int)((totalTokenCount.Value <= 2147483647U) ? totalTokenCount.Value : 2147483647U);
			if (requestedTokenCount > quota)
			{
				MailboxThrottle.tracer.TraceDebug<Guid, int, int>(0L, "Automatically deny submission for mailbox GUID <{0}> because requestedTokenCount {1} is over the quota of {2}", mailboxGuid, requestedTokenCount, quota);
				allow = false;
				return true;
			}
			allow = true;
			return false;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002374 File Offset: 0x00000574
		private static bool ThrottlingRpcResultToBoolean(ThrottlingRpcResult result)
		{
			switch (result)
			{
			case ThrottlingRpcResult.Allowed:
				return true;
			case ThrottlingRpcResult.Bypassed:
			case ThrottlingRpcResult.Failed:
				return true;
			case ThrottlingRpcResult.Denied:
				return false;
			default:
				throw new InvalidOperationException("Unexpected result from RPC client: " + result);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023B8 File Offset: 0x000005B8
		private bool ObtainTokens(string mailboxServer, int mailboxServerVersion, Guid mailboxGuid, RequestedAction requestedAction, int requestedTokenCount, Unlimited<uint> totalTokenCount, string clientInfo)
		{
			this.ThrowIfInvalid(mailboxServer, requestedTokenCount, totalTokenCount);
			int quota;
			bool flag;
			if (MailboxThrottle.TryAnswerWithoutRpc(mailboxGuid, requestedTokenCount, totalTokenCount, out quota, out flag))
			{
				return flag;
			}
			if (mailboxServerVersion >= MailboxThrottle.NewRpcVersion)
			{
				flag = this.ObtainTokensViaNewApi(mailboxServer, mailboxGuid, requestedAction, requestedTokenCount, quota, clientInfo);
				this.testabilityLastCallUsedNewRpc = true;
			}
			else if (requestedAction == RequestedAction.UserMailSubmission)
			{
				flag = this.ObtainTokensViaOldApi(mailboxServer, mailboxGuid, requestedTokenCount, quota);
				this.testabilityLastCallUsedNewRpc = false;
			}
			else
			{
				flag = true;
				this.testabilityLastCallUsedNewRpc = false;
			}
			if (!flag)
			{
				ThrottlingRpcClientImpl.EventLogger.LogEvent(ThrottlingClientEventLogConstants.Tuple_MailboxThrottled, mailboxGuid.ToString(), new object[]
				{
					mailboxGuid,
					mailboxServer,
					requestedAction,
					requestedTokenCount,
					totalTokenCount,
					clientInfo,
					Process.GetCurrentProcess().ProcessName
				});
			}
			return flag;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000248C File Offset: 0x0000068C
		private bool ObtainTokensViaOldApi(string mailboxServer, Guid mailboxGuid, int requestedTokenCount, int quota)
		{
			ThrottlingRpcClientImpl rpcClient = this.GetRpcClient(mailboxServer);
			ThrottlingRpcResult result = rpcClient.ObtainSubmissionTokens(mailboxGuid, requestedTokenCount, quota, 0);
			return MailboxThrottle.ThrottlingRpcResultToBoolean(result);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024B4 File Offset: 0x000006B4
		private bool ObtainTokensViaNewApi(string mailboxServer, Guid mailboxGuid, RequestedAction requestedAction, int requestedTokenCount, int quota, string clientInfo)
		{
			ThrottlingRpcClientImpl rpcClient = this.GetRpcClient(mailboxServer);
			ThrottlingRpcResult result = rpcClient.ObtainTokens(mailboxGuid, requestedAction, requestedTokenCount, quota, clientInfo);
			return MailboxThrottle.ThrottlingRpcResultToBoolean(result);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024E0 File Offset: 0x000006E0
		private void ThrowIfInvalid(string mailboxServer, int requestedTokenCount, Unlimited<uint> totalTokenCount)
		{
			if (string.IsNullOrEmpty(mailboxServer))
			{
				throw new ArgumentNullException("mailboxServer");
			}
			if (requestedTokenCount < 1)
			{
				throw new ArgumentOutOfRangeException("requestedTokenCount", requestedTokenCount, "requestedTokenCount should be greater than zero");
			}
			if (!totalTokenCount.IsUnlimited && totalTokenCount.Value < 1U)
			{
				throw new ArgumentOutOfRangeException("totalTokenCount", totalTokenCount, "totalTokenCount should be greater than zero and less than Int32.MaxValue");
			}
			if (this.rpcClients == null)
			{
				throw new ObjectDisposedException("MailboxThrottle instance is disposed");
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002558 File Offset: 0x00000758
		private ThrottlingRpcClientImpl GetRpcClient(string mailboxServer)
		{
			ThrottlingRpcClientImpl throttlingRpcClientImpl = null;
			lock (this.rpcClients)
			{
				if (!this.rpcClients.TryGetValue(mailboxServer, out throttlingRpcClientImpl))
				{
					throttlingRpcClientImpl = new ThrottlingRpcClientImpl(mailboxServer);
					this.rpcClients.Add(mailboxServer, throttlingRpcClientImpl);
					MailboxThrottle.tracer.TraceDebug<string>(0L, "Added a new RPC client for mailbox server {0}", mailboxServer);
				}
			}
			return throttlingRpcClientImpl;
		}

		// Token: 0x04000001 RID: 1
		private const int RpcClientDictionaryInitialCapacity = 16;

		// Token: 0x04000002 RID: 2
		private static readonly int NewRpcVersion = new ServerVersion(14, 0, 582, 0).ToInt();

		// Token: 0x04000003 RID: 3
		private static Microsoft.Exchange.Diagnostics.Trace tracer = ExTraceGlobals.ThrottlingClientTracer;

		// Token: 0x04000004 RID: 4
		private static MailboxThrottle instance;

		// Token: 0x04000005 RID: 5
		private static object instanceCreationSyncRoot = new object();

		// Token: 0x04000006 RID: 6
		private Dictionary<string, ThrottlingRpcClientImpl> rpcClients;

		// Token: 0x04000007 RID: 7
		private DisposeTracker disposeTracker;

		// Token: 0x04000008 RID: 8
		private bool testabilityLastCallUsedNewRpc;
	}
}

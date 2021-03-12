using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Audit;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.Common;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000B5 RID: 181
	internal static class RpcKillServiceImpl
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x0002444C File Offset: 0x0002264C
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcGenericReplyInfo tmpReplyInfo = null;
			RpcKillServiceImpl.Request req = null;
			RpcKillServiceImpl.Reply rep = new RpcKillServiceImpl.Reply();
			Exception ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				req = ActiveManagerGenericRpcHelper.ValidateAndGetAttachedRequest<RpcKillServiceImpl.Request>(requestInfo, 1, 0);
				ReplayCrimsonEvents.ReceivedRequestToKillService.Log<string, string, RpcKillServiceImpl.Request>(req.ServiceName, req.OriginatingServerName, req);
				lock (RpcKillServiceImpl.locker)
				{
					RpcKillServiceImpl.HandleRequestInternal(req, rep);
				}
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

		// Token: 0x06000775 RID: 1909 RVA: 0x000246DC File Offset: 0x000228DC
		private static void HandleRequestInternal(RpcKillServiceImpl.Request req, RpcKillServiceImpl.Reply rep)
		{
			Exception exception = null;
			Privilege.RunWithPrivilege("SeDebugPrivilege", true, delegate
			{
				ServiceKillConfig serviceKillConfig = ServiceKillConfig.Read(req.ServiceName);
				rep.LastKillTime = serviceKillConfig.LastKillTime;
				if (!serviceKillConfig.IsDisabled)
				{
					using (Process serviceProcess = ServiceOperations.GetServiceProcess(req.ServiceName, out exception))
					{
						if (serviceProcess != null)
						{
							DateTime startTime = serviceProcess.StartTime;
							rep.ProcessStartTime = startTime;
							bool flag = false;
							if (req.IsForce)
							{
								flag = true;
							}
							else if (ExDateTime.Now.LocalTime - serviceKillConfig.LastKillTime > serviceKillConfig.DurationBetweenKill)
							{
								if (req.CheckTime > startTime)
								{
									flag = true;
								}
								else
								{
									rep.IsSkippedDueToTimeCheck = true;
								}
							}
							else
							{
								rep.IsThrottled = true;
							}
							if (flag)
							{
								bool flag2 = false;
								DateTime lastKillTime = serviceKillConfig.LastKillTime;
								try
								{
									ReplayCrimsonEvents.KillingServiceProcess.Log<string, string, int, RpcKillServiceImpl.Request>(req.ServiceName, serviceProcess.ProcessName, serviceProcess.Id, req);
									serviceKillConfig.UpdateKillTime(DateTime.UtcNow.ToLocalTime());
									Exception ex = null;
									try
									{
										serviceProcess.Kill();
									}
									catch (Win32Exception ex2)
									{
										ex = ex2;
									}
									catch (InvalidOperationException ex3)
									{
										ex = ex3;
									}
									if (ex != null)
									{
										throw new FailedToKillProcessForServiceException(req.ServiceName, ex.Message);
									}
									flag2 = true;
									goto IL_194;
								}
								finally
								{
									if (!flag2)
									{
										serviceKillConfig.UpdateKillTime(lastKillTime);
									}
									rep.IsSucceeded = flag2;
								}
								goto IL_178;
							}
							IL_194:
							return;
						}
						IL_178:
						throw new FailedToGetProcessForServiceException(req.ServiceName, exception.Message);
					}
				}
				rep.IsSkippedDueToRegistryOverride = true;
			});
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00024790 File Offset: 0x00022990
		internal static RpcKillServiceImpl.Reply SendKillRequest(string serverName, string serviceName, DateTime minTimeCheck, bool isForce, int timeoutInMSec)
		{
			Exception ex = null;
			RpcKillServiceImpl.Reply reply = null;
			bool isSucceded = false;
			bool flag = true;
			try
			{
				ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
				{
					ReplayCrimsonEvents.SendingServiceKillRequest.Log<string, string>(serviceName, serverName);
					RpcKillServiceImpl.Request attachedRequest = new RpcKillServiceImpl.Request(serviceName, minTimeCheck, false);
					RpcGenericRequestInfo requestInfo = ActiveManagerGenericRpcHelper.PrepareClientRequest(attachedRequest, 1, 1, 0);
					reply = ActiveManagerGenericRpcHelper.RunRpcAndGetReply<RpcKillServiceImpl.Reply>(requestInfo, serverName, timeoutInMSec);
					isSucceded = reply.IsSucceeded;
				});
				flag = false;
			}
			finally
			{
				if (isSucceded)
				{
					ReplayCrimsonEvents.ServiceKillRequestSucceeded.Log<string, string, string>(serviceName, serverName, reply.ToString());
				}
				else
				{
					string text = "Unknown";
					if (ex != null)
					{
						text = string.Format("Error: {0}", ex.Message);
					}
					else if (reply != null)
					{
						if (reply.IsThrottled)
						{
							text = "Throttled";
						}
						else if (reply.IsSkippedDueToTimeCheck)
						{
							text = "SkippedDueToTimeCheck";
						}
						else if (reply.IsSkippedDueToRegistryOverride)
						{
							text = "SkippedDueToRegistryOverride";
						}
					}
					ReplayCrimsonEvents.ServiceKillRequestFailed.Log<string, string, bool, string, bool, string>(serviceName, serverName, reply != null && reply.IsThrottled, text, flag, (reply != null) ? reply.ToString() : "<null>");
				}
			}
			return reply;
		}

		// Token: 0x04000346 RID: 838
		public const int MajorVersion = 1;

		// Token: 0x04000347 RID: 839
		public const int MinorVersion = 0;

		// Token: 0x04000348 RID: 840
		public const int CommandCode = 1;

		// Token: 0x04000349 RID: 841
		private static object locker = new object();

		// Token: 0x020000B6 RID: 182
		[Serializable]
		internal class Request
		{
			// Token: 0x06000778 RID: 1912 RVA: 0x00024904 File Offset: 0x00022B04
			public Request(string serviceName, DateTime checkTime, bool isForce)
			{
				this.ServiceName = serviceName;
				this.CheckTime = checkTime;
				this.IsForce = isForce;
				this.OriginatingServerName = AmServerName.LocalComputerName.Fqdn;
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x06000779 RID: 1913 RVA: 0x00024936 File Offset: 0x00022B36
			// (set) Token: 0x0600077A RID: 1914 RVA: 0x0002493E File Offset: 0x00022B3E
			public string OriginatingServerName { get; set; }

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x0600077B RID: 1915 RVA: 0x00024947 File Offset: 0x00022B47
			// (set) Token: 0x0600077C RID: 1916 RVA: 0x0002494F File Offset: 0x00022B4F
			public string ServiceName { get; private set; }

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x0600077D RID: 1917 RVA: 0x00024958 File Offset: 0x00022B58
			// (set) Token: 0x0600077E RID: 1918 RVA: 0x00024960 File Offset: 0x00022B60
			public DateTimeOffset CheckTime { get; private set; }

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x0600077F RID: 1919 RVA: 0x00024969 File Offset: 0x00022B69
			// (set) Token: 0x06000780 RID: 1920 RVA: 0x00024971 File Offset: 0x00022B71
			public bool IsForce { get; private set; }

			// Token: 0x06000781 RID: 1921 RVA: 0x0002497A File Offset: 0x00022B7A
			public override string ToString()
			{
				return string.Format("ServiceName: '{0}' CheckTime: '{1}' IsForce: '{2}'", this.ServiceName, this.CheckTime, this.IsForce);
			}
		}

		// Token: 0x020000B7 RID: 183
		[Serializable]
		internal class Reply
		{
			// Token: 0x17000199 RID: 409
			// (get) Token: 0x06000782 RID: 1922 RVA: 0x000249A2 File Offset: 0x00022BA2
			// (set) Token: 0x06000783 RID: 1923 RVA: 0x000249AA File Offset: 0x00022BAA
			public bool IsSucceeded { get; set; }

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x06000784 RID: 1924 RVA: 0x000249B3 File Offset: 0x00022BB3
			// (set) Token: 0x06000785 RID: 1925 RVA: 0x000249BB File Offset: 0x00022BBB
			public bool IsSkippedDueToTimeCheck { get; set; }

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x06000786 RID: 1926 RVA: 0x000249C4 File Offset: 0x00022BC4
			// (set) Token: 0x06000787 RID: 1927 RVA: 0x000249CC File Offset: 0x00022BCC
			public bool IsSkippedDueToRegistryOverride { get; set; }

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000788 RID: 1928 RVA: 0x000249D5 File Offset: 0x00022BD5
			// (set) Token: 0x06000789 RID: 1929 RVA: 0x000249DD File Offset: 0x00022BDD
			public bool IsThrottled { get; set; }

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x0600078A RID: 1930 RVA: 0x000249E6 File Offset: 0x00022BE6
			// (set) Token: 0x0600078B RID: 1931 RVA: 0x000249EE File Offset: 0x00022BEE
			public DateTimeOffset ProcessStartTime { get; set; }

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x0600078C RID: 1932 RVA: 0x000249F7 File Offset: 0x00022BF7
			// (set) Token: 0x0600078D RID: 1933 RVA: 0x000249FF File Offset: 0x00022BFF
			public DateTimeOffset LastKillTime { get; set; }

			// Token: 0x0600078E RID: 1934 RVA: 0x00024A08 File Offset: 0x00022C08
			public override string ToString()
			{
				return string.Format("IsSucceeded: '{0}' ProcessStartTime: '{1}' LastKillTime: '{2}' IsThrottled: '{3}' IsSkippedDueToTimeCheck: '{4}' IsSkippedDueToRegistryOverride: '{5}'", new object[]
				{
					this.IsSucceeded,
					this.ProcessStartTime,
					this.LastKillTime,
					this.IsThrottled,
					this.IsSkippedDueToTimeCheck,
					this.IsSkippedDueToRegistryOverride
				});
			}
		}
	}
}

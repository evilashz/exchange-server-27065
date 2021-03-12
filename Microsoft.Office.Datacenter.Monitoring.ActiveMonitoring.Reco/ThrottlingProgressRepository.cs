using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200004B RID: 75
	internal class ThrottlingProgressRepository
	{
		// Token: 0x0600032C RID: 812 RVA: 0x0000B224 File Offset: 0x00009424
		internal ThrottlingProgressRepository()
		{
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000B254 File Offset: 0x00009454
		internal RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo Get(RecoveryActionId actionId, string resourceName)
		{
			Dictionary<string, RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo> dictionary = null;
			RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo result = null;
			lock (this.locker)
			{
				if (this.ridResMap.TryGetValue(actionId, out dictionary) && dictionary != null)
				{
					if (!string.IsNullOrEmpty(resourceName))
					{
						dictionary.TryGetValue(resourceName, out result);
					}
					else
					{
						result = (from pi in dictionary.Values
						where pi != null
						orderby pi.OperationStartTime descending
						select pi).FirstOrDefault<RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo>();
					}
				}
			}
			return result;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000B30C File Offset: 0x0000950C
		internal RpcSetThrottlingInProgressImpl.Reply Update(RpcSetThrottlingInProgressImpl.Request req)
		{
			RpcSetThrottlingInProgressImpl.Reply result;
			if (req.IsClear)
			{
				result = this.Delete(req.ProgressInfo, req.IsForce);
			}
			else
			{
				result = this.Add(req.ProgressInfo, req.IsForce);
			}
			return result;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000B34C File Offset: 0x0000954C
		private RpcSetThrottlingInProgressImpl.Reply Add(RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo pi, bool isForce)
		{
			RpcSetThrottlingInProgressImpl.Reply reply = new RpcSetThrottlingInProgressImpl.Reply();
			Dictionary<string, RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo> dictionary = null;
			lock (this.locker)
			{
				if (!this.ridResMap.TryGetValue(pi.ActionId, out dictionary))
				{
					dictionary = new Dictionary<string, RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo>();
					this.ridResMap[pi.ActionId] = dictionary;
				}
				RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo throttlingProgressInfo = null;
				bool flag2 = true;
				if (dictionary.TryGetValue(pi.ResourceName, out throttlingProgressInfo) && throttlingProgressInfo.IsInProgress(WorkerProcessRepository.Instance.GetWorkerProcessStartTime()))
				{
					reply.IsThrottlingAlreadyInProgress = true;
					reply.CurrentProgressInfo = throttlingProgressInfo;
					flag2 = false;
				}
				if (flag2 || isForce)
				{
					dictionary[pi.ResourceName] = pi;
					reply.IsThrottlingAlreadyInProgress = false;
					reply.CurrentProgressInfo = pi;
					reply.IsSuccess = true;
				}
			}
			return reply;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B420 File Offset: 0x00009620
		private RpcSetThrottlingInProgressImpl.Reply Delete(RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo pi, bool isForce)
		{
			RpcSetThrottlingInProgressImpl.Reply reply = new RpcSetThrottlingInProgressImpl.Reply();
			reply.IsSuccess = true;
			Dictionary<string, RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo> dictionary = null;
			RpcSetThrottlingInProgressImpl.Reply result;
			lock (this.locker)
			{
				if (this.ridResMap.TryGetValue(pi.ActionId, out dictionary))
				{
					RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo throttlingProgressInfo = null;
					bool flag2 = dictionary.TryGetValue(pi.ResourceName, out throttlingProgressInfo);
					if (isForce)
					{
						dictionary.Remove(pi.ResourceName);
						reply.IsThrottlingAlreadyInProgress = false;
						reply.CurrentProgressInfo = null;
					}
					else if (flag2)
					{
						if (pi.InstanceId == throttlingProgressInfo.InstanceId)
						{
							dictionary.Remove(pi.ResourceName);
							reply.IsThrottlingAlreadyInProgress = false;
							reply.CurrentProgressInfo = null;
						}
						else if (throttlingProgressInfo.IsInProgress(WorkerProcessRepository.Instance.GetWorkerProcessStartTime()))
						{
							reply.IsThrottlingAlreadyInProgress = true;
							reply.CurrentProgressInfo = throttlingProgressInfo;
						}
						else
						{
							dictionary.Remove(pi.ResourceName);
						}
					}
					if (dictionary.Count == 0)
					{
						this.ridResMap.Remove(pi.ActionId);
					}
				}
				result = reply;
			}
			return result;
		}

		// Token: 0x040001EA RID: 490
		private object locker = new object();

		// Token: 0x040001EB RID: 491
		private Dictionary<RecoveryActionId, Dictionary<string, RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo>> ridResMap = new Dictionary<RecoveryActionId, Dictionary<string, RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo>>();
	}
}

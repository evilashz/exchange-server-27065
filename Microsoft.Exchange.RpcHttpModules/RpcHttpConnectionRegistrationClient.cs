using System;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000007 RID: 7
	public class RpcHttpConnectionRegistrationClient : IRpcHttpConnectionRegistration
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000022B8 File Offset: 0x000004B8
		public int Register(Guid associationGroupId, string token, string serverTarget, string sessionCookie, string clientIp, Guid requestId, out string failureMessage, out string failureDetails)
		{
			int result;
			try
			{
				using (RpcHttpConnectionRegistrationRpcClient registrationClient = RpcHttpConnectionRegistrationClient.GetRegistrationClient())
				{
					result = registrationClient.Register(associationGroupId, token, serverTarget, sessionCookie, clientIp, requestId, out failureMessage, out failureDetails);
				}
			}
			catch (RpcException ex)
			{
				throw new RpcHttpConnectionRegistrationInternalException(string.Format("RpcHttpConnectionRegistrationClient::Register RPC failed: {0}: ", ex.ErrorCode), ex);
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002344 File Offset: 0x00000544
		public void Unregister(Guid associationGroupId, Guid requestId)
		{
			RpcHttpConnectionRegistrationClient.Execute("Unregister", (RpcHttpConnectionRegistrationRpcClient rpcHttpConnectionRegistration) => rpcHttpConnectionRegistration.Unregister(associationGroupId, requestId));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002383 File Offset: 0x00000583
		public void Clear()
		{
			RpcHttpConnectionRegistrationClient.Execute("Clear", (RpcHttpConnectionRegistrationRpcClient rpcHttpConnectionRegistration) => rpcHttpConnectionRegistration.Clear());
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023AC File Offset: 0x000005AC
		private static void Execute(string methodName, Func<RpcHttpConnectionRegistrationRpcClient, int> delegateFunc)
		{
			try
			{
				using (RpcHttpConnectionRegistrationRpcClient registrationClient = RpcHttpConnectionRegistrationClient.GetRegistrationClient())
				{
					int num = delegateFunc(registrationClient);
					if (num != 0)
					{
						throw new RpcHttpConnectionRegistrationException(string.Format("RpcHttpConnectionRegistrationClient::{0} call failed with error code = {1}", methodName, num), num);
					}
				}
			}
			catch (RpcException ex)
			{
				throw new RpcHttpConnectionRegistrationInternalException(string.Format("RpcHttpConnectionRegistrationClient::{0} RPC failed: {1}: ", methodName, ex.ErrorCode), ex);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000242C File Offset: 0x0000062C
		private static RpcHttpConnectionRegistrationRpcClient GetRegistrationClient()
		{
			return new RpcHttpConnectionRegistrationRpcClient();
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.SubscriptionCache;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Rpc;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Cache;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x0200003E RID: 62
	internal sealed class SubscriptionCacheClient
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000B955 File Offset: 0x00009B55
		private SubscriptionCacheClient()
		{
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000B960 File Offset: 0x00009B60
		internal static bool TryTestUserCache(string databaseServer, string primarySmtpAddress, SubscriptionCacheAction cacheAction, out string failureReason, out uint cacheActionResult, out List<SubscriptionCacheObject> cacheObjects, out ObjectState objectState)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("databaseServer", databaseServer);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("primarySmtpAddress", primarySmtpAddress);
			failureReason = null;
			cacheActionResult = 268435456U;
			cacheObjects = null;
			objectState = ObjectState.Unchanged;
			byte[] testUserCacheInputBytes = SubscriptionCacheClient.GetTestUserCacheInputBytes(primarySmtpAddress, cacheAction);
			byte[] array = null;
			using (SubscriptionCacheRpcClient subscriptionCacheRpcClient = new SubscriptionCacheRpcClient(databaseServer))
			{
				try
				{
					array = subscriptionCacheRpcClient.TestUserCache(0, testUserCacheInputBytes);
				}
				catch (RpcException exception)
				{
					failureReason = Strings.CacheRpcExceptionEncountered(exception);
					return false;
				}
			}
			MdbefPropertyCollection args = MdbefPropertyCollection.Create(array, 0, array.Length);
			int num;
			if (!RpcHelper.TryGetProperty<int>(args, 2835349507U, out num))
			{
				failureReason = Strings.CacheRpcInvalidServerVersionIssue(databaseServer);
				return false;
			}
			cacheActionResult = (uint)num;
			RpcHelper.TryGetProperty<string>(args, 2835415071U, out failureReason);
			byte[] buffer;
			if (RpcHelper.TryGetProperty<byte[]>(args, 2835480834U, out buffer))
			{
				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					cacheObjects = (List<SubscriptionCacheObject>)SubscriptionCacheClient.binaryFormatter.Deserialize(memoryStream);
				}
			}
			if (RpcHelper.TryGetProperty<int>(args, 2835546115U, out num))
			{
				objectState = (ObjectState)num;
			}
			return true;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000BA90 File Offset: 0x00009C90
		private static byte[] GetTestUserCacheInputBytes(string primarySmtpAddress, SubscriptionCacheAction cacheAction)
		{
			MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
			mdbefPropertyCollection[2684485663U] = primarySmtpAddress;
			mdbefPropertyCollection[2684420099U] = (int)cacheAction;
			return mdbefPropertyCollection.GetBytes();
		}

		// Token: 0x040000AD RID: 173
		private static readonly BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);

		// Token: 0x040000AE RID: 174
		private static readonly NetworkCredential localSystemCredential = new NetworkCredential(Environment.MachineName + "$", string.Empty, string.Empty);
	}
}

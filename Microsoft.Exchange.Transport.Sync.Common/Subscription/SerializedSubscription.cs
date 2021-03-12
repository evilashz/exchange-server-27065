using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000C2 RID: 194
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SerializedSubscription
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x0001B7B0 File Offset: 0x000199B0
		private SerializedSubscription(AggregationSubscriptionType subscriptionType, long version, byte[] serializedData)
		{
			this.subscriptionType = subscriptionType;
			this.version = version;
			this.serializedData = serializedData;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0001B7CD File Offset: 0x000199CD
		public long Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001B7D8 File Offset: 0x000199D8
		public static SerializedSubscription FromSubscription(AggregationSubscription subscription)
		{
			SerializedSubscription result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					subscription.Serialize(binaryWriter);
				}
				SerializedSubscription serializedSubscription = new SerializedSubscription(subscription.SubscriptionType, subscription.Version, memoryStream.ToArray());
				result = serializedSubscription;
			}
			return result;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001B848 File Offset: 0x00019A48
		public static SerializedSubscription FromReader(BinaryReader reader)
		{
			int count = reader.ReadInt32();
			byte[] array = reader.ReadBytes(count);
			return SerializedSubscription.FromBytes(array);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001B86C File Offset: 0x00019A6C
		public static SerializedSubscription FromBytes(byte[] serializedData)
		{
			long num;
			AggregationSubscriptionType aggregationSubscriptionType;
			using (MemoryStream memoryStream = new MemoryStream(serializedData))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					AggregationSubscription.DeserializeVersionAndSubscriptionType(binaryReader, out num, out aggregationSubscriptionType);
				}
			}
			return new SerializedSubscription(aggregationSubscriptionType, num, serializedData);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001B8D4 File Offset: 0x00019AD4
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this.serializedData.Length);
			writer.Write(this.serializedData);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		public byte[] GetBytes()
		{
			return this.serializedData;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		public bool TryDeserializeSubscription(out AggregationSubscription subscription)
		{
			bool result;
			try
			{
				subscription = this.DeserializeSubscription();
				result = true;
			}
			catch (SerializationException ex)
			{
				CommonLoggingHelper.SyncLogSession.LogError((TSLID)155UL, string.Format(CultureInfo.InvariantCulture, "Exception encountered while deserializing the subscription: {0}", new object[]
				{
					ex
				}), new object[0]);
				subscription = null;
				result = false;
			}
			return result;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001B960 File Offset: 0x00019B60
		public AggregationSubscription DeserializeSubscription()
		{
			if (this.version != AggregationSubscription.CurrentSupportedVersion)
			{
				throw new SerializationException("Serialized subscription is an unsupported version");
			}
			AggregationSubscription aggregationSubscription;
			if (this.subscriptionType == AggregationSubscriptionType.Pop)
			{
				aggregationSubscription = new PopAggregationSubscription();
			}
			else if (this.subscriptionType == AggregationSubscriptionType.IMAP)
			{
				aggregationSubscription = new IMAPAggregationSubscription();
			}
			else if (this.subscriptionType == AggregationSubscriptionType.DeltaSyncMail)
			{
				aggregationSubscription = new DeltaSyncAggregationSubscription();
			}
			else if (this.subscriptionType == AggregationSubscriptionType.Facebook)
			{
				aggregationSubscription = new ConnectSubscription();
			}
			else
			{
				if (this.subscriptionType != AggregationSubscriptionType.LinkedIn)
				{
					throw new InvalidOperationException("unsupported subscription type");
				}
				aggregationSubscription = new ConnectSubscription();
			}
			using (MemoryStream memoryStream = new MemoryStream(this.serializedData))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					aggregationSubscription.Deserialize(binaryReader);
				}
			}
			return aggregationSubscription;
		}

		// Token: 0x04000315 RID: 789
		private readonly AggregationSubscriptionType subscriptionType;

		// Token: 0x04000316 RID: 790
		private readonly long version;

		// Token: 0x04000317 RID: 791
		private readonly byte[] serializedData;
	}
}

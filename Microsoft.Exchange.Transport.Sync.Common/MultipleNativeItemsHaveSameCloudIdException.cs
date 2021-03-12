using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200004D RID: 77
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MultipleNativeItemsHaveSameCloudIdException : LocalizedException
	{
		// Token: 0x0600020D RID: 525 RVA: 0x000061E6 File Offset: 0x000043E6
		public MultipleNativeItemsHaveSameCloudIdException(string cloudId, Guid subscriptionGuid) : base(Strings.MultipleNativeItemsHaveSameCloudIdException(cloudId, subscriptionGuid))
		{
			this.cloudId = cloudId;
			this.subscriptionGuid = subscriptionGuid;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006203 File Offset: 0x00004403
		public MultipleNativeItemsHaveSameCloudIdException(string cloudId, Guid subscriptionGuid, Exception innerException) : base(Strings.MultipleNativeItemsHaveSameCloudIdException(cloudId, subscriptionGuid), innerException)
		{
			this.cloudId = cloudId;
			this.subscriptionGuid = subscriptionGuid;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006224 File Offset: 0x00004424
		protected MultipleNativeItemsHaveSameCloudIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.cloudId = (string)info.GetValue("cloudId", typeof(string));
			this.subscriptionGuid = (Guid)info.GetValue("subscriptionGuid", typeof(Guid));
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006279 File Offset: 0x00004479
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("cloudId", this.cloudId);
			info.AddValue("subscriptionGuid", this.subscriptionGuid);
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000062AA File Offset: 0x000044AA
		public string CloudId
		{
			get
			{
				return this.cloudId;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000062B2 File Offset: 0x000044B2
		public Guid SubscriptionGuid
		{
			get
			{
				return this.subscriptionGuid;
			}
		}

		// Token: 0x040000F6 RID: 246
		private readonly string cloudId;

		// Token: 0x040000F7 RID: 247
		private readonly Guid subscriptionGuid;
	}
}

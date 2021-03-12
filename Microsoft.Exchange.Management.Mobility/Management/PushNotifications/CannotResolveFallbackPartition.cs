using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Mobility;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x02000064 RID: 100
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotResolveFallbackPartition : LocalizedException
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x00010284 File Offset: 0x0000E484
		public CannotResolveFallbackPartition(string appId, string currentPartition) : base(Strings.PushNotificationFailedToResolveFallbackPartition(appId, currentPartition))
		{
			this.appId = appId;
			this.currentPartition = currentPartition;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000102A1 File Offset: 0x0000E4A1
		public CannotResolveFallbackPartition(string appId, string currentPartition, Exception innerException) : base(Strings.PushNotificationFailedToResolveFallbackPartition(appId, currentPartition), innerException)
		{
			this.appId = appId;
			this.currentPartition = currentPartition;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000102C0 File Offset: 0x0000E4C0
		protected CannotResolveFallbackPartition(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.appId = (string)info.GetValue("appId", typeof(string));
			this.currentPartition = (string)info.GetValue("currentPartition", typeof(string));
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00010315 File Offset: 0x0000E515
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("appId", this.appId);
			info.AddValue("currentPartition", this.currentPartition);
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00010341 File Offset: 0x0000E541
		public string AppId
		{
			get
			{
				return this.appId;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00010349 File Offset: 0x0000E549
		public string CurrentPartition
		{
			get
			{
				return this.currentPartition;
			}
		}

		// Token: 0x04000159 RID: 345
		private readonly string appId;

		// Token: 0x0400015A RID: 346
		private readonly string currentPartition;
	}
}

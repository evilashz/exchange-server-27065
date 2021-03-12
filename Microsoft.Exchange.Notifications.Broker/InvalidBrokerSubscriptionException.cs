using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000041 RID: 65
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidBrokerSubscriptionException : NotificationsBrokerPermanentException
	{
		// Token: 0x06000264 RID: 612 RVA: 0x0000CE89 File Offset: 0x0000B089
		public InvalidBrokerSubscriptionException(string json) : base(ServiceStrings.InvalidBrokerSubscriptionException(json))
		{
			this.json = json;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000CE9E File Offset: 0x0000B09E
		public InvalidBrokerSubscriptionException(string json, Exception innerException) : base(ServiceStrings.InvalidBrokerSubscriptionException(json), innerException)
		{
			this.json = json;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		protected InvalidBrokerSubscriptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.json = (string)info.GetValue("json", typeof(string));
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000CEDE File Offset: 0x0000B0DE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("json", this.json);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000CEF9 File Offset: 0x0000B0F9
		public string Json
		{
			get
			{
				return this.json;
			}
		}

		// Token: 0x04000120 RID: 288
		private readonly string json;
	}
}

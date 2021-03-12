using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E30 RID: 3632
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorDeviceIdInBothLists : LocalizedException
	{
		// Token: 0x0600A5F9 RID: 42489 RVA: 0x00286F0D File Offset: 0x0028510D
		public ErrorDeviceIdInBothLists(string deviceId) : base(Strings.ErrorDeviceIdInBothLists(deviceId))
		{
			this.deviceId = deviceId;
		}

		// Token: 0x0600A5FA RID: 42490 RVA: 0x00286F22 File Offset: 0x00285122
		public ErrorDeviceIdInBothLists(string deviceId, Exception innerException) : base(Strings.ErrorDeviceIdInBothLists(deviceId), innerException)
		{
			this.deviceId = deviceId;
		}

		// Token: 0x0600A5FB RID: 42491 RVA: 0x00286F38 File Offset: 0x00285138
		protected ErrorDeviceIdInBothLists(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.deviceId = (string)info.GetValue("deviceId", typeof(string));
		}

		// Token: 0x0600A5FC RID: 42492 RVA: 0x00286F62 File Offset: 0x00285162
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("deviceId", this.deviceId);
		}

		// Token: 0x17003656 RID: 13910
		// (get) Token: 0x0600A5FD RID: 42493 RVA: 0x00286F7D File Offset: 0x0028517D
		public string DeviceId
		{
			get
			{
				return this.deviceId;
			}
		}

		// Token: 0x04005FBC RID: 24508
		private readonly string deviceId;
	}
}

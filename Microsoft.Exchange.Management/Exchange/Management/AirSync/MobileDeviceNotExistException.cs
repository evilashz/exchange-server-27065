using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E2A RID: 3626
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileDeviceNotExistException : LocalizedException
	{
		// Token: 0x0600A5D8 RID: 42456 RVA: 0x00286B45 File Offset: 0x00284D45
		public MobileDeviceNotExistException(string deviceId) : base(Strings.MobileDeviceNotExistException(deviceId))
		{
			this.deviceId = deviceId;
		}

		// Token: 0x0600A5D9 RID: 42457 RVA: 0x00286B5A File Offset: 0x00284D5A
		public MobileDeviceNotExistException(string deviceId, Exception innerException) : base(Strings.MobileDeviceNotExistException(deviceId), innerException)
		{
			this.deviceId = deviceId;
		}

		// Token: 0x0600A5DA RID: 42458 RVA: 0x00286B70 File Offset: 0x00284D70
		protected MobileDeviceNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.deviceId = (string)info.GetValue("deviceId", typeof(string));
		}

		// Token: 0x0600A5DB RID: 42459 RVA: 0x00286B9A File Offset: 0x00284D9A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("deviceId", this.deviceId);
		}

		// Token: 0x1700364D RID: 13901
		// (get) Token: 0x0600A5DC RID: 42460 RVA: 0x00286BB5 File Offset: 0x00284DB5
		public string DeviceId
		{
			get
			{
				return this.deviceId;
			}
		}

		// Token: 0x04005FB3 RID: 24499
		private readonly string deviceId;
	}
}

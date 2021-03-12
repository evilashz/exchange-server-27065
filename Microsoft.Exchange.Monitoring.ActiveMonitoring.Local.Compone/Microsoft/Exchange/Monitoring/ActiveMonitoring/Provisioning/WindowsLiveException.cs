using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Provisioning
{
	// Token: 0x02000406 RID: 1030
	[Serializable]
	public class WindowsLiveException : Exception
	{
		// Token: 0x06001A1C RID: 6684 RVA: 0x0008DF5E File Offset: 0x0008C15E
		public WindowsLiveException()
		{
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0008DF66 File Offset: 0x0008C166
		public WindowsLiveException(string message) : base(message)
		{
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0008DF6F File Offset: 0x0008C16F
		public WindowsLiveException(int errorCodeParameter, string message) : base(message)
		{
			this.errorCode = errorCodeParameter;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0008DF7F File Offset: 0x0008C17F
		public WindowsLiveException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0008DF89 File Offset: 0x0008C189
		protected WindowsLiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x0008DF93 File Offset: 0x0008C193
		public int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0008DF9B File Offset: 0x0008C19B
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ErrorCode", this.ErrorCode);
			base.GetObjectData(info, context);
		}

		// Token: 0x040011CA RID: 4554
		private readonly int errorCode;
	}
}

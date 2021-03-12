using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Exchange.UM.UMService.Exceptions;

namespace Microsoft.Exchange.UM.UMService
{
	// Token: 0x02000228 RID: 552
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UMServiceHeartbeatException : UMServiceBaseException
	{
		// Token: 0x0600118B RID: 4491 RVA: 0x0003A796 File Offset: 0x00038996
		public UMServiceHeartbeatException(string extraInfo) : base(Strings.UMServiceHeartbeatException(extraInfo))
		{
			this.extraInfo = extraInfo;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0003A7B0 File Offset: 0x000389B0
		public UMServiceHeartbeatException(string extraInfo, Exception innerException) : base(Strings.UMServiceHeartbeatException(extraInfo), innerException)
		{
			this.extraInfo = extraInfo;
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0003A7CB File Offset: 0x000389CB
		protected UMServiceHeartbeatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.extraInfo = (string)info.GetValue("extraInfo", typeof(string));
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0003A7F5 File Offset: 0x000389F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("extraInfo", this.extraInfo);
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0003A810 File Offset: 0x00038A10
		public string ExtraInfo
		{
			get
			{
				return this.extraInfo;
			}
		}

		// Token: 0x040008C5 RID: 2245
		private readonly string extraInfo;
	}
}

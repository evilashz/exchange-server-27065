using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001E7 RID: 487
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMMailboxOperationException : LocalizedException
	{
		// Token: 0x06000FA1 RID: 4001 RVA: 0x00036B28 File Offset: 0x00034D28
		public UMMailboxOperationException(string moreInfo) : base(Strings.ErrorPerformingUMMailboxOperation(moreInfo))
		{
			this.moreInfo = moreInfo;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00036B3D File Offset: 0x00034D3D
		public UMMailboxOperationException(string moreInfo, Exception innerException) : base(Strings.ErrorPerformingUMMailboxOperation(moreInfo), innerException)
		{
			this.moreInfo = moreInfo;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00036B53 File Offset: 0x00034D53
		protected UMMailboxOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.moreInfo = (string)info.GetValue("moreInfo", typeof(string));
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00036B7D File Offset: 0x00034D7D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("moreInfo", this.moreInfo);
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00036B98 File Offset: 0x00034D98
		public string MoreInfo
		{
			get
			{
				return this.moreInfo;
			}
		}

		// Token: 0x040007B5 RID: 1973
		private readonly string moreInfo;
	}
}

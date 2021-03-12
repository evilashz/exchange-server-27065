using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001E6 RID: 486
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CDROperationException : LocalizedException
	{
		// Token: 0x06000F9C RID: 3996 RVA: 0x00036AB0 File Offset: 0x00034CB0
		public CDROperationException(string moreInfo) : base(Strings.ErrorPerformingCDROperation(moreInfo))
		{
			this.moreInfo = moreInfo;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00036AC5 File Offset: 0x00034CC5
		public CDROperationException(string moreInfo, Exception innerException) : base(Strings.ErrorPerformingCDROperation(moreInfo), innerException)
		{
			this.moreInfo = moreInfo;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00036ADB File Offset: 0x00034CDB
		protected CDROperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.moreInfo = (string)info.GetValue("moreInfo", typeof(string));
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00036B05 File Offset: 0x00034D05
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("moreInfo", this.moreInfo);
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00036B20 File Offset: 0x00034D20
		public string MoreInfo
		{
			get
			{
				return this.moreInfo;
			}
		}

		// Token: 0x040007B4 RID: 1972
		private readonly string moreInfo;
	}
}

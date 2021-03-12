using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011E2 RID: 4578
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NewPublishingPointException : LocalizedException
	{
		// Token: 0x0600B949 RID: 47433 RVA: 0x002A5B9E File Offset: 0x002A3D9E
		public NewPublishingPointException(string shareName, string moreInfo) : base(Strings.NewPublishingPointException(shareName, moreInfo))
		{
			this.shareName = shareName;
			this.moreInfo = moreInfo;
		}

		// Token: 0x0600B94A RID: 47434 RVA: 0x002A5BBB File Offset: 0x002A3DBB
		public NewPublishingPointException(string shareName, string moreInfo, Exception innerException) : base(Strings.NewPublishingPointException(shareName, moreInfo), innerException)
		{
			this.shareName = shareName;
			this.moreInfo = moreInfo;
		}

		// Token: 0x0600B94B RID: 47435 RVA: 0x002A5BDC File Offset: 0x002A3DDC
		protected NewPublishingPointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.shareName = (string)info.GetValue("shareName", typeof(string));
			this.moreInfo = (string)info.GetValue("moreInfo", typeof(string));
		}

		// Token: 0x0600B94C RID: 47436 RVA: 0x002A5C31 File Offset: 0x002A3E31
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("shareName", this.shareName);
			info.AddValue("moreInfo", this.moreInfo);
		}

		// Token: 0x17003A3E RID: 14910
		// (get) Token: 0x0600B94D RID: 47437 RVA: 0x002A5C5D File Offset: 0x002A3E5D
		public string ShareName
		{
			get
			{
				return this.shareName;
			}
		}

		// Token: 0x17003A3F RID: 14911
		// (get) Token: 0x0600B94E RID: 47438 RVA: 0x002A5C65 File Offset: 0x002A3E65
		public string MoreInfo
		{
			get
			{
				return this.moreInfo;
			}
		}

		// Token: 0x04006459 RID: 25689
		private readonly string shareName;

		// Token: 0x0400645A RID: 25690
		private readonly string moreInfo;
	}
}

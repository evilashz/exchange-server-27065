using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011E3 RID: 4579
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemovePublishingPointException : LocalizedException
	{
		// Token: 0x0600B94F RID: 47439 RVA: 0x002A5C6D File Offset: 0x002A3E6D
		public RemovePublishingPointException(string shareName, string moreInfo) : base(Strings.RemovePublishingPointException(shareName, moreInfo))
		{
			this.shareName = shareName;
			this.moreInfo = moreInfo;
		}

		// Token: 0x0600B950 RID: 47440 RVA: 0x002A5C8A File Offset: 0x002A3E8A
		public RemovePublishingPointException(string shareName, string moreInfo, Exception innerException) : base(Strings.RemovePublishingPointException(shareName, moreInfo), innerException)
		{
			this.shareName = shareName;
			this.moreInfo = moreInfo;
		}

		// Token: 0x0600B951 RID: 47441 RVA: 0x002A5CA8 File Offset: 0x002A3EA8
		protected RemovePublishingPointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.shareName = (string)info.GetValue("shareName", typeof(string));
			this.moreInfo = (string)info.GetValue("moreInfo", typeof(string));
		}

		// Token: 0x0600B952 RID: 47442 RVA: 0x002A5CFD File Offset: 0x002A3EFD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("shareName", this.shareName);
			info.AddValue("moreInfo", this.moreInfo);
		}

		// Token: 0x17003A40 RID: 14912
		// (get) Token: 0x0600B953 RID: 47443 RVA: 0x002A5D29 File Offset: 0x002A3F29
		public string ShareName
		{
			get
			{
				return this.shareName;
			}
		}

		// Token: 0x17003A41 RID: 14913
		// (get) Token: 0x0600B954 RID: 47444 RVA: 0x002A5D31 File Offset: 0x002A3F31
		public string MoreInfo
		{
			get
			{
				return this.moreInfo;
			}
		}

		// Token: 0x0400645B RID: 25691
		private readonly string shareName;

		// Token: 0x0400645C RID: 25692
		private readonly string moreInfo;
	}
}

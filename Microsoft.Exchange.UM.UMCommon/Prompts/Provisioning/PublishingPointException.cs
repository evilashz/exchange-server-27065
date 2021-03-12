using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x020001C6 RID: 454
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PublishingPointException : PublishingException
	{
		// Token: 0x06000F04 RID: 3844 RVA: 0x00035DF0 File Offset: 0x00033FF0
		public PublishingPointException(string moreInfo) : base(Strings.ErrorAccessingPublishingPoint(moreInfo))
		{
			this.moreInfo = moreInfo;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00035E05 File Offset: 0x00034005
		public PublishingPointException(string moreInfo, Exception innerException) : base(Strings.ErrorAccessingPublishingPoint(moreInfo), innerException)
		{
			this.moreInfo = moreInfo;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00035E1B File Offset: 0x0003401B
		protected PublishingPointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.moreInfo = (string)info.GetValue("moreInfo", typeof(string));
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00035E45 File Offset: 0x00034045
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("moreInfo", this.moreInfo);
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00035E60 File Offset: 0x00034060
		public string MoreInfo
		{
			get
			{
				return this.moreInfo;
			}
		}

		// Token: 0x0400079C RID: 1948
		private readonly string moreInfo;
	}
}

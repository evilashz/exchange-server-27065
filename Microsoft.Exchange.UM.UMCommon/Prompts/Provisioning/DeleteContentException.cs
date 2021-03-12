using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x020001C9 RID: 457
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DeleteContentException : PublishingException
	{
		// Token: 0x06000F13 RID: 3859 RVA: 0x00035F58 File Offset: 0x00034158
		public DeleteContentException(string moreInfo) : base(Strings.DeleteContentException(moreInfo))
		{
			this.moreInfo = moreInfo;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00035F6D File Offset: 0x0003416D
		public DeleteContentException(string moreInfo, Exception innerException) : base(Strings.DeleteContentException(moreInfo), innerException)
		{
			this.moreInfo = moreInfo;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00035F83 File Offset: 0x00034183
		protected DeleteContentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.moreInfo = (string)info.GetValue("moreInfo", typeof(string));
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00035FAD File Offset: 0x000341AD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("moreInfo", this.moreInfo);
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00035FC8 File Offset: 0x000341C8
		public string MoreInfo
		{
			get
			{
				return this.moreInfo;
			}
		}

		// Token: 0x0400079F RID: 1951
		private readonly string moreInfo;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D8B RID: 3467
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AnonymousPublishingUrlValidationException : StoragePermanentException
	{
		// Token: 0x06007763 RID: 30563 RVA: 0x0020EA72 File Offset: 0x0020CC72
		public AnonymousPublishingUrlValidationException(string url) : base(ServerStrings.AnonymousPublishingUrlValidationException(url))
		{
			this.url = url;
		}

		// Token: 0x06007764 RID: 30564 RVA: 0x0020EA87 File Offset: 0x0020CC87
		public AnonymousPublishingUrlValidationException(string url, Exception innerException) : base(ServerStrings.AnonymousPublishingUrlValidationException(url), innerException)
		{
			this.url = url;
		}

		// Token: 0x06007765 RID: 30565 RVA: 0x0020EA9D File Offset: 0x0020CC9D
		protected AnonymousPublishingUrlValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.url = (string)info.GetValue("url", typeof(string));
		}

		// Token: 0x06007766 RID: 30566 RVA: 0x0020EAC7 File Offset: 0x0020CCC7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("url", this.url);
		}

		// Token: 0x17001FE9 RID: 8169
		// (get) Token: 0x06007767 RID: 30567 RVA: 0x0020EAE2 File Offset: 0x0020CCE2
		public string Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x040052A9 RID: 21161
		private readonly string url;
	}
}

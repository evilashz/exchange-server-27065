using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000032 RID: 50
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnexpectedContentTypeException : LocalizedException
	{
		// Token: 0x06000194 RID: 404 RVA: 0x000058D2 File Offset: 0x00003AD2
		public UnexpectedContentTypeException(string contentType) : base(Strings.UnexpectedContentTypeException(contentType))
		{
			this.contentType = contentType;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000058E7 File Offset: 0x00003AE7
		public UnexpectedContentTypeException(string contentType, Exception innerException) : base(Strings.UnexpectedContentTypeException(contentType), innerException)
		{
			this.contentType = contentType;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000058FD File Offset: 0x00003AFD
		protected UnexpectedContentTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.contentType = (string)info.GetValue("contentType", typeof(string));
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005927 File Offset: 0x00003B27
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("contentType", this.contentType);
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00005942 File Offset: 0x00003B42
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x040000E9 RID: 233
		private readonly string contentType;
	}
}

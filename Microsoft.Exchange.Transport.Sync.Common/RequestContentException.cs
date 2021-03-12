using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200002F RID: 47
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RequestContentException : PermanentOperationLevelForItemsException
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0000576A File Offset: 0x0000396A
		public RequestContentException(int statusCode) : base(Strings.RequestContentException(statusCode))
		{
			this.statusCode = statusCode;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000577F File Offset: 0x0000397F
		public RequestContentException(int statusCode, Exception innerException) : base(Strings.RequestContentException(statusCode), innerException)
		{
			this.statusCode = statusCode;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005795 File Offset: 0x00003995
		protected RequestContentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.statusCode = (int)info.GetValue("statusCode", typeof(int));
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000057BF File Offset: 0x000039BF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("statusCode", this.statusCode);
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000057DA File Offset: 0x000039DA
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x040000E6 RID: 230
		private readonly int statusCode;
	}
}

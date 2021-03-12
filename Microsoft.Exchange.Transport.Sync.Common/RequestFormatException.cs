using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000030 RID: 48
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RequestFormatException : PermanentOperationLevelForItemsException
	{
		// Token: 0x0600018A RID: 394 RVA: 0x000057E2 File Offset: 0x000039E2
		public RequestFormatException(int statusCode) : base(Strings.RequestFormatException(statusCode))
		{
			this.statusCode = statusCode;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000057F7 File Offset: 0x000039F7
		public RequestFormatException(int statusCode, Exception innerException) : base(Strings.RequestFormatException(statusCode), innerException)
		{
			this.statusCode = statusCode;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000580D File Offset: 0x00003A0D
		protected RequestFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.statusCode = (int)info.GetValue("statusCode", typeof(int));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005837 File Offset: 0x00003A37
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("statusCode", this.statusCode);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00005852 File Offset: 0x00003A52
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x040000E7 RID: 231
		private readonly int statusCode;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000033 RID: 51
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnknownDeltaSyncException : PermanentOperationLevelForItemsException
	{
		// Token: 0x06000199 RID: 409 RVA: 0x0000594A File Offset: 0x00003B4A
		public UnknownDeltaSyncException(int statusCode) : base(Strings.UnknownDeltaSyncException(statusCode))
		{
			this.statusCode = statusCode;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000595F File Offset: 0x00003B5F
		public UnknownDeltaSyncException(int statusCode, Exception innerException) : base(Strings.UnknownDeltaSyncException(statusCode), innerException)
		{
			this.statusCode = statusCode;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005975 File Offset: 0x00003B75
		protected UnknownDeltaSyncException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.statusCode = (int)info.GetValue("statusCode", typeof(int));
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000599F File Offset: 0x00003B9F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("statusCode", this.statusCode);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000059BA File Offset: 0x00003BBA
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x040000EA RID: 234
		private readonly int statusCode;
	}
}

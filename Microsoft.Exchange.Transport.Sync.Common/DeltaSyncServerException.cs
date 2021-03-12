using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000022 RID: 34
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DeltaSyncServerException : TransientException
	{
		// Token: 0x06000153 RID: 339 RVA: 0x0000546D File Offset: 0x0000366D
		public DeltaSyncServerException(int statusCode) : base(Strings.DeltaSyncServerException(statusCode))
		{
			this.statusCode = statusCode;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005482 File Offset: 0x00003682
		public DeltaSyncServerException(int statusCode, Exception innerException) : base(Strings.DeltaSyncServerException(statusCode), innerException)
		{
			this.statusCode = statusCode;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005498 File Offset: 0x00003698
		protected DeltaSyncServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.statusCode = (int)info.GetValue("statusCode", typeof(int));
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000054C2 File Offset: 0x000036C2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("statusCode", this.statusCode);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000054DD File Offset: 0x000036DD
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x040000E3 RID: 227
		private readonly int statusCode;
	}
}

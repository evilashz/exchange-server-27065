using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000038 RID: 56
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnhandledException : LocalizedException
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00003A01 File Offset: 0x00001C01
		public UnhandledException(string typeName) : base(CXStrings.UnhandledError(typeName))
		{
			this.typeName = typeName;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003A16 File Offset: 0x00001C16
		public UnhandledException(string typeName, Exception innerException) : base(CXStrings.UnhandledError(typeName), innerException)
		{
			this.typeName = typeName;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003A2C File Offset: 0x00001C2C
		protected UnhandledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.typeName = (string)info.GetValue("typeName", typeof(string));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003A56 File Offset: 0x00001C56
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("typeName", this.typeName);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00003A71 File Offset: 0x00001C71
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x040000D3 RID: 211
		private readonly string typeName;
	}
}

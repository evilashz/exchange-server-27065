using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000007 RID: 7
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SyncUnhandledException : LocalizedException
	{
		// Token: 0x060000CE RID: 206 RVA: 0x0000487F File Offset: 0x00002A7F
		public SyncUnhandledException(Type type) : base(Strings.SyncUnhandledException(type))
		{
			this.type = type;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004894 File Offset: 0x00002A94
		public SyncUnhandledException(Type type, Exception innerException) : base(Strings.SyncUnhandledException(type), innerException)
		{
			this.type = type;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000048AA File Offset: 0x00002AAA
		protected SyncUnhandledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (Type)info.GetValue("type", typeof(Type));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000048D4 File Offset: 0x00002AD4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.type);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000048EF File Offset: 0x00002AEF
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x040000C8 RID: 200
		private readonly Type type;
	}
}

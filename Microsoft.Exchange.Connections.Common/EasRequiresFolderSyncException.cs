using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200005B RID: 91
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EasRequiresFolderSyncException : ConnectionsPermanentException
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00004B2E File Offset: 0x00002D2E
		public EasRequiresFolderSyncException(string msg) : base(CXStrings.EasRequiresFolderSyncExceptionMsg(msg))
		{
			this.msg = msg;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00004B43 File Offset: 0x00002D43
		public EasRequiresFolderSyncException(string msg, Exception innerException) : base(CXStrings.EasRequiresFolderSyncExceptionMsg(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00004B59 File Offset: 0x00002D59
		protected EasRequiresFolderSyncException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00004B83 File Offset: 0x00002D83
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00004B9E File Offset: 0x00002D9E
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040000F5 RID: 245
		private readonly string msg;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200000E RID: 14
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LiveClientFCodeException : LiveClientException
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002BC5 File Offset: 0x00000DC5
		public LiveClientFCodeException(int fCode, string msppErrorString) : base(Strings.LiveClientFCodeException(fCode, msppErrorString))
		{
			this.fCode = fCode;
			this.msppErrorString = msppErrorString;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BE2 File Offset: 0x00000DE2
		public LiveClientFCodeException(int fCode, string msppErrorString, Exception innerException) : base(Strings.LiveClientFCodeException(fCode, msppErrorString), innerException)
		{
			this.fCode = fCode;
			this.msppErrorString = msppErrorString;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C00 File Offset: 0x00000E00
		protected LiveClientFCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fCode = (int)info.GetValue("fCode", typeof(int));
			this.msppErrorString = (string)info.GetValue("msppErrorString", typeof(string));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C55 File Offset: 0x00000E55
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fCode", this.fCode);
			info.AddValue("msppErrorString", this.msppErrorString);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002C81 File Offset: 0x00000E81
		public int FCode
		{
			get
			{
				return this.fCode;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002C89 File Offset: 0x00000E89
		public string MsppErrorString
		{
			get
			{
				return this.msppErrorString;
			}
		}

		// Token: 0x0400002E RID: 46
		private readonly int fCode;

		// Token: 0x0400002F RID: 47
		private readonly string msppErrorString;
	}
}

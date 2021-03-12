using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200000D RID: 13
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LiveExternalUnknownFCodeException : LiveExternalException
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002AF9 File Offset: 0x00000CF9
		public LiveExternalUnknownFCodeException(string fCodeString, string msppErrorString) : base(Strings.LiveExternalUnknownFCodeException(fCodeString, msppErrorString))
		{
			this.fCodeString = fCodeString;
			this.msppErrorString = msppErrorString;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B16 File Offset: 0x00000D16
		public LiveExternalUnknownFCodeException(string fCodeString, string msppErrorString, Exception innerException) : base(Strings.LiveExternalUnknownFCodeException(fCodeString, msppErrorString), innerException)
		{
			this.fCodeString = fCodeString;
			this.msppErrorString = msppErrorString;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B34 File Offset: 0x00000D34
		protected LiveExternalUnknownFCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fCodeString = (string)info.GetValue("fCodeString", typeof(string));
			this.msppErrorString = (string)info.GetValue("msppErrorString", typeof(string));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B89 File Offset: 0x00000D89
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fCodeString", this.fCodeString);
			info.AddValue("msppErrorString", this.msppErrorString);
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002BB5 File Offset: 0x00000DB5
		public string FCodeString
		{
			get
			{
				return this.fCodeString;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002BBD File Offset: 0x00000DBD
		public string MsppErrorString
		{
			get
			{
				return this.msppErrorString;
			}
		}

		// Token: 0x0400002C RID: 44
		private readonly string fCodeString;

		// Token: 0x0400002D RID: 45
		private readonly string msppErrorString;
	}
}

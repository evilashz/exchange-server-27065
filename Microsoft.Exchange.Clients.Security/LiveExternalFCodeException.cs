using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200000C RID: 12
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LiveExternalFCodeException : LiveExternalException
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002A2D File Offset: 0x00000C2D
		public LiveExternalFCodeException(int fCode, string msppErrorString) : base(Strings.LiveExternalFCodeException(fCode, msppErrorString))
		{
			this.fCode = fCode;
			this.msppErrorString = msppErrorString;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A4A File Offset: 0x00000C4A
		public LiveExternalFCodeException(int fCode, string msppErrorString, Exception innerException) : base(Strings.LiveExternalFCodeException(fCode, msppErrorString), innerException)
		{
			this.fCode = fCode;
			this.msppErrorString = msppErrorString;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A68 File Offset: 0x00000C68
		protected LiveExternalFCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fCode = (int)info.GetValue("fCode", typeof(int));
			this.msppErrorString = (string)info.GetValue("msppErrorString", typeof(string));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002ABD File Offset: 0x00000CBD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fCode", this.fCode);
			info.AddValue("msppErrorString", this.msppErrorString);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002AE9 File Offset: 0x00000CE9
		public int FCode
		{
			get
			{
				return this.fCode;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002AF1 File Offset: 0x00000CF1
		public string MsppErrorString
		{
			get
			{
				return this.msppErrorString;
			}
		}

		// Token: 0x0400002A RID: 42
		private readonly int fCode;

		// Token: 0x0400002B RID: 43
		private readonly string msppErrorString;
	}
}

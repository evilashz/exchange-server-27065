using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200000B RID: 11
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LiveTransientFCodeException : LiveTransientException
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002961 File Offset: 0x00000B61
		public LiveTransientFCodeException(int fCode, string msppErrorString) : base(Strings.LiveTransientFCodeException(fCode, msppErrorString))
		{
			this.fCode = fCode;
			this.msppErrorString = msppErrorString;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000297E File Offset: 0x00000B7E
		public LiveTransientFCodeException(int fCode, string msppErrorString, Exception innerException) : base(Strings.LiveTransientFCodeException(fCode, msppErrorString), innerException)
		{
			this.fCode = fCode;
			this.msppErrorString = msppErrorString;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000299C File Offset: 0x00000B9C
		protected LiveTransientFCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fCode = (int)info.GetValue("fCode", typeof(int));
			this.msppErrorString = (string)info.GetValue("msppErrorString", typeof(string));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000029F1 File Offset: 0x00000BF1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fCode", this.fCode);
			info.AddValue("msppErrorString", this.msppErrorString);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002A1D File Offset: 0x00000C1D
		public int FCode
		{
			get
			{
				return this.fCode;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A25 File Offset: 0x00000C25
		public string MsppErrorString
		{
			get
			{
				return this.msppErrorString;
			}
		}

		// Token: 0x04000028 RID: 40
		private readonly int fCode;

		// Token: 0x04000029 RID: 41
		private readonly string msppErrorString;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200000A RID: 10
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LiveConfigurationFCodeException : LiveConfigurationException
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002895 File Offset: 0x00000A95
		public LiveConfigurationFCodeException(int fCode, string msppErrorString) : base(Strings.LiveConfigurationFCodeException(fCode, msppErrorString))
		{
			this.fCode = fCode;
			this.msppErrorString = msppErrorString;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028B2 File Offset: 0x00000AB2
		public LiveConfigurationFCodeException(int fCode, string msppErrorString, Exception innerException) : base(Strings.LiveConfigurationFCodeException(fCode, msppErrorString), innerException)
		{
			this.fCode = fCode;
			this.msppErrorString = msppErrorString;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028D0 File Offset: 0x00000AD0
		protected LiveConfigurationFCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fCode = (int)info.GetValue("fCode", typeof(int));
			this.msppErrorString = (string)info.GetValue("msppErrorString", typeof(string));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002925 File Offset: 0x00000B25
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fCode", this.fCode);
			info.AddValue("msppErrorString", this.msppErrorString);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002951 File Offset: 0x00000B51
		public int FCode
		{
			get
			{
				return this.fCode;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002959 File Offset: 0x00000B59
		public string MsppErrorString
		{
			get
			{
				return this.msppErrorString;
			}
		}

		// Token: 0x04000026 RID: 38
		private readonly int fCode;

		// Token: 0x04000027 RID: 39
		private readonly string msppErrorString;
	}
}

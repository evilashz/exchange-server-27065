using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	internal sealed class MsiException : Exception
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002CDE File Offset: 0x00000EDE
		public MsiException()
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002CE6 File Offset: 0x00000EE6
		public MsiException(string message) : base(message)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002CEF File Offset: 0x00000EEF
		public MsiException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002CF9 File Offset: 0x00000EF9
		public MsiException(uint errorCode)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002D08 File Offset: 0x00000F08
		public MsiException(uint errorCode, string message) : base(message)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002D18 File Offset: 0x00000F18
		private MsiException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info != null)
			{
				this.ErrorCode = info.GetUInt32("ErrorCode");
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002D36 File Offset: 0x00000F36
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info != null)
			{
				info.AddValue("ErrorCode", this.ErrorCode);
			}
			base.GetObjectData(info, context);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002D54 File Offset: 0x00000F54
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002D5C File Offset: 0x00000F5C
		public uint ErrorCode { get; set; }
	}
}

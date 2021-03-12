using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x0200010A RID: 266
	[Serializable]
	internal class RpcErrorExceptionInfo
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x00028C18 File Offset: 0x00028018
		public RpcErrorExceptionInfo(RpcErrorExceptionInfo other)
		{
			this.m_errorCode = other.m_errorCode;
			this.m_errorMessage = null;
			this.m_reconstitutedException = null;
			if (!string.IsNullOrEmpty(other.m_errorMessage))
			{
				string errorMessage = other.m_errorMessage;
				this.m_errorMessage = string.Copy(errorMessage);
			}
			byte[] serializedException = other.m_serializedException;
			if (serializedException != null && serializedException.Length > 0)
			{
				byte[] array = new byte[serializedException.Length];
				this.m_serializedException = array;
				serializedException.CopyTo(array, 0);
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00028BFC File Offset: 0x00027FFC
		public RpcErrorExceptionInfo()
		{
			this.m_errorCode = 0;
			base..ctor();
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00028C8C File Offset: 0x0002808C
		public override string ToString()
		{
			string arg;
			if (this.m_errorMessage == null)
			{
				arg = "<null>";
			}
			else
			{
				arg = this.m_errorMessage;
			}
			int errorCode = this.m_errorCode;
			return string.Format(RpcErrorExceptionInfo.ToStringFormat, errorCode.ToString(), arg);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00028CD0 File Offset: 0x000280D0
		[return: MarshalAs(UnmanagedType.U1)]
		public bool IsFailed()
		{
			byte[] serializedException = this.m_serializedException;
			return (serializedException != null && serializedException.Length > 0) || this.m_reconstitutedException != null || this.m_errorMessage != null || this.m_errorCode != 0;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00001A6C File Offset: 0x00000E6C
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x00001A80 File Offset: 0x00000E80
		public int ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
			set
			{
				this.m_errorCode = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00001A94 File Offset: 0x00000E94
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x00001AA8 File Offset: 0x00000EA8
		public string ErrorMessage
		{
			get
			{
				return this.m_errorMessage;
			}
			set
			{
				this.m_errorMessage = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00001ABC File Offset: 0x00000EBC
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x00001AD0 File Offset: 0x00000ED0
		public byte[] SerializedException
		{
			get
			{
				return this.m_serializedException;
			}
			set
			{
				this.m_serializedException = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00001AE4 File Offset: 0x00000EE4
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x00001AF8 File Offset: 0x00000EF8
		public Exception ReconstitutedException
		{
			get
			{
				return this.m_reconstitutedException;
			}
			set
			{
				this.m_reconstitutedException = value;
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00001B0C File Offset: 0x00000F0C
		// Note: this type is marked as 'beforefieldinit'.
		static RpcErrorExceptionInfo()
		{
			RpcErrorExceptionInfo.ToStringFormat = "RpcErrorExceptionInfo: [ErrorCode='{0}', ErrorMessage='{1}']";
		}

		// Token: 0x0400093A RID: 2362
		private int m_errorCode;

		// Token: 0x0400093B RID: 2363
		private string m_errorMessage;

		// Token: 0x0400093C RID: 2364
		private byte[] m_serializedException;

		// Token: 0x0400093D RID: 2365
		private Exception m_reconstitutedException;

		// Token: 0x0400093E RID: 2366
		private static string ToStringFormat;

		// Token: 0x0400093F RID: 2367
		public static int EcSuccess = 0;
	}
}

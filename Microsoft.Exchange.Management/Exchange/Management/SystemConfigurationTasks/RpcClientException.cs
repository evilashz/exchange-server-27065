using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A70 RID: 2672
	[Serializable]
	public class RpcClientException : Exception
	{
		// Token: 0x06005F4E RID: 24398 RVA: 0x0018F87F File Offset: 0x0018DA7F
		private RpcClientException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x0018F889 File Offset: 0x0018DA89
		protected RpcClientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06005F50 RID: 24400 RVA: 0x0018F894 File Offset: 0x0018DA94
		internal static Exception TranslateRpcException(Exception exception)
		{
			RpcException ex = exception as RpcException;
			if (ex == null)
			{
				return exception;
			}
			int errorCode = ex.ErrorCode;
			if (errorCode == 1753)
			{
				return new RpcClientException(Strings.ServerNotFound((ex != null) ? ex.Source : string.Empty), ex);
			}
			return new Win32Exception(ex.ErrorCode);
		}
	}
}

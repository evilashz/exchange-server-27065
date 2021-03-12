using System;
using System.Net;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics
{
	// Token: 0x02000019 RID: 25
	internal static class ExceptionGroupHandlers
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00004E60 File Offset: 0x00003060
		public static ExceptionAction Rpc(Exception ex)
		{
			RpcException ex2 = ex as RpcException;
			if (ex2 != null)
			{
				int[] array = new int[]
				{
					ex2.ErrorCode,
					ex2.HResult
				};
				int i = 0;
				while (i < array.Length)
				{
					int num = array[i];
					int num2 = num;
					switch (num2)
					{
					case 1721:
					case 1722:
					case 1723:
						break;
					default:
						if (num2 != 1762)
						{
							i++;
							continue;
						}
						break;
					}
					return ExceptionAction.RetryWait;
				}
			}
			return ExceptionAction.Fail;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004ED8 File Offset: 0x000030D8
		public static ExceptionAction Web(Exception ex)
		{
			if (ex is DataSourceOperationException)
			{
				return ExceptionGroupHandlers.Web(ex.InnerException);
			}
			if (ex is SoapException)
			{
				SoapException ex2 = (SoapException)ex;
				ServiceError serviceError;
				if (ex2.Code != null && ex2.Code.Name != null && Enum.TryParse<ServiceError>(ex2.Code.Name, out serviceError))
				{
					ex = new ServiceResponseException(new ServiceResponse(serviceError, string.Empty));
				}
			}
			if (ex is WebException)
			{
				WebException ex3 = (WebException)ex;
				WebExceptionStatus status = ex3.Status;
				switch (status)
				{
				case WebExceptionStatus.NameResolutionFailure:
				case WebExceptionStatus.ConnectFailure:
				case WebExceptionStatus.PipelineFailure:
				case WebExceptionStatus.ProtocolError:
				case WebExceptionStatus.ConnectionClosed:
					break;
				case WebExceptionStatus.ReceiveFailure:
				case WebExceptionStatus.SendFailure:
				case WebExceptionStatus.RequestCanceled:
					goto IL_BE;
				default:
					switch (status)
					{
					case WebExceptionStatus.KeepAliveFailure:
					case WebExceptionStatus.Timeout:
						break;
					case WebExceptionStatus.Pending:
						goto IL_BE;
					default:
						goto IL_BE;
					}
					break;
				}
				return ExceptionAction.RetryWait;
			}
			IL_BE:
			if (ex is ServiceResponseException)
			{
				ServiceResponseException ex4 = (ServiceResponseException)ex;
				ServiceError errorCode = ex4.ErrorCode;
				if (errorCode <= 101)
				{
					switch (errorCode)
					{
					case 6:
					case 8:
						break;
					case 7:
						goto IL_163;
					default:
						if (errorCode != 75 && errorCode != 101)
						{
							goto IL_163;
						}
						break;
					}
				}
				else if (errorCode <= 263)
				{
					switch (errorCode)
					{
					case 126:
					case 128:
						break;
					case 127:
						goto IL_163;
					default:
						switch (errorCode)
						{
						case 259:
						case 262:
						case 263:
							break;
						case 260:
						case 261:
							goto IL_163;
						default:
							goto IL_163;
						}
						break;
					}
				}
				else
				{
					switch (errorCode)
					{
					case 333:
					case 334:
						break;
					default:
						if (errorCode != 363)
						{
							goto IL_163;
						}
						break;
					}
				}
				return ExceptionAction.RetryWait;
				IL_163:
				if (ex4.ErrorCode.ToString().IndexOf("Transient", StringComparison.InvariantCultureIgnoreCase) != -1)
				{
					return ExceptionAction.RetryWait;
				}
			}
			return ExceptionAction.Fail;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005069 File Offset: 0x00003269
		public static ExceptionAction Unhandled(Exception ex)
		{
			return ExceptionAction.Fail;
		}
	}
}

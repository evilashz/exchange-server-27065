using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Web.Services.Protocols;
using Microsoft.Exchange.EDiscovery.Export;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200008D RID: 141
	internal abstract class ElcBaseServiceClient<ServiceBindingType, FunctionalInterfaceType> : IServiceClient<FunctionalInterfaceType>, IDisposable where ServiceBindingType : HttpWebClientProtocol, IServiceBinding, new()
	{
		// Token: 0x06000550 RID: 1360 RVA: 0x00028C78 File Offset: 0x00026E78
		protected ElcBaseServiceClient(Uri serviceEndpoint, IServiceCallingContext<ServiceBindingType> serviceCallingContext, CancellationToken abortTokenForTasks)
		{
			this.ServiceEndpoint = serviceEndpoint;
			this.ServiceCallingContext = serviceCallingContext;
			this.AbortTokenForTasks = abortTokenForTasks;
			this.TotalRetryTimeWindow = ConstantProvider.TotalRetryTimeWindow;
			this.RetrySchedule = ConstantProvider.RetrySchedule;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00028CAB File Offset: 0x00026EAB
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x00028CB3 File Offset: 0x00026EB3
		public Uri ServiceEndpoint { get; private set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000553 RID: 1363
		public abstract FunctionalInterfaceType FunctionalInterface { get; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00028CBC File Offset: 0x00026EBC
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00028CC4 File Offset: 0x00026EC4
		private protected ServiceBindingType ServiceBinding { protected get; private set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00028CCD File Offset: 0x00026ECD
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x00028CD5 File Offset: 0x00026ED5
		private protected CancellationToken AbortTokenForTasks { protected get; private set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00028CDE File Offset: 0x00026EDE
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00028CE6 File Offset: 0x00026EE6
		private protected IServiceCallingContext<ServiceBindingType> ServiceCallingContext { protected get; private set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00028CEF File Offset: 0x00026EEF
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00028CF7 File Offset: 0x00026EF7
		protected TimeSpan TotalRetryTimeWindow { get; set; }

		// Token: 0x0600055C RID: 1372 RVA: 0x00028D00 File Offset: 0x00026F00
		public static bool IsTransientError(string code)
		{
			ResponseCodeType code2;
			return Enum.TryParse<ResponseCodeType>(code, out code2) && ElcBaseServiceClient<ServiceBindingType, FunctionalInterfaceType>.IsTransientError(code2);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00028D20 File Offset: 0x00026F20
		public static bool IsTransientError(ResponseCodeType code)
		{
			if (code <= ResponseCodeType.ErrorInternalServerTransientError)
			{
				if (code <= ResponseCodeType.ErrorDeleteItemsFailed)
				{
					switch (code)
					{
					case ResponseCodeType.ErrorADOperation:
					case ResponseCodeType.ErrorADUnavailable:
						break;
					case ResponseCodeType.ErrorADSessionFilter:
						return false;
					default:
						if (code != ResponseCodeType.ErrorBatchProcessingStopped && code != ResponseCodeType.ErrorDeleteItemsFailed)
						{
							return false;
						}
						break;
					}
				}
				else if (code != ResponseCodeType.ErrorExceededConnectionCount && code != ResponseCodeType.ErrorFolderSavePropertyError)
				{
					switch (code)
					{
					case ResponseCodeType.ErrorInsufficientResources:
					case ResponseCodeType.ErrorInternalServerTransientError:
						break;
					case ResponseCodeType.ErrorInternalServerError:
						return false;
					default:
						return false;
					}
				}
			}
			else if (code <= ResponseCodeType.ErrorNotEnoughMemory)
			{
				if (code != ResponseCodeType.ErrorItemSavePropertyError)
				{
					switch (code)
					{
					case ResponseCodeType.ErrorMailboxMoveInProgress:
					case ResponseCodeType.ErrorMailboxStoreUnavailable:
						break;
					default:
						if (code != ResponseCodeType.ErrorNotEnoughMemory)
						{
							return false;
						}
						break;
					}
				}
			}
			else if (code <= ResponseCodeType.ErrorTimeoutExpired)
			{
				switch (code)
				{
				case ResponseCodeType.ErrorServerBusy:
				case ResponseCodeType.ErrorStaleObject:
					break;
				case ResponseCodeType.ErrorServiceDiscoveryFailed:
					return false;
				default:
					if (code != ResponseCodeType.ErrorTimeoutExpired)
					{
						return false;
					}
					break;
				}
			}
			else if (code != ResponseCodeType.ErrorTooManyObjectsOpened && code != ResponseCodeType.ErrorMessageTrackingTransientError)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00028DF1 File Offset: 0x00026FF1
		public virtual bool Connect()
		{
			if (this.ServiceBinding == null)
			{
				this.ServiceBinding = this.ServiceCallingContext.CreateServiceBinding(this.ServiceEndpoint);
			}
			return this.ServiceBinding != null;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00028E28 File Offset: 0x00027028
		public void Dispose()
		{
			if (this.ServiceBinding != null)
			{
				ServiceBindingType serviceBinding = this.ServiceBinding;
				serviceBinding.Dispose();
				this.ServiceBinding = default(ServiceBindingType);
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00028EBC File Offset: 0x000270BC
		protected void InternalCallService<BaseResponseMessageType>(Func<BaseResponseMessageType> delegateServiceCall, Action<BaseResponseMessageType> responseProcessor, Func<Exception, Exception> exceptionHandler, Func<bool> authorizationHandler, Action<Uri> urlRedirectionHandler)
		{
			int num = 0;
			DateTime t = DateTime.UtcNow.Add(this.TotalRetryTimeWindow);
			Exception threadException;
			Exception ex;
			Exception ex2;
			for (;;)
			{
				ex = null;
				threadException = null;
				ex2 = null;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				if (this.ServiceBinding != null)
				{
					ServiceBindingType serviceBinding = this.ServiceBinding;
					serviceBinding.UserAgent = string.Format("{0}{1}=MRM&{2}={3}&{4}={5}&{6}={7}", new object[]
					{
						ElcEwsClientHelper.GetOAuthUserAgent("ElcClient"),
						"S",
						"BI",
						DateTime.UtcNow.Ticks.ToString(),
						"R",
						num.ToString(),
						"RT",
						DateTime.UtcNow.Ticks.ToString()
					});
				}
				try
				{
					BaseResponseMessageType response = default(BaseResponseMessageType);
					bool flag4 = false;
					Thread thread = new Thread(delegate()
					{
						try
						{
							response = delegateServiceCall();
						}
						catch (Exception threadException)
						{
							threadException = threadException;
						}
					});
					thread.Start();
					while (!flag4)
					{
						if (this.AbortTokenForTasks.IsCancellationRequested)
						{
							throw new ExportException(ExportErrorType.StopRequested);
						}
						flag4 = thread.Join(5000);
					}
					if (!flag4)
					{
						thread.Abort();
					}
					if (threadException != null)
					{
						throw threadException;
					}
					if (responseProcessor != null)
					{
						responseProcessor(response);
					}
				}
				catch (RetryException ex3)
				{
					ex = ex3.InnerException;
					flag = true;
					if (ex3.ResetRetryCounter)
					{
						Tracer.TraceError("ElcBaseServiceClient.InternalCallService: Resetting retry in RetryException.", new object[0]);
						flag2 = true;
					}
				}
				catch (ExportException ex4)
				{
					ex = ex4;
					if (ex4.ErrorType == ExportErrorType.Unauthorized)
					{
						flag3 = true;
					}
				}
				catch (WebException ex5)
				{
					ex = ex5;
					if (ex5.Status == WebExceptionStatus.TrustFailure)
					{
						Tracer.TraceError("ElcBaseServiceClient.InternalCallService: Unable to establish trust on exception. No retry.", new object[0]);
						flag = false;
					}
					else if (ex5.Status == WebExceptionStatus.ConnectFailure)
					{
						SocketException ex6 = ex5.InnerException as SocketException;
						flag = (ex6 == null || ex6.SocketErrorCode != SocketError.ConnectionRefused);
						Tracer.TraceError("ElcBaseServiceClient.InternalCallService: Connect failure. Retry: {0}.", new object[]
						{
							flag
						});
					}
					else if (ex5.Status == WebExceptionStatus.NameResolutionFailure)
					{
						Tracer.TraceError("ElcBaseServiceClient.InternalCallService: DNS look up failure. No retry.", new object[0]);
						flag = false;
					}
					else
					{
						HttpWebResponse httpWebResponse = ex5.Response as HttpWebResponse;
						flag = true;
						if (httpWebResponse != null)
						{
							HttpStatusCode statusCode = httpWebResponse.StatusCode;
							switch (statusCode)
							{
							case HttpStatusCode.MovedPermanently:
							case HttpStatusCode.Found:
								break;
							default:
								if (statusCode != HttpStatusCode.TemporaryRedirect)
								{
									if (statusCode == HttpStatusCode.Unauthorized)
									{
										Tracer.TraceError("ElcBaseServiceClient.InternalCallService: HTTP 401 unauthorized", new object[0]);
										flag = false;
										flag3 = true;
										ex = new ExportException(ExportErrorType.Unauthorized, ex5);
										goto IL_352;
									}
									goto IL_352;
								}
								break;
							}
							string text = httpWebResponse.Headers[HttpResponseHeader.Location];
							Tracer.TraceInformation("ElcBaseServiceClient.InternalCallService: HTTP redirection to {0}", new object[]
							{
								text
							});
							ex = new ExportException(ExportErrorType.UnexpectedWebServiceUrlRedirection, ex5);
							flag = false;
							if (Uri.IsWellFormedUriString(text, UriKind.Absolute))
							{
								Uri uri = new Uri(text);
								if (uri.Scheme == Uri.UriSchemeHttps && urlRedirectionHandler != null)
								{
									this.ServiceEndpoint = uri;
									urlRedirectionHandler(uri);
									ex = null;
									flag = true;
									flag2 = true;
									Tracer.TraceError("ElcBaseServiceClient.InternalCallService: Resetting retry during HTTP redirection.", new object[0]);
								}
							}
						}
					}
					IL_352:;
				}
				catch (SoapException ex7)
				{
					if (ex7.Code != null && ex7.Code.Name == "ErrorAccessDenied")
					{
						flag = false;
						flag3 = true;
						ex = new ExportException(ExportErrorType.Unauthorized, ex7);
					}
					else
					{
						ex = ex7;
						flag = false;
						if (ex7.Code != null && ElcBaseServiceClient<ServiceBindingType, FunctionalInterfaceType>.IsTransientError(ex7.Code.Name))
						{
							flag = true;
						}
					}
				}
				catch (TimeoutException ex8)
				{
					ex = ex8;
					flag = true;
				}
				catch (InvalidOperationException ex9)
				{
					ex = ex9;
					flag = true;
				}
				if (ex != null)
				{
					if (exceptionHandler != null)
					{
						Tracer.TraceError("ElcBaseServiceClient.InternalCallService: Exception handler is handling exception : {0}", new object[]
						{
							ex
						});
						ex2 = exceptionHandler(ex);
						Tracer.TraceError("ElcBaseServiceClient.InternalCallService: Exception after being handled : {0}", new object[]
						{
							ex2
						});
					}
					else
					{
						ex2 = new ExportException(ExportErrorType.ExchangeWebServiceCallFailed, ex);
					}
				}
				if (flag && !flag2)
				{
					int num2 = this.GetRetryWaitTime(num);
					Tracer.TraceError("ElcBaseServiceClient.InternalCallService: Retry after {0} milliseconds on exception : {1}", new object[]
					{
						num2,
						ex
					});
					while (num2 > 0 && t > DateTime.UtcNow)
					{
						Thread.Sleep((num2 > 5000) ? 5000 : num2);
						num2 -= 5000;
						if (this.AbortTokenForTasks.IsCancellationRequested)
						{
							goto Block_8;
						}
					}
				}
				if (flag3)
				{
					Tracer.TraceInformation("ElcBaseServiceClient.InternalCallService: Unauthorized", new object[0]);
					if (authorizationHandler == null || !authorizationHandler())
					{
						goto IL_4BE;
					}
					flag = true;
				}
				if (flag2)
				{
					num = 0;
				}
				else
				{
					num++;
				}
				if (!flag || !(t > DateTime.UtcNow))
				{
					goto IL_4DF;
				}
			}
			Block_8:
			throw new ExportException(ExportErrorType.StopRequested);
			IL_4BE:
			throw ex;
			IL_4DF:
			if (ex2 != null)
			{
				Tracer.TraceError("ElcBaseServiceClient.InternalCallService: Exception thrown after all possible actions: {0}", new object[]
				{
					ex2
				});
				throw ex2;
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00029460 File Offset: 0x00027660
		private int GetRetryWaitTime(int retryAttempt)
		{
			int num = (this.RetrySchedule == null) ? 0 : this.RetrySchedule.Length;
			TimeSpan timeSpan;
			if (this.RetrySchedule == null)
			{
				timeSpan = ElcBaseServiceClient<ServiceBindingType, FunctionalInterfaceType>.DefaultRetryWaitTime;
			}
			else if (retryAttempt < 0 || retryAttempt >= num)
			{
				timeSpan = this.RetrySchedule[num - 1];
			}
			else
			{
				timeSpan = this.RetrySchedule[retryAttempt];
			}
			return (int)timeSpan.TotalMilliseconds;
		}

		// Token: 0x040003FF RID: 1023
		private static readonly TimeSpan DefaultRetryWaitTime = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000400 RID: 1024
		protected TimeSpan[] RetrySchedule;
	}
}

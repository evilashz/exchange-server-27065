using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Web.Services.Protocols;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000037 RID: 55
	internal abstract class BaseServiceClient<ServiceBindingType, FunctionalInterfaceType> : IServiceClient<FunctionalInterfaceType>, IDisposable where ServiceBindingType : HttpWebClientProtocol, IServiceBinding, new()
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00006743 File Offset: 0x00004943
		protected BaseServiceClient(Uri serviceEndpoint, IServiceCallingContext<ServiceBindingType> serviceCallingContext, CancellationToken abortTokenForTasks)
		{
			this.ServiceEndpoint = serviceEndpoint;
			this.ServiceCallingContext = serviceCallingContext;
			this.AbortTokenForTasks = abortTokenForTasks;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000676B File Offset: 0x0000496B
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00006773 File Offset: 0x00004973
		public Uri ServiceEndpoint { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001BA RID: 442
		public abstract FunctionalInterfaceType FunctionalInterface { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000677C File Offset: 0x0000497C
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00006784 File Offset: 0x00004984
		public string CurrentMailbox { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000678D File Offset: 0x0000498D
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00006795 File Offset: 0x00004995
		public IAutoDiscoverClient AutoDiscoverInterface { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000679E File Offset: 0x0000499E
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x000067A6 File Offset: 0x000049A6
		private protected ServiceBindingType ServiceBinding { protected get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000067AF File Offset: 0x000049AF
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x000067B7 File Offset: 0x000049B7
		private protected CancellationToken AbortTokenForTasks { protected get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000067C0 File Offset: 0x000049C0
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x000067C8 File Offset: 0x000049C8
		private protected IServiceCallingContext<ServiceBindingType> ServiceCallingContext { protected get; private set; }

		// Token: 0x060001C5 RID: 453 RVA: 0x000067D4 File Offset: 0x000049D4
		public static bool IsTransientError(string code)
		{
			ResponseCodeType code2;
			return Enum.TryParse<ResponseCodeType>(code, out code2) && BaseServiceClient<ServiceBindingType, FunctionalInterfaceType>.IsTransientError(code2);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000067F4 File Offset: 0x000049F4
		public static bool IsRebindableError(string code)
		{
			ResponseCodeType code2;
			return Enum.TryParse<ResponseCodeType>(code, out code2) && BaseServiceClient<ServiceBindingType, FunctionalInterfaceType>.IsRebindableError(code2);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006814 File Offset: 0x00004A14
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

		// Token: 0x060001C8 RID: 456 RVA: 0x000068E8 File Offset: 0x00004AE8
		public static bool IsRebindableError(ResponseCodeType code)
		{
			switch (code)
			{
			case ResponseCodeType.ErrorMailboxMoveInProgress:
			case ResponseCodeType.ErrorMailboxStoreUnavailable:
				break;
			default:
				switch (code)
				{
				case ResponseCodeType.ErrorProxyServiceDiscoveryFailed:
				case ResponseCodeType.ErrorProxyTokenExpired:
					break;
				default:
					if (code != ResponseCodeType.ErrorMailboxFailover)
					{
						return false;
					}
					break;
				}
				break;
			}
			return true;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000692A File Offset: 0x00004B2A
		public virtual bool Connect()
		{
			if (this.ServiceBinding == null)
			{
				this.ServiceBinding = this.ServiceCallingContext.CreateServiceBinding(this.ServiceEndpoint);
			}
			return this.ServiceBinding != null;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006964 File Offset: 0x00004B64
		public void Dispose()
		{
			if (this.ServiceBinding != null)
			{
				ServiceBindingType serviceBinding = this.ServiceBinding;
				serviceBinding.Dispose();
				this.ServiceBinding = default(ServiceBindingType);
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000069F8 File Offset: 0x00004BF8
		protected void InternalCallService<BaseResponseMessageType>(Func<BaseResponseMessageType> delegateServiceCall, Action<BaseResponseMessageType> responseProcessor, Func<Exception, Exception> exceptionHandler, Func<bool> authorizationHandler, Action<Uri> urlRedirectionHandler)
		{
			int num = 0;
			DateTime t = DateTime.UtcNow.Add(ConstantProvider.TotalRetryTimeWindow);
			ScenarioData.Current["BI"] = DateTime.UtcNow.Ticks.ToString();
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
				ScenarioData.Current["R"] = num.ToString();
				ScenarioData.Current["RT"] = DateTime.UtcNow.Ticks.ToString();
				if (this.ServiceBinding != null)
				{
					ServiceBindingType serviceBinding = this.ServiceBinding;
					serviceBinding.UserAgent = ScenarioData.Current.ToString();
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
						Tracer.TraceError("BaseServiceClient.InternalCallService: Resetting retry in RetryException.", new object[0]);
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
						Tracer.TraceError("BaseServiceClient.InternalCallService: Unable to establish trust on exception. No retry.", new object[0]);
						flag = false;
					}
					else if (ex5.Status == WebExceptionStatus.ConnectFailure)
					{
						SocketException ex6 = ex5.InnerException as SocketException;
						flag = (ex6 == null || ex6.SocketErrorCode != SocketError.ConnectionRefused);
						if (!flag)
						{
							flag = this.Rebind();
						}
						Tracer.TraceError("BaseServiceClient.InternalCallService: Connect failure. Retry: {0}.", new object[]
						{
							flag
						});
					}
					else if (ex5.Status == WebExceptionStatus.NameResolutionFailure)
					{
						Tracer.TraceError("BaseServiceClient.InternalCallService: DNS look up failure. No retry.", new object[0]);
						flag = this.Rebind();
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
										Tracer.TraceError("BaseServiceClient.InternalCallService: HTTP 401 unauthorized", new object[0]);
										flag = false;
										flag3 = true;
										ex = new ExportException(ExportErrorType.Unauthorized, ex5);
										goto IL_342;
									}
									goto IL_342;
								}
								break;
							}
							string text = httpWebResponse.Headers[HttpResponseHeader.Location];
							Tracer.TraceInformation("BaseServiceClient.InternalCallService: HTTP redirection to {0}", new object[]
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
									Tracer.TraceError("BaseServiceClient.InternalCallService: Resetting retry during HTTP redirection.", new object[0]);
								}
							}
						}
					}
					IL_342:;
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
						if (ex7.Code != null)
						{
							if (BaseServiceClient<ServiceBindingType, FunctionalInterfaceType>.IsRebindableError(ex7.Code.Name))
							{
								flag = this.Rebind();
							}
							if (BaseServiceClient<ServiceBindingType, FunctionalInterfaceType>.IsTransientError(ex7.Code.Name))
							{
								flag = true;
							}
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
						Tracer.TraceError("BaseServiceClient.InternalCallService: Exception handler is handling exception : {0}", new object[]
						{
							ex
						});
						ex2 = exceptionHandler(ex);
						Tracer.TraceError("BaseServiceClient.InternalCallService: Exception after being handled : {0}", new object[]
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
					Tracer.TraceError("BaseServiceClient.InternalCallService: Retry after {0} milliseconds on exception : {1}", new object[]
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
					Tracer.TraceInformation("BaseServiceClient.InternalCallService: Unauthorized", new object[0]);
					if (authorizationHandler == null || !authorizationHandler())
					{
						goto IL_4CB;
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
					goto IL_4EC;
				}
			}
			Block_8:
			throw new ExportException(ExportErrorType.StopRequested);
			IL_4CB:
			throw ex;
			IL_4EC:
			if (ScenarioData.Current.ContainsKey("BI"))
			{
				ScenarioData.Current.Remove("BI");
			}
			if (ex2 != null)
			{
				Tracer.TraceError("BaseServiceClient.InternalCallService: Exception thrown after all possible actions: {0}", new object[]
				{
					ex2
				});
				throw ex2;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006FC8 File Offset: 0x000051C8
		protected bool Rebind()
		{
			TimeSpan t = new TimeSpan(0, 1, 0);
			Tracer.TraceInformation("BaseServiceClient.Rebind: Request to rebind Mailbox: {0} current Url: {1}", new object[]
			{
				this.CurrentMailbox,
				this.ServiceEndpoint
			});
			try
			{
				if (ConstantProvider.RebindWithAutoDiscoveryEnabled && ConstantProvider.RebindAutoDiscoveryUrl != null && !string.IsNullOrEmpty(this.CurrentMailbox) && this.AutoDiscoverInterface != null)
				{
					if (!this.ServiceEndpoint.Host.Equals(ConstantProvider.RebindAutoDiscoveryUrl.Host, StringComparison.InvariantCultureIgnoreCase))
					{
						if (!this.CurrentMailbox.Equals(this.lastRebindMailbox) || DateTime.UtcNow - this.lastRebindTime > t)
						{
							Tracer.TraceInformation("BaseServiceClient.Rebind: AutoDiscovery to rebind Mailbox: {0} current Url: {1} with AutoDiscovery: {2}", new object[]
							{
								this.CurrentMailbox,
								this.ServiceEndpoint,
								ConstantProvider.RebindAutoDiscoveryUrl
							});
							List<AutoDiscoverResult> userEwsEndpoints = this.AutoDiscoverInterface.GetUserEwsEndpoints(new string[]
							{
								this.CurrentMailbox
							});
							if (userEwsEndpoints != null && userEwsEndpoints.Count > 0 && userEwsEndpoints[0].ResultCode == AutoDiscoverResultCode.Success)
							{
								Uri uri = new Uri(userEwsEndpoints[0].ResultValue);
								if (!uri.Host.Equals(this.ServiceEndpoint.Host, StringComparison.InvariantCultureIgnoreCase))
								{
									Tracer.TraceInformation("BaseServiceClient.Rebind: Rebind Mailbox: {0} current Url: {1} with NewURL: {2}", new object[]
									{
										this.CurrentMailbox,
										this.ServiceEndpoint,
										uri
									});
									if (this.ServiceBinding != null)
									{
										ServiceBindingType serviceBinding = this.ServiceBinding;
										serviceBinding.Url = uri.ToString();
									}
									this.ServiceEndpoint = uri;
									this.lastRebindMailbox = this.CurrentMailbox;
									this.lastRebindTime = DateTime.UtcNow;
									return this.Connect();
								}
								Tracer.TraceError("BaseServiceClient.Rebind: Autodiscovered host is same as current Host: {0}", new object[]
								{
									this.ServiceEndpoint
								});
							}
							else
							{
								Tracer.TraceError("BaseServiceClient.Rebind: Autodiscovery failed with Error: {0}", new object[]
								{
									(userEwsEndpoints != null && userEwsEndpoints.Count > 0) ? userEwsEndpoints[0].ResultCode.ToString() : "Unknown"
								});
							}
						}
						else
						{
							Tracer.TraceError("BaseServiceClient.Rebind: AutoDiscovery attempted too frequently to rebind Mailbox: {0} current Url: {1} with AutoDiscovery: {2}", new object[]
							{
								this.CurrentMailbox,
								this.ServiceEndpoint,
								ConstantProvider.RebindAutoDiscoveryUrl
							});
						}
					}
					else
					{
						Tracer.TraceError("BaseServiceClient.Rebind: Autodiscovery host is same as failing Host: {0} ", new object[]
						{
							this.ServiceEndpoint
						});
					}
				}
				else
				{
					Tracer.TraceInformation("BaseServiceClient.Rebind: settings do not allow rebind", new object[0]);
				}
			}
			catch (Exception ex)
			{
				Tracer.TraceError("BaseServiceClient.Rebind: Exception thrown during action: {0}", new object[]
				{
					ex
				});
			}
			return false;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000072B4 File Offset: 0x000054B4
		private int GetRetryWaitTime(int retryAttempt)
		{
			int num = (ConstantProvider.RetrySchedule == null) ? 0 : ConstantProvider.RetrySchedule.Length;
			TimeSpan timeSpan;
			if (retryAttempt < 0 || retryAttempt >= num)
			{
				timeSpan = ConstantProvider.RetrySchedule[num - 1];
			}
			else
			{
				timeSpan = ConstantProvider.RetrySchedule[retryAttempt];
			}
			return (int)timeSpan.TotalMilliseconds;
		}

		// Token: 0x040000B4 RID: 180
		private string lastRebindMailbox;

		// Token: 0x040000B5 RID: 181
		private DateTime lastRebindTime = DateTime.UtcNow;
	}
}

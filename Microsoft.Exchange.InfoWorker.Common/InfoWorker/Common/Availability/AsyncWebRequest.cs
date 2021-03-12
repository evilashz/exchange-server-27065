using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200005A RID: 90
	internal abstract class AsyncWebRequest : AsyncRequest
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00009C46 File Offset: 0x00007E46
		public AsyncWebRequest(Application application, ClientContext clientContext, RequestLogger requestLogger, string traceLabel) : base(application, clientContext, requestLogger)
		{
			this.traceLabel = traceLabel;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009CD0 File Offset: 0x00007ED0
		public sealed override void BeginInvoke(TaskCompleteCallback callback)
		{
			if (this.ShouldCallBeginInvokeByNewThread)
			{
				ThreadPool.QueueUserWorkItem(delegate(object dummy)
				{
					ThreadContext.SetWithExceptionHandling(this.traceLabel + "ByNewThread", this.Application.Worker, this.ClientContext, this.RequestLogger, delegate
					{
						this.BeginInvokeInternal(callback);
					});
				});
				return;
			}
			this.BeginInvokeInternal(callback);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009DD4 File Offset: 0x00007FD4
		private void BeginInvokeInternal(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						this.asyncResult = this.BeginInvoke();
					}
					catch (SoapException exception)
					{
						this.HandleException(exception);
					}
					catch (UriFormatException exception2)
					{
						this.HandleException(exception2);
					}
					catch (WebException exception3)
					{
						this.HandleException(exception3);
					}
					catch (IOException ex2)
					{
						SocketException ex3 = ex2.InnerException as SocketException;
						if (ex3 == null)
						{
							ExWatson.SendReport(ex2, ReportOptions.None, null);
							this.HandleException(ex2);
						}
						else
						{
							this.HandleException(ex3);
						}
					}
					catch (LocalizedException exception4)
					{
						this.HandleException(exception4);
					}
				});
			}
			catch (GrayException ex)
			{
				this.HandleException(ex.InnerException);
			}
			if (this.asyncResult == null && this.CompleteAtomically())
			{
				this.Complete();
			}
		}

		// Token: 0x060001FA RID: 506
		protected abstract IAsyncResult BeginInvoke();

		// Token: 0x060001FB RID: 507
		protected abstract void EndInvoke(IAsyncResult asyncResult);

		// Token: 0x060001FC RID: 508
		protected abstract void HandleException(Exception exception);

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001FD RID: 509
		protected abstract bool IsImpersonating { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00009E38 File Offset: 0x00008038
		protected virtual bool ShouldCallBeginInvokeByNewThread
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009E3C File Offset: 0x0000803C
		protected void Complete(IAsyncResult asyncResult)
		{
			if (this.asyncResult == null)
			{
				this.asyncResult = asyncResult;
			}
			if (this.CompleteAtomically())
			{
				if (asyncResult.CompletedSynchronously)
				{
					this.CompleteInternal();
					return;
				}
				ThreadContext.SetWithExceptionHandling(this.traceLabel, base.Application.IOCompletion, base.ClientContext, base.RequestLogger, new ThreadContext.ExecuteDelegate(this.CompleteInternal));
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009E9D File Offset: 0x0000809D
		private new void Complete()
		{
			base.Complete();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00009EA8 File Offset: 0x000080A8
		private void CompleteInternal()
		{
			try
			{
				this.EndInvokeWithErrorHandling();
			}
			finally
			{
				if (this.asyncResult.CompletedSynchronously && this.IsImpersonating)
				{
					this.CompleteAsync();
				}
				else
				{
					this.Complete();
				}
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009EF4 File Offset: 0x000080F4
		private void CompleteAsync()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.CompleteAsync));
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009F08 File Offset: 0x00008108
		private void CompleteAsync(object notUsed)
		{
			ThreadContext.SetWithExceptionHandling(this.traceLabel, base.Application.Worker, base.ClientContext, base.RequestLogger, new ThreadContext.ExecuteDelegate(this.Complete));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009F38 File Offset: 0x00008138
		private bool CompleteAtomically()
		{
			return Interlocked.CompareExchange(ref this.completed, 1, 0) == 0;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009F4C File Offset: 0x0000814C
		private void EndInvokeWithErrorHandling()
		{
			try
			{
				this.EndInvoke(this.asyncResult);
			}
			catch (ArgumentException exception)
			{
				this.HandleException(exception);
			}
			catch (WebException exception2)
			{
				this.HandleException(exception2);
			}
			catch (SoapException exception3)
			{
				this.HandleException(exception3);
			}
			catch (InvalidOperationException exception4)
			{
				this.HandleException(exception4);
			}
			catch (IOException ex)
			{
				SocketException ex2 = ex.InnerException as SocketException;
				if (ex2 == null)
				{
					ExWatson.SendReport(ex, ReportOptions.None, null);
					this.HandleException(ex);
				}
				else
				{
					this.HandleException(ex2);
				}
			}
			catch (LocalizedException exception5)
			{
				this.HandleException(exception5);
			}
		}

		// Token: 0x04000146 RID: 326
		private int completed;

		// Token: 0x04000147 RID: 327
		private string traceLabel;

		// Token: 0x04000148 RID: 328
		private IAsyncResult asyncResult;

		// Token: 0x0200005B RID: 91
		// (Invoke) Token: 0x06000208 RID: 520
		public delegate void CompleteDelegate();
	}
}

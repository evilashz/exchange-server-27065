using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200004E RID: 78
	internal abstract class WebServiceCallState<TRequest, TResponse> : WebServiceCall where TRequest : class where TResponse : class
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0000F30F File Offset: 0x0000D50F
		internal WebServiceCallState(WebServiceUserInformation userInformation, IExchangeAsyncDispatch exchangeAsyncDispatch, AsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
		{
			this.userInformation = userInformation;
			this.exchangeAsyncDispatch = exchangeAsyncDispatch;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000F334 File Offset: 0x0000D534
		protected IExchangeAsyncDispatch ExchangeAsyncDispatch
		{
			get
			{
				return this.exchangeAsyncDispatch;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000F33C File Offset: 0x0000D53C
		protected WebServiceUserInformation UserInformation
		{
			get
			{
				return this.userInformation;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002CF RID: 719
		protected abstract string Name { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002D0 RID: 720
		protected abstract Trace Tracer { get; }

		// Token: 0x060002D1 RID: 721
		protected abstract void InternalBegin(TRequest request);

		// Token: 0x060002D2 RID: 722
		protected abstract void InternalBeginCleanup(bool isSuccessful);

		// Token: 0x060002D3 RID: 723
		protected abstract TResponse InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x060002D4 RID: 724
		protected abstract void InternalEndCleanup();

		// Token: 0x060002D5 RID: 725
		protected abstract TResponse InternalFailure(uint serviceCode);

		// Token: 0x060002D6 RID: 726 RVA: 0x0000F344 File Offset: 0x0000D544
		public IAsyncResult Begin(TRequest request)
		{
			try
			{
				Exception ex = null;
				uint num = 0U;
				bool isSuccessful = false;
				try
				{
					this.InternalBegin(request);
					isSuccessful = true;
				}
				catch (RpcException ex2)
				{
					ex = ex2;
					num = (uint)ex2.ErrorCode;
				}
				catch (ThreadAbortException ex3)
				{
					ex = ex3;
					num = 1726U;
				}
				catch (OutOfMemoryException ex4)
				{
					ex = ex4;
					num = 14U;
				}
				catch (Exception ex5)
				{
					ex = ex5;
					this.exception = ex5;
				}
				finally
				{
					this.InternalBeginCleanup(isSuccessful);
				}
				if (num != 0U || ex != null)
				{
					WebServiceEndPoint.LogFailure(string.Format("Begin{0}: Service failure: serviceCode={1}", this.Name, num), ex, this.userInformation.EmailAddress, this.userInformation.Domain, this.userInformation.Organization, this.Tracer);
					base.InvokeCallback();
				}
				if (num != 0U)
				{
					this.response = this.InternalFailure(num);
				}
			}
			catch (Exception ex6)
			{
				WebServiceEndPoint.LogFailure(string.Format("Begin{0}: Unhandled exception thrown.", this.Name), ex6, this.userInformation.EmailAddress, this.userInformation.Domain, this.userInformation.Organization, this.Tracer);
				throw;
			}
			return this;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000F494 File Offset: 0x0000D694
		public TResponse End()
		{
			TResponse result;
			try
			{
				base.WaitForCompletion();
				Exception ex = null;
				uint num = 0U;
				try
				{
					if (this.exception != null)
					{
						throw this.exception;
					}
					if (this.response != null)
					{
						return this.response;
					}
					try
					{
						this.response = this.InternalEnd(this.completeAsyncResult);
					}
					catch (RpcException ex2)
					{
						ex = ex2;
						num = (uint)ex2.ErrorCode;
					}
					catch (ThreadAbortException ex3)
					{
						ex = ex3;
						num = 1726U;
					}
					catch (OutOfMemoryException ex4)
					{
						ex = ex4;
						num = 14U;
					}
					catch (Exception ex5)
					{
						ex = ex5;
						this.exception = ex5;
					}
				}
				finally
				{
					this.InternalEndCleanup();
				}
				if (num != 0U || ex != null)
				{
					WebServiceEndPoint.LogFailure(string.Format("End{0}: Service failure: serviceCode={1}", this.Name, num), ex, this.userInformation.EmailAddress, this.userInformation.Domain, this.userInformation.Organization, this.Tracer);
				}
				if (num != 0U)
				{
					this.response = this.InternalFailure(num);
				}
				if (this.exception != null)
				{
					throw this.exception;
				}
				result = this.response;
			}
			catch (Exception ex6)
			{
				WebServiceEndPoint.LogFailure(string.Format("End{0}: Unhandled exception thrown.", this.Name), ex6, this.userInformation.EmailAddress, this.userInformation.Domain, this.userInformation.Organization, this.Tracer);
				throw;
			}
			return result;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000F624 File Offset: 0x0000D824
		public void Complete(ICancelableAsyncResult asyncResult)
		{
			this.completeAsyncResult = asyncResult;
			base.InvokeCallback();
		}

		// Token: 0x0400016D RID: 365
		private readonly WebServiceUserInformation userInformation;

		// Token: 0x0400016E RID: 366
		private readonly IExchangeAsyncDispatch exchangeAsyncDispatch;

		// Token: 0x0400016F RID: 367
		private ICancelableAsyncResult completeAsyncResult;

		// Token: 0x04000170 RID: 368
		private Exception exception;

		// Token: 0x04000171 RID: 369
		private TResponse response = default(TResponse);
	}
}

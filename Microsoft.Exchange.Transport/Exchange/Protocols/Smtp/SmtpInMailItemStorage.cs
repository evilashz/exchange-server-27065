using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004BE RID: 1214
	internal class SmtpInMailItemStorage : ISmtpInMailItemStorage
	{
		// Token: 0x060036B4 RID: 14004 RVA: 0x000E0AD4 File Offset: 0x000DECD4
		public IAsyncResult BeginCommitMailItem(TransportMailItem mailItem, AsyncCallback callback, object state)
		{
			IAsyncResult result;
			try
			{
				result = mailItem.BeginCommitForReceive(callback, state);
			}
			catch (ExchangeDataException exception)
			{
				result = this.HandleException(exception, callback, state);
			}
			catch (IOException exception2)
			{
				result = this.HandleException(exception2, callback, state);
			}
			return result;
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x000E0B24 File Offset: 0x000DED24
		public bool EndCommitMailItem(TransportMailItem mailItem, IAsyncResult asyncResult, out Exception exception)
		{
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult != null)
			{
				SmtpInMailItemStorage smtpInMailItemStorage = lazyAsyncResult.AsyncObject as SmtpInMailItemStorage;
				if (smtpInMailItemStorage != null)
				{
					exception = (lazyAsyncResult.Result as Exception);
					return false;
				}
			}
			return mailItem.EndCommitForReceive(asyncResult, out exception);
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x000E0BAC File Offset: 0x000DEDAC
		public Task CommitMailItemAsync(TransportMailItem mailItem)
		{
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
			this.BeginCommitMailItem(mailItem, delegate(IAsyncResult ar)
			{
				TaskCompletionSource<object> taskCompletionSource2 = (TaskCompletionSource<object>)ar.AsyncState;
				Exception exception;
				if (this.EndCommitMailItem(mailItem, ar, out exception))
				{
					taskCompletionSource2.SetResult(null);
					return;
				}
				taskCompletionSource2.SetException(exception);
			}, taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x000E0BF4 File Offset: 0x000DEDF4
		private IAsyncResult HandleException(Exception exception, AsyncCallback originalCallback, object originalState)
		{
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, originalState, originalCallback);
			lazyAsyncResult.InvokeCallback(exception);
			return lazyAsyncResult;
		}
	}
}

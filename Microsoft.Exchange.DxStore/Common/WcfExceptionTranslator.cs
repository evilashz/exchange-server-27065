using System;
using System.ServiceModel;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000080 RID: 128
	public abstract class WcfExceptionTranslator<TService>
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x00010E58 File Offset: 0x0000F058
		public static Exception TranslateException(Exception ex, Func<Exception, Exception> transient, Func<Exception, Exception> permanent)
		{
			FaultException<DxStoreServerFault> faultException = ex as FaultException<DxStoreServerFault>;
			if (faultException != null)
			{
				if (faultException.Detail.IsTransientError)
				{
					return transient(ex);
				}
				return permanent(ex);
			}
			else
			{
				if (ex is TimeoutException || ex is CommunicationException)
				{
					return transient(ex);
				}
				return null;
			}
		}

		// Token: 0x060004FC RID: 1276
		public abstract Exception GenerateTransientException(Exception exception);

		// Token: 0x060004FD RID: 1277
		public abstract Exception GeneratePermanentException(Exception exception);

		// Token: 0x060004FE RID: 1278 RVA: 0x00010EBC File Offset: 0x0000F0BC
		public void Execute(CachedChannelFactory<TService> factory, TimeSpan? timeout, Action<TService> action)
		{
			this.Execute<int>(factory, timeout, delegate(TService service)
			{
				action(service);
				return 0;
			});
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00010EEC File Offset: 0x0000F0EC
		public TReturnType Execute<TReturnType>(CachedChannelFactory<TService> factory, TimeSpan? timeout, Func<TService, TReturnType> action)
		{
			TReturnType result;
			try
			{
				result = WcfUtils.Run<TService, TReturnType>(factory, timeout, action);
			}
			catch (Exception ex)
			{
				Exception ex2 = WcfExceptionTranslator<TService>.TranslateException(ex, new Func<Exception, Exception>(this.GenerateTransientException), new Func<Exception, Exception>(this.GeneratePermanentException));
				if (ex2 != null)
				{
					throw ex2;
				}
				throw;
			}
			return result;
		}
	}
}

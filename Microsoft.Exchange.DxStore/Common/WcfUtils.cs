using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200008D RID: 141
	public static class WcfUtils
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x00013154 File Offset: 0x00011354
		public static IEnumerable<Exception> EnumerateInner(this Exception root)
		{
			if (root != null)
			{
				Queue<Exception> errors = new Queue<Exception>();
				errors.Enqueue(root);
				while (errors.Count > 0)
				{
					Exception error = errors.Dequeue();
					yield return error;
					AggregateException aggregate = error as AggregateException;
					if (aggregate != null)
					{
						using (IEnumerator<Exception> enumerator = aggregate.InnerExceptions.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Exception item = enumerator.Current;
								errors.Enqueue(item);
							}
							continue;
						}
					}
					if (error.InnerException != null)
					{
						errors.Enqueue(error.InnerException);
					}
				}
			}
			yield break;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000131C0 File Offset: 0x000113C0
		public static bool IsChannelException(Exception error)
		{
			return error.EnumerateInner().Any(delegate(Exception e)
			{
				Type type = e.GetType();
				return !typeof(FaultException).IsAssignableFrom(type) && (typeof(CommunicationException).IsAssignableFrom(type) || typeof(TimeoutException).IsAssignableFrom(type));
			});
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000131EC File Offset: 0x000113EC
		public static void CloseChannel(IClientChannel channel)
		{
			if (channel != null)
			{
				try
				{
					channel.Close();
				}
				catch (Exception error)
				{
					channel.Abort();
					if (!WcfUtils.IsChannelException(error))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00013240 File Offset: 0x00011440
		public static void Run<T>(CachedChannelFactory<T> factory, TimeSpan? timeout, Action<T> methodToCall)
		{
			WcfUtils.Run<T, int>(factory, timeout, delegate(T service)
			{
				methodToCall(service);
				return 0;
			});
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00013270 File Offset: 0x00011470
		public static R Run<T, R>(CachedChannelFactory<T> factory, TimeSpan? timeout, Func<T, R> methodToCall)
		{
			T t = factory.Factory.CreateChannel();
			R result;
			using (IClientChannel clientChannel = (IClientChannel)((object)t))
			{
				if (timeout != null)
				{
					clientChannel.OperationTimeout = timeout.Value;
				}
				CommunicationState state = clientChannel.State;
				bool flag = false;
				if (state != CommunicationState.Created)
				{
					if (state != CommunicationState.Closed)
					{
						goto IL_51;
					}
				}
				try
				{
					clientChannel.Open();
					flag = true;
				}
				catch
				{
					clientChannel.Abort();
					throw;
				}
				IL_51:
				bool flag2 = false;
				try
				{
					result = methodToCall(t);
				}
				catch (Exception error)
				{
					if (WcfUtils.IsChannelException(error))
					{
						flag2 = true;
						clientChannel.Abort();
					}
					throw;
				}
				finally
				{
					if (!flag2 && flag)
					{
						WcfUtils.CloseChannel(clientChannel);
					}
				}
			}
			return result;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00013340 File Offset: 0x00011540
		public static DxStoreServerFault ConvertExceptionToDxStoreFault(Exception exception)
		{
			DxStoreFaultCode faultCode = DxStoreFaultCode.General;
			bool isTransientError = true;
			if (exception is DxStoreInstanceNotReadyException)
			{
				faultCode = DxStoreFaultCode.InstanceNotReady;
			}
			else if (exception is DxStoreInstanceStaleStoreException)
			{
				faultCode = DxStoreFaultCode.Stale;
			}
			else if (exception is DxStoreCommandConstraintFailedException)
			{
				faultCode = DxStoreFaultCode.ConstraintNotSatisfied;
			}
			else if (exception is TimeoutException)
			{
				faultCode = DxStoreFaultCode.ServerTimeout;
			}
			else
			{
				isTransientError = false;
			}
			bool isLocalized = exception is LocalizedException;
			return new DxStoreServerFault(exception, faultCode, isTransientError, isLocalized);
		}
	}
}

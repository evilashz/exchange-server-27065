using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000017 RID: 23
	internal class ExceptionHandler
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00004191 File Offset: 0x00002391
		public ExceptionHandler()
		{
			HandlerOverrideLoader.ApplyConfiguredOverrides(this, ConfigurationManager.AppSettings);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000041BA File Offset: 0x000023BA
		public bool Register(string name, Dictionary<Type, Handler> handlerDefinition)
		{
			return this.registeredHandlers.TryAdd(name, new ConcurrentDictionary<Type, Handler>(handlerDefinition));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000041E4 File Offset: 0x000023E4
		public bool AddOrUpdateOverrideHandler(string name, Type exceptionType, Handler handler)
		{
			if (!this.overrideHandlers.ContainsKey(name) && !this.overrideHandlers.TryAdd(name, new ConcurrentDictionary<Type, Handler>()))
			{
				return false;
			}
			ConcurrentDictionary<Type, Handler> concurrentDictionary = this.overrideHandlers[name];
			concurrentDictionary.AddOrUpdate(exceptionType, handler, (Type key, Handler oldvalue) => handler);
			return true;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000424C File Offset: 0x0000244C
		public bool RemoveOverrideHandler(string name, Type exceptionType)
		{
			bool result = false;
			if (this.overrideHandlers.ContainsKey(name))
			{
				Handler handler = null;
				result = this.overrideHandlers[name].TryRemove(exceptionType, out handler);
			}
			else
			{
				ExceptionHandler.Diag.TraceWarning<string>(0L, "RemoveOverrideHandler called, but no registered handler was found with the following name: {0}", name);
			}
			return result;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004298 File Offset: 0x00002498
		public MessageStatus Handle(string handlerName, Exception exception, IMessageConverter converter, TimeSpan fastRetryInterval, TimeSpan quarantineRetryInterval)
		{
			StorageExceptionHandler.LogException(converter, exception);
			exception.GetType();
			Exception exception2 = null;
			Exception exceptionAssociatedWithHandler = null;
			ConcurrentDictionary<Type, Handler> handlerMap = this.registeredHandlers[handlerName];
			ConcurrentDictionary<Type, Handler> overrideHandlerMap = this.overrideHandlers.ContainsKey(handlerName) ? this.overrideHandlers[handlerName] : null;
			Handler handler = ExceptionHandler.FindHandler(handlerMap, overrideHandlerMap, exception, out exception2, out exceptionAssociatedWithHandler);
			if (handler == null)
			{
				handler = ExceptionHandler.ThrowHandler;
			}
			return handler.CreateStatus(exception2, exceptionAssociatedWithHandler, converter, fastRetryInterval, quarantineRetryInterval);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000430C File Offset: 0x0000250C
		private static Handler FindHandler(ConcurrentDictionary<Type, Handler> handlerMap, ConcurrentDictionary<Type, Handler> overrideHandlerMap, Exception exception, out Exception exceptionToProcess, out Exception exceptionAssociatedWithHandler)
		{
			exceptionToProcess = exception;
			exceptionAssociatedWithHandler = exception;
			if (!(exception is StoreDriverAgentRaisedException))
			{
				if (!ExceptionHandler.IsBaseExceptionType(exception))
				{
					Handler handlerForException = ExceptionHandler.GetHandlerForException(handlerMap, overrideHandlerMap, exception, false);
					if (handlerForException != null)
					{
						exceptionToProcess = exception;
						exceptionAssociatedWithHandler = exception;
						return handlerForException;
					}
				}
				if (exception.InnerException != null && !(exception is SmtpResponseException))
				{
					Handler handlerForException2 = ExceptionHandler.GetHandlerForException(handlerMap, overrideHandlerMap, exception.InnerException, true);
					if (handlerForException2 != null)
					{
						exceptionAssociatedWithHandler = exception.InnerException;
						exceptionToProcess = (handlerForException2.ProcessInnerException ? exception.InnerException : exception);
						return handlerForException2;
					}
				}
				return ExceptionHandler.GetHandlerForException(handlerMap, overrideHandlerMap, exception, true);
			}
			if (exception.InnerException != null)
			{
				exceptionToProcess = exception.InnerException;
				exceptionAssociatedWithHandler = exception.InnerException;
				return ExceptionHandler.GetHandlerForException(handlerMap, overrideHandlerMap, exception.InnerException, true);
			}
			return null;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000043BC File Offset: 0x000025BC
		private static Handler GetHandlerForException(ConcurrentDictionary<Type, Handler> handlerMap, ConcurrentDictionary<Type, Handler> overrideHandlerMap, Exception exception, bool traverseTypeHierarchy = true)
		{
			if (exception is SmtpResponseException)
			{
				return ExceptionHandler.CreateHandlerFromSmtpResponseException(exception as SmtpResponseException);
			}
			Type type = exception.GetType();
			while (overrideHandlerMap == null || !overrideHandlerMap.ContainsKey(type))
			{
				if (handlerMap.ContainsKey(type))
				{
					return handlerMap[type];
				}
				type = type.BaseType;
				if (!(type != typeof(object)) || !traverseTypeHierarchy)
				{
					return null;
				}
			}
			return overrideHandlerMap[type];
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004428 File Offset: 0x00002628
		private static bool IsBaseExceptionType(Exception exception)
		{
			Type type = exception.GetType();
			return type == typeof(StoragePermanentException) || type == typeof(StorageTransientException) || type == typeof(MapiPermanentException) || type == typeof(MapiRetryableException);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004484 File Offset: 0x00002684
		private static Handler CreateHandlerFromSmtpResponseException(SmtpResponseException smtpResponseException)
		{
			Handler handler = new Handler
			{
				Action = smtpResponseException.Status.Action,
				Response = smtpResponseException.Status.Response,
				IncludeDiagnosticStatusText = false
			};
			if (smtpResponseException.Status.RetryInterval != null)
			{
				handler.SpecifiedRetryInterval = new TimeSpan?(smtpResponseException.Status.RetryInterval.Value);
			}
			return handler;
		}

		// Token: 0x04000043 RID: 67
		public static readonly Handler ThrowHandler = new Handler
		{
			Action = MessageAction.Throw
		};

		// Token: 0x04000044 RID: 68
		private static readonly Trace Diag = ExTraceGlobals.StoreDriverDeliveryTracer;

		// Token: 0x04000045 RID: 69
		private ConcurrentDictionary<string, ConcurrentDictionary<Type, Handler>> registeredHandlers = new ConcurrentDictionary<string, ConcurrentDictionary<Type, Handler>>();

		// Token: 0x04000046 RID: 70
		private ConcurrentDictionary<string, ConcurrentDictionary<Type, Handler>> overrideHandlers = new ConcurrentDictionary<string, ConcurrentDictionary<Type, Handler>>();
	}
}

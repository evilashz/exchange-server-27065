﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000A3 RID: 163
	internal class ExceptionMapper
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x00012EF0 File Offset: 0x000110F0
		public ExceptionMapper(ICollection<ExceptionMappingBase> exceptionMappings)
		{
			foreach (ExceptionMappingBase exceptionMappingBase in exceptionMappings)
			{
				this.exceptionMappingDictionary[exceptionMappingBase.ExceptionType] = exceptionMappingBase;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00012F54 File Offset: 0x00011154
		public ExceptionMapper(ExceptionMapper chainedExceptionMapper, ICollection<ExceptionMappingBase> exceptionMappings) : this(exceptionMappings)
		{
			this.chainedExceptionMapper = chainedExceptionMapper;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00012F64 File Offset: 0x00011164
		public ServiceError GetServiceError(LocalizedException exception)
		{
			return this.GetServiceError(exception, ExchangeVersion.Current);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00012F74 File Offset: 0x00011174
		public ServiceError GetServiceError(LocalizedException exception, ExchangeVersion currentExchangeVersion)
		{
			ExTraceGlobals.ExceptionTracer.TraceError<LocalizedException>((long)this.GetHashCode(), "ExceptionMapper.TryGetServiceError called for exception: {0}", exception);
			ServicePermanentException ex = exception as ServicePermanentException;
			if (ex != null)
			{
				return ex.CreateServiceError(currentExchangeVersion);
			}
			ExceptionMappingBase exceptionMapping = this.GetExceptionMapping(exception);
			return exceptionMapping.GetServiceError(exception, currentExchangeVersion);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00012FBC File Offset: 0x000111BC
		internal ExceptionMappingBase GetExceptionMapping(LocalizedException exception)
		{
			ExTraceGlobals.ExceptionTracer.TraceError<LocalizedException>((long)this.GetHashCode(), "ExceptionMapper.GetExceptionMapping called for exception: {0}", exception);
			ExceptionMappingBase exceptionMappingBase = null;
			Type type = exception.GetType();
			while (exceptionMappingBase == null && !type.Equals(typeof(Exception)))
			{
				exceptionMappingBase = this.TryMapExceptionType(type);
				type = type.GetTypeInfo().BaseType;
			}
			if (exceptionMappingBase.TryInnerExceptionForExceptionMapping && exception.InnerException != null)
			{
				LocalizedException ex = exception.InnerException as LocalizedException;
				if (ex != null)
				{
					ExTraceGlobals.ExceptionTracer.TraceError((long)this.GetHashCode(), "ExceptionMapper.TryGetExceptionMapping trying InnerException.");
					ExceptionMappingBase exceptionMapping = this.GetExceptionMapping(ex);
					if (exceptionMapping != null && !exceptionMapping.IsUnmappedException)
					{
						exceptionMappingBase = exceptionMapping;
						ExTraceGlobals.ExceptionTracer.TraceError<LocalizedString, LocalizedException>((long)this.GetHashCode(), "Mapping '{0}' found for inner exception '{1}'.", exceptionMapping.GetLocalizedMessage(ex), ex);
					}
				}
			}
			return exceptionMappingBase;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00013080 File Offset: 0x00011280
		private ExceptionMappingBase TryMapExceptionType(Type exceptionType)
		{
			ExceptionMappingBase result = null;
			if (!this.exceptionMappingDictionary.TryGetValue(exceptionType, out result) && this.chainedExceptionMapper != null)
			{
				result = this.chainedExceptionMapper.TryMapExceptionType(exceptionType);
			}
			return result;
		}

		// Token: 0x0400061D RID: 1565
		private ExceptionMapper chainedExceptionMapper;

		// Token: 0x0400061E RID: 1566
		private Dictionary<Type, ExceptionMappingBase> exceptionMappingDictionary = new Dictionary<Type, ExceptionMappingBase>();
	}
}

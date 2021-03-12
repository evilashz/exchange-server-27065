using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.EventLogs;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000218 RID: 536
	internal abstract class ExceptionMappingBase
	{
		// Token: 0x06000DEE RID: 3566 RVA: 0x00044FB8 File Offset: 0x000431B8
		public ExceptionMappingBase(Type exceptionType, ExceptionMappingBase.Attributes attributes)
		{
			this.ExceptionType = exceptionType;
			this.attributes = attributes;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00044FCE File Offset: 0x000431CE
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x00044FD6 File Offset: 0x000431D6
		public Type ExceptionType { get; private set; }

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00044FE0 File Offset: 0x000431E0
		public virtual ServiceError GetServiceError(LocalizedException exception, ExchangeVersion requestVersion)
		{
			ExTraceGlobals.ExceptionTracer.TraceError<LocalizedException>((long)this.GetHashCode(), "ExceptionMappingBase.GetServiceError called for exception: {0}", exception);
			LocalizedString localizedMessage = this.GetLocalizedMessage(exception);
			if (this.ReportException)
			{
				ServiceDiagnostics.ReportException(exception, ServicesEventLogConstants.Tuple_ErrorMappingNotFound, this, "Error mapping not found. Internal server error used for exception: {0}");
			}
			else
			{
				ExTraceGlobals.ExceptionTracer.TraceError<LocalizedString, LocalizedException>((long)this.GetHashCode(), "Mapping '{0}' used for exception '{1}'.", localizedMessage, exception);
			}
			ServiceError serviceError = new ServiceError(exception, localizedMessage, this.GetResponseCode(exception), 0, requestVersion, this.GetEffectiveVersion(exception), (this.attributes & ExceptionMappingBase.Attributes.StopsBatchProcessing) == ExceptionMappingBase.Attributes.StopsBatchProcessing, this.GetPropertyPaths(exception), this.GetConstantValues(exception));
			this.DoServiceErrorPostProcessing(exception, serviceError);
			return serviceError;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00045079 File Offset: 0x00043279
		protected virtual PropertyPath[] GetPropertyPaths(LocalizedException exception)
		{
			return null;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0004507C File Offset: 0x0004327C
		public virtual LocalizedString GetLocalizedMessage(LocalizedException exception)
		{
			return this.GetLocalizedMessage(this.GetResourceId(exception));
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0004508B File Offset: 0x0004328B
		protected virtual LocalizedString GetLocalizedMessage(CoreResources.IDs messageId)
		{
			return CoreResources.GetLocalizedString(messageId);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00045093 File Offset: 0x00043293
		protected virtual IDictionary<string, string> GetConstantValues(LocalizedException exception)
		{
			return null;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00045096 File Offset: 0x00043296
		protected virtual void DoServiceErrorPostProcessing(LocalizedException exception, ServiceError error)
		{
		}

		// Token: 0x06000DF7 RID: 3575
		protected abstract ResponseCodeType GetResponseCode(LocalizedException exception);

		// Token: 0x06000DF8 RID: 3576
		protected abstract ExchangeVersion GetEffectiveVersion(LocalizedException exception);

		// Token: 0x06000DF9 RID: 3577
		protected abstract CoreResources.IDs GetResourceId(LocalizedException exception);

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00045098 File Offset: 0x00043298
		public bool StopsBatchProcessing
		{
			get
			{
				return this.IsAttributeSet(ExceptionMappingBase.Attributes.StopsBatchProcessing);
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x000450A1 File Offset: 0x000432A1
		public bool IsUnmappedException
		{
			get
			{
				return this.IsAttributeSet(ExceptionMappingBase.Attributes.IsUnmappedException);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x000450AA File Offset: 0x000432AA
		public bool ReportException
		{
			get
			{
				return this.IsAttributeSet(ExceptionMappingBase.Attributes.ReportException);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x000450B3 File Offset: 0x000432B3
		public bool TryInnerExceptionForExceptionMapping
		{
			get
			{
				return this.IsAttributeSet(ExceptionMappingBase.Attributes.TryInnerExceptionForExceptionMapping);
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x000450BC File Offset: 0x000432BC
		protected T VerifyExceptionType<T>(LocalizedException exception) where T : LocalizedException
		{
			return exception as T;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x000450D6 File Offset: 0x000432D6
		private bool IsAttributeSet(ExceptionMappingBase.Attributes checkAttributes)
		{
			return (this.attributes & checkAttributes) == checkAttributes;
		}

		// Token: 0x04000AE0 RID: 2784
		private ExceptionMappingBase.Attributes attributes;

		// Token: 0x02000219 RID: 537
		[Flags]
		public enum Attributes
		{
			// Token: 0x04000AE3 RID: 2787
			None = 0,
			// Token: 0x04000AE4 RID: 2788
			IsUnmappedException = 1,
			// Token: 0x04000AE5 RID: 2789
			ReportException = 2,
			// Token: 0x04000AE6 RID: 2790
			TryInnerExceptionForExceptionMapping = 4,
			// Token: 0x04000AE7 RID: 2791
			StopsBatchProcessing = 8
		}
	}
}

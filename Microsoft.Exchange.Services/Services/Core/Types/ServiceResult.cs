using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000884 RID: 2180
	internal class ServiceResult<TValue>
	{
		// Token: 0x06003E82 RID: 16002 RVA: 0x000D8C1C File Offset: 0x000D6E1C
		public ServiceResult(TValue value, ServiceError error)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (error == null)
			{
				throw new ArgumentNullException("error");
			}
			this.resultValue = value;
			this.error = error;
			this.code = ServiceResultCode.Warning;
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x000D8C7C File Offset: 0x000D6E7C
		public ServiceResult(TValue value)
		{
			this.resultValue = value;
			this.code = ServiceResultCode.Success;
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x000D8CA9 File Offset: 0x000D6EA9
		public ServiceResult(ServiceError error)
		{
			if (error == null)
			{
				throw new ArgumentNullException("error");
			}
			this.error = error;
			this.code = ServiceResultCode.Error;
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x000D8CE4 File Offset: 0x000D6EE4
		public ServiceResult(ServiceResultCode code, TValue value, ServiceError error)
		{
			bool flag = code == ServiceResultCode.Success;
			bool flag2 = code == ServiceResultCode.Error || code == ServiceResultCode.Warning;
			if (flag && value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (flag2 && error == null)
			{
				throw new ArgumentNullException("error");
			}
			if (!flag2 && error != null)
			{
				throw new ArgumentException("Must be null", "error");
			}
			this.resultValue = value;
			this.error = error;
			this.code = code;
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06003E86 RID: 16006 RVA: 0x000D8D71 File Offset: 0x000D6F71
		public TValue Value
		{
			get
			{
				return this.resultValue;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06003E87 RID: 16007 RVA: 0x000D8D79 File Offset: 0x000D6F79
		// (set) Token: 0x06003E88 RID: 16008 RVA: 0x000D8D81 File Offset: 0x000D6F81
		public ServiceError Error
		{
			get
			{
				return this.error;
			}
			set
			{
				this.error = value;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06003E89 RID: 16009 RVA: 0x000D8D8A File Offset: 0x000D6F8A
		// (set) Token: 0x06003E8A RID: 16010 RVA: 0x000D8D92 File Offset: 0x000D6F92
		public ServiceResultCode Code
		{
			get
			{
				return this.code;
			}
			set
			{
				this.code = value;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06003E8B RID: 16011 RVA: 0x000D8D9B File Offset: 0x000D6F9B
		public bool IsStopBatchProcessingError
		{
			get
			{
				return this.error != null && this.error.StopsBatchProcessing;
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06003E8C RID: 16012 RVA: 0x000D8DB2 File Offset: 0x000D6FB2
		internal Dictionary<string, object> RequestData
		{
			get
			{
				return this.requestData;
			}
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x000D8DBC File Offset: 0x000D6FBC
		public static void ProcessServiceResults(ServiceResult<TValue>[] results, ProcessServiceResult<TValue> processDelegate)
		{
			ServiceError serviceError = null;
			bool flag = false;
			ServiceResult<TValue> serviceResult = null;
			for (int i = 0; i < results.Length; i++)
			{
				ServiceResult<TValue> serviceResult2 = results[i];
				if (serviceError == null)
				{
					serviceError = serviceResult2.Error;
				}
				if (flag)
				{
					if (serviceResult == null)
					{
						serviceResult = new ServiceResult<TValue>(ServiceResultCode.Warning, default(TValue), ServiceError.CreateBatchProcessingStoppedError());
					}
					serviceResult2 = serviceResult;
					results[i] = serviceResult2;
				}
				else
				{
					flag = serviceResult2.IsStopBatchProcessingError;
				}
				if (processDelegate != null)
				{
					processDelegate(serviceResult2);
				}
			}
			if (results.Length == 1 && CallContext.Current != null && CallContext.Current.IsOwa)
			{
				ServiceResult<TValue> serviceResult3 = results[0];
				if (serviceResult3.Error != null && serviceResult3.Error.IsTransient)
				{
					CallContext.Current.IsTransientErrorResponse = true;
				}
			}
		}

		// Token: 0x040023DE RID: 9182
		private TValue resultValue = default(TValue);

		// Token: 0x040023DF RID: 9183
		private ServiceError error;

		// Token: 0x040023E0 RID: 9184
		private ServiceResultCode code;

		// Token: 0x040023E1 RID: 9185
		private Dictionary<string, object> requestData = new Dictionary<string, object>();
	}
}

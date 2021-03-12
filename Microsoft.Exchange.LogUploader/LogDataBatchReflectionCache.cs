using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200001A RID: 26
	internal static class LogDataBatchReflectionCache<T> where T : LogDataBatch
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005954 File Offset: 0x00003B54
		internal static bool IsRawBatch
		{
			get
			{
				if (LogDataBatchReflectionCache<T>.attrCache == null)
				{
					object[] customAttributes = typeof(T).GetCustomAttributes(typeof(LogDataBatchAttribute), false);
					LogDataBatchReflectionCache<T>.attrCache = ((customAttributes.Length == 1) ? ((LogDataBatchAttribute)customAttributes[0]) : new LogDataBatchAttribute());
				}
				return LogDataBatchReflectionCache<T>.attrCache.IsRawBatch;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000059A7 File Offset: 0x00003BA7
		internal static bool IsMessageBatch
		{
			get
			{
				if (LogDataBatchReflectionCache<T>.isMessageBatch == null)
				{
					LogDataBatchReflectionCache<T>.isMessageBatch = new bool?(typeof(MessageBatchBase).IsAssignableFrom(typeof(T)));
				}
				return LogDataBatchReflectionCache<T>.isMessageBatch.Value;
			}
		}

		// Token: 0x0400009B RID: 155
		private static LogDataBatchAttribute attrCache;

		// Token: 0x0400009C RID: 156
		private static bool? isMessageBatch;
	}
}

using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Logging.MailboxStatistics;
using Microsoft.Exchange.MailboxLoadBalance.Logging.SoftDeletedMailboxRemoval;

namespace Microsoft.Exchange.MailboxLoadBalance.Logging
{
	// Token: 0x020000A5 RID: 165
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ObjectLogCollector
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x0000F614 File Offset: 0x0000D814
		public ObjectLogCollector()
		{
			this.loggers[typeof(MailboxStatisticsLogEntry)] = MailboxStatisticsLog.CreateWithConfig(this.GetLogConfig("MailboxStatistics"));
			this.loggers[typeof(SoftDeletedMailboxRemovalLogEntry)] = SoftDeletedMailboxRemovalLog.CreateWithConfig(this.GetLogConfig("SoftDeletedRemoval"));
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000F67C File Offset: 0x0000D87C
		public virtual void LogObject<TObject>(TObject obj) where TObject : ConfigurableObject
		{
			ObjectLog<TObject> loggerInstance = this.GetLoggerInstance<TObject>();
			loggerInstance.LogObject(obj);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000F697 File Offset: 0x0000D897
		protected virtual ObjectLogConfiguration GetLogConfig(string logName)
		{
			return new LoadBalanceLoggingConfig(logName);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		protected virtual ObjectLog<TObject> GetLoggerInstance<TObject>()
		{
			object obj;
			if (!this.loggers.TryGetValue(typeof(TObject), out obj))
			{
				throw new NotSupportedException("No known logger for objects of type " + typeof(TObject));
			}
			return (ObjectLog<TObject>)obj;
		}

		// Token: 0x040001FB RID: 507
		private readonly ConcurrentDictionary<Type, object> loggers = new ConcurrentDictionary<Type, object>();
	}
}

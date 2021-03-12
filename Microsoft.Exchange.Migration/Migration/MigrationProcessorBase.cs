using System;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000FA RID: 250
	internal abstract class MigrationProcessorBase<T, TResponse> where TResponse : MigrationProcessorResponse
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x000368CC File Offset: 0x00034ACC
		protected MigrationProcessorBase(T migrationObject, IMigrationDataProvider dataProvider)
		{
			this.MigrationObject = migrationObject;
			this.DataProvider = dataProvider;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000368E4 File Offset: 0x00034AE4
		internal TResponse Process()
		{
			MigrationEventType eventType = MigrationEventType.Information;
			string format = "Processor {0} - Starting to process {1} {2}";
			object[] array = new object[3];
			array[0] = base.GetType().Name;
			object[] array2 = array;
			int num = 1;
			T migrationObject = this.MigrationObject;
			array2[num] = migrationObject.GetType().Name;
			array[2] = this.MigrationObject;
			MigrationLogger.Log(eventType, format, array);
			MigrationLogger.Flush();
			Stopwatch stopwatch = Stopwatch.StartNew();
			TResponse tresponse = this.PerformPoisonDetection();
			if (tresponse.Result != MigrationProcessorResult.Working)
			{
				return this.ApplyResponse(tresponse);
			}
			try
			{
				this.SetContext();
				try
				{
					tresponse = this.InternalProcess();
				}
				catch (LocalizedException ex)
				{
					if (CommonUtils.IsTransientException(ex))
					{
						tresponse = this.HandleTransientException(ex);
					}
					else
					{
						tresponse = this.HandlePermanentException(ex);
					}
				}
				MigrationEventType eventType2 = MigrationEventType.Information;
				string format2 = "Processor {0} - Applying result {1} to {2} {3}.";
				object[] array3 = new object[4];
				array3[0] = base.GetType().Name;
				array3[1] = tresponse;
				object[] array4 = array3;
				int num2 = 2;
				T migrationObject2 = this.MigrationObject;
				array4[num2] = migrationObject2.GetType().Name;
				array3[3] = this.MigrationObject;
				MigrationLogger.Log(eventType2, format2, array3);
				tresponse.ClearPoison = true;
				tresponse.ProcessingDuration = new TimeSpan?(stopwatch.Elapsed);
				tresponse = this.ApplyResponse(tresponse);
			}
			finally
			{
				this.RestoreContext();
			}
			stopwatch.Stop();
			MigrationEventType eventType3 = MigrationEventType.Information;
			string format3 = "Processor {0} - Finished result {1} processing {2} {3}. Duration {4}.";
			object[] array5 = new object[5];
			array5[0] = base.GetType().Name;
			array5[1] = tresponse;
			object[] array6 = array5;
			int num3 = 2;
			T migrationObject3 = this.MigrationObject;
			array6[num3] = migrationObject3.GetType().Name;
			array5[3] = this.MigrationObject;
			array5[4] = stopwatch.Elapsed;
			MigrationLogger.Log(eventType3, format3, array5);
			return tresponse;
		}

		// Token: 0x06000D47 RID: 3399
		protected abstract TResponse InternalProcess();

		// Token: 0x06000D48 RID: 3400
		protected abstract TResponse PerformPoisonDetection();

		// Token: 0x06000D49 RID: 3401
		protected abstract void SetContext();

		// Token: 0x06000D4A RID: 3402
		protected abstract void RestoreContext();

		// Token: 0x06000D4B RID: 3403
		protected abstract TResponse ApplyResponse(TResponse response);

		// Token: 0x06000D4C RID: 3404
		protected abstract TResponse HandleTransientException(LocalizedException ex);

		// Token: 0x06000D4D RID: 3405
		protected abstract TResponse HandlePermanentException(LocalizedException ex);

		// Token: 0x040004D2 RID: 1234
		protected readonly T MigrationObject;

		// Token: 0x040004D3 RID: 1235
		protected readonly IMigrationDataProvider DataProvider;
	}
}

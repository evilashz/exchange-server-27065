using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000108 RID: 264
	internal class RemoveMigrationJobProcessor : MigrationJobProcessorBase
	{
		// Token: 0x06000DC5 RID: 3525 RVA: 0x00038D6D File Offset: 0x00036F6D
		public RemoveMigrationJobProcessor(MigrationJob migrationObject, IMigrationDataProvider dataProvider) : base(migrationObject, dataProvider)
		{
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00038D8C File Offset: 0x00036F8C
		protected override Func<int?, IEnumerable<StoreObjectId>>[] ProcessableChildObjectQueries
		{
			get
			{
				return new Func<int?, IEnumerable<StoreObjectId>>[]
				{
					(int? maxCount) => MigrationJobItem.GetAllIds(this.DataProvider, this.MigrationObject, maxCount)
				};
			}
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00038DB0 File Offset: 0x00036FB0
		protected override MigrationProcessorResponse ProcessChild(MigrationJobItem child)
		{
			MigrationJobItemProcessorBase migrationJobItemProcessorBase = new RemoveMigrationJobItemProcessor(child, this.DataProvider);
			return migrationJobItemProcessorBase.Process();
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00038DD0 File Offset: 0x00036FD0
		protected override MigrationJobProcessorResponse ProcessObject()
		{
			return MigrationJobProcessorResponse.Create(MigrationProcessorResult.Deleted, null, null, null, null, null);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00038DF0 File Offset: 0x00036FF0
		protected override MigrationJobProcessorResponse ApplyResponse(MigrationJobProcessorResponse response)
		{
			if (response.Result == MigrationProcessorResult.Deleted)
			{
				if (!this.GetChildObjectIds(this.ProcessableChildObjectQueries, new int?(1)).Any<StoreObjectId>())
				{
					this.MigrationObject.Delete(this.DataProvider, false);
					return response;
				}
				response.Result = MigrationProcessorResult.Working;
			}
			else if (response.Result == MigrationProcessorResult.Failed && response.Error == null)
			{
				response.Error = new FailureToRemoveTransientException();
			}
			return base.ApplyResponse(response);
		}
	}
}

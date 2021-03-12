using System;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002AB RID: 683
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class BrowsePeopleInPublicFolder : FindPeopleImplementation
	{
		// Token: 0x06001257 RID: 4695 RVA: 0x0005A21E File Offset: 0x0005841E
		public BrowsePeopleInPublicFolder(FindPeopleParameters parameters, IdAndSession idAndSession) : base(parameters, null, true)
		{
			ServiceCommandBase.ThrowIfNull(idAndSession, "idAndSession", "BrowsePeopleInPublicFolder::BrowsePeopleInPublicFolder");
			this.idAndSession = idAndSession;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0005A240 File Offset: 0x00058440
		public override void Validate()
		{
			this.ValidatePaging();
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0005A248 File Offset: 0x00058448
		protected override void ValidatePaging()
		{
			base.ValidatePaging();
			if (!(base.Paging is IndexedPageView))
			{
				throw new ServiceArgumentException(CoreResources.IDs.ErrorInvalidIndexedPagingParameters);
			}
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0005A270 File Offset: 0x00058470
		public override FindPeopleResult Execute()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			FindPeopleResult findPeopleResult = this.ExecuteInternal();
			stopwatch.Stop();
			base.Log(FindPeopleMetadata.PersonalSearchTime, stopwatch.ElapsedMilliseconds);
			base.Log(FindPeopleMetadata.PersonalCount, findPeopleResult.PersonaList.Length);
			base.Log(FindPeopleMetadata.TotalNumberOfPeopleInView, findPeopleResult.TotalNumberOfPeopleInView);
			return findPeopleResult;
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0005A2CC File Offset: 0x000584CC
		private FindPeopleResult ExecuteInternal()
		{
			SortBy[] sortBy = Microsoft.Exchange.Services.Core.Search.SortResults.ToXsoSortBy(base.SortResults);
			FindPeopleResult result;
			using (Folder folder = Folder.Bind(this.idAndSession.Session, this.idAndSession.Id, null))
			{
				result = FindPeopleImplementation.QueryContactsInPublicFolder((PublicFolderSession)this.idAndSession.Session, folder, sortBy, (IndexedPageView)base.Paging, null);
			}
			return result;
		}

		// Token: 0x04000D06 RID: 3334
		private readonly IdAndSession idAndSession;
	}
}

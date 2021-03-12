using System;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200036D RID: 877
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SearchPeopleInPublicFolder : FindPeopleImplementation
	{
		// Token: 0x06001899 RID: 6297 RVA: 0x000866C6 File Offset: 0x000848C6
		public SearchPeopleInPublicFolder(FindPeopleParameters parameters, IdAndSession idAndSession) : base(parameters, SearchPeopleStrategy.AdditionalSupportedProperties, false)
		{
			ServiceCommandBase.ThrowIfNull(parameters, "parameters", "SearchPeopleInPublicFolder::SearchPeopleInPublicFolder");
			ServiceCommandBase.ThrowIfNull(idAndSession, "idAndSession", "SearchPeopleInPublicFolder::SearchPeopleInPublicFolder");
			this.parameters = parameters;
			this.idAndSession = idAndSession;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00086704 File Offset: 0x00084904
		public override FindPeopleResult Execute()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			Persona[] array = this.ExecuteInternal();
			stopwatch.Stop();
			base.Log(FindPeopleMetadata.PersonalSearchTime, stopwatch.ElapsedMilliseconds);
			base.Log(FindPeopleMetadata.PersonalCount, array.Length);
			return FindPeopleResult.CreateSearchResult(array);
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0008674C File Offset: 0x0008494C
		private Persona[] ExecuteInternal()
		{
			QueryFilter andValidateRestrictionFilter = base.GetAndValidateRestrictionFilter();
			PublicFolderSession session = (PublicFolderSession)this.idAndSession.Session;
			SearchPeopleInPublicFolderStrategy searchPeopleInPublicFolderStrategy = new SearchPeopleInPublicFolderStrategy(session, this.parameters, this.idAndSession.Id, andValidateRestrictionFilter);
			return searchPeopleInPublicFolderStrategy.Execute();
		}

		// Token: 0x0400107F RID: 4223
		private readonly FindPeopleParameters parameters;

		// Token: 0x04001080 RID: 4224
		private readonly IdAndSession idAndSession;
	}
}

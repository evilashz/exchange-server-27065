using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002F3 RID: 755
	internal sealed class FindPeopleResult
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x0006D29E File Offset: 0x0006B49E
		private FindPeopleResult()
		{
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0006D2A8 File Offset: 0x0006B4A8
		public static FindPeopleResult CreateSearchResult(Persona[] personaList)
		{
			return new FindPeopleResult
			{
				PersonaList = personaList
			};
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0006D2C4 File Offset: 0x0006B4C4
		public static FindPeopleResult CreateMailboxBrowseResult(Persona[] personaList, int totalNumberOfPeopleInView, int firstLoadedRowIndex, int firstMatchingRowIndex)
		{
			return new FindPeopleResult
			{
				PersonaList = personaList,
				FirstLoadedRowIndex = firstLoadedRowIndex,
				FirstMatchingRowIndex = firstMatchingRowIndex,
				TotalNumberOfPeopleInView = totalNumberOfPeopleInView
			};
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0006D2F4 File Offset: 0x0006B4F4
		public static FindPeopleResult CreateMailFolderBrowseResult(Persona[] personaList)
		{
			return new FindPeopleResult
			{
				PersonaList = personaList
			};
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0006D310 File Offset: 0x0006B510
		public static FindPeopleResult CreateMailboxBrowseResult(Persona[] personaList, int totalNumberOfPeopleInView)
		{
			return new FindPeopleResult
			{
				PersonaList = personaList,
				TotalNumberOfPeopleInView = totalNumberOfPeopleInView
			};
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0006D334 File Offset: 0x0006B534
		public static FindPeopleResult CreateDirectoryBrowseResult(Persona[] personaList, int totalNumberOfPeopleInView, int firstLoadedRowIndex)
		{
			return new FindPeopleResult
			{
				PersonaList = personaList,
				TotalNumberOfPeopleInView = totalNumberOfPeopleInView,
				FirstLoadedRowIndex = firstLoadedRowIndex
			};
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0006D35D File Offset: 0x0006B55D
		// (set) Token: 0x0600154F RID: 5455 RVA: 0x0006D365 File Offset: 0x0006B565
		public Persona[] PersonaList { get; private set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0006D36E File Offset: 0x0006B56E
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x0006D376 File Offset: 0x0006B576
		public int FirstLoadedRowIndex { get; private set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0006D37F File Offset: 0x0006B57F
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x0006D387 File Offset: 0x0006B587
		public int TotalNumberOfPeopleInView { get; private set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0006D390 File Offset: 0x0006B590
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x0006D398 File Offset: 0x0006B598
		public int FirstMatchingRowIndex { get; private set; }
	}
}

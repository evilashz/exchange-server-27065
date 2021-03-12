using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000526 RID: 1318
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PersonNotes
	{
		// Token: 0x060038CF RID: 14543 RVA: 0x000E8F07 File Offset: 0x000E7107
		public PersonNotes(string notes, bool isTruncated)
		{
			this.notesBody = notes;
			this.isTruncated = isTruncated;
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x000E8F1D File Offset: 0x000E711D
		public string NotesBody
		{
			get
			{
				return this.notesBody;
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x060038D1 RID: 14545 RVA: 0x000E8F25 File Offset: 0x000E7125
		public bool IsTruncated
		{
			get
			{
				return this.isTruncated;
			}
		}

		// Token: 0x04001E1B RID: 7707
		private readonly string notesBody;

		// Token: 0x04001E1C RID: 7708
		private readonly bool isTruncated;
	}
}

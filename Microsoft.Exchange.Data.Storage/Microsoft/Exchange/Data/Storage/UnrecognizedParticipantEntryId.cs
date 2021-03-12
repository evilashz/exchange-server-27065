using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000925 RID: 2341
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UnrecognizedParticipantEntryId : ParticipantEntryId
	{
		// Token: 0x06005788 RID: 22408 RVA: 0x00168203 File Offset: 0x00166403
		internal UnrecognizedParticipantEntryId(byte[] entryId)
		{
			this.entryId = entryId;
		}

		// Token: 0x06005789 RID: 22409 RVA: 0x00168212 File Offset: 0x00166412
		public override string ToString()
		{
			return string.Format("Unrecognized: {0} bytes", this.entryId.Length);
		}

		// Token: 0x0600578A RID: 22410 RVA: 0x0016822B File Offset: 0x0016642B
		internal override IEnumerable<PropValue> GetParticipantProperties()
		{
			return Array<PropValue>.Empty;
		}

		// Token: 0x0600578B RID: 22411 RVA: 0x00168232 File Offset: 0x00166432
		protected override void Serialize(ParticipantEntryId.Writer writer)
		{
			writer.Write(this.entryId);
		}

		// Token: 0x04002EB9 RID: 11961
		private readonly byte[] entryId;
	}
}

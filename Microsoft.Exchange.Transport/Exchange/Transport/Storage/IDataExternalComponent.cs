using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000BD RID: 189
	internal interface IDataExternalComponent : IDataObjectComponent
	{
		// Token: 0x0600066B RID: 1643
		void MarkToDelete();

		// Token: 0x0600066C RID: 1644
		void SaveToExternalRow(Transaction transaction);

		// Token: 0x0600066D RID: 1645
		void ParentPrimaryKeyChanged();
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000189 RID: 393
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFolder : IDisposable
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060007C0 RID: 1984
		IPropertyBag PropertyBag { get; }

		// Token: 0x060007C1 RID: 1985
		IEnumerable<IMessage> GetContents();

		// Token: 0x060007C2 RID: 1986
		IEnumerable<IMessage> GetAssociatedContents();

		// Token: 0x060007C3 RID: 1987
		IEnumerable<IFolder> GetFolders();

		// Token: 0x060007C4 RID: 1988
		IFolder CreateFolder();

		// Token: 0x060007C5 RID: 1989
		IMessage CreateMessage(bool isAssociatedMessage);

		// Token: 0x060007C6 RID: 1990
		void Save();

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060007C7 RID: 1991
		bool IsContentAvailable { get; }

		// Token: 0x060007C8 RID: 1992
		string[] GetReplicaDatabases(out ushort localSiteDatabaseCount);

		// Token: 0x060007C9 RID: 1993
		StoreLongTermId GetLongTermId();
	}
}

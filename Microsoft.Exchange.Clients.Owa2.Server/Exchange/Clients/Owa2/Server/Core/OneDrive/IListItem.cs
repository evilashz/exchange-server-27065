using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000020 RID: 32
	public interface IListItem : IClientObject<ListItem>
	{
		// Token: 0x17000039 RID: 57
		object this[string fieldName]
		{
			get;
			set;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A6 RID: 166
		int Id { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A7 RID: 167
		string IdAsString { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A8 RID: 168
		IFile File { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A9 RID: 169
		IFolder Folder { get; }

		// Token: 0x060000AA RID: 170
		void BreakRoleInheritance(bool copyRoleAssignments, bool clearSubscopes);
	}
}

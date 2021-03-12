using System;
using System.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000146 RID: 326
	public class ObjectListEditor : ListEditorBase<ADObjectId>
	{
		// Token: 0x06000D28 RID: 3368 RVA: 0x00030646 File Offset: 0x0002E846
		public ObjectListEditor()
		{
			base.Name = "ObjectListEditor";
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00030659 File Offset: 0x0002E859
		protected override void InsertChangedObject(DataRow dataRow)
		{
			base.ChangedObjects.Add(dataRow["Identity"] as ADObjectId, (dataRow["ObjectClass"] ?? string.Empty).ToString());
		}
	}
}

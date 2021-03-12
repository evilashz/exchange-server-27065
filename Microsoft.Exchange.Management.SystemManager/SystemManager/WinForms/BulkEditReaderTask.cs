using System;
using System.Data;
using System.Reflection;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000063 RID: 99
	internal class BulkEditReaderTask : Reader
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0000D1BE File Offset: 0x0000B3BE
		public BulkEditReaderTask(Type configObjectType, IDataObjectCreator creator, string dataObjectName, DataTable table)
		{
			this.type = configObjectType;
			this.creator = creator;
			this.dataObjectName = dataObjectName;
			this.table = table;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000D1E3 File Offset: 0x0000B3E3
		public override object DataObject
		{
			get
			{
				return this.dataObject;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000D1EC File Offset: 0x0000B3EC
		public override void Run(object interactionHandler, DataRow row, DataObjectStore store)
		{
			if (this.dataObject == null)
			{
				if (this.creator != null)
				{
					this.dataObject = this.creator.Create(this.table);
					return;
				}
				ConstructorInfo constructor = this.type.GetConstructor(new Type[0]);
				if (null != constructor)
				{
					this.dataObject = constructor.Invoke(new object[0]);
					return;
				}
			}
			else
			{
				if (store.GetDataObject(this.dataObjectName) != null)
				{
					this.dataObject = store.GetDataObject(this.dataObjectName);
				}
				IConfigurable configurable = this.DataObject as IConfigurable;
				if (configurable != null)
				{
					try
					{
						configurable.ResetChangeTracking();
					}
					catch (NotImplementedException)
					{
					}
				}
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000D29C File Offset: 0x0000B49C
		public override bool IsRunnable(DataRow row, DataObjectStore store)
		{
			return true;
		}

		// Token: 0x040000F6 RID: 246
		private object dataObject;

		// Token: 0x040000F7 RID: 247
		private string dataObjectName;

		// Token: 0x040000F8 RID: 248
		private Type type;

		// Token: 0x040000F9 RID: 249
		private IDataObjectCreator creator;

		// Token: 0x040000FA RID: 250
		private DataTable table;
	}
}

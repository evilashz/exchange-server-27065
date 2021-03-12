using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200006A RID: 106
	public class CreateDataObjectReader : Reader
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x0000F0D6 File Offset: 0x0000D2D6
		public CreateDataObjectReader()
		{
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000F0DE File Offset: 0x0000D2DE
		public CreateDataObjectReader(Type type)
		{
			this.Type = type;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000F0ED File Offset: 0x0000D2ED
		public CreateDataObjectReader(string typeString) : this(ObjectSchemaLoader.GetTypeByString(typeString))
		{
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000F0FB File Offset: 0x0000D2FB
		public override object DataObject
		{
			get
			{
				return this.dataObject;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000F103 File Offset: 0x0000D303
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000F10B File Offset: 0x0000D30B
		public Type Type { get; set; }

		// Token: 0x06000407 RID: 1031 RVA: 0x0000F114 File Offset: 0x0000D314
		public override void Run(object interactionHandler, DataRow row, DataObjectStore store)
		{
			this.dataObject = this.Type.GetConstructor(new Type[0]).Invoke(new object[0]);
			foreach (ParameterProfile parameterProfile in this.paramInfos)
			{
				if (parameterProfile.IsRunnable(row))
				{
					object obj = row[parameterProfile.Reference];
					if (DBNull.Value.Equals(obj))
					{
						obj = null;
					}
					this.Type.GetProperty(parameterProfile.Name).SetValue(this.dataObject, obj, null);
				}
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		public override void BuildParameters(DataRow row, DataObjectStore store, IList<ParameterProfile> paramInfos)
		{
			base.BuildParameters(row, store, paramInfos);
			this.paramInfos = paramInfos;
		}

		// Token: 0x0400010E RID: 270
		private object dataObject;

		// Token: 0x0400010F RID: 271
		private IList<ParameterProfile> paramInfos;
	}
}

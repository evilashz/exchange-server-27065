using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001C7 RID: 455
	internal class ObjectLogSimplePropertyDefinition<T> : IObjectLogPropertyDefinition<T>
	{
		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002EF23 File Offset: 0x0002D123
		public ObjectLogSimplePropertyDefinition(string fieldName, Func<T, object> getValueFunc)
		{
			this.fieldName = fieldName;
			this.getValueFunc = getValueFunc;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0002EF39 File Offset: 0x0002D139
		string IObjectLogPropertyDefinition<!0>.FieldName
		{
			get
			{
				return this.fieldName;
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002EF41 File Offset: 0x0002D141
		object IObjectLogPropertyDefinition<!0>.GetValue(T objectToLog)
		{
			return this.getValueFunc(objectToLog);
		}

		// Token: 0x04000971 RID: 2417
		private string fieldName;

		// Token: 0x04000972 RID: 2418
		private Func<T, object> getValueFunc;
	}
}

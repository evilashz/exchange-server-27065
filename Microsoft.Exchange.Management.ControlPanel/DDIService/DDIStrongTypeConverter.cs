using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000126 RID: 294
	public class DDIStrongTypeConverter : IOutputConverter, IDDIConverter
	{
		// Token: 0x0600207D RID: 8317 RVA: 0x00062457 File Offset: 0x00060657
		public DDIStrongTypeConverter(Type targetType, ConvertMode convertMode)
		{
			this.targetType = targetType;
			this.convertMode = convertMode;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x00062470 File Offset: 0x00060670
		public bool CanConvert(object sourceObject)
		{
			if (sourceObject == null || !this.targetType.IsDataContract())
			{
				return false;
			}
			IEnumerable enumerable = sourceObject as IEnumerable;
			if (this.convertMode == ConvertMode.PerItemInEnumerable && enumerable != null)
			{
				Type type = null;
				foreach (object obj in enumerable)
				{
					if (type == null)
					{
						type = obj.GetType();
					}
					else if (type != obj.GetType())
					{
						throw new InvalidOperationException("Can't convert the items in an enumerable. The items are not objects of the same type");
					}
				}
				return type == null || this.targetType.GetConstructor(new Type[]
				{
					type
				}) != null;
			}
			return this.targetType.GetConstructor(new Type[]
			{
				sourceObject.GetType()
			}) != null;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x00062564 File Offset: 0x00060764
		public object Convert(object sourceObject)
		{
			IEnumerable enumerable = sourceObject as IEnumerable;
			if (this.convertMode == ConvertMode.PerItemInEnumerable && enumerable != null)
			{
				List<object> list = new List<object>();
				foreach (object obj in enumerable)
				{
					list.Add(Activator.CreateInstance(this.targetType, new object[]
					{
						obj
					}));
				}
				return list;
			}
			return Activator.CreateInstance(this.targetType, new object[]
			{
				sourceObject
			});
		}

		// Token: 0x04001CE9 RID: 7401
		private Type targetType;

		// Token: 0x04001CEA RID: 7402
		private ConvertMode convertMode;
	}
}

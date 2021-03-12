using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000EBD RID: 3773
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AccessorTemplates
	{
		// Token: 0x06008266 RID: 33382 RVA: 0x0023979F File Offset: 0x0023799F
		internal static List<T> ListPropertyGetter<T>(ref List<T> field)
		{
			if (field == null)
			{
				field = new List<T>();
			}
			return field;
		}

		// Token: 0x06008267 RID: 33383 RVA: 0x002397AE File Offset: 0x002379AE
		internal static void ListPropertySetter<T>(ref List<T> field, List<T> val)
		{
			if (val == null)
			{
				if (field != null)
				{
					field.Clear();
					return;
				}
			}
			else
			{
				if (field == null)
				{
					field = new List<T>(val);
					return;
				}
				field.Clear();
				field.AddRange(val);
			}
		}

		// Token: 0x06008268 RID: 33384 RVA: 0x002397DC File Offset: 0x002379DC
		internal static T DefaultConstructionPropertyGetter<T>(ref T field) where T : new()
		{
			if (field == null)
			{
				field = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
			}
			return field;
		}
	}
}

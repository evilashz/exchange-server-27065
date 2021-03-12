using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000127 RID: 295
	public class DDIToStringArrayConverter : IOutputConverter, IDDIConverter
	{
		// Token: 0x06002080 RID: 8320 RVA: 0x00062604 File Offset: 0x00060804
		public DDIToStringArrayConverter(string propertyName)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x00062614 File Offset: 0x00060814
		public bool CanConvert(object sourceObject)
		{
			if (sourceObject == null)
			{
				return false;
			}
			IEnumerable enumerable = sourceObject as IEnumerable;
			if (enumerable == null)
			{
				return false;
			}
			if (!string.IsNullOrEmpty(this.propertyName))
			{
				foreach (object obj in enumerable)
				{
					if (obj.GetType().GetProperty(this.propertyName) == null)
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x000626A0 File Offset: 0x000608A0
		public object Convert(object sourceObject)
		{
			IEnumerable enumerable = sourceObject as IEnumerable;
			List<string> list = new List<string>();
			bool flag = !string.IsNullOrEmpty(this.propertyName);
			foreach (object obj in enumerable)
			{
				if (flag)
				{
					list.Add((string)obj.GetType().GetProperty(this.propertyName).GetValue(obj, null));
				}
				else
				{
					list.Add(obj.ToString());
				}
			}
			return list.ToArray();
		}

		// Token: 0x04001CEB RID: 7403
		private readonly string propertyName;
	}
}

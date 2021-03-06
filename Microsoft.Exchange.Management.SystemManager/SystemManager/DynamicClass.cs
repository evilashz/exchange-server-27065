using System;
using System.Reflection;
using System.Text;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200007B RID: 123
	public abstract class DynamicClass
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		public override string ToString()
		{
			PropertyInfo[] properties = base.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			for (int i = 0; i < properties.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(properties[i].Name);
				stringBuilder.Append("=");
				stringBuilder.Append(properties[i].GetValue(this, null));
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}

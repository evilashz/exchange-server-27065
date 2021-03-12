using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.PSDirectInvoke
{
	// Token: 0x02000009 RID: 9
	internal class PSLocalVariableDictionary : IVariableDictionary
	{
		// Token: 0x17000017 RID: 23
		public object this[string name]
		{
			get
			{
				object result;
				this.variables.TryGetValue(name, out result);
				return result;
			}
			set
			{
				this.variables[name] = value;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003418 File Offset: 0x00001618
		public bool ContainsName(string name)
		{
			return this.variables.ContainsKey(name);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003426 File Offset: 0x00001626
		public void Remove(string name)
		{
			this.variables.Remove(name);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003435 File Offset: 0x00001635
		public void Set(string name, object value, VariableScopedOptions scope)
		{
			this.variables[name] = value;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003444 File Offset: 0x00001644
		public bool TryGetValue(string name, out object value)
		{
			return this.variables.TryGetValue(name, out value);
		}

		// Token: 0x0400005B RID: 91
		private readonly Dictionary<string, object> variables = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
	}
}

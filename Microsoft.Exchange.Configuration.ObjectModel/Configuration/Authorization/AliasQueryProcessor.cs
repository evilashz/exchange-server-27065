using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x0200025F RID: 607
	internal sealed class AliasQueryProcessor : RbacQuery.RbacQueryProcessor, INamedQueryProcessor
	{
		// Token: 0x06001539 RID: 5433 RVA: 0x0004E810 File Offset: 0x0004CA10
		public AliasQueryProcessor(string roleName, string key)
		{
			if (string.IsNullOrEmpty(roleName))
			{
				throw new ArgumentNullException("roleName");
			}
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			this.Name = roleName;
			this.key = key;
			this.ProcessKey();
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0004E85D File Offset: 0x0004CA5D
		// (set) Token: 0x0600153B RID: 5435 RVA: 0x0004E865 File Offset: 0x0004CA65
		public string Name { get; private set; }

		// Token: 0x0600153C RID: 5436 RVA: 0x0004E870 File Offset: 0x0004CA70
		public sealed override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			bool value = false;
			foreach (List<RbacQuery.RbacQueryProcessor> list in this.orQueries)
			{
				bool flag = true;
				foreach (RbacQuery.RbacQueryProcessor rbacQueryProcessor in list)
				{
					if (!rbacQueryProcessor.IsInRole(rbacConfiguration))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					value = true;
					break;
				}
			}
			return new bool?(value);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0004E914 File Offset: 0x0004CB14
		private void ProcessKey()
		{
			string[] array = this.key.Split(AliasQueryProcessor.commaSeparator, StringSplitOptions.RemoveEmptyEntries);
			this.orQueries = new List<List<RbacQuery.RbacQueryProcessor>>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(AliasQueryProcessor.plusSeparator, StringSplitOptions.RemoveEmptyEntries);
				List<RbacQuery.RbacQueryProcessor> list = new List<RbacQuery.RbacQueryProcessor>(array2.Length);
				this.orQueries.Add(list);
				foreach (string arg in array2)
				{
					RbacQuery.RbacQueryProcessor item;
					if (!RbacQuery.WellKnownQueryProcessors.TryGetValue(arg, out item))
					{
						throw new ArgumentException(string.Format("Key '{0}' contains a not recongized query: '{1}'. Make sure you only register this alias after all parts had been registered.", this.key, arg));
					}
					list.Add(item);
				}
			}
		}

		// Token: 0x0400065E RID: 1630
		private static char[] commaSeparator = new char[]
		{
			','
		};

		// Token: 0x0400065F RID: 1631
		private static char[] plusSeparator = new char[]
		{
			'+'
		};

		// Token: 0x04000660 RID: 1632
		private readonly string key;

		// Token: 0x04000661 RID: 1633
		private List<List<RbacQuery.RbacQueryProcessor>> orQueries;
	}
}

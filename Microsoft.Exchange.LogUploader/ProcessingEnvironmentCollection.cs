using System;
using System.Configuration;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000031 RID: 49
	[ConfigurationCollection(typeof(ProcessingEnvironment), AddItemName = "Environment")]
	internal class ProcessingEnvironmentCollection : ConfigurationElementCollection
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000B512 File Offset: 0x00009712
		public ProcessingEnvironmentCollection() : base(StringComparer.InvariantCultureIgnoreCase)
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000B51F File Offset: 0x0000971F
		public void Add(ConfigurationElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000B528 File Offset: 0x00009728
		public ProcessingEnvironment Get(object key)
		{
			return (ProcessingEnvironment)base.BaseGet(key);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000B536 File Offset: 0x00009736
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProcessingEnvironment();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000B53D File Offset: 0x0000973D
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProcessingEnvironment)element).Name;
		}
	}
}

using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000002 RID: 2
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class PropertyInformationAttribute : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public PropertyInformationAttribute(string propertyDescription, bool isMandatory)
		{
			if (string.IsNullOrEmpty(propertyDescription))
			{
				throw new ArgumentException("Parameter is either null or empty", "propertyDescription");
			}
			this.description = propertyDescription;
			this.isMandatory = isMandatory;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020FE File Offset: 0x000002FE
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002106 File Offset: 0x00000306
		public bool IsMandatory
		{
			get
			{
				return this.isMandatory;
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly string description;

		// Token: 0x04000002 RID: 2
		private readonly bool isMandatory;
	}
}

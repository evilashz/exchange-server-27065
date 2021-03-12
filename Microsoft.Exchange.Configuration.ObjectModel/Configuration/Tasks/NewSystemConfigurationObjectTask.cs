using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000078 RID: 120
	public abstract class NewSystemConfigurationObjectTask<TDataObject> : NewFixedNameSystemConfigurationObjectTask<TDataObject> where TDataObject : ADObject, new()
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000112EC File Offset: 0x0000F4EC
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x000112FE File Offset: 0x0000F4FE
		[Parameter(Mandatory = true, Position = 0)]
		public string Name
		{
			get
			{
				return this.DataObject.Name;
			}
			set
			{
				this.DataObject.Name = value;
			}
		}
	}
}

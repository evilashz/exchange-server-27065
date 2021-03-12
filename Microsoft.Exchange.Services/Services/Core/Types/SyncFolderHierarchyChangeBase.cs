using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000667 RID: 1639
	[KnownType(typeof(SyncFolderHierarchyDeleteType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(SyncFolderHierarchyCreateOrUpdateType))]
	public abstract class SyncFolderHierarchyChangeBase
	{
		// Token: 0x0600322E RID: 12846 RVA: 0x000B78D6 File Offset: 0x000B5AD6
		public SyncFolderHierarchyChangeBase()
		{
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x0600322F RID: 12847
		public abstract SyncFolderHierarchyChangesEnum ChangeType { get; }

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x000B78DE File Offset: 0x000B5ADE
		// (set) Token: 0x06003231 RID: 12849 RVA: 0x000B78EB File Offset: 0x000B5AEB
		[DataMember(Name = "ChangeType", IsRequired = true)]
		[XmlIgnore]
		public string ChangeTypeString
		{
			get
			{
				return EnumUtilities.ToString<SyncFolderHierarchyChangesEnum>(this.ChangeType);
			}
			set
			{
			}
		}
	}
}

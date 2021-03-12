using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200005D RID: 93
	[DataContract]
	public abstract class BaseRow
	{
		// Token: 0x06001A29 RID: 6697 RVA: 0x00053F75 File Offset: 0x00052175
		protected BaseRow(Identity identity, IConfigurable configurationObject)
		{
			this.Identity = identity;
			this.ConfigurationObject = configurationObject;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00053F8B File Offset: 0x0005218B
		protected BaseRow(ADObjectId identity, IConfigurable configurationObject) : this(identity.ToIdentity(), configurationObject)
		{
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00053F9A File Offset: 0x0005219A
		protected BaseRow(ADObject configurationObject) : this(configurationObject.ToIdentity(), configurationObject)
		{
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00053FA9 File Offset: 0x000521A9
		protected BaseRow(IConfigurable configurationObject) : this((ADObjectId)configurationObject.Identity, configurationObject)
		{
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00053FBD File Offset: 0x000521BD
		protected BaseRow() : this(null, null)
		{
		}

		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x00053FC7 File Offset: 0x000521C7
		// (set) Token: 0x06001A2F RID: 6703 RVA: 0x00053FCF File Offset: 0x000521CF
		public IConfigurable ConfigurationObject { get; private set; }

		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x00053FD8 File Offset: 0x000521D8
		// (set) Token: 0x06001A31 RID: 6705 RVA: 0x00053FE0 File Offset: 0x000521E0
		[DataMember(EmitDefaultValue = false)]
		public Identity Identity { get; private set; }
	}
}

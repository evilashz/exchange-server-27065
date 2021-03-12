using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200010A RID: 266
	[DataContract]
	public class AdObjectResolverRow : BaseRow
	{
		// Token: 0x06001F8F RID: 8079 RVA: 0x0005F166 File Offset: 0x0005D366
		public AdObjectResolverRow(ADRawEntry aDRawEntry) : base(new Identity(aDRawEntry[ADObjectSchema.Guid].ToString(), aDRawEntry[ADObjectSchema.Name].ToString()), aDRawEntry)
		{
			this.ADRawEntry = aDRawEntry;
		}

		// Token: 0x17001A15 RID: 6677
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x0005F19B File Offset: 0x0005D39B
		// (set) Token: 0x06001F91 RID: 8081 RVA: 0x0005F1B2 File Offset: 0x0005D3B2
		[DataMember]
		public virtual string DisplayName
		{
			get
			{
				return (string)this.ADRawEntry[ADObjectSchema.Name];
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001A16 RID: 6678
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x0005F1B9 File Offset: 0x0005D3B9
		// (set) Token: 0x06001F93 RID: 8083 RVA: 0x0005F1C1 File Offset: 0x0005D3C1
		protected internal ADRawEntry ADRawEntry { get; internal set; }

		// Token: 0x04001C7C RID: 7292
		public static PropertyDefinition[] Properties = new ADPropertyDefinition[]
		{
			ADObjectSchema.Guid,
			ADObjectSchema.Name
		};
	}
}

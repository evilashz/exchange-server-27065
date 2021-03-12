using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000DF RID: 223
	[DataContract]
	public abstract class OrgPersonBasicProperties : SetObjectProperties
	{
		// Token: 0x17001987 RID: 6535
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0005AFEA File Offset: 0x000591EA
		// (set) Token: 0x06001DF0 RID: 7664 RVA: 0x0005AFFC File Offset: 0x000591FC
		[DataMember]
		public string FirstName
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.FirstName];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.FirstName] = value;
			}
		}

		// Token: 0x17001988 RID: 6536
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x0005B00A File Offset: 0x0005920A
		// (set) Token: 0x06001DF2 RID: 7666 RVA: 0x0005B01C File Offset: 0x0005921C
		[DataMember]
		public string Initials
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.Initials];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.Initials] = value;
			}
		}

		// Token: 0x17001989 RID: 6537
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x0005B02A File Offset: 0x0005922A
		// (set) Token: 0x06001DF4 RID: 7668 RVA: 0x0005B03C File Offset: 0x0005923C
		[DataMember]
		public string LastName
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.LastName];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.LastName] = value;
			}
		}

		// Token: 0x1700198A RID: 6538
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x0005B04A File Offset: 0x0005924A
		// (set) Token: 0x06001DF6 RID: 7670 RVA: 0x0005B05C File Offset: 0x0005925C
		[DataMember]
		public string DisplayName
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.DisplayName];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.DisplayName] = value;
			}
		}
	}
}

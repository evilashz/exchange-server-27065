using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000315 RID: 789
	[DataContract]
	public class AcePermissionRecipientRow : BaseRow
	{
		// Token: 0x06002E9E RID: 11934 RVA: 0x0008E8C0 File Offset: 0x0008CAC0
		public AcePermissionRecipientRow(Identity identity) : base(identity, null)
		{
		}

		// Token: 0x17001EB2 RID: 7858
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x0008E8CA File Offset: 0x0008CACA
		// (set) Token: 0x06002EA0 RID: 11936 RVA: 0x0008E8D7 File Offset: 0x0008CAD7
		[DataMember]
		public string DisplayName
		{
			get
			{
				return base.Identity.DisplayName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x0008E8E0 File Offset: 0x0008CAE0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			AcePermissionRecipientRow acePermissionRecipientRow = obj as AcePermissionRecipientRow;
			return acePermissionRecipientRow != null && base.Identity.Equals(acePermissionRecipientRow.Identity);
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x0008E90F File Offset: 0x0008CB0F
		public override int GetHashCode()
		{
			return base.Identity.GetHashCode();
		}

		// Token: 0x040022CD RID: 8909
		public static PropertyDefinition[] Properties = new List<PropertyDefinition>
		{
			ADObjectSchema.Guid,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.MasterAccountSid
		}.ToArray();
	}
}

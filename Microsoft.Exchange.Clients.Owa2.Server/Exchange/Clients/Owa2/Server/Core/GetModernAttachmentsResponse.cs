using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003D9 RID: 985
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetModernAttachmentsResponse
	{
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x00077779 File Offset: 0x00075979
		[DataMember(Order = 1)]
		public ModernAttachmentGroup[] AttachmentGroups
		{
			get
			{
				if (this.modernAttachmentGroups == null)
				{
					return null;
				}
				return this.modernAttachmentGroups.ToArray();
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x00077790 File Offset: 0x00075990
		public void AddGroup(ModernAttachmentGroup modernAttachmentGroup)
		{
			if (modernAttachmentGroup != null)
			{
				if (this.modernAttachmentGroups == null)
				{
					this.modernAttachmentGroups = new List<ModernAttachmentGroup>(1);
				}
				this.modernAttachmentGroups.Add(modernAttachmentGroup);
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x000777B5 File Offset: 0x000759B5
		[DataMember(Order = 2)]
		public StructuredErrors[] Errors
		{
			get
			{
				if (this.errors == null)
				{
					return null;
				}
				return this.errors.ToArray();
			}
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x000777CC File Offset: 0x000759CC
		public void AddError(StructuredErrors error)
		{
			if (this.errors == null)
			{
				this.errors = new List<StructuredErrors>(1);
			}
			this.errors.Add(error);
		}

		// Token: 0x040011F7 RID: 4599
		private List<StructuredErrors> errors;

		// Token: 0x040011F8 RID: 4600
		private List<ModernAttachmentGroup> modernAttachmentGroups;
	}
}

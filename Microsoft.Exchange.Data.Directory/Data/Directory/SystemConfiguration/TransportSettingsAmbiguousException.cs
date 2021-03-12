using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A96 RID: 2710
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TransportSettingsAmbiguousException : ADOperationException
	{
		// Token: 0x06007FBC RID: 32700 RVA: 0x001A463C File Offset: 0x001A283C
		public TransportSettingsAmbiguousException(string orgId) : base(DirectoryStrings.TransportSettingsAmbiguousException(orgId))
		{
			this.orgId = orgId;
		}

		// Token: 0x06007FBD RID: 32701 RVA: 0x001A4651 File Offset: 0x001A2851
		public TransportSettingsAmbiguousException(string orgId, Exception innerException) : base(DirectoryStrings.TransportSettingsAmbiguousException(orgId), innerException)
		{
			this.orgId = orgId;
		}

		// Token: 0x06007FBE RID: 32702 RVA: 0x001A4667 File Offset: 0x001A2867
		protected TransportSettingsAmbiguousException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.orgId = (string)info.GetValue("orgId", typeof(string));
		}

		// Token: 0x06007FBF RID: 32703 RVA: 0x001A4691 File Offset: 0x001A2891
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("orgId", this.orgId);
		}

		// Token: 0x17002EBB RID: 11963
		// (get) Token: 0x06007FC0 RID: 32704 RVA: 0x001A46AC File Offset: 0x001A28AC
		public string OrgId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x04005595 RID: 21909
		private readonly string orgId;
	}
}

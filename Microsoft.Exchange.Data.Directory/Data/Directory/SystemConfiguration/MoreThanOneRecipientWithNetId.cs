using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000AA9 RID: 2729
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MoreThanOneRecipientWithNetId : ADExternalException
	{
		// Token: 0x06008015 RID: 32789 RVA: 0x001A4D68 File Offset: 0x001A2F68
		public MoreThanOneRecipientWithNetId(string netId) : base(DirectoryStrings.MoreThanOneRecipientWithNetId(netId))
		{
			this.netId = netId;
		}

		// Token: 0x06008016 RID: 32790 RVA: 0x001A4D7D File Offset: 0x001A2F7D
		public MoreThanOneRecipientWithNetId(string netId, Exception innerException) : base(DirectoryStrings.MoreThanOneRecipientWithNetId(netId), innerException)
		{
			this.netId = netId;
		}

		// Token: 0x06008017 RID: 32791 RVA: 0x001A4D93 File Offset: 0x001A2F93
		protected MoreThanOneRecipientWithNetId(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.netId = (string)info.GetValue("netId", typeof(string));
		}

		// Token: 0x06008018 RID: 32792 RVA: 0x001A4DBD File Offset: 0x001A2FBD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("netId", this.netId);
		}

		// Token: 0x17002EC8 RID: 11976
		// (get) Token: 0x06008019 RID: 32793 RVA: 0x001A4DD8 File Offset: 0x001A2FD8
		public string NetId
		{
			get
			{
				return this.netId;
			}
		}

		// Token: 0x040055A2 RID: 21922
		private readonly string netId;
	}
}

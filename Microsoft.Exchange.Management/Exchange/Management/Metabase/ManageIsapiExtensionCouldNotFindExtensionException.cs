using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FA8 RID: 4008
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ManageIsapiExtensionCouldNotFindExtensionException : LocalizedException
	{
		// Token: 0x0600AD10 RID: 44304 RVA: 0x00290EAD File Offset: 0x0028F0AD
		public ManageIsapiExtensionCouldNotFindExtensionException(string groupId, string binary) : base(Strings.ManageIsapiExtensionCouldNotFindExtensionException(groupId, binary))
		{
			this.groupId = groupId;
			this.binary = binary;
		}

		// Token: 0x0600AD11 RID: 44305 RVA: 0x00290ECA File Offset: 0x0028F0CA
		public ManageIsapiExtensionCouldNotFindExtensionException(string groupId, string binary, Exception innerException) : base(Strings.ManageIsapiExtensionCouldNotFindExtensionException(groupId, binary), innerException)
		{
			this.groupId = groupId;
			this.binary = binary;
		}

		// Token: 0x0600AD12 RID: 44306 RVA: 0x00290EE8 File Offset: 0x0028F0E8
		protected ManageIsapiExtensionCouldNotFindExtensionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupId = (string)info.GetValue("groupId", typeof(string));
			this.binary = (string)info.GetValue("binary", typeof(string));
		}

		// Token: 0x0600AD13 RID: 44307 RVA: 0x00290F3D File Offset: 0x0028F13D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupId", this.groupId);
			info.AddValue("binary", this.binary);
		}

		// Token: 0x1700378D RID: 14221
		// (get) Token: 0x0600AD14 RID: 44308 RVA: 0x00290F69 File Offset: 0x0028F169
		public string GroupId
		{
			get
			{
				return this.groupId;
			}
		}

		// Token: 0x1700378E RID: 14222
		// (get) Token: 0x0600AD15 RID: 44309 RVA: 0x00290F71 File Offset: 0x0028F171
		public string Binary
		{
			get
			{
				return this.binary;
			}
		}

		// Token: 0x040060F3 RID: 24819
		private readonly string groupId;

		// Token: 0x040060F4 RID: 24820
		private readonly string binary;
	}
}

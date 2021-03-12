using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x020000A6 RID: 166
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreManagerGroupNotFoundException : DxStoreManagerClientException
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x0001474C File Offset: 0x0001294C
		public DxStoreManagerGroupNotFoundException(string groupName) : base(Strings.DxStoreManagerGroupNotFoundException(groupName))
		{
			this.groupName = groupName;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00014766 File Offset: 0x00012966
		public DxStoreManagerGroupNotFoundException(string groupName, Exception innerException) : base(Strings.DxStoreManagerGroupNotFoundException(groupName), innerException)
		{
			this.groupName = groupName;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00014781 File Offset: 0x00012981
		protected DxStoreManagerGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupName = (string)info.GetValue("groupName", typeof(string));
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000147AB File Offset: 0x000129AB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupName", this.groupName);
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x000147C6 File Offset: 0x000129C6
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x04000294 RID: 660
		private readonly string groupName;
	}
}

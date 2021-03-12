using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000DE RID: 222
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IPv6NetworksHasDuplicateEntries : ClusCommonValidationFailedException
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x0001C5D6 File Offset: 0x0001A7D6
		public IPv6NetworksHasDuplicateEntries(string duplicate) : base(Strings.IPv6NetworksHasDuplicateEntries(duplicate))
		{
			this.duplicate = duplicate;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001C5F0 File Offset: 0x0001A7F0
		public IPv6NetworksHasDuplicateEntries(string duplicate, Exception innerException) : base(Strings.IPv6NetworksHasDuplicateEntries(duplicate), innerException)
		{
			this.duplicate = duplicate;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001C60B File Offset: 0x0001A80B
		protected IPv6NetworksHasDuplicateEntries(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.duplicate = (string)info.GetValue("duplicate", typeof(string));
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001C635 File Offset: 0x0001A835
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("duplicate", this.duplicate);
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0001C650 File Offset: 0x0001A850
		public string Duplicate
		{
			get
			{
				return this.duplicate;
			}
		}

		// Token: 0x04000737 RID: 1847
		private readonly string duplicate;
	}
}
